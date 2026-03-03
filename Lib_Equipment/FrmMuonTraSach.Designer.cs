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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMaster = new Guna.UI2.WinForms.Guna2Panel();
            this.txtMaDocGia = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpHanTra = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnTaoPhieu = new Guna.UI2.WinForms.Guna2Button();
            this.lblTaoPhieu = new System.Windows.Forms.Label();
            this.pnlBody = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlDetail = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvChiTiet = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlAction = new Guna.UI2.WinForms.Guna2Panel();
            this.txtMaCuonSach = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnThemSach = new Guna.UI2.WinForms.Guna2Button();
            this.btnTraSach = new Guna.UI2.WinForms.Guna2Button();
            this.lblChiTiet = new System.Windows.Forms.Label();
            this.pnlList = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvPhieuMuon = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblDanhSachPhieu = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlMaster.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.pnlAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuMuon)).BeginInit();
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
            this.lblTitle.Size = new System.Drawing.Size(306, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "NGHIỆP VỤ MƯỢN TRẢ SÁCH";
            // 
            // pnlMaster
            // 
            this.pnlMaster.BackColor = System.Drawing.Color.White;
            this.pnlMaster.Controls.Add(this.lblTaoPhieu);
            this.pnlMaster.Controls.Add(this.txtMaDocGia);
            this.pnlMaster.Controls.Add(this.dtpHanTra);
            this.pnlMaster.Controls.Add(this.btnTaoPhieu);
            this.pnlMaster.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMaster.Location = new System.Drawing.Point(0, 60);
            this.pnlMaster.Name = "pnlMaster";
            this.pnlMaster.Size = new System.Drawing.Size(950, 80);
            this.pnlMaster.TabIndex = 1;
            // 
            // lblTaoPhieu
            // 
            this.lblTaoPhieu.AutoSize = true;
            this.lblTaoPhieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTaoPhieu.Location = new System.Drawing.Point(20, 10);
            this.lblTaoPhieu.Name = "lblTaoPhieu";
            this.lblTaoPhieu.Size = new System.Drawing.Size(124, 19);
            this.lblTaoPhieu.TabIndex = 4;
            this.lblTaoPhieu.Text = "TẠO PHIẾU MỚI:";
            // 
            // txtMaDocGia
            // 
            this.txtMaDocGia.BorderRadius = 5;
            this.txtMaDocGia.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaDocGia.DefaultText = "";
            this.txtMaDocGia.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtMaDocGia.Location = new System.Drawing.Point(24, 33);
            this.txtMaDocGia.Name = "txtMaDocGia";
            this.txtMaDocGia.PlaceholderText = "Nhập Mã Độc Giả";
            this.txtMaDocGia.Size = new System.Drawing.Size(200, 36);
            this.txtMaDocGia.TabIndex = 0;
            // 
            // dtpHanTra
            // 
            this.dtpHanTra.BorderRadius = 5;
            this.dtpHanTra.Checked = true;
            this.dtpHanTra.FillColor = System.Drawing.Color.White;
            this.dtpHanTra.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpHanTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHanTra.Location = new System.Drawing.Point(240, 33);
            this.dtpHanTra.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpHanTra.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpHanTra.Name = "dtpHanTra";
            this.dtpHanTra.Size = new System.Drawing.Size(200, 36);
            this.dtpHanTra.TabIndex = 1;
            this.dtpHanTra.Value = new System.DateTime(2026, 3, 3, 0, 0, 0, 0);
            // 
            // btnTaoPhieu
            // 
            this.btnTaoPhieu.BorderRadius = 5;
            this.btnTaoPhieu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnTaoPhieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTaoPhieu.ForeColor = System.Drawing.Color.White;
            this.btnTaoPhieu.Location = new System.Drawing.Point(460, 33);
            this.btnTaoPhieu.Name = "btnTaoPhieu";
            this.btnTaoPhieu.Size = new System.Drawing.Size(150, 36);
            this.btnTaoPhieu.TabIndex = 2;
            this.btnTaoPhieu.Text = "+ LẬP PHIẾU";
            this.btnTaoPhieu.Click += new System.EventHandler(this.btnTaoPhieu_Click);
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.pnlDetail);
            this.pnlBody.Controls.Add(this.pnlList);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 140);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBody.Size = new System.Drawing.Size(950, 500);
            this.pnlBody.TabIndex = 2;
            // 
            // pnlDetail
            // 
            this.pnlDetail.Controls.Add(this.dgvChiTiet);
            this.pnlDetail.Controls.Add(this.pnlAction);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(410, 10);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pnlDetail.Size = new System.Drawing.Size(530, 480);
            this.pnlDetail.TabIndex = 1;
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvChiTiet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvChiTiet.ColumnHeadersHeight = 35;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvChiTiet.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvChiTiet.Location = new System.Drawing.Point(10, 90);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.Size = new System.Drawing.Size(520, 390);
            this.dgvChiTiet.TabIndex = 1;
            this.dgvChiTiet.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.dgvChiTiet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellClick);
            // 
            // pnlAction
            // 
            this.pnlAction.BackColor = System.Drawing.Color.White;
            this.pnlAction.Controls.Add(this.lblChiTiet);
            this.pnlAction.Controls.Add(this.txtMaCuonSach);
            this.pnlAction.Controls.Add(this.btnThemSach);
            this.pnlAction.Controls.Add(this.btnTraSach);
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAction.Location = new System.Drawing.Point(10, 0);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(520, 90);
            this.pnlAction.TabIndex = 0;
            // 
            // lblChiTiet
            // 
            this.lblChiTiet.AutoSize = true;
            this.lblChiTiet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChiTiet.Location = new System.Drawing.Point(10, 10);
            this.lblChiTiet.Name = "lblChiTiet";
            this.lblChiTiet.Size = new System.Drawing.Size(248, 19);
            this.lblChiTiet.TabIndex = 5;
            this.lblChiTiet.Text = "QUÉT MÃ VẠCH (THÊM/TRẢ SÁCH):";
            // 
            // txtMaCuonSach
            // 
            this.txtMaCuonSach.BorderRadius = 5;
            this.txtMaCuonSach.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaCuonSach.DefaultText = "";
            this.txtMaCuonSach.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtMaCuonSach.Location = new System.Drawing.Point(14, 40);
            this.txtMaCuonSach.Name = "txtMaCuonSach";
            this.txtMaCuonSach.PlaceholderText = "Mã cuốn sách (VD: DS001-B01)";
            this.txtMaCuonSach.Size = new System.Drawing.Size(220, 36);
            this.txtMaCuonSach.TabIndex = 3;
            // 
            // btnThemSach
            // 
            this.btnThemSach.BorderRadius = 5;
            this.btnThemSach.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnThemSach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemSach.ForeColor = System.Drawing.Color.White;
            this.btnThemSach.Location = new System.Drawing.Point(245, 40);
            this.btnThemSach.Name = "btnThemSach";
            this.btnThemSach.Size = new System.Drawing.Size(120, 36);
            this.btnThemSach.TabIndex = 4;
            this.btnThemSach.Text = "THÊM VÀO PHIẾU";
            this.btnThemSach.Click += new System.EventHandler(this.btnThemSach_Click);
            // 
            // btnTraSach
            // 
            this.btnTraSach.BorderRadius = 5;
            this.btnTraSach.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnTraSach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTraSach.ForeColor = System.Drawing.Color.White;
            this.btnTraSach.Location = new System.Drawing.Point(375, 40);
            this.btnTraSach.Name = "btnTraSach";
            this.btnTraSach.Size = new System.Drawing.Size(120, 36);
            this.btnTraSach.TabIndex = 5;
            this.btnTraSach.Text = "XÁC NHẬN TRẢ";
            this.btnTraSach.Click += new System.EventHandler(this.btnTraSach_Click);
            // 
            // pnlList
            // 
            this.pnlList.Controls.Add(this.dgvPhieuMuon);
            this.pnlList.Controls.Add(this.lblDanhSachPhieu);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlList.Location = new System.Drawing.Point(10, 10);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(400, 480);
            this.pnlList.TabIndex = 0;
            // 
            // lblDanhSachPhieu
            // 
            this.lblDanhSachPhieu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDanhSachPhieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDanhSachPhieu.Location = new System.Drawing.Point(0, 0);
            this.lblDanhSachPhieu.Name = "lblDanhSachPhieu";
            this.lblDanhSachPhieu.Size = new System.Drawing.Size(400, 30);
            this.lblDanhSachPhieu.TabIndex = 6;
            this.lblDanhSachPhieu.Text = "DANH SÁCH PHIẾU MƯỢN";
            this.lblDanhSachPhieu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvPhieuMuon
            // 
            this.dgvPhieuMuon.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvPhieuMuon.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvPhieuMuon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPhieuMuon.ColumnHeadersHeight = 35;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvPhieuMuon.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPhieuMuon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhieuMuon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPhieuMuon.Location = new System.Drawing.Point(0, 30);
            this.dgvPhieuMuon.Name = "dgvPhieuMuon";
            this.dgvPhieuMuon.ReadOnly = true;
            this.dgvPhieuMuon.RowHeadersVisible = false;
            this.dgvPhieuMuon.Size = new System.Drawing.Size(400, 450);
            this.dgvPhieuMuon.TabIndex = 0;
            this.dgvPhieuMuon.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvPhieuMuon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhieuMuon_CellClick);
            // 
            // FrmMuonTraSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(950, 640);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlMaster);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMuonTraSach";
            this.Text = "Mượn Trả Sách";
            this.Load += new System.EventHandler(this.FrmMuonTraSach_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMaster.ResumeLayout(false);
            this.pnlMaster.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.pnlAction.ResumeLayout(false);
            this.pnlAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuMuon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlMaster;
        private Guna.UI2.WinForms.Guna2Panel pnlBody;
        private Guna.UI2.WinForms.Guna2TextBox txtMaDocGia;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpHanTra;
        private Guna.UI2.WinForms.Guna2Button btnTaoPhieu;
        private System.Windows.Forms.Label lblTaoPhieu;
        private Guna.UI2.WinForms.Guna2Panel pnlDetail;
        private Guna.UI2.WinForms.Guna2Panel pnlList;
        private Guna.UI2.WinForms.Guna2DataGridView dgvPhieuMuon;
        private Guna.UI2.WinForms.Guna2DataGridView dgvChiTiet;
        private Guna.UI2.WinForms.Guna2Panel pnlAction;
        private System.Windows.Forms.Label lblDanhSachPhieu;
        private System.Windows.Forms.Label lblChiTiet;
        private Guna.UI2.WinForms.Guna2Button btnTraSach;
        private Guna.UI2.WinForms.Guna2Button btnThemSach;
        private Guna.UI2.WinForms.Guna2TextBox txtMaCuonSach;
    }
}