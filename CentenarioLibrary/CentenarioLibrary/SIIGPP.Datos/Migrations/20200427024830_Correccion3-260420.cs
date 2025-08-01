using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class Correccion3260420 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "C_FISCALIAOESTADOS",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MunicipioId",
                table: "C_FISCALIAOESTADOS",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_C_FISCALIAOESTADOS_EstadoId",
                table: "C_FISCALIAOESTADOS",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_C_FISCALIAOESTADOS_MunicipioId",
                table: "C_FISCALIAOESTADOS",
                column: "MunicipioId");

            migrationBuilder.AddForeignKey(
                name: "FK_C_FISCALIAOESTADOS_C_ESTADO_EstadoId",
                table: "C_FISCALIAOESTADOS",
                column: "EstadoId",
                principalTable: "C_ESTADO",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_C_FISCALIAOESTADOS_C_MUNICIPIO_MunicipioId",
                table: "C_FISCALIAOESTADOS",
                column: "MunicipioId",
                principalTable: "C_MUNICIPIO",
                principalColumn: "IdMunicipio",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_C_FISCALIAOESTADOS_C_ESTADO_EstadoId",
                table: "C_FISCALIAOESTADOS");

            migrationBuilder.DropForeignKey(
                name: "FK_C_FISCALIAOESTADOS_C_MUNICIPIO_MunicipioId",
                table: "C_FISCALIAOESTADOS");

            migrationBuilder.DropIndex(
                name: "IX_C_FISCALIAOESTADOS_EstadoId",
                table: "C_FISCALIAOESTADOS");

            migrationBuilder.DropIndex(
                name: "IX_C_FISCALIAOESTADOS_MunicipioId",
                table: "C_FISCALIAOESTADOS");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "C_FISCALIAOESTADOS");

            migrationBuilder.DropColumn(
                name: "MunicipioId",
                table: "C_FISCALIAOESTADOS");
        }
    }
}
