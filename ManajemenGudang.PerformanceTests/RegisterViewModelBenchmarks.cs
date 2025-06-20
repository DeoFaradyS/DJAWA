using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ManajemenGudang.Data;
using ManajemenGudang.Models;
using ManajemenGudang.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ManajemenGudang.PerformanceTests
{
    [MemoryDiagnoser]
    public class RegisterViewModelBenchmarks
    {
        private AppDbContext _context = null!;
        private RegisterViewModel _viewModel = null!;
        private const string DbFileName = "performance_test.db";

        [Params(5, 10, 15)]
        public int ExistingUserCount;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Data Source={DbFileName}")
                .Options;
            _context = new AppDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            for (int i = 0; i < ExistingUserCount; i++)
            {
                _context.Users.Add(new User { Username = $"user{i}", Password = "password" });
            }
            _context.SaveChanges();

            _viewModel = new RegisterViewModel(_context);
        }

        [Benchmark]
        public void RegisterNewUser()
        {
            _viewModel.Username = Guid.NewGuid().ToString();
            _viewModel.PerformRegistration("secret_password");
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _context.Dispose();
            // Baris ini ditambahkan untuk mengatasi file lock
            Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
            File.Delete(DbFileName);
        }
    }
}