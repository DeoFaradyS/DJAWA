// File: ManajemenGudang.PerformanceTests/RiwayatBarangViewModelBenchmarks.cs

using BenchmarkDotNet.Attributes;
using ManajemenGudang.Data;
using ManajemenGudang.Models;
using ManajemenGudang.Services;
using ManajemenGudang.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace ManajemenGudang.PerformanceTests
{
    [MemoryDiagnoser]
    public class RiwayatBarangViewModelBenchmarks
    {
        private AppDbContext _context = null!;
        private const string DbFileName = "riwayat_performance_test.db";
        private User _userToLogin = null!;
        private string _barangToSearch = "";

        // Kita akan menguji performa pada database di mana setiap pengguna memiliki 100 atau 1000 barang.
        [Params(10, 30)]
        public int BarangPerUserCount;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Data Source={DbFileName}")
                .Options;
            _context = new AppDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // Buat 10 pengguna dummy
            for (int i = 0; i < 10; i++)
            {
                var user = new User { Username = $"user{i}" };
                _context.Users.Add(user);
                _context.SaveChanges(); // SIMPAN agar user.Id terisi

                // User pertama akan kita gunakan untuk login
                if (i == 0) _userToLogin = user;

                // Beri setiap pengguna sejumlah barang dummy
                for (int j = 0; j < BarangPerUserCount; j++)
                {
                    var barang = new Barang { NamaBarang = $"Barang-{i}-{j}", UserId = user.Id };
                    _context.Barangs.Add(barang);

                    // Siapkan nama barang yang akan dicari (milik user pertama)
                    if (i == 0 && j == BarangPerUserCount / 2)
                    {
                        _barangToSearch = barang.NamaBarang;
                    }
                }

                _context.SaveChanges(); // SIMPAN barang milik user ini
            }

            // Login user pertama
            AuthenticationService.GetInstance().Login(_userToLogin);
        }


        /// <summary>
        /// Mengukur kecepatan memuat daftar riwayat barang saat ViewModel pertama kali dibuat.
        /// Ini mengukur performa dari constructor yang memanggil LoadUserItems().
        /// </summary>
        [Benchmark(Description = "Initial History Load")]
        public void InitialLoad()
        {
            // Karena constructor-lah yang melakukan pekerjaan, kita buat instance baru di sini.
            var viewModel = new RiwayatBarangViewModel(_context);
        }

        /// <summary>
        /// Mengukur kecepatan mencari barang spesifik dari daftar riwayat.
        /// </summary>
        [Benchmark(Description = "Search For Item")]
        public void SearchForExistingBarang()
        {
            // Buat instance baru untuk memastikan kondisi awal selalu sama (semua barang ter-load)
            var viewModel = new RiwayatBarangViewModel(_context);
            viewModel.SearchText = _barangToSearch; // Gunakan nama barang yang sudah kita siapkan
            viewModel.CariBarangCommand?.Execute(null);
        }


        [GlobalCleanup]
        public void GlobalCleanup()
        {
            AuthenticationService.GetInstance().Logout();
            _context.Dispose();
            Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
            File.Delete(DbFileName);
        }
    }
}