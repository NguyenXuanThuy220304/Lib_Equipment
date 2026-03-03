using Lib_Equipment.Database;
using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmQuanLyDocGia : Form
    {
        private string selectedReaderID = "";

        public FrmQuanLyDocGia()
        {
            InitializeComponent();
        }

        private void FrmQuanLyDocGia_Load(object sender, EventArgs e)
        {
            LoadComboboxDonVi();
            LoadData();
        }

        // =======================================================
        // 1. TẢI DỮ LIỆU
        // =======================================================
        private void LoadComboboxDonVi()
        {
            string query = "SELECT DepartmentID, DepartmentName FROM Department WHERE IsDeleted = 0";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            cboDonVi.DataSource = dt;
            cboDonVi.DisplayMember = "DepartmentName";
            cboDonVi.ValueMember = "DepartmentID";
        }

        private void LoadData()
        {
            // Kết hợp bảng Reader và Department
            string query = @"
                SELECT r.ReaderID, r.FullName, d.DepartmentName, r.ReaderType, 
                       CASE WHEN r.Status = 1 THEN N'Hợp lệ' ELSE N'Bị khóa' END AS TrangThai, 
                       r.DepartmentID
                FROM Reader r
                LEFT JOIN Department d ON r.DepartmentID = d.DepartmentID
                WHERE r.IsDeleted = 0";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvDocGia.DataSource = dt;

            dgvDocGia.Columns["ReaderID"].HeaderText = "Mã Độc giả";
            dgvDocGia.Columns["ReaderID"].Width = 120;
            dgvDocGia.Columns["FullName"].HeaderText = "Họ và tên";
            dgvDocGia.Columns["FullName"].Width = 200;
            dgvDocGia.Columns["DepartmentName"].HeaderText = "Khoa/Viện";
            dgvDocGia.Columns["ReaderType"].HeaderText = "Loại thẻ";
            dgvDocGia.Columns["TrangThai"].HeaderText = "Trạng thái thẻ";

            dgvDocGia.Columns["DepartmentID"].Visible = false; // Ẩn khóa ngoại
        }

        // =======================================================
        // 2. SỰ KIỆN CHỌN DÒNG
        // =======================================================
        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDocGia.Rows[e.RowIndex];

                selectedReaderID = row.Cells["ReaderID"].Value.ToString();

                txtMaDocGia.Text = selectedReaderID;
                txtHoTen.Text = row.Cells["FullName"].Value.ToString();
                cboDonVi.SelectedValue = row.Cells["DepartmentID"].Value.ToString();
                cboLoaiDocGia.Text = row.Cells["ReaderType"].Value.ToString();
                cboTrangThai.Text = row.Cells["TrangThai"].Value.ToString();

                // Khóa không cho sửa Mã sinh viên
                txtMaDocGia.Enabled = false;
            }
        }

        // =======================================================
        // 3. THÊM ĐỘC GIẢ
        // =======================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDocGia.Text) || string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã độc giả và Họ tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int status = cboTrangThai.Text == "Hợp lệ" ? 1 : 0;
            string query = @"INSERT INTO Reader (ReaderID, FullName, DepartmentID, ReaderType, Status) 
                             VALUES (@id, @name, @dept, @type, @status)";

            SqlParameter[] param = {
                new SqlParameter("@id", txtMaDocGia.Text.Trim()),
                new SqlParameter("@name", txtHoTen.Text.Trim()),
                new SqlParameter("@dept", cboDonVi.SelectedValue),
                new SqlParameter("@type", cboLoaiDocGia.Text),
                new SqlParameter("@status", status)
            };

            try
            {
                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Thêm độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi (Mã độc giả có thể đã tồn tại): " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // 4. SỬA ĐỘC GIẢ
        // =======================================================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedReaderID))
            {
                MessageBox.Show("Vui lòng chọn một độc giả dưới bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int status = cboTrangThai.Text == "Hợp lệ" ? 1 : 0;
            string query = @"UPDATE Reader 
                             SET FullName = @name, DepartmentID = @dept, ReaderType = @type, Status = @status 
                             WHERE ReaderID = @id";

            SqlParameter[] param = {
                new SqlParameter("@name", txtHoTen.Text.Trim()),
                new SqlParameter("@dept", cboDonVi.SelectedValue),
                new SqlParameter("@type", cboLoaiDocGia.Text),
                new SqlParameter("@status", status),
                new SqlParameter("@id", selectedReaderID)
            };

            if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        // =======================================================
        // 5. XÓA MỀM (SOFT DELETE)
        // =======================================================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedReaderID)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa thẻ độc giả này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "UPDATE Reader SET IsDeleted = 1 WHERE ReaderID = @id";
                SqlParameter[] param = { new SqlParameter("@id", selectedReaderID) };

                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Đã xóa thẻ độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
        }

        // =======================================================
        // 6. LÀM MỚI
        // =======================================================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            selectedReaderID = "";
            txtMaDocGia.Enabled = true;
            txtMaDocGia.Clear();
            txtHoTen.Clear();

            if (cboDonVi.Items.Count > 0) cboDonVi.SelectedIndex = 0;
            cboLoaiDocGia.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
        }
    }
}