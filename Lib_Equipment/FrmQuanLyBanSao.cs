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
            cboViTri.Items.Clear();
            cboViTri.Items.Add("Kho mượn (Được mang về)");
            cboViTri.Items.Add("Phòng đọc (Đọc tại chỗ)");
            cboViTri.Items.Add("Kho lưu trữ (Sách cũ)");
            cboViTri.SelectedIndex = 0;

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Có sẵn");
            cboTrangThai.Items.Add("Đang mượn");
            cboTrangThai.Items.Add("Hỏng");
            cboTrangThai.Items.Add("Mất");
            cboTrangThai.SelectedIndex = 0;
        }

        private void LoadData()
        {
            string query = @"
                SELECT bc.CopyID AS [Mã Bản Sao], 
                       bc.BookID AS [Mã Sách Gốc], 
                       b.Title AS [Tên Sách],
                       bc.Location AS [Vị trí Kho], 
                       bc.Status AS [Trạng thái]
                FROM BookCopy bc
                JOIN Book b ON bc.BookID = b.BookID
                WHERE bc.IsDeleted = 0 OR bc.IsDeleted IS NULL";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvBanSao.DataSource = dt;
        }

        private void dgvBanSao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBanSao.Rows[e.RowIndex];
                selectedCopyID = row.Cells["Mã Bản Sao"].Value.ToString();

                txtMaBanSao.Text = selectedCopyID;
                txtMaSach.Text = row.Cells["Mã Sách Gốc"].Value.ToString();
                cboViTri.Text = row.Cells["Vị trí Kho"].Value.ToString();
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

            string query = @"INSERT INTO BookCopy (CopyID, BookID, Location, Status, CreatedAt, IsDeleted) 
                             VALUES (@copy, @book, @loc, @status, GETDATE(), 0)";

            SqlParameter[] param = {
                new SqlParameter("@copy", txtMaBanSao.Text.Trim()),
                new SqlParameter("@book", txtMaSach.Text.Trim()),
                new SqlParameter("@loc", cboViTri.Text),
                new SqlParameter("@status", cboTrangThai.Text)
            };

            try
            {
                if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
                {
                    MessageBox.Show("Nhập kho sách thành công!", "Thông báo");
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: Mã bản sao đã tồn tại!", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCopyID))
            {
                MessageBox.Show("Vui lòng chọn bản sao cần sửa/luân chuyển!", "Cảnh báo"); return;
            }

            string query = @"UPDATE BookCopy 
                             SET Location = @loc, Status = @status 
                             WHERE CopyID = @copy";

            SqlParameter[] param = {
                new SqlParameter("@loc", cboViTri.Text),
                new SqlParameter("@status", cboTrangThai.Text),
                new SqlParameter("@copy", selectedCopyID)
            };

            if (DataProvider.Instance.ExecuteNonQuery(query, param) > 0)
            {
                MessageBox.Show("Luân chuyển/Cập nhật sách thành công!", "Thông báo");
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCopyID)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa bản sao này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            cboViTri.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
        }
    }
}