using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Application.DTOs
{
    public class ProductoColorDTO
    {
        [Required]
        public int ProductoId { get; set; }
        [Required]
        [StringLength(50)]
        public int ColorId { get; set; }
    }
}
