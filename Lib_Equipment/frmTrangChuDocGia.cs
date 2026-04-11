using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class frmTrangChuDocGia : Form
    {
        private string currentReaderID;
        private string currentFullName;

        // Điền API Key Gemini của bạn vào đây để truyền cho Form Phụ dùng
        private readonly string API_KEY = AppSecrets.GeminiApiKey;

        public frmTrangChuDocGia(string readerID, string fullName)
        {
            InitializeComponent();
            currentReaderID = readerID;
            currentFullName = fullName;
            string query = "SELECT ISNULL(Balance, 0) FROM Reader WHERE ReaderID = @id";
            SqlParameter[] param = { new SqlParameter("@id", readerID) };
            object balanceObj = DataProvider.Instance.ExecuteScalar(query, param);
            decimal balance = Convert.ToDecimal(balanceObj);

            // Hiển thị thông tin chào mừng và số dư
            lblWelcome.Text = $"Xin chào, {currentFullName} | Mã: {currentReaderID}";

            // Giả sử bạn thêm 1 Label tên là lblBalance để hiện số tiền
            lblBalance.Text = $"Số dư tài khoản: {balance:N0} VNĐ";

            // Đổi màu: Đỏ nếu nợ (âm tiền), Xanh nếu có tiền dư
            lblBalance.ForeColor = balance < 0 ? Color.Red : Color.Green;

            // === GẮN SỰ KIỆN CLICK CHO KHU VỰC MÀU CAM ===
            pnlBarcodeCard.Cursor = Cursors.Hand;
            lblBarcodeTitle.Cursor = Cursors.Hand;
            lblBarcodeDesc.Cursor = Cursors.Hand;

            pnlBarcodeCard.Click += HandleOpenBorrowForm_Click;
            lblBarcodeTitle.Click += HandleOpenBorrowForm_Click;
            lblBarcodeDesc.Click += HandleOpenBorrowForm_Click;
        }

        private void frmTrangChuDocGia_Load(object sender, EventArgs e)
        {
            LoadLichSuMuon();
        }

        // =====================================================================
        // MỞ FORM PHỤ: KHI BẤM VÀO THẺ MÀU CAM
        // =====================================================================
        private void HandleOpenBorrowForm_Click(object sender, EventArgs e)
        {
            // Mở form phụ lên, ép người dùng xử lý xong mượn sách rồi mới cho quay lại
            frmSecondaryBorrow frmPhu = new frmSecondaryBorrow(currentReaderID, currentFullName, API_KEY);
            frmPhu.ShowDialog();

            // Cập nhật lại Lưới dữ liệu ngay khi Form phụ đóng (để hiện sách vừa mượn)
            LoadLichSuMuon();
        }

        // =====================================================================
        // CHỨC NĂNG LỊCH SỬ MƯỢN TRẢ
        // =====================================================================
        private void LoadLichSuMuon()
        {
            lblGridTitle.Text = "📚 Lịch sử mượn sách của bạn";
            txtSearch.Clear();

            // [FIX LỖI]: Dùng CASE WHEN để dịch trạng thái tự động dựa vào việc đã trả hay chưa (ReturnDate)
            string query = $@"
                SELECT 
                    br.RecordID AS [Mã Phiếu],
                    b.Title AS [Tên Sách],
                    br.BorrowDate AS [Ngày Mượn],
                    br.DueDate AS [Hạn Trả],
                    CASE 
                        WHEN bd.ReturnDate IS NULL THEN N'Đang mượn'
                        ELSE N'Đã trả'
                    END AS [Trạng Thái]
                FROM BorrowRecord br
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID
                JOIN BookCopy bc ON bd.CopyID = bc.CopyID
                JOIN Book b ON bc.BookID = b.BookID
                WHERE br.ReaderID = @readerId AND br.IsDeleted = 0
                ORDER BY br.BorrowDate DESC";

            SqlParameter[] param = { new SqlParameter("@readerId", currentReaderID) };
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, param);
            dgvMain.DataSource = dt;
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            LoadLichSuMuon();
        }

        // =====================================================================
        // TÌM KIẾM THÔNG MINH (Smart Search)
        // =====================================================================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            TimKiemSachThongMinh();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TimKiemSachThongMinh();
            }
        }

        private void TimKiemSachThongMinh()
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadLichSuMuon();
                return;
            }

            lblGridTitle.Text = $"🔍 Kết quả tìm kiếm cho: '{keyword}'";

            // 1. Tách từ khóa người dùng gõ thành các từ đơn lẻ (Loại bỏ khoảng trắng thừa)
            string[] words = keyword.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string whereClause = "b.IsDeleted = 0 ";

            // Khai báo List Parameter (Sử dụng đường dẫn đầy đủ vì bạn chưa using System.Collections.Generic)
            System.Collections.Generic.List<SqlParameter> paramList = new System.Collections.Generic.List<SqlParameter>();

            // 2. Thuật toán tạo chuỗi truy vấn thông minh (Smart Query)
            for (int i = 0; i < words.Length; i++)
            {
                string paramName = $"@w{i}";

                // MẸO CỰC HAY: Thêm COLLATE SQL_Latin1_General_CP1_CI_AI để:
                // - CI (Case Insensitive): Không phân biệt hoa/thường (a = A)
                // - AI (Accent Insensitive): Không phân biệt dấu tiếng Việt (a = á = ã = ạ)
                whereClause += $@" 
                    AND (
                        b.Title COLLATE SQL_Latin1_General_CP1_CI_AI LIKE {paramName} OR 
                        b.Author COLLATE SQL_Latin1_General_CP1_CI_AI LIKE {paramName} OR 
                        bcg.CategoryName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE {paramName}
                    ) ";

                // Bọc % ở 2 đầu để tìm kiếm gần đúng (Chứa ký tự)
                paramList.Add(new SqlParameter(paramName, $"%{words[i]}%"));
            }

            // 3. Lắp ráp câu lệnh SQL hoàn chỉnh
            string query = $@"
                SELECT 
                    b.Title AS [Tên Sách], 
                    b.Author AS [Tác Giả], 
                    bcg.CategoryName AS [Thể Loại],
                    b.Publisher AS [Nhà Xuất Bản],
                    (SELECT COUNT(*) FROM BookCopy bc 
                     WHERE bc.BookID = b.BookID AND bc.Status = N'Có sẵn' AND bc.IsDeleted = 0) AS [Số quyển sẵn sàng]
                FROM Book b
                LEFT JOIN BookCategory bcg ON b.CategoryID = bcg.CategoryID
                WHERE {whereClause}
                ORDER BY b.Title ASC";

            // 4. Truy xuất Database với Parameter mảng để chống SQL Injection
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, paramList.ToArray());
            dgvMain.DataSource = dt;
        }

        private void btnLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                new FrmLogin().ShowDialog();
                this.Close();
            }
        }
    }
}