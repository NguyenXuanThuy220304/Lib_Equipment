using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmMain : Form
    {
        // Biến lưu trữ form con đang được mở
        private Form currentChildForm;

        public FrmMain()
        {
            InitializeComponent();
        }

        // =========================================================================
        // HÀM XỬ LÝ: Mở Form con bên trong Panel Desktop
        // =========================================================================
        private void OpenChildForm(Form childForm, string title)
        {
            // 1. Đóng form cũ nếu đang có form mở
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            // 2. Gán form mới
            currentChildForm = childForm;

            // 3. Cấu hình form con để chui vào panel
            childForm.TopLevel = false; // Bỏ đặc tính là cửa sổ độc lập
            childForm.FormBorderStyle = FormBorderStyle.None; // Bỏ viền
            childForm.Dock = DockStyle.Fill; // Lấp đầy khoảng trống Panel

            // 4. Thêm vào Panel và hiển thị
            pnlDesktop.Controls.Add(childForm);
            pnlDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

            // 5. Cập nhật tiêu đề Header
            lblTitle.Text = title;
        }

        // =========================================================================
        // XỬ LÝ SỰ KIỆN CLICK MENU
        // =========================================================================
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close(); // Đóng form hiện tại để về lại màn hình trống
            }
            lblTitle.Text = "TRANG CHỦ";
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {
            // TẠM THỜI CHÚNG TA CHƯA TẠO FORM NÀY, tôi viết sẵn comment để sau này bạn bỏ vào
            // FrmQuanLyTaiKhoan frm = new FrmQuanLyTaiKhoan();
            // OpenChildForm(frm, "QUẢN LÝ HỆ THỐNG");
        }

        private void btnThuVien_Click(object sender, EventArgs e)
        {
            // FrmQuanLySach frm = new FrmQuanLySach();
            // OpenChildForm(frm, "QUẢN LÝ THƯ VIỆN");
        }

        private void btnThietBi_Click(object sender, EventArgs e)
        {
            // FrmQuanLyThietBi frm = new FrmQuanLyThietBi();
            // OpenChildForm(frm, "QUẢN LÝ TRANG THIẾT BỊ");
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            // FrmBaoCao frm = new FrmBaoCao();
            // OpenChildForm(frm, "BÁO CÁO & THỐNG KÊ");
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                FrmLogin login = new FrmLogin();
                login.ShowDialog();
                this.Close(); // Đóng hoàn toàn FrmMain
            }
        }
    }
}