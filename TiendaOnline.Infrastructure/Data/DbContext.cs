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
        public DbSet<ProductoColor> ProductoColores { get; set; }
        public DbSet<ProductoTalla> ProductoTallas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Producto>()
              .Property(p => p.Precio)
              .HasPrecision(18, 2);


            modelBuilder.Entity<ProductoColor>()
                .HasOne(pc => pc.Producto)
                .WithMany(p => p.ProductoColores)
                .HasForeignKey(pc => pc.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ProductoColor>()
                .HasOne(pc => pc.Color)
                .WithMany(c => c.ProductoColores)
                .HasForeignKey(pc => pc.ColorId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<ProductoTalla>()
                .HasOne(pt => pt.Producto)
                .WithMany(p => p.ProductoTallas)
                .HasForeignKey(pt => pt.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ProductoTalla>()
                .HasOne(pt => pt.Talla)
                .WithMany(t => t.ProductoTallas)
                .HasForeignKey(pt => pt.TallaId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}