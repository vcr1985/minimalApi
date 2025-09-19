using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.DTO;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Entidades.Servicos;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Endpoints;

public static class ApiEndpoints
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        // Login - gera token
        app.MapPost("/administradores/login", async (LoginDTO login, IAdministradorServico servico) =>
        {
            var token = await servico.LoginAsync(login);
            return token is null ? Results.Unauthorized() : Results.Ok(token);
        });

        // Seed de administrador
        app.MapPost("/administradores/seed", async (AppDbContext db) =>
        {
            var admin = await db.Administradores.FirstOrDefaultAsync(a => a.Email == "admin@teste.com");
            if (admin != null)
            {
                admin.Senha = BCrypt.Net.BCrypt.HashPassword("admin1234");
                await db.SaveChangesAsync();
                return Results.Ok(new { message = "Administrador atualizado com sucesso." });
            }

            admin = new Administrador
            {
                Email = "admin@teste.com",
                Senha = BCrypt.Net.BCrypt.HashPassword("admin1234"),
                Perfil = "Admin",
                Login = "Admin",
                Nome = "Administrador Padrão"
            };
            db.Administradores.Add(admin);
            await db.SaveChangesAsync();
            return Results.Ok(new { message = "Administrador criado com sucesso." });
        }).RequireAuthorization();

        // Endpoints de veículos protegidos
        app.MapGet("/veiculos", GetVeiculos).RequireAuthorization();
        app.MapPost("/veiculos", PostVeiculo).RequireAuthorization();
        app.MapPut("/veiculos/{id}", PutVeiculo).RequireAuthorization();
        app.MapDelete("/veiculos/{id}", DeleteVeiculo).RequireAuthorization();
    }

    private static readonly List<VeiculoDTO> veiculos = new();

    private static IResult GetVeiculos() => Results.Ok(veiculos);

    private static IResult PostVeiculo(VeiculoDTO veiculo)
    {
        veiculo.Id = veiculos.Count + 1;
        veiculos.Add(veiculo);
        return Results.Created($"/veiculos/{veiculo.Id}", veiculo);
    }

    private static IResult PutVeiculo(int id, VeiculoDTO veiculo)
    {
        var existente = veiculos.FirstOrDefault(v => v.Id == id);
        if (existente == null) return Results.NotFound();

        existente.Nome = veiculo.Nome;
        existente.Marca = veiculo.Marca;
        existente.Modelo = veiculo.Modelo;
        existente.Ano = veiculo.Ano;
        existente.Cor = veiculo.Cor;
        existente.Preco = veiculo.Preco;

        return Results.Ok(existente);
    }

    private static IResult DeleteVeiculo(int id)
    {
        var existente = veiculos.FirstOrDefault(v => v.Id == id);
        if (existente == null) return Results.NotFound();

        veiculos.Remove(existente);
        return Results.NoContent();
    }
}
