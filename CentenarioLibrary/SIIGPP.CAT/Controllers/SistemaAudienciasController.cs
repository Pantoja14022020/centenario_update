using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;

using SIIGPP.CAT.Models.SistemaAudiencias;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Representantes;
using System.Data.SqlClient;
using SIIGPP.CAT.Models.RDHechos;
using Microsoft.Extensions.Options;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;


namespace SIIGPP.CAT.Controllers
{
    //Se ha creado un nuevo controlador para la denucia en linea
    [Route("api/[controller]")]
    [ApiController]
    public class SistemaAudienciasController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public SistemaAudienciasController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        ////Comprobar si el usuario de centenario tiene en su disposicion la carpeta que requiere crear una audiencia
        //// GET: api/SistemaAudiencias/ComprobarAsignacionNuc
        //[HttpGet("[action]/{nuc}/{usuariocentenario}")]
        //public async Task<IActionResult> ComprobarAsignacionNuc([FromRoute] string nuc, string usuariocentenario)
        //{         
        //    //Se hace la cnexion directa a la base de datos de pachuca para obtener la adscripcion del usuario
        //    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-7F662EC1-6705-406E-BCD0-F56ADE7BCAE2".ToUpper())).Options;
        //    using (var ctx = new DbContextSIIGPP(options))
        //    {
        //        var usuario = await ctx.Usuarios
        //        .Include(a => a.ModuloServicio.Agencia.DSP.Distrito)
        //        .Where(a => a.usuario == usuariocentenario)
        //        .FirstOrDefaultAsync();

        //        //En caso de que el usuario no exista se dara aviso y se dara un status
        //        if (usuario == null)
        //        {
        //            return Ok(new { datos = new[] { new { aviso = "El usuario no existe en el Sistema Centenario", status = false }} });

        //        }

        //        //Se guarda una variable con el dato del id del distrito para hacer la conexion y saber si tiene dicha carpeta asignada 
        //        Guid Iddistrito = usuario.ModuloServicio.Agencia.DSP.Distrito.IdDistrito;
        //        Guid IdModuloservicio = usuario.ModuloServicioId;
        //        string nombreMP = usuario.nombre;

        //        var options2 = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + Iddistrito.ToString().ToUpper())).Options;
        //        using (var ctx2 = new DbContextSIIGPP(options))
        //        {

        //            var existenciaNuc = await ctx2.RHechoes
        //            .Include(a => a.NUCs)
        //            .Where(a => a.NUCs.nucg == nuc)
        //            .FirstOrDefaultAsync();


        //            if (existenciaNuc == null)
        //            {
        //                return Ok( new{ datos = new[] { new { aviso = "La carpeta de investigación no existe, comprueba la forma en que se esta escribiendo", status = false }} });

        //            }

        //            var asignacion = await ctx2.RHechoes
        //            .Include(a => a.NUCs)
        //            .Include(a => a.ModuloServicio.Agencia.DSP.Distrito)
        //            .Where(a => a.NUCs.nucg == nuc)
        //            .Where(a => a.ModuloServicioId == IdModuloservicio)
        //            .FirstOrDefaultAsync();

        //            //Adtraego el valor de rhecho
        //            Guid rhechoid = asignacion.IdRHecho;

        //            //Consulto la direccion del suceso
        //            var direccionSuceso = await ctx2.DireccionDelitos
        //            .Where(a => a.RHechoId == rhechoid)
        //            .FirstOrDefaultAsync();

        //            var tiposVialidades = await _context.Vialidades.ToListAsync();

        //            if (asignacion == null)
        //            {
        //                return Ok( new{ datos = new[] { new { aviso = "No tienes asignada la carpeta de investigación", status = false}}});

        //            }

        //            var datos = new DatosNUCViewModel
        //            {
        //                idDistrito = asignacion.ModuloServicio.Agencia.DSP.Distrito.IdDistrito,
        //                idHecho = asignacion.IdRHecho,
        //                idAtencion = asignacion.RAtencionId,
        //                idNuc = asignacion.NUCs.idNuc,
        //                nuc = asignacion.NUCs.nucg,
        //                nombreMP = nombreMP,
        //                FechaHoraSuceso = asignacion.FechaHoraSuceso,
        //                DireccionSuceso = (direccionSuceso is null ? "Sin direccion registrada" : 
        //                                    $"{(direccionSuceso.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(v => v.Clave == direccionSuceso.TipoVialidad)?.Nombre + " " : "")}" +
        //                                    $"{(direccionSuceso.Calle == "" ? "" : direccionSuceso.Calle + " ")}" +
        //                                    $"{(direccionSuceso.NoInt == "" ? "" : direccionSuceso.NoInt + " ")}" +
        //                                    $"{(direccionSuceso.Localidad == "" ? "" : direccionSuceso.Localidad + " ")}" +
        //                                    $"{(direccionSuceso.CP == null ? "" : direccionSuceso.CP + " ")}" +
        //                                    $"{(direccionSuceso.Municipio == "" ? "" : direccionSuceso.Municipio + " ")}" +
        //                                    $"{(direccionSuceso.Estado == "" ? "" : direccionSuceso.Estado + " ")}" +
        //                                    $"{(direccionSuceso.Pais == "" ? "" : direccionSuceso.Pais)}"),
        //                status = true,
        //                aviso = "Informacion de carpeta obtenida",
        //            };

