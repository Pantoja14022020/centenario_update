using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class DA060520CAT12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Acompañantev",
                table: "CAT_AMPDEC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FechaS",
                table: "CAT_AMPDEC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoraS",
                table: "CAT_AMPDEC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Iniciales",
                table: "CAT_AMPDEC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoIdentificacion",
                table: "CAT_AMPDEC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentezcoV",
                table: "CAT_AMPDEC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Representante",
                table: "CAT_AMPDEC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tidentificacion",
                table: "CAT_AMPDEC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoEA",
                table: "CAT_AMPDEC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VNombre",
                table: "CAT_AMPDEC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VPuesto",
                table: "CAT_AMPDEC",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acompañantev",
                table: "CAT_AMPDEC");

            migrationBuilder.DropColumn(
                name: "FechaS",
                table: "CAT_AMPDEC");

            migrationBuilder.DropColumn(
                name: "HoraS",
                table: "CAT_AMPDEC");

            migrationBuilder.DropColumn(
                name: "Iniciales",
                table: "CAT_AMPDEC");

            migrationBuilder.DropColumn(
                name: "NoIdentificacion",
                table: "CAT_AMPDEC");

            migrationBuilder.DropColumn(
                name: "ParentezcoV",
                table: "CAT_AMPDEC");

            migrationBuilder.DropColumn(
                name: "Representante",
                table: "CAT_AMPDEC");

            migrationBuilder.DropColumn(
                name: "Tidentificacion",
                table: "CAT_AMPDEC");

            migrationBuilder.DropColumn(
                name: "TipoEA",
                table: "CAT_AMPDEC");

            migrationBuilder.DropColumn(
                name: "VNombre",
                table: "CAT_AMPDEC");

            migrationBuilder.DropColumn(
                name: "VPuesto",
                table: "CAT_AMPDEC");
        }
    }
}
