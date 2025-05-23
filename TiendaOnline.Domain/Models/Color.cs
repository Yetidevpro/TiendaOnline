﻿
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Domain.Models
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }

        [Required]
        [StringLength(50)]
        public string ColorNombre { get; set; }

        public ICollection<ProductoColor> ProductoColores { get; set; } = new List<ProductoColor>();
    }

}