        //            return Ok(new { datos = datos });
        //        }
        //    }
        //}

        //// GET: api/SistemaAudiencias/ListarPersonas
        ////[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        //[HttpGet("[action]/{idRAtencion}/{clasificacion}/{idDistrito}")]
        //public async Task<IActionResult> ListarPersonas([FromRoute] Guid idRAtencion, string clasificacion, Guid idDistrito)
        //{
        //    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + idDistrito.ToString().ToUpper())).Options;
        //    using (var ctx = new DbContextSIIGPP(options))
        //    {

        //        var personas = await ctx.RAPs
        //                            .Include(a => a.Persona)
        //                            .Include(a => a.Persona.DireccionPersonal)
        //                            .Where(a => a.ClasificacionPersona == clasificacion)
        //                            .Where(x => x.RAtencionId == idRAtencion)
        //                            .ToListAsync();



        //        var datos = personas.Select(d => new PersonasNucviewModel
        //        {
        //            idRAP = d.IdRAP,
        //            rAtencionId = d.RAtencionId,
        //            idPersona = d.PersonaId,
        //            /*********************************************/

        //            /*********************************************/
        //            /*CAT_PERSONA*/
        //            statusAnonimo = d.Persona.StatusAnonimo,
        //            clasificacionPersona = d.ClasificacionPersona,
        //            pInicio = d.PInicio,
        //            tipoPersona = d.Persona.TipoPersona,
        //            nombreCompleto = d.Persona.Nombre + " " + d.Persona.ApellidoPaterno + " " + d.Persona.ApellidoMaterno,
        //            Nombre = d.Persona.Nombre,
        //            ApellidoPaterno = d.Persona.ApellidoPaterno,
        //            ApellidoMaterno = d.Persona.ApellidoMaterno,
        //            Alias = d.Persona.Alias,
        //            FechaNacimiento = d.Persona.FechaNacimiento,
        //            EntidadFederativa = d.Persona.EntidadFederativa,
        //            rfc = d.Persona.RFC,
        //            razonSocial = d.Persona.RazonSocial,
        //            DocIdentificacion = d.Persona.DocIdentificacion,
        //            CURP = d.Persona.CURP,
        //            //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
        //            PoblacionAfro = (bool)(d.Persona.PoblacionAfro == null ? false : d.Persona.PoblacionAfro),
        //            RangoEdad = d.Persona.RangoEdad,
        //            RangoEdadTF = (bool)(d.Persona.RangoEdadTF == null ? false : d.Persona.RangoEdadTF),
        //            //--------------------------------------------------------------------------------------------------
        //            PoliciaDetuvo = d.Persona.PoliciaDetuvo,
        //            Sexo = d.Persona.Sexo,
        //            Genero = d.Persona.Genero,
        //            Registro = d.Persona.Registro,
        //            VerR = d.Persona.VerR.GetValueOrDefault(false),
        //            VerI = d.Persona.VerI.GetValueOrDefault(false),
        //            EstadoCivil = d.Persona.EstadoCivil,
        //            Telefono1 = d.Persona.Telefono1,
        //            Telefono2 = d.Persona.Telefono2,
        //            Correo = d.Persona.Correo,
        //            Medionotificacion = d.Persona.Medionotificacion,
        //            Nacionalidad = d.Persona.Nacionalidad,
        //            Ocupacion = d.Persona.Ocupacion,
        //            Lengua = d.Persona.Lengua,
        //            Religion = d.Persona.Religion,
        //            Discapacidad = d.Persona.Discapacidad,
        //            TipoDiscapacidad = d.Persona.TipoDiscapacidad,
        //            Parentesco = d.Persona.Parentesco,
        //            InstitutoPolicial = d.Persona.InstitutoPolicial,
        //            NivelEstudio = d.Persona.NivelEstudio,
        //            Edad = d.Persona.Edad,
        //            DatosProtegidos = d.Persona.DatosProtegidos,
        //            DocPoderNotarial = d.Persona.DocPoderNotarial,
        //            CumpleRequisitoLey = d.Persona.CumpleRequisitoLey,
        //            DecretoLibertad = d.Persona.DecretoLibertad,
        //            DispusoLibertad = d.Persona.DispusoLibertad,
        //            direccion = d.Persona.DireccionPersonal.Calle +
        //                        (d.Persona.DireccionPersonal.NoInt != "0" ? ", No.Int " +
        //                        d.Persona.DireccionPersonal.NoInt : "") + ", No.Ext " +
        //                        d.Persona.DireccionPersonal.NoExt + ", CP " +
        //                        d.Persona.DireccionPersonal.CP + ", " +
        //                        d.Persona.DireccionPersonal.Localidad + ", " +
        //                        d.Persona.DireccionPersonal.Municipio + ", " +
        //                        d.Persona.DireccionPersonal.Estado,

