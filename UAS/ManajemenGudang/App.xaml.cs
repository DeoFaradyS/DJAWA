// File: App.xaml.cs
using System.Windows;
using ManajemenGudang.Views;

namespace ManajemenGudang
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Ubah ini agar aplikasi dimulai dari halaman Login
            LoginView loginView = new LoginView();
            loginView.Show();
        }
    }
}