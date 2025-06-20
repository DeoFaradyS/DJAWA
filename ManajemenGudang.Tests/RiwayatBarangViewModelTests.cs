// File: ManajemenGudang.Tests/RiwayatBarangViewModelTests.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManajemenGudang.ViewModels;
using ManajemenGudang.Data;
using Microsoft.EntityFrameworkCore;
using ManajemenGudang.Models;
using System;
using ManajemenGudang.Services;
using System.Linq;

namespace ManajemenGudang.Tests
{
    [TestClass]
    public class RiwayatBarangViewModelTests
    {
        private AppDbContext _context = null!;
        private RiwayatBarangViewModel _viewModel = null!;
        private User _user1 = null!;
        private User _user2 = null!;

        [TestInitialize]
        public void Setup()
        {
            // Konfigurasi database in-memory
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);

            // Buat data palsu untuk pengujian
            _user1 = new User { Id = 1, Username = "user1" };
            _user2 = new User { Id = 2, Username = "user2" };

            // Buat beberapa barang untuk setiap user
            _context.Barangs.AddRange(
                new Barang { Id = 101, NamaBarang = "Laptop User 1", UserId = _user1.Id },
                new Barang { Id = 102, NamaBarang = "Mouse User 1", UserId = _user1.Id },
                new Barang { Id = 201, NamaBarang = "Keyboard User 2", UserId = _user2.Id }
            );

            _context.SaveChanges();

            // "Login" kan user1 sebelum membuat ViewModel
            AuthenticationService.GetInstance().Login(_user1);

            // Inisialisasi ViewModel SETELAH login
            // Constructor akan secara otomatis memanggil LoadUserItems()
            _viewModel = new RiwayatBarangViewModel(_context);
        }

        [TestMethod]
        public void Constructor_WhenInitialized_ShouldLoadOnlyLoggedInUserItems()
        {
            // Arrange
            // Setup sudah dilakukan di [TestInitialize]

            // Act
            // Aksi terjadi saat _viewModel dibuat di Setup()

            // Assert
            // 1. Pastikan hanya ada 2 barang yang dimuat (milik user1)
            Assert.AreEqual(2, _viewModel.DaftarBarang.Count);

            // 2. Pastikan semua barang yang dimuat benar-benar milik user1
            bool allItemsBelongToUser1 = _viewModel.DaftarBarang.All(b => b.UserId == _user1.Id);
            Assert.IsTrue(allItemsBelongToUser1);
        }

        [TestMethod]
        public void CariBarangCommand_WhenSearchTextMatches_ShouldFilterListCorrectly()
        {
            // Arrange
            _viewModel.SearchText = "Laptop"; // Cari kata "Laptop"

            // Act
            _viewModel.CariBarangCommand?.Execute(null);

            // Assert
            // 1. Hasilnya harus 1 barang
            Assert.AreEqual(1, _viewModel.DaftarBarang.Count);
            // 2. Pastikan barang yang ditemukan adalah "Laptop User 1"
            Assert.AreEqual("Laptop User 1", _viewModel.DaftarBarang.First().NamaBarang);
        }

        [TestMethod]
        public void CariBarangCommand_WhenSearchTextDoesNotMatch_ShouldReturnEmptyList()
        {
            // Arrange
            _viewModel.SearchText = "BarangTidakAda";

            // Act
            _viewModel.CariBarangCommand?.Execute(null);

            // Assert
            Assert.AreEqual(0, _viewModel.DaftarBarang.Count); // Hasilnya harus kosong
        }

        [TestCleanup]
        public void Cleanup()
        {
            AuthenticationService.GetInstance().Logout();
            _context.Dispose();
        }
    }
}