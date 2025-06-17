// File: Models/Barang.cs
using System;

namespace ManajemenGudang.Models
{
    public class Barang
    {
        public int Id { get; set; }
        public string NamaBarang { get; set; } = string.Empty;
        public string Deskripsi { get; set; } = string.Empty;
        public int Jumlah { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime TanggalInput { get; set; }

        public DateTime TanggalUpdateStatus { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}