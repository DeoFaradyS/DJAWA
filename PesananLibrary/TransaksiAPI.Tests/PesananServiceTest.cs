using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransaksiAPI.Models;
using TransaksiAPI.Services;
using System.Collections.Generic;

namespace TransaksiAPI.Tests
{
    [TestClass]
    public class PesananServiceTests
    {
        private PesananService service;

        [TestInitialize]
        public void Setup()
        {
            service = new PesananService();
        }

        [TestMethod]
        public void Tambah_ShouldAddNewPesanan()
        {
            // Arrange
            var pesananBaru = new Pesanan { Produk = "ProdukA", Jumlah = 2, Harga = 10000 };

            // Act
            var result = service.Tambah(pesananBaru);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("ProdukA", result.Produk);
            Assert.AreEqual(2, result.Jumlah);
            Assert.AreEqual(10000, result.Harga);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllPesanan()
        {
            // Arrange
            service.Tambah(new Pesanan { Produk = "ProdukA", Jumlah = 1, Harga = 5000 });
            service.Tambah(new Pesanan { Produk = "ProdukB", Jumlah = 2, Harga = 10000 });

            // Act
            var result = service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetById_ShouldReturnCorrectPesanan()
        {
            // Arrange
            var pesanan1 = service.Tambah(new Pesanan { Produk = "ProdukX", Jumlah = 3, Harga = 20000 });

            // Act
            var result = service.GetById(pesanan1.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ProdukX", result.Produk);
        }

        [TestMethod]
        public void Konfirmasi_ShouldMarkPesananAsConfirmed()
        {
            // Arrange
            var pesanan = service.Tambah(new Pesanan { Produk = "ProdukY", Jumlah = 1, Harga = 15000 });

            // Act
            var result = service.Konfirmasi(pesanan.Id);
            var updatedPesanan = service.GetById(pesanan.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(updatedPesanan.Terkonfirmasi);
        }

        [TestMethod]
        public void Hapus_ShouldRemovePesanan()
        {
            // Arrange
            var pesanan = service.Tambah(new Pesanan { Produk = "ProdukZ", Jumlah = 5, Harga = 7500 });

            // Act
            var result = service.Hapus(pesanan.Id);
            var afterDelete = service.GetById(pesanan.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(afterDelete);
        }
    }
}
