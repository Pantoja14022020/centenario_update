using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _140820LigacionesSPPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "C_SPPI_LIGACIONES",
                columns: table => new
                {
                    IdSPPiligaciones = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    DSPId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    PanelControlId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    Direccion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_SPPI_LIGACIONES", x => x.IdSPPiligaciones);
                    table.ForeignKey(
                        name: "FK_C_SPPI_LIGACIONES_C_DSP_DSPId",
                        column: x => x.DSPId,
                        principalTable: "C_DSP",
                        principalColumn: "IdDSP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_C_SPPI_LIGACIONES_PC_PANELCONTROL_PanelControlId",
                        column: x => x.PanelControlId,
                        principalTable: "PC_PANELCONTROL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_C_SPPI_LIGACIONES_DSPId",
                table: "C_SPPI_LIGACIONES",
                column: "DSPId");

            migrationBuilder.CreateIndex(
                name: "IX_C_SPPI_LIGACIONES_PanelControlId",
                table: "C_SPPI_LIGACIONES",
                column: "PanelControlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "C_SPPI_LIGACIONES");
        }
    }
}
