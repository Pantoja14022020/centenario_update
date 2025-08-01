using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _140820EtapaI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EtapaInicial",
                table: "CAT_RDILIGENCIASFORANEAS",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DSPDEstino",
                table: "CAT_RDILIGENCIAS",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "EtapaInicial",
                table: "CAT_RDILIGENCIAS",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DSPDEstino",
                table: "CAT_RACTOSINVESTIGACION",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "EtapaInicial",
                table: "CAT_RACTOSINVESTIGACION",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EtapaInicial",
                table: "CAT_RDILIGENCIASFORANEAS");

            migrationBuilder.DropColumn(
                name: "DSPDEstino",
                table: "CAT_RDILIGENCIAS");

            migrationBuilder.DropColumn(
                name: "EtapaInicial",
                table: "CAT_RDILIGENCIAS");

            migrationBuilder.DropColumn(
                name: "DSPDEstino",
                table: "CAT_RACTOSINVESTIGACION");

            migrationBuilder.DropColumn(
                name: "EtapaInicial",
                table: "CAT_RACTOSINVESTIGACION");
        }
    }
}
