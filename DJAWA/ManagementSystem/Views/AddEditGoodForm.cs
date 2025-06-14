using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using ManagementSystem.Models;
using ManagementSystem.Models.Builders;
using ManagementSystem.Services;
using ManagementSystem.Repositories;

namespace ManagementSystem.Views
{
    public partial class AddEditGoodForm : Form
    {
        private readonly IGoodService _goodService;
        private readonly GoodBuilder _goodBuilder;
        private Good _good;
        private bool _isEditMode;

        private TextBox _nameTextBox;
        private TextBox _descTextBox;
        private NumericUpDown _quantityNumeric;
        private NumericUpDown _priceNumeric;
        private Label _errorLabel;
        private Button _saveButton;

        public AddEditGoodForm(Good good = null)
        {
            _goodService = new GoodService(new GoodRepository());
            _goodBuilder = new GoodBuilder();
            _good = good ?? new Good();
            _isEditMode = good != null;

            InitializeComponent();
            this.Load += AddEditGoodForm_Load;
        }

        private void AddEditGoodForm_Load(object sender, EventArgs e)
        {
            SetupModernUI();
            LoadData();
            // Ensure form is centered properly
            this.CenterToParent();
        }

        private void InitializeComponent()
        {
            this.Text = _isEditMode ? "✏ Edit Barang" : "➕ Tambah Barang Baru";
            this.Size = new Size(600, 600);
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
                Height = 80,
                BackColor = Color.FromArgb(99, 102, 241),
                Padding = new Padding(25, 15, 25, 15)
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
                Text = _isEditMode ? "EDIT BARANG" : "TAMBAH BARANG BARU",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(25, 15)
            };

            var subtitleLabel = new Label
            {
                Text = _isEditMode ? "Perbarui informasi barang" : "Masukkan detail barang baru",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(196, 181, 253),
                AutoSize = true,
                Location = new Point(25, 45)
            };

            var closeButton = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(30, 30),
                Location = new Point(headerPanel.Width - 55, 15),
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

            headerPanel.Controls.AddRange(new Control[] { titleLabel, subtitleLabel, closeButton });
            this.Controls.Add(headerPanel);
        }

