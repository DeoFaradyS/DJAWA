using TransaksiAPI.Models;

namespace TransaksiAPI.Services
{
    public class PesananService
    {
        private static List<Pesanan> daftarPesanan = new List<Pesanan>();
        private static int idCounter = 1;

        public List<Pesanan> GetAll() => daftarPesanan;

        public Pesanan? GetById(int id) => daftarPesanan.FirstOrDefault(p => p.Id == id);

        public Pesanan Tambah(Pesanan pesanan)
        {
            pesanan.Id = idCounter++;
            daftarPesanan.Add(pesanan);
            return pesanan;
        }

        public bool Hapus(int id)
        {
            var pesanan = GetById(id);
            if (pesanan == null) return false;
            daftarPesanan.Remove(pesanan);
            return true;
        }

        //public bool Ubah(int id, Pesanan dataBaru)
        //{
        //    var pesanan = GetById(id);
        //    if (pesanan == null) return false;
        //    pesanan.Produk = dataBaru.Produk;
        //    pesanan.Jumlah = dataBaru.Jumlah;
        //    pesanan.Harga = dataBaru.Harga;
        //    return true;
        //}

        public bool Konfirmasi(int id)
        {
            var pesanan = GetById(id);
            if (pesanan == null) return false;
            pesanan.Status = "dikonfirmasi";
            return true;
        }
    }
}
