using Lib_Equipment.DAO;
using System.Data;

namespace Lib_Equipment.BLL
{
    public class BanSaoBLL
    {
        private static BanSaoBLL instance;
        public static BanSaoBLL Instance { get { if (instance == null) instance = new BanSaoBLL(); return instance; } }
        private BanSaoBLL() { }

        public DataTable LayTatCaBanSao() => BanSaoDAO.Instance.GetAllCopies();
        public DataTable LayDanhSachDauSach() => BanSaoDAO.Instance.GetBookList();

        // Nghiệp vụ sinh mã hàng loạt
        public bool SinhBanSaoHangLoat(string bookId, string soLuongStr, out string msg)
        {
            if (string.IsNullOrEmpty(bookId))
            {
                msg = "Vui lòng chọn một Đầu sách!";
                return false;
            }

            if (!int.TryParse(soLuongStr, out int quantity) || quantity <= 0)
            {
                msg = "Số lượng nhập kho phải là một số nguyên dương (> 0)!";
                return false;
            }

            int startNumber = BanSaoDAO.Instance.GetCurrentCopyCount(bookId) + 1;

            if (BanSaoDAO.Instance.AddCopiesBatch(bookId, startNumber, quantity))
            {
                msg = $"Đã nhập kho thành công {quantity} cuốn sách.\nMã vạch sinh ra từ: {bookId}-{startNumber:D2} đến {bookId}-{(startNumber + quantity - 1):D2}";
                return true;
            }

            msg = "Lỗi hệ thống hoặc mã vạch đã tồn tại!";
            return false;
        }

        public bool CapNhatTrangThai(string copyId, string status)
        {
            if (string.IsNullOrEmpty(copyId) || string.IsNullOrEmpty(status)) return false;
            return BanSaoDAO.Instance.UpdateCopyStatus(copyId, status);
        }

        public bool XoaBanSao(string copyId)
        {
            if (string.IsNullOrEmpty(copyId)) return false;
            return BanSaoDAO.Instance.DeleteCopy(copyId);
        }
    }
}