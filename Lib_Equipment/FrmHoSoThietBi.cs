using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmHoSoThietBi : Form
    {
        private string _maThietBi;
        private string _tenThietBi;

        // Constructor nhận mã và tên thiết bị từ form ngoài truyền vào
        public FrmHoSoThietBi(string maThietBi, string tenThietBi)
        {
            InitializeComponent();
            _maThietBi = maThietBi;
            _tenThietBi = tenThietBi;
        }

        private void FrmHoSoThietBi_Load(object sender, EventArgs e)
        {
            // Hiển thị tên thiết bị lên thanh Top
            lblTenThietBi.Text = $"{_maThietBi} - {_tenThietBi}";
            LoadThongBaoBaoTriDinhKy();
            LoadLichSuLuanChuyen();
            LoadLichSuBaoTri();
        }

        private void LoadLichSuLuanChuyen()
        {
            // JOIN với bảng Department để lấy Name thay vì ID
            string query = $@"
        SELECT 
            tr.TransferDate AS [Ngày chuyển],
            d1.DepartmentName AS [Từ Phòng/Khoa],
            d2.DepartmentName AS [Đến Phòng/Khoa],
            tr.Reason AS [Lý do luân chuyển],
            td.ConditionAtTransfer AS [Tình trạng máy]
        FROM TransferRecord tr
        JOIN TransferDetail td ON tr.TransferID = td.TransferID
        JOIN Department d1 ON tr.FromDepartmentID = d1.DepartmentID
        JOIN Department d2 ON tr.ToDepartmentID = d2.DepartmentID
        WHERE td.EquipmentID = '{_maThietBi}'
        ORDER BY tr.TransferDate DESC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvLuanChuyen.DataSource = dt;
            dgvLuanChuyen.EnableHeadersVisualStyles = false;
        }
        private void LoadThongBaoBaoTriDinhKy()
        {
            try
            {
                // 1. Lấy thông tin giá trị và ngày nhập của thiết bị
                string queryThietBi = "SELECT PurchasePrice, ImportDate FROM Equipment WHERE EquipmentID = @id";
                DataTable dtTB = DataProvider.Instance.ExecuteQuery(queryThietBi, new SqlParameter[] { new SqlParameter("@id", _maThietBi) });

                if (dtTB.Rows.Count == 0) return;

                double giaTri = 0;
                double.TryParse(dtTB.Rows[0]["PurchasePrice"].ToString(), out giaTri);
                DateTime ngayNhap = Convert.ToDateTime(dtTB.Rows[0]["ImportDate"]);

                // 2. Lấy ngày bảo trì gần nhất (Nếu chưa từng bảo trì thì lấy ngày nhập)
                string queryLichSu = "SELECT TOP 1 MaintenanceDate FROM MaintenanceRecord WHERE EquipmentID = @id ORDER BY MaintenanceDate DESC";
                DataTable dtLS = DataProvider.Instance.ExecuteQuery(queryLichSu, new SqlParameter[] { new SqlParameter("@id", _maThietBi) });

                DateTime ngayMocTinhToan = ngayNhap; // Mặc định là ngày nhập
                if (dtLS.Rows.Count > 0)
                {
                    ngayMocTinhToan = Convert.ToDateTime(dtLS.Rows[0]["MaintenanceDate"]);
                }

                // 3. TÍNH TOÁN THEO LOGIC CỦA BẠN
                int chuKyThang = 0;
                if (giaTri < 10000000) chuKyThang = 5;       // Dưới 10tr: 5 tháng
                else if (giaTri < 25000000) chuKyThang = 8;  // Từ 10tr - 25tr: 8 tháng
                else chuKyThang = 12;                        // Trên 25tr: 1 năm (12 tháng)

                double phiBaoTriDuKien = giaTri * 0.1; // 10% giá trị thiết bị
                DateTime ngayBaoTriTiepTheo = ngayMocTinhToan.AddMonths(chuKyThang);
                int soNgayConLai = (ngayBaoTriTiepTheo.Date - DateTime.Now.Date).Days;

                // 4. HIỂN THỊ LÊN GIAO DIỆN LUXURY
                rtbDinhKy.Clear();
                rtbDinhKy.SelectionAlignment = HorizontalAlignment.Center;
                rtbDinhKy.SelectionFont = new Font("Segoe UI", 16, FontStyle.Bold);
                rtbDinhKy.SelectionColor = Color.FromArgb(0, 51, 102);
                rtbDinhKy.AppendText("KẾ HOẠCH BẢO TRÌ ĐỊNH KỲ\n\n");

                rtbDinhKy.SelectionAlignment = HorizontalAlignment.Left;
                rtbDinhKy.SelectionFont = new Font("Segoe UI", 12, FontStyle.Regular);
                rtbDinhKy.SelectionColor = Color.Black;

                rtbDinhKy.AppendText($"🔹 Giá trị tài sản gốc: ");
                rtbDinhKy.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);
                rtbDinhKy.AppendText($"{giaTri:N0} VNĐ\n\n");

                rtbDinhKy.SelectionFont = new Font("Segoe UI", 12, FontStyle.Regular);
                rtbDinhKy.AppendText($"🔹 Chu kỳ bảo trì tiêu chuẩn: ");
                rtbDinhKy.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);
                rtbDinhKy.AppendText($"{chuKyThang} tháng / lần\n\n");

                rtbDinhKy.SelectionFont = new Font("Segoe UI", 12, FontStyle.Regular);
                rtbDinhKy.AppendText($"🔹 Chi phí dự toán (10%): ");
                rtbDinhKy.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);
                rtbDinhKy.SelectionColor = Color.FromArgb(40, 167, 69); // Xanh lá
                rtbDinhKy.AppendText($"{phiBaoTriDuKien:N0} VNĐ\n\n");

                rtbDinhKy.SelectionFont = new Font("Segoe UI", 12, FontStyle.Regular);
                rtbDinhKy.SelectionColor = Color.Black;
                rtbDinhKy.AppendText($"🔹 Mốc thời gian tham chiếu: {ngayMocTinhToan.ToString("dd/MM/yyyy")} ({(dtLS.Rows.Count > 0 ? "Bảo trì lần cuối" : "Ngày nhập mới")})\n\n");

                rtbDinhKy.AppendText($"⏳ NGÀY BẢO TRÌ TIẾP THEO: ");
                rtbDinhKy.SelectionFont = new Font("Segoe UI", 14, FontStyle.Bold);

                // Đổi màu cảnh báo nếu đã quá hạn
                if (soNgayConLai < 0)
                {
                    rtbDinhKy.SelectionColor = Color.Red;
                    rtbDinhKy.AppendText($"{ngayBaoTriTiepTheo.ToString("dd/MM/yyyy")} (Đã quá hạn {Math.Abs(soNgayConLai)} ngày!)\n");
                }
                else if (soNgayConLai <= 15)
                {
                    rtbDinhKy.SelectionColor = Color.DarkOrange;
                    rtbDinhKy.AppendText($"{ngayBaoTriTiepTheo.ToString("dd/MM/yyyy")} (Sắp đến hạn - Còn {soNgayConLai} ngày)\n");
                }
                else
                {
                    rtbDinhKy.SelectionColor = Color.Blue;
                    rtbDinhKy.AppendText($"{ngayBaoTriTiepTheo.ToString("dd/MM/yyyy")} (Còn {soNgayConLai} ngày)\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính toán định kỳ: " + ex.Message);
            }
        }
        private void LoadLichSuBaoTri()
        {
            string query = $@"
                SELECT 
                    MaintenanceDate AS [Ngày thực hiện],
                    Description AS [Nội dung xử lý],
                    Vendor AS [Đơn vị bảo trì],
                    Cost AS [Chi phí (VNĐ)]
                FROM MaintenanceRecord
                WHERE EquipmentID = '{_maThietBi}'
                ORDER BY MaintenanceDate DESC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvBaoTri.DataSource = dt;

            // Kích hoạt màu nền cho Header
            dgvBaoTri.EnableHeadersVisualStyles = false;

            // Format cột Tiền tệ
            if (dgvBaoTri.Columns.Contains("Chi phí (VNĐ)"))
            {
                dgvBaoTri.Columns["Chi phí (VNĐ)"].DefaultCellStyle.Format = "N0";
                dgvBaoTri.Columns["Chi phí (VNĐ)"].DefaultCellStyle.ForeColor = Color.Red;
                dgvBaoTri.Columns["Chi phí (VNĐ)"].DefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            }
        }
    }
}