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
            CustomizeDesign();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ApplyDynamicPermissions();
        }

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

        private Control FindControlRecursive(Control root, string name)
        {
            if (root.Name == name) return root;
            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, name);
                if (t != null) return t;
            }
            return null;
        }

        private void ApplyDynamicPermissions()
        {
            try
            {
                if (string.IsNullOrEmpty(AppSession.RoleID))
                {
                    MessageBox.Show("Chưa lấy được quyền (RoleID) từ màn hình Login!", "Lỗi hệ thống");
                    return;
                }

                // 1. ẨN TẤT CẢ CÁC NÚT
                btnHeThong.Visible = false;
                btnThuVien.Visible = false;
                btnThietBi.Visible = false;
                btnBaoCao.Visible = false;

                btnSubTaiKhoan.Visible = false;
                btnSubPhanQuyen.Visible = false;
                btnSubSaoLuu.Visible = false;

                btnSubQuanLySach.Visible = false;
                btnSubQuanLyBanSao.Visible = false; // Ẩn nút mới thêm
                btnSubQuanLyDocGia.Visible = false;
                btnSubMuonTra.Visible = false;

                btnSubDanhMucTB.Visible = false;
                btnSubLuanChuyen.Visible = false;
                btnSubBaoTri.Visible = false;

                btnSubBCThuVien.Visible = false;
                btnSubBCThietBi.Visible = false;

                // 2. TRUY VẤN CSDL ĐỂ LẤY QUYỀN
                string query = "SELECT MenuID FROM RolePermission WHERE RoleID = @role";
                SqlParameter[] param = { new SqlParameter("@role", AppSession.RoleID) };
                DataTable dtPerms = DataProvider.Instance.ExecuteQuery(query, param);

                // 3. HIỂN THỊ CÁC NÚT ĐƯỢC PHÉP
                foreach (DataRow row in dtPerms.Rows)
                {
                    string menuIdAllowed = row["MenuID"].ToString().Trim();
                    Control foundControl = FindControlRecursive(this, menuIdAllowed);
                    if (foundControl != null)
                    {
                        foundControl.Visible = true;
                    }
                }

                // 4. KIỂM TRA ĐỂ MỞ MENU CHA NẾU CÓ ÍT NHẤT 1 NÚT CON ĐƯỢC BẬT
                if (btnSubTaiKhoan.Visible || btnSubPhanQuyen.Visible || btnSubSaoLuu.Visible)
                    btnHeThong.Visible = true;

                // Đã thêm nút Bản sao vào điều kiện này
                if (btnSubQuanLySach.Visible || btnSubQuanLyBanSao.Visible || btnSubQuanLyDocGia.Visible || btnSubMuonTra.Visible)
                    btnThuVien.Visible = true;

                if (btnSubDanhMucTB.Visible || btnSubLuanChuyen.Visible || btnSubBaoTri.Visible)
                    btnThietBi.Visible = true;

                if (btnSubBCThuVien.Visible || btnSubBCThietBi.Visible)
                    btnBaoCao.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình phân quyền: " + ex.Message);
            }
        }

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

        private void btnSubTaiKhoan_Click(object sender, EventArgs e) { OpenChildForm(new FrmQuanLyTaiKhoan(), "QUẢN LÝ TÀI KHOẢN HỆ THỐNG"); }
        private void btnSubPhanQuyen_Click(object sender, EventArgs e) { OpenChildForm(new FrmPhanQuyen(), "PHÂN QUYỀN CHỨC NĂNG"); }
        private void btnSubSaoLuu_Click(object sender, EventArgs e) { OpenChildForm(new FrmSaoLuuPhucHoi(), "SAO LƯU & PHỤC HỒI DỮ LIỆU"); }

        private void btnSubQuanLySach_Click(object sender, EventArgs e) { OpenChildForm(new FrmQuanLySach(), "DANH MỤC ĐẦU SÁCH"); }

        // SỰ KIỆN GỌI FORM KHO SÁCH (BẢN SAO)
        private void btnSubQuanLyBanSao_Click(object sender, EventArgs e) { OpenChildForm(new FrmQuanLyBanSao(), "QUẢN LÝ KHO SÁCH (BẢN SAO)"); }

        private void btnSubQuanLyDocGia_Click(object sender, EventArgs e) { OpenChildForm(new FrmQuanLyDocGia(), "QUẢN LÝ ĐỘC GIẢ"); }
        private void btnSubMuonTra_Click(object sender, EventArgs e) { OpenChildForm(new FrmMuonTraSach(), "NGHIỆP VỤ MƯỢN TRẢ SÁCH"); }

        private void btnSubDanhMucTB_Click(object sender, EventArgs e) { OpenChildForm(new FrmQuanLyThietBi(), "DANH MỤC THIẾT BỊ"); }
        private void btnSubLuanChuyen_Click(object sender, EventArgs e) { OpenChildForm(new FrmLuanChuyenThietBi(), "LUÂN CHUYỂN & CẤP PHÁT THIẾT BỊ"); }
        private void btnSubBaoTri_Click(object sender, EventArgs e) { OpenChildForm(new FrmBaoTriThietBi(), "BẢO TRÌ VÀ THANH LÝ THIẾT BỊ"); }

        private void btnSubBCThuVien_Click(object sender, EventArgs e) { /* Gọi Form BC */ }
        private void btnSubBCThietBi_Click(object sender, EventArgs e) { /* Gọi Form BC */ }

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
            ApplyDynamicPermissions();
        }
    }
}