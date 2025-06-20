// File: ManajemenGudang.Tests/LoginViewModelTests.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManajemenGudang.ViewModels;
using ManajemenGudang.Data;
using Microsoft.EntityFrameworkCore;
using ManajemenGudang.Models;
using System;

namespace ManajemenGudang.Tests
{
    [TestClass]
    public class LoginViewModelTests
    {
        private AppDbContext _context = null!;
        private LoginViewModel _viewModel = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _viewModel = new LoginViewModel(_context);
        }

        [TestMethod]
        public void PerformLogin_WithAdminCredentials_ShouldReturnAdminSuccess()
        {
            // Arrange
            _viewModel.Username = "admin";

            // Act
            var result = _viewModel.PerformLogin("admin123");

            // Assert
            Assert.AreEqual(LoginResult.AdminSuccess, result);
        }

        [TestMethod]
        public void PerformLogin_WithValidUserCredentials_ShouldReturnSuccess()
        {
            // Arrange
            // Tambahkan user valid ke database in-memory
            _context.Users.Add(new User { Username = "user1", Password = "password1" });
            _context.SaveChanges();
            _viewModel.Username = "user1";

            // Act
            var result = _viewModel.PerformLogin("password1");

            // Assert
            Assert.AreEqual(LoginResult.Success, result);
        }

        [TestMethod]
        public void PerformLogin_WithInvalidPassword_ShouldReturnFailed()
        {
            // Arrange
            _context.Users.Add(new User { Username = "user1", Password = "password1" });
            _context.SaveChanges();
            _viewModel.Username = "user1";

            // Act
            var result = _viewModel.PerformLogin("password_salah"); // Password salah

            // Assert
            Assert.AreEqual(LoginResult.Failed, result);
        }

        [TestMethod]
        public void PerformLogin_WithNonExistentUser_ShouldReturnFailed()
        {
            // Arrange
            _viewModel.Username = "user_tidak_ada";

            // Act
            var result = _viewModel.PerformLogin("password_apapun");

            // Assert
            Assert.AreEqual(LoginResult.Failed, result);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}