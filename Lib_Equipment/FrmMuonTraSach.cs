using Lib_Equipment.BLL;
using Lib_Equipment.DAO;
using Lib_Equipment.DTO;
using Lib_Equipment.Helpers;
using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmMuonTraSach : Form
    {
        private DocGiaDTO currentReader = null; // Lưu cache độc giả đang giao dịch

        public FrmMuonTraSach()
        {
            InitializeComponent();
        }

        private void FrmMuonTraSach_Load(object sender, EventArgs e)
        {
            SetupGridReturnButton();
            RefreshGrid();
            //btnNapTien.Enabled = true;
        }

        private void SetupGridReturnButton()
        {
            if (dgvDangMuon.Columns.Contains("btnReturn")) return;
            DataGridViewButtonColumn btnReturn = new DataGridViewButtonColumn();
            btnReturn.Name = "btnReturn";
            btnReturn.HeaderText = "Thao tác";
            btnReturn.UseColumnTextForButtonValue = false;
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgvDangMuon.Columns.Add(btnReturn);
        }

        private void RefreshGrid()
        {
            string query = "";
            DataTable dt = new DataTable();

            if (string.IsNullOrEmpty(txtMaDG.Text.Trim()))
            {
                query = @"SELECT br.RecordID AS [Mã Phiếu], r.FullName AS [Người Mượn], bd.CopyID AS [Mã Bản Sao], b.Title AS [Tên Sách], br.BorrowDate AS [Ngày Mượn], br.DueDate AS [Hạn Trả]
                          FROM BorrowRecord br JOIN Reader r ON br.ReaderID = r.ReaderID JOIN BorrowDetail bd ON br.RecordID = bd.RecordID JOIN BookCopy bc ON bd.CopyID = bc.CopyID JOIN Book b ON bc.BookID = b.BookID WHERE bd.ReturnDate IS NULL";
                dt = DataProvider.Instance.ExecuteQuery(query);
            }
            else
            {
                query = @"SELECT br.RecordID AS [Mã Phiếu], bd.CopyID AS [Mã Bản Sao], b.Title AS [Tên Sách], br.BorrowDate AS [Ngày Mượn], br.DueDate AS [Hạn Trả]
                          FROM BorrowRecord br JOIN BorrowDetail bd ON br.RecordID = bd.RecordID JOIN BookCopy bc ON bd.CopyID = bc.CopyID JOIN Book b ON bc.BookID = b.BookID WHERE br.ReaderID = @id AND bd.ReturnDate IS NULL";
                dt = DataProvider.Instance.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@id", txtMaDG.Text.Trim()) });
            }

            dgvDangMuon.DataSource = dt;
            if (dgvDangMuon.Columns.Contains("btnReturn")) dgvDangMuon.Columns["btnReturn"].DisplayIndex = dgvDangMuon.Columns.Count - 1;
        }

        // ==========================================================
        // 1. QUÉT THẺ ĐỘC GIẢ (SỬ DỤNG BLL KIỂM TRA LUẬT)
        // ==========================================================
        private void txtMaDG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtMaDG.Text))
                {
                    txtTenDG.Clear(); currentReader = null; RefreshGrid(); return;
                }

                // 1. LẤY THÔNG TIN (KIỂM TRA CẤM VĨNH VIỄN & CÔNG NỢ)
                string query = "SELECT ReaderID, FullName, Status, ISNULL(IsPermanentlyBanned, 0) AS IsPermanentlyBanned, ISNULL(AcademicDebt, 0) AS AcademicDebt, ReaderType FROM Reader WHERE ReaderID = @id AND (IsDeleted = 0 OR IsDeleted IS NULL)";

                SqlParameter[] param = { new SqlParameter("@id", txtMaDG.Text.Trim()) };
                DataTable dt = DataProvider.Instance.ExecuteQuery(query, param);

                if (dt.Rows.Count > 0)
                {
                    // === [QUAN TRỌNG]: KHỞI TẠO ĐỐI TƯỢNG ĐỂ HẾT BỊ RỖNG ===
                    currentReader = new DocGiaDTO()
                    {
                        ReaderID = dt.Rows[0]["ReaderID"].ToString(),
                        FullName = dt.Rows[0]["FullName"].ToString(),
                        ReaderType = dt.Rows[0]["ReaderType"].ToString(),
                        Status = Convert.ToInt32(dt.Rows[0]["Status"]),
                        // Nếu DTO của bạn dùng Balance hoặc AcademicDebt, hãy gán tương ứng ở đây
                    };

                    txtTenDG.Text = currentReader.FullName;
                    string readerType = currentReader.ReaderType;

                    // LUẬT 1: ĐÃ BỊ TƯỚC QUYỀN VĨNH VIỄN THÌ KHÔNG CHO LÀM GÌ CẢ
                    if (Convert.ToBoolean(dt.Rows[0]["IsPermanentlyBanned"]))
                    {
                        MessageBox.Show("TÀI KHOẢN NÀY ĐÃ BỊ TƯỚC QUYỀN SỬ DỤNG THƯ VIỆN VĨNH VIỄN!\nLý do: Vi phạm nghiêm trọng nội quy (Mất sách/Chống đối/Quá hạn > 30 ngày).", "Lệnh Cấm Phục Vụ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        btnChoMuon.Enabled = false; txtMaBanSao.Enabled = false; return;
                    }

                    // LUẬT 2: KIỂM TRA QUÁ HẠN > 30 NGÀY -> CẤM LUÔN
                    string checkOverdue30Days = @"
                SELECT COUNT(*) FROM BorrowRecord br 
                JOIN BorrowDetail bd ON br.RecordID = bd.RecordID 
                WHERE br.ReaderID = @id AND bd.ReturnDate IS NULL 
                AND DATEDIFF(day, br.DueDate, GETDATE()) > 30";

                    SqlParameter[] paramCheck = { new SqlParameter("@id", txtMaDG.Text.Trim()) };
                    int overdue30Count = (int)DataProvider.Instance.ExecuteScalar(checkOverdue30Days, paramCheck);

                    if (overdue30Count > 0)
                    {
                        SqlParameter[] paramBan = { new SqlParameter("@id", txtMaDG.Text.Trim()) };
                        // Sửa tham số null cho ExecuteNonQuery để khớp với DataProvider của bạn
                        DataProvider.Instance.ExecuteNonQuery("UPDATE Reader SET IsPermanentlyBanned = 1, Status = 0 WHERE ReaderID = @id", paramBan);

                        string warning = readerType == "Sinh viên" ? "Hồ sơ đã chuyển sang Phòng Đào tạo (Ghi nhận công nợ - Chặn xét Tốt nghiệp)." : "Hồ sơ đã chuyển sang Phòng Tài chính (Trừ trực tiếp vào lương tháng).";

                        MessageBox.Show($"BÁO ĐỘNG: Độc giả có sách mượn quá hạn trên 30 ngày!\n\nHệ thống đã tự động TƯỚC QUYỀN SỬ DỤNG VĨNH VIỄN tài khoản này.\n{warning}", "Xử lý Kỷ luật", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        btnChoMuon.Enabled = false; txtMaBanSao.Enabled = false; currentReader = null; return;
                    }

                    // LUẬT 3: CÓ CÔNG NỢ NHƯNG CHƯA ĐÓNG PHẠT (CẢNH BÁO)
                    decimal debt = Convert.ToDecimal(dt.Rows[0]["AcademicDebt"]);
                    if (debt > 0)
                    {
                        MessageBox.Show($"LƯU Ý: Độc giả này đang có Công nợ chưa thanh toán: {debt:N0} VNĐ.\nVui lòng yêu cầu độc giả đóng phạt để giải quyết công nợ.", "Thông báo Công nợ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    txtMaBanSao.Enabled = true;
                    txtMaBanSao.Focus();
                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Độc giả này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDG.Clear(); currentReader = null; RefreshGrid();
                }
            }
        }

        // ==========================================================
        // 2. QUÉT SÁCH
        // ==========================================================
        private void txtMaBanSao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtMaBanSao.Text)) return;
                DataTable dtSach = DataProvider.Instance.ExecuteQuery("SELECT b.Title, bc.Status FROM BookCopy bc JOIN Book b ON bc.BookID = b.BookID WHERE bc.CopyID = @id AND bc.IsDeleted = 0", new SqlParameter[] { new SqlParameter("@id", txtMaBanSao.Text.Trim()) });

                if (dtSach.Rows.Count > 0)
                {
                    txtTenSachMuon.Text = dtSach.Rows[0]["Title"].ToString();
                    if (dtSach.Rows[0]["Status"].ToString() != "Có sẵn")
                    {
                        MessageBox.Show("Sách này hiện đang được mượn hoặc không có sẵn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnChoMuon.Enabled = false; txtTenSachMuon.ForeColor = Color.DimGray;
                    }
                    else
                    {
                        btnChoMuon.Enabled = true; txtTenSachMuon.ForeColor = Color.FromArgb(40, 167, 69); btnChoMuon.Focus();
                    }
                }
                else { MessageBox.Show("Mã bản sao sách không tồn tại!", "Lỗi"); btnChoMuon.Enabled = false; }
            }
        }

        // ==========================================================
        // 3. LẬP PHIẾU MƯỢN
        // ==========================================================
        private void btnChoMuon_Click(object sender, EventArgs e)
        {
            if (currentReader == null) return;
            string username = AppSession.Username ?? "admin"; // Đảm bảo lấy được tên đăng nhập

            try
            {
                // ExecuteBorrow giờ trả về mã phiếu (int)
                int newId = MuonTraDAO.Instance.ExecuteBorrow(
                    currentReader.ReaderID,
                    txtMaBanSao.Text.Trim(),
                    dtpHanTra.Value,
                    username
                );

                if (newId > 0)
                {
                    MessageBox.Show($"Mượn sách thành công! Mã phiếu: {newId}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaBanSao.Clear();
                    RefreshGrid();
                    txtMaBanSao.Focus();
                }
            }
            catch (Exception ex)
            {
                // Thông báo lỗi chi tiết để debug
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================================
        // 4. TRẢ SÁCH TẠI LƯỚI (ÁP DỤNG LUẬT PHẠT 2024)
        // ==========================================================
        private void dgvDangMuon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvDangMuon.Columns["btnReturn"].Index && e.RowIndex >= 0)
            {
                int recordId = int.Parse(dgvDangMuon.Rows[e.RowIndex].Cells["Mã Phiếu"].Value.ToString());
                string copyId = dgvDangMuon.Rows[e.RowIndex].Cells["Mã Bản Sao"].Value.ToString();
                DateTime dueDate = Convert.ToDateTime(dgvDangMuon.Rows[e.RowIndex].Cells["Hạn Trả"].Value);

                // GỌI BLL TÍNH PHẠT: Chỉ phạt khi trễ >= 3 ngày
                decimal lateFine = MuonTraBLL.Instance.CalculateLateFine(dueDate);
                int lateDays = (DateTime.Now.Date - dueDate.Date).Days;
                lateDays = lateDays > 0 ? lateDays : 0;

                Form frmTra = new Form() { Text = "Nghiệp vụ Trả sách & Thu phạt", Size = new Size(450, 360), StartPosition = FormStartPosition.CenterParent, BackColor = Color.White };
                Label lblTre = new Label() { Text = $"Trả trễ: {lateDays} ngày (Tiền phạt theo quy định: {lateFine:N0}đ)", Left = 20, Top = 20, Width = 400, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = lateFine > 0 ? Color.Red : Color.Green };

                ComboBox cboTinhTrang = new ComboBox() { Left = 150, Top = 60, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
                cboTinhTrang.Items.AddRange(new string[] { "Bình thường", "Làm hỏng (Phạt 50k)", "Mất - Mua đền sách mới" });
                cboTinhTrang.SelectedIndex = 0;

                TextBox txtTongPhat = new TextBox() { Left = 150, Top = 150, Width = 250, Text = lateFine.ToString(), Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.Red, ReadOnly = true };
                Button btnXacNhan = new Button() { Text = "THU TIỀN VÀ TRẢ SÁCH", Left = 150, Top = 230, Width = 250, Height = 50, BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold) };

                cboTinhTrang.SelectedIndexChanged += (s, ev) => {
                    decimal total = lateFine;
                    if (cboTinhTrang.Text.Contains("Làm hỏng")) total += 50000;
                    else if (cboTinhTrang.Text.Contains("Mua đền sách mới")) total += 20000;
                    txtTongPhat.Text = total.ToString("N0");
                };

                frmTra.Controls.AddRange(new Control[] { lblTre, new Label() { Text = "Tình trạng sách:", Left = 20, Top = 65 }, cboTinhTrang, new Label() { Text = "TỔNG PHẠT:", Left = 20, Top = 155, Font = new Font("Segoe UI", 10, FontStyle.Bold) }, txtTongPhat, btnXacNhan });

                // Trong đoạn xử lý btnXacNhan.Click bên trong dgvDangMuon_CellContentClick:
                btnXacNhan.Click += (s, ev) => {
                    string newStatus = "Có sẵn"; string cond = "Tốt";
                    if (cboTinhTrang.Text.Contains("Hỏng")) { newStatus = "Hỏng"; cond = "Hỏng"; }
                    else if (cboTinhTrang.Text.Contains("Mất")) { newStatus = "Mất"; cond = "Mất"; }

                    decimal finalFine = 0;
                    decimal.TryParse(txtTongPhat.Text.Replace(",", ""), out finalFine);

                    // Thực hiện trả sách
                    bool isSuccess = MuonTraDAO.Instance.ExecuteReturn(recordId, copyId, cond, finalFine, newStatus);

                    if (isSuccess)
                    {
                        MessageBox.Show("Hoàn tất quy trình trả sách!\nCông nợ đã được trừ và trạng thái độc giả đã được cập nhật.",
                                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmTra.DialogResult = DialogResult.OK;
                        frmTra.Close();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra trong quá trình xử lý trả sách.", "Lỗi");
                    }
                };

                if (frmTra.ShowDialog() == DialogResult.OK) RefreshGrid();
            }
        }

        // ==========================================================
        // CÁC HÀM FORMAT GIAO DIỆN & NẠP TIỀN
        // ==========================================================
        private void dgvDangMuon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDangMuon.Rows.Count) return;
            bool isOverdue = false;
            if (dgvDangMuon.Rows[e.RowIndex].Cells["Hạn Trả"].Value != null)
            {
                if (DateTime.TryParse(dgvDangMuon.Rows[e.RowIndex].Cells["Hạn Trả"].Value.ToString(), out DateTime dueDate))
                    if (dueDate.Date < DateTime.Now.Date) isOverdue = true;
            }

            if (dgvDangMuon.Columns[e.ColumnIndex].Name != "btnReturn")
            {
                e.CellStyle.ForeColor = isOverdue ? Color.Red : Color.FromArgb(64, 64, 64);
                e.CellStyle.Font = new Font("Segoe UI", 10.5F, isOverdue ? FontStyle.Bold : FontStyle.Regular);
            }
            else
            {
                e.CellStyle.BackColor = isOverdue ? Color.FromArgb(220, 53, 69) : Color.FromArgb(40, 167, 69);
                e.CellStyle.ForeColor = Color.White;
                e.Value = "XÁC NHẬN";
            }
        }
        
    }
}