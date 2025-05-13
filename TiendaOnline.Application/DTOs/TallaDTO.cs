using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOnline.Application.DTOs
{
    public class TallaDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }
    }
}
