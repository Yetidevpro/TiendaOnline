using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Application.DTOs;
using TiendaOnline.Domain.Models;

namespace TiendaOnline.Application.Services
{
    public interface ITallaService
    {
        Task<IEnumerable<TallaDTO>> ObtenerTodosAsync();
        Task<TallaDTO> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(TallaDTO tallaDto);
        Task<bool> ActualizarAsync(TallaDTO tallaDto);
        Task<bool> EliminarAsync(int id);
    }

}
