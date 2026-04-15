using Lib_Equipment.Database;
using System;
using System.Data;
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