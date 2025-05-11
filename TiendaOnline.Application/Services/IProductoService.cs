using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Application.DTOs;

namespace TiendaOnline.Application.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTO>> ObtenerTodosAsync();
        Task<ProductoDTO> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(ProductoDTO productoDto);
        Task<bool> ActualizarAsync(ProductoDTO productoDto);
        Task<bool> EliminarAsync(int id);
    }
}