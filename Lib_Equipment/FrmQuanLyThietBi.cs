using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
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
        }

        // =======================================================
        // 1. TẢI DỮ LIỆU
        // =======================================================
        private void LoadComboboxLoaiTB()
        {
            string query = "SELECT CategoryID, CategoryName FROM EquipmentCategory WHERE IsDeleted = 0 OR IsDeleted IS NULL";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            cboLoaiTB.DataSource = dt;
            cboLoaiTB.DisplayMember = "CategoryName";
            cboLoaiTB.ValueMember = "CategoryID";
        }

        private void LoadComboboxKhoaPhong()
        {
            string query = "SELECT DepartmentID, DepartmentName FROM Department WHERE IsDeleted = 0 OR IsDeleted IS NULL";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            cboKhoaPhong.DataSource = dt;
            cboKhoaPhong.DisplayMember = "DepartmentName";
            cboKhoaPhong.ValueMember = "DepartmentID";
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

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvThietBi.DataSource = dt;

            dgvThietBi.Columns["EquipmentID"].HeaderText = "Mã TB";
            dgvThietBi.Columns["EquipmentID"].Width = 100;
            dgvThietBi.Columns["EquipmentName"].HeaderText = "Tên Thiết Bị";
            dgvThietBi.Columns["EquipmentName"].Width = 200;
            dgvThietBi.Columns["CategoryName"].HeaderText = "Phân Loại";
            dgvThietBi.Columns["DepartmentName"].HeaderText = "Khoa/Phòng";

            dgvThietBi.Columns["ImportDate"].HeaderText = "Ngày nhập";
            dgvThietBi.Columns["ImportDate"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvThietBi.Columns["PurchasePrice"].HeaderText = "Giá nhập (VNĐ)";
            dgvThietBi.Columns["PurchasePrice"].DefaultCellStyle.Format = "N0";

            dgvThietBi.Columns["Condition"].HeaderText = "Tình trạng";

            dgvThietBi.Columns["CategoryID"].Visible = false;
            dgvThietBi.Columns["DepartmentID"].Visible = false;
        }

        // =======================================================
        // 2. SỰ KIỆN CLICK BẢNG
        // =======================================================
        private void dgvThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThietBi.Rows[e.RowIndex];

                selectedEquipmentID = row.Cells["EquipmentID"].Value.ToString();

                txtMaTB.Text = selectedEquipmentID;
                txtTenTB.Text = row.Cells["EquipmentName"].Value.ToString();

                if (row.Cells["CategoryID"].Value != DBNull.Value)
                    cboLoaiTB.SelectedValue = row.Cells["CategoryID"].Value.ToString();

                if (row.Cells["DepartmentID"].Value != DBNull.Value)
                    cboKhoaPhong.SelectedValue = row.Cells["DepartmentID"].Value.ToString();

                if (row.Cells["ImportDate"].Value != DBNull.Value)
                    dtpNgayNhap.Value = Convert.ToDateTime(row.Cells["ImportDate"].Value);

                txtGiaTien.Text = row.Cells["PurchasePrice"].Value.ToString();
                cboTinhTrang.Text = row.Cells["Condition"].Value.ToString();

                txtMaTB.Enabled = false;
            }
        }

        // =======================================================
        // 3. THÊM MỚI
        // =======================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTB.Text) || string.IsNullOrEmpty(txtTenTB.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên thiết bị!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal price = 0;
            if (!string.IsNullOrEmpty(txtGiaTien.Text) && !decimal.TryParse(txtGiaTien.Text, out price))
            {
                MessageBox.Show("Giá tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = @"INSERT INTO Equipment (EquipmentID, EquipmentName, CategoryID, DepartmentID, ImportDate, PurchasePrice, Condition, UpdatedAt) 
                             VALUES (@id, @name, @cat, @dept, @date, @price, @condition, GETDATE())";

            SqlParameter[] param = {
                new SqlParameter("@id", txtMaTB.Text.Trim()),
                new SqlParameter("@name", txtTenTB.Text.Trim()),
                new SqlParameter("@cat", cboLoaiTB.SelectedValue ?? DBNull.Value),
                new SqlParameter("@dept", cboKhoaPhong.SelectedValue ?? DBNull.Value),
                new SqlParameter("@date", dtpNgayNhap.Value),
                new SqlParameter("@price", price),
                new SqlParameter("@condition", cboTinhTrang.Text)
            };

            try
            {
                DataProvider.Instance.ExecuteNonQuery(query, param);
                MessageBox.Show("Thêm thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                btnLamMoi_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // 4. CẬP NHẬT
        // =======================================================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEquipmentID))
            {
                MessageBox.Show("Vui lòng chọn thiết bị ở bảng để sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal price = 0;
            decimal.TryParse(txtGiaTien.Text, out price);

            string query = @"UPDATE Equipment 
                             SET EquipmentName = @name, CategoryID = @cat, DepartmentID = @dept, 
                                 ImportDate = @date, PurchasePrice = @price, Condition = @condition, UpdatedAt = GETDATE() 
                             WHERE EquipmentID = @id";

            SqlParameter[] param = {
                new SqlParameter("@name", txtTenTB.Text.Trim()),
                new SqlParameter("@cat", cboLoaiTB.SelectedValue ?? DBNull.Value),
                new SqlParameter("@dept", cboKhoaPhong.SelectedValue ?? DBNull.Value),
                new SqlParameter("@date", dtpNgayNhap.Value),
                new SqlParameter("@price", price),
                new SqlParameter("@condition", cboTinhTrang.Text),
                new SqlParameter("@id", selectedEquipmentID)
            };

            DataProvider.Instance.ExecuteNonQuery(query, param);
            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        // =======================================================
        // 5. XÓA MỀM (Soft Delete)
        // =======================================================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEquipmentID)) return;

            if (MessageBox.Show("Bạn có chắc muốn xóa thiết bị này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "UPDATE Equipment SET IsDeleted = 1, UpdatedAt = GETDATE() WHERE EquipmentID = @id";
                DataProvider.Instance.ExecuteNonQuery(query, new SqlParameter[] { new SqlParameter("@id", selectedEquipmentID) });

                MessageBox.Show("Đã xóa thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                btnLamMoi_Click(null, null);
            }
        }

        // =======================================================
        // 6. LÀM MỚI
        // =======================================================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            selectedEquipmentID = "";
            txtMaTB.Enabled = true;
            txtMaTB.Clear();
            txtTenTB.Clear();
            txtGiaTien.Clear();
            dtpNgayNhap.Value = DateTime.Now;

            if (cboLoaiTB.Items.Count > 0) cboLoaiTB.SelectedIndex = 0;
            if (cboKhoaPhong.Items.Count > 0) cboKhoaPhong.SelectedIndex = 0;
            if (cboTinhTrang.Items.Count > 0) cboTinhTrang.SelectedIndex = 0;
        }
    }
}