namespace Lib_Equipment
{
    partial class FrmPhanQuyen
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
            this.dgvRole = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblDanhSachQuyen = new System.Windows.Forms.Label();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlCheckListBorder = new Guna.UI2.WinForms.Guna2Panel();
            this.clbMenu = new System.Windows.Forms.CheckedListBox();
            this.btnLuuQuyen = new Guna.UI2.WinForms.Guna2Button();
            this.lblChucNang = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRole)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.pnlCheckListBorder.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(414, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "PHÂN QUYỀN CHỨC NĂNG HỆ THỐNG";
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.dgvRole);
            this.pnlLeft.Controls.Add(this.lblDanhSachQuyen);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 60);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(20, 10, 10, 20);
            this.pnlLeft.Size = new System.Drawing.Size(550, 580);
            this.pnlLeft.TabIndex = 1;
            // 
            // lblDanhSachQuyen
            // 
            this.lblDanhSachQuyen.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDanhSachQuyen.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDanhSachQuyen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDanhSachQuyen.Location = new System.Drawing.Point(20, 10);
            this.lblDanhSachQuyen.Name = "lblDanhSachQuyen";
            this.lblDanhSachQuyen.Size = new System.Drawing.Size(520, 30);
            this.lblDanhSachQuyen.TabIndex = 0;
            this.lblDanhSachQuyen.Text = "1. CHỌN NHÓM QUYỀN (ROLE)";
            // 
            // dgvRole
            // 
            this.dgvRole.AllowUserToAddRows = false;
            this.dgvRole.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvRole.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvRole.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRole.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvRole.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRole.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvRole.Location = new System.Drawing.Point(20, 40);
            this.dgvRole.Name = "dgvRole";
            this.dgvRole.ReadOnly = true;
            this.dgvRole.RowHeadersVisible = false;
            this.dgvRole.RowTemplate.Height = 35;
            this.dgvRole.Size = new System.Drawing.Size(520, 520);
            this.dgvRole.TabIndex = 1;
            this.dgvRole.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvRole.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRole_CellClick);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.btnLuuQuyen);
            this.pnlRight.Controls.Add(this.pnlCheckListBorder);
            this.pnlRight.Controls.Add(this.lblChucNang);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(550, 60);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10, 10, 20, 20);
            this.pnlRight.Size = new System.Drawing.Size(400, 580);
            this.pnlRight.TabIndex = 2;
            // 
            // lblChucNang
            // 
            this.lblChucNang.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChucNang.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblChucNang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblChucNang.Location = new System.Drawing.Point(10, 10);
            this.lblChucNang.Name = "lblChucNang";
            this.lblChucNang.Size = new System.Drawing.Size(370, 30);
            this.lblChucNang.TabIndex = 1;
            this.lblChucNang.Text = "2. CHỌN CHỨC NĂNG ĐƯỢC PHÉP DÙNG";
            // 
            // pnlCheckListBorder
            // 
            this.pnlCheckListBorder.BorderColor = System.Drawing.Color.Silver;
            this.pnlCheckListBorder.BorderRadius = 5;
            this.pnlCheckListBorder.BorderThickness = 1;
            this.pnlCheckListBorder.Controls.Add(this.clbMenu);
            this.pnlCheckListBorder.Location = new System.Drawing.Point(10, 40);
            this.pnlCheckListBorder.Name = "pnlCheckListBorder";
            this.pnlCheckListBorder.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCheckListBorder.Size = new System.Drawing.Size(370, 450);
            this.pnlCheckListBorder.TabIndex = 2;
            // 
            // clbMenu
            // 
            this.clbMenu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbMenu.CheckOnClick = true;
            this.clbMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbMenu.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.clbMenu.FormattingEnabled = true;
            this.clbMenu.Location = new System.Drawing.Point(10, 10);
            this.clbMenu.Name = "clbMenu";
            this.clbMenu.Size = new System.Drawing.Size(350, 430);
            this.clbMenu.TabIndex = 0;
            // 
            // btnLuuQuyen
            // 
            this.btnLuuQuyen.BorderRadius = 5;
            this.btnLuuQuyen.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLuuQuyen.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLuuQuyen.ForeColor = System.Drawing.Color.White;
            this.btnLuuQuyen.Location = new System.Drawing.Point(10, 500);
            this.btnLuuQuyen.Name = "btnLuuQuyen";
            this.btnLuuQuyen.Size = new System.Drawing.Size(370, 50);
            this.btnLuuQuyen.TabIndex = 3;
            this.btnLuuQuyen.Text = "LƯU CẤP QUYỀN";
            this.btnLuuQuyen.Click += new System.EventHandler(this.btnLuuQuyen_Click);
            // 
            // FrmPhanQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(950, 640);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPhanQuyen";
            this.Text = "Phân Quyền Hệ Thống";
            this.Load += new System.EventHandler(this.FrmPhanQuyen_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRole)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlCheckListBorder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlLeft;
        private System.Windows.Forms.Label lblDanhSachQuyen;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRole;
        private Guna.UI2.WinForms.Guna2Panel pnlRight;
        private System.Windows.Forms.Label lblChucNang;
        private Guna.UI2.WinForms.Guna2Panel pnlCheckListBorder;
        private System.Windows.Forms.CheckedListBox clbMenu;
        private Guna.UI2.WinForms.Guna2Button btnLuuQuyen;
    }
}