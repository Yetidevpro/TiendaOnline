using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Domain.Models
{
    public class ProductoTalla
    {
        [Key]
        public int ProductoId { get; set; }
        [Required]
        public Producto Producto { get; set; }

        [Required]
        public int TallaId { get; set; }
        [Required]
        public Talla Talla { get; set; }
    }
}
