namespace Lib_Equipment
{
    partial class FrmLandingPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.pnlBackground = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.lblFooter = new System.Windows.Forms.Label();
            this.pnlCenter = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlStats = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlNews = new Guna.UI2.WinForms.Guna2Panel();
            this.picLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.btnDangNhap = new Guna.UI2.WinForms.Guna2Button();
            this.pnlTopNav = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLuatThuVien = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuongDan = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.pnlBackground.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlTopNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // pnlBackground
            // 
            this.pnlBackground.BackColor = System.Drawing.Color.White;
            this.pnlBackground.BackgroundImage = global::Lib_Equipment.Properties.Resources.uneti_sl1;
            this.pnlBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlBackground.Controls.Add(this.lblFooter);
            this.pnlBackground.Controls.Add(this.pnlCenter);
            this.pnlBackground.Controls.Add(this.pnlTopNav);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlBackground.FillColor2 = System.Drawing.Color.White;
            this.pnlBackground.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.pnlBackground.Location = new System.Drawing.Point(0, 0);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(1920, 1080);
            this.pnlBackground.TabIndex = 0;
            // 
            // lblFooter
            // 
            this.lblFooter.BackColor = System.Drawing.Color.Transparent;
            this.lblFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic);
            this.lblFooter.ForeColor = System.Drawing.Color.Gray;
            this.lblFooter.Location = new System.Drawing.Point(0, 1030);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(1920, 50);
            this.lblFooter.TabIndex = 2;
            this.lblFooter.Text = "Phát triển bởi: Nhóm 34 - Sinh viên CNTT UNETI © 2026";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCenter
            // 
            this.pnlCenter.BackColor = System.Drawing.Color.Transparent;
            this.pnlCenter.Controls.Add(this.pnlStats);
            this.pnlCenter.Controls.Add(this.pnlNews);
            this.pnlCenter.Controls.Add(this.picLogo);
            this.pnlCenter.Controls.Add(this.lblTitle);
            this.pnlCenter.Controls.Add(this.lblSubTitle);
            this.pnlCenter.Controls.Add(this.btnDangNhap);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 80);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(1920, 1000);
            this.pnlCenter.TabIndex = 1;
            // 
            // pnlStats
            // 
            this.pnlStats.BackColor = System.Drawing.Color.Transparent;
            this.pnlStats.BorderRadius = 20;
            this.pnlStats.FillColor = System.Drawing.Color.White;
            this.pnlStats.Location = new System.Drawing.Point(1277, 483);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.ShadowDecoration.Depth = 5;
            this.pnlStats.ShadowDecoration.Enabled = true;
            this.pnlStats.Size = new System.Drawing.Size(506, 444);
            this.pnlStats.TabIndex = 6;
            // 
            // pnlNews
            // 
            this.pnlNews.BackColor = System.Drawing.Color.Transparent;
            this.pnlNews.BorderRadius = 20;
            this.pnlNews.FillColor = System.Drawing.Color.White;
            this.pnlNews.Location = new System.Drawing.Point(213, 483);
            this.pnlNews.Name = "pnlNews";
            this.pnlNews.ShadowDecoration.Depth = 5;
            this.pnlNews.ShadowDecoration.Enabled = true;
            this.pnlNews.Size = new System.Drawing.Size(1043, 444);
            this.pnlNews.TabIndex = 5;
            // 
            // picLogo
            // 
            this.picLogo.Image = global::Lib_Equipment.Properties.Resources.Logo_ĐH_Kinh_tế_Kỹ_thuật_Công_nghiệp___UNETI;
            this.picLogo.ImageRotate = 0F;
            this.picLogo.Location = new System.Drawing.Point(908, 6);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(200, 200);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 4;
            this.picLogo.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 30F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lblTitle.Location = new System.Drawing.Point(308, 216);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1400, 80);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN VÀ THIẾT BỊ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubTitle.Location = new System.Drawing.Point(308, 296);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(1400, 40);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Khám phá tri thức - Nâng tầm kỹ năng. Dành riêng cho sinh viên và giảng viên UNET" +
    "I.";
            this.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.BorderRadius = 35;
            this.btnDangNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangNhap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.btnDangNhap.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnDangNhap.Location = new System.Drawing.Point(758, 366);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(500, 89);
            this.btnDangNhap.TabIndex = 2;
            this.btnDangNhap.Text = "ĐĂNG NHẬP";
            // 
            // pnlTopNav
            // 
            this.pnlTopNav.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopNav.Controls.Add(this.btnLuatThuVien);
            this.pnlTopNav.Controls.Add(this.btnHuongDan);
            this.pnlTopNav.Controls.Add(this.guna2ControlBox1);
            this.pnlTopNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopNav.Location = new System.Drawing.Point(0, 0);
            this.pnlTopNav.Name = "pnlTopNav";
            this.pnlTopNav.Size = new System.Drawing.Size(1920, 80);
            this.pnlTopNav.TabIndex = 0;
            // 
            // btnLuatThuVien
            // 
            this.btnLuatThuVien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuatThuVien.BorderRadius = 20;
            this.btnLuatThuVien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuatThuVien.FillColor = System.Drawing.Color.White;
            this.btnLuatThuVien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLuatThuVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnLuatThuVien.Location = new System.Drawing.Point(1450, 15);
            this.btnLuatThuVien.Name = "btnLuatThuVien";
            this.btnLuatThuVien.Size = new System.Drawing.Size(200, 50);
            this.btnLuatThuVien.TabIndex = 2;
            this.btnLuatThuVien.Text = "📜 Nội quy";
            // 
            // btnHuongDan
            // 
            this.btnHuongDan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuongDan.BorderRadius = 20;
            this.btnHuongDan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuongDan.FillColor = System.Drawing.Color.White;
            this.btnHuongDan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnHuongDan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btnHuongDan.Location = new System.Drawing.Point(1650, 15);
            this.btnHuongDan.Name = "btnHuongDan";
            this.btnHuongDan.Size = new System.Drawing.Size(200, 50);
            this.btnHuongDan.TabIndex = 1;
            this.btnHuongDan.Text = "💡 Hướng dẫn";
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.HoverState.FillColor = System.Drawing.Color.Red;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.Gray;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1850, 15);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(50, 50);
            this.guna2ControlBox1.TabIndex = 0;
            // 
            // FrmLandingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.pnlBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLandingPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cổng thông tin UNETI";
            this.pnlBackground.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlTopNav.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2GradientPanel pnlBackground;
        private Guna.UI2.WinForms.Guna2Panel pnlTopNav;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2Button btnHuongDan;
        private Guna.UI2.WinForms.Guna2Button btnLuatThuVien;
        private Guna.UI2.WinForms.Guna2Panel pnlCenter;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private Guna.UI2.WinForms.Guna2Button btnDangNhap;
        private System.Windows.Forms.Label lblFooter;
        private Guna.UI2.WinForms.Guna2PictureBox picLogo;
        private Guna.UI2.WinForms.Guna2Panel pnlStats;
        private Guna.UI2.WinForms.Guna2Panel pnlNews;
    }
}