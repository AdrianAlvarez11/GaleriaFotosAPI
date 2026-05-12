using GaleriaFotosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GaleriaFotosAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Foto> Fotos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.NombreUsuario).IsUnique();
            entity.Property(u => u.NombreUsuario).IsRequired().HasMaxLength(50);
            entity.Property(u => u.NombreReal).IsRequired().HasMaxLength(100);
            entity.Property(u => u.PinHash).IsRequired();
        });

        modelBuilder.Entity<Foto>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.HasOne(f => f.Usuario)
                  .WithMany(u => u.Fotos)
                  .HasForeignKey(f => f.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}