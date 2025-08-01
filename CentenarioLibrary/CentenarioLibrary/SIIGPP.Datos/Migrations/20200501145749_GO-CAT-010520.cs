using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class GOCAT010520 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Etiqueta",
                table: "CAT_INDICIOS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Etiqueta",
                table: "CAT_INDICIOS");
        }
    }
}
