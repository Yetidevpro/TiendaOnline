using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Application.DTOs;
using TiendaOnline.Domain.Models;

namespace TiendaOnline.Application.Services
{
    public interface IColorService
    {
        Task<IEnumerable<ColorDTO>> ObtenerTodosAsync();
        Task<ColorDTO> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(ColorDTO colorDto);
        Task<bool> ActualizarAsync(ColorDTO colorDto);
        Task<bool> EliminarAsync(int id);
    }
}
