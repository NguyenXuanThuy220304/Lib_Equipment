namespace Lib_Equipment
{
    partial class FrmTroLyAI
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.rtbChatHistory = new System.Windows.Forms.RichTextBox();
            this.txtQuestion = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSend = new Guna.UI2.WinForms.Guna2Button();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(800, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(374, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "✨ TRỢ LÝ AI THƯ VIỆN UNETI";
            // 
            // rtbChatHistory
            // 
            this.rtbChatHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbChatHistory.BackColor = System.Drawing.Color.White;
            this.rtbChatHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbChatHistory.Font = new System.Drawing.Font("Segoe UI", 11.5F);
            this.rtbChatHistory.Location = new System.Drawing.Point(20, 80);
            this.rtbChatHistory.Name = "rtbChatHistory";
            this.rtbChatHistory.ReadOnly = true;
            this.rtbChatHistory.Size = new System.Drawing.Size(760, 420);
            this.rtbChatHistory.TabIndex = 1;
            this.rtbChatHistory.Text = "🤖 Trợ lý AI: Xin chào Thủ thư! Tôi có thể giúp gì cho bạn hôm nay? (Ví dụ: Hãy gợi ý cho tôi 5 cuốn sách về lập trình C#...)\n";
            // 
            // txtQuestion
            // 
            this.txtQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuestion.BorderRadius = 20;
            this.txtQuestion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtQuestion.DefaultText = "";
            this.txtQuestion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtQuestion.ForeColor = System.Drawing.Color.Black;
            this.txtQuestion.Location = new System.Drawing.Point(20, 520);
            this.txtQuestion.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.PlaceholderText = "Nhập câu hỏi của bạn tại đây...";
            this.txtQuestion.Size = new System.Drawing.Size(620, 50);
            this.txtQuestion.TabIndex = 2;
            this.txtQuestion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuestion_KeyDown);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.BorderRadius = 20;
            this.btnSend.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(650, 520);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(130, 50);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "GỬI (Enter)";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // FrmTroLyAI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.rtbChatHistory);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTroLyAI";
            this.Text = "Trợ lý AI";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.RichTextBox rtbChatHistory;
        private Guna.UI2.WinForms.Guna2TextBox txtQuestion;
        private Guna.UI2.WinForms.Guna2Button btnSend;
    }
}