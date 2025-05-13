using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Domain.Models;


namespace TiendaOnline.Application.DTOs
{
    public class ProductoTallaDTO
    {
        [Required]
        public int ProductoId { get; set; }
        [Required]
        public int TallaId { get; set; }
    }
}
