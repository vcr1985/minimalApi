using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

// As definições de namespace e tipo devem vir antes das instruções de nível superior.
namespace MinimalApi
{
  public record LoginDTO(
      string Email,
      string Password
  );

  // As instruções de nível superior devem estar dentro do namespace
  // para acessar os tipos definidos nele, como LoginDTO.
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      var app = builder.Build();

      app.MapGet("/", () => "Hello World!");

      app.MapPost("/login", (LoginDTO loginDTO) =>
      {
        if (loginDTO.Email == "admin@teste.com" && loginDTO.Password == "123456")
        {
          return Results.Ok("Login successful");
        }
        return Results.Unauthorized();
      });

      app.Run();
    }
  }
}