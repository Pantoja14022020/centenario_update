using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class CorrecionTablas290720 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_MULTACITATORIO",
                table: "CAT_MULTACITATORIO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_HIPOTESIS",
                table: "CAT_HIPOTESIS");

            migrationBuilder.RenameTable(
                name: "CAT_MULTACITATORIO",
                newName: "C_MULTACITATORIO");

            migrationBuilder.RenameTable(
                name: "CAT_HIPOTESIS",
                newName: "C_HIPOTESIS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_C_MULTACITATORIO",
                table: "C_MULTACITATORIO",
                column: "IdMultaCitatorio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_C_HIPOTESIS",
                table: "C_HIPOTESIS",
                column: "IdHipotesis");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_C_MULTACITATORIO",
                table: "C_MULTACITATORIO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_C_HIPOTESIS",
                table: "C_HIPOTESIS");

            migrationBuilder.RenameTable(
                name: "C_MULTACITATORIO",
                newName: "CAT_MULTACITATORIO");

            migrationBuilder.RenameTable(
                name: "C_HIPOTESIS",
                newName: "CAT_HIPOTESIS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_MULTACITATORIO",
                table: "CAT_MULTACITATORIO",
                column: "IdMultaCitatorio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_HIPOTESIS",
                table: "CAT_HIPOTESIS",
                column: "IdHipotesis");
        }
    }
}
