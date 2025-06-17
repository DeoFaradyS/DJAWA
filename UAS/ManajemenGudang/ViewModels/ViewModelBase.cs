// File: ViewModels/ViewModelBase.cs

using ManajemenGudang.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ManajemenGudang.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // Sinyal untuk memberitahu View agar menutup dirinya dan menampilkan LoginView
        public Action? RequestCloseAndShowLogin;

        public ICommand LogoutCommand { get; }

        protected ViewModelBase()
        {
            LogoutCommand = new RelayCommand(Logout);
        }

        private void Logout(object? parameter)
        {
            // Hapus sesi user saat ini dari AuthenticationService
            AuthenticationService.GetInstance().Logout();

            // Kirim sinyal untuk melakukan navigasi
            RequestCloseAndShowLogin?.Invoke();
        }

        // Implementasi INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}