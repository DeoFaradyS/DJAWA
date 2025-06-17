using ManajemenGudangApp.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;

namespace ManajemenGudangApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            optionsBuilder.UseSqlite($"Data Source={Path.Combine(path, "gudang.db")}");
        }
    }
}