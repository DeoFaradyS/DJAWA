// File: Models/User.cs
namespace ManajemenGudang.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // Peringatan: Jangan simpan password plain text di aplikasi nyata!
    }
}