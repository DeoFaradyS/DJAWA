// File: Views/LoginView.xaml.cs

using System.Windows;
using ManajemenGudang.ViewModels;

namespace ManajemenGudang.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

            if (this.DataContext is ViewModels.LoginViewModel viewModel)
            {
                // Alur untuk User biasa (tetap sama)
                viewModel.OnLoginSuccess = () =>
                {
                    RiwayatBarangView riwayatView = new RiwayatBarangView();
                    riwayatView.Show();
                    this.Close();
                };

                // <-- TAMBAHKAN LOGIKA INI UNTUK ADMIN -->
                viewModel.OnLoginSuccessAdmin = () =>
                {
                    // Buka halaman dashboard admin
                    AdminDashboardView adminView = new AdminDashboardView();
                    adminView.Show();
                    this.Close();
                };
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}