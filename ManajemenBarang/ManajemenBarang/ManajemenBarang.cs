using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement
{
    public class ManajemenBarang
    {
        private List<Barang> daftarBarang = new List<Barang>();

        public void TambahBarang()
        {
            Console.WriteLine("\n=== Tambah Barang Baru ===");
            Console.Write("Nama: ");
            string nama = Console.ReadLine();
            Console.Write("Deskripsi: ");
            string deskripsi = Console.ReadLine();
            Console.Write("Jumlah: ");
            int jumlah = int.Parse(Console.ReadLine());
            Console.Write("Harga: ");
            decimal harga = decimal.Parse(Console.ReadLine());

            daftarBarang.Add(new Barang { 
                Nama = nama, 
                Deskripsi = deskripsi, 
                Jumlah = jumlah,
                Harga = harga });
            Console.WriteLine("Barang berhasil ditambahkan.");
        }

        public void LihatBarang()
        {
            Console.WriteLine("\n=== Daftar Barang ===");
            if (daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada barang di daftar.");
                return;
            }

            foreach (var barang in daftarBarang)
            {
                Console.WriteLine(
                    $"Nama: {barang.Nama}, " +
                    $"Deskripsi: {barang.Deskripsi}, " +
                    $"Jumlah: {barang.Jumlah}, Harga: {barang.Harga:C}");
            }
        }

        public void UbahBarang()
        {
            Console.WriteLine("\n=== Ubah Informasi Barang ===");
            Console.Write("Masukkan nama barang yang ingin diubah: ");
            string nama = Console.ReadLine();

            var barang = daftarBarang.FirstOrDefault
                (b => b.Nama.Equals(nama, StringComparison.OrdinalIgnoreCase));
            if (barang != null)
            {
                Console.Write("Deskripsi Baru: ");
                barang.Deskripsi = Console.ReadLine();
                Console.Write("Jumlah Baru: ");
                barang.Jumlah = int.Parse(Console.ReadLine());
                Console.Write("Harga Baru: ");
                barang.Harga = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Barang berhasil diperbarui.");
            }
            else
            {
                Console.WriteLine("Barang tidak ditemukan.");
            }
        }

        public void HapusBarang()
        {
            Console.WriteLine("\n=== Hapus Barang ===");
            Console.Write("Masukkan nama barang yang ingin dihapus: ");
            string nama = Console.ReadLine();

            var barang = daftarBarang.FirstOrDefault
                (b => b.Nama.Equals(nama, StringComparison.OrdinalIgnoreCase));
            if (barang != null)
            {
                daftarBarang.Remove(barang);
                Console.WriteLine("Barang berhasil dihapus.");
            }
            else
            {
                Console.WriteLine("Barang tidak ditemukan.");
            }
        }

        public void CariBarang()
        {
            Console.WriteLine("\n=== Cari Barang ===");
            Console.Write("Masukkan nama barang yang ingin dicari: ");
            string nama = Console.ReadLine();

            var hasil = daftarBarang.Where(
                b => b.Nama.IndexOf(nama, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            if (hasil.Count == 0)
            {
                Console.WriteLine("Barang tidak ditemukan.");
            }
            else
            {
                foreach (var barang in hasil)
                {
                    Console.WriteLine(
                        $"Nama: {barang.Nama}, " +
                        $"Deskripsi: {barang.Deskripsi}, " +
                        $"Jumlah: {barang.Jumlah}, " +
                        $"Harga: {barang.Harga:C}");
                }
            }
        }
    }
}
