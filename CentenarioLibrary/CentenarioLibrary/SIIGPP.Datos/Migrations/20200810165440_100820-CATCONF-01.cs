using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _100820CATCONF01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedioLlegada",
                table: "CAT_RATENCON",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "C_MEDIDAS_CAUTELARES",
                columns: table => new
                {
                    IdMedidasCautelaresC = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Clave = table.Column<string>(nullable: true),
                    Clasificacion = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_MEDIDAS_CAUTELARES", x => x.IdMedidasCautelaresC);
                });

            migrationBuilder.CreateTable(
                name: "C_MEDIDAS_PROTECCION",
                columns: table => new
                {
                    IdMedidasProteccionC = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Clave = table.Column<string>(nullable: true),
                    Clasificacion = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_MEDIDAS_PROTECCION", x => x.IdMedidasProteccionC);
                });

            migrationBuilder.CreateTable(
                name: "CAT_NO_MEDIDASCAUTELARES",
                columns: table => new
                {
                    IdNoMedidasCautelares = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    MedidasCautelaresId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Clave = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_NO_MEDIDASCAUTELARES", x => x.IdNoMedidasCautelares);
                    table.ForeignKey(
                        name: "FK_CAT_NO_MEDIDASCAUTELARES_CAT_MEDIDASCAUTELARES_MedidasCautelaresId",
                        column: x => x.MedidasCautelaresId,
                        principalTable: "CAT_MEDIDASCAUTELARES",
                        principalColumn: "IdMedCautelares",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_NO_MEDIDASPROTECCION",
                columns: table => new
                {
                    IdNoMedidasProteccion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    MedidasproteccionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Clave = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_NO_MEDIDASPROTECCION", x => x.IdNoMedidasProteccion);
                    table.ForeignKey(
                        name: "FK_CAT_NO_MEDIDASPROTECCION_CAT_MEDIDASPROTECCION_MedidasproteccionId",
                        column: x => x.MedidasproteccionId,
                        principalTable: "CAT_MEDIDASPROTECCION",
                        principalColumn: "IdMProteccion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_NO_MEDIDASCAUTELARES_MedidasCautelaresId",
                table: "CAT_NO_MEDIDASCAUTELARES",
                column: "MedidasCautelaresId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_NO_MEDIDASPROTECCION_MedidasproteccionId",
                table: "CAT_NO_MEDIDASPROTECCION",
                column: "MedidasproteccionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "C_MEDIDAS_CAUTELARES");

            migrationBuilder.DropTable(
                name: "C_MEDIDAS_PROTECCION");

            migrationBuilder.DropTable(
                name: "CAT_NO_MEDIDASCAUTELARES");

            migrationBuilder.DropTable(
                name: "CAT_NO_MEDIDASPROTECCION");

            migrationBuilder.DropColumn(
                name: "MedioLlegada",
                table: "CAT_RATENCON");
        }
    }
}
