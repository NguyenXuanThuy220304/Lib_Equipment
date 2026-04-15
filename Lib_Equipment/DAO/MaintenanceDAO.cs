using System;
using System.Data.SqlClient;
using Lib_Equipment.Database;

namespace Lib_Equipment.DAO
{
    public class MaintenanceDAO
    {
        private static MaintenanceDAO instance;
        public static MaintenanceDAO Instance
        {
            get { if (instance == null) instance = new MaintenanceDAO(); return instance; }
        }

        private MaintenanceDAO() { }

        // Thực hiện nghiệp vụ bảo trì/báo hỏng (Transaction)
        public bool ProcessMaintenance(string eid, string user, DateTime date, string desc, decimal cost, string vendor, string source, string status)
        {
            using (SqlConnection conn = new SqlConnection(DataProvider.Instance.ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 1. Lưu phiếu bảo trì
                    string sqlRecord = @"INSERT INTO MaintenanceRecord (EquipmentID, CreatedBy, MaintenanceDate, Description, Cost, Vendor, PaymentSource, IsDeleted)
                                         VALUES (@eid, @u, @d, @desc, @c, @v, @source, 0)";
                    SqlCommand cmdRec = new SqlCommand(sqlRecord, conn, trans);
                    cmdRec.Parameters.AddWithValue("@eid", eid);
                    cmdRec.Parameters.AddWithValue("@u", user);
                    cmdRec.Parameters.AddWithValue("@d", date);
                    cmdRec.Parameters.AddWithValue("@desc", desc);
                    cmdRec.Parameters.AddWithValue("@c", cost);
                    cmdRec.Parameters.AddWithValue("@v", vendor);
                    cmdRec.Parameters.AddWithValue("@source", source);
                    cmdRec.ExecuteNonQuery();

                    // 2. Cập nhật trạng thái máy
                    string sqlUpdate = "UPDATE Equipment SET Condition = @status, UpdatedAt = GETDATE() WHERE EquipmentID = @eid";
                    SqlCommand cmdUp = new SqlCommand(sqlUpdate, conn, trans);
                    cmdUp.Parameters.AddWithValue("@status", status);
                    cmdUp.Parameters.AddWithValue("@eid", eid);
                    cmdUp.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        // Nghiệm thu máy sửa xong
        public bool CompleteMaintenance(string eid)
        {
            string sql = "UPDATE Equipment SET Condition = N'Tốt', UpdatedAt = GETDATE() WHERE EquipmentID = @id";
            SqlParameter[] p = { new SqlParameter("@id", eid) };
            return DataProvider.Instance.ExecuteNonQuery(sql, p) > 0;
        }
    }
}