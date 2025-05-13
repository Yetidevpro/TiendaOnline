using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Domain.Models;
using TiendaOnline.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.Application.DTOs;

namespace TiendaOnline.Application.Services
{
    public class TallaService : ITallaService
    {
        private readonly ApplicationDbContext _context;

        public TallaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TallaDTO>> ObtenerTodosAsync()
        {
            return await _context.Tallas
                .Select(t => new TallaDTO { Id = t.TallaId, Nombre = t.TallaNombre })
                .ToListAsync();
        }

        public async Task<TallaDTO> ObtenerPorIdAsync(int id)
        {
            var talla = await _context.Tallas.FindAsync(id);
            if (talla == null) return null;

            return new TallaDTO { Id = talla.TallaId, Nombre = talla.TallaNombre };
        }

        public async Task<int> CrearAsync(TallaDTO dto)
        {
            var talla = new Talla { TallaNombre = dto.Nombre };
            _context.Tallas.Add(talla);
            await _context.SaveChangesAsync();
            return talla.TallaId;
        }

        public async Task<bool> ActualizarAsync(TallaDTO dto)
        {
            var talla = await _context.Tallas.FindAsync(dto.Id);
            if (talla == null) return false;

            talla.TallaNombre = dto.Nombre;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var talla = await _context.Tallas.FindAsync(id);
            if (talla == null) return false;

            _context.Tallas.Remove(talla);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
