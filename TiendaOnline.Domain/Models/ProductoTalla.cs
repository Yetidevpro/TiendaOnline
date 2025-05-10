using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOnline.Domain.Models
{
    public class ProductoTalla
    {
        [Key]
        public int ProductoTallaId { get; set; }

        public int ProductoId { get; set; }
        public int TallaId { get; set; }

        public Producto Producto { get; set; }
        public Talla Talla { get; set; }
    }
}
