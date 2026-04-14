using Lib_Equipment.BLL;
using Lib_Equipment.Helpers;
using System;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmQuanLyTaiKhoan : Form
    {
        private int selectedUserID = -1;
        public FrmQuanLyTaiKhoan() { InitializeComponent(); }

        private void FrmQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            cboQuyen.DataSource = TaiKhoanBLL.Instance.LayDanhSachQuyen();
            cboQuyen.DisplayMember = "RoleName"; cboQuyen.ValueMember = "RoleID";
            LoadData();
        }

        private void LoadData()
        {
            dgvTaiKhoan.DataSource = TaiKhoanBLL.Instance.LayDanhSachTaiKhoan();
            dgvTaiKhoan.Columns["UserID"].HeaderText = "ID"; dgvTaiKhoan.Columns["UserID"].Width = 50;
            dgvTaiKhoan.Columns["Username"].HeaderText = "Tên đăng nhập";
            dgvTaiKhoan.Columns["FullName"].HeaderText = "Họ và tên";
            dgvTaiKhoan.Columns["RoleName"].HeaderText = "Quyền hạn";
            dgvTaiKhoan.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvTaiKhoan.Columns["RoleID"].Visible = false;
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedUserID = Convert.ToInt32(dgvTaiKhoan.Rows[e.RowIndex].Cells["UserID"].Value);
                txtTenDangNhap.Text = dgvTaiKhoan.Rows[e.RowIndex].Cells["Username"].Value.ToString();
                txtHoTen.Text = dgvTaiKhoan.Rows[e.RowIndex].Cells["FullName"].Value.ToString();
                cboQuyen.SelectedValue = dgvTaiKhoan.Rows[e.RowIndex].Cells["RoleID"].Value.ToString();
                cboTrangThai.Text = dgvTaiKhoan.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString();
                txtMatKhau.Text = ""; txtTenDangNhap.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (TaiKhoanBLL.Instance.ThemTaiKhoan(txtTenDangNhap.Text.Trim(), txtMatKhau.Text.Trim(), txtHoTen.Text.Trim(), cboQuyen.SelectedValue.ToString(), cboTrangThai.Text, out string msg))
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); btnLamMoi_Click(null, null);
            }
            else MessageBox.Show(msg, "Lỗi/Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (TaiKhoanBLL.Instance.SuaTaiKhoan(selectedUserID, txtHoTen.Text.Trim(), cboQuyen.SelectedValue.ToString(), cboTrangThai.Text, txtMatKhau.Text.Trim()))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo"); LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (TaiKhoanBLL.Instance.XoaTaiKhoan(selectedUserID))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo"); LoadData(); btnLamMoi_Click(null, null);
                }
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            selectedUserID = -1; txtTenDangNhap.Enabled = true; txtTenDangNhap.Clear(); txtHoTen.Clear(); txtMatKhau.Clear();
            cboTrangThai.SelectedIndex = 0; if (cboQuyen.Items.Count > 0) cboQuyen.SelectedIndex = 0;
        }
    }
}