using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Domain.Models;

namespace TiendaOnline.Application.DTOs
{
    public class ProductoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Precio { get; set; }
        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public List<int> ColoresIds { get; set; }

        [Required]
        public List<int> TallasIds { get; set; }
    }

}
