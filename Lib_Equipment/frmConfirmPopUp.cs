using System;
using System.Windows.Forms;

namespace Lib_Equipment
{
    public partial class frmConfirmPopUp : Form
    {
        // Thuộc tính để bên ngoài đọc được xem người dùng có ấn Xác nhận không
        public bool IsConfirmed { get; private set; } = false;

        public frmConfirmPopUp(string aiResponse, string tenSach, string copyId)
        {
            InitializeComponent();

            // Gán thông tin từ Form phụ truyền sang
            lblAiResponse.Text = $"\"{aiResponse}\"";
            lblBookInfo.Text = $"📚 Cuốn sách: {tenSach}\n🆔 Mã vạch: {copyId}";
        }

        // ==========================================================
        // [THÊM MỚI]: Hỗ trợ xác nhận Giỏ hàng nhiều sách cùng lúc
        // ==========================================================
        public frmConfirmPopUp(string aiResponse, int soLuong, string danhSachMa)
        {
            InitializeComponent();
            lblAiResponse.Text = $"\"{aiResponse}\"";

            // Cắt chuỗi nếu dài quá để không tràn giao diện
            if (danhSachMa.Length > 40) danhSachMa = danhSachMa.Substring(0, 37) + "...";

            lblBookInfo.Text = $"📚 Đang mượn: {soLuong} cuốn sách\n🆔 Các mã: {danhSachMa}";
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            IsConfirmed = true;
            this.Close(); // Đóng popup lại
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            IsConfirmed = false;
            this.Close(); // Đóng popup lại
        }
    }
}