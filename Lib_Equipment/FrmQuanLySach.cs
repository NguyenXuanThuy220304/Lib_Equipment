using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmQuanLySach : Form
    {
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
            // SỬA SQL: Subquery tự động đếm số lượng bản sao đang tồn tại (không đếm sách Mất)
            string query = @"
                SELECT b.BookID, b.Title, b.Author, b.Publisher, b.PublishYear, c.CategoryName, b.CategoryID,
                       (SELECT COUNT(*) FROM BookCopy bc WHERE bc.BookID = b.BookID AND (bc.IsDeleted = 0 OR bc.IsDeleted IS NULL) AND bc.Status != N'Mất') AS [Số lượng]
                FROM Book b
                JOIN BookCategory c ON b.CategoryID = c.CategoryID
                WHERE b.IsDeleted = 0 OR b.IsDeleted IS NULL";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvSach.DataSource = dt;

            dgvSach.Columns["BookID"].HeaderText = "Mã Đầu sách";
            dgvSach.Columns["BookID"].Width = 100;
            dgvSach.Columns["Title"].HeaderText = "Tên sách";
            dgvSach.Columns["Title"].Width = 250;
            dgvSach.Columns["Author"].HeaderText = "Tác giả";
            dgvSach.Columns["Publisher"].HeaderText = "Nhà xuất bản";
            dgvSach.Columns["PublishYear"].HeaderText = "Năm XB";
            dgvSach.Columns["PublishYear"].Width = 80;
            dgvSach.Columns["CategoryName"].HeaderText = "Thể loại";
            dgvSach.Columns["CategoryID"].Visible = false;

            // Căn giữa cột Số lượng cho đẹp
            if (dgvSach.Columns.Contains("Số lượng"))
            {
                dgvSach.Columns["Số lượng"].Width = 90;
                dgvSach.Columns["Số lượng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSach.Columns["Số lượng"].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            }

        }

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

                txtMaSach.Enabled = false; // Không cho sửa khóa chính
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSach.Text) || string.IsNullOrEmpty(txtTenSach.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã đầu sách và Tên sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"INSERT INTO Book (BookID, Title, Author, Publisher, PublishYear, CategoryID, IsDeleted) 
                             VALUES (@id, @title, @author, @pub, @year, @cat, 0)";

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
                    MessageBox.Show("Thêm Đầu sách thành công!\n(Hãy sang form Nhập Kho để nhập sách vật lý)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: Mã sách đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedBookID)) return;

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
                MessageBox.Show("Cập nhật Đầu sách thành công!", "Thông báo");
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedBookID)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa đầu sách này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string query = "UPDATE Book SET IsDeleted = 1 WHERE BookID = @id";
                SqlParameter[] param = { new SqlParameter("@id", selectedBookID) };

                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Đã xóa sách khỏi danh mục!", "Thông báo");
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
        }

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