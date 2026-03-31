using Lib_Equipment.Database;

using Newtonsoft.Json;

using Newtonsoft.Json.Linq;

using System;

using System.Drawing;

using System.Net.Http;

using System.Text;

using System.Threading.Tasks;

using System.Windows.Forms;



namespace Lib_Equipment

{

    // ====================================================================
    // === THÊM MỚI 1: LỚP GHI NHỚ LỊCH SỬ VÀ GIAO DIỆN (SỐNG TỚI KHI ĐĂNG XUẤT) ===
    // ====================================================================
    public static class AiSessionMemory
    {
        public static string ChatContext = "";
        public static string RtfChatHistory = "";

        public static void ClearMemory()
        {
            ChatContext = "";
            RtfChatHistory = "";
        }
    }


    public partial class FrmTroLyAI : Form

    {

        // API Key lấy từ Google AI Studio

        private readonly string API_KEY = AppSecrets.GeminiApiKey;



        public FrmTroLyAI()

        {

            InitializeComponent();

            // === THÊM MỚI 2: KHÔI PHỤC GIAO DIỆN KHI QUAY LẠI TỪ CHỨC NĂNG KHÁC ===
            this.Load += (s, e) =>
            {
                if (!string.IsNullOrEmpty(AiSessionMemory.RtfChatHistory))
                {
                    rtbChatHistory.Rtf = AiSessionMemory.RtfChatHistory;
                    rtbChatHistory.SelectionStart = rtbChatHistory.TextLength;
                    rtbChatHistory.ScrollToCaret();
                }
            };

        }



        private async void btnSend_Click(object sender, EventArgs e)

        {

            string question = txtQuestion.Text.Trim();

            if (string.IsNullOrEmpty(question)) return;



            // Hiển thị câu hỏi của thủ thư lên màn hình

            AppendText("👤 Bạn: ", Color.Blue, FontStyle.Bold);

            AppendText(question + "\n\n", Color.Black, FontStyle.Regular);



            txtQuestion.Clear();

            btnSend.Enabled = false;

            btnSend.Text = "Đang nghĩ...";



            // Gọi hệ thống AI Agent xử lý

            await CallGeminiAI(question);



            btnSend.Enabled = true;

            btnSend.Text = "GỬI (Enter)";

            txtQuestion.Focus();

        }



        // ====================================================================

        // HÀM 1: CHUYỂN ĐỔI KẾT QUẢ SQL (DATATABLE) THÀNH CHỮ CHO AI ĐỌC

        // ====================================================================

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



        // ====================================================================

        // HÀM 2: GIAO TIẾP VỚI API GOOGLE GEMINI (ĐÃ SỬA MODEL & BẮT LỖI)

        // ====================================================================

        private async Task<string> SendToGeminiAPI(string prompt, double temperature = 0.1)

