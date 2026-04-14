using Lib_Equipment.BLL;
using Lib_Equipment.DAO;
using Lib_Equipment.Database;
using Lib_Equipment.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmQuanLyDocGia : Form
    {
        private string selectedReaderID = "";

        public FrmQuanLyDocGia() { InitializeComponent(); }

        private void FrmQuanLyDocGia_Load(object sender, EventArgs e)
        {
            // 1. Load các ComboBox (Khoa, Loại thẻ...)
            cboDonVi.DataSource = DocGiaBLL.Instance.LayDanhSachKhoaVien();
            cboDonVi.DisplayMember = "DepartmentName";
            cboDonVi.ValueMember = "DepartmentID";

            // 2. TỰ ĐỘNG QUÉT HỆ THỐNG (Thay thế nút Debug Mail)
            // Chúng ta bọc trong try-catch để nếu mất mạng/lỗi Mail thì Form vẫn mở được bình thường
            try
            {
                // Quét gửi mail nhắc nhở & xử lý kỷ luật (Ngày 3, Ngày 31)
                MuonTraBLL.Instance.TuDongKiemTraVaGuiMailLuuLuu();
            }
            catch { /* Bỏ qua lỗi kết nối SMTP để Form tiếp tục load */ }

            // 3. Hiển thị dữ liệu (RefreshGrid đã có sẵn lệnh AutoUpdateDebt)
            RefreshGrid();
        }
        private void LoadData()
        {
            dgvDocGia.DataSource = DocGiaBLL.Instance.LayDanhSachDocGia();
            dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DocGiaDAO.Instance.AutoUpdateDebt();
            RefreshGrid();
            if (dgvDocGia.Columns.Contains("Công nợ (VNĐ)")) dgvDocGia.Columns["Công nợ (VNĐ)"].DefaultCellStyle.Format = "N0";
        }
        private void dgvDocGia_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvDocGia.Rows)
            {
                if (row.Cells["Trạng thái"].Value != null)
                {
                    string trangThai = row.Cells["Trạng thái"].Value.ToString();
                    if (trangThai == "CẤM VĨNH VIỄN")
                    {
                        row.DefaultCellStyle.BackColor = Color.MistyRose;
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else if (trangThai == "Đang bị khóa")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = Color.OrangeRed;
                    }
                }
            }
        }
        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedReaderID = dgvDocGia.Rows[e.RowIndex].Cells["Mã Độc giả"].Value.ToString();
                txtMaDocGia.Text = selectedReaderID;
                txtHoTen.Text = dgvDocGia.Rows[e.RowIndex].Cells["Họ và tên"].Value.ToString();
                cboDonVi.Text = dgvDocGia.Rows[e.RowIndex].Cells["Khoa/Viện"].Value.ToString();
                txtMail.Text = dgvDocGia.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                cboLoaiDocGia.Text = dgvDocGia.Rows[e.RowIndex].Cells["Loại thẻ"].Value.ToString();
                cboTrangThai.Text = dgvDocGia.Rows[e.RowIndex].Cells["Trạng thái"].Value.ToString();
                txtMaDocGia.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string deptId = cboDonVi.SelectedValue?.ToString() ?? "";
            int status = cboTrangThai.Text.Contains("Hoạt động") ? 1 : 0;
            string email = txtMail.Text.Trim();

            // Khai báo biến để nhận giá trị từ out parameter
            string msg;

            bool isSuccess = DocGiaBLL.Instance.ThemDocGia(
                txtMaDocGia.Text.Trim(),
                txtHoTen.Text.Trim(),
                deptId,
                cboLoaiDocGia.Text,
                status,
                email,
                out msg
            );

            if (isSuccess)
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                btnLamMoi_Click(null, null);
            }
            else
            {
                // Hiển thị lỗi từ msg đã được gán trong BLL
                MessageBox.Show(msg, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string deptId = cboDonVi.SelectedValue?.ToString() ?? "";
            int status = cboTrangThai.Text.Contains("Hoạt động") ? 1 : 0;
            if (DocGiaBLL.Instance.SuaDocGia(selectedReaderID, txtHoTen.Text.Trim(), deptId, cboLoaiDocGia.Text, status, txtMail.Text))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo"); LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedReaderID)) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (DocGiaBLL.Instance.XoaDocGia(selectedReaderID))
                {
                    MessageBox.Show("Đã xóa độc giả!", "Thông báo"); LoadData(); btnLamMoi_Click(null, null);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            selectedReaderID = ""; txtMaDocGia.Enabled = true; txtMaDocGia.Clear(); txtHoTen.Clear();
            if (cboDonVi.Items.Count > 0) cboDonVi.SelectedIndex = 0;
            if (cboLoaiDocGia.Items.Count > 0) cboLoaiDocGia.SelectedIndex = 0;
            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        }
        private void RefreshGrid()
        {
            try
            {
                // 1. Phải chạy cái này trước để SQL tính toán lại (người trả sách rồi sẽ được mở khóa ở đây)
                DocGiaDAO.Instance.AutoUpdateDebt();

                // 2. Sau đó mới SELECT dữ liệu lên
                string query = @"SELECT ReaderID AS [Mã Độc giả], 
                               FullName AS [Họ và tên], 
                               DepartmentID AS [Khoa/Viện], 
                               ReaderType AS [Loại thẻ], 
                               Email, 
                               AcademicDebt AS [Công nợ (VNĐ)],
                               CASE 
                                  WHEN IsPermanentlyBanned = 1 THEN N'CẤM VĨNH VIỄN' 
                                  WHEN Status = 1 THEN N'Hoạt động' 
                                  ELSE N'Đang bị khóa' 
                               END AS [Trạng thái]
                        FROM Reader 
                        WHERE IsDeleted = 0 OR IsDeleted IS NULL";

                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                dgvDocGia.DataSource = dt;

                // 3. Tô màu trực quan (giữ nguyên logic của bạn)
                foreach (DataGridViewRow row in dgvDocGia.Rows)
                {
                    string trangThai = row.Cells["Trạng thái"].Value?.ToString();
                    if (trangThai == "CẤM VĨNH VIỄN")
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        row.DefaultCellStyle.Font = new Font(dgvDocGia.Font, FontStyle.Bold);
                        row.DefaultCellStyle.BackColor = Color.MistyRose;
                    }
                    else if (trangThai == "Đang bị khóa")
                    {
                        row.DefaultCellStyle.ForeColor = Color.OrangeRed;
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                    else
                    {
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
        private void btnDongBo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hệ thống sẽ cấp tài khoản đăng nhập cho toàn bộ Độc giả chưa có tài khoản. Tiếp tục?", "Đồng bộ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int countUsers = DocGiaBLL.Instance.DongBoHeThong();
                MessageBox.Show($"Hoàn tất!\nĐã tạo mới {countUsers} tài khoản.", "Thành công"); LoadData();
            }
        }

        private void btnDebugMail_Click(object sender, EventArgs e)
        {
            btnDebugMail.Text = "Đang quét...";
            btnDebugMail.Enabled = false;

            try
            {
                // Gọi BLL để thực hiện quét toàn bộ những người đến hạn/quá hạn chưa gửi mail
                int count = MuonTraBLL.Instance.TuDongKiemTraVaGuiMailLuuLuu();

                if (count > 0)
                    MessageBox.Show($"Hệ thống đã gửi thành công {count} email nhắc nhở và xử lý kỷ luật!", "Thành công");
                else
                    MessageBox.Show("Không có độc giả nào mới cần gửi mail trong hôm nay.", "Thông báo");

                RefreshGrid(); // Cập nhật lại màu sắc và trạng thái trên bảng
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi: " + ex.Message);
            }
            finally
            {
                btnDebugMail.Text = "GỬI MAIL NHẮC NHỞ";
                btnDebugMail.Enabled = true;
            }
        }
    }
}