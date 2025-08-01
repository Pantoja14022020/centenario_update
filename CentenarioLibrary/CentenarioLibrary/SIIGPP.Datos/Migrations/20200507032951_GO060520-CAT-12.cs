using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class GO060520CAT12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClasificacionP",
                table: "CAT_AMPDEC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DireccionP",
                table: "CAT_AMPDEC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoP",
                table: "CAT_AMPDEC",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClasificacionP",
                table: "CAT_AMPDEC");

            migrationBuilder.DropColumn(
                name: "DireccionP",
                table: "CAT_AMPDEC");

            migrationBuilder.DropColumn(
                name: "TipoP",
                table: "CAT_AMPDEC");
        }
    }
}
