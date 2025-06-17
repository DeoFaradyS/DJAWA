using System.Windows;
using ManajemenGudang.ViewModels;

namespace ManajemenGudang.Views
{
    public partial class AdminDashboardView : Window
    {
        public AdminDashboardView()
        {
            InitializeComponent();
            // Tambahkan kode ini
            if (this.DataContext is ViewModels.ViewModelBase viewModel)
            {
                viewModel.RequestCloseAndShowLogin = () =>
                {
                    LoginView loginView = new LoginView();
                    loginView.Show();
                    this.Close();
                };
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}