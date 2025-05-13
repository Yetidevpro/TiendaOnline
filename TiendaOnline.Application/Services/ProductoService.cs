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
                .Include(p => p.ProductoColores)
                .Include(p => p.ProductoTallas)
                .Select(p => new ProductoDTO
                {
                    Id = p.Id,
                    Precio = p.Precio,
                    Descripcion = p.Descripcion,
                    ColoresIds = p.ProductoColores.Select(pc => pc.ColorId).ToList(),
                    TallasIds = p.ProductoTallas.Select(pt => pt.TallaId).ToList(),
                }).ToListAsync();
        }

        public async Task<ProductoDTO> ObtenerPorIdAsync(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.ProductoColores)
                .Include(p => p.ProductoTallas)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) return null;

            return new ProductoDTO
            {
                Id = producto.Id,
                Precio = producto.Precio,
                Descripcion = producto.Descripcion,
                ColoresIds = producto.ProductoColores.Select(pc => pc.ColorId).ToList(),
                TallasIds = producto.ProductoTallas.Select(pt => pt.TallaId).ToList(),
            };
        }

        public async Task<int> CrearAsync(ProductoDTO productoDto)
        {
            await ValidarColorYTallaAsync(productoDto.ColoresIds, productoDto.TallasIds);

            var producto = new Producto
            {
                Precio = productoDto.Precio,
                Descripcion = productoDto.Descripcion
            };

            producto.ProductoColores = productoDto.ColoresIds
                .Select(id => new ProductoColor { ColorId = id })
                .ToList();

            producto.ProductoTallas = productoDto.TallasIds
                .Select(id => new ProductoTalla { TallaId = id })
                .ToList();

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return producto.Id;
        }

        public async Task<bool> ActualizarAsync(ProductoDTO productoDto)
        {
            var producto = await _context.Productos
                .Include(p => p.ProductoColores)
                .Include(p => p.ProductoTallas)
                .FirstOrDefaultAsync(p => p.Id == productoDto.Id);

            if (producto == null) return false;

            await ValidarColorYTallaAsync(productoDto.ColoresIds, productoDto.TallasIds);

            producto.Precio = productoDto.Precio;
            producto.Descripcion = productoDto.Descripcion;

            // Actualizar colores
            producto.ProductoColores.Clear();
            producto.ProductoColores = productoDto.ColoresIds
                .Select(id => new ProductoColor { ProductoId = producto.Id, ColorId = id })
                .ToList();

            // Actualizar tallas
            producto.ProductoTallas.Clear();
            producto.ProductoTallas = productoDto.TallasIds
                .Select(id => new ProductoTalla { ProductoId = producto.Id, TallaId = id })
                .ToList();

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

        private async Task ValidarColorYTallaAsync(List<int> colorIds, List<int> tallaIds)
        {
            var coloresValidos = await _context.Colores
                .Where(c => colorIds.Contains(c.ColorId))
                .Select(c => c.ColorId)
                .ToListAsync();

            var tallasValidas = await _context.Tallas
                .Where(t => tallaIds.Contains(t.TallaId))
                .Select(t => t.TallaId)
                .ToListAsync();

            if (coloresValidos.Count != colorIds.Count)
                throw new ArgumentException("Uno o más ColorId no son válidos.");

            if (tallasValidas.Count != tallaIds.Count)
                throw new ArgumentException("Uno o más TallaId no son válidos.");
        }

    }
}