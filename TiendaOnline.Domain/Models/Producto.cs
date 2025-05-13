
using System.ComponentModel.DataAnnotations;


namespace TiendaOnline.Domain.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Precio { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        public ICollection<ProductoColor> ProductoColores { get; set; } = new List<ProductoColor>();
        public ICollection<ProductoTalla> ProductoTallas { get; set; } = new List<ProductoTalla>();
    }


}

