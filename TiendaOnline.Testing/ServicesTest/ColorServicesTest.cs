using Xunit;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.Infrastructure.Data;
using TiendaOnline.Application.Services;
using TiendaOnline.Domain.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TiendaOnline.Testing.ServicesTest
{
    public class ColorServiceTests
    {
        private ApplicationDbContext CrearContexto()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            var context = new ApplicationDbContext(options);
            context.Colores.Add(new Color { ColorId = 1, ColorNombre = "Rojo" });
            context.Colores.Add(new Color { ColorId = 2, ColorNombre = "Azul" });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaDevolverColoresCorrectamente()
        {
            using var context = CrearContexto();
            var service = new ColorService(context);

            // Act
            var colores = await service.ObtenerTodosAsync();

            // Assert
            Assert.NotNull(colores);
            Assert.Equal(2, colores.Count()); 
            Assert.Contains(colores, c => c.ColorNombre == "Rojo");
            Assert.Contains(colores, c => c.ColorNombre == "Azul");
        }
    }
}
