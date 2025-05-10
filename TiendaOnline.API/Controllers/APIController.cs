using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaOnline.Application.DTOs;
using TiendaOnline.Application.Services;

namespace TiendaOnline.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoDTO>>> GetAll()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetById(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductoDTO productoDTO)
        {
            var result = await _productoService.CreateAsync(productoDTO);
            if (!result)
                return BadRequest("No se pudo crear el producto");

            return CreatedAtAction(nameof(GetById), new { id = productoDTO.Id }, productoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProductoDTO productoDTO)
        {
            var result = await _productoService.UpdateAsync(id, productoDTO);
            if (!result)
                return NotFound("Producto no encontrado para actualizar");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _productoService.DeleteAsync(id);
            if (!result)
                return NotFound("Producto no encontrado para eliminar");

            return NoContent();
        }
    }
}
