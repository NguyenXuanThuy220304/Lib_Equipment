using System;
using System.Data; // Sửa lỗi: The type or namespace name 'DataTable' could not be found
using Lib_Equipment.DAO;

namespace Lib_Equipment.BLL
{
    public class EquipmentBLL
    {
        private static EquipmentBLL instance;
        public static EquipmentBLL Instance
        {
            get { if (instance == null) instance = new EquipmentBLL(); return instance; }
        }

        private EquipmentBLL() { }

        // =======================================================
        // 1. Sửa lỗi: 'EquipmentBLL' does not contain a definition for 'LayDanhSachThietBiActive'
        // =======================================================
        public DataTable LayDanhSachThietBiActive()
        {
            return EquipmentDAO.Instance.GetActiveList();
        }

        // =======================================================
        // 2. LẤY DANH SÁCH MÁY CÓ THỂ LUÂN CHUYỂN
        // =======================================================
        public DataTable LayThietBiLuanChuyen(string deptId)
        {
            if (string.IsNullOrEmpty(deptId)) return new DataTable();
            return EquipmentDAO.Instance.GetTransferable(deptId);
        }

        // =======================================================
        // 3. NGHIỆP VỤ LƯU THIẾT BỊ (Dùng cho cả Thêm và Sửa)
        // =======================================================
        public bool LuuThietBi(string id, string name, string catId, string deptId, DateTime date, string priceStr, string cond, bool isUpdate, out string msg)
        {
            // Kiểm tra rỗng ngay tại tầng BLL để chặn dữ liệu rác
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                msg = "Mã và Tên thiết bị không được để trống!";
                return false;
            }

            // Kiểm tra định dạng giá tiền
            decimal price = 0;
            if (!decimal.TryParse(priceStr, out price) || price < 0)
            {
                msg = "Giá tiền nhập vào không hợp lệ!";
                return false;
            }

            bool result;
            if (isUpdate)
            {
                result = EquipmentDAO.Instance.Update(id, name, catId, deptId, date, price, cond);
                msg = result ? "Cập nhật thiết bị thành công!" : "Lỗi: Không thể cập nhật dữ liệu.";
            }
            else
            {
                result = EquipmentDAO.Instance.Insert(id, name, catId, deptId, date, price, cond);
                msg = result ? "Thêm thiết bị mới thành công!" : "Lỗi: Mã thiết bị có thể đã tồn tại trong hệ thống.";
            }

            return result;
        }

        // =======================================================
        // 4. Sửa lỗi: Required parameter 'msg' of 'EquipmentBLL.XoaThietBi(string, out string)'
        // =======================================================
        public bool XoaThietBi(string id, out string msg)
        {
            if (string.IsNullOrEmpty(id))
            {
                msg = "Vui lòng chọn một thiết bị để xóa!";
                return false;
            }

            bool result = EquipmentDAO.Instance.Delete(id);
            msg = result ? "Đã xóa thiết bị thành công (Xóa mềm)." : "Lỗi hệ thống khi thực hiện xóa.";
            return result;
        }
    }
}