using ManajemenUser_Deo.Controllers;

namespace ManajemenUser_Deo.Views
{
    class Login
    {
        private readonly AuthController _authController = new AuthController();

        public void ShowLoginForm()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== Login ===");

                Console.Write("Masukkan email: ");
                string email = Console.ReadLine();

                Console.Write("Masukkan password: ");
                string password = Console.ReadLine();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("\nEmail dan password tidak boleh kosong. Silakan coba lagi.");
                    Console.WriteLine("Tekan tombol apa saja untuk melanjutkan...");
                    Thread.Sleep(2000);
                    continue;
                }

                var user = _authController.Login(email, password);
                if (user != null)
                {
                    var profileView = new Profile();
                    profileView.ShowProfile(user);
                    break;
                }
                else
                {
                    Console.WriteLine("\nEmail atau password salah. Silakan coba lagi.");
                    Console.WriteLine("Tekan tombol apa saja untuk melanjutkan...");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
