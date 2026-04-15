using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic; // Cần thiết để dùng List<SqlParameter>

namespace Lib_Equipment
{
    public partial class frmTrangChuDocGia : Form
    {
        private string currentReaderID;
        private string currentFullName;
        private readonly string API_KEY = AppSecrets.GeminiApiKey;

        private Button btnHanhDongGiaHan;
        private Button btnHanhDongTraSach;

        public frmTrangChuDocGia(string readerID, string fullName)
        {
            InitializeComponent();
            currentReaderID = readerID;
            currentFullName = fullName;
            this.KeyPreview = true;

            UpdateDebtDisplay();
            lblWelcome.Text = $"Xin chào, {currentFullName} | Mã: {currentReaderID}";

            // Gắn sự kiện click cho thẻ mượn sách
            pnlBarcodeCard.Cursor = Cursors.Hand;
            lblBarcodeTitle.Cursor = Cursors.Hand;
            lblBarcodeDesc.Cursor = Cursors.Hand;

            pnlBarcodeCard.Click += HandleOpenBorrowForm_Click;
            lblBarcodeTitle.Click += HandleOpenBorrowForm_Click;
            lblBarcodeDesc.Click += HandleOpenBorrowForm_Click;

            TaoNutBanhDongThongMinh();
            dgvMain.SelectionChanged += DgvMain_SelectionChanged;
        }

        private void frmTrangChuDocGia_Load(object sender, EventArgs e) { LoadLichSuMuon(); }

        private void UpdateDebtDisplay()
        {
            string query = "SELECT ISNULL(AcademicDebt, 0) FROM Reader WHERE ReaderID = @id";
            decimal debt = Convert.ToDecimal(DataProvider.Instance.ExecuteScalar(query, new SqlParameter[] { new SqlParameter("@id", currentReaderID) }));

            if (debt > 0)
            {
                lblCongNo.Text = $"Công nợ hiện tại: {debt:N0} VNĐ (Vui lòng thanh toán tại quầy)";
                lblCongNo.ForeColor = Color.Red;
            }
            else
            {
                lblCongNo.Text = "Công nợ: 0 VNĐ (Trạng thái bình thường)";
                lblCongNo.ForeColor = Color.Green;
            }
        }

        private void HandleOpenBorrowForm_Click(object sender, EventArgs e)
        {
            frmSecondaryBorrow frmPhu = new frmSecondaryBorrow(currentReaderID, currentFullName, API_KEY);
            frmPhu.ShowDialog();
            LoadLichSuMuon();
            UpdateDebtDisplay();
        }

        private void TaoNutBanhDongThongMinh()
        {
            btnHanhDongGiaHan = new Button() { Text = "⏳ GIA HẠN SÁCH NÀY", Size = new Size(180, 40), BackColor = Color.FromArgb(255, 193, 7), Font = new Font("Segoe UI", 10, FontStyle.Bold), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Visible = false };
            btnHanhDongGiaHan.Location = new Point(dgvMain.Right - 380, dgvMain.Top - 50);
            btnHanhDongGiaHan.Click += BtnHanhDongGiaHan_Click;
            this.Controls.Add(btnHanhDongGiaHan);

            btnHanhDongTraSach = new Button() { Text = "📦 TỰ TRẢ SÁCH NÀY", Size = new Size(180, 40), BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Visible = false };
            btnHanhDongTraSach.Location = new Point(dgvMain.Right - 180, dgvMain.Top - 50);
            btnHanhDongTraSach.Click += BtnHanhDongTraSach_Click;
            this.Controls.Add(btnHanhDongTraSach);
        }

        // =====================================================================
        // XỬ LÝ SỰ KIỆN CHỌN DÒNG (AN TOÀN)
        // =====================================================================
        private void DgvMain_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null || dgvMain.CurrentRow.Index < 0 || !dgvMain.Columns.Contains("Trạng Thái"))
            {
                btnHanhDongGiaHan.Visible = btnHanhDongTraSach.Visible = false;
                return;
            }

            DataGridViewRow row = dgvMain.CurrentRow;
            string trangThai = row.Cells["Trạng Thái"].Value?.ToString();

