using Lib_Equipment.DAO;
using Lib_Equipment.Helpers;
using System;
using System.Data;

namespace Lib_Equipment.BLL
{
    public class DocGiaBLL
    {
        private static DocGiaBLL instance;
        public static DocGiaBLL Instance { get { if (instance == null) instance = new DocGiaBLL(); return instance; } }
        private DocGiaBLL() { }

        // ==========================================
        // 1. KHÔI PHỤC CÁC HÀM CŨ CỦA BẠN BỊ MẤT
        // ==========================================
        public DataTable LayDanhSachKhoaVien() => DocGiaDAO.Instance.GetDepartments();
        public DataTable LayDanhSachDocGia() => DocGiaDAO.Instance.GetAllReaders();
        public bool XoaDocGia(string id) => !string.IsNullOrEmpty(id) && DocGiaDAO.Instance.DeleteReader(id);
        public int DongBoHeThong(string defaultPasswordHash) => DocGiaDAO.Instance.SyncSystem(defaultPasswordHash);

        // ==========================================
        // 2. CÁC HÀM XỬ LÝ MỚI (ĐÃ CÓ EMAIL VÀ CÔNG NỢ)
        // ==========================================
        public bool ThemDocGia(string id, string name, string deptId, string type, int status, string email, out string msg)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name)) { msg = "Vui lòng nhập đủ thông tin!"; return false; }
            try
            {
                if (DocGiaDAO.Instance.AddReader(id, name, deptId, type, status, email))
                {
                    msg = "Thêm độc giả thành công!"; return true;
                }
                msg = "Lỗi kết nối CSDL."; return false;
            }
            catch (Exception ex) { msg = "Mã Độc giả đã tồn tại!"; return false; }
        }

        public bool SuaDocGia(string id, string name, string deptId, string type, int status, string email)
        {
            if (string.IsNullOrEmpty(id)) return false;
            return DocGiaDAO.Instance.UpdateReader(id, name, deptId, type, status, email);
        }

        public bool GhiNhanViPhamVaBoiThuong(string readerID, decimal amount, string violationType, out string msg)
        {
            try
            {
                DataTable dt = DocGiaDAO.Instance.GetReaderBasicInfo(readerID);
                if (dt.Rows.Count == 0) { msg = "Không tìm thấy thông tin độc giả."; return false; }

                string name = dt.Rows[0]["FullName"].ToString();
                string email = dt.Rows[0]["Email"].ToString();

                bool dbSuccess = DocGiaDAO.Instance.UpdateAcademicDebt(readerID, amount);

                if (dbSuccess)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        System.Threading.Tasks.Task.Run(() => Helpers.EmailHelper.SendNoticeEmail(email, name, readerID, amount, violationType));
                        msg = $"Đã ghi nợ {amount:N0} VNĐ và gửi thông báo tới Email: {email}";
                    }
                    else
                    {
                        msg = $"Đã ghi nợ {amount:N0} VNĐ (Độc giả chưa cập nhật Email)";
                    }
                    return true;
                }
                msg = "Lỗi cập nhật CSDL."; return false;
            }
            catch (Exception ex) { msg = "Lỗi hệ thống: " + ex.Message; return false; }
        }
        public int DongBoHeThong()
        {
            string defaultPassHash = SecurityHelper.HashSHA256("1"); // Pass mặc định: 1
            return DocGiaDAO.Instance.SyncSystem(defaultPassHash);
        }

    }
}