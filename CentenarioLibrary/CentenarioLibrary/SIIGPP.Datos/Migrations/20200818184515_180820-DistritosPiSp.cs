using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _180820DistritosPiSp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DistritoId",
                table: "CAT_RDILIGENCIAS",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DistritoId",
                table: "CAT_RACTOSINVESTIGACION",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RDILIGENCIAS_DistritoId",
                table: "CAT_RDILIGENCIAS",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RACTOSINVESTIGACION_DistritoId",
                table: "CAT_RACTOSINVESTIGACION",
                column: "DistritoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_RACTOSINVESTIGACION_C_DISTRITO_DistritoId",
                table: "CAT_RACTOSINVESTIGACION",
                column: "DistritoId",
                principalTable: "C_DISTRITO",
                principalColumn: "IdDistrito",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_RDILIGENCIAS_C_DISTRITO_DistritoId",
                table: "CAT_RDILIGENCIAS",
                column: "DistritoId",
                principalTable: "C_DISTRITO",
                principalColumn: "IdDistrito",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_RACTOSINVESTIGACION_C_DISTRITO_DistritoId",
                table: "CAT_RACTOSINVESTIGACION");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_RDILIGENCIAS_C_DISTRITO_DistritoId",
                table: "CAT_RDILIGENCIAS");

            migrationBuilder.DropIndex(
                name: "IX_CAT_RDILIGENCIAS_DistritoId",
                table: "CAT_RDILIGENCIAS");

            migrationBuilder.DropIndex(
                name: "IX_CAT_RACTOSINVESTIGACION_DistritoId",
                table: "CAT_RACTOSINVESTIGACION");

            migrationBuilder.DropColumn(
                name: "DistritoId",
                table: "CAT_RDILIGENCIAS");

            migrationBuilder.DropColumn(
                name: "DistritoId",
                table: "CAT_RACTOSINVESTIGACION");
        }
    }
}
