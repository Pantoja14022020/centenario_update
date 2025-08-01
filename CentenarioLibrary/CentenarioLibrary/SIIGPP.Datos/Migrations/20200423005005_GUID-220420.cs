using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIIGPP.Datos.Migrations
{
    public partial class GUID220420 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "C_ COLOR_OJOS",
                columns: table => new
                {
                    IdColorOjos = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_ COLOR_OJOS", x => x.IdColorOjos);
                });

            migrationBuilder.CreateTable(
                name: "C_ACTOSINVESTIGACION",
                columns: table => new
                {
                    IdActonvestigacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true),
                    Nomenclatura = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    RAutorizacion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_ACTOSINVESTIGACION", x => x.IdActonvestigacion);
                });

            migrationBuilder.CreateTable(
                name: "C_CANTIDAD_DE_CABELLO",
                columns: table => new
                {
                    IdCantidadCabello = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_CANTIDAD_DE_CABELLO", x => x.IdCantidadCabello);
                });

            migrationBuilder.CreateTable(
                name: "C_CLASIFICACIONPERSONA",
                columns: table => new
                {
                    IdClasificacionPersona = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_CLASIFICACIONPERSONA", x => x.IdClasificacionPersona);
                });

            migrationBuilder.CreateTable(
                name: "C_COLOR_DE_CABELLO",
                columns: table => new
                {
                    IdColorCabello = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_COLOR_DE_CABELLO", x => x.IdColorCabello);
                });

            migrationBuilder.CreateTable(
                name: "C_COMPLEXION",
                columns: table => new
                {
                    IdComplexion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_COMPLEXION", x => x.IdComplexion);
                });

            migrationBuilder.CreateTable(
                name: "C_CONFGLOBAL",
                columns: table => new
                {
                    IdConfGlobal = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Logo1 = table.Column<string>(nullable: true),
                    Logo2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_CONFGLOBAL", x => x.IdConfGlobal);
                });

            migrationBuilder.CreateTable(
                name: "C_DELITO",
                columns: table => new
                {
                    IdDelito = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    CveDelito = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    SuceptibleMASC = table.Column<bool>(nullable: false),
                    TipoMontoRobo = table.Column<bool>(nullable: false),
                    AltoImpacto = table.Column<bool>(nullable: false),
                    OfiNoOfi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_DELITO", x => x.IdDelito);
                });

            migrationBuilder.CreateTable(
                name: "C_DISCAPACIDAD",
                columns: table => new
                {
                    IdDiscapacidad = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_DISCAPACIDAD", x => x.IdDiscapacidad);
                });

            migrationBuilder.CreateTable(
                name: "C_DISTRITO",
                columns: table => new
                {
                    IdDistrito = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Contacto = table.Column<string>(nullable: true),
                    Clave = table.Column<string>(nullable: true),
                    StatusAsginacion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_DISTRITO", x => x.IdDistrito);
                });

            migrationBuilder.CreateTable(
                name: "C_DOCIDENTIFICACION",
                columns: table => new
                {
                    IdDocIdentificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_DOCIDENTIFICACION", x => x.IdDocIdentificacion);
                });

            migrationBuilder.CreateTable(
                name: "C_ESTADO",
                columns: table => new
                {
                    IdEstado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Abreviacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_ESTADO", x => x.IdEstado);
                });

            migrationBuilder.CreateTable(
                name: "C_ESTADO_CIVIL",
                columns: table => new
                {
                    IdECivil = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_ESTADO_CIVIL", x => x.IdECivil);
                });

            migrationBuilder.CreateTable(
                name: "C_FORMA_DE_CABELLO",
                columns: table => new
                {
                    IdFormaCabello = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_FORMA_DE_CABELLO", x => x.IdFormaCabello);
                });

            migrationBuilder.CreateTable(
                name: "C_FORMA_DE_CARA",
                columns: table => new
                {
                    IdFormaCara = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_FORMA_DE_CARA", x => x.IdFormaCara);
                });

            migrationBuilder.CreateTable(
                name: "C_FORMA_DE_CEJAS",
                columns: table => new
                {
                    IdFormaCejas = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_FORMA_DE_CEJAS", x => x.IdFormaCejas);
                });

            migrationBuilder.CreateTable(
                name: "C_FORMA_DE_MENTON",
                columns: table => new
                {
                    IdFormaMenton = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_FORMA_DE_MENTON", x => x.IdFormaMenton);
                });

            migrationBuilder.CreateTable(
                name: "C_GENERO",
                columns: table => new
                {
                    IdGenero = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_GENERO", x => x.IdGenero);
                });

            migrationBuilder.CreateTable(
                name: "C_GROSOR_DE_LABIOS",
                columns: table => new
                {
                    IdGrosorLabios = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_GROSOR_DE_LABIOS", x => x.IdGrosorLabios);
                });

            migrationBuilder.CreateTable(
                name: "C_INCOMPETENCIA",
                columns: table => new
                {
                    IdIncompetencia = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_INCOMPETENCIA", x => x.IdIncompetencia);
                });

            migrationBuilder.CreateTable(
                name: "C_LARGO_DE_CABELLO",
                columns: table => new
                {
                    IdLargoCabello = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_LARGO_DE_CABELLO", x => x.IdLargoCabello);
                });

            migrationBuilder.CreateTable(
                name: "C_LENGUA",
                columns: table => new
                {
                    IdLengua = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Clave = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_LENGUA", x => x.IdLengua);
                });

            migrationBuilder.CreateTable(
                name: "C_MEDIONOTIFICACION",
                columns: table => new
                {
                    IdMedioNotificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_MEDIONOTIFICACION", x => x.IdMedioNotificacion);
                });

            migrationBuilder.CreateTable(
                name: "C_NACIONALIDAD",
                columns: table => new
                {
                    IdNacionalidad = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Clave = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_NACIONALIDAD", x => x.IdNacionalidad);
                });

            migrationBuilder.CreateTable(
                name: "C_NIVELESTUDIOS",
                columns: table => new
                {
                    IdNivelEstudios = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_NIVELESTUDIOS", x => x.IdNivelEstudios);
                });

            migrationBuilder.CreateTable(
                name: "C_OCUPACION",
                columns: table => new
                {
                    IdOcupacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_OCUPACION", x => x.IdOcupacion);
                });

            migrationBuilder.CreateTable(
                name: "C_RELIGION",
                columns: table => new
                {
                    IdReligion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_RELIGION", x => x.IdReligion);
                });

            migrationBuilder.CreateTable(
                name: "C_SEXO",
                columns: table => new
                {
                    IdSexo = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Clave = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_SEXO", x => x.IdSexo);
                });

            migrationBuilder.CreateTable(
                name: "C_STATUSNUC",
                columns: table => new
                {
                    IdStatusNuc = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    NombreStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_STATUSNUC", x => x.IdStatusNuc);
                });

            migrationBuilder.CreateTable(
                name: "C_TAMAÑO_DE_BOCA",
                columns: table => new
                {
                    IdTamañoBoca = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TAMAÑO_DE_BOCA", x => x.IdTamañoBoca);
                });

            migrationBuilder.CreateTable(
                name: "C_TAMAÑO_NARIZ",
                columns: table => new
                {
                    IdTamañoNariz = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TAMAÑO_NARIZ", x => x.IdTamañoNariz);
                });

            migrationBuilder.CreateTable(
                name: "C_TEZ",
                columns: table => new
                {
                    IdTez = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TEZ", x => x.IdTez);
                });

            migrationBuilder.CreateTable(
                name: "C_TIPO_DE_CEJAS",
                columns: table => new
                {
                    IdTipoCejas = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TIPO_DE_CEJAS", x => x.IdTipoCejas);
                });

            migrationBuilder.CreateTable(
                name: "C_TIPO_DE_FRENTE",
                columns: table => new
                {
                    IdTipoFrente = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TIPO_DE_FRENTE", x => x.IdTipoFrente);
                });

            migrationBuilder.CreateTable(
                name: "C_TIPO_DE_NARIZ",
                columns: table => new
                {
                    IdTipoNariz = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TIPO_DE_NARIZ", x => x.IdTipoNariz);
                });

            migrationBuilder.CreateTable(
                name: "C_TIPO_DE_OREJAS",
                columns: table => new
                {
                    IdTipoOrejas = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TIPO_DE_OREJAS", x => x.IdTipoOrejas);
                });

            migrationBuilder.CreateTable(
                name: "C_TIPO_OJOS",
                columns: table => new
                {
                    IdTipoOjos = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TIPO_OJOS", x => x.IdTipoOjos);
                });

            migrationBuilder.CreateTable(
                name: "C_TIPOSREPRESENTANTES",
                columns: table => new
                {
                    IdTipoRepresentantes = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Tipo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TIPOSREPRESENTANTES", x => x.IdTipoRepresentantes);
                });

            migrationBuilder.CreateTable(
                name: "CA_ALMACENAMIENTO",
                columns: table => new
                {
                    IdAlmacenamiento = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    StatusActivo = table.Column<bool>(nullable: false),
                    StatusLLeno = table.Column<bool>(nullable: false),
                    EspacioDsiponible = table.Column<decimal>(nullable: false),
                    EspacioTotal = table.Column<decimal>(nullable: false),
                    EspacioUtilizado = table.Column<decimal>(nullable: false),
                    Porcentaje = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_ALMACENAMIENTO", x => x.IdAlmacenamiento);
                });

            migrationBuilder.CreateTable(
                name: "CA_ROL",
                columns: table => new
                {
                    IdRol = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Condicion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_ROL", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "CAR_CALIBRE",
                columns: table => new
                {
                    IdCalibre = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Numero = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAR_CALIBRE", x => x.IdCalibre);
                });

            migrationBuilder.CreateTable(
                name: "CAR_CLASIFICACIONARMAOB",
                columns: table => new
                {
                    IdClasificacionArma = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    NombreC = table.Column<string>(nullable: true),
                    Catalogo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAR_CLASIFICACIONARMAOB", x => x.IdClasificacionArma);
                });

            migrationBuilder.CreateTable(
                name: "CAR_MARCA_ARMA",
                columns: table => new
                {
                    IdMarcaArma = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAR_MARCA_ARMA", x => x.IdMarcaArma);
                });

            migrationBuilder.CreateTable(
                name: "CAT_MULTACITATORIO",
                columns: table => new
                {
                    IdMultaCitatorio = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_MULTACITATORIO", x => x.IdMultaCitatorio);
                });

            migrationBuilder.CreateTable(
                name: "CAT_PERSONA",
                columns: table => new
                {
                    IdPersona = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    StatusAnonimo = table.Column<bool>(nullable: false),
                    TipoPersona = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    StatusAlias = table.Column<bool>(nullable: false),
                    FechaNacimiento = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    EntidadFederativa = table.Column<string>(nullable: true),
                    DocIdentificacion = table.Column<string>(nullable: true),
                    CURP = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Genero = table.Column<string>(nullable: true),
                    EstadoCivil = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Telefono1 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Telefono2 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Medionotificacion = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Nacionalidad = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Ocupacion = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    NivelEstudio = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    Lengua = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Discapacidad = table.Column<bool>(type: "bit", nullable: false),
                    TipoDiscapacidad = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Parentesco = table.Column<string>(nullable: true),
                    DatosProtegidos = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_PERSONA", x => x.IdPersona);
                });

            migrationBuilder.CreateTable(
                name: "CD_CLAORDRES",
                columns: table => new
                {
                    IdClasificaOrdenResult = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CD_CLAORDRES", x => x.IdClasificaOrdenResult);
                });

            migrationBuilder.CreateTable(
                name: "CD_INTESIONDELITO",
                columns: table => new
                {
                    IdIntesionDelio = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CD_INTESIONDELITO", x => x.IdIntesionDelio);
                });

            migrationBuilder.CreateTable(
                name: "CD_RESULTADODELITO",
                columns: table => new
                {
                    IdResultadoDelito = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CD_RESULTADODELITO", x => x.IdResultadoDelito);
                });

            migrationBuilder.CreateTable(
                name: "CD_TIPO",
                columns: table => new
                {
                    IdTipo = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CD_TIPO", x => x.IdTipo);
                });

            migrationBuilder.CreateTable(
                name: "CD_TIPODECLARACION",
                columns: table => new
                {
                    IdTipoDeclaracion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CD_TIPODECLARACION", x => x.IdTipoDeclaracion);
                });

            migrationBuilder.CreateTable(
                name: "CD_TIPOFUERO",
                columns: table => new
                {
                    IdTipoFuero = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CD_TIPOFUERO", x => x.IdTipoFuero);
                });

            migrationBuilder.CreateTable(
                name: "CI_Institucion",
                columns: table => new
                {
                    IdInstitucion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Contacto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CI_Institucion", x => x.IdInstitucion);
                });

            migrationBuilder.CreateTable(
                name: "CI_Tipo",
                columns: table => new
                {
                    IdTipoIndicio = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CI_Tipo", x => x.IdTipoIndicio);
                });

            migrationBuilder.CreateTable(
                name: "CV_ANO",
                columns: table => new
                {
                    IdAno = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV_ANO", x => x.IdAno);
                });

            migrationBuilder.CreateTable(
                name: "CV_COLOR",
                columns: table => new
                {
                    IdColor = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV_COLOR", x => x.IdColor);
                });

            migrationBuilder.CreateTable(
                name: "CV_MARCA",
                columns: table => new
                {
                    IdMarca = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV_MARCA", x => x.IdMarca);
                });

            migrationBuilder.CreateTable(
                name: "CV_TIPOV",
                columns: table => new
                {
                    IdTipov = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV_TIPOV", x => x.IdTipov);
                });

            migrationBuilder.CreateTable(
                name: "PC_PANELCONTROL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Clave = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(800)", nullable: true),
                    Abrev = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Icono = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PC_PANELCONTROL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PI_PERSONAVISITA",
                columns: table => new
                {
                    IdPIPersonaVisita = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Nombre = table.Column<string>(nullable: true),
                    ApellidoP = table.Column<string>(nullable: true),
                    ApellidoM = table.Column<string>(nullable: true),
                    Edad = table.Column<int>(nullable: false),
                    Ocupacion = table.Column<string>(nullable: true),
                    Telefono1 = table.Column<int>(nullable: false),
                    Telefono2 = table.Column<int>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_PERSONAVISITA", x => x.IdPIPersonaVisita);
                });

            migrationBuilder.CreateTable(
                name: "SP_SERVICIOSPERICIALES",
                columns: table => new
                {
                    IdServicioPericial = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Codigo = table.Column<string>(nullable: true),
                    Servicio = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Requisitos = table.Column<string>(nullable: true),
                    EnMateriaDe = table.Column<string>(nullable: true),
                    AtencionVictimas = table.Column<bool>(nullable: false),
                    CancelableporJR = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SP_SERVICIOSPERICIALES", x => x.IdServicioPericial);
                });

            migrationBuilder.CreateTable(
                name: "C_DEPENDECIAS_DERIVACION",
                columns: table => new
                {
                    IdDDerivacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    DistritoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Contacto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_DEPENDECIAS_DERIVACION", x => x.IdDDerivacion);
                    table.ForeignKey(
                        name: "FK_C_DEPENDECIAS_DERIVACION_C_DISTRITO_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "C_DISTRITO",
                        principalColumn: "IdDistrito",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "C_DSP",
                columns: table => new
                {
                    IdDSP = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    DistritoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    NombreSubDir = table.Column<string>(nullable: true),
                    Clave = table.Column<string>(nullable: true),
                    Responsable = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    StatusInicioCarpeta = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_DSP", x => x.IdDSP);
                    table.ForeignKey(
                        name: "FK_C_DSP_C_DISTRITO_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "C_DISTRITO",
                        principalColumn: "IdDistrito",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_JUZGADOS_AGENCIAS",
                columns: table => new
                {
                    IdJuzgadoAgencia = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    DistritoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Encargado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_JUZGADOS_AGENCIAS", x => x.IdJuzgadoAgencia);
                    table.ForeignKey(
                        name: "FK_CAT_JUZGADOS_AGENCIAS_C_DISTRITO_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "C_DISTRITO",
                        principalColumn: "IdDistrito",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "C_MUNICIPIO",
                columns: table => new
                {
                    IdMunicipio = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Numero_Mpio = table.Column<int>(nullable: false),
                    EstadoId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_MUNICIPIO", x => x.IdMunicipio);
                    table.ForeignKey(
                        name: "FK_C_MUNICIPIO_C_ESTADO_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "C_ESTADO",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAR_ARMAOBJETO",
                columns: table => new
                {
                    IdArmaObjeto = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    nombreAO = table.Column<string>(nullable: true),
                    ClasificacionArmaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAR_ARMAOBJETO", x => x.IdArmaObjeto);
                    table.ForeignKey(
                        name: "FK_CAR_ARMAOBJETO_CAR_CLASIFICACIONARMAOB_ClasificacionArmaId",
                        column: x => x.ClasificacionArmaId,
                        principalTable: "CAR_CLASIFICACIONARMAOB",
                        principalColumn: "IdClasificacionArma",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_DIRECCION_PERSONAL",
                columns: table => new
                {
                    IdDPersonal = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Calle = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    NoInt = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    NoExt = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    EntreCalle1 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntreCalle2 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Referencia = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Pais = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    Municipio = table.Column<string>(type: "nvarchar(120)", nullable: true),
                    Localidad = table.Column<string>(type: "nvarchar(120)", nullable: true),
                    CP = table.Column<int>(type: "int", nullable: true),
                    PersonaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    lat = table.Column<string>(nullable: true),
                    lng = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_DIRECCION_PERSONAL", x => x.IdDPersonal);
                    table.ForeignKey(
                        name: "FK_CAT_DIRECCION_PERSONAL_CAT_PERSONA_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "CAT_PERSONA",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_DOCSPERSONA",
                columns: table => new
                {
                    IdDocumentoPersona = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    PersonaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoDocumento = table.Column<string>(nullable: true),
                    NombreDocumento = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Ruta = table.Column<string>(nullable: true),
                    Distrito = table.Column<string>(nullable: true),
                    DirSubProc = table.Column<string>(nullable: true),
                    Agencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_DOCSPERSONA", x => x.IdDocumentoPersona);
                    table.ForeignKey(
                        name: "FK_CAT_DOCSPERSONA_CAT_PERSONA_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "CAT_PERSONA",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CV_MODELO",
                columns: table => new
                {
                    IdModelo = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Dato = table.Column<string>(nullable: true),
                    MarcaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV_MODELO", x => x.IdModelo);
                    table.ForeignKey(
                        name: "FK_CV_MODELO_CV_MARCA_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "CV_MARCA",
                        principalColumn: "IdMarca",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_DIRECCION",
                columns: table => new
                {
                    IdDireccion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    PIPersonaVisitaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Calle = table.Column<string>(nullable: true),
                    NoExterior = table.Column<int>(nullable: false),
                    NoInterior = table.Column<int>(nullable: false),
                    Ecalle1 = table.Column<string>(nullable: true),
                    Ecalle2 = table.Column<string>(nullable: true),
                    Referencia = table.Column<string>(nullable: true),
                    Pais = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Municipio = table.Column<string>(nullable: true),
                    Localidad = table.Column<string>(nullable: true),
                    Cp = table.Column<int>(nullable: false),
                    Longitud = table.Column<string>(nullable: true),
                    Latitud = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_DIRECCION", x => x.IdDireccion);
                    table.ForeignKey(
                        name: "FK_PI_DIRECCION_PI_PERSONAVISITA_PIPersonaVisitaId",
                        column: x => x.PIPersonaVisitaId,
                        principalTable: "PI_PERSONAVISITA",
                        principalColumn: "IdPIPersonaVisita",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_FPERSONAS",
                columns: table => new
                {
                    IdFPersona = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    PIPersonaVisitaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoDocumento = table.Column<string>(nullable: true),
                    DescripcionDocumento = table.Column<string>(nullable: true),
                    FechaRegistro = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    Ruta = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_FPERSONAS", x => x.IdFPersona);
                    table.ForeignKey(
                        name: "FK_PI_FPERSONAS_PI_PERSONAVISITA_PIPersonaVisitaId",
                        column: x => x.PIPersonaVisitaId,
                        principalTable: "PI_PERSONAVISITA",
                        principalColumn: "IdPIPersonaVisita",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "C_AGENCIA",
                columns: table => new
                {
                    IdAgencia = table.Column<Guid>(nullable: false, defaultValueSql: "newId()"),
                    DSPId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Contacto = table.Column<string>(nullable: true),
                    Clave = table.Column<string>(nullable: true),
                    TipoServicio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_AGENCIA", x => x.IdAgencia);
                    table.ForeignKey(
                        name: "FK_C_AGENCIA_C_DSP_DSPId",
                        column: x => x.DSPId,
                        principalTable: "C_DSP",
                        principalColumn: "IdDSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "C_FISCALIAOESTADOS",
                columns: table => new
                {
                    IdFiscaliaOestado = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    EstadoIdEstado = table.Column<int>(nullable: true),
                    EstadoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    MunicipioIdMunicipio = table.Column<int>(nullable: true),
                    MunicipioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Contacto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_FISCALIAOESTADOS", x => x.IdFiscaliaOestado);
                    table.ForeignKey(
                        name: "FK_C_FISCALIAOESTADOS_C_ESTADO_EstadoIdEstado",
                        column: x => x.EstadoIdEstado,
                        principalTable: "C_ESTADO",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_FISCALIAOESTADOS_C_MUNICIPIO_MunicipioIdMunicipio",
                        column: x => x.MunicipioIdMunicipio,
                        principalTable: "C_MUNICIPIO",
                        principalColumn: "IdMunicipio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_LOCALIDAD",
                columns: table => new
                {
                    IdLocalidad = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MunicipioId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    CP = table.Column<int>(nullable: false),
                    Zona = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_LOCALIDAD", x => x.IdLocalidad);
                    table.ForeignKey(
                        name: "FK_C_LOCALIDAD_C_MUNICIPIO_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "C_MUNICIPIO",
                        principalColumn: "IdMunicipio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "C_MODULOSERVICIO",
                columns: table => new
                {
                    IdModuloServicio = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    AgenciaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Clave = table.Column<string>(nullable: true),
                    ServicioInterno = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_MODULOSERVICIO", x => x.IdModuloServicio);
                    table.ForeignKey(
                        name: "FK_C_MODULOSERVICIO_C_AGENCIA_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "C_AGENCIA",
                        principalColumn: "IdAgencia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NUC",
                columns: table => new
                {
                    idNuc = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Indicador = table.Column<string>(nullable: true),
                    DistritoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    CveDistrito = table.Column<string>(nullable: true),
                    DConsecutivo = table.Column<int>(nullable: false),
                    AgenciaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    CveAgencia = table.Column<string>(nullable: true),
                    AConsecutivo = table.Column<int>(nullable: false),
                    Año = table.Column<int>(nullable: false),
                    nucg = table.Column<string>(nullable: true),
                    StatusNUC = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NUC", x => x.idNuc);
                    table.ForeignKey(
                        name: "FK_NUC_C_AGENCIA_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "C_AGENCIA",
                        principalColumn: "IdAgencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NUC_C_DISTRITO_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "C_DISTRITO",
                        principalColumn: "IdDistrito",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RAC",
                columns: table => new
                {
                    idRac = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Indicador = table.Column<string>(nullable: true),
                    DistritoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    CveDistrito = table.Column<string>(nullable: true),
                    DConsecutivo = table.Column<int>(nullable: false),
                    AgenciaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    CveAgencia = table.Column<string>(nullable: true),
                    AConsecutivo = table.Column<int>(nullable: false),
                    Año = table.Column<int>(nullable: false),
                    racg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAC", x => x.idRac);
                    table.ForeignKey(
                        name: "FK_RAC_C_AGENCIA_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "C_AGENCIA",
                        principalColumn: "IdAgencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RAC_C_DISTRITO_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "C_DISTRITO",
                        principalColumn: "IdDistrito",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SP_ASP",
                columns: table => new
                {
                    IdASP = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    AgenciaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    ServicioPericialId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SP_ASP", x => x.IdASP);
                    table.ForeignKey(
                        name: "FK_SP_ASP_C_AGENCIA_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "C_AGENCIA",
                        principalColumn: "IdAgencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SP_ASP_SP_SERVICIOSPERICIALES_ServicioPericialId",
                        column: x => x.ServicioPericialId,
                        principalTable: "SP_SERVICIOSPERICIALES",
                        principalColumn: "IdServicioPericial",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CA_USUARIO",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    clave = table.Column<Guid>(nullable: false),
                    RolId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    usuario = table.Column<string>(nullable: true),
                    nombre = table.Column<string>(nullable: true),
                    direccion = table.Column<string>(nullable: true),
                    telefono = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    puesto = table.Column<string>(nullable: true),
                    password_hash = table.Column<byte[]>(nullable: true),
                    password_salt = table.Column<byte[]>(nullable: true),
                    condicion = table.Column<bool>(nullable: false),
                    Titular = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_USUARIO", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_CA_USUARIO_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CA_USUARIO_CA_ROL_RolId",
                        column: x => x.RolId,
                        principalTable: "CA_ROL",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JR_FACILITADORNOTIFICADOR",
                columns: table => new
                {
                    IdFacilitadorNotificador = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    StatusAsignado = table.Column<bool>(nullable: false),
                    StatusActivo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JR_FACILITADORNOTIFICADOR", x => x.IdFacilitadorNotificador);
                    table.ForeignKey(
                        name: "FK_JR_FACILITADORNOTIFICADOR_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_CMEDICOPR",
                columns: table => new
                {
                    IdCMedicoPR = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    PersonaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Nuc = table.Column<string>(nullable: true),
                    NOficio = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true),
                    FechaAsignacion = table.Column<DateTime>(nullable: false),
                    FechaUltimoStatus = table.Column<DateTime>(nullable: false),
                    NumeroAgencia = table.Column<string>(nullable: true),
                    TelefonoAgencia = table.Column<string>(nullable: true),
                    NodeSolicitud = table.Column<string>(nullable: true),
                    NumeroControl = table.Column<string>(nullable: true),
                    NumeroDistrito = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_CMEDICOPR", x => x.IdCMedicoPR);
                    table.ForeignKey(
                        name: "FK_PI_CMEDICOPR_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PI_CMEDICOPR_CAT_PERSONA_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "CAT_PERSONA",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_CMEDICOPSR",
                columns: table => new
                {
                    IdCMedicoPSR = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    ApellidoP = table.Column<string>(nullable: true),
                    ApellidoM = table.Column<string>(nullable: true),
                    NumUnicoRegistro = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true),
                    FechaAsignacion = table.Column<DateTime>(nullable: false),
                    FechaUltimoStatus = table.Column<DateTime>(nullable: false),
                    NumeroAgencia = table.Column<string>(nullable: true),
                    TelefonoAgencia = table.Column<string>(nullable: true),
                    NodeSolicitud = table.Column<string>(nullable: true),
                    NumeroControl = table.Column<string>(nullable: true),
                    NumeroDistrito = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_CMEDICOPSR", x => x.IdCMedicoPSR);
                    table.ForeignKey(
                        name: "FK_PI_CMEDICOPSR_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_IJ_ARRESTO",
                columns: table => new
                {
                    IdArresto = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaRecibido = table.Column<string>(nullable: true),
                    Oficio = table.Column<string>(nullable: true),
                    CausaPenal = table.Column<string>(nullable: true),
                    NomArrestado = table.Column<string>(nullable: true),
                    TiempoH = table.Column<string>(nullable: true),
                    JuezSolicita = table.Column<string>(nullable: true),
                    GpoAsignado = table.Column<string>(nullable: true),
                    Notas = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true),
                    FAsignacion = table.Column<DateTime>(nullable: false),
                    FFinalizacion = table.Column<DateTime>(nullable: false),
                    FUltmimoStatus = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    FechasComparescencia = table.Column<DateTime>(nullable: false),
                    FechaCumplimiento = table.Column<DateTime>(nullable: false),
                    FechaRecepcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_IJ_ARRESTO", x => x.IdArresto);
                    table.ForeignKey(
                        name: "FK_PI_IJ_ARRESTO_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_IJ_BUSQUEDA_DOMICILIO",
                columns: table => new
                {
                    IdBusquedaDomicilio = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Recepcion = table.Column<string>(nullable: true),
                    Oficio = table.Column<string>(nullable: true),
                    CausaPenal = table.Column<string>(nullable: true),
                    Juzgado = table.Column<string>(nullable: true),
                    Concepto = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true),
                    FAsignacion = table.Column<DateTime>(nullable: false),
                    FFinalizacion = table.Column<DateTime>(nullable: false),
                    FUltmimoStatus = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    FechasComparescencia = table.Column<DateTime>(nullable: false),
                    FechaCumplimiento = table.Column<DateTime>(nullable: false),
                    FechaRecepcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_IJ_BUSQUEDA_DOMICILIO", x => x.IdBusquedaDomicilio);
                    table.ForeignKey(
                        name: "FK_PI_IJ_BUSQUEDA_DOMICILIO_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_IJ_COMPARECENCIA_ELEMENTOS",
                columns: table => new
                {
                    IdCompElementos = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Comparecencia = table.Column<string>(nullable: true),
                    Elemento = table.Column<string>(nullable: true),
                    FComparecencia = table.Column<string>(nullable: true),
                    Hora = table.Column<string>(nullable: true),
                    Noficio = table.Column<int>(nullable: false),
                    CausaPenal = table.Column<string>(nullable: true),
                    AnteAutoridad = table.Column<string>(nullable: true),
                    Notas = table.Column<string>(nullable: true),
                    Recibe = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true),
                    FAsignacion = table.Column<DateTime>(nullable: false),
                    FFinalizacion = table.Column<DateTime>(nullable: false),
                    FUltmimoStatus = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    FechasComparescencia = table.Column<DateTime>(nullable: false),
                    FechaCumplimiento = table.Column<DateTime>(nullable: false),
                    FechaRecepcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_IJ_COMPARECENCIA_ELEMENTOS", x => x.IdCompElementos);
                    table.ForeignKey(
                        name: "FK_PI_IJ_COMPARECENCIA_ELEMENTOS_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_IJ_EXHORTO",
                columns: table => new
                {
                    IdExhorto = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Recepcion = table.Column<string>(nullable: true),
                    Oficio = table.Column<string>(nullable: true),
                    Juzgado = table.Column<string>(nullable: true),
                    PerAPresentar = table.Column<string>(nullable: true),
                    Delito = table.Column<string>(nullable: true),
                    Agraviado = table.Column<string>(nullable: true),
                    CausaPenal = table.Column<string>(nullable: true),
                    Asignado = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true),
                    FAsignacion = table.Column<DateTime>(nullable: false),
                    FFinalizacion = table.Column<DateTime>(nullable: false),
                    FUltmimoStatus = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    FechasComparescencia = table.Column<DateTime>(nullable: false),
                    FechaCumplimiento = table.Column<DateTime>(nullable: false),
                    ActosRealizar = table.Column<string>(nullable: true),
                    FechaRecepcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_IJ_EXHORTO", x => x.IdExhorto);
                    table.ForeignKey(
                        name: "FK_PI_IJ_EXHORTO_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_IJ_ORDEN_APRENSION",
                columns: table => new
                {
                    IdOrdenAprension = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Juzgado = table.Column<string>(nullable: true),
                    OficialiaDPartes = table.Column<string>(nullable: true),
                    CausaPenal = table.Column<string>(nullable: true),
                    Oficio = table.Column<string>(nullable: true),
                    ARC = table.Column<string>(nullable: true),
                    Imputado = table.Column<string>(nullable: true),
                    Delito = table.Column<string>(nullable: true),
                    Agraviado = table.Column<string>(nullable: true),
                    Recibida = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true),
                    FAsignacion = table.Column<DateTime>(nullable: false),
                    FFinalizacion = table.Column<DateTime>(nullable: false),
                    FUltmimoStatus = table.Column<DateTime>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    FechasComparescencia = table.Column<DateTime>(nullable: false),
                    FechaCumplimiento = table.Column<DateTime>(nullable: false),
                    FechaRecepcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_IJ_ORDEN_APRENSION", x => x.IdOrdenAprension);
                    table.ForeignKey(
                        name: "FK_PI_IJ_ORDEN_APRENSION_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_IJ_PRESENTACIONES_Y_C",
                columns: table => new
                {
                    IdPresentacionesYC = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Recepcion = table.Column<string>(nullable: true),
                    Oficio = table.Column<string>(nullable: true),
                    CausaPenal = table.Column<string>(nullable: true),
                    Dependencia = table.Column<string>(nullable: true),
                    PerAPresentar = table.Column<string>(nullable: true),
                    FdePresentacion = table.Column<string>(nullable: true),
                    Hora = table.Column<string>(nullable: true),
                    Asignado = table.Column<string>(nullable: true),
                    CumplidaOInf = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true),
                    FAsignacion = table.Column<DateTime>(nullable: false),
                    FFinalizacion = table.Column<DateTime>(nullable: false),
                    FUltmimoStatus = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    FechasComparescencia = table.Column<DateTime>(nullable: false),
                    FechaCumplimiento = table.Column<DateTime>(nullable: false),
                    FechaRecepcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_IJ_PRESENTACIONES_Y_C", x => x.IdPresentacionesYC);
                    table.ForeignKey(
                        name: "FK_PI_IJ_PRESENTACIONES_Y_C_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_IJ_REQUERIMIENTOS_VARIOS",
                columns: table => new
                {
                    IdReqVarios = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Recepcion = table.Column<string>(nullable: true),
                    Oficio = table.Column<string>(nullable: true),
                    CausaPenal = table.Column<string>(nullable: true),
                    Juzgado = table.Column<string>(nullable: true),
                    Consepto = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Hora = table.Column<string>(nullable: true),
                    Dia = table.Column<string>(nullable: true),
                    Asignado = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true),
                    FAsignacion = table.Column<DateTime>(nullable: false),
                    FFinalizacion = table.Column<DateTime>(nullable: false),
                    FUltmimoStatus = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    FechasComparescencia = table.Column<DateTime>(nullable: false),
                    FechaCumplimiento = table.Column<DateTime>(nullable: false),
                    FechaRecepcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_IJ_REQUERIMIENTOS_VARIOS", x => x.IdReqVarios);
                    table.ForeignKey(
                        name: "FK_PI_IJ_REQUERIMIENTOS_VARIOS_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_IJ_TRASLADOSYCUSTODIAS",
                columns: table => new
                {
                    IdTrasladosYC = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Recepcion = table.Column<string>(nullable: true),
                    Oficio = table.Column<string>(nullable: true),
                    CausaPenal = table.Column<string>(nullable: true),
                    Juzgado = table.Column<string>(nullable: true),
                    PersonaaTras = table.Column<string>(nullable: true),
                    Lugar = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    Hora = table.Column<string>(nullable: true),
                    Asignado = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true),
                    FAsignacion = table.Column<DateTime>(nullable: false),
                    FFinalizacion = table.Column<DateTime>(nullable: false),
                    FUltmimoStatus = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    FechasComparescencia = table.Column<DateTime>(nullable: false),
                    FechaCumplimiento = table.Column<DateTime>(nullable: false),
                    FechaRecepcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_IJ_TRASLADOSYCUSTODIAS", x => x.IdTrasladosYC);
                    table.ForeignKey(
                        name: "FK_PI_IJ_TRASLADOSYCUSTODIAS_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_RATENCON",
                columns: table => new
                {
                    IdRAtencion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    FechaHoraRegistro = table.Column<DateTime>(type: "DateTime", nullable: false),
                    FechaHoraAtencion = table.Column<DateTime>(type: "DateTime", nullable: true),
                    FechaHoraCierre = table.Column<DateTime>(type: "DateTime", nullable: true),
                    u_Nombre = table.Column<string>(type: "NvarChar(100)", nullable: true),
                    u_Puesto = table.Column<string>(nullable: true),
                    u_Modulo = table.Column<string>(nullable: true),
                    DistritoInicial = table.Column<string>(type: "NvarChar(50)", nullable: true),
                    DirSubProcuInicial = table.Column<string>(nullable: true),
                    AgenciaInicial = table.Column<string>(type: "NvarChar(50)", nullable: true),
                    StatusAtencion = table.Column<bool>(type: "bit", nullable: false),
                    StatusRegistro = table.Column<bool>(type: "bit", nullable: false),
                    MedioDenuncia = table.Column<string>(nullable: true),
                    ContencionVicitma = table.Column<bool>(nullable: false),
                    racId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ModuloServicio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_RATENCON", x => x.IdRAtencion);
                    table.ForeignKey(
                        name: "FK_CAT_RATENCON_RAC_racId",
                        column: x => x.racId,
                        principalTable: "RAC",
                        principalColumn: "idRac",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CA_PANELUSUARIO",
                columns: table => new
                {
                    IdPanelUsuario = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    UsuarioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    PanelControlId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_PANELUSUARIO", x => x.IdPanelUsuario);
                    table.ForeignKey(
                        name: "FK_CA_PANELUSUARIO_PC_PANELCONTROL_PanelControlId",
                        column: x => x.PanelControlId,
                        principalTable: "PC_PANELCONTROL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CA_PANELUSUARIO_CA_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "CA_USUARIO",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAprhensionBitacoras",
                columns: table => new
                {
                    IdOAprhensionBitacora = table.Column<Guid>(nullable: false),
                    OrdenAprensionId = table.Column<Guid>(nullable: false),
                    Texto = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAprhensionBitacoras", x => x.IdOAprhensionBitacora);
                    table.ForeignKey(
                        name: "FK_OAprhensionBitacoras_PI_IJ_ORDEN_APRENSION_OrdenAprensionId",
                        column: x => x.OrdenAprensionId,
                        principalTable: "PI_IJ_ORDEN_APRENSION",
                        principalColumn: "IdOrdenAprension",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_ARCHIVOSOAPRENSION",
                columns: table => new
                {
                    IdArchivosOAprension = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    OrdenAprensionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoDocumento = table.Column<string>(nullable: true),
                    DescripcionDocumento = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    Ruta = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_ARCHIVOSOAPRENSION", x => x.IdArchivosOAprension);
                    table.ForeignKey(
                        name: "FK_PI_ARCHIVOSOAPRENSION_PI_IJ_ORDEN_APRENSION_OrdenAprensionId",
                        column: x => x.OrdenAprensionId,
                        principalTable: "PI_IJ_ORDEN_APRENSION",
                        principalColumn: "IdOrdenAprension",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_ARCHIVOSCOMPARECENCIA",
                columns: table => new
                {
                    IdrchivosComparecencia = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    PresentacionesYCId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoDocumento = table.Column<string>(nullable: true),
                    DescripcionDocumento = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    Ruta = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_ARCHIVOSCOMPARECENCIA", x => x.IdrchivosComparecencia);
                    table.ForeignKey(
                        name: "FK_PI_ARCHIVOSCOMPARECENCIA_PI_IJ_PRESENTACIONES_Y_C_PresentacionesYCId",
                        column: x => x.PresentacionesYCId,
                        principalTable: "PI_IJ_PRESENTACIONES_Y_C",
                        principalColumn: "IdPresentacionesYC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_RAP",
                columns: table => new
                {
                    IdRAP = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RAtencionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    PersonaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ClasificacionPersona = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    PInicio = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_RAP", x => x.IdRAP);
                    table.ForeignKey(
                        name: "FK_CAT_RAP_CAT_PERSONA_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "CAT_PERSONA",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_RAP_CAT_RATENCON_RAtencionId",
                        column: x => x.RAtencionId,
                        principalTable: "CAT_RATENCON",
                        principalColumn: "IdRAtencion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_RHECHO",
                columns: table => new
                {
                    IdRHecho = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RAtencionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Agenciaid = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaReporte = table.Column<string>(nullable: true),
                    FechaHoraSuceso = table.Column<DateTime>(type: "DateTime", nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    RBreve = table.Column<string>(nullable: true),
                    NarrativaHechos = table.Column<string>(nullable: true),
                    NucId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    FechaElevaNuc = table.Column<DateTime>(type: "DateTime", nullable: true),
                    FechaElevaNuc2 = table.Column<DateTime>(nullable: false),
                    Vanabim = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_RHECHO", x => x.IdRHecho);
                    table.ForeignKey(
                        name: "FK_CAT_RHECHO_C_AGENCIA_Agenciaid",
                        column: x => x.Agenciaid,
                        principalTable: "C_AGENCIA",
                        principalColumn: "IdAgencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_RHECHO_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CAT_RHECHO_NUC_NucId",
                        column: x => x.NucId,
                        principalTable: "NUC",
                        principalColumn: "idNuc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_RHECHO_CAT_RATENCON_RAtencionId",
                        column: x => x.RAtencionId,
                        principalTable: "CAT_RATENCON",
                        principalColumn: "IdRAtencion",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CAT_TURNO",
                columns: table => new
                {
                    IdTurno = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Serie = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    NoTurno = table.Column<int>(type: "int", nullable: false),
                    FechaHoraInicio = table.Column<DateTime>(type: "DateTime", nullable: false),
                    FechaHoraFin = table.Column<DateTime>(type: "DateTime", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    RAtencionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    AgenciaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    StatusReAsignado = table.Column<bool>(nullable: false),
                    Modulo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_TURNO", x => x.IdTurno);
                    table.ForeignKey(
                        name: "FK_CAT_TURNO_C_AGENCIA_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "C_AGENCIA",
                        principalColumn: "IdAgencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_TURNO_CAT_RATENCON_RAtencionId",
                        column: x => x.RAtencionId,
                        principalTable: "CAT_RATENCON",
                        principalColumn: "IdRAtencion",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CAT_DATO_PROTEGIDO",
                columns: table => new
                {
                    IdDatosProtegidos = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RAPId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    APaterno = table.Column<string>(nullable: true),
                    AMaterno = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<string>(nullable: true),
                    CURP = table.Column<string>(nullable: true),
                    RFC = table.Column<string>(nullable: true),
                    Rutadocumento = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_DATO_PROTEGIDO", x => x.IdDatosProtegidos);
                    table.ForeignKey(
                        name: "FK_CAT_DATO_PROTEGIDO_CAT_RAP_RAPId",
                        column: x => x.RAPId,
                        principalTable: "CAT_RAP",
                        principalColumn: "IdRAP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_DIRECCION_ESCUCHA",
                columns: table => new
                {
                    IdDEscucha = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Calle = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    NoInt = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    NoExt = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    EntreCalle1 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntreCalle2 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Referencia = table.Column<string>(nullable: true),
                    Pais = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    Municipio = table.Column<string>(type: "nvarchar(120)", nullable: true),
                    Localidad = table.Column<string>(type: "nvarchar(120)", nullable: true),
                    CP = table.Column<int>(nullable: true),
                    lat = table.Column<string>(nullable: true),
                    lng = table.Column<string>(nullable: true),
                    RAPId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_DIRECCION_ESCUCHA", x => x.IdDEscucha);
                    table.ForeignKey(
                        name: "FK_CAT_DIRECCION_ESCUCHA_CAT_RAP_RAPId",
                        column: x => x.RAPId,
                        principalTable: "CAT_RAP",
                        principalColumn: "IdRAP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_AMPDEC",
                columns: table => new
                {
                    idAmpliacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    HechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    PersonaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaAD = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    ClasificacionPersona = table.Column<string>(nullable: true),
                    AmpliacionDeclaracion = table.Column<string>(nullable: true),
                    TRepresentantes = table.Column<string>(nullable: true),
                    Distrito = table.Column<string>(nullable: true),
                    DirSubProcu = table.Column<string>(nullable: true),
                    Agencia = table.Column<string>(nullable: true),
                    u_Nombre = table.Column<string>(nullable: true),
                    u_Puesto = table.Column<string>(nullable: true),
                    u_Modulo = table.Column<string>(nullable: true),
                    Numerooficio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_AMPDEC", x => x.idAmpliacion);
                    table.ForeignKey(
                        name: "FK_CAT_AMPDEC_CAT_RHECHO_HechoId",
                        column: x => x.HechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_AMPDEC_CAT_PERSONA_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "CAT_PERSONA",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ARCHIVOS",
                columns: table => new
                {
                    IdArchivos = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoDocumento = table.Column<string>(nullable: true),
                    NombreDocumento = table.Column<string>(nullable: true),
                    DescripcionDocumento = table.Column<string>(nullable: true),
                    Ruta = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ARCHIVOS", x => x.IdArchivos);
                    table.ForeignKey(
                        name: "FK_CAT_ARCHIVOS_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ARMA",
                columns: table => new
                {
                    IdRarma = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoAr = table.Column<string>(nullable: true),
                    NombreAr = table.Column<string>(nullable: true),
                    DescripcionAr = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(nullable: false),
                    FechaRegistro = table.Column<string>(nullable: true),
                    Distrito = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Subproc = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Agencia = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Puesto = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Modulo = table.Column<string>(nullable: true),
                    Matricula = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    Calibre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ARMA", x => x.IdRarma);
                    table.ForeignKey(
                        name: "FK_CAT_ARMA_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_BITACORA",
                columns: table => new
                {
                    IdBitacora = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    Tipo = table.Column<string>(nullable: true),
                    Descipcion = table.Column<string>(nullable: true),
                    Distrito = table.Column<string>(nullable: true),
                    Dirsubproc = table.Column<string>(nullable: true),
                    Agencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    Fechareporte = table.Column<string>(nullable: true),
                    Fechasis = table.Column<DateTime>(type: "DateTime", nullable: true),
                    rHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    IdPersona = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Numerooficio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_BITACORA", x => x.IdBitacora);
                    table.ForeignKey(
                        name: "FK_CAT_BITACORA_CAT_PERSONA_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "CAT_PERSONA",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_BITACORA_CAT_RHECHO_rHechoId",
                        column: x => x.rHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_C5FORMATOS",
                columns: table => new
                {
                    IdC5 = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    NumeroOficio = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    FechaStatus = table.Column<string>(nullable: true),
                    HoraStatus = table.Column<string>(nullable: true),
                    Encargadoc5 = table.Column<string>(nullable: true),
                    Puestoc5 = table.Column<string>(nullable: true),
                    Direccion5 = table.Column<string>(nullable: true),
                    Agentequerecibe = table.Column<string>(nullable: true),
                    NumtelefonicoS = table.Column<string>(nullable: true),
                    CorreoElecS = table.Column<string>(nullable: true),
                    Op1 = table.Column<bool>(nullable: false),
                    Op2 = table.Column<bool>(nullable: false),
                    Op3 = table.Column<bool>(nullable: false),
                    Op4 = table.Column<bool>(nullable: false),
                    Op5 = table.Column<bool>(nullable: false),
                    Op5Texto = table.Column<string>(nullable: true),
                    Op6 = table.Column<bool>(nullable: false),
                    Op7 = table.Column<bool>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    RazonDescripcion = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    UUsuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_C5FORMATOS", x => x.IdC5);
                    table.ForeignKey(
                        name: "FK_CAT_C5FORMATOS_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_CONDIMPPRO",
                columns: table => new
                {
                    idConImpProceso = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    PersonaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ConduccionImputadoProceso = table.Column<string>(nullable: true),
                    FechaHoraCitacion = table.Column<DateTime>(type: "DateTime", nullable: false),
                    FechahoraComparecencia = table.Column<DateTime>(type: "DateTime", nullable: false),
                    OrdeAprehension = table.Column<string>(nullable: true),
                    FechaHoraAudienciaOrdenAprehencion = table.Column<string>(nullable: true),
                    AutoridadEjecutora = table.Column<string>(nullable: true),
                    ResultadoOrdenAprehension = table.Column<string>(nullable: true),
                    FechaHoraEjecucionOrdenAprehecion = table.Column<DateTime>(type: "DateTime", nullable: false),
                    FechaHoraCancelacionOrdenAprehecion = table.Column<DateTime>(type: "DateTime", nullable: false),
                    OrdenReaprehesion = table.Column<bool>(nullable: false),
                    PlazoResolverSituacionJuridica = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Distrito = table.Column<string>(nullable: true),
                    DirSubProc = table.Column<string>(nullable: true),
                    Agencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_CONDIMPPRO", x => x.idConImpProceso);
                    table.ForeignKey(
                        name: "FK_CAT_CONDIMPPRO_CAT_PERSONA_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "CAT_PERSONA",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_CONDIMPPRO_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_DATOSRELACIONADOS",
                columns: table => new
                {
                    IdDatosRelacionados = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_DATOSRELACIONADOS", x => x.IdDatosRelacionados);
                    table.ForeignKey(
                        name: "FK_CAT_DATOSRELACIONADOS_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_DIRECCION_DELITO",
                columns: table => new
                {
                    IdDDelito = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    LugarEspecifico = table.Column<string>(nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    NoInt = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    NoExt = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    EntreCalle1 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EntreCalle2 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Referencia = table.Column<string>(nullable: true),
                    Pais = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    Municipio = table.Column<string>(type: "nvarchar(120)", nullable: true),
                    Localidad = table.Column<string>(type: "nvarchar(120)", nullable: true),
                    CP = table.Column<int>(nullable: true),
                    lat = table.Column<string>(nullable: true),
                    lng = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_DIRECCION_DELITO", x => x.IdDDelito);
                    table.ForeignKey(
                        name: "FK_CAT_DIRECCION_DELITO_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_INCOMPETENCIA",
                columns: table => new
                {
                    IdInconpentencia = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TextoFinal = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UUAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    RBreve = table.Column<string>(nullable: true),
                    Dependencia = table.Column<string>(nullable: true),
                    Articulos = table.Column<string>(nullable: true),
                    NumeroOficio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_INCOMPETENCIA", x => x.IdInconpentencia);
                    table.ForeignKey(
                        name: "FK_CAT_INCOMPETENCIA_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_INDICIOS",
                columns: table => new
                {
                    IdIndicio = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoIndicio = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    QIniciaCadena = table.Column<string>(nullable: true),
                    InstitucionQI = table.Column<string>(nullable: true),
                    UltimaUbicacion = table.Column<string>(nullable: true),
                    Distrito = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Subproc = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Agencia = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Puesto = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Modulo = table.Column<string>(nullable: true),
                    Matricula = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    Calibre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_INDICIOS", x => x.IdIndicio);
                    table.ForeignKey(
                        name: "FK_CAT_INDICIOS_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_MEDIAAFILIACION",
                columns: table => new
                {
                    idMediaAfiliacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    PersonaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Complexion = table.Column<string>(nullable: true),
                    Peso = table.Column<decimal>(nullable: false),
                    Estatura = table.Column<decimal>(nullable: false),
                    FormaCara = table.Column<string>(nullable: true),
                    ColoOjos = table.Column<string>(nullable: true),
                    Tez = table.Column<string>(nullable: true),
                    FormaCabello = table.Column<string>(nullable: true),
                    ColorCabello = table.Column<string>(nullable: true),
                    LargoCabello = table.Column<string>(nullable: true),
                    TamañoNariz = table.Column<string>(nullable: true),
                    TipoNariz = table.Column<string>(nullable: true),
                    GrosorLabios = table.Column<string>(nullable: true),
                    TipoFrente = table.Column<string>(nullable: true),
                    Cejas = table.Column<string>(nullable: true),
                    TipoCejas = table.Column<string>(nullable: true),
                    Barba = table.Column<bool>(nullable: false),
                    TamañoBoca = table.Column<string>(nullable: true),
                    TamañoOrejas = table.Column<string>(nullable: true),
                    FormaMenton = table.Column<string>(nullable: true),
                    TipoOjo = table.Column<string>(nullable: true),
                    Anteojos = table.Column<bool>(nullable: false),
                    SParticulares = table.Column<bool>(nullable: false),
                    SeñasParticulares = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Distrito = table.Column<string>(nullable: true),
                    DirSubProc = table.Column<string>(nullable: true),
                    Agencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    Numerooficio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_MEDIAAFILIACION", x => x.idMediaAfiliacion);
                    table.ForeignKey(
                        name: "FK_CAT_MEDIAAFILIACION_CAT_PERSONA_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "CAT_PERSONA",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_MEDIAAFILIACION_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_MEDIDASCAUTELARES",
                columns: table => new
                {
                    IdMedCautelares = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    PersonaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    MedidaCautelar = table.Column<string>(nullable: true),
                    Tiempo = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaTermino = table.Column<DateTime>(nullable: false),
                    FechaSys = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Distrito = table.Column<string>(nullable: true),
                    DirSubProc = table.Column<string>(nullable: true),
                    Agencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_MEDIDASCAUTELARES", x => x.IdMedCautelares);
                    table.ForeignKey(
                        name: "FK_CAT_MEDIDASCAUTELARES_CAT_PERSONA_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "CAT_PERSONA",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_MEDIDASCAUTELARES_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_MEDIDASPROTECCION",
                columns: table => new
                {
                    IdMProteccion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Victima = table.Column<string>(nullable: true),
                    Imputado = table.Column<string>(nullable: true),
                    Lugar = table.Column<string>(nullable: true),
                    Fechahora = table.Column<string>(nullable: true),
                    Agente = table.Column<string>(nullable: true),
                    Nuc = table.Column<string>(nullable: true),
                    Delito = table.Column<string>(nullable: true),
                    Narrativa = table.Column<string>(nullable: true),
                    Domicilio = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    MedidaProtecion = table.Column<string>(nullable: true),
                    Duracion = table.Column<int>(nullable: false),
                    Institucionejec = table.Column<string>(nullable: true),
                    Agencia = table.Column<string>(nullable: true),
                    Nomedidas = table.Column<string>(nullable: true),
                    Destinatarion = table.Column<string>(nullable: true),
                    Domicilion = table.Column<string>(nullable: true),
                    FInicio = table.Column<string>(nullable: true),
                    FVencimiento = table.Column<string>(nullable: true),
                    Ampliacion = table.Column<bool>(nullable: false),
                    FAmpliacion = table.Column<string>(nullable: true),
                    FterminoAm = table.Column<string>(nullable: true),
                    Ratificacion = table.Column<string>(nullable: true),
                    Distrito = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Subproc = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    UAgencia = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Puesto = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Modulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    Textofinal = table.Column<string>(nullable: true),
                    Textofinal2 = table.Column<string>(nullable: true),
                    Detalleactivo = table.Column<bool>(nullable: false),
                    Textofinaldetalle = table.Column<string>(nullable: true),
                    NumeroOficio = table.Column<string>(nullable: true),
                    NumeroOficioN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_MEDIDASPROTECCION", x => x.IdMProteccion);
                    table.ForeignKey(
                        name: "FK_CAT_MEDIDASPROTECCION_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_NOEJERCICIOAPENAL",
                columns: table => new
                {
                    IdNoEjercicioApenal = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Op1 = table.Column<bool>(nullable: false),
                    Op2 = table.Column<bool>(nullable: false),
                    Op3 = table.Column<bool>(nullable: false),
                    Op4 = table.Column<bool>(nullable: false),
                    FechaEntrevista = table.Column<DateTime>(nullable: false),
                    FechaHecho = table.Column<DateTime>(nullable: false),
                    Delito = table.Column<string>(nullable: true),
                    Denunciante = table.Column<string>(nullable: true),
                    ArticulosCp = table.Column<string>(nullable: true),
                    OficioQuerella = table.Column<bool>(nullable: false),
                    ArticuloLegislador = table.Column<string>(nullable: true),
                    TipoQuerella = table.Column<bool>(nullable: false),
                    TFechaQuerella = table.Column<DateTime>(nullable: false),
                    FFechaLimiteQI = table.Column<DateTime>(nullable: false),
                    FFechaInterposicionMp = table.Column<DateTime>(nullable: false),
                    PunibilidadMinimo = table.Column<string>(nullable: true),
                    PunibilidadMaximo = table.Column<string>(nullable: true),
                    TipoPrescripcion = table.Column<bool>(nullable: false),
                    Tipopena = table.Column<bool>(nullable: false),
                    Ttipodelito = table.Column<string>(nullable: true),
                    TAccionPenalFecha = table.Column<DateTime>(nullable: false),
                    TUltimoActoInvestigacion = table.Column<string>(nullable: true),
                    TOperacionAritmeticaFecha = table.Column<DateTime>(nullable: false),
                    DiasTranscurridos = table.Column<int>(nullable: false),
                    MesesTranscurridos = table.Column<int>(nullable: false),
                    AnosTranscurridos = table.Column<int>(nullable: false),
                    FechaUltimoActo = table.Column<DateTime>(nullable: false),
                    FechaEjercerAcion = table.Column<DateTime>(nullable: false),
                    FechaResolucionconsulta = table.Column<DateTime>(nullable: false),
                    Art123 = table.Column<bool>(nullable: false),
                    PerdonOfendido = table.Column<bool>(nullable: false),
                    PONumeroFirmas = table.Column<int>(nullable: false),
                    PONombreFirmas = table.Column<string>(nullable: true),
                    POFechaPerdon = table.Column<DateTime>(nullable: false),
                    POViolenciaMuMeOG = table.Column<bool>(nullable: false),
                    POVReparacionDaño = table.Column<string>(nullable: true),
                    POVFITratamiento = table.Column<DateTime>(nullable: false),
                    POVFFTratamiento = table.Column<DateTime>(nullable: false),
                    ExcluyenteDelito = table.Column<bool>(nullable: false),
                    EDHipotesis = table.Column<string>(nullable: true),
                    EDRazonar = table.Column<string>(nullable: true),
                    NoConstituyeDelito = table.Column<bool>(nullable: false),
                    NCDElementospenal = table.Column<string>(nullable: true),
                    NCDRazonar = table.Column<string>(nullable: true),
                    ExentoResponsabilidadPenal = table.Column<bool>(nullable: false),
                    ERPCausaInculpabilidad = table.Column<string>(nullable: true),
                    ERPRazonar = table.Column<string>(nullable: true),
                    ImputadoFallecio = table.Column<bool>(nullable: false),
                    IFImputadoNombre = table.Column<string>(nullable: true),
                    IFFechaFallecimiento = table.Column<string>(nullable: true),
                    NumeroOficio = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    UUsuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_NOEJERCICIOAPENAL", x => x.IdNoEjercicioApenal);
                    table.ForeignKey(
                        name: "FK_CAT_NOEJERCICIOAPENAL_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_OFICIO",
                columns: table => new
                {
                    IdOficios = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Texto = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    NumeroOficio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_OFICIO", x => x.IdOficios);
                    table.ForeignKey(
                        name: "FK_CAT_OFICIO_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_RACTOSINVESTIGACION",
                columns: table => new
                {
                    IdRActosInvestigacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaSolicitud = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Servicios = table.Column<string>(nullable: true),
                    Especificaciones = table.Column<string>(nullable: true),
                    Cdetenido = table.Column<bool>(nullable: false),
                    Respuestas = table.Column<string>(nullable: true),
                    NUC = table.Column<string>(nullable: true),
                    Textofinal = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(nullable: false),
                    UDirSubPro = table.Column<string>(nullable: true),
                    UUsuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    NumeroOficio = table.Column<string>(nullable: true),
                    NodeSolicitud = table.Column<string>(nullable: true),
                    NumeroDistrito = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_RACTOSINVESTIGACION", x => x.IdRActosInvestigacion);
                    table.ForeignKey(
                        name: "FK_CAT_RACTOSINVESTIGACION_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_RCITATORIO_NOTIFICACION",
                columns: table => new
                {
                    IdCitatorio_Notificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    NombrePersona = table.Column<string>(nullable: true),
                    DomicilioPersona = table.Column<string>(nullable: true),
                    ReferenciaPersona = table.Column<string>(nullable: true),
                    TelefonoPersona = table.Column<string>(nullable: true),
                    LugarCita = table.Column<string>(nullable: true),
                    FechaCita = table.Column<string>(nullable: true),
                    Nuc = table.Column<string>(nullable: true),
                    Delito = table.Column<string>(nullable: true),
                    Hora = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaReporte = table.Column<string>(nullable: true),
                    FechaSis = table.Column<DateTime>(nullable: false),
                    Tipo = table.Column<bool>(nullable: false),
                    Distrito = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Subproc = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Agencia = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Puesto = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Textofinal = table.Column<string>(nullable: true),
                    Modulo = table.Column<string>(nullable: true),
                    NumeroOficio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_RCITATORIO_NOTIFICACION", x => x.IdCitatorio_Notificacion);
                    table.ForeignKey(
                        name: "FK_CAT_RCITATORIO_NOTIFICACION_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_RDDERIVACION",
                columns: table => new
                {
                    idRDDerivacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    rHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    idDDerivacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Espesificaciones = table.Column<string>(nullable: true),
                    FechaDerivacion = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(type: "DateTime", nullable: true),
                    uDistrito = table.Column<string>(nullable: true),
                    uDirSubPro = table.Column<string>(nullable: true),
                    uAgencia = table.Column<string>(nullable: true),
                    uNombre = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_RDDERIVACION", x => x.idRDDerivacion);
                    table.ForeignKey(
                        name: "FK_CAT_RDDERIVACION_C_DEPENDECIAS_DERIVACION_idDDerivacion",
                        column: x => x.idDDerivacion,
                        principalTable: "C_DEPENDECIAS_DERIVACION",
                        principalColumn: "IdDDerivacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_RDDERIVACION_CAT_RHECHO_rHechoId",
                        column: x => x.rHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CAT_RDH",
                columns: table => new
                {
                    IdRDH = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    DelitoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ReclasificacionDelito = table.Column<string>(nullable: true),
                    TipoFuero = table.Column<string>(nullable: true),
                    TipoDeclaracion = table.Column<string>(nullable: true),
                    ResultadoDelito = table.Column<string>(nullable: true),
                    GraveNoGrave = table.Column<string>(nullable: true),
                    IntensionDelito = table.Column<string>(nullable: true),
                    ViolenciaSinViolencia = table.Column<string>(nullable: true),
                    Equiparado = table.Column<bool>(nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    Concurso = table.Column<string>(nullable: true),
                    ClasificaOrdenResult = table.Column<string>(nullable: true),
                    ArmaFuego = table.Column<bool>(nullable: false),
                    ArmaBlanca = table.Column<bool>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true),
                    ConAlgunaParteCuerpo = table.Column<string>(nullable: true),
                    ConotroElemento = table.Column<string>(nullable: true),
                    TipoRobado = table.Column<string>(nullable: true),
                    MontoRobado = table.Column<decimal>(nullable: false),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_RDH", x => x.IdRDH);
                    table.ForeignKey(
                        name: "FK_CAT_RDH_C_DELITO_DelitoId",
                        column: x => x.DelitoId,
                        principalTable: "C_DELITO",
                        principalColumn: "IdDelito",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_RDH_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_RDILIGENCIAS",
                columns: table => new
                {
                    IdRDiligencias = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    rHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaSolicitud = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(nullable: false),
                    Dirigidoa = table.Column<string>(nullable: true),
                    DirSubPro = table.Column<string>(nullable: true),
                    EmitidoPor = table.Column<string>(nullable: true),
                    uDirSubPro = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    StatusRespuesta = table.Column<string>(nullable: true),
                    Servicio = table.Column<string>(nullable: true),
                    Especificaciones = table.Column<string>(nullable: true),
                    Prioridad = table.Column<string>(nullable: true),
                    ASPId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Modulo = table.Column<string>(nullable: true),
                    Agencia = table.Column<string>(nullable: true),
                    Respuestas = table.Column<string>(nullable: true),
                    ConIndicio = table.Column<bool>(nullable: false),
                    NUC = table.Column<string>(nullable: true),
                    Textofinal = table.Column<string>(nullable: true),
                    NumeroOficio = table.Column<string>(nullable: true),
                    NodeSolicitud = table.Column<string>(nullable: true),
                    NumeroDistrito = table.Column<string>(nullable: true),
                    Lat = table.Column<string>(nullable: true),
                    Lng = table.Column<string>(nullable: true),
                    Dirigido = table.Column<bool>(nullable: false),
                    RecibidoF = table.Column<bool>(nullable: false),
                    FechaRecibidoF = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_RDILIGENCIAS", x => x.IdRDiligencias);
                    table.ForeignKey(
                        name: "FK_CAT_RDILIGENCIAS_SP_ASP_ASPId",
                        column: x => x.ASPId,
                        principalTable: "SP_ASP",
                        principalColumn: "IdASP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_RDILIGENCIAS_CAT_RHECHO_rHechoId",
                        column: x => x.rHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CAT_REPRESENTANTES",
                columns: table => new
                {
                    IdRepresentante = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    PersonaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Nombres = table.Column<string>(nullable: true),
                    ApellidoPa = table.Column<string>(nullable: true),
                    ApellidoMa = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(nullable: true),
                    EntidadFeNacimiento = table.Column<string>(nullable: true),
                    Curp = table.Column<string>(nullable: true),
                    MedioNotificacion = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    CorreoElectronico = table.Column<string>(nullable: true),
                    Nacionalidad = table.Column<string>(nullable: true),
                    Genero = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    Tipo1 = table.Column<int>(nullable: false),
                    Tipo2 = table.Column<int>(nullable: false),
                    CedulaProfesional = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    Calle = table.Column<string>(nullable: true),
                    NoInt = table.Column<string>(nullable: true),
                    NoExt = table.Column<string>(nullable: true),
                    EntreCalle1 = table.Column<string>(nullable: true),
                    EntreCalle2 = table.Column<string>(nullable: true),
                    Referencia = table.Column<string>(nullable: true),
                    Pais = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Municipio = table.Column<string>(nullable: true),
                    Localidad = table.Column<string>(nullable: true),
                    CP = table.Column<string>(nullable: true),
                    lat = table.Column<string>(nullable: true),
                    lng = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_REPRESENTANTES", x => x.IdRepresentante);
                    table.ForeignKey(
                        name: "FK_CAT_REPRESENTANTES_CAT_PERSONA_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "CAT_PERSONA",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_REPRESENTANTES_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_TERMINACIONARCHIVO",
                columns: table => new
                {
                    IdDeterminacionArchivo = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Municipio = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    MunicionEstado = table.Column<string>(nullable: true),
                    FechaIHecho = table.Column<string>(nullable: true),
                    MedioDenuncia = table.Column<string>(nullable: true),
                    ClasificacionPersona = table.Column<string>(nullable: true),
                    Delito = table.Column<string>(nullable: true),
                    Articulos = table.Column<string>(nullable: true),
                    Aifr = table.Column<string>(nullable: true),
                    Opcion1 = table.Column<string>(nullable: true),
                    Opcion2 = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UUAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    NumeroOficio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_TERMINACIONARCHIVO", x => x.IdDeterminacionArchivo);
                    table.ForeignKey(
                        name: "FK_CAT_TERMINACIONARCHIVO_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_VEHICULO",
                columns: table => new
                {
                    IdVehiculo = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Estado = table.Column<bool>(nullable: false),
                    EstadoRobado = table.Column<bool>(nullable: false),
                    Marca = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    Serie = table.Column<string>(nullable: true),
                    Placas = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Ano = table.Column<string>(nullable: true),
                    Recuperado = table.Column<bool>(nullable: false),
                    Devuelto = table.Column<bool>(nullable: false),
                    SenasParticulares = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(nullable: false),
                    FechaRegistro = table.Column<string>(nullable: true),
                    Distrito = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Subproc = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Agencia = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Puesto = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Modulo = table.Column<string>(nullable: true),
                    NoSerieMotor = table.Column<string>(nullable: true),
                    Recepcion = table.Column<string>(nullable: true),
                    Lugar = table.Column<string>(nullable: true),
                    Municipio = table.Column<string>(nullable: true),
                    Modalidad = table.Column<string>(nullable: true),
                    FechaRobo = table.Column<string>(nullable: true),
                    Propietario = table.Column<string>(nullable: true),
                    NombreDenunciante = table.Column<string>(nullable: true),
                    DomicilioDenunciante = table.Column<string>(nullable: true),
                    NumeroOficio = table.Column<string>(nullable: true),
                    Lugardeposito = table.Column<string>(nullable: true),
                    Estadov = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_VEHICULO", x => x.IdVehiculo);
                    table.ForeignKey(
                        name: "FK_CAT_VEHICULO_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IL_AGENDA",
                columns: table => new
                {
                    IdAgenda = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    NumeroOficio = table.Column<string>(nullable: true),
                    CausaPenal = table.Column<string>(nullable: true),
                    Nuc = table.Column<string>(nullable: true),
                    Victimas = table.Column<string>(nullable: true),
                    Imputado = table.Column<string>(nullable: true),
                    Delitos = table.Column<string>(nullable: true),
                    Texto = table.Column<string>(nullable: true),
                    FechaCitacion = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false),
                    DirigidoNombre = table.Column<string>(nullable: true),
                    DirigidoPuesto = table.Column<string>(nullable: true),
                    ReDireccion = table.Column<string>(nullable: true),
                    ReTelefono = table.Column<string>(nullable: true),
                    ReCorreo = table.Column<string>(nullable: true),
                    ArticulosSancion = table.Column<string>(nullable: true),
                    DireccionImputado = table.Column<string>(nullable: true),
                    TelefonoImputado = table.Column<string>(nullable: true),
                    CorreoImputado = table.Column<string>(nullable: true),
                    DefensorParticularImp = table.Column<string>(nullable: true),
                    DomicilioDPI = table.Column<string>(nullable: true),
                    TelefonoDPI = table.Column<string>(nullable: true),
                    CorreoDPI = table.Column<string>(nullable: true),
                    InformacionVicAseJu = table.Column<string>(nullable: true),
                    InformacionImpDeP = table.Column<string>(nullable: true),
                    InformacionImp = table.Column<string>(nullable: true),
                    InformacionDelito = table.Column<string>(nullable: true),
                    HoraCitacion = table.Column<string>(nullable: true),
                    LugarCitacion = table.Column<string>(nullable: true),
                    DescripcionCitacion = table.Column<string>(nullable: true),
                    DireccionHecho = table.Column<string>(nullable: true),
                    HechosIII = table.Column<string>(nullable: true),
                    ClasificacionjIII = table.Column<string>(nullable: true),
                    CorrelacionArtIII = table.Column<string>(nullable: true),
                    ArticuloIII = table.Column<string>(nullable: true),
                    ModaidadesVI = table.Column<string>(nullable: true),
                    AutoriaV = table.Column<string>(nullable: true),
                    Autoria2V = table.Column<string>(nullable: true),
                    PreceptosVI = table.Column<string>(nullable: true),
                    TestimonialVII = table.Column<string>(nullable: true),
                    PericialVII = table.Column<string>(nullable: true),
                    DocumentalesVII = table.Column<string>(nullable: true),
                    MaterialVII = table.Column<string>(nullable: true),
                    AnticipadaVII = table.Column<string>(nullable: true),
                    ArticulosVIII = table.Column<string>(nullable: true),
                    MontoVIII = table.Column<string>(nullable: true),
                    NumeroLetraVIII = table.Column<string>(nullable: true),
                    TestimonialVIII = table.Column<string>(nullable: true),
                    PericialVIII = table.Column<string>(nullable: true),
                    DocumentalesVIII = table.Column<string>(nullable: true),
                    MaterialVIII = table.Column<string>(nullable: true),
                    ArticulosIX = table.Column<string>(nullable: true),
                    PenaIX = table.Column<string>(nullable: true),
                    TestimonialesX = table.Column<string>(nullable: true),
                    TestimonialX = table.Column<string>(nullable: true),
                    PericialX = table.Column<string>(nullable: true),
                    DocumentalesX = table.Column<string>(nullable: true),
                    MaterialX = table.Column<string>(nullable: true),
                    DecomisoXI = table.Column<string>(nullable: true),
                    PropuestaXII = table.Column<string>(nullable: true),
                    TerminacionXIII = table.Column<string>(nullable: true),
                    Resumen = table.Column<string>(nullable: true),
                    Viculada = table.Column<bool>(nullable: false),
                    PlazoInvestigacion = table.Column<string>(nullable: true),
                    Prorroga = table.Column<bool>(nullable: false),
                    TiempoProrroga = table.Column<string>(nullable: true),
                    PersonaPresentar = table.Column<string>(nullable: true),
                    Tipo2 = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    Aux = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IL_AGENDA", x => x.IdAgenda);
                    table.ForeignKey(
                        name: "FK_IL_AGENDA_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IL_CITATORIO",
                columns: table => new
                {
                    IdCitatorio = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    NumeroOficio = table.Column<string>(nullable: true),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    CausaPenal = table.Column<string>(nullable: true),
                    Nuc = table.Column<string>(nullable: true),
                    Delito = table.Column<string>(nullable: true),
                    Destinatario = table.Column<string>(nullable: true),
                    Dirigidoa = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    Domicilio = table.Column<string>(nullable: true),
                    FechaCitacion = table.Column<DateTime>(nullable: false),
                    JuicioOral = table.Column<string>(nullable: true),
                    Multa = table.Column<string>(nullable: true),
                    Articulo = table.Column<string>(nullable: true),
                    LugarPresentarse = table.Column<string>(nullable: true),
                    DireccionLugar = table.Column<string>(nullable: true),
                    CausaPenalJo = table.Column<string>(nullable: true),
                    Imputado = table.Column<string>(nullable: true),
                    PeritosPolicias = table.Column<string>(nullable: true),
                    HoraCitacion = table.Column<string>(nullable: true),
                    DireccionAgencia = table.Column<string>(nullable: true),
                    FechaCitacion2 = table.Column<DateTime>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IL_CITATORIO", x => x.IdCitatorio);
                    table.ForeignKey(
                        name: "FK_IL_CITATORIO_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IL_SOLICITUDES_DE_INFORMACION",
                columns: table => new
                {
                    IdSolicitudInfo = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    CodOficio = table.Column<string>(nullable: true),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    CausaPenal = table.Column<string>(nullable: true),
                    Nuc = table.Column<string>(nullable: true),
                    Imputado = table.Column<string>(nullable: true),
                    CURP = table.Column<string>(nullable: true),
                    Fnacimiento = table.Column<string>(nullable: true),
                    Dirigidoa = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    TipoDoc = table.Column<int>(nullable: false),
                    Sobr = table.Column<string>(nullable: true),
                    Unico = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IL_SOLICITUDES_DE_INFORMACION", x => x.IdSolicitudInfo);
                    table.ForeignKey(
                        name: "FK_IL_SOLICITUDES_DE_INFORMACION_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JR_EXPEDIENTE",
                columns: table => new
                {
                    IdExpediente = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    Prefijo = table.Column<string>(nullable: true),
                    Cosecutivo = table.Column<int>(nullable: false),
                    Año = table.Column<int>(nullable: false),
                    Sede = table.Column<string>(nullable: true),
                    NoExpediente = table.Column<string>(nullable: true),
                    NoDerivacion = table.Column<int>(nullable: false),
                    StatusGeneral = table.Column<string>(nullable: true),
                    InformacionStatus = table.Column<string>(nullable: true),
                    FechaRegistroExpediente = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JR_EXPEDIENTE", x => x.IdExpediente);
                    table.ForeignKey(
                        name: "FK_JR_EXPEDIENTE_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_DETENCION",
                columns: table => new
                {
                    IdDetencion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RHechoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    PersonaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Nuc = table.Column<string>(nullable: true),
                    MpAsignado = table.Column<string>(nullable: true),
                    Mesa = table.Column<string>(nullable: true),
                    FechaIngreso = table.Column<string>(nullable: true),
                    FechaSalida = table.Column<string>(nullable: true),
                    FechaTraslado = table.Column<string>(nullable: true),
                    NumUnicoRegNacional = table.Column<string>(nullable: true),
                    TipoDetencion = table.Column<string>(nullable: true),
                    Pertenecnias = table.Column<string>(nullable: true),
                    FechaHReingreso = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    AutoridadQO = table.Column<string>(nullable: true),
                    AutoridadED = table.Column<string>(nullable: true),
                    Delito = table.Column<string>(nullable: true),
                    Tdelito = table.Column<string>(nullable: true),
                    Modalidad = table.Column<string>(nullable: true),
                    MOperandi = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    FechaUltimoStatus = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_DETENCION", x => x.IdDetencion);
                    table.ForeignKey(
                        name: "FK_PI_DETENCION_CAT_PERSONA_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "CAT_PERSONA",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PI_DETENCION_CAT_RHECHO_RHechoId",
                        column: x => x.RHechoId,
                        principalTable: "CAT_RHECHO",
                        principalColumn: "IdRHecho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_AMPOTURNO",
                columns: table => new
                {
                    IdAmpoTurno = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    TurnoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_AMPOTURNO", x => x.IdAmpoTurno);
                    table.ForeignKey(
                        name: "FK_CAT_AMPOTURNO_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_AMPOTURNO_CAT_TURNO_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "CAT_TURNO",
                        principalColumn: "IdTurno",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_DETALLE_SEGUIMIENTO",
                columns: table => new
                {
                    IdDetalles = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    IndiciosId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaHora = table.Column<DateTime>(nullable: false),
                    Fechasys = table.Column<string>(nullable: true),
                    OrigenLugar = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DestinoLugar = table.Column<string>(nullable: true),
                    QuienEntrega = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    QuienRecibe = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_DETALLE_SEGUIMIENTO", x => x.IdDetalles);
                    table.ForeignKey(
                        name: "FK_CAT_DETALLE_SEGUIMIENTO_CAT_INDICIOS_IndiciosId",
                        column: x => x.IndiciosId,
                        principalTable: "CAT_INDICIOS",
                        principalColumn: "IdIndicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_ARCHIVOSMEDIAAFILIACION",
                columns: table => new
                {
                    IdArchivosMediaAfiliacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    MediaAfiliacionid = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoDocumento = table.Column<string>(nullable: true),
                    DescripcionDocumento = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    Ruta = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_ARCHIVOSMEDIAAFILIACION", x => x.IdArchivosMediaAfiliacion);
                    table.ForeignKey(
                        name: "FK_PI_ARCHIVOSMEDIAAFILIACION_CAT_MEDIAAFILIACION_MediaAfiliacionid",
                        column: x => x.MediaAfiliacionid,
                        principalTable: "CAT_MEDIAAFILIACION",
                        principalColumn: "idMediaAfiliacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_RACTOSINDETALLES",
                columns: table => new
                {
                    IdActosInDetetalle = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RActosInvestigacionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Servicio = table.Column<string>(nullable: true),
                    ServicioNM = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    TextoFinal = table.Column<string>(nullable: true),
                    FechaRecibido = table.Column<string>(nullable: true),
                    FechaAceptado = table.Column<string>(nullable: true),
                    FechaFinalizado = table.Column<string>(nullable: true),
                    FechaEntregado = table.Column<string>(nullable: true),
                    UltmimoStatus = table.Column<DateTime>(nullable: false),
                    Respuesta = table.Column<string>(nullable: true),
                    Conclusion = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(nullable: false),
                    UDirSubPro = table.Column<string>(nullable: true),
                    UUsuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_RACTOSINDETALLES", x => x.IdActosInDetetalle);
                    table.ForeignKey(
                        name: "FK_CAT_RACTOSINDETALLES_CAT_RACTOSINVESTIGACION_RActosInvestigacionId",
                        column: x => x.RActosInvestigacionId,
                        principalTable: "CAT_RACTOSINVESTIGACION",
                        principalColumn: "IdRActosInvestigacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_FOTOS",
                columns: table => new
                {
                    IdFotos = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RActoInvestigacionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoDocumento = table.Column<string>(nullable: true),
                    DescripcionDocumento = table.Column<string>(nullable: true),
                    FechaRegistro = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    Ruta = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_FOTOS", x => x.IdFotos);
                    table.ForeignKey(
                        name: "FK_PI_FOTOS_CAT_RACTOSINVESTIGACION_RActoInvestigacionId",
                        column: x => x.RActoInvestigacionId,
                        principalTable: "CAT_RACTOSINVESTIGACION",
                        principalColumn: "IdRActosInvestigacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_PERITOSASIGNADOS",
                columns: table => new
                {
                    IdPeritoAsignadoPI = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    RActoInvestigacionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Respuesta = table.Column<string>(nullable: true),
                    NumeroInterno = table.Column<int>(nullable: false),
                    Conclusion = table.Column<string>(nullable: true),
                    FechaRecibido = table.Column<string>(nullable: true),
                    FechaAceptado = table.Column<string>(nullable: true),
                    FechaFinalizado = table.Column<string>(nullable: true),
                    FechaEntregado = table.Column<string>(nullable: true),
                    uDistrito = table.Column<string>(nullable: true),
                    uSubproc = table.Column<string>(nullable: true),
                    uAgencia = table.Column<string>(nullable: true),
                    uUsuario = table.Column<string>(nullable: true),
                    uPuesto = table.Column<string>(nullable: true),
                    uModulo = table.Column<string>(nullable: true),
                    Motivo = table.Column<string>(nullable: true),
                    FechaCambio = table.Column<DateTime>(nullable: false),
                    Fechasysregistro = table.Column<DateTime>(nullable: false),
                    Fechasysfinalizado = table.Column<DateTime>(nullable: false),
                    UltmimoStatus = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_PERITOSASIGNADOS", x => x.IdPeritoAsignadoPI);
                    table.ForeignKey(
                        name: "FK_PI_PERITOSASIGNADOS_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PI_PERITOSASIGNADOS_CAT_RACTOSINVESTIGACION_RActoInvestigacionId",
                        column: x => x.RActoInvestigacionId,
                        principalTable: "CAT_RACTOSINVESTIGACION",
                        principalColumn: "IdRActosInvestigacion",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SP_DILIGENCIAINDICIO",
                columns: table => new
                {
                    IdDiligenciaIndicio = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RDiligenciasId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    IndiciosId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SP_DILIGENCIAINDICIO", x => x.IdDiligenciaIndicio);
                    table.ForeignKey(
                        name: "FK_SP_DILIGENCIAINDICIO_CAT_INDICIOS_IndiciosId",
                        column: x => x.IndiciosId,
                        principalTable: "CAT_INDICIOS",
                        principalColumn: "IdIndicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SP_DILIGENCIAINDICIO_CAT_RDILIGENCIAS_RDiligenciasId",
                        column: x => x.RDiligenciasId,
                        principalTable: "CAT_RDILIGENCIAS",
                        principalColumn: "IdRDiligencias",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SP_DOCSDILIGENCIA",
                columns: table => new
                {
                    IdDocsDiligencia = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RDiligenciasId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoDocumento = table.Column<string>(nullable: true),
                    DescripcionDocumento = table.Column<string>(nullable: true),
                    FechaRegistro = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    Ruta = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    uDistrito = table.Column<string>(nullable: true),
                    uSubproc = table.Column<string>(nullable: true),
                    uAgencia = table.Column<string>(nullable: true),
                    uUsuario = table.Column<string>(nullable: true),
                    uPuesto = table.Column<string>(nullable: true),
                    uModulo = table.Column<string>(nullable: true),
                    fechasysregistro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SP_DOCSDILIGENCIA", x => x.IdDocsDiligencia);
                    table.ForeignKey(
                        name: "FK_SP_DOCSDILIGENCIA_CAT_RDILIGENCIAS_RDiligenciasId",
                        column: x => x.RDiligenciasId,
                        principalTable: "CAT_RDILIGENCIAS",
                        principalColumn: "IdRDiligencias",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SP_PERITOSASIGNADOS",
                columns: table => new
                {
                    IdPeritoAsignado = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    RDiligenciasId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Respuesta = table.Column<string>(nullable: true),
                    NumeroInterno = table.Column<int>(nullable: false),
                    Conclusion = table.Column<string>(nullable: true),
                    FechaRecibido = table.Column<string>(nullable: true),
                    FechaAceptado = table.Column<string>(nullable: true),
                    FechaFinalizado = table.Column<string>(nullable: true),
                    FechaEntregado = table.Column<string>(nullable: true),
                    uDistrito = table.Column<string>(nullable: true),
                    uSubproc = table.Column<string>(nullable: true),
                    uAgencia = table.Column<string>(nullable: true),
                    uUsuario = table.Column<string>(nullable: true),
                    uPuesto = table.Column<string>(nullable: true),
                    uModulo = table.Column<string>(nullable: true),
                    Fechasysregistro = table.Column<DateTime>(nullable: false),
                    Fechasysfinalizado = table.Column<DateTime>(nullable: false),
                    UltmimoStatus = table.Column<DateTime>(nullable: false),
                    NumeroControl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SP_PERITOSASIGNADOS", x => x.IdPeritoAsignado);
                    table.ForeignKey(
                        name: "FK_SP_PERITOSASIGNADOS_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SP_PERITOSASIGNADOS_CAT_RDILIGENCIAS_RDiligenciasId",
                        column: x => x.RDiligenciasId,
                        principalTable: "CAT_RDILIGENCIAS",
                        principalColumn: "IdRDiligencias",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CAT_DOCSREPRESENTANTES",
                columns: table => new
                {
                    IdDocsRepresentantes = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    RepresentanteId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoDocumento = table.Column<string>(nullable: true),
                    NombreDocumento = table.Column<string>(nullable: true),
                    DescripcionDocumento = table.Column<string>(nullable: true),
                    Ruta = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_DOCSREPRESENTANTES", x => x.IdDocsRepresentantes);
                    table.ForeignKey(
                        name: "FK_CAT_DOCSREPRESENTANTES_CAT_REPRESENTANTES_RepresentanteId",
                        column: x => x.RepresentanteId,
                        principalTable: "CAT_REPRESENTANTES",
                        principalColumn: "IdRepresentante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JR_ENVIO",
                columns: table => new
                {
                    IdEnvio = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ExpedienteId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    AutoridadqueDeriva = table.Column<string>(nullable: true),
                    uqe_Distrito = table.Column<string>(nullable: true),
                    uqe_DirSubProc = table.Column<string>(nullable: true),
                    uqe_Agencia = table.Column<string>(nullable: true),
                    uqe_Modulo = table.Column<string>(nullable: true),
                    uqe_Nombre = table.Column<string>(nullable: true),
                    uqe_Puesto = table.Column<string>(nullable: true),
                    StatusGeneral = table.Column<string>(nullable: true),
                    StatusBaja = table.Column<bool>(nullable: false),
                    MotivoBaja = table.Column<string>(nullable: true),
                    RespuestaExpediente = table.Column<string>(nullable: true),
                    EspontaneoNoEspontaneo = table.Column<string>(nullable: true),
                    PrimeraVezSubsecuente = table.Column<string>(nullable: true),
                    ContadorNODerivacion = table.Column<int>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "DateTime", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "DateTime", nullable: true),
                    NoSolicitantes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JR_ENVIO", x => x.IdEnvio);
                    table.ForeignKey(
                        name: "FK_JR_ENVIO_JR_EXPEDIENTE_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "JR_EXPEDIENTE",
                        principalColumn: "IdExpediente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JR_REGISTROCONCLUSION",
                columns: table => new
                {
                    IdRegistroConclusion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ExpedienteId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    Asunto = table.Column<string>(nullable: true),
                    Texto = table.Column<string>(nullable: true),
                    FechaHora = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Solicitates = table.Column<string>(nullable: true),
                    Reuqeridos = table.Column<string>(nullable: true),
                    StatusGeneral = table.Column<string>(nullable: true),
                    uf_Distrito = table.Column<string>(nullable: true),
                    uf_DirSubProc = table.Column<string>(nullable: true),
                    uf_Agencia = table.Column<string>(nullable: true),
                    uf_Modulo = table.Column<string>(nullable: true),
                    uf_Nombre = table.Column<string>(nullable: true),
                    uf_Puesto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JR_REGISTROCONCLUSION", x => x.IdRegistroConclusion);
                    table.ForeignKey(
                        name: "FK_JR_REGISTROCONCLUSION_JR_EXPEDIENTE_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "JR_EXPEDIENTE",
                        principalColumn: "IdExpediente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JR_REGISTRONOTIFICACION",
                columns: table => new
                {
                    IdRegistroNotificaciones = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ExpedienteId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Asunto = table.Column<string>(nullable: true),
                    Texto = table.Column<string>(nullable: true),
                    FechaHora = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Solicitates = table.Column<string>(nullable: true),
                    Reuqeridos = table.Column<string>(nullable: true),
                    uf_Distrito = table.Column<string>(nullable: true),
                    uf_DirSubProc = table.Column<string>(nullable: true),
                    uf_Agencia = table.Column<string>(nullable: true),
                    uf_Modulo = table.Column<string>(nullable: true),
                    uf_Nombre = table.Column<string>(nullable: true),
                    uf_Puesto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JR_REGISTRONOTIFICACION", x => x.IdRegistroNotificaciones);
                    table.ForeignKey(
                        name: "FK_JR_REGISTRONOTIFICACION_JR_EXPEDIENTE_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "JR_EXPEDIENTE",
                        principalColumn: "IdExpediente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_EGRESO_TEMPORALES",
                columns: table => new
                {
                    IdEgresoTemporal = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    DetencionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Motivo = table.Column<string>(nullable: true),
                    Horaegreso = table.Column<string>(nullable: true),
                    QuienSolicita = table.Column<string>(nullable: true),
                    QuienAutoriza = table.Column<string>(nullable: true),
                    Notas = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_EGRESO_TEMPORALES", x => x.IdEgresoTemporal);
                    table.ForeignKey(
                        name: "FK_PI_EGRESO_TEMPORALES_PI_DETENCION_DetencionId",
                        column: x => x.DetencionId,
                        principalTable: "PI_DETENCION",
                        principalColumn: "IdDetencion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_ESTATUS_CUSTODIA",
                columns: table => new
                {
                    IdEstatusCustodia = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    DetencionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Calle = table.Column<string>(nullable: true),
                    NoExterior = table.Column<int>(nullable: false),
                    NoInterior = table.Column<int>(nullable: false),
                    Ecalle1 = table.Column<string>(nullable: true),
                    Ecalle2 = table.Column<string>(nullable: true),
                    Referencia = table.Column<string>(nullable: true),
                    Pais = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Municipio = table.Column<string>(nullable: true),
                    Localidad = table.Column<string>(nullable: true),
                    Cp = table.Column<int>(nullable: false),
                    Longitud = table.Column<string>(nullable: true),
                    Latitud = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    ElementoAsignado = table.Column<string>(nullable: true),
                    Horainicio = table.Column<string>(nullable: true),
                    HoraTermino = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<string>(nullable: true),
                    FechaTermino = table.Column<string>(nullable: true),
                    Motivo = table.Column<string>(nullable: true),
                    Observaciones = table.Column<string>(nullable: true),
                    Origen = table.Column<string>(nullable: true),
                    Destino = table.Column<string>(nullable: true),
                    HoraSalida = table.Column<string>(nullable: true),
                    HoraLLegada = table.Column<string>(nullable: true),
                    Ruta = table.Column<string>(nullable: true),
                    Tipo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_ESTATUS_CUSTODIA", x => x.IdEstatusCustodia);
                    table.ForeignKey(
                        name: "FK_PI_ESTATUS_CUSTODIA_PI_DETENCION_DetencionId",
                        column: x => x.DetencionId,
                        principalTable: "PI_DETENCION",
                        principalColumn: "IdDetencion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_HISTORIALDETENCIONES",
                columns: table => new
                {
                    IdHistorialDetencion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    DetencionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    StatusPasado = table.Column<string>(nullable: true),
                    StatuusNuevo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_HISTORIALDETENCIONES", x => x.IdHistorialDetencion);
                    table.ForeignKey(
                        name: "FK_PI_HISTORIALDETENCIONES_PI_DETENCION_DetencionId",
                        column: x => x.DetencionId,
                        principalTable: "PI_DETENCION",
                        principalColumn: "IdDetencion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_SUBIRARCHIVO",
                columns: table => new
                {
                    IdSubirArchivo = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    DetencionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoDocumento = table.Column<string>(nullable: true),
                    NombreDocumento = table.Column<string>(nullable: true),
                    DescripcionDocumento = table.Column<string>(nullable: true),
                    Ruta = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_SUBIRARCHIVO", x => x.IdSubirArchivo);
                    table.ForeignKey(
                        name: "FK_PI_SUBIRARCHIVO_PI_DETENCION_DetencionId",
                        column: x => x.DetencionId,
                        principalTable: "PI_DETENCION",
                        principalColumn: "IdDetencion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_VISITA",
                columns: table => new
                {
                    IdVisita = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    PIPersonaVisitaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    DetencionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechayHora = table.Column<string>(nullable: true),
                    HILocutorio = table.Column<string>(nullable: true),
                    HSLocutorio = table.Column<string>(nullable: true),
                    QAutorizaVisita = table.Column<string>(nullable: true),
                    MotivoVisita = table.Column<string>(nullable: true),
                    RDetenido = table.Column<string>(nullable: true),
                    UDistrito = table.Column<string>(nullable: true),
                    USubproc = table.Column<string>(nullable: true),
                    UAgencia = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    UPuesto = table.Column<string>(nullable: true),
                    UModulo = table.Column<string>(nullable: true),
                    Fechasys = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_VISITA", x => x.IdVisita);
                    table.ForeignKey(
                        name: "FK_PI_VISITA_PI_DETENCION_DetencionId",
                        column: x => x.DetencionId,
                        principalTable: "PI_DETENCION",
                        principalColumn: "IdDetencion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PI_VISITA_PI_PERSONAVISITA_PIPersonaVisitaId",
                        column: x => x.PIPersonaVisitaId,
                        principalTable: "PI_PERSONAVISITA",
                        principalColumn: "IdPIPersonaVisita",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_INFORMES",
                columns: table => new
                {
                    IdInforme = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    PeritoAsignadoPIId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    TipoInforme = table.Column<int>(nullable: false),
                    TextoInforme = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(nullable: false),
                    uDistrito = table.Column<string>(nullable: true),
                    uSubproc = table.Column<string>(nullable: true),
                    uAgencia = table.Column<string>(nullable: true),
                    uUsuario = table.Column<string>(nullable: true),
                    uPuesto = table.Column<string>(nullable: true),
                    uModulo = table.Column<string>(nullable: true),
                    NoOficio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_INFORMES", x => x.IdInforme);
                    table.ForeignKey(
                        name: "FK_PI_INFORMES_PI_PERITOSASIGNADOS_PeritoAsignadoPIId",
                        column: x => x.PeritoAsignadoPIId,
                        principalTable: "PI_PERITOSASIGNADOS",
                        principalColumn: "IdPeritoAsignadoPI",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_SOLICITUDINTELIGENCIA",
                columns: table => new
                {
                    IdSolicitudInteligencia = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    PeritoAsignadoPIId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Mensaje = table.Column<string>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true),
                    StatusAutorizacion = table.Column<bool>(nullable: false),
                    StatusMensaje = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(nullable: false),
                    uDistrito = table.Column<string>(nullable: true),
                    uSubproc = table.Column<string>(nullable: true),
                    uAgencia = table.Column<string>(nullable: true),
                    uUsuario = table.Column<string>(nullable: true),
                    uPuesto = table.Column<string>(nullable: true),
                    uModulo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_SOLICITUDINTELIGENCIA", x => x.IdSolicitudInteligencia);
                    table.ForeignKey(
                        name: "FK_PI_SOLICITUDINTELIGENCIA_PI_PERITOSASIGNADOS_PeritoAsignadoPIId",
                        column: x => x.PeritoAsignadoPIId,
                        principalTable: "PI_PERITOSASIGNADOS",
                        principalColumn: "IdPeritoAsignadoPI",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JR_ACUERDOREPARATORIO",
                columns: table => new
                {
                    IdAcuerdoReparatorio = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    EnvioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    NombreSolicitante = table.Column<string>(nullable: true),
                    NombreRequerdio = table.Column<string>(nullable: true),
                    Delitos = table.Column<string>(nullable: true),
                    NUC = table.Column<string>(nullable: true),
                    NoExpediente = table.Column<string>(nullable: true),
                    StatusConclusion = table.Column<string>(nullable: true),
                    StatusCumplimiento = table.Column<string>(nullable: true),
                    TipoPago = table.Column<string>(nullable: true),
                    MetodoUtilizado = table.Column<string>(nullable: true),
                    MontoTotal = table.Column<decimal>(nullable: false),
                    ObjectoEspecie = table.Column<string>(nullable: true),
                    NoTotalParcialidades = table.Column<int>(nullable: false),
                    Periodo = table.Column<int>(nullable: false),
                    FechaCelebracionAcuerdo = table.Column<DateTime>(type: "DateTime", nullable: true),
                    FechaLimiteCumplimiento = table.Column<DateTime>(type: "DateTime", nullable: true),
                    StatusRespuestaCoordinadorJuridico = table.Column<string>(nullable: true),
                    RespuestaCoordinadorJuridico = table.Column<string>(nullable: true),
                    FechaHoraRespuestaCoordinadorJuridico = table.Column<DateTime>(type: "DateTime", nullable: true),
                    StatusRespuestaAMP = table.Column<string>(nullable: true),
                    RespuestaAMP = table.Column<string>(nullable: true),
                    FechaRespuestaAMP = table.Column<DateTime>(type: "DateTime", nullable: true),
                    TextoAR = table.Column<string>(nullable: true),
                    uf_Distrito = table.Column<string>(nullable: true),
                    uf_DirSubProc = table.Column<string>(nullable: true),
                    uf_Agencia = table.Column<string>(nullable: true),
                    uf_Modulo = table.Column<string>(nullable: true),
                    uf_Nombre = table.Column<string>(nullable: true),
                    uf_Puesto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JR_ACUERDOREPARATORIO", x => x.IdAcuerdoReparatorio);
                    table.ForeignKey(
                        name: "FK_JR_ACUERDOREPARATORIO_JR_ENVIO_EnvioId",
                        column: x => x.EnvioId,
                        principalTable: "JR_ENVIO",
                        principalColumn: "IdEnvio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JR_ASINGACIONENVIO",
                columns: table => new
                {
                    IdAsingacionEnvio = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    EnvioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JR_ASINGACIONENVIO", x => x.IdAsingacionEnvio);
                    table.ForeignKey(
                        name: "FK_JR_ASINGACIONENVIO_JR_ENVIO_EnvioId",
                        column: x => x.EnvioId,
                        principalTable: "JR_ENVIO",
                        principalColumn: "IdEnvio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JR_ASINGACIONENVIO_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JR_DELITODERIVADO",
                columns: table => new
                {
                    IdDelitoDerivado = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    EnvioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    RDHId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JR_DELITODERIVADO", x => x.IdDelitoDerivado);
                    table.ForeignKey(
                        name: "FK_JR_DELITODERIVADO_JR_ENVIO_EnvioId",
                        column: x => x.EnvioId,
                        principalTable: "JR_ENVIO",
                        principalColumn: "IdEnvio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JR_DELITODERIVADO_CAT_RDH_RDHId",
                        column: x => x.RDHId,
                        principalTable: "CAT_RDH",
                        principalColumn: "IdRDH",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JR_SESION",
                columns: table => new
                {
                    IdSesion = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    EnvioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    NoSesion = table.Column<int>(nullable: false),
                    FechaHoraSys = table.Column<DateTime>(type: "DateTime", nullable: true),
                    StatusSesion = table.Column<string>(nullable: true),
                    DescripcionSesion = table.Column<string>(nullable: true),
                    Asunto = table.Column<string>(nullable: true),
                    FechaHora = table.Column<DateTime>(nullable: false),
                    Solicitates = table.Column<string>(nullable: true),
                    Reuqeridos = table.Column<string>(nullable: true),
                    uf_Distrito = table.Column<string>(nullable: true),
                    uf_DirSubProc = table.Column<string>(nullable: true),
                    uf_Agencia = table.Column<string>(nullable: true),
                    uf_Modulo = table.Column<string>(nullable: true),
                    uf_Nombre = table.Column<string>(nullable: true),
                    uf_Puesto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JR_SESION", x => x.IdSesion);
                    table.ForeignKey(
                        name: "FK_JR_SESION_JR_ENVIO_EnvioId",
                        column: x => x.EnvioId,
                        principalTable: "JR_ENVIO",
                        principalColumn: "IdEnvio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JR_SESION_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JR_SOLICITANTEREQUERIDO",
                columns: table => new
                {
                    IdRSolicitanteRequerido = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    EnvioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    PersonaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    Clasificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JR_SOLICITANTEREQUERIDO", x => x.IdRSolicitanteRequerido);
                    table.ForeignKey(
                        name: "FK_JR_SOLICITANTEREQUERIDO_JR_ENVIO_EnvioId",
                        column: x => x.EnvioId,
                        principalTable: "JR_ENVIO",
                        principalColumn: "IdEnvio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JR_SOLICITANTEREQUERIDO_CAT_PERSONA_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "CAT_PERSONA",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI_SOLICITUDINTELIGENCIAASIG",
                columns: table => new
                {
                    IdSolicitudInteligenciaAsig = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    SolicitudInteligenciaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ModuloServicioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI_SOLICITUDINTELIGENCIAASIG", x => x.IdSolicitudInteligenciaAsig);
                    table.ForeignKey(
                        name: "FK_PI_SOLICITUDINTELIGENCIAASIG_C_MODULOSERVICIO_ModuloServicioId",
                        column: x => x.ModuloServicioId,
                        principalTable: "C_MODULOSERVICIO",
                        principalColumn: "IdModuloServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PI_SOLICITUDINTELIGENCIAASIG_PI_SOLICITUDINTELIGENCIA_SolicitudInteligenciaId",
                        column: x => x.SolicitudInteligenciaId,
                        principalTable: "PI_SOLICITUDINTELIGENCIA",
                        principalColumn: "IdSolicitudInteligencia",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JR_SEGUIMIENTOCUMPLIMIENTO",
                columns: table => new
                {
                    IdSeguimientoCumplimiento = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    AcuerdoReparatorioId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    NoParcialidad = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(type: "DateTime", nullable: false),
                    FechaProrroga = table.Column<DateTime>(nullable: true),
                    CantidadAPagar = table.Column<decimal>(nullable: false),
                    StatusPago = table.Column<string>(nullable: true),
                    Titulo = table.Column<string>(nullable: true),
                    Dirigidoa = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Solicitantes = table.Column<string>(nullable: true),
                    Requeridos = table.Column<string>(nullable: true),
                    FechaHoraCita = table.Column<DateTime>(nullable: true),
                    Texto = table.Column<string>(nullable: true),
                    uf_Distrito = table.Column<string>(nullable: true),
                    uf_DirSubProc = table.Column<string>(nullable: true),
                    uf_Agencia = table.Column<string>(nullable: true),
                    uf_Modulo = table.Column<string>(nullable: true),
                    uf_Nombre = table.Column<string>(nullable: true),
                    uf_Puesto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JR_SEGUIMIENTOCUMPLIMIENTO", x => x.IdSeguimientoCumplimiento);
                    table.ForeignKey(
                        name: "FK_JR_SEGUIMIENTOCUMPLIMIENTO_JR_ACUERDOREPARATORIO_AcuerdoReparatorioId",
                        column: x => x.AcuerdoReparatorioId,
                        principalTable: "JR_ACUERDOREPARATORIO",
                        principalColumn: "IdAcuerdoReparatorio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JR_CITATORIORECORDATORIO",
                columns: table => new
                {
                    IdCitatorioRecordatorio = table.Column<Guid>(type: "UNIQUEIDENTIFIER ROWGUIDCOL", nullable: false, defaultValueSql: "newId()"),
                    SesionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER ", nullable: false),
                    NoExpediente = table.Column<string>(nullable: true),
                    FechaSys = table.Column<DateTime>(type: "DateTime", nullable: false),
                    FechaHoraCita = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Duracion = table.Column<int>(nullable: false),
                    LugarCita = table.Column<string>(nullable: true),
                    dirigidoa_Nombre = table.Column<string>(nullable: true),
                    dirigidoa_Direccion = table.Column<string>(nullable: true),
                    dirigidoa_Telefono = table.Column<string>(nullable: true),
                    solicitadoPor = table.Column<string>(nullable: true),
                    solicitadoPor_Telefono = table.Column<string>(nullable: true),
                    Textooficio = table.Column<string>(nullable: true),
                    uf_Distrito = table.Column<string>(nullable: true),
                    uf_DirSubProc = table.Column<string>(nullable: true),
                    uf_Agencia = table.Column<string>(nullable: true),
                    uf_Modulo = table.Column<string>(nullable: true),
                    uf_Nombre = table.Column<string>(nullable: true),
                    uf_Puesto = table.Column<string>(nullable: true),
                    NoCitatorio = table.Column<int>(nullable: false),
                    StatusAsistencia = table.Column<bool>(nullable: false),
                    ContadorSMS = table.Column<int>(nullable: false),
                    StatusEntrega = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JR_CITATORIORECORDATORIO", x => x.IdCitatorioRecordatorio);
                    table.ForeignKey(
                        name: "FK_JR_CITATORIORECORDATORIO_JR_SESION_SesionId",
                        column: x => x.SesionId,
                        principalTable: "JR_SESION",
                        principalColumn: "IdSesion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_C_AGENCIA_DSPId",
                table: "C_AGENCIA",
                column: "DSPId");

            migrationBuilder.CreateIndex(
                name: "IX_C_DEPENDECIAS_DERIVACION_DistritoId",
                table: "C_DEPENDECIAS_DERIVACION",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_C_DSP_DistritoId",
                table: "C_DSP",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_C_FISCALIAOESTADOS_EstadoIdEstado",
                table: "C_FISCALIAOESTADOS",
                column: "EstadoIdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_C_FISCALIAOESTADOS_MunicipioIdMunicipio",
                table: "C_FISCALIAOESTADOS",
                column: "MunicipioIdMunicipio");

            migrationBuilder.CreateIndex(
                name: "IX_C_LOCALIDAD_MunicipioId",
                table: "C_LOCALIDAD",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_C_MODULOSERVICIO_AgenciaId",
                table: "C_MODULOSERVICIO",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_C_MUNICIPIO_EstadoId",
                table: "C_MUNICIPIO",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CA_PANELUSUARIO_PanelControlId",
                table: "CA_PANELUSUARIO",
                column: "PanelControlId");

            migrationBuilder.CreateIndex(
                name: "IX_CA_PANELUSUARIO_UsuarioId",
                table: "CA_PANELUSUARIO",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CA_USUARIO_ModuloServicioId",
                table: "CA_USUARIO",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_CA_USUARIO_RolId",
                table: "CA_USUARIO",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_CAR_ARMAOBJETO_ClasificacionArmaId",
                table: "CAR_ARMAOBJETO",
                column: "ClasificacionArmaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_AMPDEC_HechoId",
                table: "CAT_AMPDEC",
                column: "HechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_AMPDEC_PersonaId",
                table: "CAT_AMPDEC",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_AMPOTURNO_ModuloServicioId",
                table: "CAT_AMPOTURNO",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_AMPOTURNO_TurnoId",
                table: "CAT_AMPOTURNO",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ARCHIVOS_RHechoId",
                table: "CAT_ARCHIVOS",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ARMA_RHechoId",
                table: "CAT_ARMA",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_BITACORA_IdPersona",
                table: "CAT_BITACORA",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_BITACORA_rHechoId",
                table: "CAT_BITACORA",
                column: "rHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_C5FORMATOS_RHechoId",
                table: "CAT_C5FORMATOS",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_CONDIMPPRO_PersonaId",
                table: "CAT_CONDIMPPRO",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_CONDIMPPRO_RHechoId",
                table: "CAT_CONDIMPPRO",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_DATO_PROTEGIDO_RAPId",
                table: "CAT_DATO_PROTEGIDO",
                column: "RAPId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_DATOSRELACIONADOS_RHechoId",
                table: "CAT_DATOSRELACIONADOS",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_DETALLE_SEGUIMIENTO_IndiciosId",
                table: "CAT_DETALLE_SEGUIMIENTO",
                column: "IndiciosId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_DIRECCION_DELITO_RHechoId",
                table: "CAT_DIRECCION_DELITO",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_DIRECCION_ESCUCHA_RAPId",
                table: "CAT_DIRECCION_ESCUCHA",
                column: "RAPId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_DIRECCION_PERSONAL_PersonaId",
                table: "CAT_DIRECCION_PERSONAL",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_DOCSPERSONA_PersonaId",
                table: "CAT_DOCSPERSONA",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_DOCSREPRESENTANTES_RepresentanteId",
                table: "CAT_DOCSREPRESENTANTES",
                column: "RepresentanteId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_INCOMPETENCIA_RHechoId",
                table: "CAT_INCOMPETENCIA",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_INDICIOS_RHechoId",
                table: "CAT_INDICIOS",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_JUZGADOS_AGENCIAS_DistritoId",
                table: "CAT_JUZGADOS_AGENCIAS",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_MEDIAAFILIACION_PersonaId",
                table: "CAT_MEDIAAFILIACION",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_MEDIAAFILIACION_RHechoId",
                table: "CAT_MEDIAAFILIACION",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_MEDIDASCAUTELARES_PersonaId",
                table: "CAT_MEDIDASCAUTELARES",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_MEDIDASCAUTELARES_RHechoId",
                table: "CAT_MEDIDASCAUTELARES",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_MEDIDASPROTECCION_RHechoId",
                table: "CAT_MEDIDASPROTECCION",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_NOEJERCICIOAPENAL_RHechoId",
                table: "CAT_NOEJERCICIOAPENAL",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_OFICIO_RHechoId",
                table: "CAT_OFICIO",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RACTOSINDETALLES_RActosInvestigacionId",
                table: "CAT_RACTOSINDETALLES",
                column: "RActosInvestigacionId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RACTOSINVESTIGACION_RHechoId",
                table: "CAT_RACTOSINVESTIGACION",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RAP_PersonaId",
                table: "CAT_RAP",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RAP_RAtencionId",
                table: "CAT_RAP",
                column: "RAtencionId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RATENCON_racId",
                table: "CAT_RATENCON",
                column: "racId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RCITATORIO_NOTIFICACION_RHechoId",
                table: "CAT_RCITATORIO_NOTIFICACION",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RDDERIVACION_idDDerivacion",
                table: "CAT_RDDERIVACION",
                column: "idDDerivacion");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RDDERIVACION_rHechoId",
                table: "CAT_RDDERIVACION",
                column: "rHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RDH_DelitoId",
                table: "CAT_RDH",
                column: "DelitoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RDH_RHechoId",
                table: "CAT_RDH",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RDILIGENCIAS_ASPId",
                table: "CAT_RDILIGENCIAS",
                column: "ASPId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RDILIGENCIAS_rHechoId",
                table: "CAT_RDILIGENCIAS",
                column: "rHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_REPRESENTANTES_PersonaId",
                table: "CAT_REPRESENTANTES",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_REPRESENTANTES_RHechoId",
                table: "CAT_REPRESENTANTES",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RHECHO_Agenciaid",
                table: "CAT_RHECHO",
                column: "Agenciaid");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RHECHO_ModuloServicioId",
                table: "CAT_RHECHO",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RHECHO_NucId",
                table: "CAT_RHECHO",
                column: "NucId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_RHECHO_RAtencionId",
                table: "CAT_RHECHO",
                column: "RAtencionId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_TERMINACIONARCHIVO_RHechoId",
                table: "CAT_TERMINACIONARCHIVO",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_TURNO_AgenciaId",
                table: "CAT_TURNO",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_TURNO_RAtencionId",
                table: "CAT_TURNO",
                column: "RAtencionId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_VEHICULO_RHechoId",
                table: "CAT_VEHICULO",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_CV_MODELO_MarcaId",
                table: "CV_MODELO",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_IL_AGENDA_RHechoId",
                table: "IL_AGENDA",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_IL_CITATORIO_RHechoId",
                table: "IL_CITATORIO",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_IL_SOLICITUDES_DE_INFORMACION_RHechoId",
                table: "IL_SOLICITUDES_DE_INFORMACION",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_ACUERDOREPARATORIO_EnvioId",
                table: "JR_ACUERDOREPARATORIO",
                column: "EnvioId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_ASINGACIONENVIO_EnvioId",
                table: "JR_ASINGACIONENVIO",
                column: "EnvioId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_ASINGACIONENVIO_ModuloServicioId",
                table: "JR_ASINGACIONENVIO",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_CITATORIORECORDATORIO_SesionId",
                table: "JR_CITATORIORECORDATORIO",
                column: "SesionId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_DELITODERIVADO_EnvioId",
                table: "JR_DELITODERIVADO",
                column: "EnvioId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_DELITODERIVADO_RDHId",
                table: "JR_DELITODERIVADO",
                column: "RDHId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_ENVIO_ExpedienteId",
                table: "JR_ENVIO",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_EXPEDIENTE_RHechoId",
                table: "JR_EXPEDIENTE",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_FACILITADORNOTIFICADOR_ModuloServicioId",
                table: "JR_FACILITADORNOTIFICADOR",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_REGISTROCONCLUSION_ExpedienteId",
                table: "JR_REGISTROCONCLUSION",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_REGISTRONOTIFICACION_ExpedienteId",
                table: "JR_REGISTRONOTIFICACION",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_SEGUIMIENTOCUMPLIMIENTO_AcuerdoReparatorioId",
                table: "JR_SEGUIMIENTOCUMPLIMIENTO",
                column: "AcuerdoReparatorioId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_SESION_EnvioId",
                table: "JR_SESION",
                column: "EnvioId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_SESION_ModuloServicioId",
                table: "JR_SESION",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_SOLICITANTEREQUERIDO_EnvioId",
                table: "JR_SOLICITANTEREQUERIDO",
                column: "EnvioId");

            migrationBuilder.CreateIndex(
                name: "IX_JR_SOLICITANTEREQUERIDO_PersonaId",
                table: "JR_SOLICITANTEREQUERIDO",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_NUC_AgenciaId",
                table: "NUC",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_NUC_DistritoId",
                table: "NUC",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_OAprhensionBitacoras_OrdenAprensionId",
                table: "OAprhensionBitacoras",
                column: "OrdenAprensionId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_ARCHIVOSCOMPARECENCIA_PresentacionesYCId",
                table: "PI_ARCHIVOSCOMPARECENCIA",
                column: "PresentacionesYCId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_ARCHIVOSMEDIAAFILIACION_MediaAfiliacionid",
                table: "PI_ARCHIVOSMEDIAAFILIACION",
                column: "MediaAfiliacionid");

            migrationBuilder.CreateIndex(
                name: "IX_PI_ARCHIVOSOAPRENSION_OrdenAprensionId",
                table: "PI_ARCHIVOSOAPRENSION",
                column: "OrdenAprensionId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_CMEDICOPR_ModuloServicioId",
                table: "PI_CMEDICOPR",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_CMEDICOPR_PersonaId",
                table: "PI_CMEDICOPR",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_CMEDICOPSR_ModuloServicioId",
                table: "PI_CMEDICOPSR",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_DETENCION_PersonaId",
                table: "PI_DETENCION",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_DETENCION_RHechoId",
                table: "PI_DETENCION",
                column: "RHechoId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_DIRECCION_PIPersonaVisitaId",
                table: "PI_DIRECCION",
                column: "PIPersonaVisitaId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_EGRESO_TEMPORALES_DetencionId",
                table: "PI_EGRESO_TEMPORALES",
                column: "DetencionId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_ESTATUS_CUSTODIA_DetencionId",
                table: "PI_ESTATUS_CUSTODIA",
                column: "DetencionId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_FOTOS_RActoInvestigacionId",
                table: "PI_FOTOS",
                column: "RActoInvestigacionId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_FPERSONAS_PIPersonaVisitaId",
                table: "PI_FPERSONAS",
                column: "PIPersonaVisitaId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_HISTORIALDETENCIONES_DetencionId",
                table: "PI_HISTORIALDETENCIONES",
                column: "DetencionId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_IJ_ARRESTO_ModuloServicioId",
                table: "PI_IJ_ARRESTO",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_IJ_BUSQUEDA_DOMICILIO_ModuloServicioId",
                table: "PI_IJ_BUSQUEDA_DOMICILIO",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_IJ_COMPARECENCIA_ELEMENTOS_ModuloServicioId",
                table: "PI_IJ_COMPARECENCIA_ELEMENTOS",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_IJ_EXHORTO_ModuloServicioId",
                table: "PI_IJ_EXHORTO",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_IJ_ORDEN_APRENSION_ModuloServicioId",
                table: "PI_IJ_ORDEN_APRENSION",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_IJ_PRESENTACIONES_Y_C_ModuloServicioId",
                table: "PI_IJ_PRESENTACIONES_Y_C",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_IJ_REQUERIMIENTOS_VARIOS_ModuloServicioId",
                table: "PI_IJ_REQUERIMIENTOS_VARIOS",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_IJ_TRASLADOSYCUSTODIAS_ModuloServicioId",
                table: "PI_IJ_TRASLADOSYCUSTODIAS",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_INFORMES_PeritoAsignadoPIId",
                table: "PI_INFORMES",
                column: "PeritoAsignadoPIId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_PERITOSASIGNADOS_ModuloServicioId",
                table: "PI_PERITOSASIGNADOS",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_PERITOSASIGNADOS_RActoInvestigacionId",
                table: "PI_PERITOSASIGNADOS",
                column: "RActoInvestigacionId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_SOLICITUDINTELIGENCIA_PeritoAsignadoPIId",
                table: "PI_SOLICITUDINTELIGENCIA",
                column: "PeritoAsignadoPIId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_SOLICITUDINTELIGENCIAASIG_ModuloServicioId",
                table: "PI_SOLICITUDINTELIGENCIAASIG",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_SOLICITUDINTELIGENCIAASIG_SolicitudInteligenciaId",
                table: "PI_SOLICITUDINTELIGENCIAASIG",
                column: "SolicitudInteligenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_SUBIRARCHIVO_DetencionId",
                table: "PI_SUBIRARCHIVO",
                column: "DetencionId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_VISITA_DetencionId",
                table: "PI_VISITA",
                column: "DetencionId");

            migrationBuilder.CreateIndex(
                name: "IX_PI_VISITA_PIPersonaVisitaId",
                table: "PI_VISITA",
                column: "PIPersonaVisitaId");

            migrationBuilder.CreateIndex(
                name: "IX_RAC_AgenciaId",
                table: "RAC",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_RAC_DistritoId",
                table: "RAC",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_SP_ASP_AgenciaId",
                table: "SP_ASP",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_SP_ASP_ServicioPericialId",
                table: "SP_ASP",
                column: "ServicioPericialId");

            migrationBuilder.CreateIndex(
                name: "IX_SP_DILIGENCIAINDICIO_IndiciosId",
                table: "SP_DILIGENCIAINDICIO",
                column: "IndiciosId");

            migrationBuilder.CreateIndex(
                name: "IX_SP_DILIGENCIAINDICIO_RDiligenciasId",
                table: "SP_DILIGENCIAINDICIO",
                column: "RDiligenciasId");

            migrationBuilder.CreateIndex(
                name: "IX_SP_DOCSDILIGENCIA_RDiligenciasId",
                table: "SP_DOCSDILIGENCIA",
                column: "RDiligenciasId");

            migrationBuilder.CreateIndex(
                name: "IX_SP_PERITOSASIGNADOS_ModuloServicioId",
                table: "SP_PERITOSASIGNADOS",
                column: "ModuloServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_SP_PERITOSASIGNADOS_RDiligenciasId",
                table: "SP_PERITOSASIGNADOS",
                column: "RDiligenciasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "C_ COLOR_OJOS");

            migrationBuilder.DropTable(
                name: "C_ACTOSINVESTIGACION");

            migrationBuilder.DropTable(
                name: "C_CANTIDAD_DE_CABELLO");

            migrationBuilder.DropTable(
                name: "C_CLASIFICACIONPERSONA");

            migrationBuilder.DropTable(
                name: "C_COLOR_DE_CABELLO");

            migrationBuilder.DropTable(
                name: "C_COMPLEXION");

            migrationBuilder.DropTable(
                name: "C_CONFGLOBAL");

            migrationBuilder.DropTable(
                name: "C_DISCAPACIDAD");

            migrationBuilder.DropTable(
                name: "C_DOCIDENTIFICACION");

            migrationBuilder.DropTable(
                name: "C_ESTADO_CIVIL");

            migrationBuilder.DropTable(
                name: "C_FISCALIAOESTADOS");

            migrationBuilder.DropTable(
                name: "C_FORMA_DE_CABELLO");

            migrationBuilder.DropTable(
                name: "C_FORMA_DE_CARA");

            migrationBuilder.DropTable(
                name: "C_FORMA_DE_CEJAS");

            migrationBuilder.DropTable(
                name: "C_FORMA_DE_MENTON");

            migrationBuilder.DropTable(
                name: "C_GENERO");

            migrationBuilder.DropTable(
                name: "C_GROSOR_DE_LABIOS");

            migrationBuilder.DropTable(
                name: "C_INCOMPETENCIA");

            migrationBuilder.DropTable(
                name: "C_LARGO_DE_CABELLO");

            migrationBuilder.DropTable(
                name: "C_LENGUA");

            migrationBuilder.DropTable(
                name: "C_LOCALIDAD");

            migrationBuilder.DropTable(
                name: "C_MEDIONOTIFICACION");

            migrationBuilder.DropTable(
                name: "C_NACIONALIDAD");

            migrationBuilder.DropTable(
                name: "C_NIVELESTUDIOS");

            migrationBuilder.DropTable(
                name: "C_OCUPACION");

            migrationBuilder.DropTable(
                name: "C_RELIGION");

            migrationBuilder.DropTable(
                name: "C_SEXO");

            migrationBuilder.DropTable(
                name: "C_STATUSNUC");

            migrationBuilder.DropTable(
                name: "C_TAMAÑO_DE_BOCA");

            migrationBuilder.DropTable(
                name: "C_TAMAÑO_NARIZ");

            migrationBuilder.DropTable(
                name: "C_TEZ");

            migrationBuilder.DropTable(
                name: "C_TIPO_DE_CEJAS");

            migrationBuilder.DropTable(
                name: "C_TIPO_DE_FRENTE");

            migrationBuilder.DropTable(
                name: "C_TIPO_DE_NARIZ");

            migrationBuilder.DropTable(
                name: "C_TIPO_DE_OREJAS");

            migrationBuilder.DropTable(
                name: "C_TIPO_OJOS");

            migrationBuilder.DropTable(
                name: "C_TIPOSREPRESENTANTES");

            migrationBuilder.DropTable(
                name: "CA_ALMACENAMIENTO");

            migrationBuilder.DropTable(
                name: "CA_PANELUSUARIO");

            migrationBuilder.DropTable(
                name: "CAR_ARMAOBJETO");

            migrationBuilder.DropTable(
                name: "CAR_CALIBRE");

            migrationBuilder.DropTable(
                name: "CAR_MARCA_ARMA");

            migrationBuilder.DropTable(
                name: "CAT_AMPDEC");

            migrationBuilder.DropTable(
                name: "CAT_AMPOTURNO");

            migrationBuilder.DropTable(
                name: "CAT_ARCHIVOS");

            migrationBuilder.DropTable(
                name: "CAT_ARMA");

            migrationBuilder.DropTable(
                name: "CAT_BITACORA");

            migrationBuilder.DropTable(
                name: "CAT_C5FORMATOS");

            migrationBuilder.DropTable(
                name: "CAT_CONDIMPPRO");

            migrationBuilder.DropTable(
                name: "CAT_DATO_PROTEGIDO");

            migrationBuilder.DropTable(
                name: "CAT_DATOSRELACIONADOS");

            migrationBuilder.DropTable(
                name: "CAT_DETALLE_SEGUIMIENTO");

            migrationBuilder.DropTable(
                name: "CAT_DIRECCION_DELITO");

            migrationBuilder.DropTable(
                name: "CAT_DIRECCION_ESCUCHA");

            migrationBuilder.DropTable(
                name: "CAT_DIRECCION_PERSONAL");

            migrationBuilder.DropTable(
                name: "CAT_DOCSPERSONA");

            migrationBuilder.DropTable(
                name: "CAT_DOCSREPRESENTANTES");

            migrationBuilder.DropTable(
                name: "CAT_INCOMPETENCIA");

            migrationBuilder.DropTable(
                name: "CAT_JUZGADOS_AGENCIAS");

            migrationBuilder.DropTable(
                name: "CAT_MEDIDASCAUTELARES");

            migrationBuilder.DropTable(
                name: "CAT_MEDIDASPROTECCION");

            migrationBuilder.DropTable(
                name: "CAT_MULTACITATORIO");

            migrationBuilder.DropTable(
                name: "CAT_NOEJERCICIOAPENAL");

            migrationBuilder.DropTable(
                name: "CAT_OFICIO");

            migrationBuilder.DropTable(
                name: "CAT_RACTOSINDETALLES");

            migrationBuilder.DropTable(
                name: "CAT_RCITATORIO_NOTIFICACION");

            migrationBuilder.DropTable(
                name: "CAT_RDDERIVACION");

            migrationBuilder.DropTable(
                name: "CAT_TERMINACIONARCHIVO");

            migrationBuilder.DropTable(
                name: "CAT_VEHICULO");

            migrationBuilder.DropTable(
                name: "CD_CLAORDRES");

            migrationBuilder.DropTable(
                name: "CD_INTESIONDELITO");

            migrationBuilder.DropTable(
                name: "CD_RESULTADODELITO");

            migrationBuilder.DropTable(
                name: "CD_TIPO");

            migrationBuilder.DropTable(
                name: "CD_TIPODECLARACION");

            migrationBuilder.DropTable(
                name: "CD_TIPOFUERO");

            migrationBuilder.DropTable(
                name: "CI_Institucion");

            migrationBuilder.DropTable(
                name: "CI_Tipo");

            migrationBuilder.DropTable(
                name: "CV_ANO");

            migrationBuilder.DropTable(
                name: "CV_COLOR");

            migrationBuilder.DropTable(
                name: "CV_MODELO");

            migrationBuilder.DropTable(
                name: "CV_TIPOV");

            migrationBuilder.DropTable(
                name: "IL_AGENDA");

            migrationBuilder.DropTable(
                name: "IL_CITATORIO");

            migrationBuilder.DropTable(
                name: "IL_SOLICITUDES_DE_INFORMACION");

            migrationBuilder.DropTable(
                name: "JR_ASINGACIONENVIO");

            migrationBuilder.DropTable(
                name: "JR_CITATORIORECORDATORIO");

            migrationBuilder.DropTable(
                name: "JR_DELITODERIVADO");

            migrationBuilder.DropTable(
                name: "JR_FACILITADORNOTIFICADOR");

            migrationBuilder.DropTable(
                name: "JR_REGISTROCONCLUSION");

            migrationBuilder.DropTable(
                name: "JR_REGISTRONOTIFICACION");

            migrationBuilder.DropTable(
                name: "JR_SEGUIMIENTOCUMPLIMIENTO");

            migrationBuilder.DropTable(
                name: "JR_SOLICITANTEREQUERIDO");

            migrationBuilder.DropTable(
                name: "OAprhensionBitacoras");

            migrationBuilder.DropTable(
                name: "PI_ARCHIVOSCOMPARECENCIA");

            migrationBuilder.DropTable(
                name: "PI_ARCHIVOSMEDIAAFILIACION");

            migrationBuilder.DropTable(
                name: "PI_ARCHIVOSOAPRENSION");

            migrationBuilder.DropTable(
                name: "PI_CMEDICOPR");

            migrationBuilder.DropTable(
                name: "PI_CMEDICOPSR");

            migrationBuilder.DropTable(
                name: "PI_DIRECCION");

            migrationBuilder.DropTable(
                name: "PI_EGRESO_TEMPORALES");

            migrationBuilder.DropTable(
                name: "PI_ESTATUS_CUSTODIA");

            migrationBuilder.DropTable(
                name: "PI_FOTOS");

            migrationBuilder.DropTable(
                name: "PI_FPERSONAS");

            migrationBuilder.DropTable(
                name: "PI_HISTORIALDETENCIONES");

            migrationBuilder.DropTable(
                name: "PI_IJ_ARRESTO");

            migrationBuilder.DropTable(
                name: "PI_IJ_BUSQUEDA_DOMICILIO");

            migrationBuilder.DropTable(
                name: "PI_IJ_COMPARECENCIA_ELEMENTOS");

            migrationBuilder.DropTable(
                name: "PI_IJ_EXHORTO");

            migrationBuilder.DropTable(
                name: "PI_IJ_REQUERIMIENTOS_VARIOS");

            migrationBuilder.DropTable(
                name: "PI_IJ_TRASLADOSYCUSTODIAS");

            migrationBuilder.DropTable(
                name: "PI_INFORMES");

            migrationBuilder.DropTable(
                name: "PI_SOLICITUDINTELIGENCIAASIG");

            migrationBuilder.DropTable(
                name: "PI_SUBIRARCHIVO");

            migrationBuilder.DropTable(
                name: "PI_VISITA");

            migrationBuilder.DropTable(
                name: "SP_DILIGENCIAINDICIO");

            migrationBuilder.DropTable(
                name: "SP_DOCSDILIGENCIA");

            migrationBuilder.DropTable(
                name: "SP_PERITOSASIGNADOS");

            migrationBuilder.DropTable(
                name: "C_MUNICIPIO");

            migrationBuilder.DropTable(
                name: "PC_PANELCONTROL");

            migrationBuilder.DropTable(
                name: "CA_USUARIO");

            migrationBuilder.DropTable(
                name: "CAR_CLASIFICACIONARMAOB");

            migrationBuilder.DropTable(
                name: "CAT_TURNO");

            migrationBuilder.DropTable(
                name: "CAT_RAP");

            migrationBuilder.DropTable(
                name: "CAT_REPRESENTANTES");

            migrationBuilder.DropTable(
                name: "C_DEPENDECIAS_DERIVACION");

            migrationBuilder.DropTable(
                name: "CV_MARCA");

            migrationBuilder.DropTable(
                name: "JR_SESION");

            migrationBuilder.DropTable(
                name: "CAT_RDH");

            migrationBuilder.DropTable(
                name: "JR_ACUERDOREPARATORIO");

            migrationBuilder.DropTable(
                name: "PI_IJ_PRESENTACIONES_Y_C");

            migrationBuilder.DropTable(
                name: "CAT_MEDIAAFILIACION");

            migrationBuilder.DropTable(
                name: "PI_IJ_ORDEN_APRENSION");

            migrationBuilder.DropTable(
                name: "PI_SOLICITUDINTELIGENCIA");

            migrationBuilder.DropTable(
                name: "PI_DETENCION");

            migrationBuilder.DropTable(
                name: "PI_PERSONAVISITA");

            migrationBuilder.DropTable(
                name: "CAT_INDICIOS");

            migrationBuilder.DropTable(
                name: "CAT_RDILIGENCIAS");

            migrationBuilder.DropTable(
                name: "C_ESTADO");

            migrationBuilder.DropTable(
                name: "CA_ROL");

            migrationBuilder.DropTable(
                name: "C_DELITO");

            migrationBuilder.DropTable(
                name: "JR_ENVIO");

            migrationBuilder.DropTable(
                name: "PI_PERITOSASIGNADOS");

            migrationBuilder.DropTable(
                name: "CAT_PERSONA");

            migrationBuilder.DropTable(
                name: "SP_ASP");

            migrationBuilder.DropTable(
                name: "JR_EXPEDIENTE");

            migrationBuilder.DropTable(
                name: "CAT_RACTOSINVESTIGACION");

            migrationBuilder.DropTable(
                name: "SP_SERVICIOSPERICIALES");

            migrationBuilder.DropTable(
                name: "CAT_RHECHO");

            migrationBuilder.DropTable(
                name: "C_MODULOSERVICIO");

            migrationBuilder.DropTable(
                name: "NUC");

            migrationBuilder.DropTable(
                name: "CAT_RATENCON");

            migrationBuilder.DropTable(
                name: "RAC");

            migrationBuilder.DropTable(
                name: "C_AGENCIA");

            migrationBuilder.DropTable(
                name: "C_DSP");

            migrationBuilder.DropTable(
                name: "C_DISTRITO");
        }
    }
}
