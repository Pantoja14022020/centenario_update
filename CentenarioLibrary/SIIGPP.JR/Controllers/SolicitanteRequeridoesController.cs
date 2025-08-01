using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account.Manage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_JR.RSolicitanteRequerido;
using Microsoft.Data.SqlClient;
using SIIGPP.JR.Models.RSolicitanteRequerido;
using SIIGPP.JR.Models.RExpediente;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.JR.Models.RSeguimientoCumplimiento;
using SIIGPP.Entidades.M_Cat.DDerivacion;
using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;
using SIIGPP.JR.Models.RAcuerdoReparatorio;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
//using MoreLinq;
//using MoreLinq.Extensions;

namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitanteRequeridoesController : ControllerBase
    {
        public readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public SolicitanteRequeridoesController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        // GET: api/SolicitanteRequeridoes/ListarTodos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{EnvioId}")]
        public async Task<IEnumerable<GET_SolicitanteRequeridoViewModel>> ListarTodos([FromRoute] Guid EnvioId)
        {
            var tabla = await _context.SolicitanteRequeridos
                            .Where(a => a.EnvioId == EnvioId) 
                            .Include(a => a.Persona.DireccionPersonal)
                            .ToListAsync();

            var tiposVialidades = await _context.Vialidades.ToListAsync();

            return tabla.Select(a => new GET_SolicitanteRequeridoViewModel
            {

                IdRSolicitanteRequerido = a.IdRSolicitanteRequerido,
                EnvioId = a.EnvioId,
                PersonaId = a.PersonaId,
                Tipo = a.Tipo,
                Clasificacion = a.Clasificacion,
              
                //*********************************************************************
                Seleccion = false,
                TipoPersona = a.Persona.TipoPersona,
                StatusAnonimo = a.Persona.StatusAnonimo,
                RFC = a.Persona.RFC,
                RazonSocial = a.Persona.RazonSocial,
                NombreCompleto = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Nombre = a.Persona.Nombre,
                ApellidoMaterno = a.Persona.ApellidoMaterno,
                ApellidoPaterno = a.Persona.ApellidoPaterno,
                Alias = a.Persona.Alias,
                FechaNacimiento = a.Persona.FechaNacimiento,
                EntidadFederativa = a.Persona.EntidadFederativa,
                CURP = a.Persona.CURP,
                Sexo = a.Persona.Sexo,
                Genero = a.Persona.Genero,
                EstadoCivil = a.Persona.EstadoCivil,
                Telefono1 = a.Persona.Telefono1,
                Telefono2 = a.Persona.Telefono2,
                Correo = a.Persona.Correo,
                DocIdentificacion = a.Persona.DocIdentificacion,
                Medionotificacion = a.Persona.Medionotificacion,
                Nacionalidad = a.Persona.Nacionalidad,
                Ocupacion = a.Persona.Ocupacion,
                NivelEstudio = a.Persona.NivelEstudio,
                Lengua = a.Persona.Lengua,
                Religion = a.Persona.Religion,
                Discapacidad = a.Persona.Discapacidad,
                TipoDiscapacidad = a.Persona.TipoDiscapacidad,
                direccion = a.Persona.DireccionPersonal != null
                            ? $"{(a.Persona.DireccionPersonal.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(v => v.Clave == a.Persona.DireccionPersonal.TipoVialidad)?.Nombre + " " : "")}" +
                              $"{a.Persona.DireccionPersonal.Calle}" +
                              (a.Persona.DireccionPersonal.NoInt != "0" ? ", No.Int " + a.Persona.DireccionPersonal.NoInt : "") +
                              ", No.Ext " + a.Persona.DireccionPersonal.NoExt +
                              ", CP " + a.Persona.DireccionPersonal.CP + ", " +
                              a.Persona.DireccionPersonal.Localidad + ", " +
                              a.Persona.DireccionPersonal.Municipio + ", " +
                              a.Persona.DireccionPersonal.Estado
                            : "Sin dirección registrada",

            });

        }
        // GET: api/SolicitanteRequeridoes/ListarTodos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{EnvioId}/{conjuntoId}")]
        public async Task<IEnumerable<GET_SolicitanteRequeridoViewModel>> ListarPersonasConSinConjunto([FromRoute] Guid EnvioId, Guid conjuntoId)
        {
            var tabla = await _context.SolicitanteRequeridos
                            .Where(a => a.EnvioId == EnvioId)
                            .Where(a => a.ConjuntoDerivacionesId == conjuntoId)
                            .Include(a => a.Persona)
                            .ToListAsync();


            return tabla.Select(a => new GET_SolicitanteRequeridoViewModel
            {

                IdRSolicitanteRequerido = a.IdRSolicitanteRequerido,
                EnvioId = a.EnvioId,
                PersonaId = a.PersonaId,
                Tipo = a.Tipo,
                Clasificacion = a.Clasificacion,

                //*********************************************************************
                Seleccion = false,
                TipoPersona = a.Persona.TipoPersona,
                StatusAnonimo = a.Persona.StatusAnonimo,
                RFC = a.Persona.RFC,
                RazonSocial = a.Persona.RazonSocial,
                NombreCompleto = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Nombre = a.Persona.Nombre,
                ApellidoMaterno = a.Persona.ApellidoMaterno,
                ApellidoPaterno = a.Persona.ApellidoPaterno,
                Alias = a.Persona.Alias,
                FechaNacimiento = a.Persona.FechaNacimiento,
                EntidadFederativa = a.Persona.EntidadFederativa,
                CURP = a.Persona.CURP,
                Sexo = a.Persona.Sexo,
                Genero = a.Persona.Genero,
                EstadoCivil = a.Persona.EstadoCivil,
                Telefono1 = a.Persona.Telefono1,
                Telefono2 = a.Persona.Telefono2,
                Correo = a.Persona.Correo,
                DocIdentificacion = a.Persona.DocIdentificacion,
                Medionotificacion = a.Persona.Medionotificacion,
                Nacionalidad = a.Persona.Nacionalidad,
                Ocupacion = a.Persona.Ocupacion,
                NivelEstudio = a.Persona.NivelEstudio,
                Lengua = a.Persona.Lengua,
                Religion = a.Persona.Religion,
                Discapacidad = a.Persona.Discapacidad,
                TipoDiscapacidad = a.Persona.TipoDiscapacidad,
            });
        }

        // GET: api/SolicitanteRequeridoes/ListarTodos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{EnvioId}")]
        public async Task<IActionResult> ListarPersonRepre([FromRoute] Guid EnvioId)
        {
            try
            {
                String tabla = @"SELECT 
                                             p.IdPersona, 
                                             s.Clasificacion, 
                                             s.Tipo,
                                             CONCAT(p.Nombre, ' ', p.ApellidoPaterno, ' ', p.ApellidoMaterno) AS PersonaRepresentar, 
                                             CONCAT(j.Nombre, ' ', j.ApellidoPa, ' ', j.ApellidoMa) AS RepresentanteJr
                                       FROM CAT_PERSONA p 
                                             INNER JOIN JR_SOLICITANTEREQUERIDO s ON p.IdPersona = s.PersonaId 
                                             LEFT JOIN JR_RESPONSABLE j ON p.IdPersona = j.PersonaId
                                       WHERE s.EnvioId = @envioid";
                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@envioid", EnvioId));

                var representante = await _context.consultaRequeridoCats.FromSqlRaw(tabla, filtrosBusqueda.ToArray()).ToListAsync();

                var personasUnicas = representante.DistinctBy(a => a.IdPersona);

                return Ok(personasUnicas.Select(a => new GET_ListarPersonasRepre
                {
                    IdPersona = a.IdPersona,
                    Clasificacion = a.Clasificacion,
                    Tipo = a.Tipo,
                    NombreCompleto = a.PersonaRepresentar,
                    RepresentanteJr = a.RepresentanteJr,
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }




        }
        // GET: api/SolicitanteRequeridoes/ListarRequeridos
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{idEnvio}")]
        public async Task<IActionResult> ListarSolicitantesRequeridosC([FromRoute] Guid idEnvio)
        {
            try
            {
                var listaC = await _context.ConjuntoDerivaciones.Where(a => a.EnvioId == idEnvio).ToListAsync();

                if(listaC.Count == 0)
                {
                    //Consulta los resultados de los solicitates y requeridos del expediente
                    var tabla = await _context.SolicitanteRequeridos
                                .Where(v => v.EnvioId == idEnvio)
                                .Include(v => v.Persona)
                                .ToListAsync();

                    //Consulta los delitos derivados
                    var tabla2 = await _context.DelitosDerivados
                                .Where(v => v.EnvioId == idEnvio)
                                .Include(v => v.RDH)
                                .Include(v => v.RDH.Delito)
                                .ToListAsync();
                    //Condicionar los solicitantes y requeridos para que se guarden en determinado valor
                    var resultadosConcatenados = tabla.Select(v => new GET_SolicitanteRequeridoViewModel
                    {
                        NombreS = (v.Tipo == "Solicitante" ? v.Persona.Nombre + " " + v.Persona.ApellidoPaterno + " " + v.Persona.ApellidoMaterno + ", " : ""),
                        NombreR = (v.Tipo == "Requerido" ? v.Persona.Nombre + " " + v.Persona.ApellidoPaterno + " " + v.Persona.ApellidoMaterno + ", " : ""),
                    }).ToList();

                    //Lo mismo con los delitos derivados
                    var delitosConcatenados = tabla2.Select(v => new GET_SolicitanteRequeridoViewModel
                    {
                        NombreD = v.RDH.Delito.Nombre,
                    }).ToList();

                    // Crear objeto anónimo con propiedades concatenadas y los valores estaticos para retornar
                    var resultadoFinal = new
                    {
                        NombreS = string.Join("", resultadosConcatenados.Select(v => v.NombreS)),
                        NombreR = string.Join("", resultadosConcatenados.Select(v => v.NombreR)),
                        NombreD = string.Join(", ", delitosConcatenados.Select(v => v.NombreD)),
                    };

                    return Ok(resultadoFinal);

                }

                else
                {
                    return Ok(listaC.Select(a => new POST_CrearConjuntosSRD
                    {
                        IdConjuntoDerivaciones = a.IdConjuntoDerivaciones,
                        EnvioId = a.EnvioId,
                        SolicitadosC = a.SolicitadosC,
                        RequeridosC = a.RequeridosC,
                        DelitosC = a.DelitosC,
                        NombreS = a.NombreS,
                        DireccionS = a.DireccionS,
                        TelefonoS = a.TelefonoS,
                        ClasificacionS = a.ClasificacionS,
                        NombreR = a.NombreR,
                        DireccionR = a.DireccionR,
                        TelefonoR = a.TelefonoR,
                        ClasificacionR = a.ClasificacionR,
                        NombreD = a.NombreD,
                        NoOficio = a.NoOficio,
                        ResponsableJR = a.ResponsableJR,
                        NombreSubDirigido = a.NombreSubDirigido,
                        Validacion = "verdadero",
                    })
                    );

                }
                
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        
        // GET: api/SolicitanteRequeridoes/ListarRequeridos
        [HttpGet("[action]/{distritoId}/{idEnvio}")]
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador, USAR")]
        public async Task<IActionResult> ListarSolicitantesRequeridosCXDistrito([FromRoute] Guid distritoId, Guid idEnvio)
        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + distritoId.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var listaC = await ctx.ConjuntoDerivaciones.Where(a => a.EnvioId == idEnvio).ToListAsync();

                    if (listaC.Count == 0)
                    {
                        //Consulta los resultados de los solicitates y requeridos del expediente
                        var tabla = await ctx.SolicitanteRequeridos
                                    .Where(v => v.EnvioId == idEnvio)
                                    .Include(v => v.Persona)
                                    .ToListAsync();

                        //Consulta los delitos derivados
                        var tabla2 = await ctx.DelitosDerivados
                                    .Where(v => v.EnvioId == idEnvio)
                                    .Include(v => v.RDH)
                                    .Include(v => v.RDH.Delito)
                                    .ToListAsync();
                        //Condicionar los solicitantes y requeridos para que se guarden en determinado valor
                        var resultadosConcatenados = tabla.Select(v => new GET_SolicitanteRequeridoViewModel
                        {
                            NombreS = (v.Tipo == "Solicitante" ? v.Persona.Nombre + " " + v.Persona.ApellidoPaterno + " " + v.Persona.ApellidoMaterno + ", " : ""),
                            NombreR = (v.Tipo == "Requerido" ? v.Persona.Nombre + " " + v.Persona.ApellidoPaterno + " " + v.Persona.ApellidoMaterno + ", " : ""),
                        }).ToList();

                        //Lo mismo con los delitos derivados
                        var delitosConcatenados = tabla2.Select(v => new GET_SolicitanteRequeridoViewModel
                        {
                            NombreD = v.RDH.Delito.Nombre,
                        }).ToList();

                        // Crear objeto anónimo con propiedades concatenadas y los valores estaticos para retornar
                        var resultadoFinal = new
                        {
                            NombreS = string.Join("", resultadosConcatenados.Select(v => v.NombreS)),
                            NombreR = string.Join("", resultadosConcatenados.Select(v => v.NombreR)),
                            NombreD = string.Join(", ", delitosConcatenados.Select(v => v.NombreD)),
                        };

                        return Ok(resultadoFinal);

                    }

                    else
                    {
                        return Ok(listaC.Select(a => new POST_CrearConjuntosSRD
                        {
                            IdConjuntoDerivaciones = a.IdConjuntoDerivaciones,
                            EnvioId = a.EnvioId,
                            SolicitadosC = a.SolicitadosC,
                            RequeridosC = a.RequeridosC,
                            DelitosC = a.DelitosC,
                            NombreS = a.NombreS,
                            DireccionS = a.DireccionS,
                            TelefonoS = a.TelefonoS,
                            ClasificacionS = a.ClasificacionS,
                            NombreR = a.NombreR,
                            DireccionR = a.DireccionR,
                            TelefonoR = a.TelefonoR,
                            ClasificacionR = a.ClasificacionR,
                            NombreD = a.NombreD,
                            NoOficio = a.NoOficio,
                            ResponsableJR = a.ResponsableJR,
                            NombreSubDirigido = a.NombreSubDirigido,
                            Validacion = "verdadero",
                        })
                        );

                    }
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/SolicitanteRequeridoes/ListarTodos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{EnvioId}")]
        public async Task<IEnumerable<GET_idSolicitanteRequeridoViewModel>> NomSolReq([FromRoute] Guid EnvioId)
        {
            var tabla = await _context.Personas
                            .Where(a => a.SolicitanteRequerido.EnvioId == EnvioId)
                            .Include(a => a.SolicitanteRequerido)
                            .ToListAsync();

            var personasUnicas = tabla.DistinctBy(a => a.IdPersona);

            return personasUnicas.Select(a => new GET_idSolicitanteRequeridoViewModel
            {

                IdRSolicitanteRequerido = a.SolicitanteRequerido.IdRSolicitanteRequerido,
                EnvioId = a.SolicitanteRequerido.EnvioId,
                PersonaId = a.IdPersona,
                NombreCompleto = a.Nombre + " " + a.ApellidoPaterno + " " + a.ApellidoMaterno,
                Nombre = a.Nombre,
                ApellidoMaterno = a.ApellidoMaterno,
                ApellidoPaterno = a.ApellidoPaterno,
            });

        }

        // GET: api/SolicitanteRequeridoes/ListarTodos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{IdPersona}/{EnvioId}")]
        public async Task<IActionResult> InfoPerSolReq([FromRoute] Guid IdPersona, Guid EnvioId)
        {
            var tabla = await _context.Personas
                            .Where(a => a.IdPersona == IdPersona)
                            .Where(a => a.SolicitanteRequerido.EnvioId == EnvioId)
                            .Include(a => a.DireccionPersonal)
                            .Include(a => a.SolicitanteRequerido)
                            .ToListAsync();

            var tiposVialidades = await _context.Vialidades.ToListAsync();

            return Ok(tabla.Select(a => new PersonasViewModel
            {

                //*********************************************************************
                ClasificacionPersona = a.SolicitanteRequerido.Clasificacion,
                TipoPersona = a.TipoPersona,
                TipoSolicitante = a.SolicitanteRequerido.Tipo,
                StatusAnonimo = a.StatusAnonimo,
                RFC = a.RFC,
                RazonSocial = a.RazonSocial,
                NombreCompleto = a.Nombre + " " + a.ApellidoPaterno + " " + a.ApellidoMaterno,
                Nombre = a.Nombre,
                ApellidoMaterno = a.ApellidoMaterno,
                ApellidoPaterno = a.ApellidoPaterno,
                Alias = a.Alias,
                FechaNacimiento = a.FechaNacimiento,
                EntidadFederativa = a.EntidadFederativa,
                CURP = a.CURP,
                Sexo = a.Sexo,
                Genero = a.Genero,
                EstadoCivil = a.EstadoCivil,
                Telefono = a.Telefono1 + ";" + a.Telefono2,
                Correo = a.Correo,
                DocIdentificacion = a.DocIdentificacion,
                Medionotificacion = a.Medionotificacion,
                Nacionalidad = a.Nacionalidad,
                Ocupacion = a.Ocupacion,
                NivelEstudio = a.NivelEstudio,
                Lengua = a.Lengua,
                Religion = a.Religion,
                Discapacidad = a.Discapacidad,
                TipoDiscapacidad = a.TipoDiscapacidad,
                DireccionCompleta = $"{(a.DireccionPersonal.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(v => v.Clave == a.DireccionPersonal.TipoVialidad)?.Nombre + " " : "")}" +
                                    $" {a.DireccionPersonal.Calle + " " + a.DireccionPersonal.NoInt + " " + a.DireccionPersonal.NoExt + " " + a.DireccionPersonal.Localidad + " " + a.DireccionPersonal.Municipio + " " + a.DireccionPersonal.Pais + " " + a.DireccionPersonal.CP}",
                Referencia = a.DireccionPersonal.Referencia,
                Edadinfo = a.RangoEdad != null ? a.RangoEdad : a.Edad.ToString(),
            })
                ); ;

        }

        // GET: api/SolicitanteRequeridoes/ListarRequeridos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{EnvioId}/{SolReq}")]
        public async Task<IActionResult> ListarSolicitantesRequeridos([FromRoute] Guid EnvioId, string SolReq)
        {

            try
            {


                SqlParameter pEnvio = new SqlParameter("@envio", EnvioId);
                SqlParameter pSolReq = new SqlParameter("@solreq", SolReq);
                var consulta = await _context.ConsultaSolicitanteRequeridos.FromSqlRaw("SELECT P.*,R.IdRAP,SR.EnvioId,R.ClasificacionPersona,SR.Tipo,SR.IdRSolicitanteRequerido,CONCAT (CASE WHEN (DP.Calle ='') THEN 'SIN CALLE' ELSE DP.Calle END,' ',CASE WHEN (DP.NoExt ='') THEN 'S/N' ELSE DP.NoExt END ,' ',CASE WHEN (DP.NoInt ='0' OR DP.NoInt ='') THEN '' ELSE CONCAT('No Int. ',DP.NoInt) END,'Colonia: ',  CASE WHEN (DP.Localidad ='') THEN 'N/A' ELSE DP.Localidad END ,', ',CASE WHEN (DP.Municipio ='') THEN 'SIN MUNICIPIO' ELSE DP.Municipio END,' ',CASE WHEN (DP.Estado ='') THEN 'SIN ESTADO' ELSE DP.Estado END,', ',CASE WHEN (DP.Pais ='') THEN 'SIN PAIS' ELSE DP.Pais END,' CP ',DP.CP ) as direccion FROM JR_SOLICITANTEREQUERIDO SR INNER JOIN CAT_PERSONA P ON P.IdPersona=SR.PersonaId INNER JOIN CAT_RAP R ON R.PersonaId=SR.PersonaId INNER JOIN CAT_DIRECCION_PERSONAL DP ON DP.PersonaId=P.IdPersona WHERE SR.EnvioId=@envio AND SR.Tipo LIKE @solreq", pEnvio, pSolReq)
                    .ToListAsync();
                return Ok(consulta.Select(a => new GET_SolicitanteRequeridoViewModel
                {

                    IdRSolicitanteRequerido = a.IdRSolicitanteRequerido,
                    EnvioId = a.EnvioId,
                    PersonaId = a.IdPersona,
                    Tipo = a.Tipo,
                    Clasificacion = a.ClasificacionPersona,
                    //*********************************************************************
                    Seleccion = false,
                    TipoPersona = a.TipoPersona,
                    rapId = a.IdRAP,
                    StatusAnonimo = a.StatusAnonimo,
                    direccion = a.direccion,
                    RFC = a.RFC,
                    RazonSocial = a.RazonSocial,
                    NombreCompleto = a.Nombre + " " + a.ApellidoPaterno + " " + a.ApellidoMaterno,
                    Nombre = a.Nombre,
                    ApellidoMaterno = a.ApellidoMaterno,
                    ApellidoPaterno = a.ApellidoPaterno,
                    Alias = a.Alias,
                    FechaNacimiento = a.FechaNacimiento,
                    Edad = a.Edad,
                    EntidadFederativa = a.EntidadFederativa,
                    CURP = a.CURP,
                    Sexo = a.Sexo,
                    Genero = a.Genero,
                    EstadoCivil = a.EstadoCivil,
                    VerI = a.VerI,
                    VerR = a.VerR,
                    Registro = a.Registro,
                    Telefono1 = a.Telefono1,
                    DatosProtegidos = a.DatosProtegidos,
                    Telefono2 = a.Telefono2,
                    Correo = a.Correo,
                    DocIdentificacion = a.DocIdentificacion,
                    Medionotificacion = a.Medionotificacion,
                    Nacionalidad = a.Nacionalidad,
                    Ocupacion = a.Ocupacion,
                    NivelEstudio = a.NivelEstudio,
                    Lengua = a.Lengua,
                    Religion = a.Religion,
                    Discapacidad = a.Discapacidad,
                    TipoDiscapacidad = a.TipoDiscapacidad,
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

        }

        //Esta api es lo mismo que arriba pero aqui aplico un distint para que no se repitan los solicitantes o requeridos
        // GET: api/SolicitanteRequeridoes/ListarRequeridos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{EnvioId}/{SolReq}")]
        public async Task<IActionResult> ListarSolicitantesRequeridosDistint([FromRoute] Guid EnvioId, string SolReq)
        {

            try
            {
                String consultaP = @"SELECT 
                                            P.*,
                                            R.IdRAP,
                                            SR.EnvioId,
                                            R.ClasificacionPersona,
                                            SR.Tipo,
                                            SR.IdRSolicitanteRequerido,
                                            CONCAT (COALESCE(v.Nombre, ''),' ',CASE WHEN (DP.Calle ='') THEN 'SIN CALLE' ELSE DP.Calle END,' ',CASE WHEN (DP.NoExt ='') THEN 'S/N' ELSE DP.NoExt END ,' ',CASE WHEN (DP.NoInt ='0' OR DP.NoInt ='') THEN '' ELSE CONCAT('No Int. ',DP.NoInt) END,'Colonia: ',  CASE WHEN (DP.Localidad ='') THEN 'N/A' ELSE DP.Localidad END ,', ',CASE WHEN (DP.Municipio ='') THEN 'SIN MUNICIPIO' ELSE DP.Municipio END,' ',CASE WHEN (DP.Estado ='') THEN 'SIN ESTADO' ELSE DP.Estado END,', ',CASE WHEN (DP.Pais ='') THEN 'SIN PAIS' ELSE DP.Pais END,' CP ',DP.CP ) as direccion 
                                        FROM JR_SOLICITANTEREQUERIDO SR 
                                            INNER JOIN CAT_PERSONA P ON P.IdPersona=SR.PersonaId 
                                            INNER JOIN CAT_RAP R ON R.PersonaId=SR.PersonaId 
                                            INNER JOIN CAT_DIRECCION_PERSONAL DP ON DP.PersonaId=P.IdPersona 
                                            left join C_TIPO_VIALIDAD as v on DP.TipoVialidad = v.Clave
                                        WHERE SR.EnvioId=@envio AND SR.Tipo LIKE @solreq";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>
                {
                    new SqlParameter("@envio", EnvioId),
                    new SqlParameter("@solreq", SolReq)
                };

                var consulta = await _context.ConsultaSolicitanteRequeridos.FromSqlRaw(consultaP, filtrosBusqueda.ToArray()).ToListAsync();

                var personasUnicas = consulta.DistinctBy(a => a.IdPersona);

                return Ok(personasUnicas.Select(a => new GET_SolicitanteRequeridoViewModel
                {

                    IdRSolicitanteRequerido = a.IdRSolicitanteRequerido,
                    EnvioId = a.EnvioId,
                    PersonaId = a.IdPersona,
                    Tipo = a.Tipo,
                    Clasificacion = a.ClasificacionPersona,
                    //*********************************************************************
                    Seleccion = false,
                    TipoPersona = a.TipoPersona,
                    rapId = a.IdRAP,
                    StatusAnonimo = a.StatusAnonimo,
                    direccion = a.direccion,
                    RFC = a.RFC,
                    RazonSocial = a.RazonSocial,
                    NombreCompleto = a.Nombre + " " + a.ApellidoPaterno + " " + a.ApellidoMaterno,
                    Nombre = a.Nombre,
                    ApellidoMaterno = a.ApellidoMaterno,
                    ApellidoPaterno = a.ApellidoPaterno,
                    Alias = a.Alias,
                    FechaNacimiento = a.FechaNacimiento,
                    Edad = a.Edad,
                    EntidadFederativa = a.EntidadFederativa,
                    CURP = a.CURP,
                    Sexo = a.Sexo,
                    Genero = a.Genero,
                    EstadoCivil = a.EstadoCivil,
                    VerI = a.VerI,
                    VerR = a.VerR,
                    Registro = a.Registro,
                    Telefono1 = a.Telefono1,
                    DatosProtegidos = a.DatosProtegidos,
                    Telefono2 = a.Telefono2,
                    Correo = a.Correo,
                    DocIdentificacion = a.DocIdentificacion,
                    Medionotificacion = a.Medionotificacion,
                    Nacionalidad = a.Nacionalidad,
                    Ocupacion = a.Ocupacion,
                    NivelEstudio = a.NivelEstudio,
                    Lengua = a.Lengua,
                    Religion = a.Religion,
                    Discapacidad = a.Discapacidad,
                    TipoDiscapacidad = a.TipoDiscapacidad,
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

        }


        //************************************************************************************************************
        // BUSQUEDA GENERAL               

        // GET: api/SolicitanteRequeridoes/ListarBusquedaGeneral1
        //[Authorize(Roles = "AMPO-AMP,Administrador,Director,Coordinador, Juridico,Facilitador, Facilitador-Mixto")]
        [HttpGet("[action]/{nuc}/{NoExpediente}/{nombre}/{apaterno}/{amaterno}")]
        public async Task<IEnumerable<GET_BusquedaGeneralViewModel>> ListarBusquedaGeneral1([FromRoute] string nuc, string  noExpediente, string nombre, string apaterno, string amaterno)
        {
            string noexpediente = "";
            if (noExpediente != "NA")
            {
                 noexpediente  = HextoString(noExpediente);
            }
            var compareInfo = System.Globalization.CultureInfo.InvariantCulture.CompareInfo;

            var Env = await _context.SolicitanteRequeridos
                               .Include(a => a.Persona)
                               .Include(a => a.Envio.Expediente)
                               .Include(a => a.Envio.Expediente.RHecho)
                               .Include(a => a.Envio.Expediente.RHecho.NUCs)  
                               .Include(a => a.Envio.Expediente.RHecho.Agencia.DSP.Distrito)
                               .Where(a => a.Envio.Expediente.NoExpediente != null)
                               .Where(a => nuc != "NA" ? a.Envio.Expediente.RHecho.NUCs.nucg.Contains(nuc) : 1 == 1)
                               .Where(a => noExpediente != "NA" ? a.Envio.Expediente.NoExpediente.Contains(noexpediente) : 1 == 1)
                               .Where(a => nombre != "NA" ? DbContextSIIGPP.RemoveDiacritics(a.Persona.Nombre).Contains(DbContextSIIGPP.RemoveDiacritics(nombre)) : 1 == 1)
                               .Where(a => apaterno != "NA" ? DbContextSIIGPP.RemoveDiacritics(a.Persona.ApellidoPaterno).Contains(DbContextSIIGPP.RemoveDiacritics(apaterno)) : 1 == 1)
                               .Where(a => amaterno != "NA" ? DbContextSIIGPP.RemoveDiacritics(a.Persona.ApellidoMaterno).Contains(DbContextSIIGPP.RemoveDiacritics(amaterno)) : 1 == 1)
                               .ToListAsync();



   
       

            return Env.Select(a => new GET_BusquedaGeneralViewModel
            {
                IdEnvio = a.Envio.IdEnvio,
                Clasificacion = a.Clasificacion,
                Distrito = a.Envio.Expediente.RHecho.Agencia.DSP.Distrito.Nombre,
                ExpedienteId = a.Envio.ExpedienteId,
                AutoridadqueDeriva = a.Envio.AutoridadqueDeriva,
                uqe_Distrito = a.Envio.uqe_Distrito,
                uqe_DirSubProc = a.Envio.uqe_DirSubProc,
                uqe_Agencia = a.Envio.uqe_Agencia,
                uqe_Modulo = a.Envio.uqe_Modulo,
                uqe_Nombre = a.Envio.uqe_Nombre,
                uqe_Puesto = a.Envio.uqe_Puesto,
                StatusGeneral = a.Envio.StatusGeneral,
                InfoConclusion = a.Envio.InfoConclusion,
                StatusAMPO = a.Envio.StatusAMPO,
                InformaAMPO = a.Envio.InformaAMPO,
                RespuestaExpediente = a.Envio.RespuestaExpediente,
                EspontaneoNoEspontaneo = a.Envio.EspontaneoNoEspontaneo,
                PrimeraVezSubsecuente = a.Envio.PrimeraVezSubsecuente,
                ContadorNODerivacion = a.Envio.ContadorNODerivacion,
                FechaRegistro = a.Envio.FechaRegistro,
                FechaCierre = a.Envio.FechaCierre,
                NoSolicitantes = a.Envio.NoSolicitantes,
                // EXPEDIENTE
                NoExpediente = a.Envio.Expediente.NoExpediente,
                FechaRegistroExpediente = a.Envio.Expediente.FechaRegistroExpediente,
                FechaDerivacion = a.Envio.Expediente.FechaDerivacion,
                StatusAcepRech = a.Envio.Expediente.StatusAcepRech,

                // INFORMACION DEL HECHO 
                NUC = a.Envio.Expediente.RHecho.NUCs.nucg,

                // INFORMACION DE LA PERSONA 

                TipoPersona =a.Persona.TipoPersona,
                RFC =a.Persona.RFC,
                RazonSocial = a.Persona.RazonSocial,
                Nombre = a.Persona.Nombre,
                ApellidoPaterno =a .Persona.ApellidoPaterno,
                ApellidoMaterno = a.Persona.ApellidoMaterno,
                Alias= a.Persona.Alias,
               


    });

        }
        
        // GET: api/SolicitanteRequeridoes/ListarBusquedaGeneral2
        //[Authorize(Roles = "AMPO-AMP,Administrador,Director,Coordinador, Juridico,Facilitador, Facilitador-Mixto")]
        [HttpGet("[action]/{nuc}/{NoExpediente}/{nombre}/{apaterno}/{amaterno}")]
        public async Task<IActionResult> ListarBusquedaGeneral2([FromRoute]  string nuc, string noExpediente, string nombre, string apaterno, string amaterno)
        {

            try { 
            string noexpediente = "";
            if (noExpediente != "NA")
            {
                noexpediente = HextoString(noExpediente);
            }
            var compareInfo = System.Globalization.CultureInfo.InvariantCulture.CompareInfo;
            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-7F662EC1-6705-406E-BCD0-F56ADE7BCAE2")).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {

                    var Env = await ctx.SolicitanteRequeridos
                                   .Include(a => a.Persona)
                                   .Include(a => a.Envio.Expediente)
                                   .Include(a => a.Envio.Expediente.RHecho)
                                   .Include(a => a.Envio.Expediente.RHecho.NUCs)
                                   .Include(a => a.Envio.Expediente.RHecho.Agencia.DSP.Distrito)
                                   .Where(a => nuc != "NA" ? a.Envio.Expediente.RHecho.NUCs.nucg.Contains(nuc) : 1 == 1)
                                   .Where(a => noExpediente != "NA" ? a.Envio.Expediente.NoExpediente.Contains(noexpediente) : 1 == 1)
                                   .Where(a => nombre != "NA" ? DbContextSIIGPP.RemoveDiacritics(a.Persona.Nombre).Contains(DbContextSIIGPP.RemoveDiacritics(nombre)) : 1 == 1)
                                   .Where(a => apaterno != "NA" ? DbContextSIIGPP.RemoveDiacritics(a.Persona.ApellidoPaterno).Contains(DbContextSIIGPP.RemoveDiacritics(apaterno)) : 1 == 1)
                                   .Where(a => amaterno != "NA" ? DbContextSIIGPP.RemoveDiacritics(a.Persona.ApellidoMaterno).Contains(DbContextSIIGPP.RemoveDiacritics(amaterno)) : 1 == 1)
                                   .ToListAsync();






                    return Ok(Env.Select(a => new GET_BusquedaGeneralViewModel
                    {
                        IdEnvio = a.Envio.IdEnvio,
                        Distrito = a.Envio.Expediente.RHecho.Agencia.DSP.Distrito.Nombre,
                        ExpedienteId = a.Envio.ExpedienteId,
                        AutoridadqueDeriva = a.Envio.AutoridadqueDeriva,
                        uqe_Distrito = a.Envio.uqe_Distrito,
                        uqe_DirSubProc = a.Envio.uqe_DirSubProc,
                        uqe_Agencia = a.Envio.uqe_Agencia,
                        uqe_Modulo = a.Envio.uqe_Modulo,
                        uqe_Nombre = a.Envio.uqe_Nombre,
                        uqe_Puesto = a.Envio.uqe_Puesto,
                        StatusGeneral = a.Envio.StatusGeneral,
                        InfoConclusion = a.Envio.InfoConclusion,
                        StatusAMPO = a.Envio.StatusAMPO,
                        InformaAMPO = a.Envio.InformaAMPO,
                        RespuestaExpediente = a.Envio.RespuestaExpediente,
                        EspontaneoNoEspontaneo = a.Envio.EspontaneoNoEspontaneo,
                        PrimeraVezSubsecuente = a.Envio.PrimeraVezSubsecuente,
                        ContadorNODerivacion = a.Envio.ContadorNODerivacion,
                        FechaRegistro = a.Envio.FechaRegistro,
                        FechaCierre = a.Envio.FechaCierre,
                        NoSolicitantes = a.Envio.NoSolicitantes,
                        // EXPEDIENTE
                        NoExpediente = a.Envio.Expediente.NoExpediente,
                        FechaRegistroExpediente = a.Envio.Expediente.FechaRegistroExpediente,
                        FechaDerivacion = a.Envio.Expediente.FechaDerivacion,
                        StatusAcepRech = a.Envio.Expediente.StatusAcepRech,

                        // INFORMACION DEL HECHO 
                        NUC = a.Envio.Expediente.RHecho.NUCs.nucg,

                        // INFORMACION DE LA PERSONA 

                        TipoPersona = a.Persona.TipoPersona,
                        RFC = a.Persona.RFC,
                        RazonSocial = a.Persona.RazonSocial,
                        Nombre = a.Persona.Nombre,
                        ApellidoPaterno = a.Persona.ApellidoPaterno,
                        ApellidoMaterno = a.Persona.ApellidoMaterno,
                        Alias = a.Persona.Alias,


                    }));
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        //API para el actualizado de nombres en los conjuntos
        // PUT:  api/SolicitanteRequeridoes/ActualizarAcuerdoReparatorio
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> EditarNombresC([FromBody] PUT_NombresConjuntosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //En realidad es una api bastente sencilla, pues los proceso de remplazo yciclos estan frontend, ya que es mas sencillo maniputalr los datos y hacer recorridos

            var db = await _context.ConjuntoDerivaciones.FirstOrDefaultAsync(a => a.IdConjuntoDerivaciones == model.idConjuntoDerivaciones);

            if (db == null)
            {
                return NotFound();
            }

            //En caso de ser una edicion a los solicitantes, shago que solo haga cambios en esa columna, para evitar hacerlo en un solo update y dañe los datos de
            //su anteparte solicitante oi requerido por no recibir el datos correcto o vacio
            if(model.solRequ == "Solicitante")
            {
                db.IdConjuntoDerivaciones = model.idConjuntoDerivaciones;
                db.NombreS = model.nombreS;

            }
            if (model.solRequ == "Requerido")
            {
                db.IdConjuntoDerivaciones = model.idConjuntoDerivaciones;
                db.NombreR = model.nombreR;
            }
           

            //Esta solo es una respuesta en caso que se haya actualizado correctamente
                try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new { update = true });
        }

        //Api para actualizar los registros de solicitantes y requeridos y colocarles el id de conjunto como parte de su trasformacion a conjuntos
        // PUT:  api/SolicitanteRequeridoes/EditarConjuntoIdSolicitantesRequeridos
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> EditarConjuntoIdSolicitantesRequeridos([FromBody] PUT_CrearSViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var SR = await _context.SolicitanteRequeridos.FirstOrDefaultAsync(a => a.IdRSolicitanteRequerido == model.IdRSolicitanteRequerido);

            if (SR == null)
            {
                return NotFound();
            }

            SR.ConjuntoDerivacionesId = model.ConjuntoDerivacionesId;
            

            //Esta solo es una respuesta en caso que se haya actualizado correctamente
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new { update = true });

        }

        public static string HextoString(string InputText)
        {

            byte[] bb = Enumerable.Range(0, InputText.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(InputText.Substring(x, 2), 16))
                             .ToArray();
            return System.Text.Encoding.ASCII.GetString(bb);
            // or System.Text.Encoding.UTF7.GetString
            // or System.Text.Encoding.UTF8.GetString
            // or System.Text.Encoding.Unicode.GetString
            // or etc.
        }
   

        private bool SolicitanteRequeridoExists(Guid id)
        {
            return _context.SolicitanteRequeridos.Any(e => e.IdRSolicitanteRequerido == id);
        }

        // GET: api/SolicitanteRequeridoes/ListarNomConjuntos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{EnvioId}")]
        public async Task<IActionResult> ListarNomConjuntos([FromRoute] Guid EnvioId)
        {
            var tabla = await _context.ConjuntoDerivaciones
                            .Where(a => a.EnvioId == EnvioId)
                            .ToListAsync();

            return Ok(tabla.Select(a => new NombresConjuntosViewModel
            {
                SolicitadosC = a.SolicitadosC,
                RequeridosC = a.RequeridosC,
                NombreS = a.NombreS,
                NombreR = a.NombreR,


            })
                ); ;

        }

    }
}