using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace minimal_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Administradores",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "Administradores",
                columns: new[] { "Id", "Email", "Login", "Nome", "Perfil", "Senha" },
                values: new object[] { 1, "administrador@teste.com", "", "", "Admin", "admin123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Administradores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Administradores",
                columns: new[] { "Id", "Email", "Login", "Nome", "Perfil", "Senha" },
                values: new object[] { -1, "administrador@teste.com", "", "", "Admin", "admin123" });
        }
    }
}
