using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ManagementSystem.Models;
using ManagementSystem.Services;
using ManagementSystem.Repositories;

namespace ManagementSystem.Views
{
    public partial class GoodDetailForm : Form
    {
        private readonly Good _good;
        private readonly IGoodService _goodService;

        public GoodDetailForm(Good good)
        {
            _good = good;
            _goodService = new GoodService(new GoodRepository());
            InitializeComponent();
            SetupModernUI();
            LoadData();
        }

        // Method helper untuk format Rupiah
        private string FormatToRupiah(decimal amount)
        {
            return $"Rp {amount:N0}";
        }

        private void InitializeComponent()
        {
            this.Text = "📋 Detail Barang";
            this.Size = new Size(750, 800); // TINGGI banget jadi 800px!
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;

            this.MouseDown += Form_MouseDown;

            CreateHeaderPanel();
            CreateMainPanel();
            CreateButtonPanel();

            this.Paint += AddShadowEffect;

            // Add ESC key handler to close form
            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            };
        }

        private void CreateHeaderPanel()
        {
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110, // Lebih tinggi lagi
                BackColor = Color.FromArgb(99, 102, 241),
                Padding = new Padding(40, 20, 40, 20) // Padding lebih lega
            };

            // Enhanced gradient background
            headerPanel.Paint += (s, e) =>
            {
                using (var brush = new LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(99, 102, 241),
                    Color.FromArgb(79, 70, 229),
                    LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }
            };

