// File: ViewModels/LoginViewModel.cs
using ManajemenGudang.Data;
using ManajemenGudang.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ManajemenGudang.ViewModels
{
    public class LoginViewModel
    {
        private readonly AppDbContext _context = new AppDbContext();

        public string Username { get; set; } = "";
        public ICommand LoginCommand { get; }
        public ICommand OpenRegisterViewCommand { get; }

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

            // Cari user di database
            var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.Password == password);

            if (user != null)
            {
                // KONDISI 3 & 4: Login berhasil dan aplikasi berhenti
                MessageBox.Show("Login Berhasil!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown(); // Menutup seluruh aplikasi
            }
            else
            {
                // KONDISI 2: Login gagal
                MessageBox.Show("Username atau Password salah.", "Login Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenRegisterView(object? parameter)
        {
            RegisterView registerView = new RegisterView();
            registerView.ShowDialog(); // ShowDialog agar jendela login menunggu
        }
    }
}