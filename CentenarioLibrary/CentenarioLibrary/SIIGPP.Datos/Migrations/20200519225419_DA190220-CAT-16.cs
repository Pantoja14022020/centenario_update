using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class DA190220CAT16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "ARCHIVOS_VEHICULOS");

            migrationBuilder.DropColumn(
                name: "NombreDocumento",
                table: "ARCHIVOS_VEHICULOS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fecha",
                table: "ARCHIVOS_VEHICULOS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreDocumento",
                table: "ARCHIVOS_VEHICULOS",
                nullable: true);
        }
    }
}
