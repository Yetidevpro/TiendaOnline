
using System.ComponentModel.DataAnnotations;


namespace TiendaOnline.Domain.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TallaId { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Precio { get; set; }
        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        public Talla Talla { get; set; }
        public Color Color { get; set; }
    }
}
