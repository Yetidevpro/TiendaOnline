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

        public async Task<List<ProductoDTO>> GetAllAsync()
        {
            var productos = await _context.Productos
                .Include(p => p.ProductoColores)
                .Include(p => p.ProductoTallas)
                .ToListAsync();

            return productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Precio = p.Precio,
                Descripcion = p.Descripcion,
                Colores = p.ProductoColores.Select(pc => pc.ColorId).ToList(),
                Tallas = p.ProductoTallas.Select(pt => pt.TallaId).ToList()
            }).ToList();
        }

        public async Task<ProductoDTO> GetByIdAsync(int id)
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
                Colores = producto.ProductoColores.Select(pc => pc.ColorId).ToList(),
                Tallas = producto.ProductoTallas.Select(pt => pt.TallaId).ToList()
            };
        }

        public async Task<bool> CreateAsync(ProductoDTO productoDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var producto = new Producto
            {
                Precio = productoDTO.Precio,
                Descripcion = productoDTO.Descripcion
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            foreach (var colorId in productoDTO.Colores)
            {
                _context.ProductoColores.Add(new ProductoColor { ProductoId = producto.Id, ColorId = colorId });
            }

            foreach (var tallaId in productoDTO.Tallas)
            {
                _context.ProductoTallas.Add(new ProductoTalla { ProductoId = producto.Id, TallaId = tallaId });
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, ProductoDTO productoDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var producto = await _context.Productos
                .Include(p => p.ProductoColores)
                .Include(p => p.ProductoTallas)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) return false;

            producto.Precio = productoDTO.Precio;
            producto.Descripcion = productoDTO.Descripcion;

            
            _context.ProductoColores.RemoveRange(producto.ProductoColores);
            foreach (var colorId in productoDTO.Colores)
            {
                _context.ProductoColores.Add(new ProductoColor { ProductoId = id, ColorId = colorId });
            }

            
            _context.ProductoTallas.RemoveRange(producto.ProductoTallas);
            foreach (var tallaId in productoDTO.Tallas)
            {
                _context.ProductoTallas.Add(new ProductoTalla { ProductoId = id, TallaId = tallaId });
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var producto = await _context.Productos
                .Include(p => p.ProductoColores)
                .Include(p => p.ProductoTallas)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) return false;

            _context.ProductoColores.RemoveRange(producto.ProductoColores);
            _context.ProductoTallas.RemoveRange(producto.ProductoTallas);
            _context.Productos.Remove(producto);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }

    }

}
