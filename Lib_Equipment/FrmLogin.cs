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

            // 3. Câu lệnh SQL
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
                    // TÍNH NĂNG MỚI: AUTO CHẠY NGẦM GỬI MAIL KHI ĐĂNG NHẬP THÀNH CÔNG
                    // =================================================================
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        try { Lib_Equipment.BLL.MuonTraBLL.Instance.TuDongKiemTraVaGuiMailLuuLuu(); }
                        catch { /* Bỏ qua nếu mất mạng để không văng app */ }
                    });

                    // =================================================================
                    // XỬ LÝ PHÂN LUỒNG VÀ ÉP ĐỔI MẬT KHẨU
                    // =================================================================

                    if (AppSession.RoleID == "Reader")
                    {
                        // --- BƯỚC KIỂM TRA LUẬT KỶ LUẬT THƯ VIỆN ---
                        string checkReaderQuery = "SELECT Status, ISNULL(IsPermanentlyBanned, 0) AS IsPermanentlyBanned FROM Reader WHERE ReaderID = @readerId";
                        DataTable dtReader = DataProvider.Instance.ExecuteQuery(checkReaderQuery, new SqlParameter[] { new SqlParameter("@readerId", AppSession.Username) });

                        if (dtReader.Rows.Count > 0)
                        {
                            bool isBanned = Convert.ToBoolean(dtReader.Rows[0]["IsPermanentlyBanned"]);
                            int readerStatus = Convert.ToInt32(dtReader.Rows[0]["Status"]);

                            // Luật 1: Cấm vĩnh viễn (Đã trễ > 30 ngày)
                            if (isBanned)
                            {
                                // ĐÃ XÓA LỆNH return; ĐỂ CHO PHÉP ĐĂNG NHẬP
                                MessageBox.Show("Tài khoản của bạn đã bị CẤM VĨNH VIỄN do vi phạm nghiêm trọng nội quy (Quá hạn > 30 ngày)!\n\nHệ thống tạm thời cho phép bạn đăng nhập để thực hiện TRẢ SÁCH và THANH TOÁN CÔNG NỢ.", "Thông báo vi phạm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            // Luật 2: Khóa tạm thời (Trễ từ 3 - 30 ngày)
                            else if (readerStatus == 0) // Dùng else if để không hiện 2 thông báo liên tiếp
                            {
                                // ĐÃ XÓA LỆNH return; ĐỂ CHO PHÉP ĐĂNG NHẬP
                                MessageBox.Show("Tài khoản đang bị KHÓA TẠM THỜI do có sách mượn quá hạn!\n\nVui lòng sử dụng hệ thống để trả sách và thanh toán công nợ. Tài khoản sẽ tự động mở khóa sau khi hoàn tất.", "Tài khoản bị khóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        // -------------------------------------------------------------

                        // Kiểm tra xem có phải pass mặc định không
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
                    MessageBox.Show("Tên đăng nhập, mật khẩu không đúng hoặc tài khoản bị vô hiệu hóa!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
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