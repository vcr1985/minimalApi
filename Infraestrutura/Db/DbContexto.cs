using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Infraestrutura.Db;

public class DbContexto : DbContext
{
  public DbContexto(DbContextOptions<DbContexto> options) : base(options)
  {
  }

  // Adicione aqui as propriedades DbSet para suas entidades.
  // Exemplo:
  // public DbSet<NomeDaEntidade> NomeDaTabela { get; set; }
}