        //        });

        //        return Ok(new { datos = datos });
        //    }
        //}

        //// GET: api/SistemaAudiencias/ListarRepresentantesActivos
        ////[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        //[HttpGet("[action]/{personaid}/{idDistrito}")]
        //public async Task<IActionResult> ListarRepresentantesActivos([FromRoute] Guid personaid, Guid idDistrito)
        //{
        //    try
        //    {
        //        var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + idDistrito.ToString().ToUpper())).Options;
        //        using (var ctx = new DbContextSIIGPP(options))
        //        {
        //            String busquedaRepresentantes = @"SELECT
        //             R.IdRepresentante
        //            ,R.RHechoId
        //            ,R.PersonaId
        //            ,CONCAT(P.Nombre,' ',ApellidoPaterno,' ',ApellidoMaterno) as PersonaR
        //            ,R.Nombres
        //            ,R.ApellidoPa
        //            ,R.ApellidoMa
        //            ,R.FechaNacimiento
        //            ,R.Sexo
        //            ,R.EntidadFeNacimiento
        //            ,R.Curp
        //            ,R.MedioNotificacion
        //            ,R.Telefono
        //            ,R.CorreoElectronico
        //            ,R.Nacionalidad
        //            ,R.Genero
        //            ,R.Tipo1
        //            ,R.Tipo2
        //            ,(CASE WHEN TR.Tipo IS NULL THEN 'NINGUNO' ELSE TR.Tipo END) as TipoRep1
        //            ,(CASE WHEN TR2.Tipo IS NULL THEN 'NINGUNO' ELSE TR2.Tipo END) as TipoRep2
        //            ,R.CedulaProfesional
        //            ,R.Fecha
        //            ,R.Calle
        //            ,R.UDistrito
        //            ,R.USubproc
        //            ,R.UAgencia
        //            ,R.Usuario
        //            ,R.UPuesto
        //            ,R.UModulo
        //            ,R.Fechasys
        //            ,R.NoInt
        //            ,R.NoExt
        //            ,R.EntreCalle1
        //            ,R.EntreCalle2
        //            ,R.Referencia
        //            ,R.Pais
        //            ,R.Estado
        //            ,R.Municipio
        //            ,R.Localidad
        //            ,R.CP
        //            ,R.lat
        //            ,R.lng
        //            ,R.ArticulosPenales
        //            ,CASE WHEN (DR.TipoDocumento IS NULL) THEN 'NINGUNO' ELSE DR.TipoDocumento END AS TipoDocumento
        //            ,R.TipoAsentamiento
        //            ,R.TipoVialidad
        //            FROM CAT_REPRESENTANTES R
        //            LEFT JOIN CAT_PERSONA P ON R.PersonaId=P.IdPersona
        //            LEFT JOIN CAT_DOCSREPRESENTANTES  DR ON R.IdRepresentante=DR.RepresentanteId
        //            LEFT JOIN C_TIPOSREPRESENTANTES TR ON R.Tipo1=TR.Valor
        //            LEFT JOIN C_TIPOSREPRESENTANTES TR2 ON R.Tipo2=TR2.Valor
        //            WHERE P.IdPersona = @personaid
        //            ORDER BY R.Fechasys DESC";

        //            List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
        //            filtrosBusqueda.Add(new SqlParameter("@personaid", personaid));
        //            var repre = await ctx.qBusquedaRepresentantes.FromSqlRaw(busquedaRepresentantes, filtrosBusqueda.ToArray()).ToListAsync();



