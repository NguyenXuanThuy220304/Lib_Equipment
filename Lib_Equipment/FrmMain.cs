using Lib_Equipment.Database;
using Lib_Equipment.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class FrmMain : Form
    {
        private Form currentChildForm;

        public FrmMain()
        {
            InitializeComponent();
            CustomizeDesign(); // Ẩn các sub-menu lúc khởi tạo
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Gọi hàm phân quyền khi Form load xong
            ApplyDynamicPermissions();
        }

        // =========================================================================
        // 1. HÀM XỬ LÝ ẨN HIỆN SUB-MENU
        // =========================================================================
        private void CustomizeDesign()
        {
            pnlSubMenuHeThong.Visible = false;
            pnlSubMenuThuVien.Visible = false;
            pnlSubMenuThietBi.Visible = false;
            pnlSubMenuBaoCao.Visible = false;
        }

        private void HideAllSubMenu()
        {
            if (pnlSubMenuHeThong.Visible) pnlSubMenuHeThong.Visible = false;
            if (pnlSubMenuThuVien.Visible) pnlSubMenuThuVien.Visible = false;
            if (pnlSubMenuThietBi.Visible) pnlSubMenuThietBi.Visible = false;
            if (pnlSubMenuBaoCao.Visible) pnlSubMenuBaoCao.Visible = false;
        }

        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideAllSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        // =========================================================================
        // 2. PHÂN QUYỀN ĐỘNG (Dùng Try-Catch để soi lỗi)
        // =========================================================================
        private void ApplyDynamicPermissions()
        {
            try
            {
                // Bước 1: Kiểm tra RoleID có tồn tại không
                if (string.IsNullOrEmpty(AppSession.RoleID))
                {
                    MessageBox.Show("Cảnh báo: Không tìm thấy RoleID! Vui lòng đăng nhập lại.", "Hệ thống");
                    return;
                }

                // Bước 2: ẨN TOÀN BỘ CÁC NÚT (CẢ CHA LẪN CON)
                // Nút Cha
                btnHeThong.Visible = false;
                btnThuVien.Visible = false;
                btnThietBi.Visible = false;
                btnBaoCao.Visible = false;

                // Nút Con Hệ Thống
                btnSubTaiKhoan.Visible = false;
                btnSubPhanQuyen.Visible = false;
                btnSubSaoLuu.Visible = false;

                // Nút Con Thư Viện
                btnSubQuanLySach.Visible = false;
                btnSubQuanLyDocGia.Visible = false;
                btnSubMuonTra.Visible = false;

                // Nút Con Thiết Bị
                btnSubDanhMucTB.Visible = false;
                btnSubLuanChuyen.Visible = false;
                btnSubBaoTri.Visible = false;

                // Nút Con Báo Cáo
                btnSubBCThuVien.Visible = false;
                btnSubBCThietBi.Visible = false;

                // Bước 3: Lấy danh sách quyền từ DB
                string query = "SELECT MenuID FROM RolePermission WHERE RoleID = @role";
                SqlParameter[] param = { new SqlParameter("@role", AppSession.RoleID) };
                DataTable dtPerms = DataProvider.Instance.ExecuteQuery(query, param);

                if (dtPerms == null || dtPerms.Rows.Count == 0)
                {
                    MessageBox.Show("Tài khoản của bạn chưa được cấp quyền nào!", "Thông báo");
                    return;
                }

                // Bước 4: Duyệt DB và HIỆN lại những nút được cấp phép
                foreach (DataRow row in dtPerms.Rows)
                {
                    string menuIdAllowed = row["MenuID"].ToString();

                    // Tìm nút bấm trong toàn bộ các Control của Form (bao gồm cả trong các Panel)
                    Control[] foundControls = this.Controls.Find(menuIdAllowed, true);
                    if (foundControls.Length > 0)
                    {
                        foundControls[0].Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi phân quyền: " + ex.Message, "Lỗi hệ thống");
            }
        }

        // =========================================================================
        // 3. HÀM MỞ FORM CON
        // =========================================================================
        private void OpenChildForm(Form childForm, string title)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(childForm);
            pnlDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = title;
        }

        // =========================================================================
        // 4. SỰ KIỆN CLICK MENU CHA
        // =========================================================================
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            HideAllSubMenu();
            if (currentChildForm != null) currentChildForm.Close();
            lblTitle.Text = "TRANG CHỦ";
        }

        private void btnHeThong_Click(object sender, EventArgs e) { ShowSubMenu(pnlSubMenuHeThong); }
        private void btnThuVien_Click(object sender, EventArgs e) { ShowSubMenu(pnlSubMenuThuVien); }
        private void btnThietBi_Click(object sender, EventArgs e) { ShowSubMenu(pnlSubMenuThietBi); }
        private void btnBaoCao_Click(object sender, EventArgs e) { ShowSubMenu(pnlSubMenuBaoCao); }

        // =========================================================================
        // 5. SỰ KIỆN CLICK SUB-MENU
        // =========================================================================
        private void btnSubTaiKhoan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQuanLyTaiKhoan(), "QUẢN LÝ TÀI KHOẢN HỆ THỐNG");
        }

        private void btnSubPhanQuyen_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmPhanQuyen(), "PHÂN QUYỀN CHỨC NĂNG");
        }

        private void btnSubQuanLySach_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQuanLySach(), "DANH MỤC ĐẦU SÁCH");
        }

        private void btnSubQuanLyDocGia_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQuanLyDocGia(), "QUẢN LÝ ĐỘC GIẢ");
        }

        private void btnSubMuonTra_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmMuonTraSach(), "NGHIỆP VỤ MƯỢN TRẢ SÁCH");
        }

        private void btnSubDanhMucTB_Click(object sender, EventArgs e) { /* Gọi Form TB */ }
        private void btnSubLuanChuyen_Click(object sender, EventArgs e) { /* Gọi Form LC */ }
        private void btnSubBaoTri_Click(object sender, EventArgs e) { /* Gọi Form BT */ }
        private void btnSubBCThuVien_Click(object sender, EventArgs e) { /* Gọi Form BC */ }
        private void btnSubBCThietBi_Click(object sender, EventArgs e) { /* Gọi Form BC */ }
        private void btnSubSaoLuu_Click(object sender, EventArgs e) { /* Gọi Form Sao Luu */ }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                new FrmLogin().ShowDialog();
                this.Close();
            }
        }

        private void FrmMain_Load_1(object sender, EventArgs e)
        {
            // Gọi hàm phân quyền khi Form load xong
            ApplyDynamicPermissions();
        }
    }
}