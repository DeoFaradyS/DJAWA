using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManagementSystem.Models;
using ManagementSystem.Models.Builders;
using ManagementSystem.Services;
using ManagementSystem.Repositories;

namespace ManagementSystem.Views
{
    public partial class MainForm : Form
    {
        private readonly IGoodService _goodService;
        private List<Good> _currentGoods;

        private TextBox _searchTextBox;
        private DataGridView _dataGridView;
        private Label _statusLabel;
        private Panel _statsPanel;
        private Button _addButton;
        private Button _refreshButton;

        public MainForm()
        {
            InitializeComponent();
            _goodService = new GoodService(new GoodRepository());
            _currentGoods = new List<Good>();

            this.Load += MainForm_Load;
            this.Shown += MainForm_Shown;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            SetupPlaceholderText();
            SetupButtonHoverEffects();
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private void InitializeComponent()
        {
            this.Text = "🏪 Sistem Manajemen Barang - Inventory Pro";
            this.Size = new Size(1400, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.MinimumSize = new Size(1200, 700);
            this.WindowState = FormWindowState.Normal;
            this.Icon = SystemIcons.Application;

            // Setup anti-aliasing for better text rendering
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            // Urutan PENTING: Bottom dulu, baru Top, terakhir Fill
            CreateStatusPanel();      // Bottom - 35px (diperbesar)
            CreateDataGridPanel();    // Fill - AMBIL SPACE DULU
            CreateActionPanel();      // Top - 70px (diperbesar)
            CreateStatsPanel();       // Top - 80px (diperbesar)
            CreateHeaderPanel();      // Top - 100px (diperbesar)
        }

        private void CreateHeaderPanel()
        {
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(99, 102, 241),
                Margin = new Padding(0, 0, 0, 10)
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

                // Add subtle shadow effect
                using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(shadowBrush, new Rectangle(0, headerPanel.Height - 3, headerPanel.Width, 3));
                }
            };

            var titleLabel = new Label
            {
                Text = "SISTEM MANAJEMEN BARANG",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 20)
            };

            var subtitleLabel = new Label
            {
                Text = "Kelola inventori barang dengan mudah dan efisien • Inventory Management Pro",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(196, 181, 253),
                AutoSize = true,
                Location = new Point(40, 60)
            };

