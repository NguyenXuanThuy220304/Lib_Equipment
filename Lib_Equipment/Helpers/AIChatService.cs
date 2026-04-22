using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq;
using Lib_Equipment.Database;

namespace Lib_Equipment.BLL
{
    public class AIChatService
    {
        private static readonly HttpClient _http = new HttpClient();
        private static string API_KEY = AppSecrets.GeminiApiKey;
        private static readonly string API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent";

        // ─────────────────────────────────────────
        // NỘI QUY & HƯỚNG DẪN SỬ DỤNG (tĩnh)
        // ─────────────────────────────────────────
        private const string NOI_QUY = @"
=== NỘI QUY THƯ VIỆN UNETI ===
- Điều 1: Xuất trình Thẻ Sinh viên khi mượn trả sách/thiết bị. Không mượn hộ người khác.
- Điều 2: Thời hạn mượn sách tối đa: 14 ngày (Sinh viên) và 21 ngày (Giảng viên).
- Điều 3: Giữ gìn tài sản chung. Làm rách, mất sách hoặc hỏng thiết bị phải bồi thường 200% giá trị.
- Điều 4: Quá hạn trả sách/thiết bị sẽ bị phạt tiền tự động theo quy định của nhà trường (2.000đ/ngày).
- Điều 5: Sinh viên phải thanh toán hết công nợ mới được tiếp tục sử dụng các dịch vụ của hệ thống.
- Điều 6: Không được tự ý mang sách ra khỏi thư viện khi chưa làm thủ tục mượn.
==============================
";

        private const string HUONG_DAN = @"
=== HƯỚNG DẪN SỬ DỤNG HỆ THỐNG ===
1. Đăng nhập và tra cứu:
   - Sử dụng mã Sinh Viên / Giảng Viên do trường cấp.
   - Tại trang chủ, dùng thanh tìm kiếm để tra cứu sách theo tên, tác giả hoặc chuyên ngành.

2. Quy trình mượn sách thông minh:
   - Bấm nút 'Tự động mượn sách' (màu cam).
   - Đưa mã vạch của sách vào máy quét.
   - Hệ thống tự động đối soát và cập nhật vào lịch sử mượn.

3. Trả sách & Gia hạn:
   - Truy cập tab 'Lịch sử'.
   - Nhấn 'Gia hạn' nếu sách sắp hết hạn (áp dụng 1 lần, còn ít nhất 3 ngày).
   - Nhấn 'Trả sách' và bỏ sách vào Tủ trả tự động.

4. Trợ lý AI:
   - Mở tab 'AI Hỗ trợ' và đặt câu hỏi bằng tiếng Việt.
   - Hệ thống kết nối trực tiếp vào Database để trả lời chính xác nhất.
   - Ví dụ: 'Tôi đang mượn sách gì?', 'Sách nào sắp đến hạn?'
=====================================
";

