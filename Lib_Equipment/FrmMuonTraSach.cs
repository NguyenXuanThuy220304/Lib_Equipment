using Lib_Equipment.Database;
using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmMuonTraSach : Form
    {
        // Biến lưu ID của Phiếu mượn đang được chọn bên bảng trái
        private int selectedRecordID = -1;

        // Biến lưu ID của cuốn sách đang được chọn bên bảng phải (để trả)
        private string selectedCopyID = "";

        public FrmMuonTraSach()
        {
            InitializeComponent();
        }

        private void FrmMuonTraSach_Load(object sender, EventArgs e)
        {
            // Mặc định hạn trả là 14 ngày sau ngày mượn
            dtpHanTra.Value = DateTime.Now.AddDays(14);
            LoadDanhSachPhieuMuon();
        }

        // =======================================================
        // 1. TẢI DỮ LIỆU
        // =======================================================
        private void LoadDanhSachPhieuMuon()
        {
            string query = @"
                SELECT p.RecordID, p.ReaderID, d.FullName, p.BorrowDate, p.DueDate, p.Status
                FROM BorrowRecord p
                JOIN Reader d ON p.ReaderID = d.ReaderID
                WHERE p.IsDeleted = 0
                ORDER BY p.RecordID DESC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dgvPhieuMuon.DataSource = dt;

            dgvPhieuMuon.Columns["RecordID"].HeaderText = "Mã Phiếu";
            dgvPhieuMuon.Columns["RecordID"].Width = 60;
            dgvPhieuMuon.Columns["ReaderID"].Visible = false; // Ẩn mã độc giả
            dgvPhieuMuon.Columns["FullName"].HeaderText = "Tên Độc giả";
            dgvPhieuMuon.Columns["BorrowDate"].HeaderText = "Ngày mượn";
            dgvPhieuMuon.Columns["BorrowDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPhieuMuon.Columns["DueDate"].HeaderText = "Hạn trả";
            dgvPhieuMuon.Columns["DueDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPhieuMuon.Columns["Status"].HeaderText = "Trạng thái";
        }

        private void LoadChiTietPhieu(int recordId)
        {
            string query = @"
                SELECT ct.CopyID, s.Title, ct.ReturnDate, ct.ReturnCondition
                FROM BorrowDetail ct
                JOIN BookCopy c ON ct.CopyID = c.CopyID
                JOIN Book s ON c.BookID = s.BookID
                WHERE ct.RecordID = @recordId";

            SqlParameter[] param = { new SqlParameter("@recordId", recordId) };
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, param);
            dgvChiTiet.DataSource = dt;

            dgvChiTiet.Columns["CopyID"].HeaderText = "Mã vạch (Cuốn)";
            dgvChiTiet.Columns["Title"].HeaderText = "Tên Sách";
            dgvChiTiet.Columns["ReturnDate"].HeaderText = "Ngày trả";
            dgvChiTiet.Columns["ReturnDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvChiTiet.Columns["ReturnCondition"].HeaderText = "Tình trạng trả";
        }

        // =======================================================
        // 2. TẠO PHIẾU MƯỢN MỚI
        // =======================================================
        private void btnTaoPhieu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDocGia.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã độc giả!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // LƯU Ý: Trong thực tế, CreatedBy lấy từ biến Session tài khoản đang đăng nhập. 
            // Ở đây gán tạm = 1 (Tài khoản Admin) để chạy logic.
            int currentUserID = 1;

            // Cấu trúc OUTPUT INSERTED.RecordID giúp lấy ngay ID của phiếu vừa tạo ra
            string query = @"
                INSERT INTO BorrowRecord (ReaderID, CreatedBy, DueDate, Status) 
                OUTPUT INSERTED.RecordID
                VALUES (@reader, @user, @due, N'Borrowing')";

            SqlParameter[] param = {
                new SqlParameter("@reader", txtMaDocGia.Text.Trim()),
                new SqlParameter("@user", currentUserID),
                new SqlParameter("@due", dtpHanTra.Value)
            };

            try
            {
                // Dùng ExecuteScalar vì lệnh OUTPUT trả về 1 ô dữ liệu (ID)
                object result = DataProvider.Instance.ExecuteScalar(query, param);
                if (result != null)
                {
                    MessageBox.Show("Đã lập phiếu mượn số " + result.ToString() + ". Vui lòng chọn phiếu ở bảng và quét mã sách vào!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachPhieuMuon();
                    txtMaDocGia.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lập phiếu (Có thể Mã Độc Giả không tồn tại):\n" + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // 3. CHỌN PHIẾU BÊN TRÁI -> LOAD CHI TIẾT BÊN PHẢI
        // =======================================================
        private void dgvPhieuMuon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhieuMuon.Rows[e.RowIndex];
                selectedRecordID = Convert.ToInt32(row.Cells["RecordID"].Value);
                LoadChiTietPhieu(selectedRecordID);
            }
        }

        // Chọn sách trong chi tiết để chuẩn bị trả
        private void dgvChiTiet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedCopyID = dgvChiTiet.Rows[e.RowIndex].Cells["CopyID"].Value.ToString();
                txtMaCuonSach.Text = selectedCopyID; // Tự động điền mã lên ô Textbox
            }
        }

        // =======================================================
        // 4. QUÉT MÃ VẠCH THÊM SÁCH VÀO PHIẾU
        // =======================================================
        private void btnThemSach_Click(object sender, EventArgs e)
        {
            if (selectedRecordID == -1)
            {
                MessageBox.Show("Vui lòng chọn một Phiếu Mượn bên bảng trái trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string copyId = txtMaCuonSach.Text.Trim();
            if (string.IsNullOrEmpty(copyId)) return;

            // Kiểm tra xem sách này có đang rảnh không (Condition = 'Good')
            string checkQuery = "SELECT Condition FROM BookCopy WHERE CopyID = @copyId";
            object condition = DataProvider.Instance.ExecuteScalar(checkQuery, new SqlParameter[] { new SqlParameter("@copyId", copyId) });

            if (condition == null)
            {
                MessageBox.Show("Mã sách không tồn tại trong kho vật lý!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (condition.ToString() != "Good")
            {
                MessageBox.Show("Cuốn sách này hiện không rảnh (Đang mượn hoặc hỏng)!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Thêm vào chi tiết phiếu
            string insertQuery = "INSERT INTO BorrowDetail (RecordID, CopyID) VALUES (@record, @copy)";
            // 2. Đổi trạng thái cuốn sách thành Đang mượn
            string updateQuery = "UPDATE BookCopy SET Condition = N'Borrowed' WHERE CopyID = @copy";

            SqlParameter[] param = {
                new SqlParameter("@record", selectedRecordID),
                new SqlParameter("@copy", copyId)
            };

            try
            {
                DataProvider.Instance.ExecuteNonQuery(insertQuery, param);
                DataProvider.Instance.ExecuteNonQuery(updateQuery, new SqlParameter[] { new SqlParameter("@copy", copyId) });

                txtMaCuonSach.Clear();
                LoadChiTietPhieu(selectedRecordID); // Tải lại lưới bên phải
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm sách (Có thể sách đã có trong phiếu): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // 5. TRẢ SÁCH
        // =======================================================
        private void btnTraSach_Click(object sender, EventArgs e)
        {
            string copyId = txtMaCuonSach.Text.Trim();
            if (selectedRecordID == -1 || string.IsNullOrEmpty(copyId))
            {
                MessageBox.Show("Vui lòng chọn sách cần trả ở bảng bên phải!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Cập nhật ngày trả trong chi tiết phiếu
            string updateDetailQuery = @"
                UPDATE BorrowDetail 
                SET ReturnDate = GETDATE(), ReturnCondition = N'Good' 
                WHERE RecordID = @record AND CopyID = @copy AND ReturnDate IS NULL";

            // 2. Cập nhật trạng thái sách về lại Tốt (để người khác mượn tiếp)
            string updateBookQuery = "UPDATE BookCopy SET Condition = N'Good' WHERE CopyID = @copy";

            SqlParameter[] param = {
                new SqlParameter("@record", selectedRecordID),
                new SqlParameter("@copy", copyId)
            };

            int rowsAffected = DataProvider.Instance.ExecuteNonQuery(updateDetailQuery, param);

            if (rowsAffected > 0)
            {
                DataProvider.Instance.ExecuteNonQuery(updateBookQuery, new SqlParameter[] { new SqlParameter("@copy", copyId) });
                MessageBox.Show("Đã nhận trả sách thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaCuonSach.Clear();
                LoadChiTietPhieu(selectedRecordID);
            }
            else
            {
                MessageBox.Show("Cuốn sách này đã được trả từ trước hoặc không thuộc phiếu này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}