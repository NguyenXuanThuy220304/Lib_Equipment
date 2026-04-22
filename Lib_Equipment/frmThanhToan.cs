using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QRCoder;
using Newtonsoft.Json;
using Lib_Equipment.Database;

namespace Lib_Equipment
{
    public partial class frmThanhToan : Form
    {
        // ═══════════════════════════════════════════════════════
        //  CẤU HÌNH — Khớp với Web (UNETI Bank)
        // ═══════════════════════════════════════════════════════
        private const string LIBRARIAN_STK = "22103100046";
        private const string LIBRARIAN_NAME = "Nguyễn Xuân Thùy";
        private const string BANK_NAME = "UNETI Bank";

        private readonly string _readerID;
        private readonly string _fullName;
        private readonly decimal _amount;
        private CancellationTokenSource _cts;
        private bool _paid = false;

        // ── Màu sắc MBBank-style ──
        private static readonly Color C_BLUE_DARK = Color.FromArgb(10, 45, 110);
        private static readonly Color C_BLUE_MID = Color.FromArgb(26, 79, 160);
        private static readonly Color C_TEXT = Color.FromArgb(26, 31, 54);
        private static readonly Color C_MUTED = Color.FromArgb(107, 114, 128);
        private static readonly Color C_BORDER = Color.FromArgb(221, 226, 238);
        private static readonly Color C_BG = Color.FromArgb(242, 244, 248);
        private static readonly Color C_RED = Color.FromArgb(232, 32, 58);
        private static readonly Color C_GREEN = Color.FromArgb(22, 163, 74);
        private static readonly Color C_WHITE = Color.White;

        // Các Controls vẽ bằng code
        private Panel pnlHeader, pnlBody, pnlRecvCard, pnlArrow, pnlSendCard, pnlAmountCard, pnlQRCard, pnlPoll, pnlStatus;
        private Label lblBrand, lblHeaderTitle, lblRecvTitle, lblRecvNameLbl, lblRecvNameVal, lblRecvSTKLbl, lblRecvSTKVal, lblRecvBankLbl, lblRecvBankVal;
        private Label lblSendTitle, lblSendNameLbl, lblSendNameVal, lblSendSTKLbl, lblSendSTKVal;
        private Label lblAmountTitle, lblAmountLbl, lblAmountVal, lblNoteLbl, lblNoteVal;
        private Label lblQRTitle, lblQRHint, lblPoll, lblStatus, lblStatusSub;
        private Button btnClose;
        private PictureBox picQR;

        // ═══════════════════════════════════════════════════════
        //  CONSTRUCTOR
        // ═══════════════════════════════════════════════════════
        public frmThanhToan(string readerID, string fullName, decimal amount)
        {
            // Đã xóa InitializeComponent() vì không cần thiết khi vẽ bằng code

            _readerID = readerID;
            _fullName = fullName;
            _amount = amount;

            BuildUI(); // Vẽ đè giao diện đẹp lên
            this.Load += FrmThanhToan_Load;
        }

        private void FrmThanhToan_Load(object sender, EventArgs e)
        {
            GenerateQR();
            StartDatabasePolling();
        }

