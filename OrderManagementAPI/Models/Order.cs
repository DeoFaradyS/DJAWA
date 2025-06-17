using System;

namespace OrderManagementAPI.Models
{
    public enum OrderStatus
    {
        Pending,
        Diproses,
        Dikirim,
        Selesai
    }

    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
