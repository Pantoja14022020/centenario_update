using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _130920Resolucion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_RESOLUCION",
                columns: table => new
                {
                    IdResolucion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Victimas = table.Column<string>(nullable: true),
                    Imputados = table.Column<string>(nullable: true),
                    Delitos = table.Column<string>(nullable: true),
                    CausaPenal = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    SubTipo = table.Column<string>(nullable: true),
                    TextoDocumento = table.Column<string>(nullable: true),
                    FechaResolucion = table.Column<DateTime>(nullable: false),
                    FechaConsulta = table.Column<DateTime>(nullable: false),
                    FechaAutorizacion = table.Column<DateTime>(nullable: false),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_RESOLUCION", x => x.IdResolucion);
                    table.ForeignKey(
                        name: "FK_CAT_RESOLUCION_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RESOLUCION_RHechoId",
                table: "CAT_RESOLUCION",
                column: "RHechoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_RESOLUCION");
        }
    }
}
