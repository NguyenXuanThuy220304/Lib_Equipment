using System;
using System.Data; // Sửa lỗi: The type or namespace name 'DataTable' could not be found
using Lib_Equipment.DAO;

namespace Lib_Equipment.BLL
{
    public class MaintenanceBLL
    {
        private static MaintenanceBLL instance;
        public static MaintenanceBLL Instance
        {
            get { if (instance == null) instance = new MaintenanceBLL(); return instance; }
        }

        private MaintenanceBLL() { }

        // Sửa lỗi: 'MaintenanceBLL' does not contain a definition for 'XuLyNghiepVu'
        public bool XuLyNghiepVu(string eid, string user, DateTime date, string desc, string costStr, string vendor, string source, string action, out string msg)
        {
            // 1. Kiểm tra đầu vào
            if (string.IsNullOrEmpty(eid))
            {
                msg = "Bạn chưa chọn thiết bị nào!";
                return false;
            }

            decimal cost = 0;
            if (!decimal.TryParse(costStr, out cost) || cost < 0)
            {
                msg = "Số tiền chi phí không hợp lệ!";
                return false;
            }

            // 2. Gọi DAO thực hiện (Đã có Transaction bên trong DAO)
            if (MaintenanceDAO.Instance.ProcessMaintenance(eid, user, date, desc, cost, vendor, source, action))
            {
                msg = "Cập nhật trạng thái bảo trì/hỏng thành công!";
                return true;
            }

            msg = "Lỗi hệ thống khi cập nhật dữ liệu!";
            return false;
        }

        // Hàm nghiệm thu máy (Dùng cho sự kiện Double Click trên lưới)
        public bool NghiemThuMay(string eid, string currentStatus, out string msg)
        {
            if (currentStatus != "Đang bảo trì")
            {
                msg = "Chỉ có thể nghiệm thu máy đang ở trạng thái 'Đang bảo trì'!";
                return false;
            }

            if (MaintenanceDAO.Instance.CompleteMaintenance(eid))
            {
                msg = "Nghiệm thu thành công! Máy đã quay lại trạng thái Tốt.";
                return true;
            }

            msg = "Lỗi hệ thống khi nghiệm thu!";
            return false;
        }
    }
}