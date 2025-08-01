using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class GO030520CAT05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Moduloqueenvia",
                table: "CAT_REMISIONUI",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PuestoA",
                table: "CAT_REMISIONUI",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Moduloqueenvia",
                table: "CAT_REMISIONUI");

            migrationBuilder.DropColumn(
                name: "PuestoA",
                table: "CAT_REMISIONUI");
        }
    }
}
