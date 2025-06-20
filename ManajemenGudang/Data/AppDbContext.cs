// File: Data/AppDbContext.cs
// (MODIFIED FOR TESTABILITY)

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

        // Constructor ini ditambahkan untuk unit testing
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Constructor lama tetap ada untuk aplikasi utama
        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Hanya konfigurasi jika belum dikonfigurasi dari luar (oleh unit test)
            if (!optionsBuilder.IsConfigured)
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
                optionsBuilder.UseSqlite($"Data Source={System.IO.Path.Combine(path, "gudang.db")}");
            }
        }
    }
}