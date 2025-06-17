using InventoryManagement.Views;
using System;
using System.Collections.Generic;

namespace InventoryManagement.Controllers
{
    public class BarangController
    {
        private List<Barang> daftarBarang = new List<Barang>();
        private readonly BarangView view = new BarangView();

        public List<Barang> GetDaftarBarang() => daftarBarang;


        //Fitur tambah barang
        public void TambahBarang()
        {
            Barang barangBaru = view.InputBarang();
            daftarBarang.Add(barangBaru);
            view.TampilkanPesan("Barang berhasil ditambahkan.");
        }

        public void LihatBarang()
        {
            view.TampilkanDaftarBarang(daftarBarang);
        }

        //Fitur hapus
        public void HapusBarang()
        {
            string nama = view.InputNamaBarang("hapus");
            Barang barang = daftarBarang.Find(
                b => b.Nama.ToLower() == nama.ToLower());
            if (barang != null)
            {
                daftarBarang.Remove(barang);
                view.TampilkanPesan("Barang berhasil dihapus.");
            }
            else
            {
                view.TampilkanPesan("Barang tidak ditemukan.");
            }
        }

        //fitur CARI
        public void CariBarang()
        {
            string keyword = view.InputNamaBarang("cari");
            List<Barang> hasilPencarian = daftarBarang.FindAll
                (
                b => b.Nama.ToLower().Contains(keyword.ToLower()
                ));

            if (hasilPencarian.Count > 0)
            {
                view.TampilkanDaftarBarang(hasilPencarian);
            }
            else
            {
                view.TampilkanPesan("Tidak ada " +
                    "barang yang cocok dengan kata kunci pencarian.");
            }
        }


        // fitur UPDATE
        public void UbahBarang()
        {
            string nama = view.InputNamaBarang("ubah");
            Barang barang = daftarBarang.Find(
                b => b.Nama.ToLower() == nama.ToLower());

            if (barang != null)
            {
                Barang barangBaru = view.InputBarangUpdate(barang);
                barang.Nama = barangBaru.Nama;
                barang.Deskripsi = barangBaru.Deskripsi;
                barang.Jumlah = barangBaru.Jumlah;
                barang.Harga = barangBaru.Harga;
                view.TampilkanPesan("Barang berhasil diperbarui.");
            }
            else
            {
                view.TampilkanPesan("Barang tidak ditemukan.");
            }
        }
    }
}
