using System.Data;
using Lib_Equipment.DAO;

namespace Lib_Equipment.BLL
{
    public class ThongKeThuVienBLL
    {
        private static ThongKeThuVienBLL instance;
        public static ThongKeThuVienBLL Instance
        {
            get { if (instance == null) instance = new ThongKeThuVienBLL(); return instance; }
            private set { instance = value; }
        }
        private ThongKeThuVienBLL() { }

        public decimal TinhTongTienPhatDuKien()
        {
            // Nếu sau này có logic giảm giá, miễn phạt cho đối tượng đặc biệt thì viết code if/else ở ĐÂY
            return ThongKeThuVienDAO.Instance.GetTongTienPhat();
        }

        public DataTable LayDuLieuTopSach()
        {
            return ThongKeThuVienDAO.Instance.GetTopSach();
        }

        public DataTable LayDuLieuTrangThaiKho()
        {
            return ThongKeThuVienDAO.Instance.GetTrangThaiKho();
        }

        public DataTable LayDuLieuDanhSachDen()
        {
            return ThongKeThuVienDAO.Instance.GetDanhSachDen();
        }
    }
}