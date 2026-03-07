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
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
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
            this.pnlTop.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).BeginInit();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Size = new System.Drawing.Size(950, 60);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Text = "QUẢN LÝ DANH MỤC ĐẦU SÁCH";

            // pnlControls
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
            this.pnlControls.Location = new System.Drawing.Point(0, 60);
            this.pnlControls.Size = new System.Drawing.Size(950, 200);

            // Textboxes (Vị trí tọa độ như cũ của bạn)
            this.txtMaSach.Location = new System.Drawing.Point(30, 20);
            this.txtMaSach.Size = new System.Drawing.Size(200, 40);
            this.txtMaSach.PlaceholderText = "Mã đầu sách (VD: DS001)";
            this.txtMaSach.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtMaSach.BorderRadius = 5;

            this.txtTenSach.Location = new System.Drawing.Point(260, 20);
            this.txtTenSach.Size = new System.Drawing.Size(300, 40);
            this.txtTenSach.PlaceholderText = "Tên sách";
            this.txtTenSach.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTenSach.BorderRadius = 5;

            this.cboTheLoai.Location = new System.Drawing.Point(590, 22);
            this.cboTheLoai.Size = new System.Drawing.Size(250, 36);
            this.cboTheLoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTheLoai.BorderRadius = 5;

            this.txtTacGia.Location = new System.Drawing.Point(30, 80);
            this.txtTacGia.Size = new System.Drawing.Size(200, 40);
            this.txtTacGia.PlaceholderText = "Tác giả";
            this.txtTacGia.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTacGia.BorderRadius = 5;

            this.txtNhaXuatBan.Location = new System.Drawing.Point(260, 80);
            this.txtNhaXuatBan.Size = new System.Drawing.Size(300, 40);
            this.txtNhaXuatBan.PlaceholderText = "Nhà xuất bản";
            this.txtNhaXuatBan.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtNhaXuatBan.BorderRadius = 5;

            this.txtNamXuatBan.Location = new System.Drawing.Point(590, 80);
            this.txtNamXuatBan.Size = new System.Drawing.Size(250, 40);
            this.txtNamXuatBan.PlaceholderText = "Năm XB (VD: 2023)";
            this.txtNamXuatBan.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtNamXuatBan.BorderRadius = 5;

            // Buttons
            this.btnThem.Location = new System.Drawing.Point(30, 140);
            this.btnThem.Size = new System.Drawing.Size(110, 40);
            this.btnThem.Text = "THÊM";
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.BorderRadius = 5;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.Location = new System.Drawing.Point(165, 140);
            this.btnSua.Size = new System.Drawing.Size(110, 40);
            this.btnSua.Text = "SỬA";
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.BorderRadius = 5;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Location = new System.Drawing.Point(300, 140);
            this.btnXoa.Size = new System.Drawing.Size(110, 40);
            this.btnXoa.Text = "XÓA";
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.BorderRadius = 5;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.Location = new System.Drawing.Point(440, 140);
            this.btnLamMoi.Size = new System.Drawing.Size(110, 40);
            this.btnLamMoi.Text = "LÀM MỚI";
            this.btnLamMoi.FillColor = System.Drawing.Color.Gray;
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.BorderRadius = 5;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // dgvSach
            this.dgvSach.AllowUserToAddRows = false;
            this.dgvSach.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvSach.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(26, 75, 132);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            this.dgvSach.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSach.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            this.dgvSach.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSach.Location = new System.Drawing.Point(0, 260);
            this.dgvSach.ReadOnly = true;
            this.dgvSach.RowHeadersVisible = false;
            this.dgvSach.RowTemplate.Height = 35;
            this.dgvSach.Size = new System.Drawing.Size(950, 380);
            this.dgvSach.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.dgvSach.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
            this.dgvSach.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            this.dgvSach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSach_CellClick);

            // FrmQuanLySach
            this.ClientSize = new System.Drawing.Size(950, 640);
            this.Controls.Add(this.dgvSach);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Load += new System.EventHandler(this.FrmQuanLySach_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
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