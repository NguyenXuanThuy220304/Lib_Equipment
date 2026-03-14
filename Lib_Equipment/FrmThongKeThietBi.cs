using Lib_Equipment.Database;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OfficeOpenXml; // Thư viện EPPlus
using System.IO;     // Thư viện xử lý lưu File

namespace Lib_Equipment
{
    public partial class FrmThongKeThietBi : Form
    {
        [Obsolete]
        public FrmThongKeThietBi()
        {
            InitializeComponent();
        }

        private void FrmThongKeThietBi_Load(object sender, EventArgs e)
        {
            LoadChartTrangThai();
            LoadChartPhanBo();
            LoadGridBaoTri();
            LoadBaoCaoTaiChinh();
        }

        // ==========================================================
        // 1. BIỂU ĐỒ TRÒN: TÌNH TRẠNG THIẾT BỊ 
        // ==========================================================
        private void LoadChartTrangThai()
        {
            // Sửa cột trạng thái thành Condition
            string query = @"
                SELECT Condition AS [TrangThai], COUNT(EquipmentID) AS [SoLuong]
                FROM Equipment
                WHERE IsDeleted = 0 OR IsDeleted IS NULL
                GROUP BY Condition";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            chartTrangThai.Series.Clear();
            Series series = new Series("Trạng thái");
            series.ChartType = SeriesChartType.Doughnut;
            series.IsValueShownAsLabel = true;
            series.Label = "#VALX: #PERCENT";
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            chartTrangThai.Series.Add(series);

            foreach (DataRow row in dt.Rows)
            {
                string status = row["TrangThai"].ToString().Trim();
                int count = Convert.ToInt32(row["SoLuong"]);
                int ptIndex = series.Points.AddXY(status, count);

                if (status.Contains("Tốt") || status.Contains("Hoạt động") || status.Contains("sử dụng"))
                    series.Points[ptIndex].Color = Color.FromArgb(40, 167, 69);
                else if (status.Contains("Bảo trì"))
                    series.Points[ptIndex].Color = Color.FromArgb(255, 193, 7);
                else if (status.Contains("Hỏng"))
                    series.Points[ptIndex].Color = Color.FromArgb(220, 53, 69);
                else if (status.Contains("Thanh lý"))
                    series.Points[ptIndex].Color = Color.Gray;
            }
        }

