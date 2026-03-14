namespace Lib_Equipment
{
    partial class FrmThongKeThuVien
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlCharts = new System.Windows.Forms.Panel();
            this.chartTrangThai = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTopSach = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvBlacklist = new System.Windows.Forms.DataGridView();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopSach)).BeginInit();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlacklist)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCharts
            // 
            this.pnlCharts.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlCharts.Controls.Add(this.chartTrangThai);
            this.pnlCharts.Controls.Add(this.chartTopSach);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCharts.Location = new System.Drawing.Point(0, 0);
            this.pnlCharts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlCharts.Size = new System.Drawing.Size(1333, 394);
            this.pnlCharts.TabIndex = 1;
            // 
            // chartTrangThai
            // 
            chartArea1.Name = "ChartArea1";
            this.chartTrangThai.ChartAreas.Add(chartArea1);
            this.chartTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartTrangThai.Legends.Add(legend1);
            this.chartTrangThai.Location = new System.Drawing.Point(733, 12);
            this.chartTrangThai.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartTrangThai.Name = "chartTrangThai";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Trạng thái";
            this.chartTrangThai.Series.Add(series1);
            this.chartTrangThai.Size = new System.Drawing.Size(587, 370);
            this.chartTrangThai.TabIndex = 1;
            this.chartTrangThai.Text = "Tỷ trọng kho sách";
            // 
            // chartTopSach
            // 
            chartArea2.Name = "ChartArea1";
            this.chartTopSach.ChartAreas.Add(chartArea2);
            this.chartTopSach.Dock = System.Windows.Forms.DockStyle.Left;
            legend2.Name = "Legend1";
            this.chartTopSach.Legends.Add(legend2);
            this.chartTopSach.Location = new System.Drawing.Point(13, 12);
            this.chartTopSach.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartTopSach.Name = "chartTopSach";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Lượt mượn";
            this.chartTopSach.Series.Add(series2);
            this.chartTopSach.Size = new System.Drawing.Size(720, 370);
            this.chartTopSach.TabIndex = 0;
            this.chartTopSach.Text = "Top 10 Sách Mượn Nhiều Nhất";
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvBlacklist);
            this.pnlGrid.Controls.Add(this.lblGridTitle);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 394);
            this.pnlGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlGrid.Size = new System.Drawing.Size(1333, 468);
            this.pnlGrid.TabIndex = 2;
            // 
            // dgvBlacklist
            // 
            this.dgvBlacklist.AllowUserToAddRows = false;
            this.dgvBlacklist.AllowUserToDeleteRows = false;
            this.dgvBlacklist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBlacklist.BackgroundColor = System.Drawing.Color.White;
            this.dgvBlacklist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBlacklist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBlacklist.ColumnHeadersHeight = 40;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBlacklist.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBlacklist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBlacklist.Location = new System.Drawing.Point(13, 57);
            this.dgvBlacklist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvBlacklist.Name = "dgvBlacklist";
            this.dgvBlacklist.ReadOnly = true;
            this.dgvBlacklist.RowHeadersVisible = false;
            this.dgvBlacklist.RowHeadersWidth = 51;
            this.dgvBlacklist.RowTemplate.Height = 35;
            this.dgvBlacklist.Size = new System.Drawing.Size(1307, 399);
            this.dgvBlacklist.TabIndex = 1;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblGridTitle.Location = new System.Drawing.Point(13, 12);
            this.lblGridTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 17);
            this.lblGridTitle.Size = new System.Drawing.Size(429, 45);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "⚠ DANH SÁCH ĐEN - SINH VIÊN NỢ PHẠT";
            // 
            // FrmThongKeThuVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 862);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlCharts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmThongKeThuVien";
            this.Text = "Thống Kê Thư Viện";
            this.Load += new System.EventHandler(this.FrmThongKeThuVien_Load);
            this.pnlCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopSach)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlacklist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTopSach;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTrangThai;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgvBlacklist;
    }
}