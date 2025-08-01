using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class GO060520CAT11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "u_Puesto",
                table: "CAT_AMPDEC",
                newName: "Usuario");

            migrationBuilder.RenameColumn(
                name: "u_Nombre",
                table: "CAT_AMPDEC",
                newName: "USubproc");

            migrationBuilder.RenameColumn(
                name: "u_Modulo",
                table: "CAT_AMPDEC",
                newName: "UPuesto");

            migrationBuilder.RenameColumn(
                name: "FechaAD",
                table: "CAT_AMPDEC",
                newName: "Fechasys");

            migrationBuilder.RenameColumn(
                name: "Distrito",
                table: "CAT_AMPDEC",
                newName: "UModulo");

            migrationBuilder.RenameColumn(
                name: "DirSubProcu",
                table: "CAT_AMPDEC",
                newName: "UDistrito");

            migrationBuilder.RenameColumn(
                name: "AmpliacionDeclaracion",
                table: "CAT_AMPDEC",
                newName: "UAgencia");

            migrationBuilder.RenameColumn(
                name: "Agencia",
                table: "CAT_AMPDEC",
                newName: "Manifestacion");


            migrationBuilder.AddColumn<string>(
                name: "Hechos",
                table: "CAT_AMPDEC",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "Hechos",
                table: "CAT_AMPDEC");

            migrationBuilder.RenameColumn(
                name: "Usuario",
                table: "CAT_AMPDEC",
                newName: "u_Puesto");

            migrationBuilder.RenameColumn(
                name: "USubproc",
                table: "CAT_AMPDEC",
                newName: "u_Nombre");

            migrationBuilder.RenameColumn(
                name: "UPuesto",
                table: "CAT_AMPDEC",
                newName: "u_Modulo");

            migrationBuilder.RenameColumn(
                name: "UModulo",
                table: "CAT_AMPDEC",
                newName: "Distrito");

            migrationBuilder.RenameColumn(
                name: "UDistrito",
                table: "CAT_AMPDEC",
                newName: "DirSubProcu");

            migrationBuilder.RenameColumn(
                name: "UAgencia",
                table: "CAT_AMPDEC",
                newName: "AmpliacionDeclaracion");

            migrationBuilder.RenameColumn(
                name: "Manifestacion",
                table: "CAT_AMPDEC",
                newName: "Agencia");

            migrationBuilder.RenameColumn(
                name: "Fechasys",
                table: "CAT_AMPDEC",
                newName: "FechaAD");
        }
    }
}
