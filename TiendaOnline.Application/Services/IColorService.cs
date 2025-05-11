using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Domain.Models;

namespace TiendaOnline.Application.Services
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> ObtenerTodosAsync();
    }
}
