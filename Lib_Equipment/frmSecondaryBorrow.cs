using Lib_Equipment.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lib_Equipment
{
    public partial class frmSecondaryBorrow : Form
    {
        private string readerID;
        private string fullName;
        private string apiKey;
        private string currentCopyID;

        public frmSecondaryBorrow(string readerID, string fullName, string apiKey)
        {
            InitializeComponent();
            this.readerID = readerID;
            this.fullName = fullName;
            this.apiKey = apiKey;

            this.Load += (s, e) => { txtBarcodeScanner.Clear(); txtBarcodeScanner.Focus(); };
            this.Click += (s, e) => { txtBarcodeScanner.Focus(); };

            txtBarcodeScanner.Leave += (s, e) => { txtBarcodeScanner.Focus(); };
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            txtBarcodeScanner.Clear();
            txtBarcodeScanner.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Nếu bấm nút X thì đóng form này lại
        }

        private async void txtBarcodeScanner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string copyId = txtBarcodeScanner.Text.Trim();
                txtBarcodeScanner.Clear();

                if (string.IsNullOrEmpty(copyId)) return;

                // 1. Kiểm tra CSDL
                string checkQuery = $@"
                    SELECT b.Title, b.Author, bc.Status 
                    FROM BookCopy bc 
                    JOIN Book b ON bc.BookID = b.BookID 
                    WHERE bc.CopyID = @copyID AND bc.IsDeleted = 0";

                SqlParameter[] paramCheck = { new SqlParameter("@copyID", copyId) };
                DataTable dtSach = DataProvider.Instance.ExecuteQuery(checkQuery, paramCheck);

                if (dtSach.Rows.Count > 0)
                {
                    string tenSach = dtSach.Rows[0]["Title"].ToString();
                    string tacGia = dtSach.Rows[0]["Author"].ToString();
                    string trangThai = dtSach.Rows[0]["Status"].ToString();

                    if (trangThai == "Có sẵn")
                    {
                        currentCopyID = copyId;

                        // 2. In thông tin sách ra Form
                        lblBookTitle.Text = tenSach;
                        lblBookAuthor.Text = "Tác giả: " + tacGia;
                        lblStatus.Text = "Trạng thái: " + trangThai;

                        lblStatus.Text = "Đang gọi AI xử lý...";
                        lblStatus.ForeColor = System.Drawing.Color.Goldenrod;

                        // Tạm thời gỡ ép Focus để tránh kẹt khi hiện Popup
                        txtBarcodeScanner.Leave -= (s, ev) => { txtBarcodeScanner.Focus(); };

                        // 3. Gọi AI Assistant và hiện Popup xác nhận
                        await GoiAIVaConfirm(tenSach);
                    }
                    else
                    {
                        MessageBox.Show($"Cuốn sách này hiện đang: '{trangThai}'. Không thể mượn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtBarcodeScanner.Clear();
                        txtBarcodeScanner.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Mã vạch không hợp lệ hoặc sách không có trong hệ thống!", "Lỗi Quét", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBarcodeScanner.Clear();
                    txtBarcodeScanner.Focus();
                }
            }
        }

        private async Task GoiAIVaConfirm(string tenSach)
        {
            string aiPrompt = $"Sinh viên {fullName} vừa quét mã vạch mượn sách '{tenSach}'. Đóng vai Trợ lý AI Thư viện, hãy nói 1 câu chào thân thiện, khen ngợi việc chọn sách, và xác nhận mượn (Dưới 30 chữ).";
            string aiResponse = await CallGeminiAPI(aiPrompt);

            lblStatus.Text = "Đã nhận diện thành công!";
            lblStatus.ForeColor = System.Drawing.Color.Green;

            // Mở Popup Guna
            frmConfirmPopUp confirm = new frmConfirmPopUp(aiResponse, tenSach, currentCopyID);
            confirm.ShowDialog();

            if (confirm.IsConfirmed)
            {
                XuLyMuonSachThanhCong(currentCopyID);
                this.Close(); 
            }
            else
            {
                lblBookTitle.Text = "Vui lòng quét mã vạch trên sách...";
                lblBookAuthor.Text = "";
                lblStatus.Text = "";

                txtBarcodeScanner.Leave += (s, ev) => { txtBarcodeScanner.Focus(); };
                txtBarcodeScanner.Clear();
                txtBarcodeScanner.Focus();
            }
        }

        private void XuLyMuonSachThanhCong(string copyId)
        {
            string query = @"
                BEGIN TRY
                    BEGIN TRAN;
                    
                    -- 1. Bỏ cột RecordID đi để SQL TỰ ĐỘNG TĂNG
                    INSERT INTO BorrowRecord (ReaderID, CreatedBy, BorrowDate, DueDate, Status, IsDeleted) 
                    VALUES (@readerId, @readerId, GETDATE(), DATEADD(day, 14, GETDATE()), N'Đang mượn', 0);

                    -- 2. Lấy cái RecordID vừa được SQL tự động đếm ở bước 1
                    DECLARE @newRecordId INT = SCOPE_IDENTITY();

                    -- 3. Dùng @newRecordId đó lưu vào bảng Chi tiết mượn (BorrowDetail)
                    INSERT INTO BorrowDetail (RecordID, CopyID, FineAmount) 
                    VALUES (@newRecordId, @copyId, 0);

                    -- 4. Cập nhật trạng thái sách
                    UPDATE BookCopy SET Status = N'Đang mượn' WHERE CopyID = @copyId;
                    
                    COMMIT TRAN;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRAN;
                    THROW;
                END CATCH
            ";

            SqlParameter[] param = {
                new SqlParameter("@readerId", readerID),
                new SqlParameter("@copyId", copyId)
            };

            try
            {
                DataProvider.Instance.ExecuteNonQuery(query, param);
                MessageBox.Show("Mượn sách thành công! Bạn có 14 ngày để trả sách nhé.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi tạo phiếu mượn: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<string> CallGeminiAPI(string prompt)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";
                    var payload = new { contents = new[] { new { parts = new[] { new { text = prompt } } } }, generationConfig = new { temperature = 0.7 } };

                    var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        JObject jsonResponse = JObject.Parse(responseString);
                        return jsonResponse["candidates"][0]["content"]["parts"][0]["text"].ToString().Trim();
                    }
                    return $"Cuốn sách '{prompt.Split('\'')[1]}' đã sẵn sàng. Bạn có muốn hoàn tất phiếu mượn không?";
                }
            }
            catch
            {
                return $"Hệ thống đã nhận diện sách. Nhấn XÁC NHẬN để mượn cuốn này.";
            }
        }
    }
}