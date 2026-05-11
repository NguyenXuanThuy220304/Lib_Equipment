using System;
using System.Drawing;

namespace Lib_Equipment
{
    partial class FrmQuanLyDocGia
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlControls = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDebugMail = new Guna.UI2.WinForms.Guna2Button();
            this.btnDongBo = new Guna.UI2.WinForms.Guna2Button();
            this.txtMaDocGia = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.cboDonVi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboLoaiDocGia = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnInThe = new Guna.UI2.WinForms.Guna2Button();
            this.dgvDocGia = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocGia)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControls
            // 
            this.pnlControls.BackColor = System.Drawing.Color.White;
            this.pnlControls.Controls.Add(this.btnDebugMail);
            this.pnlControls.Controls.Add(this.btnDongBo);
            this.pnlControls.Controls.Add(this.txtMaDocGia);
            this.pnlControls.Controls.Add(this.txtMail);
            this.pnlControls.Controls.Add(this.txtHoTen);
            this.pnlControls.Controls.Add(this.cboDonVi);
            this.pnlControls.Controls.Add(this.cboLoaiDocGia);
            this.pnlControls.Controls.Add(this.cboTrangThai);
            this.pnlControls.Controls.Add(this.btnLamMoi);
            this.pnlControls.Controls.Add(this.btnXoa);
            this.pnlControls.Controls.Add(this.btnSua);
            this.pnlControls.Controls.Add(this.btnThem);
            this.pnlControls.Controls.Add(this.btnInThe);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1267, 246);
            this.pnlControls.TabIndex = 1;
            // 
            // btnDebugMail
            // 
            this.btnDebugMail.BorderRadius = 5;
            this.btnDebugMail.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnDebugMail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDebugMail.ForeColor = System.Drawing.Color.White;
            this.btnDebugMail.Location = new System.Drawing.Point(863, 172);
            this.btnDebugMail.Name = "btnDebugMail";
            this.btnDebugMail.Size = new System.Drawing.Size(147, 49);
            this.btnDebugMail.TabIndex = 9;
            this.btnDebugMail.Text = "Debug mail";
            this.btnDebugMail.Click += new System.EventHandler(this.btnDebugMail_Click);
            // 
            // btnDongBo
            // 
            this.btnDongBo.BorderRadius = 5;
            this.btnDongBo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnDongBo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDongBo.ForeColor = System.Drawing.Color.White;
            this.btnDongBo.Location = new System.Drawing.Point(698, 172);
            this.btnDongBo.Name = "btnDongBo";
            this.btnDongBo.Size = new System.Drawing.Size(147, 49);
            this.btnDongBo.TabIndex = 9;
            this.btnDongBo.Text = "ĐỒNG BỘ";
            this.btnDongBo.Click += new System.EventHandler(this.btnDongBo_Click);
            // 
            // txtMaDocGia
            // 
            this.txtMaDocGia.BorderRadius = 5;
            this.txtMaDocGia.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaDocGia.DefaultText = "";
            this.txtMaDocGia.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtMaDocGia.Location = new System.Drawing.Point(40, 25);
            this.txtMaDocGia.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaDocGia.Name = "txtMaDocGia";
            this.txtMaDocGia.PlaceholderText = "Mã Độc giả (Mã SV/GV)";
            this.txtMaDocGia.SelectedText = "";
            this.txtMaDocGia.Size = new System.Drawing.Size(267, 49);
            this.txtMaDocGia.TabIndex = 4;
            // 
            // txtMail
            // 
            this.txtMail.BorderRadius = 5;
            this.txtMail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMail.DefaultText = "";
            this.txtMail.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtMail.Location = new System.Drawing.Point(347, 98);
            this.txtMail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMail.Name = "txtMail";
            this.txtMail.PlaceholderText = "Email";
            this.txtMail.SelectedText = "";
            this.txtMail.Size = new System.Drawing.Size(333, 49);
            this.txtMail.TabIndex = 5;
            // 
            // txtHoTen
            // 
            this.txtHoTen.BorderRadius = 5;
            this.txtHoTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHoTen.DefaultText = "";
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtHoTen.Location = new System.Drawing.Point(347, 25);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.PlaceholderText = "Họ và tên";
            this.txtHoTen.SelectedText = "";
            this.txtHoTen.Size = new System.Drawing.Size(333, 49);
            this.txtHoTen.TabIndex = 5;
            // 
            // cboDonVi
            // 
            this.cboDonVi.BackColor = System.Drawing.Color.Transparent;
            this.cboDonVi.BorderRadius = 5;
            this.cboDonVi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDonVi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDonVi.FocusedColor = System.Drawing.Color.Empty;
            this.cboDonVi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDonVi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboDonVi.ItemHeight = 43;
            this.cboDonVi.Location = new System.Drawing.Point(720, 25);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Size = new System.Drawing.Size(332, 49);
            this.cboDonVi.TabIndex = 6;
            // 
            // cboLoaiDocGia
            // 
            this.cboLoaiDocGia.BackColor = System.Drawing.Color.Transparent;
            this.cboLoaiDocGia.BorderRadius = 5;
            this.cboLoaiDocGia.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiDocGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiDocGia.FocusedColor = System.Drawing.Color.Empty;
            this.cboLoaiDocGia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiDocGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLoaiDocGia.ItemHeight = 43;
            this.cboLoaiDocGia.Items.AddRange(new object[] {
            "Sinh viên",
            "Giảng viên"});
            this.cboLoaiDocGia.Location = new System.Drawing.Point(40, 98);
            this.cboLoaiDocGia.Name = "cboLoaiDocGia";
            this.cboLoaiDocGia.Size = new System.Drawing.Size(265, 49);
            this.cboLoaiDocGia.StartIndex = 0;
            this.cboLoaiDocGia.TabIndex = 7;
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.BackColor = System.Drawing.Color.Transparent;
            this.cboTrangThai.BorderRadius = 5;
            this.cboTrangThai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FocusedColor = System.Drawing.Color.Empty;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboTrangThai.ItemHeight = 43;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Hoạt động",
            "Khóa"});
            this.cboTrangThai.Location = new System.Drawing.Point(720, 98);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(332, 49);
            this.cboTrangThai.StartIndex = 0;
            this.cboTrangThai.TabIndex = 8;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 5;
            this.btnLamMoi.FillColor = System.Drawing.Color.Gray;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(533, 172);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(147, 49);
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
            this.btnXoa.Location = new System.Drawing.Point(364, 172);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(147, 49);
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
            this.btnSua.Location = new System.Drawing.Point(202, 172);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(147, 49);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "SỬA";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 5;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(40, 172);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(147, 49);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "THÊM";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnInThe
            // 
            this.btnInThe.BorderRadius = 5;
            this.btnInThe.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnInThe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnInThe.ForeColor = System.Drawing.Color.White;
            this.btnInThe.Location = new System.Drawing.Point(1027, 172);
            this.btnInThe.Name = "btnInThe";
            this.btnInThe.Size = new System.Drawing.Size(130, 49);
            this.btnInThe.TabIndex = 10;
            this.btnInThe.Text = "IN THẺ SV";
            this.btnInThe.Click += new System.EventHandler(this.btnInThe_Click);
            // 
            // dgvDocGia
            // 
            this.dgvDocGia.AllowUserToAddRows = false;
            this.dgvDocGia.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDocGia.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDocGia.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDocGia.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDocGia.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDocGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDocGia.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDocGia.Location = new System.Drawing.Point(0, 246);
            this.dgvDocGia.Name = "dgvDocGia";
            this.dgvDocGia.ReadOnly = true;
            this.dgvDocGia.RowHeadersVisible = false;
            this.dgvDocGia.RowHeadersWidth = 51;
            this.dgvDocGia.RowTemplate.Height = 35;
            this.dgvDocGia.Size = new System.Drawing.Size(1267, 542);
            this.dgvDocGia.TabIndex = 2;
            this.dgvDocGia.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDocGia.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvDocGia.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvDocGia.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvDocGia.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvDocGia.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvDocGia.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDocGia.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvDocGia.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDocGia.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDocGia.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDocGia.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDocGia.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvDocGia.ThemeStyle.ReadOnly = true;
            this.dgvDocGia.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDocGia.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDocGia.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDocGia.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDocGia.ThemeStyle.RowsStyle.Height = 35;
            this.dgvDocGia.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDocGia.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDocGia.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDocGia_CellClick);
            this.dgvDocGia.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDocGia_CellDoubleClick);
            // 
            // FrmQuanLyDocGia
            // 
            this.ClientSize = new System.Drawing.Size(1267, 788);
            this.Controls.Add(this.dgvDocGia);
            this.Controls.Add(this.pnlControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmQuanLyDocGia";
            this.Text = "Quản lý Độc giả";
            this.Load += new System.EventHandler(this.FrmQuanLyDocGia_Load);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocGia)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private Guna.UI2.WinForms.Guna2Button btnInThe;
        private Guna.UI2.WinForms.Guna2Panel pnlControls;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDocGia;
        private Guna.UI2.WinForms.Guna2TextBox txtHoTen;
        private Guna.UI2.WinForms.Guna2TextBox txtMaDocGia;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2ComboBox cboDonVi;
        private Guna.UI2.WinForms.Guna2ComboBox cboTrangThai;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoaiDocGia;
        private Guna.UI2.WinForms.Guna2Button btnDongBo;
        private Guna.UI2.WinForms.Guna2Button btnDebugMail;
        private Guna.UI2.WinForms.Guna2TextBox txtMail;
    }
}