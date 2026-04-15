using System;
using System.Data;
using System.Data.SqlClient;
using Lib_Equipment.Database;

namespace Lib_Equipment.DAO
{
    public class HistoryDAO
    {
        private static HistoryDAO instance;
        public static HistoryDAO Instance
        {
            get { if (instance == null) instance = new HistoryDAO(); return instance; }
        }

        private HistoryDAO() { }

        // 1. Lấy lịch sử luân chuyển của một thiết bị cụ thể
        public DataTable GetTransferHistory(string equipmentId)
        {
            // Truy vấn kết hợp bảng Record và Detail để lấy lộ trình máy đã đi qua
            string sql = @"
                SELECT 
                    tr.TransferDate AS [Ngày chuyển],
                    d1.DepartmentName AS [Từ đơn vị],
                    d2.DepartmentName AS [Đến đơn vị],
                    tr.Reason AS [Lý do],
                    td.ConditionAtTransfer AS [Tình trạng lúc chuyển]
                FROM TransferRecord tr
                JOIN TransferDetail td ON tr.TransferID = td.TransferID
                JOIN Department d1 ON tr.FromDepartmentID = d1.DepartmentID
                JOIN Department d2 ON tr.ToDepartmentID = d2.DepartmentID
                WHERE td.EquipmentID = @eid AND tr.IsDeleted = 0
                ORDER BY tr.TransferDate DESC";

            SqlParameter[] p = { new SqlParameter("@eid", equipmentId) };
            return DataProvider.Instance.ExecuteQuery(sql, p);
        }

        // 2. Lấy lịch sử bảo trì/sửa chữa của một thiết bị cụ thể
        public DataTable GetMaintenanceHistory(string equipmentId)
        {
            string sql = @"
                SELECT 
                    MaintenanceDate AS [Ngày thực hiện],
                    Description AS [Nội dung xử lý],
                    Vendor AS [Đơn vị thực hiện],
                    PaymentSource AS [Nguồn chi phí],
                    Cost AS [Chi phí (VNĐ)]
                FROM MaintenanceRecord
                WHERE EquipmentID = @eid AND IsDeleted = 0
                ORDER BY MaintenanceDate DESC";

            SqlParameter[] p = { new SqlParameter("@eid", equipmentId) };
            return DataProvider.Instance.ExecuteQuery(sql, p);
        }
    }
}