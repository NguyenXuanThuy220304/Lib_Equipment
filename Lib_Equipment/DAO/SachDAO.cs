using System;
using System.Data;
using System.Data.SqlClient;
using Lib_Equipment.Database;

namespace Lib_Equipment.DAO
{
    public class SachDAO
    {
        private static SachDAO instance;
        public static SachDAO Instance { get { if (instance == null) instance = new SachDAO(); return instance; } }
        private SachDAO() { }

        public DataTable GetCategories() => DataProvider.Instance.ExecuteQuery("SELECT CategoryID, CategoryName FROM BookCategory WHERE IsDeleted = 0");

        public DataTable GetAllBooks()
        {
            string query = @"
                SELECT b.BookID, b.Title, b.Author, b.Publisher, b.PublishYear, c.CategoryName, b.CategoryID, 
                       b.Price, b.BookType, b.PageCount,
                       (SELECT COUNT(*) FROM BookCopy bc WHERE bc.BookID = b.BookID AND (bc.IsDeleted = 0 OR bc.IsDeleted IS NULL) AND bc.Status != N'Mất') AS [Số lượng]
                FROM Book b
                JOIN BookCategory c ON b.CategoryID = c.CategoryID
                WHERE b.IsDeleted = 0 OR b.IsDeleted IS NULL";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool AddBook(string id, string title, string author, string publisher, object year, string categoryId, decimal price, string bookType, int pageCount)
        {
            string query = @"INSERT INTO Book (BookID, Title, Author, Publisher, PublishYear, CategoryID, Price, BookType, PageCount, IsDeleted) 
                             VALUES (@id, @title, @author, @pub, @year, @cat, @price, @bType, @pageCount, 0)";
            SqlParameter[] param = {
                new SqlParameter("@id", id), new SqlParameter("@title", title),
                new SqlParameter("@author", author), new SqlParameter("@pub", publisher),
                new SqlParameter("@year", year), new SqlParameter("@cat", categoryId),
                new SqlParameter("@price", price), new SqlParameter("@bType", bookType),
                new SqlParameter("@pageCount", pageCount)
            };
            return DataProvider.Instance.ExecuteNonQuery(query, param) > 0;
        }

        public bool UpdateBook(string id, string title, string author, string publisher, object year, string categoryId, decimal price, string bookType, int pageCount)
        {
            string query = @"UPDATE Book SET Title = @title, Author = @author, Publisher = @pub, PublishYear = @year, CategoryID = @cat, Price = @price, BookType = @bType, PageCount = @pageCount WHERE BookID = @id";
            SqlParameter[] param = {
                new SqlParameter("@title", title), new SqlParameter("@author", author),
                new SqlParameter("@pub", publisher), new SqlParameter("@year", year),
                new SqlParameter("@cat", categoryId), new SqlParameter("@price", price),
                new SqlParameter("@bType", bookType), new SqlParameter("@pageCount", pageCount),
                new SqlParameter("@id", id)
            };
            return DataProvider.Instance.ExecuteNonQuery(query, param) > 0;
        }

        public DataTable GetBookCopiesForExport(string bookId)
        {
            string query = @"
                SELECT b.CabinetLocation AS [Vị trí tủ], bc.CopyID AS [Mã cuốn sách], b.PublishYear AS [Năm xuất bản], bc.CopyID AS [Mã vạch]
                FROM BookCopy bc JOIN Book b ON bc.BookID = b.BookID
                WHERE bc.BookID = @bookId AND (bc.IsDeleted = 0 OR bc.IsDeleted IS NULL)
                ORDER BY bc.CopyID ASC";
            SqlParameter[] param = { new SqlParameter("@bookId", bookId) };
            return DataProvider.Instance.ExecuteQuery(query, param);
        }

        public bool DeleteBook(string id)
        {
            SqlParameter[] param = { new SqlParameter("@id", id) };
            return DataProvider.Instance.ExecuteNonQuery("UPDATE Book SET IsDeleted = 1 WHERE BookID = @id", param) > 0;
        }
    }
}