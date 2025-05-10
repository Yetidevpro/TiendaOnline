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

        public ICollection<ProductoTalla> ProductoTallas { get; set; } = new List<ProductoTalla>();
    }

}
