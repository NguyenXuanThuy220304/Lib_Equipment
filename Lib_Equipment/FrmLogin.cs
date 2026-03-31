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

            // 2. BĂM MẬT KHẨU (Sử dụng chuẩn SHA256 của bạn)
            string hashedPassword = SecurityHelper.HashSHA256(password);

            // 3. Câu lệnh SQL - Lấy thêm FullName để hiển thị lời chào trên form Độc giả
            string query = "SELECT UserID, Username, RoleID, FullName FROM [User] WHERE Username = @user AND PasswordHash = @pass AND Status = 1";

            // 4. Khai báo tham số
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@user", username),
                new SqlParameter("@pass", hashedPassword)
            };

            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery(query, parameters);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Đăng nhập thành công -> Lưu thông tin vào Session
                    AppSession.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                    AppSession.Username = dt.Rows[0]["Username"].ToString();
                    AppSession.RoleID = dt.Rows[0]["RoleID"].ToString();

                    string fullName = dt.Rows[0]["FullName"].ToString();

                    // =================================================================
                    // XỬ LÝ PHÂN LUỒNG VÀ ÉP ĐỔI MẬT KHẨU
                    // =================================================================

                    // Giả sử RoleID của Độc giả là Reader (Bạn kiểm tra lại DB của mình nhé, nếu là chữ thì đổi thành "DocGia")
                    if (AppSession.RoleID == "Reader")
                    {
                        // Sinh ra mã Hash của chữ "1" để so sánh xem có phải pass mặc định không
                        string defaultPasswordHash = SecurityHelper.HashSHA256("1");

                        if (hashedPassword == defaultPasswordHash)
                        {
                            MessageBox.Show("Đây là lần đăng nhập đầu tiên.\nHệ thống yêu cầu bạn đổi mật khẩu để bảo mật tài khoản!", "Thiết lập bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Mở form đổi mật khẩu, bắt người dùng xử lý xong mới chạy tiếp
                            frmChangePass frmDoiMK = new frmChangePass(AppSession.Username);
                            frmDoiMK.ShowDialog();
                        }

                        // Mở Trang chủ Độc giả (Kèm theo username và tên hiển thị)
                        frmTrangChuDocGia frmDocGia = new frmTrangChuDocGia(AppSession.Username, fullName);
                        this.Hide();
                        frmDocGia.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        // Nếu là Quản trị viên, Thủ thư... thì vào thẳng phần mềm quản lý
                        MessageBox.Show("Đăng nhập quyền Quản trị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        FrmMain frm = new FrmMain();
                        this.Hide();
                        frm.ShowDialog();
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

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnDangNhap.PerformClick();
            }
        }
    }
}