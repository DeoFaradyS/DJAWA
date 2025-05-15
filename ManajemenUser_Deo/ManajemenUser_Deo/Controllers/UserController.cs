using ManajemenUser_Deo.UserManagement;
using ManajemenUser_Deo.Config;
using System.Diagnostics;

namespace ManajemenUser_Deo.Controllers
{
    /// <summary>
    /// Mengatur manajemen pengguna seperti mencari, membuat, memperbarui, dan memvalidasi data pengguna.
    /// </summary>
    public class UserController
    {
        private const string FilePath = AppConfig.UserDataPath;

        /// <summary>
        /// Memuat daftar pengguna dari file JSON.
        /// </summary>
        private List<User> LoadUsers() 
            => JsonHelper.JsonHelper.LoadFromJson<User>(FilePath);

        /// <summary>
        /// Mencari pengguna berdasarkan email dan password.
        /// </summary>
        public User FindUserByCredentials(string email, string password)
        {
            Debug.Assert(!string.IsNullOrEmpty(email), "Email harus diisi");
            Debug.Assert(!string.IsNullOrEmpty(password), "Password harus diisi");

            var user = LoadUsers().FirstOrDefault(u => u.Email == email && u.Password == password);

            // Postcondition: jika ditemukan, email dan password harus sesuai
            if (user != null)
            {
                Debug.Assert(user.Email == email, "Email user harus sesuai");
                Debug.Assert(user.Password == password, "Password user harus sesuai");
            }

            return user;
        }

        /// <summary>
        /// Mengecek apakah email sudah terdaftar.
        /// </summary>
        public bool CheckIfEmailExists(string email)
        {
            Debug.Assert(!string.IsNullOrEmpty(email), "Email harus diisi");
            return LoadUsers().Exists(u => u.Email == email);
        }

        /// <summary>
        /// Membuat pengguna baru dan menyimpannya.
        /// </summary>
        public User CreateUser(string name, string email, string password)
        {
            Debug.Assert(!string.IsNullOrEmpty(name), "Name harus diisi");
            Debug.Assert(!string.IsNullOrEmpty(email), "Email harus diisi");
            Debug.Assert(!string.IsNullOrEmpty(password), "Password harus diisi");

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

            Debug.Assert(newUser.Id > 0, "User baru harus memiliki ID valid");
            return newUser;
        }

        /// <summary>
        /// Memperbarui data pengguna berdasarkan ID.
        /// </summary>
        public bool UpdateProfile(int id, string name, string email, string password)
        {
            Debug.Assert(id > 0, "ID harus positif");
            Debug.Assert(!string.IsNullOrEmpty(name), "Name harus diisi");
            Debug.Assert(!string.IsNullOrEmpty(email), "Email harus diisi");
            Debug.Assert(!string.IsNullOrEmpty(password), "Password harus diisi");

            var users = LoadUsers();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                user.Name = name;
                user.Email = email;
                user.Password = password;

                JsonHelper.JsonHelper.SaveToJson(users, FilePath);

                // Postcondition: data user harus sudah terupdate
                Debug.Assert(user.Name == name, "Name harus terupdate");
                Debug.Assert(user.Email == email, "Email harus terupdate");
                Debug.Assert(user.Password == password, "Password harus terupdate");

                return true;
            }
            else
            {
                Console.WriteLine("User tidak ditemukan.");
                return false;
            }
        }

        /// <summary>
        /// Menampilkan informasi pengguna ke konsol.
        /// </summary>
        public void PrintUserDetails(User user)
        {
            Debug.Assert(user != null, "User tidak boleh null");

            Console.WriteLine("=== Profile ===");
            Console.WriteLine($"Name        : {user.Name}");
            Console.WriteLine($"Email       : {user.Email}");
            Console.WriteLine($"Password    : {user.Password}");
            Console.WriteLine($"Role        : {user.Role}");
            Console.WriteLine($"Created At  : {user.CreatedAt}");
        }
    }
}
