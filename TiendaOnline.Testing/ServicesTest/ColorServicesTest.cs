using Xunit;
using TiendaOnline.Application.Services;
using TiendaOnline.Application.DTOs;
using System.Threading.Tasks;
using System.Linq;
using TiendaOnline.Testing.ServicesTest;

public class ColorServiceTests
{
    [Fact]
    public async Task Crear_Obtener_Actualizar_Eliminar_Color()
    {
        var context = TestHelper.GetInMemoryDbContext("ColorTestDb");
        var service = new ColorService(context);

        // Crear
        var colorId = await service.CrearAsync(new ColorDTO { Nombre = "Rojo" });
        Assert.True(colorId > 0);

        // Obtener por ID
        var color = await service.ObtenerPorIdAsync(colorId);
        Assert.NotNull(color);
        Assert.Equal("Rojo", color.Nombre);

        // Actualizar
        color.Nombre = "Verde";
        var actualizado = await service.ActualizarAsync(color);
        Assert.True(actualizado);

        var actualizadoColor = await service.ObtenerPorIdAsync(colorId);
        Assert.Equal("Verde", actualizadoColor.Nombre);

        // Eliminar
        var eliminado = await service.EliminarAsync(colorId);
        Assert.True(eliminado);

        //Obtener por ID
        var borrado = await service.ObtenerPorIdAsync(colorId);
        Assert.Null(borrado);
    }
}
