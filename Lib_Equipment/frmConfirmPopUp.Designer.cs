namespace Lib_Equipment
{
    partial class frmConfirmPopUp
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblAiResponse = new System.Windows.Forms.Label();
            this.lblBookInfo = new System.Windows.Forms.Label();
            this.btnXacNhan = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 10;
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2ShadowForm1
            // 
            this.guna2ShadowForm1.TargetForm = this;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(480, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(0, 0);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(480, 50);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "🤖 TRỢ LÝ AI XÁC NHẬN";
            this.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAiResponse
            // 
            this.lblAiResponse.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Italic);
            this.lblAiResponse.ForeColor = System.Drawing.Color.Black;
            this.lblAiResponse.Location = new System.Drawing.Point(20, 65);
            this.lblAiResponse.Name = "lblAiResponse";
            this.lblAiResponse.Size = new System.Drawing.Size(440, 75);
            this.lblAiResponse.TabIndex = 1;
            this.lblAiResponse.Text = "\"Lời chào từ AI sẽ nằm ở đây...\"";
            this.lblAiResponse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBookInfo
            // 
            this.lblBookInfo.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblBookInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lblBookInfo.Location = new System.Drawing.Point(20, 150);
            this.lblBookInfo.Name = "lblBookInfo";
            this.lblBookInfo.Size = new System.Drawing.Size(440, 50);
            this.lblBookInfo.TabIndex = 2;
            this.lblBookInfo.Text = "📚 Cuốn sách: \r\n🆔 Mã vạch: ";
            this.lblBookInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BorderRadius = 8;
            this.btnXacNhan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.HoverState.FillColor = System.Drawing.Color.Green;
            this.btnXacNhan.Location = new System.Drawing.Point(24, 220);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(200, 45);
            this.btnXacNhan.TabIndex = 3;
            this.btnXacNhan.Text = "XÁC NHẬN MƯỢN";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 8;
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.HoverState.FillColor = System.Drawing.Color.Red;
            this.btnHuy.Location = new System.Drawing.Point(256, 220);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(200, 45);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "HỦY BỎ";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmConfirmPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 290);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.lblBookInfo);
            this.Controls.Add(this.lblAiResponse);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfirmPopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xác nhận";
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblAiResponse;
        private System.Windows.Forms.Label lblBookInfo;
        private Guna.UI2.WinForms.Guna2Button btnXacNhan;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
    }
}