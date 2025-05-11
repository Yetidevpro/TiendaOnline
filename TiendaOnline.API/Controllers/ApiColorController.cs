using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Application.Services;
using TiendaOnline.Domain.Models;

namespace TiendaOnline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        //GET: api/color
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetAll()
        {
            var colores = await _colorService.ObtenerTodosAsync();
            return Ok(colores);
        }
    }
}