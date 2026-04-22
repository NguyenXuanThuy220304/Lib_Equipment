using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using Lib_Equipment.Database;

namespace Lib_Equipment
{
    public partial class FrmTroLyAI : Form
    {
        private bool _isThinking = false;
        private CheckBox _chkDebugMode;
        private string _currentRole = "";

        // NỘI QUY THƯ VIỆN - AI SẼ ĐỌC Ở ĐÂY ĐỂ TRẢ LỜI CÁC CÂU HỎI VỀ THÔNG TIN
        private readonly string _libraryRules = @"
- Mở cửa: Từ 7h00 đến 17h00, Thứ 2 đến Thứ 6.
- Quy định mượn: Sinh viên mượn tối đa 14 ngày. Giảng viên 21 ngày.
- Xử phạt: Quá hạn phạt 2.000đ/ngày. Làm mất/hỏng đền 200% giá trị.
- Điều kiện: Không được mượn sách nếu đang nợ phí.
- Nếu người dùng chào hỏi, hãy chào lại một cách lịch sự và thân thiện.
";

        public FrmTroLyAI(string userRole = "Admin")
        {
            InitializeComponent();
            _currentRole = userRole;
            SetupDebugCheckbox();

            // ĐỔI TÊN TIÊU ĐỀ TRÊN GIAO DIỆN THEO QUYỀN (SỬA LỖI LÚC NÀO CŨNG HIỆN ADMIN)
            Control lbl = this.Controls.Find("lblTitle", true).FirstOrDefault()
                       ?? this.Controls.Find("label1", true).FirstOrDefault(); // Đề phòng bạn đặt tên label là label1
            if (lbl != null)
            {
                lbl.Text = "Trợ Lý UNETI " + _currentRole.ToUpper();
            }

            this.Load += (s, e) => {
                if (!string.IsNullOrEmpty(AiSessionMemory.RtfChatHistory))
                    rtbChatHistory.Rtf = AiSessionMemory.RtfChatHistory;
                else
                {
                    AppendText("🤖 UNETI AI: ", Color.FromArgb(0, 120, 212), FontStyle.Bold);
                    AppendText($"Hệ thống Thông minh sẵn sàng. Quyền truy cập: [{_currentRole.ToUpper()}]. Bạn cần tra cứu tài liệu hay hỗ trợ thông tin gì?\n\n", Color.FromArgb(32, 33, 36), FontStyle.Regular);
                }
            };
        }

        private void SetupDebugCheckbox()
        {
            _chkDebugMode = new CheckBox { Text = "🛠 Xem Debug AI", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Italic), ForeColor = Color.DimGray, Cursor = Cursors.Hand };
            if (this.Controls.ContainsKey("pnlTopTitle"))
            {
                var topPanel = this.Controls["pnlTopTitle"];
                _chkDebugMode.Location = new Point(topPanel.Width - 180, 15);
                _chkDebugMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                topPanel.Controls.Add(_chkDebugMode);
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string question = txtQuestion.Text.Trim();
            if (string.IsNullOrEmpty(question) || _isThinking) return;

            AppendText("👤 Bạn: ", Color.FromArgb(100, 100, 100), FontStyle.Bold);
            AppendText(question + "\n\n", Color.FromArgb(32, 33, 36), FontStyle.Regular);
            txtQuestion.Clear();

            _isThinking = true;
            btnSend.Enabled = false;
            var thinkingTask = ShowThinkingAnimation();
            await ProcessLocalAI(question);
            _isThinking = false;
            btnSend.Enabled = true;
            txtQuestion.Focus();
        }

        private async Task ShowThinkingAnimation()
        {
            int baseLen = 0;
            this.Invoke(new Action(() => {
                AppendText("🤖 UNETI AI: ", Color.FromArgb(0, 120, 212), FontStyle.Bold);
                AppendText("Đang phân tích", Color.Gray, FontStyle.Italic);
                baseLen = rtbChatHistory.TextLength;
            }));
            int dots = 0;
            while (_isThinking)
            {
                this.Invoke(new Action(() => {
                    rtbChatHistory.Select(baseLen, rtbChatHistory.TextLength - baseLen);
                    rtbChatHistory.SelectedText = "";
                    dots = (dots % 3) + 1;
                    rtbChatHistory.AppendText(new string('.', dots));
                }));
                await Task.Delay(400);
            }
            this.Invoke(new Action(() => {
                int startDelete = baseLen - 15;
                if (startDelete >= 0 && rtbChatHistory.TextLength >= startDelete)
                {
                    rtbChatHistory.Select(startDelete, rtbChatHistory.TextLength - startDelete);
                    rtbChatHistory.SelectedText = "";
                }
            }));
        }

