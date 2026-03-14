using Lib_Equipment.Database;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Thư viện vẽ biểu đồ

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
        }

        // ==========================================================
        // 1. BIỂU ĐỒ CỘT: TOP 10 SÁCH ĐƯỢC MƯỢN NHIỀU NHẤT
        // ==========================================================
        private void LoadChartTopSach()
        {
            string query = @"
                SELECT TOP 10 
                    b.Title AS [TenSach], 
                    COUNT(bd.CopyID) AS [LuotMuon]
                FROM BorrowDetail bd
                JOIN BookCopy bc ON bd.CopyID = bc.CopyID
                JOIN Book b ON bc.BookID = b.BookID
                GROUP BY b.Title
                ORDER BY [LuotMuon] DESC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            // Cấu hình biểu đồ
            chartTopSach.Series.Clear();
            Series series = new Series("Lượt mượn");
            series.ChartType = SeriesChartType.Column; // Biểu đồ cột
            series.Color = Color.FromArgb(40, 167, 69); // Màu xanh lá đẹp mắt
            series.IsValueShownAsLabel = true; // Hiện số trên đầu cột
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            chartTopSach.Series.Add(series);
            chartTopSach.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 9);
            chartTopSach.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 9);
            chartTopSach.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // Tắt lưới dọc cho đỡ rối

            // Nạp dữ liệu vào biểu đồ
            chartTopSach.DataSource = dt;
            chartTopSach.Series["Lượt mượn"].XValueMember = "TenSach";
            chartTopSach.Series["Lượt mượn"].YValueMembers = "LuotMuon";
            chartTopSach.DataBind();
        }

        // ==========================================================
        // 2. BIỂU ĐỒ TRÒN: TỶ TRỌNG TRẠNG THÁI KHO SÁCH VẬT LÝ
        // ==========================================================
        private void LoadChartTrangThaiKho()
        {
            string query = @"
                SELECT Status AS [TrangThai], COUNT(CopyID) AS [SoLuong]
                FROM BookCopy
                WHERE IsDeleted = 0 OR IsDeleted IS NULL
                GROUP BY Status";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            chartTrangThai.Series.Clear();
            Series series = new Series("Trạng thái");
            series.ChartType = SeriesChartType.Pie; // Biểu đồ tròn
            series.IsValueShownAsLabel = true;
            series.Label = "#VALX: #VALY cuốn\n(#PERCENT)"; // Hiện chữ: Tên trạng thái + Số lượng + %
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Custom màu sắc cho từng trạng thái
            chartTrangThai.Series.Add(series);
            foreach (DataRow row in dt.Rows)
            {
                string status = row["TrangThai"].ToString();
                int count = Convert.ToInt32(row["SoLuong"]);
                int ptIndex = series.Points.AddXY(status, count);

                if (status == "Có sẵn") series.Points[ptIndex].Color = Color.FromArgb(0, 123, 255); // Xanh dương
                else if (status == "Đang mượn") series.Points[ptIndex].Color = Color.FromArgb(255, 193, 7); // Vàng
                else if (status == "Hỏng") series.Points[ptIndex].Color = Color.Gray;
                else if (status == "Mất") series.Points[ptIndex].Color = Color.FromArgb(220, 53, 69); // Đỏ
            }
        }

        // ==========================================================
        // 3. BẢNG DANH SÁCH ĐEN: SINH VIÊN NỢ SÁCH & TIỀN PHẠT DỰ KIẾN
        // ==========================================================
        private void LoadDanhSachDen()
        {
            string query = @"
                SELECT 
                    r.ReaderID AS [Mã Độc giả],
                    r.FullName AS [Họ và Tên],
                    r.DepartmentID AS [Khoa/Viện],
                    COUNT(bd.CopyID) AS [Số sách nợ quá hạn],
                    SUM(DATEDIFF(day, br.DueDate, GETDATE()) * 1000) AS [Tiền phạt dự kiến (VNĐ)]
                FROM BorrowRecord br
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID
                JOIN Reader r ON br.ReaderID = r.ReaderID
                WHERE bd.ReturnDate IS NULL AND br.DueDate < CAST(GETDATE() AS DATE)
                GROUP BY r.ReaderID, r.FullName, r.DepartmentID
                ORDER BY [Tiền phạt dự kiến (VNĐ)] DESC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvBlacklist.DataSource = dt;

            // Làm đẹp UI cho DataGridView
            dgvBlacklist.EnableHeadersVisualStyles = false;
            dgvBlacklist.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(220, 53, 69); // Màu đỏ cảnh báo
            dgvBlacklist.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBlacklist.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvBlacklist.ColumnHeadersHeight = 45;

            if (dgvBlacklist.Columns.Contains("Tiền phạt dự kiến (VNĐ)"))
            {
                dgvBlacklist.Columns["Tiền phạt dự kiến (VNĐ)"].DefaultCellStyle.Format = "N0"; // Format tiền tệ có dấu phẩy
                dgvBlacklist.Columns["Tiền phạt dự kiến (VNĐ)"].DefaultCellStyle.ForeColor = Color.Red;
                dgvBlacklist.Columns["Tiền phạt dự kiến (VNĐ)"].DefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            }
        }
    }
}