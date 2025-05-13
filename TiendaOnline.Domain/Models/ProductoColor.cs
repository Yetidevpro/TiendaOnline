using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOnline.Domain.Models
{
    public class ProductoColor
    {
        [Required]
        public int ProductoId { get; set; }
        [Required]
        public Producto Producto { get; set; }
        [Required]
        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
