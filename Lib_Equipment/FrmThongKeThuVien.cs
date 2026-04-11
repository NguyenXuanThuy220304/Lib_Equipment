using Lib_Equipment.Database;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OfficeOpenXml; // Thư viện Excel
using System.IO;

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
            LoadChartTopSach();
            LoadChartTrangThaiKho();
            LoadDanhSachDen();
            LoadTheTongQuan();
        }

        // ==========================================================
        // 1. THẺ TỔNG QUAN (SUMMARY CARDS)
        // ==========================================================
        private void LoadTheTongQuan()
        {
            // Đã đồng bộ: Nhân với 1000đ/ngày thay vì 5000đ
            string queryPhat = @"
                SELECT SUM(DATEDIFF(day, br.DueDate, GETDATE()) * 1000) 
                FROM BorrowRecord br
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID
                WHERE bd.ReturnDate IS NULL AND br.DueDate < CAST(GETDATE() AS DATE) AND br.IsDeleted = 0";

            object objPhat = DataProvider.Instance.ExecuteScalar(queryPhat);
            decimal tongPhat = objPhat != DBNull.Value ? Convert.ToDecimal(objPhat) : 0;

            Label lblTongPhat = new Label();
            lblTongPhat.Text = $"💰 TỔNG TIỀN PHẠT DỰ KIẾN THU: {tongPhat:N0} VNĐ";
            lblTongPhat.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTongPhat.ForeColor = Color.FromArgb(192, 57, 43); // Đỏ đậm
            lblTongPhat.AutoSize = true;
            lblTongPhat.Location = new Point(10, 15);

            pnlTop.Controls.Add(lblTongPhat);
        }

        // ==========================================================
        // 2. BIỂU ĐỒ CỘT: TOP 10 ĐẦU SÁCH
        // ==========================================================
        private void LoadChartTopSach()
        {
            string query = @"
        SELECT TOP 10 b.Title AS [TenSach], COUNT(bd.CopyID) AS [LuotMuon]
        FROM BorrowDetail bd
        JOIN BookCopy bc ON bd.CopyID = bc.CopyID
        JOIN Book b ON bc.BookID = b.BookID
        GROUP BY b.Title
        ORDER BY [LuotMuon] DESC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            chartTopSach.Series.Clear();
            chartTopSach.Titles.Clear();
            chartTopSach.Legends.Clear();

            // 1. Cấu hình Tiêu đề
            Title title = new Title("TOP 10 ĐẦU SÁCH MƯỢN NHIỀU NHẤT");
            title.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(41, 128, 185);
            chartTopSach.Titles.Add(title);

            // 2. Cấu hình Legend - CHIẾN THUẬT MỚI: ĐẨY XUỐNG DƯỚI (Bottom)
            Legend legend = new Legend("Legend1");
            legend.Enabled = true;
            legend.Docking = Docking.Bottom; // Đẩy xuống dưới để lấy chiều rộng tối đa
            legend.Alignment = StringAlignment.Center;
            legend.Font = new Font("Segoe UI", 9);
            legend.IsTextAutoFit = true;
            chartTopSach.Legends.Add(legend);

            // 3. Khởi tạo Series
            Series series = new Series("TopSach");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Ẩn nhãn Series tổng
            series.IsVisibleInLegend = false;

            chartTopSach.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTopSach.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;
            chartTopSach.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // 4. Đổ dữ liệu
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tenDayDu = dt.Rows[i]["TenSach"].ToString();
                double luotMuon = Convert.ToDouble(dt.Rows[i]["LuotMuon"]);
                string maNgan = "S" + (i + 1);

                int ptIdx = series.Points.AddXY(maNgan, luotMuon);

                // Hiện S1, S2... kèm tên sách xuống bảng chú thích bên dưới
                series.Points[ptIdx].LegendText = maNgan + ": " + tenDayDu;
                series.Points[ptIdx].IsVisibleInLegend = true;

                series.Points[ptIdx].Color = GetColorByOrder(i);
            }

            chartTopSach.Series.Add(series);

            // Cập nhật giao diện
            chartTopSach.Invalidate();
        }

        // Hàm lấy bảng màu
        private Color GetColorByOrder(int i)
        {
            Color[] palette = {
        Color.FromArgb(52, 152, 219), Color.FromArgb(46, 204, 113),
        Color.FromArgb(155, 89, 182), Color.FromArgb(241, 196, 15),
        Color.FromArgb(231, 76, 60), Color.FromArgb(26, 188, 156),
        Color.FromArgb(52, 73, 94), Color.FromArgb(230, 126, 34),
        Color.FromArgb(149, 165, 166), Color.FromArgb(127, 140, 141)
    };
            return palette[i % palette.Length];
        }
        // ==========================================================
        // 3. BIỂU ĐỒ BÁNH KHUYẾT: TRẠNG THÁI KHO
        // ==========================================================
        private void LoadChartTrangThaiKho()
        {
            string query = "SELECT Status AS [TrangThai], COUNT(CopyID) AS [SoLuong] FROM BookCopy WHERE IsDeleted = 0 GROUP BY Status";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            chartTrangThai.Series.Clear();
            chartTrangThai.Titles.Clear();
            chartTrangThai.Titles.Add("TỶ LỆ TRẠNG THÁI KHO SÁCH");
            chartTrangThai.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);

            Series series = new Series("Trạng thái");
            series.ChartType = SeriesChartType.Doughnut;
            series["DoughnutRadius"] = "50";
            series.IsValueShownAsLabel = true;
            series.Label = "#VALX: #PERCENT{P0}";
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            foreach (DataRow row in dt.Rows)
            {
                string status = row["TrangThai"].ToString();
                int val = Convert.ToInt32(row["SoLuong"]);
                int ptIdx = series.Points.AddXY(status, val);

                if (status == "Có sẵn") series.Points[ptIdx].Color = Color.FromArgb(46, 204, 113);
                else if (status == "Đang mượn") series.Points[ptIdx].Color = Color.FromArgb(241, 196, 15);
                else if (status == "Hỏng") series.Points[ptIdx].Color = Color.FromArgb(230, 126, 34);
                else if (status == "Mất") series.Points[ptIdx].Color = Color.FromArgb(231, 76, 60);
            }
            chartTrangThai.Series.Add(series);
            chartTrangThai.Legends[0].Font = new Font("Segoe UI", 10);
        }

        // ==========================================================
        // DANH SÁCH ĐEN (ĐỒNG BỘ STYLE VỚI BÊN THIẾT BỊ)
        // ==========================================================
        private void LoadDanhSachDen()
        {
            string query = @"
                SELECT 
                    r.ReaderID AS [Mã Độc giả],
                    r.FullName AS [Họ và tên],
                    COUNT(bd.CopyID) AS [Số sách quá hạn],
                    SUM(DATEDIFF(day, br.DueDate, GETDATE()) * 1000) AS [Tiền phạt dự kiến (VNĐ)]
                FROM BorrowRecord br
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID
                JOIN Reader r ON br.ReaderID = r.ReaderID
                WHERE bd.ReturnDate IS NULL AND br.DueDate < CAST(GETDATE() AS DATE)
                GROUP BY r.ReaderID, r.FullName
                ORDER BY [Tiền phạt dự kiến (VNĐ)] DESC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvBlacklist.DataSource = dt;

            // Làm đẹp bảng Grid giống hệt dgvBaoTri
            dgvBlacklist.EnableHeadersVisualStyles = false;
            dgvBlacklist.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(192, 57, 43); // Đỏ cảnh báo
            dgvBlacklist.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBlacklist.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvBlacklist.ColumnHeadersHeight = 45;

            // Bôi đỏ chữ cho những người bị phạt tiền
            if (dgvBlacklist.Columns.Contains("Tiền phạt dự kiến (VNĐ)"))
            {
                foreach (DataGridViewRow row in dgvBlacklist.Rows)
                {
                    row.Cells["Tiền phạt dự kiến (VNĐ)"].Style.Format = "N0";
                    row.Cells["Tiền phạt dự kiến (VNĐ)"].Style.ForeColor = Color.Red;
                    row.Cells["Tiền phạt dự kiến (VNĐ)"].Style.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                }
            }
        }

        // ==========================================================
        // 5. XUẤT EXCEL CHUYÊN NGHIỆP
        // ==========================================================
        private void btnXuatExcelThuVien_Click(object sender, EventArgs e)
        {
            if (dgvBlacklist.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu độc giả quá hạn để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Lưu báo cáo Danh sách đen";
            sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
            sfd.FileName = $"BaoCao_DanhSachDen_{DateTime.Now:ddMMyyyy}.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 1. Tạo đối tượng Excel
                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    app.Visible = false;

                    worksheet = workbook.Sheets["Sheet1"];
                    worksheet = workbook.ActiveSheet;
                    worksheet.Name = "DanhSachDen";

                    // 2. In Tiêu đề
                    worksheet.Cells[1, 1] = "BÁO CÁO DANH SÁCH ĐỘC GIẢ NỢ SÁCH QUÁ HẠN";
                    worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 4]].Merge();
                    worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 4]].Font.Bold = true;
                    worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 4]].Font.Size = 14;
                    worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 4]].Font.Color = ColorTranslator.ToOle(Color.Red);
                    worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 4]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    // 3. In Tên Cột
                    for (int i = 1; i < dgvBlacklist.Columns.Count + 1; i++)
                    {
                        worksheet.Cells[3, i] = dgvBlacklist.Columns[i - 1].HeaderText;
                        worksheet.Cells[3, i].Font.Bold = true;
                        worksheet.Cells[3, i].Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                        worksheet.Cells[3, i].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    }

                    // 4. Đổ dữ liệu từ DataGridView
                    for (int i = 0; i < dgvBlacklist.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvBlacklist.Columns.Count; j++)
                        {
                            // In dữ liệu
                            worksheet.Cells[i + 4, j + 1] = dgvBlacklist.Rows[i].Cells[j].Value?.ToString();
                            // Kẻ khung
                            worksheet.Cells[i + 4, j + 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        }
                    }

                    // Tự động giãn cột
                    worksheet.Columns.AutoFit();

                    // 5. Lưu và Đóng file
                    workbook.SaveAs(sfd.FileName);
                    workbook.Close();
                    app.Quit();

                    // Giải phóng bộ nhớ
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                    if (MessageBox.Show("Xuất báo cáo Excel thành công! Mở file ngay?", "Hoàn tất", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất Excel (Lưu ý máy phải cài sẵn MS Office): \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}