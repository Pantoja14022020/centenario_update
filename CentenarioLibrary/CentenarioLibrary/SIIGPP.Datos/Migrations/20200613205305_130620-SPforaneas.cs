using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _130620SPforaneas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_RDILIGENCIASFORANEAS",
                columns: table => new
                {
                    IdRDiligenciasForaneas = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    rHechoId = table.Column<Guid>(nullable: false),
                    FechaSolicitud = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(nullable: false),
                    Dirigidoa = table.Column<string>(nullable: true),
                    DirSubPro = table.Column<string>(nullable: true),
                    EmitidoPor = table.Column<string>(nullable: true),
                    uDirSubPro = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    StatusRespuesta = table.Column<string>(nullable: true),
                    Servicio = table.Column<string>(nullable: true),
                    Especificaciones = table.Column<string>(nullable: true),
                    Prioridad = table.Column<string>(nullable: true),
                    ASPId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Modulo = table.Column<string>(nullable: true),
                    Agencia = table.Column<string>(nullable: true),
                    Respuestas = table.Column<string>(nullable: true),
                    NUC = table.Column<string>(nullable: true),
                    Textofinal = table.Column<string>(nullable: true),
                    NumeroOficio = table.Column<string>(nullable: true),
                    NodeSolicitud = table.Column<string>(nullable: true),
                    NumeroDistrito = table.Column<string>(nullable: true),
                    Lat = table.Column<string>(nullable: true),
                    Lng = table.Column<string>(nullable: true),
                    Dirigido = table.Column<bool>(nullable: false),
                    RecibidoF = table.Column<bool>(nullable: false),
                    FechaRecibidoF = table.Column<DateTime>(nullable: false),
                    Respuesta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_RDILIGENCIASFORANEAS", x => x.IdRDiligenciasForaneas);
                    table.ForeignKey(
                        name: "FK_CAT_RDILIGENCIASFORANEAS_SP_ASP_ASPId",
                        column: x => x.ASPId,
                        principalTable: "SP_ASP",
                        principalColumn: "IdASP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SP_PERITOSASIGNADOSFORANEAS",
                columns: table => new
                {
                    IdPeritosAsignadoForaneas = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    RDiligenciasForaneasId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    NumeroInterno = table.Column<int>(nullable: false),
                    Conclusion = table.Column<string>(nullable: true),
                    FechaRecibido = table.Column<string>(nullable: true),
                    FechaAceptado = table.Column<string>(nullable: true),
                    FechaFinalizado = table.Column<string>(nullable: true),
                    FechaEntregado = table.Column<string>(nullable: true),
                    uDistrito = table.Column<string>(nullable: true),
                    uSubproc = table.Column<string>(nullable: true),
                    uAgencia = table.Column<string>(nullable: true),
                    uUsuario = table.Column<string>(nullable: true),
                    uPuesto = table.Column<string>(nullable: true),
                    uModulo = table.Column<string>(nullable: true),
                    Fechasysregistro = table.Column<DateTime>(nullable: false),
                    Fechasysfinalizado = table.Column<DateTime>(nullable: false),
                    UltmimoStatus = table.Column<DateTime>(nullable: false),
                    NumeroControl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SP_PERITOSASIGNADOSFORANEAS", x => x.IdPeritosAsignadoForaneas);
                    table.ForeignKey(
                        name: "FK_SP_PERITOSASIGNADOSFORANEAS_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SP_PERITOSASIGNADOSFORANEAS_CAT_RDILIGENCIASFORANEAS_RDiligenciasForaneasId",
                        column: x => x.RDiligenciasForaneasId,
                        principalTable: "CAT_RDILIGENCIASFORANEAS",
                        principalColumn: "IdRDiligenciasForaneas",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RDILIGENCIASFORANEAS_ASPId",
                table: "CAT_RDILIGENCIASFORANEAS",
                column: "ASPId");

            migrationBuilder.CreateIndex(
                name: "IX_SP_PERITOSASIGNADOSFORANEAS_ModuloServicioId",
                table: "SP_PERITOSASIGNADOSFORANEAS",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_SP_PERITOSASIGNADOSFORANEAS_RDiligenciasForaneasId",
                table: "SP_PERITOSASIGNADOSFORANEAS",
                column: "RDiligenciasForaneasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SP_PERITOSASIGNADOSFORANEAS");

            migrationBuilder.DropTable(
                name: "CAT_RDILIGENCIASFORANEAS");
        }
    }
}
