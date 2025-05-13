using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Application.DTOs;
using TiendaOnline.Application.Services;
using TiendaOnline.Domain.Models;

namespace TiendaOnline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TallasController : ControllerBase
    {
        private readonly ITallaService _tallaService;

        public TallasController(ITallaService tallaService)
        {
            _tallaService = tallaService;
        }
        // GET: api/tallas

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _tallaService.ObtenerTodosAsync());

        // GET: api/tallas/{id}

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var talla = await _tallaService.ObtenerPorIdAsync(id);
            if (talla == null) return NotFound();
            return Ok(talla);
        }

        // POST: api/tallas

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TallaDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = await _tallaService.CrearAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, dto);
        }

        // PUT: api/tallas/{id}

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TallaDTO dto)
        {
            if (id != dto.Id) return BadRequest("IDs no coinciden.");
            var result = await _tallaService.ActualizarAsync(dto);
            if (!result) return NotFound();
            return NoContent();
        }
        // DELETE: api/tallas/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tallaService.EliminarAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
