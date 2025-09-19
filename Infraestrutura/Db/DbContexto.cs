using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Entidades.Servicos; // Para Veiculo

namespace MinimalApi.Infraestrutura.Db
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Administrador> Administradores { get; set; } = default!;
    public DbSet<Veiculo> Veiculos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Administrador>().ToTable("Administradores").HasData(
          new Administrador
          {
            Id = 1,
            Email = "admin@teste.com",
            Senha = "admin123",
            Perfil = "Admin"
          });
    }
  }
}
