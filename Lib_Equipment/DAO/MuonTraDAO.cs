using Lib_Equipment.Database;
using Lib_Equipment.DTO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Lib_Equipment.DAO
{
    public class MuonTraDAO
    {
        private static MuonTraDAO instance;
        public static MuonTraDAO Instance
        {
            get { if (instance == null) instance = new MuonTraDAO(); return instance; }
            private set { instance = value; }
        }
        private MuonTraDAO() { }

        public DocGiaDTO GetReaderInfo(string readerId)
        {
            string query = "SELECT ReaderID, FullName, ReaderType, Status, ISNULL(AcademicDebt, 0) AS AcademicDebt FROM Reader WHERE ReaderID = @id AND (IsDeleted = 0 OR IsDeleted IS NULL)";
            SqlParameter[] param = { new SqlParameter("@id", readerId) };
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, param);

            if (dt.Rows.Count > 0)
            {
                return new DocGiaDTO
                {
                    ReaderID = dt.Rows[0]["ReaderID"].ToString(),
                    FullName = dt.Rows[0]["FullName"].ToString(),
                    ReaderType = dt.Rows[0]["ReaderType"].ToString(),
                    Status = Convert.ToInt32(dt.Rows[0]["Status"])
                };
            }
            return null;
        }

        public int CountBorrowedBooks(string readerId)
        {
            string query = "SELECT COUNT(*) FROM BorrowRecord br JOIN BorrowDetail bd ON br.RecordID = bd.RecordID WHERE br.ReaderID = @id AND bd.ReturnDate IS NULL";
            SqlParameter[] param = { new SqlParameter("@id", readerId) };
            object res = DataProvider.Instance.ExecuteScalar(query, param);
            return res != null ? Convert.ToInt32(res) : 0;
        }

        public int CountOverdueBooks(string readerId)
        {
            string query = "SELECT COUNT(*) FROM BorrowRecord br JOIN BorrowDetail bd ON br.RecordID = bd.RecordID WHERE br.ReaderID = @id AND bd.ReturnDate IS NULL AND br.DueDate < CAST(GETDATE() AS DATE)";
            SqlParameter[] param = { new SqlParameter("@id", readerId) };
            object res = DataProvider.Instance.ExecuteScalar(query, param);
            return res != null ? Convert.ToInt32(res) : 0;
        }

        // CHỐT HẠ: Logic Mượn sách dùng StringBuilder + Transaction + Fix lỗi 'admin'
        public int ExecuteBorrow(string readerId, string copyId, DateTime dueDate, string createdBy)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BEGIN TRY");
            sb.AppendLine("    BEGIN TRAN;");

            // ĐÃ SỬA: Lưu trực tiếp tên người dùng (chuỗi) vào cột CreatedBy thay vì lấy ID số
            sb.AppendLine(@"
        INSERT INTO BorrowRecord (ReaderID, CreatedBy, BorrowDate, DueDate, Status, IsDeleted) 
        VALUES (@readerId, @user, GETDATE(), @dueDate, N'Đang mượn', 0);");

            sb.AppendLine("    DECLARE @newRecordId INT = SCOPE_IDENTITY();");

            sb.AppendLine(@"
        INSERT INTO BorrowDetail (RecordID, CopyID, FineAmount, IsWarnedDay4, IsWarnedDay31) 
        VALUES (@newRecordId, @copyId, 0, 0, 0);");

            sb.AppendLine("    UPDATE BookCopy SET Status = N'Đang mượn' WHERE CopyID = @copyId;");

            // Trả về mã phiếu vừa được tạo
            sb.AppendLine("    SELECT @newRecordId;");

            sb.AppendLine("    COMMIT TRAN;");
            sb.AppendLine("END TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK TRAN; THROW; END CATCH");

            SqlParameter[] param = {
        new SqlParameter("@readerId", readerId),
        new SqlParameter("@copyId", copyId),
        new SqlParameter("@dueDate", dueDate),
        new SqlParameter("@user", createdBy) // Biến createdBy truyền thẳng vào luôn
    };

            try
            {
                // Chạy ExecuteScalar để lấy được ID của phiếu mượn (để sau này in phiếu nếu cần)
                object result = DataProvider.Instance.ExecuteScalar(sb.ToString(), param);
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch
            {
                throw; // Ném lỗi ra ngoài cho Form xử lý hiển thị thông báo
            }
        }

        public bool ExecuteReturn(int recordId, string copyId, string condition, decimal fineAmount, string newStatus)
        {
            // Câu lệnh SQL bao gồm cập nhật Trả sách, Trạng thái sách và Trạng thái Độc giả
            string sqlReturn = @"
        BEGIN TRAN;
        BEGIN TRY
            -- 1. Cập nhật thông tin trả sách vào chi tiết phiếu mượn
            UPDATE BorrowDetail 
            SET ReturnDate = GETDATE(), ReturnCondition = @cond, FineAmount = @fine 
            WHERE RecordID = @rec AND CopyID = @copy;

            -- 2. Cập nhật trạng thái mới cho bản sao sách (thường là N'Có sẵn')
            UPDATE BookCopy SET Status = @status WHERE CopyID = @copy;

            -- 3. XỬ LÝ ĐỘC GIẢ: 
            -- CHỈ mở khóa thẻ (nếu ko bị cấm vĩnh viễn), TUYỆT ĐỐI KHÔNG reset nợ về 0 ở đây nữa.
            UPDATE Reader
            SET Status = CASE 
                            WHEN IsPermanentlyBanned = 0 THEN 1 
                            ELSE 0 
                         END
            WHERE ReaderID = (SELECT TOP 1 ReaderID FROM BorrowRecord WHERE RecordID = @rec);

            COMMIT TRAN;
        END TRY
        BEGIN CATCH 
            IF @@TRANCOUNT > 0 ROLLBACK TRAN; 
            THROW; 
        END CATCH;";

            SqlParameter[] param = {
                new SqlParameter("@rec", recordId),
                new SqlParameter("@copy", copyId),
                new SqlParameter("@cond", condition),
                new SqlParameter("@fine", fineAmount),
                new SqlParameter("@status", newStatus)
            };

            return DataProvider.Instance.ExecuteNonQuery(sqlReturn, param) > 0;
        }

        public DataTable GetSachDangMuon(string readerID)
        {
            string query = @"
                SELECT br.RecordID AS [Mã Phiếu], bd.CopyID AS [Mã cuốn sách], b.Title AS [Tên sách], br.BorrowDate AS [Ngày mượn], br.DueDate AS [Hạn trả],
                CASE WHEN DATEDIFF(day, br.DueDate, GETDATE()) > 0 THEN DATEDIFF(day, br.DueDate, GETDATE()) ELSE 0 END AS [Số ngày trễ],
                CASE WHEN DATEDIFF(day, br.DueDate, GETDATE()) >= 3 THEN DATEDIFF(day, br.DueDate, GETDATE()) * 2000 ELSE 0 END AS [Phạt dự kiến]
                FROM BorrowDetail bd JOIN BorrowRecord br ON bd.RecordID = br.RecordID
                JOIN BookCopy bc ON bd.CopyID = bc.CopyID JOIN Book b ON bc.BookID = b.BookID
                WHERE br.ReaderID = @id AND bd.ReturnDate IS NULL";
            SqlParameter[] param = { new SqlParameter("@id", readerID) };
            return DataProvider.Instance.ExecuteQuery(query, param);
        }

        public DataTable GetDanhSachCanGuiMailTuDong()
        {
            // Tìm những người trễ ĐÚNG 3 ngày (để khóa tạm) hoặc ĐÚNG 31 ngày (để khóa VV)
            string query = @"SELECT bd.RecordID, bd.CopyID, r.ReaderID, r.FullName, r.Email, 
                       DATEDIFF(day, br.DueDate, GETDATE()) AS SoNgayTre, bd.IsWarnedDay4, bd.IsWarnedDay31
                FROM BorrowDetail bd 
                JOIN BorrowRecord br ON bd.RecordID = br.RecordID 
                JOIN Reader r ON br.ReaderID = r.ReaderID
                WHERE bd.ReturnDate IS NULL 
                AND (
                    (DATEDIFF(day, br.DueDate, GETDATE()) = 3 AND bd.IsWarnedDay4 = 0) 
                    OR 
                    (DATEDIFF(day, br.DueDate, GETDATE()) = 31 AND bd.IsWarnedDay31 = 0)
                )";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // 2. Sửa hàm cập nhật trạng thái đã gửi mail
        public void UpdateSentMailStatus(int recordID, string copyID, string column)
        {
            // Xác định dòng cần update dựa vào RecordID và CopyID
            string query = $"UPDATE BorrowDetail SET {column} = 1 WHERE RecordID = @recID AND CopyID = @copyID";
            SqlParameter[] param = {
        new SqlParameter("@recID", recordID),
        new SqlParameter("@copyID", copyID)
    };
            DataProvider.Instance.ExecuteNonQuery(query, param);
        }
    }
}