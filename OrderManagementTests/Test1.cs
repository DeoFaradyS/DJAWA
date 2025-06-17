using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManagementAPI.Services;
using OrderManagementAPI.Models;

namespace OrderManagementTests
{
    [TestClass]
    public class OrderServiceTests
    {
        private OrderService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new OrderService();
        }

        [TestMethod]
        public void GetAll_ShouldReturnData()
        {
            var result = _service.GetAll();
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void UpdateStatus_ValidTransition_ShouldSucceed()
        {
            var result = _service.UpdateStatus(1, OrderStatus.Diproses);
            Assert.IsTrue(result);
        }
    }
}
