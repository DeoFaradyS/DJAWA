using ManajemenUser_Deo.UserManagement;
using ManajemenUser_Deo.Config;

namespace ManajemenUser_Deo.Controllers
{
    /// <summary>
    /// Mengatur manajemen pengguna seperti mencari, membuat, memperbarui, dan memvalidasi data pengguna.
    /// </summary>
    public class UserController
    {
        private const string FilePath = AppConfig.UserDataPath;

        // Memuat daftar pengguna dari file JSON
        private List<User> LoadUsers()
        {
            return JsonHelper.JsonHelper.LoadFromJson<User>(FilePath);
        }

        // Mencari pengguna berdasarkan kredensial (email dan password)
        public User FindUserByCredentials(string email, string password)
        {
            var users = LoadUsers();
            return users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        // Memeriksa apakah email sudah terdaftar
        public bool CheckIfEmailExists(string email)
        {
            var users = LoadUsers();
            return users.Exists(u => u.Email == email);
        }

        // Membuat pengguna baru
        public User CreateUser(string name, string email, string password)
        {
            var users = LoadUsers();

            var newUser = new User
            {
                Id = users.Count + 1,
                Name = name,
                Email = email,
                Password = password,
                CreatedAt = DateTime.Now,
                Role = UserRole.User
            };

            users.Add(newUser);
            JsonHelper.JsonHelper.SaveToJson(users, FilePath);

            return newUser;
        }

        // Memperbarui profil pengguna
        public bool UpdateProfile(int id, string name, string email, string password)
        {
            var users = LoadUsers();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                user.Name = name;
                user.Email = email;
                user.Password = password;

                JsonHelper.JsonHelper.SaveToJson(users, FilePath);
                return true;
            }
            else
            {
                Console.WriteLine("User tidak ditemukan.");
                return false;
            }
        }

        // Menampilkan detail profil pengguna.
        public void PrintUserDetails(User user)
        {
            Console.WriteLine("=== Profile ===");
            Console.WriteLine($"Name        : {user.Name}");
            Console.WriteLine($"Email       : {user.Email}");
            Console.WriteLine($"Password    : {user.Password}");
            Console.WriteLine($"Role        : {user.Role}");
            Console.WriteLine($"Created At  : {user.CreatedAt}");
        }
    }
}
