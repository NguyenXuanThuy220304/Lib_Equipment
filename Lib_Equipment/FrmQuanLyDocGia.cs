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
                       d.DepartmentName AS [Khoa/Viện], 
                       r.ReaderType AS [Loại thẻ],
                       r.Balance AS [Số dư (VNĐ)],
                       CASE r.Status WHEN 1 THEN N'Hoạt động' ELSE N'Khóa' END AS [Trạng thái]
                FROM Reader r
                LEFT JOIN Department d ON r.DepartmentID = d.DepartmentID
                WHERE r.IsDeleted = 0";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvDocGia.DataSource = dt;

            // Làm đẹp giao diện Lưới
            dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvDocGia.Columns.Contains("Số dư (VNĐ)"))
            {
                dgvDocGia.Columns["Số dư (VNĐ)"].DefaultCellStyle.Format = "N0";
            }
        }

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDocGia.Rows[e.RowIndex];
                selectedReaderID = row.Cells["Mã Độc giả"].Value.ToString();

                txtMaDocGia.Text = selectedReaderID;
                txtHoTen.Text = row.Cells["Họ và tên"].Value.ToString();
                cboDonVi.Text = row.Cells["Khoa/Viện"].Value.ToString();
                cboLoaiDocGia.Text = row.Cells["Loại thẻ"].Value.ToString();
                cboTrangThai.Text = row.Cells["Trạng thái"].Value.ToString();

                txtMaDocGia.Enabled = false; // Không cho sửa Mã
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDocGia.Text) || string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ Mã độc giả và Họ tên!", "Cảnh báo");
                return;
            }

            // SỬ DỤNG TRANSACTION: Đảm bảo vừa tạo người dùng vừa tạo phiếu thu tiền thành công
            string query = @"
                BEGIN TRAN;
                BEGIN TRY
                    -- 1. Tạo Độc giả mới với số dư mặc định 200.000 VNĐ
                    INSERT INTO Reader (ReaderID, FullName, DepartmentID, ReaderType, Status, CreatedAt, IsDeleted, Balance, BadDebt) 
                    VALUES (@id, @name, @dept, @type, @status, GETDATE(), 0, 200000, 0);

                    -- 2. Sinh ra biên lai trong bảng Giao dịch
                    INSERT INTO ReaderTransaction (ReaderID, Amount, TransactionType, Description)
                    VALUES (@id, 200000, N'Nạp tiền', N'Thu phí mở thẻ Độc giả mới');
                    
                    COMMIT TRAN;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRAN;
                    THROW;
                END CATCH;
            ";

            int status = cboTrangThai.Text == "Hoạt động" ? 1 : 0;

            SqlParameter[] param = {
                new SqlParameter("@id", txtMaDocGia.Text.Trim()),
                new SqlParameter("@name", txtHoTen.Text.Trim()),
                new SqlParameter("@dept", cboDonVi.SelectedValue),
                new SqlParameter("@type", cboLoaiDocGia.Text),
                new SqlParameter("@status", status)
            };

            try
            {
                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Mở thẻ độc giả thành công!\nHệ thống đã thu phí 200.000 VNĐ và cộng vào số dư thẻ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm độc giả (Mã này có thể đã tồn tại): \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedReaderID)) return;

            string query = @"UPDATE Reader 
                             SET FullName = @name, DepartmentID = @dept, ReaderType = @type, Status = @status 
                             WHERE ReaderID = @id";

            int status = cboTrangThai.Text == "Hoạt động" ? 1 : 0;

            SqlParameter[] param = {
                new SqlParameter("@name", txtHoTen.Text.Trim()),
                new SqlParameter("@dept", cboDonVi.SelectedValue),
                new SqlParameter("@type", cboLoaiDocGia.Text),
                new SqlParameter("@status", status),
                new SqlParameter("@id", selectedReaderID)
            };

            if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedReaderID)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string query = "UPDATE Reader SET IsDeleted = 1 WHERE ReaderID = @id";
                SqlParameter[] param = { new SqlParameter("@id", selectedReaderID) };

                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
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
            txtMaDocGia.Enabled = true;
            txtMaDocGia.Clear();
            txtHoTen.Clear();
            cboDonVi.SelectedIndex = 0;
            cboLoaiDocGia.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
        }

        private void btnDongBo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hệ thống sẽ:\n1. Nạp 200k cho các thẻ đang có số dư 0đ\n2. Cấp tài khoản đăng nhập cho toàn bộ Độc giả chưa có tài khoản.\n\nTiếp tục?", "Xác nhận đồng bộ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                // =========================================================
                // BƯỚC 1: BƠM 200K CHO CÁC TÀI KHOẢN CŨ 0Đ
                // =========================================================
                string querySyncBalance = @"
                    DECLARE @rID VARCHAR(20);
                    -- Tìm tất cả độc giả đang có số dư = 0 hoặc NULL
                    DECLARE cur CURSOR FOR SELECT ReaderID FROM Reader WHERE ISNULL(Balance, 0) = 0;
                    OPEN cur;
                    FETCH NEXT FROM cur INTO @rID;
                    WHILE @@FETCH_STATUS = 0
                    BEGIN
                        -- Bơm 200k vào tài khoản
                        UPDATE Reader SET Balance = 200000 WHERE ReaderID = @rID;
                        
                        -- Ghi hóa đơn vào Lịch sử giao dịch để không bị hụt dòng tiền
                        INSERT INTO ReaderTransaction (ReaderID, Amount, TransactionType, Description)
                        VALUES (@rID, 200000, N'Nạp tiền', N'Thu phí mở thẻ ban đầu (Đồng bộ hệ thống)');
                        
                        FETCH NEXT FROM cur INTO @rID;
                    END
                    CLOSE cur;
                    DEALLOCATE cur;
                ";
                DataProvider.Instance.ExecuteNonQuery(querySyncBalance);

                // =========================================================
                // BƯỚC 2: TẠO TÀI KHOẢN USER CHO ĐỘC GIẢ
                // =========================================================
                string queryGetReaders = @"
                    SELECT r.ReaderID, r.FullName 
                    FROM Reader r 
                    LEFT JOIN [User] u ON r.ReaderID = u.Username
                    WHERE r.IsDeleted = 0 AND u.UserID IS NULL";

                DataTable dtReaders = DataProvider.Instance.ExecuteQuery(queryGetReaders);

                int count = 0;
                string defaultPassHash = SecurityHelper.HashSHA256("1"); // Mật khẩu mặc định là "1"

                foreach (DataRow row in dtReaders.Rows)
                {
                    string readerId = row["ReaderID"].ToString();
                    string fullName = row["FullName"].ToString();
                    int userStatus = 1;

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
                        if (DataProvider.Instance.ExecuteNonQuery(queryInsert, param) > 0) count++;
                    }
                    catch { continue; }
                }

                MessageBox.Show($"Hoàn tất!\n- Đã nạp 200.000đ cho các độc giả cũ.\n- Đã đồng bộ tạo mới {count} tài khoản Đăng nhập (Pass mặc định: 1).", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }
    }
}