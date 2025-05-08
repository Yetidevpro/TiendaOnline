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

            // Configuraciones adicionales si se necesitan
            modelBuilder.Entity<Color>()
                .HasMany(c => c.Productos)
                .WithOne(p => p.Color)
                .HasForeignKey(p => p.ColorId);

            modelBuilder.Entity<Talla>()
                .HasMany(t => t.Productos)
                .WithOne(p => p.Talla)
                .HasForeignKey(p => p.TallaId);
        }
    }
}