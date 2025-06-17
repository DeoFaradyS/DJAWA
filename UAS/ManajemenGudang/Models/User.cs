// File: Models/User.cs
namespace ManajemenGudang.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<Barang> Barangs { get; set; } = new List<Barang>();
        public string Role { get; set; } = string.Empty;
    }
}