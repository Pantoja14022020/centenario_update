using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class DA010520CAT04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_REMISIONUI",
                columns: table => new
                {
                    IdRemisionUI = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Fecha = table.Column<string>(nullable: true),
                    DirigidoA = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_REMISIONUI", x => x.IdRemisionUI);
                    table.ForeignKey(
                        name: "FK_CAT_REMISIONUI_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_SCOLABORACIONMP",
                columns: table => new
                {
                    IdSColaboraMP = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    AgenciaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Texto = table.Column<string>(nullable: true),
                    NUC = table.Column<string>(nullable: true),
                    UsuarioSolicita = table.Column<string>(nullable: true),
                    AgenciaOrigen = table.Column<string>(nullable: true),
                    AgenciaDestino = table.Column<string>(nullable: true),
                    Satus = table.Column<string>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true),
                    FechaRespuesta = table.Column<DateTime>(nullable: false),
                    FechaRechazo = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_SCOLABORACIONMP", x => x.IdSColaboraMP);
                    table.ForeignKey(
                        name: "FK_CAT_SCOLABORACIONMP_C_AGENCIA_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "C_AGENCIA",
                        principalColumn: "IdAgencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_SCOLABORACIONMP_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ASIGNACION_COLABORACION",
                columns: table => new
                {
                    IdAsignacionColaboraciones = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    SColaboraMPId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
             
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ASIGNACION_COLABORACION", x => x.IdAsignacionColaboraciones);
                    table.ForeignKey(
                        name: "FK_CAT_ASIGNACION_COLABORACION_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_ASIGNACION_COLABORACION_CAT_SCOLABORACIONMP_SColaboraMPId",
                        column: x => x.SColaboraMPId,
                        principalTable: "CAT_SCOLABORACIONMP",
                        principalColumn: "IdSColaboraMP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ASIGNACION_COLABORACION_ModuloServicioId",
                table: "CAT_ASIGNACION_COLABORACION",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ASIGNACION_COLABORACION_SColaboraMPId",
                table: "CAT_ASIGNACION_COLABORACION",
                column: "SColaboraMPId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_REMISIONUI_RHechoId",
                table: "CAT_REMISIONUI",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_SCOLABORACIONMP_AgenciaId",
                table: "CAT_SCOLABORACIONMP",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_SCOLABORACIONMP_RHechoId",
                table: "CAT_SCOLABORACIONMP",
                column: "RHechoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_ASIGNACION_COLABORACION");

            migrationBuilder.DropTable(
                name: "CAT_REMISIONUI");

            migrationBuilder.DropTable(
                name: "CAT_SCOLABORACIONMP");
        }
    }
}
