using System;
using System.Data;
using System.Text;
using Lib_Equipment.Database;

namespace Lib_Equipment.DAO
{
    public class BanSaoDAO
    {
        private static BanSaoDAO instance;
        public static BanSaoDAO Instance { get { if (instance == null) instance = new BanSaoDAO(); return instance; } }
        private BanSaoDAO() { }

        public DataTable GetAllCopies()
        {
            string query = @"
                SELECT bc.CopyID AS [Mã Bản Sao], 
                       bc.BookID AS [Mã Sách Gốc], 
                       b.Title AS [Tên Sách], 
                       bc.Status AS [Trạng thái] 
                FROM BookCopy bc 
                JOIN Book b ON bc.BookID = b.BookID 
                WHERE bc.IsDeleted = 0 OR bc.IsDeleted IS NULL";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetBookList()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT BookID, Title FROM Book WHERE IsDeleted = 0 OR IsDeleted IS NULL");
        }

        public int GetCurrentCopyCount(string bookId)
        {
            string query = $"SELECT COUNT(*) FROM BookCopy WHERE BookID = '{bookId}'";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        // ĐÃ FIX: Dùng StringBuilder ghép chuỗi thay vì Connection/Transaction thủ công
        public bool AddCopiesBatch(string bookId, int startNumber, int quantity)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BEGIN TRAN;");
            sb.AppendLine("BEGIN TRY");

            for (int i = 0; i < quantity; i++)
            {
                string copyId = $"{bookId}-{(startNumber + i):D1}"; // Format D2 tạo số 01, 02...
                sb.AppendLine($"INSERT INTO BookCopy (CopyID, BookID, Status, CreatedAt, IsDeleted) VALUES ('{copyId}', '{bookId}', N'Có sẵn', GETDATE(), 0);");
            }

            sb.AppendLine("COMMIT TRAN;");
            sb.AppendLine("END TRY");
            sb.AppendLine("BEGIN CATCH ROLLBACK TRAN; THROW; END CATCH;");

            // Gửi cục SQL khổng lồ này xuống Database chạy 1 lần
            return DataProvider.Instance.ExecuteNonQuery(sb.ToString(), null) > 0;
        }

        public bool UpdateCopyStatus(string copyId, string status)
        {
            string query = $"UPDATE BookCopy SET Status = N'{status}' WHERE CopyID = '{copyId}'";
            return DataProvider.Instance.ExecuteNonQuery(query,null) > 0;
        }

        public bool DeleteCopy(string copyId)
        {
            string query = $"UPDATE BookCopy SET IsDeleted = 1 WHERE CopyID = '{copyId}'";
            return DataProvider.Instance.ExecuteNonQuery(query, null) > 0;
        }
    }
}