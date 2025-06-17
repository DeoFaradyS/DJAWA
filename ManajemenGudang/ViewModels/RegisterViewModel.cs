// File: ViewModels/RegisterViewModel.cs

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ManajemenGudang.Data;
using ManajemenGudang.Models;

namespace ManajemenGudang.ViewModels
{
    public class RegisterViewModel
    {
        private readonly AppDbContext _context = new AppDbContext();

        public string Username { get; set; } = "";
        // Password tidak perlu properti karena kita ambil langsung dari PasswordBox

        // --- PASTIKAN BARIS DI BAWAH INI ADA ---
        public Action? RequestClose;
        // -----------------------------------------

        public ICommand RegisterCommand { get; }
        public ICommand BackToLoginCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
            BackToLoginCommand = new RelayCommand(BackToLogin);
        }

        private void Register(object? parameter)
        {
            var passwordBox = parameter as PasswordBox;
            if (passwordBox == null) return;
            string password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong.", "Registrasi Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool usernameExists = _context.Users.Any(u => u.Username.ToLower() == Username.ToLower());
            if (usernameExists)
            {
                MessageBox.Show("Username ini sudah digunakan. Silakan pilih username lain.", "Registrasi Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newUser = new User
            {
                Username = this.Username,
                Password = password
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
            RequestClose?.Invoke(); // Menutup jendela setelah sukses
        }

        private void BackToLogin(object? parameter)
        {
            RequestClose?.Invoke(); // Menutup jendela untuk kembali ke login
        }
    }
}