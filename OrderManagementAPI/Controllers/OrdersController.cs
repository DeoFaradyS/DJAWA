using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Models;
using OrderManagementAPI.Services;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service = new();

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _service.GetById(id);
            return order == null ? NotFound() : Ok(order);
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] string statusStr)
        {
            if (!Enum.TryParse<OrderStatus>(statusStr, true, out var newStatus))
                return BadRequest("Status tidak valid.");

            var result = _service.UpdateStatus(id, newStatus);
            return result ? Ok("Status berhasil diperbarui.") : BadRequest("Transisi status tidak valid.");
        }
    }
}
