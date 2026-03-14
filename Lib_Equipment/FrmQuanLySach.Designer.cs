namespace Lib_Equipment
{
    partial class FrmQuanLySach
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
            this.txtMaSach = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTenSach = new Guna.UI2.WinForms.Guna2TextBox();
            this.cboTheLoai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtTacGia = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNhaXuatBan = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNamXuatBan = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.dgvSach = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControls
            // 
            this.pnlControls.BackColor = System.Drawing.Color.White;
            this.pnlControls.Controls.Add(this.txtMaSach);
            this.pnlControls.Controls.Add(this.txtTenSach);
            this.pnlControls.Controls.Add(this.cboTheLoai);
            this.pnlControls.Controls.Add(this.txtTacGia);
            this.pnlControls.Controls.Add(this.txtNhaXuatBan);
            this.pnlControls.Controls.Add(this.txtNamXuatBan);
            this.pnlControls.Controls.Add(this.btnLamMoi);
            this.pnlControls.Controls.Add(this.btnXoa);
            this.pnlControls.Controls.Add(this.btnSua);
            this.pnlControls.Controls.Add(this.btnThem);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(950, 200);
            this.pnlControls.TabIndex = 1;
            // 
            // txtMaSach
            // 
            this.txtMaSach.BorderRadius = 5;
            this.txtMaSach.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaSach.DefaultText = "";
            this.txtMaSach.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtMaSach.Location = new System.Drawing.Point(34, 21);
            this.txtMaSach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaSach.Name = "txtMaSach";
            this.txtMaSach.PlaceholderText = "Mã đầu sách (VD: DS001)";
            this.txtMaSach.SelectedText = "";
            this.txtMaSach.Size = new System.Drawing.Size(225, 42);
            this.txtMaSach.TabIndex = 0;
            // 
            // txtTenSach
            // 
            this.txtTenSach.BorderRadius = 5;
            this.txtTenSach.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenSach.DefaultText = "";
            this.txtTenSach.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTenSach.Location = new System.Drawing.Point(292, 21);
            this.txtTenSach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenSach.Name = "txtTenSach";
            this.txtTenSach.PlaceholderText = "Tên sách";
            this.txtTenSach.SelectedText = "";
            this.txtTenSach.Size = new System.Drawing.Size(338, 42);
            this.txtTenSach.TabIndex = 1;
            // 
            // cboTheLoai
            // 
            this.cboTheLoai.BackColor = System.Drawing.Color.Transparent;
            this.cboTheLoai.BorderRadius = 5;
            this.cboTheLoai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTheLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTheLoai.FocusedColor = System.Drawing.Color.Empty;
            this.cboTheLoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTheLoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboTheLoai.ItemHeight = 30;
            this.cboTheLoai.Location = new System.Drawing.Point(590, 22);
            this.cboTheLoai.Name = "cboTheLoai";
            this.cboTheLoai.Size = new System.Drawing.Size(250, 36);
            this.cboTheLoai.TabIndex = 2;
            // 
            // txtTacGia
            // 
            this.txtTacGia.BorderRadius = 5;
            this.txtTacGia.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTacGia.DefaultText = "";
            this.txtTacGia.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTacGia.Location = new System.Drawing.Point(34, 84);
            this.txtTacGia.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTacGia.Name = "txtTacGia";
            this.txtTacGia.PlaceholderText = "Tác giả";
            this.txtTacGia.SelectedText = "";
            this.txtTacGia.Size = new System.Drawing.Size(225, 42);
            this.txtTacGia.TabIndex = 3;
            // 
            // txtNhaXuatBan
            // 
            this.txtNhaXuatBan.BorderRadius = 5;
            this.txtNhaXuatBan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNhaXuatBan.DefaultText = "";
            this.txtNhaXuatBan.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtNhaXuatBan.Location = new System.Drawing.Point(292, 84);
            this.txtNhaXuatBan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNhaXuatBan.Name = "txtNhaXuatBan";
            this.txtNhaXuatBan.PlaceholderText = "Nhà xuất bản";
            this.txtNhaXuatBan.SelectedText = "";
            this.txtNhaXuatBan.Size = new System.Drawing.Size(338, 42);
            this.txtNhaXuatBan.TabIndex = 4;
            // 
            // txtNamXuatBan
            // 
            this.txtNamXuatBan.BorderRadius = 5;
            this.txtNamXuatBan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNamXuatBan.DefaultText = "";
            this.txtNamXuatBan.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtNamXuatBan.Location = new System.Drawing.Point(664, 84);
            this.txtNamXuatBan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNamXuatBan.Name = "txtNamXuatBan";
            this.txtNamXuatBan.PlaceholderText = "Năm XB (VD: 2023)";
            this.txtNamXuatBan.SelectedText = "";
            this.txtNamXuatBan.Size = new System.Drawing.Size(281, 42);
            this.txtNamXuatBan.TabIndex = 5;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 5;
            this.btnLamMoi.FillColor = System.Drawing.Color.Gray;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(440, 140);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 40);
            this.btnLamMoi.TabIndex = 6;
            this.btnLamMoi.Text = "LÀM MỚI";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 5;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(300, 140);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(110, 40);
            this.btnXoa.TabIndex = 7;
            this.btnXoa.Text = "XÓA";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BorderRadius = 5;
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(165, 140);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(110, 40);
            this.btnSua.TabIndex = 8;
            this.btnSua.Text = "SỬA";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 5;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(30, 140);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(110, 40);
            this.btnThem.TabIndex = 9;
            this.btnThem.Text = "THÊM";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvSach
            // 
            this.dgvSach.AllowUserToAddRows = false;
            this.dgvSach.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvSach.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSach.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSach.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSach.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSach.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSach.Location = new System.Drawing.Point(0, 200);
            this.dgvSach.Name = "dgvSach";
            this.dgvSach.ReadOnly = true;
            this.dgvSach.RowHeadersVisible = false;
            this.dgvSach.RowHeadersWidth = 51;
            this.dgvSach.RowTemplate.Height = 35;
            this.dgvSach.Size = new System.Drawing.Size(950, 440);
            this.dgvSach.TabIndex = 0;
            this.dgvSach.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvSach.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvSach.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvSach.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvSach.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvSach.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvSach.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSach.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvSach.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSach.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSach.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvSach.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSach.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvSach.ThemeStyle.ReadOnly = true;
            this.dgvSach.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvSach.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSach.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSach.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvSach.ThemeStyle.RowsStyle.Height = 35;
            this.dgvSach.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSach.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvSach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSach_CellClick);
            // 
            // FrmQuanLySach
            // 
            this.ClientSize = new System.Drawing.Size(950, 640);
            this.Controls.Add(this.dgvSach);
            this.Controls.Add(this.pnlControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmQuanLySach";
            this.Load += new System.EventHandler(this.FrmQuanLySach_Load);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private Guna.UI2.WinForms.Guna2Panel pnlControls;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSach;
        private Guna.UI2.WinForms.Guna2TextBox txtTacGia;
        private Guna.UI2.WinForms.Guna2TextBox txtTenSach;
        private Guna.UI2.WinForms.Guna2TextBox txtMaSach;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2ComboBox cboTheLoai;
        private Guna.UI2.WinForms.Guna2TextBox txtNamXuatBan;
        private Guna.UI2.WinForms.Guna2TextBox txtNhaXuatBan;
    }
}