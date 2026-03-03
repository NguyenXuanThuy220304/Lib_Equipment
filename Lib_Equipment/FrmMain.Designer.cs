namespace Lib_Equipment
{
    partial class FrmMain
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
            this.pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDangXuat = new Guna.UI2.WinForms.Guna2Button();
            this.btnBaoCao = new Guna.UI2.WinForms.Guna2Button();
            this.btnThietBi = new Guna.UI2.WinForms.Guna2Button();
            this.btnThuVien = new Guna.UI2.WinForms.Guna2Button();
            this.btnHeThong = new Guna.UI2.WinForms.Guna2Button();
            this.btnTrangChu = new Guna.UI2.WinForms.Guna2Button();
            this.pnlLogo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnMinimize = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnMaximize = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.pnlDesktop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblWelcomeText = new System.Windows.Forms.Label();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.pnlSidebar.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlDesktop.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 15;
            this.guna2Elipse1.TargetControl = this;
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.pnlSidebar.Controls.Add(this.btnDangXuat);
            this.pnlSidebar.Controls.Add(this.btnBaoCao);
            this.pnlSidebar.Controls.Add(this.btnThietBi);
            this.pnlSidebar.Controls.Add(this.btnThuVien);
            this.pnlSidebar.Controls.Add(this.btnHeThong);
            this.pnlSidebar.Controls.Add(this.btnTrangChu);
            this.pnlSidebar.Controls.Add(this.pnlLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(333, 862);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDangXuat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnDangXuat.ForeColor = System.Drawing.Color.White;
            this.btnDangXuat.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDangXuat.Location = new System.Drawing.Point(0, 800);
            this.btnDangXuat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(333, 62);
            this.btnDangXuat.TabIndex = 6;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDangXuat.TextOffset = new System.Drawing.Point(20, 0);
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCao.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnBaoCao.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnBaoCao.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnBaoCao.Location = new System.Drawing.Point(0, 371);
            this.btnBaoCao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(333, 62);
            this.btnBaoCao.TabIndex = 5;
            this.btnBaoCao.Text = "  Báo cáo và Thống kê";
            this.btnBaoCao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnBaoCao.TextOffset = new System.Drawing.Point(20, 0);
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            // 
            // btnThietBi
            // 
            this.btnThietBi.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnThietBi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnThietBi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnThietBi.ForeColor = System.Drawing.Color.White;
            this.btnThietBi.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnThietBi.Location = new System.Drawing.Point(0, 309);
            this.btnThietBi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThietBi.Name = "btnThietBi";
            this.btnThietBi.Size = new System.Drawing.Size(333, 62);
            this.btnThietBi.TabIndex = 4;
            this.btnThietBi.Text = "  Quản lý Trang thiết bị";
            this.btnThietBi.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnThietBi.TextOffset = new System.Drawing.Point(20, 0);
            this.btnThietBi.Click += new System.EventHandler(this.btnThietBi_Click);
            // 
            // btnThuVien
            // 
            this.btnThuVien.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnThuVien.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnThuVien.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnThuVien.ForeColor = System.Drawing.Color.White;
            this.btnThuVien.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnThuVien.Location = new System.Drawing.Point(0, 247);
            this.btnThuVien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThuVien.Name = "btnThuVien";
            this.btnThuVien.Size = new System.Drawing.Size(333, 62);
            this.btnThuVien.TabIndex = 3;
            this.btnThuVien.Text = "  Quản lý Thư viện";
            this.btnThuVien.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnThuVien.TextOffset = new System.Drawing.Point(20, 0);
            this.btnThuVien.Click += new System.EventHandler(this.btnThuVien_Click);
            // 
            // btnHeThong
            // 
            this.btnHeThong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHeThong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnHeThong.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnHeThong.ForeColor = System.Drawing.Color.White;
            this.btnHeThong.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnHeThong.Location = new System.Drawing.Point(0, 185);
            this.btnHeThong.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHeThong.Name = "btnHeThong";
            this.btnHeThong.Size = new System.Drawing.Size(333, 62);
            this.btnHeThong.TabIndex = 2;
            this.btnHeThong.Text = "  Quản lý Hệ thống";
            this.btnHeThong.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnHeThong.TextOffset = new System.Drawing.Point(20, 0);
            this.btnHeThong.Click += new System.EventHandler(this.btnHeThong_Click);
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTrangChu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnTrangChu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnTrangChu.ForeColor = System.Drawing.Color.White;
            this.btnTrangChu.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnTrangChu.Location = new System.Drawing.Point(0, 123);
            this.btnTrangChu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(333, 62);
            this.btnTrangChu.TabIndex = 1;
            this.btnTrangChu.Text = "  Trang chủ";
            this.btnTrangChu.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnTrangChu.TextOffset = new System.Drawing.Point(20, 0);
            this.btnTrangChu.Click += new System.EventHandler(this.btnTrangChu_Click);
            // 
            // pnlLogo
            // 
            this.pnlLogo.Controls.Add(this.lblLogo);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(333, 123);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(16, 26);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(300, 74);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "UNETI ADMIN";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnMinimize);
            this.pnlHeader.Controls.Add(this.btnMaximize);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(333, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1267, 74);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.lblTitle.Location = new System.Drawing.Point(27, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(175, 37);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "TRANG CHỦ";
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.btnMinimize.FillColor = System.Drawing.Color.White;
            this.btnMinimize.IconColor = System.Drawing.Color.Gray;
            this.btnMinimize.Location = new System.Drawing.Point(1091, 15);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(47, 37);
            this.btnMinimize.TabIndex = 2;
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.btnMaximize.FillColor = System.Drawing.Color.White;
            this.btnMaximize.IconColor = System.Drawing.Color.Gray;
            this.btnMaximize.Location = new System.Drawing.Point(1145, 15);
            this.btnMaximize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(47, 37);
            this.btnMaximize.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FillColor = System.Drawing.Color.White;
            this.btnClose.HoverState.FillColor = System.Drawing.Color.Red;
            this.btnClose.HoverState.IconColor = System.Drawing.Color.White;
            this.btnClose.IconColor = System.Drawing.Color.Gray;
            this.btnClose.Location = new System.Drawing.Point(1200, 15);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(47, 37);
            this.btnClose.TabIndex = 0;
            // 
            // pnlDesktop
            // 
            this.pnlDesktop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.pnlDesktop.Controls.Add(this.lblWelcomeText);
            this.pnlDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDesktop.Location = new System.Drawing.Point(333, 74);
            this.pnlDesktop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlDesktop.Name = "pnlDesktop";
            this.pnlDesktop.Size = new System.Drawing.Size(1267, 788);
            this.pnlDesktop.TabIndex = 2;
            // 
            // lblWelcomeText
            // 
            this.lblWelcomeText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWelcomeText.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcomeText.ForeColor = System.Drawing.Color.Silver;
            this.lblWelcomeText.Location = new System.Drawing.Point(0, 0);
            this.lblWelcomeText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWelcomeText.Name = "lblWelcomeText";
            this.lblWelcomeText.Size = new System.Drawing.Size(1267, 788);
            this.lblWelcomeText.TabIndex = 0;
            this.lblWelcomeText.Text = "HỆ THỐNG QUẢN LÝ TÀI SẢN & THƯ VIỆN";
            this.lblWelcomeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.pnlHeader;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 862);
            this.Controls.Add(this.pnlDesktop);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlDesktop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2Panel pnlDesktop;
        private Guna.UI2.WinForms.Guna2Panel pnlLogo;
        private Guna.UI2.WinForms.Guna2Button btnTrangChu;
        private System.Windows.Forms.Label lblLogo;
        private Guna.UI2.WinForms.Guna2Button btnDangXuat;
        private Guna.UI2.WinForms.Guna2Button btnBaoCao;
        private Guna.UI2.WinForms.Guna2Button btnThietBi;
        private Guna.UI2.WinForms.Guna2Button btnThuVien;
        private Guna.UI2.WinForms.Guna2Button btnHeThong;
        private Guna.UI2.WinForms.Guna2ControlBox btnMinimize;
        private Guna.UI2.WinForms.Guna2ControlBox btnMaximize;
        private Guna.UI2.WinForms.Guna2ControlBox btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcomeText;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
    }
}