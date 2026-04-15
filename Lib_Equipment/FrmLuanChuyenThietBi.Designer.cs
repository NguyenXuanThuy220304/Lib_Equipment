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
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gbThongTinPhieu = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnThucHien = new Guna.UI2.WinForms.Guna2Button();
            this.txtLyDo = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpNgayChuyen = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDenKhoa = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblDenKhoa = new System.Windows.Forms.Label();
            this.cboTuKhoa = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.txtMaPhieu = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.gbDanhSachThietBi = new Guna.UI2.WinForms.Guna2GroupBox();
            this.dgvThietBi = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlLeft.SuspendLayout();
            this.gbThongTinPhieu.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.gbDanhSachThietBi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbThongTinPhieu);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(15, 10, 10, 15);
            this.pnlLeft.Size = new System.Drawing.Size(400, 788);
            this.pnlLeft.TabIndex = 1;
            // 
            // gbThongTinPhieu
            // 
            this.gbThongTinPhieu.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gbThongTinPhieu.BorderRadius = 10;
            this.gbThongTinPhieu.Controls.Add(this.btnThucHien);
            this.gbThongTinPhieu.Controls.Add(this.txtLyDo);
            this.gbThongTinPhieu.Controls.Add(this.dtpNgayChuyen);
            this.gbThongTinPhieu.Controls.Add(this.label4);
            this.gbThongTinPhieu.Controls.Add(this.cboDenKhoa);
            this.gbThongTinPhieu.Controls.Add(this.lblDenKhoa);
            this.gbThongTinPhieu.Controls.Add(this.cboTuKhoa);
            this.gbThongTinPhieu.Controls.Add(this.lblTuKhoa);
            this.gbThongTinPhieu.Controls.Add(this.txtMaPhieu);
            this.gbThongTinPhieu.Controls.Add(this.label1);
            this.gbThongTinPhieu.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.gbThongTinPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTinPhieu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbThongTinPhieu.ForeColor = System.Drawing.Color.White;
            this.gbThongTinPhieu.Location = new System.Drawing.Point(15, 10);
            this.gbThongTinPhieu.Name = "gbThongTinPhieu";
            this.gbThongTinPhieu.Size = new System.Drawing.Size(375, 763);
            this.gbThongTinPhieu.TabIndex = 0;
            this.gbThongTinPhieu.Text = "THÔNG TIN BÀN GIAO";
            this.gbThongTinPhieu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(20, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 21);
            this.label1.Text = "Mã phiếu:";
            // 
            // txtMaPhieu
            this.txtMaPhieu.BorderRadius = 5;
            this.txtMaPhieu.DefaultText = "";
            this.txtMaPhieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaPhieu.Location = new System.Drawing.Point(20, 85);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.ReadOnly = true;
            this.txtMaPhieu.Size = new System.Drawing.Size(335, 40);
            this.txtMaPhieu.TabIndex = 1;
            // 
            // lblTuKhoa
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.BackColor = System.Drawing.Color.Transparent;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTuKhoa.ForeColor = System.Drawing.Color.Gray;
            this.lblTuKhoa.Location = new System.Drawing.Point(20, 145);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(142, 21);
            this.lblTuKhoa.Text = "Từ Khoa (Nơi giao):";
            // 
            // cboTuKhoa
            this.cboTuKhoa.BorderRadius = 5;
            this.cboTuKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTuKhoa.ForeColor = System.Drawing.Color.Black;
            this.cboTuKhoa.Location = new System.Drawing.Point(20, 170);
            this.cboTuKhoa.Name = "cboTuKhoa";
            this.cboTuKhoa.Size = new System.Drawing.Size(335, 36);
            this.cboTuKhoa.TabIndex = 2;
            this.cboTuKhoa.SelectedIndexChanged += new System.EventHandler(this.cboTuKhoa_SelectedIndexChanged);
            // 
            // lblDenKhoa
            this.lblDenKhoa.AutoSize = true;
            this.lblDenKhoa.BackColor = System.Drawing.Color.Transparent;
            this.lblDenKhoa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDenKhoa.ForeColor = System.Drawing.Color.Gray;
            this.lblDenKhoa.Location = new System.Drawing.Point(20, 230);
            this.lblDenKhoa.Name = "lblDenKhoa";
            this.lblDenKhoa.Size = new System.Drawing.Size(155, 21);
            this.lblDenKhoa.Text = "Đến Khoa (Nơi nhận):";
            // 
            // cboDenKhoa
            this.cboDenKhoa.BorderRadius = 5;
            this.cboDenKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDenKhoa.ForeColor = System.Drawing.Color.Black;
            this.cboDenKhoa.Location = new System.Drawing.Point(20, 255);
            this.cboDenKhoa.Name = "cboDenKhoa";
            this.cboDenKhoa.Size = new System.Drawing.Size(335, 36);
            this.cboDenKhoa.TabIndex = 3;
            // 
            // label4
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(20, 315);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 21);
            this.label4.Text = "Ngày bàn giao:";
            // 
            // dtpNgayChuyen
            this.dtpNgayChuyen.BorderRadius = 5;
            this.dtpNgayChuyen.Checked = true;
            this.dtpNgayChuyen.FillColor = System.Drawing.Color.White;
            this.dtpNgayChuyen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayChuyen.ForeColor = System.Drawing.Color.Black;
            this.dtpNgayChuyen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayChuyen.Location = new System.Drawing.Point(20, 340);
            this.dtpNgayChuyen.Name = "dtpNgayChuyen";
            this.dtpNgayChuyen.Size = new System.Drawing.Size(335, 40);
            this.dtpNgayChuyen.TabIndex = 4;
            this.dtpNgayChuyen.Value = new System.DateTime(2026, 4, 15, 0, 0, 0, 0);
            // 
            // txtLyDo
            this.txtLyDo.BorderRadius = 5;
            this.txtLyDo.DefaultText = "";
            this.txtLyDo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLyDo.Location = new System.Drawing.Point(20, 410);
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.PlaceholderText = "Lý do bàn giao...";
            this.txtLyDo.Size = new System.Drawing.Size(335, 100);
            this.txtLyDo.TabIndex = 5;
            // 
            // btnThucHien
            // 
            this.btnThucHien.BorderRadius = 8;
            this.btnThucHien.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnThucHien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThucHien.ForeColor = System.Drawing.Color.White;
            this.btnThucHien.Location = new System.Drawing.Point(20, 540);
            this.btnThucHien.Name = "btnThucHien";
            this.btnThucHien.Size = new System.Drawing.Size(335, 55);
            this.btnThucHien.TabIndex = 6;
            this.btnThucHien.Text = "XÁC NHẬN BÀN GIAO";
            this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.gbDanhSachThietBi);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(400, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10, 10, 15, 15);
            this.pnlRight.Size = new System.Drawing.Size(867, 788);
            this.pnlRight.TabIndex = 2;
            // 
            // gbDanhSachThietBi
            // 
            this.gbDanhSachThietBi.BorderRadius = 10;
            this.gbDanhSachThietBi.Controls.Add(this.dgvThietBi);
            this.gbDanhSachThietBi.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.gbDanhSachThietBi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDanhSachThietBi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbDanhSachThietBi.ForeColor = System.Drawing.Color.White;
            this.gbDanhSachThietBi.Location = new System.Drawing.Point(10, 10);
            this.gbDanhSachThietBi.Name = "gbDanhSachThietBi";
            this.gbDanhSachThietBi.Size = new System.Drawing.Size(842, 763);
            this.gbDanhSachThietBi.Text = "DANH SÁCH THIẾT BỊ BÀN GIAO";
            // 
            // dgvThietBi (TỐI ƯU CỰC MẠNH: KHÓA SỬA + ĐẸP Ô TÍCH)
            // 
            this.dgvThietBi.AllowUserToAddRows = false;
            this.dgvThietBi.AllowUserToDeleteRows = false;
            this.dgvThietBi.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvThietBi.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvThietBi.BackgroundColor = System.Drawing.Color.White;
            this.dgvThietBi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvThietBi.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvThietBi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvThietBi.ColumnHeadersHeight = 45;
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
            this.dgvThietBi.Location = new System.Drawing.Point(0, 40);
            this.dgvThietBi.Name = "dgvThietBi";
            this.dgvThietBi.ReadOnly = false; // Phải để False để tick được checkbox
            this.dgvThietBi.RowHeadersVisible = false;
            this.dgvThietBi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThietBi.ShowCellErrors = false;
            this.dgvThietBi.ShowCellToolTips = false;
            this.dgvThietBi.ShowEditingIcon = false;
            this.dgvThietBi.ShowRowErrors = false;
            this.dgvThietBi.Size = new System.Drawing.Size(842, 723);
            this.dgvThietBi.TabIndex = 0;
            this.dgvThietBi.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvThietBi.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            // 
            // FrmLuanChuyenThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1267, 788);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLuanChuyenThietBi";
            this.Load += new System.EventHandler(this.FrmLuanChuyenThietBi_Load);
            this.pnlLeft.ResumeLayout(false);
            this.gbThongTinPhieu.ResumeLayout(false);
            this.gbThongTinPhieu.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.gbDanhSachThietBi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
    }
}