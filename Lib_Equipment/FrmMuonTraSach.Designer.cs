namespace Lib_Equipment
{
    partial class FrmMuonTraSach
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gbThaoTac = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblMaBanSao = new System.Windows.Forms.Label();
            this.txtMaBanSao = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenSachMuon = new System.Windows.Forms.Label();
            this.txtTenSachMuon = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblHanTra = new System.Windows.Forms.Label();
            this.dtpHanTra = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnChoMuon = new Guna.UI2.WinForms.Guna2Button();
            this.pnlSpacing = new System.Windows.Forms.Panel();
            this.gbDocGia = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblMaDG = new System.Windows.Forms.Label();
            this.txtMaDG = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenDG = new System.Windows.Forms.Label();
            this.txtTenDG = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.gbDanhSach = new Guna.UI2.WinForms.Guna2GroupBox();
            this.dgvDangMuon = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblTitleRight = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            this.gbThaoTac.SuspendLayout();
            this.gbDocGia.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.gbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbThaoTac);
            this.pnlLeft.Controls.Add(this.pnlSpacing);
            this.pnlLeft.Controls.Add(this.gbDocGia);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(25, 25, 15, 25);
            this.pnlLeft.Size = new System.Drawing.Size(430, 800);
            this.pnlLeft.TabIndex = 1;
            // 
            // gbDocGia
            // 
            this.gbDocGia.BorderColor = System.Drawing.Color.LightGray;
            this.gbDocGia.BorderRadius = 10;
            this.gbDocGia.Controls.Add(this.lblMaDG);
            this.gbDocGia.Controls.Add(this.txtMaDG);
            this.gbDocGia.Controls.Add(this.lblTenDG);
            this.gbDocGia.Controls.Add(this.txtTenDG);
            this.gbDocGia.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.gbDocGia.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDocGia.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.gbDocGia.ForeColor = System.Drawing.Color.White;
            this.gbDocGia.Location = new System.Drawing.Point(25, 25);
            this.gbDocGia.Name = "gbDocGia";
            this.gbDocGia.Size = new System.Drawing.Size(390, 240);
            this.gbDocGia.TabIndex = 0;
            this.gbDocGia.Text = "👤 1. THÔNG TIN ĐỘC GIẢ";
            // 
            // lblMaDG
            // 
            this.lblMaDG.AutoSize = true;
            this.lblMaDG.BackColor = System.Drawing.Color.White;
            this.lblMaDG.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMaDG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMaDG.Location = new System.Drawing.Point(20, 65);
            this.lblMaDG.Name = "lblMaDG";
            this.lblMaDG.Size = new System.Drawing.Size(206, 17);
            this.lblMaDG.TabIndex = 1;
            this.lblMaDG.Text = "Mã Độc giả (Nhấn Enter để tìm):";
            // 
            // txtMaDG
            // 
            this.txtMaDG.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.txtMaDG.BorderRadius = 6;
            this.txtMaDG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaDG.DefaultText = "";
            this.txtMaDG.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtMaDG.ForeColor = System.Drawing.Color.Black;
            this.txtMaDG.Location = new System.Drawing.Point(20, 88);
            this.txtMaDG.Name = "txtMaDG";
            this.txtMaDG.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtMaDG.PlaceholderText = "Nhập mã SV/GV...";
            this.txtMaDG.Size = new System.Drawing.Size(350, 42);
            this.txtMaDG.TabIndex = 0;
            this.txtMaDG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaDG_KeyDown);
            // 
            // lblTenDG
            // 
            this.lblTenDG.AutoSize = true;
            this.lblTenDG.BackColor = System.Drawing.Color.White;
            this.lblTenDG.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTenDG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTenDG.Location = new System.Drawing.Point(20, 150);
            this.lblTenDG.Name = "lblTenDG";
            this.lblTenDG.Size = new System.Drawing.Size(72, 17);
            this.lblTenDG.TabIndex = 3;
            this.lblTenDG.Text = "Họ và tên:";
            // 
            // txtTenDG
            // 
            this.txtTenDG.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.txtTenDG.BorderRadius = 6;
            this.txtTenDG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDG.DefaultText = "";
            this.txtTenDG.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtTenDG.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtTenDG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.txtTenDG.Location = new System.Drawing.Point(20, 173);
            this.txtTenDG.Name = "txtTenDG";
            this.txtTenDG.ReadOnly = true;
            this.txtTenDG.Size = new System.Drawing.Size(350, 42);
            this.txtTenDG.TabIndex = 2;
            // 
            // pnlSpacing
            // 
            this.pnlSpacing.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpacing.Location = new System.Drawing.Point(25, 265);
            this.pnlSpacing.Name = "pnlSpacing";
            this.pnlSpacing.Size = new System.Drawing.Size(390, 20);
            this.pnlSpacing.TabIndex = 2;
            // 
            // gbThaoTac
            // 
            this.gbThaoTac.BorderColor = System.Drawing.Color.LightGray;
            this.gbThaoTac.BorderRadius = 10;
            this.gbThaoTac.Controls.Add(this.lblMaBanSao);
            this.gbThaoTac.Controls.Add(this.txtMaBanSao);
            this.gbThaoTac.Controls.Add(this.lblTenSachMuon);
            this.gbThaoTac.Controls.Add(this.txtTenSachMuon);
            this.gbThaoTac.Controls.Add(this.lblHanTra);
            this.gbThaoTac.Controls.Add(this.dtpHanTra);
            this.gbThaoTac.Controls.Add(this.btnChoMuon);
            this.gbThaoTac.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.gbThaoTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThaoTac.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.gbThaoTac.ForeColor = System.Drawing.Color.White;
            this.gbThaoTac.Location = new System.Drawing.Point(25, 285);
            this.gbThaoTac.Name = "gbThaoTac";
            this.gbThaoTac.Size = new System.Drawing.Size(390, 490);
            this.gbThaoTac.TabIndex = 1;
            this.gbThaoTac.Text = "📚 2. QUÉT SÁCH CHO MƯỢN";
            // 
            // lblMaBanSao
            // 
            this.lblMaBanSao.AutoSize = true;
            this.lblMaBanSao.BackColor = System.Drawing.Color.White;
            this.lblMaBanSao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMaBanSao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMaBanSao.Location = new System.Drawing.Point(20, 65);
            this.lblMaBanSao.Name = "lblMaBanSao";
            this.lblMaBanSao.Size = new System.Drawing.Size(199, 17);
            this.lblMaBanSao.TabIndex = 1;
            this.lblMaBanSao.Text = "Mã sách (Nhấn Enter kiểm tra):";
            // 
            // txtMaBanSao
            // 
            this.txtMaBanSao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.txtMaBanSao.BorderRadius = 6;
            this.txtMaBanSao.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaBanSao.DefaultText = "";
            this.txtMaBanSao.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtMaBanSao.ForeColor = System.Drawing.Color.Black;
            this.txtMaBanSao.Location = new System.Drawing.Point(20, 88);
            this.txtMaBanSao.Name = "txtMaBanSao";
            this.txtMaBanSao.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtMaBanSao.PlaceholderText = "Quét mã vạch...";
            this.txtMaBanSao.Size = new System.Drawing.Size(350, 42);
            this.txtMaBanSao.TabIndex = 0;
            this.txtMaBanSao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaBanSao_KeyDown);
            // 
            // lblTenSachMuon
            // 
            this.lblTenSachMuon.AutoSize = true;
            this.lblTenSachMuon.BackColor = System.Drawing.Color.White;
            this.lblTenSachMuon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTenSachMuon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTenSachMuon.Location = new System.Drawing.Point(20, 150);
            this.lblTenSachMuon.Name = "lblTenSachMuon";
            this.lblTenSachMuon.Size = new System.Drawing.Size(100, 17);
            this.lblTenSachMuon.TabIndex = 6;
            this.lblTenSachMuon.Text = "Cuốn sẽ mượn:";
            // 
            // txtTenSachMuon
            // 
            this.txtTenSachMuon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.txtTenSachMuon.BorderRadius = 6;
            this.txtTenSachMuon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenSachMuon.DefaultText = "";
            this.txtTenSachMuon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtTenSachMuon.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Italic);
            this.txtTenSachMuon.ForeColor = System.Drawing.Color.DimGray;
            this.txtTenSachMuon.Location = new System.Drawing.Point(20, 173);
            this.txtTenSachMuon.Name = "txtTenSachMuon";
            this.txtTenSachMuon.PlaceholderText = "Chưa xác định...";
            this.txtTenSachMuon.ReadOnly = true;
            this.txtTenSachMuon.Size = new System.Drawing.Size(350, 42);
            this.txtTenSachMuon.TabIndex = 5;
            // 
            // lblHanTra
            // 
            this.lblHanTra.AutoSize = true;
            this.lblHanTra.BackColor = System.Drawing.Color.White;
            this.lblHanTra.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblHanTra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHanTra.Location = new System.Drawing.Point(20, 235);
            this.lblHanTra.Name = "lblHanTra";
            this.lblHanTra.Size = new System.Drawing.Size(106, 17);
            this.lblHanTra.TabIndex = 3;
            this.lblHanTra.Text = "Hạn trả dự kiến:";
            // 
            // dtpHanTra
            // 
            this.dtpHanTra.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.dtpHanTra.BorderRadius = 6;
            this.dtpHanTra.BorderThickness = 1;
            this.dtpHanTra.Checked = true;
            this.dtpHanTra.FillColor = System.Drawing.Color.White;
            this.dtpHanTra.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dtpHanTra.ForeColor = System.Drawing.Color.Black;
            this.dtpHanTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHanTra.Location = new System.Drawing.Point(20, 258);
            this.dtpHanTra.Name = "dtpHanTra";
            this.dtpHanTra.Size = new System.Drawing.Size(350, 42);
            this.dtpHanTra.TabIndex = 2;
            // 
            // btnChoMuon
            // 
            this.btnChoMuon.BorderRadius = 8;
            this.btnChoMuon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChoMuon.Enabled = false;
            this.btnChoMuon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnChoMuon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnChoMuon.ForeColor = System.Drawing.Color.White;
            this.btnChoMuon.Location = new System.Drawing.Point(20, 335);
            this.btnChoMuon.Name = "btnChoMuon";
            this.btnChoMuon.Size = new System.Drawing.Size(350, 55);
            this.btnChoMuon.TabIndex = 4;
            this.btnChoMuon.Text = "XÁC NHẬN CHO MƯỢN";
            this.btnChoMuon.Click += new System.EventHandler(this.btnChoMuon_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.gbDanhSach);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(430, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10, 25, 25, 25);
            this.pnlRight.Size = new System.Drawing.Size(837, 800);
            this.pnlRight.TabIndex = 2;
            // 
            // gbDanhSach
            // 
            this.gbDanhSach.BorderColor = System.Drawing.Color.LightGray;
            this.gbDanhSach.BorderRadius = 10;
            this.gbDanhSach.Controls.Add(this.dgvDangMuon);
            this.gbDanhSach.Controls.Add(this.lblTitleRight);
            this.gbDanhSach.CustomBorderColor = System.Drawing.Color.White;
            this.gbDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDanhSach.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbDanhSach.Location = new System.Drawing.Point(10, 25);
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.Padding = new System.Windows.Forms.Padding(15, 60, 15, 15);
            this.gbDanhSach.Size = new System.Drawing.Size(802, 750);
            this.gbDanhSach.TabIndex = 0;
            // 
            // lblTitleRight
            // 
            this.lblTitleRight.AutoSize = true;
            this.lblTitleRight.BackColor = System.Drawing.Color.White;
            this.lblTitleRight.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitleRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.lblTitleRight.Location = new System.Drawing.Point(15, 15);
            this.lblTitleRight.Name = "lblTitleRight";
            this.lblTitleRight.Size = new System.Drawing.Size(326, 25);
            this.lblTitleRight.TabIndex = 1;
            this.lblTitleRight.Text = "DANH SÁCH PHIẾU ĐANG MƯỢN";
            // 
            // dgvDangMuon
            // 
            this.dgvDangMuon.AllowUserToAddRows = false;
            this.dgvDangMuon.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDangMuon.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDangMuon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDangMuon.ColumnHeadersHeight = 45;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDangMuon.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDangMuon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDangMuon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDangMuon.Location = new System.Drawing.Point(15, 60);
            this.dgvDangMuon.Name = "dgvDangMuon";
            this.dgvDangMuon.ReadOnly = true;
            this.dgvDangMuon.RowHeadersVisible = false;
            this.dgvDangMuon.RowTemplate.Height = 40;
            this.dgvDangMuon.Size = new System.Drawing.Size(772, 675);
            this.dgvDangMuon.TabIndex = 0;
            this.dgvDangMuon.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvDangMuon.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDangMuon_CellContentClick);
            this.dgvDangMuon.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDangMuon_CellFormatting);
            // 
            // FrmMuonTraSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1267, 800);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMuonTraSach";
            this.Text = "Quản lý Mượn / Trả";
            this.Load += new System.EventHandler(this.FrmMuonTraSach_Load);
            this.pnlLeft.ResumeLayout(false);
            this.gbThaoTac.ResumeLayout(false);
            this.gbThaoTac.PerformLayout();
            this.gbDocGia.ResumeLayout(false);
            this.gbDocGia.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.gbDanhSach.ResumeLayout(false);
            this.gbDanhSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel pnlLeft;
        private Guna.UI2.WinForms.Guna2GroupBox gbDocGia;
        private System.Windows.Forms.Label lblMaDG;
        private Guna.UI2.WinForms.Guna2TextBox txtMaDG;
        private System.Windows.Forms.Label lblTenDG;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDG;
        private System.Windows.Forms.Panel pnlSpacing;
        private Guna.UI2.WinForms.Guna2GroupBox gbThaoTac;
        private System.Windows.Forms.Label lblMaBanSao;
        private Guna.UI2.WinForms.Guna2TextBox txtMaBanSao;
        private System.Windows.Forms.Label lblTenSachMuon;
        private Guna.UI2.WinForms.Guna2TextBox txtTenSachMuon;
        private System.Windows.Forms.Label lblHanTra;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpHanTra;
        private Guna.UI2.WinForms.Guna2Button btnChoMuon;
        private Guna.UI2.WinForms.Guna2Panel pnlRight;
        private Guna.UI2.WinForms.Guna2GroupBox gbDanhSach;
        private System.Windows.Forms.Label lblTitleRight;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDangMuon;
    }
}