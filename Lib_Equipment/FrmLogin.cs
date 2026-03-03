using Lib_Equipment.Database;
using Lib_Equipment.Helpers;
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

            // 2. BĂM MẬT KHẨU
            string hashedPassword = SecurityHelper.HashSHA256(password);

            // 3. Câu lệnh SQL - Lấy đầy đủ thông tin để gán vào Session
            string query = "SELECT UserID, Username, RoleID FROM [User] WHERE Username = @user AND PasswordHash = @pass AND Status = 1";

            // 4. Khai báo tham số (Chú ý tên biến @user và @pass phải khớp với câu lệnh SQL ở trên)
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@user", username),
                new SqlParameter("@pass", hashedPassword)
            };

            try
            {
                // 5. THAY ĐỔI QUAN TRỌNG: Sử dụng ExecuteQuery để lấy DataTable thay vì ExecuteScalar
                DataTable dt = DataProvider.Instance.ExecuteQuery(query, parameters);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Đăng nhập thành công -> Lưu thông tin vào Session
                    AppSession.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                    AppSession.Username = dt.Rows[0]["Username"].ToString();
                    AppSession.RoleID = dt.Rows[0]["RoleID"].ToString(); // Dòng này giúp FrmMain phân quyền đúng

                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở Form chính
                    FrmMain frm = new FrmMain();
                    this.Hide();
                    frm.ShowDialog();
                    this.Close();
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