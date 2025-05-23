﻿using Xunit;
using TiendaOnline.Application.Services;
using TiendaOnline.Application.DTOs;
using TiendaOnline.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaOnline.Testing.ServicesTest;

public class ProductoServiceTests
{
    [Fact]
    public async Task Crear_Obtener_Actualizar_Eliminar_Producto()
    {
        var context = TestHelper.GetInMemoryDbContext("ProductoTestDb");

        // Crear colores y tallas para el producto
        var color = new Color { ColorNombre = "Azul" };
        var talla = new Talla { TallaNombre = "XL" };
        context.Colores.Add(color);
        context.Tallas.Add(talla);
        await context.SaveChangesAsync();

        var service = new ProductoService(context);

        // Crear
        var productoDto = new ProductoDTO
        {
            Precio = 99.99m,
            Descripcion = "Bufanda Azul",
            ColoresIds = new List<int> { color.ColorId },
            TallasIds = new List<int> { talla.TallaId }
        };

        var id = await service.CrearAsync(productoDto);
        Assert.True(id > 0);

        // Obtener
        var producto = await service.ObtenerPorIdAsync(id);
        Assert.Equal("Bufanda Azul", producto.Descripcion);
        Assert.Single(producto.ColoresIds);
        Assert.Single(producto.TallasIds);

        // Actualizar
        producto.Descripcion = "Bufanda verde";
        await service.ActualizarAsync(producto);
        var actualizado = await service.ObtenerPorIdAsync(id);
        Assert.Equal("Bufanda verde", actualizado.Descripcion);

        // Eliminar
        await service.EliminarAsync(id);
        var eliminado = await service.ObtenerPorIdAsync(id);
        Assert.Null(eliminado);
    }
}
