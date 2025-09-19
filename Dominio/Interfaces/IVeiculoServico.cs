using MinimalApi.Dominio.DTO;
using MinimalApi.Dominio.Entidades.Servicos;

namespace MinimalApi.Dominio.Interfaces
{
  public interface IVeiculoServico
  {
    Task<IEnumerable<Veiculo>> ObterVeiculosAsync(int pagina, int tamanhoPagina, string? filtro = null, string? ordenacao = null);
    Task<Veiculo?> ObterVeiculoPorIdAsync(int id);
    Task<Veiculo> CriarVeiculoAsync(VeiculoDTO dto);
    Task<Veiculo?> AtualizarVeiculoAsync(int id, VeiculoDTO dto);
    Task<bool> DeletarVeiculoAsync(int id);
  }
}
