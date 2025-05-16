using Xunit;
using InventoryManagement.Controllers;
using InventoryManagement.Views;
using System.Collections.Generic;
using System.Reflection;

namespace InventoryManagement.Tests
{
    public class BarangControllerTests
    {
        [TestMethod]
        public void TambahBarang_ShouldAddBarangToList()
        {
            // Arrange
            var controller = new BarangControllerDummy();

            // Act
            controller.TambahBarang();

            // Assert
            var list = controller.GetDaftarBarang();
            Assert.Single(list);
            Assert.Equal("Laptop", list[0].Nama);
        }

        [TestMethod]
        public void HapusBarang_ShouldRemoveBarangFromList()
        {
            var controller = new BarangControllerDummy();
            controller.TambahBarang(); // tambah Laptop

            // Act
            controller.HapusBarang(); // hapus Laptop

            // Assert
            var list = controller.GetDaftarBarang();
            Assert.Empty(list);
        }
    }

    // Dummy controller untuk bypass inputan user
    class BarangControllerDummy : BarangController
    {
        public BarangControllerDummy()
        {
            // override view dengan dummy
            typeof(BarangController)
                .GetField("view", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(this, new DummyView());
        }

        public List<Barang> GetDaftarBarang()
        {
            var field = typeof(BarangController).GetField("daftarBarang", BindingFlags.NonPublic | BindingFlags.Instance);
            return (List<Barang>)field?.GetValue(this);
        }

        private class DummyView : BarangView
        {
            public override Barang InputBarang() => new Barang
            {
                Nama = "Laptop",
                Deskripsi = "Laptop Gaming",
                Jumlah = 5,
                Harga = 15000000
            };

            public override string InputNamaBarang(string aksi) => "Laptop";

            public override Barang InputBarangUpdate(Barang barangLama) => new Barang
            {
                Nama = "Laptop Baru",
                Deskripsi = "Laptop Gaming Update",
                Jumlah = 10,
                Harga = 17000000
            };

            public override void TampilkanPesan(string pesan) { }
            public override void TampilkanDaftarBarang(List<Barang> barangList) { }
        }
    }
    
}
