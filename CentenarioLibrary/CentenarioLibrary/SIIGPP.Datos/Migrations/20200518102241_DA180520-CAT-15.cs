using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class DA180520CAT15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ARCHIVOS_VEHICULOS",
                columns: table => new
                {
                    IdArchivoVehiculos = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    VehiculoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoDocumento = table.Column<string>(nullable: true),
                    NombreDocumento = table.Column<string>(nullable: true),
                    DescripcionDocumento = table.Column<string>(nullable: true),
                    Ruta = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_ARCHIVOS_VEHICULOS", x => x.IdArchivoVehiculos);
                    table.ForeignKey(
                        name: "FK_ARCHIVOS_VEHICULOS_CAT_VEHICULO_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "CAT_VEHICULO",
                        principalColumn: "IdVehiculo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ARCHIVOS_VEHICULOS_VehiculoId",
                table: "ARCHIVOS_VEHICULOS",
                column: "VehiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ARCHIVOS_VEHICULOS");
        }
    }
}
