using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Dominio.DTO;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Infraestrutura.Db;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalApi.Dominio.Entidades.Servicos
{
    public class AdministradorServico : IAdministradorServico
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AdministradorServico(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ===========================
        // LOGIN - gera token JWT
        // ===========================
        public async Task<TokenDTO?> LoginAsync(LoginDTO login)
        {
            if (login is null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Senha))
                return null;

            var admin = await _context.Administradores
                .FirstOrDefaultAsync(a => a.Email.ToLower() == login.Email.ToLower());

            if (admin == null || !BCrypt.Net.BCrypt.Verify(login.Senha, admin.Senha))
                return null;

            var jwtKey = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception("JWT Key não configurada no appsettings.json");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwtKey);
            var dataExpiracao = DateTime.UtcNow.AddHours(1);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim(ClaimTypes.Role, admin.Perfil)
                }),
                Expires = dataExpiracao,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new TokenDTO(tokenString, dataExpiracao);
        }

        // ===========================
        // VALIDA LOGIN - retorna o admin se válido
        // ===========================
        public async Task<Administrador?> ValidarLoginAsync(LoginDTO loginDTO)
        {
            if (loginDTO is null || string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Senha))
                return null;

            var admin = await _context.Administradores
                .FirstOrDefaultAsync(a => a.Email.ToLower() == loginDTO.Email.ToLower());

            if (admin == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Senha, admin.Senha))
                return null;

            return admin;
        }

        // ===========================
        // EXEMPLOS DE OUTROS MÉTODOS
        // ===========================
        public async Task<List<Administrador>> ListarAsync()
        {
            return await _context.Administradores.ToListAsync();
        }

        public async Task<Administrador> CriarAsync(Administrador admin)
        {
            admin.Senha = BCrypt.Net.BCrypt.HashPassword(admin.Senha);
            _context.Administradores.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
    }
}
