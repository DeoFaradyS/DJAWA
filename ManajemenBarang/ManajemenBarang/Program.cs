using System;
using System.Collections.Generic;

namespace InventoryManagement
{
    class Program
    {
        enum State
        {
            Menu,
            Tambah,
            Lihat,
            Ubah,
            Hapus,
            Cari,
            Keluar
        }

        enum Aksi
        {
            Tambah,
            Lihat,
            Ubah,
            Hapus,
            Cari,
            Keluar,
            TidakValid
        }

        static Dictionary<(State, Aksi), 
            State> tabelTransisi = new Dictionary<
                (State, Aksi), State>
        {
            { (State.Menu, Aksi.Tambah), State.Tambah },
            { (State.Menu, Aksi.Lihat), State.Lihat },
            { (State.Menu, Aksi.Ubah), State.Ubah },
            { (State.Menu, Aksi.Hapus), State.Hapus },
            { (State.Menu, Aksi.Cari), State.Cari },
            { (State.Menu, Aksi.Keluar), State.Keluar }
        };

        static void Main(string[] args)
        {
            State stateSaatIni = State.Menu;
            ManajemenBarang manajemen = new ManajemenBarang();

            while (stateSaatIni != State.Keluar)
            {
                if (stateSaatIni == State.Menu)
                {
                    TampilkanMenu();
                    string input = Console.ReadLine();
                    Aksi aksi = ParseInput(input);
                    stateSaatIni = tabelTransisi.ContainsKey((stateSaatIni, aksi)) 
                        ? tabelTransisi[(stateSaatIni, aksi)] : State.Menu;
                }
                else
                {
                    switch (stateSaatIni)
                    {
                        case State.Tambah:
                            manajemen.TambahBarang();
                            stateSaatIni = State.Menu;
                            break;
                        case State.Lihat:
                            manajemen.LihatBarang();
                            stateSaatIni = State.Menu;
                            break;
                        case State.Ubah:
                            manajemen.UbahBarang();
                            stateSaatIni = State.Menu;
                            break;
                        case State.Hapus:
                            manajemen.HapusBarang();
                            stateSaatIni = State.Menu;
                            break;
                        case State.Cari:
                            manajemen.CariBarang();
                            stateSaatIni = State.Menu;
                            break;
                    }
                }
            }

            Console.WriteLine("Keluar dari aplikasi...");
        }

        static void TampilkanMenu()
        {
            Console.WriteLine("\n=== Sistem Manajemen Barang ===");
            Console.WriteLine("1. Tambah Barang");
            Console.WriteLine("2. Lihat Barang");
            Console.WriteLine("3. Ubah Barang");
            Console.WriteLine("4. Hapus Barang");
            Console.WriteLine("5. Cari Barang");
            Console.WriteLine("6. Keluar");
            Console.Write("Pilih menu: ");
        }

        static Aksi ParseInput(string input)
        {
            return input switch
            {
                "1" => Aksi.Tambah,
                "2" => Aksi.Lihat,
                "3" => Aksi.Ubah,
                "4" => Aksi.Hapus,
                "5" => Aksi.Cari,
                "6" => Aksi.Keluar,
                _ => Aksi.TidakValid
            };
        }
    }
}