using ManajemenUser_Deo.Controllers;

namespace ManajemenUser_Deo.Views
{
    class Login
    {
        private readonly AuthController _authController = new AuthController();

        public void ShowLoginForm(string message = "")
        {
            Console.Clear();

            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message + "\n");
            }

            // Input User
            Console.WriteLine("=== Login ===\n");

            Console.Write("Masukkan email: ");
            string email = Console.ReadLine();

            Console.Write("Masukkan password: ");
            string password = Console.ReadLine();

            _authController.Login(email, password);
        }
    }
}
