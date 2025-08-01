using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _120920GoRasgosFaciales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lateralidad",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pomulos",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Pupilentes",
                table: "CAT_MEDIAAFILIACION",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "C_POMULOS",
                columns: table => new
                {
                    IdPomulos = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_POMULOS", x => x.IdPomulos);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "C_POMULOS");

            migrationBuilder.DropColumn(
                name: "Lateralidad",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "Pomulos",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "Pupilentes",
                table: "CAT_MEDIAAFILIACION");
        }
    }
}
