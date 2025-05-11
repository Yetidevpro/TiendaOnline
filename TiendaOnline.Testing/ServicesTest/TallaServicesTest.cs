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
    public class TallaServiceTests
    {
        private ApplicationDbContext CrearContexto()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            var context = new ApplicationDbContext(options);
            context.Tallas.Add(new Talla { TallaId = 1, TallaNombre = "S" });
            context.Tallas.Add(new Talla { TallaId = 2, TallaNombre = "M" });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaDevolverTallasCorrectamente()
        {
            using var context = CrearContexto();
            var service = new TallaService(context);

            // Act
            var tallas = await service.ObtenerTodosAsync();

            // Assert
            Assert.NotNull(tallas);
            Assert.Equal(2, tallas.Count()); 
            Assert.Contains(tallas, t => t.TallaNombre == "S");
            Assert.Contains(tallas, t => t.TallaNombre == "M");
        }
    }
}
