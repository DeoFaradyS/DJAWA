using InventoryManagement.Controllers;
using System;

namespace InventoryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            BarangController controller = new BarangController();
            bool berjalan = true;

            while (berjalan)
            {
                Console.WriteLine("\n=== Menu Manajemen Barang ===");
                Console.WriteLine("1. Tambah Barang");
                Console.WriteLine("2. Lihat Barang");
                Console.WriteLine("3. Hapus Barang");
                Console.WriteLine("4. Cari Barang");
                Console.WriteLine("5. Ubah Barang");
                Console.WriteLine("0. Keluar");
                Console.Write("Pilih menu: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        controller.TambahBarang();
                        break;
                    case "2":
                        controller.LihatBarang();
                        break;
                    case "3":
                        controller.HapusBarang();
                        break;
                    case "4":
                        controller.CariBarang();
                        break;
                    case "5":
                        controller.UbahBarang();+0
                        break;
                    case "0":
                        berjalan = false;
                        break;
                    default:
                        Console.WriteLine("Menu tidak valid.");
                        break;
                }
            }
        }
    }
}
