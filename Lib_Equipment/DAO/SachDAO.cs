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
            // [ĐÃ SỬA]: Lấy thêm cột b.CabinetLocation để hiển thị lên bảng cho Thủ thư xem
            string query = @"
                SELECT b.BookID, b.Title, b.Author, b.Publisher, b.PublishYear, c.CategoryName, b.CategoryID, 
                       b.Price, b.BookType, b.PageCount, b.CabinetLocation,
                       (SELECT COUNT(*) FROM BookCopy bc WHERE bc.BookID = b.BookID AND (bc.IsDeleted = 0 OR bc.IsDeleted IS NULL) AND bc.Status != N'Mất') AS [Số lượng]
                FROM Book b
                JOIN BookCategory c ON b.CategoryID = c.CategoryID
                WHERE b.IsDeleted = 0 OR b.IsDeleted IS NULL";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // [ĐÃ SỬA]: Thêm tham số 'string viTri' vào hàm
        public bool AddBook(string id, string title, string author, string publisher, object year, string categoryId, decimal price, string bookType, int pageCount, string viTri)
        {
            string query = @"INSERT INTO Book (BookID, Title, Author, Publisher, PublishYear, CategoryID, Price, BookType, PageCount, CabinetLocation, IsDeleted) 
                     VALUES (@id, @title, @author, @pub, @year, @cat, @price, @bType, @pageCount, @loc, 0)";
            SqlParameter[] param = {
        new SqlParameter("@id", id), new SqlParameter("@title", title),
        new SqlParameter("@author", author), new SqlParameter("@pub", publisher),
        new SqlParameter("@year", year), new SqlParameter("@cat", categoryId),
        new SqlParameter("@price", price), new SqlParameter("@bType", bookType),
        new SqlParameter("@pageCount", pageCount), new SqlParameter("@loc", viTri)
    };
            return DataProvider.Instance.ExecuteNonQuery(query, param) > 0;
        }

        public void SyncLocations()
        {
            // Lấy danh sách đầu sách kèm số lượng bản sao
            string query = @"SELECT b.BookID, b.CabinetLocation, ISNULL(sub.CountCopy, 1) as Qty 
                     FROM Book b LEFT JOIN BookCategory c ON b.CategoryID = c.CategoryID
                     LEFT JOIN (SELECT BookID, COUNT(*) as CountCopy FROM BookCopy WHERE IsDeleted = 0 OR IsDeleted IS NULL GROUP BY BookID) sub ON b.BookID = sub.BookID
                     WHERE b.IsDeleted = 0 ORDER BY c.CategoryName ASC, b.Title ASC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            string[] Cabinets = { "A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4" };

            int currentCabinetIdx = 0;
            int currentCopiesInCabinet = 0;
            int bookSttInCabinet = 1;

            foreach (DataRow row in dt.Rows)
            {
                int bookQty = Convert.ToInt32(row["Qty"]);

                // Nếu thêm cụm bản sao này vào mà vượt quá 150 cuốn của tủ hiện tại -> Sang tủ mới
                if (currentCopiesInCabinet + bookQty > 150)
                {
                    currentCabinetIdx++;
                    currentCopiesInCabinet = 0;
                    bookSttInCabinet = 1; // Reset STT đầu sách về 001 cho tủ mới
                }

                string newLoc = $"{Cabinets[currentCabinetIdx]}-{bookSttInCabinet:D3}";

                if (row["CabinetLocation"].ToString() != newLoc)
                {
                    DataProvider.Instance.ExecuteNonQuery("UPDATE Book SET CabinetLocation = @loc WHERE BookID = @id",
                        new SqlParameter[] { new SqlParameter("@loc", newLoc), new SqlParameter("@id", row["BookID"]) });
                }

                // Tích lũy số lượng bản sao để theo dõi dung lượng tủ
                currentCopiesInCabinet += bookQty;
                // Tăng STT cho đầu sách tiếp theo
                bookSttInCabinet++;
            }
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

        // Cập nhật hàm DeleteBook trong SachDAO.cs
        public bool DeleteBook(string id)
        {
            // Sử dụng Transaction để đảm bảo xóa sạch cả 2 bảng hoặc không xóa gì cả (an toàn dữ liệu)
            using (SqlConnection conn = new SqlConnection(DataProvider.Instance.ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 1. Xóa các bản sao (BookCopy) liên quan đến đầu sách này
                    string queryCopy = "DELETE FROM BookCopy WHERE BookID = @id";
                    SqlCommand cmdCopy = new SqlCommand(queryCopy, conn, trans);
                    cmdCopy.Parameters.AddWithValue("@id", id);
                    cmdCopy.ExecuteNonQuery();

                    // 2. Xóa vĩnh viễn đầu sách (Book)
                    string queryBook = "DELETE FROM Book WHERE BookID = @id";
                    SqlCommand cmdBook = new SqlCommand(queryBook, conn, trans);
                    cmdBook.Parameters.AddWithValue("@id", id);
                    int result = cmdBook.ExecuteNonQuery();

                    trans.Commit(); // Xác nhận xóa vĩnh viễn
                    return result > 0;
                }
                catch (Exception ex)
                {
                    trans.Rollback(); // Nếu có lỗi (đang bị mượn chẳng hạn), hủy lệnh xóa
                    Console.WriteLine("Lỗi xóa vĩnh viễn: " + ex.Message);
                    return false;
                }
            }
        }
    }
}