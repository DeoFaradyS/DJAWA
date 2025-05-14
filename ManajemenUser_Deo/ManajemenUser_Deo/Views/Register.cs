using ManajemenUser_Deo.Controllers;

namespace ManajemenUser_Deo.Views
{
    class Register
    {
        private readonly AuthController _authController = new AuthController();
        public void ShowRegistrationForm()
        {
            Console.Clear();
            Console.WriteLine("=== Registrasi ===");

            Console.Write("Masukkan nama: ");
            string name = Console.ReadLine();

            Console.Write("Masukkan email: ");
            string email = Console.ReadLine();

            Console.Write("Masukkan password: ");
            string password = Console.ReadLine();

            _authController.Register(name, email, password);
        }
    }
}
