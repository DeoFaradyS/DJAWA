// File: Data/AppDbContext.cs
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Pastikan Anda menggunakan kode ini untuk menentukan lokasi database secara pasti
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
            optionsBuilder.UseSqlite($"Data Source={System.IO.Path.Combine(path, "gudang.db")}");
        }
    }
}