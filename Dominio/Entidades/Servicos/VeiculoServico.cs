using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.DTO;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Entidades.Servicos
{
    public class VeiculoServico : IVeiculoServico
    {
        private readonly AppDbContext _dbContext;

        public VeiculoServico(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Veiculo>> ObterVeiculosAsync(int pagina, int tamanhoPagina, string? filtro = null, string? ordenacao = null)
        {
            var query = _dbContext.Veiculos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(v => v.Nome.ToLower().Contains(filtro) || v.Marca.ToLower().Contains(filtro));
            }

            if (!string.IsNullOrWhiteSpace(ordenacao))
            {
                ordenacao = ordenacao.ToLower();
                query = ordenacao switch
                {
                    "nome" => query.OrderBy(v => v.Nome),
                    "marca" => query.OrderBy(v => v.Marca),
                    "ano" => query.OrderBy(v => v.Ano),
                    "preco" => query.OrderBy(v => v.Preco),
                    _ => query
                };
            }

            return await query
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
        }

        public async Task<Veiculo?> ObterVeiculoPorIdAsync(int id)
        {
            return await _dbContext.Veiculos.FindAsync(id);
        }

        public async Task<Veiculo> CriarVeiculoAsync(VeiculoDTO dto)
        {
            var veiculo = new Veiculo
            {
                Nome = dto.Nome,
                Marca = dto.Marca,
                Ano = dto.Ano,
                Cor = dto.Cor,
                Preco = dto.Preco,
                Modelo = dto.Modelo
            };
            _dbContext.Veiculos.Add(veiculo);
            await _dbContext.SaveChangesAsync();
            return veiculo;
        }

        public async Task<Veiculo?> AtualizarVeiculoAsync(int id, VeiculoDTO dto)
        {
            var veiculo = await _dbContext.Veiculos.FindAsync(id);
            if (veiculo == null) return null;

            veiculo.Nome = dto.Nome;
            veiculo.Marca = dto.Marca;
            veiculo.Ano = dto.Ano;
            veiculo.Cor = dto.Cor;
            veiculo.Preco = dto.Preco;
            veiculo.Modelo = dto.Modelo;

            await _dbContext.SaveChangesAsync();
            return veiculo;
        }

        public async Task<bool> DeletarVeiculoAsync(int id)
        {
            var veiculo = await _dbContext.Veiculos.FindAsync(id);
            if (veiculo == null) return false;

            _dbContext.Veiculos.Remove(veiculo);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
