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
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvRole = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblDanhSachQuyen = new System.Windows.Forms.Label();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLuuQuyen = new Guna.UI2.WinForms.Guna2Button();
            this.pnlCheckListBorder = new Guna.UI2.WinForms.Guna2Panel();
            this.clbMenu = new System.Windows.Forms.CheckedListBox();
            this.lblChucNang = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRole)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.pnlCheckListBorder.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.dgvRole);
            this.pnlLeft.Controls.Add(this.lblDanhSachQuyen);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(27, 12, 13, 25);
            this.pnlLeft.Size = new System.Drawing.Size(733, 788);
            this.pnlLeft.TabIndex = 1;
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
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRole.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRole.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvRole.Location = new System.Drawing.Point(27, 49);
            this.dgvRole.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvRole.Name = "dgvRole";
            this.dgvRole.ReadOnly = true;
            this.dgvRole.RowHeadersVisible = false;
            this.dgvRole.RowHeadersWidth = 51;
            this.dgvRole.RowTemplate.Height = 35;
            this.dgvRole.Size = new System.Drawing.Size(693, 714);
            this.dgvRole.TabIndex = 1;
            this.dgvRole.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvRole.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvRole.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvRole.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvRole.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvRole.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvRole.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvRole.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.dgvRole.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvRole.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvRole.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvRole.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRole.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvRole.ThemeStyle.ReadOnly = true;
            this.dgvRole.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvRole.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRole.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvRole.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvRole.ThemeStyle.RowsStyle.Height = 35;
            this.dgvRole.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvRole.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvRole.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRole_CellClick);
            // 
            // lblDanhSachQuyen
            // 
            this.lblDanhSachQuyen.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDanhSachQuyen.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDanhSachQuyen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDanhSachQuyen.Location = new System.Drawing.Point(27, 12);
            this.lblDanhSachQuyen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDanhSachQuyen.Name = "lblDanhSachQuyen";
            this.lblDanhSachQuyen.Size = new System.Drawing.Size(693, 37);
            this.lblDanhSachQuyen.TabIndex = 0;
            this.lblDanhSachQuyen.Text = "1. CHỌN NHÓM QUYỀN (ROLE)";
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.btnLuuQuyen);
            this.pnlRight.Controls.Add(this.pnlCheckListBorder);
            this.pnlRight.Controls.Add(this.lblChucNang);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(733, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(13, 12, 27, 25);
            this.pnlRight.Size = new System.Drawing.Size(534, 788);
            this.pnlRight.TabIndex = 2;
            // 
            // btnLuuQuyen
            // 
            this.btnLuuQuyen.BorderRadius = 5;
            this.btnLuuQuyen.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLuuQuyen.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLuuQuyen.ForeColor = System.Drawing.Color.White;
            this.btnLuuQuyen.Location = new System.Drawing.Point(13, 615);
            this.btnLuuQuyen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLuuQuyen.Name = "btnLuuQuyen";
            this.btnLuuQuyen.Size = new System.Drawing.Size(493, 62);
            this.btnLuuQuyen.TabIndex = 3;
            this.btnLuuQuyen.Text = "LƯU CẤP QUYỀN";
            this.btnLuuQuyen.Click += new System.EventHandler(this.btnLuuQuyen_Click);
            // 
            // pnlCheckListBorder
            // 
            this.pnlCheckListBorder.BorderColor = System.Drawing.Color.Silver;
            this.pnlCheckListBorder.BorderRadius = 5;
            this.pnlCheckListBorder.BorderThickness = 1;
            this.pnlCheckListBorder.Controls.Add(this.clbMenu);
            this.pnlCheckListBorder.Location = new System.Drawing.Point(13, 49);
            this.pnlCheckListBorder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlCheckListBorder.Name = "pnlCheckListBorder";
            this.pnlCheckListBorder.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlCheckListBorder.Size = new System.Drawing.Size(493, 554);
            this.pnlCheckListBorder.TabIndex = 2;
            // 
            // clbMenu
            // 
            this.clbMenu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbMenu.CheckOnClick = true;
            this.clbMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbMenu.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.clbMenu.FormattingEnabled = true;
            this.clbMenu.Location = new System.Drawing.Point(13, 12);
            this.clbMenu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clbMenu.Name = "clbMenu";
            this.clbMenu.Size = new System.Drawing.Size(467, 530);
            this.clbMenu.TabIndex = 0;
            // 
            // lblChucNang
            // 
            this.lblChucNang.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChucNang.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblChucNang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblChucNang.Location = new System.Drawing.Point(13, 12);
            this.lblChucNang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChucNang.Name = "lblChucNang";
            this.lblChucNang.Size = new System.Drawing.Size(494, 37);
            this.lblChucNang.TabIndex = 1;
            this.lblChucNang.Text = "2. CHỌN CHỨC NĂNG ĐƯỢC PHÉP DÙNG";
            // 
            // FrmPhanQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1267, 788);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmPhanQuyen";
            this.Text = "Phân Quyền Hệ Thống";
            this.Load += new System.EventHandler(this.FrmPhanQuyen_Load);
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRole)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlCheckListBorder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
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