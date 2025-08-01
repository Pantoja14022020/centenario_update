using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _240720CAT30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_CONTENCIONES_PERSONAS_CAT_RHECHO_RHechoId",
                table: "CAT_CONTENCIONES_PERSONAS");

            migrationBuilder.RenameColumn(
                name: "RHechoId",
                table: "CAT_CONTENCIONES_PERSONAS",
                newName: "RAtencionId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_CONTENCIONES_PERSONAS_RHechoId",
                table: "CAT_CONTENCIONES_PERSONAS",
                newName: "IX_CAT_CONTENCIONES_PERSONAS_RAtencionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_CONTENCIONES_PERSONAS_CAT_RATENCON_RAtencionId",
                table: "CAT_CONTENCIONES_PERSONAS",
                column: "RAtencionId",
                principalTable: "CAT_RATENCON",
                principalColumn: "IdRAtencion",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_CONTENCIONES_PERSONAS_CAT_RATENCON_RAtencionId",
                table: "CAT_CONTENCIONES_PERSONAS");

            migrationBuilder.RenameColumn(
                name: "RAtencionId",
                table: "CAT_CONTENCIONES_PERSONAS",
                newName: "RHechoId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_CONTENCIONES_PERSONAS_RAtencionId",
                table: "CAT_CONTENCIONES_PERSONAS",
                newName: "IX_CAT_CONTENCIONES_PERSONAS_RHechoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_CONTENCIONES_PERSONAS_CAT_RHECHO_RHechoId",
                table: "CAT_CONTENCIONES_PERSONAS",
                column: "RHechoId",
                principalTable: "CAT_RHECHO",
                principalColumn: "IdRHecho",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
