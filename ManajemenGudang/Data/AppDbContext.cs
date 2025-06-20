// File: Data/AppDbContext.cs
// (Versi Final yang Disederhanakan)

using ManajemenGudang.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;

namespace ManajemenGudang.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Barang> Barangs { get; set; }

        // Constructor untuk Unit Test (dan sekarang juga untuk Performance Test)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Constructor untuk aplikasi utama WPF
        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Hanya konfigurasi jika belum dikonfigurasi dari luar (lewat constructor)
            if (!optionsBuilder.IsConfigured)
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                optionsBuilder.UseSqlite($"Data Source={Path.Combine(path, "gudang.db")}");
            }
        }
    }
}