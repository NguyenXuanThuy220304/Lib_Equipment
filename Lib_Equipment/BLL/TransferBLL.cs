using System;
using System.Data;
using Lib_Equipment.DAO;

namespace Lib_Equipment.BLL
{
    public class TransferBLL
    {
        private static TransferBLL instance;
        public static TransferBLL Instance
        {
            get { if (instance == null) instance = new TransferBLL(); return instance; }
        }

        private TransferBLL() { }

        // Nghiệp vụ: Luân chuyển thiết bị hàng loạt
        public bool ThucHienLuanChuyen(string fromDept, string toDept, string user, DateTime date, string reason, DataTable dtSelected, out string msg)
        {
            // 1. Kiểm tra logic phòng ban
            if (fromDept == toDept)
            {
                msg = "Khoa nhận thiết bị không được trùng với Khoa xuất!";
                return false;
            }

            // 2. Kiểm tra danh sách thiết bị chọn
            if (dtSelected == null || dtSelected.Rows.Count == 0)
            {
                msg = "Vui lòng chọn ít nhất một thiết bị để thực hiện luân chuyển!";
                return false;
            }

            // 3. Kiểm tra lý do
            if (string.IsNullOrWhiteSpace(reason))
            {
                msg = "Vui lòng nhập lý do luân chuyển để lưu vết hồ sơ!";
                return false;
            }

            // 4. Gọi DAO thực thi Transaction (Lưu phiếu + Lưu chi tiết + Đổi vị trí máy)
            try
            {
                if (TransferDAO.Instance.ExecuteTransfer(fromDept, toDept, user, date, reason, dtSelected))
                {
                    msg = $"Đã luân chuyển thành công {dtSelected.Rows.Count} thiết bị sang đơn vị mới.";
                    return true;
                }
                msg = "Lỗi hệ thống trong quá trình thực thi giao dịch SQL.";
                return false;
            }
            catch (Exception ex)
            {
                msg = "Lỗi phát sinh: " + ex.Message;
                return false;
            }
        }
    }
}