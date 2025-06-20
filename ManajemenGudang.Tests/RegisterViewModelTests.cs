// File: ManajemenGudang.Tests/RegisterViewModelTests.cs
// (MODIFIED TO TEST LOGIC DIRECTLY)

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManajemenGudang.ViewModels;
using ManajemenGudang.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ManajemenGudang.Models;
using System;

namespace ManajemenGudang.Tests
{
    [TestClass]
    public class RegisterViewModelTests
    {
        private AppDbContext _context = null!;
        private RegisterViewModel _viewModel = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _viewModel = new RegisterViewModel(_context);
        }

        [TestMethod]
        public void Register_WithValidAndNewUsername_ShouldSucceedAndReturnTrue()
        {
            // Arrange
            _viewModel.Username = "userbaru";

            // Act
            // Langsung panggil metode logika, tidak perlu membuat PasswordBox
            bool result = _viewModel.PerformRegistration("password123");

            // Assert
            Assert.IsTrue(result); // Verifikasi metode mengembalikan true
            Assert.AreEqual(1, _context.Users.Count());
            Assert.AreEqual("userbaru", _context.Users.First().Username);
        }

        [TestMethod]
        public void Register_WithExistingUsername_ShouldFailAndReturnFalse()
        {
            // Arrange
            _context.Users.Add(new User { Username = "sudahada", Password = "passwordlama" });
            _context.SaveChanges();
            _viewModel.Username = "sudahada";

            // Act
            bool result = _viewModel.PerformRegistration("passwordbaru");

            // Assert
            Assert.IsFalse(result); // Verifikasi metode mengembalikan false
            Assert.AreEqual(1, _context.Users.Count()); // Pastikan tidak ada user baru ditambahkan
        }

        [TestMethod]
        public void Register_WithEmptyUsername_ShouldFailAndReturnFalse()
        {
            // Arrange
            _viewModel.Username = "";

            // Act
            bool result = _viewModel.PerformRegistration("password123");

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0, _context.Users.Count());
        }

        [TestMethod]
        public void Register_WithEmptyPassword_ShouldFailAndReturnFalse()
        {
            // Arrange
            _viewModel.Username = "userbaru";

            // Act
            bool result = _viewModel.PerformRegistration(""); // Kirim password kosong

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0, _context.Users.Count());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}