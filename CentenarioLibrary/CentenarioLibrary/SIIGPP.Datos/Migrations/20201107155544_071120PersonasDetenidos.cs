using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _071120PersonasDetenidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "TipoServicio",
                table: "CAT_VEHICULO",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CumpleRequisitoLey",
                table: "CAT_PERSONA",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DecretoLibertad",
                table: "CAT_PERSONA",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DispusoLibertad",
                table: "CAT_PERSONA",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InicioDetenido",
                table: "CAT_PERSONA",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "TipoServicio",
                table: "CAT_VEHICULO");

            migrationBuilder.DropColumn(
                name: "CumpleRequisitoLey",
                table: "CAT_PERSONA");

            migrationBuilder.DropColumn(
                name: "DecretoLibertad",
                table: "CAT_PERSONA");

            migrationBuilder.DropColumn(
                name: "DispusoLibertad",
                table: "CAT_PERSONA");

            migrationBuilder.DropColumn(
                name: "InicioDetenido",
                table: "CAT_PERSONA");
        }
    }
}
