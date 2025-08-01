using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _160720CAT25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_PROCEDIMIENTOABREVIADO",
                columns: table => new
                {
                    IdProcedimientoAbreviado = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    CausaPenal = table.Column<string>(nullable: true),
                    Delito = table.Column<string>(nullable: true),
                    TitularOrientePoniente = table.Column<string>(nullable: true),
                    OrientePoniente = table.Column<string>(nullable: true),
                    Imputado = table.Column<string>(nullable: true),
                    EdadImputado = table.Column<string>(nullable: true),
                    FechaNImputado = table.Column<string>(nullable: true),
                    AutoDescripcionIndignenaImputado = table.Column<string>(nullable: true),
                    AsistenciaConsularImputado = table.Column<string>(nullable: true),
                    DomicilioParticularImputado = table.Column<string>(nullable: true),
                    DomicilioEscucha = table.Column<string>(nullable: true),
                    DefensaParticularPublicaImputado = table.Column<string>(nullable: true),
                    BaseAcusacionRH = table.Column<string>(nullable: true),
                    TiempoRH = table.Column<string>(nullable: true),
                    LugarRH = table.Column<string>(nullable: true),
                    ModoRH = table.Column<string>(nullable: true),
                    DolosoCulposo = table.Column<string>(nullable: true),
                    PrisionDe = table.Column<string>(nullable: true),
                    PrisionLP = table.Column<bool>(nullable: false),
                    MultaLP = table.Column<bool>(nullable: false),
                    PrisionALP = table.Column<string>(nullable: true),
                    MultaDeLPLP = table.Column<string>(nullable: true),
                    MultaALP = table.Column<string>(nullable: true),
                    PrisionMayor5LP = table.Column<bool>(nullable: false),
                    PrisionMenor5LP = table.Column<bool>(nullable: false),
                    TipoAutoriaParticipacion = table.Column<string>(nullable: true),
                    CodigoPenalPL = table.Column<string>(nullable: true),
                    RegistroAntecedentes = table.Column<string>(nullable: true),
                    DGNumero = table.Column<string>(nullable: true),
                    DGFecha = table.Column<string>(nullable: true),
                    DGComunica = table.Column<string>(nullable: true),
                    DCDe = table.Column<string>(nullable: true),
                    DCNumero = table.Column<string>(nullable: true),
                    DCFecha = table.Column<string>(nullable: true),
                    DCComunica = table.Column<string>(nullable: true),
                    PSOpcion1PS = table.Column<bool>(nullable: false),
                    PrisionOp1PS = table.Column<string>(nullable: true),
                    PSOpcion2PS = table.Column<bool>(nullable: false),
                    MultaOp2PS = table.Column<string>(nullable: true),
                    CanditadOp2PS = table.Column<string>(nullable: true),
                    CantidadHechosOp2PS = table.Column<string>(nullable: true),
                    PSOpcion3PS = table.Column<bool>(nullable: false),
                    PSOpcion4PS = table.Column<bool>(nullable: false),
                    PSOpcion5PS = table.Column<bool>(nullable: false),
                    DuranteOp5PS = table.Column<string>(nullable: true),
                    PSOpcion6PS = table.Column<bool>(nullable: false),
                    PeriodoOp6PS = table.Column<string>(nullable: true),
                    Opcion1PPA = table.Column<bool>(nullable: false),
                    ExplicarOP1PPA = table.Column<string>(nullable: true),
                    Opcion2PPA = table.Column<bool>(nullable: false),
                    ExplicarOP2PPA = table.Column<string>(nullable: true),
                    Opcion3PPA = table.Column<bool>(nullable: false),
                    Opcion4PPA = table.Column<bool>(nullable: false),
                    Opcion5PPA = table.Column<bool>(nullable: false),
                    Opcion6PPA = table.Column<bool>(nullable: false),
                    Opcion7PPA = table.Column<bool>(nullable: false),
                    Opcion8PPA = table.Column<bool>(nullable: false),
                    Opcion9PPA = table.Column<bool>(nullable: false),
                    Opcion10PPA = table.Column<bool>(nullable: false),
                    Opcion11PPA = table.Column<bool>(nullable: false),
                    Opcion12PPA = table.Column<bool>(nullable: false),
                    Opcion13PPA = table.Column<bool>(nullable: false),
                    OtroOP13PPA = table.Column<string>(nullable: true),
                    Opcion1RD = table.Column<bool>(nullable: false),
                    Opcion2RD = table.Column<bool>(nullable: false),
                    Opcion3RD = table.Column<bool>(nullable: false),
                    GastosOp1RD = table.Column<bool>(nullable: false),
                    GastosErogadosOP3RD = table.Column<string>(nullable: true),
                    GastosOp2RD = table.Column<bool>(nullable: false),
                    GastosCuantificadosOP3RD = table.Column<string>(nullable: true),
                    GastosOp3RD = table.Column<bool>(nullable: false),
                    GastosAsesorJuridicoOP3RD = table.Column<string>(nullable: true),
                    GastosOp4RD = table.Column<bool>(nullable: false),
                    Opcion4RD = table.Column<bool>(nullable: false),
                    Opcion5RD = table.Column<bool>(nullable: false),
                    ExplicacionImputadoFechaHora = table.Column<DateTime>(nullable: false),
                    PosturaVictimaFechaHora = table.Column<DateTime>(nullable: false),
                    Opcion1PV = table.Column<bool>(nullable: false),
                    Opcion2PV = table.Column<bool>(nullable: false),
                    PorqueOp2PV = table.Column<string>(nullable: true),
                    NumeroOficio = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_CAT_PROCEDIMIENTOABREVIADO", x => x.IdProcedimientoAbreviado);
                    table.ForeignKey(
                        name: "FK_CAT_PROCEDIMIENTOABREVIADO_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_PROCEDIMIENTOABREVIADO_RHechoId",
                table: "CAT_PROCEDIMIENTOABREVIADO",
                column: "RHechoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_PROCEDIMIENTOABREVIADO");
        }
    }
}
