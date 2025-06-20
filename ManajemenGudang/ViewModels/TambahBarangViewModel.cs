// File: ViewModels/TambahBarangViewModel.cs
// (MODIFIED FOR TESTABILITY)

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
        private readonly AppDbContext _context;

        public string NamaBarangInput { get; set; } = "";
        public string DeskripsiInput { get; set; } = "";
        public int JumlahInput { get; set; }

        public ICommand? TambahBarangCommand { get; }
        public ICommand? KembaliCommand { get; }
        public Action? RequestClose;

        // Constructors untuk DI dan WPF
        public TambahBarangViewModel() : this(new AppDbContext()) { }
        public TambahBarangViewModel(AppDbContext context)
        {
            _context = context;
            TambahBarangCommand = new RelayCommand(ExecuteTambahBarang);
            KembaliCommand = new RelayCommand(Kembali);
        }

        // Metode yang dipanggil UI, bertugas sebagai "Adapter"
        private void ExecuteTambahBarang(object? parameter)
        {
            // Panggil logika inti
            if (PerformTambahBarang())
            {
                // Jika logika berhasil, baru tampilkan UI feedback
                MessageBox.Show("Barang berhasil ditambahkan!", "Sukses");
                RequestClose?.Invoke(); // Tutup jendela setelah berhasil
            }
            else
            {
                // Di masa depan, Anda bisa menambahkan logika untuk menampilkan error
                // jika PerformTambahBarang() mengembalikan false karena validasi gagal.
                MessageBox.Show("Gagal menambahkan barang. Periksa kembali input Anda.", "Gagal");
            }
        }

        // --- INI METODE BARU YANG AKAN KITA UJI ---
        // Logika murni, tanpa UI, mengembalikan boolean
        public bool PerformTambahBarang()
        {
            var currentUser = AuthenticationService.GetInstance().CurrentUser;
            if (currentUser == null)
            {
                // Tidak bisa menambah barang jika tidak ada user yang login
                return false;
            }

            // Di sini Anda bisa menambahkan validasi, contoh:
            if (string.IsNullOrWhiteSpace(NamaBarangInput) || JumlahInput <= 0)
            {
                return false; // Gagal jika nama kosong atau jumlah tidak valid
            }

            var barangBaru = new Barang
            {
                NamaBarang = this.NamaBarangInput,
                Deskripsi = this.DeskripsiInput,
                Jumlah = this.JumlahInput,
                Status = "Belum Disetujui",
                TanggalInput = DateTime.Now,
                TanggalUpdateStatus = DateTime.Now,
                UserId = currentUser.Id // Gunakan Id dari user yang sedang login
            };

            _context.Barangs.Add(barangBaru);
            _context.SaveChanges();

            return true; // Sukses
        }

        private void Kembali(object? parameter)
        {
            RequestClose?.Invoke();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}