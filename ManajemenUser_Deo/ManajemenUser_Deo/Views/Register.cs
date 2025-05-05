using ManajemenUser_Deo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenUser_Deo.Views
{
    public class Register
    {
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

            // Panggil controller
            var auth = new AuthController();
            auth.Register(name, email, password);
        }
    }
}
