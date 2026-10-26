using Microsoft.EntityFrameworkCore;
using PlataformadeJogos.Models;

namespace PlataformadeJogos.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Jogo> Jogos => Set<Jogo>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Estudio> Estudios => Set<Estudio>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Categoria>(e =>
        {
            e.HasIndex(x => x.Nome).IsUnique();
            e.Property(x => x.Nome).HasMaxLength(80).IsRequired();
        });
        b.Entity<Estudio>(e =>
        {
            e.HasIndex(x => x.Nome).IsUnique();
            e.Property(x => x.Nome).HasMaxLength(120).IsRequired();
        });
        b.Entity<Usuario>(e =>
        {
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Email).HasMaxLength(254).IsRequired();
        });
        b.Entity<Jogo>(e =>
        {
            e.Property(x => x.Nome).HasMaxLength(100).IsRequired();
            // Restrict: impede apagar categoria/estúdio que ainda possuam jogos
            e.HasOne(x => x.Categoria).WithMany(c => c.Jogos)
                .HasForeignKey(x => x.CategoriaId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Estudio).WithMany(s => s.Jogos)
                .HasForeignKey(x => x.EstudioId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
