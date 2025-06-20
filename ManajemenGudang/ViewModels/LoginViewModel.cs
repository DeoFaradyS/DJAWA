// File: ViewModels/LoginViewModel.cs
// (MODIFIED FOR TESTABILITY)

using System;
using ManajemenGudang.Data;
using ManajemenGudang.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ManajemenGudang.Services;

namespace ManajemenGudang.ViewModels
{
    public enum LoginResult
    {
        Success,
        AdminSuccess,
        Failed
    }

    public class LoginViewModel
    {
        private readonly AppDbContext _context;

        public string Username { get; set; } = "";
        public Action? OnLoginSuccess;
        public Action? OnLoginSuccessAdmin { get; set; }

        public ICommand? LoginCommand { get; }
        public ICommand? OpenRegisterViewCommand { get; }

        // Constructors untuk DI dan WPF
        public LoginViewModel() : this(new AppDbContext()) { }
        public LoginViewModel(AppDbContext context)
        {
            _context = context;
            LoginCommand = new RelayCommand(ExecuteLogin);
            OpenRegisterViewCommand = new RelayCommand(OpenRegisterView);
        }

        // Metode yang dipanggil UI, bertugas sebagai "Adapter"
        private void ExecuteLogin(object? parameter)
        {
            var passwordBox = parameter as PasswordBox;
            if (passwordBox == null) return;
            string password = passwordBox.Password;

            // Panggil logika inti
            var result = PerformLogin(password);

            // Proses hasil dari logika inti untuk berinteraksi dengan UI
            if (result == LoginResult.AdminSuccess)
            {
                var adminUser = new Models.User { Id = -1, Username = "admin", Role = "Admin" };
                AuthenticationService.GetInstance().Login(adminUser);
                MessageBox.Show("Login sebagai Admin berhasil!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                OnLoginSuccessAdmin?.Invoke();
            }
            else if (result == LoginResult.Success)
            {
                // Ambil user dari DB untuk disimpan di session
                var user = _context.Users.First(u => u.Username == Username);
                AuthenticationService.GetInstance().Login(user);
                MessageBox.Show("Login Berhasil!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                OnLoginSuccess?.Invoke();
            }
            else // Failed
            {
                MessageBox.Show("Username atau Password salah.", "Login Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // --- INI METODE BARU YANG AKAN KITA UJI ---
        // Logika murni, tanpa UI, mengembalikan enum
        public LoginResult PerformLogin(string password)
        {
            // 1. Cek Akun Dummy Admin
            if (Username.ToLower() == "admin" && password == "admin123")
            {
                return LoginResult.AdminSuccess;
            }

            // 2. Jika bukan admin, cari di database untuk user biasa
            var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.Password == password);

            if (user != null)
            {
                return LoginResult.Success;
            }
            else
            {
                return LoginResult.Failed;
            }
        }

        private void OpenRegisterView(object? parameter)
        {
            RegisterView registerView = new RegisterView();
            registerView.ShowDialog();
        }
    }
}