using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _230720CAT29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_ACUMULACION_CARPETA",
                columns: table => new
                {
                    IdAcumulacionCarpeta = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    NUCFusion = table.Column<string>(nullable: true),
                    RhechoIdFusion = table.Column<Guid>(nullable: false),
                    FechaSys = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    UUsuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ACUMULACION_CARPETA", x => x.IdAcumulacionCarpeta);
                    table.ForeignKey(
                        name: "FK_CAT_ACUMULACION_CARPETA_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_CONTENCIONES_PERSONAS",
                columns: table => new
                {
                    IdContencionesPersona = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    QueRequirio = table.Column<string>(nullable: true),
                    NombrePersona = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    UUsuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_CONTENCIONES_PERSONAS", x => x.IdContencionesPersona);
                    table.ForeignKey(
                        name: "FK_CAT_CONTENCIONES_PERSONAS_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ACUMULACION_CARPETA_RHechoId",
                table: "CAT_ACUMULACION_CARPETA",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_CONTENCIONES_PERSONAS_RHechoId",
                table: "CAT_CONTENCIONES_PERSONAS",
                column: "RHechoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_ACUMULACION_CARPETA");

            migrationBuilder.DropTable(
                name: "CAT_CONTENCIONES_PERSONAS");
        }
    }
}
