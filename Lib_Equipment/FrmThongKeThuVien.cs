using Lib_Equipment.BLL;
using Lib_Equipment.Database;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lib_Equipment
{
    public partial class FrmThongKeThuVien : Form
    {
        public FrmThongKeThuVien()
        {
            InitializeComponent();
        }

        private void FrmThongKeThuVien_Load(object sender, EventArgs e)
        {
            FormatAllGrids();

            // 1. Tải Dashboard (Logic truy vấn SQL cũ của bạn vẫn chạy bình thường)
            LoadDashboardData();

            // =============== GỌI HÀM KHOÁC ÁO MỚI VÀO ĐÂY ===============
            ApplyModernChartStyle();
            // ==============================================================

            // 2. Tải dữ liệu vào Lưới để chuẩn bị Xuất Excel
            LoadDataToGrids();

            // Mặc định chọn tab Dashboard
            btnMenuDashboard.Checked = true;
        }

        // ========================================================
        // 0. SỰ KIỆN MENU ICON MỚI (BO GÓC LUXURY)
        // ========================================================
        private void btnMenuDashboard_CheckedChanged(object sender, EventArgs e) { if (btnMenuDashboard.Checked) tabControlThongKe.SelectedIndex = 0; }
        private void btnMenuTopSach_CheckedChanged(object sender, EventArgs e) { if (btnMenuTopSach.Checked) tabControlThongKe.SelectedIndex = 1; }
        private void btnMenuKhoSach_CheckedChanged(object sender, EventArgs e) { if (btnMenuKhoSach.Checked) tabControlThongKe.SelectedIndex = 2; }
        private void btnMenuBlacklist_CheckedChanged(object sender, EventArgs e) { if (btnMenuBlacklist.Checked) tabControlThongKe.SelectedIndex = 3; }

        // ========================================================
        // 1. DASHBOARD - THẺ SỐ LIỆU & BIỂU ĐỒ
        // ========================================================
        private void LoadDashboardData()
        {
            try
            {
                // 1.1. THẺ SỐ LIỆU TỔNG QUAN (Đã khớp 100% với CSDL SQL.sql)
                string sqlTongQuan = @"
                    DECLARE @TongSach INT, @TongDocGia INT, @DangMuon INT;
                    
                    -- Đếm tổng số bản sao sách vật lý
                    SELECT @TongSach = ISNULL(COUNT(*), 0) FROM BookCopy WHERE IsDeleted = 0 OR IsDeleted IS NULL; 
                    
                    -- Đếm tổng số độc giả
                    SELECT @TongDocGia = ISNULL(COUNT(*), 0) FROM Reader WHERE IsDeleted = 0 OR IsDeleted IS NULL;
                    
                    -- Đếm số sách đang được mượn (chưa trả)
                    SELECT @DangMuon = ISNULL(COUNT(*), 0) FROM BorrowDetail WHERE ReturnDate IS NULL;
                    
                    SELECT @TongSach AS TongSach, @TongDocGia AS TongDocGia, @DangMuon AS DangMuon;
                ";

                try
                {
                    DataTable dtTongQuan = DataProvider.Instance.ExecuteQuery(sqlTongQuan);
                    if (dtTongQuan.Rows.Count > 0)
                    {
                        lblTongSach.Text = dtTongQuan.Rows[0]["TongSach"].ToString();
                        lblTongDocGia.Text = dtTongQuan.Rows[0]["TongDocGia"].ToString();
                        lblDangMuon.Text = dtTongQuan.Rows[0]["DangMuon"].ToString();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi lấy thẻ số liệu: " + ex.Message); }

                // Tiền phạt lấy từ BLL
                decimal tongPhat = ThongKeThuVienBLL.Instance.TinhTongTienPhatDuKien();
                lblTongPhat.Text = tongPhat.ToString("N0") + " đ";

                // 1.2. BIỂU ĐỒ CỘT (TOP SÁCH)
                DataTable dtTopSach = ThongKeThuVienBLL.Instance.LayDuLieuTopSach();
                chartTopSach.Series.Clear();
                Series seriesCol = new Series("TopSach");
                seriesCol.ChartType = SeriesChartType.Column;
                seriesCol.IsValueShownAsLabel = true;
                seriesCol.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                for (int i = 0; i < dtTopSach.Rows.Count; i++)
                {
                    string tenSach = dtTopSach.Rows[i]["TenSach"].ToString();
                    if (tenSach.Length > 15) tenSach = tenSach.Substring(0, 15) + "..."; // Cắt ngắn tên nếu quá dài
                    int ptIdx = seriesCol.Points.AddXY(tenSach, Convert.ToDouble(dtTopSach.Rows[i]["LuotMuon"]));
                    seriesCol.Points[ptIdx].Color = GetColorByOrder(i);
                }
                chartTopSach.Series.Add(seriesCol);
                chartTopSach.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartTopSach.Titles.Clear();
                chartTopSach.Titles.Add(new Title("TOP 7 SÁCH MƯỢN NHIỀU NHẤT", Docking.Top, new Font("Segoe UI", 14, FontStyle.Bold), Color.FromArgb(26, 75, 132)));

                // 1.3. BIỂU ĐỒ TRÒN (TRẠNG THÁI KHO)
                DataTable dtTrangThai = ThongKeThuVienBLL.Instance.LayDuLieuTrangThaiKho();
                chartTrangThai.Series.Clear();
                Series seriesPie = new Series("Trạng thái");
                seriesPie.ChartType = SeriesChartType.Doughnut;
                seriesPie.IsValueShownAsLabel = true;
                seriesPie.LabelForeColor = Color.White;
                seriesPie.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                foreach (DataRow row in dtTrangThai.Rows)
                {
                    string status = row["TrangThai"].ToString();
                    int pt = seriesPie.Points.AddXY(status, Convert.ToInt32(row["SoLuong"]));
                    if (status == "Có sẵn") seriesPie.Points[pt].Color = Color.FromArgb(46, 204, 113);
                    else if (status == "Đang mượn") seriesPie.Points[pt].Color = Color.FromArgb(241, 196, 15);
                    else if (status == "Hỏng") seriesPie.Points[pt].Color = Color.FromArgb(230, 126, 34);
                    else seriesPie.Points[pt].Color = Color.FromArgb(231, 76, 60);
                }
                chartTrangThai.Series.Add(seriesPie);
                chartTrangThai.Titles.Clear();
                chartTrangThai.Titles.Add(new Title("TỶ LỆ KHO SÁCH", Docking.Top, new Font("Segoe UI", 14, FontStyle.Bold), Color.FromArgb(26, 75, 132)));
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load Dashboard: " + ex.Message); }
        }

        private Color GetColorByOrder(int i)
        {
            Color[] palette = { Color.FromArgb(52, 152, 219), Color.FromArgb(46, 204, 113), Color.FromArgb(155, 89, 182), Color.FromArgb(241, 196, 15), Color.FromArgb(231, 76, 60) };
            return palette[i % palette.Length];
        }

        // ========================================================
        // 2. CẤU HÌNH LƯỚI & ĐỔ DỮ LIỆU
        // ========================================================
        private void FormatAllGrids()
        {
            DataGridView[] grids = { dgvTopSach, dgvKhoSach, dgvBlacklist };
            foreach (var dgv in grids)
            {
                dgv.AllowUserToAddRows = false; dgv.ReadOnly = true;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.BackgroundColor = Color.White; dgv.BorderStyle = BorderStyle.None;
                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 75, 132);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                dgv.ColumnHeadersHeight = 45;
                dgv.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                dgv.RowTemplate.Height = 40;
            }

            // Cấu hình màu đỏ riêng cho Blacklist
            dgvBlacklist.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(192, 57, 43);
        }

        // ========================================================
        // HÀM MỚI ĐƯỢC THÊM VÀO: TỰ ĐỘNG VẼ HEADER CHO BẢNG
        // ========================================================
        private void AddHeaderAboveGrid(DataGridView dgv, string titleText, string panelName, Color? titleColor = null)
        {
            if (dgv.Parent != null && !dgv.Parent.Controls.ContainsKey(panelName))
            {
                Guna.UI2.WinForms.Guna2Panel pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
                pnlHeader.Name = panelName;
                pnlHeader.Height = 60;
                pnlHeader.Dock = DockStyle.Top;
                pnlHeader.FillColor = Color.FromArgb(240, 248, 255);
                pnlHeader.CustomBorderColor = Color.LightGray;
                pnlHeader.CustomBorderThickness = new Padding(0, 0, 0, 2);

                Label lblTitle = new Label();
                lblTitle.Text = titleText;
                lblTitle.Font = new Font("Segoe UI", 15, FontStyle.Bold);
                lblTitle.ForeColor = titleColor ?? Color.FromArgb(0, 51, 102);
                lblTitle.Dock = DockStyle.Fill;
                lblTitle.TextAlign = ContentAlignment.MiddleCenter;
                lblTitle.BackColor = Color.Transparent;

                pnlHeader.Controls.Add(lblTitle);
                dgv.Parent.Controls.Add(pnlHeader);
                pnlHeader.SendToBack();
            }
        }

        private void LoadDataToGrids()
        {
            try
            {
                // Gọi hàm vẽ Tiêu đề cho 3 bảng vừa mới thêm
                AddHeaderAboveGrid(dgvTopSach, "TOP 10 SÁCH MƯỢN NHIỀU NHẤT", "HeaderTopSach");
                AddHeaderAboveGrid(dgvKhoSach, "TÌNH TRẠNG KHO SÁCH", "HeaderKhoSach");
                AddHeaderAboveGrid(dgvBlacklist, "DANH SÁCH ĐEN ĐỘC GIẢ NỢ PHẠT", "HeaderBlacklist", Color.FromArgb(192, 57, 43));

                // Dùng chung dữ liệu từ BLL để đổ vào Grid
                dgvTopSach.DataSource = ThongKeThuVienBLL.Instance.LayDuLieuTopSach();
                dgvKhoSach.DataSource = ThongKeThuVienBLL.Instance.LayDuLieuTrangThaiKho();

                DataTable dtBlacklist = ThongKeThuVienBLL.Instance.LayDuLieuDanhSachDen();
                dgvBlacklist.DataSource = dtBlacklist;

                if (dgvBlacklist.Columns.Contains("Tiền phạt dự kiến (VNĐ)"))
                {
                    dgvBlacklist.Columns["Tiền phạt dự kiến (VNĐ)"].DefaultCellStyle.Format = "N0";
                    dgvBlacklist.Columns["Tiền phạt dự kiến (VNĐ)"].DefaultCellStyle.ForeColor = Color.Red;
                    dgvBlacklist.Columns["Tiền phạt dự kiến (VNĐ)"].DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu lưới: " + ex.Message); }
        }

        // ========================================================
        // 3. XUẤT EXCEL CHỐNG LỖI TUYỆT ĐỐI 100% (DÙNG XML SPREADSHEET NATIVE)
        // Không dùng Interop, bỏ qua mọi lỗi của MS Office
        // ========================================================
        private void btnXuatExcelAll_Click(object sender, EventArgs e)
        {
            if (dgvTopSach.Rows.Count == 0 && dgvKhoSach.Rows.Count == 0 && dgvBlacklist.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu để xuất báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel File (*.xls)|*.xls";
            sfd.Title = "Lưu báo cáo Thống kê Thư viện";
            sfd.FileName = "BaoCao_ThuVien_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + ".xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Tự tay Code C# biên dịch ra cấu trúc lõi của file Excel
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                    {
                        sw.WriteLine("<?xml version=\"1.0\"?>");
                        sw.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
                        sw.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
                        sw.WriteLine(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
                        sw.WriteLine(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
                        sw.WriteLine(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">");

                        // ĐỊNH NGHĨA MÀU SẮC (STYLES)
                        sw.WriteLine(" <Styles>");
                        sw.WriteLine("  <Style ss:ID=\"Title\">");
                        sw.WriteLine("   <Font ss:Bold=\"1\" ss:Size=\"16\" ss:Color=\"#1A4B84\"/>");
                        sw.WriteLine("   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\"/>");
                        sw.WriteLine("  </Style>");

                        sw.WriteLine("  <Style ss:ID=\"HeaderBlue\">");
                        sw.WriteLine("   <Font ss:Bold=\"1\" ss:Color=\"#FFFFFF\"/>");
                        sw.WriteLine("   <Interior ss:Color=\"#1A4B84\" ss:Pattern=\"Solid\"/>");
                        sw.WriteLine("   <Borders><Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/></Borders>");
                        sw.WriteLine("  </Style>");

                        sw.WriteLine("  <Style ss:ID=\"HeaderRed\">");
                        sw.WriteLine("   <Font ss:Bold=\"1\" ss:Color=\"#FFFFFF\"/>");
                        sw.WriteLine("   <Interior ss:Color=\"#C0392B\" ss:Pattern=\"Solid\"/>");
                        sw.WriteLine("   <Borders><Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/></Borders>");
                        sw.WriteLine("  </Style>");

                        sw.WriteLine("  <Style ss:ID=\"NormalCell\">");
                        sw.WriteLine("   <Borders><Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/></Borders>");
                        sw.WriteLine("  </Style>");
                        sw.WriteLine(" </Styles>");

                        // TẠO 3 SHEET BẰNG CODE
                        WriteGridToXml(sw, dgvTopSach, "Top_Sach", "TOP 10 SÁCH MƯỢN NHIỀU NHẤT", "HeaderBlue");
                        WriteGridToXml(sw, dgvKhoSach, "Kho_Sach", "TÌNH TRẠNG KHO SÁCH", "HeaderBlue");
                        WriteGridToXml(sw, dgvBlacklist, "DanhSach_Den", "DANH SÁCH ĐEN ĐỘC GIẢ NỢ", "HeaderRed");

                        sw.WriteLine("</Workbook>");
                    }

                    if (MessageBox.Show("Xuất báo cáo thành công tuyệt đối!\nBạn có muốn mở file ngay bây giờ không?", "Hoàn tất", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi ghi file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // HÀM HỖ TRỢ BIÊN DỊCH LƯỚI THÀNH XML
        private void WriteGridToXml(StreamWriter sw, DataGridView dgv, string sheetName, string title, string headerStyle)
        {
            if (dgv.Rows.Count == 0) return;

            sw.WriteLine($" <Worksheet ss:Name=\"{sheetName}\">");
            sw.WriteLine("  <Table>");

            // Thiết lập độ rộng cột
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                sw.WriteLine("   <Column ss:AutoFitWidth=\"1\" ss:Width=\"150\"/>");
            }

            // Dòng 1
            sw.WriteLine("   <Row>");
            sw.WriteLine("    <Cell><Data ss:Type=\"String\">TRƯỜNG ĐH KINH TẾ - KỸ THUẬT CÔNG NGHIỆP</Data></Cell>");
            sw.WriteLine("   </Row>");
            sw.WriteLine("   <Row></Row>");

            // Dòng 3 (Tiêu đề)
            sw.WriteLine("   <Row ss:Height=\"25\">");
            sw.WriteLine($"    <Cell ss:MergeAcross=\"{dgv.Columns.Count - 1}\" ss:StyleID=\"Title\"><Data ss:Type=\"String\">{title}</Data></Cell>");
            sw.WriteLine("   </Row>");
            sw.WriteLine("   <Row></Row>");

            // Cột Header
            sw.WriteLine("   <Row ss:Height=\"20\">");
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                string headerText = dgv.Columns[i].HeaderText.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
                sw.WriteLine($"    <Cell ss:StyleID=\"{headerStyle}\"><Data ss:Type=\"String\">{headerText}</Data></Cell>");
            }
            sw.WriteLine("   </Row>");

            // Data
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                sw.WriteLine("   <Row>");
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    string val = dgv.Rows[i].Cells[j].Value?.ToString() ?? "";
                    val = val.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;"); // Lọc ký tự đặc biệt chống lỗi
                    sw.WriteLine($"    <Cell ss:StyleID=\"NormalCell\"><Data ss:Type=\"String\">{val}</Data></Cell>");
                }
                sw.WriteLine("   </Row>");
            }

            sw.WriteLine("  </Table>");
            sw.WriteLine(" </Worksheet>");
        }

        private void btnMenuTopSach_Click(object sender, EventArgs e)
        {

        }
        private void ApplyModernChartStyle()
        {
            // Màu nền sáng chuẩn Dashboard thanh lịch
            Color lightBg = Color.White;
            Color darkText = Color.FromArgb(64, 64, 64); // Chữ màu xám đậm để không bị chói mắt
            Color gridColor = Color.FromArgb(230, 230, 230); // Lưới kẻ sọc màu xám nhạt

            // Đổi màu nền của Panel chứa biểu đồ cho đồng bộ
            if (pnlCharts != null) pnlCharts.BackColor = lightBg;

            // =========================================================
            // 1. CHART TOP SÁCH (THANH NGANG - MÀU SÁNG)
            // =========================================================
            chartTopSach.BackColor = lightBg;
            chartTopSach.ChartAreas[0].BackColor = lightBg;
            chartTopSach.ChartAreas[0].BorderWidth = 0;

            // Tùy chỉnh chữ và tắt lưới dọc
            chartTopSach.ChartAreas[0].AxisX.LabelStyle.ForeColor = darkText;
            chartTopSach.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            chartTopSach.ChartAreas[0].AxisX.LineColor = gridColor;
            chartTopSach.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTopSach.ChartAreas[0].AxisX.Interval = 1; // Hiện đủ tên 5 cuốn sách

            // Tùy chỉnh chữ và biến lưới ngang thành nét đứt mờ
            chartTopSach.ChartAreas[0].AxisY.LabelStyle.ForeColor = darkText;
            chartTopSach.ChartAreas[0].AxisY.LineColor = gridColor;
            chartTopSach.ChartAreas[0].AxisY.MajorGrid.LineColor = gridColor;
            chartTopSach.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            if (chartTopSach.Legends.Count > 0)
            {
                chartTopSach.Legends[0].BackColor = lightBg;
                chartTopSach.Legends[0].ForeColor = darkText;
            }

            if (chartTopSach.Series.Count > 0)
            {
                var s1 = chartTopSach.Series[0];
                s1.ChartType = SeriesChartType.Bar; // Biểu đồ thanh ngang
                s1.Color = Color.FromArgb(142, 124, 230); // Tím pastel
                s1.BackGradientStyle = GradientStyle.LeftRight; // Gradient đổ từ trái sang phải
                s1.BackSecondaryColor = Color.FromArgb(219, 112, 219); // Đuổi sang màu Hồng

                // Chữ số trên cột màu đen cho dễ nhìn trên nền sáng
                s1.LabelForeColor = Color.Black;
                s1.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                s1.IsValueShownAsLabel = true;
            }

            // =========================================================
            // 2. CHART TRẠNG THÁI (SÓNG GRADIENT - MÀU SÁNG)
            // =========================================================
            chartTrangThai.BackColor = lightBg;
            chartTrangThai.ChartAreas[0].BackColor = lightBg;
            chartTrangThai.ChartAreas[0].BorderWidth = 0;

            chartTrangThai.ChartAreas[0].AxisX.LabelStyle.ForeColor = darkText;
            chartTrangThai.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            chartTrangThai.ChartAreas[0].AxisX.LineColor = gridColor;
            chartTrangThai.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTrangThai.ChartAreas[0].AxisX.Interval = 1;

            chartTrangThai.ChartAreas[0].AxisY.LabelStyle.ForeColor = darkText;
            chartTrangThai.ChartAreas[0].AxisY.LineColor = gridColor;
            chartTrangThai.ChartAreas[0].AxisY.MajorGrid.LineColor = gridColor;
            chartTrangThai.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chartTrangThai.ChartAreas[0].AxisY.Minimum = 0;

            if (chartTrangThai.Legends.Count > 0)
            {
                chartTrangThai.Legends[0].BackColor = lightBg;
                chartTrangThai.Legends[0].ForeColor = darkText;
                chartTrangThai.Legends[0].Docking = Docking.Top;
            }

            if (chartTrangThai.Series.Count > 0)
            {
                var s2 = chartTrangThai.Series[0];
                s2.ChartType = SeriesChartType.SplineArea; // Biểu đồ sóng ngầm

                // Nền xanh Cyan trong suốt đậm hơn chút để nổi trên nền trắng
                s2.Color = Color.FromArgb(120, 0, 190, 255);
                s2.BackGradientStyle = GradientStyle.TopBottom;
                s2.BackSecondaryColor = Color.Transparent;

                // Đường line biên sắc nét hơn
                s2.BorderColor = Color.FromArgb(0, 150, 220);
                s2.BorderWidth = 3;

                // Chấm tròn tại các đỉnh điểm
                s2.MarkerStyle = MarkerStyle.Circle;
                s2.MarkerSize = 8;
                s2.MarkerColor = Color.White;
                s2.MarkerBorderColor = Color.FromArgb(0, 150, 220);
                s2.MarkerBorderWidth = 2;

                s2.LabelForeColor = darkText;
                s2.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                s2.IsValueShownAsLabel = true;
            }
        }
    }
}