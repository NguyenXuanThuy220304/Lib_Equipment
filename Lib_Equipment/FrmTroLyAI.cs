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
    public partial class FrmTroLyAI : Form
    {
        // API Key lấy từ Google AI Studio
        private readonly string API_KEY = "AIzaSyBq6irhlY8wzYteJdMN68CHUJ7qI1a5O3E";

        public FrmTroLyAI()
        {
            InitializeComponent();
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
        // HÀM 2: GIAO TIẾP VỚI API GOOGLE GEMINI (ÉP CHUẨN TLS 1.2)
        // ====================================================================
        private async Task<string> SendToGeminiAPI(string prompt)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={API_KEY}";
                var payload = new { contents = new[] { new { parts = new[] { new { text = prompt } } } } };
                string jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(responseString);
                    return jsonResponse["candidates"][0]["content"]["parts"][0]["text"].ToString().Trim();
                }
                return "ERROR";
            }
        }

        // ====================================================================
        // HÀM 3: LUỒNG XỬ LÝ CHÍNH (AI AGENT 2 NHỊP)
        // ====================================================================
        private async Task CallGeminiAI(string question)
        {
            try
            {
                // ---------------------------------------------------------
                // NHỊP 1: YÊU CẦU AI DỊCH CÂU HỎI THÀNH CÂU LỆNH SQL
                // ---------------------------------------------------------
                string schemaPrompt = @"
Bạn là một chuyên gia cơ sở dữ liệu SQL Server. Hệ thống Thư viện của tôi có các bảng sau:
1. Reader(ReaderID, FullName, DepartmentID, ReaderType, Status)
2. Book(BookID, Title, Author)
3. BookCopy(CopyID, BookID, Status) -- Status có thể là N'Sẵn sàng', N'Đang mượn', N'Cũ/Rách', N'Mất'
4. BorrowRecord(RecordID, ReaderID, BorrowDate, DueDate)
5. BorrowDetail(RecordID, CopyID, ReturnDate, FineAmount)

NẾU câu hỏi của người dùng CẦN tra cứu dữ liệu (như: sách còn không, ai đang mượn, sách quá hạn...), hãy CHỈ TRẢ VỀ MỘT CÂU LỆNH SQL SELECT duy nhất (Dùng T-SQL, có N'' trước chuỗi tiếng Việt). KHÔNG giải thích, KHÔNG dùng Markdown.
NẾU câu hỏi KHÔNG liên quan đến việc tra cứu dữ liệu (ví dụ: xin chào, hãy tư vấn cách học...), hãy trả lời chính xác chữ: NOT_DB

Câu hỏi của người dùng: " + question;

                string step1Response = await SendToGeminiAPI(schemaPrompt);

                // Dọn dẹp các ký tự thừa nếu AI lỡ chèn thêm (ví dụ: ```sql)
                string sqlQuery = step1Response.Replace("```sql", "").Replace("```", "").Trim();

                // ---------------------------------------------------------
                // KIỂM TRA: NẾU KHÔNG PHẢI LÀ CÂU HỎI TRA CỨU DỮ LIỆU
                // ---------------------------------------------------------
                if (sqlQuery == "NOT_DB" || sqlQuery == "ERROR" || !sqlQuery.ToUpper().StartsWith("SELECT"))
                {
                    // Trả lời như một Chatbot bình thường
                    string normalAnswer = await SendToGeminiAPI("Hãy đóng vai thủ thư UNETI và trả lời câu sau một cách thân thiện: " + question);
                    AppendText("🤖 Trợ lý AI: ", Color.FromArgb(40, 167, 69), FontStyle.Bold);
                    AppendText(normalAnswer + "\n\n", Color.Black, FontStyle.Regular);
                    return;
                }

                // ---------------------------------------------------------
                // NHỊP 2: CHẠY SQL LẤY DỮ LIỆU -> ĐƯA LẠI CHO AI ĐỂ TẠO CÂU TRẢ LỜI
                // ---------------------------------------------------------
                AppendText("⏳ (AI đang quét dữ liệu trong kho...)\n", Color.Gray, FontStyle.Italic);

                // Chạy lệnh SQL do AI vừa viết
                System.Data.DataTable dt = DataProvider.Instance.ExecuteQuery(sqlQuery);
                string dbResultText = ConvertDataTableToText(dt);

                // Bơm kết quả thật vào cho AI để nó trả lời sinh viên
                string finalPrompt = $@"
Dưới đây là kết quả trích xuất từ cơ sở dữ liệu cho câu hỏi '{question}':
{dbResultText}

Hãy đóng vai một thủ thư chuyên nghiệp của UNETI, sử dụng kết quả trên để trả lời người dùng một cách tự nhiên, lịch sự và rõ ràng nhất. 
Tuyệt đối KHÔNG đề cập đến việc bạn đã dùng câu lệnh SQL hay Database. Chỉ trả lời thông tin người dùng cần.
";
                string finalAnswer = await SendToGeminiAPI(finalPrompt);

                AppendText("🤖 Trợ lý AI: ", Color.FromArgb(40, 167, 69), FontStyle.Bold);
                AppendText(finalAnswer + "\n\n", Color.Black, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                AppendText($"⚠️ Lỗi xử lý: AI vừa tạo ra một truy vấn phức tạp chưa được hỗ trợ hoặc máy chủ bận.\n(Chi tiết: {ex.Message})\n\n", Color.Red, FontStyle.Italic);
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
            rtbChatHistory.ScrollToCaret(); // Tự động cuộn xuống cuối
        }

        private void txtQuestion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Chặn tiếng "bíp" của Windows
                btnSend.PerformClick();
            }
        }
    }
}