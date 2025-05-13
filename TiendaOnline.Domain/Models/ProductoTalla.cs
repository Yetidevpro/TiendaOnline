using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOnline.Domain.Models
{
    public class ProductoTalla
    {
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int TallaId { get; set; }
        public Talla Talla { get; set; }
    }
}
