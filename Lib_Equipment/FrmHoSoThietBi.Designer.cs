namespace Lib_Equipment
{
    partial class FrmHoSoThietBi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTenThietBi = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControlHoSo = new System.Windows.Forms.TabControl();
            this.tabLuanChuyen = new System.Windows.Forms.TabPage();
            this.dgvLuanChuyen = new System.Windows.Forms.DataGridView();
            this.tabBaoTri = new System.Windows.Forms.TabPage();
            this.dgvBaoTri = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.tabControlHoSo.SuspendLayout();
            this.tabLuanChuyen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuanChuyen)).BeginInit();
            this.tabBaoTri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoTri)).BeginInit();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.pnlTop.Controls.Add(this.lblTenThietBi);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(800, 80);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.LightGray;
            this.lblTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "HỒ SƠ LÝ LỊCH THIẾT BỊ";

            // lblTenThietBi
            this.lblTenThietBi.AutoSize = true;
            this.lblTenThietBi.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTenThietBi.ForeColor = System.Drawing.Color.White;
            this.lblTenThietBi.Location = new System.Drawing.Point(15, 40);
            this.lblTenThietBi.Name = "lblTenThietBi";
            this.lblTenThietBi.Text = "[Mã] - [Tên Thiết Bị]";

            // tabControlHoSo
            this.tabControlHoSo.Controls.Add(this.tabLuanChuyen);
            this.tabControlHoSo.Controls.Add(this.tabBaoTri);
            this.tabControlHoSo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlHoSo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tabControlHoSo.Location = new System.Drawing.Point(0, 80);
            this.tabControlHoSo.Name = "tabControlHoSo";
            this.tabControlHoSo.SelectedIndex = 0;
            this.tabControlHoSo.Size = new System.Drawing.Size(800, 370);

            // tabLuanChuyen
            this.tabLuanChuyen.Controls.Add(this.dgvLuanChuyen);
            this.tabLuanChuyen.Location = new System.Drawing.Point(4, 29);
            this.tabLuanChuyen.Name = "tabLuanChuyen";
            this.tabLuanChuyen.Padding = new System.Windows.Forms.Padding(3);
            this.tabLuanChuyen.Size = new System.Drawing.Size(792, 337);
            this.tabLuanChuyen.Text = " 🔄 Lịch sử Luân chuyển ";
            this.tabLuanChuyen.UseVisualStyleBackColor = true;

            // dgvLuanChuyen
            this.dgvLuanChuyen.AllowUserToAddRows = false;
            this.dgvLuanChuyen.AllowUserToDeleteRows = false;
            this.dgvLuanChuyen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLuanChuyen.BackgroundColor = System.Drawing.Color.White;
            this.dgvLuanChuyen.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.dgvLuanChuyen.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLuanChuyen.ColumnHeadersHeight = 40;
            this.dgvLuanChuyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLuanChuyen.Location = new System.Drawing.Point(3, 3);
            this.dgvLuanChuyen.Name = "dgvLuanChuyen";
            this.dgvLuanChuyen.ReadOnly = true;
            this.dgvLuanChuyen.RowHeadersVisible = false;
            this.dgvLuanChuyen.RowTemplate.Height = 35;

            // tabBaoTri
            this.tabBaoTri.Controls.Add(this.dgvBaoTri);
            this.tabBaoTri.Location = new System.Drawing.Point(4, 29);
            this.tabBaoTri.Name = "tabBaoTri";
            this.tabBaoTri.Padding = new System.Windows.Forms.Padding(3);
            this.tabBaoTri.Size = new System.Drawing.Size(792, 337);
            this.tabBaoTri.Text = " ⚙ Lịch sử Bảo trì / Sửa chữa ";
            this.tabBaoTri.UseVisualStyleBackColor = true;

            // dgvBaoTri
            this.dgvBaoTri.AllowUserToAddRows = false;
            this.dgvBaoTri.AllowUserToDeleteRows = false;
            this.dgvBaoTri.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBaoTri.BackgroundColor = System.Drawing.Color.White;
            this.dgvBaoTri.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            this.dgvBaoTri.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBaoTri.ColumnHeadersHeight = 40;
            this.dgvBaoTri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBaoTri.Location = new System.Drawing.Point(3, 3);
            this.dgvBaoTri.Name = "dgvBaoTri";
            this.dgvBaoTri.ReadOnly = true;
            this.dgvBaoTri.RowHeadersVisible = false;
            this.dgvBaoTri.RowTemplate.Height = 35;

            // FrmHoSoThietBi
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlHoSo);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHoSoThietBi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hồ Sơ Thiết Bị";
            this.Load += new System.EventHandler(this.FrmHoSoThietBi_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tabControlHoSo.ResumeLayout(false);
            this.tabLuanChuyen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuanChuyen)).EndInit();
            this.tabBaoTri.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoTri)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTenThietBi;
        private System.Windows.Forms.TabControl tabControlHoSo;
        private System.Windows.Forms.TabPage tabLuanChuyen;
        private System.Windows.Forms.DataGridView dgvLuanChuyen;
        private System.Windows.Forms.TabPage tabBaoTri;
        private System.Windows.Forms.DataGridView dgvBaoTri;
    }
}