using ManajemenUser_Deo.Controllers;
using ManajemenUser_Deo.UserManagement;

namespace UserManagementTest
{
    [TestClass]
    public class UserControllerTests
    {
        private string _tempFilePath;
        private UserController _userController;

        [TestInitialize]
        public void Setup()
        {
            // Buat file JSON sementara sebagai dummy data user
            _tempFilePath = Path.Combine(
                Path.GetTempPath(),
                $"test_users_{Guid.NewGuid()}.json"
            );

            // Salin dari file JSON sumber ke file dummy agar data asli tidak berubah
            File.Copy("test_users.json", _tempFilePath, overwrite: true);

            // Inisialisasi UserController dengan path ke file dummy
            _userController = new UserController(_tempFilePath);
        }

        // Bersihkan file setelah test selesai
        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_tempFilePath))
            {
                File.Delete(_tempFilePath);
            }
        }

        // Test untuk memverifikasi bahwa email yang sudah ada terdeteksi
        [TestMethod]
        public void CheckIfEmailExists_ShouldReturnTrue_WhenEmailExists()
        {
            string existingEmail = "existing@example.com";

            bool result = _userController.CheckIfEmailExists(existingEmail);

            Assert.IsTrue(result);
        }

        // Test untuk memverifikasi bahwa email yang belum ada tidak terdeteksi
        [TestMethod]
        public void CheckIfEmailExists_ShouldReturnFalse_WhenEmailDoesNotExist()
        {
            string newEmail = "newemail@example.com";

            bool result = _userController.CheckIfEmailExists(newEmail);

            Assert.IsFalse(result);
        }

        // Test untuk memverifikasi user berhasil ditemukan dengan email dan password valid
        [TestMethod]
        public void FindUserByCredentials_ShouldReturnUser_WhenCredentialsAreValid()
        {
            string email = "existing@example.com";
            string password = "newpass"; 

            var user = _userController.FindUserByCredentials(email, password);

            Assert.IsNotNull(user);
            Assert.AreEqual(email, user.Email);
        }

        // Test untuk memverifikasi user tidak ditemukan jika password salah
        [TestMethod]
        public void FindUserByCredentials_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            string email = "existing@example.com";
            string wrongPassword = "wrongpass";

            // Act
            var user = _userController.FindUserByCredentials(email, wrongPassword);

            // Assert
            Assert.IsNull(user);
        }

        // Test untuk memastikan CreateUser berhasil dan mengembalikan user baru
        [TestMethod]
        public void CreateUser_ShouldReturnNewUser_WhenDataIsValid()
        {
            var name = "New User";
            var email = "newuser@example.com";
            var password = "newpass";

            var newUser = _userController.CreateUser(name, email, password);

            Assert.IsNotNull(newUser);
            Assert.AreEqual(name, newUser.Name);
            Assert.AreEqual(email, newUser.Email);
            Assert.AreEqual(password, newUser.Password);
            Assert.IsTrue(newUser.Id > 0);
        }

        // Test untuk memastikan UpdateProfile berhasil jika user ditemukan
        [TestMethod]
        public void UpdateProfile_ShouldReturnTrue_WhenUserExists()
        {
            var existingUser = _userController.CreateUser("User", "user@example.com", "oldpass");

            var result = _userController.UpdateProfile(existingUser.Id, "Updated User", "updated@example.com", "newpass");

            Assert.IsTrue(result);

            var updatedUser = _userController.FindUserByCredentials("updated@example.com", "newpass");
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual("Updated User", updatedUser.Name);
        }

        // Test untuk memastikan UpdateProfile gagal jika user tidak ditemukan
        [TestMethod]
        public void UpdateProfile_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            var result = _userController.UpdateProfile(999, "Ghost", "ghost@example.com", "nopass");

            Assert.IsFalse(result);
        }

        // Test untuk memastikan PrintUserDetails mencetak informasi user ke console
        [TestMethod]
        public void PrintUserDetails_ShouldPrintUserInfo()
        {
            var user = new User
            {
                Name = "Print Test",
                Email = "print@example.com",
                Password = "printpass",
                Role = UserRole.User,
                CreatedAt = DateTime.Now
            };

            var output = new StringWriter();
            Console.SetOut(output);

            _userController.PrintUserDetails(user);

            var result = output.ToString();
            Assert.IsTrue(result.Contains("=== Profile ==="));
            Assert.IsTrue(result.Contains("Print Test"));
            Assert.IsTrue(result.Contains("print@example.com"));
            Assert.IsTrue(result.Contains("printpass"));
        }

    }
}
