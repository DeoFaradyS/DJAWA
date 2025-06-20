// File: ManajemenGudang.PerformanceTests/AdminDashboardViewModelBenchmarks.cs

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using ManajemenGudang.Data;
using ManajemenGudang.Models;
using ManajemenGudang.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ManajemenGudang.PerformanceTests
{
    [SimpleJob(RuntimeMoniker.Net80)]
    [MemoryDiagnoser]
    public class AdminDashboardViewModelBenchmarks
    {
        private AppDbContext _context = null!;
        private User _userToSelect = null!;
        private Barang _barangToUpdate = null!;
        private const string DbFileName = "admin_performance_test.db";

        [Params(10, 100)]
        public int UserCount;

        private const int BarangPerUserCount = 50;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Data Source={DbFileName}")
                .Options;

            _context = new AppDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            for (int i = 0; i < UserCount; i++)
            {
                var user = new User { Username = $"user{i}" };
                _context.Users.Add(user);
                _context.SaveChanges(); // Simpan agar user.Id tersedia

                if (i == UserCount / 2)
                    _userToSelect = user;

                for (int j = 0; j < BarangPerUserCount; j++)
                {
                    var barang = new Barang
                    {
                        NamaBarang = $"Barang-{i}-{j}",
                        UserId = user.Id,
                        Status = "Belum Disetujui"
                    };

                    _context.Barangs.Add(barang);

                    if (i == UserCount / 2 && j == 0)
                        _barangToUpdate = barang;
                }

                _context.SaveChanges();
            }
        }

        [Benchmark(Description = "Load All Users (Constructor)")]
        public void LoadAllUsers_Constructor()
        {
            var viewModel = new AdminDashboardViewModel(_context);
        }

        [Benchmark(Description = "Select User to Load Items")]
        public void LoadSelectedUserItems()
        {
            var viewModel = new AdminDashboardViewModel(_context)
            {
                SelectedUser = _userToSelect
            };
        }

        [Benchmark(Description = "Update Item Status")]
        public void UpdateItemStatus()
        {
            var viewModel = new AdminDashboardViewModel(_context)
            {
                SelectedUser = _userToSelect,
                SelectedBarang = _barangToUpdate,
                SelectedStatus = "Barang Tersimpan"
            };

            // Eksekusi perintah update status
            if (viewModel.UpdateStatusCommand.CanExecute(null))
            {
                // NONAKTIFKAN MessageBox.Show dengan mengecek apakah sedang di benchmark (bisa juga disesuaikan di ViewModel)
                viewModel.UpdateStatusCommand.Execute(null);
            }
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
