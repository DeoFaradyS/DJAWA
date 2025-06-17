// File: App.xaml.cs
using System.Windows;
using ManajemenGudangApp.Views;

namespace ManajemenGudangApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RegisterView registerView = new RegisterView();
            registerView.Show();
        }
    }
}