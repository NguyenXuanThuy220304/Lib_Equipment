using System;
using System.Data;
using Lib_Equipment.DAO;
using Lib_Equipment.Helpers; // Dùng SecurityHelper ở tầng này

namespace Lib_Equipment.BLL
{
    public class TaiKhoanBLL
    {
        private static TaiKhoanBLL instance;
        public static TaiKhoanBLL Instance
        {
            get { if (instance == null) instance = new TaiKhoanBLL(); return instance; }
            private set { instance = value; }
        }
        private TaiKhoanBLL() { }

        public DataTable LayDanhSachQuyen() => TaiKhoanDAO.Instance.GetRoles();
        public DataTable LayDanhSachTaiKhoan() => TaiKhoanDAO.Instance.GetAllAccounts();

        public bool ThemTaiKhoan(string user, string rawPassword, string name, string role, string statusText, out string msg)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(rawPassword))
            {
                msg = "Vui lòng nhập tên đăng nhập và mật khẩu!"; return false;
            }

            // UI cung cấp mật khẩu thô -> BLL băm nó ra -> Đẩy xuống DAO
            string passHash = SecurityHelper.HashSHA256(rawPassword);
            int status = statusText == "Hoạt động" ? 1 : 0;

            try
            {
                if (TaiKhoanDAO.Instance.AddAccount(user, passHash, name, role, status))
                {
                    msg = "Thêm tài khoản thành công!"; return true;
                }
                msg = "Lỗi không xác định."; return false;
            }
            catch (Exception ex)
            {
                msg = "Lỗi (Có thể tên đăng nhập đã tồn tại): " + ex.Message; return false;
            }
        }

        public bool SuaTaiKhoan(int userId, string name, string role, string statusText, string rawNewPassword)
        {
            if (userId == -1) return false;
            int status = statusText == "Hoạt động" ? 1 : 0;
            string newPassHash = string.IsNullOrEmpty(rawNewPassword) ? null : SecurityHelper.HashSHA256(rawNewPassword);
            return TaiKhoanDAO.Instance.UpdateAccount(userId, name, role, status, newPassHash);
        }

        public bool XoaTaiKhoan(int userId) => userId != -1 && TaiKhoanDAO.Instance.DeleteAccount(userId);
    }
}