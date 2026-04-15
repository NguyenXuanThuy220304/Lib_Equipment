using System;
using System.Data;
using System.Data.SqlClient;
using Lib_Equipment.Database;

namespace Lib_Equipment.DAO
{
    public class TransferDAO
    {
        private static TransferDAO instance;
        public static TransferDAO Instance
        {
            get { if (instance == null) instance = new TransferDAO(); return instance; }
        }

        private TransferDAO() { }

        // Thực hiện nghiệp vụ Luân chuyển hàng loạt thiết bị (Dùng Transaction)
        public bool ExecuteTransfer(string fromDept, string toDept, string user, DateTime date, string reason, DataTable dtSelected)
        {
            // Sử dụng Connection trực tiếp để kiểm soát Transaction
            using (SqlConnection conn = new SqlConnection(DataProvider.Instance.ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // Bước 1: Lưu thông tin chung vào bảng TransferRecord
                    // Lấy luôn ID vừa sinh ra bằng SCOPE_IDENTITY()
                    string sqlRec = @"INSERT INTO TransferRecord (FromDepartmentID, ToDepartmentID, CreatedBy, TransferDate, Reason, IsDeleted) 
                                      VALUES (@f, @t, @u, @d, @r, 0); 
                                      SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdRec = new SqlCommand(sqlRec, conn, trans);
                    cmdRec.Parameters.AddWithValue("@f", fromDept);
                    cmdRec.Parameters.AddWithValue("@t", toDept);
                    cmdRec.Parameters.AddWithValue("@u", user);
                    cmdRec.Parameters.AddWithValue("@d", date);
                    cmdRec.Parameters.AddWithValue("@r", reason);

                    // Lấy mã phiếu vừa tạo để dùng cho các bản ghi chi tiết
                    int newTransferId = Convert.ToInt32(cmdRec.ExecuteScalar());

                    // Bước 2: Duyệt qua danh sách thiết bị đã chọn để luân chuyển
                    foreach (DataRow row in dtSelected.Rows)
                    {
                        string equipmentId = row["EquipmentID"].ToString();
                        string currentCondition = row["Condition"].ToString();

                        // Chèn vào bảng chi tiết luân chuyển
                        string sqlDetail = @"INSERT INTO TransferDetail (TransferID, EquipmentID, ConditionAtTransfer) 
                                             VALUES (@transId, @eid, @cond)";
                        SqlCommand cmdDet = new SqlCommand(sqlDetail, conn, trans);
                        cmdDet.Parameters.AddWithValue("@transId", newTransferId);
                        cmdDet.Parameters.AddWithValue("@eid", equipmentId);
                        cmdDet.Parameters.AddWithValue("@cond", currentCondition);
                        cmdDet.ExecuteNonQuery();

                        // Cập nhật vị trí (Khoa) mới và trạng thái "Đang sử dụng" cho thiết bị
                        string sqlUpdateEq = @"UPDATE Equipment 
                                               SET DepartmentID = @toDept, Condition = N'Đang sử dụng', UpdatedAt = GETDATE() 
                                               WHERE EquipmentID = @eid";
                        SqlCommand cmdUp = new SqlCommand(sqlUpdateEq, conn, trans);
                        cmdUp.Parameters.AddWithValue("@toDept", toDept);
                        cmdUp.Parameters.AddWithValue("@eid", equipmentId);
                        cmdUp.ExecuteNonQuery();
                    }

                    trans.Commit(); // Xác nhận hoàn tất mọi thay đổi
                    return true;
                }
                catch (Exception)
                {
                    trans.Rollback(); // Nếu có lỗi ở bất kỳ vòng lặp nào, hủy bỏ toàn bộ để tránh sai lệch dữ liệu
                    return false;
                }
            }
        }
    }
}