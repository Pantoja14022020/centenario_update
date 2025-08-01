using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _130620Correcion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Respuesta",
                table: "SP_PERITOSASIGNADOS");

            migrationBuilder.DropColumn(
                name: "DistritoEnvia",
                table: "CAT_RDILIGENCIAS");

            migrationBuilder.DropColumn(
                name: "DistritoEnviaN",
                table: "CAT_RDILIGENCIAS");

            migrationBuilder.DropColumn(
                name: "DistritoRecibe",
                table: "CAT_RDILIGENCIAS");

            migrationBuilder.RenameColumn(
                name: "DistritorecibeN",
                table: "CAT_RDILIGENCIAS",
                newName: "Respuesta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Respuesta",
                table: "CAT_RDILIGENCIAS",
                newName: "DistritorecibeN");

            migrationBuilder.AddColumn<string>(
                name: "Respuesta",
                table: "SP_PERITOSASIGNADOS",
                nullable: true);

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
        }
    }
}
