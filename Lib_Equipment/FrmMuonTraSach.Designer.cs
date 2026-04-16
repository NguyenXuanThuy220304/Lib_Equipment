namespace Lib_Equipment
{
    partial class FrmMuonTraSach
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gbThaoTac = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblMaBanSao = new System.Windows.Forms.Label();
            this.txtMaBanSao = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenSachMuon = new System.Windows.Forms.Label();
            this.txtTenSachMuon = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblHanTra = new System.Windows.Forms.Label();
            this.dtpHanTra = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnChoMuon = new Guna.UI2.WinForms.Guna2Button();
            this.pnlSpacing = new System.Windows.Forms.Panel();
            this.gbDocGia = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblMaDG = new System.Windows.Forms.Label();
            this.txtMaDG = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenDG = new System.Windows.Forms.Label();
            this.txtTenDG = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.gbDanhSach = new Guna.UI2.WinForms.Guna2GroupBox();
            this.dgvDangMuon = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblTitleRight = new System.Windows.Forms.Label();
            this.txtTimKiemSachTra = new Guna.UI2.WinForms.Guna2TextBox(); // Ô QUÉT MÃ MỚI
            this.pnlLeft.SuspendLayout();
            this.gbThaoTac.SuspendLayout();
            this.gbDocGia.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.gbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbThaoTac);
            this.pnlLeft.Controls.Add(this.pnlSpacing);
            this.pnlLeft.Controls.Add(this.gbDocGia);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(4);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(33, 31, 20, 31);
            this.pnlLeft.Size = new System.Drawing.Size(573, 985);
            this.pnlLeft.TabIndex = 1;
            // 
            // gbThaoTac
            // 
            this.gbThaoTac.BorderColor = System.Drawing.Color.LightGray;
            this.gbThaoTac.BorderRadius = 10;
            this.gbThaoTac.Controls.Add(this.lblMaBanSao);
            this.gbThaoTac.Controls.Add(this.txtMaBanSao);
            this.gbThaoTac.Controls.Add(this.lblTenSachMuon);
            this.gbThaoTac.Controls.Add(this.txtTenSachMuon);
            this.gbThaoTac.Controls.Add(this.lblHanTra);
            this.gbThaoTac.Controls.Add(this.dtpHanTra);
            this.gbThaoTac.Controls.Add(this.btnChoMuon);
            this.gbThaoTac.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.gbThaoTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThaoTac.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.gbThaoTac.ForeColor = System.Drawing.Color.White;
            this.gbThaoTac.Location = new System.Drawing.Point(33, 365);
            this.gbThaoTac.Margin = new System.Windows.Forms.Padding(4);
            this.gbThaoTac.Name = "gbThaoTac";
            this.gbThaoTac.Size = new System.Drawing.Size(520, 589);
            this.gbThaoTac.TabIndex = 1;
            this.gbThaoTac.Text = "2. QUÉT SÁCH CHO MƯỢN";
            // 
            // lblMaBanSao
            // 
            this.lblMaBanSao.AutoSize = true;
            this.lblMaBanSao.BackColor = System.Drawing.Color.White;
            this.lblMaBanSao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMaBanSao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMaBanSao.Location = new System.Drawing.Point(27, 80);
            this.lblMaBanSao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaBanSao.Name = "lblMaBanSao";
            this.lblMaBanSao.Size = new System.Drawing.Size(245, 21);
            this.lblMaBanSao.TabIndex = 1;
            this.lblMaBanSao.Text = "Mã sách (Nhấn Enter kiểm tra):";
            // 
            // txtMaBanSao
            // 
            this.txtMaBanSao.BorderRadius = 6;
            this.txtMaBanSao.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaBanSao.DefaultText = "";
            this.txtMaBanSao.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtMaBanSao.ForeColor = System.Drawing.Color.Black;
            this.txtMaBanSao.Location = new System.Drawing.Point(27, 108);
            this.txtMaBanSao.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtMaBanSao.Name = "txtMaBanSao";
            this.txtMaBanSao.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtMaBanSao.PlaceholderText = "Quét mã vạch...";
            this.txtMaBanSao.SelectedText = "";
            this.txtMaBanSao.Size = new System.Drawing.Size(467, 52);
            this.txtMaBanSao.TabIndex = 0;
            this.txtMaBanSao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaBanSao_KeyDown);
            // 
            // lblTenSachMuon
            // 
            this.lblTenSachMuon.AutoSize = true;
            this.lblTenSachMuon.BackColor = System.Drawing.Color.White;
            this.lblTenSachMuon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTenSachMuon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTenSachMuon.Location = new System.Drawing.Point(27, 185);
            this.lblTenSachMuon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenSachMuon.Name = "lblTenSachMuon";
            this.lblTenSachMuon.Size = new System.Drawing.Size(124, 21);
            this.lblTenSachMuon.TabIndex = 6;
            this.lblTenSachMuon.Text = "Cuốn sẽ mượn:";
            // 
            // txtTenSachMuon
            // 
            this.txtTenSachMuon.BorderRadius = 6;
            this.txtTenSachMuon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenSachMuon.DefaultText = "";
            this.txtTenSachMuon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtTenSachMuon.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Italic);
            this.txtTenSachMuon.ForeColor = System.Drawing.Color.DimGray;
            this.txtTenSachMuon.Location = new System.Drawing.Point(27, 213);
            this.txtTenSachMuon.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtTenSachMuon.Name = "txtTenSachMuon";
            this.txtTenSachMuon.PlaceholderText = "Chưa xác định...";
            this.txtTenSachMuon.ReadOnly = true;
            this.txtTenSachMuon.SelectedText = "";
            this.txtTenSachMuon.Size = new System.Drawing.Size(467, 52);
            this.txtTenSachMuon.TabIndex = 5;
            // 
            // lblHanTra
            // 
            this.lblHanTra.AutoSize = true;
            this.lblHanTra.BackColor = System.Drawing.Color.White;
            this.lblHanTra.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblHanTra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHanTra.Location = new System.Drawing.Point(27, 289);
            this.lblHanTra.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHanTra.Name = "lblHanTra";
            this.lblHanTra.Size = new System.Drawing.Size(132, 21);
            this.lblHanTra.TabIndex = 3;
            this.lblHanTra.Text = "Hạn trả dự kiến:";
            // 
            // dtpHanTra
            // 
            this.dtpHanTra.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.dtpHanTra.BorderRadius = 6;
            this.dtpHanTra.BorderThickness = 1;
            this.dtpHanTra.Checked = true;
            this.dtpHanTra.FillColor = System.Drawing.Color.White;
            this.dtpHanTra.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dtpHanTra.ForeColor = System.Drawing.Color.Black;
            this.dtpHanTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHanTra.Location = new System.Drawing.Point(27, 318);
            this.dtpHanTra.Margin = new System.Windows.Forms.Padding(4);
            this.dtpHanTra.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpHanTra.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpHanTra.Name = "dtpHanTra";
            this.dtpHanTra.Size = new System.Drawing.Size(467, 52);
            this.dtpHanTra.TabIndex = 2;
            this.dtpHanTra.Value = new System.DateTime(2026, 4, 4, 13, 34, 41, 358);
            // 
            // btnChoMuon
            // 
            this.btnChoMuon.BorderRadius = 8;
            this.btnChoMuon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChoMuon.Enabled = false;
            this.btnChoMuon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnChoMuon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnChoMuon.ForeColor = System.Drawing.Color.White;
            this.btnChoMuon.Location = new System.Drawing.Point(27, 412);
            this.btnChoMuon.Margin = new System.Windows.Forms.Padding(4);
            this.btnChoMuon.Name = "btnChoMuon";
            this.btnChoMuon.Size = new System.Drawing.Size(467, 68);
            this.btnChoMuon.TabIndex = 4;
            this.btnChoMuon.Text = "XÁC NHẬN CHO MƯỢN";
            this.btnChoMuon.Click += new System.EventHandler(this.btnChoMuon_Click);
            // 
            // pnlSpacing
            // 
            this.pnlSpacing.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpacing.Location = new System.Drawing.Point(33, 340);
            this.pnlSpacing.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSpacing.Name = "pnlSpacing";
            this.pnlSpacing.Size = new System.Drawing.Size(520, 25);
            this.pnlSpacing.TabIndex = 2;
            // 
            // gbDocGia
            // 
            this.gbDocGia.BorderColor = System.Drawing.Color.LightGray;
            this.gbDocGia.BorderRadius = 10;
            this.gbDocGia.Controls.Add(this.lblMaDG);
            this.gbDocGia.Controls.Add(this.txtMaDG);
            this.gbDocGia.Controls.Add(this.lblTenDG);
            this.gbDocGia.Controls.Add(this.txtTenDG);
            this.gbDocGia.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.gbDocGia.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDocGia.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.gbDocGia.ForeColor = System.Drawing.Color.White;
            this.gbDocGia.Location = new System.Drawing.Point(33, 31);
            this.gbDocGia.Margin = new System.Windows.Forms.Padding(4);
            this.gbDocGia.Name = "gbDocGia";
            this.gbDocGia.Size = new System.Drawing.Size(520, 309);
            this.gbDocGia.TabIndex = 0;
            this.gbDocGia.Text = "1. THÔNG TIN ĐỘC GIẢ";
            // 
            // lblMaDG
            // 
            this.lblMaDG.AutoSize = true;
            this.lblMaDG.BackColor = System.Drawing.Color.White;
            this.lblMaDG.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMaDG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMaDG.Location = new System.Drawing.Point(27, 80);
            this.lblMaDG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaDG.Name = "lblMaDG";
            this.lblMaDG.Size = new System.Drawing.Size(255, 21);
            this.lblMaDG.TabIndex = 1;
            this.lblMaDG.Text = "Mã Độc giả (Nhấn Enter để tìm):";
            // 
            // txtMaDG
            // 
            this.txtMaDG.BorderRadius = 6;
            this.txtMaDG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaDG.DefaultText = "";
            this.txtMaDG.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtMaDG.ForeColor = System.Drawing.Color.Black;
            this.txtMaDG.Location = new System.Drawing.Point(27, 108);
            this.txtMaDG.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtMaDG.Name = "txtMaDG";
            this.txtMaDG.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtMaDG.PlaceholderText = "Nhập mã SV/GV...";
            this.txtMaDG.SelectedText = "";
            this.txtMaDG.Size = new System.Drawing.Size(467, 52);
            this.txtMaDG.TabIndex = 0;
            this.txtMaDG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaDG_KeyDown);
            // 
            // lblTenDG
            // 
            this.lblTenDG.AutoSize = true;
            this.lblTenDG.BackColor = System.Drawing.Color.White;
            this.lblTenDG.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTenDG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTenDG.Location = new System.Drawing.Point(27, 185);
            this.lblTenDG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenDG.Name = "lblTenDG";
            this.lblTenDG.Size = new System.Drawing.Size(87, 21);
            this.lblTenDG.TabIndex = 3;
            this.lblTenDG.Text = "Họ và tên:";
            // 
            // txtTenDG
            // 
            this.txtTenDG.BorderRadius = 6;
            this.txtTenDG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDG.DefaultText = "";
            this.txtTenDG.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtTenDG.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtTenDG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.txtTenDG.Location = new System.Drawing.Point(27, 213);
            this.txtTenDG.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtTenDG.Name = "txtTenDG";
            this.txtTenDG.PlaceholderText = "";
            this.txtTenDG.ReadOnly = true;
            this.txtTenDG.SelectedText = "";
            this.txtTenDG.Size = new System.Drawing.Size(467, 52);
            this.txtTenDG.TabIndex = 2;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.gbDanhSach);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(573, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(4);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(13, 31, 33, 31);
            this.pnlRight.Size = new System.Drawing.Size(1116, 985);
            this.pnlRight.TabIndex = 2;
            // 
            // gbDanhSach
            // 
            this.gbDanhSach.BorderColor = System.Drawing.Color.LightGray;
            this.gbDanhSach.BorderRadius = 10;
            this.gbDanhSach.Controls.Add(this.txtTimKiemSachTra); // ADD TEXTBOX TÌM KIẾM
            this.gbDanhSach.Controls.Add(this.dgvDangMuon);
            this.gbDanhSach.Controls.Add(this.lblTitleRight);
            this.gbDanhSach.CustomBorderColor = System.Drawing.Color.White;
            this.gbDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDanhSach.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbDanhSach.Location = new System.Drawing.Point(13, 31);
            this.gbDanhSach.Margin = new System.Windows.Forms.Padding(4);
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.Padding = new System.Windows.Forms.Padding(20, 74, 20, 18);
            this.gbDanhSach.Size = new System.Drawing.Size(1070, 923);
            this.gbDanhSach.TabIndex = 0;
            // 
            // txtTimKiemSachTra (Ô SCAN MÃ VẠCH)
            // 
            this.txtTimKiemSachTra.BorderRadius = 18;
            this.txtTimKiemSachTra.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiemSachTra.DefaultText = "";
            this.txtTimKiemSachTra.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtTimKiemSachTra.ForeColor = System.Drawing.Color.Black;
            this.txtTimKiemSachTra.Location = new System.Drawing.Point(680, 12);
            this.txtTimKiemSachTra.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTimKiemSachTra.Name = "txtTimKiemSachTra";
            this.txtTimKiemSachTra.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtTimKiemSachTra.PlaceholderText = "Quét mã vạch sách để trả nhanh...";
            this.txtTimKiemSachTra.SelectedText = "";
            this.txtTimKiemSachTra.Size = new System.Drawing.Size(350, 42);
            this.txtTimKiemSachTra.TabIndex = 2;
            this.txtTimKiemSachTra.TextChanged += new System.EventHandler(this.txtTimKiemSachTra_TextChanged);
            // 
            // dgvDangMuon
            // 
            this.dgvDangMuon.AllowUserToAddRows = false;
            this.dgvDangMuon.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvDangMuon.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDangMuon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDangMuon.ColumnHeadersHeight = 45;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDangMuon.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDangMuon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDangMuon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDangMuon.Location = new System.Drawing.Point(20, 114);
            this.dgvDangMuon.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDangMuon.Name = "dgvDangMuon";
            this.dgvDangMuon.ReadOnly = true;
            this.dgvDangMuon.RowHeadersVisible = false;
            this.dgvDangMuon.RowHeadersWidth = 51;
            this.dgvDangMuon.RowTemplate.Height = 40;
            this.dgvDangMuon.Size = new System.Drawing.Size(1030, 791);
            this.dgvDangMuon.TabIndex = 0;
            this.dgvDangMuon.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDangMuon.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvDangMuon.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvDangMuon.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvDangMuon.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvDangMuon.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvDangMuon.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDangMuon.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvDangMuon.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDangMuon.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDangMuon.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDangMuon.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDangMuon.ThemeStyle.HeaderStyle.Height = 45;
            this.dgvDangMuon.ThemeStyle.ReadOnly = true;
            this.dgvDangMuon.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDangMuon.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDangMuon.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDangMuon.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDangMuon.ThemeStyle.RowsStyle.Height = 40;
            this.dgvDangMuon.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDangMuon.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDangMuon.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDangMuon_CellContentClick);
            this.dgvDangMuon.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDangMuon_CellFormatting);
            // 
            // lblTitleRight
            // 
            this.lblTitleRight.AutoSize = true;
            this.lblTitleRight.BackColor = System.Drawing.Color.White;
            this.lblTitleRight.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitleRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.lblTitleRight.Location = new System.Drawing.Point(20, 18);
            this.lblTitleRight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleRight.Name = "lblTitleRight";
            this.lblTitleRight.Size = new System.Drawing.Size(399, 32);
            this.lblTitleRight.TabIndex = 1;
            this.lblTitleRight.Text = "DANH SÁCH PHIẾU ĐANG MƯỢN";
            // 
            // FrmMuonTraSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1689, 985);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMuonTraSach";
            this.Text = "Quản lý Mượn / Trả";
            this.Load += new System.EventHandler(this.FrmMuonTraSach_Load);
            this.pnlLeft.ResumeLayout(false);
            this.gbThaoTac.ResumeLayout(false);
            this.gbThaoTac.PerformLayout();
            this.gbDocGia.ResumeLayout(false);
            this.gbDocGia.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.gbDanhSach.ResumeLayout(false);
            this.gbDanhSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel pnlLeft;
        private Guna.UI2.WinForms.Guna2GroupBox gbDocGia;
        private System.Windows.Forms.Label lblMaDG;
        private Guna.UI2.WinForms.Guna2TextBox txtMaDG;
        private System.Windows.Forms.Label lblTenDG;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDG;
        private System.Windows.Forms.Panel pnlSpacing;
        private Guna.UI2.WinForms.Guna2GroupBox gbThaoTac;
        private System.Windows.Forms.Label lblMaBanSao;
        private Guna.UI2.WinForms.Guna2TextBox txtMaBanSao;
        private System.Windows.Forms.Label lblTenSachMuon;
        private Guna.UI2.WinForms.Guna2TextBox txtTenSachMuon;
        private System.Windows.Forms.Label lblHanTra;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpHanTra;
        private Guna.UI2.WinForms.Guna2Button btnChoMuon;
        private Guna.UI2.WinForms.Guna2Panel pnlRight;
        private Guna.UI2.WinForms.Guna2GroupBox gbDanhSach;
        private System.Windows.Forms.Label lblTitleRight;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDangMuon;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiemSachTra;
    }
}