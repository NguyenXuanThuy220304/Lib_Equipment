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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gbThaoTac = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblMaBanSao = new System.Windows.Forms.Label();
            this.txtMaBanSao = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenSachMuon = new System.Windows.Forms.Label();
            this.txtTenSachMuon = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblHanTra = new System.Windows.Forms.Label();
            this.dtpHanTra = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnChoMuon = new Guna.UI2.WinForms.Guna2Button();
            this.gbDocGia = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblMaDG = new System.Windows.Forms.Label();
            this.txtMaDG = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenDG = new System.Windows.Forms.Label();
            this.txtTenDG = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvDangMuon = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlLeft.SuspendLayout();
            this.gbThaoTac.SuspendLayout();
            this.gbDocGia.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbThaoTac);
            this.pnlLeft.Controls.Add(this.gbDocGia);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.pnlLeft.Size = new System.Drawing.Size(467, 788);
            this.pnlLeft.TabIndex = 1;
            // 
            // gbThaoTac
            // 
            this.gbThaoTac.BorderRadius = 5;
            this.gbThaoTac.Controls.Add(this.lblMaBanSao);
            this.gbThaoTac.Controls.Add(this.txtMaBanSao);
            this.gbThaoTac.Controls.Add(this.lblTenSachMuon);
            this.gbThaoTac.Controls.Add(this.txtTenSachMuon);
            this.gbThaoTac.Controls.Add(this.lblHanTra);
            this.gbThaoTac.Controls.Add(this.dtpHanTra);
            this.gbThaoTac.Controls.Add(this.btnChoMuon);
            this.gbThaoTac.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.gbThaoTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThaoTac.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbThaoTac.ForeColor = System.Drawing.Color.White;
            this.gbThaoTac.Location = new System.Drawing.Point(27, 271);
            this.gbThaoTac.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbThaoTac.Name = "gbThaoTac";
            this.gbThaoTac.Size = new System.Drawing.Size(413, 492);
            this.gbThaoTac.TabIndex = 1;
            this.gbThaoTac.Text = "2. QUÉT SÁCH CHO MƯỢN";
            // 
            // lblMaBanSao
            // 
            this.lblMaBanSao.AutoSize = true;
            this.lblMaBanSao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaBanSao.ForeColor = System.Drawing.Color.Black;
            this.lblMaBanSao.Location = new System.Drawing.Point(20, 55);
            this.lblMaBanSao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaBanSao.Name = "lblMaBanSao";
            this.lblMaBanSao.Size = new System.Drawing.Size(227, 20);
            this.lblMaBanSao.TabIndex = 1;
            this.lblMaBanSao.Text = "Mã sách (Nhấn Enter kiểm tra):";
            // 
            // txtMaBanSao
            // 
            this.txtMaBanSao.BorderRadius = 5;
            this.txtMaBanSao.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaBanSao.DefaultText = "";
            this.txtMaBanSao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaBanSao.ForeColor = System.Drawing.Color.Black;
            this.txtMaBanSao.Location = new System.Drawing.Point(20, 78);
            this.txtMaBanSao.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtMaBanSao.Name = "txtMaBanSao";
            this.txtMaBanSao.PlaceholderText = "";
            this.txtMaBanSao.SelectedText = "";
            this.txtMaBanSao.Size = new System.Drawing.Size(373, 44);
            this.txtMaBanSao.TabIndex = 0;
            this.txtMaBanSao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaBanSao_KeyDown);
            // 
            // lblTenSachMuon
            // 
            this.lblTenSachMuon.AutoSize = true;
            this.lblTenSachMuon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenSachMuon.ForeColor = System.Drawing.Color.Black;
            this.lblTenSachMuon.Location = new System.Drawing.Point(20, 135);
            this.lblTenSachMuon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenSachMuon.Name = "lblTenSachMuon";
            this.lblTenSachMuon.Size = new System.Drawing.Size(114, 20);
            this.lblTenSachMuon.TabIndex = 6;
            this.lblTenSachMuon.Text = "Cuốn sẽ mượn:";
            // 
            // txtTenSachMuon
            // 
            this.txtTenSachMuon.BorderRadius = 5;
            this.txtTenSachMuon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenSachMuon.DefaultText = "Chưa xác định...";
            this.txtTenSachMuon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.txtTenSachMuon.ForeColor = System.Drawing.Color.DimGray;
            this.txtTenSachMuon.Location = new System.Drawing.Point(20, 158);
            this.txtTenSachMuon.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtTenSachMuon.Name = "txtTenSachMuon";
            this.txtTenSachMuon.PlaceholderText = "";
            this.txtTenSachMuon.ReadOnly = true;
            this.txtTenSachMuon.SelectedText = "";
            this.txtTenSachMuon.Size = new System.Drawing.Size(373, 44);
            this.txtTenSachMuon.TabIndex = 5;
            // 
            // lblHanTra
            // 
            this.lblHanTra.AutoSize = true;
            this.lblHanTra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHanTra.ForeColor = System.Drawing.Color.Black;
            this.lblHanTra.Location = new System.Drawing.Point(20, 215);
            this.lblHanTra.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHanTra.Name = "lblHanTra";
            this.lblHanTra.Size = new System.Drawing.Size(121, 20);
            this.lblHanTra.TabIndex = 3;
            this.lblHanTra.Text = "Hạn trả dự kiến:";
            // 
            // dtpHanTra
            // 
            this.dtpHanTra.BorderRadius = 5;
            this.dtpHanTra.Checked = true;
            this.dtpHanTra.FillColor = System.Drawing.Color.White;
            this.dtpHanTra.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpHanTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHanTra.Location = new System.Drawing.Point(20, 238);
            this.dtpHanTra.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpHanTra.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpHanTra.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpHanTra.Name = "dtpHanTra";
            this.dtpHanTra.Size = new System.Drawing.Size(373, 44);
            this.dtpHanTra.TabIndex = 2;
            this.dtpHanTra.Value = new System.DateTime(2026, 3, 14, 13, 50, 46, 112);
            // 
            // btnChoMuon
            // 
            this.btnChoMuon.BorderRadius = 5;
            this.btnChoMuon.Enabled = false;
            this.btnChoMuon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnChoMuon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnChoMuon.ForeColor = System.Drawing.Color.White;
            this.btnChoMuon.Location = new System.Drawing.Point(20, 308);
            this.btnChoMuon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChoMuon.Name = "btnChoMuon";
            this.btnChoMuon.Size = new System.Drawing.Size(373, 62);
            this.btnChoMuon.TabIndex = 4;
            this.btnChoMuon.Text = "XÁC NHẬN CHO MƯỢN";
            this.btnChoMuon.Click += new System.EventHandler(this.btnChoMuon_Click);
            // 
            // gbDocGia
            // 
            this.gbDocGia.BorderRadius = 5;
            this.gbDocGia.Controls.Add(this.lblMaDG);
            this.gbDocGia.Controls.Add(this.txtMaDG);
            this.gbDocGia.Controls.Add(this.lblTenDG);
            this.gbDocGia.Controls.Add(this.txtTenDG);
            this.gbDocGia.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.gbDocGia.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDocGia.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbDocGia.ForeColor = System.Drawing.Color.White;
            this.gbDocGia.Location = new System.Drawing.Point(27, 25);
            this.gbDocGia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbDocGia.Name = "gbDocGia";
            this.gbDocGia.Size = new System.Drawing.Size(413, 246);
            this.gbDocGia.TabIndex = 0;
            this.gbDocGia.Text = "1. THÔNG TIN ĐỘC GIẢ";
            // 
            // lblMaDG
            // 
            this.lblMaDG.AutoSize = true;
            this.lblMaDG.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaDG.ForeColor = System.Drawing.Color.Black;
            this.lblMaDG.Location = new System.Drawing.Point(20, 62);
            this.lblMaDG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaDG.Name = "lblMaDG";
            this.lblMaDG.Size = new System.Drawing.Size(235, 20);
            this.lblMaDG.TabIndex = 1;
            this.lblMaDG.Text = "Mã Độc giả (Nhấn Enter để tìm):";
            // 
            // txtMaDG
            // 
            this.txtMaDG.BorderRadius = 5;
            this.txtMaDG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaDG.DefaultText = "";
            this.txtMaDG.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaDG.ForeColor = System.Drawing.Color.Black;
            this.txtMaDG.Location = new System.Drawing.Point(20, 84);
            this.txtMaDG.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtMaDG.Name = "txtMaDG";
            this.txtMaDG.PlaceholderText = "";
            this.txtMaDG.SelectedText = "";
            this.txtMaDG.Size = new System.Drawing.Size(373, 44);
            this.txtMaDG.TabIndex = 0;
            this.txtMaDG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaDG_KeyDown);
            // 
            // lblTenDG
            // 
            this.lblTenDG.AutoSize = true;
            this.lblTenDG.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenDG.ForeColor = System.Drawing.Color.Black;
            this.lblTenDG.Location = new System.Drawing.Point(20, 148);
            this.lblTenDG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenDG.Name = "lblTenDG";
            this.lblTenDG.Size = new System.Drawing.Size(80, 20);
            this.lblTenDG.TabIndex = 3;
            this.lblTenDG.Text = "Họ và tên:";
            // 
            // txtTenDG
            // 
            this.txtTenDG.BorderRadius = 5;
            this.txtTenDG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDG.DefaultText = "";
            this.txtTenDG.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTenDG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.txtTenDG.Location = new System.Drawing.Point(20, 170);
            this.txtTenDG.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtTenDG.Name = "txtTenDG";
            this.txtTenDG.PlaceholderText = "";
            this.txtTenDG.ReadOnly = true;
            this.txtTenDG.SelectedText = "";
            this.txtTenDG.Size = new System.Drawing.Size(373, 44);
            this.txtTenDG.TabIndex = 2;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.dgvDangMuon);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(467, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(13, 25, 27, 25);
            this.pnlRight.Size = new System.Drawing.Size(800, 788);
            this.pnlRight.TabIndex = 2;
            // 
            // dgvDangMuon
            // 
            this.dgvDangMuon.AllowUserToAddRows = false;
            this.dgvDangMuon.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDangMuon.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.dgvDangMuon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDangMuon.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDangMuon.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDangMuon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDangMuon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDangMuon.Location = new System.Drawing.Point(13, 25);
            this.dgvDangMuon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDangMuon.Name = "dgvDangMuon";
            this.dgvDangMuon.ReadOnly = true;
            this.dgvDangMuon.RowHeadersVisible = false;
            this.dgvDangMuon.RowHeadersWidth = 51;
            this.dgvDangMuon.RowTemplate.Height = 40;
            this.dgvDangMuon.Size = new System.Drawing.Size(760, 738);
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
            this.dgvDangMuon.ThemeStyle.HeaderStyle.Height = 40;
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
            // FrmMuonTraSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1267, 788);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmMuonTraSach";
            this.Text = "Quản lý Mượn / Trả";
            this.Load += new System.EventHandler(this.FrmMuonTraSach_Load);
            this.pnlLeft.ResumeLayout(false);
            this.gbThaoTac.ResumeLayout(false);
            this.gbThaoTac.PerformLayout();
            this.gbDocGia.ResumeLayout(false);
            this.gbDocGia.PerformLayout();
            this.pnlRight.ResumeLayout(false);
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
        private Guna.UI2.WinForms.Guna2GroupBox gbThaoTac;
        private System.Windows.Forms.Label lblMaBanSao;
        private Guna.UI2.WinForms.Guna2TextBox txtMaBanSao;
        private System.Windows.Forms.Label lblTenSachMuon;
        private Guna.UI2.WinForms.Guna2TextBox txtTenSachMuon;
        private System.Windows.Forms.Label lblHanTra;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpHanTra;
        private Guna.UI2.WinForms.Guna2Button btnChoMuon;
        private Guna.UI2.WinForms.Guna2Panel pnlRight;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDangMuon;
    }
}