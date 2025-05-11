using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Domain.Models;
using TiendaOnline.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TiendaOnline.Application.Services
{
    public class TallaService : ITallaService
    {
        private readonly ApplicationDbContext _context;

        public TallaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Talla>> ObtenerTodosAsync()
        {
            return await _context.Tallas.ToListAsync();
        }
    }
}
