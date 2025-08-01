using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class correcion4260420 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_JUZGADOS_AGENCIAS_C_DISTRITO_DistritoId",
                table: "CAT_JUZGADOS_AGENCIAS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_JUZGADOS_AGENCIAS",
                table: "CAT_JUZGADOS_AGENCIAS");

            migrationBuilder.RenameTable(
                name: "CAT_JUZGADOS_AGENCIAS",
                newName: "C_JUZGADOS_AGENCIAS");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_JUZGADOS_AGENCIAS_DistritoId",
                table: "C_JUZGADOS_AGENCIAS",
                newName: "IX_C_JUZGADOS_AGENCIAS_DistritoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_C_JUZGADOS_AGENCIAS",
                table: "C_JUZGADOS_AGENCIAS",
                column: "IdJuzgadoAgencia");

            migrationBuilder.AddForeignKey(
                name: "FK_C_JUZGADOS_AGENCIAS_C_DISTRITO_DistritoId",
                table: "C_JUZGADOS_AGENCIAS",
                column: "DistritoId",
                principalTable: "C_DISTRITO",
                principalColumn: "IdDistrito",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_C_JUZGADOS_AGENCIAS_C_DISTRITO_DistritoId",
                table: "C_JUZGADOS_AGENCIAS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_C_JUZGADOS_AGENCIAS",
                table: "C_JUZGADOS_AGENCIAS");

            migrationBuilder.RenameTable(
                name: "C_JUZGADOS_AGENCIAS",
                newName: "CAT_JUZGADOS_AGENCIAS");

            migrationBuilder.RenameIndex(
                name: "IX_C_JUZGADOS_AGENCIAS_DistritoId",
                table: "CAT_JUZGADOS_AGENCIAS",
                newName: "IX_CAT_JUZGADOS_AGENCIAS_DistritoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_JUZGADOS_AGENCIAS",
                table: "CAT_JUZGADOS_AGENCIAS",
                column: "IdJuzgadoAgencia");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_JUZGADOS_AGENCIAS_C_DISTRITO_DistritoId",
                table: "CAT_JUZGADOS_AGENCIAS",
                column: "DistritoId",
                principalTable: "C_DISTRITO",
                principalColumn: "IdDistrito",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
