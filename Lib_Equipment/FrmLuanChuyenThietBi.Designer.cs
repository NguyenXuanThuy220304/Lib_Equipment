namespace Lib_Equipment
{
    partial class FrmLuanChuyenThietBi
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
            this.gbThongTinPhieu = new Guna.UI2.WinForms.Guna2GroupBox();
            this.txtMaPhieu = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.cboTuKhoa = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblDenKhoa = new System.Windows.Forms.Label();
            this.cboDenKhoa = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dtpNgayChuyen = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.txtLyDo = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnThucHien = new Guna.UI2.WinForms.Guna2Button();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.gbDanhSachThietBi = new Guna.UI2.WinForms.Guna2GroupBox();
            this.dgvThietBi = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.gbThongTinPhieu.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.gbDanhSachThietBi.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(432, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "LUÂN CHUYỂN VÀ CẤP PHÁT THIẾT BỊ";
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbThongTinPhieu);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 60);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(20, 10, 10, 20);
            this.pnlLeft.Size = new System.Drawing.Size(400, 580);
            this.pnlLeft.TabIndex = 1;
            // 
            // gbThongTinPhieu
            // 
            this.gbThongTinPhieu.BorderRadius = 5;
            this.gbThongTinPhieu.Controls.Add(this.btnThucHien);
            this.gbThongTinPhieu.Controls.Add(this.txtLyDo);
            this.gbThongTinPhieu.Controls.Add(this.dtpNgayChuyen);
            this.gbThongTinPhieu.Controls.Add(this.cboDenKhoa);
            this.gbThongTinPhieu.Controls.Add(this.lblDenKhoa);
            this.gbThongTinPhieu.Controls.Add(this.cboTuKhoa);
            this.gbThongTinPhieu.Controls.Add(this.lblTuKhoa);
            this.gbThongTinPhieu.Controls.Add(this.txtMaPhieu);
            this.gbThongTinPhieu.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.gbThongTinPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTinPhieu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbThongTinPhieu.ForeColor = System.Drawing.Color.White;
            this.gbThongTinPhieu.Location = new System.Drawing.Point(20, 10);
            this.gbThongTinPhieu.Name = "gbThongTinPhieu";
            this.gbThongTinPhieu.Size = new System.Drawing.Size(370, 550);
            this.gbThongTinPhieu.TabIndex = 0;
            this.gbThongTinPhieu.Text = "THÔNG TIN PHIẾU CHUYỂN";
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.BorderRadius = 5;
            this.txtMaPhieu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaPhieu.DefaultText = "";
            this.txtMaPhieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaPhieu.ForeColor = System.Drawing.Color.Black;
            this.txtMaPhieu.Location = new System.Drawing.Point(20, 60);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.PlaceholderText = "Mã phiếu (VD: LC_001)";
            this.txtMaPhieu.Size = new System.Drawing.Size(330, 40);
            this.txtMaPhieu.TabIndex = 0;
            // 
            // lblTuKhoa
            // 
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTuKhoa.ForeColor = System.Drawing.Color.Black;
            this.lblTuKhoa.Location = new System.Drawing.Point(16, 120);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(155, 19);
            this.lblTuKhoa.TabIndex = 1;
            this.lblTuKhoa.Text = "TỪ KHOA (Nơi chuyển):";
            // 
            // cboTuKhoa
            // 
            this.cboTuKhoa.BackColor = System.Drawing.Color.Transparent;
            this.cboTuKhoa.BorderRadius = 5;
            this.cboTuKhoa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTuKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTuKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTuKhoa.ForeColor = System.Drawing.Color.Black;
            this.cboTuKhoa.ItemHeight = 30;
            this.cboTuKhoa.Location = new System.Drawing.Point(20, 145);
            this.cboTuKhoa.Name = "cboTuKhoa";
            this.cboTuKhoa.Size = new System.Drawing.Size(330, 36);
            this.cboTuKhoa.TabIndex = 2;
            this.cboTuKhoa.SelectedIndexChanged += new System.EventHandler(this.cboTuKhoa_SelectedIndexChanged);
            // 
            // lblDenKhoa
            // 
            this.lblDenKhoa.AutoSize = true;
            this.lblDenKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDenKhoa.ForeColor = System.Drawing.Color.Black;
            this.lblDenKhoa.Location = new System.Drawing.Point(16, 200);
            this.lblDenKhoa.Name = "lblDenKhoa";
            this.lblDenKhoa.Size = new System.Drawing.Size(155, 19);
            this.lblDenKhoa.TabIndex = 3;
            this.lblDenKhoa.Text = "ĐẾN KHOA (Nơi nhận):";
            // 
            // cboDenKhoa
            // 
            this.cboDenKhoa.BackColor = System.Drawing.Color.Transparent;
            this.cboDenKhoa.BorderRadius = 5;
            this.cboDenKhoa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDenKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDenKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDenKhoa.ForeColor = System.Drawing.Color.Black;
            this.cboDenKhoa.ItemHeight = 30;
            this.cboDenKhoa.Location = new System.Drawing.Point(20, 225);
            this.cboDenKhoa.Name = "cboDenKhoa";
            this.cboDenKhoa.Size = new System.Drawing.Size(330, 36);
            this.cboDenKhoa.TabIndex = 4;
            // 
            // dtpNgayChuyen
            // 
            this.dtpNgayChuyen.BorderRadius = 5;
            this.dtpNgayChuyen.Checked = true;
            this.dtpNgayChuyen.FillColor = System.Drawing.Color.White;
            this.dtpNgayChuyen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayChuyen.ForeColor = System.Drawing.Color.Black;
            this.dtpNgayChuyen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayChuyen.Location = new System.Drawing.Point(20, 285);
            this.dtpNgayChuyen.Name = "dtpNgayChuyen";
            this.dtpNgayChuyen.Size = new System.Drawing.Size(330, 40);
            this.dtpNgayChuyen.TabIndex = 5;
            // 
            // txtLyDo
            // 
            this.txtLyDo.BorderRadius = 5;
            this.txtLyDo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLyDo.DefaultText = "";
            this.txtLyDo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLyDo.ForeColor = System.Drawing.Color.Black;
            this.txtLyDo.Location = new System.Drawing.Point(20, 345);
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.PlaceholderText = "Lý do luân chuyển...";
            this.txtLyDo.Size = new System.Drawing.Size(330, 80);
            this.txtLyDo.TabIndex = 6;
            // 
            // btnThucHien
            // 
            this.btnThucHien.BorderRadius = 5;
            this.btnThucHien.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThucHien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThucHien.ForeColor = System.Drawing.Color.White;
            this.btnThucHien.Location = new System.Drawing.Point(20, 450);
            this.btnThucHien.Name = "btnThucHien";
            this.btnThucHien.Size = new System.Drawing.Size(330, 50);
            this.btnThucHien.TabIndex = 7;
            this.btnThucHien.Text = "THỰC HIỆN LUÂN CHUYỂN";
            this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.gbDanhSachThietBi);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(400, 60);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10, 10, 20, 20);
            this.pnlRight.Size = new System.Drawing.Size(550, 580);
            this.pnlRight.TabIndex = 2;
            // 
            // gbDanhSachThietBi
            // 
            this.gbDanhSachThietBi.BorderRadius = 5;
            this.gbDanhSachThietBi.Controls.Add(this.dgvThietBi);
            this.gbDanhSachThietBi.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.gbDanhSachThietBi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDanhSachThietBi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbDanhSachThietBi.ForeColor = System.Drawing.Color.White;
            this.gbDanhSachThietBi.Location = new System.Drawing.Point(10, 10);
            this.gbDanhSachThietBi.Name = "gbDanhSachThietBi";
            this.gbDanhSachThietBi.Size = new System.Drawing.Size(520, 550);
            this.gbDanhSachThietBi.TabIndex = 0;
            this.gbDanhSachThietBi.Text = "CHỌN THIẾT BỊ CẦN CHUYỂN";
            // 
            // dgvThietBi
            // 
            this.dgvThietBi.AllowUserToAddRows = false;
            this.dgvThietBi.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvThietBi.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
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
            this.dgvThietBi.Location = new System.Drawing.Point(0, 40);
            this.dgvThietBi.Name = "dgvThietBi";
            this.dgvThietBi.RowHeadersVisible = false;
            this.dgvThietBi.RowTemplate.Height = 35;
            this.dgvThietBi.Size = new System.Drawing.Size(520, 510);
            this.dgvThietBi.TabIndex = 0;
            this.dgvThietBi.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            // 
            // FrmLuanChuyenThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(950, 640);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLuanChuyenThietBi";
            this.Text = "Luân chuyển Thiết bị";
            this.Load += new System.EventHandler(this.FrmLuanChuyenThietBi_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.gbThongTinPhieu.ResumeLayout(false);
            this.gbThongTinPhieu.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.gbDanhSachThietBi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlLeft;
        private Guna.UI2.WinForms.Guna2GroupBox gbThongTinPhieu;
        private Guna.UI2.WinForms.Guna2Panel pnlRight;
        private Guna.UI2.WinForms.Guna2GroupBox gbDanhSachThietBi;
        private Guna.UI2.WinForms.Guna2TextBox txtMaPhieu;
        private System.Windows.Forms.Label lblTuKhoa;
        private Guna.UI2.WinForms.Guna2ComboBox cboTuKhoa;
        private System.Windows.Forms.Label lblDenKhoa;
        private Guna.UI2.WinForms.Guna2ComboBox cboDenKhoa;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayChuyen;
        private Guna.UI2.WinForms.Guna2TextBox txtLyDo;
        private Guna.UI2.WinForms.Guna2Button btnThucHien;
        private Guna.UI2.WinForms.Guna2DataGridView dgvThietBi;
    }
}