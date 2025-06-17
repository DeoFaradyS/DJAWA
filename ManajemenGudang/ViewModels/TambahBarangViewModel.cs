// File: ViewModels/TambahBarangViewModel.cs
using ManajemenGudang.Data;
using ManajemenGudang.Models;
using ManajemenGudang.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ManajemenGudang.ViewModels
{
    public class TambahBarangViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context = new();
        public string NamaBarangInput { get; set; } = "";
        public string DeskripsiInput { get; set; } = "";
        public int JumlahInput { get; set; }

        public ICommand? TambahBarangCommand { get; }
        public ICommand? KembaliCommand { get; }
        public Action? RequestClose;

        public TambahBarangViewModel()
        {
            TambahBarangCommand = new RelayCommand(TambahBarang);
            KembaliCommand = new RelayCommand(Kembali);
        }

        private void TambahBarang(object? parameter)
        {
            var currentUser = AuthenticationService.GetInstance().CurrentUser;
            if (currentUser == null) return;

            var barangBaru = new Barang
            {
                NamaBarang = this.NamaBarangInput,
                Deskripsi = this.DeskripsiInput,
                Jumlah = this.JumlahInput,
                Status = "Belum Disetujui",
                TanggalInput = DateTime.Now,
                TanggalUpdateStatus = DateTime.Now,
                UserId = currentUser.Id
            };
            _context.Barangs.Add(barangBaru);
            _context.SaveChanges();

            MessageBox.Show("Barang berhasil ditambahkan!", "Sukses");
            RequestClose?.Invoke(); // Tutup jendela setelah berhasil
        }

        private void Kembali(object? parameter)
        {
            RequestClose?.Invoke(); // Tutup jendela untuk kembali
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}