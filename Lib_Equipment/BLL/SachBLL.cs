using System;
using System.Data;
using Lib_Equipment.DAO;

namespace Lib_Equipment.BLL
{
    public class SachBLL
    {
        private static SachBLL instance;
        public static SachBLL Instance { get { if (instance == null) instance = new SachBLL(); return instance; } }
        private SachBLL() { }
        public void DongBoViTri() => SachDAO.Instance.SyncLocations();
        public DataTable LayDanhSachTheLoai() => SachDAO.Instance.GetCategories();
        public DataTable LayDanhSachSach() => SachDAO.Instance.GetAllBooks();

        // [ĐÃ SỬA]: Thêm tham số 'string viTri' vào chữ ký hàm
        public bool ThemSach(string id, string title, string author, string publisher, string yearStr, string categoryId, string priceStr, string bookType, string pageStr, string viTri, out string msg)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(title))
            {
                msg = "Vui lòng nhập Mã đầu sách và Tên sách!"; return false;
            }

            int year = 0; int.TryParse(yearStr, out year);
            object yearObj = year == 0 ? (object)DBNull.Value : year;

            decimal price = 0; decimal.TryParse(priceStr, out price);
            int pageCount = 0; int.TryParse(pageStr, out pageCount);

            try
            {
                if (SachDAO.Instance.AddBook(id, title, author, publisher, yearObj, categoryId, price, bookType, pageCount, viTri))
                {
                    msg = "Thêm thành công!"; return true;
                }
                msg = "Thất bại!"; return false;
            }

            catch
            {
                msg = "Lỗi: Mã sách đã tồn tại!"; return false;
            }
        }
        public bool SuaSach(string id, string title, string author, string publisher, string yearStr, string categoryId, string priceStr, string bookType, string pageStr)
        {
            if (string.IsNullOrEmpty(id)) return false;

            int year = 0; int.TryParse(yearStr, out year);
            object yearObj = year == 0 ? (object)DBNull.Value : year;

            decimal price = 0; decimal.TryParse(priceStr, out price);
            int pageCount = 0; int.TryParse(pageStr, out pageCount);

            return SachDAO.Instance.UpdateBook(id, title, author, publisher, yearObj, categoryId, price, bookType, pageCount);
        }

        public DataTable LayDanhSachInPhieu(string bookId)
        {
            if (string.IsNullOrEmpty(bookId)) return null;
            return SachDAO.Instance.GetBookCopiesForExport(bookId);
        }

        // Trong SachBLL.cs
        public bool XoaSach(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;

            // Gọi lệnh xóa vĩnh viễn từ DAO
            return SachDAO.Instance.DeleteBook(id);
        }
    }
}