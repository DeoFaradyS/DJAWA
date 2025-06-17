using ManajemenUser_Deo.Controllers;

namespace ManajemenUser_Deo.Views
{
    class Register
    {
        private readonly AuthController _authController = new AuthController();
        public void ShowRegistrationForm()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Registrasi ===");

                Console.Write("Masukkan nama: ");
                string name = Console.ReadLine();

                Console.Write("Masukkan email: ");
                string email = Console.ReadLine();

                Console.Write("Masukkan password: ");
                string password = Console.ReadLine();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("\nSemua field harus diisi! Silakan coba lagi.");
                    Console.WriteLine("Tekan tombol apa saja untuk melanjutkan...");
                    Thread.Sleep(2000);
                    continue;
                }

                bool success = _authController.Register(name, email, password);
                if (success)
                {
                    var loginView = new Login();
                    loginView.ShowLoginForm();
                    break;
                }
            }
        }

    }
}
