using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOnline.Domain.Models
{
    public class ProductoColor
    {
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