        {

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            using (HttpClient client = new HttpClient())

            {

                // Đã sửa model thành gemini-1.5-flash để tránh lỗi không tồn tại model

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

                        // Đọc chi tiết lỗi từ Google để in ra màn hình

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



        // ====================================================================

        // HÀM 3: LUỒNG XỬ LÝ CHÍNH (AI AGENT VỚI FULL DATABASE)

        // ====================================================================

        private async Task CallGeminiAI(string question)

        {

            try

            {

                // ---------------------------------------------------------

                // NHỊP 1: YÊU CẦU AI DỊCH CÂU HỎI THÀNH CÂU LỆNH SQL

                // ---------------------------------------------------------

                // === THÊM MỚI 3: NHÚNG LỊCH SỬ NGỮ CẢNH VÀO LỆNH PROMPT CŨ CỦA BẠN ===
                string schemaPrompt = $@"

Bạn là một chuyên gia cơ sở dữ liệu SQL Server và là Trợ lý Quản lý cho một hệ thống trường học. 

Cơ sở dữ liệu [Lib_EquipmentDB] của chúng tôi gồm các bảng sau:



-- QUẢN LÝ SÁCH & MƯỢN TRẢ

-- QUẢN LÝ SÁCH & MƯỢN TRẢ

1. Sách (Book): BookID, Title, Author, Publisher, PublishYear, CategoryID, CreatedAt, IsDeleted

2. Danh mục sách (BookCategory): CategoryID, CategoryName, IsDeleted

3. Bản sao sách (BookCopy): CopyID, BookID, Condition, CreatedAt, IsDeleted, Status (LƯU Ý: Cột Status CHỈ chứa các chữ: N'Có sẵn', N'Đang mượn', N'Hỏng', N'Mất')

4. Phiếu mượn (BorrowRecord): RecordID, ReaderID, CreatedBy, BorrowDate, DueDate, Status, IsDeleted, ReturnDate

5. Chi tiết mượn (BorrowDetail): RecordID, CopyID, ReturnDate, ReturnCondition, FineAmount

6. Độc giả (Reader): ReaderID, FullName, DepartmentID, ReaderType, Status, CreatedAt, IsDeleted



-- QUẢN LÝ THIẾT BỊ & ĐIỀU CHUYỂN

7. Thiết bị (Equipment): EquipmentID, EquipmentName, CategoryID, DepartmentID, PurchasePrice, ImportDate, Condition, UpdatedBy, UpdatedAt, IsDeleted

8. Danh mục thiết bị (EquipmentCategory): CategoryID, CategoryName, IsDeleted

9. Ghi nhận bảo trì (MaintenanceRecord): MaintenanceID, EquipmentID, CreatedBy, MaintenanceDate, Description, Cost, Vendor, IsDeleted

10. Phiếu điều chuyển (TransferRecord): TransferID, FromDepartmentID, ToDepartmentID, CreatedBy, TransferDate, Reason, IsDeleted

11. Chi tiết điều chuyển (TransferDetail): TransferID, EquipmentID, ConditionAtTransfer

12. Khoa/Phòng ban (Department): DepartmentID, DepartmentName, DepartmentType, CreatedAt, IsDeleted



-- QUẢN LÝ HỆ THỐNG & NHÂN SỰ

13. Người dùng/Nhân viên (User): UserID, Username, PasswordHash, FullName, RoleID, Status, CreatedAt, IsDeleted

14. Vai trò (Role): RoleID, RoleName

15. Quyền (RolePermission): RoleID, MenuID

16. Menu hệ thống (SysMenu): MenuID, MenuName



[LỊCH SỬ TRÒ CHUYỆN ĐỂ HIỂU NGỮ CẢNH]
{AiSessionMemory.ChatContext}



QUY TẮC XÂY DỰNG SQL NGHIÊM NGẶT:

- DỰA VÀO LỊCH SỬ: Nếu câu hỏi hiện tại thiếu chủ ngữ hoặc mang tính chất hỏi tiếp (VD: 'A bao nhiêu tuổi', 'trong số đó...', 'vậy còn...'), bạn BẮT BUỘC phải đọc Lịch sử trò chuyện để biết người dùng đang nói về ai/cái gì để viết T-SQL chính xác.

- TRẠNG THÁI (STATUS): Bắt buộc dùng đúng các từ khóa tiếng Việt đã được định nghĩa trong ngoặc đơn của từng bảng (Ví dụ: N'Có sẵn'). Tuyệt đối KHÔNG tự ý dịch sang tiếng Anh như 'Available'.

- CHỈ trả về DUY NHẤT 1 câu lệnh T-SQL SELECT, KHÔNG giải thích, KHÔNG bọc trong markdown (```sql).

- BẮT BUỘC: Nếu bảng có cột IsDeleted, luôn luôn thêm điều kiện 'IsDeleted = 0'.

- BẮT BUỘC TÌM KIẾM TƯƠNG ĐỐI: Khi tìm kiếm theo tên (Title, EquipmentName, FullName...), TUYỆT ĐỐI dùng toán tử LIKE N'%từ_khóa%' thay vì dấu '='. 

- BÓC TÁCH TỪ KHÓA: AI phải tự hiểu và loại bỏ các từ thừa của người dùng. Ví dụ: Người dùng hỏi ""sách hóa học"", chỉ lấy từ khóa '%hóa học%' để ném vào LIKE.

- PHÂN BIỆT ĐẦU SÁCH VÀ CUỐN SÁCH: Nếu hỏi 'có mấy quyển/cuốn/chiếc', phải đếm số lượng bản sao vật lý trong bảng BookCopy (sách) hoặc Equipment (thiết bị). Nếu hỏi 'có bao nhiêu đầu sách/tựa sách', mới đếm trong bảng Book.

- Dùng tiền tố N trước tất cả các chuỗi văn bản tiếng Việt.

- Tự động JOIN các bảng dựa vào Logic ID. Luôn thêm TOP 50 để tránh quá tải.

- NẾU câu hỏi giao tiếp thông thường (chào hỏi, không liên quan dữ liệu), trả về đúng chữ: NOT_DB



Câu hỏi của quản lý: {question}";



                // Nhiệt độ 0.0 để AI tập trung viết SQL logic chính xác tuyệt đối

                string step1Response = await SendToGeminiAPI(schemaPrompt, 0.0);



                if (step1Response.StartsWith("ERROR_API"))

                {

                    AppendText($"⚠️ Hệ thống AI báo lỗi: \n{step1Response}\n\n", Color.Red, FontStyle.Italic);

                    return;

                }



                string sqlQuery = step1Response.Replace("```sql", "").Replace("```", "").Trim();



                AppendText($"[DEBUG LỆNH SQL]: {sqlQuery}\n\n", Color.Orange, FontStyle.Italic);



                // ---------------------------------------------------------

                // KIỂM TRA: NẾU KHÔNG PHẢI LÀ CÂU HỎI TRA CỨU DỮ LIỆU

                // ---------------------------------------------------------

                if (sqlQuery == "NOT_DB" || !sqlQuery.ToUpper().StartsWith("SELECT"))

                {

                    // === THÊM MỚI 3.1: NHÚNG LỊCH SỬ VÀO CÂU CHAT BÌNH THƯỜNG ===
                    string normalPrompt = $"[Ngữ cảnh cuộc trò chuyện: {AiSessionMemory.ChatContext}]\nBạn là Trợ lý AI quản lý thông minh của trường UNETI. Hãy trả lời câu hỏi sau một cách chuyên nghiệp, thân thiện: {question}";

                    string normalAnswer = await SendToGeminiAPI(normalPrompt, 0.7);



                    if (normalAnswer.StartsWith("ERROR_API"))

                    {

                        AppendText($"⚠️ Hệ thống AI báo lỗi: \n{normalAnswer}\n\n", Color.Red, FontStyle.Italic);

                        return;

                    }



                    AppendText("🤖 Trợ lý Quản lý: ", Color.FromArgb(40, 167, 69), FontStyle.Bold);

                    AppendText(normalAnswer + "\n\n", Color.Black, FontStyle.Regular);

                    // Ghi nhớ câu hỏi và câu trả lời
                    AiSessionMemory.ChatContext += $"\nHỏi: {question}\nĐáp: {normalAnswer}";

                    return;

                }



                // Bảo mật: Chặn đứng mọi lệnh làm thay đổi cơ sở dữ liệu

                if (sqlQuery.ToUpper().Contains("DELETE ") || sqlQuery.ToUpper().Contains("DROP ") || sqlQuery.ToUpper().Contains("UPDATE ") || sqlQuery.ToUpper().Contains("INSERT ") || sqlQuery.ToUpper().Contains("TRUNCATE "))

                {

                    AppendText("⚠️ Lỗi bảo mật: Câu lệnh truy vấn không hợp lệ.\n\n", Color.Red, FontStyle.Italic);

                    return;

                }



                // ---------------------------------------------------------

                // NHỊP 2: CHẠY SQL LẤY DỮ LIỆU -> ĐƯA LẠI CHO AI ĐỂ BÁO CÁO

                // ---------------------------------------------------------

                AppendText("⏳ (Đang trích xuất dữ liệu tổng hợp...)\n", Color.Gray, FontStyle.Italic);



                System.Data.DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);

                string dbResultText = ConvertDataTableToText(dt);



                // === THÊM MỚI 3.2: NHÚNG LỊCH SỬ VÀO LỆNH BÁO CÁO SQL ===
                string finalPrompt = $@"

[Ngữ cảnh cuộc trò chuyện]: {AiSessionMemory.ChatContext}

Dưới đây là dữ liệu kết quả được trích xuất từ hệ thống cho câu hỏi '{question}':

Dữ liệu: {dbResultText}



Nhiệm vụ: 

Đóng vai một người Quản lý hệ thống cấp cao, sử dụng CHỈ dữ liệu trên và Ngữ cảnh để báo cáo lại cho người dùng một cách rõ ràng, mạch lạc và chuyên nghiệp. 

- Trình bày dạng danh sách gạch đầu dòng nếu có nhiều kết quả.

- Tuyệt đối KHÔNG nhắc đến việc bạn đã dùng lệnh SQL.

- Nếu dữ liệu ghi 'Không tìm thấy...', hãy báo cáo rằng không có dữ liệu khớp với yêu cầu tìm kiếm hiện tại.";



                // Nhiệt độ 0.2 để báo cáo bám sát số liệu, không bịa đặt thêm

                string finalAnswer = await SendToGeminiAPI(finalPrompt, 0.2);



                if (finalAnswer.StartsWith("ERROR_API"))

                {

                    AppendText($"⚠️ Lỗi khi tạo báo cáo: \n{finalAnswer}\n\n", Color.Red, FontStyle.Italic);

                    return;

                }



                AppendText("🤖 Trợ lý Quản lý: ", Color.FromArgb(40, 167, 69), FontStyle.Bold);

                AppendText(finalAnswer + "\n\n", Color.Black, FontStyle.Regular);

                // Ghi nhớ câu hỏi và câu trả lời vào não bộ AI
                AiSessionMemory.ChatContext += $"\nHỏi: {question}\nĐáp: {finalAnswer}";

            }

            catch (Exception ex)

            {

                AppendText($"⚠️ Lỗi xử lý dữ liệu: Hệ thống không thể phân tích yêu cầu này.\n(Chi tiết: {ex.Message})\n\n", Color.Red, FontStyle.Italic);

            }

        }


        // ====================================================================

        // CÁC HÀM HỖ TRỢ GIAO DIỆN

        // ====================================================================

        private void AppendText(string text, Color color, FontStyle style)

        {

            rtbChatHistory.SelectionStart = rtbChatHistory.TextLength;

            rtbChatHistory.SelectionLength = 0;

            rtbChatHistory.SelectionColor = color;

            rtbChatHistory.SelectionFont = new Font(rtbChatHistory.Font, style);

            rtbChatHistory.AppendText(text);

            rtbChatHistory.ScrollToCaret();


            // === THÊM MỚI 4: LƯU TÌNH TRẠNG GIAO DIỆN NGAY LẬP TỨC ===
            AiSessionMemory.RtfChatHistory = rtbChatHistory.Rtf;

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