        private void BuildUI()
        {
            this.Text = "UNETI Bank — Thanh toán công nợ";
            this.Size = new Size(440, 820);
            this.MinimumSize = new Size(400, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = C_BG;
            this.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Header
            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 64, BackColor = C_BLUE_DARK };
            pnlHeader.Paint += (s, e) => DrawHeaderGradient(e.Graphics, pnlHeader.ClientRectangle);

            lblBrand = new Label { Text = "UNETI BANK", Font = new Font("Segoe UI", 9f, FontStyle.Bold), ForeColor = Color.FromArgb(180, 210, 255), AutoSize = true, Location = new Point(20, 10) };
            lblHeaderTitle = new Label { Text = "Thanh toán công nợ", Font = new Font("Segoe UI", 13f, FontStyle.Bold), ForeColor = C_WHITE, AutoSize = true, Location = new Point(20, 30) };
            btnClose = new Button { Text = "✕", Size = new Size(36, 36), FlatStyle = FlatStyle.Flat, ForeColor = C_WHITE, BackColor = Color.Transparent, Font = new Font("Segoe UI", 11f), Cursor = Cursors.Hand, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 255, 255, 255);
            btnClose.Click += (s, e) => { _cts?.Cancel(); this.Close(); };
            pnlHeader.Controls.AddRange(new Control[] { lblBrand, lblHeaderTitle, btnClose });
            pnlHeader.Resize += (s, e) => btnClose.Location = new Point(pnlHeader.Width - 46, 14);

            // Receiver Card
            pnlRecvCard = MakeCard(0);
            lblRecvTitle = MakeCardTitle("THÔNG TIN NGƯỜI NHẬN");
            lblRecvNameLbl = MakeLabel("Tên người nhận", 0, 36);
            lblRecvNameVal = MakeValue(LIBRARIAN_NAME, 0, 36, bold: true, color: C_BLUE_MID);
            lblRecvSTKLbl = MakeLabel("Số tài khoản", 0, 66);
            lblRecvSTKVal = MakeValue(LIBRARIAN_STK, 0, 66, bold: true, color: C_BLUE_MID);
            lblRecvBankLbl = MakeLabel("Ngân hàng", 0, 96);
            lblRecvBankVal = MakeValue(BANK_NAME, 0, 96, bold: false);
            pnlRecvCard.Height = 128;
            pnlRecvCard.Controls.AddRange(new Control[] { lblRecvTitle, lblRecvNameLbl, lblRecvNameVal, lblRecvSTKLbl, lblRecvSTKVal, lblRecvBankLbl, lblRecvBankVal });
            pnlRecvCard.Paint += PaintCardSeparators;
            pnlRecvCard.Resize += (s, e) => AlignValues(pnlRecvCard);

            // Arrow
            pnlArrow = new Panel { Height = 36, BackColor = C_BG };
            pnlArrow.Controls.Add(new Label { Text = "↓ Từ tài khoản", Font = new Font("Segoe UI", 9f), ForeColor = C_MUTED, AutoSize = true, Location = new Point(0, 10) });

            // Sender Card
            pnlSendCard = MakeCard(0);
            lblSendTitle = MakeCardTitle("NGƯỜI CHUYỂN");
            lblSendNameLbl = MakeLabel("Họ tên", 0, 36);
            lblSendNameVal = MakeValue(_fullName, 0, 36, bold: true);
            lblSendSTKLbl = MakeLabel("Mã độc giả (STK)", 0, 66);
            lblSendSTKVal = MakeValue(_readerID, 0, 66, bold: true, color: C_BLUE_MID);
            pnlSendCard.Height = 98;
            pnlSendCard.Controls.AddRange(new Control[] { lblSendTitle, lblSendNameLbl, lblSendNameVal, lblSendSTKLbl, lblSendSTKVal });
            pnlSendCard.Paint += PaintCardSeparators;
            pnlSendCard.Resize += (s, e) => AlignValues(pnlSendCard);

            // Amount Card
            pnlAmountCard = MakeCard(10);
            pnlAmountCard.BackColor = Color.FromArgb(255, 249, 249);
            lblAmountTitle = MakeCardTitle("CHI TIẾT GIAO DỊCH");
            lblAmountLbl = MakeLabel("Số tiền thanh toán", 0, 36);
            lblAmountVal = MakeValue(_amount.ToString("N0") + " VNĐ", 0, 33, bold: true, fontSize: 16f, color: C_RED);
            lblNoteLbl = MakeLabel("Nội dung CK", 0, 70);
            lblNoteVal = MakeValue($"{_fullName} - {_readerID} - Thanh toan no", 0, 70, bold: false, color: C_MUTED);
            lblNoteVal.Font = new Font("Segoe UI", 8.5f);
            pnlAmountCard.Height = 108;
            pnlAmountCard.Controls.AddRange(new Control[] { lblAmountTitle, lblAmountLbl, lblAmountVal, lblNoteLbl, lblNoteVal });
            pnlAmountCard.Paint += PaintCardSeparators;
            pnlAmountCard.Resize += (s, e) => AlignValues(pnlAmountCard);

            // QR Card
            pnlQRCard = MakeCard(10);
            lblQRTitle = MakeCardTitle("QUÉT MÃ QR ĐỂ THANH TOÁN");
            picQR = new PictureBox { Size = new Size(200, 200), SizeMode = PictureBoxSizeMode.Zoom, BackColor = C_WHITE, Location = new Point(0, 30), BorderStyle = BorderStyle.None, Padding = new Padding(6) };
            picQR.Paint += DrawQRBorder;
            lblQRHint = new Label { Text = "📱 Mở UNETI Bank → Quét mã QR", Font = new Font("Segoe UI", 9f), ForeColor = C_MUTED, AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Height = 22, Location = new Point(0, 238) };
            pnlQRCard.Height = 278;
            pnlQRCard.Controls.AddRange(new Control[] { lblQRTitle, picQR, lblQRHint });
            pnlQRCard.Resize += (s, e) => {
                int inner = pnlQRCard.Width - 32;
                picQR.Width = picQR.Height = Math.Min(200, inner);
                picQR.Left = (inner - picQR.Width) / 2 + 16;
                lblQRHint.Width = inner; lblQRHint.Left = 16; lblQRHint.Top = picQR.Bottom + 8;
                pnlQRCard.Height = lblQRHint.Bottom + 16;
            };

            // Polling
            pnlPoll = new Panel { Height = 36, BackColor = C_BG };
            lblPoll = new Label { Text = "⏳ Đang chờ xác nhận từ hệ thống...", Font = new Font("Segoe UI", 9f, FontStyle.Italic), ForeColor = C_MUTED, AutoSize = true, Location = new Point(0, 10) };
            pnlPoll.Controls.Add(lblPoll);

            // Status Banner
            pnlStatus = new Panel { Height = 120, BackColor = Color.FromArgb(240, 253, 244), Visible = false };
            pnlStatus.Paint += DrawSuccessBorder;
            lblStatus = new Label { Text = "✅ Thanh toán công nợ thành công!", Font = new Font("Segoe UI", 13f, FontStyle.Bold), ForeColor = C_GREEN, AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Top, Height = 52 };
            lblStatusSub = new Label { Text = $"Đã thanh toán {_amount:N0} VNĐ · Công nợ = 0 VNĐ", Font = new Font("Segoe UI", 9f), ForeColor = Color.FromArgb(22, 101, 52), AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Top, Height = 28 };

            var btnDone = new Button { Text = "Đóng", Dock = DockStyle.Bottom, Height = 36, FlatStyle = FlatStyle.Flat, BackColor = C_GREEN, ForeColor = C_WHITE, Font = new Font("Segoe UI", 10f, FontStyle.Bold), Cursor = Cursors.Hand };
            btnDone.FlatAppearance.BorderSize = 0;
            btnDone.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };
            pnlStatus.Controls.AddRange(new Control[] { lblStatus, lblStatusSub, btnDone });

