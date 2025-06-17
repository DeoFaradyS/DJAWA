using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("|--Manajemen Pesanan User--|");
            Console.WriteLine("|1. Lihat Status Pesanan   |");
            Console.WriteLine("|2. Riwayat Pesanan        |");
            Console.WriteLine("|0. Kembali                |");
            Console.WriteLine("|__________________________|");
            Console.Write("Pilih Salah Satu Nomer diatas : ");
            var pilihan = Console.ReadLine();

            Debug.Assert(pilihan != null, "Pilihan tidak ada");

            if (pilihan == "0")
            {
                Console.WriteLine("Keluar dari program.");
                break;
            }
            else if (pilihan == "1")
            {
                
                OrderService.LihatStatusPesanan();

            }
            else if (pilihan == "2")
            {
                Console.WriteLine("[DEBUG] Menjalankan LihatRiwayatPesanan()");
                OrderService.LihatRiwayatPesanan();
                Debug.Assert(OrderService.orders.Count >= 1, "Riwayat pesanan seharusnya tidak kosong.");
            }
            else
            {
                Console.WriteLine("Pilihan Tidak Ada, tekan Enter untuk ulangi.");
                Console.ReadLine();
            }
            Debug.Assert(OrderService.orders != null, "List pesanan (orders) tidak boleh null.");
        }
        Console.WriteLine("Program selesai dijalankan.");
    }
}
