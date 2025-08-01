using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _160720CAT26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Texto1RD",
                table: "CAT_PROCEDIMIENTOABREVIADO",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Texto2RD",
                table: "CAT_PROCEDIMIENTOABREVIADO",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Texto1RD",
                table: "CAT_PROCEDIMIENTOABREVIADO");

            migrationBuilder.DropColumn(
                name: "Texto2RD",
                table: "CAT_PROCEDIMIENTOABREVIADO");
        }
    }
}
