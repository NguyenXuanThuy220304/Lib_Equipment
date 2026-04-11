using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Lib_Equipment
{
    public partial class frmSecondaryBorrow : Form
    {
        private string readerID;
        private string fullName;
        private string apiKey;

        // GIỎ HÀNG & GIAO DIỆN TỰ ĐỘNG (POS)
        private DataTable dtGioHang;
        private RichTextBox rtbPhieuMuonLive; // Box hiển thị phiếu mượn trực tiếp
        private Button btnXacNhanPOS; // Nút xác nhận tự tạo bằng code

        public frmSecondaryBorrow(string readerID, string fullName, string apiKey)
        {
            InitializeComponent();
            this.readerID = readerID;
            this.fullName = fullName;
            this.apiKey = apiKey;

            // Khởi tạo Giỏ hàng
            dtGioHang = new DataTable();
            dtGioHang.Columns.Add("Mã Bản Sao");
            dtGioHang.Columns.Add("Tên Sách");

            this.Load += FrmSecondaryBorrow_Load;

            // Ép con trỏ chuột luôn nằm ở ô quét mã
            this.Click += (s, e) => { txtBarcodeScanner.Focus(); };
            txtBarcodeScanner.Leave += (s, e) => { txtBarcodeScanner.Focus(); };
        }

        private void FrmSecondaryBorrow_Load(object sender, EventArgs e)
        {
            txtBarcodeScanner.Clear();
            txtBarcodeScanner.Focus();

            // ==========================================================
            // TỰ ĐỘNG TẠO GIAO DIỆN "PHIẾU MƯỢN LIVE" (Không cần kéo thả)
            // ==========================================================
            TaoGiaoDienPhieuLive();
            CapNhatPhieuLive(); // Hiển thị khung phiếu trống ban đầu
        }

        private void TaoGiaoDienPhieuLive()
        {
            // 1. Ẩn bớt các Label cũ (như "Vui lòng quét mã vạch...") để không bị đè chữ
            foreach (Control ctrl in this.Controls)
            {
                // Giữ lại ô Textbox quét mã (nếu có) và thanh tiêu đề (Panel)
                if (ctrl.Name == "txtBarcodeScanner" || ctrl.Name == "pnlHeader" || ctrl.Name == "btnClose") continue;
                ctrl.Visible = false;
            }

            // 2. Tính toán kích thước tự động co giãn theo Form của bạn
            int margin = 20;
            int topOffset = 70; // Khoảng cách từ trên xuống (chừa chỗ cho ô nhập mã)
            int btnHeight = 60;

            // Tạo Box hiển thị Phiếu mượn (Mô phỏng tờ biên lai thật)
            rtbPhieuMuonLive = new RichTextBox();
            rtbPhieuMuonLive.Name = "rtbPhieuMuonLive";
            rtbPhieuMuonLive.Font = new Font("Courier New", 12, FontStyle.Bold);
            // Tự động kéo dãn lấp đầy khoảng trống
            rtbPhieuMuonLive.Location = new Point(margin, topOffset);
            rtbPhieuMuonLive.Size = new Size(this.ClientSize.Width - (margin * 2), this.ClientSize.Height - topOffset - btnHeight - margin - 10);
            rtbPhieuMuonLive.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbPhieuMuonLive.ReadOnly = true;
            rtbPhieuMuonLive.BackColor = Color.FromArgb(255, 255, 240);
            rtbPhieuMuonLive.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(rtbPhieuMuonLive);
            rtbPhieuMuonLive.BringToFront(); // Ép nổi lên trên cùng

            // Tạo Nút Xác Nhận to, rõ ràng ở sát đáy Form
            btnXacNhanPOS = new Button();
            btnXacNhanPOS.Name = "btnXacNhanPOS";
            btnXacNhanPOS.Text = "XÁC NHẬN MƯỢN & IN BIÊN LAI";
            btnXacNhanPOS.Location = new Point(margin, this.ClientSize.Height - btnHeight - margin);
            btnXacNhanPOS.Size = new Size(this.ClientSize.Width - (margin * 2), btnHeight);
            btnXacNhanPOS.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnXacNhanPOS.BackColor = Color.FromArgb(40, 167, 69);
            btnXacNhanPOS.ForeColor = Color.White;
            btnXacNhanPOS.FlatStyle = FlatStyle.Flat;
            btnXacNhanPOS.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnXacNhanPOS.Cursor = Cursors.Hand;
            btnXacNhanPOS.Click += BtnXacNhanPOS_Click;
            this.Controls.Add(btnXacNhanPOS);
            btnXacNhanPOS.BringToFront(); // Ép nổi lên trên cùng
        }

        // ==========================================================
        // CẬP NHẬT GIAO DIỆN PHIẾU NGAY SAU MỖI LẦN QUÉT
        // ==========================================================
        private void CapNhatPhieuLive()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("==================================================");
            sb.AppendLine("             PHIẾU MƯỢN SÁCH TẠM THỜI             ");
            sb.AppendLine("==================================================");
            sb.AppendLine($" Độc giả: {fullName} ({readerID})");
            sb.AppendLine($" Ngày   : {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine(" STT | MÃ SÁCH       | TÊN SÁCH");
            sb.AppendLine("--------------------------------------------------");

            // Đổ danh sách từ giỏ hàng vào phiếu
            for (int i = 0; i < dtGioHang.Rows.Count; i++)
            {
                string ma = dtGioHang.Rows[i]["Mã Bản Sao"].ToString();
                string ten = dtGioHang.Rows[i]["Tên Sách"].ToString();
                if (ten.Length > 22) ten = ten.Substring(0, 19) + "..."; // Cắt chữ dài

                sb.AppendLine($" {i + 1,-3} | {ma,-13} | {ten}");
            }

            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine($" TỔNG ĐANG CHỜ MƯỢN: {dtGioHang.Rows.Count} CUỐN");
            sb.AppendLine("==================================================");

            rtbPhieuMuonLive.Text = sb.ToString();

            // Tự động cuộn xuống dòng cuối cùng
            rtbPhieuMuonLive.SelectionStart = rtbPhieuMuonLive.Text.Length;
            rtbPhieuMuonLive.ScrollToCaret();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            txtBarcodeScanner.Clear();
            txtBarcodeScanner.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ==========================================================
        // QUÉT MÃ -> NHẬN DIỆN -> ĐƯA THẲNG VÀO PHIẾU
        // ==========================================================
        private void txtBarcodeScanner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string copyId = txtBarcodeScanner.Text.Trim();
                txtBarcodeScanner.Clear();

                if (string.IsNullOrEmpty(copyId)) return;

                // 1. Kiểm tra xem sách đã quét vào giỏ chưa
                foreach (DataRow row in dtGioHang.Rows)
                {
                    if (row["Mã Bản Sao"].ToString() == copyId)
                    {
                        MessageBox.Show("Cuốn sách này đã nằm trong Phiếu mượn!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 2. Kiểm tra CSDL
                string checkQuery = $@"
                    SELECT b.Title, bc.Status 
                    FROM BookCopy bc 
                    JOIN Book b ON bc.BookID = b.BookID 
                    WHERE bc.CopyID = @copyID AND bc.IsDeleted = 0";

                SqlParameter[] paramCheck = { new SqlParameter("@copyID", copyId) };
                DataTable dtSach = DataProvider.Instance.ExecuteQuery(checkQuery, paramCheck);

                if (dtSach.Rows.Count > 0)
                {
                    string tenSach = dtSach.Rows[0]["Title"].ToString();
                    string trangThai = dtSach.Rows[0]["Status"].ToString();

                    if (trangThai == "Có sẵn")
                    {
                        // Thêm vào giỏ hàng
                        dtGioHang.Rows.Add(copyId, tenSach);

                        // Cập nhật lại giao diện phiếu Live
                        CapNhatPhieuLive();

                        // Nếu có Label trạng thái, báo câu xanh lá
                        if (this.Controls.ContainsKey("lblStatus"))
                        {
                            this.Controls["lblStatus"].Text = $"Đã đưa sách '{tenSach}' vào phiếu chờ.";
                            this.Controls["lblStatus"].ForeColor = Color.Green;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Cuốn sách này hiện đang: '{trangThai}'. Không thể mượn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Mã vạch không hợp lệ hoặc sách không có trong hệ thống!", "Lỗi Quét", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txtBarcodeScanner.Focus();
            }
        }

        // ==========================================================
        // XÁC NHẬN CHỐT ĐƠN -> GỌI AI ĐÁNH GIÁ CHUNG -> LƯU DB -> IN ẢNH PNG
        // ==========================================================
        private async void BtnXacNhanPOS_Click(object sender, EventArgs e)
        {
            if (dtGioHang.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu mượn đang trống! Vui lòng quét mã sách trước.", "Thông báo");
                return;
            }

            // Giao diện chờ
            btnXacNhanPOS.Text = "ĐANG XỬ LÝ VÀ CHỜ AI ĐÁNH GIÁ...";
            btnXacNhanPOS.Enabled = false;

            // 1. Đưa list sách cho AI đánh giá gu đọc sách
            string danhSachTenSach = "";
            foreach (DataRow row in dtGioHang.Rows)
            {
                danhSachTenSach += row["Tên Sách"].ToString() + ", ";
            }
            string aiPrompt = $"Độc giả {fullName} vừa quyết định mượn các sách sau: {danhSachTenSach}. Hãy viết 1 câu ngắn (dưới 25 chữ) nhận xét sự kết hợp sách này hoặc chúc họ học tập hiệu quả.";
            string aiMessage = await CallGeminiAPI(aiPrompt);

            // 2. Lưu Database
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.AppendLine("BEGIN TRY");
            sbQuery.AppendLine("BEGIN TRAN;");

            sbQuery.AppendLine("INSERT INTO BorrowRecord (ReaderID, CreatedBy, BorrowDate, DueDate, Status, IsDeleted) VALUES (@readerId, NULL, GETDATE(), DATEADD(day, 14, GETDATE()), N'Đang mượn', 0);");
            sbQuery.AppendLine("DECLARE @newRecordId INT = SCOPE_IDENTITY();");

            foreach (DataRow row in dtGioHang.Rows)
            {
                string cId = row["Mã Bản Sao"].ToString();
                sbQuery.AppendLine($"INSERT INTO BorrowDetail (RecordID, CopyID, FineAmount) VALUES (@newRecordId, '{cId}', 0);");
                sbQuery.AppendLine($"UPDATE BookCopy SET Status = N'Đang mượn' WHERE CopyID = '{cId}';");
            }

            sbQuery.AppendLine("SELECT @newRecordId;");
            sbQuery.AppendLine("COMMIT TRAN;");
            sbQuery.AppendLine("END TRY BEGIN CATCH ROLLBACK TRAN; THROW; END CATCH");

            SqlParameter[] param = { new SqlParameter("@readerId", readerID) };

            try
            {
                object result = DataProvider.Instance.ExecuteScalar(sbQuery.ToString(), param);
                string recordId = result.ToString();

                MessageBox.Show($"Hoàn tất! Hệ thống đã ghi nhận phiếu mượn gồm {dtGioHang.Rows.Count} cuốn sách.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 3. Kích hoạt vẽ Ảnh Biên Lai (in luôn lời AI lên phiếu)
                XuatPhieuRaAnh(recordId, readerID, fullName, dtGioHang, aiMessage);

                this.Close(); // Đóng form để sinh viên mang sách ra về
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi tạo phiếu mượn: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnXacNhanPOS.Enabled = true;
                btnXacNhanPOS.Text = "XÁC NHẬN MƯỢN & IN BIÊN LAI";
            }
        }

        // ==========================================================
        // HÀM XUẤT ẢNH PNG (NHƯ GIẤY MÁY IN BILL THẬT)
        // ==========================================================
        private void XuatPhieuRaAnh(string maPhieu, string maSV, string tenSV, DataTable gioHang, string loiChucAI)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "HoaDon_ThuVien");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                string safeName = tenSV.Replace(" ", "");
                string fileName = $"Bill_{safeName}_{DateTime.Now.ToString("ddMMyyyy_HHmmss")}.png";
                string filePath = Path.Combine(folderPath, fileName);

                int billWidth = 550;
                int baseHeight = 520;
                int itemHeight = 35;
                int billHeight = baseHeight + (gioHang.Rows.Count * itemHeight);

                using (Bitmap bmp = new Bitmap(billWidth, billHeight))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);

                        Font fontTitle = new Font("Courier New", 20, FontStyle.Bold);
                        Font fontHeader = new Font("Courier New", 14, FontStyle.Bold);
                        Font fontBody = new Font("Courier New", 12, FontStyle.Regular);
                        Font fontItalic = new Font("Courier New", 12, FontStyle.Italic);
                        Brush brush = Brushes.Black;
                        Pen pen = new Pen(Color.Black, 2);

                        int y = 20;
                        int xMargin = 20;

                        g.DrawString("THƯ VIỆN ĐẠI HỌC UNETI", fontTitle, brush, 100, y); y += 40;
                        g.DrawString("PHIẾU MƯỢN SÁCH ĐIỆN TỬ", fontHeader, brush, 130, y); y += 50;

                        g.DrawString($"Mã phiếu: #{maPhieu}", fontBody, brush, xMargin, y); y += 30;
                        g.DrawString($"Ngày lập: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}", fontBody, brush, xMargin, y); y += 30;
                        g.DrawString($"Hạn trả : {DateTime.Now.AddDays(14).ToString("dd/MM/yyyy")} (14 ngày)", fontHeader, brush, xMargin, y); y += 40;
                        g.DrawString($"Độc giả : {tenSV} ({maSV})", fontBody, brush, xMargin, y); y += 40;

                        g.DrawLine(pen, xMargin, y, billWidth - xMargin, y); y += 15;
                        g.DrawString("STT  MÃ SÁCH       TÊN SÁCH", fontHeader, brush, xMargin, y); y += 30;
                        g.DrawLine(pen, xMargin, y, billWidth - xMargin, y); y += 20;

                        for (int i = 0; i < gioHang.Rows.Count; i++)
                        {
                            string copyId = gioHang.Rows[i]["Mã Bản Sao"].ToString();
                            string title = gioHang.Rows[i]["Tên Sách"].ToString();
                            if (title.Length > 20) title = title.Substring(0, 17) + "...";

                            g.DrawString($"{i + 1,-4} {copyId,-13} {title}", fontBody, brush, xMargin, y);
                            y += itemHeight;
                        }

                        g.DrawLine(pen, xMargin, y, billWidth - xMargin, y); y += 20;
                        g.DrawString($"TỔNG SỐ CUỐN: {gioHang.Rows.Count}", fontHeader, brush, xMargin, y); y += 40;

                        // Chữ ký AI
                        RectangleF rectAI = new RectangleF(xMargin, y, billWidth - (xMargin * 2), 80);
                        g.DrawString($"🤖 Lời nhắn từ AI Thư Viện:\n\"{loiChucAI}\"", fontItalic, brush, rectAI);
                        y += 70;

                        g.DrawString("Thủ thư duyệt", fontBody, brush, 50, y);
                        g.DrawString("Độc giả ký nhận", fontBody, brush, 320, y);
                        y += 80;

                        g.DrawString("*** Cảm ơn quý độc giả ***", new Font("Courier New", 12, FontStyle.Italic), brush, 140, y);
                    }
                    bmp.Save(filePath, ImageFormat.Png);
                }

                // Mở tự động ảnh PNG bằng ứng dụng của Windows
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo file ảnh: " + ex.Message, "Lỗi in ấn");
            }
        }

        private async Task<string> CallGeminiAPI(string prompt)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";
                    var payload = new { contents = new[] { new { parts = new[] { new { text = prompt } } } }, generationConfig = new { temperature = 0.8 } };

                    var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        JObject jsonResponse = JObject.Parse(responseString);
                        return jsonResponse["candidates"][0]["content"]["parts"][0]["text"].ToString().Trim();
                    }
                    return "Cảm ơn bạn đã lựa chọn những cuốn sách này. Chúc bạn học tập tốt!";
                }
            }
            catch
            {
                return "Hệ thống đã ghi nhận sách. Chúc bạn một ngày tốt lành!";
            }
        }
    }
}