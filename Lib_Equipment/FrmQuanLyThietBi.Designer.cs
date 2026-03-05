namespace Lib_Equipment
{
    partial class FrmQuanLyThietBi
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
            this.pnlControls = new Guna.UI2.WinForms.Guna2Panel();
            this.txtMaTB = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTenTB = new Guna.UI2.WinForms.Guna2TextBox();
            this.cboLoaiTB = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboKhoaPhong = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dtpNgayNhap = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.txtGiaTien = new Guna.UI2.WinForms.Guna2TextBox();
            this.cboTinhTrang = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlAction = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.dgvThietBi = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.pnlAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).BeginInit();
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
            this.lblTitle.Size = new System.Drawing.Size(359, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ DANH MỤC THIẾT BỊ";
            // 
            // pnlControls
            // 
            this.pnlControls.BackColor = System.Drawing.Color.White;
            this.pnlControls.Controls.Add(this.txtMaTB);
            this.pnlControls.Controls.Add(this.txtTenTB);
            this.pnlControls.Controls.Add(this.cboLoaiTB);
            this.pnlControls.Controls.Add(this.cboKhoaPhong);
            this.pnlControls.Controls.Add(this.dtpNgayNhap);
            this.pnlControls.Controls.Add(this.txtGiaTien);
            this.pnlControls.Controls.Add(this.cboTinhTrang);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 60);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(950, 160);
            this.pnlControls.TabIndex = 1;
            // 
            // CỘT 1
            // 
            this.txtMaTB.BorderRadius = 5;
            this.txtMaTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaTB.DefaultText = "";
            this.txtMaTB.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtMaTB.Location = new System.Drawing.Point(30, 20);
            this.txtMaTB.Name = "txtMaTB";
            this.txtMaTB.PlaceholderText = "Mã thiết bị (VD: TB_001)";
            this.txtMaTB.Size = new System.Drawing.Size(250, 40);
            this.txtMaTB.TabIndex = 0;
            // 
            // cboLoaiTB
            // 
            this.cboLoaiTB.BackColor = System.Drawing.Color.Transparent;
            this.cboLoaiTB.BorderRadius = 5;
            this.cboLoaiTB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiTB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiTB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLoaiTB.ItemHeight = 30;
            this.cboLoaiTB.Location = new System.Drawing.Point(30, 90);
            this.cboLoaiTB.Name = "cboLoaiTB";
            this.cboLoaiTB.Size = new System.Drawing.Size(250, 36);
            this.cboLoaiTB.TabIndex = 3;
            // 
            // CỘT 2
            // 
            this.txtTenTB.BorderRadius = 5;
            this.txtTenTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenTB.DefaultText = "";
            this.txtTenTB.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTenTB.Location = new System.Drawing.Point(320, 20);
            this.txtTenTB.Name = "txtTenTB";
            this.txtTenTB.PlaceholderText = "Tên thiết bị";
            this.txtTenTB.Size = new System.Drawing.Size(250, 40);
            this.txtTenTB.TabIndex = 1;
            // 
            // cboKhoaPhong
            // 
            this.cboKhoaPhong.BackColor = System.Drawing.Color.Transparent;
            this.cboKhoaPhong.BorderRadius = 5;
            this.cboKhoaPhong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKhoaPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoaPhong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboKhoaPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboKhoaPhong.ItemHeight = 30;
            this.cboKhoaPhong.Location = new System.Drawing.Point(320, 90);
            this.cboKhoaPhong.Name = "cboKhoaPhong";
            this.cboKhoaPhong.Size = new System.Drawing.Size(250, 36);
            this.cboKhoaPhong.TabIndex = 4;
            // 
            // CỘT 3
            // 
            this.dtpNgayNhap.BorderRadius = 5;
            this.dtpNgayNhap.Checked = true;
            this.dtpNgayNhap.FillColor = System.Drawing.Color.White;
            this.dtpNgayNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayNhap.Location = new System.Drawing.Point(610, 20);
            this.dtpNgayNhap.Name = "dtpNgayNhap";
            this.dtpNgayNhap.Size = new System.Drawing.Size(140, 40);
            this.dtpNgayNhap.TabIndex = 2;
            // 
            // txtGiaTien
            // 
            this.txtGiaTien.BorderRadius = 5;
            this.txtGiaTien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGiaTien.DefaultText = "";
            this.txtGiaTien.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtGiaTien.Location = new System.Drawing.Point(770, 20);
            this.txtGiaTien.Name = "txtGiaTien";
            this.txtGiaTien.PlaceholderText = "Giá nhập (VNĐ)";
            this.txtGiaTien.Size = new System.Drawing.Size(150, 40);
            this.txtGiaTien.TabIndex = 6;
            // 
            // cboTinhTrang
            // 
            this.cboTinhTrang.BackColor = System.Drawing.Color.Transparent;
            this.cboTinhTrang.BorderRadius = 5;
            this.cboTinhTrang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTinhTrang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTinhTrang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboTinhTrang.ItemHeight = 30;
            this.cboTinhTrang.Items.AddRange(new object[] {
            "Tốt",
            "Đang sử dụng",
            "Cần bảo trì",
            "Đã thanh lý"});
            this.cboTinhTrang.Location = new System.Drawing.Point(610, 90);
            this.cboTinhTrang.Name = "cboTinhTrang";
            this.cboTinhTrang.Size = new System.Drawing.Size(310, 36);
            this.cboTinhTrang.StartIndex = 0;
            this.cboTinhTrang.TabIndex = 5;
            // 
            // pnlAction
            // 
            this.pnlAction.BackColor = System.Drawing.Color.White;
            this.pnlAction.Controls.Add(this.btnLamMoi);
            this.pnlAction.Controls.Add(this.btnXoa);
            this.pnlAction.Controls.Add(this.btnSua);
            this.pnlAction.Controls.Add(this.btnThem);
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAction.Location = new System.Drawing.Point(0, 220);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(950, 70);
            this.pnlAction.TabIndex = 2;
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 5;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(30, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(120, 45);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "THÊM";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BorderRadius = 5;
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(170, 10);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(120, 45);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "CẬP NHẬT";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 5;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(310, 10);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(120, 45);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "XÓA";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 5;
            this.btnLamMoi.FillColor = System.Drawing.Color.Gray;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(450, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 45);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "LÀM MỚI";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // dgvThietBi
            // 
            this.dgvThietBi.AllowUserToAddRows = false;
            this.dgvThietBi.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvThietBi.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvThietBi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvThietBi.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvThietBi.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvThietBi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThietBi.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvThietBi.Location = new System.Drawing.Point(0, 290);
            this.dgvThietBi.Name = "dgvThietBi";
            this.dgvThietBi.ReadOnly = true;
            this.dgvThietBi.RowHeadersVisible = false;
            this.dgvThietBi.RowTemplate.Height = 35;
            this.dgvThietBi.Size = new System.Drawing.Size(950, 350);
            this.dgvThietBi.TabIndex = 3;
            this.dgvThietBi.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvThietBi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThietBi_CellClick);
            // 
            // FrmQuanLyThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(950, 640);
            this.Controls.Add(this.dgvThietBi);
            this.Controls.Add(this.pnlAction);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmQuanLyThietBi";
            this.Text = "Quản lý Thiết bị";
            this.Load += new System.EventHandler(this.FrmQuanLyThietBi_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.pnlAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlControls;
        private Guna.UI2.WinForms.Guna2Panel pnlAction;
        private Guna.UI2.WinForms.Guna2DataGridView dgvThietBi;
        private Guna.UI2.WinForms.Guna2TextBox txtMaTB;
        private Guna.UI2.WinForms.Guna2TextBox txtTenTB;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoaiTB;
        private Guna.UI2.WinForms.Guna2ComboBox cboKhoaPhong;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayNhap;
        private Guna.UI2.WinForms.Guna2TextBox txtGiaTien;
        private Guna.UI2.WinForms.Guna2ComboBox cboTinhTrang;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
    }
}