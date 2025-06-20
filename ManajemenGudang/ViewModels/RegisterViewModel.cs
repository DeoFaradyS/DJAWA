// File: ViewModels/RegisterViewModel.cs
// (MODIFIED FOR TESTABILITY)

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
        private readonly AppDbContext _context;

        public string Username { get; set; } = "";
        public Action? RequestClose;

        public ICommand RegisterCommand { get; }
        public ICommand BackToLoginCommand { get; }

        public RegisterViewModel() : this(new AppDbContext()) { }
        public RegisterViewModel(AppDbContext context)
        {
            _context = context;
            RegisterCommand = new RelayCommand(ExecuteRegister); // Ubah pemanggilan ke metode baru
            BackToLoginCommand = new RelayCommand(BackToLogin);
        }

        // Metode ini yang akan dipanggil oleh Command dari UI
        private void ExecuteRegister(object? parameter)
        {
            var passwordBox = parameter as PasswordBox;
            if (passwordBox == null) return;
            string password = passwordBox.Password;

            // Panggil metode logika utama, dan proses hasilnya
            if (PerformRegistration(password))
            {
                MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                RequestClose?.Invoke(); // Menutup jendela setelah sukses
            }
        }

        // --- INI METODE BARU YANG AKAN KITA UJI ---
        // Metode ini berisi logika murni, tanpa referensi ke UI (MessageBox/PasswordBox)
        // dan mengembalikan boolean untuk menandakan sukses atau gagal.
        public bool PerformRegistration(string password)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                // Di aplikasi nyata, pesan error akan ditangani oleh ExecuteRegister
                return false;
            }

            bool usernameExists = _context.Users.Any(u => u.Username.ToLower() == Username.ToLower());
            if (usernameExists)
            {
                // Di aplikasi nyata, pesan error akan ditangani oleh ExecuteRegister
                return false;
            }

            var newUser = new User
            {
                Username = this.Username,
                Password = password,
                Role = "User"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return true; // Registrasi sukses
        }

        private void BackToLogin(object? parameter)
        {
            RequestClose?.Invoke();
        }
    }
}