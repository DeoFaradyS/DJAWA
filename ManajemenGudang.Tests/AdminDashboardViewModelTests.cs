// File: ManajemenGudang.Tests/AdminDashboardViewModelTests.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManajemenGudang.ViewModels;
using ManajemenGudang.Data;
using Microsoft.EntityFrameworkCore;
using ManajemenGudang.Models;
using System;
using System.Linq;

namespace ManajemenGudang.Tests
{
    [TestClass]
    public class AdminDashboardViewModelTests
    {
        private AppDbContext _context = null!;
        private AdminDashboardViewModel _viewModel = null!;
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
            _context.Users.AddRange(_user1, _user2);

            _context.Barangs.AddRange(
                new Barang { Id = 101, NamaBarang = "Laptop User 1", UserId = _user1.Id, Status = "Belum Disetujui" },
                new Barang { Id = 102, NamaBarang = "Mouse User 1", UserId = _user1.Id, Status = "Belum Disetujui" },
                new Barang { Id = 201, NamaBarang = "Keyboard User 2", UserId = _user2.Id, Status = "Belum Disetujui" }
            );

            _context.SaveChanges();

            // Inisialisasi ViewModel
            _viewModel = new AdminDashboardViewModel(_context);
        }

        [TestMethod]
        public void Constructor_WhenInitialized_ShouldLoadAllUsers()
        {
            // Arrange & Act
            // Aksi terjadi saat _viewModel dibuat di Setup()

            // Assert
            // Pastikan semua user (2) berhasil dimuat
            Assert.AreEqual(2, _viewModel.AllUsers.Count);
            // Pastikan daftar barang awalnya kosong sampai ada user yang dipilih
            Assert.AreEqual(0, _viewModel.SelectedUserItems.Count);
        }

        [TestMethod]
        public void SelectedUser_WhenSet_ShouldLoadCorrectItemsForThatUser()
        {
            // Arrange
            var userToSelect = _viewModel.AllUsers.First(u => u.Id == _user2.Id);

            // Act
            // "Pilih" user2 di ViewModel. Ini akan memicu `LoadItemsForSelectedUser()`
            _viewModel.SelectedUser = userToSelect;

            // Assert
            // 1. Pastikan hanya ada 1 barang (milik user2) yang dimuat
            Assert.AreEqual(1, _viewModel.SelectedUserItems.Count);
            // 2. Pastikan barang yang dimuat adalah "Keyboard User 2"
            Assert.AreEqual("Keyboard User 2", _viewModel.SelectedUserItems.First().NamaBarang);
        }

        [TestMethod]
        public void UpdateStatusCommand_WhenExecuted_ShouldChangeItemStatusInDatabase()
        {
            // Arrange
            // 1. Pilih user1
            _viewModel.SelectedUser = _viewModel.AllUsers.First(u => u.Id == _user1.Id);

            // 2. Pilih barang pertama milik user1 ("Laptop User 1")
            _viewModel.SelectedBarang = _viewModel.SelectedUserItems.First(b => b.Id == 101);

            // 3. Pilih status baru yang akan diterapkan
            _viewModel.SelectedStatus = "Barang Tersimpan";

            var itemIdToUpdate = _viewModel.SelectedBarang.Id;

            // Act
            _viewModel.UpdateStatusCommand.Execute(null);

            // Assert
            // Ambil kembali data barang langsung dari database untuk verifikasi
            var updatedItemInDb = _context.Barangs.Find(itemIdToUpdate);

            Assert.IsNotNull(updatedItemInDb);
            // Pastikan statusnya telah berubah di database
            Assert.AreEqual("Barang Tersimpan", updatedItemInDb.Status);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}