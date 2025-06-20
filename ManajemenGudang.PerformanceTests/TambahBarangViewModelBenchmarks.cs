// File: ManajemenGudang.PerformanceTests/TambahBarangViewModelBenchmarks.cs

using BenchmarkDotNet.Attributes;
using ManajemenGudang.Data;
using ManajemenGudang.Models;
using ManajemenGudang.Services;
using ManajemenGudang.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ManajemenGudang.PerformanceTests
{
    [MemoryDiagnoser]
    public class TambahBarangViewModelBenchmarks
    {
        private AppDbContext _context = null!;
        private TambahBarangViewModel _viewModel = null!;
        private const string DbFileName = "tambahbarang_performance_test.db";

        // Kita akan menguji performa penambahan barang pada tabel yang sudah berisi 0, 100, dan 1000 barang.
        [Params(5, 10, 15)]
        public int ExistingBarangCount;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Data Source={DbFileName}")
                .Options;
            _context = new AppDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // Buat satu user untuk menjadi "pemilik" barang
            var user = new User { Id = 1, Username = "testuser", Password = "password" };
            _context.Users.Add(user);

            // Jika parameter > 0, isi tabel Barangs dengan data dummy
            if (ExistingBarangCount > 0)
            {
                for (int i = 0; i < ExistingBarangCount; i++)
                {
                    _context.Barangs.Add(new Barang
                    {
                        NamaBarang = $"Barang Lama {i}",
                        UserId = user.Id
                    });
                }
            }
            _context.SaveChanges();

            // PENTING: "Login" kan user tersebut agar 'AuthenticationService' memiliki CurrentUser
            AuthenticationService.GetInstance().Login(user);

            _viewModel = new TambahBarangViewModel(_context);
        }

        /// <summary>
        /// Mengukur kecepatan proses penambahan satu barang baru ke database.
        /// </summary>
        [Benchmark]
        public void AddNewBarang()
        {
            // Siapkan data untuk barang baru yang akan ditambahkan
            _viewModel.NamaBarangInput = "Barang Baru";
            _viewModel.DeskripsiInput = "Ini adalah deskripsi barang baru.";
            _viewModel.JumlahInput = 50;

            _viewModel.PerformTambahBarang();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            // Logout user untuk membersihkan state
            AuthenticationService.GetInstance().Logout();
            _context.Dispose();
            Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
            File.Delete(DbFileName);
        }
    }
}