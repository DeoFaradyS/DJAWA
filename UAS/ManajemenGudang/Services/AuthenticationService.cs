// File: Services/AuthenticationService.cs

using ManajemenGudang.Models; // Pastikan namespace ini benar

namespace ManajemenGudang.Services
{
    public class AuthenticationService
    {
        // Properti statis untuk menyimpan satu-satunya instance
        private static AuthenticationService? _instance;

        // Properti untuk menyimpan user yang sedang login
        public User? CurrentUser { get; private set; }

        // Private constructor agar tidak bisa dibuat dari luar
        private AuthenticationService() { }

        // Metode statis untuk mendapatkan instance (INI BAGIAN KRUSIAL DARI SINGLETON)
        public static AuthenticationService GetInstance()
        {
            // Jika instance belum ada, buat satu.
            if (_instance == null)
            {
                _instance = new AuthenticationService();
            }
            // Selalu kembalikan instance yang sama.
            return _instance;
        }

        public void Login(User user)
        {
            CurrentUser = user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}