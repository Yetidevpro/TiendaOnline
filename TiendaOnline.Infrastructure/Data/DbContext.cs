using Microsoft.EntityFrameworkCore;
using TiendaOnline.Domain.Models;

namespace TiendaOnline.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<Talla> Tallas { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Producto>()
              .Property(p => p.Precio)
              .HasPrecision(18, 2);


            // Relaciones entre Producto -> Color
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Color)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.ColorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relaciones entre Producto -> Talla
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Talla)
                .WithMany(t => t.Productos)
                .HasForeignKey(p => p.TallaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Datos iniciales - Colores
            modelBuilder.Entity<Color>().HasData(
                new Color { ColorId = 1, ColorNombre = "Amarillo" },
                new Color { ColorId = 2, ColorNombre = "Azul" },
                new Color { ColorId = 3, ColorNombre = "Azul Marino" },
                new Color { ColorId = 4, ColorNombre = "Beige" },
                new Color { ColorId = 5, ColorNombre = "Blanco" },
                new Color { ColorId = 6, ColorNombre = "Caqui" },
                new Color { ColorId = 7, ColorNombre = "Celeste" },
                new Color { ColorId = 8, ColorNombre = "Coral" },
                new Color { ColorId = 9, ColorNombre = "Dorado" },
                new Color { ColorId = 10, ColorNombre = "Fucsia" },
                new Color { ColorId = 11, ColorNombre = "Gris" },
                new Color { ColorId = 12, ColorNombre = "Marrón" },
                new Color { ColorId = 13, ColorNombre = "Morado" },
                new Color { ColorId = 14, ColorNombre = "Naranja" },
                new Color { ColorId = 15, ColorNombre = "Negro" },
                new Color { ColorId = 16, ColorNombre = "Plateado" },
                new Color { ColorId = 17, ColorNombre = "Rosa" },
                new Color { ColorId = 18, ColorNombre = "Rojo" },
                new Color { ColorId = 19, ColorNombre = "Turquesa" },
                new Color { ColorId = 20, ColorNombre = "Verde" }
            );

            // Datos iniciales - Tallas
            modelBuilder.Entity<Talla>().HasData(
                 
                 new Talla { TallaId = 1, TallaNombre = "0-3 meses" },
                 new Talla { TallaId = 2, TallaNombre = "3-6 meses" },
                 new Talla { TallaId = 3, TallaNombre = "6-9 meses" },
                 new Talla { TallaId = 4, TallaNombre = "9-12 meses" },
                 new Talla { TallaId = 5, TallaNombre = "12-18 meses" },

                 
                 new Talla { TallaId = 6, TallaNombre = "2 años" },
                 new Talla { TallaId = 7, TallaNombre = "3 años" },
                 new Talla { TallaId = 8, TallaNombre = "4 años" },
                 new Talla { TallaId = 9, TallaNombre = "5 años" },
                 new Talla { TallaId = 10, TallaNombre = "6 años" },
                 new Talla { TallaId = 11, TallaNombre = "8 años" },
                 new Talla { TallaId = 12, TallaNombre = "10 años" },
                 new Talla { TallaId = 13, TallaNombre = "12 años" },
                 new Talla { TallaId = 14, TallaNombre = "14 años" },

                 
                 new Talla { TallaId = 15, TallaNombre = "S" },
                 new Talla { TallaId = 16, TallaNombre = "M" },
                 new Talla { TallaId = 17, TallaNombre = "L" },
                 new Talla { TallaId = 18, TallaNombre = "XL" },
                 new Talla { TallaId = 19, TallaNombre = "XXL" },
                 new Talla { TallaId = 19, TallaNombre = "3XL" }
            );
        }

    }
}