        private async Task ProcessLocalAI(string question)
        {
            try
            {
                // ==================================================
                // BƯỚC 1: PHÂN LOẠI CÂU HỎI THÀNH 3 NHÓM (SÁCH, THIẾT BỊ, THÔNG TIN)
                // ==================================================
                string extractPrompt = $@"Nhiệm vụ: Phân loại câu hỏi thuộc SÁCH, THIETBI, hay THONGTIN (hỏi nội quy, giờ mở cửa, luật mượn, chào hỏi).
Ví dụ 1: 'tìm sách C#' -> KETQUA: SACH|C#
Ví dụ 2: 'cho xem máy chiếu' -> KETQUA: THIETBI|máy chiếu
Ví dụ 3: 'luật mượn sách thế nào' -> KETQUA: THONGTIN|luật mượn sách
Ví dụ 4: 'xin chào' -> KETQUA: THONGTIN|xin chào

Câu hỏi: '{question}'
Chỉ in ra đúng 1 dòng KETQUA:";

                string aiResultRaw = await SendToOllamaAPI(extractPrompt, 50, 0.0f);

                string target = "SACH";
                string keyword = question;

                if (aiResultRaw.Contains("|"))
                {
                    string[] parts = aiResultRaw.Split(new[] { '|' }, 2);
                    string category = parts[0].ToUpper();

                    if (category.Contains("THIETBI") || category.Contains("THIẾT BỊ")) target = "THIETBI";
                    else if (category.Contains("THONGTIN") || category.Contains("THÔNG TIN")) target = "THONGTIN";
                    else target = "SACH";

                    keyword = parts[1].Trim().Replace("'", "");
                }
                else
                {
                    // Fallback nếu AI trả lời lỗi
                    if (question.ToLower().Contains("luật") || question.ToLower().Contains("nội quy") || question.ToLower().Contains("chào") || question.ToLower().Contains("phạt")) target = "THONGTIN";
                    else if (question.ToLower().Contains("thiết bị") || question.ToLower().Contains("máy")) target = "THIETBI";
                }

                // Chặn quyền Quản lý thiết bị tìm sách (Nhưng vẫn cho phép hỏi THONGTIN)
                if (_currentRole == "ThietBi" && target == "SACH") target = "THIETBI";

                if (_chkDebugMode.Checked)
                {
                    AppendText($"   [DEBUG - AI Bóc tách]: {aiResultRaw} -> Chọn nhóm: {target}\n", Color.DarkOrange, FontStyle.Italic);
                }

                string dataText = "";

                // ==================================================
                // BƯỚC 2: XỬ LÝ THEO TỪNG NHÓM (CÓ DATABASE VÀ KHÔNG DATABASE)
                // ==================================================
                if (target == "THONGTIN")
                {
                    // [NHÓM THÔNG TIN]: GỌI AI TRẢ LỜI NHƯ NHÂN VIÊN CSKH DỰA VÀO NỘI QUY CÓ SẴN
                    if (_chkDebugMode.Checked) AppendText("   [DEBUG]: Câu hỏi dạng thông tin, AI sẽ trả lời tự do không dùng Database.\n\n", Color.Teal, FontStyle.Italic);

                    string chatPrompt = $@"Bạn là Trợ lý CSKH Thư viện UNETI. Hãy trả lời câu hỏi sau một cách lịch sự, thân thiện và ngắn gọn dựa vào nội quy sau.
NỘI QUY THƯ VIỆN:
{_libraryRules}

Câu hỏi người dùng: {question}
TRẢ LỜI BẰNG TIẾNG VIỆT:";

                    // Cho AI sáng tạo 1 chút (temp=0.3) và cho phép trả lời dài (250 tokens)
                    dataText = await SendToOllamaAPI(chatPrompt, 250, 0.3f);
                }
                else
                {
                    // [NHÓM SÁCH VÀ THIẾT BỊ]: TẠO LỆNH SQL VÀ TRUY VẤN
                    string sql = "";
                    if (target == "SACH")
                    {
                        sql = $"SELECT b.Title AS [Tên Sách], b.CabinetLocation AS [Vị trí], c.CategoryName AS [Thể Loại], b.Author AS [Tác Giả] FROM Book b JOIN BookCategory c ON b.CategoryID = c.CategoryID WHERE b.Title LIKE N'%{keyword}%' OR c.CategoryName LIKE N'%{keyword}%'";
                    }
                    else
                    {
                        sql = $"SELECT e.EquipmentName AS [Tên Thiết Bị], e.Condition AS [Tình Trạng], c.CategoryName AS [Loại] FROM Equipment e JOIN EquipmentCategory c ON e.CategoryID = c.CategoryID WHERE e.EquipmentName LIKE N'%{keyword}%' OR c.CategoryName LIKE N'%{keyword}%'";
                    }

                    if (_chkDebugMode.Checked) AppendText($"   [DEBUG - C# Sinh SQL]: {sql}\n", Color.Teal, FontStyle.Italic);

                    try
                    {
                        DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dataText = $"Đây là thông tin {(target == "SACH" ? "Sách" : "Thiết bị")} bạn cần tìm:\n" + ConvertDataTableToText(dt);
                        }
                        else
                        {
                            dataText = $"Xin lỗi, hệ thống không tìm thấy {(target == "SACH" ? "sách" : "thiết bị")} nào khớp với từ khóa '{keyword}'.";
                        }
                    }
                    catch (Exception ex)
                    {
                        dataText = "Lỗi kết nối truy xuất cơ sở dữ liệu.";
                        if (_chkDebugMode.Checked) AppendText($"   [DEBUG - LỖI SQL]: {ex.Message}\n", Color.Red, FontStyle.Italic);
                    }
                }

                _isThinking = false;
                await Task.Delay(400);

                // IN KẾT QUẢ RA MÀN HÌNH CHAT
                this.Invoke(new Action(() => {
                    AppendText("🤖 UNETI AI: ", Color.FromArgb(0, 120, 212), FontStyle.Bold);
                    AppendText(dataText.Replace("```", "").Trim() + "\n\n", Color.Black, FontStyle.Regular);
                    AiSessionMemory.RtfChatHistory = rtbChatHistory.Rtf;
                }));
            }
            catch (Exception ex)
            {
                _isThinking = false;
                AppendText("⚠️ Lỗi AI: " + ex.Message + "\n\n", Color.Red, FontStyle.Italic);
            }
        }

