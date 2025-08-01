using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class GO050520CAT08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ModuloServicioId",
                table: "CAT_REMISIONUI",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CAT_REMISIONUI_ModuloServicioId",
                table: "CAT_REMISIONUI",
                column: "ModuloServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_REMISIONUI_C_MODULOSERVICIO_ModuloServicioId",
                table: "CAT_REMISIONUI",
                column: "ModuloServicioId",
                principalTable: "C_MODULOSERVICIO",
                principalColumn: "IdModuloServicio",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_REMISIONUI_C_MODULOSERVICIO_ModuloServicioId",
                table: "CAT_REMISIONUI");

            migrationBuilder.DropIndex(
                name: "IX_CAT_REMISIONUI_ModuloServicioId",
                table: "CAT_REMISIONUI");

            migrationBuilder.DropColumn(
                name: "ModuloServicioId",
                table: "CAT_REMISIONUI");
        }
    }
}

