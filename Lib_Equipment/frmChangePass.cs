using Lib_Equipment.Database;
using Lib_Equipment.Helpers;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class frmChangePass : Form
    {
        private string currentUser;

        public frmChangePass(string username)
        {
            InitializeComponent();
            currentUser = username;
        }

        // Xử lý nút Cập nhật
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string newPass = txtNewPass.Text.Trim();
            string confirmPass = txtConfirmPass.Text.Trim();

            if (newPass.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự để đảm bảo an toàn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Băm mật khẩu mới bằng SHA256 
            string newHash = SecurityHelper.HashSHA256(newPass);

            // Cập nhật vào Database
            string query = "UPDATE [User] SET PasswordHash = @pass WHERE Username = @user";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@pass", newHash),
                new SqlParameter("@user", currentUser)
            };

            try
            {
                if (DataProvider.Instance.ExecuteNonQuery(query, parameters) > 0)
                {
                    MessageBox.Show("Đổi mật khẩu thành công! Bạn có thể sử dụng các tính năng của Thư viện.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form đổi pass, luồng ở frmLogin sẽ tiếp tục mở Trang chủ
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật mật khẩu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xử lý nút Bỏ qua
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}