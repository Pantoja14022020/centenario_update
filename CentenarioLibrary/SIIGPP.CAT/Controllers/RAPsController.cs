using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using SIIGPP.CAT.Models.Registro;
using SIIGPP.CAT.Models.Victimas;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Direcciones;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Cat.Turnador;
using SIIGPP.Entidades.M_Cat.PersonaDesap;
using SIIGPP.Entidades.M_Cat.MedAfiliacion;
using SIIGPP.CAT.FilterClass;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Administracion;
using SIIGPP.CAT.Models.Persona;
//using MoreLinq;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_Cat.MedFiliacionDesaparecido;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RAPsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public RAPsController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/RAPs/ListarDireccionAgencia/idAgencia
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,Director,Procurador, AMPO-IL")]
        [HttpGet("[action]/{idAgencia}")]
        public async Task<IActionResult> ListarDireccionAgencia([FromRoute] Guid idAgencia)
        {

            var b = await _context.Agencias
                                   .Where(a => a.IdAgencia == idAgencia)
                                   .ToListAsync();

            if (b == null)
            {
                return NotFound("No hay registros");

            }

            return Ok(b.Select(d => new Agencia
            {
               IdAgencia = d.IdAgencia, 
               Direccion = d.Direccion, 
            })
            );

        }

        // GET: api/RAPs/ListarPersonaSCPDF/idEnvio
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,Director,Procurador, AMPO-IL")]
        [HttpGet("[action]/{idEnvio}")]
        public async Task<IActionResult> ListarPersonaSCPDF([FromRoute] Guid idEnvio)
        {

            var b = await _context.Personas
                .Include(p => p.DireccionPersonal)
                .Include(p => p.SolicitanteRequerido)
                .Where(p => p.SolicitanteRequerido.EnvioId == idEnvio)
                .ToListAsync();

            if (b == null)
            {
                return NotFound("No hay registros");

            }

            return Ok(b.Select(d => new PersonaViewModel
            {
                IdPersona = d.IdPersona,
                NombreCompleto = d.Nombre + " " + d.ApellidoPaterno + " " + d.ApellidoMaterno,
                EntidadFederativa = d.EntidadFederativa,
                DocIdentificacion = d.DocIdentificacion,
                CURP = d.CURP,
                Sexo = d.Sexo,
                Genero = d.Genero,
                Registro = d.Registro,
                EstadoCivil = d.EstadoCivil,
                Telefonos = d.Telefono1 + ", " + d.Telefono2,
                Nacionalidad = d.Nacionalidad,
                Ocupacion = d.Ocupacion,
                NivelEstudio = d.NivelEstudio,
                Parentesco = d.Parentesco,
                Clasificacion = d.SolicitanteRequerido.Clasificacion,
                Relacion = d.Relacion,
                Edad = d.Edad,
                Tipo = d.SolicitanteRequerido.Tipo,
                Direccion = d.DireccionPersonal.Calle + (d.DireccionPersonal.NoInt != "0" ? ", No.Int " + d.DireccionPersonal.NoInt : "") + ", No.Ext " + d.DireccionPersonal.NoExt + ", CP " + d.DireccionPersonal.CP + ", " + d.DireccionPersonal.Localidad + ", " + d.DireccionPersonal.Municipio + ", " + d.DireccionPersonal.Estado,

            })
            );

        }

        // GET: api/RAPs/ListarPersonaPDF/idPersona
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,Director,Procurador, AMPO-IL")]
        [HttpGet("[action]/{personaId}")]
        public async Task<IActionResult> ListarPersonaPDF([FromRoute] string personaId)
        {
            try
            {

                var idsArray = personaId.Split(',');

                // Convertir los IDs de cadena a Guid
                var guids = idsArray.Select(Guid.Parse).ToList();

                var b = await _context.Personas
                    .Include(p => p.DireccionPersonal)
                    .Include(p => p.SolicitanteRequerido)
                    .Where(p => guids.Contains(p.IdPersona))
                    .ToListAsync();

                var c = b.DistinctBy(p => p.IdPersona);

                if (c == null)
                {
                    return NotFound("No hay registros");

                }

                var tiposVialidades = await _context.Vialidades.ToListAsync();

                return Ok(c.Select(d => new PersonaViewModel
                {
                    IdPersona = d.IdPersona,
                    NombreCompleto = d.Nombre + " " + d.ApellidoPaterno + " " + d.ApellidoMaterno,
                    EntidadFederativa = d.EntidadFederativa,
                    DocIdentificacion = d.DocIdentificacion,
                    CURP = d.CURP,
                    Sexo = d.Sexo,
                    Genero = d.Genero,
                    Registro = d.Registro,
                    EstadoCivil = d.EstadoCivil,
                    Telefonos = d.Telefono1 + ", " + d.Telefono2,
                    Nacionalidad = d.Nacionalidad,
                    Ocupacion = d.Ocupacion,
                    FechaNacimiento = d.FechaNacimiento,
                    NivelEstudio = d.NivelEstudio,
                    Parentesco = d.Parentesco,
                    Clasificacion = d.SolicitanteRequerido.Clasificacion,
                    Relacion = d.Relacion,
                    Edad = d.Edad,
                    Tipo = d.SolicitanteRequerido.Tipo,
                    Direccion = d.DireccionPersonal != null
                                ? $"{(d.DireccionPersonal.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(v => v.Clave == d.DireccionPersonal.TipoVialidad)?.Nombre + " " : "")}" +
                                  $"{d.DireccionPersonal.Calle}" +
                                  (d.DireccionPersonal.NoInt != "0" ? ", No.Int " + d.DireccionPersonal.NoInt : "") +
                                  ", No.Ext " + d.DireccionPersonal.NoExt +
                                  ", CP " + d.DireccionPersonal.CP + ", " +
                                  d.DireccionPersonal.Localidad + ", " +
                                  d.DireccionPersonal.Municipio + ", " +
                                  d.DireccionPersonal.Estado
                                : "Sin dirección registrada",
                }));

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/RAPs/ListarDenunciante/id
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Director,Procurador, AMPO-IL,Recepción")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IActionResult> ListarDenunciante([FromRoute] Guid rAtencionId)
        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Include(a => a.RAtencion)
                            .Include(a => a.RAtencion.RACs)
                            .Where(a => a.ClasificacionPersona == "Denunciante")
                            .Where(x => x.RAtencionId == rAtencionId).FirstOrDefaultAsync();

            if (rap == null)
            {
                return Ok(new { ner = 1 });

            }

            return Ok(new RAPViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                IdRAP = rap.IdRAP,
                RAtencionId = rap.RAtencionId,
                PersonaId = rap.PersonaId,
                ClasificacionPersona = rap.ClasificacionPersona,
                /*********************************************/
                /*CAT_RATENCION*/
                FechaHoraInicio = rap.RAtencion.FechaHoraRegistro,
                u_Nombre = rap.RAtencion.u_Nombre,
                u_Puesto = rap.RAtencion.u_Puesto,
                u_Modulo = rap.RAtencion.u_Modulo,
                DistritoInicial = rap.RAtencion.DistritoInicial,
                DirSubProcuInicial = rap.RAtencion.DirSubProcuInicial,
                AgenciaInicial = rap.RAtencion.AgenciaInicial,
                StatusAtencion = rap.RAtencion.StatusAtencion,
                StatusRegistro = rap.RAtencion.StatusRegistro,
                MedioDenuncia = rap.RAtencion.MedioDenuncia,
                Rac = rap.RAtencion.RACs.racg,
                RacId = rap.RAtencion.RACs.idRac,
                ContencionVictima = rap.RAtencion.ContencionVicitma,
                Numerooficio = rap.RAtencion.ModuloServicio,
                /*********************************************/
                /*CAT_PERSONA*/

                TipoPersona = rap.Persona.TipoPersona,
                Nombre = rap.Persona.Nombre,
                ApellidoPaterno = rap.Persona.ApellidoPaterno,
                ApellidoMaterno = rap.Persona.ApellidoMaterno,
                Alias = rap.Persona.Alias,
                FechaNacimiento = rap.Persona.FechaNacimiento,
                EntidadFederativa = rap.Persona.EntidadFederativa,
                RFC = rap.Persona.RFC,
                RazonSocial = rap.Persona.RazonSocial,
                DocIdentificacion = rap.Persona.DocIdentificacion,
                CURP = rap.Persona.CURP,
                //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
                PoblacionAfro = (bool)(rap.Persona.PoblacionAfro == null ? false : rap.Persona.PoblacionAfro),
                RangoEdad = rap.Persona.RangoEdad,
                RangoEdadTF = (bool)(rap.Persona.RangoEdadTF == null ? false : rap.Persona.RangoEdadTF),
                PoliciaDetuvo = (Guid)rap.Persona.PoliciaDetuvo,
                //--------------------------------------------------------------------------------------------------
                Sexo = rap.Persona.Sexo,
                Genero = rap.Persona.Genero,
                EstadoCivil = rap.Persona.EstadoCivil,
                Telefono1 = rap.Persona.Telefono1,
                Telefono2 = rap.Persona.Telefono2,
                Correo = rap.Persona.Correo,
                Medionotificacion = rap.Persona.Medionotificacion,
                Nacionalidad = rap.Persona.Nacionalidad,
                Ocupacion = rap.Persona.Ocupacion,
                Lengua = rap.Persona.Lengua,
                Religion = rap.Persona.Religion,
                Discapacidad = rap.Persona.Discapacidad,
                TipoDiscapacidad = rap.Persona.TipoDiscapacidad,
                Parentesco = rap.Persona.Parentesco,
                NivelEstudio = rap.Persona.NivelEstudio,
                Edad = rap.Persona.Edad,
                DatoProtegido = rap.Persona.DatosProtegidos,
                /*********************************************/
                /*CAT_DIRECCIONPERSONA*/
                /*********************************************/

            });

        }

        // GET: api/RAPs/Listar/id
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Director,Procurador, AMPO-IL,Recepción")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IActionResult> Listar([FromRoute] Guid rAtencionId)
        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Include(a => a.RAtencion)
                            .Include(a => a.RAtencion.RACs)
                            .Where(a => a.PInicio == true)
                            .Where(x => x.RAtencionId == rAtencionId).FirstOrDefaultAsync();

            if (rap == null)
            {
                return NotFound("No hay registros");

            }            

            return Ok(new RAPViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                IdRAP = rap.IdRAP,
                RAtencionId = rap.RAtencionId,
                PersonaId = rap.PersonaId,
                ClasificacionPersona = rap.ClasificacionPersona,
                /*********************************************/
                /*CAT_RATENCION*/
                FechaHoraInicio = rap.RAtencion.FechaHoraRegistro,
                u_Nombre = rap.RAtencion.u_Nombre,
                u_Puesto = rap.RAtencion.u_Puesto,
                u_Modulo = rap.RAtencion.u_Modulo,
                DistritoInicial = rap.RAtencion.DistritoInicial,
                DirSubProcuInicial = rap.RAtencion.DirSubProcuInicial,
                AgenciaInicial = rap.RAtencion.AgenciaInicial,
                StatusAtencion = rap.RAtencion.StatusAtencion,
                StatusRegistro = rap.RAtencion.StatusRegistro,
                MedioDenuncia = rap.RAtencion.MedioDenuncia,
                Rac = rap.RAtencion.RACs.racg,
                RacId = rap.RAtencion.RACs.idRac,
                ContencionVictima = rap.RAtencion.ContencionVicitma,
                Numerooficio= rap.RAtencion.ModuloServicio,
                /*********************************************/
                /*CAT_PERSONA*/

                TipoPersona = rap.Persona.TipoPersona,
                Nombre = rap.Persona.Nombre,
                ApellidoPaterno = rap.Persona.ApellidoPaterno,
                ApellidoMaterno = rap.Persona.ApellidoMaterno,
                Alias = rap.Persona.Alias,
                FechaNacimiento = rap.Persona.FechaNacimiento,
                EntidadFederativa = rap.Persona.EntidadFederativa,
                RFC = rap.Persona.RFC,
                RazonSocial = rap.Persona.RazonSocial,
                DocIdentificacion = rap.Persona.DocIdentificacion,
                CURP = rap.Persona.CURP,
                //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
                PoblacionAfro = (bool)(rap.Persona.PoblacionAfro == null ? false : rap.Persona.PoblacionAfro),
                RangoEdad = rap.Persona.RangoEdad,
                RangoEdadTF = (bool)(rap.Persona.RangoEdadTF == null ? false : rap.Persona.RangoEdadTF),
                PoliciaDetuvo = (Guid)rap.Persona.PoliciaDetuvo,
                //--------------------------------------------------------------------------------------------------
                Sexo = rap.Persona.Sexo,
                Genero = rap.Persona.Genero,
                EstadoCivil = rap.Persona.EstadoCivil,
                Telefono1 = rap.Persona.Telefono1,
                Telefono2 = rap.Persona.Telefono2,
                Correo = rap.Persona.Correo,
                Medionotificacion = rap.Persona.Medionotificacion,
                Nacionalidad = rap.Persona.Nacionalidad,
                Ocupacion = rap.Persona.Ocupacion,
                Lengua = rap.Persona.Lengua,
                Religion = rap.Persona.Religion,
                Discapacidad = rap.Persona.Discapacidad,
                TipoDiscapacidad = rap.Persona.TipoDiscapacidad,
                Parentesco = rap.Persona.Parentesco,
                NivelEstudio = rap.Persona.NivelEstudio,
                Edad = rap.Persona.Edad,
                DatoProtegido=rap.Persona.DatosProtegidos,
                /*********************************************/
                /*CAT_DIRECCIONPERSONA*/
                /*********************************************/

            });

        }

        // GET: api/RAPs/ListarTodos/id
        [Authorize(Roles = "Administrador, Recepción,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IEnumerable<VictimasViewModel>> ListarTodos([FromRoute] Guid rAtencionId)

        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Where(x => x.RAtencionId == rAtencionId)
                            .ToListAsync();

            return rap.Select(a =>  new VictimasViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                idRAP = a.IdRAP,
                RAtencionId = a.RAtencionId,
                PersonaId = a.PersonaId,
                ClasificacionPersona = a.ClasificacionPersona,
                PInicio = a.PInicio,
                /*********************************************/

                /*********************************************/
                /*CAT_PERSONA*/
                StatusAnonimo = a.Persona.StatusAnonimo,
                TipoPersona = a.Persona.TipoPersona,
                NombreCompleto = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Nombre = a.Persona.Nombre,
                ApellidoPaterno = a.Persona.ApellidoPaterno,
                ApellidoMaterno = a.Persona.ApellidoMaterno,
                Alias = a.Persona.Alias,
                FechaNacimiento = a.Persona.FechaNacimiento,
                EntidadFederativa = a.Persona.EntidadFederativa,
                RFC = a.Persona.RFC,
                RazonSocial = a.Persona.RazonSocial,
                DocIdentificacion = a.Persona.DocIdentificacion,
                CURP = a.Persona.CURP,
                //AGREGACIONES NUEVAS QUE NO ESTAN EN TODOS LADOS
                PoblacionAfro = (bool)(a.Persona.PoblacionAfro == null ? false : a.Persona.PoblacionAfro),
                RangoEdad = a.Persona.RangoEdad,
                RangoEdadTF = (bool)(a.Persona.RangoEdadTF == null ? false : a.Persona.RangoEdadTF),
                PoliciaDetuvo = (Guid)a.Persona.PoliciaDetuvo,
                //----------------------------------------------------------------------------------------------
                Sexo = a.Persona.Sexo,
                Genero = a.Persona.Genero,
                Registro = a.Persona.Registro.GetValueOrDefault(false),
                VerR = a.Persona.VerR.GetValueOrDefault(false),
                VerI = a.Persona.VerI.GetValueOrDefault(false),
                EstadoCivil = a.Persona.EstadoCivil,
                Telefono1 = a.Persona.Telefono1,
                Telefono2 = a.Persona.Telefono2,
                Correo = a.Persona.Correo,
                Medionotificacion = a.Persona.Medionotificacion,
                Nacionalidad = a.Persona.Nacionalidad,
                Ocupacion = a.Persona.Ocupacion,
                Lengua = a.Persona.Lengua,
                Religion = a.Persona.Religion,
                Discapacidad = a.Persona.Discapacidad,
                TipoDiscapacidad = a.Persona.TipoDiscapacidad,
                Parentesco = a.Persona.Parentesco,
                NivelEstudio = a.Persona.NivelEstudio,
                Relacion = a.Persona.Relacion,
                DatosFalsos = a.Persona.DatosFalsos,
                DatosProtegidos = a.Persona.DatosProtegidos,
                Edad = a.Persona.Edad,
                DocPoderNotarial = a.Persona.DocPoderNotarial,
                InicioDetenido = a.Persona.InicioDetenido,
                CumpleRequisitoLey = a.Persona.CumpleRequisitoLey,
                DecretoLibertad = a.Persona.DecretoLibertad,
                DispusoLibertad = a.Persona.DispusoLibertad

            });

        }

        // GET: api/RAPs/listarrepresentados/id
        [Authorize(Roles = "Administrador, Recepción,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{personaId}")]
        public async Task<IEnumerable<VictimasViewModel>> listarrepresentados([FromRoute] Guid personaId)

        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Where(x => x.PersonaId == personaId)
                            .ToListAsync();

            return rap.Select(a => new VictimasViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                idRAP = a.IdRAP,
                RAtencionId = a.RAtencionId,
                PersonaId = a.PersonaId,
                ClasificacionPersona = a.ClasificacionPersona,
                PInicio = a.PInicio,
                /*********************************************/

                /*********************************************/
                /*CAT_PERSONA*/
                StatusAnonimo = a.Persona.StatusAnonimo,
                TipoPersona = a.Persona.TipoPersona,
                NombreCompleto = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Nombre = a.Persona.Nombre,
                ApellidoPaterno = a.Persona.ApellidoPaterno,
                ApellidoMaterno = a.Persona.ApellidoMaterno,
                Alias = a.Persona.Alias,
                FechaNacimiento = a.Persona.FechaNacimiento,
                EntidadFederativa = a.Persona.EntidadFederativa,
                RFC = a.Persona.RFC,
                RazonSocial = a.Persona.RazonSocial,
                DocIdentificacion = a.Persona.DocIdentificacion,
                CURP = a.Persona.CURP,
                //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
                PoblacionAfro = (bool)(a.Persona.PoblacionAfro == null ? false : a.Persona.PoblacionAfro),
                RangoEdad = a.Persona.RangoEdad,
                RangoEdadTF = (bool)(a.Persona.RangoEdadTF == null ? false : a.Persona.RangoEdadTF),
                //--------------------------------------------------------------------------------------------------
                Sexo = a.Persona.Sexo,
                Genero = a.Persona.Genero,
                Registro = a.Persona.Registro.GetValueOrDefault(false),
                VerR = a.Persona.VerR.GetValueOrDefault(false),
                VerI = a.Persona.VerI.GetValueOrDefault(false),
                EstadoCivil = a.Persona.EstadoCivil,
                Telefono1 = a.Persona.Telefono1,
                Telefono2 = a.Persona.Telefono2,
                Correo = a.Persona.Correo,
                Medionotificacion = a.Persona.Medionotificacion,
                Nacionalidad = a.Persona.Nacionalidad,
                Ocupacion = a.Persona.Ocupacion,
                Lengua = a.Persona.Lengua,
                Religion = a.Persona.Religion,
                Discapacidad = a.Persona.Discapacidad,
                TipoDiscapacidad = a.Persona.TipoDiscapacidad,
                Parentesco = a.Persona.Parentesco,
                NivelEstudio = a.Persona.NivelEstudio,
                Relacion = a.Persona.Relacion,
                DatosFalsos = a.Persona.DatosFalsos,
                DatosProtegidos = a.Persona.DatosProtegidos,
                Edad = a.Persona.Edad,
                DocPoderNotarial = a.Persona.DocPoderNotarial,
                InicioDetenido = a.Persona.InicioDetenido,
                CumpleRequisitoLey = a.Persona.CumpleRequisitoLey,
                DecretoLibertad = a.Persona.DecretoLibertad,
                DispusoLibertad = a.Persona.DispusoLibertad

            });

        }
        //NUEVA API PARA LISTAR LOS POLICIAS QUE DETUVIERON AL IMPUTADO
        // GET: api/RAPs/listarPoliciasDetuvieron
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{Idpersona}")]
        public async Task<IActionResult> listarPoliciasDetuvieron([FromRoute] Guid Idpersona)


        {

            try
            {
                String busquedaVictimasM = @"select 
                                                    CONCAT(p.Nombre,' ',p.ApellidoPaterno,' ',p.ApellidoMaterno)as NombreC, 
                                                    r.IdRAP,
                                                    r.RAtencionId,
                                                    r.PersonaId,
                                                    r.ClasificacionPersona,
                                                    p.StatusAnonimo,
                                                    p.TipoPersona,
                                                    p.Nombre,
                                                    p.ApellidoPaterno,
                                                    p.ApellidoMaterno,
                                                    p.Alias,
                                                    p.FechaNacimiento,
                                                    p.EntidadFederativa,
                                                    p.RFC,
                                                    p.RazonSocial,
                                                    p.DocIdentificacion,
                                                    p.CURP,
                                                    p.Sexo,
                                                    p.Genero,
                                                    p.EstadoCivil,
                                                    p.Telefono1,
                                                    p.Telefono2,
                                                    p.Correo,
                                                    p.Medionotificacion,
                                                    p.Nacionalidad,
                                                    p.Ocupacion,
                                                    p.Lengua,
                                                    p.Religion,
                                                    p.Discapacidad,
                                                    p.TipoDiscapacidad,
                                                    p.Parentesco,
                                                    p.NivelEstudio,
                                                    p.Relacion
                                                    from CAT_RAP as r
                                                    left join CAT_PERSONA as p on r.PersonaId = p.IdPersona
                                                    where p.IdPersona = (select p.PoliciaDetuvo from CAT_PERSONA as p where p.IdPersona = @idpersona)";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@idpersona", Idpersona));
                var rap = await _context.qVictimaMedida.FromSqlRaw(busquedaVictimasM, filtrosBusqueda.ToArray()).ToListAsync();


                return Ok(rap.Select(a => new VictimasViewModelM
                {
                    /*********************************************/
                    /*CAT_RAP*/
                    idRAP = a.IdRAP,
                    RAtencionId = a.RAtencionId,
                    PersonaId = a.PersonaId,
                    ClasificacionPersona = a.ClasificacionPersona,
                    /*********************************************/

                    /*********************************************/
                    /*CAT_PERSONA*/
                    StatusAnonimo = a.StatusAnonimo,
                    TipoPersona = a.TipoPersona,
                    NombreC = a.NombreC,
                    Nombre = a.Nombre,
                    ApellidoPaterno = a.ApellidoPaterno,
                    ApellidoMaterno = a.ApellidoMaterno,
                    Alias = a.Alias,
                    FechaNacimiento = a.FechaNacimiento,
                    EntidadFederativa = a.EntidadFederativa,
                    RFC = a.RFC,
                    RazonSocial = a.RazonSocial,
                    DocIdentificacion = a.DocIdentificacion,
                    CURP = a.CURP,
                    Sexo = a.Sexo,
                    Genero = a.Genero,
                    EstadoCivil = a.EstadoCivil,
                    Telefono1 = a.Telefono1,
                    Telefono2 = a.Telefono2,
                    Correo = a.Correo,
                    Medionotificacion = a.Medionotificacion,
                    Nacionalidad = a.Nacionalidad,
                    Ocupacion = a.Ocupacion,
                    Lengua = a.Lengua,
                    Religion = a.Religion,
                    Discapacidad = a.Discapacidad,
                    TipoDiscapacidad = a.TipoDiscapacidad,
                    Parentesco = a.Parentesco,
                    NivelEstudio = a.NivelEstudio,
                    Relacion = a.Relacion

                }));

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }


        // GET: api/RAPs/ListarFull/id
        [Authorize(Roles = "Administrador, Recepción,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rAtencionId}")]     
        public async Task<IEnumerable<VictimasViewModel>> ListarTodosFull([FromRoute] Guid rAtencionId)

        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Include(a=> a.DatoProtegido)
                            .Where(x => x.RAtencionId == rAtencionId)
                            .ToListAsync();

            return rap.Select(a => new VictimasViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                idRAP = a.IdRAP,
                RAtencionId = a.RAtencionId,
                PersonaId = a.PersonaId,
                ClasificacionPersona = a.ClasificacionPersona,
                PInicio = a.PInicio,
                /*********************************************/
                
                /*********************************************/
                /*CAT_PERSONA*/
                StatusAnonimo = a.Persona.StatusAnonimo,
                TipoPersona = a.Persona.TipoPersona,
                NombreCompleto = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Nombre = a.Persona.DatosProtegidos  ? a.DatoProtegido.Nombre: a.Persona.Nombre, //a.Persona.Nombre,
                ApellidoPaterno = a.Persona.DatosProtegidos ? a.DatoProtegido.APaterno: a.Persona.ApellidoPaterno,//a.Persona.ApellidoPaterno,
                ApellidoMaterno = a.Persona.DatosProtegidos ? a.DatoProtegido.AMaterno: a.Persona.ApellidoMaterno,//a.Persona.ApellidoMaterno,
                Alias = a.Persona.Alias,
                FechaNacimiento = a.Persona.DatosProtegidos ? a.DatoProtegido.FechaNacimiento : a.Persona.FechaNacimiento,//a.Persona.FechaNacimiento,
                EntidadFederativa = a.Persona.EntidadFederativa,
                RFC = a.Persona.DatosProtegidos ? a.DatoProtegido.RFC: a.Persona.RFC,//a.Persona.RFC,
                RazonSocial = a.Persona.RazonSocial,
                DocIdentificacion = a.Persona.DocIdentificacion,
                CURP = a.Persona.DatosProtegidos ? a.DatoProtegido.CURP : a.Persona.CURP,
                //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
                PoblacionAfro = (bool)(a.Persona.PoblacionAfro == null ? false : a.Persona.PoblacionAfro),
                RangoEdad = a.Persona.RangoEdad,
                RangoEdadTF = (bool)(a.Persona.RangoEdadTF == null ? false : a.Persona.RangoEdadTF),
                //--------------------------------------------------------------------------------------------------
                Sexo = a.Persona.Sexo,
                Genero = a.Persona.Genero,
                Registro = a.Persona.Registro.GetValueOrDefault(false),
                VerR = a.Persona.VerR.GetValueOrDefault(false),
                VerI = a.Persona.VerI.GetValueOrDefault(false),
                EstadoCivil = a.Persona.EstadoCivil,
                Telefono1 = a.Persona.Telefono1,
                Telefono2 = a.Persona.Telefono2,
                Correo = a.Persona.Correo,
                Medionotificacion = a.Persona.Medionotificacion,
                Nacionalidad = a.Persona.Nacionalidad,
                Ocupacion = a.Persona.Ocupacion,
                Lengua = a.Persona.Lengua,
                Religion = a.Persona.Religion,
                Discapacidad = a.Persona.Discapacidad,
                TipoDiscapacidad = a.Persona.TipoDiscapacidad,
                Parentesco = a.Persona.Parentesco,
                NivelEstudio = a.Persona.NivelEstudio,
                Relacion = a.Persona.Relacion,
                DatosFalsos = a.Persona.DatosFalsos,
                DatosProtegidos = a.Persona.DatosProtegidos,
                Edad = a.Persona.Edad,
                DocPoderNotarial = a.Persona.DocPoderNotarial,
                InicioDetenido = a.Persona.InicioDetenido,
                CumpleRequisitoLey = a.Persona.CumpleRequisitoLey,
                DecretoLibertad = a.Persona.DecretoLibertad,
                DispusoLibertad = a.Persona.DispusoLibertad

            });

        }
        // GET: api/RAPs/ListarRegistroPorPersona
        [Authorize(Roles = "Administrador,Recepción,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{personaId}")]
        public async Task<IEnumerable<ListaRegistrosENUCViewModel>> ListarRegistroPorPersona([FromRoute] Guid personaId)

        {
            var rap = await _context.RAPs
                            .Where(x => x.PersonaId == personaId)
                            .Where(x => x.RAtencion.StatusRegistro == true)
                            .Include(x => x.RAtencion).ToListAsync();

            return rap.Select(a => new ListaRegistrosENUCViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                IdRAP = a.IdRAP,
                RAtencionId = a.RAtencionId,
                PersonaId = a.PersonaId,
                ClasificacionPersona = a.ClasificacionPersona,
            });

        }

        // CREA EL REGISTRO DE  ATENCION DE PERSONA SIN  MANDAR EL USUARIO QUE ATIENDE, MODULO Y PUESTO
        // POST: api/RAPs/CrearRAP
        [Authorize(Roles = "Administrador,Recepción,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-AMP")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearRAP(CrearRAPViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DateTime fecha = System.DateTime.Now;
            string serie = "A";
            int noturno = 1;
            try
            {


                RAtencion InsertarRA = new RAtencion
                {
                    DistritoInicial = model.DistritoInicial,
                    FechaHoraRegistro = fecha,
                    AgenciaInicial = model.AgenciaInicial,
                    DirSubProcuInicial = model.DirSubProcu,
                    StatusAtencion = false,
                    StatusRegistro = false,
                    racId = model.racid,
                    MedioDenuncia = "Denuncia",
                    ContencionVicitma = false,
                };

                _context.RAtencions.Add(InsertarRA);
                RAP InsertarRAP = new RAP
                {
                    RAtencionId = InsertarRA.IdRAtencion,
                    PersonaId = model.PersonaId,
                    ClasificacionPersona = model.ClasificacionPersona,
                    PInicio = model.PInicio,
                };

                _context.RAPs.Add(InsertarRAP);

                Guid idagencia = model.agenciaId;

                var turno = await _context.Turnos
                                 .Where(a => a.AgenciaId == idagencia)
                                 .Where(a => a.FechaHoraInicio.ToString("dd/MM/yyyy") == fecha.ToString("dd/MM/yyyy"))
                                 .OrderByDescending(a => a.NoTurno)
                                 .Take(1)
                                 .FirstOrDefaultAsync();



                if (turno != null)
                {
                    serie = turno.Serie;
                    noturno = turno.NoTurno + 1;
                }



                Turno InsertarTurno = new Turno
                {
                    Serie = serie,
                    NoTurno = noturno,
                    FechaHoraInicio = fecha,
                    AgenciaId = idagencia,
                    Status = false,
                    StatusReAsignado = false,
                    RAtencionId = InsertarRA.IdRAtencion,
                    Modulo = model.Modulo,
                };
                _context.Turnos.Add(InsertarTurno);
                //********************************************************************** 

                await _context.SaveChangesAsync();

                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }
            return Ok(new { notu = noturno, fh = fecha.ToString("dd/MM/yyyy hh:mm:ss") });


        }


        // POST: api/RAPs/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear(CrearVicitmaViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid personaid;
            Guid idrap;
            try
            {
                Persona InsertarPersona = new Persona
                {
                    StatusAnonimo = model.StatusAnonimo,
                    TipoPersona = model.TipoPersona,
                    RFC = model.RFC,
                    RazonSocial = model.RazonSocial,
                    Nombre = model.Nombre,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = model.ApellidoMaterno,
                    Alias = model.Alias,
                    StatusAlias = model.StatusAlias,
                    FechaNacimiento = model.FechaNacimiento,
                    EntidadFederativa = model.EntidadFederativa,
                    DocIdentificacion = model.DocIdentificacion,
                    CURP = model.CURP,
                    //AGREGACIONES NUEVAS QUE NO ESTAN EN TODOS LADOS
                    PoblacionAfro = model.PoblacionAfro,
                    RangoEdad = model.RangoEdad,
                    RangoEdadTF = model.RangoEdadTF,
                    PoliciaDetuvo = model.PoliciaDetuvo,
                    //------------------------------------------
                    Sexo = model.Sexo,
                    Genero = model.Genero,
                    Registro = model.Registro,
                    VerR = model.VerR,
                    VerI = model.VerI,
                    EstadoCivil = model.EstadoCivil,
                    Telefono1 = model.Telefono1,
                    Telefono2 = model.Telefono2,
                    Correo = model.Correo,
                    Medionotificacion = model.Medionotificacion,
                    Nacionalidad = model.Nacionalidad,
                    Ocupacion = model.Ocupacion,
                    NivelEstudio = model.NivelEstudio,
                    Lengua = model.Lengua,
                    Religion = model.Religion,
                    Discapacidad = model.Discapacidad,
                    TipoDiscapacidad = model.TipoDiscapacidad,
                    DatosProtegidos = model.DatosProtegidos,
                    Parentesco = model.Parentesco,
                    Edad = model.Edad,
                    Relacion = model.Relacion,
                    DocPoderNotarial = model.DocPoderNotarial,
                    InicioDetenido = model.InicioDetenido,
                    CumpleRequisitoLey = model.CumpleRequisitoLey,
                    DecretoLibertad = model.DecretoLibertad,
                    DispusoLibertad = model.DispusoLibertad

                };

                _context.Personas.Add(InsertarPersona);


                var idRA = model.RAtencionId;
                var idP = InsertarPersona.IdPersona;

                DireccionPersonal InsertarDP = new DireccionPersonal
                {

                    Calle = model.Calle,
                    NoExt = model.NoExt,
                    NoInt = model.NoInt,
                    EntreCalle1 = model.EntreCalle1,
                    EntreCalle2 = model.EntreCalle2,
                    Referencia = model.Referencia,
                    Pais = model.Pais,
                    Estado = model.Estado,
                    Municipio = model.Municipio,
                    Localidad = model.Localidad,
                    CP = model.CP,
                    lat = model.lat,
                    lng = model.lng,
                    PersonaId = idP,
                    TipoVialidad = model.tipoVialidad,
                    TipoAsentamiento = model.tipoAsentamiento,
                };

                _context.DireccionPersonals.Add(InsertarDP);



                RAP InsertarRAP = new RAP
                {
                    RAtencionId = idRA,
                    PersonaId = idP,
                    ClasificacionPersona = model.ClasificacionPersona,
                    PInicio = model.PInicio,
                };

                _context.RAPs.Add(InsertarRAP);

                DireccionEscucha InsertarDE = new DireccionEscucha
                {
                    RAPId = InsertarRAP.IdRAP,
                    Calle = model.de_Calle,
                    NoExt = model.de_NoExt,
                    NoInt = model.de_NoInt,
                    EntreCalle1 = model.de_EntreCalle1,
                    EntreCalle2 = model.de_EntreCalle2,
                    Referencia = model.de_Referencia,
                    Pais = model.de_Pais,
                    Estado = model.de_Estado,
                    Municipio = model.de_Municipio,
                    Localidad = model.de_Localidad,
                    CP = model.de_CP,
                    lat = model.de_lat,
                    lng = model.de_lng,
                    TipoVialidad = model.de_tipoVialidad,
                    TipoAsentamiento = model.de_tipoAsentamiento,
                };

                _context.DireccionEscuchas.Add(InsertarDE);
                //********************************************************************** 

                await _context.SaveChangesAsync();
                personaid = InsertarPersona.IdPersona;
                idrap = InsertarRAP.IdRAP;
                //**********************************************************************
                return Ok(new { personaid = personaid ,  idrap = idrap });
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // POST: api/RAPs/Insertar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Insertar(InsertarRAPViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid rapidGenerado ;

            try
            {

                RAP InsertarRAP = new RAP
                {
                    RAtencionId = model.RAtencionId,
                    PersonaId = model.PersonaId,
                    ClasificacionPersona = model.ClasificacionPersona,
                    PInicio = model.PInicio,
                };

                _context.RAPs.Add(InsertarRAP);

                rapidGenerado = InsertarRAP.IdRAP;

                DireccionEscucha InsertarDE = new DireccionEscucha
                {
                    RAPId = InsertarRAP.IdRAP,
                    Calle = model.de_Calle,
                    NoExt = model.de_NoExt,
                    NoInt = model.de_NoInt,
                    EntreCalle1 = model.de_EntreCalle1,
                    EntreCalle2 = model.de_EntreCalle2,
                    Referencia = model.de_Referencia,
                    Pais = model.de_Pais,
                    Estado = model.de_Estado,
                    Municipio = model.de_Municipio,
                    Localidad = model.de_Localidad,
                    CP = model.de_CP,
                    lat = model.de_lat,
                    lng = model.de_lng,
                    TipoVialidad = model.de_tipoVialidad,
                    TipoAsentamiento = model.de_tipoAsentamiento,
                };

                _context.DireccionEscuchas.Add(InsertarDE);
                //********************************************************************** 

                await _context.SaveChangesAsync();

                //**********************************************************************



            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }


            return Ok();
        }
        // PUT: api/RAPs/Actualizar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Director, Coordinador, Jurídico,  Facilitador, Notificador, AMPO-IL,Recepción")] 
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarVictimaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rap = await _context.RAPs.FirstOrDefaultAsync(a => a.IdRAP == model.rapId);
            if (rap == null)
            {
                return NotFound();
            }
            rap.ClasificacionPersona = model.ClasificacionPersona;


            var persona = await _context.Personas.FirstOrDefaultAsync(a => a.IdPersona == model.PersonaId);

            if (persona == null)
            {

            }
            persona.StatusAnonimo = model.StatusAnonimo;
            persona.TipoPersona = model.TipoPersona;
            persona.RFC = model.RFC;
            persona.RazonSocial = model.RazonSocial;
            persona.Nombre = model.Nombre;
            persona.ApellidoPaterno = model.ApellidoPaterno;
            persona.ApellidoMaterno = model.ApellidoMaterno;
            persona.Alias = model.Alias;
            persona.StatusAlias = model.StatusAlias;
            persona.FechaNacimiento = model.FechaNacimiento;
            persona.EntidadFederativa = model.EntidadFederativa;
            persona.CURP = model.CURP;
            //AGREGACIONES NUEVAS QUE NO ESTAN EN TODOS LADOS
            persona.PoblacionAfro = model.PoblacionAfro;
            persona.RangoEdad = model.RangoEdad;
            persona.RangoEdadTF = model.RangoEdadTF;
            persona.PoliciaDetuvo = model.PoliciaDetuvo;
            //---------------------------------------------
            persona.Sexo = model.Sexo;
            persona.DocIdentificacion = model.DocIdentificacion;
            persona.Genero = model.Genero;
            persona.Registro = model.Registro;
            persona.VerR = model.VerR;
            persona.VerI = model.VerI;
            persona.EstadoCivil = model.EstadoCivil;
            persona.Telefono1 = model.Telefono1;
            persona.Telefono2 = model.Telefono2;
            persona.Correo = model.Correo;
            persona.Medionotificacion = model.Medionotificacion;
            persona.Nacionalidad = model.Nacionalidad;
            persona.Ocupacion = model.Ocupacion;
            persona.NivelEstudio = model.NivelEstudio;
            persona.Lengua = model.Lengua;
            persona.Religion = model.Religion;
            persona.Discapacidad = model.Discapacidad;
            persona.TipoDiscapacidad = model.TipoDiscapacidad;
            persona.DatosProtegidos = model.DatosProtegidos;
            persona.Parentesco = model.Parentesco;
            persona.Relacion = model.Relacion;
            persona.Edad = model.Edad;
            persona.DocPoderNotarial = model.DocPoderNotarial;
            persona.InicioDetenido = model.InicioDetenido;
            persona.CumpleRequisitoLey = model.CumpleRequisitoLey;
            persona.DecretoLibertad = model.DecretoLibertad;
            persona.DispusoLibertad = model.DispusoLibertad;


            //********************************************
            var direccionp = await _context.DireccionPersonals.FirstOrDefaultAsync(a => a.PersonaId == model.PersonaId);

            if (direccionp == null)
            {
                DireccionPersonal InsertarDP = new DireccionPersonal
                {

                    Calle = model.calle,
                    NoExt = model.noext,
                    NoInt = model.noint,
                    EntreCalle1 = model.entrecalle1,
                    EntreCalle2 = model.entrecalle2,
                    Referencia = model.referencia,
                    Pais = model.pais,
                    Estado = model.estado,
                    Municipio = model.municipio,
                    Localidad = model.localidad,
                    CP = model.cp,
                    lat = model.lat,
                    lng = model.lng,
                    PersonaId = model.PersonaId,
                    TipoVialidad = model.tipoVialidad,
                    TipoAsentamiento = model.tipoAsentamiento,
                };

                _context.DireccionPersonals.Add(InsertarDP);
            }
            else
            {
                direccionp.Calle = model.calle;
                direccionp.NoExt = model.noext;
                direccionp.NoInt = model.noint;
                direccionp.EntreCalle1 = model.entrecalle1;
                direccionp.EntreCalle2 = model.entrecalle2;
                direccionp.Referencia = model.referencia;
                direccionp.Pais = model.pais;
                direccionp.Estado = model.estado;
                direccionp.Municipio = model.municipio;
                direccionp.Localidad = model.localidad;
                direccionp.CP = model.cp;
                direccionp.lat = model.lat;
                direccionp.lng = model.lng;
                direccionp.TipoVialidad = model.tipoVialidad;
                direccionp.TipoAsentamiento = model.tipoAsentamiento;
            }
            

            //********************************************
            var direccione = await _context.DireccionEscuchas.FirstOrDefaultAsync(a => a.RAPId == model.rapId);

            if (direccione == null)
            {
                DireccionEscucha InsertarDE = new DireccionEscucha
                {
                    RAPId = model.rapId,
                    Calle = model.de_Calle,
                    NoExt = model.de_NoExt,
                    NoInt = model.de_NoInt,
                    EntreCalle1 = model.de_EntreCalle1,
                    EntreCalle2 = model.de_EntreCalle2,
                    Referencia = model.de_Referencia,
                    Pais = model.de_Pais,
                    Estado = model.de_Estado,
                    Municipio = model.de_Municipio,
                    Localidad = model.de_Localidad,
                    CP = model.de_CP,
                    lat = model.de_lat,
                    lng = model.de_lng,
                    TipoVialidad = model.de_tipoVialidad,
                    TipoAsentamiento = model.de_tipoAsentamiento,
                };

                _context.DireccionEscuchas.Add(InsertarDE);
            }
            else
            {
                direccione.Calle = model.de_Calle;
                direccione.NoExt = model.de_NoExt;
                direccione.NoInt = model.de_NoInt;
                direccione.EntreCalle1 = model.de_EntreCalle1;
                direccione.EntreCalle2 = model.de_EntreCalle2;
                direccione.Referencia = model.de_Referencia;
                direccione.Pais = model.de_Pais;
                direccione.Estado = model.de_Estado;
                direccione.Municipio = model.de_Municipio;
                direccione.Localidad = model.de_Localidad;
                direccione.CP = model.de_CP;
                direccione.lat = model.de_lat;
                direccione.lng = model.de_lng;
                direccione.TipoVialidad = model.de_tipoVialidad;
                direccione.TipoAsentamiento = model.de_tipoAsentamiento;
            }
            

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


            return Ok();
        }
        // PUT: api/RAPs/ActualizarPersonaDirPer
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Director, Coordinador, Jurídico,  Facilitador, Notificador,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarPersonaDirPer([FromBody] ActualizarVictimaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var persona = await _context.Personas.FirstOrDefaultAsync(a => a.IdPersona == model.PersonaId);

            if (persona == null)
            {
                return NotFound();
            }
            persona.StatusAnonimo = model.StatusAnonimo;
            persona.TipoPersona = model.TipoPersona;
            persona.RFC = model.RFC;
            persona.RazonSocial = model.RazonSocial;
            persona.Nombre = model.Nombre;
            persona.ApellidoPaterno = model.ApellidoPaterno;
            persona.Alias = model.Alias;
            persona.StatusAlias = model.StatusAlias;
            persona.ApellidoMaterno = model.ApellidoMaterno;
            persona.FechaNacimiento = model.FechaNacimiento;
            persona.EntidadFederativa = model.EntidadFederativa;
            persona.CURP = model.CURP;
            //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
            persona.PoblacionAfro = model.PoblacionAfro;
            persona.RangoEdad = model.RangoEdad;
            persona.RangoEdadTF = model.RangoEdadTF;
            persona.PoliciaDetuvo = model.PoliciaDetuvo;
            //-----------------------------------------------------------------------------------------------
            persona.Sexo = model.Sexo;
            persona.DocIdentificacion = model.DocIdentificacion;
            persona.Genero = model.Genero;
            persona.Registro = model.Registro;
            persona.VerR = model.VerR;
            persona.VerI = model.VerI;
            persona.EstadoCivil = model.EstadoCivil;
            persona.Telefono1 = model.Telefono1;
            persona.Telefono2 = model.Telefono2;
            persona.Correo = model.Correo;
            persona.Medionotificacion = model.Medionotificacion;
            persona.Nacionalidad = model.Nacionalidad;
            persona.Ocupacion = model.Ocupacion;
            persona.NivelEstudio = model.NivelEstudio;
            persona.Lengua = model.Lengua;
            persona.Religion = model.Religion;
            persona.Discapacidad = model.Discapacidad;
            persona.TipoDiscapacidad = model.TipoDiscapacidad;
            persona.Parentesco = model.Parentesco;
            persona.Relacion = model.Relacion;
            persona.Edad = model.Edad;
            persona.DocPoderNotarial = model.DocPoderNotarial;

            //********************************************
            var direccionp = await _context.DireccionPersonals.FirstOrDefaultAsync(a => a.PersonaId == model.PersonaId);

            if (direccionp == null)
            {
                return NotFound();
            }
            direccionp.Calle = model.calle;
            direccionp.NoExt = model.noext;
            direccionp.NoInt = model.noint;
            direccionp.EntreCalle1 = model.entrecalle1;
            direccionp.EntreCalle2 = model.entrecalle2;
            direccionp.Referencia = model.referencia;
            direccionp.Pais = model.pais;
            direccionp.Estado = model.estado;
            direccionp.Municipio = model.municipio;
            direccionp.Localidad = model.localidad;
            direccionp.CP = model.cp;
            direccionp.lat = model.lat;
            direccionp.lng = model.lng;
            direccionp.TipoVialidad = model.tipoVialidad;
            direccionp.TipoAsentamiento = model.tipoAsentamiento;

            //********************************************


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }
        // GET: api/RAPs/ListarDP
       
        [HttpGet("[action]/{personaId}")]
        public async Task<IActionResult> ListarDP([FromRoute] Guid personaId)

        {
            var dp = await _context.DireccionPersonals                         
                            .Where(x => x.PersonaId == personaId)
                            .FirstOrDefaultAsync();

            if (dp == null)
            {
                var result = new ObjectResult(new { statusCode = "404", message = "No se ha registrado dirección de las personas involucradas." });
                result.StatusCode = 404;
                return result;

            }

            return Ok(new DireccionPersonalViewModel
            {
                /*********************************************/
                /*CAT_DIRECCIONPERSONA*/
                calle = dp.Calle,
                noint = dp.NoInt,
                noext = dp.NoExt,
                entrecalle1 = dp.EntreCalle1,
                entrecalle2 = dp.EntreCalle2,
                referencia = dp.Referencia,
                pais = dp.Pais,
                estado = dp.Estado,
                municipio = dp.Municipio,
                localidad = dp.Localidad,
                cp = dp.CP,
                lat = dp.lat,
                lng = dp.lng,
                idPersona = dp.PersonaId,
                tipoVialidad = dp.TipoVialidad,
                tipoAsentamiento = dp.TipoAsentamiento,
            });

        }

        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador, USAR, AMPO-AMP Mixto, AMPO AMP")]
        [HttpGet("[action]/{personaId}/{idDistrito}")]
        public async Task<IActionResult> ListarDPxDis([FromRoute] Guid personaId, Guid idDistrito)

        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + idDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var dp = await ctx.DireccionPersonals
                                .Where(x => x.PersonaId == personaId)
                                .ToListAsync();


                    return Ok(dp.Select(a => new DireccionPersonalViewModel
                    {
                        /*********************************************/
                        /*CAT_DIRECCIONPERSONA*/
                        calle = a.Calle,
                        noint = a.NoInt,
                        noext = a.NoExt,
                        entrecalle1 = a.EntreCalle1,
                        entrecalle2 = a.EntreCalle2,
                        referencia = a.Referencia,
                        pais = a.Pais,
                        estado = a.Estado,
                        municipio = a.Municipio,
                        localidad = a.Localidad,
                        cp = a.CP,
                        lat = a.lat,
                        lng = a.lng,
                        idPersona = a.PersonaId
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


        // GET: api/RAPs/ListarDE

        [HttpGet("[action]/{rapId}")]
        public async Task<IActionResult> ListarDE([FromRoute] Guid rapId)

        {
            var de = await _context.DireccionEscuchas
                            .Where(x => x.RAPId == rapId)
                            .Include(x => x.RAP)
                            .FirstOrDefaultAsync();

            if (de == null)
            {
                return NotFound("No hay registros");

            }

            return Ok(new DireccionEscuchaViewModel
            {
                IdDEscucha = de.IdDEscucha,
                calle = de.Calle,
                noint = de.NoInt,
                noext = de.NoExt,
                entrecalle1 = de.EntreCalle1,
                entrecalle2 = de.EntreCalle2,
                referencia = de.Referencia,
                pais = de.Pais,
                estado = de.Estado,
                municipio = de.Municipio,
                localidad = de.Localidad,
                cp = de.CP,
                lat = de.lat,
                lng = de.lng,
                idPersona = de.RAP.PersonaId,
                tipoVialidad = de.TipoVialidad,
                tipoAsentamiento = de.TipoAsentamiento,
            });

        }

        // PUT: api/RAPs/Actualizardireccionescucha
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizardireccionescucha([FromBody] DireccionEscuchaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dies = await _context.DireccionEscuchas.FirstOrDefaultAsync(a => a.IdDEscucha == model.IdDEscucha);

            if (dies == null)
            {
                return NotFound();
            }

            dies.Calle = model.calle;
            dies.NoInt = model.noint;
            dies.NoExt = model.noext;
            dies.EntreCalle1 = model.entrecalle1;
            dies.EntreCalle2 = model.entrecalle2;
            dies.Referencia = model.referencia;
            dies.Pais = model.pais;
            dies.Estado = model.estado;
            dies.Municipio = model.municipio;
            dies.Localidad = model.localidad;
            dies.CP = model.cp;
            dies.lat = model.lat;
            dies.lng = model.lng;
            dies.TipoVialidad = model.tipoVialidad;
            dies.TipoAsentamiento = model.tipoAsentamiento;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }


        // PUT: api/RAPs/ActualizarMC

        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarMC([FromBody] ActualizarMCViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var ra = await _context.RAtencions.FirstOrDefaultAsync(a => a.IdRAtencion == model.idRAtencion);

            if (ra == null)
            {
                return NotFound();
            }

            ra.ContencionVicitma = model.contencionVictima;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // GET: api/RAPs/Listarpersona/personaId
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción, Facilitador, Facilitador-Mixto")]        
        [HttpGet("[action]/{personaId}")]
        public async Task<IActionResult> Listarpersona([FromRoute] Guid personaId)

        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Where(a => a.PersonaId == personaId)
                            .FirstOrDefaultAsync();

            if (rap == null)
            {
                return NotFound("No hay registros");

            }


            return Ok(new VictimasViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                idRAP = rap.IdRAP,
                RAtencionId = rap.RAtencionId,
                PersonaId = rap.PersonaId,
                /*********************************************/

                /*********************************************/
                /*CAT_PERSONA*/
                StatusAnonimo = rap.Persona.StatusAnonimo,
                ClasificacionPersona = rap.ClasificacionPersona,
                TipoPersona = rap.Persona.TipoPersona,
                NombreCompleto = rap.Persona.Nombre + " " + rap.Persona.ApellidoPaterno + " " + rap.Persona.ApellidoMaterno,
                Nombre = rap.Persona.Nombre,
                ApellidoPaterno = rap.Persona.ApellidoPaterno,
                ApellidoMaterno = rap.Persona.ApellidoMaterno,
                Alias = rap.Persona.Alias,
                FechaNacimiento = rap.Persona.FechaNacimiento,
                EntidadFederativa = rap.Persona.EntidadFederativa,
                RFC = rap.Persona.RFC,
                RazonSocial = rap.Persona.RazonSocial,
                DocIdentificacion = rap.Persona.DocIdentificacion,
                CURP = rap.Persona.CURP,
                //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
                PoblacionAfro = (bool)(rap.Persona.PoblacionAfro == null ? false : rap.Persona.PoblacionAfro),
                RangoEdad = rap.Persona.RangoEdad,
                RangoEdadTF = (bool)(rap.Persona.RangoEdadTF == null ? false : rap.Persona.RangoEdadTF),
                //--------------------------------------------------------------------------------------------------
                Sexo = rap.Persona.Sexo,
                Genero = rap.Persona.Genero,
                VerR = rap.Persona.VerR.GetValueOrDefault(false),
                VerI = rap.Persona.VerI.GetValueOrDefault(false),
                EstadoCivil = rap.Persona.EstadoCivil,
                Telefono1 = rap.Persona.Telefono1,
                Telefono2 = rap.Persona.Telefono2,
                Correo = rap.Persona.Correo,
                Medionotificacion = rap.Persona.Medionotificacion,
                Nacionalidad = rap.Persona.Nacionalidad,
                Ocupacion = rap.Persona.Ocupacion,
                Lengua = rap.Persona.Lengua,
                Religion = rap.Persona.Religion,
                Discapacidad = rap.Persona.Discapacidad,
                TipoDiscapacidad = rap.Persona.TipoDiscapacidad,
                Parentesco = rap.Persona.Parentesco,
                NivelEstudio = rap.Persona.NivelEstudio,
                Edad = rap.Persona.Edad,
                DatosProtegidos=rap.Persona.DatosProtegidos,
                DocPoderNotarial = rap.Persona.DocPoderNotarial

            });

        }
        // GET: api/RAPs/ListarImputados/  
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IEnumerable<VictimasViewModel>> ListarImputados([FromRoute] Guid rAtencionId)

        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona) 
                            .Where(a => a.ClasificacionPersona == "Imputado")
                            .Where(x => x.RAtencionId == rAtencionId).ToListAsync();

            return rap.Select(a => new VictimasViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                idRAP = a.IdRAP,
                RAtencionId = a.RAtencionId,
                PersonaId = a.PersonaId,
                ClasificacionPersona = a.ClasificacionPersona,
                /*********************************************/

                /*********************************************/
                /*CAT_PERSONA*/
                StatusAnonimo = a.Persona.StatusAnonimo,
                TipoPersona = a.Persona.TipoPersona,
                NombreCompleto = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Nombre = a.Persona.Nombre,
                ApellidoPaterno = a.Persona.ApellidoPaterno,
                ApellidoMaterno = a.Persona.ApellidoMaterno,
                Alias = a.Persona.Alias,
                FechaNacimiento = a.Persona.FechaNacimiento,
                EntidadFederativa = a.Persona.EntidadFederativa,
                RFC = a.Persona.RFC,
                RazonSocial = a.Persona.RazonSocial,
                DocIdentificacion = a.Persona.DocIdentificacion,
                CURP = a.Persona.CURP,
                //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
                PoblacionAfro = (bool)(a.Persona.PoblacionAfro == null ? false : a.Persona.PoblacionAfro),
                RangoEdad = a.Persona.RangoEdad,
                RangoEdadTF = (bool)(a.Persona.RangoEdadTF == null ? false : a.Persona.RangoEdadTF),
                //--------------------------------------------------------------------------------------------------
                Sexo = a.Persona.Sexo,
                Genero = a.Persona.Genero,
                EstadoCivil = a.Persona.EstadoCivil,
                Telefono1 = a.Persona.Telefono1,
                Telefono2 = a.Persona.Telefono2,
                Correo = a.Persona.Correo,
                Medionotificacion = a.Persona.Medionotificacion,
                Nacionalidad = a.Persona.Nacionalidad,
                Ocupacion = a.Persona.Ocupacion,
                Lengua = a.Persona.Lengua,
                Religion = a.Persona.Religion,
                Discapacidad = a.Persona.Discapacidad,
                TipoDiscapacidad = a.Persona.TipoDiscapacidad,
                Parentesco = a.Persona.Parentesco,
                NivelEstudio = a.Persona.NivelEstudio,


            });

        }

        // GET: api/RAPs/ListarImputadoJR
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IActionResult> ListarImputadoJR([FromRoute] Guid rAtencionId, Guid RHechoId, Guid idMProteccion)

        {

            try
            {
                String busquedaVictimasJR = @"select 
                                                    CONCAT(p.Nombre,' ',p.ApellidoPaterno,' ',p.ApellidoMaterno)as NombreC, 
                                                    r.IdRAP,
                                                    r.RAtencionId,
                                                    r.PersonaId,
                                                    r.ClasificacionPersona,
                                                    p.StatusAnonimo,
                                                    p.TipoPersona,
                                                    p.Nombre,
                                                    p.ApellidoPaterno,
                                                    p.ApellidoMaterno,
                                                    p.Alias,
                                                    p.FechaNacimiento,
                                                    p.EntidadFederativa,
                                                    p.RFC,
                                                    p.RazonSocial,
                                                    p.DocIdentificacion,
                                                    p.CURP,
                                                    p.Sexo,
                                                    p.Genero,
                                                    p.EstadoCivil,
                                                    p.Telefono1,
                                                    p.Telefono2,
                                                    p.Correo,
                                                    p.Medionotificacion,
                                                    p.Nacionalidad,
                                                    p.Ocupacion,
                                                    p.Lengua,
                                                    p.Religion,
                                                    p.Discapacidad,
                                                    p.TipoDiscapacidad,
                                                    p.Parentesco,
                                                    p.NivelEstudio,
                                                    p.Relacion,
                                                    p.Edad,
                                                    CONCAT(COALESCE(v.Nombre, ''),' ',d.Calle,' ',d.NoInt,' ',d.NoExt,' ',d.Localidad,' ',d.Municipio,' ',d.Estado,' ',d.Pais,' ',d.CP)as DireccionP                                                     
                                                    from CAT_RAP as r
                                                    left join CAT_PERSONA as p on r.PersonaId = p.IdPersona
                                                    left join CAT_RATENCON as a on a.IdRAtencion = r.RAtencionId
                                                    left join CAT_DIRECCION_PERSONAL as d on d.PersonaId = r.PersonaId
                                                    left join C_TIPO_VIALIDAD as v on d.TipoVialidad = v.Clave
                                                    where a.IdRAtencion =@ratencionid and r.ClasificacionPersona = 'Imputado'";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@ratencionid", rAtencionId));

                var rap = await _context.qVictimaJR.FromSqlRaw(busquedaVictimasJR, filtrosBusqueda.ToArray()).ToListAsync();


                return Ok(rap.Select(a => new VictimasViewModelJR
                {
                    /*********************************************/
                    /*CAT_RAP*/
                    idRAP = a.IdRAP,
                    RAtencionId = a.RAtencionId,
                    PersonaId = a.PersonaId,
                    ClasificacionPersona = a.ClasificacionPersona,
                    /*********************************************/

                    /*********************************************/
                    /*CAT_PERSONA*/
                    StatusAnonimo = a.StatusAnonimo,
                    TipoPersona = a.TipoPersona,
                    NombreC = a.NombreC,
                    Nombre = a.Nombre,
                    ApellidoPaterno = a.ApellidoPaterno,
                    ApellidoMaterno = a.ApellidoMaterno,
                    Alias = a.Alias,
                    FechaNacimiento = a.FechaNacimiento,
                    EntidadFederativa = a.EntidadFederativa,
                    RFC = a.RFC,
                    RazonSocial = a.RazonSocial,
                    DocIdentificacion = a.DocIdentificacion,
                    CURP = a.CURP,
                    Sexo = a.Sexo,
                    Genero = a.Genero,
                    EstadoCivil = a.EstadoCivil,
                    Telefono1 = a.Telefono1,
                    Telefono2 = a.Telefono2,
                    Correo = a.Correo,
                    Medionotificacion = a.Medionotificacion,
                    Nacionalidad = a.Nacionalidad,
                    Ocupacion = a.Ocupacion,
                    Lengua = a.Lengua,
                    Religion = a.Religion,
                    Discapacidad = a.Discapacidad,
                    TipoDiscapacidad = a.TipoDiscapacidad,
                    Parentesco = a.Parentesco,
                    NivelEstudio = a.NivelEstudio,
                    Relacion = a.Relacion,
                    Edad = a.Edad,
                    DireccionP = a.DireccionP


                }));

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/RAPs/ListarVictimas/ 
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IEnumerable<VictimasViewModel>> ListarVictimas([FromRoute] Guid rAtencionId)

        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Where(a => a.ClasificacionPersona != "Testigo")
                            .Where(a => a.ClasificacionPersona != "Imputado")
                            .Where(x => x.RAtencionId == rAtencionId).ToListAsync();

            return rap.Select(a => new VictimasViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                idRAP = a.IdRAP,
                RAtencionId = a.RAtencionId,
                PersonaId = a.PersonaId,
                ClasificacionPersona = a.ClasificacionPersona,
                /*********************************************/

                /*********************************************/
                /*CAT_PERSONA*/
                StatusAnonimo = a.Persona.StatusAnonimo,
                TipoPersona = a.Persona.TipoPersona,
                NombreCompleto = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Nombre = a.Persona.Nombre,
                ApellidoPaterno = a.Persona.ApellidoPaterno,
                ApellidoMaterno = a.Persona.ApellidoMaterno,
                Alias = a.Persona.Alias,
                FechaNacimiento = a.Persona.FechaNacimiento,
                EntidadFederativa = a.Persona.EntidadFederativa,
                RFC = a.Persona.RFC,
                RazonSocial = a.Persona.RazonSocial,
                DocIdentificacion = a.Persona.DocIdentificacion,
                CURP = a.Persona.CURP,
                //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
                PoblacionAfro = (bool)(a.Persona.PoblacionAfro == null ? false : a.Persona.PoblacionAfro),
                RangoEdad = a.Persona.RangoEdad,
                RangoEdadTF = (bool)(a.Persona.RangoEdadTF == null ? false : a.Persona.RangoEdadTF),
                //--------------------------------------------------------------------------------------------------
                Sexo = a.Persona.Sexo,
                Genero = a.Persona.Genero,
                EstadoCivil = a.Persona.EstadoCivil,
                Telefono1 = a.Persona.Telefono1,
                Telefono2 = a.Persona.Telefono2,
                Correo = a.Persona.Correo,
                Medionotificacion = a.Persona.Medionotificacion,
                Nacionalidad = a.Persona.Nacionalidad,
                Ocupacion = a.Persona.Ocupacion,
                Lengua = a.Persona.Lengua,
                Religion = a.Persona.Religion,
                Discapacidad = a.Persona.Discapacidad,
                TipoDiscapacidad = a.Persona.TipoDiscapacidad,
                Parentesco = a.Persona.Parentesco,
                NivelEstudio = a.Persona.NivelEstudio,
                Edad = a.Persona.Edad

            });

        }

        // GET: api/RAPs/ListarVictimasJR
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IActionResult> ListarVictimasJR([FromRoute] Guid rAtencionId, Guid RHechoId, Guid idMProteccion)

        {

            try
            {
                String busquedaVictimasJR = @"select 
                                                    CONCAT(p.Nombre,' ',p.ApellidoPaterno,' ',p.ApellidoMaterno)as NombreC, 
                                                    r.IdRAP,
                                                    r.RAtencionId,
                                                    r.PersonaId,
                                                    r.ClasificacionPersona,
                                                    p.StatusAnonimo,
                                                    p.TipoPersona,
                                                    p.Nombre,
                                                    p.ApellidoPaterno,
                                                    p.ApellidoMaterno,
                                                    p.Alias,
                                                    p.FechaNacimiento,
                                                    p.EntidadFederativa,
                                                    p.RFC,
                                                    p.RazonSocial,
                                                    p.DocIdentificacion,
                                                    p.CURP,
                                                    p.Sexo,
                                                    p.Genero,
                                                    p.EstadoCivil,
                                                    p.Telefono1,
                                                    p.Telefono2,
                                                    p.Correo,
                                                    p.Medionotificacion,
                                                    p.Nacionalidad,
                                                    p.Ocupacion,
                                                    p.Lengua,
                                                    p.Religion,
                                                    p.Discapacidad,
                                                    p.TipoDiscapacidad,
                                                    p.Parentesco,
                                                    p.NivelEstudio,
                                                    p.Relacion,
                                                    p.Edad,
                                                    CONCAT(COALESCE(v.Nombre, ''),' ',d.Calle,' ',d.NoInt,' ',d.NoExt,' ',d.Localidad,' ',d.Municipio,' ',d.Estado,' ',d.Pais,' ',d.CP)as DireccionP                                                     
                                                    from CAT_RAP as r
                                                    left join CAT_PERSONA as p on r.PersonaId = p.IdPersona
                                                    left join CAT_RATENCON as a on a.IdRAtencion = r.RAtencionId
                                                    left join CAT_DIRECCION_PERSONAL as d on d.PersonaId = r.PersonaId
                                                    left join C_TIPO_VIALIDAD as v on d.TipoVialidad = v.Clave
                                                    where a.IdRAtencion =@ratencionid and r.ClasificacionPersona !='Testigo' and r.ClasificacionPersona !='Imputado' and r.ClasificacionPersona !='Oficial que detuvo'";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@ratencionid", rAtencionId));

                var rap = await _context.qVictimaJR.FromSqlRaw(busquedaVictimasJR, filtrosBusqueda.ToArray()).ToListAsync();


                return Ok(rap.Select(a => new VictimasViewModelJR
                {
                    /*********************************************/
                    /*CAT_RAP*/
                    idRAP = a.IdRAP,
                    RAtencionId = a.RAtencionId,
                    PersonaId = a.PersonaId,
                    ClasificacionPersona = a.ClasificacionPersona,
                    /*********************************************/

                    /*********************************************/
                    /*CAT_PERSONA*/
                    StatusAnonimo = a.StatusAnonimo,
                    TipoPersona = a.TipoPersona,
                    NombreC = a.NombreC,
                    Nombre = a.Nombre,
                    ApellidoPaterno = a.ApellidoPaterno,
                    ApellidoMaterno = a.ApellidoMaterno,
                    Alias = a.Alias,
                    FechaNacimiento = a.FechaNacimiento,
                    EntidadFederativa = a.EntidadFederativa,
                    RFC = a.RFC,
                    RazonSocial = a.RazonSocial,
                    DocIdentificacion = a.DocIdentificacion,
                    CURP = a.CURP,
                    Sexo = a.Sexo,
                    Genero = a.Genero,
                    EstadoCivil = a.EstadoCivil,
                    Telefono1 = a.Telefono1,
                    Telefono2 = a.Telefono2,
                    Correo = a.Correo,
                    Medionotificacion = a.Medionotificacion,
                    Nacionalidad = a.Nacionalidad,
                    Ocupacion = a.Ocupacion,
                    Lengua = a.Lengua,
                    Religion = a.Religion,
                    Discapacidad = a.Discapacidad,
                    TipoDiscapacidad = a.TipoDiscapacidad,
                    Parentesco = a.Parentesco,
                    NivelEstudio = a.NivelEstudio,
                    Relacion = a.Relacion,
                    Edad=a.Edad,
                    DireccionP = a.DireccionP
                    

                }));

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        //Api para listar todas las victimas
        // GET: api/RAPs/ListarTodosVic
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IEnumerable<VictimasViewModel>> ListarTodosVic([FromRoute] Guid rAtencionId)

        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Where(x => x.RAtencionId == rAtencionId)
                            .Where(x => x.ClasificacionPersona == "Victima indirecta" || x.ClasificacionPersona == "Victima directa" || x.ClasificacionPersona == "Testigo" || x.ClasificacionPersona == "Denunciante")
                            .ToListAsync();

            return rap.Select(a => new VictimasViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                idRAP = a.IdRAP,
                RAtencionId = a.RAtencionId,
                PersonaId = a.PersonaId,
                ClasificacionPersona = a.ClasificacionPersona,
                /*********************************************/

                /*********************************************/
                /*CAT_PERSONA*/
                StatusAnonimo = a.Persona.StatusAnonimo,
                TipoPersona = a.Persona.TipoPersona,
                NombreCompleto = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Nombre = a.Persona.Nombre,
                ApellidoPaterno = a.Persona.ApellidoPaterno,
                ApellidoMaterno = a.Persona.ApellidoMaterno,
                Alias = a.Persona.Alias,
                FechaNacimiento = a.Persona.FechaNacimiento,
                EntidadFederativa = a.Persona.EntidadFederativa,
                RFC = a.Persona.RFC,
                RazonSocial = a.Persona.RazonSocial,
                DocIdentificacion = a.Persona.DocIdentificacion,
                CURP = a.Persona.CURP,
                //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
                PoblacionAfro = (bool)(a.Persona.PoblacionAfro == null ? false : a.Persona.PoblacionAfro),
                RangoEdad = a.Persona.RangoEdad,
                RangoEdadTF = (bool)(a.Persona.RangoEdadTF == null ? false : a.Persona.RangoEdadTF),
                //--------------------------------------------------------------------------------------------------
                Sexo = a.Persona.Sexo,
                Genero = a.Persona.Genero,
                EstadoCivil = a.Persona.EstadoCivil,
                Telefono1 = a.Persona.Telefono1,
                Telefono2 = a.Persona.Telefono2,
                Correo = a.Persona.Correo,
                Medionotificacion = a.Persona.Medionotificacion,
                Nacionalidad = a.Persona.Nacionalidad,
                Ocupacion = a.Persona.Ocupacion,
                Lengua = a.Persona.Lengua,
                Religion = a.Persona.Religion,
                Discapacidad = a.Persona.Discapacidad,
                TipoDiscapacidad = a.Persona.TipoDiscapacidad,
                Parentesco = a.Persona.Parentesco,
                NivelEstudio = a.Persona.NivelEstudio,
                Relacion = a.Persona.Relacion

            });

        }

        // GET: api/RAPs/ListarVictimasM
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rAtencionId}/{RHechoId}/{idMProteccion}")]
        public async Task<IActionResult>ListarVictimasM([FromRoute] Guid rAtencionId,Guid RHechoId, Guid idMProteccion)

        {

            try
            {
                String busquedaVictimasM = @"select 
                                                    CONCAT(p.Nombre,' ',p.ApellidoPaterno,' ',p.ApellidoMaterno)as NombreC, 
                                                    r.IdRAP,
                                                    r.RAtencionId,
                                                    r.PersonaId,
                                                    r.ClasificacionPersona,
                                                    p.StatusAnonimo,
                                                    p.TipoPersona,
                                                    p.Nombre,
                                                    p.ApellidoPaterno,
                                                    p.ApellidoMaterno,
                                                    p.Alias,
                                                    p.FechaNacimiento,
                                                    p.EntidadFederativa,
                                                    p.RFC,
                                                    p.RazonSocial,
                                                    p.DocIdentificacion,
                                                    p.CURP,
                                                    p.Sexo,
                                                    p.Genero,
                                                    p.EstadoCivil,
                                                    p.Telefono1,
                                                    p.Telefono2,
                                                    p.Correo,
                                                    p.Medionotificacion,
                                                    p.Nacionalidad,
                                                    p.Ocupacion,
                                                    p.Lengua,
                                                    p.Religion,
                                                    p.Discapacidad,
                                                    p.TipoDiscapacidad,
                                                    p.Parentesco,
                                                    p.NivelEstudio,
                                                    p.Relacion
                                                    from CAT_RAP as r
                                                    left join CAT_PERSONA as p on r.PersonaId = p.IdPersona
                                                    left join CAT_MEDIDASPROTECCION as m on m.Victima
                                                    like p.Nombre+' '+p.ApellidoPaterno+' '+p.ApellidoMaterno+', %' 
                                                    OR m.Victima like '%, %'+p.Nombre+' '+p.ApellidoPaterno+' '+p.ApellidoMaterno
                                                    OR m.Victima like p.Nombre+' '+p.ApellidoPaterno+' '+p.ApellidoMaterno
                                                    OR m.Victima like '%, %'+p.Nombre+' '+p.ApellidoPaterno+' '+p.ApellidoMaterno+'%, %'
                                                    where r.RAtencionId = @ratencionid and m.RHechoId= @rhechoid and m.IdMProteccion =@idmproteccion";

            List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
            filtrosBusqueda.Add(new SqlParameter("@ratencionid", rAtencionId));
            filtrosBusqueda.Add(new SqlParameter("@rhechoid", RHechoId));
            filtrosBusqueda.Add(new SqlParameter("@idmproteccion", idMProteccion));

                var rap = await _context.qVictimaMedida.FromSqlRaw(busquedaVictimasM, filtrosBusqueda.ToArray()).ToListAsync();


            return Ok(rap.Select(a => new VictimasViewModelM
            {
                /*********************************************/
                /*CAT_RAP*/
                idRAP = a.IdRAP,
                RAtencionId = a.RAtencionId,
                PersonaId = a.PersonaId,
                ClasificacionPersona = a.ClasificacionPersona,
                /*********************************************/

                /*********************************************/
                /*CAT_PERSONA*/
                StatusAnonimo = a.StatusAnonimo,
                TipoPersona = a.TipoPersona,
                NombreC = a.NombreC,
                Nombre = a.Nombre,
                ApellidoPaterno = a.ApellidoPaterno,
                ApellidoMaterno = a.ApellidoMaterno,
                Alias = a.Alias,
                FechaNacimiento = a.FechaNacimiento,
                EntidadFederativa = a.EntidadFederativa,
                RFC = a.RFC,
                RazonSocial = a.RazonSocial,
                DocIdentificacion = a.DocIdentificacion,
                CURP = a.CURP,
                Sexo = a.Sexo,
                Genero = a.Genero,
                EstadoCivil = a.EstadoCivil,
                Telefono1 = a.Telefono1,
                Telefono2 = a.Telefono2,
                Correo = a.Correo,
                Medionotificacion = a.Medionotificacion,
                Nacionalidad = a.Nacionalidad,
                Ocupacion = a.Ocupacion,
                Lengua = a.Lengua,
                Religion = a.Religion,
                Discapacidad = a.Discapacidad,
                TipoDiscapacidad = a.TipoDiscapacidad,
                Parentesco = a.Parentesco,
                NivelEstudio = a.NivelEstudio,
                Relacion = a.Relacion

            }));

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }


        // GET: api/RAPs/ListarInputadosM
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rAtencionId}/{RHechoId}/{idMProteccion}")]
        public async Task<IActionResult> ListarInputadosM([FromRoute] Guid rAtencionId, Guid RHechoId, Guid idMProteccion)


        {

            try
            {
                String busquedaVictimasM = @"select 
                                                    CONCAT(p.Nombre,' ',p.ApellidoPaterno,' ',p.ApellidoMaterno)as NombreC, 
                                                    r.IdRAP,
                                                    r.RAtencionId,
                                                    r.PersonaId,
                                                    r.ClasificacionPersona,
                                                    p.StatusAnonimo,
                                                    p.TipoPersona,
                                                    p.Nombre,
                                                    p.ApellidoPaterno,
                                                    p.ApellidoMaterno,
                                                    p.Alias,
                                                    p.FechaNacimiento,
                                                    p.EntidadFederativa,
                                                    p.RFC,
                                                    p.RazonSocial,
                                                    p.DocIdentificacion,
                                                    p.CURP,
                                                    p.Sexo,
                                                    p.Genero,
                                                    p.EstadoCivil,
                                                    p.Telefono1,
                                                    p.Telefono2,
                                                    p.Correo,
                                                    p.Medionotificacion,
                                                    p.Nacionalidad,
                                                    p.Ocupacion,
                                                    p.Lengua,
                                                    p.Religion,
                                                    p.Discapacidad,
                                                    p.TipoDiscapacidad,
                                                    p.Parentesco,
                                                    p.NivelEstudio,
                                                    p.Relacion
                                                    from CAT_RAP as r
                                                    left join CAT_PERSONA as p on r.PersonaId = p.IdPersona
                                                    left join CAT_MEDIDASPROTECCION as m on m.Imputado
                                                    like p.Nombre+' '+p.ApellidoPaterno+' '+p.ApellidoMaterno+', %' 
                                                    OR m.Imputado like '%, %'+p.Nombre+' '+p.ApellidoPaterno+' '+p.ApellidoMaterno
                                                    OR m.Imputado like p.Nombre+' '+p.ApellidoPaterno+' '+p.ApellidoMaterno
                                                    OR m.Imputado like '%, %'+p.Nombre+' '+p.ApellidoPaterno+' '+p.ApellidoMaterno+'%, %'
                                                    where r.RAtencionId = @ratencionid and m.RHechoId= @rhechoid and m.IdMProteccion =@idmproteccion";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@ratencionid", rAtencionId));
                filtrosBusqueda.Add(new SqlParameter("@rhechoid", RHechoId));
                filtrosBusqueda.Add(new SqlParameter("@idmproteccion", idMProteccion));
                var rap = await _context.qVictimaMedida.FromSqlRaw(busquedaVictimasM, filtrosBusqueda.ToArray()).ToListAsync();


                return Ok(rap.Select(a => new VictimasViewModelM
                {
                    /*********************************************/
                    /*CAT_RAP*/
                    idRAP = a.IdRAP,
                    RAtencionId = a.RAtencionId,
                    PersonaId = a.PersonaId,
                    ClasificacionPersona = a.ClasificacionPersona,
                    /*********************************************/

                    /*********************************************/
                    /*CAT_PERSONA*/
                    StatusAnonimo = a.StatusAnonimo,
                    TipoPersona = a.TipoPersona,
                    NombreC = a.NombreC,
                    Nombre = a.Nombre,
                    ApellidoPaterno = a.ApellidoPaterno,
                    ApellidoMaterno = a.ApellidoMaterno,
                    Alias = a.Alias,
                    FechaNacimiento = a.FechaNacimiento,
                    EntidadFederativa = a.EntidadFederativa,
                    RFC = a.RFC,
                    RazonSocial = a.RazonSocial,
                    DocIdentificacion = a.DocIdentificacion,
                    CURP = a.CURP,
                    Sexo = a.Sexo,
                    Genero = a.Genero,
                    EstadoCivil = a.EstadoCivil,
                    Telefono1 = a.Telefono1,
                    Telefono2 = a.Telefono2,
                    Correo = a.Correo,
                    Medionotificacion = a.Medionotificacion,
                    Nacionalidad = a.Nacionalidad,
                    Ocupacion = a.Ocupacion,
                    Lengua = a.Lengua,
                    Religion = a.Religion,
                    Discapacidad = a.Discapacidad,
                    TipoDiscapacidad = a.TipoDiscapacidad,
                    Parentesco = a.Parentesco,
                    NivelEstudio = a.NivelEstudio,
                    Relacion = a.Relacion

                }));

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }
        //API PARA LISTARPOLICIAS
        // GET: api/RAPs/ListarPolicias
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IActionResult> ListarPolicias([FromRoute] Guid rAtencionId)


        {

            try
            {
                String busquedaVictimasM = @"select 
                                                    CONCAT(p.Nombre,' ',p.ApellidoPaterno,' ',p.ApellidoMaterno)as NombreC, 
                                                    r.IdRAP,
                                                    r.RAtencionId,
                                                    r.PersonaId,
                                                    r.ClasificacionPersona,
                                                    p.StatusAnonimo,
                                                    p.TipoPersona,
                                                    p.Nombre,
                                                    p.ApellidoPaterno,
                                                    p.ApellidoMaterno,
                                                    p.Alias,
                                                    p.FechaNacimiento,
                                                    p.EntidadFederativa,
                                                    p.RFC,
                                                    p.RazonSocial,
                                                    p.DocIdentificacion,
                                                    p.CURP,
                                                    p.Sexo,
                                                    p.Genero,
                                                    p.EstadoCivil,
                                                    p.Telefono1,
                                                    p.Telefono2,
                                                    p.Correo,
                                                    p.Medionotificacion,
                                                    p.Nacionalidad,
                                                    p.Ocupacion,
                                                    p.Lengua,
                                                    p.Religion,
                                                    p.Discapacidad,
                                                    p.TipoDiscapacidad,
                                                    p.Parentesco,
                                                    p.NivelEstudio,
                                                    p.Relacion
                                                    from CAT_RAP as r
                                                    left join CAT_PERSONA as p on r.PersonaId = p.IdPersona
                                                    left join CAT_RATENCON as a on a.IdRAtencion = r.RAtencionId
                                                    where a.IdRAtencion = @ratencionid and r.ClasificacionPersona = 'Oficial que detuvo'";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@ratencionid", rAtencionId));
                var rap = await _context.qVictimaMedida.FromSqlRaw(busquedaVictimasM, filtrosBusqueda.ToArray()).ToListAsync();


                return Ok(rap.Select(a => new VictimasViewModelM
                {
                    /*********************************************/
                    /*CAT_RAP*/
                    idRAP = a.IdRAP,
                    RAtencionId = a.RAtencionId,
                    PersonaId = a.PersonaId,
                    ClasificacionPersona = a.ClasificacionPersona,
                    /*********************************************/

                    /*********************************************/
                    /*CAT_PERSONA*/
                    StatusAnonimo = a.StatusAnonimo,
                    TipoPersona = a.TipoPersona,
                    NombreC = a.NombreC,
                    Nombre = a.Nombre,
                    ApellidoPaterno = a.ApellidoPaterno,
                    ApellidoMaterno = a.ApellidoMaterno,
                    Alias = a.Alias,
                    FechaNacimiento = a.FechaNacimiento,
                    EntidadFederativa = a.EntidadFederativa,
                    RFC = a.RFC,
                    RazonSocial = a.RazonSocial,
                    DocIdentificacion = a.DocIdentificacion,
                    CURP = a.CURP,
                    Sexo = a.Sexo,
                    Genero = a.Genero,
                    EstadoCivil = a.EstadoCivil,
                    Telefono1 = a.Telefono1,
                    Telefono2 = a.Telefono2,
                    Correo = a.Correo,
                    Medionotificacion = a.Medionotificacion,
                    Nacionalidad = a.Nacionalidad,
                    Ocupacion = a.Ocupacion,
                    Lengua = a.Lengua,
                    Religion = a.Religion,
                    Discapacidad = a.Discapacidad,
                    TipoDiscapacidad = a.TipoDiscapacidad,
                    Parentesco = a.Parentesco,
                    NivelEstudio = a.NivelEstudio,
                    Relacion = a.Relacion

                }));

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        //Api la listar todos los imputados
        // GET: api/RAPs/ListarTodosImp/id
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IEnumerable<VictimasViewModel>> ListarTodosImp([FromRoute] Guid rAtencionId)

        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Where(x => x.ClasificacionPersona == "Imputado")
                            .Where(x => x.RAtencionId == rAtencionId).ToListAsync();

            return rap.Select(a => new VictimasViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                idRAP = a.IdRAP,
                RAtencionId = a.RAtencionId,
                PersonaId = a.PersonaId,
                ClasificacionPersona = a.ClasificacionPersona,
                /*********************************************/

                /*********************************************/
                /*CAT_PERSONA*/
                StatusAnonimo = a.Persona.StatusAnonimo,
                TipoPersona = a.Persona.TipoPersona,
                NombreCompleto = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Nombre = a.Persona.Nombre,
                ApellidoPaterno = a.Persona.ApellidoPaterno,
                ApellidoMaterno = a.Persona.ApellidoMaterno,
                Alias = a.Persona.Alias,
                FechaNacimiento = a.Persona.FechaNacimiento,
                EntidadFederativa = a.Persona.EntidadFederativa,
                RFC = a.Persona.RFC,
                RazonSocial = a.Persona.RazonSocial,
                DocIdentificacion = a.Persona.DocIdentificacion,
                CURP = a.Persona.CURP,
                //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
                PoblacionAfro = (bool)(a.Persona.PoblacionAfro == null ? false : a.Persona.PoblacionAfro),
                RangoEdad = a.Persona.RangoEdad,
                RangoEdadTF = (bool)(a.Persona.RangoEdadTF == null ? false : a.Persona.RangoEdadTF),
                //--------------------------------------------------------------------------------------------------
                Sexo = a.Persona.Sexo,
                Genero = a.Persona.Genero,
                EstadoCivil = a.Persona.EstadoCivil,
                Telefono1 = a.Persona.Telefono1,
                Telefono2 = a.Persona.Telefono2,
                Correo = a.Persona.Correo,
                Medionotificacion = a.Persona.Medionotificacion,
                Nacionalidad = a.Persona.Nacionalidad,
                Ocupacion = a.Persona.Ocupacion,
                Lengua = a.Persona.Lengua,
                Religion = a.Persona.Religion,
                Discapacidad = a.Persona.Discapacidad,
                TipoDiscapacidad = a.Persona.TipoDiscapacidad,
                Parentesco = a.Persona.Parentesco,
                NivelEstudio = a.Persona.NivelEstudio,
                Edad = a.Persona.Edad

            });

        }

        // GET: api/RAPs/ListarIidyPInicio
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Director,Recepción")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IActionResult> ListarIidyPInicio([FromRoute] Guid rAtencionId)
        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Include(a => a.RAtencion)
                            .Include(a => a.RAtencion.RACs)
                            .Where( x => x.PInicio == true)
                            .Where(x => x.RAtencionId == rAtencionId).FirstOrDefaultAsync();

            if (rap == null)
            {
                return NotFound("No hay registros");

            }

            return Ok(new RAPViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                IdRAP = rap.IdRAP,
                RAtencionId = rap.RAtencionId,
                PersonaId = rap.PersonaId,
                ClasificacionPersona = rap.ClasificacionPersona,
                /*********************************************/
                /*CAT_RATENCION*/
                FechaHoraInicio = rap.RAtencion.FechaHoraRegistro,
                u_Nombre = rap.RAtencion.u_Nombre,
                u_Puesto = rap.RAtencion.u_Puesto,
                u_Modulo = rap.RAtencion.u_Modulo,
                DistritoInicial = rap.RAtencion.DistritoInicial,
                DirSubProcuInicial = rap.RAtencion.DirSubProcuInicial,
                AgenciaInicial = rap.RAtencion.AgenciaInicial,
                StatusAtencion = rap.RAtencion.StatusAtencion,
                StatusRegistro = rap.RAtencion.StatusRegistro,
                MedioDenuncia = rap.RAtencion.MedioDenuncia,
                Rac = rap.RAtencion.RACs.racg,
                ContencionVictima = rap.RAtencion.ContencionVicitma,
                Numerooficio = rap.RAtencion.ModuloServicio,
                /*********************************************/
                /*CAT_PERSONA*/

                TipoPersona = rap.Persona.TipoPersona,
                Nombre = rap.Persona.Nombre,
                ApellidoPaterno = rap.Persona.ApellidoPaterno,
                ApellidoMaterno = rap.Persona.ApellidoMaterno,
                Alias = rap.Persona.Alias,
                FechaNacimiento = rap.Persona.FechaNacimiento,
                EntidadFederativa = rap.Persona.EntidadFederativa,
                RFC = rap.Persona.RFC,
                RazonSocial = rap.Persona.RazonSocial,
                DocIdentificacion = rap.Persona.DocIdentificacion,
                CURP = rap.Persona.CURP,
                //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
                PoblacionAfro = (bool)(rap.Persona.PoblacionAfro == null ? false : rap.Persona.PoblacionAfro),
                RangoEdad = rap.Persona.RangoEdad,
                RangoEdadTF = (bool)(rap.Persona.RangoEdadTF == null ? false : rap.Persona.RangoEdadTF),
                //--------------------------------------------------------------------------------------------------
                Sexo = rap.Persona.Sexo,
                Genero = rap.Persona.Genero,
                EstadoCivil = rap.Persona.EstadoCivil,
                Telefono1 = rap.Persona.Telefono1,
                Telefono2 = rap.Persona.Telefono2,
                Correo = rap.Persona.Correo,
                Medionotificacion = rap.Persona.Medionotificacion,
                Nacionalidad = rap.Persona.Nacionalidad,
                Ocupacion = rap.Persona.Ocupacion,
                Lengua = rap.Persona.Lengua,
                Religion = rap.Persona.Religion,
                Discapacidad = rap.Persona.Discapacidad,
                TipoDiscapacidad = rap.Persona.TipoDiscapacidad,
                Parentesco = rap.Persona.Parentesco,
                NivelEstudio = rap.Persona.NivelEstudio,
                /*********************************************/
                /*CAT_DIRECCIONPERSONA*/
                /*********************************************/

            });

        }

        //Api para listar Victima directa nombre junto con su direccion de escucha
        // GET: api/RAPs/ListarVicDE
        [HttpGet("[action]/{IdRatencion}")]
        public async Task <IEnumerable<DENombreViewModel>>ListarVicDE([FromRoute] Guid IdRatencion)

        {
            var den = await _context.DireccionEscuchas
                            .Where(a => a.RAP.RAtencionId == IdRatencion)
                            .Where(a => a.RAP.ClasificacionPersona == "Victima directa")
                            .Include(a => a.RAP.Persona)
                            .ToListAsync();

            var tiposVialidades = await _context.Vialidades.ToListAsync();

            return den.Select(a => new DENombreViewModel
            {
                DireccionE = $"{(a.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(v => v.Clave == a.TipoVialidad)?.Nombre + " " : "")}" +
                             $"{a.Calle} {a.NoExt} {a.NoInt} {a.Localidad} {a.CP} {a.Municipio} {a.Estado}",
                NombreCompleto = a.RAP.Persona.Nombre + " " + a.RAP.Persona.ApellidoPaterno + " " + a.RAP.Persona.ApellidoMaterno,
                PersonaId = a.RAP.PersonaId,
                Apaterno = a.RAP.Persona.ApellidoPaterno,
                Amaterno = a.RAP.Persona.ApellidoMaterno,
                Edad = a.RAP.Persona.Edad,
                Alias = a.RAP.Persona.Alias,
                TipoVialidad = a.TipoVialidad,
            });

        }

        //Api para listar imputado nombre junto con su direccion de escucha
        // GET: api/RAPs/ListarImpDE
        [HttpGet("[action]/{IdRatencion}")]
        public async Task<IEnumerable<DENombreViewModel>> ListarImpDE([FromRoute] Guid IdRatencion)

        {
            var den = await _context.DireccionEscuchas
                            .Where(a => a.RAP.RAtencionId == IdRatencion)
                            .Where(a => a.RAP.ClasificacionPersona == "Imputado")
                            .Include(a => a.RAP.Persona)
                            .ToListAsync();

            var tiposVialidades = await _context.Vialidades.ToListAsync();

            return den.Select(a => new DENombreViewModel
            {
                DireccionE = $"{(a.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(v => v.Clave == a.TipoVialidad)?.Nombre + " ": "")}" +
                             $"{a.Calle} {a.NoInt} {a.Localidad} {a.CP} {a.Municipio} {a.Estado}",
                NombreCompleto = a.RAP.Persona.Nombre + " " + a.RAP.Persona.ApellidoPaterno + " " + a.RAP.Persona.ApellidoMaterno,
                PersonaId = a.RAP.PersonaId,
                FechaNacimiento = a.RAP.Persona.FechaNacimiento,
                CURP = a.RAP.Persona.CURP,
                Telefono = a.RAP.Persona.Telefono1,
                Alias = a.RAP.Persona.Alias,
                Correo = a.RAP.Persona.Correo,
                Genero = a.RAP.Persona.Sexo,
                TipoVialidad = a.TipoVialidad,
            });

        }


        // GET: api/RAPs/ListarPersonasconIdEscucha
        [HttpGet("[action]/{IdRatencion}")]
        public async Task<IEnumerable<DEPersonaViewModel>> ListarPersonasconIdEscucha([FromRoute] Guid IdRatencion)

        {
            var den = await _context.DireccionEscuchas
                            .Where(a => a.RAP.RAtencionId == IdRatencion)
                            .Include(a => a.RAP.Persona)
                            .ToListAsync();

            return den.Select(a => new DEPersonaViewModel
            {
                
                NombreCompleto = a.RAP.Persona.Nombre + " " + a.RAP.Persona.ApellidoPaterno + " " + a.RAP.Persona.ApellidoMaterno,
                PersonaId = a.RAP.PersonaId,
                ClasificacionPersona = a.RAP.ClasificacionPersona,
                Curp = a.RAP.Persona.CURP,
                IdDEscucha = a.IdDEscucha

            });

        }

        // PUT: api/RAPs/ActualizarClasificacion
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Director, Coordinador, Jurídico,  Facilitador, Notificador,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarClasificacion([FromBody] ActualizarClasificacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rap = await _context.RAPs.FirstOrDefaultAsync(a => a.IdRAP == model.IdRAP);

            if (rap == null)
            {
                return NotFound();
            }

            rap.ClasificacionPersona = model.ClasificacionPersona;

            var persona = await _context.Personas.FirstOrDefaultAsync(a => a.IdPersona == model.PersonaId);

            if (persona == null)
            {
                return NotFound();
            }
            persona.Registro = model.Registro;
            persona.VerI = model.VerI;
            persona.VerR = model.VerR;
            persona.Parentesco = model.Parentesco;

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

            return Ok();
        }

        // PUT: api/RAPs/ActualizarClasificacion
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, Director, Coordinador, Jurídico,  Facilitador, Notificador,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarClasificacionD([FromBody] ActualizarClasificacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rap = await _context.RAPs.FirstOrDefaultAsync(a => a.IdRAP == model.IdRAP);

            if (rap == null)
            {
                return NotFound();
            }

            rap.ClasificacionPersona = model.ClasificacionPersona;

            var persona = await _context.Personas.FirstOrDefaultAsync(a => a.IdPersona == model.PersonaId);

            if (persona == null)
            {
                return NotFound();
            }
            persona.Registro = model.Registro;
            persona.VerI = model.VerI;
            persona.VerR = model.VerR;
            persona.Parentesco = model.Parentesco;

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

            return Ok();
        }

        // GET: api/RAPs/ListarRatencionPinicio
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Director,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IActionResult> ListarRatencionPinicio([FromRoute] Guid rAtencionId)
        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Include(a => a.RAtencion)
                            .Include(a => a.RAtencion.RACs)
                            .Where(x => x.RAtencionId == rAtencionId)
                            .Where(x => x.PInicio == true)
                            .FirstOrDefaultAsync();

            if (rap == null)
            {
                return NotFound("No hay registros");

            }

            return Ok(new RAPViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                IdRAP = rap.IdRAP,
                RAtencionId = rap.RAtencionId,
                PersonaId = rap.PersonaId,
                ClasificacionPersona = rap.ClasificacionPersona,
                /*********************************************/
                /*CAT_RATENCION*/
                FechaHoraInicio = rap.RAtencion.FechaHoraRegistro,
                u_Nombre = rap.RAtencion.u_Nombre,
                u_Puesto = rap.RAtencion.u_Puesto,
                u_Modulo = rap.RAtencion.u_Modulo,
                DistritoInicial = rap.RAtencion.DistritoInicial,
                DirSubProcuInicial = rap.RAtencion.DirSubProcuInicial,
                AgenciaInicial = rap.RAtencion.AgenciaInicial,
                StatusAtencion = rap.RAtencion.StatusAtencion,
                StatusRegistro = rap.RAtencion.StatusRegistro,
                MedioDenuncia = rap.RAtencion.MedioDenuncia,
                Rac = rap.RAtencion.RACs.racg,
                ContencionVictima = rap.RAtencion.ContencionVicitma,
                Numerooficio = rap.RAtencion.ModuloServicio,
                /*********************************************/
                /*CAT_PERSONA*/

                TipoPersona = rap.Persona.TipoPersona,
                Nombre = rap.Persona.Nombre,
                ApellidoPaterno = rap.Persona.ApellidoPaterno,
                ApellidoMaterno = rap.Persona.ApellidoMaterno,
                Alias = rap.Persona.Alias,
                FechaNacimiento = rap.Persona.FechaNacimiento,
                EntidadFederativa = rap.Persona.EntidadFederativa,
                RFC = rap.Persona.RFC,
                RazonSocial = rap.Persona.RazonSocial,
                DocIdentificacion = rap.Persona.DocIdentificacion,
                CURP = rap.Persona.CURP,
                //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
                PoblacionAfro = (bool)(rap.Persona.PoblacionAfro == null ? false : rap.Persona.PoblacionAfro),
                RangoEdad = rap.Persona.RangoEdad,
                RangoEdadTF = (bool)(rap.Persona.RangoEdadTF == null ? false : rap.Persona.RangoEdadTF),
                //--------------------------------------------------------------------------------------------------
                Sexo = rap.Persona.Sexo,
                Genero = rap.Persona.Genero,
                EstadoCivil = rap.Persona.EstadoCivil,
                Telefono1 = rap.Persona.Telefono1,
                Telefono2 = rap.Persona.Telefono2,
                Correo = rap.Persona.Correo,
                Medionotificacion = rap.Persona.Medionotificacion,
                Nacionalidad = rap.Persona.Nacionalidad,
                Ocupacion = rap.Persona.Ocupacion,
                Lengua = rap.Persona.Lengua,
                Religion = rap.Persona.Religion,
                Discapacidad = rap.Persona.Discapacidad,
                TipoDiscapacidad = rap.Persona.TipoDiscapacidad,
                Parentesco = rap.Persona.Parentesco,
                NivelEstudio = rap.Persona.NivelEstudio,
                /*********************************************/
                /*CAT_DIRECCIONPERSONA*/
                /*********************************************/

            });

        }


        // POST: api/RAPs/CrearImputado
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearImputado(CrearVicitmaViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid personaid;
            Guid idrap;
            try
            {
                Persona InsertarPersona = new Persona
                {
                    StatusAnonimo = model.StatusAnonimo,
                    TipoPersona = model.TipoPersona,
                    RFC = model.RFC,
                    RazonSocial = model.RazonSocial,
                    Nombre = model.Nombre,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = model.ApellidoMaterno,
                    Alias = model.Alias,
                    StatusAlias = model.StatusAlias,
                    FechaNacimiento = model.FechaNacimiento,
                    EntidadFederativa = model.EntidadFederativa,
                    DocIdentificacion = model.DocIdentificacion,
                    CURP = model.CURP,
                    //AGREGACIONES NUEVAS QUE PUEDEN NO ESTAN EN TODOS LADOS
                    PoblacionAfro = model.PoblacionAfro,
                    RangoEdad = model.RangoEdad,
                    RangoEdadTF = model.RangoEdadTF,
                    //------------------------------------------+
                    Sexo = model.Sexo,
                    Genero = model.Genero,
                    EstadoCivil = model.EstadoCivil,
                    Telefono1 = model.Telefono1,
                    Telefono2 = model.Telefono2,
                    Correo = model.Correo,
                    Medionotificacion = model.Medionotificacion,
                    Nacionalidad = model.Nacionalidad,
                    Ocupacion = model.Ocupacion,
                    NivelEstudio = model.NivelEstudio,
                    Lengua = model.Lengua,
                    Religion = model.Religion,
                    Discapacidad = model.Discapacidad,
                    TipoDiscapacidad = model.TipoDiscapacidad,
                    DatosProtegidos = model.DatosProtegidos,
                    Edad = model.Edad,
                    DocPoderNotarial = model.DocPoderNotarial,
                    InicioDetenido = model.InicioDetenido


                };

                _context.Personas.Add(InsertarPersona);


                var idRA = model.RAtencionId;
                var idP = InsertarPersona.IdPersona;

                DireccionPersonal InsertarDP = new DireccionPersonal
                {

                    Calle = model.Calle,
                    NoExt = model.NoExt,
                    NoInt = model.NoInt,
                    EntreCalle1 = model.EntreCalle1,
                    EntreCalle2 = model.EntreCalle2,
                    Referencia = model.Referencia,
                    Pais = model.Pais,
                    Estado = model.Estado,
                    Municipio = model.Municipio,
                    Localidad = model.Localidad,
                    CP = model.CP,
                    lat = model.lat,
                    lng = model.lng,
                    PersonaId = idP,
                    TipoVialidad = model.tipoVialidad,
                    TipoAsentamiento = model.tipoAsentamiento,
                };

                _context.DireccionPersonals.Add(InsertarDP);



                RAP InsertarRAP = new RAP
                {
                    RAtencionId = idRA,
                    PersonaId = idP,
                    ClasificacionPersona = model.ClasificacionPersona,
                    PInicio = model.PInicio,
                };

                _context.RAPs.Add(InsertarRAP);

                //********************************************************************** 

                await _context.SaveChangesAsync();
                personaid = InsertarPersona.IdPersona;
                idrap = InsertarRAP.IdRAP;
                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }
            return Ok(new { personaid = personaid, idrap = idrap });


        }


        // POST: api/RAPs/CrearPolicia
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearPolicia(CrearVicitmaViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid personaid;
            Guid idrap;
            var idPoli = model.PoliciaDetuvo;
            try
            {
                Persona InsertarPersona = new Persona
                {
                    StatusAnonimo = false,
                    TipoPersona = "Fisica",
                    RFC = "",
                    RazonSocial = "",
                    Nombre = model.Nombre,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = model.ApellidoMaterno,
                    Alias = "",
                    StatusAlias = false,
                    FechaNacimiento = "",
                    EntidadFederativa = "",
                    DocIdentificacion = "",
                    CURP = "",
                    Sexo = "",
                    Genero = "",
                    EstadoCivil = "",
                    Telefono1 = "",
                    Telefono2 = "",
                    Correo = "",
                    Medionotificacion = "",
                    Nacionalidad = "",
                    Ocupacion = "",
                    NivelEstudio = "",
                    Lengua = "",
                    Religion = "",
                    Discapacidad = false,
                    TipoDiscapacidad = "",
                    DatosProtegidos = false,
                    InstitutoPolicial = model.InstitutoPolicial,
                    InformePolicial = model.InformePolicial,
                    PoliciaDetuvo = idPoli



                };

                _context.Personas.Add(InsertarPersona);

                //********************************************************************** 

                var idRA = model.RAtencionId;
                var idP = InsertarPersona.IdPersona;


                RAP InsertarRAP = new RAP
                {
                    RAtencionId = idRA,
                    PersonaId = idP,
                    ClasificacionPersona = "Oficial que detuvo",
                    PInicio = false,
                };

                _context.RAPs.Add(InsertarRAP);

                //********************************************************************** 

                await _context.SaveChangesAsync();
                personaid = InsertarPersona.IdPersona;
                idrap = InsertarRAP.IdRAP;
                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }
            return Ok();


        }

        //API PARA CREAR POLICIA Y RELACIONARLO
        // POST: api/RAPs/CrearPoliciaEdit
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearPoliciaEdit(CrearVicitmaViewModelPoli model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid personaid;
            Guid idrap;
            var idPoli = model.PoliciaDetuvo;
            try
            {
                Persona InsertarPersona = new Persona
                {
                    StatusAnonimo = false,
                    TipoPersona = "Fisica",
                    RFC = "",
                    RazonSocial = "",
                    Nombre = model.Nombre,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = model.ApellidoMaterno,
                    Alias = "",
                    StatusAlias = false,
                    FechaNacimiento = "",
                    EntidadFederativa = "",
                    DocIdentificacion = "",
                    CURP = "",
                    Sexo = "",
                    Genero = "",
                    EstadoCivil = "",
                    Telefono1 = "",
                    Telefono2 = "",
                    Correo = "",
                    Medionotificacion = "",
                    Nacionalidad = "",
                    Ocupacion = "",
                    NivelEstudio = "",
                    Lengua = "",
                    Religion = "",
                    Discapacidad = false,
                    TipoDiscapacidad = "",
                    DatosProtegidos = false,
                    InstitutoPolicial = model.InstitutoPolicial,
                    InformePolicial = model.InformePolicial,
                    PoliciaDetuvo = idPoli



                };

                _context.Personas.Add(InsertarPersona);

                await _context.SaveChangesAsync();

                //********************************************************************** 

                var idRA = model.RAtencionId;
                var idP = InsertarPersona.IdPersona;


                RAP InsertarRAP = new RAP
                {
                    RAtencionId = idRA,
                    PersonaId = idP,
                    ClasificacionPersona = "Oficial que detuvo",
                    PInicio = false,
                };

                _context.RAPs.Add(InsertarRAP);

                await _context.SaveChangesAsync();

                //**********************************************************************

                var relaPoliImput = await _context.Personas.FirstOrDefaultAsync(a => a.IdPersona == model.PersonaId);

                if (relaPoliImput == null)
                {
                    return NotFound();
                }

                relaPoliImput.PoliciaDetuvo = idP;


                //********************************************************************** 

                await _context.SaveChangesAsync();
                personaid = InsertarPersona.IdPersona;
                idrap = InsertarRAP.IdRAP;
                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }
            return Ok();


        }

        // GET: api/RAPs/ListarVictimasEstadistica
        [Authorize(Roles = "Administrador, Recepción,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{rAtencionId}/{nombre}/{paterno}/{materno}/{ocupacion}/{sexo}/{nacionalidad}/{edad}/{anos}/{deanos}/{aanos}/{relacion}/{relacionado}")]
        public async Task<IEnumerable<EstadisticaViewModel>> ListarVictimasEstadistica([FromRoute] Guid rAtencionId, string nombre, string paterno, string materno, string ocupacion, string sexo, string nacionalidad, string edad, int anos, int deanos, int aanos, string relacion, string relacionado)

        {


            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Where(x => x.RAtencionId == rAtencionId)
                            .Where(x => x.ClasificacionPersona == "Victima directa" || x.ClasificacionPersona == "Victima indirecta")
                            .Where(a => (nombre != "ZKR" ? a.Persona.Nombre == nombre : a.Persona.Nombre != nombre))
                            .Where(a => (paterno != "ZKR" ? a.Persona.ApellidoPaterno == paterno : a.Persona.ApellidoPaterno != paterno))
                            .Where(a => (materno != "ZKR" ? a.Persona.ApellidoMaterno == materno : a.Persona.ApellidoMaterno != materno))
                            .Where(a => (ocupacion != "ZKR" ? a.Persona.Ocupacion == ocupacion : a.Persona.Ocupacion != ocupacion))
                            .Where(a => (sexo != "ZKR" ? a.Persona.Sexo == sexo : a.Persona.Sexo != sexo))
                            .Where(a => (nacionalidad != "ZKR" ? a.Persona.Nacionalidad == nacionalidad : a.Persona.Nacionalidad != nacionalidad))
                            .Where(a => (edad == "Si(númerica)" ? a.Persona.Edad == anos : edad == "Rango de edad" ? a.Persona.Edad >= deanos && a.Persona.Edad <= aanos : a.Persona.Edad > -1))
                            .Where(a => (relacion == "Si" && relacionado =="ZKR" ? a.Persona.Relacion == true: relacion == "Si" && relacionado != "ZKR" ? a.Persona.Relacion == true && a.Persona.Parentesco == relacionado : relacion =="No" ? a.Persona.Relacion == false : a.Persona.Relacion == true || a.Persona.Relacion == false))
                            .GroupBy(v => v.RAtencionId)
                            .Select(x => new { etiqueta = x.Key, valor1 = x.Count(v => v.RAtencionId == x.Key) })
                            .ToListAsync();


            return rap.Select(a => new EstadisticaViewModel
            {

                Cantidad = a.valor1,
                ratencion = a.etiqueta

            });

        }

        // GET: api/RAPs/ListarImputadosEstadistica
        [Authorize(Roles = "Administrador, Recepción,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{rAtencionId}/{nombre}/{paterno}/{materno}/{ocupacion}/{sexo}/{nacionalidad}/{edad}/{anos}/{deanos}/{aanos}")]
        public async Task<IEnumerable<EstadisticaViewModel>> ListarImputadosEstadistica([FromRoute] Guid rAtencionId, string nombre, string paterno, string materno, string ocupacion, string sexo, string nacionalidad, string edad, int anos, int deanos, int aanos)

        {


            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Where(x => x.RAtencionId == rAtencionId)
                            .Where(x => x.ClasificacionPersona == "Imputado")
                            .Where(a => (nombre != "ZKR" ? a.Persona.Nombre == nombre : a.Persona.Nombre != nombre))
                            .Where(a => (paterno != "ZKR" ? a.Persona.ApellidoPaterno == paterno : a.Persona.ApellidoPaterno != paterno))
                            .Where(a => (materno != "ZKR" ? a.Persona.ApellidoMaterno == materno : a.Persona.ApellidoMaterno != materno))
                            .Where(a => (ocupacion != "ZKR" ? a.Persona.Ocupacion == ocupacion : a.Persona.Ocupacion != ocupacion))
                            .Where(a => (sexo != "ZKR" ? a.Persona.Sexo == sexo : a.Persona.Sexo != sexo))
                            .Where(a => (nacionalidad != "ZKR" ? a.Persona.Nacionalidad == nacionalidad : a.Persona.Nacionalidad != nacionalidad))
                            .Where(a => (edad == "Si(númerica)" ? a.Persona.Edad == anos : edad == "Rango de edad" ? a.Persona.Edad >= deanos && a.Persona.Edad <= aanos : a.Persona.Edad > -1))
                            .GroupBy(v => v.RAtencionId)
                            .Select(x => new { etiqueta = x.Key, valor1 = x.Count(v => v.RAtencionId == x.Key) })
                            .ToListAsync();


            return rap.Select(a => new EstadisticaViewModel
            {

                Cantidad = a.valor1,
                ratencion = a.etiqueta

            });

        }


        // PUT: api/RAPs/ActualizarDAtosFalsos
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Director, Coordinador, Jurídico,  Facilitador, Notificador,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarDAtosFalsos([FromBody] ActualizarDatosFalsosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rap = await _context.Personas.FirstOrDefaultAsync(a => a.IdPersona == model.IdPersona);

            if (rap == null)
            {
                return NotFound();
            }

            rap.DatosFalsos = model.DatosFalsos;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }


        // GET: api/RAPs/BusquedaporNombre
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{Indicadoragencia}/{idAgencia}/{IndicadorModulo}/{idModuloServicio}/{IddspU}/{IdAgenciaU}/{rol}/{nombre}/{apellidop}/{apellidom}/{nuc}")]
        public async Task<IEnumerable<BusquedaNombreViewModel>> BusquedaporNombre([FromRoute] Boolean Indicadoragencia, Guid idAgencia, Boolean IndicadorModulo, Guid idModuloServicio, Guid IddspU, Guid IdAgenciaU,string rol, string nombre, string apellidop, string apellidom, string nuc)
        {

            var personas = await _context.RAPs
                          .Include(a => a.Persona)    
                          .Where(a => a.PInicio)
                          .Where(a => nombre != "ZKR" ? a.Persona.Nombre == nombre : 1 == 1)
                          .Where(a => apellidop != "ZKR" ? a.Persona.ApellidoPaterno == apellidop : 1 == 1)
                          .Where(a => apellidom != "ZKR" ? a.Persona.ApellidoMaterno == apellidom : 1 == 1)
                          .ToListAsync();

            var personasf = personas.Select(a => new BusquedaNombreViewModel
            {
                RAtencionId = a.RAtencionId,
                Persona = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno
            });

            IEnumerable<BusquedaNombreViewModel> items = new BusquedaNombreViewModel[] { };

            foreach (var personaf in personasf)
            {
                var carpeta = await _context.RHechoes
                    .Include(a => a.NUCs)
                    .Include(a => a.Agencia)
                    .Include(a => a.ModuloServicio)
                    .Where(a => nuc != "ZKR" ? a.NUCs.nucg == nuc : 1 == 1)
                    .Where(a => a.NucId != null)
                    .Where(a => a.RAtencionId == personaf.RAtencionId)
                    .Where(a => Indicadoragencia ? a.Agenciaid == idAgencia : rol == "AD" ? a.Agencia.DSPId == IddspU : 1 == 1)
                    .Where(a => IndicadorModulo ? a.ModuloServicioId == idModuloServicio : rol == "C" ? a.ModuloServicio.AgenciaId == IdAgenciaU : 1 == 1)
                    .FirstOrDefaultAsync();

                if (carpeta != null)
                {

                    IEnumerable<BusquedaNombreViewModel> ReadLines()
                    {
                        IEnumerable<BusquedaNombreViewModel> item2;

                        item2 = (new[]{new BusquedaNombreViewModel{
                        RAtencionId = personaf.RAtencionId,
                        Persona = personaf.Persona,
                        Nuc = carpeta.NUCs.nucg,
                        Agencia = carpeta.Agencia.Nombre,
                        Modulo = carpeta.ModuloServicio.Nombre,
                    }});

                        return item2;
                    }

                    items = items.Concat(ReadLines());


                }

            }

            return items;

        }


        //Api para listar imputado nombre junto con su direccion de escucha,direccion personal y representante particular
        // GET: api/RAPs/ListarImpDEDPRE
        [HttpGet("[action]/{IdRatencion}")]
        public async Task<IEnumerable<ImputadoDEDPREL>> ListarImpDEDPRE([FromRoute] Guid IdRatencion)

        {
            var DEIM = await _context.DireccionEscuchas
                            .Where(a => a.RAP.RAtencionId == IdRatencion)
                            .Where(a => a.RAP.ClasificacionPersona == "Imputado")
                            .Include(a => a.RAP.Persona)
                            .ToListAsync();

            var tiposVialidades = await _context.Vialidades.ToListAsync();

            var DEIMF =  DEIM.Select(a => new ImputadoDEDPREL
            {
                DireccionE = $"{(a.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(v => v.Clave == a.TipoVialidad)?.Nombre + " " : "")}" +
                             $"{a.Calle} {a.NoInt} {a.Localidad} {a.CP} {a.Municipio} {a.Estado}",
                NombreImputado = a.RAP.Persona.Nombre + " " + a.RAP.Persona.ApellidoPaterno + " " + a.RAP.Persona.ApellidoMaterno,
                PersonaId = a.RAP.PersonaId,
                FechaNacimiento = a.RAP.Persona.FechaNacimiento,
                CURP = a.RAP.Persona.CURP,
                Telefono = a.RAP.Persona.Telefono1,
                Alias = a.RAP.Persona.Alias,
                Correo = a.RAP.Persona.Correo,

            });


            IEnumerable<ImputadoDEDPREL> items = new ImputadoDEDPREL[] { };

            foreach (var DEIMR in DEIMF)
            {
                var representante = await _context.Representantes
                    .Where(a => a.PersonaId == DEIMR.PersonaId)
                    .Where(a => a.Tipo1 == 3 || a.Tipo2 == 3)
                    .FirstOrDefaultAsync();

                string nombrepresentante ="", telefonore = "", correore = "", direccionRepresentante = "";

                if(representante != null)
                {
                    nombrepresentante = representante.Nombres + " " + representante.ApellidoPa + " " + representante.ApellidoMa;
                    telefonore = representante.Telefono;
                    correore = representante.CorreoElectronico;
                    direccionRepresentante = $"{(representante.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(d => d.Clave == representante.TipoVialidad)?.Nombre + " " : "")}" +
                                 $"{representante.Calle} {representante.NoExt} {representante.NoInt} {representante.Localidad} {representante.CP} {representante.Municipio} {representante.Estado}";
                }


                IEnumerable<ImputadoDEDPREL> ReadLines()
                {
                    IEnumerable<ImputadoDEDPREL> item2;

                    item2 = (new[]{new ImputadoDEDPREL{
                    DireccionE = DEIMR.DireccionE,
                    NombreImputado = DEIMR.NombreImputado,
                    PersonaId = DEIMR.PersonaId,
                    FechaNacimiento = DEIMR.FechaNacimiento,
                    CURP = DEIMR.CURP,
                    Telefono = DEIMR.Telefono,
                    Alias = DEIMR.Alias,
                    Correo = DEIMR.Correo,
                    NombreRepresentanteParticular = nombrepresentante,
                    DireccionRepresentante = direccionRepresentante,
                    TelefonoRe = telefonore,
                    CorreoRe = correore,
                    RepresentanteActivo = representante != null,

                }});

                    return item2;
                }

                items = items.Concat(ReadLines());

            }

            IEnumerable<ImputadoDEDPREL> items2 = new ImputadoDEDPREL[] { };


            foreach (var item in items)
            {
                var direccionp = await _context.DireccionPersonals
                    .Where(a => a.PersonaId == item.PersonaId)
                    .FirstOrDefaultAsync();

                IEnumerable<ImputadoDEDPREL> ReadLines()
                {
                    IEnumerable<ImputadoDEDPREL> item2;

                    item2 = (new[]{new ImputadoDEDPREL{
                    DireccionE = item.DireccionE,
                    NombreImputado = item.NombreImputado,
                    PersonaId = item.PersonaId,
                    FechaNacimiento = item.FechaNacimiento,
                    CURP = item.CURP,
                    Telefono = item.Telefono,
                    Alias = item.Alias,
                    Correo = item.Correo,
                    NombreRepresentanteParticular = item.NombreRepresentanteParticular,
                    DireccionRepresentante = item.DireccionRepresentante,
                    TelefonoRe = item.TelefonoRe,
                    CorreoRe = item.CorreoRe,
                    RepresentanteActivo = item.RepresentanteActivo,
                    DireccionP = $"{(direccionp.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(c => c.Clave == direccionp.TipoVialidad)?.Nombre + " ": "")}" +
                                 $"{direccionp.Calle} {direccionp.NoInt} {direccionp.Localidad} {direccionp.CP} {direccionp.Municipio} {direccionp.Estado}",
                }});

                    return item2;
                }

                items2 = items2.Concat(ReadLines());

            }

            return items2;

        }


        //Api para listar victima directa junto con su direccion de escucha,direccion personal , representante legal y juridico
        // GET: api/RAPs/ListarVicDEDPRE
        [HttpGet("[action]/{IdRatencion}")]
        public async Task<IEnumerable<VictimaDEDPRLRJ>> ListarVicDEDPRE([FromRoute] Guid IdRatencion)

        {
            var DEIM = await _context.DireccionEscuchas
                            .Where(a => a.RAP.RAtencionId == IdRatencion)
                            .Where(a => a.RAP.ClasificacionPersona == "Victima directa")
                            .Include(a => a.RAP.Persona)
                            .ToListAsync();

            var tiposVialidades = await _context.Vialidades.ToListAsync();

            var DEIMF = DEIM.Select(a => new VictimaDEDPRLRJ
            {
                DireccionE = $"{(a.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(v => v.Clave == a.TipoVialidad)?.Nombre + " " : "")}" +
                             $"{a.Calle} {a.NoInt} {a.Localidad} {a.CP} {a.Municipio} {a.Estado}",
                NombreVictima = a.RAP.Persona.Nombre + " " + a.RAP.Persona.ApellidoPaterno + " " + a.RAP.Persona.ApellidoMaterno,
                PersonaId = a.RAP.PersonaId,
                FechaNacimiento = a.RAP.Persona.FechaNacimiento,
                CURP = a.RAP.Persona.CURP,
                Telefono = a.RAP.Persona.Telefono1,
                Alias = a.RAP.Persona.Alias,
                Correo = a.RAP.Persona.Correo
            });


            IEnumerable<VictimaDEDPRLRJ> items = new VictimaDEDPRLRJ[] { };

            foreach (var DEIMR in DEIMF)
            {
                var representante = await _context.Representantes
                    .Where(a => a.PersonaId == DEIMR.PersonaId)
                    .Where(a => a.Tipo1 == 1 || a.Tipo2 == 1)
                    .FirstOrDefaultAsync();

                string nombrepresentante = "", telefonore = "", correore = "", direccionRepresentante = "";

                if (representante != null)
                {
                    nombrepresentante = representante.Nombres + " " + representante.ApellidoPa + " " + representante.ApellidoMa;
                    telefonore = representante.Telefono;
                    correore = representante.CorreoElectronico;
                    direccionRepresentante = $"{(representante.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(d => d.Clave == representante.TipoVialidad)?.Nombre + " " : "")}" +
                                 $"{representante.Calle} {representante.NoExt} {representante.NoInt} {representante.Localidad} {representante.CP} {representante.Municipio} {representante.Estado}";
                }


                IEnumerable<VictimaDEDPRLRJ> ReadLines()
                {
                    IEnumerable<VictimaDEDPRLRJ> item2;

                    item2 = (new[]{new VictimaDEDPRLRJ{
                    DireccionE = DEIMR.DireccionE,
                    NombreVictima = DEIMR.NombreVictima,
                    PersonaId = DEIMR.PersonaId,
                    FechaNacimiento = DEIMR.FechaNacimiento,
                    CURP = DEIMR.CURP,
                    Telefono = DEIMR.Telefono,
                    Alias = DEIMR.Alias,
                    Correo = DEIMR.Correo,
                    NombreRepresentanteLegal = nombrepresentante,
                    DireccionRepresentante = direccionRepresentante,
                    TelefonoReL = telefonore,
                    CorreoReL = correore,
                    RepresentanteActivo = representante != null,

                }});

                    return item2;
                }

                items = items.Concat(ReadLines());

            }

            IEnumerable<VictimaDEDPRLRJ> items2 = new VictimaDEDPRLRJ[] { };


            foreach (var item in items)
            {
                var direccionp = await _context.DireccionPersonals
                    .Where(a => a.PersonaId == item.PersonaId)
                    .FirstOrDefaultAsync();

                IEnumerable<VictimaDEDPRLRJ> ReadLines()
                {
                    IEnumerable<VictimaDEDPRLRJ> item2;

                    item2 = (new[]{new VictimaDEDPRLRJ{
                    DireccionE = item.DireccionE,
                    NombreVictima = item.NombreVictima,
                    PersonaId = item.PersonaId,
                    FechaNacimiento = item.FechaNacimiento,
                    CURP = item.CURP,
                    Telefono = item.Telefono,
                    Alias = item.Alias,
                    Correo = item.Correo,
                    NombreRepresentanteLegal = item.NombreRepresentanteLegal,
                    DireccionRepresentante = item.DireccionRepresentante,
                    TelefonoReL = item.TelefonoReL,
                    CorreoReL = item.CorreoReL,
                    RepresentanteActivo = item.RepresentanteActivo,
                    DireccionP = $"{(direccionp.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(d => d.Clave == direccionp.TipoVialidad)?.Nombre + " ": "")}" +
                                 $"{direccionp.Calle} {direccionp.NoInt} {direccionp.Localidad} {direccionp.CP} {direccionp.Municipio} {direccionp.Estado}",
                }});

                    return item2;
                }

                items2 = items2.Concat(ReadLines());

            }

            IEnumerable<VictimaDEDPRLRJ> items3 = new VictimaDEDPRLRJ[] { };

            foreach (var item in items2)
            {
                var representante = await _context.Representantes
                    .Where(a => a.PersonaId == item.PersonaId)
                    .Where(a => a.Tipo1 == 4 || a.Tipo2 == 4)
                    .FirstOrDefaultAsync();

                string nombrepresentante = "", telefonore = "", correore = "", direccionRepresentante = "";

                if (representante != null)
                {
                    nombrepresentante = representante.Nombres + " " + representante.ApellidoPa + " " + representante.ApellidoMa;
                    telefonore = representante.Telefono;
                    correore = representante.CorreoElectronico;
                    direccionRepresentante = $"{(representante.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(d => d.Clave == representante.TipoVialidad)?.Nombre + " " : "")}" +
                                 $"{representante.Calle} {representante.NoExt} {representante.NoInt} {representante.Localidad} {representante.CP} {representante.Municipio} {representante.Estado}";
                }


                IEnumerable<VictimaDEDPRLRJ> ReadLines()
                {
                    IEnumerable<VictimaDEDPRLRJ> item2;

                    item2 = (new[]{new VictimaDEDPRLRJ{
                    DireccionE = item.DireccionE,
                    NombreVictima = item.NombreVictima,
                    PersonaId = item.PersonaId,
                    FechaNacimiento = item.FechaNacimiento,
                    CURP = item.CURP,
                    Telefono = item.Telefono,
                    Alias = item.Alias,
                    Correo = item.Correo,
                    NombreRepresentanteLegal = item.NombreRepresentanteLegal,
                    TelefonoReL = item.TelefonoReL,
                    CorreoReL = item.CorreoReL,
                    RepresentanteActivo = item.RepresentanteActivo || representante != null,
                    DireccionRepresentante = direccionRepresentante,
                    DireccionP =  item.DireccionP,
                    NombreRepresentanteJuridico = nombrepresentante,
                    TelefonoReJ = telefonore,
                    CorreoReJ = correore

                }});

                    return item2;
                }

                items3 = items3.Concat(ReadLines());

            }

            return items3;

        }


        // POST: api/RAPs/CrearModuloCaptura
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearModuloCaptura(CrearVicitmaViewModel model)
        {

            var rapersonare = await _context.RAPs
                    .Where(a => a.RAtencionId == model.RAtencionId)
                    .FirstOrDefaultAsync();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid personaid;
            Guid idrap;
            try
            {
                Persona InsertarPersona = new Persona
                {
                    StatusAnonimo = model.StatusAnonimo,
                    TipoPersona = model.TipoPersona,
                    RFC = model.RFC,
                    RazonSocial = model.RazonSocial,
                    Nombre = model.Nombre,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = model.ApellidoMaterno,
                    Alias = model.Alias,
                    StatusAlias = model.StatusAlias,
                    FechaNacimiento = model.FechaNacimiento,
                    EntidadFederativa = model.EntidadFederativa,
                    DocIdentificacion = model.DocIdentificacion,
                    CURP = model.CURP,
                    //AGREGACIONES NUEVAS QUE NO ESTAN EN TODOS LADOS
                    PoblacionAfro = model.PoblacionAfro,
                    RangoEdad = model.RangoEdad,
                    RangoEdadTF = model.RangoEdadTF,
                    PoliciaDetuvo = model.PoliciaDetuvo,
                //------------------------------------------+
                    Sexo = model.Sexo,
                    Genero = model.Genero,
                    EstadoCivil = model.EstadoCivil,
                    Telefono1 = model.Telefono1,
                    Telefono2 = model.Telefono2,
                    Correo = model.Correo,
                    Medionotificacion = model.Medionotificacion,
                    Nacionalidad = model.Nacionalidad,
                    Ocupacion = model.Ocupacion,
                    NivelEstudio = model.NivelEstudio,
                    Lengua = model.Lengua,
                    Religion = model.Religion,
                    Discapacidad = model.Discapacidad,
                    TipoDiscapacidad = model.TipoDiscapacidad,
                    DatosProtegidos = model.DatosProtegidos,
                    Parentesco = model.Parentesco,
                    Edad = model.Edad,
                    Relacion = model.Relacion,
                    DocPoderNotarial = model.DocPoderNotarial,
                    InicioDetenido = model.InicioDetenido,
                    CumpleRequisitoLey = model.CumpleRequisitoLey,
                    DecretoLibertad = model.DecretoLibertad,
                    DispusoLibertad = model.DispusoLibertad

                };

                _context.Personas.Add(InsertarPersona);


                var idRA = model.RAtencionId;
                var idP = InsertarPersona.IdPersona;

                DireccionPersonal InsertarDP = new DireccionPersonal
                {

                    Calle = model.Calle,
                    NoExt = model.NoExt,
                    NoInt = model.NoInt,
                    EntreCalle1 = model.EntreCalle1,
                    EntreCalle2 = model.EntreCalle2,
                    Referencia = model.Referencia,
                    Pais = model.Pais,
                    Estado = model.Estado,
                    Municipio = model.Municipio,
                    Localidad = model.Localidad,
                    CP = model.CP,
                    lat = model.lat,
                    lng = model.lng,
                    PersonaId = idP

                };

                _context.DireccionPersonals.Add(InsertarDP);



                RAP InsertarRAP = new RAP
                {
                    RAtencionId = idRA,
                    PersonaId = idP,
                    ClasificacionPersona = model.ClasificacionPersona,
                    PInicio = rapersonare == null ? true : false,
                };

                _context.RAPs.Add(InsertarRAP);

                DireccionEscucha InsertarDE = new DireccionEscucha
                {
                    RAPId = InsertarRAP.IdRAP,
                    Calle = model.Calle,
                    NoExt = model.NoExt,
                    NoInt = model.NoInt,
                    EntreCalle1 = model.EntreCalle1,
                    EntreCalle2 = model.EntreCalle2,
                    Referencia = model.Referencia,
                    Pais = model.Pais,
                    Estado = model.Estado,
                    Municipio = model.Municipio,
                    Localidad = model.Localidad,
                    CP = model.CP,
                    lat = model.lat,
                    lng = model.lng,
                };

                _context.DireccionEscuchas.Add(InsertarDE);
                //********************************************************************** 

                await _context.SaveChangesAsync();
                personaid = InsertarPersona.IdPersona;
                idrap = InsertarRAP.IdRAP;
                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }
            return Ok(new { personaid = personaid, idrap = idrap });


        }


        // POST: api/RAPs/CrearPoliciaModuloCaptura
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearPoliciaModuloCaptura(CrearVicitmaViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid personaid;
            Guid idrap;
            try
            {
                Persona InsertarPersona = new Persona
                {
                    StatusAnonimo = false,
                    TipoPersona = "Fisica",
                    RFC = "",
                    RazonSocial = "",
                    Nombre = model.Nombre,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = model.ApellidoMaterno,
                    Alias = "",
                    StatusAlias = false,
                    FechaNacimiento = "",
                    EntidadFederativa = "",
                    DocIdentificacion = "",
                    CURP = "",
                    Sexo = "",
                    Genero = "",
                    EstadoCivil = "",
                    Telefono1 = "",
                    Telefono2 = "",
                    Correo = "",
                    Medionotificacion = "",
                    Nacionalidad = "",
                    Ocupacion = "",
                    NivelEstudio = "",
                    Lengua = "",
                    Religion = "",
                    Discapacidad = false,
                    TipoDiscapacidad = "",
                    DatosProtegidos = false,
                    InstitutoPolicial = model.InstitutoPolicial,
                    InformePolicial = model.InformePolicial,
                    PoliciaDetuvo = model.PoliciaDetuvo,


                };

                _context.Personas.Add(InsertarPersona);


                var idRA = model.RAtencionId;
                var idP = InsertarPersona.IdPersona;


                RAP InsertarRAP = new RAP
                {
                    RAtencionId = idRA,
                    PersonaId = idP,
                    ClasificacionPersona = "Oficial que detuvo",
                    PInicio = false,
                };

                _context.RAPs.Add(InsertarRAP);

                //********************************************************************** 

                await _context.SaveChangesAsync();
                personaid = InsertarPersona.IdPersona;
                idrap = InsertarRAP.IdRAP;
                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
            return Ok(new
            {
                personaId = personaid,
                rapId = idrap
            });


        }

        // GET: api/RAPs/ListarTodosModuloCaptura
        [Authorize(Roles = "Administrador, Recepción,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IEnumerable<VictimasViewModel>> ListarTodosModuloCaptura([FromRoute] Guid rAtencionId)

        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Where(x => x.RAtencionId == rAtencionId)
                            .Where(x => x.ClasificacionPersona != "NA")
                            .ToListAsync();

            return rap.Select(a => new VictimasViewModel
            {
                /*********************************************/
                /*CAT_RAP*/
                idRAP = a.IdRAP,
                RAtencionId = a.RAtencionId,
                PersonaId = a.PersonaId,
                ClasificacionPersona = a.ClasificacionPersona,
                PInicio = a.PInicio,
                /*********************************************/

                /*********************************************/
                /*CAT_PERSONA*/
                StatusAnonimo = a.Persona.StatusAnonimo,
                TipoPersona = a.Persona.TipoPersona,
                NombreCompleto = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Nombre = a.Persona.Nombre,
                ApellidoPaterno = a.Persona.ApellidoPaterno,
                ApellidoMaterno = a.Persona.ApellidoMaterno,
                Alias = a.Persona.Alias,
                FechaNacimiento = a.Persona.FechaNacimiento,
                EntidadFederativa = a.Persona.EntidadFederativa,
                RFC = a.Persona.RFC,
                RazonSocial = a.Persona.RazonSocial,
                DocIdentificacion = a.Persona.DocIdentificacion,
                CURP = a.Persona.CURP,
                //Integraciones que pueden no estan en todos lados por ser nuevas--------------------------------
                PoblacionAfro = (bool)(a.Persona.PoblacionAfro == null ? false : a.Persona.PoblacionAfro),
                RangoEdad = a.Persona.RangoEdad,
                RangoEdadTF = (bool)(a.Persona.RangoEdadTF == null ? false : a.Persona.RangoEdadTF),
                //--------------------------------------------------------------------------------------------------
                Sexo = a.Persona.Sexo,
                Genero = a.Persona.Genero,
                EstadoCivil = a.Persona.EstadoCivil,
                Telefono1 = a.Persona.Telefono1,
                Telefono2 = a.Persona.Telefono2,
                Correo = a.Persona.Correo,
                Medionotificacion = a.Persona.Medionotificacion,
                Nacionalidad = a.Persona.Nacionalidad,
                Ocupacion = a.Persona.Ocupacion,
                Lengua = a.Persona.Lengua,
                Religion = a.Persona.Religion,
                Discapacidad = a.Persona.Discapacidad,
                TipoDiscapacidad = a.Persona.TipoDiscapacidad,
                Parentesco = a.Persona.Parentesco,
                NivelEstudio = a.Persona.NivelEstudio,
                Relacion = a.Persona.Relacion,
                DatosFalsos = a.Persona.DatosFalsos,
                DatosProtegidos = a.Persona.DatosProtegidos,
                Edad = a.Persona.Edad,
                DocPoderNotarial = a.Persona.DocPoderNotarial,
                InicioDetenido = a.Persona.InicioDetenido,
                CumpleRequisitoLey = a.Persona.CumpleRequisitoLey,
                DecretoLibertad = a.Persona.DecretoLibertad,
                DispusoLibertad = a.Persona.DispusoLibertad,
                Registro = a.Persona.Registro ?? false,
            });

        }




        // GET: api/RAPs/EstadisticaPersonas
        [Authorize(Roles = "Administrador, Recepción,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{PersonasEstadistica}")]
        public async Task<IActionResult> EstadisticaPersonas([FromQuery]PersonasEstadistica PersonasEstadistica)
        {
            var carpetas = await _context.RHechoes
                .Where(a => PersonasEstadistica.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == PersonasEstadistica.DatosGenerales.Distrito : 1 == 1)
                .Where(a => PersonasEstadistica.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == PersonasEstadistica.DatosGenerales.Dsp : 1 == 1)
                .Where(a => PersonasEstadistica.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == PersonasEstadistica.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.FechaElevaNuc2 >= PersonasEstadistica.DatosGenerales.Fechadesde)
                .Where(a => a.FechaElevaNuc2 <= PersonasEstadistica.DatosGenerales.Fechahasta)
                .Where(a => PersonasEstadistica.EstatusEtapaCarpeta.EtapaActual != "null" ? a.NUCs.Etapanuc == PersonasEstadistica.EstatusEtapaCarpeta.EtapaActual : 1 == 1)
                .Where(a => PersonasEstadistica.EstatusEtapaCarpeta.StatusActual != "null" ? a.NUCs.StatusNUC == PersonasEstadistica.EstatusEtapaCarpeta.StatusActual : 1 == 1)
                .ToListAsync();

            var carpetaf = carpetas.Select(a => new PersonasEstadisticaViewModel
            {
                RHechoId = a.IdRHecho,
                RAtencionId = a.RAtencionId,
            });

            IEnumerable<PersonasEstadisticaViewModel> items = new PersonasEstadisticaViewModel[] { };

            if (PersonasEstadistica.CaracteristicasDelitoEstadistica.FiltroActivoDelito)
            {
                foreach (var carpeta in carpetaf)
                {
                    var delitof = await _context.RDHs
                        .Where(a => a.RHechoId == carpeta.RHechoId)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.Delitoactivo ? a.DelitoId == PersonasEstadistica.CaracteristicasDelitoEstadistica.DelitoNombre : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.Delitoactivo && PersonasEstadistica.CaracteristicasDelitoEstadistica.Delitoespecifico != "null" ? a.DelitoEspecifico == PersonasEstadistica.CaracteristicasDelitoEstadistica.Delitoespecifico : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.Tipofuero != "null" ? a.TipoFuero == PersonasEstadistica.CaracteristicasDelitoEstadistica.Tipofuero : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.Requisitoprocedibilidad != "null" ? a.TipoDeclaracion == PersonasEstadistica.CaracteristicasDelitoEstadistica.Requisitoprocedibilidad : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.Gradoejecucion != "null" ? a.ResultadoDelito == PersonasEstadistica.CaracteristicasDelitoEstadistica.Gradoejecucion : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.Prisionpreventiva != "null" ? a.GraveNoGrave == PersonasEstadistica.CaracteristicasDelitoEstadistica.Prisionpreventiva : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.Formacomision != "null" ? a.IntensionDelito == PersonasEstadistica.CaracteristicasDelitoEstadistica.Formacomision : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia != "null" ? a.ViolenciaSinViolencia == PersonasEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.Modalidaddelito != "null" ? a.Tipo == PersonasEstadistica.CaracteristicasDelitoEstadistica.Modalidaddelito : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.ClasificaOrdenResult != "null" ? a.ClasificaOrdenResult == PersonasEstadistica.CaracteristicasDelitoEstadistica.ClasificaOrdenResult : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.Montorobado > 0 ? a.MontoRobado == PersonasEstadistica.CaracteristicasDelitoEstadistica.Montorobado : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.Descripcionrobado != "null" ? a.TipoRobado == PersonasEstadistica.CaracteristicasDelitoEstadistica.Descripcionrobado : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia == "Violencia física" && PersonasEstadistica.CaracteristicasDelitoEstadistica.Armablanca == "Si" ? a.ArmaBlanca : PersonasEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia == "Violencia física" && PersonasEstadistica.CaracteristicasDelitoEstadistica.Armablanca == "No" ? !a.ArmaBlanca : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia == "Violencia física" && PersonasEstadistica.CaracteristicasDelitoEstadistica.Armafuego == "Si" ? a.ArmaFuego : PersonasEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia == "Violencia física" && PersonasEstadistica.CaracteristicasDelitoEstadistica.Armafuego == "No" ? !a.ArmaFuego : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia == "Violencia física" && PersonasEstadistica.CaracteristicasDelitoEstadistica.ConOtroElemento != "null" ? a.ConotroElemento == PersonasEstadistica.CaracteristicasDelitoEstadistica.ConOtroElemento : 1 == 1)
                        .Where(a => PersonasEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia == "Violencia física" && PersonasEstadistica.CaracteristicasDelitoEstadistica.ConAlgunaParteCuerpo != "null" ? a.ConAlgunaParteCuerpo == PersonasEstadistica.CaracteristicasDelitoEstadistica.ConAlgunaParteCuerpo : 1 == 1)
                        .FirstOrDefaultAsync();

                    if (delitof != null)
                    {

                        IEnumerable<PersonasEstadisticaViewModel> ReadLines()
                        {
                            IEnumerable<PersonasEstadisticaViewModel> item2;

                            item2 = (new[]{new PersonasEstadisticaViewModel{
                                RHechoId = carpeta.RHechoId,
                                RAtencionId = carpeta.RAtencionId,
                            }});

                            return item2;
                        }

                        items = items.Concat(ReadLines());

                    }

                }
                carpetaf = items;
            }


            items = new PersonasEstadisticaViewModel[] { };

            int victimadirecta = 0, victimaindirecta = 0, imputado = 0, testigo = 0;

            foreach (var carpeta in carpetaf)
            {
                var personaf = await _context.RAPs
                    .Where(a => a.RAtencionId == carpeta.RAtencionId)
                    .Where(a => a.ClasificacionPersona == "Imputado" || a.ClasificacionPersona == "Testigo" || a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .Where(a => PersonasEstadistica.Sexo != "null" ? a.Persona.Sexo == PersonasEstadistica.Sexo : 1 == 1)
                    .Where(a => a.ClasificacionPersona == "Imputado" && PersonasEstadistica.DatosDetenidoEstadistica.CumpleRequisitos != "null" ? a.Persona.CumpleRequisitoLey == PersonasEstadistica.DatosDetenidoEstadistica.CumpleRequisitos : 1 == 1)
                    .Where(a => a.ClasificacionPersona == "Imputado" && PersonasEstadistica.DatosDetenidoEstadistica.CumpleRequisitos == "Si" && PersonasEstadistica.DatosDetenidoEstadistica.DecretoLibertad != "null" ? a.Persona.DecretoLibertad == PersonasEstadistica.DatosDetenidoEstadistica.DecretoLibertad : 1 == 1)
                    .Where(a => a.ClasificacionPersona == "Imputado" && PersonasEstadistica.DatosDetenidoEstadistica.CumpleRequisitos == "No" && PersonasEstadistica.DatosDetenidoEstadistica.DispusoLibertad != "null" ? a.Persona.DispusoLibertad == PersonasEstadistica.DatosDetenidoEstadistica.DispusoLibertad : 1 == 1)
                    .ToListAsync();


                

                foreach (var persona in personaf)
                {

                    if (persona.ClasificacionPersona == "Victima directa")
                        victimadirecta++;
                    else if (persona.ClasificacionPersona == "Victima indirecta")
                        victimaindirecta++;
                    else if (persona.ClasificacionPersona == "Testigo")
                        testigo++;
                    else if (persona.ClasificacionPersona == "Imputado")
                        imputado++;

                    IEnumerable<PersonasEstadisticaViewModel> ReadLines()
                    {
                        IEnumerable<PersonasEstadisticaViewModel> item2;

                        item2 = (new[]{new PersonasEstadisticaViewModel{
                                RHechoId = carpeta.RHechoId,
                                RAtencionId = carpeta.RAtencionId,
                                Clasificacion = persona.ClasificacionPersona
                            }});

                        return item2;
                    }

                    items = items.Concat(ReadLines());

                }

            }
            carpetaf = items;
          

            return Ok(new { Victimadirecta = victimadirecta, Victimaindirecta  = victimaindirecta , Testigo  = testigo , Imputado  = imputado });

        }



        // GET: api/RAPs/EstadisticaVictimasporMedidaProteccion
        [Authorize(Roles = "Administrador, Recepción,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{VictimasCSMedidaPrteccion}")]
        public async Task<IEnumerable<VictimasCSMedidaProteccionViewModel >> EstadisticaVictimasporMedidaProteccion([FromQuery] VictimasCSMedidaPrteccion VictimasCSMedidaPrteccion)
        {
            var carpetas = await _context.RHechoes
                .Where(a => a.Status)
                .Where(a => VictimasCSMedidaPrteccion.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == VictimasCSMedidaPrteccion.DatosGenerales.Distrito : 1 == 1)
                .Where(a => VictimasCSMedidaPrteccion.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == VictimasCSMedidaPrteccion.DatosGenerales.Dsp : 1 == 1)
                .Where(a => VictimasCSMedidaPrteccion.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == VictimasCSMedidaPrteccion.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.FechaElevaNuc2 >= VictimasCSMedidaPrteccion.DatosGenerales.Fechadesde)
                .Where(a => a.FechaElevaNuc2 <= VictimasCSMedidaPrteccion.DatosGenerales.Fechahasta)
                .ToListAsync();

            var carpetaf = carpetas.Select(a => new PersonasEstadisticaViewModel
            {
                RHechoId = a.IdRHecho,
                RAtencionId = a.RAtencionId,
            });

            int conmedida = 0, sinmedida = 0;

            foreach (var carpeta in carpetaf)
            {
                var personaf = await _context.RAPs
                    .Where(a => a.RAtencionId == carpeta.RAtencionId)
                    .Include(a => a.Persona)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var persona in personaf)
                {

                    var medidaf = await _context.Medidasproteccions
                        .Where(a => a.RHechoId == carpeta.RHechoId)
                        .Where(a => a.Victima.Contains(persona.Persona.Nombre + " " + persona.Persona.ApellidoPaterno + " " + persona.Persona.ApellidoMaterno))
                        .FirstOrDefaultAsync();

                    if(VictimasCSMedidaPrteccion.MedidaProteccion == "null")
                    {
                        if (medidaf != null)
                            conmedida++;
                        else
                            sinmedida++;
                    }
                    else
                    {
                        var nomedidaf = await _context.NoMedidasProteccions
                            .Where(a => a.Medidasproteccion.RHechoId == carpeta.RHechoId)
                            .Where(a => a.Medidasproteccion.Victima.Contains(persona.Persona.Nombre + " " + persona.Persona.ApellidoPaterno + " " + persona.Persona.ApellidoMaterno))
                            .Where(a => a.Descripcion == VictimasCSMedidaPrteccion.MedidaProteccion)
                            .FirstOrDefaultAsync();

                        if (nomedidaf != null)
                            conmedida++;

                    }
                
                }

            }

            IEnumerable<VictimasCSMedidaProteccionViewModel> items = new VictimasCSMedidaProteccionViewModel[] { };

            IEnumerable<VictimasCSMedidaProteccionViewModel> ReadLines(string tipo, int cantidad)
            {
                IEnumerable<VictimasCSMedidaProteccionViewModel> item2;

                item2 = (new[]{new VictimasCSMedidaProteccionViewModel{
                                Tipo = tipo,
                                Cantidad = cantidad
                            }});

                return item2;
            }

            if(VictimasCSMedidaPrteccion.MedidaProteccion == "null")
            {
                items = items.Concat(ReadLines("Victimas con medida de protección", conmedida));
                items = items.Concat(ReadLines("Victimas sin medida de protección", sinmedida));
            }else
                items = items.Concat(ReadLines("Victimas con medida de protección "+ VictimasCSMedidaPrteccion.MedidaProteccion, conmedida));


            return items;


        }

        //ELIMINAR UN REGISTRO CON COPIA A LA BD DE LOG
        // GET: api/AmpDecs/Eliminar
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var consultarap = await _context.RAPs.Where(a => a.IdRAP == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultarap == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró RAP para esta atención" });
                }
                else
                {
                    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("LOG")).Options;
                    using (var ctx = new DbContextSIIGPP(options))
                    {
                        Guid gLog = Guid.NewGuid();
                        DateTime fecha = System.DateTime.Now;
                        LogAdmon laRegistro = new LogAdmon
                        {
                            IdLogAdmon = gLog,
                            UsuarioId = model.usuario,
                            Tabla = model.tabla,
                            FechaMov = fecha,
                            RegistroId = model.infoBorrado.registroId,
                            SolicitanteId = model.solicitante,
                            RazonMov = model.razon,
                            MovimientoId = new Guid("058C5C2A-27C2-4C02-859A-9D8C2F688A6B")
                        };

                        ctx.Add(laRegistro);
                        LogRAP logRAP= new LogRAP
                        {
                            LogAdmonId = gLog,
                            IdRAP= consultarap.IdRAP,
                            RAtencionId= consultarap.RAtencionId,
                            PersonaId= consultarap.PersonaId,
                            ClasificacionPersona= consultarap.ClasificacionPersona,
                            PInicio= consultarap.PInicio
                        };
                        ctx.Add(logRAP);

                        var consultaPersona = await _context.Personas.Where(a => a.IdPersona == consultarap.PersonaId)
                                          .Take(1).FirstOrDefaultAsync();
                        if (consultaPersona == null)
                        {
                            return Ok(new { res = "Error", men = "No se encontró ninguna persona con la información enviada" });
                        }
                        else
                        {

                            LogPersona logPersona = new LogPersona
                            {
                                LogAdmonId = gLog,
                                IdPersona= consultaPersona.IdPersona,
                                StatusAnonimo= consultaPersona.StatusAnonimo,
                                TipoPersona = consultaPersona.TipoPersona,
                                RFC = consultaPersona.RFC,
                                RazonSocial = consultaPersona.RazonSocial,
                                Nombre = consultaPersona.Nombre,
                                ApellidoPaterno = consultaPersona.ApellidoPaterno,
                                ApellidoMaterno = consultaPersona.ApellidoMaterno,
                                Alias = consultaPersona.Alias,
                                StatusAlias = consultaPersona.StatusAlias,
                                FechaNacimiento = consultaPersona.FechaNacimiento,
                                EntidadFederativa = consultaPersona.EntidadFederativa,
                                DocIdentificacion = consultaPersona.DocIdentificacion,
                                CURP = consultaPersona.CURP,
                                Sexo = consultaPersona.Sexo,
                                Genero = consultaPersona.Genero,
                                Registro = consultaPersona.Registro,
                                VerR = consultaPersona.VerR,
                                VerI = consultaPersona.VerI,
                                EstadoCivil = consultaPersona.EstadoCivil,
                                Telefono1 = consultaPersona.Telefono1,
                                Telefono2 = consultaPersona.Telefono2,
                                Correo = consultaPersona.Correo,
                                Medionotificacion = consultaPersona.Medionotificacion,
                                Nacionalidad = consultaPersona.Nacionalidad,
                                Ocupacion = consultaPersona.Ocupacion,
                                NivelEstudio = consultaPersona.NivelEstudio,
                                Lengua = consultaPersona.Lengua,
                                Religion = consultaPersona.Religion,
                                Discapacidad = consultaPersona.Discapacidad,
                                TipoDiscapacidad = consultaPersona.TipoDiscapacidad,
                                Parentesco = consultaPersona.Parentesco,
                                DatosProtegidos = consultaPersona.DatosProtegidos,
                                Numerornd = consultaPersona.Numerornd,
                                InstitutoPolicial = consultaPersona.InstitutoPolicial,
                                InformePolicial = consultaPersona.InformePolicial,
                                Relacion = consultaPersona.Relacion,
                                Edad = consultaPersona.Edad,
                                DatosFalsos = consultaPersona.DatosFalsos,
                                DocPoderNotarial = consultaPersona.DocPoderNotarial,
                                InicioDetenido = consultaPersona.InicioDetenido,
                                CumpleRequisitoLey = consultaPersona.CumpleRequisitoLey,
                                DecretoLibertad = consultaPersona.DecretoLibertad,
                                DispusoLibertad = consultaPersona.DispusoLibertad,
                            };
                            ctx.Add(logPersona);
                            _context.Remove(consultarap);
                            _context.Remove(consultaPersona);

                            //SE APLICAN TODOS LOS CAMBIOS A LA BD
                            await ctx.SaveChangesAsync();
                            await _context.SaveChangesAsync();
                        }
                        
                       
                    }
                }

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "Persona eliminada Correctamente" });
        }


        // GET: api/AmpDecs/SetPersonaIncial
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetPersonaInicial([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var consultaPersonaInicial = await _context.RAPs.Where(a => a.IdRAP == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();

                if (consultaPersonaInicial == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ninguna Persona con la información enviada" });
                }

                else
                {

                    var consultaPersona = await _context.RAPs.Where(a => a.RAtencionId == model.infoBorrado.rAtencionId).Where(a => a.PInicio == true)
                                              .Take(1).FirstOrDefaultAsync();

                    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("LOG")).Options;
                    using (var ctx = new DbContextSIIGPP(options))
                    {

                        Guid gLog = Guid.NewGuid();
                        DateTime fecha = System.DateTime.Now;
                        LogAdmon laRegistro = new LogAdmon
                        {
                            IdLogAdmon = gLog,
                            UsuarioId = model.usuario,
                            Tabla = model.tabla,
                            FechaMov = fecha,
                            RegistroId = model.infoBorrado.registroId,
                            SolicitanteId = model.solicitante,
                            RazonMov = model.razon,
                            MovimientoId = new Guid("9563ECB9-1979-4DC3-8CA9-0922409E6203")
                        };

                        ctx.Add(laRegistro);

                        if (consultaPersona != null)
                        {

                            LogRAP nRAP = new LogRAP
                            {
                                LogAdmonId = gLog,
                                IdRAP = consultaPersona.IdRAP,
                                RAtencionId = consultaPersona.RAtencionId,
                                PersonaId = consultaPersona.PersonaId,
                                ClasificacionPersona = consultaPersona.ClasificacionPersona,
                                PInicio = consultaPersona.PInicio,

                            };
                            ctx.Add(nRAP);
                            consultaPersona.PInicio = false;
                        }

                        LogRAP RAPInicial = new LogRAP
                        {
                            LogAdmonId = gLog,
                            IdRAP = consultaPersonaInicial.IdRAP,
                            RAtencionId = consultaPersonaInicial.RAtencionId,
                            PersonaId = consultaPersonaInicial.PersonaId,
                            ClasificacionPersona = consultaPersonaInicial.ClasificacionPersona,
                            PInicio = consultaPersonaInicial.PInicio,
                        };

                        ctx.Add(RAPInicial);
                        consultaPersonaInicial.PInicio = true;
                        await _context.SaveChangesAsync();
                        await ctx.SaveChangesAsync();

                    }
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "Entrevista definida como inicial" });
        }


        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/RAPs/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            var rapsCarpeta = await _context.RAPs
                            .Include(a => a.Persona)
                            .Where(x => x.RAtencionId == model.IdRAtencion)
                            .ToListAsync();
            try
            { 
                if (!ModelState.IsValid)
                {
                    var result = new ObjectResult(new { statusCode = "402", mensaje = "Información incompleta" });
                    result.StatusCode = 402;
                    return result;
                    
                }
                if (rapsCarpeta == null)
                {
                    return Ok();

                }
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {


                    foreach (RAP rapActual in rapsCarpeta)
                    {

                        var insertarPersona = await ctx.Personas.FirstOrDefaultAsync(a => a.IdPersona == rapActual.PersonaId);

                        if (insertarPersona == null)
                        {
                            insertarPersona = new Persona();
                            ctx.Personas.Add(insertarPersona);
                        }


                        insertarPersona.IdPersona = rapActual.Persona.IdPersona;
                        insertarPersona.StatusAnonimo = rapActual.Persona.StatusAnonimo;
                        insertarPersona.TipoPersona = rapActual.Persona.TipoPersona;
                        insertarPersona.RFC = rapActual.Persona.RFC;
                        insertarPersona.RazonSocial = rapActual.Persona.RazonSocial;
                        insertarPersona.Nombre = rapActual.Persona.Nombre;
                        insertarPersona.ApellidoPaterno = rapActual.Persona.ApellidoPaterno;
                        insertarPersona.ApellidoMaterno = rapActual.Persona.ApellidoMaterno;
                        insertarPersona.Alias = rapActual.Persona.Alias;
                        insertarPersona.StatusAlias = rapActual.Persona.StatusAlias;
                        insertarPersona.FechaNacimiento = rapActual.Persona.FechaNacimiento;
                        insertarPersona.EntidadFederativa = rapActual.Persona.EntidadFederativa;
                        insertarPersona.DocIdentificacion = rapActual.Persona.DocIdentificacion;
                        insertarPersona.PoblacionAfro = rapActual.Persona.PoblacionAfro;
                        insertarPersona.RangoEdad = rapActual.Persona.RangoEdad;
                        insertarPersona.RangoEdadTF = rapActual.Persona.RangoEdadTF;
                        insertarPersona.CURP = rapActual.Persona.CURP;
                        insertarPersona.Sexo = rapActual.Persona.Sexo;
                        insertarPersona.Genero = rapActual.Persona.Genero;
                        insertarPersona.EstadoCivil = rapActual.Persona.EstadoCivil;
                        insertarPersona.Telefono1 = rapActual.Persona.Telefono1;
                        insertarPersona.Telefono2 = rapActual.Persona.Telefono2;
                        insertarPersona.Correo = rapActual.Persona.Correo;
                        insertarPersona.Medionotificacion = rapActual.Persona.Medionotificacion;
                        insertarPersona.Nacionalidad = rapActual.Persona.Nacionalidad;
                        insertarPersona.Ocupacion = rapActual.Persona.Ocupacion;
                        insertarPersona.NivelEstudio = rapActual.Persona.NivelEstudio;
                        insertarPersona.Lengua = rapActual.Persona.Lengua;
                        insertarPersona.Religion = rapActual.Persona.Religion;
                        insertarPersona.Discapacidad = rapActual.Persona.Discapacidad;
                        insertarPersona.TipoDiscapacidad = rapActual.Persona.TipoDiscapacidad;
                        insertarPersona.Parentesco = rapActual.Persona.Parentesco;
                        insertarPersona.DatosProtegidos = rapActual.Persona.DatosProtegidos;
                        insertarPersona.InstitutoPolicial = rapActual.Persona.InstitutoPolicial;
                        insertarPersona.Numerornd = rapActual.Persona.Numerornd;
                        insertarPersona.InformePolicial = rapActual.Persona.InformePolicial;
                        insertarPersona.Relacion = rapActual.Persona.Relacion;
                        insertarPersona.Edad = rapActual.Persona.Edad;
                        insertarPersona.DatosFalsos = rapActual.Persona.DatosFalsos;
                        insertarPersona.DocPoderNotarial = rapActual.Persona.DocPoderNotarial;
                        insertarPersona.CumpleRequisitoLey = rapActual.Persona.CumpleRequisitoLey;
                        insertarPersona.DecretoLibertad = rapActual.Persona.DecretoLibertad;
                        insertarPersona.DispusoLibertad = rapActual.Persona.DispusoLibertad;
                        insertarPersona.InicioDetenido = rapActual.Persona.InicioDetenido;
                        insertarPersona.Registro = rapActual.Persona.Registro;
                        insertarPersona.VerI = rapActual.Persona.VerI;
                        insertarPersona.VerR = rapActual.Persona.VerR;
                        insertarPersona.PoblacionAfro = rapActual.Persona.PoblacionAfro;
                        insertarPersona.RangoEdadTF = rapActual.Persona.RangoEdadTF;
                        insertarPersona.RangoEdad = rapActual.Persona.RangoEdad;
                        insertarPersona.PoliciaDetuvo = rapActual.Persona.PoliciaDetuvo;


                        var insertarRap = await ctx.RAPs.FirstOrDefaultAsync(a => a.IdRAP == rapActual.IdRAP);

                        if (insertarRap == null)
                        {
                            insertarRap = new RAP();
                            ctx.RAPs.Add(insertarRap);
                        }
                        insertarRap.IdRAP = rapActual.IdRAP;
                        insertarRap.RAtencionId = rapActual.RAtencionId;
                        insertarRap.PersonaId = rapActual.PersonaId;
                        insertarRap.ClasificacionPersona = rapActual.ClasificacionPersona;
                        insertarRap.PInicio = rapActual.PInicio;

                        //BUSCAR DIRECCIONES
                        var consutadireccion = await _context.DireccionPersonals.FirstOrDefaultAsync(a => a.PersonaId == rapActual.PersonaId);

                        if (consutadireccion != null)
                        {
                            // VER SI LA DIRECCION NO ESTA EN EL DESTINO
                            var insertarDireccion = await ctx.DireccionPersonals.FirstOrDefaultAsync(a => a.IdDPersonal == consutadireccion.IdDPersonal);

                            if (insertarDireccion == null)
                            {
                                insertarDireccion = new DireccionPersonal();
                                ctx.DireccionPersonals.Add(insertarDireccion);
                            }

                            insertarDireccion.IdDPersonal = consutadireccion.IdDPersonal;
                            insertarDireccion.Calle = consutadireccion.Calle;
                            insertarDireccion.NoInt = consutadireccion.NoInt;
                            insertarDireccion.NoExt = consutadireccion.NoExt;
                            insertarDireccion.EntreCalle1 = consutadireccion.EntreCalle1;
                            insertarDireccion.EntreCalle2 = consutadireccion.EntreCalle2;
                            insertarDireccion.Referencia = consutadireccion.Referencia;
                            insertarDireccion.Pais = consutadireccion.Pais;
                            insertarDireccion.Estado = consutadireccion.Estado;
                            insertarDireccion.Municipio = consutadireccion.Municipio;
                            insertarDireccion.Localidad = consutadireccion.Localidad;
                            insertarDireccion.CP = consutadireccion.CP;
                            insertarDireccion.PersonaId = consutadireccion.PersonaId;
                            insertarDireccion.lat = consutadireccion.lat;
                            insertarDireccion.lng = consutadireccion.lng;
                            insertarDireccion.TipoVialidad = consutadireccion.TipoVialidad;
                            insertarDireccion.TipoAsentamiento = consutadireccion.TipoAsentamiento;
                        }

                        //BUSCAR REDES SOCIALES PERSONALES 
                        var consultaRedesSociales = await _context.RedesSocialesPersonal.Where(a => a.PersonaId == rapActual.PersonaId).ToListAsync();
                            if(consultaRedesSociales != null)
                                foreach (RedesSocialesPersonal redSocialPersonal in consultaRedesSociales)
                                {
                                    // VER SI LA RED SOCIAL PERSONAL NO ESTA EN EL DESTINO
                                    var insertarRedSocialPersonal = await ctx.RedesSocialesPersonal.FirstOrDefaultAsync(a => a.IdRedesSocialesPersonal == redSocialPersonal.IdRedesSocialesPersonal);
                                    if (insertarRedSocialPersonal == null)
                                    {
                                        insertarRedSocialPersonal = new RedesSocialesPersonal();
                                        ctx.RedesSocialesPersonal.Add(insertarRedSocialPersonal);
                                    }
                                    insertarRedSocialPersonal.IdRedesSocialesPersonal = redSocialPersonal.IdRedesSocialesPersonal;
                                    insertarRedSocialPersonal.PersonaId = redSocialPersonal.PersonaId;
                                    insertarRedSocialPersonal.RedSocialId = redSocialPersonal.RedSocialId;
                                    insertarRedSocialPersonal.Enlace = redSocialPersonal.Enlace;
                                }
                        // BUSCAR DIRECCION DE OCUPACION
                        var consultaDireccionOcupacion = await _context.DireccionOcupacion.Where(a => a.PersonaId == rapActual.PersonaId).FirstOrDefaultAsync();
                        if (consultaDireccionOcupacion != null)
                        {
                            // VER SI LA DIRECCION DE OCUPACïÓN  NO ESTÁ EN EL DESTINO
                            var insertarDireccionOcupacion = await ctx.DireccionOcupacion.FirstOrDefaultAsync(a => a.IdDOcupacion == consultaDireccionOcupacion.IdDOcupacion);
                            if (insertarDireccionOcupacion == null)
                            {
                                insertarDireccionOcupacion = new DireccionOcupacion();
                                ctx.DireccionOcupacion.Add(insertarDireccionOcupacion);
                            }
                            insertarDireccionOcupacion.IdDOcupacion = consultaDireccionOcupacion.IdDOcupacion;
                            insertarDireccionOcupacion.PersonaId = consultaDireccionOcupacion.PersonaId;
                            insertarDireccionOcupacion.Calle = consultaDireccionOcupacion.Calle;
                            insertarDireccionOcupacion.NoInt = consultaDireccionOcupacion.NoInt;
                            insertarDireccionOcupacion.NoExt = consultaDireccionOcupacion.NoExt;
                            insertarDireccionOcupacion.EntreCalle1 = consultaDireccionOcupacion.EntreCalle1;
                            insertarDireccionOcupacion.EntreCalle2 = consultaDireccionOcupacion.EntreCalle2;
                            insertarDireccionOcupacion.Referencia = consultaDireccionOcupacion.Referencia;
                            insertarDireccionOcupacion.Pais = consultaDireccionOcupacion.Pais;
                            insertarDireccionOcupacion.Estado = consultaDireccionOcupacion.Estado;
                            insertarDireccionOcupacion.Municipio = consultaDireccionOcupacion.Municipio;
                            insertarDireccionOcupacion.Localidad = consultaDireccionOcupacion.Localidad;
                            insertarDireccionOcupacion.CP = consultaDireccionOcupacion.CP;
                            insertarDireccionOcupacion.TipoVialidad = consultaDireccionOcupacion.TipoVialidad;
                            insertarDireccionOcupacion.TipoAsentamiento = consultaDireccionOcupacion.TipoAsentamiento;

                        }
                        // BUSCAR LA MEDIA FILIACIÓN DE LA PERSONA
                        var consultaMediaFiliacion = await _context.MediaAfiliacions.Where(a => a.PersonaId == rapActual.PersonaId).FirstOrDefaultAsync();
                        if (consultaMediaFiliacion != null)
                        {
                            var insertarMediFiliacion = await ctx.MediaAfiliacions.FirstOrDefaultAsync(a => a.idMediaAfiliacion == consultaMediaFiliacion.idMediaAfiliacion);
                            if (insertarMediFiliacion == null)
                            {
                                insertarMediFiliacion = new MediaAfiliacion();
                                ctx.MediaAfiliacions.Add(insertarMediFiliacion);
                            }
                            insertarMediFiliacion.idMediaAfiliacion = consultaMediaFiliacion.idMediaAfiliacion;
                            insertarMediFiliacion.PersonaId = consultaMediaFiliacion.PersonaId;
                            insertarMediFiliacion.RHechoId = consultaMediaFiliacion.RHechoId;
                            insertarMediFiliacion.Complexion = consultaMediaFiliacion.Complexion;
                            insertarMediFiliacion.Peso = consultaMediaFiliacion.Peso;
                            insertarMediFiliacion.Estatura = consultaMediaFiliacion.Estatura;
                            insertarMediFiliacion.FormaCara = consultaMediaFiliacion.FormaCara;
                            insertarMediFiliacion.ColoOjos = consultaMediaFiliacion.ColoOjos;
                            insertarMediFiliacion.Tez = consultaMediaFiliacion.Tez;
                            insertarMediFiliacion.FormaCabello = consultaMediaFiliacion.FormaCabello;
                            insertarMediFiliacion.ColorCabello = consultaMediaFiliacion.ColorCabello;
                            insertarMediFiliacion.LargoCabello = consultaMediaFiliacion.LargoCabello;
                            insertarMediFiliacion.TamañoNariz = consultaMediaFiliacion.TamañoNariz;
                            insertarMediFiliacion.TipoNariz = consultaMediaFiliacion.TipoNariz;
                            insertarMediFiliacion.GrosorLabios = consultaMediaFiliacion.GrosorLabios;
                            insertarMediFiliacion.TipoFrente = consultaMediaFiliacion.TipoFrente;
                            insertarMediFiliacion.Cejas = consultaMediaFiliacion.Cejas;
                            insertarMediFiliacion.TipoCejas = consultaMediaFiliacion.TipoCejas;
                            insertarMediFiliacion.DentaduraCompleta = consultaMediaFiliacion.DentaduraCompleta;
                            insertarMediFiliacion.TamañoBoca = consultaMediaFiliacion.TamañoBoca;
                            insertarMediFiliacion.TamañoOrejas = consultaMediaFiliacion.TamañoOrejas;
                            insertarMediFiliacion.FormaMenton = consultaMediaFiliacion.FormaMenton;
                            insertarMediFiliacion.TipoOjo = consultaMediaFiliacion.TipoOjo;
                            insertarMediFiliacion.Cicatriz = consultaMediaFiliacion.Cicatriz;
                            insertarMediFiliacion.Tatuaje = consultaMediaFiliacion.Tatuaje;
                            insertarMediFiliacion.TratamientosQuimicosCabello = consultaMediaFiliacion.TratamientosQuimicosCabello;
                            insertarMediFiliacion.FechaSys = consultaMediaFiliacion.FechaSys;
                            insertarMediFiliacion.Distrito = consultaMediaFiliacion.Distrito;
                            insertarMediFiliacion.DirSubProc = consultaMediaFiliacion.DirSubProc;
                            insertarMediFiliacion.Agencia = consultaMediaFiliacion.Agencia;
                            insertarMediFiliacion.Usuario = consultaMediaFiliacion.Usuario;
                            insertarMediFiliacion.Puesto = consultaMediaFiliacion.Puesto;
                            insertarMediFiliacion.Numerooficio = consultaMediaFiliacion.Numerooficio;
                            insertarMediFiliacion.AdherenciaOreja = consultaMediaFiliacion.AdherenciaOreja;
                            insertarMediFiliacion.Calvicie = consultaMediaFiliacion.Calvicie;
                            insertarMediFiliacion.DescripcionCicatriz = consultaMediaFiliacion.DescripcionCicatriz;
                            insertarMediFiliacion.DescripcionTatuaje = consultaMediaFiliacion.DescripcionTatuaje;
                            insertarMediFiliacion.DientesAusentes = consultaMediaFiliacion.DientesAusentes;
                            insertarMediFiliacion.FormaOjo = consultaMediaFiliacion.FormaOjo;
                            insertarMediFiliacion.Gruposanguineo = consultaMediaFiliacion.Gruposanguineo;
                            insertarMediFiliacion.ImplantacionCeja = consultaMediaFiliacion.ImplantacionCeja;
                            insertarMediFiliacion.OtrasCaracteristicas = consultaMediaFiliacion.OtrasCaracteristicas;
                            insertarMediFiliacion.PuntaNariz = consultaMediaFiliacion.PuntaNariz;
                            insertarMediFiliacion.TamanoDental = consultaMediaFiliacion.TamanoDental;
                            insertarMediFiliacion.TipoDentadura = consultaMediaFiliacion.TipoDentadura;
                            insertarMediFiliacion.TipoMenton = consultaMediaFiliacion.TipoMenton;
                            insertarMediFiliacion.TratamientoDental = consultaMediaFiliacion.TratamientoDental;
                            insertarMediFiliacion.Lateralidad = consultaMediaFiliacion.Lateralidad;
                            insertarMediFiliacion.Pomulos = consultaMediaFiliacion.Pomulos;
                            insertarMediFiliacion.Pupilentes = consultaMediaFiliacion.Pupilentes;
                            insertarMediFiliacion.Tipo2Ojos = consultaMediaFiliacion.Tipo2Ojos;
                        }
                        //BUSCAR SI HAY MEDIA AFILIACION CON DESAPARECIDO
                        var consultaMAD = await _context.MediaFiliacionDesaparecidos.Where(a => a.MediaFiliacion.PersonaId == rapActual.PersonaId).FirstOrDefaultAsync();
                        if (consultaMAD != null) { 
                        var insertarMADesap = await ctx.MediaFiliacionDesaparecidos.FirstOrDefaultAsync(a => a.MediaFiliacionId == consultaMAD.MediaFiliacionId);

                        if (insertarMADesap == null)
                        {
                            insertarMADesap = new MediaFiliacionDesaparecido();
                            ctx.MediaFiliacionDesaparecidos.Add(insertarMADesap);
                        }

                        insertarMADesap.IdMediaFiliacionDesaparecido = consultaMAD.IdMediaFiliacionDesaparecido;
                        insertarMADesap.MediaFiliacionId = consultaMAD.MediaFiliacionId;
                        insertarMADesap.Manchas = consultaMAD.Manchas;
                        insertarMADesap.TipoManchas = consultaMAD.TipoManchas;
                        insertarMADesap.ManchasOtrasCyU = consultaMAD.ManchasOtrasCyU;
                        insertarMADesap.Lunares = consultaMAD.Lunares;
                        insertarMADesap.LunaresCyU = consultaMAD.LunaresCyU;
                        insertarMADesap.Cicatrices = consultaMAD.Cicatrices;
                        insertarMADesap.TipoCicatrices = consultaMAD.TipoCicatrices;
                        insertarMADesap.CicatricesTraumaticasCyU = consultaMAD.CicatricesTraumaticasCyU;
                        insertarMADesap.CicatricesQuirurgicasTipo = consultaMAD.CicatricesQuirurgicasTipo;
                        insertarMADesap.CicatricesQuirurgicasCesareaNumero = consultaMAD.CicatricesQuirurgicasCesareaNumero;
                        insertarMADesap.CicatricesQuirurgicasCesareaOrientacion = consultaMAD.CicatricesQuirurgicasCesareaOrientacion;
                        insertarMADesap.CicatricesQuirurgicasOperacionMyU = consultaMAD.CicatricesQuirurgicasOperacionMyU;
                        insertarMADesap.Tatuajes = consultaMAD.Tatuajes;
                        insertarMADesap.TatuajesNumero = consultaMAD.TatuajesNumero;
                        insertarMADesap.TatuajesDescripcion = consultaMAD.TatuajesDescripcion;
                        insertarMADesap.Piercing = consultaMAD.Piercing;
                        insertarMADesap.PiercingNumero = consultaMAD.PiercingNumero;
                        insertarMADesap.PiercingDescripcion = consultaMAD.PiercingDescripcion;
                        insertarMADesap.UñasEstado = consultaMAD.UñasEstado;
                        insertarMADesap.UñasNoSaludables = consultaMAD.UñasNoSaludables;
                        insertarMADesap.UñasPostizas = consultaMAD.UñasPostizas;
                        insertarMADesap.Deformidades = consultaMAD.Deformidades;
                        insertarMADesap.TipoDeformidades = consultaMAD.TipoDeformidades;
                        insertarMADesap.CongenitasUbicacion = consultaMAD.CongenitasUbicacion;
                        insertarMADesap.AdquiridasUbicacion = consultaMAD.AdquiridasUbicacion;
                        insertarMADesap.ProtesisDental = consultaMAD.ProtesisDental;
                        insertarMADesap.ProtesisDentalUbicacion = consultaMAD.ProtesisDentalUbicacion;
                        insertarMADesap.DentaduraCaracteristicas = consultaMAD.DentaduraCaracteristicas;
                        insertarMADesap.DentaduraDetalles = consultaMAD.DentaduraDetalles;
                        insertarMADesap.Traumatismos = consultaMAD.Traumatismos;
                        insertarMADesap.TipoTraumatismos = consultaMAD.TipoTraumatismos;
                        insertarMADesap.UbicacionFracturas = consultaMAD.UbicacionFracturas;
                        insertarMADesap.TipoLesiones = consultaMAD.TipoLesiones;
                        insertarMADesap.CausaMordedura = consultaMAD.CausaMordedura;
                        insertarMADesap.TipoLesionesOtra = consultaMAD.TipoLesionesOtra;
                        insertarMADesap.UbicacionLesiones = consultaMAD.UbicacionLesiones;
                        insertarMADesap.FacultadesMentales = consultaMAD.FacultadesMentales;
                        insertarMADesap.TipoRetraso = consultaMAD.TipoRetraso;
                        insertarMADesap.EnfermedadesCronicas = consultaMAD.EnfermedadesCronicas;
                        insertarMADesap.EnfermedadTipo = consultaMAD.EnfermedadTipo;
                        insertarMADesap.EnfermedadTiempoDiagnostico = consultaMAD.EnfermedadTiempoDiagnostico;
                        insertarMADesap.TratamientoEnfermedadCronica = consultaMAD.TratamientoEnfermedadCronica;
                        insertarMADesap.TratamientoMedicamento = consultaMAD.TratamientoMedicamento;
                        insertarMADesap.TratamientoPeriocidad = consultaMAD.TratamientoPeriocidad;
                        insertarMADesap.Alergias = consultaMAD.Alergias;
                        insertarMADesap.TratamientoAlergia = consultaMAD.TratamientoAlergia;
                        insertarMADesap.MdicamentoTratamientoAlergia = consultaMAD.MdicamentoTratamientoAlergia;
                        insertarMADesap.PeriocidadTratamientoAlergia = consultaMAD.PeriocidadTratamientoAlergia;
                        insertarMADesap.Lentes = consultaMAD.Lentes;
                        insertarMADesap.TipoLentes = consultaMAD.TipoLentes;
                        insertarMADesap.LentesGraduacion = consultaMAD.LentesGraduacion;
                        insertarMADesap.AparatosAuditivos = consultaMAD.AparatosAuditivos;
                        insertarMADesap.oidos = consultaMAD.oidos;
                    }
              

                //BUSCA SI HAY REGISTROS DE DIRECCION ESCUCHA
                var consultadirescucha = await _context.DireccionEscuchas.FirstOrDefaultAsync(a => a.RAPId == rapActual.IdRAP);
                        //
                        if (consultadirescucha != null)
                        {
                            // BUSCA SI LA DIRECCION ESCUCHA NO ESTA EN EL DESTINO
                            var insertarDirEscucha = await ctx.DireccionEscuchas.FirstOrDefaultAsync(a => a.IdDEscucha == consultadirescucha.IdDEscucha);
                            //
                            if (insertarDirEscucha == null)
                            {
                                insertarDirEscucha = new DireccionEscucha();
                                ctx.DireccionEscuchas.Add(insertarDirEscucha);
                            }
                            //
                            insertarDirEscucha.IdDEscucha = consultadirescucha.IdDEscucha;
                            insertarDirEscucha.Calle = consultadirescucha.Calle;
                            insertarDirEscucha.NoInt = consultadirescucha.NoInt;
                            insertarDirEscucha.NoExt = consultadirescucha.NoExt;
                            insertarDirEscucha.EntreCalle1 = consultadirescucha.EntreCalle1;
                            insertarDirEscucha.EntreCalle2 = consultadirescucha.EntreCalle2;
                            insertarDirEscucha.Referencia = consultadirescucha.Referencia;
                            insertarDirEscucha.Pais = consultadirescucha.Pais;
                            insertarDirEscucha.Estado = consultadirescucha.Estado;
                            insertarDirEscucha.Municipio = consultadirescucha.Municipio;
                            insertarDirEscucha.Localidad = consultadirescucha.Localidad;
                            insertarDirEscucha.CP = consultadirescucha.CP;
                            insertarDirEscucha.lat = consultadirescucha.lat;
                            insertarDirEscucha.lng = consultadirescucha.lng;
                            insertarDirEscucha.RAPId = consultadirescucha.RAPId;
                            insertarDirEscucha.TipoVialidad = consutadireccion.TipoVialidad;
                            insertarDirEscucha.TipoAsentamiento = consutadireccion.TipoAsentamiento;
                        }
                        //
                        await ctx.SaveChangesAsync();
                    }
                    
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }


    }
}