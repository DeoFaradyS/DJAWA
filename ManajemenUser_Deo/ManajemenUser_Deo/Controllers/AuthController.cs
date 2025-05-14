using ManajemenUser_Deo.Views;

namespace ManajemenUser_Deo.Controllers
{
    class AuthController
    {
        private UserController userController = new UserController();

        // Mendaftarkan user baru ke dalam sistem.
        public void Register(string name, string email, string password)
        {
            if (userController.CheckIfEmailExists(email))
            {
                Console.WriteLine("\nEmail sudah digunakan. Silakan gunakan email lain.");
                return;
            }

            var newUser = userController.CreateUser(name, email, password);
            Console.WriteLine($"Registrasi berhasil! Selamat datang, {newUser.Name}.");

            var loginView = new Login();
            loginView.ShowLoginForm("Registrasi berhasil!\n");
        }

        // Melakukan proses login pengguna berdasarkan email dan password.
        public void Login(string email, string password)
        {
            var authenticatedUser = userController.FindUserByCredentials(email, password);

            if (authenticatedUser == null)
            {
                Console.WriteLine("Email atau password salah.");
                return;
            }

            var profileView = new Profile();
            profileView.ShowProfile(authenticatedUser);
        }

        // Melakukan logout dan menampilkan pesan konfirmasi.
        public void Logout()
        {
            Console.WriteLine("\nAnda berhasil logout.");
            Console.ReadKey();
        }
    }
}
