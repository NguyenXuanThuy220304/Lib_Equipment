using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lib_Equipment.BLL;

namespace Lib_Equipment
{
    public partial class FrmTroLyAI : Form
    {
        // ═══════════════════════════════════════════════════════
        // BIẾN PHÂN QUYỀN — truyền vào từ form đăng nhập
        // ═══════════════════════════════════════════════════════
        private readonly string _role;
        private readonly string _readerID;
        private bool _isFirstMessage = true;

        // Màu bubble theo người gửi
        private readonly Color COLOR_USER_BG = Color.FromArgb(0, 120, 212);
        private readonly Color COLOR_USER_TEXT = Color.White;
        private readonly Color COLOR_AI_BG = Color.White;
        private readonly Color COLOR_AI_TEXT = Color.FromArgb(18, 24, 38);
        private readonly Color COLOR_TIME = Color.FromArgb(160, 170, 185);
        private readonly Color COLOR_LABEL_USER = Color.FromArgb(0, 120, 212);
        private readonly Color COLOR_LABEL_AI = Color.FromArgb(52, 211, 153);

        // ═══════════════════════════════════════════════════════
        // CONSTRUCTOR — nhận Role và ReaderID từ màn hình đăng nhập
        // ═══════════════════════════════════════════════════════
        public FrmTroLyAI(string role = "Admin", string readerID = null)
        {
            InitializeComponent();
            _role = role;
            _readerID = readerID;

            SetupByRole();
            ShowWelcomeMessage();
        }

        // ═══════════════════════════════════════════════════════
        // CÀI ĐẶT GIAO DIỆN THEO VAI TRÒ
        // ═══════════════════════════════════════════════════════
        private void SetupByRole()
        {
            switch (_role)
            {
                case "LIBRARIAN":
                    lblRoleName.Text = "Thủ Thư";
                    lblHeaderTitle.Text = "Trợ Lý Thư Viện";
                    lblAvatarIcon.Text = "📚";
                    pnlAvatarWrap.BackColor = Color.FromArgb(26, 75, 132);
                    btnSend.FillColor = Color.FromArgb(26, 75, 132);
                    pnlInputBox.CustomBorderColor = Color.FromArgb(26, 75, 132);
                    UpdateSuggestButtons("📚 Sách mượn nhiều nhất?", "⚠️ Ai đang quá hạn?", "📊 Thống kê hôm nay?");
                    break;

                case "Reader":
                    lblRoleName.Text = "Độc Giả";
                    lblHeaderTitle.Text = "Trợ Lý Độc Giả";
                    lblAvatarIcon.Text = "🎓";
                    pnlAvatarWrap.BackColor = Color.FromArgb(16, 137, 62);
                    btnSend.FillColor = Color.FromArgb(16, 137, 62);
                    pnlInputBox.CustomBorderColor = Color.FromArgb(16, 137, 62);
                    UpdateSuggestButtons("📖 Tôi đang mượn sách gì?", "⏰ Sách nào sắp đến hạn?", "🔍 Tìm sách theo thể loại?");
                    break;

                case "ASSET_MANAGER":
                    lblRoleName.Text = "Cán Bộ Thiết Bị";
                    lblHeaderTitle.Text = "Trợ Lý Thiết Bị";
                    lblAvatarIcon.Text = "🔧";
                    pnlAvatarWrap.BackColor = Color.FromArgb(162, 84, 18);
                    btnSend.FillColor = Color.FromArgb(162, 84, 18);
                    pnlInputBox.CustomBorderColor = Color.FromArgb(162, 84, 18);
                    UpdateSuggestButtons("🔧 Thiết bị cần bảo trì?", "❌ Thiết bị đang hỏng?", "📋 Lịch bảo trì tuần này?");
                    break;

                default: // Admin
                    lblRoleName.Text = "Quản Trị Viên";
                    lblHeaderTitle.Text = "Trợ Lý AI UNETI";
                    lblAvatarIcon.Text = "🤖";
                    pnlAvatarWrap.BackColor = Color.FromArgb(0, 120, 212);
                    UpdateSuggestButtons("📚 Sách mượn nhiều nhất?", "⚠️ Thiết bị cần bảo trì?", "📊 Tổng quan hệ thống?");
                    break;
            }
        }

        private void UpdateSuggestButtons(string t1, string t2, string t3)
        {
            btnSuggest1.Text = t1;
            btnSuggest2.Text = t2;
            btnSuggest3.Text = t3;
        }

        // ═══════════════════════════════════════════════════════
        // TIN NHẮN CHÀO MỪNG
        // ═══════════════════════════════════════════════════════
        private void ShowWelcomeMessage()
        {
            string msg;
            switch (_role)
            {
                case "LIBRARIAN":
                    msg = "Xin chào Thủ thư! 👋\n\nTôi có thể giúp bạn:\n• Tra cứu sách và tình trạng bản sao\n• Xem danh sách độc giả quá hạn / nợ phạt\n• Thống kê lượt mượn và sách phổ biến\n\nBạn cần tra cứu gì không?";
                    break;
                case "Reader":
                    msg = "Xin chào bạn đọc! 📖\n\nTôi có thể giúp bạn:\n• Xem sách đang mượn và ngày trả\n• Kiểm tra tiền phạt (nếu có)\n• Tìm kiếm sách theo thể loại\n\nBạn cần hỗ trợ gì?";
                    break;
                case "ASSET_MANAGER":
                    msg = "Xin chào Cán bộ! 🔧\n\nTôi có thể giúp bạn:\n• Kiểm tra tình trạng thiết bị\n• Nhắc lịch bảo trì sắp đến\n• Thống kê thiết bị hỏng / cần sửa\n\nBạn muốn kiểm tra gì?";
                    break;
                default:
                    msg = "Xin chào Admin! 🛡️\n\nTôi có toàn quyền truy cập hệ thống:\n• Thống kê thư viện & thiết bị\n• Báo cáo tổng hợp\n• Cảnh báo bất thường\n\nBạn cần xem báo cáo gì hôm nay?";
                    break;
            }
            AppendAIBubble(msg);
        }

