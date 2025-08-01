using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class GO110920RasgosFaciales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeñasParticulares",
                table: "CAT_MEDIAAFILIACION",
                newName: "TratamientosQuimicosCabello");

            migrationBuilder.RenameColumn(
                name: "SParticulares",
                table: "CAT_MEDIAAFILIACION",
                newName: "Tatuaje");

            migrationBuilder.RenameColumn(
                name: "Barba",
                table: "CAT_MEDIAAFILIACION",
                newName: "DentaduraCompleta");

            migrationBuilder.RenameColumn(
                name: "Anteojos",
                table: "CAT_MEDIAAFILIACION",
                newName: "Cicatriz");

            migrationBuilder.AddColumn<string>(
                name: "AdherenciaOreja",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Calvicie",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescripcionCicatriz",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescripcionTatuaje",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DientesAusentes",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormaOjo",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gruposanguineo",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImplantacionCeja",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtrasCaracteristicas",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PuntaNariz",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TamanoDental",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoDentadura",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoMenton",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TratamientoDental",
                table: "CAT_MEDIAAFILIACION",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "C_ADHERENCIA_OREJA",
                columns: table => new
                {
                    IdAdherencia_Oreja = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_ADHERENCIA_OREJA", x => x.IdAdherencia_Oreja);
                });

            migrationBuilder.CreateTable(
                name: "C_CALVICIE",
                columns: table => new
                {
                    IdCalvicie = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_CALVICIE", x => x.IdCalvicie);
                });

            migrationBuilder.CreateTable(
                name: "C_FORMA_OJO",
                columns: table => new
                {
                    IdForma_de_ojo = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_FORMA_OJO", x => x.IdForma_de_ojo);
                });

            migrationBuilder.CreateTable(
                name: "C_IMPLANTACION_CEJA",
                columns: table => new
                {
                    IdImplantacion_Ceja = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_IMPLANTACION_CEJA", x => x.IdImplantacion_Ceja);
                });

            migrationBuilder.CreateTable(
                name: "C_PUNTA_NARIZ",
                columns: table => new
                {
                    IdPunta_Nariz = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_PUNTA_NARIZ", x => x.IdPunta_Nariz);
                });

            migrationBuilder.CreateTable(
                name: "C_TAMANO_DENTAL",
                columns: table => new
                {
                    IdTamano_Dental = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TAMANO_DENTAL", x => x.IdTamano_Dental);
                });

            migrationBuilder.CreateTable(
                name: "C_TIPO_DENTADURA",
                columns: table => new
                {
                    IdTipo_Dentadura = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TIPO_DENTADURA", x => x.IdTipo_Dentadura);
                });

            migrationBuilder.CreateTable(
                name: "C_TIPO_MENTON",
                columns: table => new
                {
                    IdTipo_de_Menton = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TIPO_MENTON", x => x.IdTipo_de_Menton);
                });

            migrationBuilder.CreateTable(
                name: "C_TRATAMIENTO_DENTAL",
                columns: table => new
                {
                    IdTratamiento_Dental = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TRATAMIENTO_DENTAL", x => x.IdTratamiento_Dental);
                });

            migrationBuilder.CreateTable(
                name: "C_TRATAMIENTO_QUIMICO_CABELLO",
                columns: table => new
                {
                    IdTratamientos_Quimicos_Cabello = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TRATAMIENTO_QUIMICO_CABELLO", x => x.IdTratamientos_Quimicos_Cabello);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "C_ADHERENCIA_OREJA");

            migrationBuilder.DropTable(
                name: "C_CALVICIE");

            migrationBuilder.DropTable(
                name: "C_FORMA_OJO");

            migrationBuilder.DropTable(
                name: "C_IMPLANTACION_CEJA");

            migrationBuilder.DropTable(
                name: "C_PUNTA_NARIZ");

            migrationBuilder.DropTable(
                name: "C_TAMANO_DENTAL");

            migrationBuilder.DropTable(
                name: "C_TIPO_DENTADURA");

            migrationBuilder.DropTable(
                name: "C_TIPO_MENTON");

            migrationBuilder.DropTable(
                name: "C_TRATAMIENTO_DENTAL");

            migrationBuilder.DropTable(
                name: "C_TRATAMIENTO_QUIMICO_CABELLO");

            migrationBuilder.DropColumn(
                name: "AdherenciaOreja",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "Calvicie",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "DescripcionCicatriz",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "DescripcionTatuaje",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "DientesAusentes",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "FormaOjo",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "Gruposanguineo",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "ImplantacionCeja",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "OtrasCaracteristicas",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "PuntaNariz",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "TamanoDental",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "TipoDentadura",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "TipoMenton",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropColumn(
                name: "TratamientoDental",
                table: "CAT_MEDIAAFILIACION");

            migrationBuilder.RenameColumn(
                name: "TratamientosQuimicosCabello",
                table: "CAT_MEDIAAFILIACION",
                newName: "SeñasParticulares");

            migrationBuilder.RenameColumn(
                name: "Tatuaje",
                table: "CAT_MEDIAAFILIACION",
                newName: "SParticulares");

            migrationBuilder.RenameColumn(
                name: "DentaduraCompleta",
                table: "CAT_MEDIAAFILIACION",
                newName: "Barba");

            migrationBuilder.RenameColumn(
                name: "Cicatriz",
                table: "CAT_MEDIAAFILIACION",
                newName: "Anteojos");
        }
    }
}
