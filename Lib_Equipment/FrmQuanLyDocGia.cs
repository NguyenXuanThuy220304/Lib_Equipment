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
            dgvDocGia.DataBindingComplete += dgvDocGia_DataBindingComplete;
        }

        private void FrmQuanLyDocGia_Load(object sender, EventArgs e)
        {
            // KHÓA Ô NHẬP MÃ ĐỂ TỰ ĐỘNG SINH
            txtMaDocGia.Enabled = false;

            // 1. Tạm ngắt sự kiện để tránh lỗi khi gán DataSource
            cboDonVi.SelectedIndexChanged -= TuDongSinhMa_Event;
            cboLoaiDocGia.SelectedIndexChanged -= TuDongSinhMa_Event;

            // Load các ComboBox (Khoa, Loại thẻ...)
            cboDonVi.DataSource = DocGiaBLL.Instance.LayDanhSachKhoaVien();
            cboDonVi.DisplayMember = "DepartmentName";
            cboDonVi.ValueMember = "DepartmentID";

            // 2. TỰ ĐỘNG QUÉT HỆ THỐNG
            try
            {
                MuonTraBLL.Instance.TuDongKiemTraVaGuiMailLuuLuu();
            }
            catch { /* Bỏ qua lỗi kết nối SMTP để Form tiếp tục load */ }

            // 3. Hiển thị dữ liệu
            LoadData();

            // 4. Bật lại sự kiện và sinh mã lần đầu
            cboDonVi.SelectedIndexChanged += TuDongSinhMa_Event;
            cboLoaiDocGia.SelectedIndexChanged += TuDongSinhMa_Event;
            SinhMaDocGiaTuDong();
        }

        // =================================================================================
        // THUẬT TOÁN TỰ ĐỘNG SINH MÃ ĐỘC GIẢ: [LOẠI] - [NGÀNH] - [STT]
        // =================================================================================
        private void TuDongSinhMa_Event(object sender, EventArgs e)
        {
            // Nếu đang chọn sửa dữ liệu cũ thì không sinh mã mới
            if (string.IsNullOrEmpty(selectedReaderID))
            {
                SinhMaDocGiaTuDong();
            }
        }

        // =================================================================================
        // THUẬT TOÁN TỰ ĐỘNG SINH MÃ ĐỘC GIẢ: [LOẠI] - [NGÀNH] - [STT] (CHỐNG TRÙNG TUYỆT ĐỐI)
        // =================================================================================
        // =================================================================================
        // THUẬT TOÁN TỰ ĐỘNG SINH MÃ ĐỘC GIẢ: [LOẠI] - [NGÀNH] - [STT] (CHỐNG TRÙNG TUYỆT ĐỐI)
        // =================================================================================
        private void SinhMaDocGiaTuDong()
        {
            if (cboDonVi.SelectedValue == null || cboLoaiDocGia.SelectedItem == null) return;
            string deptId = cboDonVi.SelectedValue.ToString();
            if (deptId == "System.Data.DataRowView") return;

            string loaiDocGia = cboLoaiDocGia.Text.ToLower();

            // 1. Lấy Tiền tố Loại Độc giả (GV / SV)
            string prefixLoai = "SV"; // Mặc định
            if (loaiDocGia.Contains("giảng viên") || loaiDocGia.Contains("cán bộ")) prefixLoai = "GV";
            else if (loaiDocGia.Contains("sinh viên")) prefixLoai = "SV";

            // 2. Lấy Tiền tố Ngành (Cắt bỏ chữ K_ hoặc P_ hoặc TV_ trong DepartmentID)
            // Ví dụ: K_IT -> IT, K_BUS -> BUS, P_DT -> DT
            string prefixNganh = deptId;
            string[] parts = deptId.Split('_');
            if (parts.Length > 1) prefixNganh = parts[1];

            string basePrefix = $"{prefixLoai}_{prefixNganh}_"; // VD: SV-IT- hoặc GV-BUS-

            // 3. Truy vấn tìm số lớn nhất đang có trong CSDL
            string query = "SELECT ReaderID FROM Reader WHERE ReaderID LIKE @prefix + '%'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@prefix", basePrefix) });

            int maxNum = 0;
            foreach (DataRow row in dt.Rows)
            {
                string id = row["ReaderID"].ToString().Trim();
                if (id.Length > basePrefix.Length)
                {
                    string numPart = id.Substring(basePrefix.Length);
                    if (int.TryParse(numPart, out int num))
                    {
                        if (num > maxNum) maxNum = num;
                    }
                }
            }

            // 4. Sinh mã và QUÉT CHỐNG TRÙNG LẶP (Bảo đảm không bao giờ văng lỗi SQL)
            int newNum = maxNum + 1;
            string newID = basePrefix + newNum.ToString("D3"); // Mặc định 3 số: 001, 002

            while (true)
            {
                // Kiểm tra trực tiếp trong DB xem mã này đã tồn tại chưa
                string checkQuery = "SELECT COUNT(*) FROM Reader WHERE ReaderID = @id";
                int count = (int)DataProvider.Instance.ExecuteScalar(checkQuery, new SqlParameter[] { new SqlParameter("@id", newID) });

                // Nếu chưa có ai dùng -> An toàn 100%, thoát vòng lặp và lấy mã này
                if (count == 0)
                {
                    break;
                }

                // Nếu đen đủi bị trùng, tiếp tục tăng STT lên 1 và quét lại
                newNum++;
                newID = basePrefix + newNum.ToString("D3");
            }

            // Xuất ra ô textbox
            txtMaDocGia.Text = newID;
        }

        // =================================================================================
        // HÀM LOAD DỮ LIỆU ĐÃ ĐƯỢC CHUẨN HÓA
        // =================================================================================
        private void LoadData()
        {
            try
            {
                string sqlUpdateStatus = @"
                    UPDATE Reader SET Status = 0 WHERE ReaderID IN (
                        SELECT DISTINCT br.ReaderID FROM BorrowRecord br 
                        JOIN BorrowDetail bd ON br.RecordID = bd.RecordID 
                        WHERE bd.ReturnDate IS NULL AND CAST(br.DueDate AS DATE) < CAST(GETDATE() AS DATE)
                    );

                    UPDATE Reader SET IsPermanentlyBanned = 1, Status = 0 WHERE ReaderID IN (
                        SELECT DISTINCT br.ReaderID FROM BorrowRecord br 
                        JOIN BorrowDetail bd ON br.RecordID = bd.RecordID 
                        WHERE bd.ReturnDate IS NULL AND DATEDIFF(day, br.DueDate, GETDATE()) > 30
                    );

                    UPDATE Reader SET Status = 1 WHERE Status = 0 AND ISNULL(IsPermanentlyBanned, 0) = 0 
                    AND ReaderID NOT IN (
                        SELECT DISTINCT br.ReaderID FROM BorrowRecord br 
                        JOIN BorrowDetail bd ON br.RecordID = bd.RecordID 
                        WHERE bd.ReturnDate IS NULL AND CAST(br.DueDate AS DATE) < CAST(GETDATE() AS DATE)
                    );";

                DataProvider.Instance.ExecuteNonQuery(sqlUpdateStatus, null);

                string query = @"
                    SELECT r.ReaderID AS [Mã Độc giả], r.FullName AS [Họ và tên], 
                           d.DepartmentName AS [Khoa/Viện], r.ReaderType AS [Loại thẻ], r.Email, 
                           r.AcademicDebt AS [Công nợ (VNĐ)],
                           CASE 
                              WHEN r.IsPermanentlyBanned = 1 THEN N'CẤM VĨNH VIỄN' 
                              WHEN r.Status = 1 THEN N'Hoạt động' 
                              ELSE N'Đang bị khóa' 
                           END AS [Trạng thái]
                    FROM Reader r
                    LEFT JOIN Department d ON r.DepartmentID = d.DepartmentID 
                    WHERE r.IsDeleted = 0 OR r.IsDeleted IS NULL";

                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                dgvDocGia.DataSource = dt;
                dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvDocGia.Columns.Contains("Công nợ (VNĐ)"))
                {
                    dgvDocGia.Columns["Công nợ (VNĐ)"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message); }
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
                        row.DefaultCellStyle.Font = new Font(dgvDocGia.Font, FontStyle.Bold);
                    }
                    else if (trangThai == "Đang bị khóa")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = Color.OrangeRed;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDocGia.Rows[e.RowIndex];

                selectedReaderID = row.Cells["Mã Độc giả"].Value?.ToString();
                txtMaDocGia.Text = selectedReaderID;
                txtHoTen.Text = row.Cells["Họ và tên"].Value?.ToString();
                txtMail.Text = row.Cells["Email"].Value?.ToString();

                string tenKhoaTrenBang = row.Cells["Khoa/Viện"].Value?.ToString();
                int index = cboDonVi.FindStringExact(tenKhoaTrenBang);
                if (index != -1) cboDonVi.SelectedIndex = index;

                cboLoaiDocGia.Text = row.Cells["Loại thẻ"].Value?.ToString();

                string trangThai = row.Cells["Trạng thái"].Value?.ToString();
                cboTrangThai.Text = (trangThai == "CẤM VĨNH VIỄN") ? "Khóa" : trangThai;
            }
        }

        // =================================================================================
        // CÁC NÚT CHỨC NĂNG
        // =================================================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            string deptId = cboDonVi.SelectedValue?.ToString() ?? "";
            int status = cboTrangThai.Text.Contains("Hoạt động") ? 1 : 0;
            string email = txtMail.Text.Trim();
            string msg;

            bool isSuccess = DocGiaBLL.Instance.ThemDocGia(
                txtMaDocGia.Text.Trim(), txtHoTen.Text.Trim(), deptId,
                cboLoaiDocGia.Text, status, email, out msg);

            if (isSuccess)
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                btnLamMoi_Click(null, null); // Tự động reset và sinh mã mới
            }
            else
            {
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
            selectedReaderID = "";
            txtHoTen.Clear();
            txtMail.Clear();
            if (cboDonVi.Items.Count > 0) cboDonVi.SelectedIndex = 0;
            if (cboLoaiDocGia.Items.Count > 0) cboLoaiDocGia.SelectedIndex = 0;
            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;

            LoadData();

            // Sinh mã mới sau khi đã reset các form
            SinhMaDocGiaTuDong();
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

        //private void btnDebugMail_Click(object sender, EventArgs e)
        //{
        //    btnDebugMail.Text = "Đang quét...";
        //    btnDebugMail.Enabled = false;

        //    try
        //    {
        //        int count = MuonTraBLL.Instance.TuDongKiemTraVaGuiMailLuuLuu();
        //        if (count > 0)
        //            MessageBox.Show($"Hệ thống đã gửi thành công {count} email nhắc nhở và xử lý kỷ luật!", "Thành công");
        //        else
        //            MessageBox.Show("Không có độc giả nào mới cần gửi mail trong hôm nay.", "Thông báo");

        //        LoadData();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi thực thi: " + ex.Message);
        //    }
        //    finally
        //    {
        //        btnDebugMail.Text = "GỬI MAIL NHẮC NHỞ";
        //        btnDebugMail.Enabled = true;
        //    }
        //}
        private void btnDebugMail_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hệ thống sẽ quét TỔNG HỢP (Công nợ cũ + Sách đang quá hạn) để gửi 1 email chi tiết duy nhất cho từng người.\nBạn có chắc chắn muốn thực hiện?", "Xác nhận gửi mail", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            btnDebugMail.Text = "Đang gửi Mail...";
            btnDebugMail.Enabled = false;

            int emailCount = 0;

            try
            {
                // 1. Lệnh SQL quét và GOM NHÓM Độc giả có Nợ cũ HOẶC đang cầm Sách quá hạn
                string query = @"
            SELECT r.ReaderID, r.FullName, r.Email, ISNULL(r.AcademicDebt, 0) AS AcademicDebt,
                   b.Title, DATEDIFF(day, br.DueDate, GETDATE()) AS LateDays
            FROM Reader r
            LEFT JOIN BorrowRecord br ON r.ReaderID = br.ReaderID
            LEFT JOIN BorrowDetail bd ON br.RecordID = bd.RecordID 
                  AND bd.ReturnDate IS NULL 
                  AND CAST(br.DueDate AS DATE) < CAST(GETDATE() AS DATE)
            LEFT JOIN BookCopy bc ON bd.CopyID = bc.CopyID
            LEFT JOIN Book b ON bc.BookID = b.BookID
            WHERE (r.AcademicDebt > 0 OR bd.RecordID IS NOT NULL)
              AND r.Email IS NOT NULL 
              AND LTRIM(RTRIM(r.Email)) <> '' 
              AND (r.IsDeleted = 0 OR r.IsDeleted IS NULL)
            ORDER BY r.ReaderID";

                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                // Các biến dùng để gom nhóm nhiều sách vào chung 1 email của 1 độc giả
                string currentReaderID = "";
                string currentName = "";
                string currentEmail = "";
                decimal currentFixedDebt = 0;
                decimal totalEstimatedFine = 0;
                string reasonHtml = "";

                // 2. Duyệt qua từng dòng dữ liệu
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string id = row["ReaderID"].ToString();

                    // KHI CHUYỂN SANG ĐỘC GIẢ MỚI (Hoặc là dòng đầu tiên)
                    if (id != currentReaderID)
                    {
                        // Bước A: Gửi email cho người cũ trước khi chuyển qua xử lý người mới
                        if (currentReaderID != "")
                        {
                            decimal totalDebt = currentFixedDebt + totalEstimatedFine;
                            bool sent = EmailHelper.SendNoticeEmail(currentEmail, currentName, currentReaderID, totalDebt, reasonHtml);
                            if (sent) emailCount++;
                        }

                        // Bước B: Khởi tạo thông tin cho người mới
                        currentReaderID = id;
                        currentName = row["FullName"].ToString();
                        currentEmail = row["Email"].ToString();
                        currentFixedDebt = Convert.ToDecimal(row["AcademicDebt"]);
                        totalEstimatedFine = 0;

                        reasonHtml = "";

                        // Nếu người này có nợ cũ (tiền phạt từ những lần mượn trước chưa trả)
                        if (currentFixedDebt > 0)
                        {
                            reasonHtml += $"• Nợ cũ chưa thanh toán (từ các giao dịch trước): <b style='color:#d93025;'>{currentFixedDebt:N0} VNĐ</b><br/>";
                        }
                    }

                    // KHI NGƯỜI NÀY CÓ SÁCH QUÁ HẠN (Cộng dồn vào reasonHtml)
                    if (row["Title"] != DBNull.Value && row["LateDays"] != DBNull.Value)
                    {
                        int lateDays = Convert.ToInt32(row["LateDays"]);
                        if (lateDays > 0)
                        {
                            string title = row["Title"].ToString();
                            decimal estimatedFine = lateDays * 2000; // Phạt 2.000đ/ngày
                            totalEstimatedFine += estimatedFine;

                            reasonHtml += $"• Sách đang mượn quá hạn: <i>'{title}'</i><br/>" +
                                          $"&nbsp;&nbsp;→ Trễ {lateDays} ngày - Phạt tạm tính: <b style='color:#d93025;'>{estimatedFine:N0} VNĐ</b><br/>";
                        }
                    }

                    // KHI LÀ DÒNG CUỐI CÙNG CỦA DANH SÁCH -> Chốt sổ gửi luôn
                    if (i == dt.Rows.Count - 1)
                    {
                        decimal totalDebt = currentFixedDebt + totalEstimatedFine;
                        bool sent = EmailHelper.SendNoticeEmail(currentEmail, currentName, currentReaderID, totalDebt, reasonHtml);
                        if (sent) emailCount++;
                    }
                }

                MessageBox.Show($"Hoàn tất quét hệ thống!\nĐã gửi tổng cộng {emailCount} Email tổng hợp chi tiết cho các Độc giả.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình quét và gửi Mail: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnDebugMail.Text = "GỬI MAIL NHẮC NHỞ";
                btnDebugMail.Enabled = true;
            }
        }

        // =================================================================================
        // IN THẺ THƯ VIỆN 
        // =================================================================================
        private void btnInThe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedReaderID))
            {
                MessageBox.Show("Vui lòng chọn một độc giả từ danh sách để in thẻ!", "Thông báo");
                return;
            }

            string name = txtHoTen.Text;
            string id = selectedReaderID;
            string dept = cboDonVi.Text;
            string type = cboLoaiDocGia.Text;
            string email = txtMail.Text;

            XuatTheThuVien(id, name, dept, type, email);
        }

        private void XuatTheThuVien(string id, string name, string dept, string type, string email)
        {
            try
            {
                int width = 1011;
                int height = 638;

                using (Bitmap bmp = new Bitmap(width, height))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        g.Clear(Color.White);

                        GraphicsPath path = new GraphicsPath();
                        int radius = 40;
                        path.AddArc(0, 0, radius, radius, 180, 90);
                        path.AddArc(width - radius, 0, radius, radius, 270, 90);
                        path.AddArc(width - radius, height - radius, radius, radius, 0, 90);
                        path.AddArc(0, height - radius, radius, radius, 90, 90);
                        path.CloseAllFigures();
                        g.SetClip(path);

                        LinearGradientBrush headerBrush = new LinearGradientBrush(new Point(0, 0), new Point(width, 140), Color.FromArgb(0, 51, 102), Color.FromArgb(0, 82, 165));
                        g.FillRectangle(headerBrush, 0, 0, width, 140);

                        g.DrawString("THƯ VIỆN ĐẠI HỌC UNETI", new Font("Times New Roman", 32, FontStyle.Bold), Brushes.White, 40, 25);
                        g.DrawString("UNIVERSITY OF ECONOMICS - TECHNOLOGY FOR INDUSTRIES", new Font("Times New Roman", 13), Brushes.LightGray, 40, 85);
                        g.DrawString("LIBRARY CARD", new Font("Times New Roman", 13, FontStyle.Italic), Brushes.Khaki, 830, 20);

                        int xInfoStart = 350;
                        int barcodeWidth = 620;
                        int barcodeHeight = 100;
                        int yBarcode = 160;

                        Rectangle rectBarcode = new Rectangle(xInfoStart, yBarcode, barcodeWidth, barcodeHeight);
                        g.FillRectangle(Brushes.White, rectBarcode);
                        g.DrawRectangle(new Pen(Color.Black, 1), rectBarcode);

                        Random rnd = new Random(id.GetHashCode());
                        int currentX = rectBarcode.X + 15;
                        while (currentX < rectBarcode.Right - 15)
                        {
                            int w = rnd.Next(2, 6);
                            if (currentX + w > rectBarcode.Right - 15) break;
                            g.FillRectangle(Brushes.Black, currentX, rectBarcode.Y + 10, w, rectBarcode.Height - 35);
                            currentX += w + rnd.Next(1, 4);
                        }
                        g.DrawString(id, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, xInfoStart + (barcodeWidth / 2) - 40, yBarcode + barcodeHeight - 22);

                        int imgWidth = 280;
                        int imgHeight = 370;
                        int yContentStart = 160;
                        g.DrawRectangle(new Pen(Color.FromArgb(200, 200, 200), 2), 40, yContentStart, imgWidth, imgHeight);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(252, 252, 252)), 41, yContentStart + 1, imgWidth - 2, imgHeight - 2);
                        g.DrawString("ẢNH THẺ 3x4", new Font("Times New Roman", 16, FontStyle.Italic), Brushes.Silver, 95, yContentStart + 160);

                        Font fontInfo = new Font("Times New Roman", 16, FontStyle.Bold);
                        Font fontLabel = new Font("Times New Roman", 13, FontStyle.Regular);
                        Brush labelBrush = Brushes.DimGray;
                        Brush infoBrush = Brushes.Black;

                        int yTextStart = 280;
                        int lineGap = 85;

                        g.DrawString("MÃ SINH VIÊN:", fontLabel, labelBrush, xInfoStart, yTextStart);
                        g.DrawString(id, fontInfo, infoBrush, xInfoStart, yTextStart + 25);

                        g.DrawString("HỌ VÀ TÊN:", fontLabel, labelBrush, xInfoStart, yTextStart + lineGap);
                        g.DrawString(name.ToUpper(), fontInfo, infoBrush, xInfoStart, yTextStart + lineGap + 25);

                        g.DrawString("KHOA / VIỆN:", fontLabel, labelBrush, xInfoStart, yTextStart + (lineGap * 2));
                        RectangleF rectDept = new RectangleF(xInfoStart, yTextStart + (lineGap * 2) + 25, width - xInfoStart - 40, 70);
                        g.DrawString(dept, fontInfo, infoBrush, rectDept);

                        g.DrawString("EMAIL:", fontLabel, labelBrush, xInfoStart, yTextStart + (lineGap * 3));
                        string displayEmail = string.IsNullOrEmpty(email) ? "Chưa cập nhật" : email;
                        g.DrawString(displayEmail, fontInfo, infoBrush, xInfoStart, yTextStart + (lineGap * 3) + 25);

                        g.DrawLine(new Pen(Color.FromArgb(0, 51, 102), 3), 40, height - 35, width - 40, height - 35);
                        g.DrawString("Thẻ có giá trị sử dụng trong suốt quá trình học tập và công tác tại trường.",
                                     new Font("Times New Roman", 10, FontStyle.Italic),
                                     Brushes.Gray, 40, height - 25);
                    }

                    string folderPath = Path.Combine(Application.StartupPath, "TheThuVien_Uneti");
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                    string filePath = Path.Combine(folderPath, $"Card_TNR_{id}.png");
                    bmp.Save(filePath, ImageFormat.Png);
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi in thẻ: " + ex.Message); }
        }

        // =================================================================================
        // DOUBLE CLICK MỞ FORM HỒ SƠ LƯU VẾT
        // =================================================================================
        private void dgvDocGia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string readerID = dgvDocGia.Rows[e.RowIndex].Cells["Mã Độc giả"].Value.ToString();
                string fullName = dgvDocGia.Rows[e.RowIndex].Cells["Họ và tên"].Value.ToString();

                FrmHoSoDocGia frmProfile = new FrmHoSoDocGia(readerID, fullName);
                frmProfile.ShowDialog();

                LoadData();
            }
        }
    }
}