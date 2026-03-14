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
            this.pnlWelcome.Name = "pnlWelcome";
            this.pnlWelcome.Size = new System.Drawing.Size(1000, 150);
            this.pnlWelcome.TabIndex = 0;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.lblDate.Location = new System.Drawing.Point(740, 95);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(200, 21);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Thứ Hai, 01 Tháng 01, 2026";
            // 
            // lblClock
            // 
            this.lblClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClock.AutoSize = true;
            this.lblClock.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblClock.ForeColor = System.Drawing.Color.White;
            this.lblClock.Location = new System.Drawing.Point(730, 30);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(200, 59);
            this.lblClock.TabIndex = 2;
            this.lblClock.Text = "00:00:00";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.LightGray;
            this.lblSubTitle.Location = new System.Drawing.Point(35, 85);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(465, 25);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Bộ phận Quản lý Cơ sở vật chất và Học liệu (Nội bộ)";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(30, 35);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(650, 45);
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
            this.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLogo.Location = new System.Drawing.Point(80, 220);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(260, 260);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 1;
            this.picLogo.TabStop = false;
            // 
            // lblSchoolName
            // 
            this.lblSchoolName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblSchoolName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.lblSchoolName.Location = new System.Drawing.Point(40, 500);
            this.lblSchoolName.Name = "lblSchoolName";
            this.lblSchoolName.Size = new System.Drawing.Size(340, 70);
            this.lblSchoolName.TabIndex = 2;
            this.lblSchoolName.Text = "TRƯỜNG ĐẠI HỌC KINH TẾ KỸ THUẬT CÔNG NGHIỆP";
            this.lblSchoolName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLibInfo
            // 
            this.pnlLibInfo.BackColor = System.Drawing.Color.White;
            this.pnlLibInfo.Controls.Add(this.lblLibDesc);
            this.pnlLibInfo.Controls.Add(this.lblLibTitle);
            this.pnlLibInfo.Location = new System.Drawing.Point(420, 200);
            this.pnlLibInfo.Name = "pnlLibInfo";
            this.pnlLibInfo.Size = new System.Drawing.Size(530, 190);
            this.pnlLibInfo.TabIndex = 3;
            // 
            // lblLibDesc
            // 
            this.lblLibDesc.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblLibDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLibDesc.Location = new System.Drawing.Point(20, 70);
            this.lblLibDesc.Name = "lblLibDesc";
            this.lblLibDesc.Size = new System.Drawing.Size(490, 100);
            this.lblLibDesc.TabIndex = 1;
            this.lblLibDesc.Text = "Hệ thống cung cấp nền tảng số hóa toàn diện để quản lý hàng ngàn đầu sách giáo trình, tài liệu tham khảo.\r\n\r\n- Hỗ trợ mượn trả sách nhanh chóng qua mã vạch.\r\n- Theo dõi và tự động cảnh báo sinh viên nợ phạt quá hạn.";
            // 
            // lblLibTitle
            // 
            this.lblLibTitle.AutoSize = true;
            this.lblLibTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblLibTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblLibTitle.Location = new System.Drawing.Point(15, 20);
            this.lblLibTitle.Name = "lblLibTitle";
            this.lblLibTitle.Size = new System.Drawing.Size(306, 30);
            this.lblLibTitle.TabIndex = 0;
            this.lblLibTitle.Text = "📚 THÔNG TIN VỀ THƯ VIỆN";
            // 
            // pnlEquipInfo
            // 
            this.pnlEquipInfo.BackColor = System.Drawing.Color.White;
            this.pnlEquipInfo.Controls.Add(this.lblEquipDesc);
            this.pnlEquipInfo.Controls.Add(this.lblEquipTitle);
            this.pnlEquipInfo.Location = new System.Drawing.Point(420, 430);
            this.pnlEquipInfo.Name = "pnlEquipInfo";
            this.pnlEquipInfo.Size = new System.Drawing.Size(530, 190);
            this.pnlEquipInfo.TabIndex = 4;
            // 
            // lblEquipDesc
            // 
            this.lblEquipDesc.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblEquipDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEquipDesc.Location = new System.Drawing.Point(20, 70);
            this.lblEquipDesc.Name = "lblEquipDesc";
            this.lblEquipDesc.Size = new System.Drawing.Size(490, 100);
            this.lblEquipDesc.TabIndex = 2;
            this.lblEquipDesc.Text = "Số hóa quy trình quản lý vòng đời của tài sản, máy móc và thiết bị dạy học trong toàn trường.\r\n\r\n- Luân chuyển thiết bị giữa các Khoa/Viện dễ dàng.\r\n- Lưu vết lịch sử bảo trì, sửa chữa và lập báo cáo tài chính minh bạch.";
            // 
            // lblEquipTitle
            // 
            this.lblEquipTitle.AutoSize = true;
            this.lblEquipTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblEquipTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.lblEquipTitle.Location = new System.Drawing.Point(15, 20);
            this.lblEquipTitle.Name = "lblEquipTitle";
            this.lblEquipTitle.Size = new System.Drawing.Size(332, 30);
            this.lblEquipTitle.TabIndex = 1;
            this.lblEquipTitle.Text = "⚙ THÔNG TIN TRANG THIẾT BỊ";
            // 
            // FrmTrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.pnlEquipInfo);
            this.Controls.Add(this.pnlLibInfo);
            this.Controls.Add(this.lblSchoolName);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.pnlWelcome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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