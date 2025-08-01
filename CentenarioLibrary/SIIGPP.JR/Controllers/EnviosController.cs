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
using SIIGPP.Datos;
using SIIGPP.Entidades.M_JR.RDelitodelitosDerivados;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_JR.RSolicitanteRequerido;
using SIIGPP.JR.Models.REnvio;
using SIIGPP.Entidades.M_Cat.Registro;
//using MoreLinq;
using SIIGPP.Entidades.M_Cat.GRAC;
using SIIGPP.Entidades.M_JR.RExpediente;
using SIIGPP.JR.Models.RExpediente;
using SIIGPP.Entidades.M_Cat.Representantes;
using SIIGPP.Entidades.M_Configuracion.Cat_TRepresentantes;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;
        public EnviosController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        // GET: api/Envios/ListaEnviosPorExpedienteNoDerivacion
        // API: Lista los envios por idExpediente y Numero de derivacion
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{expedienteId}/{noDerivacion}")]
        public async Task<IActionResult> ListaEnviosPorExpedienteNoDerivacion([FromRoute] Guid expedienteId, int noDerivacion)
        {
            var Tabla = await _context.Envios

                                .Include(a => a.Expediente.RHecho.Agencia)
                                .Include(a => a.Expediente.RHecho.Agencia.DSP.Distrito)
                                .Where(a => a.ExpedienteId == expedienteId)
                                .Where(a => a.ContadorNODerivacion == noDerivacion)
                                .FirstOrDefaultAsync();

            if (Tabla == null)
            {

                return Ok(new { ner = 1 });
            }
            return Ok(new GET_EnviosViewModel
            {
                IdEnvio = Tabla.IdEnvio,
                ExpedienteId = Tabla.ExpedienteId,
                AutoridadqueDeriva = Tabla.AutoridadqueDeriva,
                uqe_Distrito = Tabla.uqe_Distrito,
                uqe_DirSubProc = Tabla.uqe_DirSubProc,
                uqe_idAgencia = Tabla.Expediente.RHecho.Agenciaid,
                uqe_DireccionAgencia = Tabla.Expediente.RHecho.Agencia.Direccion,
                uqe_telefonoAgencia = Tabla.Expediente.RHecho.Agencia.Telefono,
                uqe_Agencia = Tabla.uqe_Agencia,
                uqe_Modulo = Tabla.uqe_Modulo,
                uqe_Nombre = Tabla.uqe_Nombre,
                uqe_Puesto = Tabla.uqe_Puesto,
                StatusGeneralEnvio = Tabla.StatusGeneral,
                EspontaneoNoEspontaneo = Tabla.EspontaneoNoEspontaneo,
                PrimeraVezSubsecuente = Tabla.PrimeraVezSubsecuente,
                ContadorNODerivacion = Tabla.ContadorNODerivacion,
                FechaRegistro = Tabla.FechaRegistro,
                NoSolicitantes = Tabla.NoSolicitantes,
                DistritoOrigenId = Tabla.Expediente.RHecho.Agencia.DSP.Distrito.IdDistrito,
                ArregloRepresentantes = Tabla.ArregloRepresentantes
            });
        }
        // GET: api/Envios/InformacionExpediente
        // API: Detalla la informacion del expediente
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{expedienteId}/{envioId}")]
        public async Task<IActionResult> InformacionExpediente([FromRoute] Guid expedienteId, Guid envioId)
        {
            var Tabla = await _context.Envios
                                .Include(a => a.Expediente)
                                .Include(a => a.Expediente.RHecho)
                                .Include(a => a.Expediente.RHecho.NUCs)
                                .Where(a => a.ExpedienteId == expedienteId)
                                .Where(a => a.IdEnvio == envioId)
                                .FirstOrDefaultAsync();

            if (Tabla == null)
            {

                return Ok(new { ner = 1 });
            }
            return Ok(new GET_EnvioExpedienteViewModel
            {
                IdExpediente = Tabla.ExpedienteId,
                RHechoId = Tabla.Expediente.RHechoId,
                NoExpediente = Tabla.Expediente.NoExpediente,
                NoDerivacion = Tabla.Expediente.NoDerivacion,
                StatusGeneral = Tabla.StatusGeneral,
                InformacionStatus = Tabla.Expediente.InformacionStatus,
                FechaRegistroExpediente = Tabla.Expediente.FechaRegistroExpediente,
                FechaDerivacion = Tabla.Expediente.FechaDerivacion,

                NUC = Tabla.Expediente.RHecho.NUCs.nucg,
                FechaHoraSuceso = Tabla.Expediente.RHecho.FechaHoraSuceso,
                ReseñaBreve = Tabla.Expediente.RHecho.RBreve,
                NarracionHechos = Tabla.Expediente.RHecho.NarrativaHechos,

                IdEnvio = Tabla.IdEnvio,
                AutoridadqueDeriva = Tabla.AutoridadqueDeriva,
                uqe_Distrito = Tabla.uqe_Distrito,
                uqe_DirSubProc = Tabla.uqe_DirSubProc,
                uqe_Agencia = Tabla.uqe_Agencia,
                uqe_Modulo = Tabla.uqe_Modulo,
                uqe_Nombre = Tabla.uqe_Nombre,
                uqe_Puesto = Tabla.uqe_Puesto,
                StatusGeneralEnvio = Tabla.StatusGeneral,
                EspontaneoNoEspontaneo = Tabla.EspontaneoNoEspontaneo,
                PrimeraVezSubsecuente = Tabla.PrimeraVezSubsecuente,
                ContadorNODerivacion = Tabla.ContadorNODerivacion,
                FechaRegistro = Tabla.FechaRegistro,
                NoSolicitantes = Tabla.NoSolicitantes,
                ArregloConjunto = Tabla.ArregloConjunto,
                ArregloRepresentantes = Tabla.ArregloRepresentantes,
            });
        }

        // GET: api/Envios/InformacionExpedienteXDistrito
        // API: Detalla la informacion del expediente dependiendo el distrito
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador, USAR")]
        [HttpGet("[action]/{expedienteId}/{envioId}/{distritoId}")]
        public async Task<IActionResult> InformacionExpedienteXDistrito([FromRoute] Guid expedienteId, Guid envioId, Guid distritoId)
        {
            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + distritoId.ToString().ToUpper())).Options;
            using (var ctx = new DbContextSIIGPP(options))
            {
                var Tabla = await ctx.Envios
                                    .Include(a => a.Expediente)
                                    .Include(a => a.Expediente.RHecho)
                                    .Include(a => a.Expediente.RHecho.NUCs)
                                    .Where(a => a.ExpedienteId == expedienteId)
                                    .Where(a => a.IdEnvio == envioId)
                                    .FirstOrDefaultAsync();

                if (Tabla == null)
                {
                    return Ok(new { ner = 1 });
                }
                return Ok(new GET_EnvioExpedienteViewModel
                {
                    IdExpediente = Tabla.ExpedienteId,
                    RHechoId = Tabla.Expediente.RHechoId,
                    NoExpediente = Tabla.Expediente.NoExpediente,
                    NoDerivacion = Tabla.Expediente.NoDerivacion,
                    StatusGeneral = Tabla.StatusGeneral,
                    InformacionStatus = Tabla.Expediente.InformacionStatus,
                    FechaRegistroExpediente = Tabla.Expediente.FechaRegistroExpediente,

                    NUC = Tabla.Expediente.RHecho.NUCs.nucg,
                    FechaHoraSuceso = Tabla.Expediente.RHecho.FechaHoraSuceso,
                    ReseñaBreve = Tabla.Expediente.RHecho.RBreve,
                    NarracionHechos = Tabla.Expediente.RHecho.NarrativaHechos,

                    IdEnvio = Tabla.IdEnvio,
                    AutoridadqueDeriva = Tabla.AutoridadqueDeriva,
                    uqe_Distrito = Tabla.uqe_Distrito,
                    uqe_DirSubProc = Tabla.uqe_DirSubProc,
                    uqe_Agencia = Tabla.uqe_Agencia,
                    uqe_Modulo = Tabla.uqe_Modulo,
                    uqe_Nombre = Tabla.uqe_Nombre,
                    uqe_Puesto = Tabla.uqe_Puesto,
                    StatusGeneralEnvio = Tabla.StatusGeneral,
                    EspontaneoNoEspontaneo = Tabla.EspontaneoNoEspontaneo,
                    PrimeraVezSubsecuente = Tabla.PrimeraVezSubsecuente,
                    ContadorNODerivacion = Tabla.ContadorNODerivacion,
                    FechaRegistro = Tabla.FechaRegistro,
                    NoSolicitantes = Tabla.NoSolicitantes,
                });
            }
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
        // GET: api/Envios/BusquedaExp
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{distritoId}/{noexp}")]
        public async Task<IActionResult> BusquedaExp([FromRoute] Guid distritoId, string noExp)
        {

            string noexpediente = HextoString(noExp);


            var Tabla = await _context.Envios
                                .Include(a => a.Expediente)
                                .Include(a => a.Expediente.RHecho)
                                .Include(a => a.Expediente.RHecho.NUCs)
                                .Include(a => a.Expediente.RHecho.Agencia)
                                .Include(a => a.Expediente.RHecho.Agencia.DSP)
                                .Include(a => a.Expediente.RHecho.Agencia.DSP.Distrito)
                                .Where(a => a.Expediente.NoExpediente == noexpediente)
                                .Where(a => a.Expediente.RHecho.Agencia.DSP.Distrito.IdDistrito == distritoId)
                                .FirstOrDefaultAsync();

            if (Tabla == null)
            {
                return Ok(new { ner = 1 });
            }
            return Ok(new GET_EnvioExpedienteViewModel
            {
                IdExpediente = Tabla.ExpedienteId,
                RHechoId = Tabla.Expediente.RHechoId,
                NoExpediente = Tabla.Expediente.NoExpediente,
                NoDerivacion = Tabla.Expediente.NoDerivacion,
                StatusGeneral = Tabla.StatusGeneral,
                InformacionStatus = Tabla.Expediente.InformacionStatus,
                FechaRegistroExpediente = Tabla.Expediente.FechaRegistroExpediente,

                NUC = Tabla.Expediente.RHecho.NUCs.nucg,
                FechaHoraSuceso = Tabla.Expediente.RHecho.FechaHoraSuceso,
                ReseñaBreve = Tabla.Expediente.RHecho.RBreve,
                NarracionHechos = Tabla.Expediente.RHecho.NarrativaHechos,

                IdEnvio = Tabla.IdEnvio,
                AutoridadqueDeriva = Tabla.AutoridadqueDeriva,
                uqe_Distrito = Tabla.uqe_Distrito,
                uqe_DirSubProc = Tabla.uqe_DirSubProc,
                uqe_Agencia = Tabla.uqe_Agencia,
                uqe_Modulo = Tabla.uqe_Modulo,
                uqe_Nombre = Tabla.uqe_Nombre,
                uqe_Puesto = Tabla.uqe_Puesto,
                StatusGeneralEnvio = Tabla.StatusGeneral,
                EspontaneoNoEspontaneo = Tabla.EspontaneoNoEspontaneo,
                PrimeraVezSubsecuente = Tabla.PrimeraVezSubsecuente,
                ContadorNODerivacion = Tabla.ContadorNODerivacion,
                FechaRegistro = Tabla.FechaRegistro,
                NoSolicitantes = Tabla.NoSolicitantes,
            });
        }
        // GET: api/Envios/InformacionExpedienteCAT
        // API: Detalla la informacion del expediente
        //[Authorize(Roles = "Director, Administrador, AMPO-AMP, AMPO-AMP Mixto, AMPO-AMP Detenido, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{rhechoId}")]
        public async Task<IEnumerable<GET_EnviosAllViewModel>> InformacionExpedienteCAT([FromRoute] Guid rhechoId)
        {
            var a = await _context.Envios
                                .Include(x => x.Expediente)
                                .Include(x => x.Expediente.RHecho)
                                .Include(x => x.Expediente.RHecho.NUCs)
                                .Where(x => x.Expediente.RHechoId == rhechoId)
                                .OrderByDescending(x => x.FechaRegistro)
                                .ToListAsync();

            return a.Select(x => new GET_EnviosAllViewModel
            {
                IdEnvio = x.IdEnvio,
                ExpedienteId = x.ExpedienteId,
                AutoridadqueDeriva = x.AutoridadqueDeriva,
                uqe_Distrito = x.uqe_Distrito,
                uqe_DirSubProc = x.uqe_DirSubProc,
                uqe_Agencia = x.uqe_Agencia,
                uqe_Modulo = x.uqe_Modulo,
                uqe_Nombre = x.uqe_Nombre,
                uqe_Puesto = x.uqe_Puesto,
                StatusGeneral = x.StatusGeneral,
                InfoConclusion = x.InfoConclusion,
                StatusAMPO = x.StatusAMPO,
                InformaAMPO = x.InformaAMPO,
                RespuestaExpediente = x.RespuestaExpediente,
                EspontaneoNoEspontaneo = x.EspontaneoNoEspontaneo,
                PrimeraVezSubsecuente = x.PrimeraVezSubsecuente,
                ContadorNODerivacion = x.ContadorNODerivacion,
                FechaRegistro = x.FechaRegistro,
                FechaCierre = x.FechaCierre,
                NoSolicitantes = x.NoSolicitantes,
                oficioAMPO = x.oficioAMPO,
                FirmaInfoAMPO = x.FirmaInfoAMPO,
                PuestoFirmaAMPO = x.PuestoFirmaAMPO,
                // EXPEDIENTE
                NoExpediente = x.Expediente.NoExpediente,
                FechaRegistroExpediente = x.Expediente.FechaRegistroExpediente,
                FechaDerivacion = x.Expediente.FechaDerivacion,
                StatusAcepRech = x.Expediente.StatusAcepRech,
                InformacionStatus = x.Expediente.InformacionStatus,
                DistritoIdDestino = x.Expediente.DistritoIdDestino,
                // INFORMACION DEL HECHO 
                NUC = x.Expediente.RHecho.NUCs.nucg,
                statusEnvio = x.statusEnvio,
            });
        }

        // GET: api/Envios/InformacionExpedienteCAT
        // API: Detalla la informacion del expediente
        //[Authorize(Roles = "Director, Administrador, AMPO-AMP, AMPO-AMP Mixto, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{rhechoId}/{expedienteId}")]
        public async Task<IActionResult> InformacionExpedienteInd([FromRoute] Guid rhechoId, Guid expedienteId)
        {
            var a = await _context.Envios
                                .Include(x => x.Expediente)
                                .Include(x => x.Expediente.RHecho)
                                .Include(x => x.Expediente.RHecho.NUCs)
                                .Where(x => x.Expediente.RHechoId == rhechoId)
                                .Where(x => x.Expediente.IdExpediente == expedienteId)
                                .OrderByDescending(x => x.ContadorNODerivacion)
                                .FirstOrDefaultAsync();

            return Ok(new GET_EnviosAllViewModel
            {
                IdEnvio = a.IdEnvio,
                ExpedienteId = a.ExpedienteId,
                AutoridadqueDeriva = a.AutoridadqueDeriva,
                uqe_Distrito = a.uqe_Distrito,
                uqe_DirSubProc = a.uqe_DirSubProc,
                uqe_Agencia = a.uqe_Agencia,
                uqe_Modulo = a.uqe_Modulo,
                uqe_Nombre = a.uqe_Nombre,
                uqe_Puesto = a.uqe_Puesto,
                StatusGeneral = a.StatusGeneral,
                InfoConclusion = a.InfoConclusion,
                StatusAMPO = a.StatusAMPO,
                InformaAMPO = a.InformaAMPO,
                RespuestaExpediente = a.RespuestaExpediente,
                EspontaneoNoEspontaneo = a.EspontaneoNoEspontaneo,
                PrimeraVezSubsecuente = a.PrimeraVezSubsecuente,
                ContadorNODerivacion = a.ContadorNODerivacion,
                FechaRegistro = a.FechaRegistro,
                FechaCierre = a.FechaCierre,
                NoSolicitantes = a.NoSolicitantes,
                // EXPEDIENTE
                NoExpediente = a.Expediente.NoExpediente,
                FechaRegistroExpediente = a.Expediente.FechaRegistroExpediente,
                FechaDerivacion = a.Expediente.FechaDerivacion,
                StatusAcepRech = a.Expediente.StatusAcepRech,
                InformacionStatus = a.Expediente.InformacionStatus,
                FirmaInfoAMPO = a.FirmaInfoAMPO,
                PuestoFirmaAMPO = a.PuestoFirmaAMPO,
                // INFORMACION DEL HECHO 
                NUC = a.Expediente.RHecho.NUCs.nucg,
            });
        }

        // POST: api/Envios/CrearEnvios
        // API: SE CREA UN ENVIO Y LA REAPERTURA DE UN EXPEDIENTE 
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearEnvios([FromBody] POST_CrearEnvioViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var fechaHoraExp = DateTime.Now;
                // SE AGREGA EL REGISTRO DE ENVIO
                //************************************************************

                Envio Env = new Envio
                {
                    ExpedienteId = model.ExpedienteId,
                    AutoridadqueDeriva = model.AutoridadqueDeriva,
                    uqe_Distrito = model.uqe_Distrito,
                    uqe_DirSubProc = model.uqe_DirSubProc,
                    uqe_Agencia = model.uqe_Agencia,
                    uqe_Modulo = model.uqe_Modulo,
                    uqe_Nombre = model.uqe_Nombre,
                    uqe_Puesto = model.uqe_Puesto,
                    StatusGeneral = model.StatusGeneralEnvio,
                    RespuestaExpediente = model.RespuestaExpediente,
                    EspontaneoNoEspontaneo = model.EspontaneoNoEspontaneo,
                    PrimeraVezSubsecuente = "Subsecuente",
                    ContadorNODerivacion = model.ContadorNODerivacion,
                    FechaRegistro = fechaHoraExp,
                    NoSolicitantes = model.Solicitantes.Count + model.Requeridos.Count,
                };
                _context.Envios.Add(Env);

                //************************************************************
                // DETALLE DE LOS SOLICITANTES Y DE LOS REQUERIDOS
                var idEnvio = Env.IdEnvio;
                if (model.ContadorNODerivacion > 1)
                {
                    foreach (var detalleS in model.Solicitantes)
                    {
                        SolicitanteRequerido S = new SolicitanteRequerido
                        {
                            EnvioId = idEnvio,
                            PersonaId = detalleS.personaId,
                            Tipo = "Solicitante",
                            Clasificacion = detalleS.Tipo,
                        };
                        _context.SolicitanteRequeridos.Add(S);
                    }
                    foreach (var detalleR in model.Requeridos)
                    {
                        SolicitanteRequerido R = new SolicitanteRequerido
                        {
                            EnvioId = idEnvio,
                            PersonaId = detalleR.code,
                            Tipo = "Requerido",
                            Clasificacion = detalleR.Tipo,
                        };
                        _context.SolicitanteRequeridos.Add(R);
                    }

                    //************************************************************
                    // DETALLE DE LOS DELITOS SUCEPTIBLES  MASC
                    foreach (var detalleDelitos in model.Delitos)
                    {
                        DelitoDerivado delito = new DelitoDerivado
                        {
                            EnvioId = idEnvio,
                            RDHId = detalleDelitos.code,
                        };
                        _context.DelitosDerivados.Add(delito);
                    }
                }
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        // PUT: api/Envios/AceptacionDerivacion
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> AceptacionDerivacion([FromBody] PUT_StatusModuloViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var db = await _context.Envios.FirstOrDefaultAsync(a => a.IdEnvio == model.IdEnvio);

            if (db == null)
            {
                return NotFound();
            }

            db.StatusGeneral = model.StatusGeneralEnvio;
            db.RespuestaExpediente = model.RespuestaExpediente;
            db.StatusAMPO = "No contestado";

            if (model.StatusGeneralEnvio == "Procedente")
            {
                var db2 = await _context.Expedientes.FirstOrDefaultAsync(a => a.IdExpediente == model.IdExpediente);

                if (db2 == null)
                {
                    return NotFound();
                }

                DateTime fechaActual = DateTime.Today;
                var año = fechaActual.Year;
                var fechaHoraExp = DateTime.Now;

                db2.Prefijo = model.Prefijo;
                db2.Año = año;
                db2.Cosecutivo = model.Cosecutivo;
                db2.Sede = model.Sede;
                db2.NoExpediente = model.NoExpediente;
                db2.NoDerivacion = model.NoDerivacion;
                db2.FechaRegistroExpediente = fechaHoraExp;
            }

            try
            {
                await _context.SaveChangesAsync();

                var registroLocal = await _context.Expedientes
                                            .Where(a => a.IdExpediente == model.IdExpediente)
                                            .Take(1).FirstOrDefaultAsync();

                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoOrigen.ToString().ToUpper())).Options;

                using (var ctx = new DbContextSIIGPP(options))
                {
                    var derivacion = await ctx.Expedientes.FirstOrDefaultAsync(a => a.IdExpediente == model.IdExpediente);

                    if (derivacion == null)
                    {
                        derivacion = new Expediente();
                        ctx.Expedientes.Add(derivacion);
                    }

                    derivacion.IdExpediente = registroLocal.IdExpediente;
                    derivacion.RHechoId = registroLocal.RHechoId;
                    derivacion.Prefijo = registroLocal.Prefijo;
                    derivacion.Cosecutivo = registroLocal.Cosecutivo;
                    derivacion.Año = registroLocal.Año;
                    derivacion.Sede = registroLocal.Sede;
                    derivacion.NoExpediente = registroLocal.NoExpediente;
                    derivacion.NoDerivacion = registroLocal.NoDerivacion;
                    derivacion.InformacionStatus = registroLocal.InformacionStatus;
                    derivacion.FechaRegistroExpediente = registroLocal.FechaRegistroExpediente;
                    derivacion.FechaDerivacion = registroLocal.FechaDerivacion;
                    derivacion.StatusAcepRech = registroLocal.StatusAcepRech;
                    derivacion.DistritoIdDestino = registroLocal.DistritoIdDestino;

                    await ctx.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // PUT: api/Envios/StatusModuloRespuesta
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> StatusModuloRespuesta([FromBody] PUT_StatusModuloViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var db = await _context.Envios.FirstOrDefaultAsync(a => a.IdEnvio == model.IdEnvio);

                if (db == null)
                {
                    return NotFound();
                }

                db.StatusGeneral = model.StatusGeneralEnvio;
                db.RespuestaExpediente = model.RespuestaExpediente;
                db.StatusAMPO = "No contestado";
  
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                var db2 = await _context.Envios.FirstOrDefaultAsync(a => a.IdEnvio == model.IdEnvio);

                if (db2 == null)
                {
                    return NotFound();
                }

                db2.StatusGeneral = "Procedente";

                await _context.SaveChangesAsync();

                return BadRequest(ex);
            }

        }

        // PUT: api/Envios/ActualizarStatusEnvio
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatusEnvio([FromBody] PUT_StatusModuloViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Busca el id de envio para actualizar el estatus
            var db = await _context.Envios.FirstOrDefaultAsync(a => a.IdEnvio == model.IdEnvio);

            if (db == null)
            {
                return NotFound();
            }

            //Se actualiza informacion general
            db.StatusGeneral = model.StatusGeneralEnvio;
            db.RespuestaExpediente = model.RespuestaExpediente;
            db.StatusAMPO = "No contestado";

            //var noExpediente = "";

            try
            {
                await _context.SaveChangesAsync();

                var registroLocal = await _context.Expedientes
                                            .Where(a => a.IdExpediente == model.IdExpediente)
                                            .Take(1).FirstOrDefaultAsync();

                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoOrigen.ToString().ToUpper())).Options;

                using (var ctx = new DbContextSIIGPP(options))
                {
                    var derivacion = await ctx.Expedientes.FirstOrDefaultAsync(a => a.IdExpediente == model.IdExpediente);

                    if (derivacion == null)
                    {
                        derivacion = new Expediente();
                        ctx.Expedientes.Add(derivacion);
                    }

                    derivacion.IdExpediente = registroLocal.IdExpediente;
                    derivacion.RHechoId = registroLocal.RHechoId;
                    derivacion.Prefijo = registroLocal.Prefijo;
                    derivacion.Cosecutivo = registroLocal.Cosecutivo;
                    derivacion.Año = registroLocal.Año;
                    derivacion.Sede = registroLocal.Sede;
                    derivacion.NoExpediente = registroLocal.NoExpediente;
                    derivacion.NoDerivacion = registroLocal.NoDerivacion;
                    derivacion.InformacionStatus = registroLocal.InformacionStatus;
                    derivacion.FechaRegistroExpediente = registroLocal.FechaRegistroExpediente;
                    derivacion.FechaDerivacion = registroLocal.FechaDerivacion;
                    derivacion.StatusAcepRech = registroLocal.StatusAcepRech;
                    derivacion.DistritoIdDestino = registroLocal.DistritoIdDestino;

                    await ctx.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var db2 = await _context.Envios.FirstOrDefaultAsync(a => a.IdEnvio == model.IdEnvio);

                if (db2 == null)
                {
                    return NotFound();
                }

                db2.StatusGeneral = "Procedente";

                await _context.SaveChangesAsync();

                return BadRequest(ex);
            }

            return Ok(new { avisoConexion = "La conexion al distrito destino se logro" });
        }

        // PUT: api/Envios/StatusAMPO
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> StatusAMPO([FromBody] PUT_StatusAMPOViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var db = await _context.Envios.FirstOrDefaultAsync(a => a.IdEnvio == model.IdEnvio);

            if (db == null)
            {
                return NotFound();
            }

            db.StatusAMPO = model.StatusAMPO;
            db.InformaAMPO = model.InfoAMPO;
            db.oficioAMPO = model.oficioAMPO;
            db.StatusGeneral = model.StatusGeneral;
            db.FirmaInfoAMPO = model.FirmaInfoAMPO;
            db.PuestoFirmaAMPO = model.PuestoFirmaAMPO;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }

            return Ok();
        }



        //Nueva API para listar el rezago de los expedientes
        // GET: api/Envios/ListarTodosRezago
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Facilitador, Facilitador-Mixto, Notificador, USAR")]
        [HttpGet("[action]/{idDistrito}")]
        public async Task<IActionResult> ListarTodosRezago([FromRoute] Guid idDistrito)

        {
            try
            {
                //La consulta consta de las siguientes caracteristicas
                //Muestra los valores normales de la informacion de los expedientes, sin embargo, la instrucccion es que cuente con las caracteristicas de que 
                //-Se pueda determinar si tiene alguna sesion satisfactoria, tiene algun acuerdo reparatorio sy se esta cumpliendo el acuerdo
                String expedientesConRezago = @"SELECT 
                                                        EX.IdExpediente,
                                                        EX.NoExpediente,
                                                        EX.FechaRegistroExpediente,
                                                        EX.FechaDerivacion,
                                                        EN.IdEnvio,
                                                        EN.StatusGeneral,
                                                        EN.AutoridadqueDeriva,
                                                        NUC.nucg,
                                                        RH.FechaElevaNuc,
                                                        CD.IdConjuntoDerivaciones,
                                                        SE.IdSesion,
                                                        SE.FechaHoraSys AS fechahorasesion,
                                                        SE.StatusSesion,
                                                        SE.NoSesion,
                                                        AR.IdAcuerdoReparatorio,
                                                        AR.FechaCelebracionAcuerdo,
                                                        MS.Nombre,
                                                        SA.StatusPago,
                                                        sa.Fecha as fechaseguimiento
                                                    FROM 
                                                        JR_EXPEDIENTE AS EX
                                                        LEFT JOIN CAT_RHECHO AS RH ON RH.IdRHecho = EX.RHechoId
                                                        LEFT JOIN NUC AS NUC ON NUC.idNuc = RH.NucId
                                                        LEFT JOIN JR_ENVIO AS EN ON EN.ExpedienteId = EX.IdExpediente
                                                        LEFT JOIN JR_ASINGACIONENVIO AS AE ON AE.EnvioId = EN.IdEnvio
                                                        LEFT JOIN C_MODULOSERVICIO AS MS ON MS.IdModuloServicio = AE.ModuloServicioId
                                                        LEFT JOIN JR_CONJUNTODERIVACIONES AS CD ON CD.EnvioId = EN.IdEnvio
                                                        LEFT JOIN JR_SESION AS SE ON SE.EnvioId = EN.IdEnvio
                                                        LEFT JOIN JR_ACUERDOREPARATORIO AS AR ON AR.EnvioId = EN.IdEnvio
                                                        LEFT JOIN JR_SEGUIMIENTOCUMPLIMIENTO AS SA ON SA.AcuerdoReparatorioId = AR.IdAcuerdoReparatorio
                                                    WHERE 
                                                        EX.DistritoIdDestino = @idDistrito AND EX.IdExpediente IS NOT NULL AND EN.IdEnvio IS NOT NULL AND (EN.StatusGeneral != 'Concluido')
                                                    ORDER BY 
                                                        CASE 
                                                            WHEN SE.StatusSesion = 'Se realiza sesión con acuerdo reparatorio' THEN 0
                                                            ELSE 1
                                                        END,
                                                        CASE 
                                                            WHEN SA.StatusPago = 'No pagado' THEN 0
                                                            ELSE 1
                                                        END,
                                                        CASE 
                                                            WHEN MS.Nombre like '%Sala%' THEN 0
                                                            ELSE 1
                                                        END";
                //Hacer una consulta de esta indole arroja multiples valores por expediente, por medio de order by por case hicimos que se muestren primero las sesiones que ya tinen acuerdo, asi como los estatus de pago de No pagado
                //Para que en front pueda evaluarse y mostrar una leyenda de si o no

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>
                {
                    new SqlParameter("@idDistrito", idDistrito)
                };

                var ExRez = await _context.ConsultaExpedientesRezagados.FromSqlRaw(expedientesConRezago, filtrosBusqueda.ToArray()).ToListAsync();

                //El orden de listado es importante para esta parte, pues la funcion DistinctBy va a tomar el primer registro de todo el resultado de la consultay asi determinar si al menos hay una sesion satisfactoria o si hay
                //un pago no realizado para determinar que no se esta cumpliendo el acuerdo
                var personasUnicas = ExRez.DistinctBy(a => a.IdExpediente);

                return Ok(personasUnicas.Select(a => new ExpedientesRezagoViewModel
                {
                    //Dentro de esta seccion no hay mucho que explicar, la de resaltar los operadores ternarios colocados ya que sin ellos, los valores null truncan la funcion
                    //y en esta caso es normal los valores nulos ¿Por que? Por que no todos los expedientes tienen idConjunto, Sesiones creadas, acuerdos y asi sucesivamente.
                    IdExpediente = a.IdExpediente,
                    NoExpediente = a.NoExpediente,
                    FechaRegistroExpediente = a.FechaRegistroExpediente,
                    FechaDerivacion = a.FechaDerivacion,
                    IdEnvio = a.IdEnvio,
                    StatusGeneral = a.StatusGeneral,
                    AutoridadqueDeriva = a.AutoridadqueDeriva,
                    nucg = a.nucg,
                    FechaElevaNuc = a.FechaElevaNuc,
                    IdConjuntoDerivaciones = a.IdConjuntoDerivaciones ?? Guid.Empty,
                    IdSesion = a.IdSesion ?? Guid.Empty,
                    fechahorasesion = a.fechahorasesion,
                    StatusSesion = a.StatusSesion,
                    NoSesion = a.NoSesion,
                    IdAcuerdoReparatorio = a.IdAcuerdoReparatorio ?? Guid.Empty,
                    FechaCelebracionAcuerdo = a.FechaCelebracionAcuerdo,
                    Nombre = a.Nombre,
                    StatusPago = a.StatusPago,
                    fechaseguimiento = a.fechaseguimiento,

                }));

            }
            //Este catch hara que en caso de error te muestre que clase de error en el reponse de la api (Revisar consola) Es muy usado en todo el proyecto pero esa es su funcion.
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/Envios/ListarDerivacionesProcedentesInicial
        //[Authorize(Roles = "AMPO-AMP,Administrador,Director,Coordinador")]
        [HttpGet("[action]/{idDistrito}")]
        public async Task<IEnumerable<GET_EnviosAllViewModel>> ListarDerivacionesProcedentesInicial([FromRoute] Guid idDistrito)
        {
            var Env = await _context.Envios
                          .Include(a => a.Expediente)
                          .Include(a => a.Expediente.RHecho)
                          .Include(a => a.Expediente.RHecho.NUCs)
                          .Where(a => a.StatusGeneral == "Procedente")
                          .Where(a => a.Expediente.DistritoIdDestino == idDistrito)
                          .OrderBy(a => a.Expediente.FechaDerivacion)
                          .ToListAsync();

            return Env.Select(a => new GET_EnviosAllViewModel
            {
                IdEnvio = a.IdEnvio,
                ExpedienteId = a.ExpedienteId,
                NoDerivacion = a.Expediente.NoDerivacion,
                rHechoId = a.Expediente.RHechoId,
                rBreve = a.Expediente.RHecho.RBreve,
                AutoridadqueDeriva = a.AutoridadqueDeriva,
                uqe_Distrito = a.uqe_Distrito,
                uqe_DirSubProc = a.uqe_DirSubProc,
                uqe_Agencia = a.uqe_Agencia,
                uqe_Modulo = a.uqe_Modulo,
                uqe_Nombre = a.uqe_Nombre,
                uqe_Puesto = a.uqe_Puesto,
                StatusGeneral = a.StatusGeneral,
                InfoConclusion = a.InfoConclusion,
                StatusAMPO = a.StatusAMPO,
                InformaAMPO = a.InformaAMPO,
                RespuestaExpediente = a.RespuestaExpediente,
                EspontaneoNoEspontaneo = a.EspontaneoNoEspontaneo,
                PrimeraVezSubsecuente = a.PrimeraVezSubsecuente,
                ContadorNODerivacion = a.ContadorNODerivacion,
                FechaRegistro = a.FechaRegistro,
                FechaCierre = a.FechaCierre,
                NoSolicitantes = a.NoSolicitantes,
                // EXPEDIENTE
                NoExpediente = a.Expediente.NoExpediente,
                FechaRegistroExpediente = a.Expediente.FechaRegistroExpediente,
                FechaDerivacion = a.Expediente.FechaDerivacion,
                StatusAcepRech = a.Expediente.StatusAcepRech,

                // INFORMACION DEL HECHO 
                NUC = a.Expediente.RHecho.NUCs.nucg,


            });

        }



        // GET: api/Envios/ListarDerivacionesProcedentes
        //[Authorize(Roles = "AMPO-AMP,Administrador,Director,Coordinador")]
        [HttpGet("[action]/{fechai}/{fechaf}")]
        public async Task<IEnumerable<GET_EnviosAllViewModel>> ListarDerivacionesProcedentes([FromRoute] DateTime fechai, DateTime fechaf)
        {
            var Env = await _context.Envios
                          .Include(a => a.Expediente)
                          .Include(a => a.Expediente.RHecho)
                          .Include(a => a.Expediente.RHecho.NUCs)
                          //  .Where(a => a.Expediente.RHecho.NUCs.DistritoId == idDistrito)
                          .Where(a => a.Expediente.FechaRegistroExpediente >= fechai)
                          .Where(a => a.Expediente.FechaRegistroExpediente <= fechaf)
                          .Where(a => a.StatusGeneral == "Procedente")
                          .OrderBy(a => a.Expediente.FechaDerivacion)
                          .ToListAsync();

            return Env.Select(a => new GET_EnviosAllViewModel
            {
                IdEnvio = a.IdEnvio,
                ExpedienteId = a.ExpedienteId,
                NoDerivacion = a.Expediente.NoDerivacion,
                rHechoId = a.Expediente.RHechoId,
                rBreve = a.Expediente.RHecho.RBreve,
                AutoridadqueDeriva = a.AutoridadqueDeriva,
                uqe_Distrito = a.uqe_Distrito,
                uqe_DirSubProc = a.uqe_DirSubProc,
                uqe_Agencia = a.uqe_Agencia,
                uqe_Modulo = a.uqe_Modulo,
                uqe_Nombre = a.uqe_Nombre,
                uqe_Puesto = a.uqe_Puesto,
                StatusGeneral = a.StatusGeneral,
                InfoConclusion = a.InfoConclusion,
                StatusAMPO = a.StatusAMPO,
                InformaAMPO = a.InformaAMPO,
                RespuestaExpediente = a.RespuestaExpediente,
                EspontaneoNoEspontaneo = a.EspontaneoNoEspontaneo,
                PrimeraVezSubsecuente = a.PrimeraVezSubsecuente,
                ContadorNODerivacion = a.ContadorNODerivacion,
                FechaRegistro = a.FechaRegistro,
                FechaCierre = a.FechaCierre,
                NoSolicitantes = a.NoSolicitantes,
                // EXPEDIENTE
                NoExpediente = a.Expediente.NoExpediente,
                FechaRegistroExpediente = a.Expediente.FechaRegistroExpediente,
                FechaDerivacion = a.Expediente.FechaDerivacion,
                StatusAcepRech = a.Expediente.StatusAcepRech,

                // INFORMACION DEL HECHO 
                NUC = a.Expediente.RHecho.NUCs.nucg,


            });

        }
        // GET: api/Envios/ListarDerivacionesFiltro
        //[Authorize(Roles = "AMPO-AMP,Administrador,Director,Coordinador")]
        [HttpGet("[action]/{idDistrito}/{fechai}/{fechaf}/{statusAMPO}")]
        public async Task<IEnumerable<GET_EnviosAllViewModel>> ListarDerivacionesFiltro([FromRoute] Guid idDistrito, DateTime fechai, DateTime fechaf, string statusAMPO)
        {
            var Env = await _context.Envios
                          .Include(a => a.Expediente)
                          .Include(a => a.Expediente.RHecho)
                          .Include(a => a.Expediente.RHecho.NUCs)
                          .Where(a => a.Expediente.RHecho.NUCs.DistritoId == idDistrito)
                          .Where(a => a.Expediente.FechaRegistroExpediente >= fechai)
                          .Where(a => a.Expediente.FechaRegistroExpediente <= fechaf)
                          .Where(a => a.StatusGeneral != "Solicitado")
                          .Where(a => a.StatusGeneral != "No procedente")
                          .Where(a => a.StatusGeneral != "Procedente")
                          .Where(a => statusAMPO != "Todos" ? a.StatusAMPO == statusAMPO : 1 == 1)
                          .OrderBy(a => a.Expediente.FechaDerivacion)
                          .ToListAsync();

            return Env.Select(a => new GET_EnviosAllViewModel
            {
                IdEnvio = a.IdEnvio,
                ExpedienteId = a.ExpedienteId,
                rHechoId = a.Expediente.RHechoId,
                AutoridadqueDeriva = a.AutoridadqueDeriva,
                uqe_Distrito = a.uqe_Distrito,
                uqe_DirSubProc = a.uqe_DirSubProc,
                uqe_Agencia = a.uqe_Agencia,
                uqe_Modulo = a.uqe_Modulo,
                uqe_Nombre = a.uqe_Nombre,
                uqe_Puesto = a.uqe_Puesto,
                StatusGeneral = a.StatusGeneral,
                InfoConclusion = a.InfoConclusion,
                StatusAMPO = a.StatusAMPO,
                InformaAMPO = a.InformaAMPO,
                RespuestaExpediente = a.RespuestaExpediente,
                EspontaneoNoEspontaneo = a.EspontaneoNoEspontaneo,
                PrimeraVezSubsecuente = a.PrimeraVezSubsecuente,
                ContadorNODerivacion = a.ContadorNODerivacion,
                FechaRegistro = a.FechaRegistro,
                FechaCierre = a.FechaCierre,
                NoSolicitantes = a.NoSolicitantes,
                // EXPEDIENTE
                NoExpediente = a.Expediente.NoExpediente,
                FechaRegistroExpediente = a.Expediente.FechaRegistroExpediente,
                FechaDerivacion = a.Expediente.FechaDerivacion,
                StatusAcepRech = a.Expediente.StatusAcepRech,

                // INFORMACION DEL HECHO 
                NUC = a.Expediente.RHecho.NUCs.nucg,


            });

        }
        //************************************************************************************************************
        //************************************************************************************************************

        // GET: api/Envios/InformeGeneral/
        //[Authorize(Roles = "AMPO-AMP,Administrador,Director,Coordinador")]
        //[HttpGet("[action]/{fechai}/{fechaf}")]
        [HttpGet("[action]/{year}")]
        public async Task<IEnumerable<GET_InformeGeneralViewModel>> InformeGeneral([FromRoute] int year)
        {
            var envios = await _context.Envios
                               .Where(v => v.FechaRegistro.Year == year)
                              .GroupBy(v => v.FechaRegistro.Month)
                              .Select(x => new {
                                  mes = x.Key,
                                  baja = x.Count(v => v.StatusGeneral == "Baja"),
                                  tramite = x.Count(v => v.StatusGeneral == "En trámite"),
                                  concluido = x.Count(v => v.StatusGeneral == "Concluido"),
                              })
                             .ToListAsync();

            var envio = envios.Select(a => new GET_InformeGeneralViewModel
            {
                mes = a.mes,
                registrados = a.concluido + a.baja + a.tramite,
                acuerdos = a.concluido,
                baja = a.baja,
                tramite = a.tramite,


            });
            //***************************************************************************************
            IEnumerable<GET_InformeGeneralViewModel> items1 = new GET_InformeGeneralViewModel[] { };

            foreach (var env in envio)
            {
                var acuerdos = await _context.AcuerdoReparatorios
                                    .Include(a => a.Envio)
                                    .Where(v => v.Envio.FechaRegistro.Year == year)
                                    .Where(a => a.Envio.FechaRegistro.Month == env.mes)
                                    .GroupBy(v => v.Envio.FechaRegistro.Month)
                                    .Select(x => new {
                                        mes = x.Key,
                                        inmediato = x.Count(v => v.StatusConclusion == "Inmediato"),
                                        diferido = x.Count(v => v.StatusConclusion == "Diferido"),
                                    })
                             .ToListAsync();

                foreach (var acuerdo in acuerdos)
                {
                    IEnumerable<GET_InformeGeneralViewModel> ReadLines()
                    {
                        IEnumerable<GET_InformeGeneralViewModel> item;
                        item = (new[]{new GET_InformeGeneralViewModel{
                                mes = env.mes,
                                registrados = env.registrados,
                                acuerdos = env.acuerdos,
                                inmediato = acuerdo.inmediato,
                                diferidos = acuerdo.diferido,
                                baja = env.baja,
                                tramite = env.tramite,
                              }});

                        return item;
                    }

                    items1 = items1.Concat(ReadLines());

                }


            }
            //***************************************************************************************
            IEnumerable<GET_InformeGeneralViewModel> items2 = new GET_InformeGeneralViewModel[] { };
            foreach (var env in items1)
            {
                var acuerdos = await _context.AcuerdoReparatorios
                                    .Include(a => a.Envio)
                                    .Where(v => v.Envio.FechaRegistro.Year == year)
                                    .Where(a => a.Envio.FechaRegistro.Month == env.mes)
                                    .Where(a => a.StatusConclusion == "Diferido")
                                    .GroupBy(v => v.Envio.FechaRegistro.Month)
                                    .Select(x => new {
                                        mes = x.Key,
                                        cumplido = x.Count(v => v.StatusCumplimiento == "Cumplido"),
                                        incumplido = x.Count(v => v.StatusCumplimiento == "Incumplido"),
                                        encumplimiento = x.Count(v => v.StatusCumplimiento == "En cumplimiento"),
                                    })
                             .ToListAsync();

                foreach (var acuerdo in acuerdos)
                {
                    IEnumerable<GET_InformeGeneralViewModel> ReadLines()
                    {
                        IEnumerable<GET_InformeGeneralViewModel> item;
                        item = (new[]{new GET_InformeGeneralViewModel{
                                mes = env.mes,
                                registrados = env.registrados,
                                acuerdos = env.acuerdos,
                                inmediato = env.inmediato,
                                diferidos = env.diferidos,
                                cumplidos= acuerdo.cumplido,
                                incumplidos = acuerdo.incumplido,
                                encumplimiento = acuerdo.encumplimiento,
                                baja = env.baja,
                                tramite = env.tramite,
                              }});

                        return item;
                    }

                    items2 = items2.Concat(ReadLines());

                }


            }
            //***************************************************************************************

            return items2;
        }
        // GET: api/Envios/InformeGeneralInmediatos/
        //[Authorize(Roles = "AMPO-AMP,Administrador,Director,Coordinador")]
        //[HttpGet("[action]   /{fechai}/{fechaf}")]
        [HttpGet("[action]/{year}/{mes}")]
        public async Task<IEnumerable<GET_InformacionGeneralInmediatos>> InformeGeneralInmediatos([FromRoute] int year, int mes)
        {
            var envios = await _context.AcuerdoReparatorios
                              .Include(v => v.Envio)
                              .Where(v => v.StatusConclusion == "Inmediato")
                              .Where(v => v.Envio.FechaRegistro.Year == year)
                              .Where(v => v.Envio.FechaRegistro.Month == mes)
                              .GroupBy(v => v.Envio.FechaRegistro.Month)
                              .Select(x => new
                              {
                                  mes = x.Key,
                                  mediacion = x.Count(v => v.MetodoUtilizado == "Mediación"),
                                  conciliacion = x.Count(v => v.MetodoUtilizado == "conciliación"),
                                  junta = x.Count(v => v.MetodoUtilizado == "Junta restaurativa"),
                              })
                             .ToListAsync();

            return envios.Select(a => new GET_InformacionGeneralInmediatos
            {
                mes = a.mes,
                mediacion = a.mediacion,
                conciliacion = a.conciliacion,
                junta = a.junta,


            });

        }
        // GET: api/Envios/InformeGeneralDiferidos/
        //[Authorize(Roles = "AMPO-AMP,Administrador,Director,Coordinador")]
        //[HttpGet("[action]/{fechai}/{fechaf}")]
        [HttpGet("[action]/{year}/{mes}")]
        public async Task<IEnumerable<Get_InformacionGeneralDiferidos>> InformeGeneralDiferidos([FromRoute] int year, int mes)
        {
            var acuerdos = await _context.AcuerdoReparatorios
                                .Include(v => v.Envio)
                                .Where(v => v.StatusConclusion == "Diferido")
                                .Where(v => v.Envio.FechaRegistro.Year == year)
                                .Where(v => v.Envio.FechaRegistro.Month == mes)
                                .GroupBy(v => v.StatusCumplimiento)
                                .Select(x => new {
                                    sc = x.Key,
                                    med = x.Count(v => v.MetodoUtilizado == "Mediación"),
                                    conc = x.Count(v => v.MetodoUtilizado == "Conciliación"),
                                    junta = x.Count(v => v.MetodoUtilizado == "Junta restaurativa"),
                                })
                             .ToListAsync();

            return acuerdos.Select(a => new Get_InformacionGeneralDiferidos
            {
                statuscumplimiento = a.sc,
                mediacion = a.med,
                conciliacion = a.conc,
                junta = a.junta,
            });

        }
        // GET: api/Envios/InformeGeneralAT/
        //[Authorize(Roles = "AMPO-AMP,Administrador,Director,Coordinador")]
        //[HttpGet("[action]/{fechai}/{fechaf}")]
        [HttpGet("[action]/{year}/{mes}")]
        public async Task<IEnumerable<Get_InformacionGeneralDiferidos>> InformeGeneralAT([FromRoute] int year, int mes)
        {
            var acuerdos = await _context.AcuerdoReparatorios
                                .Include(v => v.Envio)
                                .Where(v => v.Envio.FechaRegistro.Year == year)
                                .Where(v => v.Envio.FechaRegistro.Month == mes)
                                .GroupBy(v => v.StatusCumplimiento)
                                .Select(x => new {
                                    sc = x.Key,
                                    med = x.Count(v => v.MetodoUtilizado == "Mediación"),
                                    conc = x.Count(v => v.MetodoUtilizado == "Conciliación"),
                                    junta = x.Count(v => v.MetodoUtilizado == "Junta restaurativa"),
                                })
                             .ToListAsync();

            return acuerdos.Select(a => new Get_InformacionGeneralDiferidos
            {
                statuscumplimiento = a.sc,
                mediacion = a.med,
                conciliacion = a.conc,
                junta = a.junta,
            });

        }
        //************************************************************************************************************
        //************************************************************************************************************



        // GET: api/Envios/InformacionCompleta/fechai/fechaf
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador,Director,Coordinador")]
        [HttpGet("[action]/{fechai}/{fechaf}")]
        public async Task<IEnumerable<GET_EnviosViewModel>> InformacionCompleta([FromRoute] DateTime fechai, DateTime fechaf)
        {
            var Env = await _context.Envios
                          .Where(a => a.AutoridadqueDeriva == "CAT")
                          .Where(a => a.FechaRegistro >= fechai)
                          .Where(a => a.FechaRegistro <= fechaf)
                          .ToListAsync();

            return Env.Select(a => new GET_EnviosViewModel
            {
                IdEnvio = a.IdEnvio,
                ExpedienteId = a.ExpedienteId,
                AutoridadqueDeriva = a.AutoridadqueDeriva,
                uqe_Distrito = a.uqe_Distrito,
                uqe_DirSubProc = a.uqe_DirSubProc,
                uqe_Agencia = a.uqe_Agencia,
                uqe_Modulo = a.uqe_Modulo,
                uqe_Nombre = a.uqe_Nombre,
                uqe_Puesto = a.uqe_Puesto,
                StatusGeneralEnvio = a.StatusGeneral,
                EspontaneoNoEspontaneo = a.EspontaneoNoEspontaneo,
                PrimeraVezSubsecuente = a.PrimeraVezSubsecuente,
                ContadorNODerivacion = a.ContadorNODerivacion,
                FechaRegistro = a.FechaRegistro,
                NoSolicitantes = a.NoSolicitantes,

            });

        }


        //GET: api/Envios/ContarCarpetasiniciadas/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        //[Authorize(Roles = "Administrador,Director,Coordinador,")]
        [HttpGet("[action]/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasModuloViewModel>> ContarCarpetasiniciadas([FromRoute] DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.Envios
                                      .Where(a => a.AutoridadqueDeriva == "CAT")
                                      .Where(a => a.FechaRegistro >= fechai)
                                      .Where(a => a.FechaRegistro <= fechaf)
                                      .GroupBy(v => v.uqe_Modulo)
                                      .Select(x => new {
                                          etiqueta = x.Key
                                      ,
                                          valor1 = x.Count(v => v.uqe_Modulo == x.Key)
                                      })
                                      .ToListAsync();

            return Tabla.Select(v => new EstadisticasModuloViewModel
            {
                Modulo = v.etiqueta.ToString(),
                Envios = v.valor1
            }

            );
        }

        //GET: api/Envios/ContarCarpetasiniciadasFechaMes/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        //[Authorize(Roles = "Administrador,Director,Coordinador,")]
        [HttpGet("[action]/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasFechaViewModel>> ContarCarpetasiniciadasFechaMes([FromRoute] DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.Envios
                                      .Where(a => a.AutoridadqueDeriva == "CAT")
                                      .Where(a => a.FechaRegistro >= fechai)
                                      .Where(a => a.FechaRegistro <= fechaf)
                                      .GroupBy(v => v.FechaRegistro.Day)
                                      .Select(x => new {
                                          etiqueta = x.Key
                                      ,
                                          valor1 = x.Count(v => v.FechaRegistro.Day == x.Key)
                                      })
                                      .ToListAsync();

            return Tabla.Select(v => new EstadisticasFechaViewModel
            {
                Fecha = v.etiqueta,
                Envios = v.valor1
            }

            );
        }
        public string mes(int a)
        {
            if (a == 1) return "Enero";
            if (a == 2) return "Febrero";
            if (a == 3) return "Marzo";
            if (a == 4) return "Abril";
            if (a == 5) return "Mayo";
            if (a == 6) return "Junio";
            if (a == 7) return "Julio";
            if (a == 8) return "Agosto";
            if (a == 9) return "Septiembre";
            if (a == 10) return "Octubre";
            if (a == 11) return "Noviembre";
            else return "Diciembre";


        }

        //GET: api/Envios/ContarCarpetasiniciadasFechaAño/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [Authorize(Roles = "Administrador,Director,Coordinador,")]
        [HttpGet("[action]/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasFechaViewModel>> ContarCarpetasiniciadasFechaAño([FromRoute] DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.Envios
                                      .Where(a => a.AutoridadqueDeriva == "CAT")
                                      .Where(a => a.FechaRegistro >= fechai)
                                      .Where(a => a.FechaRegistro <= fechaf)
                                      .GroupBy(v => v.FechaRegistro.Month)
                                      .Select(x => new {
                                          etiqueta = x.Key
                                      ,
                                          valor1 = x.Count(v => v.FechaRegistro.Month == x.Key)
                                      })
                                      .ToListAsync();

            return Tabla.Select(v => new EstadisticasFechaViewModel
            {
                Fechas = mes(v.etiqueta),
                Envios = v.valor1
            }

            );
        }

        //GET: api/Envios/ContarCarpetasiniciadasFechaAños/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        //[Authorize(Roles = "Administrador,Director,Coordinador,")]
        [HttpGet("[action]/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasFechaViewModel>> ContarCarpetasiniciadasFechaAños([FromRoute] DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.Envios
                                      .Where(a => a.AutoridadqueDeriva == "CAT")
                                      .Where(a => a.FechaRegistro >= fechai)
                                      .Where(a => a.FechaRegistro <= fechaf)
                                      .GroupBy(v => v.FechaRegistro.Year)
                                      .Select(x => new {
                                          etiqueta = x.Key
                                      ,
                                          valor1 = x.Count(v => v.FechaRegistro.Year == x.Key)
                                      })
                                      .ToListAsync();

            return Tabla.Select(v => new EstadisticasFechaViewModel
            {
                Fecha = v.etiqueta,
                Envios = v.valor1
            }

            );
        }



        // API: Detalla la informacion del expediente
        //api/Envios/InformacionStatusAMPO/ 
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{expedienteId}/{envioId}")]
        public async Task<IActionResult> InformacionStatusAMPO([FromRoute] Guid expedienteId, Guid envioId)
        {
            var a = await _context.Envios
                                .Include(x => x.Expediente)
                                .Include(x => x.Expediente.RHecho)
                                .Include(x => x.Expediente.RHecho.NUCs)
                                .Where(x => x.ExpedienteId == expedienteId)
                                .Where(x => x.IdEnvio == envioId)
                                .FirstOrDefaultAsync();


            return Ok(new GET_EnviosAllViewModel
            {
                IdEnvio = a.IdEnvio,
                ExpedienteId = a.ExpedienteId,
                AutoridadqueDeriva = a.AutoridadqueDeriva,
                uqe_Distrito = a.uqe_Distrito,
                uqe_DirSubProc = a.uqe_DirSubProc,
                uqe_Agencia = a.uqe_Agencia,
                uqe_Modulo = a.uqe_Modulo,
                uqe_Nombre = a.uqe_Nombre,
                uqe_Puesto = a.uqe_Puesto,
                StatusGeneral = a.StatusGeneral,
                InfoConclusion = a.InfoConclusion,
                StatusAMPO = a.StatusAMPO,
                InformaAMPO = a.InformaAMPO,
                RespuestaExpediente = a.RespuestaExpediente,
                EspontaneoNoEspontaneo = a.EspontaneoNoEspontaneo,
                PrimeraVezSubsecuente = a.PrimeraVezSubsecuente,
                ContadorNODerivacion = a.ContadorNODerivacion,
                FechaRegistro = a.FechaRegistro,
                FechaCierre = a.FechaCierre,
                NoSolicitantes = a.NoSolicitantes,
                FirmaInfoAMPO = a.FirmaInfoAMPO,
                PuestoFirmaAMPO = a.PuestoFirmaAMPO,
                // EXPEDIENTE
                NoExpediente = a.Expediente.NoExpediente,
                StatusAcepRech = a.Expediente.StatusAcepRech,

                // INFORMACION DEL HECHO 
                NUC = a.Expediente.RHecho.NUCs.nucg,
                oficioAMPO = a.oficioAMPO,


            });
        }


        // API: Detalla la informacion del expediente
        //api/Envios/InformacionStatusAMPO/ 
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{envioId}")]
        public async Task<IActionResult> InformacionFirmaAMPO([FromRoute] Guid envioId)
        {
            var a = await _context.Envios
                                .Where(x => x.IdEnvio == envioId)
                                .FirstOrDefaultAsync();

            return Ok(new ModelFirmaAMPO
            {
                IdEnvio = a.IdEnvio,
                FirmaInfoAMPO = a.FirmaInfoAMPO,
                PuestoFirmaAMPO = a.PuestoFirmaAMPO


            });
        }

        // PUT: api/Envios/StatusEnvioDerivacion
        //[Authorize(Roles = "Director, Administrador, AMPO-AMP, AMPO-AMP Mixto, AMPO-AMP Detenido, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> StatusEnvioDerivacion([FromBody] GET_EnviosAllViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var envio = await _context.Envios.FirstOrDefaultAsync(x => x.IdEnvio == model.IdEnvio);

                if (envio == null)
                {
                    return NotFound(new { message = "Envío no encontrado" });
                }

                envio.statusEnvio = model.statusEnvio;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Estado de envío actualizado correctamente" });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción inesperada
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el estado de envío", error = ex.Message });
            }
        }
    }
}