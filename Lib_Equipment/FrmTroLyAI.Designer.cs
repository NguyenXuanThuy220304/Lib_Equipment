namespace Lib_Equipment
{
    partial class FrmTroLyAI
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlAvatarWrap = new System.Windows.Forms.Panel();
            this.lblAvatarIcon = new System.Windows.Forms.Label();
            this.lblRoleName = new System.Windows.Forms.Label();
            this.lblRoleBadge = new System.Windows.Forms.Label();
            this.pnlDivider1 = new System.Windows.Forms.Panel();
            this.lblSuggestTitle = new System.Windows.Forms.Label();
            this.btnSuggest1 = new Guna.UI2.WinForms.Guna2Button();
            this.btnSuggest2 = new Guna.UI2.WinForms.Guna2Button();
            this.btnSuggest3 = new Guna.UI2.WinForms.Guna2Button();
            this.pnlDivider2 = new System.Windows.Forms.Panel();
            this.lblModelInfo = new System.Windows.Forms.Label();
            this.pnlChat = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderDot = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblHeaderSub = new System.Windows.Forms.Label();
            this.pnlScrollArea = new System.Windows.Forms.Panel();
            this.rtbChatHistory = new System.Windows.Forms.RichTextBox();
            this.pnlTypingIndicator = new System.Windows.Forms.Panel();
            this.lblTyping = new System.Windows.Forms.Label();
            this.pnlInputArea = new System.Windows.Forms.Panel();
            this.pnlInputBox = new Guna.UI2.WinForms.Guna2Panel();
            this.txtQuestion = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSend = new Guna.UI2.WinForms.Guna2Button();
            this.lblHint = new System.Windows.Forms.Label();

            this.pnlMain.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlChat.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlScrollArea.SuspendLayout();
            this.pnlTypingIndicator.SuspendLayout();
            this.pnlInputArea.SuspendLayout();
            this.pnlInputBox.SuspendLayout();
            this.SuspendLayout();

            // ── pnlMain (container tổng) ──────────────────────────────
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Controls.Add(this.pnlChat);
            this.pnlMain.Controls.Add(this.pnlSidebar);
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Size = new System.Drawing.Size(1200, 760);

            // ── pnlSidebar (cột trái 260px) ───────────────────────────
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(18, 24, 38);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Width = 260;
            this.pnlSidebar.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.pnlSidebar.Controls.Add(this.pnlAvatarWrap);
            this.pnlSidebar.Controls.Add(this.lblRoleName);
            this.pnlSidebar.Controls.Add(this.lblRoleBadge);
            this.pnlSidebar.Controls.Add(this.pnlDivider1);
            this.pnlSidebar.Controls.Add(this.lblSuggestTitle);
            this.pnlSidebar.Controls.Add(this.btnSuggest1);
            this.pnlSidebar.Controls.Add(this.btnSuggest2);
            this.pnlSidebar.Controls.Add(this.btnSuggest3);
            this.pnlSidebar.Controls.Add(this.pnlDivider2);
            this.pnlSidebar.Controls.Add(this.lblModelInfo);

            // Avatar circle
            this.pnlAvatarWrap.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.pnlAvatarWrap.Size = new System.Drawing.Size(72, 72);
            this.pnlAvatarWrap.Location = new System.Drawing.Point(94, 30);
            this.pnlAvatarWrap.Controls.Add(this.lblAvatarIcon);

            this.lblAvatarIcon.Text = "🤖";
            this.lblAvatarIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 28F);
            this.lblAvatarIcon.ForeColor = System.Drawing.Color.White;
            this.lblAvatarIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAvatarIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvatarIcon.BackColor = System.Drawing.Color.Transparent;

            // Tên vai trò
            this.lblRoleName.Text = "UNETI AI";
            this.lblRoleName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblRoleName.ForeColor = System.Drawing.Color.White;
            this.lblRoleName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRoleName.Size = new System.Drawing.Size(220, 35);
            this.lblRoleName.Location = new System.Drawing.Point(20, 115);
            this.lblRoleName.Name = "lblRoleName";

            // Badge vai trò
            this.lblRoleBadge.Text = "● Đang hoạt động";
            this.lblRoleBadge.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRoleBadge.ForeColor = System.Drawing.Color.FromArgb(52, 211, 153);
            this.lblRoleBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRoleBadge.Size = new System.Drawing.Size(220, 22);
            this.lblRoleBadge.Location = new System.Drawing.Point(20, 150);
            this.lblRoleBadge.Name = "lblRoleBadge";

            // Divider 1
            this.pnlDivider1.BackColor = System.Drawing.Color.FromArgb(40, 50, 70);
            this.pnlDivider1.Size = new System.Drawing.Size(220, 1);
            this.pnlDivider1.Location = new System.Drawing.Point(20, 185);

            // Label gợi ý
            this.lblSuggestTitle.Text = "GỢI Ý CÂU HỎI";
            this.lblSuggestTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSuggestTitle.ForeColor = System.Drawing.Color.FromArgb(100, 120, 150);
            this.lblSuggestTitle.Size = new System.Drawing.Size(220, 20);
            this.lblSuggestTitle.Location = new System.Drawing.Point(20, 200);

            // Buttons gợi ý
            System.Drawing.Point[] suggestLoc = {
                new System.Drawing.Point(20, 228),
                new System.Drawing.Point(20, 276),
                new System.Drawing.Point(20, 324)
            };
            Guna.UI2.WinForms.Guna2Button[] suggestBtns = { this.btnSuggest1, this.btnSuggest2, this.btnSuggest3 };
            string[] suggestTexts = {
                "📚 Sách đang mượn nhiều nhất?",
                "⚠️ Độc giả đang quá hạn?",
                "🔧 Thiết bị cần bảo trì?"
            };
            for (int i = 0; i < 3; i++)
            {
                suggestBtns[i].Text = suggestTexts[i];
                suggestBtns[i].Font = new System.Drawing.Font("Segoe UI", 9.5F);
                suggestBtns[i].ForeColor = System.Drawing.Color.FromArgb(180, 200, 230);
                suggestBtns[i].FillColor = System.Drawing.Color.FromArgb(30, 40, 58);
                suggestBtns[i].BorderRadius = 10;
                suggestBtns[i].Size = new System.Drawing.Size(220, 42);
                suggestBtns[i].Location = suggestLoc[i];
                suggestBtns[i].TextAlign = (System.Windows.Forms.HorizontalAlignment)System.Drawing.ContentAlignment.MiddleLeft;
                suggestBtns[i].ImageOffset = new System.Drawing.Point(8, 0);
            }
            this.btnSuggest1.Name = "btnSuggest1";
            this.btnSuggest2.Name = "btnSuggest2";
            this.btnSuggest3.Name = "btnSuggest3";
            this.btnSuggest1.Click += new System.EventHandler(this.btnSuggest_Click);
            this.btnSuggest2.Click += new System.EventHandler(this.btnSuggest_Click);
            this.btnSuggest3.Click += new System.EventHandler(this.btnSuggest_Click);

            // Divider 2
            this.pnlDivider2.BackColor = System.Drawing.Color.FromArgb(40, 50, 70);
            this.pnlDivider2.Size = new System.Drawing.Size(220, 1);
            this.pnlDivider2.Location = new System.Drawing.Point(20, 660);

            // Model info
            this.lblModelInfo.Text = "⚡ Gemini 2.0 Flash  |  UNETI v1.0";
            this.lblModelInfo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblModelInfo.ForeColor = System.Drawing.Color.FromArgb(70, 90, 120);
            this.lblModelInfo.Size = new System.Drawing.Size(220, 40);
            this.lblModelInfo.Location = new System.Drawing.Point(20, 672);
            this.lblModelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── pnlChat (phần phải) ───────────────────────────────────
            this.pnlChat.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.pnlChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChat.Controls.Add(this.pnlScrollArea);
            this.pnlChat.Controls.Add(this.pnlHeader);
            this.pnlChat.Controls.Add(this.pnlInputArea);

            // ── pnlHeader ─────────────────────────────────────────────
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 72;
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 0, 24, 0);
            this.pnlHeader.Controls.Add(this.lblHeaderDot);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Controls.Add(this.lblHeaderSub);

            this.lblHeaderDot.Text = "●";
            this.lblHeaderDot.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblHeaderDot.ForeColor = System.Drawing.Color.FromArgb(52, 211, 153);
            this.lblHeaderDot.Size = new System.Drawing.Size(24, 72);
            this.lblHeaderDot.Location = new System.Drawing.Point(24, 0);
            this.lblHeaderDot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblHeaderTitle.Text = "Trợ Lý AI UNETI";
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(18, 24, 38);
            this.lblHeaderTitle.Size = new System.Drawing.Size(400, 42);
            this.lblHeaderTitle.Location = new System.Drawing.Point(54, 8);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            this.lblHeaderSub.Text = "Hỏi về sách, thiết bị, thống kê — tôi luôn sẵn sàng hỗ trợ";
            this.lblHeaderSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(130, 140, 160);
            this.lblHeaderSub.Size = new System.Drawing.Size(500, 22);
            this.lblHeaderSub.Location = new System.Drawing.Point(56, 44);
            this.lblHeaderSub.TextAlign = System.Drawing.ContentAlignment.TopLeft;

            // ── pnlScrollArea (khu vực chat) ──────────────────────────
            this.pnlScrollArea.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.pnlScrollArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScrollArea.Padding = new System.Windows.Forms.Padding(20, 16, 20, 0);
            this.pnlScrollArea.Controls.Add(this.rtbChatHistory);
            this.pnlScrollArea.Controls.Add(this.pnlTypingIndicator);

            // rtbChatHistory
            this.rtbChatHistory.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.rtbChatHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbChatHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbChatHistory.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rtbChatHistory.ReadOnly = true;
            this.rtbChatHistory.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbChatHistory.Name = "rtbChatHistory";
            this.rtbChatHistory.TabIndex = 0;
            this.rtbChatHistory.Text = "";

            // pnlTypingIndicator
            this.pnlTypingIndicator.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.pnlTypingIndicator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTypingIndicator.Height = 36;
            this.pnlTypingIndicator.Visible = false;
            this.pnlTypingIndicator.Controls.Add(this.lblTyping);

            this.lblTyping.Text = "  🤖  AI đang soạn câu trả lời...";
            this.lblTyping.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblTyping.ForeColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.lblTyping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTyping.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── pnlInputArea ──────────────────────────────────────────
            this.pnlInputArea.BackColor = System.Drawing.Color.White;
            this.pnlInputArea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInputArea.Height = 90;
            this.pnlInputArea.Padding = new System.Windows.Forms.Padding(20, 14, 20, 8);
            this.pnlInputArea.Controls.Add(this.pnlInputBox);
            this.pnlInputArea.Controls.Add(this.lblHint);

            // pnlInputBox (khung input bo tròn)
            this.pnlInputBox.FillColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.pnlInputBox.BorderRadius = 24;
            this.pnlInputBox.CustomBorderColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.pnlInputBox.CustomBorderThickness = new System.Windows.Forms.Padding(2);
            this.pnlInputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInputBox.Height = 52;
            this.pnlInputBox.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.pnlInputBox.Controls.Add(this.txtQuestion);
            this.pnlInputBox.Controls.Add(this.btnSend);

            // txtQuestion
            this.txtQuestion.BorderRadius = 0;
            this.txtQuestion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtQuestion.DefaultText = "";
            this.txtQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuestion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtQuestion.FillColor = System.Drawing.Color.Transparent;
            this.txtQuestion.BorderColor = System.Drawing.Color.Transparent;
            this.txtQuestion.FocusedState.BorderColor = System.Drawing.Color.Transparent;
            this.txtQuestion.PlaceholderText = "Nhập câu hỏi của bạn tại đây...";
            this.txtQuestion.SelectedText = "";
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.TabIndex = 0;
            this.txtQuestion.TextOffset = new System.Drawing.Point(8, 0);
            this.txtQuestion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuestion_KeyDown);

            // btnSend
            this.btnSend.BorderRadius = 18;
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.FillColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Width = 110;
            this.btnSend.Text = "Gửi  ▶";
            this.btnSend.Name = "btnSend";
            this.btnSend.TabIndex = 1;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // lblHint
            this.lblHint.Text = "Nhấn Enter để gửi  •  Shift+Enter xuống dòng";
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(180, 185, 195);
            this.lblHint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblHint.Height = 18;
            this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── FrmTroLyAI ────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(18, 24, 38);
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTroLyAI";
            this.Text = "Trợ Lý AI UNETI";

            this.pnlMain.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlChat.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlScrollArea.ResumeLayout(false);
            this.pnlTypingIndicator.ResumeLayout(false);
            this.pnlInputArea.ResumeLayout(false);
            this.pnlInputBox.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // Controls
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlAvatarWrap;
        private System.Windows.Forms.Label lblAvatarIcon;
        private System.Windows.Forms.Label lblRoleName;
        private System.Windows.Forms.Label lblRoleBadge;
        private System.Windows.Forms.Panel pnlDivider1;
        private System.Windows.Forms.Label lblSuggestTitle;
        private Guna.UI2.WinForms.Guna2Button btnSuggest1;
        private Guna.UI2.WinForms.Guna2Button btnSuggest2;
        private Guna.UI2.WinForms.Guna2Button btnSuggest3;
        private System.Windows.Forms.Panel pnlDivider2;
        private System.Windows.Forms.Label lblModelInfo;
        private System.Windows.Forms.Panel pnlChat;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderDot;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSub;
        private System.Windows.Forms.Panel pnlScrollArea;
        private System.Windows.Forms.RichTextBox rtbChatHistory;
        private System.Windows.Forms.Panel pnlTypingIndicator;
        private System.Windows.Forms.Label lblTyping;
        private System.Windows.Forms.Panel pnlInputArea;
        private Guna.UI2.WinForms.Guna2Panel pnlInputBox;
        private Guna.UI2.WinForms.Guna2TextBox txtQuestion;
        private Guna.UI2.WinForms.Guna2Button btnSend;
        private System.Windows.Forms.Label lblHint;
    }
}