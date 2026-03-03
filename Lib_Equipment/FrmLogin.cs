using Lib_Equipment.Database;
using Lib_Equipment.Database;
using Lib_Equipment.Helpers; // Gọi hàm bảo mật
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // 1. Kiểm tra rỗng
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. BĂM MẬT KHẨU TRƯỚC KHI TRUY VẤN
            string hashedPassword = SecurityHelper.HashSHA256(password);

            // 3. Câu lệnh SQL
            string query = "SELECT COUNT(*) FROM [User] WHERE Username = @username AND PasswordHash = @password AND Status = 1 AND IsDeleted = 0";

            // 4. Truyền tham số đã mã hóa
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", hashedPassword) // Sử dụng biến đã băm
            };

            try
            {
                // 5. Thực thi truy vấn
                int result = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query, parameters));

                if (result > 0)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result > 0)
                    {
                        // MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Ẩn FrmLogin và mở FrmMain
                        FrmMain frmMain = new FrmMain();
                        this.Hide();
                        frmMain.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập, mật khẩu không đúng hoặc tài khoản bị khóa!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL:\n" + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}