        // ==================================================
        // HÀM GỌI API ĐƯỢC TỐI ƯU HÓA (CHO CẢ CHẾ ĐỘ CHAT VÀ SQL)
        // ==================================================
        private async Task<string> SendToOllamaAPI(string prompt, int maxTokens = 50, float temp = 0.0f)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(2);
                var payload = new
                {
                    model = "uneti-bot",
                    prompt = prompt,
                    stream = false,
                    options = new
                    {
                        temperature = temp,      // temp = 0 khi tách từ khóa, temp = 0.3 khi trả lời chat
                        top_k = 1,
                        num_predict = maxTokens  // Giới hạn độ dài trả lời
                    }
                };
                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:11434/api/generate", content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JObject.Parse(result)["response"]?.ToString() ?? "";
                }
                return "";
            }
        }

        private string ConvertDataTableToText(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Math.Min(dt.Rows.Count, 15); i++)
            {
                sb.Append("  • ");
                foreach (DataColumn col in dt.Columns)
                {
                    sb.Append($"{col.ColumnName}: {dt.Rows[i][col]} | ");
                }
                if (sb.Length > 3) sb.Length -= 3;
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private void AppendText(string text, Color color, FontStyle style)
        {
            if (rtbChatHistory.InvokeRequired) { rtbChatHistory.Invoke(new Action(() => AppendText(text, color, style))); return; }
            rtbChatHistory.SelectionStart = rtbChatHistory.TextLength;
            rtbChatHistory.SelectionColor = color;
            rtbChatHistory.SelectionFont = new Font("Segoe UI", 11.5F, style);
            rtbChatHistory.AppendText(text);
            rtbChatHistory.ScrollToCaret();
        }
    }
}