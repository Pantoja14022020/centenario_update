using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class DA180520CAT14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NsChasis",
                table: "CAT_VEHICULO",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NsMotor",
                table: "CAT_VEHICULO",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StatusVehiculo",
                table: "CAT_VEHICULO",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Ubicacion",
                table: "CAT_VEHICULO",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NDenunciaOficio",
                table: "CAT_RHECHO",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hipotesis",
                table: "CAT_RDH",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NsChasis",
                table: "CAT_VEHICULO");

            migrationBuilder.DropColumn(
                name: "NsMotor",
                table: "CAT_VEHICULO");

            migrationBuilder.DropColumn(
                name: "StatusVehiculo",
                table: "CAT_VEHICULO");

            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "CAT_VEHICULO");

            migrationBuilder.DropColumn(
                name: "NDenunciaOficio",
                table: "CAT_RHECHO");

            migrationBuilder.DropColumn(
                name: "Hipotesis",
                table: "CAT_RDH");
        }
    }
}
