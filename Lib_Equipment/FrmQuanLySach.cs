using Lib_Equipment.BLL;
using System;
using System.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lib_Equipment
{
    public partial class FrmQuanLySach : Form
    {
        private string selectedBookID = "";

        public FrmQuanLySach()
        {
            InitializeComponent();
        }

        private void FrmQuanLySach_Load(object sender, EventArgs e)
        {
            cboTheLoai.DataSource = SachBLL.Instance.LayDanhSachTheLoai();
            cboTheLoai.DisplayMember = "CategoryName";
            cboTheLoai.ValueMember = "CategoryID";

            if (cboLoaiSach.Items.Count > 0) cboLoaiSach.SelectedIndex = 0;
            LoadData();
        }

        private void LoadData()
        {
            dgvSach.DataSource = SachBLL.Instance.LayDanhSachSach();

            dgvSach.Columns["BookID"].HeaderText = "Mã Sách";
            dgvSach.Columns["Title"].HeaderText = "Tên sách";

            if (dgvSach.Columns.Contains("Price"))
            {
                dgvSach.Columns["Price"].HeaderText = "Giá tiền";
                dgvSach.Columns["Price"].DefaultCellStyle.Format = "N0";
            }
            if (dgvSach.Columns.Contains("Rarity"))
            {
                dgvSach.Columns["Rarity"].HeaderText = "Loại sách";
            }
            // MỚI THÊM: Hiện cột vị trí trên bảng cho Thủ thư dễ nhìn
            if (dgvSach.Columns.Contains("CabinetLocation"))
            {
                dgvSach.Columns["CabinetLocation"].HeaderText = "Vị trí kệ sách";
                dgvSach.Columns["CabinetLocation"].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                dgvSach.Columns["CabinetLocation"].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            }
        }

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSach.Rows[e.RowIndex];
                selectedBookID = row.Cells["BookID"].Value.ToString();

                txtMaSach.Text = selectedBookID;
                txtTenSach.Text = row.Cells["Title"].Value.ToString();
                txtTacGia.Text = row.Cells["Author"].Value.ToString();
                txtNhaXuatBan.Text = row.Cells["Publisher"].Value.ToString();
                txtNamXuatBan.Text = row.Cells["PublishYear"].Value.ToString();
                cboTheLoai.SelectedValue = row.Cells["CategoryID"].Value.ToString();

                if (dgvSach.Columns.Contains("Price") && row.Cells["Price"].Value != null)
                    txtGiaSach.Text = row.Cells["Price"].Value.ToString();

                if (dgvSach.Columns.Contains("BookType") && row.Cells["BookType"].Value != null)
                    cboLoaiSach.Text = row.Cells["BookType"].Value.ToString();

                if (dgvSach.Columns.Contains("PageCount") && row.Cells["PageCount"].Value != null && txtSoTrang != null)
                    txtSoTrang.Text = row.Cells["PageCount"].Value.ToString();

                txtMaSach.Enabled = false;
            }
        }

        // ====================================================================================
        // ĐIỂM ĂN TIỀN LÀ ĐÂY: Nút Thêm sách tự động gọi AI tính toán vị trí
        // ====================================================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            string categoryId = cboTheLoai.SelectedValue.ToString();
            string tenSach = txtTenSach.Text.Trim();

            // Tự động sinh vị trí (Sẽ có dạng A1-001.1 nếu chèn vào giữa)
            string viTriMoi = Lib_Equipment.Helpers.LocationHelper.GenerateNewBookLocation(categoryId, tenSach);

            if (SachBLL.Instance.ThemSach(txtMaSach.Text.Trim(), tenSach, txtTacGia.Text.Trim(), txtNhaXuatBan.Text.Trim(), txtNamXuatBan.Text.Trim(), categoryId, txtGiaSach.Text.Trim(), cboLoaiSach.Text, txtSoTrang.Text.Trim(), viTriMoi, out string msg))
            {
                MessageBox.Show($"{msg}\nVị trí: {viTriMoi}");
                LoadData();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (SachBLL.Instance.SuaSach(txtMaSach.Text.Trim(), txtTenSach.Text.Trim(), txtTacGia.Text.Trim(), txtNhaXuatBan.Text.Trim(), txtNamXuatBan.Text.Trim(), cboTheLoai.SelectedValue.ToString(), txtGiaSach.Text.Trim(), cboLoaiSach.Text, txtSoTrang.Text.Trim()))
            {
                MessageBox.Show("Cập nhật Đầu sách thành công!", "Thông báo");
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedBookID))
            {
                MessageBox.Show("Vui lòng chọn đầu sách cần xóa!", "Thông báo");
                return;
            }

            // Cảnh báo mạnh mẽ vì đây là xóa vĩnh viễn
            DialogResult dr = MessageBox.Show($"CẢNH BÁO: Bạn đang xóa VĨNH VIỄN đầu sách '{selectedBookID}' và TOÀN BỘ bản sao liên quan.\nThao tác này không thể hoàn tác. Bạn có chắc chắn không?",
                                              "Xác nhận xóa vĩnh viễn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                if (SachBLL.Instance.XoaSach(selectedBookID))
                {
                    MessageBox.Show("Đã xóa vĩnh viễn đầu sách và giải phóng vị trí kệ!", "Thành công");
                    LoadData(); // Load lại Grid để thấy vị trí đã trống
                    btnLamMoi_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Lỗi hệ thống khi xóa dữ liệu!", "Lỗi");
                }
            }
        }

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedBookID))
            {
                MessageBox.Show("Vui lòng chọn một đầu sách dưới lưới để in phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtExport = SachBLL.Instance.LayDanhSachInPhieu(selectedBookID);

            if (dtExport == null || dtExport.Rows.Count == 0)
            {
                MessageBox.Show("Đầu sách này chưa có cuốn sách vật lý nào trong kho để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
            sfd.FileName = $"PhieuDan_Sach_{selectedBookID}_{DateTime.Now:ddMMyy}.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Excel.Application xlApp = new Excel.Application();
                try
                {
                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(Type.Missing);
                    Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.ActiveSheet;
                    xlWorksheet.Name = "PhieuDanSach";

                    xlWorksheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlWorksheet.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    for (int j = 0; j < dtExport.Columns.Count; j++)
                    {
                        var cell = xlWorksheet.Cells[1, j + 1];
                        cell.Value = dtExport.Columns[j].ColumnName;
                        cell.Font.Bold = true;
                        cell.Font.Size = 12;
                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                        cell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    }
                    xlWorksheet.Rows[1].RowHeight = 35;

                    for (int i = 0; i < dtExport.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtExport.Columns.Count; j++)
                        {
                            var cell = xlWorksheet.Cells[i + 2, j + 1];

                            if (j == 3)
                            {
                                string maVachGoc = dtExport.Rows[i][j].ToString();
                                cell.Value = $"*{maVachGoc}*";
                                cell.Font.Name = "IDAutomationHC39M Free Version";
                                cell.Font.Size = 12;
                            }
                            else
                            {
                                cell.Value = dtExport.Rows[i][j].ToString();
                                cell.Font.Name = "Arial";
                                cell.Font.Size = 11;
                            }

                            cell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        }
                        xlWorksheet.Rows[i + 2].RowHeight = 78;
                    }

                    xlWorksheet.Columns[1].ColumnWidth = 12;
                    xlWorksheet.Columns[2].ColumnWidth = 20;
                    xlWorksheet.Columns[3].ColumnWidth = 16;
                    xlWorksheet.Columns[4].ColumnWidth = 40;

                    xlWorkbook.SaveAs(sfd.FileName);
                    xlWorkbook.Close();
                    xlApp.Quit();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);

                    if (MessageBox.Show("Đã tạo file phiếu dán thành công! Bạn có muốn mở Excel lên xem ngay không?", "Hoàn tất", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất file (Excel phải được cài trên máy): \n" + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    xlApp.Quit();
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            selectedBookID = "";
            txtMaSach.Enabled = true;
            txtMaSach.Clear();
            txtTenSach.Clear();
            txtTacGia.Clear();
            txtNhaXuatBan.Clear();
            txtNamXuatBan.Clear();
            txtGiaSach.Clear();
            if (cboTheLoai.Items.Count > 0) cboTheLoai.SelectedIndex = 0;
            if (cboLoaiSach.Items.Count > 0) cboLoaiSach.SelectedIndex = 0;
        }
    }
}