// File: ViewModels/LoginViewModel.cs

using System; // Pastikan using ini ada
using ManajemenGudang.Data;
using ManajemenGudang.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ManajemenGudang.Services;

namespace ManajemenGudang.ViewModels
{
    public class LoginViewModel
    {
        private readonly AppDbContext _context = new AppDbContext();

        public string Username { get; set; } = "";

        // --- PASTIKAN BARIS DI BAWAH INI ADA ---
        public Action? OnLoginSuccess;
        public Action? OnLoginSuccessAdmin { get; set; }
        // -----------------------------------------

        public ICommand? LoginCommand { get; }
        public ICommand? OpenRegisterViewCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            OpenRegisterViewCommand = new RelayCommand(OpenRegisterView);
        }

        private void Login(object? parameter)
        {
            var passwordBox = parameter as PasswordBox;
            if (passwordBox == null) return;
            string password = passwordBox.Password;

            // <-- MODIFIKASI LOGIKA LOGIN -->
            // 1. Cek Akun Dummy Admin
            if (Username.ToLower() == "admin" && password == "admin123")
            {
                // Buat objek User sementara untuk Admin
                var adminUser = new Models.User { Id = -1, Username = "admin", Role = "Admin" };
                Services.AuthenticationService.GetInstance().Login(adminUser);
                MessageBox.Show("Login sebagai Admin berhasil!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);

                // Panggil sinyal khusus Admin
                OnLoginSuccessAdmin?.Invoke();
                return; // Hentikan proses di sini
            }

            // 2. Jika bukan admin, cari di database untuk user biasa
            var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.Password == password);

            if (user != null)
            {
                Services.AuthenticationService.GetInstance().Login(user);
                MessageBox.Show("Login Berhasil!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);

                // Panggil sinyal untuk user biasa
                OnLoginSuccess?.Invoke();
            }
            else
            {
                MessageBox.Show("Username atau Password salah.", "Login Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenRegisterView(object? parameter)
        {
            RegisterView registerView = new RegisterView();
            registerView.ShowDialog();
        }
    }
}