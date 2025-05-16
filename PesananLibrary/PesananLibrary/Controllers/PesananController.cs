using Microsoft.AspNetCore.Mvc;
using TransaksiAPI.Models;
using TransaksiAPI.Services;

namespace TransaksiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PesananController : ControllerBase
    {
        private readonly PesananService _service;

        public PesananController(PesananService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pesanan = _service.GetById(id);
            return pesanan == null ? NotFound() : Ok(pesanan);
        }

        [HttpPost]
        public IActionResult Tambah(Pesanan pesanan)
        {
            var data = _service.Tambah(pesanan);
            return CreatedAtAction(nameof(GetById), new { id = data.Id }, data);
        }

        //[HttpPut("{id}")]
        //public IActionResult Ubah(int id, Pesanan pesanan)
        //{
        //    var result = _service.Ubah(id, pesanan);
        //    return result ? Ok("Pesanan diubah") : NotFound();
        //}

        [HttpPatch("{id}/konfirmasi")]
        public IActionResult Konfirmasi(int id)
        {
            var result = _service.Konfirmasi(id);
            return result ? Ok("Pesanan dikonfirmasi") : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Hapus(int id)
        {
            var result = _service.Hapus(id);
            return result ? Ok("Pesanan dihapus") : NotFound();
        }
    }
}
