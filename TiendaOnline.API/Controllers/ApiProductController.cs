using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Application.DTOs;
using TiendaOnline.Application.Services;

namespace TiendaOnline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // GET: api/producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetAll()
        {
            var productos = await _productoService.ObtenerTodosAsync();
            return Ok(productos);
        }

        // GET: api/producto/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetById(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        // POST: api/producto
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductoDTO productoDto)
        {
            try
            {
                var newId = await _productoService.CrearAsync(productoDto);
                return CreatedAtAction(nameof(GetById), new { id = newId }, productoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT: api/producto/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProductoDTO productoDto)
        {
            if (id != productoDto.Id)
                return BadRequest(new { error = "El ID de la URL no coincide con el del cuerpo de la solicitud. Tienes que añadir la misma Id tanto en la URL como en el JSON" });

            try
            {
                var actualizado = await _productoService.ActualizarAsync(productoDto);
                if (!actualizado) return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/producto/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _productoService.EliminarAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
