using ManajemenUser_Deo.Views;

class Program
{
    static void Main()
    {
        bool isRunning = true;

        while (isRunning)
        {
            ShowMenu();
            string input = Console.ReadLine();
            isRunning = ProcessInput(input); 
        }
    }

    private static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Aplikasi Manajemen User ===");
        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        Console.WriteLine("3. Keluar");
        Console.Write("Pilih opsi (1-3): ");
    }

    private static bool ProcessInput(string input)
    {
        switch (input)
        {
            case "1":
                var registerView = new Register();
                registerView.ShowRegistrationForm();
                return true;
            case "2":
                var loginView = new Login();
                loginView.ShowLoginForm();
                return true;
            case "3":
                Console.WriteLine("\nKeluar dari aplikasi...");
                Thread.Sleep(2000);
                return false;
            default:
                Console.WriteLine("\nInput tidak dikenali. Silakan coba lagi.");
                Thread.Sleep(2000);
                return true;
        }
    }
}
