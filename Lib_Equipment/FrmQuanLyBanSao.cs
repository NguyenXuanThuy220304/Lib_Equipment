using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmQuanLyBanSao : Form
    {
        private string selectedCopyID = "";

        public FrmQuanLyBanSao()
        {
            InitializeComponent();
        }

        private void FrmQuanLyBanSao_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            LoadData();
        }

        private void LoadCombobox()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Có sẵn");
            cboTrangThai.Items.Add("Đang mượn");
            cboTrangThai.Items.Add("Hỏng");
            cboTrangThai.Items.Add("Mất");
            cboTrangThai.SelectedIndex = 0;
        }

        private void LoadData()
        {
            // BỎ COLUMN LOCATION ĐI, CHỈ CÒN TRẠNG THÁI
            string query = @"
                SELECT bc.CopyID AS [Mã Bản Sao], 
                       bc.BookID AS [Mã Sách Gốc], 
                       b.Title AS [Tên Sách],
                       bc.Status AS [Trạng thái]
                FROM BookCopy bc
                JOIN Book b ON bc.BookID = b.BookID
                WHERE bc.IsDeleted = 0 OR bc.IsDeleted IS NULL";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvBanSao.DataSource = dt;

            if (dgvBanSao.Columns.Contains("Tên Sách"))
            {
                dgvBanSao.Columns["Tên Sách"].Width = 350;
            }
        }

        private void dgvBanSao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBanSao.Rows[e.RowIndex];
                selectedCopyID = row.Cells["Mã Bản Sao"].Value.ToString();

                txtMaBanSao.Text = selectedCopyID;
                txtMaSach.Text = row.Cells["Mã Sách Gốc"].Value.ToString();
                cboTrangThai.Text = row.Cells["Trạng thái"].Value.ToString();

                txtMaBanSao.Enabled = false; // Khóa mã bản sao khi sửa
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaBanSao.Text) || string.IsNullOrEmpty(txtMaSach.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã cuốn vật lý và Mã đầu sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem đầu sách có tồn tại không
            string checkBook = "SELECT COUNT(*) FROM Book WHERE BookID = @bookId";
            SqlParameter[] pCheck = { new SqlParameter("@bookId", txtMaSach.Text.Trim()) };
            int count = (int)DataProvider.Instance.ExecuteScalar(checkBook, pCheck);

            if (count == 0)
            {
                MessageBox.Show("Mã đầu sách không tồn tại. Vui lòng tạo Đầu sách trước!", "Lỗi");
                return;
            }

            // SQL MỚI KHÔNG CÒN LOCATION
            string query = @"INSERT INTO BookCopy (CopyID, BookID, Status, CreatedAt, IsDeleted) 
                             VALUES (@copy, @book, @status, GETDATE(), 0)";

            SqlParameter[] param = {
                new SqlParameter("@copy", txtMaBanSao.Text.Trim()),
                new SqlParameter("@book", txtMaSach.Text.Trim()),
                new SqlParameter("@status", cboTrangThai.Text)
            };

            try
            {
                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Tạo mã vạch sách thành công!", "Thông báo");
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: Mã bản sao này đã tồn tại trong kho!", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCopyID))
            {
                MessageBox.Show("Vui lòng chọn bản sao cần cập nhật trạng thái!", "Cảnh báo"); return;
            }

            // CHỈ UPDATE STATUS
            string query = @"UPDATE BookCopy 
                             SET Status = @status 
                             WHERE CopyID = @copy";

            SqlParameter[] param = {
                new SqlParameter("@status", cboTrangThai.Text),
                new SqlParameter("@copy", selectedCopyID)
            };

            if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
            {
                MessageBox.Show("Cập nhật trạng thái sách thành công!", "Thông báo");
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCopyID)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa (hủy) mã vạch sách này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string query = "UPDATE BookCopy SET IsDeleted = 1 WHERE CopyID = @copy";
                SqlParameter[] param = { new SqlParameter("@copy", selectedCopyID) };

                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Đã xóa bản sao khỏi kho!", "Thông báo");
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            selectedCopyID = "";
            txtMaBanSao.Enabled = true;
            txtMaBanSao.Clear();
            txtMaSach.Clear();
            cboTrangThai.SelectedIndex = 0;
        }
    }
}