        // ─────────────────────────────────────────
        // CHUẨN HÓA ROLE VỀ 1 CHUẨN DUY NHẤT
        // ─────────────────────────────────────────
        public static string NormalizeRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) return "Reader";
            switch (role.Trim().ToUpper())
            {
                case "ADMIN": return "ADMIN";
                case "LIBRARIAN": case "THUTHU": return "LIBRARIAN";
                case "ASSET_MANAGER": case "CANBOTHIETBI": return "ASSET_MANAGER";
                case "READER": case "DOCGIA": return "Reader";
                case "DIRECTOR": return "DIRECTOR";
                default: return "Reader";
            }
        }

        // ─────────────────────────────────────────
        // HÀM CHÍNH: Gửi câu hỏi → nhận trả lời (có retry)
        // ─────────────────────────────────────────
        public static async Task<string> AskAsync(string userMessage, string role, string readerID = null)
        {
            role = NormalizeRole(role);
            string dbContext = GetDbContextByRole(role, readerID);
            string fullPrompt = BuildSystemPrompt(role, dbContext)
                                + "\n\nCâu hỏi của người dùng: " + userMessage;

            var body = new
            {
                contents = new[]
                {
                    new {
                        role = "user",
                        parts = new[] { new { text = fullPrompt } }
                    }
                }
            };
            string bodyJson = JsonConvert.SerializeObject(body);

            int maxRetry = 3;
            for (int attempt = 0; attempt < maxRetry; attempt++)
            {
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, API_URL + "?key=" + API_KEY);
                    request.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                    var response = await _http.SendAsync(request);
                    string json = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(json);

                    if (parsed["error"] != null)
                    {
                        string errMsg = parsed["error"]["message"].ToString();
                        // Lỗi quá tải → chờ rồi thử lại
                        if (errMsg.Contains("high demand") || errMsg.Contains("overloaded") || errMsg.Contains("503"))
                        {
                            if (attempt < maxRetry - 1)
                            {
                                await Task.Delay(3000 * (attempt + 1)); // 3s, 6s, 9s
                                continue;
                            }
                            return "⚠️ AI đang bận, vui lòng thử lại sau ít phút.";
                        }
                        return "⚠️ Lỗi API: " + errMsg;
                    }

                    return parsed["candidates"][0]["content"]["parts"][0]["text"].ToString();
                }
                catch (Exception ex)
                {
                    if (attempt == maxRetry - 1)
                        return "⚠️ Không thể kết nối AI: " + ex.Message;
                    await Task.Delay(2000);
                }
            }
            return "⚠️ AI không phản hồi, vui lòng thử lại.";
        }

        // ─────────────────────────────────────────
        // XÂY DỰNG SYSTEM PROMPT THEO ROLE
        // ─────────────────────────────────────────
        private static string BuildSystemPrompt(string role, string dbContext)
        {
            string basePrompt = $@"
Bạn là trợ lý AI của Hệ thống Quản lý Thư viện và Thiết bị Trường ĐH Kinh tế - Kỹ thuật Công nghiệp (UNETI).
Trả lời bằng tiếng Việt, ngắn gọn, thân thiện, chính xác.
Chỉ trả lời dựa trên dữ liệu và nội quy được cung cấp bên dưới, không bịa đặt.

{NOI_QUY}
{HUONG_DAN}
=== DỮ LIỆU THỰC TẾ TỪ HỆ THỐNG ===
{dbContext}
=====================================
";
            switch (role)
            {
                case "LIBRARIAN":
                    return basePrompt + @"
Vai trò: THỦ THƯ
Bạn được phép hỗ trợ:
✅ Tra cứu sách, bản sao, tình trạng còn/hết
✅ Danh sách độc giả đang mượn, quá hạn, nợ phạt
✅ Thống kê lượt mượn, sách phổ biến
✅ Giải thích nội quy và hướng dẫn sử dụng
✅ Gợi ý nhập thêm sách
❌ KHÔNG trả lời về thiết bị";

                case "Reader":
                    return basePrompt + @"
Vai trò: ĐỘC GIẢ
Bạn được phép hỗ trợ:
✅ Sách đang mượn, ngày hết hạn, tiền phạt của chính độc giả này
✅ Tra cứu sách còn trong thư viện
✅ Giải thích nội quy thư viện và hướng dẫn sử dụng hệ thống
✅ Gợi ý sách theo thể loại
❌ KHÔNG tiết lộ thông tin độc giả khác
❌ KHÔNG trả lời về thiết bị hay quản trị";

                case "ASSET_MANAGER":
                    return basePrompt + @"
Vai trò: CÁN BỘ QUẢN LÝ THIẾT BỊ
Bạn được phép hỗ trợ:
✅ Tra cứu tình trạng thiết bị, phòng ban
✅ Lịch bảo trì sắp đến, thiết bị cần sửa chữa
✅ Thống kê thiết bị hỏng, đang mượn
✅ Giải thích nội quy liên quan thiết bị
❌ KHÔNG trả lời về sách hay thư viện";

                case "DIRECTOR":
                    return basePrompt + @"
Vai trò: BAN GIÁM HIỆU
Bạn được phép hỗ trợ:
✅ Xem báo cáo tổng hợp thư viện & thiết bị
✅ Thống kê, xu hướng, cảnh báo bất thường
✅ Tóm tắt nội quy và tình hình chấp hành
❌ Không chỉnh sửa dữ liệu";

                case "ADMIN":
                    return basePrompt + @"
Vai trò: QUẢN TRỊ VIÊN
Bạn có quyền truy cập TOÀN BỘ hệ thống:
✅ Thống kê thư viện + thiết bị
✅ Phân tích báo cáo tổng hợp
✅ Cảnh báo bất thường, xu hướng
✅ Giải thích nội quy và hướng dẫn cho mọi đối tượng";

                default:
                    return basePrompt + "Chỉ trả lời câu hỏi chung về hệ thống và nội quy thư viện.";
            }
        }

        // ─────────────────────────────────────────
        // LẤY DỮ LIỆU THỰC TỪ SQL THEO ROLE
        // ─────────────────────────────────────────
        public static string GetDbContextByRole(string role, string readerID = null)
        {
            var sb = new StringBuilder();
            try
            {
                if (role == "LIBRARIAN" || role == "ADMIN" || role == "DIRECTOR")
                {
                    try
                    {
                        var dt = DataProvider.Instance.ExecuteQuery(@"
                            SELECT
                                (SELECT COUNT(*) FROM Book WHERE IsDeleted=0) AS TongSach,
                                (SELECT COUNT(*) FROM BookCopy WHERE IsDeleted=0) AS TongBanSao,
                                (SELECT COUNT(*) FROM BorrowDetail WHERE ReturnDate IS NULL) AS DangMuon,
                                (SELECT COUNT(*) FROM Reader WHERE IsDeleted=0) AS TongDocGia,
                                (SELECT COUNT(*) FROM Reader WHERE AcademicDebt > 0) AS DangNoQuan
                        ");
                        if (dt.Rows.Count > 0)
                        {
                            var r = dt.Rows[0];
                            sb.AppendLine("[THƯ VIỆN]");
                            sb.AppendLine($"- Tổng đầu sách: {r["TongSach"]}");
                            sb.AppendLine($"- Tổng bản sao: {r["TongBanSao"]}");
                            sb.AppendLine($"- Đang được mượn: {r["DangMuon"]}");
                            sb.AppendLine($"- Tổng độc giả: {r["TongDocGia"]}");
                            sb.AppendLine($"- Đang nợ phạt: {r["DangNoQuan"]}");
                        }
                    }
                    catch (Exception ex) { sb.AppendLine("⚠️ Lỗi thống kê thư viện: " + ex.Message); }

                    try
                    {
                        var dtTop = DataProvider.Instance.ExecuteQuery(@"
                            SELECT TOP 5 b.Title, COUNT(*) AS LuotMuon
                            FROM BorrowDetail bd
                            JOIN BookCopy bc ON bd.CopyID = bc.CopyID
                            JOIN Book b ON bc.BookID = b.BookID
                            GROUP BY b.Title ORDER BY LuotMuon DESC
                        ");
                        sb.Append("- Top sách mượn nhiều: ");
                        sb.AppendLine(string.Join(", ", dtTop.AsEnumerable()
                            .Select(r => $"{r["Title"]} ({r["LuotMuon"]} lượt)")));
                    }
                    catch (Exception ex) { sb.AppendLine("⚠️ Lỗi top sách: " + ex.Message); }

                    try
                    {
                        var dtQuaHan = DataProvider.Instance.ExecuteQuery(@"
                            SELECT COUNT(*) AS SoLuong FROM BorrowRecord
                            WHERE DueDate < GETDATE() AND ReturnDate IS NULL AND IsDeleted = 0
                        ");
                        sb.AppendLine($"- Phiếu mượn quá hạn: {dtQuaHan.Rows[0]["SoLuong"]}");
                    }
                    catch (Exception ex) { sb.AppendLine("⚠️ Lỗi quá hạn: " + ex.Message); }
                }

                if (role == "Reader" && !string.IsNullOrEmpty(readerID))
                {
                    try
                    {
                        var dtInfo = DataProvider.Instance.ExecuteQuery(
                            "SELECT FullName, AcademicDebt, Status FROM Reader WHERE ReaderID = @id",
                            new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@id", readerID)
                            });
                        if (dtInfo.Rows.Count > 0)
                        {
                            sb.AppendLine($"[ĐỘC GIẢ: {dtInfo.Rows[0]["FullName"]}]");
                            sb.AppendLine($"- Nợ phạt: {dtInfo.Rows[0]["AcademicDebt"]:N0} đ");
                            sb.AppendLine($"- Trạng thái: {(dtInfo.Rows[0]["Status"].ToString() == "1" ? "Hoạt động" : "Bị khóa")}");
                        }
                    }
                    catch (Exception ex) { sb.AppendLine("⚠️ Lỗi thông tin độc giả: " + ex.Message); }

                    try
                    {
                        var dtMuon = DataProvider.Instance.ExecuteQuery(@"
                            SELECT b.Title, br.DueDate,
                                   DATEDIFF(day, GETDATE(), br.DueDate) AS ConLai
                            FROM BorrowRecord br
                            JOIN BorrowDetail bd ON br.RecordID = bd.RecordID
                            JOIN BookCopy bc ON bd.CopyID = bc.CopyID
                            JOIN Book b ON bc.BookID = b.BookID
                            WHERE br.ReaderID = @id AND bd.ReturnDate IS NULL",
                            new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@id", readerID)
                            });
                        sb.AppendLine($"- Đang mượn {dtMuon.Rows.Count} cuốn:");
                        foreach (DataRow row in dtMuon.Rows)
                        {
                            int conLai = Convert.ToInt32(row["ConLai"]);
                            string trangThai = conLai < 0
                                ? $"⚠️ QUÁ HẠN {Math.Abs(conLai)} ngày"
                                : $"còn {conLai} ngày";
                            sb.AppendLine($"  + {row["Title"]} | hạn: {Convert.ToDateTime(row["DueDate"]):dd/MM/yyyy} ({trangThai})");
                        }
                    }
                    catch (Exception ex) { sb.AppendLine("⚠️ Lỗi sách đang mượn: " + ex.Message); }
                }

                if (role == "ASSET_MANAGER" || role == "ADMIN" || role == "DIRECTOR")
                {
                    try
                    {
                        var dtEq = DataProvider.Instance.ExecuteQuery(@"
                            SELECT Condition, COUNT(*) AS SoLuong
                            FROM Equipment WHERE IsDeleted = 0
                            GROUP BY Condition
                        ");
                        sb.AppendLine("[THIẾT BỊ]");
                        foreach (DataRow row in dtEq.Rows)
                            sb.AppendLine($"- {row["Condition"]}: {row["SoLuong"]} thiết bị");
                    }
                    catch (Exception ex) { sb.AppendLine("⚠️ Lỗi thống kê thiết bị: " + ex.Message); }

                    try
                    {
                        var dtBaoTri = DataProvider.Instance.ExecuteQuery(@"
                            SELECT COUNT(*) AS SoLuong FROM Equipment
                            WHERE NgayBaoTriDinhKy <= DATEADD(day, 7, GETDATE())
                            AND IsDeleted = 0
                        ");
                        sb.AppendLine($"- Cần bảo trì trong 7 ngày tới: {dtBaoTri.Rows[0]["SoLuong"]} thiết bị");
                    }
                    catch (Exception ex) { sb.AppendLine("⚠️ Lỗi bảo trì: " + ex.Message); }
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine("⚠️ Lỗi tổng: " + ex.Message);
            }

            return sb.ToString();
        }
    }
}