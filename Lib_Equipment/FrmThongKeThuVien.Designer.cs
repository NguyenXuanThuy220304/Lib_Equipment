namespace Lib_Equipment
{
    partial class FrmThongKeThuVien
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();

            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlCharts = new System.Windows.Forms.Panel();
            this.chartTopSach = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTrangThai = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvBlacklist = new System.Windows.Forms.DataGridView();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlGridBottom = new System.Windows.Forms.Panel();
            this.btnXuatExcelThuVien = new System.Windows.Forms.Button();

            this.pnlCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopSach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).BeginInit();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlacklist)).BeginInit();
            this.pnlGridBottom.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1000, 60);
            this.pnlTop.TabIndex = 0;

            // 
            // pnlCharts
            // 
            this.pnlCharts.Controls.Add(this.chartTrangThai);
            this.pnlCharts.Controls.Add(this.chartTopSach);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCharts.Location = new System.Drawing.Point(0, 60);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCharts.Size = new System.Drawing.Size(1000, 350);
            this.pnlCharts.TabIndex = 1;

            // 
            // chartTopSach
            // 
            chartArea1.Name = "ChartArea1";
            this.chartTopSach.ChartAreas.Add(chartArea1);
            this.chartTopSach.Dock = System.Windows.Forms.DockStyle.Left;

            // Cấu hình Legend trực tiếp trong Designer
            legend1.Name = "LegendTenSach";
            legend1.Enabled = true; // BẮT BUỘC BẬT
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.IsTextAutoFit = true;
            legend1.MaximumAutoSize = 50F; // Cho phép chiếm 50% chiều rộng biểu đồ

            this.chartTopSach.Legends.Add(legend1);
            this.chartTopSach.Location = new System.Drawing.Point(10, 10);
            this.chartTopSach.Name = "chartTopSach";
            this.chartTopSach.Size = new System.Drawing.Size(850, 330); // Giảm xuống 750 để cân đối hơn
            this.chartTopSach.TabIndex = 0;

            // 
            // chartTrangThai
            // 
            chartArea2.Name = "ChartArea1";
            this.chartTrangThai.ChartAreas.Add(chartArea2);
            this.chartTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartTrangThai.Legends.Add(legend2);
            this.chartTrangThai.Location = new System.Drawing.Point(560, 10);
            this.chartTrangThai.Name = "chartTrangThai";
            this.chartTrangThai.Size = new System.Drawing.Size(430, 330);
            this.chartTrangThai.TabIndex = 1;

            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.dgvBlacklist);
            this.pnlGrid.Controls.Add(this.pnlGridBottom);
            this.pnlGrid.Controls.Add(this.lblGridTitle);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 410);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(10);
            this.pnlGrid.Size = new System.Drawing.Size(1000, 290);
            this.pnlGrid.TabIndex = 2;

            // 
            // lblGridTitle
            // 
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.lblGridTitle.Location = new System.Drawing.Point(10, 10);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(980, 40);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "⚠️ DANH SÁCH ĐEN (ĐỘC GIẢ QUÁ HẠN)";
            this.lblGridTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // pnlGridBottom
            // 
            this.pnlGridBottom.Controls.Add(this.btnXuatExcelThuVien);
            this.pnlGridBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlGridBottom.Location = new System.Drawing.Point(10, 220);
            this.pnlGridBottom.Name = "pnlGridBottom";
            this.pnlGridBottom.Size = new System.Drawing.Size(980, 60);
            this.pnlGridBottom.TabIndex = 2;

            // 
            // btnXuatExcelThuVien
            // 
            this.btnXuatExcelThuVien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatExcelThuVien.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnXuatExcelThuVien.FlatAppearance.BorderSize = 0;
            this.btnXuatExcelThuVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcelThuVien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcelThuVien.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcelThuVien.Location = new System.Drawing.Point(760, 10);
            this.btnXuatExcelThuVien.Name = "btnXuatExcelThuVien";
            this.btnXuatExcelThuVien.Size = new System.Drawing.Size(220, 40);
            this.btnXuatExcelThuVien.TabIndex = 0;
            this.btnXuatExcelThuVien.Text = "XUẤT EXCEL DANH SÁCH ĐEN";
            this.btnXuatExcelThuVien.UseVisualStyleBackColor = false;
            this.btnXuatExcelThuVien.Click += new System.EventHandler(this.btnXuatExcelThuVien_Click);

            // 
            // dgvBlacklist
            // 
            this.dgvBlacklist.AllowUserToAddRows = false;
            this.dgvBlacklist.AllowUserToDeleteRows = false;
            this.dgvBlacklist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBlacklist.BackgroundColor = System.Drawing.Color.White;
            this.dgvBlacklist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.dgvBlacklist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBlacklist.ColumnHeadersHeight = 45;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBlacklist.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBlacklist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBlacklist.Location = new System.Drawing.Point(10, 50);
            this.dgvBlacklist.Name = "dgvBlacklist";
            this.dgvBlacklist.ReadOnly = true;
            this.dgvBlacklist.RowHeadersVisible = false;
            this.dgvBlacklist.RowHeadersWidth = 51;
            this.dgvBlacklist.RowTemplate.Height = 35;
            this.dgvBlacklist.Size = new System.Drawing.Size(980, 170);
            this.dgvBlacklist.TabIndex = 1;

            // 
            // FrmThongKeThuVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 249);
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlCharts);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmThongKeThuVien";
            this.Text = "Thống Kê Thư Viện";
            this.Load += new System.EventHandler(this.FrmThongKeThuVien_Load);
            this.pnlCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTopSach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlacklist)).EndInit();
            this.pnlGridBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTopSach;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTrangThai;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgvBlacklist;
        private System.Windows.Forms.Panel pnlGridBottom;
        private System.Windows.Forms.Button btnXuatExcelThuVien;
    }
}