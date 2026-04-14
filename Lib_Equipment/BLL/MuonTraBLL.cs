using Lib_Equipment.DAO;
using Lib_Equipment.DTO; 
using System;
using System.Data;

namespace Lib_Equipment.BLL
{
    public class MuonTraBLL
    {
        private static MuonTraBLL instance;
        public static MuonTraBLL Instance { get { if (instance == null) instance = new MuonTraBLL(); return instance; } private set { instance = value; } }
        private MuonTraBLL() { }

        public bool ValidateBorrow(DocGiaDTO docGia, out string message)
        {
            if (docGia == null) { message = "Không tìm thấy Độc giả này!"; return false; }
            if (docGia.Status != 1) { message = "Thẻ độc giả này đã bị khóa hệ thống!"; return false; }

            int overdueCount = MuonTraDAO.Instance.CountOverdueBooks(docGia.ReaderID);
            if (overdueCount > 0)
            {
                message = $"CẢNH BÁO: Độc giả đang có {overdueCount} cuốn sách QUÁ HẠN! Hệ thống tạm khóa quyền mượn mới.";
                return false;
            }

            int currentBorrowed = MuonTraDAO.Instance.CountBorrowedBooks(docGia.ReaderID);
            bool isVIP = docGia.ReaderType.Contains("Giảng viên");
            int maxLimit = isVIP ? 9 : 6;

            if (currentBorrowed >= maxLimit)
            {
                message = $"Vi phạm quy định: Đối tượng '{docGia.ReaderType}' chỉ được mượn tối đa {maxLimit} tài liệu. Hiện đang mượn {currentBorrowed} cuốn.";
                return false;
            }

            message = "Hợp lệ";
            return true;
        }
        public int TuDongKiemTraVaGuiMailLuuLuu()
        {
            int emailCount = 0;
            DataTable dt = MuonTraDAO.Instance.GetDanhSachCanGuiMailTuDong();

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    int recID = Convert.ToInt32(row["RecordID"]);
                    string cID = row["CopyID"].ToString();
                    string email = row["Email"].ToString();
                    string maDG = row["ReaderID"].ToString();
                    int soNgayTre = Convert.ToInt32(row["SoNgayTre"]);

                    // 1. Xử lý khóa thẻ dựa trên tầng kỷ luật
                    if (soNgayTre >= 31)
                        DocGiaDAO.Instance.TuocQuyenVinhVien(maDG, soNgayTre * 2000);
                    else if (soNgayTre >= 3)
                        DocGiaDAO.Instance.KhoaTheTamThoi(maDG, soNgayTre * 2000);

                    // 2. Gửi email và cập nhật trạng thái Warned
                    if (!string.IsNullOrEmpty(email))
                    {
                        string subject = soNgayTre >= 31 ? "[KHẨN BÁO] TƯỚC QUYỀN VĨNH VIỄN" : "[CẢNH BÁO] QUÁ HẠN SÁCH";
                        string body = $"Chào bạn, sách mã {cID} đã trễ {soNgayTre} ngày. Vui lòng thanh toán...";

                        if (Helpers.EmailHelper.SendEmail(email, subject, body))
                        {
                            string column = soNgayTre >= 31 ? "IsWarnedDay31" : "IsWarnedDay4";
                            MuonTraDAO.Instance.UpdateSentMailStatus(recID, cID, column);
                            emailCount++;
                        }
                    }
                }
                catch { continue; }
            }
            return emailCount;
        }
        public DateTime CalculateDueDate(string readerType, DateTime borrowDate)
        {
            bool isVIP = readerType.Contains("Giảng viên");
            int allowedDays = isVIP ? 45 : 30;
            return borrowDate.AddDays(allowedDays);
        }

        // ĐÂY LÀ HÀM TÍNH TIỀN PHẠT THẬT (KHÔNG GÁN CỨNG 50K NỮA)
        public decimal CalculateLateFine(DateTime dueDate)
        {
            int lateDays = (DateTime.Now.Date - dueDate.Date).Days;
            
            // Nếu trễ từ 3 ngày trở lên, nhân số ngày trễ với 2000đ
            if (lateDays >= 3) 
            {
                return lateDays * 2000;
            }
            
            // Trễ 1-2 ngày hoặc chưa tới hạn thì phạt = 0
            return 0;
        }
    }
}