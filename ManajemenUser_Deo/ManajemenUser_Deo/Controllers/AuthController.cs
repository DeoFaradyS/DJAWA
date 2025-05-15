using System.Diagnostics;
using ManajemenUser_Deo.UserManagement;

namespace ManajemenUser_Deo.Controllers
{
    class AuthController
    {
        private UserController _userController = new UserController();

        /// <summary>
        /// Mendaftarkan user baru ke sistem.
        /// Mengembalikan false jika email sudah terdaftar.
        /// </summary>
        public bool Register(string name, string email, string password)
        {
            // Precondition: input tidak boleh null atau kosong
            Debug.Assert(!string.IsNullOrEmpty(name), "Name harus diisi");
            Debug.Assert(!string.IsNullOrEmpty(email), "Email harus diisi");
            Debug.Assert(!string.IsNullOrEmpty(password), "Password harus diisi");

            if (_userController.CheckIfEmailExists(email))
            {
                Console.WriteLine("\nEmail sudah digunakan. Silakan gunakan email lain.");
                Console.ReadKey();
                return false;
            }

            var newUser = _userController.CreateUser(name, email, password);

            // Postcondition: user baru harus berhasil dibuat dan emailnya sesuai
            Debug.Assert(newUser != null, "User baru harus berhasil dibuat");
            Debug.Assert(newUser.Email == email, "Email user baru harus sama dengan input");

            Console.WriteLine($"\nRegistrasi berhasil! Silakan login untuk melanjutkan.");
            Console.ReadKey();
            return true;
        }

        /// <summary>
        /// Melakukan autentikasi user berdasarkan email dan password.
        /// Mengembalikan objek User jika berhasil, null jika gagal.
        /// </summary>
        public User Login(string email, string password)
        {
            // Precondition: input tidak boleh null atau kosong
            Debug.Assert(!string.IsNullOrEmpty(email), "Email harus diisi");
            Debug.Assert(!string.IsNullOrEmpty(password), "Password harus diisi");

            var authenticatedUser = _userController.FindUserByCredentials(email, password);

            if (authenticatedUser == null)
            {
                Console.WriteLine("Email atau password salah.");
                Console.ReadKey();
                return null;
            }

            // Postcondition: authenticatedUser tidak boleh null saat login berhasil
            Debug.Assert(authenticatedUser != null, "User yang terautentikasi tidak boleh null");

            return authenticatedUser;
        }

        /// <summary>
        /// Proses logout user dengan menampilkan konfirmasi.
        /// </summary>
        public void Logout()
        {
            Console.WriteLine("\nAnda berhasil logout.");
            Console.ReadKey();
        }
    }
}
