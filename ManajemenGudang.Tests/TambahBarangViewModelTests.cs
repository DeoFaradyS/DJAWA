// File: ManajemenGudang.Tests/TambahBarangViewModelTests.cs

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
    public class TambahBarangViewModelTests
    {
        private AppDbContext _context = null!;
        private TambahBarangViewModel _viewModel = null!;
        private User _dummyUser = null!;

        [TestInitialize]
        public void Setup()
        {
            // Konfigurasi database in-memory
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);

            // PENTING: "Login" kan user palsu sebelum setiap tes
            // Ini untuk memenuhi ketergantungan pada AuthenticationService.GetInstance().CurrentUser
            _dummyUser = new User { Id = 99, Username = "testuser" };
            AuthenticationService.GetInstance().Login(_dummyUser);

            // Buat ViewModel setelah user "login"
            _viewModel = new TambahBarangViewModel(_context);
        }

        [TestMethod]
        public void PerformTambahBarang_WithValidData_ShouldAddBarangAndReturnTrue()
        {
            // Arrange
            _viewModel.NamaBarangInput = "Laptop Gaming";
            _viewModel.DeskripsiInput = "Laptop dengan spek tinggi";
            _viewModel.JumlahInput = 15;

            // Act
            bool result = _viewModel.PerformTambahBarang();

            // Assert
            // 1. Pastikan metode mengembalikan true
            Assert.IsTrue(result);

            // 2. Pastikan satu barang berhasil ditambahkan ke database
            Assert.AreEqual(1, _context.Barangs.Count());

            // 3. Verifikasi detail barang yang ditambahkan sudah benar
            var addedBarang = _context.Barangs.First();
            Assert.AreEqual("Laptop Gaming", addedBarang.NamaBarang);
            Assert.AreEqual(15, addedBarang.Jumlah);
            Assert.AreEqual("Belum Disetujui", addedBarang.Status);

            // 4. Pastikan barang terhubung dengan user yang benar
            Assert.AreEqual(_dummyUser.Id, addedBarang.UserId);
        }

        [TestMethod]
        public void PerformTambahBarang_WithInvalidData_ShouldNotAddBarangAndReturnFalse()
        {
            // Arrange
            _viewModel.NamaBarangInput = ""; // Nama barang kosong (tidak valid)
            _viewModel.DeskripsiInput = "Deskripsi apapun";
            _viewModel.JumlahInput = 10;

            // Act
            bool result = _viewModel.PerformTambahBarang();

            // Assert
            // 1. Pastikan metode mengembalikan false
            Assert.IsFalse(result);

            // 2. Pastikan tidak ada barang yang ditambahkan ke database
            Assert.AreEqual(0, _context.Barangs.Count());
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Logout user untuk membersihkan state singleton
            AuthenticationService.GetInstance().Logout();
            _context.Dispose();
        }
    }
}