using Lib_Equipment.BLL;
using Lib_Equipment.DAO;
using Lib_Equipment.Database;
using Lib_Equipment.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmQuanLyDocGia : Form
    {
        private string selectedReaderID = "";

        public FrmQuanLyDocGia()
        {
            InitializeComponent();
        }

        private void FrmQuanLyDocGia_Load(object sender, EventArgs e)
        {
            // 1. Load các ComboBox (Khoa, Loại thẻ...)
            cboDonVi.DataSource = DocGiaBLL.Instance.LayDanhSachKhoaVien();
            cboDonVi.DisplayMember = "DepartmentName";
            cboDonVi.ValueMember = "DepartmentID";

            // 2. TỰ ĐỘNG QUÉT HỆ THỐNG
            try
            {
                // Quét gửi mail nhắc nhở & xử lý kỷ luật (Ngày 3, Ngày 31)
                MuonTraBLL.Instance.TuDongKiemTraVaGuiMailLuuLuu();
            }
            catch { /* Bỏ qua lỗi kết nối SMTP để Form tiếp tục load */ }

            // 3. Hiển thị dữ liệu (RefreshGrid đã có sẵn lệnh AutoUpdateDebt)
            RefreshGrid();
        }

        private void LoadData()
        {
            dgvDocGia.DataSource = DocGiaBLL.Instance.LayDanhSachDocGia();
            dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // GIỮ NGUYÊN LOGIC CỦA BẠN: Tự động update nợ
            //DocGiaDAO.Instance.AutoUpdateDebt();
            RefreshGrid();

            if (dgvDocGia.Columns.Contains("Công nợ (VNĐ)"))
                dgvDocGia.Columns["Công nợ (VNĐ)"].DefaultCellStyle.Format = "N0";
        }

        private void dgvDocGia_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvDocGia.Rows)
            {
                if (row.Cells["Trạng thái"].Value != null)
                {
                    string trangThai = row.Cells["Trạng thái"].Value.ToString();
                    if (trangThai == "CẤM VĨNH VIỄN")
                    {
                        row.DefaultCellStyle.BackColor = Color.MistyRose;
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else if (trangThai == "Đang bị khóa")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = Color.OrangeRed;
                    }
                }
            }
        }

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDocGia.Rows[e.RowIndex];

                // 1. Gán các thông tin chữ
                selectedReaderID = row.Cells["Mã Độc giả"].Value?.ToString();
                txtMaDocGia.Text = selectedReaderID;
                txtHoTen.Text = row.Cells["Họ và tên"].Value?.ToString();
                txtMail.Text = row.Cells["Email"].Value?.ToString();

                // 2. TÌM TÊN KHOA TRONG COMBOBOX (Quan trọng nhất)
                string tenKhoaTrenBang = row.Cells["Khoa/Viện"].Value?.ToString();
                // Lệnh này sẽ tìm đúng cái tên khoa đó trong danh sách của ComboBox
                int index = cboDonVi.FindStringExact(tenKhoaTrenBang);
                if (index != -1)
                {
                    cboDonVi.SelectedIndex = index;
                }

                // 3. Các thông tin khác
                cboLoaiDocGia.Text = row.Cells["Loại thẻ"].Value?.ToString();

                string trangThai = row.Cells["Trạng thái"].Value?.ToString();
                cboTrangThai.Text = (trangThai == "CẤM VĨNH VIỄN") ? "Khóa" : trangThai;

                txtMaDocGia.Enabled = false;
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string deptId = cboDonVi.SelectedValue?.ToString() ?? "";
            int status = cboTrangThai.Text.Contains("Hoạt động") ? 1 : 0;
            string email = txtMail.Text.Trim();

            // Khai báo biến để nhận giá trị từ out parameter
            string msg;

            bool isSuccess = DocGiaBLL.Instance.ThemDocGia(
                txtMaDocGia.Text.Trim(),
                txtHoTen.Text.Trim(),
                deptId,
                cboLoaiDocGia.Text,
                status,
                email,
                out msg
            );

            if (isSuccess)
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                btnLamMoi_Click(null, null);
            }
            else
            {
                // Hiển thị lỗi từ msg đã được gán trong BLL
                MessageBox.Show(msg, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string deptId = cboDonVi.SelectedValue?.ToString() ?? "";
            int status = cboTrangThai.Text.Contains("Hoạt động") ? 1 : 0;
            if (DocGiaBLL.Instance.SuaDocGia(selectedReaderID, txtHoTen.Text.Trim(), deptId, cboLoaiDocGia.Text, status, txtMail.Text))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedReaderID)) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (DocGiaBLL.Instance.XoaDocGia(selectedReaderID))
                {
                    MessageBox.Show("Đã xóa độc giả!", "Thông báo");
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            selectedReaderID = ""; txtMaDocGia.Enabled = true; txtMaDocGia.Clear(); txtHoTen.Clear();
            if (cboDonVi.Items.Count > 0) cboDonVi.SelectedIndex = 0;
            if (cboLoaiDocGia.Items.Count > 0) cboLoaiDocGia.SelectedIndex = 0;
            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        }

        private void RefreshGrid()
        {
            try
            {
                // =====================================================================
                // THUẬT TOÁN TỰ ĐỘNG KHÓA / MỞ KHÓA TÀI KHOẢN (THAY THẾ HÀM CŨ)
                // =====================================================================
                string sqlUpdateStatus = @"
            -- 1. TẠM KHÓA (Status = 0) những ai đang có sách trễ hạn (từ 1 ngày trở lên)
            UPDATE Reader 
            SET Status = 0 
            WHERE ReaderID IN (
                SELECT DISTINCT br.ReaderID 
                FROM BorrowRecord br 
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID 
                WHERE bd.ReturnDate IS NULL AND CAST(br.DueDate AS DATE) < CAST(GETDATE() AS DATE)
            );

            -- 2. CẤM VĨNH VIỄN (IsPermanentlyBanned = 1) những ai trễ quá 30 ngày
            UPDATE Reader 
            SET IsPermanentlyBanned = 1, Status = 0 
            WHERE ReaderID IN (
                SELECT DISTINCT br.ReaderID 
                FROM BorrowRecord br 
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID 
                WHERE bd.ReturnDate IS NULL AND DATEDIFF(day, br.DueDate, GETDATE()) > 30
            );

            -- 3. TỰ ĐỘNG MỞ KHÓA (Status = 1) cho những ai ĐÃ TRẢ HẾT sách quá hạn
            -- (Và đảm bảo họ không nằm trong danh sách đen Cấm vĩnh viễn)
            UPDATE Reader 
            SET Status = 1 
            WHERE Status = 0 
            AND ISNULL(IsPermanentlyBanned, 0) = 0  -- ĐÃ VÁ LỖI DBNULL TẠI ĐÂY
            AND ReaderID NOT IN (
                SELECT DISTINCT br.ReaderID 
                FROM BorrowRecord br 
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID 
                WHERE bd.ReturnDate IS NULL AND CAST(br.DueDate AS DATE) < CAST(GETDATE() AS DATE)
            );
        ";
                // Chạy lệnh tự động cập nhật trạng thái trước khi lấy dữ liệu
                DataProvider.Instance.ExecuteNonQuery(sqlUpdateStatus, null);

                // =====================================================================
                // LẤY DỮ LIỆU ĐÃ CẬP NHẬT LÊN LƯỚI
                // =====================================================================
                // Thay đổi câu query để lấy Tên khoa thay vì Mã khoa
                string query = @"SELECT r.ReaderID AS [Mã Độc giả], 
                       r.FullName AS [Họ và tên], 
                       d.DepartmentName AS [Khoa/Viện], -- Lấy tên thay vì mã
                       r.ReaderType AS [Loại thẻ], 
                       r.Email, 
                       r.AcademicDebt AS [Công nợ (VNĐ)],
                       CASE 
                          WHEN r.IsPermanentlyBanned = 1 THEN N'CẤM VĨNH VIỄN' 
                          WHEN r.Status = 1 THEN N'Hoạt động' 
                          ELSE N'Đang bị khóa' 
                       END AS [Trạng thái]
                FROM Reader r
                LEFT JOIN Department d ON r.DepartmentID = d.DepartmentID -- Join với bảng Khoa
                WHERE r.IsDeleted = 0 OR r.IsDeleted IS NULL";

                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                dgvDocGia.DataSource = dt;

                // Tô màu trực quan cho Độc giả
                foreach (DataGridViewRow row in dgvDocGia.Rows)
                {
                    string trangThai = row.Cells["Trạng thái"].Value?.ToString();
                    if (trangThai == "CẤM VĨNH VIỄN")
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        row.DefaultCellStyle.Font = new Font(dgvDocGia.Font, FontStyle.Bold);
                        row.DefaultCellStyle.BackColor = Color.MistyRose;
                    }
                    else if (trangThai == "Đang bị khóa")
                    {
                        row.DefaultCellStyle.ForeColor = Color.OrangeRed;
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                    else
                    {
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
        private void btnDongBo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hệ thống sẽ cấp tài khoản đăng nhập cho toàn bộ Độc giả chưa có tài khoản. Tiếp tục?", "Đồng bộ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int countUsers = DocGiaBLL.Instance.DongBoHeThong();
                MessageBox.Show($"Hoàn tất!\nĐã tạo mới {countUsers} tài khoản.", "Thành công");
                LoadData();
            }
        }

        private void btnDebugMail_Click(object sender, EventArgs e)
        {
            btnDebugMail.Text = "Đang quét...";
            btnDebugMail.Enabled = false;

            try
            {
                // Gọi BLL để thực hiện quét toàn bộ những người đến hạn/quá hạn chưa gửi mail
                int count = MuonTraBLL.Instance.TuDongKiemTraVaGuiMailLuuLuu();

                if (count > 0)
                    MessageBox.Show($"Hệ thống đã gửi thành công {count} email nhắc nhở và xử lý kỷ luật!", "Thành công");
                else
                    MessageBox.Show("Không có độc giả nào mới cần gửi mail trong hôm nay.", "Thông báo");

                RefreshGrid(); // Cập nhật lại màu sắc và trạng thái trên bảng
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi: " + ex.Message);
            }
            finally
            {
                btnDebugMail.Text = "GỬI MAIL NHẮC NHỞ";
                btnDebugMail.Enabled = true;
            }
        }
        //In thẻ
        private void btnInThe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedReaderID))
            {
                MessageBox.Show("Vui lòng chọn một độc giả từ danh sách để in thẻ!", "Thông báo");
                return;
            }

            // Lấy thông tin từ các ô nhập liệu
            string name = txtHoTen.Text;
            string id = selectedReaderID;
            string dept = cboDonVi.Text;
            string type = cboLoaiDocGia.Text;
            string email = txtMail.Text;

            XuatTheThuVien(id, name, dept, type, email);
        }

        // 2. Hàm vẽ và xuất thẻ "Luxury"
        private void XuatTheThuVien(string id, string name, string dept, string type, string email)
        {
            try
            {
                // Kích thước thẻ chuẩn ISO (Pixel 300 DPI)
                int width = 1011;
                int height = 638;

                using (Bitmap bmp = new Bitmap(width, height))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        g.Clear(Color.White);

                        // --- 1. BO GÓC VÀ HEADER ---
                        GraphicsPath path = new GraphicsPath();
                        int radius = 40;
                        path.AddArc(0, 0, radius, radius, 180, 90);
                        path.AddArc(width - radius, 0, radius, radius, 270, 90);
                        path.AddArc(width - radius, height - radius, radius, radius, 0, 90);
                        path.AddArc(0, height - radius, radius, radius, 90, 90);
                        path.CloseAllFigures();
                        g.SetClip(path);

                        // Header màu xanh UNETI
                        LinearGradientBrush headerBrush = new LinearGradientBrush(new Point(0, 0), new Point(width, 140), Color.FromArgb(0, 51, 102), Color.FromArgb(0, 82, 165));
                        g.FillRectangle(headerBrush, 0, 0, width, 140);

                        // Font Times New Roman cho Header
                        g.DrawString("THƯ VIỆN ĐẠI HỌC UNETI", new Font("Times New Roman", 32, FontStyle.Bold), Brushes.White, 40, 25);
                        g.DrawString("UNIVERSITY OF ECONOMICS - TECHNOLOGY FOR INDUSTRIES", new Font("Times New Roman", 13), Brushes.LightGray, 40, 85);
                        g.DrawString("LIBRARY CARD", new Font("Times New Roman", 13, FontStyle.Italic), Brushes.Khaki, 830, 20);

                        // --- 2. VẼ MÃ VẠCH LÊN ĐẦU (Dưới Header) ---
                        int xInfoStart = 350; // Điểm bắt đầu của khối thông tin bên phải
                        int barcodeWidth = 620;
                        int barcodeHeight = 100;
                        int yBarcode = 160;

                        Rectangle rectBarcode = new Rectangle(xInfoStart, yBarcode, barcodeWidth, barcodeHeight);
                        g.FillRectangle(Brushes.White, rectBarcode);
                        g.DrawRectangle(new Pen(Color.Black, 1), rectBarcode);

                        // Vẽ vạch (Barcode Engine giả lập)
                        Random rnd = new Random(id.GetHashCode());
                        int currentX = rectBarcode.X + 15;
                        while (currentX < rectBarcode.Right - 15)
                        {
                            int w = rnd.Next(2, 6);
                            if (currentX + w > rectBarcode.Right - 15) break;
                            g.FillRectangle(Brushes.Black, currentX, rectBarcode.Y + 10, w, rectBarcode.Height - 35);
                            currentX += w + rnd.Next(1, 4);
                        }
                        // Mã số dưới mã vạch giữ OCR cho máy dễ đọc hoặc đổi Times tùy bạn (ở đây tớ để TNR 14)
                        g.DrawString(id, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, xInfoStart + (barcodeWidth / 2) - 40, yBarcode + barcodeHeight - 22);

                        // --- 3. VẼ ẢNH THẺ ĐẦY BÊN TRÁI ---
                        int imgWidth = 280;
                        int imgHeight = 370;
                        int yContentStart = 160;
                        g.DrawRectangle(new Pen(Color.FromArgb(200, 200, 200), 2), 40, yContentStart, imgWidth, imgHeight);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(252, 252, 252)), 41, yContentStart + 1, imgWidth - 2, imgHeight - 2);
                        g.DrawString("ẢNH THẺ 3x4", new Font("Times New Roman", 16, FontStyle.Italic), Brushes.Silver, 95, yContentStart + 160);

                        // --- 4. THÔNG TIN VĂN BẢN (TIMES NEW ROMAN - SIZE 16) ---
                        // Thiết lập font đồng nhất theo ý bạn
                        Font fontInfo = new Font("Times New Roman", 16, FontStyle.Bold);
                        Font fontLabel = new Font("Times New Roman", 13, FontStyle.Regular);
                        Brush labelBrush = Brushes.DimGray;
                        Brush infoBrush = Brushes.Black;

                        int yTextStart = 280;
                        int lineGap = 85;

                        // 1. Mã Sinh Viên
                        g.DrawString("MÃ SINH VIÊN:", fontLabel, labelBrush, xInfoStart, yTextStart);
                        g.DrawString(id, fontInfo, infoBrush, xInfoStart, yTextStart + 25);

                        // 2. Họ và tên
                        g.DrawString("HỌ VÀ TÊN:", fontLabel, labelBrush, xInfoStart, yTextStart + lineGap);
                        g.DrawString(name.ToUpper(), fontInfo, infoBrush, xInfoStart, yTextStart + lineGap + 25);

                        // 3. Khoa / Viện
                        g.DrawString("KHOA / VIỆN:", fontLabel, labelBrush, xInfoStart, yTextStart + (lineGap * 2));
                        // Dùng RectangleF để tự động xuống dòng nếu tên khoa dài
                        RectangleF rectDept = new RectangleF(xInfoStart, yTextStart + (lineGap * 2) + 25, width - xInfoStart - 40, 70);
                        g.DrawString(dept, fontInfo, infoBrush, rectDept);

                        // 4. Email
                        g.DrawString("EMAIL:", fontLabel, labelBrush, xInfoStart, yTextStart + (lineGap * 3));
                        string displayEmail = string.IsNullOrEmpty(email) ? "Chưa cập nhật" : email;
                        g.DrawString(displayEmail, fontInfo, infoBrush, xInfoStart, yTextStart + (lineGap * 3) + 25);

                        // Trang trí thêm đường kẻ mỏng ở đáy thẻ cho Luxury
                        g.DrawLine(new Pen(Color.FromArgb(0, 51, 102), 3), 40, height - 35, width - 40, height - 35);

                        // Dòng ghi chú nhỏ - Dịch xuống cách đáy 25px
                        g.DrawString("Thẻ có giá trị sử dụng trong suốt quá trình học tập và công tác tại trường.",
                                     new Font("Times New Roman", 10, FontStyle.Italic),
                                     Brushes.Gray, 40, height - 25);
                    }

                    // --- 5. LƯU VÀ MỞ FILE ---
                    string folderPath = Path.Combine(Application.StartupPath, "TheThuVien_Uneti");
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                    string filePath = Path.Combine(folderPath, $"Card_TNR_{id}.png");
                    bmp.Save(filePath, ImageFormat.Png);
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi in thẻ: " + ex.Message); }
        }
        //end
        // TÍNH NĂNG MỚI: KÍCH ĐÚP CHUỘT MỞ HỒ SƠ LƯU VẾT
        private void dgvDocGia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string readerID = dgvDocGia.Rows[e.RowIndex].Cells["Mã Độc giả"].Value.ToString();
                string fullName = dgvDocGia.Rows[e.RowIndex].Cells["Họ và tên"].Value.ToString();

                // Mở Form Hồ Sơ Độc Giả siêu xịn
                FrmHoSoDocGia frmProfile = new FrmHoSoDocGia(readerID, fullName);
                frmProfile.ShowDialog();

                // Refresh lại Grid sau khi tắt Form Hồ Sơ (lỡ như có thủ thư vừa trừ/thêm nợ)
                RefreshGrid();
            }
        }
    }
}