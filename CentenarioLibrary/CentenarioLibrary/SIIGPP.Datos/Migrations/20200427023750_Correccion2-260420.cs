using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class Correccion2260420 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_C_FISCALIAOESTADOS_C_MUNICIPIO_MunicipioIdMunicipio",
                table: "C_FISCALIAOESTADOS");

            migrationBuilder.DropIndex(
                name: "IX_C_FISCALIAOESTADOS_MunicipioIdMunicipio",
                table: "C_FISCALIAOESTADOS");

            migrationBuilder.DropColumn(
                name: "MunicipioId",
                table: "C_FISCALIAOESTADOS");

            migrationBuilder.DropColumn(
                name: "MunicipioIdMunicipio",
                table: "C_FISCALIAOESTADOS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MunicipioId",
                table: "C_FISCALIAOESTADOS",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "MunicipioIdMunicipio",
                table: "C_FISCALIAOESTADOS",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_C_FISCALIAOESTADOS_MunicipioIdMunicipio",
                table: "C_FISCALIAOESTADOS",
                column: "MunicipioIdMunicipio");

            migrationBuilder.AddForeignKey(
                name: "FK_C_FISCALIAOESTADOS_C_MUNICIPIO_MunicipioIdMunicipio",
                table: "C_FISCALIAOESTADOS",
                column: "MunicipioIdMunicipio",
                principalTable: "C_MUNICIPIO",
                principalColumn: "IdMunicipio",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