            // Add version label
            var versionLabel = new Label
            {
                Text = "v2.0",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(196, 181, 253),
                AutoSize = true,
                Location = new Point(headerPanel.Width - 80, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            headerPanel.Controls.AddRange(new Control[] { titleLabel, subtitleLabel, versionLabel });
            this.Controls.Add(headerPanel);
        }

        private void CreateStatsPanel()
        {
            _statsPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.White,
                Padding = new Padding(40, 15, 40, 15)
            };

            // Add subtle border
            _statsPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(229, 231, 235), 1))
                {
                    e.Graphics.DrawLine(pen, 0, _statsPanel.Height - 1, _statsPanel.Width, _statsPanel.Height - 1);
                }
            };

            this.Controls.Add(_statsPanel);
        }

        private void CreateActionPanel()
        {
            var actionPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(40, 15, 40, 15)
            };

            // Search container with modern styling
            var searchContainer = new Panel
            {
                Size = new Size(320, 40),
                Location = new Point(0, 15),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            _searchTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(35, 8),
                Size = new Size(280, 25),
                BorderStyle = BorderStyle.None,
                BackColor = Color.White
            };

            // Search icon
            var searchIcon = new Label
            {
                Text = "🔍",
                Font = new Font("Segoe UI", 12),
                Location = new Point(8, 8),
                Size = new Size(25, 25),
                ForeColor = Color.FromArgb(107, 114, 128)
            };

            searchContainer.Controls.AddRange(new Control[] { searchIcon, _searchTextBox });

            _addButton = new Button
            {
                Text = "➕ Tambah Barang",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(160, 40),
                Location = new Point(340, 15),
                BackColor = Color.FromArgb(59, 130, 246),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            _addButton.FlatAppearance.BorderSize = 0;

            _refreshButton = new Button
            {
                Text = "🔄 Refresh Data",
                Font = new Font("Segoe UI", 10),
                Size = new Size(120, 40),
                Location = new Point(510, 15),
                BackColor = Color.FromArgb(34, 197, 94),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            _refreshButton.FlatAppearance.BorderSize = 0;

            var clearButton = new Button
            {
                Text = "✕ Clear",
                Font = new Font("Segoe UI", 10),
                Size = new Size(80, 40),
                Location = new Point(640, 15),
                BackColor = Color.FromArgb(107, 114, 128),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            clearButton.FlatAppearance.BorderSize = 0;
            clearButton.Click += (s, e) => {
                _searchTextBox.Text = "Cari nama barang...";
                _searchTextBox.ForeColor = Color.Gray;
                PopulateDataGridView(_currentGoods);
                SetStatus($"✅ Menampilkan semua {_currentGoods.Count} barang");
            };

            // Export button
            var exportButton = new Button
            {
                Text = "📊 Export",
                Font = new Font("Segoe UI", 10),
                Size = new Size(100, 40),
                Location = new Point(730, 15),
                BackColor = Color.FromArgb(139, 92, 246),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            exportButton.FlatAppearance.BorderSize = 0;
            exportButton.Click += ExportButton_Click;

            actionPanel.Controls.AddRange(new Control[] {
                searchContainer, _addButton, _refreshButton, clearButton, exportButton
            });

            _addButton.Click += AddButton_Click;
            _refreshButton.Click += RefreshButton_Click;
            _searchTextBox.KeyDown += SearchTextBox_KeyDown;

            this.Controls.Add(actionPanel);
        }

        private void CreateDataGridPanel()
        {
            var dataPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40, 20, 40, 20),
                BackColor = Color.FromArgb(248, 250, 252)
            };

            _dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10),
                GridColor = Color.FromArgb(229, 231, 235),
                RowTemplate = { Height = 45 },
                ScrollBars = ScrollBars.Both,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                AllowUserToResizeRows = false
            };

            // Add border to data grid
            var borderPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(1),
                BackColor = Color.FromArgb(229, 231, 235)
            };
            borderPanel.Controls.Add(_dataGridView);

            dataPanel.Controls.Add(borderPanel);
            this.Controls.Add(dataPanel);
        }

        private void CreateStatusPanel()
        {
            var statusPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 35,
                BackColor = Color.FromArgb(55, 65, 81),
                Padding = new Padding(40, 8, 40, 8)
            };

            _statusLabel = new Label
            {
                Text = "Siap • Ready to serve",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 10)
            };

            // Add current time label
            var timeLabel = new Label
            {
                Text = DateTime.Now.ToString("dd MMMM yyyy • HH:mm:ss"),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(156, 163, 175),
                AutoSize = true,
                Location = new Point(statusPanel.Width - 200, 12),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            // Update time every second
            var timer = new Timer { Interval = 1000 };
            timer.Tick += (s, e) => timeLabel.Text = DateTime.Now.ToString("dd MMMM yyyy • HH:mm:ss");
            timer.Start();

            statusPanel.Controls.AddRange(new Control[] { _statusLabel, timeLabel });
            this.Controls.Add(statusPanel);
        }

        private void SetupDataGridView()
        {
            _dataGridView.Columns.Clear();

            _dataGridView.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251),
                SelectionBackColor = Color.FromArgb(59, 130, 246),
                SelectionForeColor = Color.White
            };

            _dataGridView.DefaultCellStyle = new DataGridViewCellStyle
            {
                SelectionBackColor = Color.FromArgb(59, 130, 246),
                SelectionForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Padding = new Padding(10, 5, 10, 5)
            };

            _dataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(55, 65, 81),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(10, 8, 10, 8)
            };

            _dataGridView.ColumnHeadersHeight = 40;
            _dataGridView.EnableHeadersVisualStyles = false;

            _dataGridView.Columns.Add("Id", "ID");
            _dataGridView.Columns.Add("Name", "Nama Barang");
            _dataGridView.Columns.Add("Description", "Deskripsi");
            _dataGridView.Columns.Add("Quantity", "Stok");
            _dataGridView.Columns.Add("Price", "Harga");
            _dataGridView.Columns.Add("Status", "Status");
            _dataGridView.Columns.Add("CreatedAt", "Tanggal");

            _dataGridView.Columns["Id"].Visible = false;
            _dataGridView.Columns["Name"].Width = 200;
            _dataGridView.Columns["Description"].Width = 250;
            _dataGridView.Columns["Quantity"].Width = 100;
            _dataGridView.Columns["Price"].Width = 150;
            _dataGridView.Columns["Status"].Width = 120;
            _dataGridView.Columns["CreatedAt"].Width = 120;

            _dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
            _dataGridView.KeyDown += DataGridView_KeyDown;
            _dataGridView.CellFormatting += DataGridView_CellFormatting;
        }

        private void SetupPlaceholderText()
        {
            string placeholderText = "Cari nama barang...";
            _searchTextBox.Text = placeholderText;
            _searchTextBox.ForeColor = Color.Gray;

            _searchTextBox.Enter += (sender, e) =>
            {
                if (_searchTextBox.Text == placeholderText)
                {
                    _searchTextBox.Text = "";
                    _searchTextBox.ForeColor = Color.Black;
                }
            };

            _searchTextBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(_searchTextBox.Text))
                {
                    _searchTextBox.Text = placeholderText;
                    _searchTextBox.ForeColor = Color.Gray;
                }
            };
        }

        private void SetupButtonHoverEffects()
        {
            SetupButtonHover(_addButton, Color.FromArgb(59, 130, 246), Color.FromArgb(37, 99, 235));
            SetupButtonHover(_refreshButton, Color.FromArgb(34, 197, 94), Color.FromArgb(22, 163, 74));
        }

        private void SetupButtonHover(Button button, Color normal, Color hover)
        {
            button.MouseEnter += (s, e) => button.BackColor = hover;
            button.MouseLeave += (s, e) => button.BackColor = normal;
        }

        private async Task LoadDataAsync()
        {
            try
            {
                SetStatus("📥 Memuat data barang...");
                this.Cursor = Cursors.WaitCursor;

                _currentGoods = await _goodService.GetAllGoodsAsync();

                if (_currentGoods == null)
                    _currentGoods = new List<Good>();

                PopulateDataGridView(_currentGoods);
                UpdateStatsPanel();
                SetStatus($"✅ Berhasil memuat {_currentGoods.Count} barang • Ready");
            }
            catch (Exception ex)
            {
                _currentGoods = new List<Good>();
                MessageBox.Show($"Terjadi kesalahan saat memuat data:\n\n{ex.Message}",
                    "❌ Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("❌ Gagal memuat data • Error");

                PopulateDataGridView(_currentGoods);
                UpdateStatsPanel();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async Task CreateSampleData()
        {
            try
            {
                SetStatus("📦 Membuat data contoh...");

                var goodBuilder = new GoodBuilder();

                var sampleData = new[]
                {
                    new { Name = "Laptop Asus ROG", Description = "Laptop gaming ROG Strix G15 dengan RTX 4060", Quantity = 15, Price = 18500000m },
                    new { Name = "Mouse Gaming Logitech", Description = "Mouse gaming G502 HERO dengan sensor 25K DPI", Quantity = 45, Price = 850000m },
                    new { Name = "Keyboard Mechanical", Description = "Keyboard gaming Cherry MX Blue Switch RGB", Quantity = 8, Price = 1250000m },
                    new { Name = "Monitor Gaming 27\"", Description = "Monitor curved 165Hz 1440p dengan HDR", Quantity = 12, Price = 4500000m },
                    new { Name = "Headset SteelSeries", Description = "Headset gaming Arctis 7 wireless dengan DTS", Quantity = 25, Price = 2200000m },
                    new { Name = "Webcam Logitech C920", Description = "Webcam 1080p HD untuk streaming dan meeting", Quantity = 0, Price = 1450000m },
                    new { Name = "SSD Samsung 1TB", Description = "SSD NVMe 980 PRO dengan kecepatan baca 7GB/s", Quantity = 35, Price = 2100000m },
                    new { Name = "RAM Corsair 32GB", Description = "RAM DDR4 3200MHz kit 2x16GB untuk gaming", Quantity = 6, Price = 3200000m }
                };

                foreach (var data in sampleData)
                {
                    var good = goodBuilder
                        .Reset()
                        .SetName(data.Name)
                        .SetDescription(data.Description)
                        .SetQuantity(data.Quantity)
                        .SetPrice(data.Price)
                        .Build();

                    await _goodService.CreateGoodAsync(good);
                }

                SetStatus("✅ Data contoh berhasil dibuat • Sample data created");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat membuat data contoh:\n\n{ex.Message}",
                    "❌ Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateDataGridView(List<Good> goods)
        {
            _dataGridView.Rows.Clear();

            if (goods == null || goods.Count == 0)
                return;

            foreach (var good in goods)
            {
                var status = GetStockStatus(good.Quantity);

                _dataGridView.Rows.Add(
                    good.Id,
                    good.Name ?? "No Name",
                    string.IsNullOrEmpty(good.Description) ? "Tidak ada deskripsi" : good.Description,
                    $"{good.Quantity:N0} unit",
                    $"Rp {good.Price:N0}",
                    status,
                    good.CreatedAt.ToString("dd MMM yyyy")
                );
            }

            _dataGridView.Refresh();
        }

        private string GetStockStatus(int quantity)
        {
            if (quantity == 0) return "❌ Stok Habis";
            if (quantity < 10) return "⚠ Stok Menipis";
            if (quantity < 20) return "⚡ Stok Terbatas";
            return "✅ Stok Tersedia";
        }

        private void UpdateStatsPanel()
        {
            _statsPanel.Controls.Clear();

            var totalItems = _currentGoods.Count;
            var totalStock = _currentGoods.Sum(g => g.Quantity);
            var totalValue = _currentGoods.Sum(g => g.Price * g.Quantity);
            var lowStockCount = _currentGoods.Count(g => g.Quantity < 10);
            var outOfStockCount = _currentGoods.Count(g => g.Quantity == 0);

            // Format nilai dalam jutaan untuk display yang lebih clean
            string valueDisplay = totalValue >= 1000000000
                ? $"Rp {(totalValue / 1000000000):N1}B"
                : totalValue >= 1000000
                ? $"Rp {(totalValue / 1000000):N1}M"
                : $"Rp {totalValue:N0}";

            CreateStatCard($"📦 {totalItems} Total Barang", Color.FromArgb(59, 130, 246), 0);
            CreateStatCard($"📊 {totalStock:N0} Total Stok", Color.FromArgb(34, 197, 94), 200);
            CreateStatCard($"💰 {valueDisplay}", Color.FromArgb(139, 92, 246), 400);
            CreateStatCard($"⚠ {lowStockCount} Menipis", Color.FromArgb(245, 158, 11), 600);
            CreateStatCard($"❌ {outOfStockCount} Habis", Color.FromArgb(239, 68, 68), 800);
        }

        private void CreateStatCard(string text, Color color, int x)
        {
            var card = new Panel
            {
                Size = new Size(180, 50),
                Location = new Point(x, 15),
                BackColor = color,
                Padding = new Padding(15, 10, 15, 10)
            };

            var label = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            card.Controls.Add(label);
            _statsPanel.Controls.Add(card);

            // Rounded corners and shadow effect
            card.Paint += (s, e) =>
            {
                using (var path = CreateRoundedRectanglePath(card.ClientRectangle, 10))
                using (var brush = new SolidBrush(color))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(brush, path);
                }
            };

            // Add hover effect
            card.MouseEnter += (s, e) => {
                var hoverColor = ControlPaint.Light(color, 0.1f);
                card.BackColor = hoverColor;
            };
            card.MouseLeave += (s, e) => card.BackColor = color;
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        private async void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await PerformSearch();
                e.Handled = true;
            }
        }

        private async Task PerformSearch()
        {
            string searchTerm = _searchTextBox.Text.Trim();

            if (searchTerm == "Cari nama barang..." || string.IsNullOrWhiteSpace(searchTerm))
            {
                PopulateDataGridView(_currentGoods);
                SetStatus($"✅ Menampilkan semua {_currentGoods.Count} barang");
                return;
            }

            try
            {
                SetStatus("🔍 Mencari barang...");
                this.Cursor = Cursors.WaitCursor;

                var results = await _goodService.SearchGoodsByNameAsync(searchTerm);
                PopulateDataGridView(results);

                SetStatus($"🔍 Ditemukan {results.Count} hasil untuk '{searchTerm}'");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat mencari:\n\n{ex.Message}",
                    "❌ Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("❌ Pencarian gagal");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var addForm = new AddEditGoodForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                _ = LoadDataAsync();
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            _ = LoadDataAsync();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentGoods == null || _currentGoods.Count == 0)
                {
                    MessageBox.Show("Tidak ada data untuk diekspor!", "⚠ Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var dialog = new SaveFileDialog())
                {
                    dialog.Filter = "CSV files (.csv)|.csv";
                    dialog.Title = "Export Data Barang";
                    dialog.FileName = $"data_barang_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var csv = "ID,Nama Barang,Deskripsi,Stok,Harga,Status,Tanggal Dibuat\n";
                        foreach (var good in _currentGoods)
                        {
                            csv += $"{good.Id},{good.Name},{good.Description},{good.Quantity},{good.Price},{GetStockStatus(good.Quantity)},{good.CreatedAt:yyyy-MM-dd}\n";
                        }

                        System.IO.File.WriteAllText(dialog.FileName, csv);
                        MessageBox.Show($"Data berhasil diekspor ke:\n{dialog.FileName}",
                            "✅ Export Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal mengekspor data:\n\n{ex.Message}",
                    "❌ Export Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ShowGoodDetail(e.RowIndex);
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            var dataGridView = sender as DataGridView;

            if (e.KeyCode == Keys.Enter && dataGridView.SelectedRows.Count > 0)
            {
                ShowGoodDetail(dataGridView.SelectedRows[0].Index);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete && dataGridView.SelectedRows.Count > 0)
            {
                DeleteSelectedGood(dataGridView.SelectedRows[0].Index);
                e.Handled = true;
            }
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_dataGridView.Columns[e.ColumnIndex].Name == "Status")
            {
                string status = e.Value?.ToString();
                switch (status)
                {
                    case "❌ Stok Habis":
                        e.CellStyle.BackColor = Color.FromArgb(254, 242, 242);
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                        e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        break;
                    case "⚠ Stok Menipis":
                        e.CellStyle.BackColor = Color.FromArgb(255, 251, 235);
                        e.CellStyle.ForeColor = Color.FromArgb(245, 158, 11);
                        e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        break;
                    case "⚡ Stok Terbatas":
                        e.CellStyle.BackColor = Color.FromArgb(240, 253, 244);
                        e.CellStyle.ForeColor = Color.FromArgb(34, 197, 94);
                        break;
                    case "✅ Stok Tersedia":
                        e.CellStyle.BackColor = Color.FromArgb(240, 253, 244);
                        e.CellStyle.ForeColor = Color.FromArgb(34, 197, 94);
                        e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        break;
                }
            }
        }

        private void ShowGoodDetail(int rowIndex)
        {
            var goodId = Convert.ToInt32(_dataGridView.Rows[rowIndex].Cells["Id"].Value);
            var good = _currentGoods.FirstOrDefault(g => g.Id == goodId);

            if (good != null)
            {
                var detailForm = new GoodDetailForm(good);
                if (detailForm.ShowDialog() == DialogResult.OK)
                {
                    _ = LoadDataAsync();
                }
            }
        }

        private async void DeleteSelectedGood(int rowIndex)
        {
            var goodId = Convert.ToInt32(_dataGridView.Rows[rowIndex].Cells["Id"].Value);
            var goodName = _dataGridView.Rows[rowIndex].Cells["Name"].Value.ToString();

            var result = MessageBox.Show(
                $"Apakah Anda yakin ingin menghapus '{goodName}'?\n\nTindakan ini tidak dapat dibatalkan!",
                "⚠ Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SetStatus("🗑 Menghapus barang...");
                    this.Cursor = Cursors.WaitCursor;

                    await _goodService.DeleteGoodAsync(goodId);
                    await LoadDataAsync();

                    SetStatus($"✅ '{goodName}' berhasil dihapus");
                    MessageBox.Show($"Barang '{goodName}' berhasil dihapus!",
                        "✅ Berhasil Dihapus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menghapus barang:\n\n{ex.Message}",
                        "❌ Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SetStatus("❌ Gagal menghapus barang");
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void SetStatus(string message)
        {
            _statusLabel.Text = message;
            Application.DoEvents();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
                "Apakah Anda yakin ingin keluar dari aplikasi?",
                "🚪 Konfirmasi Keluar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }

            base.OnFormClosing(e);
        }
    }
}