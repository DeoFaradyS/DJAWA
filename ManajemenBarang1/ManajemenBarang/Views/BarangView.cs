using System.Diagnostics;

namespace InventoryManagement.Views
{
    public class BarangView
    {
        public virtual  Barang InputBarang()
        {
            Console.WriteLine("\n=== Tambah Barang ===");
            Console.Write("Nama: ");
            string nama = Console.ReadLine();
            Console.Write("Deskripsi: ");
            string deskripsi = Console.ReadLine();
            Console.Write("Jumlah: ");
            int jumlah = int.Parse(Console.ReadLine());
            Console.Write("Harga: ");
            decimal harga = decimal.Parse(Console.ReadLine());

            //  Precondition
            if (string.IsNullOrWhiteSpace(nama)) throw new ArgumentException("Nama tidak boleh kosong");
            if (jumlah < 0) throw new ArgumentException("Jumlah tidak boleh negatif");
            if (harga < 0) throw new ArgumentException("Harga tidak boleh negatif");

            var barang = new Barang
            {
                Nama = nama,
                Deskripsi = deskripsi,
                Jumlah = jumlah,
                Harga = harga
            };

            // Postcondition
            Debug.Assert(barang != null);
            Debug.Assert(barang.Nama == nama);
            Debug.Assert(barang.Jumlah >= 0);
            Debug.Assert(barang.Harga >= 0);

            return barang;
        }

        public virtual void TampilkanDaftarBarang(List<Barang> barangList)
        {
            // Precondition
            if (barangList == null) throw new ArgumentNullException(nameof(barangList));

            Console.WriteLine("\n=== Daftar Barang ===");
            if (barangList.Count == 0)
            {
                Console.WriteLine("Belum ada barang yang ditambahkan.");
                return;
            }

            foreach (var barang in barangList)
            {
                Console.WriteLine(
                    $"Nama: {barang.Nama}, " +
                    $"Deskripsi: {barang.Deskripsi}," +
                    $" Jumlah: {barang.Jumlah}, " +
                    $"Harga: {barang.Harga:C}");
            }

            // Postcondition
            Debug.Assert(barangList.Count >= 1);
        }

        public virtual string InputNamaBarang(string aksi)
        {
            Console.Write($"\nMasukkan nama barang yang ingin {aksi}: ");
            string nama = Console.ReadLine();

            // Precondition
            if (string.IsNullOrWhiteSpace(nama)) 
                throw new ArgumentException("Nama barang tidak boleh kosong");

            return nama;
        }

        public virtual void TampilkanPesan(string pesan)
        {
            // Precondition
            if (string.IsNullOrWhiteSpace(pesan)) throw new ArgumentException("Pesan tidak boleh kosong");

            Console.WriteLine($"\n{pesan}");
        }

        public virtual void TampilkanBarang(Barang barang)
        {
            // Precondition
            if (barang == null) throw new ArgumentNullException(nameof(barang));

            Console.WriteLine("\n=== Detail Barang Ditemukan ===");
            Console.WriteLine($"Nama: {barang.Nama}");
            Console.WriteLine($"Deskripsi: {barang.Deskripsi}");
            Console.WriteLine($"Jumlah: {barang.Jumlah}");
            Console.WriteLine($"Harga: {barang.Harga:C}");
        }

        public virtual Barang InputBarangUpdate(Barang barangLama)
        {
            // Precondition
            if (barangLama == null) throw new ArgumentNullException(nameof(barangLama));

            Console.WriteLine("\n=== Ubah Data Barang ===");
            Console.WriteLine(
                $"Data lama ->" +
                $" Nama: {barangLama.Nama}, " +
                $"Deskripsi: {barangLama.Deskripsi}, " +
                $"Jumlah: {barangLama.Jumlah}," +
                $" Harga: {barangLama.Harga:C}");

            Console.Write("Nama baru: ");
            string nama = Console.ReadLine();
            Console.Write("Deskripsi baru: ");
            string deskripsi = Console.ReadLine();
            Console.Write("Jumlah baru: ");
            int jumlah = int.Parse(Console.ReadLine());
            Console.Write("Harga baru: ");
            decimal harga = decimal.Parse(Console.ReadLine());

            // Precondition
            if (string.IsNullOrWhiteSpace(nama)) throw new ArgumentException("Nama tidak boleh kosong");
            if (jumlah < 0) throw new ArgumentException("Jumlah tidak boleh negatif");
            if (harga < 0) throw new ArgumentException("Harga tidak boleh negatif");

            var barangBaru = new Barang
            {
                Nama = nama,
                Deskripsi = deskripsi,
                Jumlah = jumlah,
                Harga = harga
            };

            // Postcondition
            Debug.Assert(barangBaru != null);
            Debug.Assert(barangBaru.Nama == nama);

            return barangBaru;
        }
    }
}
