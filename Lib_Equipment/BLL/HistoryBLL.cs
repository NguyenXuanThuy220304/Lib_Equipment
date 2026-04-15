using System;
using System.Data;
using Lib_Equipment.DAO;

namespace Lib_Equipment.BLL
{
    public class HistoryBLL
    {
        private static HistoryBLL instance;
        public static HistoryBLL Instance
        {
            get { if (instance == null) instance = new HistoryBLL(); return instance; }
        }

        private HistoryBLL() { }

        // 1. Nghiệp vụ: Lấy toàn bộ lịch sử luân chuyển của thiết bị
        public DataTable LayLichSuLuanChuyen(string equipmentId)
        {
            if (string.IsNullOrEmpty(equipmentId)) return new DataTable();

            // Gọi DAO để lấy dữ liệu thô từ Database
            return HistoryDAO.Instance.GetTransferHistory(equipmentId);
        }

        // 2. Nghiệp vụ: Lấy toàn bộ lịch sử bảo trì/sửa chữa của thiết bị
        public DataTable LayLichSuBaoTri(string equipmentId)
        {
            if (string.IsNullOrEmpty(equipmentId)) return new DataTable();

            // Gọi DAO để lấy dữ liệu thô từ Database
            return HistoryDAO.Instance.GetMaintenanceHistory(equipmentId);
        }
    }
}