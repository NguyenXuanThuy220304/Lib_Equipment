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
            this.pnlControls.SuspendLayout();
            this.pnlAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).BeginInit();
            this.SuspendLayout();
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
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Margin = new System.Windows.Forms.Padding(4);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1267, 197);
            this.pnlControls.TabIndex = 1;
            // 
            // txtMaTB
            // 
            this.txtMaTB.BorderRadius = 5;
            this.txtMaTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaTB.DefaultText = "";
            this.txtMaTB.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtMaTB.Location = new System.Drawing.Point(40, 25);
            this.txtMaTB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaTB.Name = "txtMaTB";
            this.txtMaTB.PlaceholderText = "Mã thiết bị (VD: TB_001)";
            this.txtMaTB.SelectedText = "";
            this.txtMaTB.Size = new System.Drawing.Size(333, 49);
            this.txtMaTB.TabIndex = 0;
            // 
            // txtTenTB
            // 
            this.txtTenTB.BorderRadius = 5;
            this.txtTenTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenTB.DefaultText = "";
            this.txtTenTB.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTenTB.Location = new System.Drawing.Point(427, 25);
            this.txtTenTB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTenTB.Name = "txtTenTB";
            this.txtTenTB.PlaceholderText = "Tên thiết bị";
            this.txtTenTB.SelectedText = "";
            this.txtTenTB.Size = new System.Drawing.Size(333, 49);
            this.txtTenTB.TabIndex = 1;
            // 
            // cboLoaiTB
            // 
            this.cboLoaiTB.BackColor = System.Drawing.Color.Transparent;
            this.cboLoaiTB.BorderRadius = 5;
            this.cboLoaiTB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiTB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiTB.FocusedColor = System.Drawing.Color.Empty;
            this.cboLoaiTB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLoaiTB.ItemHeight = 30;
            this.cboLoaiTB.Location = new System.Drawing.Point(40, 111);
            this.cboLoaiTB.Margin = new System.Windows.Forms.Padding(4);
            this.cboLoaiTB.Name = "cboLoaiTB";
            this.cboLoaiTB.Size = new System.Drawing.Size(332, 36);
            this.cboLoaiTB.TabIndex = 3;
            // 
            // cboKhoaPhong
            // 
            this.cboKhoaPhong.BackColor = System.Drawing.Color.Transparent;
            this.cboKhoaPhong.BorderRadius = 5;
            this.cboKhoaPhong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKhoaPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoaPhong.FocusedColor = System.Drawing.Color.Empty;
            this.cboKhoaPhong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboKhoaPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboKhoaPhong.ItemHeight = 30;
            this.cboKhoaPhong.Location = new System.Drawing.Point(427, 111);
            this.cboKhoaPhong.Margin = new System.Windows.Forms.Padding(4);
            this.cboKhoaPhong.Name = "cboKhoaPhong";
            this.cboKhoaPhong.Size = new System.Drawing.Size(332, 36);
            this.cboKhoaPhong.TabIndex = 4;
            // 
            // dtpNgayNhap
            // 
            this.dtpNgayNhap.BorderRadius = 5;
            this.dtpNgayNhap.Checked = true;
            this.dtpNgayNhap.FillColor = System.Drawing.Color.White;
            this.dtpNgayNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayNhap.Location = new System.Drawing.Point(813, 25);
            this.dtpNgayNhap.Margin = new System.Windows.Forms.Padding(4);
            this.dtpNgayNhap.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayNhap.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayNhap.Name = "dtpNgayNhap";
            this.dtpNgayNhap.Size = new System.Drawing.Size(187, 49);
            this.dtpNgayNhap.TabIndex = 2;
            this.dtpNgayNhap.Value = new System.DateTime(2026, 3, 14, 13, 52, 3, 242);
            // 
            // txtGiaTien
            // 
            this.txtGiaTien.BorderRadius = 5;
            this.txtGiaTien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGiaTien.DefaultText = "";
            this.txtGiaTien.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtGiaTien.Location = new System.Drawing.Point(1027, 25);
            this.txtGiaTien.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtGiaTien.Name = "txtGiaTien";
            this.txtGiaTien.PlaceholderText = "Giá nhập (VNĐ)";
            this.txtGiaTien.SelectedText = "";
            this.txtGiaTien.Size = new System.Drawing.Size(200, 49);
            this.txtGiaTien.TabIndex = 6;
            // 
            // cboTinhTrang
            // 
            this.cboTinhTrang.BackColor = System.Drawing.Color.Transparent;
            this.cboTinhTrang.BorderRadius = 5;
            this.cboTinhTrang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTinhTrang.FocusedColor = System.Drawing.Color.Empty;
            this.cboTinhTrang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTinhTrang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboTinhTrang.ItemHeight = 30;
            this.cboTinhTrang.Items.AddRange(new object[] {
            "Tốt",
            "Đang sử dụng",
            "Cần bảo trì",
            "Đã thanh lý"});
            this.cboTinhTrang.Location = new System.Drawing.Point(813, 111);
            this.cboTinhTrang.Margin = new System.Windows.Forms.Padding(4);
            this.cboTinhTrang.Name = "cboTinhTrang";
            this.cboTinhTrang.Size = new System.Drawing.Size(412, 36);
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
            this.pnlAction.Location = new System.Drawing.Point(0, 197);
            this.pnlAction.Margin = new System.Windows.Forms.Padding(4);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(1267, 86);
            this.pnlAction.TabIndex = 2;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 5;
            this.btnLamMoi.FillColor = System.Drawing.Color.Gray;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(600, 12);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(4);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(160, 55);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "LÀM MỚI";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 5;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(413, 12);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(4);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(160, 55);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "XÓA";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BorderRadius = 5;
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(227, 12);
            this.btnSua.Margin = new System.Windows.Forms.Padding(4);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(160, 55);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "CẬP NHẬT";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 5;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(40, 12);
            this.btnThem.Margin = new System.Windows.Forms.Padding(4);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(160, 55);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "THÊM";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
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
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvThietBi.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvThietBi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThietBi.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvThietBi.Location = new System.Drawing.Point(0, 283);
            this.dgvThietBi.Margin = new System.Windows.Forms.Padding(4);
            this.dgvThietBi.Name = "dgvThietBi";
            this.dgvThietBi.ReadOnly = true;
            this.dgvThietBi.RowHeadersVisible = false;
            this.dgvThietBi.RowHeadersWidth = 51;
            this.dgvThietBi.RowTemplate.Height = 35;
            this.dgvThietBi.Size = new System.Drawing.Size(1267, 505);
            this.dgvThietBi.TabIndex = 3;
            this.dgvThietBi.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvThietBi.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvThietBi.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvThietBi.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvThietBi.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvThietBi.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvThietBi.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvThietBi.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvThietBi.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvThietBi.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvThietBi.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvThietBi.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvThietBi.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvThietBi.ThemeStyle.ReadOnly = true;
            this.dgvThietBi.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvThietBi.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvThietBi.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvThietBi.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvThietBi.ThemeStyle.RowsStyle.Height = 35;
            this.dgvThietBi.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvThietBi.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvThietBi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThietBi_CellClick);
            this.dgvThietBi.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBaoTri_CellDoubleClick);
            // 
            // FrmQuanLyThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1267, 788);
            this.Controls.Add(this.dgvThietBi);
            this.Controls.Add(this.pnlAction);
            this.Controls.Add(this.pnlControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmQuanLyThietBi";
            this.Text = "Quản lý Thiết bị";
            this.Load += new System.EventHandler(this.FrmQuanLyThietBi_Load);
            this.pnlControls.ResumeLayout(false);
            this.pnlAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
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