using Lib_Equipment.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OfficeOpenXml;
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
        // 1. THẺ TỔNG QUAN
        // ==========================================================
        private void LoadTheTongQuan()
        {
            // Lấy dữ liệu qua BLL, không dùng SQL ở đây nữa
            decimal tongPhat = ThongKeThuVienBLL.Instance.TinhTongTienPhatDuKien();

            Label lblTongPhat = new Label();
            lblTongPhat.Text = $"💰 TỔNG TIỀN PHẠT DỰ KIẾN THU: {tongPhat:N0} VNĐ";
            lblTongPhat.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTongPhat.ForeColor = Color.FromArgb(192, 57, 43);
            lblTongPhat.AutoSize = true;
            lblTongPhat.Location = new Point(10, 15);

            pnlTop.Controls.Add(lblTongPhat);
        }

        // ==========================================================
        // 2. BIỂU ĐỒ CỘT
        // ==========================================================
        private void LoadChartTopSach()
        {
            // Lấy dữ liệu qua BLL
            DataTable dt = ThongKeThuVienBLL.Instance.LayDuLieuTopSach();

            chartTopSach.Series.Clear();
            chartTopSach.Titles.Clear();
            chartTopSach.Legends.Clear();

            Title title = new Title("TOP 10 ĐẦU SÁCH MƯỢN NHIỀU NHẤT");
            title.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(41, 128, 185);
            chartTopSach.Titles.Add(title);

            Legend legend = new Legend("Legend1");
            legend.Enabled = true;
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.Font = new Font("Segoe UI", 9);
            legend.IsTextAutoFit = true;
            chartTopSach.Legends.Add(legend);

            Series series = new Series("TopSach");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            series.IsVisibleInLegend = false;

            chartTopSach.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTopSach.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;
            chartTopSach.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tenDayDu = dt.Rows[i]["TenSach"].ToString();
                double luotMuon = Convert.ToDouble(dt.Rows[i]["LuotMuon"]);
                string maNgan = "S" + (i + 1);

                int ptIdx = series.Points.AddXY(maNgan, luotMuon);

                series.Points[ptIdx].LegendText = maNgan + ": " + tenDayDu;
                series.Points[ptIdx].IsVisibleInLegend = true;
                series.Points[ptIdx].Color = GetColorByOrder(i);
            }

            chartTopSach.Series.Add(series);
            chartTopSach.Invalidate();
        }

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
        // 3. BIỂU ĐỒ BÁNH
        // ==========================================================
        private void LoadChartTrangThaiKho()
        {
            // Lấy dữ liệu qua BLL
            DataTable dt = ThongKeThuVienBLL.Instance.LayDuLieuTrangThaiKho();

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
        // 4. LƯỚI DANH SÁCH ĐEN
        // ==========================================================
        private void LoadDanhSachDen()
        {
            // Lấy dữ liệu qua BLL
            DataTable dt = ThongKeThuVienBLL.Instance.LayDuLieuDanhSachDen();
            dgvBlacklist.DataSource = dt;

            dgvBlacklist.EnableHeadersVisualStyles = false;
            dgvBlacklist.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(192, 57, 43);
            dgvBlacklist.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBlacklist.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvBlacklist.ColumnHeadersHeight = 45;

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
        // 5. XUẤT EXCEL
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
                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    app.Visible = false;

                    worksheet = workbook.Sheets["Sheet1"];
                    worksheet = workbook.ActiveSheet;
                    worksheet.Name = "DanhSachDen";

                    worksheet.Cells[1, 1] = "BÁO CÁO DANH SÁCH ĐỘC GIẢ NỢ SÁCH QUÁ HẠN";
                    worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 4]].Merge();
                    worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 4]].Font.Bold = true;
                    worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 4]].Font.Size = 14;
                    worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 4]].Font.Color = ColorTranslator.ToOle(Color.Red);
                    worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 4]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    for (int i = 1; i < dgvBlacklist.Columns.Count + 1; i++)
                    {
                        worksheet.Cells[3, i] = dgvBlacklist.Columns[i - 1].HeaderText;
                        worksheet.Cells[3, i].Font.Bold = true;
                        worksheet.Cells[3, i].Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                        worksheet.Cells[3, i].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    }

                    for (int i = 0; i < dgvBlacklist.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvBlacklist.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 4, j + 1] = dgvBlacklist.Rows[i].Cells[j].Value?.ToString();
                            worksheet.Cells[i + 4, j + 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        }
                    }

                    worksheet.Columns.AutoFit();

                    workbook.SaveAs(sfd.FileName);
                    workbook.Close();
                    app.Quit();

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