        // ==========================================================
        // 2. BIỂU ĐỒ CỘT: PHÂN BỔ THIẾT BỊ THEO KHOA / PHÒNG
        // ==========================================================
        private void LoadChartPhanBo()
        {
            // Sửa cột vị trí thành DepartmentID
            string query = @"
                SELECT TOP 10 
                    DepartmentID AS [PhongBan], 
                    COUNT(EquipmentID) AS [SoLuong]
                FROM Equipment
                WHERE IsDeleted = 0 OR IsDeleted IS NULL
                GROUP BY DepartmentID
                ORDER BY [SoLuong] DESC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            chartPhanBo.Series.Clear();
            Series series = new Series("Số lượng thiết bị");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(26, 75, 132);
            series.IsValueShownAsLabel = true;
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            chartPhanBo.Series.Add(series);
            chartPhanBo.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 9);
            chartPhanBo.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 9);
            chartPhanBo.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            chartPhanBo.DataSource = dt;
            chartPhanBo.Series["Số lượng thiết bị"].XValueMember = "PhongBan";
            chartPhanBo.Series["Số lượng thiết bị"].YValueMembers = "SoLuong";
            chartPhanBo.DataBind();
        }

        // ==========================================================
        // 3. BẢNG DỮ LIỆU: DANH SÁCH THIẾT BỊ ĐANG HỎNG / BẢO TRÌ
        // ==========================================================
        private void LoadGridBaoTri()
        {
            // Sử dụng các cột Condition, DepartmentID, EquipmentName
            string query = @"
                SELECT 
                    EquipmentID AS [Mã Thiết Bị],
                    EquipmentName AS [Tên Thiết Bị],
                    DepartmentID AS [Vị trí / Khoa],
                    Condition AS [Tình trạng hiện tại],
                    ImportDate AS [Ngày Nhập]
                FROM Equipment
                WHERE (Condition LIKE N'%Hỏng%' OR Condition LIKE N'%Bảo trì%') 
                  AND (IsDeleted = 0 OR IsDeleted IS NULL)
                ORDER BY DepartmentID ASC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvBaoTri.DataSource = dt;

            // Làm đẹp bảng Grid
            dgvBaoTri.EnableHeadersVisualStyles = false;
            dgvBaoTri.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 0);
            dgvBaoTri.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBaoTri.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvBaoTri.ColumnHeadersHeight = 45;

            // Bôi đỏ chữ cho các máy bị "Hỏng"
            if (dgvBaoTri.Columns.Contains("Tình trạng hiện tại"))
            {
                foreach (DataGridViewRow row in dgvBaoTri.Rows)
                {
                    string status = row.Cells["Tình trạng hiện tại"].Value.ToString();
                    if (status.Contains("Hỏng"))
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        row.DefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                    }
                }
            }
        }
        // ==========================================================
        // 4. BÁO CÁO TÀI CHÍNH (TỰ ĐỘNG SINH GIAO DIỆN)
        // ==========================================================
        private void LoadBaoCaoTaiChinh()
        {
            // 1. Lấy dữ liệu Tiền từ Database
            string sqlTaiSan = "SELECT SUM(PurchasePrice) FROM Equipment WHERE IsDeleted = 0 OR IsDeleted IS NULL";
            object objTaiSan = DataProvider.Instance.ExecuteScalar(sqlTaiSan);
            decimal tongTaiSan = objTaiSan != DBNull.Value ? Convert.ToDecimal(objTaiSan) : 0;

            string sqlBaoTri = "SELECT SUM(Cost) FROM MaintenanceRecord WHERE IsDeleted = 0 OR IsDeleted IS NULL";
            object objBaoTri = DataProvider.Instance.ExecuteScalar(sqlBaoTri);
            decimal tongBaoTri = objBaoTri != DBNull.Value ? Convert.ToDecimal(objBaoTri) : 0;

            // 2. Tạo Thẻ hiển thị Tổng Giá trị Tài Sản
            Label lblTongTaiSan = new Label();
            lblTongTaiSan.Text = $"💰 TỔNG GIÁ TRỊ TÀI SẢN: {tongTaiSan:N0} VNĐ";
            lblTongTaiSan.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTongTaiSan.ForeColor = Color.FromArgb(40, 167, 69); // Màu xanh lá mượt mà
            lblTongTaiSan.AutoSize = true;
            lblTongTaiSan.Location = new Point(10, 10); // Căn lùi sang góc phải thanh Top

            // 3. Tạo Thẻ hiển thị Tổng Chi phí sửa chữa
            Label lblTongBaoTri = new Label();
            lblTongBaoTri.Text = $"🔧 TỔNG CHI PHÍ BẢO TRÌ: {tongBaoTri:N0} VNĐ";
            lblTongBaoTri.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTongBaoTri.ForeColor = Color.FromArgb(220, 53, 69); // Màu đỏ chi phí
            lblTongBaoTri.AutoSize = true;
            lblTongBaoTri.Location = new Point(10, 35); // Nằm ngay dưới thẻ Tài sản

            // 4. Gắn 2 thẻ này lên thanh pnlTop (Thanh màu trắng trên cùng chứa Tiêu đề)
            pnlTop.Controls.Add(lblTongTaiSan);
            pnlTop.Controls.Add(lblTongBaoTri);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvBaoTri.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất báo cáo!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất báo cáo thiết bị ra Excel";
            saveFileDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = "BaoCao_ThietBi_CanBaoTri.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    
                    // 2. TẠO FILE EXCEL (Chỉ định đích danh OfficeOpenXml)
                    using (OfficeOpenXml.ExcelPackage excel = new OfficeOpenXml.ExcelPackage())
                    {
                        var sheet = excel.Workbook.Worksheets.Add("BaoCao");

                        // IN TIÊU ĐỀ
                        sheet.Cells["A1:E1"].Merge = true;
                        sheet.Cells["A1"].Value = "BÁO CÁO DANH SÁCH THIẾT BỊ HỎNG / CẦN BẢO TRÌ";
                        sheet.Cells["A1"].Style.Font.Size = 14;
                        sheet.Cells["A1"].Style.Font.Bold = true;
                        sheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        sheet.Cells["A1"].Style.Font.Color.SetColor(Color.Red);

                        sheet.Cells["A2:E2"].Merge = true;
                        sheet.Cells["A2"].Value = "Ngày xuất báo cáo: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                        sheet.Cells["A2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        sheet.Cells["A2"].Style.Font.Italic = true;

                        // IN CỘT TIÊU ĐỀ
                        int startRow = 4;
                        for (int i = 0; i < dgvBaoTri.Columns.Count; i++)
                        {
                            sheet.Cells[startRow, i + 1].Value = dgvBaoTri.Columns[i].HeaderText;
                            sheet.Cells[startRow, i + 1].Style.Font.Bold = true;
                            sheet.Cells[startRow, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            sheet.Cells[startRow, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 140, 0));
                            sheet.Cells[startRow, i + 1].Style.Font.Color.SetColor(Color.White);
                            sheet.Cells[startRow, i + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        }

                        // IN DỮ LIỆU
                        for (int i = 0; i < dgvBaoTri.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvBaoTri.Columns.Count; j++)
                            {
                                object cellValue = dgvBaoTri.Rows[i].Cells[j].Value;
                                sheet.Cells[i + startRow + 1, j + 1].Value = cellValue != null ? cellValue.ToString() : "";
                                sheet.Cells[i + startRow + 1, j + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                            }
                        }

                        sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                        System.IO.File.WriteAllBytes(saveFileDialog.FileName, excel.GetAsByteArray());

                        if (MessageBox.Show("Xuất báo cáo thành công! Mở file Excel ngay?", "Thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveFileDialog.FileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Đã sửa lại chữ "Lỗi kết nối CSDL" thành "Lỗi xuất Excel" cho chuẩn xác
                    MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void dgvBaoTri_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có click đúng vào dòng dữ liệu không (tránh click nhầm tiêu đề)
            if (e.RowIndex >= 0)
            {
                // Lấy mã thiết bị ở cột đầu tiên ("Mã Thiết Bị") của dòng vừa click
                string maThietBi = dgvBaoTri.Rows[e.RowIndex].Cells["Mã Thiết Bị"].Value.ToString();
                string tenThietBi = dgvBaoTri.Rows[e.RowIndex].Cells["Tên Thiết Bị"].Value.ToString();

                // Mở Form Lịch sử (Popup) và truyền Mã thiết bị sang để nó tự động truy vấn CSDL
                // (Chút nữa chúng ta sẽ tạo form này)
                FrmHoSoThietBi frmHoSo = new FrmHoSoThietBi(maThietBi, tenThietBi);
                frmHoSo.ShowDialog();
            }
        }
    }
}