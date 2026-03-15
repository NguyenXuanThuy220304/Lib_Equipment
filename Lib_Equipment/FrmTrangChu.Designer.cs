namespace Lib_Equipment
{
    partial class FrmTrangChu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTrangChu));
            this.pnlWelcome = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblClock = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblSchoolName = new System.Windows.Forms.Label();
            this.pnlLibInfo = new System.Windows.Forms.Panel();
            this.lblLibDesc = new System.Windows.Forms.Label();
            this.lblLibTitle = new System.Windows.Forms.Label();
            this.pnlEquipInfo = new System.Windows.Forms.Panel();
            this.lblEquipDesc = new System.Windows.Forms.Label();
            this.lblEquipTitle = new System.Windows.Forms.Label();
            this.pnlWelcome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlLibInfo.SuspendLayout();
            this.pnlEquipInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWelcome
            // 
            this.pnlWelcome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.pnlWelcome.Controls.Add(this.lblDate);
            this.pnlWelcome.Controls.Add(this.lblClock);
            this.pnlWelcome.Controls.Add(this.lblSubTitle);
            this.pnlWelcome.Controls.Add(this.lblWelcome);
            this.pnlWelcome.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlWelcome.Location = new System.Drawing.Point(0, 0);
            this.pnlWelcome.Margin = new System.Windows.Forms.Padding(4);
            this.pnlWelcome.Name = "pnlWelcome";
            this.pnlWelcome.Size = new System.Drawing.Size(1333, 185);
            this.pnlWelcome.TabIndex = 0;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.lblDate.Location = new System.Drawing.Point(987, 117);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(249, 28);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Thứ Hai, 01 Tháng 01, 2026";
            // 
            // lblClock
            // 
            this.lblClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClock.AutoSize = true;
            this.lblClock.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblClock.ForeColor = System.Drawing.Color.White;
            this.lblClock.Location = new System.Drawing.Point(973, 37);
            this.lblClock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(246, 72);
            this.lblClock.TabIndex = 2;
            this.lblClock.Text = "00:00:00";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.LightGray;
            this.lblSubTitle.Location = new System.Drawing.Point(47, 105);
            this.lblSubTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(571, 32);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Bộ phận Quản lý Cơ sở vật chất và Học liệu (Nội bộ)";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(40, 43);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(796, 54);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN & THIẾT BỊ";
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // picLogo
            // 
            this.picLogo.Image = global::Lib_Equipment.Properties.Resources.logo_u;
            this.picLogo.Location = new System.Drawing.Point(107, 271);
            this.picLogo.Margin = new System.Windows.Forms.Padding(4);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(346, 320);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 1;
            this.picLogo.TabStop = false;
            // 
            // lblSchoolName
            // 
            this.lblSchoolName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblSchoolName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.lblSchoolName.Location = new System.Drawing.Point(53, 615);
            this.lblSchoolName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSchoolName.Name = "lblSchoolName";
            this.lblSchoolName.Size = new System.Drawing.Size(453, 86);
            this.lblSchoolName.TabIndex = 2;
            this.lblSchoolName.Text = "TRƯỜNG ĐẠI HỌC KINH TẾ KỸ THUẬT CÔNG NGHIỆP";
            this.lblSchoolName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLibInfo
            // 
            this.pnlLibInfo.BackColor = System.Drawing.Color.White;
            this.pnlLibInfo.Controls.Add(this.lblLibDesc);
            this.pnlLibInfo.Controls.Add(this.lblLibTitle);
            this.pnlLibInfo.Location = new System.Drawing.Point(560, 246);
            this.pnlLibInfo.Margin = new System.Windows.Forms.Padding(4);
            this.pnlLibInfo.Name = "pnlLibInfo";
            this.pnlLibInfo.Size = new System.Drawing.Size(707, 234);
            this.pnlLibInfo.TabIndex = 3;
            // 
            // lblLibDesc
            // 
            this.lblLibDesc.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblLibDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLibDesc.Location = new System.Drawing.Point(27, 86);
            this.lblLibDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLibDesc.Name = "lblLibDesc";
            this.lblLibDesc.Size = new System.Drawing.Size(653, 123);
            this.lblLibDesc.TabIndex = 1;
            this.lblLibDesc.Text = resources.GetString("lblLibDesc.Text");
            // 
            // lblLibTitle
            // 
            this.lblLibTitle.AutoSize = true;
            this.lblLibTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblLibTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblLibTitle.Location = new System.Drawing.Point(20, 25);
            this.lblLibTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLibTitle.Name = "lblLibTitle";
            this.lblLibTitle.Size = new System.Drawing.Size(385, 37);
            this.lblLibTitle.TabIndex = 0;
            this.lblLibTitle.Text = "📚 THÔNG TIN VỀ THƯ VIỆN";
            // 
            // pnlEquipInfo
            // 
            this.pnlEquipInfo.BackColor = System.Drawing.Color.White;
            this.pnlEquipInfo.Controls.Add(this.lblEquipDesc);
            this.pnlEquipInfo.Controls.Add(this.lblEquipTitle);
            this.pnlEquipInfo.Location = new System.Drawing.Point(560, 529);
            this.pnlEquipInfo.Margin = new System.Windows.Forms.Padding(4);
            this.pnlEquipInfo.Name = "pnlEquipInfo";
            this.pnlEquipInfo.Size = new System.Drawing.Size(707, 234);
            this.pnlEquipInfo.TabIndex = 4;
            // 
            // lblEquipDesc
            // 
            this.lblEquipDesc.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblEquipDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEquipDesc.Location = new System.Drawing.Point(27, 86);
            this.lblEquipDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEquipDesc.Name = "lblEquipDesc";
            this.lblEquipDesc.Size = new System.Drawing.Size(653, 123);
            this.lblEquipDesc.TabIndex = 2;
            this.lblEquipDesc.Text = resources.GetString("lblEquipDesc.Text");
            // 
            // lblEquipTitle
            // 
            this.lblEquipTitle.AutoSize = true;
            this.lblEquipTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblEquipTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.lblEquipTitle.Location = new System.Drawing.Point(20, 25);
            this.lblEquipTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEquipTitle.Name = "lblEquipTitle";
            this.lblEquipTitle.Size = new System.Drawing.Size(428, 37);
            this.lblEquipTitle.TabIndex = 1;
            this.lblEquipTitle.Text = "⚙ THÔNG TIN TRANG THIẾT BỊ";
            // 
            // FrmTrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1333, 862);
            this.Controls.Add(this.pnlEquipInfo);
            this.Controls.Add(this.pnlLibInfo);
            this.Controls.Add(this.lblSchoolName);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.pnlWelcome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmTrangChu";
            this.Text = "Trang Chủ";
            this.pnlWelcome.ResumeLayout(false);
            this.pnlWelcome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlLibInfo.ResumeLayout(false);
            this.pnlLibInfo.PerformLayout();
            this.pnlEquipInfo.ResumeLayout(false);
            this.pnlEquipInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlWelcome;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblSchoolName;
        private System.Windows.Forms.Panel pnlLibInfo;
        private System.Windows.Forms.Label lblLibDesc;
        private System.Windows.Forms.Label lblLibTitle;
        private System.Windows.Forms.Panel pnlEquipInfo;
        private System.Windows.Forms.Label lblEquipDesc;
        private System.Windows.Forms.Label lblEquipTitle;
    }
}