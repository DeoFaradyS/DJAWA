using ManajemenGudangApp.Data;
using ManajemenGudangApp.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls; // Diperlukan untuk PasswordBox
using System.Windows.Input;

namespace ManajemenGudangApp.ViewModels
{
    public class RegisterViewModel
    {
        private readonly AppDbContext _context = new AppDbContext();

        public string Username { get; set; } = "";
        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register(object? parameter)
        {
            // Ambil password dari PasswordBox yang dikirim sebagai parameter
            var passwordBox = parameter as PasswordBox;
            if (passwordBox == null) return;

            string password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong.", "Registrasi Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // VALIDASI: Cek apakah username sudah ada
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
        }
    }
}