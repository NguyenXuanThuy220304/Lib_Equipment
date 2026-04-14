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

                // === ĐÃ FIX LỖI: HÚT DỮ LIỆU TỪ 3 CỘT MỚI BẮN LÊN TEXTBOX ===
                if (dgvSach.Columns.Contains("Price") && row.Cells["Price"].Value != null)
                    txtGiaSach.Text = row.Cells["Price"].Value.ToString();

                if (dgvSach.Columns.Contains("BookType") && row.Cells["BookType"].Value != null)
                    cboLoaiSach.Text = row.Cells["BookType"].Value.ToString();

                // Vì ô txtSoTrang bạn tạo sau bằng code chay, nên cần check null để tránh lỗi
                if (dgvSach.Columns.Contains("PageCount") && row.Cells["PageCount"].Value != null && txtSoTrang != null)
                    txtSoTrang.Text = row.Cells["PageCount"].Value.ToString();

                txtMaSach.Enabled = false; // Không cho sửa khóa chính
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (SachBLL.Instance.ThemSach(txtMaSach.Text.Trim(), txtTenSach.Text.Trim(), txtTacGia.Text.Trim(), txtNhaXuatBan.Text.Trim(), txtNamXuatBan.Text.Trim(), cboTheLoai.SelectedValue.ToString(), txtGiaSach.Text.Trim(), cboLoaiSach.Text, txtSoTrang.Text.Trim(), out string msg))
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); btnLamMoi_Click(null, null);
            }
            else MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (SachBLL.Instance.SuaSach(txtMaSach.Text.Trim(), txtTenSach.Text.Trim(), txtTacGia.Text.Trim(), txtNhaXuatBan.Text.Trim(), txtNamXuatBan.Text.Trim(), cboTheLoai.SelectedValue.ToString(), txtGiaSach.Text.Trim(), cboLoaiSach.Text, txtSoTrang.Text.Trim()))
            {
                MessageBox.Show("Cập nhật Đầu sách thành công!", "Thông báo");
                LoadData();
            }
        }

        // ĐÃ FIX: Khôi phục lại hàm btnXoa_Click
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedBookID)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa đầu sách này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SachBLL.Instance.XoaSach(selectedBookID))
                {
                    MessageBox.Show("Đã xóa sách khỏi danh mục!", "Thông báo");
                    LoadData();
                    btnLamMoi_Click(null, null);
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

                    // ÉP CĂN GIỮA TOÀN BỘ SHEET (CẢ NGANG LẪN DỌC)
                    xlWorksheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlWorksheet.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    // 1. Tạo Tiêu đề các cột (Dòng 1)
                    for (int j = 0; j < dtExport.Columns.Count; j++)
                    {
                        var cell = xlWorksheet.Cells[1, j + 1];
                        cell.Value = dtExport.Columns[j].ColumnName;
                        cell.Font.Bold = true;
                        cell.Font.Size = 12;
                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                        cell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    }
                    xlWorksheet.Rows[1].RowHeight = 35; // Chiều cao dòng tiêu đề

                    // 2. Đổ dữ liệu sách (Từ dòng 2 trở đi)
                    for (int i = 0; i < dtExport.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtExport.Columns.Count; j++)
                        {
                            var cell = xlWorksheet.Cells[i + 2, j + 1];

                            if (j == 3) // CỘT 4: MÃ VẠCH (BARCODE)
                            {
                                string maVachGoc = dtExport.Rows[i][j].ToString();
                                // Chèn thêm dấu * ở 2 đầu (Bắt buộc đối với font Code 39 để máy quét hiểu)
                                cell.Value = $"*{maVachGoc}*";

                                // Tên font trong máy bạn đang dùng (như trong ảnh là IDAutomation)
                                // Nếu tên font trong máy bạn khác đi 1 chút, hãy sửa lại đoạn chữ màu đỏ bên dưới nhé!
                                cell.Font.Name = "IDAutomationHC39M Free Version";
                                cell.Font.Size = 12;
                            }
                            else
                            {
                                // Các cột chữ bình thường
                                cell.Value = dtExport.Rows[i][j].ToString();
                                cell.Font.Name = "Arial";
                                cell.Font.Size = 11;
                            }

                            // Kẻ khung
                            cell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        }

                        // Chỉnh chiều cao dòng cực lớn (55) để hiện vừa mã vạch như trong ảnh
                        xlWorksheet.Rows[i + 2].RowHeight = 78;
                    }

                    // 3. Set độ rộng (Column Width) cố định cho từng cột như ảnh mẫu
                    xlWorksheet.Columns[1].ColumnWidth = 12; // Vị trí tủ
                    xlWorksheet.Columns[2].ColumnWidth = 20; // Mã cuốn sách
                    xlWorksheet.Columns[3].ColumnWidth = 16; // Năm xuất bản
                    xlWorksheet.Columns[4].ColumnWidth = 40; // Mã vạch (Cần siêu rộng)

                    // Lưu file
                    xlWorkbook.SaveAs(sfd.FileName);
                    xlWorkbook.Close();
                    xlApp.Quit();

                    // Dọn dẹp RAM
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
        // ĐÃ FIX: Khôi phục lại hàm btnLamMoi_Click
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