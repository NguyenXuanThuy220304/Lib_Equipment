namespace Lib_Equipment
{
    partial class FrmThongKeThietBi
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlCharts = new System.Windows.Forms.Panel();
            this.chartPhanBo = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTrangThai = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvBaoTri = new System.Windows.Forms.DataGridView();
            this.pnlGridBottom = new System.Windows.Forms.Panel();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPhanBo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).BeginInit();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoTri)).BeginInit();
            this.pnlGridBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCharts
            // 
            this.pnlCharts.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlCharts.Controls.Add(this.chartPhanBo);
            this.pnlCharts.Controls.Add(this.chartTrangThai);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCharts.Location = new System.Drawing.Point(0, 60);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCharts.Size = new System.Drawing.Size(1000, 320);
            this.pnlCharts.TabIndex = 1;
            // 
            // chartPhanBo
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPhanBo.ChartAreas.Add(chartArea1);
            this.chartPhanBo.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartPhanBo.Legends.Add(legend1);
            this.chartPhanBo.Location = new System.Drawing.Point(450, 10);
            this.chartPhanBo.Name = "chartPhanBo";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "SoLuong";
            this.chartPhanBo.Series.Add(series1);
            this.chartPhanBo.Size = new System.Drawing.Size(540, 300);
            this.chartPhanBo.TabIndex = 0;
            // 
            // chartTrangThai
            // 
            chartArea2.Name = "ChartArea1";
            this.chartTrangThai.ChartAreas.Add(chartArea2);
            this.chartTrangThai.Dock = System.Windows.Forms.DockStyle.Left;
            legend2.Name = "Legend1";
            this.chartTrangThai.Legends.Add(legend2);
            this.chartTrangThai.Location = new System.Drawing.Point(10, 10);
            this.chartTrangThai.Name = "chartTrangThai";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series2.Legend = "Legend1";
            series2.Name = "TrangThai";
            this.chartTrangThai.Series.Add(series2);
            this.chartTrangThai.Size = new System.Drawing.Size(440, 300);
            this.chartTrangThai.TabIndex = 1;
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvBaoTri);
            this.pnlGrid.Controls.Add(this.pnlGridBottom);
            this.pnlGrid.Controls.Add(this.lblGridTitle);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 380);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(10);
            this.pnlGrid.Size = new System.Drawing.Size(1000, 320);
            this.pnlGrid.TabIndex = 0;
            // 
            // dgvBaoTri
            // 
            this.dgvBaoTri.AllowUserToAddRows = false;
            this.dgvBaoTri.AllowUserToDeleteRows = false;
            this.dgvBaoTri.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBaoTri.BackgroundColor = System.Drawing.Color.White;
            this.dgvBaoTri.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.dgvBaoTri.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBaoTri.ColumnHeadersHeight = 40;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBaoTri.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBaoTri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBaoTri.Location = new System.Drawing.Point(10, 52);
            this.dgvBaoTri.Name = "dgvBaoTri";
            this.dgvBaoTri.ReadOnly = true;
            this.dgvBaoTri.RowHeadersVisible = false;
            this.dgvBaoTri.RowHeadersWidth = 51;
            this.dgvBaoTri.RowTemplate.Height = 35;
            this.dgvBaoTri.Size = new System.Drawing.Size(980, 198);
            this.dgvBaoTri.TabIndex = 0;
            this.dgvBaoTri.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBaoTri_CellDoubleClick);
            // 
            // pnlGridBottom
            // 
            this.pnlGridBottom.Controls.Add(this.btnXuatExcel);
            this.pnlGridBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlGridBottom.Location = new System.Drawing.Point(10, 250);
            this.pnlGridBottom.Name = "pnlGridBottom";
            this.pnlGridBottom.Size = new System.Drawing.Size(980, 60);
            this.pnlGridBottom.TabIndex = 1;
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXuatExcel.FlatAppearance.BorderSize = 0;
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Location = new System.Drawing.Point(760, 10);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(220, 40);
            this.btnXuatExcel.TabIndex = 0;
            this.btnXuatExcel.Text = "XUẤT BÁO CÁO EXCEL";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.lblGridTitle.Location = new System.Drawing.Point(10, 10);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.lblGridTitle.Size = new System.Drawing.Size(531, 42);
            this.lblGridTitle.TabIndex = 2;
            this.lblGridTitle.Text = "⚙ DANH SÁCH THIẾT BỊ ĐANG HỎNG / CHỜ BẢO TRÌ";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1000, 60);
            this.pnlTop.TabIndex = 2;
            // 
            // FrmThongKeThietBi
            // 
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlCharts);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmThongKeThietBi";
            this.Load += new System.EventHandler(this.FrmThongKeThietBi_Load);
            this.pnlCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPhanBo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoTri)).EndInit();
            this.pnlGridBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Panel pnlCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTrangThai;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPhanBo;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgvBaoTri;
        private System.Windows.Forms.Panel pnlGridBottom;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Panel pnlTop;
    }
}