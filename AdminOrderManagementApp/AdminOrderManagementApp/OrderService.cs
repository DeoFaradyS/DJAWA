using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminOrderManagementApp
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }

    public class OrderService
    {
        public List<Order> GetAllOrders()
        {
            return new List<Order>
        {
            new Order { Id = 1, Status = "Pending" },
            new Order { Id = 2, Status = "Diproses" }
        };
        }

        public void UpdateOrderStatus(Order order, string newStatus)
        {
            if (order.Status == "Pending" && newStatus == "Diproses")
            {
                order.Status = newStatus;
            }
            else
            {
                throw new InvalidOperationException("Transisi status tidak valid!");
            }
        }
    }

}
