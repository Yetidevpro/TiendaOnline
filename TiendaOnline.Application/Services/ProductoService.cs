using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Application.DTOs;
using TiendaOnline.Domain.Models;
using TiendaOnline.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;



namespace TiendaOnline.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly ApplicationDbContext _context;

        public ProductoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoDTO>> ObtenerTodosAsync()
        {
            return await _context.Productos
                .Include(p => p.Color)
                .Include(p => p.Talla)
                .Select(p => new ProductoDTO
                {
                    Id = p.Id,
                    Precio = p.Precio,
                    Descripcion = p.Descripcion,
                    ColorId = p.ColorId,
                    ColorNombre = p.Color.ColorNombre,
                    TallaId = p.TallaId,
                    TallaNombre = p.Talla.TallaNombre
                }).ToListAsync();
        }

        public async Task<ProductoDTO> ObtenerPorIdAsync(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Color)
                .Include(p => p.Talla)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) return null;

            return new ProductoDTO
            {
                Id = producto.Id,
                Precio = producto.Precio,
                Descripcion = producto.Descripcion,
                ColorId = producto.ColorId,
                ColorNombre = producto.Color.ColorNombre,
                TallaId = producto.TallaId,
                TallaNombre = producto.Talla.TallaNombre
            };
        }

        public async Task<int> CrearAsync(ProductoDTO productoDto)
        {
            await ValidarColorYTallaAsync(productoDto.ColorId, productoDto.TallaId);

            var producto = new Producto
            {
                Precio = productoDto.Precio,
                Descripcion = productoDto.Descripcion,
                ColorId = productoDto.ColorId,
                TallaId = productoDto.TallaId
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return producto.Id;
        }

        public async Task<bool> ActualizarAsync(ProductoDTO productoDto)
        {
            var producto = await _context.Productos.FindAsync(productoDto.Id);

            if (producto == null) return false;

            await ValidarColorYTallaAsync(productoDto.ColorId, productoDto.TallaId);

            producto.Precio = productoDto.Precio;
            producto.Descripcion = productoDto.Descripcion;
            producto.ColorId = productoDto.ColorId;
            producto.TallaId = productoDto.TallaId;

            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return false;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task ValidarColorYTallaAsync(int colorId, int tallaId)
        {
            var colorExiste = await _context.Colores.AnyAsync(c => c.ColorId == colorId);
            if (!colorExiste)
                throw new ArgumentException("ColorId no válido.");

            var tallaExiste = await _context.Tallas.AnyAsync(t => t.TallaId == tallaId);
            if (!tallaExiste)
                throw new ArgumentException("TallaId no válido.");
        }

    }
}