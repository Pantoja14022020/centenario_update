using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class guid16042001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JR_EJEMPLO",
                newName: "rowguid");

            migrationBuilder.AlterColumn<Guid>(
                name: "rowguid",
                table: "JR_EJEMPLO",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newid()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rowguid",
                table: "JR_EJEMPLO",
                newName: "Id");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "JR_EJEMPLO",
                nullable: false,
                defaultValueSql: "newid()",
                oldClrType: typeof(Guid));
        }
    }
}
