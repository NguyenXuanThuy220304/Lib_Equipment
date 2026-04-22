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
            this.pnlChat = new System.Windows.Forms.Panel();
            this.pnlScrollArea = new System.Windows.Forms.Panel();
            this.rtbChatHistory = new System.Windows.Forms.RichTextBox();
            this.pnlTypingIndicator = new System.Windows.Forms.Panel();
            this.lblTyping = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderDot = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblHeaderSub = new System.Windows.Forms.Label();
            this.pnlInputArea = new System.Windows.Forms.Panel();
            this.pnlInputBox = new Guna.UI2.WinForms.Guna2Panel();
            this.txtQuestion = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSend = new Guna.UI2.WinForms.Guna2Button();
            this.lblHint = new System.Windows.Forms.Label();
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
            this.pnlMain.SuspendLayout();
            this.pnlChat.SuspendLayout();
            this.pnlScrollArea.SuspendLayout();
            this.pnlTypingIndicator.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlInputArea.SuspendLayout();
            this.pnlInputBox.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlAvatarWrap.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlMain.Controls.Add(this.pnlChat);
            this.pnlMain.Controls.Add(this.pnlSidebar);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1200, 760);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlChat
            // 
            this.pnlChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlChat.Controls.Add(this.pnlScrollArea);
            this.pnlChat.Controls.Add(this.pnlHeader);
            this.pnlChat.Controls.Add(this.pnlInputArea);
            this.pnlChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChat.Location = new System.Drawing.Point(260, 0);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(940, 760);
            this.pnlChat.TabIndex = 0;
            // 
            // pnlScrollArea
            // 
            this.pnlScrollArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlScrollArea.Controls.Add(this.rtbChatHistory);
            this.pnlScrollArea.Controls.Add(this.pnlTypingIndicator);
            this.pnlScrollArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScrollArea.Location = new System.Drawing.Point(0, 72);
            this.pnlScrollArea.Name = "pnlScrollArea";
            this.pnlScrollArea.Padding = new System.Windows.Forms.Padding(20, 16, 20, 0);
            this.pnlScrollArea.Size = new System.Drawing.Size(940, 598);
            this.pnlScrollArea.TabIndex = 0;
            // 
            // rtbChatHistory
            // 
            this.rtbChatHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.rtbChatHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbChatHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbChatHistory.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rtbChatHistory.Location = new System.Drawing.Point(20, 16);
            this.rtbChatHistory.Name = "rtbChatHistory";
            this.rtbChatHistory.ReadOnly = true;
            this.rtbChatHistory.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbChatHistory.Size = new System.Drawing.Size(900, 546);
            this.rtbChatHistory.TabIndex = 0;
            this.rtbChatHistory.Text = "";
            // 
            // pnlTypingIndicator
            // 
            this.pnlTypingIndicator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlTypingIndicator.Controls.Add(this.lblTyping);
            this.pnlTypingIndicator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTypingIndicator.Location = new System.Drawing.Point(20, 562);
            this.pnlTypingIndicator.Name = "pnlTypingIndicator";
            this.pnlTypingIndicator.Size = new System.Drawing.Size(900, 36);
            this.pnlTypingIndicator.TabIndex = 1;
            this.pnlTypingIndicator.Visible = false;
            // 
            // lblTyping
            // 
            this.lblTyping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTyping.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblTyping.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.lblTyping.Location = new System.Drawing.Point(0, 0);
            this.lblTyping.Name = "lblTyping";
            this.lblTyping.Size = new System.Drawing.Size(900, 36);
            this.lblTyping.TabIndex = 0;
            this.lblTyping.Text = "  🤖  AI đang soạn câu trả lời...";
            this.lblTyping.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblHeaderDot);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Controls.Add(this.lblHeaderSub);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 0, 24, 0);
            this.pnlHeader.Size = new System.Drawing.Size(940, 72);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblHeaderDot
            // 
            this.lblHeaderDot.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblHeaderDot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(211)))), ((int)(((byte)(153)))));
            this.lblHeaderDot.Location = new System.Drawing.Point(24, 0);
            this.lblHeaderDot.Name = "lblHeaderDot";
            this.lblHeaderDot.Size = new System.Drawing.Size(24, 72);
            this.lblHeaderDot.TabIndex = 0;
            this.lblHeaderDot.Text = "●";
            this.lblHeaderDot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(24)))), ((int)(((byte)(38)))));
            this.lblHeaderTitle.Location = new System.Drawing.Point(54, 8);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(400, 42);
            this.lblHeaderTitle.TabIndex = 1;
            this.lblHeaderTitle.Text = "Trợ Lý AI UNETI";
            this.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblHeaderSub
            // 
            this.lblHeaderSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(140)))), ((int)(((byte)(160)))));
            this.lblHeaderSub.Location = new System.Drawing.Point(56, 44);
            this.lblHeaderSub.Name = "lblHeaderSub";
            this.lblHeaderSub.Size = new System.Drawing.Size(500, 22);
            this.lblHeaderSub.TabIndex = 2;
            this.lblHeaderSub.Text = "Hỏi về sách, thiết bị, thống kê — tôi luôn sẵn sàng hỗ trợ";
            // 
            // pnlInputArea
            // 
            this.pnlInputArea.BackColor = System.Drawing.Color.White;
            this.pnlInputArea.Controls.Add(this.pnlInputBox);
            this.pnlInputArea.Controls.Add(this.lblHint);
            this.pnlInputArea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInputArea.Location = new System.Drawing.Point(0, 670);
            this.pnlInputArea.Name = "pnlInputArea";
            this.pnlInputArea.Padding = new System.Windows.Forms.Padding(20, 14, 20, 8);
            this.pnlInputArea.Size = new System.Drawing.Size(940, 90);
            this.pnlInputArea.TabIndex = 2;
            // 
            // pnlInputBox
            // 
            this.pnlInputBox.BorderRadius = 24;
            this.pnlInputBox.Controls.Add(this.txtQuestion);
            this.pnlInputBox.Controls.Add(this.btnSend);
            this.pnlInputBox.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.pnlInputBox.CustomBorderThickness = new System.Windows.Forms.Padding(2);
            this.pnlInputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInputBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlInputBox.Location = new System.Drawing.Point(20, 14);
            this.pnlInputBox.Name = "pnlInputBox";
            this.pnlInputBox.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.pnlInputBox.Size = new System.Drawing.Size(900, 50);
            this.pnlInputBox.TabIndex = 0;
            // 
            // txtQuestion
            // 
            this.txtQuestion.BorderColor = System.Drawing.Color.Transparent;
            this.txtQuestion.BorderRadius = 20;
            this.txtQuestion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtQuestion.DefaultText = "";
            this.txtQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuestion.FillColor = System.Drawing.Color.WhiteSmoke;
            this.txtQuestion.FocusedState.BorderColor = System.Drawing.Color.Transparent;
            this.txtQuestion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtQuestion.Location = new System.Drawing.Point(8, 0);
            this.txtQuestion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtQuestion.PlaceholderText = "Nhập câu hỏi của bạn tại đây...";
            this.txtQuestion.SelectedText = "";
            this.txtQuestion.Size = new System.Drawing.Size(774, 50);
            this.txtQuestion.TabIndex = 0;
            this.txtQuestion.TextOffset = new System.Drawing.Point(8, 0);
            this.txtQuestion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuestion_KeyDown);
            // 
            // btnSend
            // 
            this.btnSend.BorderRadius = 18;
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(782, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(110, 50);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Gửi  ▶";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblHint
            // 
            this.lblHint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(185)))), ((int)(((byte)(195)))));
            this.lblHint.Location = new System.Drawing.Point(20, 64);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(900, 18);
            this.lblHint.TabIndex = 1;
            this.lblHint.Text = "Nhấn Enter để gửi  •  Shift+Enter xuống dòng";
            this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(85)))), ((int)(((byte)(142)))));
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
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.pnlSidebar.Size = new System.Drawing.Size(260, 760);
            this.pnlSidebar.TabIndex = 1;
            // 
            // pnlAvatarWrap
            // 
            this.pnlAvatarWrap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.pnlAvatarWrap.Controls.Add(this.lblAvatarIcon);
            this.pnlAvatarWrap.Location = new System.Drawing.Point(94, 30);
            this.pnlAvatarWrap.Name = "pnlAvatarWrap";
            this.pnlAvatarWrap.Size = new System.Drawing.Size(72, 72);
            this.pnlAvatarWrap.TabIndex = 0;
            // 
            // lblAvatarIcon
            // 
            this.lblAvatarIcon.BackColor = System.Drawing.Color.Transparent;
            this.lblAvatarIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvatarIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 28F);
            this.lblAvatarIcon.ForeColor = System.Drawing.Color.White;
            this.lblAvatarIcon.Location = new System.Drawing.Point(0, 0);
            this.lblAvatarIcon.Name = "lblAvatarIcon";
            this.lblAvatarIcon.Size = new System.Drawing.Size(72, 72);
            this.lblAvatarIcon.TabIndex = 0;
            this.lblAvatarIcon.Text = "🤖";
            this.lblAvatarIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRoleName
            // 
            this.lblRoleName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblRoleName.ForeColor = System.Drawing.Color.White;
            this.lblRoleName.Location = new System.Drawing.Point(20, 115);
            this.lblRoleName.Name = "lblRoleName";
            this.lblRoleName.Size = new System.Drawing.Size(220, 35);
            this.lblRoleName.TabIndex = 1;
            this.lblRoleName.Text = "UNETI AI";
            this.lblRoleName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRoleBadge
            // 
            this.lblRoleBadge.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRoleBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(211)))), ((int)(((byte)(153)))));
            this.lblRoleBadge.Location = new System.Drawing.Point(20, 150);
            this.lblRoleBadge.Name = "lblRoleBadge";
            this.lblRoleBadge.Size = new System.Drawing.Size(220, 22);
            this.lblRoleBadge.TabIndex = 2;
            this.lblRoleBadge.Text = "● Đang hoạt động";
            this.lblRoleBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlDivider1
            // 
            this.pnlDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.pnlDivider1.Location = new System.Drawing.Point(20, 185);
            this.pnlDivider1.Name = "pnlDivider1";
            this.pnlDivider1.Size = new System.Drawing.Size(220, 1);
            this.pnlDivider1.TabIndex = 3;
            // 
            // lblSuggestTitle
            // 
            this.lblSuggestTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSuggestTitle.ForeColor = System.Drawing.Color.White;
            this.lblSuggestTitle.Location = new System.Drawing.Point(20, 200);
            this.lblSuggestTitle.Name = "lblSuggestTitle";
            this.lblSuggestTitle.Size = new System.Drawing.Size(220, 20);
            this.lblSuggestTitle.TabIndex = 4;
            this.lblSuggestTitle.Text = "GỢI Ý CÂU HỎI";
            // 
            // btnSuggest1
            // 
            this.btnSuggest1.BorderRadius = 10;
            this.btnSuggest1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.btnSuggest1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnSuggest1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(230)))));
            this.btnSuggest1.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnSuggest1.Location = new System.Drawing.Point(20, 228);
            this.btnSuggest1.Name = "btnSuggest1";
            this.btnSuggest1.Size = new System.Drawing.Size(220, 42);
            this.btnSuggest1.TabIndex = 5;
            this.btnSuggest1.Text = "Sách đang mượn nhiều nhất?";
            this.btnSuggest1.Click += new System.EventHandler(this.btnSuggest_Click);
            // 
            // btnSuggest2
            // 
            this.btnSuggest2.BorderRadius = 10;
            this.btnSuggest2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(58)))));
            this.btnSuggest2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnSuggest2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(230)))));
            this.btnSuggest2.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnSuggest2.Location = new System.Drawing.Point(20, 276);
            this.btnSuggest2.Name = "btnSuggest2";
            this.btnSuggest2.Size = new System.Drawing.Size(220, 42);
            this.btnSuggest2.TabIndex = 6;
            this.btnSuggest2.Text = "Độc giả đang quá hạn?";
            this.btnSuggest2.Click += new System.EventHandler(this.btnSuggest_Click);
            // 
            // btnSuggest3
            // 
            this.btnSuggest3.BorderRadius = 10;
            this.btnSuggest3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(58)))));
            this.btnSuggest3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnSuggest3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(230)))));
            this.btnSuggest3.ImageOffset = new System.Drawing.Point(8, 0);
            this.btnSuggest3.Location = new System.Drawing.Point(20, 324);
            this.btnSuggest3.Name = "btnSuggest3";
            this.btnSuggest3.Size = new System.Drawing.Size(220, 42);
            this.btnSuggest3.TabIndex = 7;
            this.btnSuggest3.Text = "Thiết bị cần bảo trì?";
            this.btnSuggest3.Click += new System.EventHandler(this.btnSuggest_Click);
            // 
            // pnlDivider2
            // 
            this.pnlDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.pnlDivider2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDivider2.Location = new System.Drawing.Point(20, 699);
            this.pnlDivider2.Name = "pnlDivider2";
            this.pnlDivider2.Size = new System.Drawing.Size(220, 1);
            this.pnlDivider2.TabIndex = 8;
            // 
            // lblModelInfo
            // 
            this.lblModelInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblModelInfo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblModelInfo.ForeColor = System.Drawing.Color.White;
            this.lblModelInfo.Location = new System.Drawing.Point(20, 700);
            this.lblModelInfo.Name = "lblModelInfo";
            this.lblModelInfo.Size = new System.Drawing.Size(220, 40);
            this.lblModelInfo.TabIndex = 9;
            this.lblModelInfo.Text = "⚡ Gemini 2.0 Flash  |  UNETI v1.0";
            this.lblModelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmTroLyAI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(24)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTroLyAI";
            this.Text = "Trợ Lý AI UNETI";
            this.pnlMain.ResumeLayout(false);
            this.pnlChat.ResumeLayout(false);
            this.pnlScrollArea.ResumeLayout(false);
            this.pnlTypingIndicator.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlInputArea.ResumeLayout(false);
            this.pnlInputBox.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlAvatarWrap.ResumeLayout(false);
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