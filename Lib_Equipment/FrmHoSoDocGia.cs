using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public class FrmHoSoDocGia : Form
    {
        private string _readerID;
        private DataGridView dgvHistory;
        private Label lblDebtValue;

        public FrmHoSoDocGia(string readerId, string readerName)
        {
            _readerID = readerId;

            this.Text = "HỒ SƠ LƯU VẾT ĐỘC GIẢ";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            BuildUI(readerName);
            LoadData();
        }

        private void BuildUI(string readerName)
        {
            // 1. Header Xanh Đậm
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.FromArgb(0, 51, 102) };
            Label lblTitle = new Label { Text = $"HỒ SƠ CHI TIẾT: {readerName.ToUpper()}", ForeColor = Color.White, Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            // 2. Truy vấn Thông tin Độc giả từ Database
            string query = @"
                SELECT 
                    ISNULL(ReaderType, N'Chưa xác định') AS ReaderType, 
                    ISNULL(Email, N'Chưa cập nhật') AS Email, 
                    ISNULL(AcademicDebt, 0) AS Debt, 
                    ISNULL(IsPermanentlyBanned, 0) AS IsBanned, 
                    ISNULL(Status, 1) AS Status 
                FROM Reader 
                WHERE ReaderID = @id";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@id", _readerID) });

            string type = "N/A", email = "N/A"; decimal debt = 0; bool isBanned = false; int status = 1;
            if (dt.Rows.Count > 0)
            {
                type = dt.Rows[0]["ReaderType"].ToString();
                email = dt.Rows[0]["Email"].ToString();
                debt = Convert.ToDecimal(dt.Rows[0]["Debt"]);
                isBanned = Convert.ToBoolean(dt.Rows[0]["IsBanned"]);
                status = Convert.ToInt32(dt.Rows[0]["Status"]);
            }

            // 3. Panel Thông tin trái
            Panel pnlInfo = new Panel { Location = new Point(20, 90), Size = new Size(280, 500), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

            Label lblAvatar = new Label { Text = "👤", Font = new Font("Segoe UI", 60), AutoSize = true, Location = new Point(80, 20) };
            Label lblId = new Label { Text = $"Mã ĐG: {_readerID}", Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true, Location = new Point(20, 140) };
            Label lblType = new Label { Text = $"Vai trò: {type}", Font = new Font("Segoe UI", 11), AutoSize = true, Location = new Point(20, 170) };
            Label lblEmail = new Label { Text = $"Email: {email}", Font = new Font("Segoe UI", 10), AutoSize = true, Location = new Point(20, 200) };

            string statusText = isBanned ? "CẤM VĨNH VIỄN" : (status == 1 ? "Hoạt động tốt" : "Đang bị khóa");
            Color statusColor = isBanned ? Color.Red : (status == 1 ? Color.Green : Color.Orange);
            Label lblStatus = new Label { Text = $"Trạng thái: {statusText}", ForeColor = statusColor, Font = new Font("Segoe UI", 11, FontStyle.Bold), AutoSize = true, Location = new Point(20, 250) };

            Label lblDebtTitle = new Label { Text = "CÔNG NỢ HIỆN TẠI", ForeColor = Color.Gray, Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = true, Location = new Point(20, 310) };

            lblDebtValue = new Label { Text = $"{debt:N0} VNĐ", ForeColor = debt > 0 ? Color.Red : Color.Green, Font = new Font("Segoe UI", 24, FontStyle.Bold), AutoSize = true, Location = new Point(20, 340) };

            pnlInfo.Controls.AddRange(new Control[] { lblAvatar, lblId, lblType, lblEmail, lblStatus, lblDebtTitle, lblDebtValue });
            this.Controls.Add(pnlInfo);

            // 4. Bảng Lịch sử Lưu vết bên phải
            Label lblHistoryTitle = new Label { Text = "LỊCH SỬ GIAO DỊCH & LƯU VẾT ÁN PHẠT", Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = Color.FromArgb(0, 51, 102), AutoSize = true, Location = new Point(320, 90) };
            this.Controls.Add(lblHistoryTitle);

            dgvHistory = new DataGridView
            {
                Location = new Point(320, 130),
                Size = new Size(640, 460),
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false
            };

            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 75, 132);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvHistory.ColumnHeadersHeight = 40;
            dgvHistory.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
            dgvHistory.RowTemplate.Height = 35;
            dgvHistory.CellFormatting += DgvHistory_CellFormatting;

            this.Controls.Add(dgvHistory);

            decimal totalFinesInHistory = 0;
            try { totalFinesInHistory = Convert.ToDecimal(DataProvider.Instance.ExecuteScalar("SELECT ISNULL(SUM(FineAmount), 0) FROM BorrowRecord br JOIN BorrowDetail bd ON br.RecordID = bd.RecordID WHERE br.ReaderID = @id", new SqlParameter[] { new SqlParameter("@id", _readerID) })); } catch { }

            if (totalFinesInHistory > 0 && debt == 0)
            {
                Label lblPaid = new Label { Text = "*(Đã thanh toán đủ tiền phạt)*", ForeColor = Color.Green, Font = new Font("Segoe UI", 9, FontStyle.Italic), AutoSize = true, Location = new Point(20, 390) };
                pnlInfo.Controls.Add(lblPaid);
            }
        }

        private void LoadData()
        {
            // ĐÃ SỬA: Đổi VARCHAR thành NVARCHAR, và gọi cột ReturnCondition thay vì bc.Status
            string query = @"
                SELECT 
                    ISNULL(b.Title, N'Sách đã bị xóa/thanh lý') AS [Tên Sách], 
                    br.BorrowDate AS [Ngày Mượn], 
                    br.DueDate AS [Hạn Trả], 
                    ISNULL(CONVERT(NVARCHAR, bd.ReturnDate, 103), N'Chưa trả') AS [Ngày Trả], 
                    ISNULL(bd.FineAmount, 0) AS [Tiền Phạt], 
                    CASE 
                        WHEN bd.ReturnDate IS NULL THEN N'Đang mượn' 
                        ELSE ISNULL(bd.ReturnCondition, N'Bình thường') 
                    END AS [Tình Trạng]
                FROM BorrowRecord br
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID
                LEFT JOIN BookCopy bc ON bd.CopyID = bc.CopyID
                LEFT JOIN Book b ON bc.BookID = b.BookID
                WHERE br.ReaderID = @id
                ORDER BY br.BorrowDate DESC";

            DataTable dtHistory = DataProvider.Instance.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@id", _readerID) });

            if (dtHistory.Rows.Count == 0)
            {
                dgvHistory.Visible = false;
                Label lblEmpty = new Label()
                {
                    Text = "📭 Độc giả này chưa có lịch sử mượn sách nào.",
                    Font = new Font("Segoe UI", 14, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = false,
                    Size = new Size(640, 460),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(320, 130)
                };
                this.Controls.Add(lblEmpty);
                lblEmpty.BringToFront();
            }
            else
            {
                dgvHistory.DataSource = dtHistory;
                if (dgvHistory.Columns.Contains("Tiền Phạt"))
                    dgvHistory.Columns["Tiền Phạt"].DefaultCellStyle.Format = "N0";
            }
        }

        private void DgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == DBNull.Value || e.Value == null) return;

            if (dgvHistory.Columns[e.ColumnIndex].Name == "Tiền Phạt")
            {
                decimal fine = Convert.ToDecimal(e.Value);
                if (fine > 0) { e.CellStyle.ForeColor = Color.Red; e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold); }
            }

            if (dgvHistory.Columns[e.ColumnIndex].Name == "Tình Trạng")
            {
                string status = e.Value.ToString();
                if (status == "Hỏng" || status == "Mất" || status.Contains("Lỗi"))
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else if (status == "Đang mượn" || status == "Chưa trả")
                {
                    e.CellStyle.ForeColor = Color.Orange;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
            }
        }
    }
}