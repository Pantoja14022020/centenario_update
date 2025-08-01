using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Desgloses;
using SIIGPP.Entidades.M_Cat.GRAC;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Cat.Desglose;
using SIIGPP.Entidades.M_Cat.GNUC;
using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Cat.RDHecho;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Datos;
using SIIGPP.CAT.FilterClass;
using SIIGPP.Entidades.M_Cat.Direcciones;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesglosesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;
        private IConfiguration _configuration;

        public DesglosesController(DbContextSIIGPP context, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;
        }


        // GET: api/Desgloses/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IActionResult> Listar([FromRoute] Guid rHechoId)
        {

            try
            {

                var HitorialDesgloses = await _context.Desgloses
                              .Where(a => a.RHechoId == rHechoId)
                              .ToListAsync();

                return Ok(HitorialDesgloses.Select(a => new CrearHDesgloseViewModel
                {
                    IdDesgloses = a.IdDesgloses,
                    RHechoId = a.RHechoId,
                    nucg = a.nucg,
                    DistritoEnvia = a.DistritoEnvia,
                    DirSubEnvia = a.DirSubEnvia,
                    AgenciaEnvia = a.AgenciaEnvia,
                    ModuloServicioIdEnvia = a.ModuloServicioIdEnvia,
                    DistritoRecibe = a.DistritoRecibe,
                    DirSubRecibe = a.DirSubRecibe,
                    AgenciaRecibe = a.AgenciaRecibe,
                    ModuloRecibe = a.ModuloRecibe,
                    ModuloServicioIdRecibe = a.ModuloServicioIdRecibe,
                    PersonaIdDesglosadas = a.PersonaIdDesglosadas,
                    RDHIdDesglosados = a.RDHIdDesglosados,
                    Contenido = a.Contenido,
                    FechaDesglose = a.FechaDesglose,
                    UsuarioEnvia = a.UsuarioEnvia,
                    PuestoEnvia = a.PuestoEnvia


                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }


        }


        // GET: api/Desgloses/ObtenerNumDesglose
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IActionResult> ObtenerNumDesglose([FromRoute] Guid rHechoId)

        {

            var hi = await _context.Desgloses
                .Where(a => a.RHechoId == rHechoId)
                .ToListAsync();

            var total = hi.Count;
            return Ok(new { total = total });



        }

        // POST: api/Desgloses/CrearRegistroDesglose
        //[Authorize(Roles = "Administrador,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearRegistroDesglose([FromBody] CrearHDesgloseViewModel model)
        {
            Guid iddesglose;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Desglose InsertarHDesglose = new Desglose
            {
                RHechoId = model.RHechoId,
                nucg = model.nucg,
                DistritoEnvia = model.DistritoEnvia,
                DirSubEnvia = model.DirSubEnvia,
                AgenciaEnvia = model.AgenciaEnvia,
                ModuloServicioIdEnvia = model.ModuloServicioIdEnvia,
                DistritoRecibe = model.DistritoRecibe,
                DirSubRecibe = model.DirSubRecibe,
                AgenciaRecibe = model.AgenciaRecibe,
                ModuloRecibe = model.ModuloRecibe,
                ModuloServicioIdRecibe = model.ModuloServicioIdRecibe,
                PersonaIdDesglosadas = model.PersonaIdDesglosadas,
                RDHIdDesglosados = model.RDHIdDesglosados,
                Contenido = model.Contenido,
                FechaDesglose = System.DateTime.Now,
                UsuarioEnvia = model.UsuarioEnvia,
                PuestoEnvia = model.PuestoEnvia

            };
            _context.Desgloses.Add(InsertarHDesglose);
            try
            {

                await _context.SaveChangesAsync();
                iddesglose = InsertarHDesglose.IdDesgloses;

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new { iddesglose = iddesglose });
        }

        // GET: api/Desgloses/ListarRacRAtencion
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IActionResult> ListarRacRAtencion([FromRoute] Guid rAtencionId)
        {
            var ms = await _context.RAtencions
                            .Include(a => a.RACs)
                            .Where(a => a.IdRAtencion == rAtencionId)
                            .FirstOrDefaultAsync();

            if (ms == null)
            {
                return BadRequest("No hay registros");

            }
            return Ok(new RAtencionRacsViewModel
            {
                //RAC
                IdRac = ms.RACs.idRac,
                Indicador = ms.RACs.Indicador,
                DistritoId = ms.RACs.DistritoId,
                DConsecutivo = ms.RACs.DConsecutivo,
                AgenciaId = ms.RACs.AgenciaId,
                CveDistrito = ms.RACs.CveDistrito,
                CveAgencia = ms.RACs.CveAgencia,
                AConsecutivo = ms.RACs.AConsecutivo,
                Año = ms.RACs.Año,
                racg = ms.RACs.racg,
                Asignado = ms.RACs.Asignado,
                Ndenuncia = ms.RACs.Ndenuncia,

                //RATENCION
                IdRAtencion = ms.IdRAtencion,
                FechaHoraRegistro = ms.FechaHoraRegistro,
                FechaHoraAtencion = ms.FechaHoraAtencion,
                FechaHoraCierre = ms.FechaHoraCierre,
                u_Nombre = ms.u_Nombre,
                u_Puesto = ms.u_Puesto,
                u_Modulo = ms.u_Modulo,
                DistritoInicial = ms.DistritoInicial,
                DirSubProcuInicial = ms.DirSubProcuInicial,
                AgenciaInicial = ms.AgenciaInicial,
                StatusAtencion = ms.StatusAtencion,
                StatusRegistro = ms.StatusRegistro,
                MedioDenuncia = ms.MedioDenuncia,
                ContencionVicitma = ms.ContencionVicitma,
                ModuloServicio = ms.ModuloServicio,
                MedioLlegada = ms.MedioLlegada

            });

        }

        // POST: api/Desgloses/CrearRacRatencion
        //[Authorize(Roles = "Administrador,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearRacRatencion([FromBody] RAtencionRacsViewModel model)
        {
            Guid idatencion;

            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Rac InsertarRAC = new Rac
                {
                    Indicador = model.Indicador,
                    DistritoId = model.DistritoId,
                    CveDistrito = model.CveDistrito,
                    DConsecutivo = model.DConsecutivo,
                    AgenciaId = model.AgenciaId,
                    CveAgencia = model.CveAgencia,
                    AConsecutivo = model.AConsecutivo,
                    Año = model.Año,
                    racg = model.racg,
                    Asignado = model.Asignado,
                    Ndenuncia = model.Ndenuncia,

                };
                _context.Racs.Add(InsertarRAC);
                await _context.SaveChangesAsync();

                var idrac = InsertarRAC.idRac;

                RAtencion InsertarRA = new RAtencion
                {
                    FechaHoraRegistro = model.FechaHoraRegistro,
                    FechaHoraAtencion = model.FechaHoraAtencion,
                    FechaHoraCierre = model.FechaHoraCierre,
                    u_Nombre = model.u_Nombre,
                    u_Puesto = model.u_Puesto,
                    u_Modulo = model.u_Modulo,
                    DistritoInicial = model.DistritoInicial,
                    DirSubProcuInicial = model.DirSubProcuInicial,
                    AgenciaInicial = model.AgenciaInicial,
                    StatusAtencion = model.StatusAtencion,
                    StatusRegistro = model.StatusRegistro,
                    MedioDenuncia = model.MedioDenuncia,
                    ContencionVicitma = model.ContencionVicitma,
                    racId = idrac,
                    ModuloServicio = model.ModuloServicio,
                    MedioLlegada = model.MedioLlegada

                };
                _context.RAtencions.Add(InsertarRA);
                await _context.SaveChangesAsync();

                idatencion = InsertarRA.IdRAtencion;


            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok(new { idatencion = idatencion });


        }

        // GET: api/Desgloses/ListarpersonaPD
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{personaId}")]
        public async Task<IActionResult> ListarpersonaPD([FromRoute] Guid personaId)

        {
            var rap = await _context.RAPs
                            .Include(a => a.Persona)
                            .Where(a => a.PersonaId == personaId)
                            .FirstOrDefaultAsync();

            if (rap == null)
            {
                return NotFound("No hay registros");

            }


            return Ok(new PersonasViewModel
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
                DatosProtegidos = rap.Persona.DatosProtegidos,
                DocPoderNotarial = rap.Persona.DocPoderNotarial,
                StatusAlias = rap.Persona.StatusAlias,
                Numerornd = rap.Persona.Numerornd,
                InformePolicial = rap.Persona.InformePolicial

            });

        }

        // POST: api/Desgloses/CrearRacRatencion
        //[Authorize(Roles = "Administrador,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarPersonasD([FromBody] PersonasViewModel model)
        {

            Guid idpersonar;
            Guid idrapr;
            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

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
                    Parentesco = model.Parentesco,
                    DatosProtegidos = model.DatosProtegidos,
                    InstitutoPolicial = model.InstitutoPolicial,
                    Numerornd = model.Numerornd,
                    InformePolicial = model.InformePolicial,
                    Relacion = model.Relacion,
                    Edad = model.Edad,
                    DatosFalsos = model.DatosFalsos,
                    DocPoderNotarial = model.DocPoderNotarial,
                    CumpleRequisitoLey = model.CumpleRequisitoLey,
                    DecretoLibertad = model.DecretoLibertad,
                    DispusoLibertad = model.DispusoLibertad,
                    InicioDetenido = model.InicioDetenido,
                    VerR = model.VerR,
                    VerI = model.VerI,
                    Registro = model.Registro,
                    PoblacionAfro = model.PoblacionAfro,
                    RangoEdad = model.RangoEdad,
                    RangoEdadTF = model.RangoEdadTF,
                    PoliciaDetuvo = model.PoliciaDetuvo

                };
                _context.Personas.Add(InsertarPersona);
                await _context.SaveChangesAsync();

                idpersonar = InsertarPersona.IdPersona;

                RAP InsertarRAP = new RAP
                {
                    PersonaId = idpersonar,
                    RAtencionId = model.RAtencionId,
                    ClasificacionPersona = model.ClasificacionPersona,
                    PInicio = model.PInicio

                };
                _context.RAPs.Add(InsertarRAP);
                await _context.SaveChangesAsync();

                idrapr = InsertarRAP.IdRAP;




            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok(new { idpersonar = idpersonar, idrapr = idrapr });

        }


        // GET: api/RHechoes/ListarPorId
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IActionResult> ListarRHechoNuc([FromRoute] Guid rHechoId)
        {
            var a = await _context.RHechoes
                          .Include(x => x.NUCs)
                          .Where(x => x.IdRHecho == rHechoId)
                          .FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound("No hay registros");
            }

            return Ok(new NucRHechoViewModel
            {

                Indicador = a.NUCs.Indicador,
                DistritoId = a.NUCs.DistritoId,
                DConsecutivo = a.NUCs.DConsecutivo,
                AgenciaId = a.NUCs.AgenciaId,
                AConsecutivo = a.NUCs.AConsecutivo,
                Año = a.NUCs.Año,
                CveAgencia = a.NUCs.CveAgencia,
                CveDistrito = a.NUCs.CveDistrito,
                nucg = a.NUCs.Indicador,
                StatusNUC = a.NUCs.StatusNUC,
                Etapanuc = a.NUCs.Etapanuc,

                RAtencionId = a.RAtencionId,
                ModuloServicioId = a.ModuloServicioId,
                AgenciaidR = a.Agenciaid,
                FechaReporte = a.FechaReporte,
                FechaHoraSuceso = a.FechaHoraSuceso,
                Status = a.Status,
                RBreve = a.RBreve,
                NarrativaHechos = a.NarrativaHechos,
                FechaElevaNuc = a.FechaElevaNuc,
                FechaElevaNuc2 = a.FechaElevaNuc2,
                Vanabim = a.Vanabim,
                NDenunciaOficio = a.NDenunciaOficio,
                Texto = a.Texto,
                FechaHoraSuceso2 = a.FechaHoraSuceso2


            });

        }
        // POST: api/Desgloses/CrearNucRhecho
        //[Authorize(Roles = "Administrador,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearNucRhecho([FromBody] NucRHechoViewModel model)
        {

            Guid idrhecho;

            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Nuc InsertarNUC = new Nuc
                {
                    Indicador = model.Indicador,
                    DistritoId = model.DistritoId,
                    DConsecutivo = model.DConsecutivo,
                    AgenciaId = model.AgenciaId,
                    AConsecutivo = model.AConsecutivo,
                    Año = model.Año,
                    CveAgencia = model.CveAgencia,
                    CveDistrito = model.CveDistrito,
                    nucg = model.nucg,
                    StatusNUC = model.StatusNUC,
                    Etapanuc = model.Etapanuc,

                };
                _context.Nucs.Add(InsertarNUC);
                await _context.SaveChangesAsync();

                var idnuc = InsertarNUC.idNuc;

                RHecho InsertarRHecho = new RHecho
                {
                    RAtencionId = model.RAtencionId,
                    ModuloServicioId = model.ModuloServicioId,
                    Agenciaid = model.AgenciaidR,
                    FechaReporte = model.FechaReporte,
                    FechaHoraSuceso = model.FechaHoraSuceso,
                    Status = model.Status,
                    RBreve = model.RBreve,
                    NarrativaHechos = model.NarrativaHechos,
                    NucId = idnuc,
                    FechaElevaNuc = model.FechaElevaNuc,
                    FechaElevaNuc2 = model.FechaElevaNuc2,
                    Vanabim = model.Vanabim,
                    NDenunciaOficio = model.NDenunciaOficio,
                    Texto = model.Texto,
                    FechaHoraSuceso2 = model.FechaHoraSuceso2

                };
                _context.RHechoes.Add(InsertarRHecho);
                await _context.SaveChangesAsync();

                idrhecho = InsertarRHecho.IdRHecho;



            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok(new { idrhecho = idrhecho });


        }


        // GET: api/Desgloses/ListardelitosD
        [HttpGet("[action]/{IdRDH}")]
        public async Task<IActionResult> ListardelitosD([FromRoute] Guid IdRDH)
        {
            var a = await _context.RDHs
                          .Where(x => x.IdRDH == IdRDH)
                          .FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound("No hay registros");
            }

            return Ok(new RDHsViewModel
            {
                IdRDH = a.IdRDH,
                DelitoId = a.DelitoId,
                RHechoId = a.RHechoId,
                ReclasificacionDelito = a.ReclasificacionDelito,
                TipoFuero = a.TipoFuero,
                TipoDeclaracion = a.TipoDeclaracion,
                ResultadoDelito = a.ResultadoDelito,
                GraveNoGrave = a.GraveNoGrave,
                IntensionDelito = a.IntensionDelito,
                ViolenciaSinViolencia = a.ViolenciaSinViolencia,
                Equiparado = a.Equiparado,
                Tipo = a.Tipo,
                Concurso = a.Concurso,
                ClasificaOrdenResult = a.ClasificaOrdenResult,
                ArmaFuego = a.ArmaFuego,
                ArmaBlanca = a.ArmaBlanca,
                Observaciones = a.Observaciones,
                ConAlgunaParteCuerpo = a.ConAlgunaParteCuerpo,
                ConotroElemento = a.ConotroElemento,
                TipoRobado = a.TipoRobado,
                MontoRobado = a.MontoRobado,
                Fechasys = a.Fechasys,
                Hipotesis = a.Hipotesis,
                DelitoEspecifico = a.DelitoEspecifico,
                TipoViolencia = a.TipoViolencia,
                SubtipoSexual = a.SubtipoSexual,
                TipoInfoDigital = a.TipoInfoDigital,
                MedioDigital = a.MedioDigital,



            });

        }

        // POST: api/Desgloses/CrearRacRatencion
        //[Authorize(Roles = "Administrador,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarDelitosD([FromBody] RDHsViewModel model)
        {

            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                RDH InsertarRDH = new RDH
                {
                    DelitoId = model.DelitoId,
                    RHechoId = model.RHechoId,
                    ReclasificacionDelito = model.ReclasificacionDelito,
                    TipoFuero = model.TipoFuero,
                    TipoDeclaracion = model.TipoDeclaracion,
                    ResultadoDelito = model.ResultadoDelito,
                    GraveNoGrave = model.GraveNoGrave,
                    IntensionDelito = model.IntensionDelito,
                    ViolenciaSinViolencia = model.ViolenciaSinViolencia,
                    Equiparado = model.Equiparado,
                    Tipo = model.Tipo,
                    Concurso = model.Concurso,
                    ClasificaOrdenResult = model.ClasificaOrdenResult,
                    ArmaFuego = model.ArmaFuego,
                    ArmaBlanca = model.ArmaBlanca,
                    Observaciones = model.Observaciones,
                    ConAlgunaParteCuerpo = model.ConAlgunaParteCuerpo,
                    ConotroElemento = model.ConotroElemento,
                    TipoRobado = model.TipoRobado,
                    MontoRobado = model.MontoRobado,
                    Fechasys = model.Fechasys,
                    Hipotesis = model.Hipotesis,
                    DelitoEspecifico = model.DelitoEspecifico,
                    TipoViolencia = model.TipoViolencia,
                    SubtipoSexual = model.SubtipoSexual,
                    TipoInfoDigital = model.TipoInfoDigital,
                    MedioDigital = model.MedioDigital,
                };
                _context.RDHs.Add(InsertarRDH);
                await _context.SaveChangesAsync();



            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok();


        }

        // GET: api/Desgloses/ListardocumentospersonaPD
        [HttpGet("[action]/{personaId}")]
        public async Task<IActionResult> ListardocumentospersonaPD([FromRoute] Guid personaId)
        {
            var dp = await _context.DocumentosPesonas
                            .Where(x => x.PersonaId == personaId)
                            .FirstOrDefaultAsync();

            if (dp == null)
            {
                return Ok(new { Ruta = "", TipoDocumento = "No se registro documento de esta persona" });

            }

            return Ok(new DocumentosPesona
            {

                TipoDocumento = dp.TipoDocumento,
                NombreDocumento = dp.NombreDocumento,
                Descripcion = dp.Descripcion,
                FechaRegistro = dp.FechaRegistro,
                Ruta = dp.Ruta,
                Distrito = dp.Distrito,
                DirSubProc = dp.DirSubProc,
                Agencia = dp.Agencia,
                Usuario = dp.Usuario,
                Puesto = dp.Puesto

            });

        }


        // POST: api/Desgloses/GuardarDocumentosPersonasD
        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarDocumentosPersonasD([FromBody] DocumentosPesona model)
        {

            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                DocumentosPesona InsertarDocPersona = new DocumentosPesona
                {
                    PersonaId = model.PersonaId,
                    TipoDocumento = model.TipoDocumento,
                    NombreDocumento = model.NombreDocumento,
                    Descripcion = model.Descripcion,
                    FechaRegistro = model.FechaRegistro,
                    Ruta = model.Ruta,
                    Distrito = model.Distrito,
                    DirSubProc = model.DirSubProc,
                    Agencia = model.Agencia,
                    Usuario = model.Usuario,
                    Puesto = model.Puesto
                };
                _context.DocumentosPesonas.Add(InsertarDocPersona);
                await _context.SaveChangesAsync();



            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok();


        }

        // POST: api/Desgloses/GuardarDocumentosPersonasD
        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarDireccionPersonasD([FromBody] CrearDireccionesViewModel model)
        {

            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

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
                    PersonaId = model.idPersona,
                    TipoVialidad = model.tipoVialidad,
                    TipoAsentamiento = model.tipoAsentamiento,
                };

                _context.DireccionPersonals.Add(InsertarDP);



                DireccionEscucha InsertarDE = new DireccionEscucha
                {
                    RAPId = model.RAPId,
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
                    TipoAsentamiento =model.de_tipoAsentamiento,
                };

                _context.DireccionEscuchas.Add(InsertarDE);
                //********************************************************************** 

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();


        }
        
        // POST: api/Desgloses/CrearDireccionDelito
        [HttpPost("[action]/{IdRHecho}/{newIdRHecho}")]
        public async Task<IActionResult> CrearDireccionDelito([FromRoute] Guid IdRHecho, [FromRoute] Guid newIdRHecho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var dp = await _context.DireccionDelitos
                                .Where(x => x.RHechoId == IdRHecho)
                                .FirstOrDefaultAsync();

                if (dp == null)
                {
                    var result = new ObjectResult(new { statusCode = "404", message = "No se ha registrado dirección del delito" });
                    result.StatusCode = 404;
                    return result;
                }

                DireccionDelito InsertarDD = new DireccionDelito
                {
                    RHechoId = newIdRHecho,
                    LugarEspecifico = dp.LugarEspecifico,
                    Calle = dp.Calle,
                    NoExt = dp.NoExt,
                    NoInt = dp.NoInt,
                    EntreCalle1 = dp.EntreCalle1,
                    EntreCalle2 = dp.EntreCalle2,
                    Referencia = dp.Referencia,
                    Pais = dp.Pais,
                    Estado = dp.Estado,
                    Municipio = dp.Municipio,
                    Localidad = dp.Localidad,
                    CP = dp.CP,
                    lat = dp.lat,
                    lng = dp.lng,
                    TipoVialidad = dp.TipoVialidad,
                    TipoAsentamiento = dp.TipoAsentamiento,
                };

                _context.DireccionDelitos.Add(InsertarDD);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();


        }
        // POST: api/Desgloses/Eliminar
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
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
                        MovimientoId = new Guid("1c7fa65f-9a5d-43d9-be62-5f9161099c62")
                    };

                    ctx.Add(laRegistro);

                    var consultaDesglose = await _context.Desgloses.Where(a => a.IdDesgloses == model.infoBorrado.registroId)
                                      .Take(1).FirstOrDefaultAsync();
                    if (consultaDesglose == null)
                    {
                        return Ok(new { res = "Error", men = "No se encontró el registro de desglose con la información enviada" });
                    }
                    else
                    {
                        LogDesglose desglose = new LogDesglose
                        {
                            LogAdmonId = gLog,
                            DesgloseId = consultaDesglose.IdDesgloses,
                            RHechoId = consultaDesglose.RHechoId,
                            nucg = consultaDesglose.nucg,
                            DistritoEnvia = consultaDesglose.DistritoEnvia,
                            DirSubEnvia = consultaDesglose.DirSubEnvia,
                            AgenciaEnvia = consultaDesglose.AgenciaEnvia,
                            ModuloServicioIdEnvia = consultaDesglose.ModuloServicioIdEnvia,
                            UsuarioEnvia = consultaDesglose.UsuarioEnvia,
                            PuestoEnvia = consultaDesglose.PuestoEnvia,
                            DistritoRecibe = consultaDesglose.DistritoRecibe,
                            DirSubRecibe = consultaDesglose.DirSubRecibe,
                            AgenciaRecibe = consultaDesglose.AgenciaRecibe,
                            ModuloRecibe = consultaDesglose.ModuloRecibe,
                            ModuloServicioIdRecibe = consultaDesglose.ModuloServicioIdRecibe,
                            PersonaIdDesglosadas = consultaDesglose.PersonaIdDesglosadas,
                            RDHIdDesglosados = consultaDesglose.RDHIdDesglosados,
                            Contenido = consultaDesglose.Contenido,
                            FechaDesglose = consultaDesglose.FechaDesglose,
                        };
                        ctx.Add(desglose);
                        _context.Remove(consultaDesglose);

                        var consultaNuc = await _context.RHechoes
                            .Include(a => a.NUCs)
                            .Where(a => a.IdRHecho == model.infoBorrado.rHechoId)
                            .Take(1).FirstOrDefaultAsync();
                        if (consultaNuc == null)
                        {
                            return Ok(new { res = "Error", men = "No se encontró NUC con la información enviada" });
                        }
                        else
                        {
                            LogNuc nuc = new LogNuc
                            {
                                LogAdminId = gLog,
                                idNuc = consultaNuc.NUCs.idNuc,
                                Indicador = consultaNuc.NUCs.Indicador,
                                DistritoId = consultaNuc.NUCs.DistritoId,
                                CveDistrito = consultaNuc.NUCs.CveDistrito,
                                DConsecutivo = consultaNuc.NUCs.DConsecutivo,
                                AgenciaId = consultaNuc.NUCs.AgenciaId,
                                CveAgencia = consultaNuc.NUCs.CveAgencia,
                                AConsecutivo = consultaNuc.NUCs.AConsecutivo,
                                Año = consultaNuc.NUCs.Año,
                                nucg = consultaNuc.NUCs.nucg,
                                StatusNUC = consultaNuc.NUCs.StatusNUC,
                                Etapanuc = consultaNuc.NUCs.Etapanuc
                            };
                            ctx.Add(nuc);

                            consultaNuc.NUCs.Etapanuc = "Inicial";
                            consultaNuc.NUCs.StatusNUC = "Inicio de la investigación";
                        }
                        await ctx.SaveChangesAsync();
                        await _context.SaveChangesAsync();
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
            return Ok(new { res = "success", men = "Desglose cancelado Correctamente" });
        }

        // POST: api/Desgloses/Clonar
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var desgloces = await _context.Desgloses.Where(x => x.RHechoId == model.IdRHecho).ToListAsync();

            if (desgloces == null)
            {
                return Ok();
            }

            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;

                using (var ctx = new DbContextSIIGPP(options))
                {
                    foreach (Desglose desgloceActual in desgloces)
                    {

                        var insertarDesgloce = await ctx.Desgloses.FirstOrDefaultAsync(a => a.IdDesgloses == desgloceActual.IdDesgloses);

                        if (insertarDesgloce == null)
                        {
                            insertarDesgloce = new Desglose();
                            ctx.Desgloses.Add(insertarDesgloce);
                        }

                        insertarDesgloce.IdDesgloses = desgloceActual.IdDesgloses;
                        insertarDesgloce.RHechoId = desgloceActual.RHechoId;
                        insertarDesgloce.nucg = desgloceActual.nucg;
                        insertarDesgloce.DistritoEnvia = desgloceActual.DistritoEnvia;
                        insertarDesgloce.DirSubEnvia = desgloceActual.DirSubEnvia;
                        insertarDesgloce.AgenciaEnvia = desgloceActual.AgenciaEnvia;
                        insertarDesgloce.ModuloServicioIdEnvia = desgloceActual.ModuloServicioIdEnvia;
                        insertarDesgloce.UsuarioEnvia = desgloceActual.UsuarioEnvia;
                        insertarDesgloce.PuestoEnvia = desgloceActual.PuestoEnvia;
                        insertarDesgloce.DistritoRecibe = desgloceActual.DistritoRecibe;
                        insertarDesgloce.DirSubRecibe = desgloceActual.DirSubRecibe;
                        insertarDesgloce.AgenciaRecibe = desgloceActual.AgenciaRecibe;
                        insertarDesgloce.ModuloRecibe = desgloceActual.ModuloRecibe;
                        insertarDesgloce.ModuloServicioIdRecibe = desgloceActual.ModuloServicioIdRecibe;
                        insertarDesgloce.PersonaIdDesglosadas = desgloceActual.PersonaIdDesglosadas;
                        insertarDesgloce.RDHIdDesglosados = desgloceActual.RDHIdDesglosados;
                        insertarDesgloce.Contenido = desgloceActual.Contenido;
                        insertarDesgloce.FechaDesglose = desgloceActual.FechaDesglose;

                        await ctx.SaveChangesAsync();
                    }
                    return Ok();
                }
            }

            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result; ;
            }
        }

        // GET: api/Desgloses/ListarDireccionPE
        [HttpGet("[action]/{personaId}")]
        public async Task<IActionResult> ListarDireccionPE([FromRoute] Guid personaId)

        {
            try
            {
                var dp = await _context.DireccionPersonals
                                .Where(x => x.PersonaId == personaId)
                                .FirstOrDefaultAsync();

                var de = await _context.DireccionEscuchas
                                .Include(d => d.RAP)
                                .Where(d => d.RAP.PersonaId == personaId)
                                .FirstOrDefaultAsync();

                if (dp == null && de == null)
                {
                    var result = new ObjectResult(new { statusCode = "404", message = "No se ha registrado dirección de las personas involucradas." });
                    result.StatusCode = 404;
                    return result;

                }

                return Ok(new DireccionesViewModel
                {
                    /*********************************************/
                    /*CAT_DIRECCIONPERSONA*/
                    calle = dp?.Calle ?? "",
                    noint = dp?.NoInt ?? "",
                    noext = dp?.NoExt ?? "",
                    entrecalle1 = dp?.EntreCalle1 ?? "",
                    entrecalle2 = dp?.EntreCalle2 ?? "",
                    referencia = dp?.Referencia ?? "",
                    pais = dp?.Pais ?? "",
                    estado = dp?.Estado ?? "",
                    municipio = dp?.Municipio ?? "",
                    localidad = dp?.Localidad ?? "",
                    cp = dp?.CP ?? 0,
                    lat = dp?.lat ?? "",
                    lng = dp?.lng ?? "",
                    tipoVialidad = dp?.TipoVialidad ?? 0,
                    tipoAsentamiento = dp?.TipoAsentamiento ?? 0,
                    /*CAT_DIRECCIONESCUCHA*/
                    de_calle = de?.Calle ?? "",
                    de_noint = de?.NoInt ?? "",
                    de_noext = de?.NoExt ?? "",
                    de_entrecalle1 = de?.EntreCalle1 ?? "",
                    de_entrecalle2 = de?.EntreCalle2 ?? "",
                    de_referencia = de?.Referencia ?? "",
                    de_pais = de?.Pais ?? "",
                    de_estado = de?.Estado ?? "",
                    de_municipio = de?.Municipio ?? "",
                    de_localidad = de?.Localidad ?? "",
                    de_cp = de?.CP ?? 0,
                    de_lat = de?.lat ?? "",
                    de_lng = de?.lng ?? "",
                    de_tipoVialidad = de?.TipoVialidad ?? 0,
                    de_tipoAsentamiento = de?.TipoAsentamiento ?? 0,
                });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }
    }
}