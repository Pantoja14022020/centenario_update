using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class Correccion260420 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_C_FISCALIAOESTADOS_C_ESTADO_EstadoIdEstado",
                table: "C_FISCALIAOESTADOS");

            migrationBuilder.DropIndex(
                name: "IX_C_FISCALIAOESTADOS_EstadoIdEstado",
                table: "C_FISCALIAOESTADOS");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "C_FISCALIAOESTADOS");

            migrationBuilder.DropColumn(
                name: "EstadoIdEstado",
                table: "C_FISCALIAOESTADOS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EstadoId",
                table: "C_FISCALIAOESTADOS",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "EstadoIdEstado",
                table: "C_FISCALIAOESTADOS",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_C_FISCALIAOESTADOS_EstadoIdEstado",
                table: "C_FISCALIAOESTADOS",
                column: "EstadoIdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_C_FISCALIAOESTADOS_C_ESTADO_EstadoIdEstado",
                table: "C_FISCALIAOESTADOS",
                column: "EstadoIdEstado",
                principalTable: "C_ESTADO",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
