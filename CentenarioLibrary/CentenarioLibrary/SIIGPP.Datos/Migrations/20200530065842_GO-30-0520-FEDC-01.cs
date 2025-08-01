using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class GO300520FEDC01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FEDC_NOACCIONPENAL",
                columns: table => new
                {
                    IdNoAcionPenal = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    NumeroOficio = table.Column<string>(nullable: true),
                    Delitos = table.Column<string>(nullable: true),
                    Victimas = table.Column<string>(nullable: true),
                    Imputados = table.Column<string>(nullable: true),
                    Cosumacion = table.Column<string>(nullable: true),
                    AusenciaVOluntad = table.Column<string>(nullable: true),
                    CausasAtipicidad = table.Column<string>(nullable: true),
                    FalteElementos = table.Column<string>(nullable: true),
                    EfectosCodigo = table.Column<string>(nullable: true),
                    Sobreseimiento = table.Column<string>(nullable: true),
                    HechocncDelito = table.Column<string>(nullable: true),
                    Antecedente = table.Column<string>(nullable: true),
                    Articulo25 = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_FEDC_NOACCIONPENAL", x => x.IdNoAcionPenal);
                    table.ForeignKey(
                        name: "FK_FEDC_NOACCIONPENAL_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FEDC_NOACCIONPENAL_RHechoId",
                table: "FEDC_NOACCIONPENAL",
                column: "RHechoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FEDC_NOACCIONPENAL");
        }
    }
}
