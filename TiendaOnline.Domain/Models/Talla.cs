using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Domain.Models
{
    public class Talla
    {
        [Key]
        public int TallaId { get; set; }
        [Required]
        [StringLength(10)]
        public string TallaNombre { get; set; }

        // Relación con Producto
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();

        public Talla()
        {
            Productos = new List<Producto>();
        }
    }
}
