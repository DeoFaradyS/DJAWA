using ManajemenGudang.Data;
using ManajemenGudang.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ManajemenGudang.ViewModels
{
    public class AdminDashboardViewModel : ViewModelBase
    {
        private readonly AppDbContext _context = new();
        private User? _selectedUser;
        private Barang? _selectedBarang;

        public ObservableCollection<User> AllUsers { get; set; }
        public ObservableCollection<Barang> SelectedUserItems { get; set; }
        public List<string> AvailableStatuses { get; } = new List<string> { "Barang Tersimpan", "Barang Keluar" };
        public string? SelectedStatus { get; set; }

        public User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                LoadItemsForSelectedUser();
                OnPropertyChanged();
            }
        }

        public Barang? SelectedBarang
        {
            get => _selectedBarang;
            set { _selectedBarang = value; OnPropertyChanged(); }
        }

        public ICommand UpdateStatusCommand { get; }

        public AdminDashboardViewModel()
        {
            // Muat semua user KECUALI admin dummy
            AllUsers = new ObservableCollection<User>(_context.Users.ToList());
            SelectedUserItems = new ObservableCollection<Barang>();
            UpdateStatusCommand = new RelayCommand(UpdateStatus, CanUpdateStatus);
        }

        private void LoadItemsForSelectedUser()
        {
            SelectedUserItems.Clear();
            if (SelectedUser != null)
            {
                var items = _context.Barangs.Where(b => b.UserId == SelectedUser.Id).ToList();
                foreach (var item in items)
                {
                    SelectedUserItems.Add(item);
                }
            }
        }

        private void UpdateStatus(object? parameter)
        {
            if (SelectedBarang == null || string.IsNullOrEmpty(SelectedStatus)) return;

            var itemInDb = _context.Barangs.Find(SelectedBarang.Id);
            if (itemInDb != null)
            {
                itemInDb.Status = SelectedStatus;
                itemInDb.TanggalUpdateStatus = DateTime.Now;
                _context.SaveChanges();

                // Refresh tampilan di UI
                LoadItemsForSelectedUser();
                MessageBox.Show("Status barang berhasil diperbarui.", "Sukses");
            }
        }

        private bool CanUpdateStatus(object? parameter)
        {
            // Tombol "Ubah Status" hanya aktif jika ada barang yang dipilih
            return SelectedBarang != null;
        }
    }
}