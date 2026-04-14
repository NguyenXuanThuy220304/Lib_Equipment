using System;
using System.Data;
using System.Data.SqlClient;
using Lib_Equipment.Database;

namespace Lib_Equipment.DAO
{
    public class TaiKhoanDAO
    {
        private static TaiKhoanDAO instance;
        public static TaiKhoanDAO Instance
        {
            get { if (instance == null) instance = new TaiKhoanDAO(); return instance; }
            private set { instance = value; }
        }
        private TaiKhoanDAO() { }

        public DataTable GetRoles() => DataProvider.Instance.ExecuteQuery("SELECT RoleID, RoleName FROM Role");

        public DataTable GetAllAccounts()
        {
            string query = @"SELECT u.UserID, u.Username, u.FullName, u.RoleID, r.RoleName, CASE WHEN u.Status = 1 THEN N'Hoạt động' ELSE N'Bị khóa' END AS TrangThai
                             FROM [User] u JOIN Role r ON u.RoleID = r.RoleID WHERE u.IsDeleted = 0";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool AddAccount(string user, string passHash, string name, string role, int status)
        {
            string query = "INSERT INTO [User] (Username, PasswordHash, FullName, RoleID, Status) VALUES (@user, @pass, @name, @role, @status)";
            SqlParameter[] param = { new SqlParameter("@user", user), new SqlParameter("@pass", passHash), new SqlParameter("@name", name), new SqlParameter("@role", role), new SqlParameter("@status", status) };
            return DataProvider.Instance.ExecuteNonQuery(query, param) > 0;
        }

        public bool UpdateAccount(int userId, string name, string role, int status, string newPassHash = null)
        {
            string query = "UPDATE [User] SET FullName = @name, RoleID = @role, Status = @status";
            if (!string.IsNullOrEmpty(newPassHash)) query += ", PasswordHash = @pass";
            query += " WHERE UserID = @id";

            SqlParameter[] param = { new SqlParameter("@name", name), new SqlParameter("@role", role), new SqlParameter("@status", status), new SqlParameter("@id", userId) };
            if (!string.IsNullOrEmpty(newPassHash))
            {
                Array.Resize(ref param, param.Length + 1);
                param[param.Length - 1] = new SqlParameter("@pass", newPassHash);
            }
            return DataProvider.Instance.ExecuteNonQuery(query, param) > 0;
        }

        public bool DeleteAccount(int userId)
        {
            return DataProvider.Instance.ExecuteNonQuery("UPDATE [User] SET IsDeleted = 1 WHERE UserID = @id", new SqlParameter[] { new SqlParameter("@id", userId) }) > 0;
        }
    }
}