using ManajemenUser_Deo.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenUser_Deo.Views
{
    internal class Profile
    {
        public void ShowProfile(User user)
        {
            Console.Clear();
            Console.WriteLine("=== Profile ===");

            // Menampilkan data pengguna
            Console.WriteLine($"Name        : {user.Name}");
            Console.WriteLine($"Email       : {user.Email}");
            Console.WriteLine($"Password    : {user.Password}");
            Console.WriteLine($"Role        : {user.Role}");
            Console.WriteLine($"Created At  : {user.CreatedAt}");

            // Menampilkan opsi untuk edit profil atau logout
            Console.WriteLine("\nPilih opsi:");
            Console.WriteLine("1. Edit Profil");
            Console.WriteLine("2. Logout");
            Console.Write("Pilih opsi (1/2): ");
            string choice = Console.ReadLine();
        }
    }
}
