namespace Lib_Equipment
{
    partial class FrmQRThietBi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.picQR = new System.Windows.Forms.PictureBox();
            this.lblQRStatus = new System.Windows.Forms.Label();
            this.btnInQR = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuuQR = new Guna.UI2.WinForms.Guna2Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitleInfo = new System.Windows.Forms.Label();

            // Labels thông tin
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMaTB = new System.Windows.Forms.Label();
            this.lblTenTB = new System.Windows.Forms.Label();
            this.lblPhanLoai = new System.Windows.Forms.Label();
            this.lblKhoaPhong = new System.Windows.Forms.Label();
            this.lblNgayNhap = new System.Windows.Forms.Label();
            this.lblGiaNhap = new System.Windows.Forms.Label();
            this.lblTinhTrang = new System.Windows.Forms.Label();
            this.lblBaoTri = new System.Windows.Forms.Label();

            this.lblLichSuTitle = new System.Windows.Forms.Label();
            this.dgvLichSu = new Guna.UI2.WinForms.Guna2DataGridView();

            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.SuspendLayout();

            // ── pnlLeft (chứa QR + nút) ─────────────────────
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Width = 320;
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(20);
            this.pnlLeft.Controls.Add(this.picQR);
            this.pnlLeft.Controls.Add(this.lblQRStatus);
            this.pnlLeft.Controls.Add(this.btnInQR);
            this.pnlLeft.Controls.Add(this.btnLuuQR);

