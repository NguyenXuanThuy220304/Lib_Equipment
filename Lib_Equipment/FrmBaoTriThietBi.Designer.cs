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
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).BeginInit();
            this.gbThongTin.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.dgvThietBi);
            this.pnlBody.Controls.Add(this.gbThongTin);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.pnlBody.Size = new System.Drawing.Size(1267, 788);
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
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvThietBi.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvThietBi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThietBi.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvThietBi.Location = new System.Drawing.Point(534, 25);
            this.dgvThietBi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvThietBi.Name = "dgvThietBi";
            this.dgvThietBi.ReadOnly = true;
            this.dgvThietBi.RowHeadersVisible = false;
            this.dgvThietBi.RowHeadersWidth = 51;
            this.dgvThietBi.RowTemplate.Height = 35;
            this.dgvThietBi.Size = new System.Drawing.Size(706, 738);
            this.dgvThietBi.TabIndex = 1;
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
            this.gbThongTin.Location = new System.Drawing.Point(27, 25);
            this.gbThongTin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(507, 738);
            this.gbThongTin.TabIndex = 0;
            this.gbThongTin.Text = "CHI TIẾT PHIẾU";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            this.lblDescription.Location = new System.Drawing.Point(20, 476);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(130, 20);
            this.lblDescription.TabIndex = 15;
            this.lblDescription.Text = "Nội dung chi tiết:";
            // 
            // lblHanhDong
            // 
            this.lblHanhDong.AutoSize = true;
            this.lblHanhDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHanhDong.ForeColor = System.Drawing.Color.Black;
            this.lblHanhDong.Location = new System.Drawing.Point(20, 406);
            this.lblHanhDong.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHanhDong.Name = "lblHanhDong";
            this.lblHanhDong.Size = new System.Drawing.Size(209, 20);
            this.lblHanhDong.TabIndex = 14;
            this.lblHanhDong.Text = "Hành động / Tình trạng mới:";
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCost.ForeColor = System.Drawing.Color.Black;
            this.lblCost.Location = new System.Drawing.Point(20, 336);
            this.lblCost.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(160, 20);
            this.lblCost.TabIndex = 13;
            this.lblCost.Text = "Chi phí bảo trì (VNĐ):";
            // 
            // lblVendor
            // 
            this.lblVendor.AutoSize = true;
            this.lblVendor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVendor.ForeColor = System.Drawing.Color.Black;
            this.lblVendor.Location = new System.Drawing.Point(20, 266);
            this.lblVendor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(194, 20);
            this.lblVendor.TabIndex = 12;
            this.lblVendor.Text = "Đơn vị thực hiện (Vendor):";
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNgay.ForeColor = System.Drawing.Color.Black;
            this.lblNgay.Location = new System.Drawing.Point(20, 196);
            this.lblNgay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(120, 20);
            this.lblNgay.TabIndex = 11;
            this.lblNgay.Text = "Ngày thực hiện:";
            // 
            // lblThietBi
            // 
            this.lblThietBi.AutoSize = true;
            this.lblThietBi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblThietBi.ForeColor = System.Drawing.Color.Black;
            this.lblThietBi.Location = new System.Drawing.Point(20, 126);
            this.lblThietBi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblThietBi.Name = "lblThietBi";
            this.lblThietBi.Size = new System.Drawing.Size(116, 20);
            this.lblThietBi.TabIndex = 10;
            this.lblThietBi.Text = "Thiết bị bảo trì:";
            // 
            // lblMaPhieu
            // 
            this.lblMaPhieu.AutoSize = true;
            this.lblMaPhieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaPhieu.ForeColor = System.Drawing.Color.Black;
            this.lblMaPhieu.Location = new System.Drawing.Point(20, 55);
            this.lblMaPhieu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.Size = new System.Drawing.Size(78, 20);
            this.lblMaPhieu.TabIndex = 9;
            this.lblMaPhieu.Text = "Mã phiếu:";
            // 
            // btnThucHien
            // 
            this.btnThucHien.BorderRadius = 5;
            this.btnThucHien.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThucHien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThucHien.ForeColor = System.Drawing.Color.White;
            this.btnThucHien.Location = new System.Drawing.Point(20, 591);
            this.btnThucHien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThucHien.Name = "btnThucHien";
            this.btnThucHien.Size = new System.Drawing.Size(467, 55);
            this.btnThucHien.TabIndex = 8;
            this.btnThucHien.Text = "THỰC HIỆN";
            this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // cboHanhDong
            // 
            this.cboHanhDong.BackColor = System.Drawing.Color.Transparent;
            this.cboHanhDong.BorderRadius = 5;
            this.cboHanhDong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboHanhDong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHanhDong.FocusedColor = System.Drawing.Color.Empty;
            this.cboHanhDong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboHanhDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboHanhDong.ItemHeight = 30;
            this.cboHanhDong.Location = new System.Drawing.Point(20, 428);
            this.cboHanhDong.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboHanhDong.Name = "cboHanhDong";
            this.cboHanhDong.Size = new System.Drawing.Size(465, 36);
            this.cboHanhDong.TabIndex = 6;
            // 
            // txtDescription
            // 
            this.txtDescription.BorderRadius = 5;
            this.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDescription.DefaultText = "";
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(20, 498);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.PlaceholderText = "";
            this.txtDescription.SelectedText = "";
            this.txtDescription.Size = new System.Drawing.Size(467, 80);
            this.txtDescription.TabIndex = 7;
            // 
            // txtCost
            // 
            this.txtCost.BorderRadius = 5;
            this.txtCost.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCost.DefaultText = "";
            this.txtCost.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCost.ForeColor = System.Drawing.Color.Black;
            this.txtCost.Location = new System.Drawing.Point(20, 358);
            this.txtCost.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtCost.Name = "txtCost";
            this.txtCost.PlaceholderText = "";
            this.txtCost.SelectedText = "";
            this.txtCost.Size = new System.Drawing.Size(467, 39);
            this.txtCost.TabIndex = 5;
            // 
            // txtVendor
            // 
            this.txtVendor.BorderRadius = 5;
            this.txtVendor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtVendor.DefaultText = "";
            this.txtVendor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtVendor.ForeColor = System.Drawing.Color.Black;
            this.txtVendor.Location = new System.Drawing.Point(20, 288);
            this.txtVendor.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtVendor.Name = "txtVendor";
            this.txtVendor.PlaceholderText = "";
            this.txtVendor.SelectedText = "";
            this.txtVendor.Size = new System.Drawing.Size(467, 39);
            this.txtVendor.TabIndex = 4;
            // 
            // dtpNgayBT
            // 
            this.dtpNgayBT.BorderRadius = 5;
            this.dtpNgayBT.Checked = true;
            this.dtpNgayBT.FillColor = System.Drawing.Color.White;
            this.dtpNgayBT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayBT.ForeColor = System.Drawing.Color.Black;
            this.dtpNgayBT.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBT.Location = new System.Drawing.Point(20, 218);
            this.dtpNgayBT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpNgayBT.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayBT.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayBT.Name = "dtpNgayBT";
            this.dtpNgayBT.Size = new System.Drawing.Size(467, 39);
            this.dtpNgayBT.TabIndex = 3;
            this.dtpNgayBT.Value = new System.DateTime(2026, 3, 14, 13, 51, 16, 177);
            // 
            // txtTenTB
            // 
            this.txtTenTB.BorderRadius = 5;
            this.txtTenTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenTB.DefaultText = "";
            this.txtTenTB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenTB.ForeColor = System.Drawing.Color.Black;
            this.txtTenTB.Location = new System.Drawing.Point(193, 148);
            this.txtTenTB.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtTenTB.Name = "txtTenTB";
            this.txtTenTB.PlaceholderText = "";
            this.txtTenTB.SelectedText = "";
            this.txtTenTB.Size = new System.Drawing.Size(293, 39);
            this.txtTenTB.TabIndex = 2;
            // 
            // txtMaTB
            // 
            this.txtMaTB.BorderRadius = 5;
            this.txtMaTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaTB.DefaultText = "";
            this.txtMaTB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaTB.ForeColor = System.Drawing.Color.Black;
            this.txtMaTB.Location = new System.Drawing.Point(20, 148);
            this.txtMaTB.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtMaTB.Name = "txtMaTB";
            this.txtMaTB.PlaceholderText = "";
            this.txtMaTB.SelectedText = "";
            this.txtMaTB.Size = new System.Drawing.Size(160, 39);
            this.txtMaTB.TabIndex = 1;
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.BorderRadius = 5;
            this.txtMaPhieu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaPhieu.DefaultText = "";
            this.txtMaPhieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaPhieu.ForeColor = System.Drawing.Color.Black;
            this.txtMaPhieu.Location = new System.Drawing.Point(20, 78);
            this.txtMaPhieu.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.PlaceholderText = "";
            this.txtMaPhieu.SelectedText = "";
            this.txtMaPhieu.Size = new System.Drawing.Size(467, 39);
            this.txtMaPhieu.TabIndex = 0;
            // 
            // FrmBaoTriThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 788);
            this.Controls.Add(this.pnlBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmBaoTriThietBi";
            this.Text = "Bảo Trì Thiết Bị";
            this.Load += new System.EventHandler(this.FrmBaoTriThietBi_Load);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).EndInit();
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
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