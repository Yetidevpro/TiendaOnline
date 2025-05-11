using Xunit;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.Infrastructure.Data;
using TiendaOnline.Application.Services;
using TiendaOnline.Application.DTOs;
using TiendaOnline.Domain.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace TiendaOnline.Testing.ServicesTest
{
    public class ProductoServiceTests
    {
        private ApplicationDbContext CrearContexto()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Base de datos única por test
                .Options;

            var context = new ApplicationDbContext(options);
            context.Colores.Add(new Color { ColorId = 18, ColorNombre = "Rojo" });
            context.Tallas.Add(new Talla { TallaId = 15, TallaNombre = "S" });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task CrearProducto_ColorYtallaValidos_CreaCorrectamente()
        {
            using var context = CrearContexto();
            var service = new ProductoService(context);

            var dto = new ProductoDTO
            {
                Precio = 19.99m,
                Descripcion = "Body para bebé",
                ColorId = 18,
                TallaId = 15
            };

            var id = await service.CrearAsync(dto);
            Assert.True(id > 0);
        }

        [Fact]
        public async Task CrearProducto_ColorInvalido_LanzaExcepcion()
        {
            using var context = CrearContexto();
            var service = new ProductoService(context);

            var dto = new ProductoDTO
            {
                Precio = 10,
                Descripcion = "Producto",
                ColorId = 99, // inválido
                TallaId = 15
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CrearAsync(dto));
        }

        [Fact]
        public async Task CrearProducto_TallaInvalida_LanzaExcepcion()
        {
            using var context = CrearContexto();
            var service = new ProductoService(context);

            var dto = new ProductoDTO
            {
                Precio = 10,
                Descripcion = "Producto",
                ColorId = 18,
                TallaId = 99 // inválido
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CrearAsync(dto));
        }

        [Fact]
        public async Task ActualizarProducto_ConDatosValidos_ActualizaCorrectamente()
        {
            using var context = CrearContexto();
            var service = new ProductoService(context);

            var id = await service.CrearAsync(new ProductoDTO
            {
                Precio = 15,
                Descripcion = "Producto original",
                ColorId = 18,
                TallaId = 15
            });

            var actualizado = await service.ActualizarAsync(new ProductoDTO
            {
                Id = id,
                Precio = 25,
                Descripcion = "Producto actualizado",
                ColorId = 18,
                TallaId = 15
            });

            var producto = await service.ObtenerPorIdAsync(id);

            Assert.True(actualizado);
            Assert.Equal(25, producto.Precio);
            Assert.Equal("Producto actualizado", producto.Descripcion);
        }

        [Fact]
        public async Task EliminarProducto_Existente_EliminaCorrectamente()
        {
            using var context = CrearContexto();
            var service = new ProductoService(context);

            var id = await service.CrearAsync(new ProductoDTO
            {
                Precio = 10,
                Descripcion = "Producto",
                ColorId = 18,
                TallaId = 15
            });

            var eliminado = await service.EliminarAsync(id);
            var producto = await service.ObtenerPorIdAsync(id);

            Assert.True(eliminado);
            Assert.Null(producto);
        }

        [Fact]
        public async Task ObtenerPorId_Existente_DevuelveProductoCorrecto()
        {
            using var context = CrearContexto();
            var service = new ProductoService(context);

            var id = await service.CrearAsync(new ProductoDTO
            {
                Precio = 10,
                Descripcion = "Producto buscado",
                ColorId = 18,
                TallaId = 15
            });

            var producto = await service.ObtenerPorIdAsync(id);

            Assert.NotNull(producto);
            Assert.Equal("Producto buscado", producto.Descripcion);
        }

        [Fact]
        public async Task ObtenerTodos_DevuelveListaCorrecta()
        {
            using var context = CrearContexto();
            var service = new ProductoService(context);

            await service.CrearAsync(new ProductoDTO
            {
                Precio = 10,
                Descripcion = "Producto A",
                ColorId = 18,
                TallaId = 15
            });

            await service.CrearAsync(new ProductoDTO
            {
                Precio = 20,
                Descripcion = "Producto B",
                ColorId = 18,
                TallaId = 15
            });

            var lista = await service.ObtenerTodosAsync();

            Assert.Equal(2, lista.Count());
        }
    }
}
