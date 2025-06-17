using ManajemenGudang.ViewModels; // Diperlukan untuk mengakses ViewModelBase
using System.Windows;

namespace ManajemenGudang.Views
{
    public partial class RiwayatBarangView : Window
    {
        public RiwayatBarangView()
        {
            InitializeComponent();

            if (this.DataContext is ViewModelBase viewModel)
            {
                // Berlangganan sinyal logout
                viewModel.RequestCloseAndShowLogin = () =>
                {
                    // Jika sinyal diterima, buka LoginView
                    LoginView loginView = new LoginView();
                    loginView.Show();

                    // Tutup jendela ini
                    this.Close();
                };
            }
        }
    }
}