        private void CreateMainPanel()
        {
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 25, 30, 25),
                BackColor = Color.White,
                AutoScroll = true
            };

            int currentY = 0;

            _errorLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(220, 38, 38),
                Location = new Point(0, currentY),
                Size = new Size(510, 35),
                Visible = false,
                BackColor = Color.FromArgb(254, 242, 242),
                Padding = new Padding(12, 8, 12, 8),
                BorderStyle = BorderStyle.None
            };
            mainPanel.Controls.Add(_errorLabel);
            currentY += 45;

            // NAMA BARANG
            var nameLabel = new Label
            {
                Text = "📦 Nama Barang *",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(0, currentY),
                Size = new Size(510, 22),
                AutoSize = false
            };
            mainPanel.Controls.Add(nameLabel);
            currentY += 30;

            _nameTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, currentY),
                Size = new Size(510, 40),
                BorderStyle = BorderStyle.FixedSingle,
                Text = "Masukkan nama barang...",
                ForeColor = Color.Gray,
                Padding = new Padding(10, 8, 10, 8)
            };
            _nameTextBox.Enter += (s, e) =>
            {
                if (_nameTextBox.Text == "Masukkan nama barang...")
                {
                    _nameTextBox.Text = "";
                    _nameTextBox.ForeColor = Color.Black;
                }
                _nameTextBox.BackColor = Color.FromArgb(240, 253, 255);
            };
            _nameTextBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(_nameTextBox.Text))
                {
                    _nameTextBox.Text = "Masukkan nama barang...";
                    _nameTextBox.ForeColor = Color.Gray;
                }
                _nameTextBox.BackColor = Color.White;
            };
            _nameTextBox.TextChanged += ValidateInput;
            mainPanel.Controls.Add(_nameTextBox);
            currentY += 50;

            // DESKRIPSI
            var descLabel = new Label
            {
                Text = "📝 Deskripsi",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(0, currentY),
                Size = new Size(510, 22),
                AutoSize = false
            };
            mainPanel.Controls.Add(descLabel);
            currentY += 30;

            _descTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, currentY),
                Size = new Size(510, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Text = "Deskripsi barang (opsional)...",
                ForeColor = Color.Gray,
                Padding = new Padding(10, 8, 10, 8)
            };
            _descTextBox.Enter += (s, e) =>
            {
                if (_descTextBox.Text == "Deskripsi barang (opsional)...")
                {
                    _descTextBox.Text = "";
                    _descTextBox.ForeColor = Color.Black;
                }
                _descTextBox.BackColor = Color.FromArgb(240, 253, 255);
            };
            _descTextBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(_descTextBox.Text))
                {
                    _descTextBox.Text = "Deskripsi barang (opsional)...";
                    _descTextBox.ForeColor = Color.Gray;
                }
                _descTextBox.BackColor = Color.White;
            };
            _descTextBox.TextChanged += ValidateInput;
            mainPanel.Controls.Add(_descTextBox);
            currentY += 80;

            // JUMLAH STOK
            var quantityLabel = new Label
            {
                Text = "📊 Jumlah Stok *",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(0, currentY),
                Size = new Size(510, 22),
                AutoSize = false
            };
            mainPanel.Controls.Add(quantityLabel);
            currentY += 30;

            var quantityContainer = new Panel
            {
                Location = new Point(0, currentY),
                Size = new Size(510, 40)
            };

            _quantityNumeric = new NumericUpDown
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, 0),
                Size = new Size(180, 35),
                Minimum = 0,
                Maximum = 999999,
                BorderStyle = BorderStyle.FixedSingle
            };
            _quantityNumeric.ValueChanged += ValidateInput;
            _quantityNumeric.Enter += (s, e) => _quantityNumeric.BackColor = Color.FromArgb(240, 253, 255);
            _quantityNumeric.Leave += (s, e) => _quantityNumeric.BackColor = Color.White;
            quantityContainer.Controls.Add(_quantityNumeric);

            var unitLabel = new Label
            {
                Text = "pcs",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(190, 10),
                Size = new Size(50, 20),
                AutoSize = true
            };
            quantityContainer.Controls.Add(unitLabel);
            mainPanel.Controls.Add(quantityContainer);
            currentY += 50;

            // HARGA
            var priceLabel = new Label
            {
                Text = "💰 Harga (Rp) *",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(0, currentY),
                Size = new Size(510, 22),
                AutoSize = false
            };
            mainPanel.Controls.Add(priceLabel);
            currentY += 30;

            var priceContainer = new Panel
            {
                Location = new Point(0, currentY),
                Size = new Size(510, 40)
            };

            _priceNumeric = new NumericUpDown
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, 0),
                Size = new Size(200, 35),
                Minimum = 0.01m,
                Maximum = 999999999.99m,
                DecimalPlaces = 2,
                ThousandsSeparator = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            _priceNumeric.ValueChanged += ValidateInput;
            _priceNumeric.Enter += (s, e) => _priceNumeric.BackColor = Color.FromArgb(240, 253, 255);
            _priceNumeric.Leave += (s, e) => _priceNumeric.BackColor = Color.White;
            priceContainer.Controls.Add(_priceNumeric);

            var currencyLabel = new Label
            {
                Text = "IDR",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(210, 10),
                Size = new Size(50, 20),
                AutoSize = true
            };
            priceContainer.Controls.Add(currencyLabel);
            mainPanel.Controls.Add(priceContainer);
            currentY += 50;

            // HELP TEXT
            var helpLabel = new Label
            {
                Text = "💡 Tips: Gunakan deskripsi yang jelas untuk memudahkan pencarian",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(0, currentY),
                Size = new Size(510, 20),
                AutoSize = false
            };
            mainPanel.Controls.Add(helpLabel);

            this.Controls.Add(mainPanel);
        }

        private void CreateButtonPanel()
        {
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(30, 20, 30, 20)
            };

            _saveButton = new Button
            {
                Text = _isEditMode ? "💾 Update Barang" : "💾 Simpan Barang",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(180, 40),
                Location = new Point(60, 20),
                BackColor = Color.FromArgb(34, 197, 94),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            _saveButton.FlatAppearance.BorderSize = 0;
            _saveButton.Click += SaveButton_Click;

            var cancelButton = new Button
            {
                Text = "✕ Batal",
                Font = new Font("Segoe UI", 10),
                Size = new Size(120, 40),
                Location = new Point(300, 20),
                BackColor = Color.FromArgb(107, 114, 128),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            cancelButton.FlatAppearance.BorderSize = 0;
            cancelButton.Click += (s, e) => this.Close();

            // Add keyboard shortcuts info
            var shortcutLabel = new Label
            {
                Text = "Ctrl+S: Simpan • Esc: Batal",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(0, 2),
                Size = new Size(200, 15),
                AutoSize = false
            };

            buttonPanel.Controls.AddRange(new Control[] { _saveButton, cancelButton, shortcutLabel });
            this.Controls.Add(buttonPanel);

            // Add hover effects
            SetupButtonHoverEffects();

            // Add keyboard shortcuts
            this.KeyDown += (s, e) => {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    if (_saveButton.Enabled)
                        SaveButton_Click(_saveButton, EventArgs.Empty);
                    e.Handled = true;
                }
            };
        }

        private void SetupModernUI()
        {
            this.Region = CreateRoundedRectangle(this.ClientRectangle, 12);
        }

        private void SetupButtonHoverEffects()
        {
            var originalSaveColor = Color.FromArgb(34, 197, 94);
            var hoverSaveColor = Color.FromArgb(22, 163, 74);

            _saveButton.MouseEnter += (s, e) => _saveButton.BackColor = hoverSaveColor;
            _saveButton.MouseLeave += (s, e) => _saveButton.BackColor = originalSaveColor;

            // Find cancel button and add hover effect
            var cancelButton = this.Controls.OfType<Panel>()
                .SelectMany(p => p.Controls.OfType<Button>())
                .FirstOrDefault(b => b.Text == "✕ Batal");

            if (cancelButton != null)
            {
                var originalCancelColor = Color.FromArgb(107, 114, 128);
                var hoverCancelColor = Color.FromArgb(75, 85, 99);
                cancelButton.MouseEnter += (s, e) => cancelButton.BackColor = hoverCancelColor;
                cancelButton.MouseLeave += (s, e) => cancelButton.BackColor = originalCancelColor;
            }
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
            if (_isEditMode && _good != null)
            {
                _nameTextBox.Text = _good.Name;
                _nameTextBox.ForeColor = string.IsNullOrEmpty(_good.Name) ? Color.Gray : Color.Black;

                if (!string.IsNullOrEmpty(_good.Description))
                {
                    _descTextBox.Text = _good.Description;
                    _descTextBox.ForeColor = Color.Black;
                }
                else
                {
                    _descTextBox.Text = "Deskripsi barang (opsional)...";
                    _descTextBox.ForeColor = Color.Gray;
                }

                _quantityNumeric.Value = _good.Quantity;
                _priceNumeric.Value = _good.Price;

                // Trigger validation after loading data
                ValidateInput(null, null);
            }
        }

        private void ValidateInput(object sender, EventArgs e)
        {
            var name = _nameTextBox.Text == "Masukkan nama barang..." ? "" : _nameTextBox.Text;
            var desc = _descTextBox.Text == "Deskripsi barang (opsional)..." ? "" : _descTextBox.Text;

            if (ValidateInputWithBuilder(name, desc, (int)_quantityNumeric.Value, _priceNumeric.Value, out var errors))
            {
                HideError();
                _saveButton.Enabled = true;
                _saveButton.BackColor = Color.FromArgb(34, 197, 94);
            }
            else
            {
                ShowError(string.Join(", ", errors));
                _saveButton.Enabled = false;
                _saveButton.BackColor = Color.FromArgb(156, 163, 175);
            }
        }

        private bool ValidateInputWithBuilder(string name, string description, int quantity, decimal price, out List<string> errors)
        {
            _goodBuilder.Reset()
                       .SetName(name)
                       .SetDescription(description)
                       .SetQuantity(quantity)
                       .SetPrice(price);

            errors = _goodBuilder.ValidationErrors;
            return _goodBuilder.IsValid;
        }

        private void ShowError(string message)
        {
            _errorLabel.Text = "⚠ " + message;
            _errorLabel.Visible = true;
        }

        private void HideError()
        {
            _errorLabel.Visible = false;
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                _saveButton.Enabled = false;
                _saveButton.Text = "💾 Menyimpan...";
                this.Cursor = Cursors.WaitCursor;

                var name = _nameTextBox.Text == "Masukkan nama barang..." ? "" : _nameTextBox.Text;
                var desc = _descTextBox.Text == "Deskripsi barang (opsional)..." ? "" : _descTextBox.Text;

                if (_isEditMode)
                {
                    _good = _goodBuilder
                        .Reset()
                        .SetId(_good.Id)
                        .SetName(name)
                        .SetDescription(desc)
                        .SetQuantity((int)_quantityNumeric.Value)
                        .SetPrice(_priceNumeric.Value)
                        .UpdateTimestamp()
                        .Build();

                    await _goodService.UpdateGoodAsync(_good);
                    ShowSuccessMessage("✅ Barang berhasil diupdate!");
                }
                else
                {
                    _good = _goodBuilder
                        .Reset()
                        .SetName(name)
                        .SetDescription(desc)
                        .SetQuantity((int)_quantityNumeric.Value)
                        .SetPrice(_priceNumeric.Value)
                        .Build();

                    await _goodService.CreateGoodAsync(_good);
                    ShowSuccessMessage("✅ Barang berhasil ditambahkan!");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ValidationException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                ShowError($"Terjadi kesalahan: {ex.Message}");
                MessageBox.Show($"Detail Error:\n\n{ex.Message}", "❌ Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _saveButton.Enabled = true;
                _saveButton.Text = _isEditMode ? "💾 Update Barang" : "💾 Simpan Barang";
                this.Cursor = Cursors.Default;
            }
        }

        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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