        // ═══════════════════════════════════════════════════════
        // SỰ KIỆN GỬI TIN NHẮN
        // ═══════════════════════════════════════════════════════
        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendMessage();
        }

        private void txtQuestion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                btnSend_Click(null, null);
            }
        }

        // Click vào gợi ý → tự điền vào ô nhập
        private void btnSuggest_Click(object sender, EventArgs e)
        {
            var btn = sender as Guna.UI2.WinForms.Guna2Button;
            if (btn == null) return;

            // Bỏ icon emoji ở đầu
            string text = btn.Text;
            if (text.Length > 2) text = text.Substring(2).Trim();
            txtQuestion.Text = text;
            txtQuestion.Focus();
        }

        private async Task SendMessage()
        {
            string question = txtQuestion.Text.Trim();
            if (string.IsNullOrEmpty(question)) return;

            // Hiện tin nhắn người dùng
            AppendUserBubble(question);
            txtQuestion.Clear();

            // Khóa input, hiện typing
            SetInputEnabled(false);
            pnlTypingIndicator.Visible = true;

            try
            {
                string answer = await AIChatService.AskAsync(question, _role, _readerID);
                pnlTypingIndicator.Visible = false;
                AppendAIBubble(answer);
            }
            catch (Exception ex)
            {
                pnlTypingIndicator.Visible = false;
                AppendAIBubble("⚠️ Không thể kết nối đến AI. Lỗi: " + ex.Message);
            }
            finally
            {
                SetInputEnabled(true);
                txtQuestion.Focus();
            }
        }

        private void SetInputEnabled(bool enabled)
        {
            btnSend.Enabled = enabled;
            txtQuestion.Enabled = enabled;
            btnSend.FillColor = enabled
                ? GetRoleColor()
                : Color.FromArgb(180, 185, 195);
        }

        // ═══════════════════════════════════════════════════════
        // VẼ BUBBLE TIN NHẮN VÀO RICHTEXTBOX
        // ═══════════════════════════════════════════════════════

        private void AppendUserBubble(string message)
        {
            string time = DateTime.Now.ToString("HH:mm");

            // Thêm khoảng cách
            AppendText("\n", Color.Transparent, 6);

            // Label "Bạn  HH:mm" — căn phải
            AppendText("                                                                    ", Color.Transparent, 4);
            AppendText($"Bạn  {time}\n", COLOR_LABEL_USER, 9, FontStyle.Bold);

            // Bubble nội dung (giả lập bằng indent + màu)
            AppendText("        ", Color.Transparent, 4); // indent trái
            AppendText($" {message} \n", COLOR_USER_TEXT, 11,
                       FontStyle.Regular, COLOR_USER_BG, true);

            rtbChatHistory.ScrollToCaret();
        }

        private void AppendAIBubble(string message)
        {
            string time = DateTime.Now.ToString("HH:mm");

            AppendText("\n", Color.Transparent, 6);

            // Label "🤖 AI  HH:mm"
            AppendText($"🤖 AI  {time}\n", COLOR_LABEL_AI, 9, FontStyle.Bold);

            // Bubble nội dung
            AppendText($" {message} \n", COLOR_AI_TEXT, 11,
                       FontStyle.Regular, COLOR_AI_BG, false);

            rtbChatHistory.ScrollToCaret();
        }

        /// <summary>
        /// Hàm helper: thêm text có màu chữ, font, màu nền vào RichTextBox
        /// </summary>
        private void AppendText(string text, Color foreColor, float fontSize,
                                FontStyle style = FontStyle.Regular,
                                Color? bgColor = null, bool isUser = false)
        {
            rtbChatHistory.SelectionStart = rtbChatHistory.TextLength;
            rtbChatHistory.SelectionLength = 0;

            rtbChatHistory.SelectionFont = new Font("Segoe UI", fontSize, style);

            if (foreColor != Color.Transparent)
                rtbChatHistory.SelectionColor = foreColor;

            if (bgColor.HasValue)
                rtbChatHistory.SelectionBackColor = bgColor.Value;
            else
                rtbChatHistory.SelectionBackColor = Color.FromArgb(245, 247, 250);

            // Căn phải nếu là tin nhắn user
            rtbChatHistory.SelectionAlignment = isUser
                ? HorizontalAlignment.Right
                : HorizontalAlignment.Left;

            rtbChatHistory.AppendText(text);
        }

        // ═══════════════════════════════════════════════════════
        // HELPER: màu accent theo role
        // ═══════════════════════════════════════════════════════
        private Color GetRoleColor()
        {
            switch (_role)
            {
                case "LIBRARIAN": return Color.FromArgb(26, 75, 132);
                case "Reader": return Color.FromArgb(16, 137, 62);
                case "ASSET_MANAGER": return Color.FromArgb(162, 84, 18);
                default: return Color.FromArgb(0, 120, 212);
            }
        }
    }
}