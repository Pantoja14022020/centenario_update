using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class GO120620CAT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DistritoEnvia",
                table: "CAT_RDILIGENCIAS",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DistritoEnviaN",
                table: "CAT_RDILIGENCIAS",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DistritoRecibe",
                table: "CAT_RDILIGENCIAS",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DistritorecibeN",
                table: "CAT_RDILIGENCIAS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistritoEnvia",
                table: "CAT_RDILIGENCIAS");

            migrationBuilder.DropColumn(
                name: "DistritoEnviaN",
                table: "CAT_RDILIGENCIAS");

            migrationBuilder.DropColumn(
                name: "DistritoRecibe",
                table: "CAT_RDILIGENCIAS");

            migrationBuilder.DropColumn(
                name: "DistritorecibeN",
                table: "CAT_RDILIGENCIAS");
        }
    }
}