            var flow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true, BackColor = C_BG, Padding = new Padding(14, 14, 14, 20) };
            flow.Controls.AddRange(new Control[] { pnlRecvCard, pnlArrow, pnlSendCard, pnlAmountCard, pnlQRCard, pnlPoll, pnlStatus });
            flow.Resize += (s, e) => {
                int w = flow.ClientSize.Width - flow.Padding.Horizontal;
                foreach (Control c in flow.Controls) { if (c != pnlArrow && c != pnlPoll) c.Width = w; }
            };

            this.Controls.Add(flow);
            this.Controls.Add(pnlHeader);
        }

        private void GenerateQR()
        {
            try
            {
                var payload = new
                {
                    app = "uneti_bank",
                    receiver_name = LIBRARIAN_NAME,
                    receiver_stk = LIBRARIAN_STK,
                    bank = BANK_NAME,
                    amount = _amount,
                    note = $"{_fullName} - {_readerID} - Thanh toan cong no",
                    sender_stk = _readerID
                };
                string json = JsonConvert.SerializeObject(payload);

                using (var qrGen = new QRCodeGenerator())
                {
                    var qrData = qrGen.CreateQrCode(json, QRCodeGenerator.ECCLevel.M);
                    using (var qrCode = new PngByteQRCode(qrData))
                    {
                        byte[] qrBytes = qrCode.GetGraphic(5, new byte[] { 10, 45, 110, 255 }, new byte[] { 255, 255, 255, 255 }, true);
                        using (var ms = new System.IO.MemoryStream(qrBytes))
                        {
                            picQR.Image = new Bitmap(ms);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblQRHint.Text = $"Lỗi tạo QR: {ex.Message}";
                lblQRHint.ForeColor = C_RED;
            }
        }

        private void StartDatabasePolling()
        {
            _cts = new CancellationTokenSource();
            Task.Run(async () =>
            {
                var deadline = DateTime.UtcNow.AddMinutes(15);
                int dots = 0;

                while (DateTime.UtcNow < deadline && !_cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        string query = "SELECT ISNULL(AcademicDebt, 0) FROM Reader WHERE ReaderID = @id";
                        object result = DataProvider.Instance.ExecuteScalar(query, new SqlParameter[] { new SqlParameter("@id", _readerID) });

                        if (result != null)
                        {
                            decimal currentDebt = Convert.ToDecimal(result);
                            if (currentDebt == 0)
                            {
                                this.Invoke((Action)ShowSuccess);
                                return;
                            }
                        }
                    }
                    catch { /* Ignore error */ }

                    dots = (dots % 3) + 1;
                    string d = new string('.', dots);
                    this.Invoke((Action)(() =>
                    {
                        lblPoll.Text = $"⏳ Đang chờ xác nhận từ hệ thống{d}";
                    }));

                    await Task.Delay(2000, _cts.Token).ContinueWith(_ => { });
                }
            }, _cts.Token);
        }

        private void ShowSuccess()
        {
            _paid = true;

            pnlQRCard.Visible = false;
            pnlPoll.Visible = false;
            pnlArrow.Visible = false;
            pnlSendCard.Visible = false;
            pnlRecvCard.Visible = false;
            pnlAmountCard.Visible = false;

            lblHeaderTitle.Text = "Thanh toán thành công";
            pnlHeader.BackColor = C_GREEN;

            pnlStatus.Visible = true;
            pnlStatus.Height = 130;

            int countdown = 3;
            lblStatusSub.Text = $"Đã thanh toán {_amount:N0} VNĐ · Công nợ = 0\nTự động đóng sau {countdown} giây...";

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer() { Interval = 1000 };
            t.Tick += (s, ev) =>
            {
                countdown--;
                if (countdown <= 0)
                {
                    t.Stop();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblStatusSub.Text = $"Đã thanh toán {_amount:N0} VNĐ · Công nợ = 0\nTự động đóng sau {countdown} giây...";
                }
            };
            t.Start();
        }

        private static void AlignValues(Panel card) { int right = card.ClientSize.Width - 16; foreach (Control c in card.Controls) { if (c is Label lbl && lbl.TextAlign == ContentAlignment.MiddleRight) { lbl.Width = card.ClientSize.Width / 2 + 20; lbl.Left = right - lbl.Width; } } }

        private void DrawHeaderGradient(Graphics g, Rectangle r)
        {
            using (var brush = new LinearGradientBrush(r, C_BLUE_DARK, C_BLUE_MID, 135f))
            {
                g.FillRectangle(brush, r);
            }
        }

        private void PaintCardSeparators(object sender, PaintEventArgs e)
        {
            var p = (Panel)sender;
            var g = e.Graphics;
            using (var pen = new Pen(C_BORDER, 1))
            {
                int[] ys = { 56, 86, 116 };
                foreach (var y in ys)
                {
                    if (y < p.Height - 10) g.DrawLine(pen, 0, y, p.Width, y);
                }
            }
        }

        private void DrawQRBorder(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(C_BORDER, 1.5f))
            {
                g.DrawRoundedRectangle(pen, new Rectangle(1, 1, picQR.Width - 3, picQR.Height - 3), 10);
            }
        }

        private void DrawSuccessBorder(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            using (var pen = new Pen(Color.FromArgb(187, 247, 208), 2))
            {
                g.DrawRectangle(pen, new Rectangle(0, 0, pnlStatus.Width - 1, pnlStatus.Height - 1));
            }
        }

        private Panel MakeCard(int topMargin) { return new Panel { BackColor = C_WHITE, Margin = new Padding(0, topMargin, 0, 0), Padding = new Padding(16, 10, 16, 10), Height = 100 }; }
        private Label MakeCardTitle(string text) { return new Label { Text = text, Font = new Font("Segoe UI", 8f, FontStyle.Bold), ForeColor = C_MUTED, AutoSize = true, Location = new Point(16, 12) }; }
        private Label MakeLabel(string text, int x, int y) { return new Label { Text = text, Font = new Font("Segoe UI", 9f), ForeColor = C_MUTED, AutoSize = true, Location = new Point(16, y) }; }
        private Label MakeValue(string text, int x, int y, bool bold = false, float fontSize = 9.5f, Color? color = null) { return new Label { Text = text, Font = new Font("Segoe UI", fontSize, bold ? FontStyle.Bold : FontStyle.Regular), ForeColor = color ?? C_TEXT, AutoSize = false, TextAlign = ContentAlignment.MiddleRight, Height = 22, Anchor = AnchorStyles.Top | AnchorStyles.Right, Location = new Point(x, y - 2) }; }

        protected override void OnFormClosing(FormClosingEventArgs e) { _cts?.Cancel(); base.OnFormClosing(e); }
    }

    internal static class GraphicsExtensions
    {
        public static void DrawRoundedRectangle(this Graphics g, Pen pen, Rectangle r, int radius)
        {
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddArc(r.X, r.Y, radius * 2, radius * 2, 180, 90);
                path.AddArc(r.Right - radius * 2, r.Y, radius * 2, radius * 2, 270, 90);
                path.AddArc(r.Right - radius * 2, r.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddArc(r.X, r.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                path.CloseFigure();
                g.DrawPath(pen, path);
            }
        }
    }
}