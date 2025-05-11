using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Infrastructure.Data;
using TiendaOnline.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace TiendaOnline.Application.Services
{
    public class ColorService : IColorService
    {
        private readonly ApplicationDbContext _context;

        public ColorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Color>> ObtenerTodosAsync()
        {
            return await _context.Colores.ToListAsync();
        }
    }
}
