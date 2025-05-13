using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Infrastructure.Data;
using TiendaOnline.Domain.Models;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.Application.DTOs;

namespace TiendaOnline.Application.Services
{
    public class ColorService : IColorService
    {
        private readonly ApplicationDbContext _context;

        public ColorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ColorDTO>> ObtenerTodosAsync()
        {
            return await _context.Colores
                .Select(c => new ColorDTO { Id = c.ColorId, Nombre = c.ColorNombre })
                .ToListAsync();
        }
        public async Task<ColorDTO> ObtenerPorIdAsync(int id)
        {
            var color = await _context.Colores.FindAsync(id);
            if (color == null) return null;

            return new ColorDTO { Id = color.ColorId, Nombre = color.ColorNombre };
        }

        public async Task<int> CrearAsync(ColorDTO dto)
        {
            var color = new Color { ColorNombre = dto.Nombre };
            _context.Colores.Add(color);
            await _context.SaveChangesAsync();
            return color.ColorId;
        }

        public async Task<bool> ActualizarAsync(ColorDTO dto)
        {
            var color = await _context.Colores.FindAsync(dto.Id);
            if (color == null) return false;

            color.ColorNombre = dto.Nombre;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var color = await _context.Colores.FindAsync(id);
            if (color == null) return false;

            _context.Colores.Remove(color);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}