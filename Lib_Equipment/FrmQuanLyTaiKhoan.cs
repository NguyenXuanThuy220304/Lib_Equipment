using Lib_Equipment.Database;
using Lib_Equipment.Database;
using Lib_Equipment.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmQuanLyTaiKhoan : Form
    {
        // Biến lưu ID của người dùng đang được chọn trên DataGridView
        private int selectedUserID = -1;

        public FrmQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        private void FrmQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadComboboxQuyen();
            LoadData();
        }

        // =======================================================
        // 1. TẢI DỮ LIỆU LÊN GIAO DIỆN
        // =======================================================
        private void LoadComboboxQuyen()
        {
            string query = "SELECT RoleID, RoleName FROM Role";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            cboQuyen.DataSource = dt;
            cboQuyen.DisplayMember = "RoleName"; // Tên hiển thị ra ngoài
            cboQuyen.ValueMember = "RoleID";     // Giá trị thực sự ẩn bên dưới
        }

        private void LoadData()
        {
            // Chỉ lấy các tài khoản chưa bị xóa (IsDeleted = 0)
            string query = @"
                SELECT u.UserID, u.Username, u.FullName, u.RoleID, r.RoleName, 
                       CASE WHEN u.Status = 1 THEN N'Hoạt động' ELSE N'Bị khóa' END AS TrangThai
                FROM [User] u
                JOIN Role r ON u.RoleID = r.RoleID
                WHERE u.IsDeleted = 0";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvTaiKhoan.DataSource = dt;

            // Đổi tên cột cho đẹp
            dgvTaiKhoan.Columns["UserID"].HeaderText = "ID";
            dgvTaiKhoan.Columns["UserID"].Width = 50;
            dgvTaiKhoan.Columns["Username"].HeaderText = "Tên đăng nhập";
            dgvTaiKhoan.Columns["FullName"].HeaderText = "Họ và tên";
            dgvTaiKhoan.Columns["RoleName"].HeaderText = "Quyền hạn";
            dgvTaiKhoan.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvTaiKhoan.Columns["RoleID"].Visible = false; // Ẩn cột mã Role đi
        }

        // =======================================================
        // 2. SỰ KIỆN CLICK VÀO BẢNG ĐỂ ĐỔ DỮ LIỆU LÊN TEXTBOX
        // =======================================================
        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTaiKhoan.Rows[e.RowIndex];
                selectedUserID = Convert.ToInt32(row.Cells["UserID"].Value);

                txtTenDangNhap.Text = row.Cells["Username"].Value.ToString();
                txtHoTen.Text = row.Cells["FullName"].Value.ToString();
                cboQuyen.SelectedValue = row.Cells["RoleID"].Value.ToString();
                cboTrangThai.Text = row.Cells["TrangThai"].Value.ToString();

                // Khi click vào để sửa, không hiển thị mật khẩu cũ
                txtMatKhau.Text = "";
                txtTenDangNhap.Enabled = false; // Không cho sửa tên đăng nhập
            }
        }

        // =======================================================
        // 3. THÊM TÀI KHOẢN MỚI
        // =======================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Băm mật khẩu bằng class Helper đã viết
            string hashedPassword = SecurityHelper.HashSHA256(txtMatKhau.Text.Trim());
            int status = cboTrangThai.Text == "Hoạt động" ? 1 : 0;

            string query = "INSERT INTO [User] (Username, PasswordHash, FullName, RoleID, Status) VALUES (@user, @pass, @name, @role, @status)";

            SqlParameter[] param = {
                new SqlParameter("@user", txtTenDangNhap.Text.Trim()),
                new SqlParameter("@pass", hashedPassword),
                new SqlParameter("@name", txtHoTen.Text.Trim()),
                new SqlParameter("@role", cboQuyen.SelectedValue.ToString()),
                new SqlParameter("@status", status)
            };

            try
            {
                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(null, null); // Xóa trắng các ô nhập
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi (Có thể tên đăng nhập đã tồn tại): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // 4. SỬA THÔNG TIN TÀI KHOẢN
        // =======================================================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedUserID == -1)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản dưới bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int status = cboTrangThai.Text == "Hoạt động" ? 1 : 0;
            string query = "UPDATE [User] SET FullName = @name, RoleID = @role, Status = @status";

            // Nếu người dùng nhập vào ô mật khẩu -> Ý là họ muốn đổi mật khẩu mới
            if (!string.IsNullOrEmpty(txtMatKhau.Text.Trim()))
            {
                query += ", PasswordHash = @pass";
            }

            query += " WHERE UserID = @id";

            SqlParameter[] param = {
                new SqlParameter("@name", txtHoTen.Text.Trim()),
                new SqlParameter("@role", cboQuyen.SelectedValue.ToString()),
                new SqlParameter("@status", status),
                new SqlParameter("@id", selectedUserID),
                new SqlParameter("@pass", SecurityHelper.HashSHA256(txtMatKhau.Text.Trim()))
            };

            if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        // =======================================================
        // 5. XÓA TÀI KHOẢN (SOFT DELETE - KHÔNG DÙNG LỆNH DELETE)
        // =======================================================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedUserID == -1) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Cập nhật cột IsDeleted = 1 thay vì xóa cứng
                string query = "UPDATE [User] SET IsDeleted = 1 WHERE UserID = @id";
                SqlParameter[] param = { new SqlParameter("@id", selectedUserID) };

                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
        }

        // =======================================================
        // 6. LÀM MỚI FORM (RESET TEXTBOXES)
        // =======================================================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            selectedUserID = -1;
            txtTenDangNhap.Enabled = true;
            txtTenDangNhap.Clear();
            txtHoTen.Clear();
            txtMatKhau.Clear();
            cboTrangThai.SelectedIndex = 0;
            if (cboQuyen.Items.Count > 0) cboQuyen.SelectedIndex = 0;
        }
    }
}