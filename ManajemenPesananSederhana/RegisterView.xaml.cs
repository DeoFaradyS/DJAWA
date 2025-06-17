// File: Views/RegisterView.xaml.cs
using System.Windows;
using ManajemenGudang.ViewModels;

namespace ManajemenGudang.Views
{
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();

            if (this.DataContext is RegisterViewModel viewModel)
            {
                viewModel.RequestClose = () =>
                {
                    this.Close();
                };
            }
        }
    }
}