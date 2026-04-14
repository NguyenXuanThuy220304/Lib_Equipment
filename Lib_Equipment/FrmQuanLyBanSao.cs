using Lib_Equipment.BLL;
using System;
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
            // 1. Load Combobox Trạng thái
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Có sẵn", "Đang mượn", "Hỏng", "Mất" });
            cboTrangThai.SelectedIndex = 0;

            // 2. Load Combobox Đầu sách
            cboMaSach.DataSource = BanSaoBLL.Instance.LayDanhSachDauSach();
            cboMaSach.DisplayMember = "Title";
            cboMaSach.ValueMember = "BookID";

            LoadData();
        }

        private void LoadData()
        {
            dgvBanSao.DataSource = BanSaoBLL.Instance.LayTatCaBanSao();
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
                cboMaSach.SelectedValue = row.Cells["Mã Sách Gốc"].Value.ToString();
                cboTrangThai.Text = row.Cells["Trạng thái"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string bookId = cboMaSach.SelectedValue?.ToString() ?? "";
            string soLuong = txtSoLuong.Text.Trim();

            if (BanSaoBLL.Instance.SinhBanSaoHangLoat(bookId, soLuong, out string msg))
            {
                MessageBox.Show(msg, "Nhập kho thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                btnLamMoi_Click(null, null);
            }
            else
            {
                MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCopyID))
            {
                MessageBox.Show("Vui lòng chọn bản sao cần cập nhật trạng thái!", "Cảnh báo");
                return;
            }

            if (BanSaoBLL.Instance.CapNhatTrangThai(selectedCopyID, cboTrangThai.Text))
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
                if (BanSaoBLL.Instance.XoaBanSao(selectedCopyID))
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
            txtMaBanSao.Clear();
            txtSoLuong.Text = "1"; // Mặc định nhập 1 cuốn
            if (cboMaSach.Items.Count > 0) cboMaSach.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
        }
    }
}