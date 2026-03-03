using Lib_Equipment.Database;
using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmQuanLySach : Form
    {
        // Biến cờ kiểm tra xem đang chọn dòng nào để Sửa/Xóa
        private string selectedBookID = "";

        public FrmQuanLySach()
        {
            InitializeComponent();
        }

        private void FrmQuanLySach_Load(object sender, EventArgs e)
        {
            LoadComboboxTheLoai();
            LoadData();
        }

        // =======================================================
        // 1. TẢI DỮ LIỆU
        // =======================================================
        private void LoadComboboxTheLoai()
        {
            string query = "SELECT CategoryID, CategoryName FROM BookCategory WHERE IsDeleted = 0";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            cboTheLoai.DataSource = dt;
            cboTheLoai.DisplayMember = "CategoryName";
            cboTheLoai.ValueMember = "CategoryID";
        }

        private void LoadData()
        {
            // JOIN bảng Book và BookCategory để lấy tên thể loại thay vì mã số
            string query = @"
                SELECT b.BookID, b.Title, b.Author, b.Publisher, b.PublishYear, c.CategoryName, b.CategoryID
                FROM Book b
                JOIN BookCategory c ON b.CategoryID = c.CategoryID
                WHERE b.IsDeleted = 0";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvSach.DataSource = dt;

            // Đổi tên cột tiếng Việt cho thân thiện với người dùng
            dgvSach.Columns["BookID"].HeaderText = "Mã sách";
            dgvSach.Columns["BookID"].Width = 100;
            dgvSach.Columns["Title"].HeaderText = "Tên sách";
            dgvSach.Columns["Title"].Width = 250;
            dgvSach.Columns["Author"].HeaderText = "Tác giả";
            dgvSach.Columns["Publisher"].HeaderText = "Nhà xuất bản";
            dgvSach.Columns["PublishYear"].HeaderText = "Năm XB";
            dgvSach.Columns["PublishYear"].Width = 80;
            dgvSach.Columns["CategoryName"].HeaderText = "Thể loại";

            dgvSach.Columns["CategoryID"].Visible = false; // Ẩn cột ID khóa ngoại
        }

        // =======================================================
        // 2. CHỌN SÁCH TỪ BẢNG (CELL CLICK)
        // =======================================================
        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSach.Rows[e.RowIndex];

                selectedBookID = row.Cells["BookID"].Value.ToString();

                txtMaSach.Text = selectedBookID;
                txtTenSach.Text = row.Cells["Title"].Value.ToString();
                txtTacGia.Text = row.Cells["Author"].Value.ToString();
                txtNhaXuatBan.Text = row.Cells["Publisher"].Value.ToString();
                txtNamXuatBan.Text = row.Cells["PublishYear"].Value.ToString();
                cboTheLoai.SelectedValue = row.Cells["CategoryID"].Value.ToString();

                // Khóa không cho sửa Mã đầu sách (Khóa chính)
                txtMaSach.Enabled = false;
            }
        }

        // =======================================================
        // 3. THÊM SÁCH MỚI
        // =======================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSach.Text) || string.IsNullOrEmpty(txtTenSach.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã sách và Tên sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"INSERT INTO Book (BookID, Title, Author, Publisher, PublishYear, CategoryID) 
                             VALUES (@id, @title, @author, @pub, @year, @cat)";

            // Xử lý chuyển đổi năm xuất bản an toàn
            int year = 0;
            int.TryParse(txtNamXuatBan.Text.Trim(), out year);

            SqlParameter[] param = {
                new SqlParameter("@id", txtMaSach.Text.Trim()),
                new SqlParameter("@title", txtTenSach.Text.Trim()),
                new SqlParameter("@author", txtTacGia.Text.Trim()),
                new SqlParameter("@pub", txtNhaXuatBan.Text.Trim()),
                new SqlParameter("@year", year == 0 ? (object)DBNull.Value : year),
                new SqlParameter("@cat", cboTheLoai.SelectedValue)
            };

            try
            {
                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Thêm sách mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi (Mã sách có thể đã tồn tại): " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // 4. SỬA THÔNG TIN SÁCH
        // =======================================================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedBookID))
            {
                MessageBox.Show("Vui lòng chọn một đầu sách dưới bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"UPDATE Book 
                             SET Title = @title, Author = @author, Publisher = @pub, PublishYear = @year, CategoryID = @cat 
                             WHERE BookID = @id";

            int year = 0;
            int.TryParse(txtNamXuatBan.Text.Trim(), out year);

            SqlParameter[] param = {
                new SqlParameter("@title", txtTenSach.Text.Trim()),
                new SqlParameter("@author", txtTacGia.Text.Trim()),
                new SqlParameter("@pub", txtNhaXuatBan.Text.Trim()),
                new SqlParameter("@year", year == 0 ? (object)DBNull.Value : year),
                new SqlParameter("@cat", cboTheLoai.SelectedValue),
                new SqlParameter("@id", selectedBookID)
            };

            if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
            {
                MessageBox.Show("Cập nhật thông tin sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        // =======================================================
        // 5. XÓA SÁCH (SOFT DELETE)
        // =======================================================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedBookID)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa đầu sách này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Soft delete: Chỉ update cờ IsDeleted
                string query = "UPDATE Book SET IsDeleted = 1 WHERE BookID = @id";
                SqlParameter[] param = { new SqlParameter("@id", selectedBookID) };

                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Đã xóa sách khỏi danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            selectedBookID = "";
            txtMaSach.Enabled = true;
            txtMaSach.Clear();
            txtTenSach.Clear();
            txtTacGia.Clear();
            txtNhaXuatBan.Clear();
            txtNamXuatBan.Clear();
            if (cboTheLoai.Items.Count > 0) cboTheLoai.SelectedIndex = 0;
        }
    }
}