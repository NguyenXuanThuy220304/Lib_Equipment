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
        // HÀM TÌM KIẾM ĐỆ QUY (TÌM XUYÊN QUA MỌI PANEL LỒNG NHAU)
        // =========================================================================
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

        // =========================================================================
        // PHÂN QUYỀN ĐỘNG CHUẨN NHẤT
        // =========================================================================
        private void ApplyDynamicPermissions()
        {
            try
            {
                if (string.IsNullOrEmpty(AppSession.RoleID))
                {
                    MessageBox.Show("Chưa lấy được quyền (RoleID) từ màn hình Login!", "Lỗi hệ thống");
                    return;
                }

                // 1. ẨN TẤT CẢ CÁC NÚT (Liệt kê rõ ràng để không ẩn nhầm Panel chứa nó)
                // - Nút Cha
                btnHeThong.Visible = false;
                btnThuVien.Visible = false;
                btnThietBi.Visible = false;
                btnBaoCao.Visible = false;

                // - Nút Con
                btnSubTaiKhoan.Visible = false;
                btnSubPhanQuyen.Visible = false;
                btnSubSaoLuu.Visible = false;

                btnSubQuanLySach.Visible = false;
                btnSubQuanLyDocGia.Visible = false;
                btnSubMuonTra.Visible = false;

                btnSubDanhMucTB.Visible = false;
                btnSubLuanChuyen.Visible = false;
                btnSubBaoTri.Visible = false;

                btnSubBCThuVien.Visible = false;
                btnSubBCThietBi.Visible = false;

                // 2. TRUY VẤN CSDL
                string query = "SELECT MenuID FROM RolePermission WHERE RoleID = @role";
                SqlParameter[] param = { new SqlParameter("@role", AppSession.RoleID) };
                DataTable dtPerms = DataProvider.Instance.ExecuteQuery(query, param);

                // 3. HIỂN THỊ CÁC NÚT ĐƯỢC PHÉP
                foreach (DataRow row in dtPerms.Rows)
                {
                    // Thêm .Trim() để đề phòng Database có khoảng trắng dư thừa
                    string menuIdAllowed = row["MenuID"].ToString().Trim();

                    // Sử dụng hàm tìm kiếm đệ quy tự viết
                    Control foundControl = FindControlRecursive(this, menuIdAllowed);
                    if (foundControl != null)
                    {
                        foundControl.Visible = true;
                    }
                }

                // 4. BƯỚC LOGIC THÔNG MINH (CỨU CÁNH UX): 
                // Nếu người dùng được cấp quyền bất kỳ Nút Con nào, thì BẮT BUỘC phải hiện Nút Cha tương ứng
                // để họ có chỗ bấm xổ menu xuống.
                if (btnSubTaiKhoan.Visible || btnSubPhanQuyen.Visible || btnSubSaoLuu.Visible)
                    btnHeThong.Visible = true;

                if (btnSubQuanLySach.Visible || btnSubQuanLyDocGia.Visible || btnSubMuonTra.Visible)
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

        private void btnSubDanhMucTB_Click(object sender, EventArgs e) { 
            FrmQuanLyThietBi frmQuanLyThietBi = new FrmQuanLyThietBi();
            OpenChildForm(frmQuanLyThietBi, "DANH MỤC THIẾT BỊ");
        }
        private void btnSubLuanChuyen_Click(object sender, EventArgs e) {
            FrmLuanChuyenThietBi frm = new FrmLuanChuyenThietBi();
            OpenChildForm(frm, "LUÂN CHUYỂN & CẤP PHÁT THIẾT BỊ");
        }
        private void btnSubBaoTri_Click(object sender, EventArgs e) { OpenChildForm(new FrmBaoTriThietBi(), "BẢO TRÌ VÀ THANH LÝ THIẾT BỊ"); }
        private void btnSubBCThuVien_Click(object sender, EventArgs e) { /* Gọi Form BC */ }
        private void btnSubBCThietBi_Click(object sender, EventArgs e) { /* Gọi Form BC */ }
        private void btnSubSaoLuu_Click(object sender, EventArgs e) {
            FrmSaoLuuPhucHoi frm = new FrmSaoLuuPhucHoi();
            OpenChildForm(frm, "SAO LƯU & PHỤC HỒI DỮ LIỆU");
        }

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