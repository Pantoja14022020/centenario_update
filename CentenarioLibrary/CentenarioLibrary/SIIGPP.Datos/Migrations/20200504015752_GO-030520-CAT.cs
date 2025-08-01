using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class GO030520CAT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Agencia",
                table: "CAT_HISTORIAL_CARPETA",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agencia",
                table: "CAT_HISTORIAL_CARPETA");
        }
    }
}
