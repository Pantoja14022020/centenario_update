using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _030820CAT31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "C_DELITO_ESPECIFICO",
                columns: table => new
                {
                    IdDelitoEspecifico = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true),
                    DelitoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_DELITO_ESPECIFICO", x => x.IdDelitoEspecifico);
                    table.ForeignKey(
                        name: "FK_C_DELITO_ESPECIFICO_C_DELITO_DelitoId",
                        column: x => x.DelitoId,
                        principalTable: "C_DELITO",
                        principalColumn: "IdDelito",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_C_DELITO_ESPECIFICO_DelitoId",
                table: "C_DELITO_ESPECIFICO",
                column: "DelitoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "C_DELITO_ESPECIFICO");
        }
    }
}
