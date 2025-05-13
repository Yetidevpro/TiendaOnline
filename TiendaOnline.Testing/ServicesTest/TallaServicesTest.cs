using Xunit;
using TiendaOnline.Application.Services;
using TiendaOnline.Application.DTOs;
using System.Threading.Tasks;
using TiendaOnline.Testing.ServicesTest;

public class TallaServiceTests
{
    [Fact]
    public async Task Crear_Obtener_Actualizar_Eliminar_Talla()
    {
        var context = TestHelper.GetInMemoryDbContext("TallaTestDb");
        var service = new TallaService(context);

        var id = await service.CrearAsync(new TallaDTO { Nombre = "M" });
        var talla = await service.ObtenerPorIdAsync(id);
        Assert.Equal("M", talla.Nombre);

        talla.Nombre = "L";
        await service.ActualizarAsync(talla);
        var tallaActualizada = await service.ObtenerPorIdAsync(id);
        Assert.Equal("L", tallaActualizada.Nombre);

        await service.EliminarAsync(id);
        var eliminada = await service.ObtenerPorIdAsync(id);
        Assert.Null(eliminada);
    }
}
