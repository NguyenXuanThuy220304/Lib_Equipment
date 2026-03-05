namespace Lib_Equipment
{
    partial class FrmSaoLuuPhucHoi
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
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbBackup = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblBackupDesc = new System.Windows.Forms.Label();
            this.txtBackupPath = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnBrowseBackup = new Guna.UI2.WinForms.Guna2Button();
            this.btnThucHienBackup = new Guna.UI2.WinForms.Guna2Button();
            this.gbRestore = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblRestoreDesc = new System.Windows.Forms.Label();
            this.txtRestorePath = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnBrowseRestore = new Guna.UI2.WinForms.Guna2Button();
            this.btnThucHienRestore = new Guna.UI2.WinForms.Guna2Button();
            this.pnlTop.SuspendLayout();
            this.gbBackup.SuspendLayout();
            this.gbRestore.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(437, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SAO LƯU VÀ PHỤC HỒI DỮ LIỆU (BACKUP)";
            // 
            // gbBackup
            // 
            this.gbBackup.BorderRadius = 5;
            this.gbBackup.Controls.Add(this.btnThucHienBackup);
            this.gbBackup.Controls.Add(this.btnBrowseBackup);
            this.gbBackup.Controls.Add(this.txtBackupPath);
            this.gbBackup.Controls.Add(this.lblBackupDesc);
            this.gbBackup.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.gbBackup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbBackup.ForeColor = System.Drawing.Color.White;
            this.gbBackup.Location = new System.Drawing.Point(30, 80);
            this.gbBackup.Name = "gbBackup";
            this.gbBackup.Size = new System.Drawing.Size(890, 200);
            this.gbBackup.TabIndex = 1;
            this.gbBackup.Text = "1. SAO LƯU DỮ LIỆU (BACKUP)";
            // 
            // lblBackupDesc
            // 
            this.lblBackupDesc.AutoSize = true;
            this.lblBackupDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBackupDesc.ForeColor = System.Drawing.Color.Black;
            this.lblBackupDesc.Location = new System.Drawing.Point(20, 60);
            this.lblBackupDesc.Name = "lblBackupDesc";
            this.lblBackupDesc.Size = new System.Drawing.Size(418, 19);
            this.lblBackupDesc.TabIndex = 0;
            this.lblBackupDesc.Text = "Chọn thư mục lưu trữ để xuất toàn bộ cơ sở dữ liệu ra một file .bak";
            // 
            // txtBackupPath
            // 
            this.txtBackupPath.BorderRadius = 5;
            this.txtBackupPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBackupPath.DefaultText = "";
            this.txtBackupPath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBackupPath.ForeColor = System.Drawing.Color.Black;
            this.txtBackupPath.Location = new System.Drawing.Point(24, 95);
            this.txtBackupPath.Name = "txtBackupPath";
            this.txtBackupPath.ReadOnly = true;
            this.txtBackupPath.Size = new System.Drawing.Size(600, 40);
            this.txtBackupPath.TabIndex = 1;
            // 
            // btnBrowseBackup
            // 
            this.btnBrowseBackup.BorderRadius = 5;
            this.btnBrowseBackup.FillColor = System.Drawing.Color.Gray;
            this.btnBrowseBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBrowseBackup.ForeColor = System.Drawing.Color.White;
            this.btnBrowseBackup.Location = new System.Drawing.Point(640, 95);
            this.btnBrowseBackup.Name = "btnBrowseBackup";
            this.btnBrowseBackup.Size = new System.Drawing.Size(100, 40);
            this.btnBrowseBackup.TabIndex = 2;
            this.btnBrowseBackup.Text = "CHỌN...";
            this.btnBrowseBackup.Click += new System.EventHandler(this.btnBrowseBackup_Click);
            // 
            // btnThucHienBackup
            // 
            this.btnThucHienBackup.BorderRadius = 5;
            this.btnThucHienBackup.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThucHienBackup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThucHienBackup.ForeColor = System.Drawing.Color.White;
            this.btnThucHienBackup.Location = new System.Drawing.Point(24, 150);
            this.btnThucHienBackup.Name = "btnThucHienBackup";
            this.btnThucHienBackup.Size = new System.Drawing.Size(200, 40);
            this.btnThucHienBackup.TabIndex = 3;
            this.btnThucHienBackup.Text = "TIẾN HÀNH SAO LƯU";
            this.btnThucHienBackup.Click += new System.EventHandler(this.btnThucHienBackup_Click);
            // 
            // gbRestore
            // 
            this.gbRestore.BorderRadius = 5;
            this.gbRestore.Controls.Add(this.btnThucHienRestore);
            this.gbRestore.Controls.Add(this.btnBrowseRestore);
            this.gbRestore.Controls.Add(this.txtRestorePath);
            this.gbRestore.Controls.Add(this.lblRestoreDesc);
            this.gbRestore.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.gbRestore.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbRestore.ForeColor = System.Drawing.Color.White;
            this.gbRestore.Location = new System.Drawing.Point(30, 310);
            this.gbRestore.Name = "gbRestore";
            this.gbRestore.Size = new System.Drawing.Size(890, 200);
            this.gbRestore.TabIndex = 2;
            this.gbRestore.Text = "2. PHỤC HỒI DỮ LIỆU (RESTORE)";
            // 
            // lblRestoreDesc
            // 
            this.lblRestoreDesc.AutoSize = true;
            this.lblRestoreDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRestoreDesc.ForeColor = System.Drawing.Color.Black;
            this.lblRestoreDesc.Location = new System.Drawing.Point(20, 60);
            this.lblRestoreDesc.Name = "lblRestoreDesc";
            this.lblRestoreDesc.Size = new System.Drawing.Size(437, 19);
            this.lblRestoreDesc.TabIndex = 0;
            this.lblRestoreDesc.Text = "Chọn file .bak đã sao lưu trước đó để khôi phục lại toàn bộ dữ liệu. \r\n";
            // 
            // txtRestorePath
            // 
            this.txtRestorePath.BorderRadius = 5;
            this.txtRestorePath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRestorePath.DefaultText = "";
            this.txtRestorePath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRestorePath.ForeColor = System.Drawing.Color.Black;
            this.txtRestorePath.Location = new System.Drawing.Point(24, 95);
            this.txtRestorePath.Name = "txtRestorePath";
            this.txtRestorePath.ReadOnly = true;
            this.txtRestorePath.Size = new System.Drawing.Size(600, 40);
            this.txtRestorePath.TabIndex = 1;
            // 
            // btnBrowseRestore
            // 
            this.btnBrowseRestore.BorderRadius = 5;
            this.btnBrowseRestore.FillColor = System.Drawing.Color.Gray;
            this.btnBrowseRestore.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBrowseRestore.ForeColor = System.Drawing.Color.White;
            this.btnBrowseRestore.Location = new System.Drawing.Point(640, 95);
            this.btnBrowseRestore.Name = "btnBrowseRestore";
            this.btnBrowseRestore.Size = new System.Drawing.Size(100, 40);
            this.btnBrowseRestore.TabIndex = 2;
            this.btnBrowseRestore.Text = "CHỌN...";
            this.btnBrowseRestore.Click += new System.EventHandler(this.btnBrowseRestore_Click);
            // 
            // btnThucHienRestore
            // 
            this.btnThucHienRestore.BorderRadius = 5;
            this.btnThucHienRestore.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnThucHienRestore.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThucHienRestore.ForeColor = System.Drawing.Color.White;
            this.btnThucHienRestore.Location = new System.Drawing.Point(24, 150);
            this.btnThucHienRestore.Name = "btnThucHienRestore";
            this.btnThucHienRestore.Size = new System.Drawing.Size(200, 40);
            this.btnThucHienRestore.TabIndex = 3;
            this.btnThucHienRestore.Text = "TIẾN HÀNH PHỤC HỒI";
            this.btnThucHienRestore.Click += new System.EventHandler(this.btnThucHienRestore_Click);
            // 
            // FrmSaoLuuPhucHoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(950, 640);
            this.Controls.Add(this.gbRestore);
            this.Controls.Add(this.gbBackup);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSaoLuuPhucHoi";
            this.Text = "Sao Lưu Phục Hồi";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.gbBackup.ResumeLayout(false);
            this.gbBackup.PerformLayout();
            this.gbRestore.ResumeLayout(false);
            this.gbRestore.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2GroupBox gbBackup;
        private System.Windows.Forms.Label lblBackupDesc;
        private Guna.UI2.WinForms.Guna2TextBox txtBackupPath;
        private Guna.UI2.WinForms.Guna2Button btnBrowseBackup;
        private Guna.UI2.WinForms.Guna2Button btnThucHienBackup;
        private Guna.UI2.WinForms.Guna2GroupBox gbRestore;
        private Guna.UI2.WinForms.Guna2Button btnThucHienRestore;
        private Guna.UI2.WinForms.Guna2Button btnBrowseRestore;
        private Guna.UI2.WinForms.Guna2TextBox txtRestorePath;
        private System.Windows.Forms.Label lblRestoreDesc;
    }
}