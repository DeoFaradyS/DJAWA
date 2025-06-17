using Xunit;
using System.Collections.Generic;

public class OrderServiceTests
{
    [Fact]
    public void GetStatusPesananById_ReturnsCorrectStatus_WhenIdExists()
    {
        var result = OrderService.GetStatusPesananById("PS01");
        Assert.Equal("ID: PS01, Barang : Sabun Batang Lifeboy , Harga : Rp70.000, Status: Di Tunda", result);
    }

    [Fact]
    public void GetStatusPesananById_ReturnsNotFound_WhenIdNotExists()
    {
        var result = OrderService.GetStatusPesananById("INVALID");
        Assert.Equal("Pesanan tidak ditemukan.", result);
    }

    [Fact]
    public void GetAllRiwayatPesanan_ReturnsCorrectData()
    {
        var result = OrderService.GetAllRiwayatPesanan();
        Assert.Contains("ID: PS01, Barang : Sabun Batang Lifeboy , Harga : Rp70.000, Status: Di Tunda", result);
        Assert.Equal(4, result.Count);
    }
}
