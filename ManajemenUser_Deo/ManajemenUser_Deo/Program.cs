using ManajemenUser_Deo;
using ManajemenUser_Deo.Controllers;
using ManajemenUser_Deo.Views;

class Program
{
    static void Main()
    {
        // Tampilkan menu
        Console.WriteLine("=== Aplikasi Manajemen User ===");
        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        Console.WriteLine("3. Keluar");
        Console.Write("Pilih opsi (1-3): ");

        string input = Console.ReadLine();

        // Proses input
        switch (input)
        {
            case "1":
                var registerView = new Register();
                registerView.ShowRegistrationForm();
                break;
            case "2":
                var loginView = new Login();
                loginView.ShowLoginForm();
                break;
            case "3":
                Console.WriteLine("\nKeluar dari aplikasi...");
                break;
            default:
                Console.WriteLine("\nInput tidak dikenali. Silakan coba lagi.");
                break;
        }
    }
}