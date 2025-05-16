using System;
using System.Collections.Generic;

public static class OrderService
{
    public static List<Order> orders = new List<Order>
    {
        new Order { Id = "PS01", Barang = "Sabun Batang Lifeboy ", Status = "Di Tunda", Harga = "70.000" },
        new Order { Id = "PS02", Barang = "Shampo Pantine + Conditioner ", Status = "Di Kirim", Harga = "40.000" },
        new Order { Id = "PS03", Barang = "Rinso Detergen ", Status = "Dalam Pengeriman", Harga = "25.000" },
        new Order { Id = "PS04", Barang = "Mama Lemon", Status = "Di Terima", Harga = "60.000" }
    };

    public static string GetStatusPesananById(string id)
    {
        var order = orders.Find(o => o.Id == id);
        return order == null ? "Pesanan tidak ditemukan." :
            $"ID: {order.Id}, Barang : {order.Barang}, Harga : Rp{order.Harga:N0}, Status: {order.Status}";
    }

    public static List<string> GetAllRiwayatPesanan()
    {
        List<string> result = new();
        foreach (var order in orders)
        {
            result.Add($"ID: {order.Id}, Barang : {order.Barang}, Harga : Rp{order.Harga:N0}, Status: {order.Status}");
        }
        return result;
    }

    public static void LihatStatusPesanan()
    {
        Console.Write("Masukkan ID: ");
        string id = Console.ReadLine();
        var order = orders.Find(o => o.Id == id);

        if (order == null)
            Console.WriteLine("Pesanan tidak ditemukan.");
        else
        {
            Console.WriteLine($"ID: {order.Id}, Barang : {order.Barang}, Harga : Rp{order.Harga:N0}, Status: {order.Status}");
        }

        Console.WriteLine("Tekan Enter...");
        Console.ReadLine();
    }

    public static void LihatRiwayatPesanan()
    {
        Console.WriteLine("Riwayat Pesanan:");
        foreach (var order in orders)
        {
            Console.WriteLine($"ID: {order.Id}, Barang : {order.Barang}, Harga : Rp{order.Harga:N0}, Status: {order.Status}");
        }
        Console.WriteLine("Tekan Enter...");
        Console.ReadLine();
    }
}
