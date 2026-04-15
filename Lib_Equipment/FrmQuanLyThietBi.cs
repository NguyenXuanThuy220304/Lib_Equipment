using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmQuanLyThietBi : Form
    {
        private string selectedEquipmentID = "";

        public FrmQuanLyThietBi()
        {
            InitializeComponent();
        }

        private void FrmQuanLyThietBi_Load(object sender, EventArgs e)
        {
            LoadComboboxLoaiTB();
            LoadComboboxKhoaPhong();
            LoadData();
            AdjustSearchLayout();
            pnlSearch.Visible = false;
            pnlControls.Visible = true;
            CheckMaintenanceAlert();
        }
        private void FrmQuanLyThietBi_Resize(object sender, EventArgs e)
        {
            AdjustSearchLayout();
        }
        // =======================================================
        // 1. TẢI DỮ LIỆU & ĐỊNH DẠNG
        // =======================================================
        private void LoadComboboxLoaiTB()
        {
            string query = "SELECT CategoryID, CategoryName FROM EquipmentCategory WHERE IsDeleted = 0 OR IsDeleted IS NULL";
            cboLoaiTB.DataSource = DataProvider.Instance.ExecuteQuery(query);
            cboLoaiTB.DisplayMember = "CategoryName";
            cboLoaiTB.ValueMember = "CategoryID";
        }

        private void LoadComboboxKhoaPhong()
        {
            string query = "SELECT DepartmentID, DepartmentName FROM Department WHERE IsDeleted = 0 OR IsDeleted IS NULL";
            cboKhoaPhong.DataSource = DataProvider.Instance.ExecuteQuery(query);
            cboKhoaPhong.DisplayMember = "DepartmentName";
            cboKhoaPhong.ValueMember = "DepartmentID";
        }

        private void FormatGrid()
        {
            if (dgvThietBi.Columns.Contains("EquipmentID")) dgvThietBi.Columns["EquipmentID"].HeaderText = "Mã TB";
            if (dgvThietBi.Columns.Contains("EquipmentName")) dgvThietBi.Columns["EquipmentName"].HeaderText = "Tên Thiết Bị";
            if (dgvThietBi.Columns.Contains("CategoryName")) dgvThietBi.Columns["CategoryName"].HeaderText = "Phân Loại";
            if (dgvThietBi.Columns.Contains("DepartmentName")) dgvThietBi.Columns["DepartmentName"].HeaderText = "Khoa/Phòng";
            if (dgvThietBi.Columns.Contains("ImportDate")) dgvThietBi.Columns["ImportDate"].HeaderText = "Ngày Nhập";
            if (dgvThietBi.Columns.Contains("PurchasePrice"))
            {
                dgvThietBi.Columns["PurchasePrice"].HeaderText = "Giá Nhập";
                dgvThietBi.Columns["PurchasePrice"].DefaultCellStyle.Format = "N0";
            }
            if (dgvThietBi.Columns.Contains("Condition")) dgvThietBi.Columns["Condition"].HeaderText = "Tình Trạng";

            // LUÔN LUÔN ẨN CÁC CỘT ID ĐỂ TRÁNH LỖI KHI CLICK
            if (dgvThietBi.Columns.Contains("CategoryID")) dgvThietBi.Columns["CategoryID"].Visible = false;
            if (dgvThietBi.Columns.Contains("DepartmentID")) dgvThietBi.Columns["DepartmentID"].Visible = false;
        }

        private void LoadData()
        {
            string query = @"
                SELECT e.EquipmentID, e.EquipmentName, c.CategoryName, d.DepartmentName, 
                       e.ImportDate, e.PurchasePrice, e.Condition, 
                       e.CategoryID, e.DepartmentID 
                FROM Equipment e
                LEFT JOIN EquipmentCategory c ON e.CategoryID = c.CategoryID
                LEFT JOIN Department d ON e.DepartmentID = d.DepartmentID
                WHERE e.IsDeleted = 0 OR e.IsDeleted IS NULL";

            dgvThietBi.DataSource = DataProvider.Instance.ExecuteQuery(query);
            FormatGrid();
        }

        // =======================================================
        // 2. TÌM KIẾM (HỖ TRỢ VIẾT TẮT)
        // =======================================================
        private void btnOpenSearch_Click(object sender, EventArgs e)
        {
            pnlControls.Visible = false;
            pnlSearch.Visible = true;
            btnOpenSearch.Visible = false;
            txtSearch.Focus();
        }

        private void btnDoSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) { LoadData(); return; }

            // Thuật toán: Biến "DH" thành "%D%H%" để tìm kiếm viết tắt
            string fuzzyKey = "%";
            foreach (char c in keyword) { fuzzyKey += c + "%"; }

            // Tìm kiếm trên chuỗi kết hợp (Tên + Loại)
            string query = @"
                SELECT e.EquipmentID, e.EquipmentName, c.CategoryName, d.DepartmentName, 
                       e.ImportDate, e.PurchasePrice, e.Condition,
                       e.CategoryID, e.DepartmentID 
                FROM Equipment e
                LEFT JOIN EquipmentCategory c ON e.CategoryID = c.CategoryID
                LEFT JOIN Department d ON e.DepartmentID = d.DepartmentID
                WHERE (e.IsDeleted = 0 OR e.IsDeleted IS NULL)
                AND (
                    (e.EquipmentName + ' ' + ISNULL(c.CategoryName, '')) LIKE @fuzzy
                    OR e.EquipmentID LIKE @normal
                )";

            SqlParameter[] param = {
                new SqlParameter("@fuzzy", fuzzyKey),
                new SqlParameter("@normal", "%" + keyword + "%")
            };

            dgvThietBi.DataSource = DataProvider.Instance.ExecuteQuery(query, param);
            FormatGrid();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            pnlControls.Visible = true;
            pnlSearch.Visible = false;
            btnOpenSearch.Visible = true;
            selectedEquipmentID = "";
            txtMaTB.Enabled = true;
            txtMaTB.Clear(); txtTenTB.Clear(); txtGiaTien.Clear(); txtSearch.Clear();
            LoadData();
        }

        // =======================================================
        // 3. MÃ VẠCH (MỞ FORM MỚI)
        // =======================================================
        private void btnBarcode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEquipmentID))
            {
                MessageBox.Show("Vui lòng chọn một thiết bị từ danh sách để xem mã vạch!", "Thông báo");
                return;
            }

            // SỬA DÒNG NÀY: Truyền thêm txtTenTB.Text vào tham số thứ 2
            FrmBarcodeViewer barcodeForm = new FrmBarcodeViewer(selectedEquipmentID, txtTenTB.Text);
            barcodeForm.ShowDialog();
        }

        // =======================================================
        // 4. CLICK & DOUBLE CLICK
        // =======================================================
        private void dgvThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThietBi.Rows[e.RowIndex];
                selectedEquipmentID = row.Cells["EquipmentID"].Value.ToString();

                txtMaTB.Text = selectedEquipmentID;
                txtTenTB.Text = row.Cells["EquipmentName"].Value.ToString();

                // Gán giá trị Combobox an toàn
                if (dgvThietBi.Columns.Contains("CategoryID"))
                    cboLoaiTB.SelectedValue = row.Cells["CategoryID"].Value;

                if (dgvThietBi.Columns.Contains("DepartmentID"))
                    cboKhoaPhong.SelectedValue = row.Cells["DepartmentID"].Value;

                txtGiaTien.Text = row.Cells["PurchasePrice"].Value.ToString();
                cboTinhTrang.Text = row.Cells["Condition"].Value.ToString();
                txtMaTB.Enabled = false;
            }
        }

        private void dgvThietBi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string id = dgvThietBi.Rows[e.RowIndex].Cells["EquipmentID"].Value.ToString();
                string name = dgvThietBi.Rows[e.RowIndex].Cells["EquipmentName"].Value.ToString();
                FrmHoSoThietBi frm = new FrmHoSoThietBi(id, name);
                frm.ShowDialog();
            }
        }

        // =======================================================
        // 5. CÁC THAO TÁC NGHIỆP VỤ (THÊM, SỬA, XÓA, CẢNH BÁO)
        // =======================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTB.Text) || string.IsNullOrEmpty(txtTenTB.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ Mã và Tên thiết bị!"); return;
            }
            decimal price = 0; decimal.TryParse(txtGiaTien.Text, out price);

            string query = @"INSERT INTO Equipment (EquipmentID, EquipmentName, CategoryID, DepartmentID, ImportDate, PurchasePrice, Condition, UpdatedAt, IsDeleted) 
                             VALUES (@id, @name, @cat, @dept, @date, @price, @condition, GETDATE(), 0)";

            SqlParameter[] p = {
                new SqlParameter("@id", txtMaTB.Text), new SqlParameter("@name", txtTenTB.Text),
                new SqlParameter("@cat", cboLoaiTB.SelectedValue), new SqlParameter("@dept", cboKhoaPhong.SelectedValue),
                new SqlParameter("@date", dtpNgayNhap.Value), new SqlParameter("@price", price),
                new SqlParameter("@condition", cboTinhTrang.Text)
            };
            DataProvider.Instance.ExecuteNonQuery(query, p);
            MessageBox.Show("Thêm mới thành công!");
            LoadData(); btnLamMoi_Click(null, null);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEquipmentID)) { MessageBox.Show("Chọn thiết bị cần sửa!"); return; }
            decimal price = 0; decimal.TryParse(txtGiaTien.Text, out price);

            string query = @"UPDATE Equipment SET EquipmentName=@n, CategoryID=@c, DepartmentID=@d, 
                             ImportDate=@dt, PurchasePrice=@p, Condition=@cond, UpdatedAt=GETDATE() WHERE EquipmentID=@id";
            SqlParameter[] p = {
                new SqlParameter("@n", txtTenTB.Text), new SqlParameter("@c", cboLoaiTB.SelectedValue),
                new SqlParameter("@d", cboKhoaPhong.SelectedValue), new SqlParameter("@dt", dtpNgayNhap.Value),
                new SqlParameter("@p", price), new SqlParameter("@cond", cboTinhTrang.Text), new SqlParameter("@id", selectedEquipmentID)
            };
            DataProvider.Instance.ExecuteNonQuery(query, p);
            MessageBox.Show("Cập nhật thành công!");
            LoadData();
        }
        private void AdjustSearchLayout()
        {
            // Căn giữa Group tìm kiếm (Thanh Search + Nút Tìm)
            // Giả sử tổng chiều rộng của 2 cái là 630px (500 search + 10 gap + 120 button)
            int totalWidth = txtSearch.Width + 15 + btnDoSearch.Width;
            int startX = (pnlSearch.Width - totalWidth) / 2;

            txtSearch.Location = new Point(startX, (pnlSearch.Height - txtSearch.Height) / 2);
            btnDoSearch.Location = new Point(txtSearch.Right + 15, txtSearch.Location.Y);
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEquipmentID)) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string query = "UPDATE Equipment SET IsDeleted = 1 WHERE EquipmentID = @id";
                DataProvider.Instance.ExecuteNonQuery(query, new SqlParameter[] { new SqlParameter("@id", selectedEquipmentID) });
                MessageBox.Show("Đã xóa thiết bị!");
                LoadData(); btnLamMoi_Click(null, null);
            }
        }

        private void CheckMaintenanceAlert()
        {
            try
            {
                string q = "SELECT COUNT(*) FROM Equipment WHERE NgayBaoTriDinhKy <= GETDATE() AND Condition != N'Đã thanh lý' AND IsDeleted = 0";
                int count = (int)DataProvider.Instance.ExecuteScalar(q);
                if (count > 0) MessageBox.Show($"Hệ thống: Có {count} thiết bị cần bảo trì định kỳ!", "Cảnh báo bảo trì", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { }
        }
    }
}