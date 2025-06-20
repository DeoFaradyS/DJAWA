// File: ManajemenGudang.PerformanceTests/LoginViewModelBenchmarks.cs

using BenchmarkDotNet.Attributes;
using ManajemenGudang.Data;
using ManajemenGudang.Models;
using ManajemenGudang.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ManajemenGudang.PerformanceTests
{
    [MemoryDiagnoser]
    public class LoginViewModelBenchmarks
    {
        private AppDbContext _context = null!;
        private LoginViewModel _viewModel = null!;
        private const string DbFileName = "login_performance_test.db";
        private string _userToFind = "";

        // Kita akan menguji performa login pada database yang berisi 100, 1000, dan 10000 pengguna.
        [Params(5, 10, 15)]
        public int UserCount;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Data Source={DbFileName}")
                .Options;
            _context = new AppDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // Tentukan satu user yang pasti akan kita cari nanti
            _userToFind = $"user{UserCount / 2}";

            // Isi database dengan pengguna dummy
            for (int i = 0; i < UserCount; i++)
            {
                _context.Users.Add(new User { Username = $"user{i}", Password = "password" });
            }
            _context.SaveChanges();

            _viewModel = new LoginViewModel(_context);
        }

        /// <summary>
        /// Mengukur kecepatan login sebagai Admin.
        /// Seharusnya sangat cepat dan tidak terpengaruh oleh UserCount.
        /// </summary>
        [Benchmark(Description = "Admin Login")]
        public void LoginAsAdmin()
        {
            _viewModel.Username = "admin";
            _viewModel.PerformLogin("admin123");
        }

        /// <summary>
        /// Mengukur kecepatan login sebagai pengguna yang valid dan ada di database.
        /// Performanya akan dipengaruhi oleh UserCount.
        /// </summary>
        [Benchmark(Description = "Valid User Login")]
        public void LoginAsExistingUser()
        {
            _viewModel.Username = _userToFind; // Menggunakan user yang sudah kita siapkan
            _viewModel.PerformLogin("password");
        }

        /// <summary>
        /// Mengukur kecepatan upaya login yang gagal karena user tidak ada.
        /// Performanya juga akan dipengaruhi oleh UserCount.
        /// </summary>
        [Benchmark(Description = "Non-Existent User Login")]
        public void LoginAsNonExistentUser()
        {
            _viewModel.Username = "user_tidak_akan_pernah_ada";
            _viewModel.PerformLogin("password");
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _context.Dispose();
            Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
            File.Delete(DbFileName);
        }
    }
}