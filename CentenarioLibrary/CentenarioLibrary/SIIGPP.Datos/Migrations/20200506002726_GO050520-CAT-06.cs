using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class GO050520CAT06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRechazo",
                table: "CAT_REMISIONUI",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Rechazo",
                table: "CAT_REMISIONUI",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaRechazo",
                table: "CAT_REMISIONUI");

            migrationBuilder.DropColumn(
                name: "Rechazo",
                table: "CAT_REMISIONUI");
        }
    }
}
