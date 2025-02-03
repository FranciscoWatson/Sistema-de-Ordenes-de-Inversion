using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOI.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregarContraseniaCuenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Cuentas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Cuentas");
        }
    }
}
