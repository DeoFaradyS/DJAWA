using System;

namespace ManajemenPesananSederhana
{
    public class Order
    {
        public int OrderId { get; set; }
        public string NamaPelanggan { get; set; }
        public string Status { get; set; }
        public DateTime TanggalPesan { get; set; }
        public int Stok { get; set; }
        public decimal Harga { get; set; }
    }
}
