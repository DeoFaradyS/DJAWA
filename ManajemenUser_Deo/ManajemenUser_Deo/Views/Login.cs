using ManajemenUser_Deo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ManajemenUser_Deo.Views
{
    internal class Login
    {
        public void ShowLoginForm(string message = "")
        {
            Console.Clear();
            if (!string.IsNullOrEmpty(message)) 
            {
                Console.WriteLine(message);
            }
            Console.WriteLine("=== Login ===");

            Console.Write("Masukkan email: ");
            string email = Console.ReadLine();

            Console.Write("Masukkan password: ");
            string password = Console.ReadLine();

            // Panggil controller
            var auth = new AuthController();
            auth.Login(email, password);
        }
    }
}
