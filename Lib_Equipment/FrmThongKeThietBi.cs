using Lib_Equipment.Database;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lib_Equipment
{
    public partial class FrmThongKeThietBi : Form
    {
        public FrmThongKeThietBi()
        {
            InitializeComponent();
        }

        private void FrmThongKeThietBi_Load(object sender, EventArgs e)
        {
            FormatAllGrids();
            LoadDashboardData();
            LoadDataTab_TinhTrang();
            LoadDataTab_PhanBo();
            LoadDataTab_BaoTri();

            // Mặc định chọn tab đầu tiên
            btnMenuDashboard.Checked = true;
        }

        // ========================================================
        // SỰ KIỆN MENU ICON
        // ========================================================
        private void btnMenuDashboard_CheckedChanged(object sender, EventArgs e) { if (btnMenuDashboard.Checked) tabControlThongKe.SelectedIndex = 0; }
        private void btnMenuTinhTrang_CheckedChanged(object sender, EventArgs e) { if (btnMenuTinhTrang.Checked) tabControlThongKe.SelectedIndex = 1; }
        private void btnMenuPhanBo_CheckedChanged(object sender, EventArgs e) { if (btnMenuPhanBo.Checked) tabControlThongKe.SelectedIndex = 2; }
        private void btnMenuBaoTri_CheckedChanged(object sender, EventArgs e) { if (btnMenuBaoTri.Checked) tabControlThongKe.SelectedIndex = 3; }

        // ========================================================
        // 1. DASHBOARD & DATA
        // ========================================================
        private void LoadDashboardData()
        {
            try
            {
                string sqlTongQuan = @"
                    DECLARE @TongMay INT, @TongGiaTri FLOAT, @TongBaoTri FLOAT, @TongThanhLy FLOAT;
                    SELECT @TongMay = ISNULL(COUNT(*), 0), @TongGiaTri = ISNULL(SUM(PurchasePrice), 0) FROM Equipment WHERE IsDeleted = 0 OR IsDeleted IS NULL;
                    SELECT @TongBaoTri = ISNULL(SUM(Cost), 0) FROM MaintenanceRecord;
                    SELECT @TongThanhLy = ISNULL(SUM(TotalRecoveryValue), 0) FROM LiquidationRecord;
                    SELECT @TongMay AS TongMay, @TongGiaTri AS TongGiaTri, @TongBaoTri AS TongBaoTri, @TongThanhLy AS TongThanhLy;
                ";
                DataTable dtTongQuan = DataProvider.Instance.ExecuteQuery(sqlTongQuan);
                if (dtTongQuan.Rows.Count > 0)
                {
                    DataRow r = dtTongQuan.Rows[0];
                    lblTongThietBi.Text = r["TongMay"].ToString();
                    lblTongGiaTri.Text = Convert.ToDouble(r["TongGiaTri"]).ToString("N0") + " đ";
                    lblTongBaoTri.Text = Convert.ToDouble(r["TongBaoTri"]).ToString("N0") + " đ";
                    lblTongThanhLy.Text = Convert.ToDouble(r["TongThanhLy"]).ToString("N0") + " đ";
                }

                // TÌNH TRẠNG (Tròn)
                string sqlTinhTrang = "SELECT Condition, COUNT(EquipmentID) AS SoLuong FROM Equipment WHERE IsDeleted = 0 OR IsDeleted IS NULL GROUP BY Condition";
                DataTable dtTinhTrang = DataProvider.Instance.ExecuteQuery(sqlTinhTrang);
                chartTinhTrang.Series.Clear();
                Series seriesPie = new Series("Tình trạng");
                seriesPie.ChartType = SeriesChartType.Doughnut;
                seriesPie.IsValueShownAsLabel = true;
                seriesPie.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                seriesPie.LabelForeColor = Color.White;
                foreach (DataRow row in dtTinhTrang.Rows)
                {
                    string status = row["Condition"].ToString();
                    int pt = seriesPie.Points.AddXY(status, Convert.ToInt32(row["SoLuong"]));
                    if (status == "Tốt" || status == "Đang sử dụng") seriesPie.Points[pt].Color = Color.FromArgb(40, 167, 69);
                    else if (status.Contains("bảo trì") || status.Contains("Hỏng nhẹ")) seriesPie.Points[pt].Color = Color.FromArgb(255, 193, 7);
                    else seriesPie.Points[pt].Color = Color.FromArgb(220, 53, 69);
                }
                chartTinhTrang.Series.Add(seriesPie);
                chartTinhTrang.Titles.Clear();
                chartTinhTrang.Titles.Add(new Title("TỶ LỆ TÌNH TRẠNG", Docking.Top, new Font("Segoe UI", 14, FontStyle.Bold), Color.FromArgb(26, 75, 132)));

                // PHÂN BỔ KHOA (Cột)
                string sqlKhoa = @"SELECT TOP 5 d.DepartmentName, COUNT(e.EquipmentID) AS SoLuong FROM Department d LEFT JOIN Equipment e ON d.DepartmentID = e.DepartmentID AND (e.IsDeleted = 0 OR e.IsDeleted IS NULL) WHERE d.IsDeleted = 0 OR d.IsDeleted IS NULL GROUP BY d.DepartmentName ORDER BY SoLuong DESC";
                DataTable dtKhoa = DataProvider.Instance.ExecuteQuery(sqlKhoa);
                chartKhoa.Series.Clear();
                Series seriesCol = new Series("KhoaPhòng");
                seriesCol.ChartType = SeriesChartType.Column;
                seriesCol.IsValueShownAsLabel = true;
                seriesCol.Color = Color.FromArgb(41, 128, 185);
                seriesCol.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                foreach (DataRow row in dtKhoa.Rows)
                {
                    string tenKhoa = row["DepartmentName"].ToString().Replace("Khoa ", "").Replace("Phòng ", "");
                    seriesCol.Points.AddXY(tenKhoa, Convert.ToInt32(row["SoLuong"]));
                }
                chartKhoa.Series.Add(seriesCol);
                chartKhoa.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartKhoa.Titles.Clear();
                chartKhoa.Titles.Add(new Title("TOP 5 ĐƠN VỊ SỞ HỮU", Docking.Top, new Font("Segoe UI", 14, FontStyle.Bold), Color.FromArgb(26, 75, 132)));
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load Dashboard: " + ex.Message); }
        }

        private void FormatAllGrids()
        {
            DataGridView[] grids = { dgvTinhTrang, dgvPhanBo, dgvBaoTri };
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
        }
        // Hàm tạo Header tự động cho bất kỳ DataGridView nào
        // Hàm tự động vẽ Header nằm TRÊN cái bảng
        private void AddHeaderAboveGrid(DataGridView dgv, string titleText, string panelName)
        {
            // Kiểm tra xem đã vẽ chưa, chưa vẽ thì mới làm để tránh bị đè nhiều lớp
            if (dgv.Parent != null && !dgv.Parent.Controls.ContainsKey(panelName))
            {
                // 1. Tạo Panel Tiêu đề
                Guna.UI2.WinForms.Guna2Panel pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
                pnlHeader.Name = panelName;
                pnlHeader.Height = 60; // Chiều cao của tiêu đề
                pnlHeader.Dock = DockStyle.Top; // Neo lên trên cùng
                pnlHeader.FillColor = Color.FromArgb(240, 248, 255);
                pnlHeader.CustomBorderColor = Color.LightGray;
                pnlHeader.CustomBorderThickness = new Padding(0, 0, 0, 2);

                // 2. Tạo Label chứa chữ
                Label lblTitle = new Label();
                lblTitle.Text = titleText;
                lblTitle.Font = new Font("Segoe UI", 15, FontStyle.Bold);
                lblTitle.ForeColor = Color.FromArgb(0, 51, 102);
                lblTitle.Dock = DockStyle.Fill;
                lblTitle.TextAlign = ContentAlignment.MiddleCenter;
                lblTitle.BackColor = Color.Transparent;

                // 3. Ráp chữ vào Panel
                pnlHeader.Controls.Add(lblTitle);

                // 4. Nhét Panel vào CÙNG KHÔNG GIAN với cái bảng
                dgv.Parent.Controls.Add(pnlHeader);

                // ==========================================
                // ĐÃ SỬA TẠI ĐÂY: DÙNG SendToBack() ĐỂ ĐẨY BẢNG XUỐNG DƯỚI
                // ==========================================
                pnlHeader.SendToBack();
            }
        }
        private void LoadDataTab_TinhTrang()
        {
            AddHeaderAboveGrid(dgvTinhTrang, "THỐNG KÊ SỐ LƯỢNG THIẾT BỊ THEO TRẠNG THÁI", "pnlHeaderTinhTrang");
            dgvTinhTrang.DataSource = DataProvider.Instance.ExecuteQuery(@"SELECT Condition AS [Tình trạng hiện tại], COUNT(EquipmentID) AS [Số lượng thiết bị], SUM(PurchasePrice) AS [Tổng Nguyên giá (VNĐ)] FROM Equipment WHERE IsDeleted = 0 OR IsDeleted IS NULL GROUP BY Condition ORDER BY [Số lượng thiết bị] DESC");
        }

        private void LoadDataTab_PhanBo()
        {
            AddHeaderAboveGrid(dgvPhanBo, "THỐNG KÊ PHÂN BỔ THIẾT BỊ THEO KHOA / PHÒNG", "pnlHeaderPhanBo");
            dgvPhanBo.DataSource = DataProvider.Instance.ExecuteQuery(@"SELECT d.DepartmentName AS [Tên Khoa / Phòng], COUNT(e.EquipmentID) AS [Tổng số máy], SUM(e.PurchasePrice) AS [Tổng giá trị (VNĐ)] FROM Department d LEFT JOIN Equipment e ON d.DepartmentID = e.DepartmentID AND (e.IsDeleted = 0 OR e.IsDeleted IS NULL) WHERE d.IsDeleted = 0 OR d.IsDeleted IS NULL GROUP BY d.DepartmentName ORDER BY [Tổng giá trị (VNĐ)] DESC");
        }

        private void LoadDataTab_BaoTri()
        {
            AddHeaderAboveGrid(dgvBaoTri, "THỐNG KÊ CHI PHÍ BẢO TRÌ VÀ THANH LÝ THIẾT BỊ", "pnlHeaderBaoTri");
            dgvBaoTri.DataSource = DataProvider.Instance.ExecuteQuery(@"SELECT e.EquipmentID AS [Mã TB], e.EquipmentName AS [Tên TB], e.Condition AS [Trạng thái], ISNULL((SELECT SUM(Cost) FROM MaintenanceRecord WHERE EquipmentID = e.EquipmentID), 0) AS [Tổng phí sửa (VNĐ)], ISNULL((SELECT SUM(TotalRecoveryValue) FROM LiquidationDetail ld JOIN LiquidationRecord lr ON ld.LiquidationID = lr.LiquidationID WHERE ld.EquipmentID = e.EquipmentID), 0) AS [Thu hồi thanh lý (VNĐ)] FROM Equipment e WHERE e.IsDeleted = 0 OR e.Condition = N'Đã thanh lý' ORDER BY [Tổng phí sửa (VNĐ)] DESC");
        }

        // ========================================================
        // 3. XUẤT EXCEL CHỐNG LỖI TUYỆT ĐỐI 100% (XML SPREADSHEET)
        // ========================================================
        private void btnXuatExcelAll_Click(object sender, EventArgs e)
        {
            if (dgvTinhTrang.Rows.Count == 0 && dgvPhanBo.Rows.Count == 0 && dgvBaoTri.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu để xuất báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel File (*.xls)|*.xls";
            sfd.Title = "Lưu báo cáo Thống kê Thiết bị";
            sfd.FileName = "BaoCao_ThietBi_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + ".xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Tự tay Code C# biên dịch ra cấu trúc lõi của file Excel siêu tốc độ
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                    {
                        sw.WriteLine("<?xml version=\"1.0\"?>");
                        sw.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
                        sw.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
                        sw.WriteLine(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
                        sw.WriteLine(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
                        sw.WriteLine(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">");

                        // ĐỊNH NGHĨA STYLES (MÀU SẮC)
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

                        sw.WriteLine("  <Style ss:ID=\"HeaderOrange\">");
                        sw.WriteLine("   <Font ss:Bold=\"1\" ss:Color=\"#FFFFFF\"/>");
                        sw.WriteLine("   <Interior ss:Color=\"#E67E22\" ss:Pattern=\"Solid\"/>");
                        sw.WriteLine("   <Borders><Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/></Borders>");
                        sw.WriteLine("  </Style>");

                        sw.WriteLine("  <Style ss:ID=\"NormalCell\">");
                        sw.WriteLine("   <Borders><Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/><Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/></Borders>");
                        sw.WriteLine("  </Style>");
                        sw.WriteLine(" </Styles>");

                        // TẠO 3 SHEET BẰNG CODE
                        WriteGridToXml(sw, dgvTinhTrang, "TinhTrang_TB", "TÌNH TRẠNG THIẾT BỊ", "HeaderBlue");
                        WriteGridToXml(sw, dgvPhanBo, "PhanBo_Khoa", "PHÂN BỔ TÀI SẢN KHOA", "HeaderBlue");
                        WriteGridToXml(sw, dgvBaoTri, "BaoTri_ThanhLy", "CHI PHÍ BẢO TRÌ & THANH LÝ", "HeaderOrange");

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
                // Thay thế ký tự đặc biệt để chống vỡ cấu trúc XML
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
                    val = val.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
                    sw.WriteLine($"    <Cell ss:StyleID=\"NormalCell\"><Data ss:Type=\"String\">{val}</Data></Cell>");
                }
                sw.WriteLine("   </Row>");
            }

            sw.WriteLine("  </Table>");
            sw.WriteLine(" </Worksheet>");
        }
    }
}