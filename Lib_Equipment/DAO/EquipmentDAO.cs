using System;
using System.Data; // Sửa lỗi: The type or namespace name 'DataTable' could not be found
using System.Data.SqlClient;
using Lib_Equipment.Database;

namespace Lib_Equipment.DAO
{
    public class EquipmentDAO
    {
        private static EquipmentDAO instance;
        public static EquipmentDAO Instance
        {
            get
            {
                if (instance == null) instance = new EquipmentDAO();
                return instance;
            }
        }

        private EquipmentDAO() { }

        // =======================================================
        // 1. LẤY DANH SÁCH HOẠT ĐỘNG (Dùng cho Quản lý & Bảo trì)
        // =======================================================
        public DataTable GetActiveList()
        {
            string sql = @"SELECT e.EquipmentID, e.EquipmentName, c.CategoryName, d.DepartmentName, 
                           e.ImportDate, e.PurchasePrice, e.Condition, e.CategoryID, e.DepartmentID 
                           FROM Equipment e
                           LEFT JOIN EquipmentCategory c ON e.CategoryID = c.CategoryID
                           LEFT JOIN Department d ON e.DepartmentID = d.DepartmentID
                           WHERE (e.IsDeleted = 0 OR e.IsDeleted IS NULL) AND e.Condition != N'Đã thanh lý'";
            return DataProvider.Instance.ExecuteQuery(sql);
        }

        // =======================================================
        // 2. LẤY DANH SÁCH LUÂN CHUYỂN (Sửa lỗi thiếu hàm GetTransferable)
        // =======================================================
        public DataTable GetTransferable(string deptId)
        {
            // CHỈ lấy máy Tốt hoặc Đang sử dụng. Máy Hỏng/Bảo trì sẽ bị loại bỏ ở đây.
            string sql = @"SELECT EquipmentID, EquipmentName, Condition 
                           FROM Equipment 
                           WHERE DepartmentID = @dept 
                           AND (IsDeleted = 0 OR IsDeleted IS NULL) 
                           AND Condition IN (N'Tốt', N'Đang sử dụng')";

            SqlParameter[] param = { new SqlParameter("@dept", deptId) };
            return DataProvider.Instance.ExecuteQuery(sql, param);
        }

        // =======================================================
        // 3. CÁC THAO TÁC THÊM, SỬA, XÓA
        // =======================================================
        public bool Insert(string id, string name, string catId, string deptId, DateTime date, decimal price, string cond)
        {
            string sql = @"INSERT INTO Equipment (EquipmentID, EquipmentName, CategoryID, DepartmentID, ImportDate, PurchasePrice, Condition, UpdatedAt, IsDeleted) 
                           VALUES (@id, @name, @cat, @dept, @date, @price, @condition, GETDATE(), 0)";

            SqlParameter[] p = {
                new SqlParameter("@id", id),
                new SqlParameter("@name", name),
                new SqlParameter("@cat", catId),
                new SqlParameter("@dept", deptId),
                new SqlParameter("@date", date),
                new SqlParameter("@price", price),
                new SqlParameter("@condition", cond)
            };
            return DataProvider.Instance.ExecuteNonQuery(sql, p) > 0;
        }

        public bool Update(string id, string name, string catId, string deptId, DateTime date, decimal price, string cond)
        {
            string sql = @"UPDATE Equipment SET EquipmentName = @name, CategoryID = @cat, DepartmentID = @dept, 
                           ImportDate = @date, PurchasePrice = @price, Condition = @condition, UpdatedAt = GETDATE() 
                           WHERE EquipmentID = @id";

            SqlParameter[] p = {
                new SqlParameter("@name", name),
                new SqlParameter("@cat", catId),
                new SqlParameter("@dept", deptId),
                new SqlParameter("@date", date),
                new SqlParameter("@price", price),
                new SqlParameter("@condition", cond),
                new SqlParameter("@id", id)
            };
            return DataProvider.Instance.ExecuteNonQuery(sql, p) > 0;
        }

        public bool Delete(string id)
        {
            string sql = "UPDATE Equipment SET IsDeleted = 1, UpdatedAt = GETDATE() WHERE EquipmentID = @id";
            SqlParameter[] p = { new SqlParameter("@id", id) };
            return DataProvider.Instance.ExecuteNonQuery(sql, p) > 0;
        }
    }
}