using OrderManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagementAPI.Services
{
    public class OrderService
    {
        private static List<Order> orders = new List<Order>
        {
            new Order { Id = 1, CustomerName = "Kevin", Status = OrderStatus.Pending, OrderDate = DateTime.Now },
            new Order { Id = 2, CustomerName = "Deo", Status = OrderStatus.Diproses, OrderDate = DateTime.Now },
            new Order { Id = 3, CustomerName = "Alya", Status = OrderStatus.Pending, OrderDate = DateTime.Now },
            new Order { Id = 4, CustomerName = "Rizki", Status = OrderStatus.Diproses, OrderDate = DateTime.Now },
            new Order { Id = 5, CustomerName = "Salsa", Status = OrderStatus.Dikirim, OrderDate = DateTime.Now },
            new Order { Id = 6, CustomerName = "Bima", Status = OrderStatus.Selesai, OrderDate = DateTime.Now },
            new Order { Id = 7, CustomerName = "Zahra", Status = OrderStatus.Pending, OrderDate = DateTime.Now },
            new Order { Id = 8, CustomerName = "Adit", Status = OrderStatus.Diproses, OrderDate = DateTime.Now },
            new Order { Id = 9, CustomerName = "Farah", Status = OrderStatus.Dikirim, OrderDate = DateTime.Now },
            new Order { Id = 10, CustomerName = "Fajar", Status = OrderStatus.Selesai, OrderDate = DateTime.Now },
            new Order { Id = 11, CustomerName = "Lina", Status = OrderStatus.Pending, OrderDate = DateTime.Now },
            new Order { Id = 12, CustomerName = "Yusuf", Status = OrderStatus.Diproses, OrderDate = DateTime.Now },
            new Order { Id = 13, CustomerName = "Citra", Status = OrderStatus.Dikirim, OrderDate = DateTime.Now },
            new Order { Id = 14, CustomerName = "Riko", Status = OrderStatus.Selesai, OrderDate = DateTime.Now },
            new Order { Id = 15, CustomerName = "Nina", Status = OrderStatus.Pending, OrderDate = DateTime.Now },
            new Order { Id = 16, CustomerName = "Hadi", Status = OrderStatus.Diproses, OrderDate = DateTime.Now },
            new Order { Id = 17, CustomerName = "Mega", Status = OrderStatus.Dikirim, OrderDate = DateTime.Now },
            new Order { Id = 18, CustomerName = "Ilham", Status = OrderStatus.Selesai, OrderDate = DateTime.Now },
            new Order { Id = 19, CustomerName = "Sari", Status = OrderStatus.Pending, OrderDate = DateTime.Now },
            new Order { Id = 20, CustomerName = "Dian", Status = OrderStatus.Diproses, OrderDate = DateTime.Now },
            new Order { Id = 21, CustomerName = "Bobby", Status = OrderStatus.Dikirim, OrderDate = DateTime.Now },
            new Order { Id = 22, CustomerName = "Gita", Status = OrderStatus.Selesai, OrderDate = DateTime.Now },
            new Order { Id = 23, CustomerName = "Nando", Status = OrderStatus.Pending, OrderDate = DateTime.Now },
            new Order { Id = 24, CustomerName = "Eka", Status = OrderStatus.Diproses, OrderDate = DateTime.Now },

        };

        public List<Order> GetAll() => orders;

        public Order? GetById(int id) => orders.FirstOrDefault(o => o.Id == id);

        public bool UpdateStatus(int id, OrderStatus newStatus)
        {
            var order = GetById(id);
            if (order == null) return false;

            if (!OrderStatusTransition.IsValidTransition(order.Status, newStatus))
                return false;

            order.Status = newStatus;
            return true;
        }
    }
}
