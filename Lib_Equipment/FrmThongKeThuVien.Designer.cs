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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pnlLeftWrapper = new System.Windows.Forms.Panel();
            this.pnlLeftMenu = new Guna.UI2.WinForms.Guna2Panel();
            this.btnMenuBlacklist = new Guna.UI2.WinForms.Guna2Button();
            this.btnMenuKhoSach = new Guna.UI2.WinForms.Guna2Button();
            this.btnMenuTopSach = new Guna.UI2.WinForms.Guna2Button();
            this.btnMenuDashboard = new Guna.UI2.WinForms.Guna2Button();
            this.tabControlThongKe = new Guna.UI2.WinForms.Guna2TabControl();
            this.tabDashboard = new System.Windows.Forms.TabPage();
            this.pnlCharts = new System.Windows.Forms.Panel();
            this.chartTrangThai = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTopSach = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlCards = new System.Windows.Forms.Panel();
            this.card4 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTongPhat = new System.Windows.Forms.Label();
            this.lblTitle4 = new System.Windows.Forms.Label();
            this.card3 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblDangMuon = new System.Windows.Forms.Label();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.card2 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTongDocGia = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.card1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTongSach = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.pnlTopAction = new System.Windows.Forms.Panel();
            this.btnXuatExcelAll = new Guna.UI2.WinForms.Guna2Button();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.tabTopSach = new System.Windows.Forms.TabPage();
            this.dgvTopSach = new System.Windows.Forms.DataGridView();
            this.tabKhoSach = new System.Windows.Forms.TabPage();
            this.dgvKhoSach = new System.Windows.Forms.DataGridView();
            this.tabBlacklist = new System.Windows.Forms.TabPage();
            this.dgvBlacklist = new System.Windows.Forms.DataGridView();
            this.pnlLeftWrapper.SuspendLayout();
            this.pnlLeftMenu.SuspendLayout();
            this.tabControlThongKe.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            this.pnlCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopSach)).BeginInit();
            this.pnlCards.SuspendLayout();
            this.card4.SuspendLayout();
            this.card3.SuspendLayout();
            this.card2.SuspendLayout();
            this.card1.SuspendLayout();
            this.pnlTopAction.SuspendLayout();
            this.tabTopSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSach)).BeginInit();
            this.tabKhoSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhoSach)).BeginInit();
            this.tabBlacklist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlacklist)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeftWrapper
            // 
            this.pnlLeftWrapper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.pnlLeftWrapper.Controls.Add(this.pnlLeftMenu);
            this.pnlLeftWrapper.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeftWrapper.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftWrapper.Name = "pnlLeftWrapper";
            this.pnlLeftWrapper.Size = new System.Drawing.Size(90, 750);
            this.pnlLeftWrapper.TabIndex = 1;
            // 
            // pnlLeftMenu
            // 
            this.pnlLeftMenu.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeftMenu.BorderRadius = 15;
            this.pnlLeftMenu.Controls.Add(this.btnMenuBlacklist);
            this.pnlLeftMenu.Controls.Add(this.btnMenuKhoSach);
            this.pnlLeftMenu.Controls.Add(this.btnMenuTopSach);
            this.pnlLeftMenu.Controls.Add(this.btnMenuDashboard);
            this.pnlLeftMenu.FillColor = System.Drawing.Color.White;
            this.pnlLeftMenu.Location = new System.Drawing.Point(15, 15);
            this.pnlLeftMenu.Name = "pnlLeftMenu";
            this.pnlLeftMenu.ShadowDecoration.Enabled = true;
            this.pnlLeftMenu.Size = new System.Drawing.Size(60, 240);
            this.pnlLeftMenu.TabIndex = 0;
            // 
            // btnMenuBlacklist
            // 
            this.btnMenuBlacklist.BorderRadius = 15;
            this.btnMenuBlacklist.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnMenuBlacklist.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnMenuBlacklist.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnMenuBlacklist.CustomizableEdges.TopLeft = false;
            this.btnMenuBlacklist.CustomizableEdges.TopRight = false;
            this.btnMenuBlacklist.FillColor = System.Drawing.Color.Transparent;
            this.btnMenuBlacklist.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.btnMenuBlacklist.ForeColor = System.Drawing.Color.Gray;
            this.btnMenuBlacklist.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnMenuBlacklist.Location = new System.Drawing.Point(0, 180);
            this.btnMenuBlacklist.Name = "btnMenuBlacklist";
            this.btnMenuBlacklist.Size = new System.Drawing.Size(60, 60);
            this.btnMenuBlacklist.TabIndex = 3;
            this.btnMenuBlacklist.Text = "☠️";
            this.btnMenuBlacklist.UseTransparentBackground = true;
            this.btnMenuBlacklist.CheckedChanged += new System.EventHandler(this.btnMenuBlacklist_CheckedChanged);
            // 
            // btnMenuKhoSach
            // 
            this.btnMenuKhoSach.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnMenuKhoSach.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnMenuKhoSach.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnMenuKhoSach.FillColor = System.Drawing.Color.Transparent;
            this.btnMenuKhoSach.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.btnMenuKhoSach.ForeColor = System.Drawing.Color.Gray;
            this.btnMenuKhoSach.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnMenuKhoSach.Location = new System.Drawing.Point(0, 120);
            this.btnMenuKhoSach.Name = "btnMenuKhoSach";
            this.btnMenuKhoSach.Size = new System.Drawing.Size(60, 60);
            this.btnMenuKhoSach.TabIndex = 2;
            this.btnMenuKhoSach.Text = "📚";
            this.btnMenuKhoSach.UseTransparentBackground = true;
            this.btnMenuKhoSach.CheckedChanged += new System.EventHandler(this.btnMenuKhoSach_CheckedChanged);
            // 
            // btnMenuTopSach
            // 
            this.btnMenuTopSach.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnMenuTopSach.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnMenuTopSach.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnMenuTopSach.FillColor = System.Drawing.Color.Transparent;
            this.btnMenuTopSach.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.btnMenuTopSach.ForeColor = System.Drawing.Color.Gray;
            this.btnMenuTopSach.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnMenuTopSach.Location = new System.Drawing.Point(0, 60);
            this.btnMenuTopSach.Name = "btnMenuTopSach";
            this.btnMenuTopSach.Size = new System.Drawing.Size(60, 60);
            this.btnMenuTopSach.TabIndex = 1;
            this.btnMenuTopSach.Text = "📈";
            this.btnMenuTopSach.UseTransparentBackground = true;
            this.btnMenuTopSach.CheckedChanged += new System.EventHandler(this.btnMenuTopSach_CheckedChanged);
            this.btnMenuTopSach.Click += new System.EventHandler(this.btnMenuTopSach_Click);
            // 
            // btnMenuDashboard
            // 
            this.btnMenuDashboard.BorderRadius = 15;
            this.btnMenuDashboard.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnMenuDashboard.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnMenuDashboard.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnMenuDashboard.CustomizableEdges.BottomLeft = false;
            this.btnMenuDashboard.CustomizableEdges.BottomRight = false;
            this.btnMenuDashboard.FillColor = System.Drawing.Color.Transparent;
            this.btnMenuDashboard.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.btnMenuDashboard.ForeColor = System.Drawing.Color.Gray;
            this.btnMenuDashboard.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnMenuDashboard.Location = new System.Drawing.Point(0, 0);
            this.btnMenuDashboard.Name = "btnMenuDashboard";
            this.btnMenuDashboard.Size = new System.Drawing.Size(60, 60);
            this.btnMenuDashboard.TabIndex = 0;
            this.btnMenuDashboard.Text = "📊";
            this.btnMenuDashboard.UseTransparentBackground = true;
            this.btnMenuDashboard.CheckedChanged += new System.EventHandler(this.btnMenuDashboard_CheckedChanged);
            // 
            // tabControlThongKe
            // 
            this.tabControlThongKe.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlThongKe.Controls.Add(this.tabDashboard);
            this.tabControlThongKe.Controls.Add(this.tabTopSach);
            this.tabControlThongKe.Controls.Add(this.tabKhoSach);
            this.tabControlThongKe.Controls.Add(this.tabBlacklist);
            this.tabControlThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlThongKe.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControlThongKe.Location = new System.Drawing.Point(90, 0);
            this.tabControlThongKe.Name = "tabControlThongKe";
            this.tabControlThongKe.SelectedIndex = 0;
            this.tabControlThongKe.Size = new System.Drawing.Size(1110, 750);
            this.tabControlThongKe.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.tabControlThongKe.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tabControlThongKe.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabControlThongKe.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.tabControlThongKe.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tabControlThongKe.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.tabControlThongKe.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControlThongKe.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabControlThongKe.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.tabControlThongKe.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControlThongKe.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.tabControlThongKe.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tabControlThongKe.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabControlThongKe.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.tabControlThongKe.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tabControlThongKe.TabButtonSize = new System.Drawing.Size(0, 1);
            this.tabControlThongKe.TabIndex = 0;
            this.tabControlThongKe.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControlThongKe.TabMenuVisible = false;
            // 
            // tabDashboard
            // 
            this.tabDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.tabDashboard.Controls.Add(this.pnlCharts);
            this.tabDashboard.Controls.Add(this.pnlCards);
            this.tabDashboard.Controls.Add(this.pnlTopAction);
            this.tabDashboard.Location = new System.Drawing.Point(5, 4);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Padding = new System.Windows.Forms.Padding(15);
            this.tabDashboard.Size = new System.Drawing.Size(1101, 742);
            this.tabDashboard.TabIndex = 0;
            this.tabDashboard.Text = "Dashboard";
            // 
            // pnlCharts
            // 
            this.pnlCharts.Controls.Add(this.chartTrangThai);
            this.pnlCharts.Controls.Add(this.chartTopSach);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCharts.Location = new System.Drawing.Point(15, 205);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.pnlCharts.Size = new System.Drawing.Size(1071, 522);
            this.pnlCharts.TabIndex = 2;
            // 
            // chartTrangThai
            // 
            chartArea1.Name = "ChartArea1";
            this.chartTrangThai.ChartAreas.Add(chartArea1);
            this.chartTrangThai.Dock = System.Windows.Forms.DockStyle.Right;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.chartTrangThai.Legends.Add(legend1);
            this.chartTrangThai.Location = new System.Drawing.Point(546, 15);
            this.chartTrangThai.Name = "chartTrangThai";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartTrangThai.Series.Add(series1);
            this.chartTrangThai.Size = new System.Drawing.Size(525, 507);
            this.chartTrangThai.TabIndex = 1;
            // 
            // chartTopSach
            // 
            chartArea2.Name = "ChartArea1";
            this.chartTopSach.ChartAreas.Add(chartArea2);
            this.chartTopSach.Dock = System.Windows.Forms.DockStyle.Left;
            this.chartTopSach.Location = new System.Drawing.Point(0, 15);
            this.chartTopSach.Name = "chartTopSach";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            this.chartTopSach.Series.Add(series2);
            this.chartTopSach.Size = new System.Drawing.Size(992, 507);
            this.chartTopSach.TabIndex = 0;
            // 
            // pnlCards
            // 
            this.pnlCards.Controls.Add(this.card4);
            this.pnlCards.Controls.Add(this.card3);
            this.pnlCards.Controls.Add(this.card2);
            this.pnlCards.Controls.Add(this.card1);
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCards.Location = new System.Drawing.Point(15, 75);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Size = new System.Drawing.Size(1071, 130);
            this.pnlCards.TabIndex = 1;
            // 
            // card4
            // 
            this.card4.BackColor = System.Drawing.Color.Transparent;
            this.card4.BorderRadius = 10;
            this.card4.Controls.Add(this.lblTongPhat);
            this.card4.Controls.Add(this.lblTitle4);
            this.card4.FillColor = System.Drawing.Color.White;
            this.card4.Location = new System.Drawing.Point(1023, 10);
            this.card4.Name = "card4";
            this.card4.ShadowDecoration.Enabled = true;
            this.card4.Size = new System.Drawing.Size(260, 110);
            this.card4.TabIndex = 3;
            // 
            // lblTongPhat
            // 
            this.lblTongPhat.AutoSize = true;
            this.lblTongPhat.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTongPhat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.lblTongPhat.Location = new System.Drawing.Point(15, 50);
            this.lblTongPhat.Name = "lblTongPhat";
            this.lblTongPhat.Size = new System.Drawing.Size(40, 46);
            this.lblTongPhat.TabIndex = 0;
            this.lblTongPhat.Text = "0";
            // 
            // lblTitle4
            // 
            this.lblTitle4.AutoSize = true;
            this.lblTitle4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle4.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle4.Location = new System.Drawing.Point(15, 15);
            this.lblTitle4.Name = "lblTitle4";
            this.lblTitle4.Size = new System.Drawing.Size(223, 23);
            this.lblTitle4.TabIndex = 1;
            this.lblTitle4.Text = "DỰ KIẾN THU PHẠT (VNĐ)";
            // 
            // card3
            // 
            this.card3.BackColor = System.Drawing.Color.Transparent;
            this.card3.BorderRadius = 10;
            this.card3.Controls.Add(this.lblDangMuon);
            this.card3.Controls.Add(this.lblTitle3);
            this.card3.FillColor = System.Drawing.Color.White;
            this.card3.Location = new System.Drawing.Point(686, 10);
            this.card3.Name = "card3";
            this.card3.ShadowDecoration.Enabled = true;
            this.card3.Size = new System.Drawing.Size(260, 110);
            this.card3.TabIndex = 2;
            // 
            // lblDangMuon
            // 
            this.lblDangMuon.AutoSize = true;
            this.lblDangMuon.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblDangMuon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.lblDangMuon.Location = new System.Drawing.Point(15, 50);
            this.lblDangMuon.Name = "lblDangMuon";
            this.lblDangMuon.Size = new System.Drawing.Size(40, 46);
            this.lblDangMuon.TabIndex = 0;
            this.lblDangMuon.Text = "0";
            // 
            // lblTitle3
            // 
            this.lblTitle3.AutoSize = true;
            this.lblTitle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle3.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle3.Location = new System.Drawing.Point(15, 15);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(162, 23);
            this.lblTitle3.TabIndex = 1;
            this.lblTitle3.Text = "ĐANG CHO MƯỢN";
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.Transparent;
            this.card2.BorderRadius = 10;
            this.card2.Controls.Add(this.lblTongDocGia);
            this.card2.Controls.Add(this.lblTitle2);
            this.card2.FillColor = System.Drawing.Color.White;
            this.card2.Location = new System.Drawing.Point(350, 10);
            this.card2.Name = "card2";
            this.card2.ShadowDecoration.Enabled = true;
            this.card2.Size = new System.Drawing.Size(260, 110);
            this.card2.TabIndex = 1;
            // 
            // lblTongDocGia
            // 
            this.lblTongDocGia.AutoSize = true;
            this.lblTongDocGia.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTongDocGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblTongDocGia.Location = new System.Drawing.Point(15, 50);
            this.lblTongDocGia.Name = "lblTongDocGia";
            this.lblTongDocGia.Size = new System.Drawing.Size(40, 46);
            this.lblTongDocGia.TabIndex = 0;
            this.lblTongDocGia.Text = "0";
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle2.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle2.Location = new System.Drawing.Point(15, 15);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(134, 23);
            this.lblTitle2.TabIndex = 1;
            this.lblTitle2.Text = "TỔNG ĐỘC GIẢ";
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.Transparent;
            this.card1.BorderRadius = 10;
            this.card1.Controls.Add(this.lblTongSach);
            this.card1.Controls.Add(this.lblTitle1);
            this.card1.FillColor = System.Drawing.Color.White;
            this.card1.Location = new System.Drawing.Point(7, 10);
            this.card1.Name = "card1";
            this.card1.ShadowDecoration.Enabled = true;
            this.card1.Size = new System.Drawing.Size(260, 110);
            this.card1.TabIndex = 0;
            // 
            // lblTongSach
            // 
            this.lblTongSach.AutoSize = true;
            this.lblTongSach.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTongSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTongSach.Location = new System.Drawing.Point(15, 50);
            this.lblTongSach.Name = "lblTongSach";
            this.lblTongSach.Size = new System.Drawing.Size(40, 46);
            this.lblTongSach.TabIndex = 0;
            this.lblTongSach.Text = "0";
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle1.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle1.Location = new System.Drawing.Point(15, 15);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(137, 23);
            this.lblTitle1.TabIndex = 1;
            this.lblTitle1.Text = "TỔNG SỐ SÁCH";
            // 
            // pnlTopAction
            // 
            this.pnlTopAction.Controls.Add(this.btnXuatExcelAll);
            this.pnlTopAction.Controls.Add(this.lblMainTitle);
            this.pnlTopAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopAction.Location = new System.Drawing.Point(15, 15);
            this.pnlTopAction.Name = "pnlTopAction";
            this.pnlTopAction.Size = new System.Drawing.Size(1071, 60);
            this.pnlTopAction.TabIndex = 0;
            // 
            // btnXuatExcelAll
            // 
            this.btnXuatExcelAll.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnXuatExcelAll.BorderRadius = 5;
            this.btnXuatExcelAll.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXuatExcelAll.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcelAll.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcelAll.Location = new System.Drawing.Point(821, 5);
            this.btnXuatExcelAll.Name = "btnXuatExcelAll";
            this.btnXuatExcelAll.Size = new System.Drawing.Size(250, 45);
            this.btnXuatExcelAll.TabIndex = 1;
            this.btnXuatExcelAll.Text = "📥 Xuất Excel Toàn bộ";
            this.btnXuatExcelAll.Click += new System.EventHandler(this.btnXuatExcelAll_Click);
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.AutoSize = true;
            this.lblMainTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblMainTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.lblMainTitle.Location = new System.Drawing.Point(0, 10);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(357, 41);
            this.lblMainTitle.TabIndex = 0;
            this.lblMainTitle.Text = "DASHBOARD THƯ VIỆN";
            // 
            // tabTopSach
            // 
            this.tabTopSach.Controls.Add(this.dgvTopSach);
            this.tabTopSach.Location = new System.Drawing.Point(5, 4);
            this.tabTopSach.Name = "tabTopSach";
            this.tabTopSach.Padding = new System.Windows.Forms.Padding(15);
            this.tabTopSach.Size = new System.Drawing.Size(1101, 742);
            this.tabTopSach.TabIndex = 1;
            this.tabTopSach.Text = "Top Sách";
            this.tabTopSach.UseVisualStyleBackColor = true;
            // 
            // dgvTopSach
            // 
            this.dgvTopSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTopSach.Location = new System.Drawing.Point(15, 15);
            this.dgvTopSach.Name = "dgvTopSach";
            this.dgvTopSach.RowHeadersWidth = 51;
            this.dgvTopSach.Size = new System.Drawing.Size(1071, 712);
            this.dgvTopSach.TabIndex = 0;
            // 
            // tabKhoSach
            // 
            this.tabKhoSach.Controls.Add(this.dgvKhoSach);
            this.tabKhoSach.Location = new System.Drawing.Point(5, 4);
            this.tabKhoSach.Name = "tabKhoSach";
            this.tabKhoSach.Padding = new System.Windows.Forms.Padding(15);
            this.tabKhoSach.Size = new System.Drawing.Size(1101, 742);
            this.tabKhoSach.TabIndex = 2;
            this.tabKhoSach.Text = "Kho Sách";
            this.tabKhoSach.UseVisualStyleBackColor = true;
            // 
            // dgvKhoSach
            // 
            this.dgvKhoSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhoSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhoSach.Location = new System.Drawing.Point(15, 15);
            this.dgvKhoSach.Name = "dgvKhoSach";
            this.dgvKhoSach.RowHeadersWidth = 51;
            this.dgvKhoSach.Size = new System.Drawing.Size(1071, 712);
            this.dgvKhoSach.TabIndex = 0;
            // 
            // tabBlacklist
            // 
            this.tabBlacklist.Controls.Add(this.dgvBlacklist);
            this.tabBlacklist.Location = new System.Drawing.Point(5, 4);
            this.tabBlacklist.Name = "tabBlacklist";
            this.tabBlacklist.Padding = new System.Windows.Forms.Padding(15);
            this.tabBlacklist.Size = new System.Drawing.Size(1101, 742);
            this.tabBlacklist.TabIndex = 3;
            this.tabBlacklist.Text = "Blacklist";
            this.tabBlacklist.UseVisualStyleBackColor = true;
            // 
            // dgvBlacklist
            // 
            this.dgvBlacklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBlacklist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBlacklist.Location = new System.Drawing.Point(15, 15);
            this.dgvBlacklist.Name = "dgvBlacklist";
            this.dgvBlacklist.RowHeadersWidth = 51;
            this.dgvBlacklist.Size = new System.Drawing.Size(1071, 712);
            this.dgvBlacklist.TabIndex = 0;
            // 
            // FrmThongKeThuVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.tabControlThongKe);
            this.Controls.Add(this.pnlLeftWrapper);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmThongKeThuVien";
            this.Text = "Báo cáo Thống kê Thư viện";
            this.Load += new System.EventHandler(this.FrmThongKeThuVien_Load);
            this.pnlLeftWrapper.ResumeLayout(false);
            this.pnlLeftMenu.ResumeLayout(false);
            this.tabControlThongKe.ResumeLayout(false);
            this.tabDashboard.ResumeLayout(false);
            this.pnlCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopSach)).EndInit();
            this.pnlCards.ResumeLayout(false);
            this.card4.ResumeLayout(false);
            this.card4.PerformLayout();
            this.card3.ResumeLayout(false);
            this.card3.PerformLayout();
            this.card2.ResumeLayout(false);
            this.card2.PerformLayout();
            this.card1.ResumeLayout(false);
            this.card1.PerformLayout();
            this.pnlTopAction.ResumeLayout(false);
            this.pnlTopAction.PerformLayout();
            this.tabTopSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSach)).EndInit();
            this.tabKhoSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhoSach)).EndInit();
            this.tabBlacklist.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlacklist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeftWrapper;
        private Guna.UI2.WinForms.Guna2Panel pnlLeftMenu;
        private Guna.UI2.WinForms.Guna2Button btnMenuBlacklist;
        private Guna.UI2.WinForms.Guna2Button btnMenuKhoSach;
        private Guna.UI2.WinForms.Guna2Button btnMenuTopSach;
        private Guna.UI2.WinForms.Guna2Button btnMenuDashboard;
        private Guna.UI2.WinForms.Guna2TabControl tabControlThongKe;
        private System.Windows.Forms.TabPage tabDashboard;
        private System.Windows.Forms.Panel pnlCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTrangThai;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTopSach;
        private System.Windows.Forms.Panel pnlCards;
        private Guna.UI2.WinForms.Guna2Panel card4;
        private System.Windows.Forms.Label lblTongPhat;
        private System.Windows.Forms.Label lblTitle4;
        private Guna.UI2.WinForms.Guna2Panel card3;
        private System.Windows.Forms.Label lblDangMuon;
        private System.Windows.Forms.Label lblTitle3;
        private Guna.UI2.WinForms.Guna2Panel card2;
        private System.Windows.Forms.Label lblTongDocGia;
        private System.Windows.Forms.Label lblTitle2;
        private Guna.UI2.WinForms.Guna2Panel card1;
        private System.Windows.Forms.Label lblTongSach;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Panel pnlTopAction;
        private Guna.UI2.WinForms.Guna2Button btnXuatExcelAll;
        private System.Windows.Forms.Label lblMainTitle;
        private System.Windows.Forms.TabPage tabTopSach;
        private System.Windows.Forms.DataGridView dgvTopSach;
        private System.Windows.Forms.TabPage tabKhoSach;
        private System.Windows.Forms.DataGridView dgvKhoSach;
        private System.Windows.Forms.TabPage tabBlacklist;
        private System.Windows.Forms.DataGridView dgvBlacklist;
    }
}