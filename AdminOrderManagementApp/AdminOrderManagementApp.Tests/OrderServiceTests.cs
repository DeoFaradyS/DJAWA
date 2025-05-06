using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdminOrderManagementApp.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void GetAllOrders_ShouldReturnListOfOrders()
        {
            var service = new OrderService();
            var orders = service.GetAllOrders();
            Assert.NotNull(orders);
            Assert.True(orders.Count > 0);
        }

        [Fact]
        public void UpdateOrderStatus_ValidTransition_ShouldUpdateStatus()
        {
            var service = new OrderService();
            var order = new Order { Id = 1, Status = "Pending" };
            service.UpdateOrderStatus(order, "Diproses");
            Assert.Equal("Diproses", order.Status);
        }

        [Fact]
        public void UpdateOrderStatus_InvalidTransition_ShouldThrowException()
        {
            var service = new OrderService();
            var order = new Order { Id = 1, Status = "Pending" };
            Assert.Throws<InvalidOperationException>(() => service.UpdateOrderStatus(order, "Selesai"));
        }
    }
}