        //            var datos = repre.Select(a => new RepresentanteViewModel
        //            {
        //                IdRepresentante = a.IdRepresentante,
        //                RHechoId = a.RHechoId,
        //                PersonaId = a.PersonaId,
        //                PersonaR = a.PersonaR,
        //                Nombres = a.Nombres,
        //                ApellidoPa = a.ApellidoPa,
        //                ApellidoMa = a.ApellidoMa,
        //                FechaNacimiento = a.FechaNacimiento,
        //                Sexo = a.Sexo,
        //                EntidadFeNacimiento = a.EntidadFeNacimiento,
        //                Curp = a.Curp,
        //                MedioNotificacion = a.MedioNotificacion,
        //                Telefono = a.Telefono,
        //                CorreoElectronico = a.CorreoElectronico,
        //                Nacionalidad = a.Nacionalidad,
        //                Genero = a.Genero,
        //                Tipo1 = a.Tipo1,
        //                Tipo2 = a.Tipo2,
        //                CedulaProfesional = a.CedulaProfesional,
        //                Fecha = a.Fecha,
        //                UDistrito = a.UDistrito,
        //                USubproc = a.USubproc,
        //                UAgencia = a.UAgencia,
        //                Usuario = a.Usuario,
        //                UPuesto = a.UPuesto,
        //                UModulo = a.UModulo,
        //                Fechasys = a.Fechasys,
        //                TipoRep1 = a.TipoRep1,
        //                TipoRep2 = a.TipoRep2,
        //                Calle = a.Calle,
        //                NoInt = a.NoInt,
        //                NoExt = a.NoExt,
        //                EntreCalle1 = a.EntreCalle1,
        //                EntreCalle2 = a.EntreCalle2,
        //                Referencia = a.Referencia,
        //                Pais = a.Pais,
        //                Estado = a.Estado,
        //                Municipio = a.Municipio,
        //                Localidad = a.Localidad,
        //                CP = a.CP,
        //                lat = a.lat,
        //                lng = a.lng,
        //                ArticulosPenales = a.ArticulosPenales,
        //                TipoDocumento = a.TipoDocumento,
        //                TipoAsentamiento = a.TipoAsentamiento,
        //                TipoVialidad = a.TipoVialidad

        //            });

        //            return Ok(new { datos = datos });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
        //        result.StatusCode = 402;
        //        return result;
        //    }
        //}

        //// GET: api/SistemaAudiencias/ListarPorHecho
        ////[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-IL,Recepción")]
        //[HttpGet("[action]/{rhechoid}/{idDistrito}")]
        //public async Task<IActionResult> ListarDelitos([FromRoute] Guid rhechoid, Guid idDistrito)
        //{

        //    try
        //    {
        //        var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + idDistrito.ToString().ToUpper())).Options;
        //        using (var ctx = new DbContextSIIGPP(options))
        //        {
        //            var delitos = await ctx.RDHs
        //                          .Include(a => a.Delito)
        //                          .Include(a => a.RHecho)

        //                          .Where(a => a.RHechoId == rhechoid)
        //                          .Where(a => a.Equiparado)
        //                          .ToListAsync();

        //            var datos = delitos.Select(a => new RDHechosViewModel
        //            {
        //                IdRDH = a.IdRDH,
        //                RHechoId = a.RHechoId,

        //                DelitoId = a.DelitoId,
        //                nombreDelito = a.Delito.Nombre,
        //                OfiNoOfic = a.Delito.OfiNoOfi,
        //                altoImpacto = a.Delito.AltoImpacto,
        //                suceptibleMASC = a.Delito.SuceptibleMASC,

        //                TipoRobado = a.TipoRobado,
        //                TipoDeclaracion = a.TipoDeclaracion,
        //                ViolenciaSinViolencia = a.ViolenciaSinViolencia,
        //                ArmaBlanca = a.ArmaBlanca,
        //                ArmaFuego = a.ArmaFuego,
        //                ClasificaOrdenResult = a.ClasificaOrdenResult,
        //                ConAlgunaParteCuerpo = a.ConAlgunaParteCuerpo,
        //                Concurso = a.Concurso,
        //                ConotroElemento = a.ConotroElemento,
        //                Equiparado = a.Equiparado,
        //                GraveNoGrave = a.GraveNoGrave,
        //                IntensionDelito = a.IntensionDelito,
        //                MontoRobado = a.MontoRobado,
        //                ResultadoDelito = a.ResultadoDelito,
        //                Tipo = a.Tipo,
        //                TipoFuero = a.TipoFuero,
        //                Observaciones = a.Observaciones,
        //                Fechasys = a.Fechasys,
        //                Hipotesis = a.Hipotesis,
        //                DelitoEspecifico = a.DelitoEspecifico,
        //                TipoViolencia = a.TipoViolencia,
        //                SubtipoSexual = a.SubtipoSexual,
        //                TipoInfoDigital = a.TipoInfoDigital,
        //                MedioDigital = a.MedioDigital,



        //            });

        //            return Ok(new { datos = datos });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
        //        result.StatusCode = 402;
        //        return result;
        //    }


        //}

    }
}