            // picQR
            this.picQR.Location = new System.Drawing.Point(20, 20);
            this.picQR.Size = new System.Drawing.Size(280, 280);
            this.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // lblQRStatus
            this.lblQRStatus.Location = new System.Drawing.Point(20, 305);
            this.lblQRStatus.Size = new System.Drawing.Size(280, 25);
            this.lblQRStatus.Text = "✅ QR sẵn sàng - Quét để xem thông tin";
            this.lblQRStatus.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblQRStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblQRStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // btnInQR
            this.btnInQR.Text = "🖨️  IN QR";
            this.btnInQR.Location = new System.Drawing.Point(20, 340);
            this.btnInQR.Size = new System.Drawing.Size(280, 50);
            this.btnInQR.BorderRadius = 8;
            this.btnInQR.FillColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.btnInQR.ForeColor = System.Drawing.Color.White;
            this.btnInQR.Font = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold);
            this.btnInQR.Click += new System.EventHandler(this.btnInQR_Click);

            // btnLuuQR
            this.btnLuuQR.Text = "💾  LƯU ẢNH QR";
            this.btnLuuQR.Location = new System.Drawing.Point(20, 400);
            this.btnLuuQR.Size = new System.Drawing.Size(280, 45);
            this.btnLuuQR.BorderRadius = 8;
            this.btnLuuQR.FillColor = System.Drawing.Color.FromArgb(0, 172, 193);
            this.btnLuuQR.ForeColor = System.Drawing.Color.White;
            this.btnLuuQR.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            this.btnLuuQR.Click += new System.EventHandler(this.btnLuuQR_Click);

            // ── pnlRight (thông tin + lịch sử) ─────────────
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Padding = new System.Windows.Forms.Padding(15);
            this.pnlRight.Controls.Add(this.pnlInfo);
            this.pnlRight.Controls.Add(this.lblLichSuTitle);
            this.pnlRight.Controls.Add(this.dgvLichSu);

            // pnlInfo (thẻ thông tin)
            this.pnlInfo.BackColor = System.Drawing.Color.White;
            this.pnlInfo.BorderRadius = 10;
            this.pnlInfo.Location = new System.Drawing.Point(15, 15);
            this.pnlInfo.Size = new System.Drawing.Size(640, 230);
            this.pnlInfo.Controls.Add(this.lblTitleInfo);

            // Thêm label tiêu đề
            this.lblTitleInfo.Text = "📋  THÔNG TIN THIẾT BỊ";
            this.lblTitleInfo.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Bold);
            this.lblTitleInfo.ForeColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.lblTitleInfo.Location = new System.Drawing.Point(15, 12);
            this.lblTitleInfo.AutoSize = true;

            // =========================================================
            // ĐÃ TRẢI PHẲNG CODE ĐỂ DESIGNER KHÔNG BỊ LÚ NỮA
            // =========================================================

            // --- DÒNG 1 (Y = 45) ---
            this.label1.Text = "Mã TB:";
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.label1.Location = new System.Drawing.Point(15, 45);
            this.label1.AutoSize = true;
            this.pnlInfo.Controls.Add(this.label1);

            this.lblMaTB.Text = "---";
            this.lblMaTB.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.lblMaTB.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblMaTB.Location = new System.Drawing.Point(165, 45);
            this.lblMaTB.Size = new System.Drawing.Size(160, 28);
            this.lblMaTB.AutoEllipsis = true;
            this.pnlInfo.Controls.Add(this.lblMaTB);

            this.label2.Text = "Khoa/Phòng:";
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.label2.Location = new System.Drawing.Point(340, 45);
            this.label2.AutoSize = true;
            this.pnlInfo.Controls.Add(this.label2);

            this.lblKhoaPhong.Text = "---";
            this.lblKhoaPhong.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.lblKhoaPhong.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblKhoaPhong.Location = new System.Drawing.Point(490, 45);
            this.lblKhoaPhong.Size = new System.Drawing.Size(140, 28);
            this.lblKhoaPhong.AutoEllipsis = true;
            this.pnlInfo.Controls.Add(this.lblKhoaPhong);

            // --- DÒNG 2 (Y = 73) ---
            this.label3.Text = "Tên TB:";
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.label3.Location = new System.Drawing.Point(15, 73);
            this.label3.AutoSize = true;
            this.pnlInfo.Controls.Add(this.label3);

            this.lblTenTB.Text = "---";
            this.lblTenTB.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.lblTenTB.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblTenTB.Location = new System.Drawing.Point(165, 73);
            this.lblTenTB.Size = new System.Drawing.Size(160, 28);
            this.lblTenTB.AutoEllipsis = true;
            this.pnlInfo.Controls.Add(this.lblTenTB);

            this.label4.Text = "Ngày nhập:";
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.label4.Location = new System.Drawing.Point(340, 73);
            this.label4.AutoSize = true;
            this.pnlInfo.Controls.Add(this.label4);

            this.lblNgayNhap.Text = "---";
            this.lblNgayNhap.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.lblNgayNhap.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblNgayNhap.Location = new System.Drawing.Point(490, 73);
            this.lblNgayNhap.Size = new System.Drawing.Size(140, 28);
            this.lblNgayNhap.AutoEllipsis = true;
            this.pnlInfo.Controls.Add(this.lblNgayNhap);

            // --- DÒNG 3 (Y = 101) ---
            this.label5.Text = "Phân loại:";
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.label5.Location = new System.Drawing.Point(15, 101);
            this.label5.AutoSize = true;
            this.pnlInfo.Controls.Add(this.label5);

            this.lblPhanLoai.Text = "---";
            this.lblPhanLoai.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.lblPhanLoai.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblPhanLoai.Location = new System.Drawing.Point(165, 101);
            this.lblPhanLoai.Size = new System.Drawing.Size(160, 28);
            this.lblPhanLoai.AutoEllipsis = true;
            this.pnlInfo.Controls.Add(this.lblPhanLoai);

            this.label6.Text = "Giá nhập:";
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.label6.Location = new System.Drawing.Point(340, 101);
            this.label6.AutoSize = true;
            this.pnlInfo.Controls.Add(this.label6);

            this.lblGiaNhap.Text = "---";
            this.lblGiaNhap.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.lblGiaNhap.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblGiaNhap.Location = new System.Drawing.Point(490, 101);
            this.lblGiaNhap.Size = new System.Drawing.Size(140, 28);
            this.lblGiaNhap.AutoEllipsis = true;
            this.pnlInfo.Controls.Add(this.lblGiaNhap);

            // --- DÒNG 4 (Y = 129) ---
            this.label7.Text = "Tình trạng:";
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.label7.Location = new System.Drawing.Point(15, 129);
            this.label7.AutoSize = true;
            this.pnlInfo.Controls.Add(this.label7);

            this.lblTinhTrang.Text = "---";
            this.lblTinhTrang.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.lblTinhTrang.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblTinhTrang.Location = new System.Drawing.Point(165, 129);
            this.lblTinhTrang.Size = new System.Drawing.Size(160, 28);
            this.lblTinhTrang.AutoEllipsis = true;
            this.pnlInfo.Controls.Add(this.lblTinhTrang);

            this.label8.Text = "BT định kỳ:";
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.label8.Location = new System.Drawing.Point(340, 129);
            this.label8.AutoSize = true;
            this.pnlInfo.Controls.Add(this.label8);

            this.lblBaoTri.Text = "---";
            this.lblBaoTri.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.lblBaoTri.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblBaoTri.Location = new System.Drawing.Point(490, 129);
            this.lblBaoTri.Size = new System.Drawing.Size(140, 28);
            this.lblBaoTri.AutoEllipsis = true;
            this.pnlInfo.Controls.Add(this.lblBaoTri);
            // =========================================================

            // lblLichSuTitle
            this.lblLichSuTitle.Text = "🔧  LỊCH SỬ BẢO TRÌ";
            this.lblLichSuTitle.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Bold);
            this.lblLichSuTitle.ForeColor = System.Drawing.Color.FromArgb(26, 75, 132);
            this.lblLichSuTitle.Location = new System.Drawing.Point(15, 255);
            this.lblLichSuTitle.AutoSize = true;

            // dgvLichSu
            this.dgvLichSu.AllowUserToAddRows = false;
            this.dgvLichSu.AllowUserToDeleteRows = false;
            this.dgvLichSu.ReadOnly = true;
            this.dgvLichSu.RowHeadersVisible = false;
            this.dgvLichSu.Location = new System.Drawing.Point(15, 283);
            this.dgvLichSu.Size = new System.Drawing.Size(640, 260);
            this.dgvLichSu.RowTemplate.Height = 32;
            this.dgvLichSu.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.dgvLichSu.ColumnHeadersHeight = 36;

            // ── Form chính ───────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.ClientSize = new System.Drawing.Size(985, 580);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "FrmQRThietBi";
            this.Text = "QR Code Thiết Bị";
            this.Load += new System.EventHandler(this.FrmQRThietBi_Load);

            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.ResumeLayout(false);
        }

        // Controls
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.PictureBox picQR;
        private System.Windows.Forms.Label lblQRStatus;
        private Guna.UI2.WinForms.Guna2Button btnInQR;
        private Guna.UI2.WinForms.Guna2Button btnLuuQR;
        private Guna.UI2.WinForms.Guna2Panel pnlInfo;
        private System.Windows.Forms.Label lblTitleInfo;
        private System.Windows.Forms.Label label1, label2, label3, label4;
        private System.Windows.Forms.Label label5, label6, label7, label8;
        private System.Windows.Forms.Label lblMaTB;
        private System.Windows.Forms.Label lblTenTB;
        private System.Windows.Forms.Label lblPhanLoai;
        private System.Windows.Forms.Label lblKhoaPhong;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.Label lblGiaNhap;
        private System.Windows.Forms.Label lblTinhTrang;
        private System.Windows.Forms.Label lblBaoTri;
        private System.Windows.Forms.Label lblLichSuTitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvLichSu;
    }
}