            if (trangThai == "Đang mượn" && dgvMain.Columns.Contains("Hạn Trả"))
            {
                DateTime dueDate = Convert.ToDateTime(row.Cells["Hạn Trả"].Value);
                int daysLeft = (dueDate.Date - DateTime.Now.Date).Days;
                btnHanhDongTraSach.Visible = true;
                btnHanhDongGiaHan.Visible = (daysLeft >= 3);
            }
            else
            {
                btnHanhDongGiaHan.Visible = btnHanhDongTraSach.Visible = false;
            }
        }

        // =====================================================================
        // TÌM KIẾM THÔNG MINH (VIẾT TẮT + VỊ TRÍ)
        // =====================================================================
        private void btnSearch_Click(object sender, EventArgs e) { TimKiemSachThongMinh(); }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; TimKiemSachThongMinh(); } }

        private void TimKiemSachThongMinh()
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) { LoadLichSuMuon(); return; }

            lblGridTitle.Text = $"🔍 Tìm kiếm kho sách: '{keyword}'";

            // 1. Tạo Pattern viết tắt (Ví dụ: "CNTT" -> "%C%N%T%T%")
            string acronym = "%";
            foreach (char c in keyword) if (char.IsLetterOrDigit(c)) acronym += c + "%";

            // 2. Tạo điều kiện lọc từng từ cho tìm kiếm chi tiết
            string[] words = keyword.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<SqlParameter> paramList = new List<SqlParameter>();
            string wordConditions = "";

            for (int i = 0; i < words.Length; i++)
            {
                string pName = $"@word{i}";
                // COLLATE giúp tìm kiếm không dấu/có dấu đều ra
                wordConditions += $"(b.Title COLLATE SQL_Latin1_General_CP1_CI_AI LIKE {pName} " +
                                  $"OR b.Author COLLATE SQL_Latin1_General_CP1_CI_AI LIKE {pName} " +
                                  $"OR bcg.CategoryName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE {pName})";

                if (i < words.Length - 1) wordConditions += " AND ";
                paramList.Add(new SqlParameter(pName, $"%{words[i]}%"));
            }

            // Nạp tham số viết tắt vào bộ lọc
            paramList.Add(new SqlParameter("@acronym", acronym));

            // 3. Câu lệnh SQL: Thêm điều kiện so khớp @acronym với cả Thể loại
            string query = $@"
        SELECT 
            b.CabinetLocation AS [Vị trí kệ],
            b.Title AS [Tên Sách], 
            b.Author AS [Tác Giả], 
            bcg.CategoryName AS [Thể Loại],
            (SELECT COUNT(*) FROM BookCopy bc 
             WHERE bc.BookID = b.BookID AND bc.Status = N'Có sẵn' AND bc.IsDeleted = 0) AS [Số quyển sẵn sàng]
        FROM Book b
        LEFT JOIN BookCategory bcg ON b.CategoryID = bcg.CategoryID
        WHERE b.IsDeleted = 0 
          AND (
                ({wordConditions})           -- Tìm theo từng từ
                OR b.Title LIKE @acronym     -- Viết tắt tên sách
                OR bcg.CategoryName LIKE @acronym -- VIẾT TẮT THỂ LOẠI (MỚI THÊM)
              )
        ORDER BY b.Title ASC";

            // 4. Thực thi và hiển thị
            dgvMain.DataSource = DataProvider.Instance.ExecuteQuery(query, paramList.ToArray());

            // Bôi đỏ in đậm cột Vị trí kệ cho rực rỡ
            DinhDangCotViTri();
        }

        private void LoadLichSuMuon()
        {
            lblGridTitle.Text = "📚 Lịch sử mượn sách của bạn";
            string query = @"
                SELECT 
                    b.CabinetLocation AS [Vị trí kệ],
                    br.RecordID AS [Mã Phiếu], bc.CopyID AS [Mã Bản Sao], b.Title AS [Tên Sách], 
                    br.BorrowDate AS [Ngày Mượn], br.DueDate AS [Hạn Trả],
                    CASE WHEN bd.ReturnDate IS NULL THEN N'Đang mượn' ELSE N'Đã trả' END AS [Trạng Thái]
                FROM BorrowRecord br
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID
                JOIN BookCopy bc ON bd.CopyID = bc.CopyID
                JOIN Book b ON bc.BookID = b.BookID
                WHERE br.ReaderID = @readerId AND br.IsDeleted = 0
                ORDER BY br.BorrowDate DESC";

            dgvMain.DataSource = DataProvider.Instance.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@readerId", currentReaderID) });
            DinhDangCotViTri();
        }

        private void DinhDangCotViTri()
        {
            if (dgvMain.Columns.Contains("Vị trí kệ"))
            {
                dgvMain.Columns["Vị trí kệ"].DisplayIndex = 0; // Đẩy lên đầu bảng
                dgvMain.Columns["Vị trí kệ"].DefaultCellStyle.ForeColor = Color.Red;
                dgvMain.Columns["Vị trí kệ"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvMain.Columns["Vị trí kệ"].Width = 110;
            }
        }

        // --- Các hàm Gia hạn/Trả sách (Giữ nguyên logic của bạn nhưng thêm check an toàn) ---
        private void BtnHanhDongGiaHan_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvMain.CurrentRow;
            string recordId = row.Cells["Mã Phiếu"].Value.ToString();
            string copyId = row.Cells["Mã Bản Sao"].Value.ToString();
            string tenSach = row.Cells["Tên Sách"].Value.ToString();

            string typeQuery = "SELECT ReaderType FROM Reader WHERE ReaderID = @id";
            string readerType = DataProvider.Instance.ExecuteScalar(typeQuery, new SqlParameter[] { new SqlParameter("@id", currentReaderID) })?.ToString() ?? "Sinh viên";
            int addDays = readerType.Contains("Giảng viên") ? 21 : 14;

            if (MessageBox.Show($"Xác nhận gia hạn thêm {addDays} ngày cho: {tenSach}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (XacNhanBangMaVach("GIA HẠN", tenSach, copyId))
                {
                    DataProvider.Instance.ExecuteNonQuery("UPDATE BorrowRecord SET DueDate = DATEADD(day, @days, DueDate) WHERE RecordID = @id", new SqlParameter[] { new SqlParameter("@days", addDays), new SqlParameter("@id", recordId) });
                    MessageBox.Show("Gia hạn thành công!"); LoadLichSuMuon();
                }
            }
        }

        private void BtnHanhDongTraSach_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvMain.CurrentRow;
            string recordId = row.Cells["Mã Phiếu"].Value.ToString();
            string copyId = row.Cells["Mã Bản Sao"].Value.ToString();
            string tenSach = row.Cells["Tên Sách"].Value.ToString();
            DateTime dueDate = Convert.ToDateTime(row.Cells["Hạn Trả"].Value);

            if (MessageBox.Show("Bạn xác nhận trả cuốn sách này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (XacNhanBangMaVach("TRẢ SÁCH", tenSach, copyId))
                {
                    int lateDays = (DateTime.Now.Date - dueDate.Date).Days;
                    decimal fine = (lateDays > 0) ? lateDays * 2000 : 0;

                    // Thực thi SQL trả sách (Transaction ngầm)
                    string sql = @"UPDATE BorrowDetail SET ReturnDate = GETDATE(), FineAmount = @fine WHERE RecordID = @rec AND CopyID = @copy;
                                   UPDATE BookCopy SET Status = N'Có sẵn' WHERE CopyID = @copy;
                                   IF @fine > 0 UPDATE Reader SET AcademicDebt = ISNULL(AcademicDebt, 0) + @fine WHERE ReaderID = @reader";

                    DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@fine", fine), new SqlParameter("@rec", recordId), new SqlParameter("@copy", copyId), new SqlParameter("@reader", currentReaderID) });
                    MessageBox.Show(fine > 0 ? $"Đã trả! Phạt quá hạn: {fine:N0} VNĐ" : "Trả sách thành công!");
                    LoadLichSuMuon(); UpdateDebtDisplay();
                }
            }
        }

        private bool XacNhanBangMaVach(string hanhDong, string tenSach, string maBanSaoGoc)
        {
            bool isMatched = false;
            Form scanForm = new Form() { Width = 450, Height = 200, Text = $"Xác thực {hanhDong}", StartPosition = FormStartPosition.CenterParent, BackColor = Color.White };
            Label lbl = new Label() { Left = 20, Top = 20, Width = 400, Text = $"Quét mã vạch: {tenSach}", Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txt = new TextBox() { Left = 20, Top = 60, Width = 390, Font = new Font("Segoe UI", 12) };
            txt.KeyDown += (s, ev) => {
                if (ev.KeyCode == Keys.Enter)
                {
                    if (txt.Text.Trim().ToUpper() == maBanSaoGoc.ToUpper()) { isMatched = true; scanForm.Close(); }
                    else { MessageBox.Show("Mã vạch không khớp!"); txt.Clear(); }
                }
            };
            scanForm.Controls.Add(lbl); scanForm.Controls.Add(txt); scanForm.ShowDialog();
            return isMatched;
        }

        private void btnLichSu_Click(object sender, EventArgs e) { LoadLichSuMuon(); }

        private void frmTrangChuDocGia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12) { if (MessageBox.Show("Đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) { this.Hide(); new FrmLogin().ShowDialog(); this.Close(); } }
        }
    }
}