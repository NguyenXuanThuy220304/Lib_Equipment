using System;
using System.Data;
using Lib_Equipment.Database; // Chứa DataProvider của bạn

namespace Lib_Equipment.DAO
{
    public class ThongKeThuVienDAO
    {
        // Áp dụng Singleton Pattern để tối ưu bộ nhớ
        private static ThongKeThuVienDAO instance;
        public static ThongKeThuVienDAO Instance
        {
            get { if (instance == null) instance = new ThongKeThuVienDAO(); return instance; }
            private set { instance = value; }
        }
        private ThongKeThuVienDAO() { }

        // 1. Lấy tổng tiền phạt (Quy định 2024: Phạt 2000đ/ngày nếu quá hạn >= 3 ngày)
        public decimal GetTongTienPhat()
        {
            string query = @"
                SELECT SUM(DATEDIFF(day, br.DueDate, GETDATE()) * 2000) 
                FROM BorrowRecord br
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID
                WHERE bd.ReturnDate IS NULL 
                  AND DATEDIFF(day, br.DueDate, GETDATE()) >= 3 
                  AND br.IsDeleted = 0";

            object result = DataProvider.Instance.ExecuteScalar(query);
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        // 2. Lấy Top 10 sách mượn nhiều
        public DataTable GetTopSach()
        {
            string query = @"
                SELECT TOP 10 b.Title AS [TenSach], COUNT(bd.CopyID) AS [LuotMuon]
                FROM BorrowDetail bd
                JOIN BookCopy bc ON bd.CopyID = bc.CopyID
                JOIN Book b ON bc.BookID = b.BookID
                GROUP BY b.Title
                ORDER BY [LuotMuon] DESC";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // 3. Lấy Trạng thái kho sách
        public DataTable GetTrangThaiKho()
        {
            string query = "SELECT Status AS [TrangThai], COUNT(CopyID) AS [SoLuong] FROM BookCopy WHERE IsDeleted = 0 GROUP BY Status";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // 4. Lấy Danh sách đen
        public DataTable GetDanhSachDen()
        {
            string query = @"
                SELECT 
                    r.ReaderID AS [Mã Độc giả],
                    r.FullName AS [Họ và tên],
                    COUNT(bd.CopyID) AS [Số sách quá hạn],
                    SUM(DATEDIFF(day, br.DueDate, GETDATE()) * 2000) AS [Tiền phạt dự kiến (VNĐ)]
                FROM BorrowRecord br
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID
                JOIN Reader r ON br.ReaderID = r.ReaderID
                WHERE bd.ReturnDate IS NULL AND DATEDIFF(day, br.DueDate, GETDATE()) >= 3
                GROUP BY r.ReaderID, r.FullName
                ORDER BY [Tiền phạt dự kiến (VNĐ)] DESC";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}