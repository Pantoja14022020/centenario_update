using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations.DbContextSIIGPP2Migrations
{
    public partial class _140620CAT24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgenciaEnvia",
                table: "CAT_RDILIGENCIASFORANEAS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgenciaRecibe",
                table: "CAT_RDILIGENCIASFORANEAS",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "AgenciaEnvia",
                table: "CAT_RDILIGENCIASFORANEAS");

            migrationBuilder.DropColumn(
                name: "AgenciaRecibe",
                table: "CAT_RDILIGENCIASFORANEAS");

        }
    }
}
