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
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gbDocGia = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblMaDG = new System.Windows.Forms.Label();
            this.txtMaDG = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenDG = new System.Windows.Forms.Label();
            this.txtTenDG = new Guna.UI2.WinForms.Guna2TextBox();
            this.gbThaoTac = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblMaBanSao = new System.Windows.Forms.Label();
            this.txtMaBanSao = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenSachMuon = new System.Windows.Forms.Label();
            this.txtTenSachMuon = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblHanTra = new System.Windows.Forms.Label();
            this.dtpHanTra = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnChoMuon = new Guna.UI2.WinForms.Guna2Button();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvDangMuon = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.gbDocGia.SuspendLayout();
            this.gbThaoTac.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(950, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(328, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ MƯỢN / TRẢ SÁCH";
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbThaoTac);
            this.pnlLeft.Controls.Add(this.gbDocGia);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 60);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(20);
            this.pnlLeft.Size = new System.Drawing.Size(350, 580);
            this.pnlLeft.TabIndex = 1;
            // 
            // gbDocGia
            // 
            this.gbDocGia.BorderRadius = 5;
            this.gbDocGia.Controls.Add(this.lblMaDG);
            this.gbDocGia.Controls.Add(this.txtMaDG);
            this.gbDocGia.Controls.Add(this.lblTenDG);
            this.gbDocGia.Controls.Add(this.txtTenDG);
            this.gbDocGia.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.gbDocGia.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDocGia.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbDocGia.ForeColor = System.Drawing.Color.White;
            this.gbDocGia.Location = new System.Drawing.Point(20, 20);
            this.gbDocGia.Name = "gbDocGia";
            this.gbDocGia.Size = new System.Drawing.Size(310, 200);
            this.gbDocGia.TabIndex = 0;
            this.gbDocGia.Text = "1. THÔNG TIN ĐỘC GIẢ";
            // 
            // lblMaDG
            // 
            this.lblMaDG.AutoSize = true;
            this.lblMaDG.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaDG.ForeColor = System.Drawing.Color.Black;
            this.lblMaDG.Location = new System.Drawing.Point(15, 50);
            this.lblMaDG.Name = "lblMaDG";
            this.lblMaDG.Size = new System.Drawing.Size(187, 15);
            this.lblMaDG.TabIndex = 1;
            this.lblMaDG.Text = "Mã Độc giả (Nhấn Enter để tìm):";
            // 
            // txtMaDG
            // 
            this.txtMaDG.BorderRadius = 5;
            this.txtMaDG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaDG.DefaultText = "";
            this.txtMaDG.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaDG.ForeColor = System.Drawing.Color.Black;
            this.txtMaDG.Location = new System.Drawing.Point(15, 68);
            this.txtMaDG.Name = "txtMaDG";
            this.txtMaDG.Size = new System.Drawing.Size(280, 36);
            this.txtMaDG.TabIndex = 0;
            this.txtMaDG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaDG_KeyDown);
            // 
            // lblTenDG
            // 
            this.lblTenDG.AutoSize = true;
            this.lblTenDG.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenDG.ForeColor = System.Drawing.Color.Black;
            this.lblTenDG.Location = new System.Drawing.Point(15, 120);
            this.lblTenDG.Name = "lblTenDG";
            this.lblTenDG.Size = new System.Drawing.Size(64, 15);
            this.lblTenDG.TabIndex = 3;
            this.lblTenDG.Text = "Họ và tên:";
            // 
            // txtTenDG
            // 
            this.txtTenDG.BorderRadius = 5;
            this.txtTenDG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDG.DefaultText = "";
            this.txtTenDG.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTenDG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.txtTenDG.Location = new System.Drawing.Point(15, 138);
            this.txtTenDG.Name = "txtTenDG";
            this.txtTenDG.ReadOnly = true;
            this.txtTenDG.Size = new System.Drawing.Size(280, 36);
            this.txtTenDG.TabIndex = 2;
            // 
            // gbThaoTac
            // 
            this.gbThaoTac.BorderRadius = 5;
            this.gbThaoTac.Controls.Add(this.lblMaBanSao);
            this.gbThaoTac.Controls.Add(this.txtMaBanSao);
            this.gbThaoTac.Controls.Add(this.lblTenSachMuon);
            this.gbThaoTac.Controls.Add(this.txtTenSachMuon);
            this.gbThaoTac.Controls.Add(this.lblHanTra);
            this.gbThaoTac.Controls.Add(this.dtpHanTra);
            this.gbThaoTac.Controls.Add(this.btnChoMuon);
            this.gbThaoTac.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.gbThaoTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThaoTac.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbThaoTac.ForeColor = System.Drawing.Color.White;
            this.gbThaoTac.Location = new System.Drawing.Point(20, 220);
            this.gbThaoTac.Name = "gbThaoTac";
            this.gbThaoTac.Size = new System.Drawing.Size(310, 340);
            this.gbThaoTac.TabIndex = 1;
            this.gbThaoTac.Text = "2. QUÉT SÁCH CHO MƯỢN";
            // 
            // lblMaBanSao
            // 
            this.lblMaBanSao.AutoSize = true;
            this.lblMaBanSao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaBanSao.ForeColor = System.Drawing.Color.Black;
            this.lblMaBanSao.Location = new System.Drawing.Point(15, 45);
            this.lblMaBanSao.Name = "lblMaBanSao";
            this.lblMaBanSao.Size = new System.Drawing.Size(180, 15);
            this.lblMaBanSao.TabIndex = 1;
            this.lblMaBanSao.Text = "Mã sách (Nhấn Enter kiểm tra):";
            // 
            // txtMaBanSao
            // 
            this.txtMaBanSao.BorderRadius = 5;
            this.txtMaBanSao.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaBanSao.DefaultText = "";
            this.txtMaBanSao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaBanSao.ForeColor = System.Drawing.Color.Black;
            this.txtMaBanSao.Location = new System.Drawing.Point(15, 63);
            this.txtMaBanSao.Name = "txtMaBanSao";
            this.txtMaBanSao.Size = new System.Drawing.Size(280, 36);
            this.txtMaBanSao.TabIndex = 0;
            this.txtMaBanSao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaBanSao_KeyDown);
            // 
            // lblTenSachMuon
            // 
            this.lblTenSachMuon.AutoSize = true;
            this.lblTenSachMuon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenSachMuon.ForeColor = System.Drawing.Color.Black;
            this.lblTenSachMuon.Location = new System.Drawing.Point(15, 110);
            this.lblTenSachMuon.Name = "lblTenSachMuon";
            this.lblTenSachMuon.Size = new System.Drawing.Size(89, 15);
            this.lblTenSachMuon.TabIndex = 6;
            this.lblTenSachMuon.Text = "Cuốn sẽ mượn:";
            // 
            // txtTenSachMuon
            // 
            this.txtTenSachMuon.BorderRadius = 5;
            this.txtTenSachMuon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenSachMuon.DefaultText = "Chưa xác định...";
            this.txtTenSachMuon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.txtTenSachMuon.ForeColor = System.Drawing.Color.DimGray;
            this.txtTenSachMuon.Location = new System.Drawing.Point(15, 128);
            this.txtTenSachMuon.Name = "txtTenSachMuon";
            this.txtTenSachMuon.ReadOnly = true;
            this.txtTenSachMuon.Size = new System.Drawing.Size(280, 36);
            this.txtTenSachMuon.TabIndex = 5;
            // 
            // lblHanTra
            // 
            this.lblHanTra.AutoSize = true;
            this.lblHanTra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHanTra.ForeColor = System.Drawing.Color.Black;
            this.lblHanTra.Location = new System.Drawing.Point(15, 175);
            this.lblHanTra.Name = "lblHanTra";
            this.lblHanTra.Size = new System.Drawing.Size(95, 15);
            this.lblHanTra.TabIndex = 3;
            this.lblHanTra.Text = "Hạn trả dự kiến:";
            // 
            // dtpHanTra
            // 
            this.dtpHanTra.BorderRadius = 5;
            this.dtpHanTra.Checked = true;
            this.dtpHanTra.FillColor = System.Drawing.Color.White;
            this.dtpHanTra.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpHanTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHanTra.Location = new System.Drawing.Point(15, 193);
            this.dtpHanTra.Name = "dtpHanTra";
            this.dtpHanTra.Size = new System.Drawing.Size(280, 36);
            this.dtpHanTra.TabIndex = 2;
            // 
            // btnChoMuon
            // 
            this.btnChoMuon.BorderRadius = 5;
            this.btnChoMuon.Enabled = false;
            this.btnChoMuon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnChoMuon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnChoMuon.ForeColor = System.Drawing.Color.White;
            this.btnChoMuon.Location = new System.Drawing.Point(15, 250);
            this.btnChoMuon.Name = "btnChoMuon";
            this.btnChoMuon.Size = new System.Drawing.Size(280, 50);
            this.btnChoMuon.TabIndex = 4;
            this.btnChoMuon.Text = "XÁC NHẬN CHO MƯỢN";
            this.btnChoMuon.Click += new System.EventHandler(this.btnChoMuon_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.dgvDangMuon);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(350, 60);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10, 20, 20, 20);
            this.pnlRight.Size = new System.Drawing.Size(600, 580);
            this.pnlRight.TabIndex = 2;
            // 
            // dgvDangMuon
            // 
            this.dgvDangMuon.AllowUserToAddRows = false;
            this.dgvDangMuon.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDangMuon.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.dgvDangMuon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDangMuon.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDangMuon.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDangMuon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDangMuon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDangMuon.Location = new System.Drawing.Point(10, 20);
            this.dgvDangMuon.Name = "dgvDangMuon";
            this.dgvDangMuon.ReadOnly = true;
            this.dgvDangMuon.RowHeadersVisible = false;
            this.dgvDangMuon.RowTemplate.Height = 40;
            this.dgvDangMuon.Size = new System.Drawing.Size(570, 540);
            this.dgvDangMuon.TabIndex = 0;
            this.dgvDangMuon.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvDangMuon.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDangMuon.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDangMuon.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDangMuon_CellContentClick);
            this.dgvDangMuon.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDangMuon_CellFormatting);
            // 
            // FrmMuonTraSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(950, 640);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMuonTraSach";
            this.Text = "Quản lý Mượn / Trả";
            this.Load += new System.EventHandler(this.FrmMuonTraSach_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.gbDocGia.ResumeLayout(false);
            this.gbDocGia.PerformLayout();
            this.gbThaoTac.ResumeLayout(false);
            this.gbThaoTac.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlLeft;
        private Guna.UI2.WinForms.Guna2GroupBox gbDocGia;
        private System.Windows.Forms.Label lblMaDG;
        private Guna.UI2.WinForms.Guna2TextBox txtMaDG;
        private System.Windows.Forms.Label lblTenDG;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDG;
        private Guna.UI2.WinForms.Guna2GroupBox gbThaoTac;
        private System.Windows.Forms.Label lblMaBanSao;
        private Guna.UI2.WinForms.Guna2TextBox txtMaBanSao;
        private System.Windows.Forms.Label lblTenSachMuon;
        private Guna.UI2.WinForms.Guna2TextBox txtTenSachMuon;
        private System.Windows.Forms.Label lblHanTra;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpHanTra;
        private Guna.UI2.WinForms.Guna2Button btnChoMuon;
        private Guna.UI2.WinForms.Guna2Panel pnlRight;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDangMuon;
    }
}