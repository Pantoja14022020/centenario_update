using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class _080620 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ARCHIVOS_VEHICULOS_CAT_VEHICULO_VehiculoId",
                table: "ARCHIVOS_VEHICULOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ARCHIVOS_VEHICULOS",
                table: "ARCHIVOS_VEHICULOS");

            migrationBuilder.RenameTable(
                name: "ARCHIVOS_VEHICULOS",
                newName: "CAT_ARCHIVOS_VEHICULOS");

            migrationBuilder.RenameIndex(
                name: "IX_ARCHIVOS_VEHICULOS_VehiculoId",
                table: "CAT_ARCHIVOS_VEHICULOS",
                newName: "IX_CAT_ARCHIVOS_VEHICULOS_VehiculoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_ARCHIVOS_VEHICULOS",
                table: "CAT_ARCHIVOS_VEHICULOS",
                column: "IdArchivoVehiculos");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ARCHIVOS_VEHICULOS_CAT_VEHICULO_VehiculoId",
                table: "CAT_ARCHIVOS_VEHICULOS",
                column: "VehiculoId",
                principalTable: "CAT_VEHICULO",
                principalColumn: "IdVehiculo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ARCHIVOS_VEHICULOS_CAT_VEHICULO_VehiculoId",
                table: "CAT_ARCHIVOS_VEHICULOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_ARCHIVOS_VEHICULOS",
                table: "CAT_ARCHIVOS_VEHICULOS");

            migrationBuilder.RenameTable(
                name: "CAT_ARCHIVOS_VEHICULOS",
                newName: "ARCHIVOS_VEHICULOS");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_ARCHIVOS_VEHICULOS_VehiculoId",
                table: "ARCHIVOS_VEHICULOS",
                newName: "IX_ARCHIVOS_VEHICULOS_VehiculoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ARCHIVOS_VEHICULOS",
                table: "ARCHIVOS_VEHICULOS",
                column: "IdArchivoVehiculos");

            migrationBuilder.AddForeignKey(
                name: "FK_ARCHIVOS_VEHICULOS_CAT_VEHICULO_VehiculoId",
                table: "ARCHIVOS_VEHICULOS",
                column: "VehiculoId",
                principalTable: "CAT_VEHICULO",
                principalColumn: "IdVehiculo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
