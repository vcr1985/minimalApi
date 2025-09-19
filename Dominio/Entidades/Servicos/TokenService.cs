using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Aplicacao.Servicos
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Administrador administrador)
        {
            // 1️⃣ Pegando a chave secreta do appsettings.json
            var chave = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

            // 2️⃣ Criando claims (informações que vão no token)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, administrador.Login),
                new Claim(ClaimTypes.Email, administrador.Email),
                new Claim(ClaimTypes.Role, administrador.Perfil)
            };

            // 3️⃣ Definindo as credenciais de assinatura
            var credenciais = new SigningCredentials(
                new SymmetricSecurityKey(chave),
                SecurityAlgorithms.HmacSha256Signature
            );

            // 4️⃣ Criando o token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // token válido por 2 horas
                signingCredentials: credenciais
            );

            // 5️⃣ Retornando o token como string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
