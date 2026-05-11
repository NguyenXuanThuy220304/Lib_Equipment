using Lib_Equipment.BLL;
using Lib_Equipment.DAO;
using Lib_Equipment.DTO;
using Lib_Equipment.Helpers;
using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmMuonTraSach : Form
    {
        private DocGiaDTO currentReader = null;

        public FrmMuonTraSach()
        {
            InitializeComponent();
            dgvDangMuon.DataError += (s, e) => { e.ThrowException = false; };
        }

        private void FrmMuonTraSach_Load(object sender, EventArgs e)
        {
            SetupGridReturnButton();
            RefreshGrid();
        }

        private void SetupGridReturnButton()
        {
            if (dgvDangMuon.Columns.Contains("btnReturn")) return;
            DataGridViewButtonColumn btnReturn = new DataGridViewButtonColumn();
            btnReturn.Name = "btnReturn";
            btnReturn.HeaderText = "Thao tác";
            btnReturn.UseColumnTextForButtonValue = false;
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvDangMuon.Columns.Add(btnReturn);
        }

        private void RefreshGrid()
        {
            string query = "";
            DataTable dt = new DataTable();

            // Thuật toán: Chỉ lấy những phiếu CHƯA TRẢ (ReturnDate IS NULL) 
            // HOẶC những phiếu của sách ĐANG CHỜ DUYỆT nhưng phải là giao dịch MỚI NHẤT (MAX RecordID) của cuốn sách đó.

            string sqlFilter = @"
        SELECT 
            br.RecordID AS [Mã Phiếu], 
            r.FullName AS [Người Mượn], 
            r.Email, 
            bd.CopyID AS [Mã Bản Sao], 
            b.Title AS [Tên Sách], 
            br.BorrowDate AS [Ngày Mượn], 
            br.DueDate AS [Hạn Trả], 
            bc.Status AS [Tình Trạng Vật Lý], 
            ISNULL(bd.FineAmount, 0) AS [Phạt Trễ Chốt], 
            br.ReaderID
        FROM BorrowRecord br 
        JOIN Reader r ON br.ReaderID = r.ReaderID 
        JOIN BorrowDetail bd ON br.RecordID = bd.RecordID 
        JOIN BookCopy bc ON bd.CopyID = bc.CopyID 
        JOIN Book b ON bc.BookID = b.BookID 
        WHERE 
            -- Trường hợp 1: Sách đang mượn bình thường (chưa trả)
            (bd.ReturnDate IS NULL AND bc.Status = N'Đang mượn')
            
            -- Trường hợp 2: Sách sinh viên vừa bấm trả tự động (Chờ kiểm duyệt)
            -- Điều kiện then chốt: RecordID phải là cái lớn nhất của cuốn sách đó (Tránh đội mồ phiếu cũ)
            OR (bc.Status = N'Chờ kiểm duyệt' 
                AND bd.RecordID = (SELECT MAX(RecordID) FROM BorrowDetail WHERE CopyID = bd.CopyID)
                AND bd.ReturnDate IS NOT NULL) -- Vì khi tự trả ta đã chốt ReturnDate rồi
    ";

            if (string.IsNullOrEmpty(txtMaDG.Text.Trim()))
            {
                query = sqlFilter + " ORDER BY CASE WHEN bc.Status = N'Chờ kiểm duyệt' THEN 0 ELSE 1 END, br.BorrowDate DESC";
                dt = DataProvider.Instance.ExecuteQuery(query);
            }
            else
            {
                query = sqlFilter + " AND br.ReaderID = @id ORDER BY br.BorrowDate DESC";
                dt = DataProvider.Instance.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@id", txtMaDG.Text.Trim()) });
            }

            dgvDangMuon.DataSource = dt;

            // Đẩy cột nút bấm về cuối
            if (dgvDangMuon.Columns.Contains("btnReturn"))
                dgvDangMuon.Columns["btnReturn"].DisplayIndex = dgvDangMuon.Columns.Count - 1;

            // Ẩn bớt các cột ID để nhìn cho chuyên nghiệp
            if (dgvDangMuon.Columns.Contains("ReaderID")) dgvDangMuon.Columns["ReaderID"].Visible = false;
            if (dgvDangMuon.Columns.Contains("Email")) dgvDangMuon.Columns["Email"].Visible = false;

            txtTimKiemSachTra.Clear();
        }

        private void txtMaDG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtMaDG.Text))
                {
                    txtTenDG.Clear(); currentReader = null; RefreshGrid(); return;
                }

                string query = "SELECT ReaderID, FullName, Status, ISNULL(IsPermanentlyBanned, 0) AS IsPermanentlyBanned, ISNULL(AcademicDebt, 0) AS AcademicDebt, ReaderType FROM Reader WHERE ReaderID = @id AND (IsDeleted = 0 OR IsDeleted IS NULL)";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@id", txtMaDG.Text.Trim()) });

                if (dt.Rows.Count > 0)
                {
                    currentReader = new DocGiaDTO()
                    {
                        ReaderID = dt.Rows[0]["ReaderID"].ToString(),
                        FullName = dt.Rows[0]["FullName"].ToString(),
                        ReaderType = dt.Rows[0]["ReaderType"].ToString(),
                        Status = Convert.ToInt32(dt.Rows[0]["Status"]),
                    };

                    txtTenDG.Text = currentReader.FullName;

                    if (Convert.ToBoolean(dt.Rows[0]["IsPermanentlyBanned"]))
                    {
                        MessageBox.Show("TÀI KHOẢN NÀY ĐÃ BỊ TƯỚC QUYỀN SỬ DỤNG THƯ VIỆN VĨNH VIỄN!", "Lệnh Cấm Phục Vụ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        btnChoMuon.Enabled = false; txtMaBanSao.Enabled = false; return;
                    }

                    string checkOverdue30Days = @"SELECT COUNT(*) FROM BorrowRecord br JOIN BorrowDetail bd ON br.RecordID = bd.RecordID WHERE br.ReaderID = @id AND bd.ReturnDate IS NULL AND DATEDIFF(day, br.DueDate, GETDATE()) > 30";
                    int overdue30Count = (int)DataProvider.Instance.ExecuteScalar(checkOverdue30Days, new SqlParameter[] { new SqlParameter("@id", txtMaDG.Text.Trim()) });

                    if (overdue30Count > 0)
                    {
                        DataProvider.Instance.ExecuteNonQuery("UPDATE Reader SET IsPermanentlyBanned = 1, Status = 0 WHERE ReaderID = @id", new SqlParameter[] { new SqlParameter("@id", txtMaDG.Text.Trim()) });
                        MessageBox.Show($"BÁO ĐỘNG: Độc giả có sách mượn quá hạn trên 30 ngày!\nHệ thống tự động TƯỚC QUYỀN SỬ DỤNG VĨNH VIỄN.", "Xử lý Kỷ luật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnChoMuon.Enabled = false; txtMaBanSao.Enabled = false; currentReader = null; return;
                    }

                    decimal debt = Convert.ToDecimal(dt.Rows[0]["AcademicDebt"]);
                    if (debt > 0) MessageBox.Show($"LƯU Ý: Độc giả này đang có Công nợ chưa thanh toán: {debt:N0} VNĐ.", "Thông báo Công nợ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtMaBanSao.Enabled = true;
                    txtMaBanSao.Focus();
                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Độc giả này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDG.Clear(); currentReader = null; RefreshGrid();
                }
            }
        }

        private void txtMaBanSao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtMaBanSao.Text)) return;

                // Bổ sung: Tìm thêm người đang mượn cuốn sách này (nếu có)
                string sql = @"
            SELECT b.Title, bc.Status, 
                   (SELECT TOP 1 br.ReaderID FROM BorrowDetail bd JOIN BorrowRecord br ON bd.RecordID = br.RecordID WHERE bd.CopyID = bc.CopyID AND bd.ReturnDate IS NULL ORDER BY br.BorrowDate DESC) as NguoiDangMuon
            FROM BookCopy bc JOIN Book b ON bc.BookID = b.BookID 
            WHERE bc.CopyID = @id AND bc.IsDeleted = 0";

                DataTable dtSach = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@id", txtMaBanSao.Text.Trim()) });

                if (dtSach.Rows.Count > 0)
                {
                    txtTenSachMuon.Text = dtSach.Rows[0]["Title"].ToString();
                    string trangThaiSach = dtSach.Rows[0]["Status"].ToString();
                    string nguoiDangMuon = dtSach.Rows[0]["NguoiDangMuon"].ToString();

                    if (trangThaiSach == "Có sẵn")
                    {
                        btnChoMuon.Enabled = true;
                        btnGiaHan.Enabled = false; // Tắt nút gia hạn
                        txtTenSachMuon.ForeColor = Color.FromArgb(40, 167, 69);
                        btnChoMuon.Focus();
                    }
                    else if (trangThaiSach == "Đang mượn" && currentReader != null && nguoiDangMuon == currentReader.ReaderID)
                    {
                        // Sách đang mượn và đúng người này đang mượn -> Cho phép gia hạn
                        btnChoMuon.Enabled = false;
                        btnGiaHan.Enabled = true; // Bật nút gia hạn
                        txtTenSachMuon.ForeColor = Color.DarkOrange;
                        MessageBox.Show("Độc giả này đang mượn cuốn sách này.\nNhấn nút GIA HẠN để cộng thêm ngày.", "Trạng thái", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnGiaHan.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Sách này hiện đang được mượn bởi người khác hoặc không có sẵn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnChoMuon.Enabled = false;
                        btnGiaHan.Enabled = false;
                        txtTenSachMuon.ForeColor = Color.DimGray;
                    }
                }
                else
                {
                    MessageBox.Show("Mã bản sao sách không tồn tại!", "Lỗi");
                    btnChoMuon.Enabled = false;
                    btnGiaHan.Enabled = false;
                }
            }
        }

        private void btnChoMuon_Click(object sender, EventArgs e)
        {
            if (currentReader == null)
            {
                MessageBox.Show("Vui lòng quét thẻ độc giả trước!", "Thông báo");
                return;
            }

            // 1. Đếm số sách đang mượn thực tế
            int currentBorrowed = MuonTraDAO.Instance.CountBorrowedBooks(currentReader.ReaderID);

            // 2. Xác định hạn mức
            int maxLimit = currentReader.ReaderType.Contains("Giảng viên") ? 9 : 6;

            // 3. Kiểm tra hạn mức
            if (currentBorrowed >= maxLimit)
            {
                MessageBox.Show($"Độc giả đã đạt giới hạn mượn sách!\n" +
                                $"Hạn mức tối đa: {maxLimit} cuốn.\n" +
                                $"Hiện đang giữ: {currentBorrowed} cuốn.\n\n" +
                                $"Vui lòng yêu cầu độc giả trả sách cũ trước khi mượn mới.",
                                "Cảnh báo hạn mức", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // =====================================================================
            // LUẬT TÍNH HẠN TRẢ TỰ ĐỘNG
            // =====================================================================
            // Tùy chỉnh số ngày cho mượn tại đây (VD: Giảng viên 45 ngày, Sinh viên 30 ngày)
            int soNgayMuon = currentReader.ReaderType.Contains("Giảng viên") ? 45 : 30;
            DateTime ngayHanTraTuDong = DateTime.Now.AddDays(soNgayMuon);
            // =====================================================================

            string username = AppSession.Username ?? "admin";

            try
            {
                // Truyền ngayHanTraTuDong vào DAO thay vì lấy từ dtpHanTra.Value
                int newId = MuonTraDAO.Instance.ExecuteBorrow(
                    currentReader.ReaderID,
                    txtMaBanSao.Text.Trim(),
                    ngayHanTraTuDong,
                    username
                );

                if (newId > 0)
                {
                    MessageBox.Show($"Mượn sách thành công! Mã phiếu: {newId}\n" +
                                    $"(Số sách đang mượn: {currentBorrowed + 1}/{maxLimit})\n\n" +
                                    $"HỆ THỐNG TỰ ĐỘNG GIAO HẠN TRẢ: {ngayHanTraTuDong.ToString("dd/MM/yyyy")}",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnChoMuon.Enabled = false;
                    txtMaBanSao.Clear();
                    txtTenSachMuon.Clear();
                    RefreshGrid();
                    txtMaBanSao.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiemSachTra_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiemSachTra.Text.Trim();
            DataTable dt = (DataTable)dgvDangMuon.DataSource;

            if (dt != null)
            {
                try
                {
                    if (string.IsNullOrEmpty(keyword)) dt.DefaultView.RowFilter = "";
                    else dt.DefaultView.RowFilter = string.Format("CONVERT([Mã Phiếu], 'System.String') LIKE '%{0}%' OR [Mã Bản Sao] LIKE '%{0}%' OR [Tên Sách] LIKE '%{0}%'", keyword);
                }
                catch { }
            }
        }

        //private void dgvDangMuon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == dgvDangMuon.Columns["btnReturn"].Index && e.RowIndex >= 0)
        //    {
        //        var row = dgvDangMuon.Rows[e.RowIndex];
        //        int recordId = int.Parse(row.Cells["Mã Phiếu"].Value.ToString());
        //        string copyId = row.Cells["Mã Bản Sao"].Value.ToString();
        //        string tenSach = row.Cells["Tên Sách"].Value.ToString();
        //        DateTime dueDate = Convert.ToDateTime(row.Cells["Hạn Trả"].Value);
        //        string physicalStatus = row.Cells["Tình Trạng Vật Lý"].Value.ToString();

        //        string readerName = "";
        //        if (dgvDangMuon.Columns.Contains("Người Mượn")) readerName = row.Cells["Người Mượn"].Value.ToString();
        //        string readerEmail = row.Cells["Email"].Value?.ToString();
        //        string readerID = row.Cells["ReaderID"].Value.ToString();

        //        decimal lateFine = Convert.ToDecimal(row.Cells["Phạt Trễ Chốt"].Value);
        //        int lateDays = (int)(lateFine / 2000);

        //        // Nếu trả trực tiếp tại quầy (Xác nhận) -> Tính tiền trễ bằng thời gian thực
        //        if (physicalStatus != "Chờ kiểm duyệt")
        //        {
        //            lateFine = MuonTraBLL.Instance.CalculateLateFine(dueDate);
        //            lateDays = (DateTime.Now.Date - dueDate.Date).Days;
        //            lateDays = lateDays > 0 ? lateDays : 0;
        //        }

        //        string formTitle = physicalStatus == "Chờ kiểm duyệt" ? "KIỂM DUYỆT SÁCH TỪ TỦ TỰ ĐỘNG" : "THU TIỀN MẶT & XÁC NHẬN TRẢ SÁCH";

        //        Form frmTra = new Form() { Text = formTitle, Size = new Size(460, 400), StartPosition = FormStartPosition.CenterParent, BackColor = Color.White };

        //        Label lblHeader = new Label() { Text = $"Sách: {tenSach}", Left = 20, Top = 20, Width = 400, Height = 40, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

        //        ComboBox cboTinhTrang = new ComboBox() { Left = 20, Top = 70, Width = 400, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11) };
        //        cboTinhTrang.Items.AddRange(new string[] { "Bình thường", "Sách bị hỏng/rách (Phạt 50k)", "Làm mất sách (Mua đền sách mới + 20k)" });
        //        cboTinhTrang.SelectedIndex = 0;

        //        Label lblPhat = new Label() { Text = $"PHẠT TRỄ HẠN ({lateDays} ngày): {lateFine:N0} VNĐ", Left = 20, Top = 120, Width = 400, ForeColor = Color.Red, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
        //        TextBox txtTotal = new TextBox() { Left = 20, Top = 150, Width = 400, ReadOnly = true, Font = new Font("Segoe UI", 14, FontStyle.Bold), Text = lateFine.ToString("N0") };

        //        // Đổi tên nút bấm để Thủ thư đỡ nhầm lẫn
        //        string btnText = physicalStatus == "Chờ kiểm duyệt" ? "DUYỆT & GHI NỢ VÀO TÀI KHOẢN" : "ĐÃ THU TIỀN MẶT & XÁC NHẬN TRẢ";
        //        Color btnColor = physicalStatus == "Chờ kiểm duyệt" ? Color.FromArgb(230, 81, 0) : Color.FromArgb(40, 167, 69);
        //        Button btnXacNhan = new Button() { Text = btnText, Left = 20, Top = 220, Width = 400, Height = 60, BackColor = btnColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold) };

        //        cboTinhTrang.SelectedIndexChanged += (s, ev) =>
        //        {
        //            decimal penalty = 0;
        //            if (cboTinhTrang.SelectedIndex == 1) penalty = 50000;
        //            if (cboTinhTrang.SelectedIndex == 2) penalty = 200000;
        //            txtTotal.Text = (lateFine + penalty).ToString("N0");
        //        };

        //        frmTra.Controls.AddRange(new Control[] { lblHeader, cboTinhTrang, lblPhat, txtTotal, btnXacNhan });

        //        btnXacNhan.Click += (s, ev) =>
        //        {
        //            string newStatus = "Có sẵn"; string cond = "Tốt";
        //            if (cboTinhTrang.SelectedIndex == 1) { newStatus = "Hỏng"; cond = "Hỏng"; }
        //            if (cboTinhTrang.SelectedIndex == 2) { newStatus = "Mất"; cond = "Mất"; }

        //            decimal finalFine = 0;
        //            decimal.TryParse(txtTotal.Text.Replace(",", "").Replace(".", ""), out finalFine);

        //            // ExecuteReturn vẫn chạy để ghi nhận lịch sử vào BorrowDetail
        //            bool isSuccess = MuonTraDAO.Instance.ExecuteReturn(recordId, copyId, cond, finalFine, newStatus);

        //            if (isSuccess)
        //            {
        //                // ====================================================================
        //                // PHÂN NHÁNH LOGIC: CHỈ GHI NỢ NẾU LÀ "DUYỆT TỪ TỦ TỰ ĐỘNG"
        //                // ====================================================================
        //                if (finalFine > 0)
        //                {
        //                    if (physicalStatus == "Chờ kiểm duyệt")
        //                    {
        //                        // 1. Cộng dồn vào công nợ (AcademicDebt)
        //                        string sqlUpdateDebt = "UPDATE Reader SET AcademicDebt = ISNULL(AcademicDebt, 0) + @fine WHERE ReaderID = @readerID";
        //                        DataProvider.Instance.ExecuteNonQuery(sqlUpdateDebt, new SqlParameter[] {
        //                    new SqlParameter("@fine", finalFine),
        //                    new SqlParameter("@readerID", readerID)
        //                });

        //                        // 2. Gửi Mail thông báo nợ
        //                        if (!string.IsNullOrEmpty(readerEmail))
        //                        {
        //                            string reason = (lateDays > 0 ? $"Trễ hạn {lateDays} ngày. " : "") + (cboTinhTrang.SelectedIndex > 0 ? $"Tình trạng: {cboTinhTrang.Text}" : "");
        //                            System.Threading.Tasks.Task.Run(() => EmailHelper.SendNoticeEmail(readerEmail, readerName, readerID, finalFine, reason));
        //                        }

        //                        MessageBox.Show("Đã duyệt xong!\nTiền phạt đã được ghi vào Công nợ của độc giả.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    }
        //                    else
        //                    {
        //                        // TRẢ TẠI QUẦY (XÁC NHẬN): Đã thu tiền mặt -> Không cộng nợ, không gửi mail đòi nợ
        //                        MessageBox.Show("Hoàn tất quy trình trả sách!\nThủ thư đã thu tiền phạt trực tiếp.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    }
        //                }
        //                else
        //                {
        //                    // Trả đúng hạn, sách nguyên vẹn
        //                    MessageBox.Show("Đã nhận lại sách an toàn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }

        //                frmTra.DialogResult = DialogResult.OK;
        //                frmTra.Close();
        //            }
        //        };

        //        if (frmTra.ShowDialog() == DialogResult.OK) RefreshGrid();
        //    }
        //}
        private void dgvDangMuon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvDangMuon.Columns["btnReturn"].Index && e.RowIndex >= 0)
            {
                var row = dgvDangMuon.Rows[e.RowIndex];
                int recordId = int.Parse(row.Cells["Mã Phiếu"].Value.ToString());
                string copyId = row.Cells["Mã Bản Sao"].Value.ToString();
                string tenSach = row.Cells["Tên Sách"].Value.ToString();
                DateTime dueDate = Convert.ToDateTime(row.Cells["Hạn Trả"].Value);
                string physicalStatus = row.Cells["Tình Trạng Vật Lý"].Value.ToString();

                string readerName = "";
                if (dgvDangMuon.Columns.Contains("Người Mượn")) readerName = row.Cells["Người Mượn"].Value.ToString();
                string readerEmail = row.Cells["Email"].Value?.ToString();
                string readerID = row.Cells["ReaderID"].Value.ToString();

                // 1. Lấy thông tin thuộc tính của cuốn sách từ CSDL
                string queryBookInfo = "SELECT ISNULL(b.BookType, N'Bình thường') AS BookType, ISNULL(b.Price, 0) AS Price, ISNULL(b.PageCount, 0) AS PageCount FROM BookCopy bc JOIN Book b ON bc.BookID = b.BookID WHERE bc.CopyID = @copyId";
                DataTable dtBookInfo = DataProvider.Instance.ExecuteQuery(queryBookInfo, new SqlParameter[] { new SqlParameter("@copyId", copyId) });

                string loaiSach = "Bình thường";
                decimal giaBia = 0;
                int soTrang = 0;

                if (dtBookInfo.Rows.Count > 0)
                {
                    loaiSach = dtBookInfo.Rows[0]["BookType"].ToString();
                    giaBia = Convert.ToDecimal(dtBookInfo.Rows[0]["Price"]);
                    soTrang = Convert.ToInt32(dtBookInfo.Rows[0]["PageCount"]);
                }

                // Tính tiền trễ hạn
                decimal lateFine = Convert.ToDecimal(row.Cells["Phạt Trễ Chốt"].Value);
                int lateDays = (int)(lateFine / 2000);

                if (physicalStatus != "Chờ kiểm duyệt")
                {
                    lateFine = MuonTraBLL.Instance.CalculateLateFine(dueDate);
                    lateDays = (DateTime.Now.Date - dueDate.Date).Days;
                    lateDays = lateDays > 0 ? lateDays : 0;
                }

                string formTitle = physicalStatus == "Chờ kiểm duyệt" ? "KIỂM DUYỆT SÁCH TỪ TỦ TỰ ĐỘNG" : "THU TIỀN MẶT & XÁC NHẬN TRẢ SÁCH";
                Form frmTra = new Form() { Text = formTitle, Size = new Size(500, 420), StartPosition = FormStartPosition.CenterParent, BackColor = Color.White };

                Label lblHeader = new Label() { Text = $"Sách: {tenSach}\nLoại: {loaiSach}", Left = 20, Top = 10, Width = 450, Height = 50, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

                ComboBox cboTinhTrang = new ComboBox() { Left = 20, Top = 70, Width = 450, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11) };
                // Sửa lại nhãn cho chuẩn nghiệp vụ
                cboTinhTrang.Items.AddRange(new string[] { "Bình thường (Sách nguyên vẹn)", "Làm mất, hỏng, bẩn tài liệu (Tính phí phạt tự động)" });
                cboTinhTrang.SelectedIndex = 0;

                Label lblPhat = new Label() { Text = $"PHẠT TRỄ HẠN ({lateDays} ngày): {lateFine:N0} VNĐ", Left = 20, Top = 120, Width = 450, ForeColor = Color.Red, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

                Label lblChiTietBoiThuong = new Label() { Left = 20, Top = 150, Width = 450, Height = 40, ForeColor = Color.Blue, Font = new Font("Segoe UI", 9, FontStyle.Italic) };
                TextBox txtTotal = new TextBox() { Left = 20, Top = 190, Width = 450, ReadOnly = true, Font = new Font("Segoe UI", 14, FontStyle.Bold), Text = lateFine.ToString("N0") };

                string btnText = physicalStatus == "Chờ kiểm duyệt" ? "DUYỆT & GHI NỢ VÀO TÀI KHOẢN" : "ĐÃ THU TIỀN MẶT & XÁC NHẬN TRẢ";
                Color btnColor = physicalStatus == "Chờ kiểm duyệt" ? Color.FromArgb(230, 81, 0) : Color.FromArgb(40, 167, 69);
                Button btnXacNhan = new Button() { Text = btnText, Left = 20, Top = 250, Width = 450, Height = 60, BackColor = btnColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold) };

                // 2. Logic tính phí phạt theo cấu trúc của quy định thư viện
                cboTinhTrang.SelectedIndexChanged += (s, ev) => {
                    decimal phiBoiThuong = 0;
                    decimal phiXuLy = 20000;
                    lblChiTietBoiThuong.Text = "";

                    if (cboTinhTrang.SelectedIndex == 1) // Hỏng/Mất
                    {
                        if (loaiSach == "Sách hiếm")
                        {
                            phiBoiThuong = (giaBia * 3) + phiXuLy;
                            lblChiTietBoiThuong.Text = $"(Gấp 3 lần giá bìa: {giaBia * 3:N0}đ + Phí xử lý: 20k)";
                        }
                        else if (loaiSach == "TL nội bộ (Tiếng Việt)")
                        {
                            phiBoiThuong = (soTrang * 1000) + phiXuLy;
                            lblChiTietBoiThuong.Text = $"({soTrang} trang x 1.000đ + Phí xử lý: 20k)";
                        }
                        else if (loaiSach == "TL nội bộ (Ngoại ngữ)")
                        {
                            phiBoiThuong = (soTrang * 10000) + phiXuLy;
                            lblChiTietBoiThuong.Text = $"({soTrang} trang x 10.000đ + Phí xử lý: 20k)";
                        }
                        else // Bình thường
                        {
                            phiBoiThuong = phiXuLy;
                            lblChiTietBoiThuong.Text = "(Lưu ý: SV phải tự mua đền sách tương đương + Thu phí xử lý 20k)";
                        }
                    }

                    txtTotal.Text = (lateFine + phiBoiThuong).ToString("N0");
                };

                frmTra.Controls.AddRange(new Control[] { lblHeader, cboTinhTrang, lblPhat, lblChiTietBoiThuong, txtTotal, btnXacNhan });

                btnXacNhan.Click += (s, ev) => {
                    string newStatus = "Có sẵn"; string cond = "Tốt";
                    if (cboTinhTrang.SelectedIndex == 1) { newStatus = "Hỏng/Mất"; cond = "Lỗi"; }

                    decimal finalFine = 0;
                    decimal.TryParse(txtTotal.Text.Replace(",", "").Replace(".", ""), out finalFine);

                    bool isSuccess = MuonTraDAO.Instance.ExecuteReturn(recordId, copyId, cond, finalFine, newStatus);

                    if (isSuccess)
                    {
                        if (finalFine > 0)
                        {
                            if (physicalStatus == "Chờ kiểm duyệt")
                            {
                                string sqlUpdateDebt = "UPDATE Reader SET AcademicDebt = ISNULL(AcademicDebt, 0) + @fine WHERE ReaderID = @readerID";
                                DataProvider.Instance.ExecuteNonQuery(sqlUpdateDebt, new SqlParameter[] {
                            new SqlParameter("@fine", finalFine),
                            new SqlParameter("@readerID", readerID)
                        });

                                if (!string.IsNullOrEmpty(readerEmail))
                                {
                                    string reason = (lateDays > 0 ? $"Trễ hạn {lateDays} ngày. " : "") + (cboTinhTrang.SelectedIndex > 0 ? $"Tình trạng bồi thường: Làm mất/Hỏng tài liệu." : "");
                                    System.Threading.Tasks.Task.Run(() => EmailHelper.SendNoticeEmail(readerEmail, readerName, readerID, finalFine, reason));
                                }
                                MessageBox.Show("Đã duyệt xong!\nTiền phạt đã được ghi vào Công nợ của độc giả.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Hoàn tất quy trình trả sách!\nThủ thư đã thu tiền phạt trực tiếp.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Đã nhận lại sách an toàn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        frmTra.DialogResult = DialogResult.OK;
                        frmTra.Close();
                    }
                };

                if (frmTra.ShowDialog() == DialogResult.OK) RefreshGrid();
            }
        }
        private void dgvDangMuon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDangMuon.Rows.Count) return;

            bool isOverdue = false;
            if (dgvDangMuon.Rows[e.RowIndex].Cells["Hạn Trả"].Value != null)
            {
                if (DateTime.TryParse(dgvDangMuon.Rows[e.RowIndex].Cells["Hạn Trả"].Value.ToString(), out DateTime dueDate))
                    if (dueDate.Date < DateTime.Now.Date) isOverdue = true;
            }

            string physicalStatus = dgvDangMuon.Rows[e.RowIndex].Cells["Tình Trạng Vật Lý"].Value?.ToString();

            if (dgvDangMuon.Columns[e.ColumnIndex].Name != "btnReturn")
            {
                if (physicalStatus == "Chờ kiểm duyệt")
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    e.CellStyle.ForeColor = Color.DarkOrange;
                    e.CellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                }
                else
                {
                    e.CellStyle.ForeColor = isOverdue ? Color.Red : Color.FromArgb(64, 64, 64);
                    e.CellStyle.Font = new Font("Segoe UI", 10.5F, isOverdue ? FontStyle.Bold : FontStyle.Regular);
                }
            }
            else
            {
                e.CellStyle.BackColor = isOverdue ? Color.FromArgb(220, 53, 69) : Color.FromArgb(40, 167, 69);
                e.CellStyle.ForeColor = Color.White;
                e.Value = physicalStatus == "Chờ kiểm duyệt" ? "DUYỆT" : "XÁC NHẬN";
            }
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            if (currentReader == null || string.IsNullOrEmpty(txtMaBanSao.Text)) return;

            // Lấy thông tin phiếu mượn đang active: ID Phiếu, Hạn trả và Số lần đã gia hạn
            string lenhTruyVanPhieu = @"
        SELECT TOP 1 br.RecordID, br.DueDate, ISNULL(bd.RenewCount, 0) AS RenewCount 
        FROM BorrowDetail bd 
        JOIN BorrowRecord br ON bd.RecordID = br.RecordID 
        WHERE bd.CopyID = @maBanSao AND bd.ReturnDate IS NULL 
        ORDER BY br.BorrowDate DESC";

            DataTable dtPhieu = DataProvider.Instance.ExecuteQuery(lenhTruyVanPhieu, new SqlParameter[] {
        new SqlParameter("@maBanSao", txtMaBanSao.Text.Trim())
    });

            if (dtPhieu.Rows.Count > 0)
            {
                int maPhieu = Convert.ToInt32(dtPhieu.Rows[0]["RecordID"]);
                DateTime ngayHanTra = Convert.ToDateTime(dtPhieu.Rows[0]["DueDate"]);
                int soLanGiaHan = Convert.ToInt32(dtPhieu.Rows[0]["RenewCount"]);

                // ==============================================================
                // LUẬT 1: CHỈ ĐƯỢC GIA HẠN 1 LẦN DUY NHẤT
                // ==============================================================
                if (soLanGiaHan >= 1)
                {
                    MessageBox.Show("Tài liệu này đã được gia hạn 1 lần trước đó.\nKhông thể gia hạn thêm!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ==============================================================
                // LUẬT 2: CHẶN GIA HẠN NẾU NẰM TRONG 3 NGÀY CUỐI HOẶC QUÁ HẠN
                // ==============================================================
                int soNgayConLai = (ngayHanTra.Date - DateTime.Now.Date).Days;

                if (soNgayConLai <= 3)
                {
                    if (soNgayConLai < 0)
                        MessageBox.Show("Tài liệu này đã quá hạn trả, không thể gia hạn!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show($"Chỉ còn {soNgayConLai} ngày là đến hạn trả (nằm trong 3 ngày cuối).\nTheo quy định, không thể gia hạn lúc này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                // Áp dụng số ngày gia hạn theo loại độc giả
                int soNgayGiaHan = currentReader.ReaderType.Contains("Giảng viên") ? 28 : 14;

                if (MessageBox.Show($"Xác nhận gia hạn thêm {soNgayGiaHan} ngày cho tài liệu: {txtTenSachMuon.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // Cập nhật ngày hạn trả VÀ tăng số lần gia hạn lên 1
                        string lenhCapNhat = @"
                    UPDATE BorrowRecord SET DueDate = DATEADD(day, @soNgay, DueDate) WHERE RecordID = @maPhieu;
                    UPDATE BorrowDetail SET RenewCount = ISNULL(RenewCount, 0) + 1 WHERE RecordID = @maPhieu AND CopyID = @maBanSao;";

                        DataProvider.Instance.ExecuteNonQuery(lenhCapNhat, new SqlParameter[] {
                    new SqlParameter("@soNgay", soNgayGiaHan),
                    new SqlParameter("@maPhieu", maPhieu),
                    new SqlParameter("@maBanSao", txtMaBanSao.Text.Trim())
                });

                        MessageBox.Show($"Gia hạn thành công! (Cộng thêm {soNgayGiaHan} ngày)", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Khôi phục trạng thái UI
                        btnGiaHan.Enabled = false;
                        txtMaBanSao.Clear();
                        txtTenSachMuon.Clear();
                        RefreshGrid();
                        txtMaBanSao.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}