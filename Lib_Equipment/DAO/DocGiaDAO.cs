using System;
using System.Data;
using System.Data.SqlClient;
using Lib_Equipment.Database;

namespace Lib_Equipment.DAO
{
    public class DocGiaDAO
    {
        private static DocGiaDAO instance;
        public static DocGiaDAO Instance { get { if (instance == null) instance = new DocGiaDAO(); return instance; } }
        private DocGiaDAO() { }

        public DataTable GetDepartments() => DataProvider.Instance.ExecuteQuery("SELECT DepartmentID, DepartmentName FROM Department WHERE IsDeleted = 0");

        public DataTable GetAllReaders()
        {
            string query = @"
                SELECT r.ReaderID AS [Mã Độc giả], r.FullName AS [Họ và tên], d.DepartmentName AS [Khoa/Viện], 
                       r.ReaderType AS [Loại thẻ], r.Email, r.AcademicDebt AS [Công nợ (VNĐ)],
                       CASE WHEN r.IsPermanentlyBanned = 1 THEN N'CẤM VĨNH VIỄN' 
                            WHEN r.Status = 1 THEN N'Hoạt động' ELSE N'Khóa' END AS [Trạng thái]
                FROM Reader r LEFT JOIN Department d ON r.DepartmentID = d.DepartmentID WHERE r.IsDeleted = 0";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public void AutoUpdateDebt()
        {
            // Gọi thủ tục không cần tham số
            DataProvider.Instance.ExecuteNonQuery("EXEC sp_UpdateAcademicDebt", null);
        }
        public bool AddReader(string id, string name, string deptId, string type, int status, string email)
        {
            string query = @"INSERT INTO Reader (ReaderID, FullName, DepartmentID, ReaderType, Status, CreatedAt, IsDeleted, AcademicDebt, IsPermanentlyBanned, Email) 
                             VALUES (@id, @name, @dept, @type, @status, GETDATE(), 0, 0, 0, @email)";
            SqlParameter[] param = { new SqlParameter("@id", id), new SqlParameter("@name", name), new SqlParameter("@dept", deptId), new SqlParameter("@type", type), new SqlParameter("@status", status), new SqlParameter("@email", email) };
            return DataProvider.Instance.ExecuteNonQuery(query, param) > 0;
        }

        public bool UpdateReader(string id, string name, string deptId, string type, int status, string email)
        {
            string query = "UPDATE Reader SET FullName = @name, DepartmentID = @dept, ReaderType = @type, Status = @status, Email = @email WHERE ReaderID = @id";
            SqlParameter[] param = { new SqlParameter("@name", name), new SqlParameter("@dept", deptId), new SqlParameter("@type", type), new SqlParameter("@status", status), new SqlParameter("@email", email), new SqlParameter("@id", id) };
            return DataProvider.Instance.ExecuteNonQuery(query, param) > 0;
        }

        public bool DeleteReader(string id) => DataProvider.Instance.ExecuteNonQuery("UPDATE Reader SET IsDeleted = 1 WHERE ReaderID = @id", new SqlParameter[] { new SqlParameter("@id", id) }) > 0;

        public DataTable GetReaderBasicInfo(string readerId)
        {
            string query = "SELECT FullName, Email FROM Reader WHERE ReaderID = @id";
            SqlParameter[] param = { new SqlParameter("@id", readerId) };
            return DataProvider.Instance.ExecuteQuery(query, param);
        }

        public bool UpdateAcademicDebt(string readerId, decimal amount)
        {
            string query = "UPDATE Reader SET AcademicDebt = ISNULL(AcademicDebt, 0) + @amount WHERE ReaderID = @id";
            SqlParameter[] param = { new SqlParameter("@amount", amount), new SqlParameter("@id", readerId) };
            return DataProvider.Instance.ExecuteNonQuery(query, param) > 0;
        }

        // Sửa lại hàm TuocQuyenVinhVien trong DocGiaDAO.cs
        // FILE: DAO/DocGiaDAO.cs
        public void TuocQuyenVinhVien(string readerID, decimal soTienPhat)
        {
            string query = "UPDATE Reader SET IsPermanentlyBanned = 1, Status = 0, AcademicDebt = @tien WHERE ReaderID = @id";
            SqlParameter[] param = {
        new SqlParameter("@tien", soTienPhat),
        new SqlParameter("@id", readerID)
    };
            DataProvider.Instance.ExecuteNonQuery(query, param);
        }
        // Khóa tạm thời (Chỉ chỉnh Status = 0)
        public void KhoaTheTamThoi(string readerID, decimal soTienPhat)
        {
            string query = "UPDATE Reader SET Status = 0, AcademicDebt = @tien WHERE ReaderID = @id";
            SqlParameter[] param = {
        new SqlParameter("@tien", soTienPhat),
        new SqlParameter("@id", readerID)
    };
            DataProvider.Instance.ExecuteNonQuery(query, param);
        }
        // Dùng cho Sync
        public int SyncSystem(string defaultPasswordHash)
        {
            string queryGetReaders = "SELECT r.ReaderID, r.FullName FROM Reader r LEFT JOIN [User] u ON r.ReaderID = u.Username WHERE r.IsDeleted = 0 AND u.UserID IS NULL";
            DataTable dtReaders = DataProvider.Instance.ExecuteQuery(queryGetReaders);
            int createdCount = 0;
            string queryInsertUser = "INSERT INTO [User] (Username, PasswordHash, FullName, RoleID, Status, CreatedAt, IsDeleted) VALUES (@user, @pass, @name, 'Reader', 1, GETDATE(), 0)";
            foreach (DataRow row in dtReaders.Rows)
            {
                try { DataProvider.Instance.ExecuteNonQuery(queryInsertUser, new SqlParameter[] { new SqlParameter("@user", row["ReaderID"].ToString()), new SqlParameter("@pass", defaultPasswordHash), new SqlParameter("@name", row["FullName"].ToString()) }); createdCount++; } catch { }
            }
            return createdCount;
        }

    }
}