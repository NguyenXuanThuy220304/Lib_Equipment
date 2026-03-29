using Lib_Equipment.Database;
using Lib_Equipment.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
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
            LoadComboboxDonVi();
            LoadData();
        }

        // =======================================================
        // 1. TẢI DỮ LIỆU
        // =======================================================
        private void LoadComboboxDonVi()
        {
            string query = "SELECT DepartmentID, DepartmentName FROM Department WHERE IsDeleted = 0";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            cboDonVi.DataSource = dt;
            cboDonVi.DisplayMember = "DepartmentName";
            cboDonVi.ValueMember = "DepartmentID";
        }

        private void LoadData()
        {
            string query = @"
                SELECT r.ReaderID AS [Mã Độc giả], 
                       r.FullName AS [Họ và tên], 
                       r.DepartmentID AS [Khoa/Viện], 
                       r.ReaderType AS [Loại thẻ],
                       CASE 
                           WHEN r.Status = '0' OR r.Status = 'False' THEN N'Bị khóa (Thủ công)'
                           WHEN (SELECT COUNT(*) FROM BorrowRecord br 
                                 JOIN BorrowDetail bd ON br.RecordID = bd.RecordID 
                                 WHERE br.ReaderID = r.ReaderID 
                                 AND bd.ReturnDate IS NULL 
                                 AND br.DueDate < CAST(GETDATE() AS DATE)) > 0 
                           THEN N'Tạm khóa (Quá hạn)'
                           ELSE N'Hợp lệ' 
                       END AS [Trạng thái thẻ]
                FROM Reader r
                WHERE r.IsDeleted = 0 OR r.IsDeleted IS NULL";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvDocGia.DataSource = dt;

            // Format màu cho dễ nhìn (Bôi đỏ các thẻ bị khóa)
            if (dgvDocGia.Columns.Contains("Trạng thái thẻ"))
            {
                foreach (DataGridViewRow row in dgvDocGia.Rows)
                {
                    string status = row.Cells["Trạng thái thẻ"].Value.ToString();
                    if (status != "Hợp lệ")
                    {
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        // =======================================================
        // 2. SỰ KIỆN CHỌN DÒNG
        // =======================================================
        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDocGia.Rows[e.RowIndex];

                selectedReaderID = row.Cells["Mã Độc giả"].Value.ToString();

                txtMaDocGia.Text = selectedReaderID;
                txtHoTen.Text = row.Cells["Họ và tên"].Value.ToString();
                cboDonVi.SelectedValue = row.Cells["Khoa/Viện"].Value.ToString();
                cboLoaiDocGia.Text = row.Cells["Loại thẻ"].Value.ToString();
                cboTrangThai.Text = row.Cells["Trạng thái thẻ"].Value.ToString();

                // Khóa không cho sửa Mã sinh viên
                txtMaDocGia.Enabled = false;
            }
        }

        // =======================================================
        // 3. THÊM ĐỘC GIẢ (TÍCH HỢP TỰ ĐỘNG TẠO TÀI KHOẢN)
        // =======================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDocGia.Text) || string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã độc giả và Họ tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gán giá trị bit 1/0 thay vì string cho Status
            int status = cboTrangThai.Text == "Hợp lệ" ? 1 : 0;
            int userStatus = status;

            // Mã băm SHA256 cho mật khẩu mặc định là "1"
            string defaultPassHash = SecurityHelper.HashSHA256("1");

            // Sử dụng Transaction trong T-SQL để đồng thời Insert vào 2 bảng
            string query = @"
                BEGIN TRY
                    BEGIN TRAN;
                    
                    -- 1. Tạo hồ sơ Độc giả
                    INSERT INTO Reader (ReaderID, FullName, DepartmentID, ReaderType, Status, CreatedAt, IsDeleted) 
                    VALUES (@id, @name, @dept, @type, @status, GETDATE(), 0);

                    -- 2. Tự động cấp Tài khoản (SỬA ROLEID THÀNH Reader)
                    INSERT INTO [User] (Username, PasswordHash, FullName, RoleID, Status, CreatedAt, IsDeleted)
                    VALUES (@id, @pass, @name, 'Reader', @userStatus, GETDATE(), 0);

                    COMMIT TRAN;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRAN;
                    THROW; -- Đẩy lỗi ra C# catch
                END CATCH
            ";

            SqlParameter[] param = {
                new SqlParameter("@id", txtMaDocGia.Text.Trim()),
                new SqlParameter("@name", txtHoTen.Text.Trim()),
                new SqlParameter("@dept", cboDonVi.SelectedValue),
                new SqlParameter("@type", cboLoaiDocGia.Text),
                new SqlParameter("@status", status),
                new SqlParameter("@pass", defaultPassHash),
                new SqlParameter("@userStatus", userStatus)
            };

            try
            {
                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show($"Thêm Độc giả thành công!\n\nHệ thống đã cấp tài khoản:\n- Tài khoản: {txtMaDocGia.Text.Trim()}\n- Mật khẩu: 1",
                                    "Cấp tài khoản tự động", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi (Mã độc giả có thể đã tồn tại): " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // 4. SỬA ĐỘC GIẢ (ĐỒNG BỘ THÔNG TIN SANG BẢNG USER)
        // =======================================================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedReaderID))
            {
                MessageBox.Show("Vui lòng chọn một độc giả dưới bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int status = cboTrangThai.Text == "Hợp lệ" ? 1 : 0;
            int userStatus = status;

            string query = @"
                BEGIN TRY
                    BEGIN TRAN;

                    -- Cập nhật thông tin thẻ độc giả
                    UPDATE Reader 
                    SET FullName = @name, DepartmentID = @dept, ReaderType = @type, Status = @status 
                    WHERE ReaderID = @id;

                    -- Đồng bộ cập nhật Tên và Trạng thái khóa/mở sang tài khoản đăng nhập
                    UPDATE [User] 
                    SET FullName = @name, Status = @userStatus 
                    WHERE Username = @id;

                    COMMIT TRAN;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRAN;
                    THROW;
                END CATCH
            ";

            SqlParameter[] param = {
                new SqlParameter("@name", txtHoTen.Text.Trim()),
                new SqlParameter("@dept", cboDonVi.SelectedValue),
                new SqlParameter("@type", cboLoaiDocGia.Text),
                new SqlParameter("@status", status),
                new SqlParameter("@userStatus", userStatus),
                new SqlParameter("@id", selectedReaderID)
            };

            try
            {
                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // 5. XÓA MỀM (SOFT DELETE CẢ ĐỘC GIẢ LẪN TÀI KHOẢN)
        // =======================================================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedReaderID)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa thẻ độc giả này?\nTài khoản đăng nhập của sinh viên cũng sẽ bị hủy.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = @"
                    BEGIN TRAN;
                    UPDATE Reader SET IsDeleted = 1 WHERE ReaderID = @id;
                    UPDATE [User] SET IsDeleted = 1 WHERE Username = @id;
                    COMMIT TRAN;
                ";
                SqlParameter[] param = { new SqlParameter("@id", selectedReaderID) };

                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Đã xóa thẻ độc giả và thu hồi tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
        }

        // =======================================================
        // 6. LÀM MỚI (XÓA TRẮNG FORM NHẬP)
        // =======================================================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            selectedReaderID = "";
            txtMaDocGia.Enabled = true;
            txtMaDocGia.Clear();
            txtHoTen.Clear();

            if (cboDonVi.Items.Count > 0) cboDonVi.SelectedIndex = 0;
            cboLoaiDocGia.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
        }

        // =======================================================
        // 7. ĐỒNG BỘ TÀI KHOẢN CŨ
        // =======================================================
        private void btnDongBo_Click(object sender, EventArgs e)
        {
            // Tìm những Độc giả có trong bảng Reader nhưng CHƯA CÓ trong bảng User
            string queryGet = @"
                SELECT ReaderID, FullName, Status 
                FROM Reader 
                WHERE ReaderID NOT IN (SELECT Username FROM [User]) AND IsDeleted = 0";

            DataTable dtChuaCoTK = DataProvider.Instance.ExecuteQuery(queryGet);

            if (dtChuaCoTK == null || dtChuaCoTK.Rows.Count == 0)
            {
                MessageBox.Show("Tuyệt vời! Tất cả độc giả trong hệ thống đều đã có tài khoản đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Phát hiện {dtChuaCoTK.Rows.Count} độc giả cũ chưa có tài khoản.\nBạn có muốn tự động tạo tài khoản (Mật khẩu mặc định: 1) cho họ không?", "Xác nhận đồng bộ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int count = 0;
                string defaultPassHash = SecurityHelper.HashSHA256("1");

                foreach (DataRow row in dtChuaCoTK.Rows)
                {
                    string readerId = row["ReaderID"].ToString();
                    string fullName = row["FullName"].ToString();

                    // ĐẢM BẢO GÁN CỨNG TRẠNG THÁI 1 (MỞ KHÓA) KHI ĐỒNG BỘ ĐỂ ĐĂNG NHẬP ĐƯỢC
                    int userStatus = 1;

                    // Lệnh Insert cho từng người (SỬA ROLEID THÀNH Reader)
                    string queryInsert = @"
                        INSERT INTO [User] (Username, PasswordHash, FullName, RoleID, Status, CreatedAt, IsDeleted)
                        VALUES (@user, @pass, @name, 'Reader', @status, GETDATE(), 0)";

                    SqlParameter[] param = {
                        new SqlParameter("@user", readerId),
                        new SqlParameter("@pass", defaultPassHash),
                        new SqlParameter("@name", fullName),
                        new SqlParameter("@status", userStatus)
                    };

                    try
                    {
                        if (DataProvider.Instance.ExecuteNonQuery(queryInsert, param) > 0)
                        {
                            count++;
                        }
                    }
                    catch
                    {
                        // Bỏ qua lỗi và chạy tiếp
                        continue;
                    }
                }

                MessageBox.Show($"Hoàn tất! Đã đồng bộ thành công {count} tài khoản cho độc giả cũ.\nBây giờ họ có thể đăng nhập bằng Mã độc giả và pass: 1.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}