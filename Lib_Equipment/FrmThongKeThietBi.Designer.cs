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
            this.btnMenuBaoTri = new Guna.UI2.WinForms.Guna2Button();
            this.btnMenuPhanBo = new Guna.UI2.WinForms.Guna2Button();
            this.btnMenuTinhTrang = new Guna.UI2.WinForms.Guna2Button();
            this.btnMenuDashboard = new Guna.UI2.WinForms.Guna2Button();
            this.tabControlThongKe = new Guna.UI2.WinForms.Guna2TabControl();
            this.tabDashboard = new System.Windows.Forms.TabPage();
            this.pnlCharts = new System.Windows.Forms.Panel();
            this.chartTinhTrang = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartKhoa = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlCards = new System.Windows.Forms.Panel();
            this.card4 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTongThanhLy = new System.Windows.Forms.Label();
            this.lblTitle4 = new System.Windows.Forms.Label();
            this.card3 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTongBaoTri = new System.Windows.Forms.Label();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.card2 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTongGiaTri = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.card1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTongThietBi = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.pnlTopAction = new System.Windows.Forms.Panel();
            this.btnXuatExcelAll = new Guna.UI2.WinForms.Guna2Button();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.tabTinhTrang = new System.Windows.Forms.TabPage();
            this.dgvTinhTrang = new System.Windows.Forms.DataGridView();
            this.tabPhanBo = new System.Windows.Forms.TabPage();
            this.dgvPhanBo = new System.Windows.Forms.DataGridView();
            this.tabBaoTri = new System.Windows.Forms.TabPage();
            this.dgvBaoTri = new System.Windows.Forms.DataGridView();
            this.pnlLeftWrapper.SuspendLayout();
            this.pnlLeftMenu.SuspendLayout();
            this.tabControlThongKe.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            this.pnlCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTinhTrang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartKhoa)).BeginInit();
            this.pnlCards.SuspendLayout();
            this.card4.SuspendLayout();
            this.card3.SuspendLayout();
            this.card2.SuspendLayout();
            this.card1.SuspendLayout();
            this.pnlTopAction.SuspendLayout();
            this.tabTinhTrang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTinhTrang)).BeginInit();
            this.tabPhanBo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanBo)).BeginInit();
            this.tabBaoTri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoTri)).BeginInit();
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
            this.pnlLeftMenu.Controls.Add(this.btnMenuBaoTri);
            this.pnlLeftMenu.Controls.Add(this.btnMenuPhanBo);
            this.pnlLeftMenu.Controls.Add(this.btnMenuTinhTrang);
            this.pnlLeftMenu.Controls.Add(this.btnMenuDashboard);
            this.pnlLeftMenu.FillColor = System.Drawing.Color.White;
            this.pnlLeftMenu.Location = new System.Drawing.Point(15, 15);
            this.pnlLeftMenu.Name = "pnlLeftMenu";
            this.pnlLeftMenu.ShadowDecoration.Enabled = true;
            this.pnlLeftMenu.Size = new System.Drawing.Size(60, 240);
            this.pnlLeftMenu.TabIndex = 0;
            // 
            // btnMenuBaoTri
            // 
            this.btnMenuBaoTri.BorderRadius = 15;
            this.btnMenuBaoTri.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnMenuBaoTri.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnMenuBaoTri.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnMenuBaoTri.CustomizableEdges.TopLeft = false;
            this.btnMenuBaoTri.CustomizableEdges.TopRight = false;
            this.btnMenuBaoTri.FillColor = System.Drawing.Color.Transparent;
            this.btnMenuBaoTri.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.btnMenuBaoTri.ForeColor = System.Drawing.Color.Gray;
            this.btnMenuBaoTri.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnMenuBaoTri.Location = new System.Drawing.Point(0, 180);
            this.btnMenuBaoTri.Name = "btnMenuBaoTri";
            this.btnMenuBaoTri.Size = new System.Drawing.Size(60, 60);
            this.btnMenuBaoTri.TabIndex = 3;
            this.btnMenuBaoTri.Text = "💰";
            this.btnMenuBaoTri.UseTransparentBackground = true;
            this.btnMenuBaoTri.CheckedChanged += new System.EventHandler(this.btnMenuBaoTri_CheckedChanged);
            // 
            // btnMenuPhanBo
            // 
            this.btnMenuPhanBo.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnMenuPhanBo.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnMenuPhanBo.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnMenuPhanBo.FillColor = System.Drawing.Color.Transparent;
            this.btnMenuPhanBo.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.btnMenuPhanBo.ForeColor = System.Drawing.Color.Gray;
            this.btnMenuPhanBo.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnMenuPhanBo.Location = new System.Drawing.Point(0, 120);
            this.btnMenuPhanBo.Name = "btnMenuPhanBo";
            this.btnMenuPhanBo.Size = new System.Drawing.Size(60, 60);
            this.btnMenuPhanBo.TabIndex = 2;
            this.btnMenuPhanBo.Text = "🏢";
            this.btnMenuPhanBo.UseTransparentBackground = true;
            this.btnMenuPhanBo.CheckedChanged += new System.EventHandler(this.btnMenuPhanBo_CheckedChanged);
            // 
            // btnMenuTinhTrang
            // 
            this.btnMenuTinhTrang.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnMenuTinhTrang.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(75)))), ((int)(((byte)(132)))));
            this.btnMenuTinhTrang.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnMenuTinhTrang.FillColor = System.Drawing.Color.Transparent;
            this.btnMenuTinhTrang.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.btnMenuTinhTrang.ForeColor = System.Drawing.Color.Gray;
            this.btnMenuTinhTrang.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnMenuTinhTrang.Location = new System.Drawing.Point(0, 60);
            this.btnMenuTinhTrang.Name = "btnMenuTinhTrang";
            this.btnMenuTinhTrang.Size = new System.Drawing.Size(60, 60);
            this.btnMenuTinhTrang.TabIndex = 1;
            this.btnMenuTinhTrang.Text = "📋";
            this.btnMenuTinhTrang.UseTransparentBackground = true;
            this.btnMenuTinhTrang.CheckedChanged += new System.EventHandler(this.btnMenuTinhTrang_CheckedChanged);
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
            this.tabControlThongKe.Controls.Add(this.tabTinhTrang);
            this.tabControlThongKe.Controls.Add(this.tabPhanBo);
            this.tabControlThongKe.Controls.Add(this.tabBaoTri);
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
            this.pnlCharts.Controls.Add(this.chartTinhTrang);
            this.pnlCharts.Controls.Add(this.chartKhoa);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCharts.Location = new System.Drawing.Point(15, 201);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.pnlCharts.Size = new System.Drawing.Size(1071, 526);
            this.pnlCharts.TabIndex = 2;
            // 
            // chartTinhTrang
            // 
            chartArea1.Name = "ChartArea1";
            this.chartTinhTrang.ChartAreas.Add(chartArea1);
            this.chartTinhTrang.Dock = System.Windows.Forms.DockStyle.Right;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.chartTinhTrang.Legends.Add(legend1);
            this.chartTinhTrang.Location = new System.Drawing.Point(671, 15);
            this.chartTinhTrang.Name = "chartTinhTrang";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartTinhTrang.Series.Add(series1);
            this.chartTinhTrang.Size = new System.Drawing.Size(400, 511);
            this.chartTinhTrang.TabIndex = 1;
            this.chartTinhTrang.Text = "chartTinhTrang";
            // 
            // chartKhoa
            // 
            chartArea2.Name = "ChartArea1";
            this.chartKhoa.ChartAreas.Add(chartArea2);
            this.chartKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartKhoa.Location = new System.Drawing.Point(0, 15);
            this.chartKhoa.Name = "chartKhoa";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            this.chartKhoa.Series.Add(series2);
            this.chartKhoa.Size = new System.Drawing.Size(1071, 511);
            this.chartKhoa.TabIndex = 0;
            this.chartKhoa.Text = "chartKhoa";
            // 
            // pnlCards
            // 
            this.pnlCards.Controls.Add(this.card4);
            this.pnlCards.Controls.Add(this.card3);
            this.pnlCards.Controls.Add(this.card2);
            this.pnlCards.Controls.Add(this.card1);
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCards.Location = new System.Drawing.Point(15, 71);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Size = new System.Drawing.Size(1071, 130);
            this.pnlCards.TabIndex = 1;
            // 
            // card4
            // 
            this.card4.BackColor = System.Drawing.Color.Transparent;
            this.card4.BorderRadius = 10;
            this.card4.Controls.Add(this.lblTongThanhLy);
            this.card4.Controls.Add(this.lblTitle4);
            this.card4.FillColor = System.Drawing.Color.White;
            this.card4.Location = new System.Drawing.Point(1180, 10);
            this.card4.Name = "card4";
            this.card4.ShadowDecoration.Enabled = true;
            this.card4.Size = new System.Drawing.Size(330, 110);
            this.card4.TabIndex = 3;
            // 
            // lblTongThanhLy
            // 
            this.lblTongThanhLy.AutoSize = true;
            this.lblTongThanhLy.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTongThanhLy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblTongThanhLy.Location = new System.Drawing.Point(15, 50);
            this.lblTongThanhLy.Name = "lblTongThanhLy";
            this.lblTongThanhLy.Size = new System.Drawing.Size(40, 46);
            this.lblTongThanhLy.TabIndex = 0;
            this.lblTongThanhLy.Text = "0";
            // 
            // lblTitle4
            // 
            this.lblTitle4.AutoSize = true;
            this.lblTitle4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle4.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle4.Location = new System.Drawing.Point(15, 15);
            this.lblTitle4.Name = "lblTitle4";
            this.lblTitle4.Size = new System.Drawing.Size(224, 23);
            this.lblTitle4.TabIndex = 1;
            this.lblTitle4.Text = "THU HỒI THANH LÝ (VNĐ)";
            // 
            // card3
            // 
            this.card3.BackColor = System.Drawing.Color.Transparent;
            this.card3.BorderRadius = 10;
            this.card3.Controls.Add(this.lblTongBaoTri);
            this.card3.Controls.Add(this.lblTitle3);
            this.card3.FillColor = System.Drawing.Color.White;
            this.card3.Location = new System.Drawing.Point(790, 10);
            this.card3.Name = "card3";
            this.card3.ShadowDecoration.Enabled = true;
            this.card3.Size = new System.Drawing.Size(330, 110);
            this.card3.TabIndex = 2;
            // 
            // lblTongBaoTri
            // 
            this.lblTongBaoTri.AutoSize = true;
            this.lblTongBaoTri.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTongBaoTri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.lblTongBaoTri.Location = new System.Drawing.Point(15, 50);
            this.lblTongBaoTri.Name = "lblTongBaoTri";
            this.lblTongBaoTri.Size = new System.Drawing.Size(40, 46);
            this.lblTongBaoTri.TabIndex = 0;
            this.lblTongBaoTri.Text = "0";
            // 
            // lblTitle3
            // 
            this.lblTitle3.AutoSize = true;
            this.lblTitle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle3.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle3.Location = new System.Drawing.Point(15, 15);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(217, 23);
            this.lblTitle3.TabIndex = 1;
            this.lblTitle3.Text = "TỔNG PHÍ BẢO TRÌ (VNĐ)";
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.Transparent;
            this.card2.BorderRadius = 10;
            this.card2.Controls.Add(this.lblTongGiaTri);
            this.card2.Controls.Add(this.lblTitle2);
            this.card2.FillColor = System.Drawing.Color.White;
            this.card2.Location = new System.Drawing.Point(400, 10);
            this.card2.Name = "card2";
            this.card2.ShadowDecoration.Enabled = true;
            this.card2.Size = new System.Drawing.Size(330, 110);
            this.card2.TabIndex = 1;
            // 
            // lblTongGiaTri
            // 
            this.lblTongGiaTri.AutoSize = true;
            this.lblTongGiaTri.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTongGiaTri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblTongGiaTri.Location = new System.Drawing.Point(15, 50);
            this.lblTongGiaTri.Name = "lblTongGiaTri";
            this.lblTongGiaTri.Size = new System.Drawing.Size(40, 46);
            this.lblTongGiaTri.TabIndex = 0;
            this.lblTongGiaTri.Text = "0";
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle2.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle2.Location = new System.Drawing.Point(15, 15);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(183, 23);
            this.lblTitle2.TabIndex = 1;
            this.lblTitle2.Text = "TỔNG TÀI SẢN (VNĐ)";
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.Transparent;
            this.card1.BorderRadius = 10;
            this.card1.Controls.Add(this.lblTongThietBi);
            this.card1.Controls.Add(this.lblTitle1);
            this.card1.FillColor = System.Drawing.Color.White;
            this.card1.Location = new System.Drawing.Point(10, 10);
            this.card1.Name = "card1";
            this.card1.ShadowDecoration.Enabled = true;
            this.card1.Size = new System.Drawing.Size(330, 110);
            this.card1.TabIndex = 0;
            // 
            // lblTongThietBi
            // 
            this.lblTongThietBi.AutoSize = true;
            this.lblTongThietBi.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTongThietBi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTongThietBi.Location = new System.Drawing.Point(15, 50);
            this.lblTongThietBi.Name = "lblTongThietBi";
            this.lblTongThietBi.Size = new System.Drawing.Size(40, 46);
            this.lblTongThietBi.TabIndex = 0;
            this.lblTongThietBi.Text = "0";
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle1.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle1.Location = new System.Drawing.Point(15, 15);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(159, 23);
            this.lblTitle1.TabIndex = 1;
            this.lblTitle1.Text = "TỔNG SỐ THIẾT BỊ";
            // 
            // pnlTopAction
            // 
            this.pnlTopAction.Controls.Add(this.btnXuatExcelAll);
            this.pnlTopAction.Controls.Add(this.lblMainTitle);
            this.pnlTopAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopAction.Location = new System.Drawing.Point(15, 15);
            this.pnlTopAction.Name = "pnlTopAction";
            this.pnlTopAction.Size = new System.Drawing.Size(1071, 56);
            this.pnlTopAction.TabIndex = 0;
            // 
            // btnXuatExcelAll
            // 
            this.btnXuatExcelAll.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnXuatExcelAll.BorderRadius = 5;
            this.btnXuatExcelAll.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXuatExcelAll.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcelAll.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcelAll.Location = new System.Drawing.Point(813, 6);
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
            this.lblMainTitle.Location = new System.Drawing.Point(3, 9);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(336, 41);
            this.lblMainTitle.TabIndex = 0;
            this.lblMainTitle.Text = "DASHBOARD THIẾT BỊ";
            // 
            // tabTinhTrang
            // 
            this.tabTinhTrang.Controls.Add(this.dgvTinhTrang);
            this.tabTinhTrang.Location = new System.Drawing.Point(5, 4);
            this.tabTinhTrang.Name = "tabTinhTrang";
            this.tabTinhTrang.Padding = new System.Windows.Forms.Padding(15);
            this.tabTinhTrang.Size = new System.Drawing.Size(1101, 742);
            this.tabTinhTrang.TabIndex = 1;
            this.tabTinhTrang.Text = "Tình trạng";
            this.tabTinhTrang.UseVisualStyleBackColor = true;
            // 
            // dgvTinhTrang
            // 
            this.dgvTinhTrang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTinhTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTinhTrang.Location = new System.Drawing.Point(15, 15);
            this.dgvTinhTrang.Name = "dgvTinhTrang";
            this.dgvTinhTrang.RowHeadersWidth = 51;
            this.dgvTinhTrang.Size = new System.Drawing.Size(1071, 712);
            this.dgvTinhTrang.TabIndex = 1;
            // 
            // tabPhanBo
            // 
            this.tabPhanBo.Controls.Add(this.dgvPhanBo);
            this.tabPhanBo.Location = new System.Drawing.Point(5, 4);
            this.tabPhanBo.Name = "tabPhanBo";
            this.tabPhanBo.Padding = new System.Windows.Forms.Padding(15);
            this.tabPhanBo.Size = new System.Drawing.Size(1101, 742);
            this.tabPhanBo.TabIndex = 2;
            this.tabPhanBo.Text = "Phân bổ";
            this.tabPhanBo.UseVisualStyleBackColor = true;
            // 
            // dgvPhanBo
            // 
            this.dgvPhanBo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhanBo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhanBo.Location = new System.Drawing.Point(15, 15);
            this.dgvPhanBo.Name = "dgvPhanBo";
            this.dgvPhanBo.RowHeadersWidth = 51;
            this.dgvPhanBo.Size = new System.Drawing.Size(1071, 712);
            this.dgvPhanBo.TabIndex = 2;
            // 
            // tabBaoTri
            // 
            this.tabBaoTri.Controls.Add(this.dgvBaoTri);
            this.tabBaoTri.Location = new System.Drawing.Point(5, 4);
            this.tabBaoTri.Name = "tabBaoTri";
            this.tabBaoTri.Padding = new System.Windows.Forms.Padding(15);
            this.tabBaoTri.Size = new System.Drawing.Size(1101, 742);
            this.tabBaoTri.TabIndex = 3;
            this.tabBaoTri.Text = "Bảo trì";
            this.tabBaoTri.UseVisualStyleBackColor = true;
            // 
            // dgvBaoTri
            // 
            this.dgvBaoTri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoTri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBaoTri.Location = new System.Drawing.Point(15, 15);
            this.dgvBaoTri.Name = "dgvBaoTri";
            this.dgvBaoTri.RowHeadersWidth = 51;
            this.dgvBaoTri.Size = new System.Drawing.Size(1071, 712);
            this.dgvBaoTri.TabIndex = 2;
            // 
            // FrmThongKeThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.tabControlThongKe);
            this.Controls.Add(this.pnlLeftWrapper);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmThongKeThietBi";
            this.Text = "Báo cáo Thống kê Thiết bị";
            this.Load += new System.EventHandler(this.FrmThongKeThietBi_Load);
            this.pnlLeftWrapper.ResumeLayout(false);
            this.pnlLeftMenu.ResumeLayout(false);
            this.tabControlThongKe.ResumeLayout(false);
            this.tabDashboard.ResumeLayout(false);
            this.pnlCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTinhTrang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartKhoa)).EndInit();
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
            this.tabTinhTrang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTinhTrang)).EndInit();
            this.tabPhanBo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanBo)).EndInit();
            this.tabBaoTri.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoTri)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeftWrapper;
        private Guna.UI2.WinForms.Guna2Panel pnlLeftMenu;
        private Guna.UI2.WinForms.Guna2Button btnMenuDashboard;
        private Guna.UI2.WinForms.Guna2Button btnMenuTinhTrang;
        private Guna.UI2.WinForms.Guna2Button btnMenuPhanBo;
        private Guna.UI2.WinForms.Guna2Button btnMenuBaoTri;
        private Guna.UI2.WinForms.Guna2TabControl tabControlThongKe;
        private System.Windows.Forms.TabPage tabDashboard;
        private System.Windows.Forms.TabPage tabTinhTrang;
        private System.Windows.Forms.TabPage tabPhanBo;
        private System.Windows.Forms.TabPage tabBaoTri;
        private System.Windows.Forms.Panel pnlTopAction;
        private System.Windows.Forms.Label lblMainTitle;
        private Guna.UI2.WinForms.Guna2Button btnXuatExcelAll;
        private System.Windows.Forms.Panel pnlCards;
        private Guna.UI2.WinForms.Guna2Panel card4;
        private System.Windows.Forms.Label lblTongThanhLy;
        private System.Windows.Forms.Label lblTitle4;
        private Guna.UI2.WinForms.Guna2Panel card3;
        private System.Windows.Forms.Label lblTongBaoTri;
        private System.Windows.Forms.Label lblTitle3;
        private Guna.UI2.WinForms.Guna2Panel card2;
        private System.Windows.Forms.Label lblTongGiaTri;
        private System.Windows.Forms.Label lblTitle2;
        private Guna.UI2.WinForms.Guna2Panel card1;
        private System.Windows.Forms.Label lblTongThietBi;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Panel pnlCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTinhTrang;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartKhoa;
        private System.Windows.Forms.DataGridView dgvTinhTrang;
        private System.Windows.Forms.DataGridView dgvPhanBo;
        private System.Windows.Forms.DataGridView dgvBaoTri;
    }
}