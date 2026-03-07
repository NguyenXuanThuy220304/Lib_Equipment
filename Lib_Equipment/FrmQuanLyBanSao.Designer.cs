namespace Lib_Equipment
{
    partial class FrmQuanLyBanSao
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlControls = new Guna.UI2.WinForms.Guna2Panel();
            this.txtMaBanSao = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaSach = new Guna.UI2.WinForms.Guna2TextBox();
            this.cboViTri = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.dgvBanSao = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBanSao)).BeginInit();
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
            this.lblTitle.Text = "QUẢN LÝ BẢN SAO SÁCH (NHẬP KHO)";

            // pnlControls
            this.pnlControls.BackColor = System.Drawing.Color.White;
            this.pnlControls.Controls.Add(this.txtMaBanSao);
            this.pnlControls.Controls.Add(this.txtMaSach);
            this.pnlControls.Controls.Add(this.cboViTri);
            this.pnlControls.Controls.Add(this.cboTrangThai);
            this.pnlControls.Controls.Add(this.btnLamMoi);
            this.pnlControls.Controls.Add(this.btnXoa);
            this.pnlControls.Controls.Add(this.btnSua);
            this.pnlControls.Controls.Add(this.btnThem);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 60);
            this.pnlControls.Size = new System.Drawing.Size(950, 150);

            // txtMaBanSao
            this.txtMaBanSao.BorderRadius = 5;
            this.txtMaBanSao.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtMaBanSao.Location = new System.Drawing.Point(30, 20);
            this.txtMaBanSao.PlaceholderText = "Mã cuốn vật lý (VD: DS001-B01)";
            this.txtMaBanSao.Size = new System.Drawing.Size(250, 40);

            // txtMaSach
            this.txtMaSach.BorderRadius = 5;
            this.txtMaSach.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtMaSach.Location = new System.Drawing.Point(300, 20);
            this.txtMaSach.PlaceholderText = "Mã Đầu sách (VD: DS001)";
            this.txtMaSach.Size = new System.Drawing.Size(200, 40);

            // cboViTri
            this.cboViTri.BorderRadius = 5;
            this.cboViTri.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboViTri.Location = new System.Drawing.Point(520, 22);
            this.cboViTri.Size = new System.Drawing.Size(200, 36);

            // cboTrangThai
            this.cboTrangThai.BorderRadius = 5;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTrangThai.Location = new System.Drawing.Point(740, 22);
            this.cboTrangThai.Size = new System.Drawing.Size(180, 36);

            // Nút bấm
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(30, 85);
            this.btnThem.Size = new System.Drawing.Size(110, 40);
            this.btnThem.Text = "THÊM KHO";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.FillColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(155, 85);
            this.btnSua.Size = new System.Drawing.Size(110, 40);
            this.btnSua.Text = "CHUYỂN KHO";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(280, 85);
            this.btnXoa.Size = new System.Drawing.Size(110, 40);
            this.btnXoa.Text = "XÓA";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.FillColor = System.Drawing.Color.Gray;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(405, 85);
            this.btnLamMoi.Size = new System.Drawing.Size(110, 40);
            this.btnLamMoi.Text = "LÀM MỚI";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // dgvBanSao
            this.dgvBanSao.AllowUserToAddRows = false;
            this.dgvBanSao.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvBanSao.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            this.dgvBanSao.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBanSao.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvBanSao.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBanSao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBanSao.Location = new System.Drawing.Point(0, 210);
            this.dgvBanSao.Name = "dgvBanSao";
            this.dgvBanSao.ReadOnly = true;
            this.dgvBanSao.RowHeadersVisible = false;
            this.dgvBanSao.RowTemplate.Height = 35;
            this.dgvBanSao.Size = new System.Drawing.Size(950, 430);
            this.dgvBanSao.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvBanSao.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvBanSao.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvBanSao.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBanSao_CellClick);

            // Form
            this.ClientSize = new System.Drawing.Size(950, 640);
            this.Controls.Add(this.dgvBanSao);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmQuanLyBanSao";
            this.Load += new System.EventHandler(this.FrmQuanLyBanSao_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBanSao)).EndInit();
            this.ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlControls;
        private Guna.UI2.WinForms.Guna2TextBox txtMaBanSao;
        private Guna.UI2.WinForms.Guna2TextBox txtMaSach;
        private Guna.UI2.WinForms.Guna2ComboBox cboViTri;
        private Guna.UI2.WinForms.Guna2ComboBox cboTrangThai;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private Guna.UI2.WinForms.Guna2DataGridView dgvBanSao;
    }
}