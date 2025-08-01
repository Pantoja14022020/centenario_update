using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class DA170520CONF01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstitutoPolicial",
                table: "CAT_PERSONA",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numerornd",
                table: "CAT_PERSONA",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CAT_HIPOTESIS",
                columns: table => new
                {
                    IdHipotesis = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_HIPOTESIS", x => x.IdHipotesis);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_HIPOTESIS");

            migrationBuilder.DropColumn(
                name: "InstitutoPolicial",
                table: "CAT_PERSONA");

            migrationBuilder.DropColumn(
                name: "Numerornd",
                table: "CAT_PERSONA");
        }
    }
}