            var titleLabel = new Label
            {
                Text = "DETAIL BARANG",
                Font = new Font("Segoe UI", 20, FontStyle.Bold), // Font lebih gede
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 20)
            };

            var subtitleLabel = new Label
            {
                Name = "subtitleLabel",
                Font = new Font("Segoe UI", 12), // Font lebih gede
                ForeColor = Color.FromArgb(196, 181, 253),
                AutoSize = true,
                Location = new Point(40, 55),
                MaximumSize = new Size(600, 0)
            };

            var statusBadge = new Label
            {
                Name = "statusBadge",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(34, 197, 94),
                ForeColor = Color.White,
                Size = new Size(100, 25), // Lebih gede
                Location = new Point(40, 80),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "AKTIF"
            };

            var closeButton = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 18, FontStyle.Bold), // Font lebih gede
                Size = new Size(40, 40), // Lebih gede
                Location = new Point(headerPanel.Width - 80, 20),
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (s, e) => this.Close();
            closeButton.MouseEnter += (s, e) => closeButton.BackColor = Color.FromArgb(50, 255, 255, 255);
            closeButton.MouseLeave += (s, e) => closeButton.BackColor = Color.Transparent;

            headerPanel.Controls.AddRange(new Control[] { titleLabel, subtitleLabel, statusBadge, closeButton });
            this.Controls.Add(headerPanel);
        }

        private void CreateMainPanel()
        {
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40, 40, 40, 40), // Padding LEGA banget
                BackColor = Color.White,
                AutoScroll = false
            };

            var nameCard = CreateDetailCard("📦 Nama Barang", "nameValue", 0);
            var descCard = CreateDetailCard("📝 Deskripsi", "descValue", 120, true); // Jarak LEGA banget (120)
            var infoCard = CreateInfoCard(260); // Jarak LEGA banget (260)
            var timestampCard = CreateTimestampCard(400); // Jarak LEGA banget (400)

            mainPanel.Controls.AddRange(new Control[] { nameCard, descCard, infoCard, timestampCard });
            this.Controls.Add(mainPanel);
        }

        private Panel CreateDetailCard(string title, string valueName, int y, bool multiline = false)
        {
            var card = new Panel
            {
                Size = new Size(650, multiline ? 110 : 80), // GEDE banget
                Location = new Point(0, y),
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(30, 20, 30, 20) // Padding lega
            };

            // Add subtle border
            card.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(229, 231, 235), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 13, FontStyle.Bold), // Font gede
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(0, 0),
                Size = new Size(590, 28),
                AutoSize = false
            };

            var valueLabel = new Label
            {
                Name = valueName,
                Font = new Font("Segoe UI", 14), // Font gede
                ForeColor = Color.FromArgb(17, 24, 39),
                Location = new Point(0, 32),
                Size = new Size(590, multiline ? 65 : 35),
                AutoSize = false,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Padding = new Padding(18, 12, 18, 12)
            };

            card.Controls.AddRange(new Control[] { titleLabel, valueLabel });
            return card;
        }

        private Panel CreateInfoCard(int y)
        {
            var card = new Panel
            {
                Size = new Size(650, 110), // GEDE banget
                Location = new Point(0, y),
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(30, 20, 30, 20) // Padding lega
            };

            // Add subtle border
            card.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(229, 231, 235), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
            };

            var titleLabel = new Label
            {
                Text = "📊 Informasi Stok & Harga",
                Font = new Font("Segoe UI", 13, FontStyle.Bold), // Font gede
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(0, 0),
                Size = new Size(590, 28)
            };

            var quantityPanel = new Panel
            {
                Size = new Size(290, 65), // Lebih gede
                Location = new Point(0, 32),
                BackColor = Color.White,
                Padding = new Padding(25, 15, 25, 15)
            };

            var qtyLabel = new Label
            {
                Text = "Jumlah Stok",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(0, 0),
                Size = new Size(240, 20)
            };

            var qtyValue = new Label
            {
                Name = "quantityValue",
                Font = new Font("Segoe UI", 18, FontStyle.Bold), // Font lebih gede
                ForeColor = Color.FromArgb(34, 197, 94),
                Location = new Point(0, 23),
                Size = new Size(240, 32)
            };

            quantityPanel.Controls.AddRange(new Control[] { qtyLabel, qtyValue });

            var pricePanel = new Panel
            {
                Size = new Size(300, 65), // Lebih gede
                Location = new Point(320, 32),
                BackColor = Color.White,
                Padding = new Padding(25, 15, 25, 15)
            };

            var priceLabel = new Label
            {
                Text = "Harga Satuan",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(0, 0),
                Size = new Size(250, 20)
            };

            var priceValue = new Label
            {
                Name = "priceValue",
                Font = new Font("Segoe UI", 18, FontStyle.Bold), // Font lebih gede
                ForeColor = Color.FromArgb(59, 130, 246),
                Location = new Point(0, 23),
                Size = new Size(250, 32)
            };

            pricePanel.Controls.AddRange(new Control[] { priceLabel, priceValue });

            card.Controls.AddRange(new Control[] { titleLabel, quantityPanel, pricePanel });
            return card;
        }

        private Panel CreateTimestampCard(int y)
        {
            var card = new Panel
            {
                Size = new Size(650, 110), // GEDE banget
                Location = new Point(0, y),
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(30, 20, 30, 20) // Padding lega
            };

            // Add subtle border
            card.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(229, 231, 235), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
            };

            var titleLabel = new Label
            {
                Text = "🕒 Riwayat Waktu",
                Font = new Font("Segoe UI", 13, FontStyle.Bold), // Font gede
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(0, 0),
                Size = new Size(590, 28)
            };

            var createdPanel = new Panel
            {
                Size = new Size(290, 65), // Lebih gede
                Location = new Point(0, 32),
                BackColor = Color.White,
                Padding = new Padding(25, 15, 25, 15)
            };

            var createdLabel = new Label
            {
                Text = "Dibuat Pada",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(0, 0),
                Size = new Size(240, 20)
            };

            var createdValue = new Label
            {
                Name = "createdValue",
                Font = new Font("Segoe UI", 12), // Font lebih gede
                ForeColor = Color.FromArgb(17, 24, 39),
                Location = new Point(0, 23),
                Size = new Size(240, 32)
            };

            createdPanel.Controls.AddRange(new Control[] { createdLabel, createdValue });

            var updatedPanel = new Panel
            {
                Size = new Size(300, 65), // Lebih gede
                Location = new Point(320, 32),
                BackColor = Color.White,
                Padding = new Padding(25, 15, 25, 15)
            };

            var updatedLabel = new Label
            {
                Text = "Terakhir Diupdate",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(0, 0),
                Size = new Size(250, 20)
            };

            var updatedValue = new Label
            {
                Name = "updatedValue",
                Font = new Font("Segoe UI", 12), // Font lebih gede
                ForeColor = Color.FromArgb(17, 24, 39),
                Location = new Point(0, 23),
                Size = new Size(250, 32)
            };

            updatedPanel.Controls.AddRange(new Control[] { updatedLabel, updatedValue });

            card.Controls.AddRange(new Control[] { titleLabel, createdPanel, updatedPanel });
            return card;
        }

        private void CreateButtonPanel()
        {
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 100, // TINGGI banget
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(40, 35, 40, 35) // Padding LEGA banget
            };

            var editButton = new Button
            {
                Text = "✏ Edit Barang",
                Font = new Font("Segoe UI", 13, FontStyle.Bold), // Font gede
                Size = new Size(160, 45), // GEDE banget
                Location = new Point(280, 28),
                BackColor = Color.FromArgb(59, 130, 246),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            editButton.FlatAppearance.BorderSize = 0;

            var deleteButton = new Button
            {
                Text = "🗑 Hapus",
                Font = new Font("Segoe UI", 13, FontStyle.Bold), // Font gede
                Size = new Size(120, 45), // GEDE banget
                Location = new Point(450, 28),
                BackColor = Color.FromArgb(220, 38, 38),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            deleteButton.FlatAppearance.BorderSize = 0;

            var closeButton = new Button
            {
                Text = "✕ Tutup",
                Font = new Font("Segoe UI", 12), // Font gede
                Size = new Size(110, 45), // GEDE banget
                Location = new Point(580, 28),
                BackColor = Color.FromArgb(107, 114, 128),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            closeButton.FlatAppearance.BorderSize = 0;

            // Add keyboard shortcuts info
            var shortcutLabel = new Label
            {
                Text = "F2: Edit • Del: Hapus • Esc: Tutup",
                Font = new Font("Segoe UI", 10), // Font gede
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(0, 8),
                Size = new Size(250, 20),
                AutoSize = false
            };

            // Setup hover effects
            SetupButtonHover(editButton, Color.FromArgb(59, 130, 246), Color.FromArgb(37, 99, 235));
            SetupButtonHover(deleteButton, Color.FromArgb(220, 38, 38), Color.FromArgb(185, 28, 28));
            SetupButtonHover(closeButton, Color.FromArgb(107, 114, 128), Color.FromArgb(75, 85, 99));

            buttonPanel.Controls.AddRange(new Control[] { shortcutLabel, editButton, deleteButton, closeButton });
            this.Controls.Add(buttonPanel);

            editButton.Click += EditButton_Click;
            deleteButton.Click += DeleteButton_Click;
            closeButton.Click += (s, e) => this.Close();

            // Add keyboard shortcuts
            this.KeyDown += (s, e) => {
                switch (e.KeyCode)
                {
                    case Keys.F2:
                        EditButton_Click(editButton, EventArgs.Empty);
                        e.Handled = true;
                        break;
                    case Keys.Delete:
                        DeleteButton_Click(deleteButton, EventArgs.Empty);
                        e.Handled = true;
                        break;
                }
            };
        }

        private void SetupButtonHover(Button button, Color normal, Color hover)
        {
            button.MouseEnter += (s, e) => button.BackColor = hover;
            button.MouseLeave += (s, e) => button.BackColor = normal;
        }

        private void SetupModernUI()
        {
            this.Region = CreateRoundedRectangle(this.ClientRectangle, 12);
        }

        private Region CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return new Region(path);
        }

        private void LoadData()
        {
            var subtitleLabel = this.Controls.Find("subtitleLabel", true)[0] as Label;
            subtitleLabel.Text = $"ID: #{_good.Id} • {DateTime.Now.ToString("dd MMMM yyyy")} • Total Nilai: {FormatToRupiah(_good.Price * _good.Quantity)}";

            var statusBadge = this.Controls.Find("statusBadge", true)[0] as Label;
            if (_good.Quantity == 0)
            {
                statusBadge.Text = "HABIS";
                statusBadge.BackColor = Color.FromArgb(220, 38, 38);
            }
            else if (_good.Quantity < 10)
            {
                statusBadge.Text = "MENIPIS";
                statusBadge.BackColor = Color.FromArgb(245, 158, 11);
            }
            else
            {
                statusBadge.Text = "TERSEDIA";
                statusBadge.BackColor = Color.FromArgb(34, 197, 94);
            }

            var nameValue = this.Controls.Find("nameValue", true)[0] as Label;
            nameValue.Text = _good.Name;

            var descValue = this.Controls.Find("descValue", true)[0] as Label;
            descValue.Text = string.IsNullOrEmpty(_good.Description) ?
                "Tidak ada deskripsi tersedia" : _good.Description;

            var quantityValue = this.Controls.Find("quantityValue", true)[0] as Label;
            quantityValue.Text = $"{_good.Quantity:N0} unit";

            var priceValue = this.Controls.Find("priceValue", true)[0] as Label;
            priceValue.Text = FormatToRupiah(_good.Price);

            var createdValue = this.Controls.Find("createdValue", true)[0] as Label;
            createdValue.Text = _good.CreatedAt.ToString("dd MMM yyyy\nHH:mm");

            var updatedValue = this.Controls.Find("updatedValue", true)[0] as Label;
            updatedValue.Text = _good.UpdatedAt.ToString("dd MMM yyyy\nHH:mm");

            // Update quantity value color based on stock level
            if (_good.Quantity == 0)
            {
                quantityValue.ForeColor = Color.FromArgb(220, 38, 38);
            }
            else if (_good.Quantity < 10)
            {
                quantityValue.ForeColor = Color.FromArgb(245, 158, 11);
            }
            else
            {
                quantityValue.ForeColor = Color.FromArgb(34, 197, 94);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var editForm = new AddEditGoodForm(_good);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                $"⚠ PERINGATAN PENGHAPUSAN ⚠\n\nAnda akan menghapus barang:\n\nNama: {_good.Name}\nStok: {_good.Quantity} unit\nNilai: {FormatToRupiah(_good.Price * _good.Quantity)}\n\nTindakan ini TIDAK DAPAT dibatalkan!\n\nApakah Anda yakin ingin melanjutkan?",
                "🗑 Konfirmasi Hapus Barang",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    await _goodService.DeleteGoodAsync(_good.Id);

                    MessageBox.Show($"✅ Barang '{_good.Name}' berhasil dihapus dari sistem!",
                        "Penghapusan Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Terjadi kesalahan saat menghapus barang:\n\n{ex.Message}",
                        "Error Penghapusan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        private void AddShadowEffect(object sender, PaintEventArgs e)
        {
            using (var brush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
            {
                e.Graphics.FillRectangle(brush, new Rectangle(8, 8, this.Width, this.Height));
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    }
}