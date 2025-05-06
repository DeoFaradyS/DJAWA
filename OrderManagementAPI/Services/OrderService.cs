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
            new Order { Id = 2, CustomerName = "Deo", Status = OrderStatus.Diproses, OrderDate = DateTime.Now }
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
