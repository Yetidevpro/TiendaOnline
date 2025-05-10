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
        Task<List<ProductoDTO>> GetAllAsync();
        Task<ProductoDTO> GetByIdAsync(int id);
        Task<bool> CreateAsync(ProductoDTO productoDTO);
        Task<bool> UpdateAsync(int id, ProductoDTO productoDTO);
        Task<bool> DeleteAsync(int id);
    }

}
