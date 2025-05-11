using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Application.Services;
using TiendaOnline.Domain.Models;

namespace TiendaOnline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TallaController : ControllerBase
    {
        private readonly ITallaService _tallaService;

        public TallaController(ITallaService tallaService)
        {
            _tallaService = tallaService;
        }

        // GET: api/talla
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Talla>>> GetAll()
        {
            var tallas = await _tallaService.ObtenerTodosAsync();
            return Ok(tallas);
        }
    }
}
