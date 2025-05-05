using ManajemenUser_Deo.UserManagement;
using ManajemenUser_Deo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenUser_Deo.Controllers
{
    internal class AuthController
    {
        private const string FilePath = "data_user.json";

        public void Register(string name, string email, string password)
        {
            // Load data user lama
            var users = Libraries.LoadFromJson<User>(FilePath);

            // Cek apakah email sudah digunakan
            if (users.Exists(u => u.Email == email))
            {
                Console.WriteLine("\nEmail sudah digunakan. Silakan gunakan email lain.");
                return;
            }

            // Buat user baru
            var newUser = new User
            {
                Id = users.Count,
                Name = name,
                Email = email,
                Password = password,
                CreatedAt = DateTime.Now,
                Role = UserRole.User
            };

            // Tambahkan dan simpan
            users.Add(newUser);
            Libraries.SaveToJson(users, FilePath);

            var loginView = new Login();
            loginView.ShowLoginForm("Registrasi berhasil!\n");
        }

        public void Login(string email, string password)
        {
            // Load data user lama
            var users = Libraries.LoadFromJson<User>(FilePath);

            var userResult = users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (userResult != null)
            {
                var profileView = new Profile();
                profileView.ShowProfile(userResult);
            }
            else
            {
                Console.WriteLine("Email atau password salah.");
            }
        }
    }
}
