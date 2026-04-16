using Lib_Equipment.BLL;
using Lib_Equipment.DAO;
using Lib_Equipment.Database;
using Lib_Equipment.DTO;
using Lib_Equipment.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            sb.AppendLine("              PHIẾU MƯỢN SÁCH TẠM THỜI             ");
            sb.AppendLine("==================================================");
            sb.AppendLine($" Độc giả: {fullName} ({readerID})");
            sb.AppendLine($" Ngày    : {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine(" STT | MÃ SÁCH       | TÊN SÁCH");
            sb.AppendLine("--------------------------------------------------");

            for (int i = 0; i < dtGioHang.Rows.Count; i++)
            {
                string ma = dtGioHang.Rows[i]["Mã Bản Sao"].ToString();
                string ten = dtGioHang.Rows[i]["Tên Sách"].ToString();
                if (ten.Length > 22) ten = ten.Substring(0, 19) + "...";

                sb.AppendLine($" {i + 1,-3} | {ma,-13} | {ten}");
            }

            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine($" TỔNG ĐANG CHỜ MƯỢN: {dtGioHang.Rows.Count} CUỐN");
            sb.AppendLine("==================================================");

            // Thêm hướng dẫn hủy
            if (dtGioHang.Rows.Count > 0)
            {
                sb.AppendLine("");
                sb.AppendLine(" 💡 Mẹo: Quét lại mã sách đã chọn để HỦY MƯỢN.");
            }

            rtbPhieuMuonLive.Text = sb.ToString();
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

                // ==========================================================
                // LOGIC MỚI: KIỂM TRA ĐỂ HỦY MƯỢN
                // ==========================================================
                DataRow foundRow = null;
                foreach (DataRow row in dtGioHang.Rows)
                {
                    if (row["Mã Bản Sao"].ToString().ToUpper() == copyId.ToUpper())
                    {
                        foundRow = row;
                        break;
                    }
                }

                if (foundRow != null)
                {
                    string tenSachHuy = foundRow["Tên Sách"].ToString();
                    DialogResult dr = MessageBox.Show($"Cuốn sách '{tenSachHuy}' đã có trong phiếu.\nBạn có muốn HỦY MƯỢN cuốn này không?",
                                                     "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        dtGioHang.Rows.Remove(foundRow);
                        CapNhatPhieuLive();
                        return; // Kết thúc xử lý
                    }
                    return;
                }

                // --- Logic thêm mới sách (giữ nguyên của bạn) ---
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
                        dtGioHang.Rows.Add(copyId, tenSach);
                        CapNhatPhieuLive();
                    }
                    else
                    {
                        MessageBox.Show($"Cuốn sách này hiện đang: '{trangThai}'. Không thể mượn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Mã vạch không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Lấy tên người dùng đang đăng nhập (hoặc mặc định là "admin" nếu chưa có)
            string username = AppSession.Username ?? "Reader";

            // ==========================================================
            // 1. KIỂM TRA LUẬT MƯỢN (Giới hạn số lượng & Nợ quá hạn)
            // ==========================================================
            DataTable dtReader = DataProvider.Instance.ExecuteQuery("SELECT ReaderType, Status FROM Reader WHERE ReaderID = @id", new SqlParameter[] { new SqlParameter("@id", readerID) });
            if (dtReader.Rows.Count == 0) return;

            // Tạo đối tượng DTO để truyền vào BLL
            DocGiaDTO docGia = new DocGiaDTO
            {
                ReaderID = this.readerID,
                ReaderType = dtReader.Rows[0]["ReaderType"].ToString(),
                Status = Convert.ToInt32(dtReader.Rows[0]["Status"])
            };

            // Kiểm tra thẻ khóa / nợ quá hạn
            if (!MuonTraBLL.Instance.ValidateBorrow(docGia, out string message))
            {
                MessageBox.Show(message, "Vi phạm quy định", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // Kiểm tra hạn mức (Sinh viên: 6, Giảng viên: 9)
            int currentBorrowed = MuonTraDAO.Instance.CountBorrowedBooks(readerID);
            bool isVIP = docGia.ReaderType.Contains("Giảng viên");
            int maxLimit = isVIP ? 9 : 6;

            if (currentBorrowed + dtGioHang.Rows.Count > maxLimit)
            {
                MessageBox.Show($"Vượt quá hạn mức! Bạn là '{docGia.ReaderType}' chỉ được mượn tối đa {maxLimit} cuốn.\n\nĐang giữ: {currentBorrowed} cuốn.\nMuốn mượn thêm: {dtGioHang.Rows.Count} cuốn.", "Quá tải", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy số ngày mượn tiêu chuẩn (30 hoặc 45)
            int allowedDays = isVIP ? 45 : 30;

            // ==========================================================
            // 2. CHỐT ĐƠN VÀ LƯU DATABASE
            // ==========================================================
            btnXacNhanPOS.Text = "ĐANG XỬ LÝ VÀ CHỜ AI ĐÁNH GIÁ...";
            btnXacNhanPOS.Enabled = false;

            string danhSachTenSach = "";
            foreach (DataRow row in dtGioHang.Rows) { danhSachTenSach += row["Tên Sách"].ToString() + ", "; }
            string aiPrompt = $"Độc giả {fullName} vừa quyết định mượn các sách sau: {danhSachTenSach}. Hãy viết 1 câu ngắn (dưới 25 chữ) nhận xét sự kết hợp sách này hoặc chúc họ học tập hiệu quả.";
            string aiMessage = await CallGeminiAPI(aiPrompt);

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.AppendLine("BEGIN TRY");
            sbQuery.AppendLine("BEGIN TRAN;");

            // ĐÃ SỬA: Thay đổi NULL thành @user để lưu tên người tạo phiếu
            sbQuery.AppendLine("INSERT INTO BorrowRecord (ReaderID, CreatedBy, BorrowDate, DueDate, Status, IsDeleted) VALUES (@readerId, @user, GETDATE(), DATEADD(day, @days, GETDATE()), N'Đang mượn', 0);");
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

            // ĐÃ SỬA: Bổ sung tham số @user vào danh sách Parameters
            SqlParameter[] param = {
        new SqlParameter("@readerId", readerID),
        new SqlParameter("@days", allowedDays),
        new SqlParameter("@user", username)
    };

            try
            {
                string recordId = DataProvider.Instance.ExecuteScalar(sbQuery.ToString(), param).ToString();
                MessageBox.Show($"Hoàn tất! Hệ thống đã ghi nhận phiếu mượn gồm {dtGioHang.Rows.Count} cuốn sách.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Truyền thêm allowedDays vào hàm xuất ảnh để in đúng hạn trả
                XuatPhieuRaAnh(recordId, readerID, fullName, dtGioHang, aiMessage, allowedDays);

                this.Close();
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
        private void XuatPhieuRaAnh(string maPhieu, string maSV, string tenSV, DataTable gioHang, string loiChucAI, int allowedDays)
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

                        // IN HẠN TRẢ ĐÚNG THEO LUẬT (30 HOẶC 45)
                        g.DrawString($"Hạn trả : {DateTime.Now.AddDays(allowedDays).ToString("dd/MM/yyyy")} ({allowedDays} ngày)", fontHeader, brush, xMargin, y); y += 40;

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