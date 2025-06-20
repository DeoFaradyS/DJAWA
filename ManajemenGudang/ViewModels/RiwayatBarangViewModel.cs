// File: ViewModels/RiwayatBarangViewModel.cs
// (MODIFIED FOR TESTABILITY)

using ManajemenGudang.Data;
using ManajemenGudang.Models;
using ManajemenGudang.Services;
using ManajemenGudang.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ManajemenGudang.ViewModels
{
    public class RiwayatBarangViewModel : ViewModelBase
    {
        private readonly AppDbContext _context; // Dihapus ' = new()'
        private readonly User? _currentUser;
        public string SearchText { get; set; } = "";
        public ObservableCollection<Barang> DaftarBarang { get; set; }

        public ICommand? OpenTambahBarangViewCommand { get; }
        public ICommand? CariBarangCommand { get; }

        // Constructor default untuk WPF
        public RiwayatBarangViewModel() : this(new AppDbContext())
        {
        }

        // Constructor untuk DI dan Unit Testing
        public RiwayatBarangViewModel(AppDbContext context)
        {
            _context = context;
            DaftarBarang = new ObservableCollection<Barang>();

            // Baris ini opsional di dalam test, karena bisa menyebabkan error jika dijalankan di Designer
            // Tapi kita biarkan agar fungsionalitas di aplikasi utama tetap sama.
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

            _currentUser = AuthenticationService.GetInstance().CurrentUser;
            if (_currentUser == null)
            {
                // Di aplikasi nyata, ini akan menutup aplikasi. Di test, kita bisa uji skenario ini.
                MessageBox.Show("Sesi tidak ditemukan, aplikasi akan ditutup.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current?.Shutdown();
                return;
            }

            LoadUserItems();
            OpenTambahBarangViewCommand = new RelayCommand(OpenTambahBarangView);
            CariBarangCommand = new RelayCommand(CariBarang);
        }

        private void LoadUserItems()
        {
            if (_currentUser == null) return;
            var items = _context.Barangs.Where(b => b.UserId == _currentUser.Id).ToList();
            DaftarBarang.Clear();
            foreach (var item in items)
            {
                DaftarBarang.Add(item);
            }
        }

        private void CariBarang(object? parameter)
        {
            if (_currentUser == null) return;
            var hasil = _context.Barangs
                .Where(b => b.UserId == _currentUser.Id && b.NamaBarang.ToLower().Contains(SearchText.ToLower()))
                .ToList();
            DaftarBarang.Clear();
            foreach (var barang in hasil)
            {
                DaftarBarang.Add(barang);
            }
        }

        private void OpenTambahBarangView(object? parameter)
        {
            TambahBarangView tambahView = new TambahBarangView();
            tambahView.ShowDialog();
            LoadUserItems(); // Refresh data setelah window ditutup
        }
    }
}