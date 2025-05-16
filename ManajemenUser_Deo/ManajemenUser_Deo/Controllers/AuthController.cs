using System.Diagnostics;
using ManajemenUser_Deo.UserManagement;

namespace ManajemenUser_Deo.Controllers
{
    public class AuthController
    {
        private UserController _userController;

        public AuthController(UserController? userController = null)
        {
            _userController = userController ?? new UserController();
        }

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
                Thread.Sleep(2000);
                return false;
            }

            var newUser = _userController.CreateUser(name, email, password);

            // Postcondition: user baru harus berhasil dibuat dan emailnya sesuai
            Debug.Assert(newUser != null, "User baru harus berhasil dibuat");
            Debug.Assert(newUser.Email == email, "Email user baru harus sama dengan input");

            Console.WriteLine($"\nRegistrasi berhasil! Silakan login untuk melanjutkan.");
            Thread.Sleep(2000);
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
                Thread.Sleep(2000);
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
            Thread.Sleep(2000);
        }
    }
}
