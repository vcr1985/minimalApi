using MinimalApi.Dominio.DTO;
using MinimalApi.Dominio.Entidades;
using System.Threading.Tasks;

namespace MinimalApi.Dominio.Interfaces
{
    public interface IAdministradorServico
    {
        Task<TokenDTO?> LoginAsync(LoginDTO login);

        Task<Administrador?> ValidarLoginAsync(LoginDTO loginDTO);
    }
}
