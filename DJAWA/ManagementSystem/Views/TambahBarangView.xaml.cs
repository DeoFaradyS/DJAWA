using ManajemenGudang.ViewModels;
using System.Windows;

namespace ManajemenGudang.Views
{
    public partial class TambahBarangView : Window
    {
        public TambahBarangView()
        {
            InitializeComponent();
            if (this.DataContext is TambahBarangViewModel viewModel)
            {
                viewModel.RequestClose = () => { this.Close(); };
            }
        }
    }
}