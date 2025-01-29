using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOI.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar Tipos de Activos
            migrationBuilder.InsertData(
                table: "TiposActivos",
                columns: new[] { "TipoActivoId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Acción" },
                    { 2, "Bono" },
                    { 3, "FCI" }
                });

            // Insertar Estados
            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "EstadoId", "Descripcion" },
                values: new object[,]
                {
                    { 0, "En proceso" },
                    { 1, "Ejecutada" },
                    { 3, "Cancelada" }
                });

            // Insertar Activos
            migrationBuilder.InsertData(
                table: "Activos",
                columns: new[] { "ActivoId", "Ticker", "Nombre", "TipoActivoId", "PrecioUnitario" },
                values: new object[,]
                {
                    { 1, "AAPL", "Apple", 1, 177.97m },
                    { 2, "GOOGL", "Alphabet Inc", 1, 138.21m },
                    { 3, "MSFT", "Microsoft", 1, 329.04m },
                    { 4, "KO", "Coca Cola", 1, 58.3m },
                    { 5, "WMT", "Walmart", 1, 163.42m },
                    { 6, "AL30", "BONOS ARGENTINA USD 2030 L.A", 2, 307.4m },
                    { 7, "GD30", "Bonos Globales Argentina USD Step Up 2030", 2, 336.1m },
                    { 8, "Delta.Pesos", "Delta Pesos Clase A", 3, 0.0181m },
                    { 9, "Fima.Premium", "Fima Premium Clase A", 3, 0.0317m }
                });
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "TiposActivos", keyColumn: "TipoActivoId", keyValue: 1);
            migrationBuilder.DeleteData(table: "TiposActivos", keyColumn: "TipoActivoId", keyValue: 2);
            migrationBuilder.DeleteData(table: "TiposActivos", keyColumn: "TipoActivoId", keyValue: 3);

            migrationBuilder.DeleteData(table: "Estados", keyColumn: "EstadoId", keyValue: 0);
            migrationBuilder.DeleteData(table: "Estados", keyColumn: "EstadoId", keyValue: 1);
            migrationBuilder.DeleteData(table: "Estados", keyColumn: "EstadoId", keyValue: 3);

            migrationBuilder.DeleteData(table: "Activos", keyColumn: "ActivoId", keyValue: 1);
            migrationBuilder.DeleteData(table: "Activos", keyColumn: "ActivoId", keyValue: 2);
            migrationBuilder.DeleteData(table: "Activos", keyColumn: "ActivoId", keyValue: 3);
            migrationBuilder.DeleteData(table: "Activos", keyColumn: "ActivoId", keyValue: 4);
            migrationBuilder.DeleteData(table: "Activos", keyColumn: "ActivoId", keyValue: 5);
            migrationBuilder.DeleteData(table: "Activos", keyColumn: "ActivoId", keyValue: 6);
            migrationBuilder.DeleteData(table: "Activos", keyColumn: "ActivoId", keyValue: 7);
            migrationBuilder.DeleteData(table: "Activos", keyColumn: "ActivoId", keyValue: 8);
            migrationBuilder.DeleteData(table: "Activos", keyColumn: "ActivoId", keyValue: 9);
        }

    }
}
