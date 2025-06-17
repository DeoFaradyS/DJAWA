using ManajemenUser_Deo.Controllers;
using ManajemenUser_Deo.UserManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UserManagementTest
{
    [TestClass]
    public class AuthControllerTests
    {
        private string _tempFilePath;
        private UserController _userController;
        private AuthController _authController;

        [TestInitialize]
        public void Setup()
        {
            // 1. Buat file JSON sementara dengan nama unik
            _tempFilePath = Path.Combine(
                Path.GetTempPath(),
                $"test_users_{Guid.NewGuid()}.json"
            );

            // 2. Salin isi file JSON asli (test_users.json) ke file sementara
            File.Copy("test_users.json", _tempFilePath, overwrite: true);

            // 3. Buat UserController dengan path file sementara
            _userController = new UserController(_tempFilePath);

            // 4. Inject UserController ke AuthController
            _authController = new AuthController(_userController);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Hapus file JSON sementara setelah test selesai
            if (File.Exists(_tempFilePath))
            {
                File.Delete(_tempFilePath);
            }
        }

        /// <summary>
        /// Test register user baru dengan data valid.
        /// Harus berhasil dan mengembalikan true.
        /// </summary>
        [TestMethod]
        public void Register_ShouldReturnTrue_WhenEmailNotExists()
        {
            string name = "Test User";
            string email = $"testuser_{Guid.NewGuid()}@example.com";
            string password = "testpass";

            bool result = _authController.Register(name, email, password);

            Assert.IsTrue(result, "Register harus berhasil jika email belum terdaftar");
            Assert.IsTrue(_userController.CheckIfEmailExists(email), "Email harus terdaftar setelah registrasi");
        }

        /// <summary>
        /// Test register user dengan email yang sudah terdaftar.
        /// Harus gagal dan mengembalikan false.
        /// </summary>
        [TestMethod]
        public void Register_ShouldReturnFalse_WhenEmailAlreadyExists()
        {
            // Ambil email dari file JSON yang sudah ada (pastikan ada email ini di test_users.json)
            string existingEmail = "existing@example.com";

            bool result = _authController.Register("New User", existingEmail, "newpass");

            Assert.IsFalse(result, "Register harus gagal jika email sudah terdaftar");
        }

        /// <summary>
        /// Test login dengan kredensial valid.
        /// Harus berhasil dan mengembalikan objek User.
        /// </summary>
        [TestMethod]
        public void Login_ShouldReturnUser_WhenCredentialsAreValid()
        {
            string email = $"loginuser_{Guid.NewGuid()}@example.com";
            string password = "loginpass";

            // Buat user dulu di file temp
            _userController.CreateUser("Login User", email, password);

            var user = _authController.Login(email, password);

            Assert.IsNotNull(user, "Login harus mengembalikan User jika kredensial valid");
            Assert.AreEqual(email, user.Email, "Email user harus sesuai input login");
        }

        /// <summary>
        /// Test login dengan password salah.
        /// Harus gagal dan mengembalikan null.
        /// </summary>
        [TestMethod]
        public void Login_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            string email = $"loginfail_{Guid.NewGuid()}@example.com";
            string password = "correctpass";

            _userController.CreateUser("Fail User", email, password);

            var user = _authController.Login(email, "wrongpass");

            Assert.IsNull(user, "Login harus gagal dan return null jika password salah");
        }

        /// <summary>
        /// Test method Logout yang hanya menampilkan pesan.
        /// Pastikan tidak melempar exception.
        /// </summary>
        [TestMethod]
        public void Logout_ShouldNotThrowException()
        {
            try
            {
                _authController.Logout();
            }
            catch (Exception ex)
            {
                Assert.Fail("Logout tidak boleh melempar exception, tapi dapat: " + ex.Message);
            }
        }
    }
}
