namespace Lib_Equipment
{
    partial class FrmBaoTriThietBi
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
            this.pnlBody = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvThietBi = new Guna.UI2.WinForms.Guna2DataGridView();
            this.gbThongTin = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblHanhDong = new System.Windows.Forms.Label();
            this.lblCost = new System.Windows.Forms.Label();
            this.lblVendor = new System.Windows.Forms.Label();
            this.lblNgay = new System.Windows.Forms.Label();
            this.lblThietBi = new System.Windows.Forms.Label();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.btnThucHien = new Guna.UI2.WinForms.Guna2Button();
            this.cboHanhDong = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtDescription = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtCost = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtVendor = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpNgayBT = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.txtTenTB = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaTB = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaPhieu = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlTop.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).BeginInit();
            this.gbThongTin.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(325, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BẢO TRÌ VÀ THANH LÝ THIẾT BỊ";
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.dgvThietBi);
            this.pnlBody.Controls.Add(this.gbThongTin);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 60);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(20);
            this.pnlBody.Size = new System.Drawing.Size(950, 580);
            this.pnlBody.TabIndex = 1;
            // 
            // dgvThietBi
            // 
            this.dgvThietBi.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvThietBi.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvThietBi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvThietBi.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvThietBi.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvThietBi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThietBi.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvThietBi.Location = new System.Drawing.Point(400, 20);
            this.dgvThietBi.Name = "dgvThietBi";
            this.dgvThietBi.ReadOnly = true;
            this.dgvThietBi.RowHeadersVisible = false;
            this.dgvThietBi.RowTemplate.Height = 35;
            this.dgvThietBi.Size = new System.Drawing.Size(530, 540);
            this.dgvThietBi.TabIndex = 1;
            this.dgvThietBi.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvThietBi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThietBi_CellClick);
            // 
            // gbThongTin
            // 
            this.gbThongTin.BorderRadius = 5;
            this.gbThongTin.Controls.Add(this.lblDescription);
            this.gbThongTin.Controls.Add(this.lblHanhDong);
            this.gbThongTin.Controls.Add(this.lblCost);
            this.gbThongTin.Controls.Add(this.lblVendor);
            this.gbThongTin.Controls.Add(this.lblNgay);
            this.gbThongTin.Controls.Add(this.lblThietBi);
            this.gbThongTin.Controls.Add(this.lblMaPhieu);
            this.gbThongTin.Controls.Add(this.btnThucHien);
            this.gbThongTin.Controls.Add(this.cboHanhDong);
            this.gbThongTin.Controls.Add(this.txtDescription);
            this.gbThongTin.Controls.Add(this.txtCost);
            this.gbThongTin.Controls.Add(this.txtVendor);
            this.gbThongTin.Controls.Add(this.dtpNgayBT);
            this.gbThongTin.Controls.Add(this.txtTenTB);
            this.gbThongTin.Controls.Add(this.txtMaTB);
            this.gbThongTin.Controls.Add(this.txtMaPhieu);
            this.gbThongTin.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbThongTin.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbThongTin.ForeColor = System.Drawing.Color.White;
            this.gbThongTin.Location = new System.Drawing.Point(20, 20);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(380, 540);
            this.gbThongTin.TabIndex = 0;
            this.gbThongTin.Text = "CHI TIẾT PHIẾU";
            // 
            // lblMaPhieu
            // 
            this.lblMaPhieu.AutoSize = true;
            this.lblMaPhieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaPhieu.ForeColor = System.Drawing.Color.Black;
            this.lblMaPhieu.Location = new System.Drawing.Point(15, 45);
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.Size = new System.Drawing.Size(61, 15);
            this.lblMaPhieu.TabIndex = 9;
            this.lblMaPhieu.Text = "Mã phiếu:";
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.BorderRadius = 5;
            this.txtMaPhieu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaPhieu.DefaultText = "";
            this.txtMaPhieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaPhieu.ForeColor = System.Drawing.Color.Black;
            this.txtMaPhieu.Location = new System.Drawing.Point(15, 63);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.Size = new System.Drawing.Size(350, 32);
            this.txtMaPhieu.TabIndex = 0;
            // 
            // lblThietBi
            // 
            this.lblThietBi.AutoSize = true;
            this.lblThietBi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblThietBi.ForeColor = System.Drawing.Color.Black;
            this.lblThietBi.Location = new System.Drawing.Point(15, 102);
            this.lblThietBi.Name = "lblThietBi";
            this.lblThietBi.Size = new System.Drawing.Size(95, 15);
            this.lblThietBi.TabIndex = 10;
            this.lblThietBi.Text = "Thiết bị bảo trì:";
            // 
            // txtMaTB
            // 
            this.txtMaTB.BorderRadius = 5;
            this.txtMaTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaTB.DefaultText = "";
            this.txtMaTB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaTB.ForeColor = System.Drawing.Color.Black;
            this.txtMaTB.Location = new System.Drawing.Point(15, 120);
            this.txtMaTB.Name = "txtMaTB";
            this.txtMaTB.Size = new System.Drawing.Size(120, 32);
            this.txtMaTB.TabIndex = 1;
            // 
            // txtTenTB
            // 
            this.txtTenTB.BorderRadius = 5;
            this.txtTenTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenTB.DefaultText = "";
            this.txtTenTB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenTB.ForeColor = System.Drawing.Color.Black;
            this.txtTenTB.Location = new System.Drawing.Point(145, 120);
            this.txtTenTB.Name = "txtTenTB";
            this.txtTenTB.Size = new System.Drawing.Size(220, 32);
            this.txtTenTB.TabIndex = 2;
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNgay.ForeColor = System.Drawing.Color.Black;
            this.lblNgay.Location = new System.Drawing.Point(15, 159);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(96, 15);
            this.lblNgay.TabIndex = 11;
            this.lblNgay.Text = "Ngày thực hiện:";
            // 
            // dtpNgayBT
            // 
            this.dtpNgayBT.BorderRadius = 5;
            this.dtpNgayBT.Checked = true;
            this.dtpNgayBT.FillColor = System.Drawing.Color.White;
            this.dtpNgayBT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayBT.ForeColor = System.Drawing.Color.Black;
            this.dtpNgayBT.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBT.Location = new System.Drawing.Point(15, 177);
            this.dtpNgayBT.Name = "dtpNgayBT";
            this.dtpNgayBT.Size = new System.Drawing.Size(350, 32);
            this.dtpNgayBT.TabIndex = 3;
            // 
            // lblVendor
            // 
            this.lblVendor.AutoSize = true;
            this.lblVendor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVendor.ForeColor = System.Drawing.Color.Black;
            this.lblVendor.Location = new System.Drawing.Point(15, 216);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(155, 15);
            this.lblVendor.TabIndex = 12;
            this.lblVendor.Text = "Đơn vị thực hiện (Vendor):";
            // 
            // txtVendor
            // 
            this.txtVendor.BorderRadius = 5;
            this.txtVendor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtVendor.DefaultText = "";
            this.txtVendor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtVendor.ForeColor = System.Drawing.Color.Black;
            this.txtVendor.Location = new System.Drawing.Point(15, 234);
            this.txtVendor.Name = "txtVendor";
            this.txtVendor.Size = new System.Drawing.Size(350, 32);
            this.txtVendor.TabIndex = 4;
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCost.ForeColor = System.Drawing.Color.Black;
            this.lblCost.Location = new System.Drawing.Point(15, 273);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(126, 15);
            this.lblCost.TabIndex = 13;
            this.lblCost.Text = "Chi phí bảo trì (VNĐ):";
            // 
            // txtCost
            // 
            this.txtCost.BorderRadius = 5;
            this.txtCost.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCost.DefaultText = "";
            this.txtCost.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCost.ForeColor = System.Drawing.Color.Black;
            this.txtCost.Location = new System.Drawing.Point(15, 291);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(350, 32);
            this.txtCost.TabIndex = 5;
            // 
            // lblHanhDong
            // 
            this.lblHanhDong.AutoSize = true;
            this.lblHanhDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHanhDong.ForeColor = System.Drawing.Color.Black;
            this.lblHanhDong.Location = new System.Drawing.Point(15, 330);
            this.lblHanhDong.Name = "lblHanhDong";
            this.lblHanhDong.Size = new System.Drawing.Size(167, 15);
            this.lblHanhDong.TabIndex = 14;
            this.lblHanhDong.Text = "Hành động / Tình trạng mới:";
            // 
            // cboHanhDong
            // 
            this.cboHanhDong.BackColor = System.Drawing.Color.Transparent;
            this.cboHanhDong.BorderRadius = 5;
            this.cboHanhDong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboHanhDong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHanhDong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboHanhDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboHanhDong.ItemHeight = 30;
            this.cboHanhDong.Location = new System.Drawing.Point(15, 348);
            this.cboHanhDong.Name = "cboHanhDong";
            this.cboHanhDong.Size = new System.Drawing.Size(350, 36);
            this.cboHanhDong.TabIndex = 6;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            this.lblDescription.Location = new System.Drawing.Point(15, 387);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(102, 15);
            this.lblDescription.TabIndex = 15;
            this.lblDescription.Text = "Nội dung chi tiết:";
            // 
            // txtDescription
            // 
            this.txtDescription.BorderRadius = 5;
            this.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDescription.DefaultText = "";
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(15, 405);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(350, 65);
            this.txtDescription.TabIndex = 7;
            // 
            // btnThucHien
            // 
            this.btnThucHien.BorderRadius = 5;
            this.btnThucHien.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThucHien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThucHien.ForeColor = System.Drawing.Color.White;
            this.btnThucHien.Location = new System.Drawing.Point(15, 480);
            this.btnThucHien.Name = "btnThucHien";
            this.btnThucHien.Size = new System.Drawing.Size(350, 45);
            this.btnThucHien.TabIndex = 8;
            this.btnThucHien.Text = "THỰC HIỆN";
            this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // FrmBaoTriThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 640);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBaoTriThietBi";
            this.Text = "Bảo Trì Thiết Bị";
            this.Load += new System.EventHandler(this.FrmBaoTriThietBi_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).EndInit();
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlBody;
        private Guna.UI2.WinForms.Guna2DataGridView dgvThietBi;
        private Guna.UI2.WinForms.Guna2GroupBox gbThongTin;
        private Guna.UI2.WinForms.Guna2Button btnThucHien;
        private Guna.UI2.WinForms.Guna2ComboBox cboHanhDong;
        private Guna.UI2.WinForms.Guna2TextBox txtDescription;
        private Guna.UI2.WinForms.Guna2TextBox txtCost;
        private Guna.UI2.WinForms.Guna2TextBox txtVendor;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayBT;
        private Guna.UI2.WinForms.Guna2TextBox txtTenTB;
        private Guna.UI2.WinForms.Guna2TextBox txtMaTB;
        private Guna.UI2.WinForms.Guna2TextBox txtMaPhieu;
        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.Label lblThietBi;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.Label lblVendor;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Label lblHanhDong;
        private System.Windows.Forms.Label lblDescription;
    }
}