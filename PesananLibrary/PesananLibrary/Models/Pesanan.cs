namespace TransaksiAPI.Models
{
    public class Pesanan
    {
        public int Id { get; set; }
        public string Produk { get; set; }
        public int Jumlah { get; set; }
        public decimal Harga { get; set; }
        public string Status { get; set; } = "menunggu_konfirmasi";
        public DateTime Tanggal { get; set; } = DateTime.Now;
        public bool Terkonfirmasi { get; set; } = false;
    }

}
