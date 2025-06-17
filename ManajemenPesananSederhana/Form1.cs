using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ManajemenPesananSederhana
{
    public partial class Form1 : Form
    {
        private List<Order> pesanan;

        public Form1()
        {
            InitializeComponent();
            InisialisasiPesanan();
            InisialisasiComboBox();

            dataGridView1.CellFormatting += dataGridView1_CellFormatting;

            this.Text = "Aplikasi Manajemen Pesanan";
            this.BackColor = Color.WhiteSmoke;
        }

        private void InisialisasiPesanan()
        {
            pesanan = new List<Order>
            {
                new Order { OrderId = 1, NamaPelanggan = "Andi", Status = "Diproses", TanggalPesan = DateTime.Now.AddDays(-2), Stok = 5 , Harga = 150000 },
                new Order { OrderId = 2, NamaPelanggan = "Budi", Status = "Dikirim", TanggalPesan = DateTime.Now.AddDays(-3), Stok = 3, Harga = 200000 },
                new Order { OrderId = 3, NamaPelanggan = "Citra", Status = "Selesai", TanggalPesan = DateTime.Now.AddDays(-5), Stok = 0, Harga = 250000 }
            };
        }

        private void InisialisasiComboBox()
        {
            comboFilter.Items.Clear();
            comboFilter.Items.Add("Status Pesanan");
            comboFilter.Items.Add("Riwayat Pesanan");
            comboFilter.SelectedIndex = 0;
        }

        private void btnTampilkan_Click(object sender, EventArgs e)
        {
            if (comboFilter.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih filter.");
                return;
            }

            string pilihan = comboFilter.SelectedItem.ToString().Trim();
            OrderViewer viewer = null;

            if (pilihan == "Status Pesanan")
                viewer = new StatusViewer(pesanan);
            else if (pilihan == "Riwayat Pesanan")
                viewer = new RiwayatViewer(pesanan);
            else
            {
                MessageBox.Show("Pilihan tidak dikenali: " + pilihan);
                return;
            }

            var hasil = viewer.Tampilkan();

            if (hasil == null || hasil.Count == 0)
            {
                MessageBox.Show("Tidak ada data yang cocok.");
            }

            dataGridView1.Columns.Clear(); 
            dataGridView1.DataSource = hasil;

            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.MistyRose;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.LightGray;
        }

        private void comboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Harga" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal nilai))
                {
                    e.Value = string.Format("Rp {0:N0}", nilai);
                    e.FormattingApplied = true;
                }
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "TanggalPesan" && e.Value is DateTime)
            {
                e.Value = ((DateTime)e.Value).ToString("dd MMM yyyy");
                e.FormattingApplied = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void label1_Click(object sender, EventArgs e)
        {
        
        }

        private void lblJudul_Click(object sender, EventArgs e)
        {
        
        }
    }
}
