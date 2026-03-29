using Lib_Equipment.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib_Equipment
{
    // ====================================================================
    // LỚP LƯU TRỮ "THREAD" HỘI THOẠI (Sống đến khi Đăng xuất)
    // ====================================================================
    public static class AiChatSession
    {
        public static List<string> HistoryLogs = new List<string>();
        public static string SavedRtfChat = ""; // Lưu lại giao diện màu sắc khung chat

        // Lấy 6 tin nhắn gần nhất để AI nhớ ngữ cảnh mà không bị quá tải
        public static string GetRecentContext()
        {
            int count = HistoryLogs.Count;
            int start = count > 6 ? count - 6 : 0;
            return string.Join("\n", HistoryLogs.GetRange(start, count - start));
        }

        public static void AddLog(string userQuestion, string aiAnswer)
        {
            HistoryLogs.Add($"Quản lý hỏi: {userQuestion}");
            HistoryLogs.Add($"Hệ thống trả lời: {aiAnswer}");
        }

        public static void ClearSession()
        {
            HistoryLogs.Clear();
            SavedRtfChat = "";
        }
    }

    public partial class FrmTroLyAI : Form
    {
        // API Key lấy từ Google AI Studio
        private readonly string API_KEY = AppSecrets.GeminiApiKey;

        public FrmTroLyAI()
        {
            InitializeComponent();
        }

        // ====================================================================
        // KHÔI PHỤC LỊCH SỬ KHI MỞ LẠI FORM
        // ====================================================================
        private void FrmTroLyAI_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AiChatSession.SavedRtfChat))
            {
                rtbChatHistory.Rtf = AiChatSession.SavedRtfChat;
                rtbChatHistory.SelectionStart = rtbChatHistory.TextLength;
                rtbChatHistory.ScrollToCaret();
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string question = txtQuestion.Text.Trim();
            if (string.IsNullOrEmpty(question)) return;

            AppendText("👤 Bạn: ", Color.Blue, FontStyle.Bold);
            AppendText(question + "\n\n", Color.Black, FontStyle.Regular);

            txtQuestion.Clear();
            btnSend.Enabled = false;
            btnSend.Text = "Đang nghĩ...";

            await CallGeminiAI(question);

            // Lưu lại giao diện để tắt Form đi mở lại không bị mất chữ
            AiChatSession.SavedRtfChat = rtbChatHistory.Rtf;

            btnSend.Enabled = true;
            btnSend.Text = "GỬI (Enter)";
            txtQuestion.Focus();
        }

        private string ConvertDataTableToText(System.Data.DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return "Không tìm thấy dữ liệu nào khớp với yêu cầu trong Cơ sở dữ liệu.";

            StringBuilder sb = new StringBuilder();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                sb.Append("- ");
                foreach (System.Data.DataColumn col in dt.Columns)
                {
                    sb.Append($"{col.ColumnName}: {row[col]} | ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private async Task<string> SendToGeminiAPI(string prompt, double temperature = 0.1)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            using (HttpClient client = new HttpClient())
            {
                // Nhớ đổi lại gemini-1.5-flash hoặc bản bạn đang dùng nhé
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={API_KEY}";
                var payload = new
                {
                    contents = new[] { new { parts = new[] { new { text = prompt } } } },
                    generationConfig = new { temperature = temperature }
                };

                string jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        JObject jsonResponse = JObject.Parse(responseString);
                        return jsonResponse["candidates"][0]["content"]["parts"][0]["text"].ToString().Trim();
                    }
                    else
                    {
                        string errorDetail = await response.Content.ReadAsStringAsync();
                        return $"ERROR_API: Lỗi {response.StatusCode} - {errorDetail}";
                    }
                }
                catch (Exception ex)
                {
                    return $"ERROR_API: Lỗi kết nối mạng hoặc hệ thống ({ex.Message})";
                }
            }
        }

        private async Task CallGeminiAI(string question)
        {
            try
            {
                string chatContext = AiChatSession.GetRecentContext();

                // ---------------------------------------------------------
                // NHỊP 1: TẠO LỆNH SQL DỰA TRÊN NGỮ CẢNH CŨ
                // ---------------------------------------------------------
                string schemaPrompt = $@"
Bạn là một chuyên gia cơ sở dữ liệu SQL Server và là Trợ lý Quản lý trường học. 
Cơ sở dữ liệu [Lib_EquipmentDB] gồm các bảng:
1. Sách (Book): BookID, Title, Author, Publisher, PublishYear, CategoryID, CreatedAt, IsDeleted
2. Bản sao sách (BookCopy): CopyID, BookID, Condition, CreatedAt, IsDeleted, Status (N'Có sẵn', N'Đang mượn', N'Hỏng', N'Mất')
3. Phiếu mượn (BorrowRecord): RecordID, ReaderID, CreatedBy, BorrowDate, DueDate, Status, IsDeleted, ReturnDate
4. Chi tiết mượn (BorrowDetail): RecordID, CopyID, ReturnDate, ReturnCondition, FineAmount
5. Độc giả (Reader): ReaderID, FullName, DepartmentID, ReaderType, Status, CreatedAt, IsDeleted
6. Thiết bị (Equipment): EquipmentID, EquipmentName, CategoryID, DepartmentID, PurchasePrice, ImportDate, Condition, UpdatedBy, UpdatedAt, IsDeleted
7. Ghi nhận bảo trì (MaintenanceRecord): MaintenanceID, EquipmentID, CreatedBy, MaintenanceDate, Description, Cost, Vendor, IsDeleted
8. Người dùng/Nhân viên (User): UserID, Username, PasswordHash, FullName, RoleID, Status, CreatedAt, IsDeleted

[LỊCH SỬ TRÒ CHUYỆN GẦN ĐÂY ĐỂ LẤY NGỮ CẢNH]
{chatContext}

QUY TẮC NGHIÊM NGẶT:
- Dựa vào LỊCH SỬ trên, nếu Câu hỏi hiện tại có chứa từ thay thế (như: 'trong số đó', 'của người này', 'những cuốn đó', 'vậy còn...'), bạn PHẢI TỰ ĐỘNG ghép thêm điều kiện WHERE của câu hỏi trước vào câu hỏi hiện tại để tạo ra lệnh SQL chính xác.
- CHỈ trả về DUY NHẤT 1 câu lệnh T-SQL SELECT, KHÔNG giải thích, KHÔNG bọc trong markdown (```sql).
- Bắt buộc thêm 'IsDeleted = 0'. Trạng thái sách bắt buộc dùng tiếng Việt N'Có sẵn', N'Đang mượn', v.v.
- Tìm kiếm tên dùng LIKE N'%...%'. Tự động JOIN các bảng dựa vào Logic ID. Thêm TOP 50.
- NẾU câu hỏi hiện tại chỉ là giao tiếp thông thường (chào hỏi, cảm ơn), trả về đúng chữ: NOT_DB

Câu hỏi hiện tại của quản lý: {question}";

                string step1Response = await SendToGeminiAPI(schemaPrompt, 0.0);

                if (step1Response.StartsWith("ERROR_API"))
                {
                    AppendText($"⚠️ Hệ thống AI báo lỗi: \n{step1Response}\n\n", Color.Red, FontStyle.Italic);
                    return;
                }

                string sqlQuery = step1Response.Replace("```sql", "").Replace("```", "").Trim();
                AppendText($"[DEBUG LỆNH SQL]: {sqlQuery}\n\n", Color.Orange, FontStyle.Italic);

                // ---------------------------------------------------------
                // XỬ LÝ GIAO TIẾP THÔNG THƯỜNG (CÓ NGỮ CẢNH)
                // ---------------------------------------------------------
                if (sqlQuery == "NOT_DB" || !sqlQuery.ToUpper().StartsWith("SELECT"))
                {
                    string normalPrompt = $@"
Bạn là Trợ lý AI quản lý thông minh của trường UNETI. Dưới đây là lịch sử đang trò chuyện:
{chatContext}

Câu hỏi hiện tại: {question}
Hãy trả lời tiếp nối câu chuyện một cách chuyên nghiệp, thân thiện và ngắn gọn.";

                    string normalAnswer = await SendToGeminiAPI(normalPrompt, 0.7);

                    if (normalAnswer.StartsWith("ERROR_API"))
                    {
                        AppendText($"⚠️ Lỗi AI: \n{normalAnswer}\n\n", Color.Red, FontStyle.Italic);
                        return;
                    }

                    AppendText("🤖 Trợ lý Quản lý: ", Color.FromArgb(40, 167, 69), FontStyle.Bold);
                    AppendText(normalAnswer + "\n\n", Color.Black, FontStyle.Regular);

                    // Lưu vào trí nhớ
                    AiChatSession.AddLog(question, normalAnswer);
                    return;
                }

                // Bảo mật
                if (sqlQuery.ToUpper().Contains("DELETE ") || sqlQuery.ToUpper().Contains("DROP ") || sqlQuery.ToUpper().Contains("UPDATE ") || sqlQuery.ToUpper().Contains("INSERT "))
                {
                    AppendText("⚠️ Lỗi bảo mật: Lệnh bị từ chối.\n\n", Color.Red, FontStyle.Italic);
                    return;
                }

                // ---------------------------------------------------------
                // NHỊP 2: BÁO CÁO DỮ LIỆU SQL
                // ---------------------------------------------------------
                AppendText("⏳ (Đang trích xuất dữ liệu tổng hợp...)\n", Color.Gray, FontStyle.Italic);

                System.Data.DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);
                string dbResultText = ConvertDataTableToText(dt);

                string finalPrompt = $@"
Lịch sử đang nói chuyện: {chatContext}
Câu hỏi hiện tại: '{question}'
Dữ liệu trích xuất từ SQL: {dbResultText}

Nhiệm vụ: Đóng vai Quản lý hệ thống, dựa vào Dữ liệu trích xuất để trả lời câu hỏi hiện tại. 
- Nếu câu hỏi hỏi tiếp nối (Ví dụ: Trong số đó có bao nhiêu...), hãy trả lời thẳng vào vấn đề.
- Trình bày dạng danh sách gạch đầu dòng nếu có nhiều kết quả.
- KHÔNG nhắc đến lệnh SQL.";

                string finalAnswer = await SendToGeminiAPI(finalPrompt, 0.2);

                if (finalAnswer.StartsWith("ERROR_API"))
                {
                    AppendText($"⚠️ Lỗi báo cáo: \n{finalAnswer}\n\n", Color.Red, FontStyle.Italic);
                    return;
                }

                AppendText("🤖 Trợ lý Quản lý: ", Color.FromArgb(40, 167, 69), FontStyle.Bold);
                AppendText(finalAnswer + "\n\n", Color.Black, FontStyle.Regular);

                // Lưu vào trí nhớ toàn cục
                AiChatSession.AddLog(question, finalAnswer);
            }
            catch (Exception ex)
            {
                AppendText($"⚠️ Lỗi hệ thống: {ex.Message}\n\n", Color.Red, FontStyle.Italic);
            }
        }

        private void AppendText(string text, Color color, FontStyle style)
        {
            rtbChatHistory.SelectionStart = rtbChatHistory.TextLength;
            rtbChatHistory.SelectionLength = 0;
            rtbChatHistory.SelectionColor = color;
            rtbChatHistory.SelectionFont = new Font(rtbChatHistory.Font, style);
            rtbChatHistory.AppendText(text);
            rtbChatHistory.ScrollToCaret();
            AiChatSession.SavedRtfChat = rtbChatHistory.Rtf;
        }

        private void txtQuestion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSend.PerformClick();
            }
        }
    }
}