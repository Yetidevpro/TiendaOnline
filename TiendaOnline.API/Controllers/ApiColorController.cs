using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Application.DTOs;
using TiendaOnline.Application.Services;
using TiendaOnline.Domain.Models;

namespace TiendaOnline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColoresController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColoresController(IColorService colorService)
        {
            _colorService = colorService;
        }
        // GET: api/colores
        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _colorService.ObtenerTodosAsync());
        // GET: api/colores/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var color = await _colorService.ObtenerPorIdAsync(id);
            if (color == null) return NotFound();
            return Ok(color);
        }
        // POST: api/colores
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ColorDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = await _colorService.CrearAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, dto);
        }
        // PUT: api/colores/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ColorDTO dto)
        {
            if (id != dto.Id) return BadRequest("IDs no coinciden.");
            var result = await _colorService.ActualizarAsync(dto);
            if (!result) return NotFound();
            return NoContent();
        }
        // DELETE: api/colores/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _colorService.EliminarAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
