using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using SIIGPP.Entidades.M_JR.RAsignacionEnvios;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_JR.RExpediente;
using SIIGPP.Entidades.M_JR.RRegistros;
using SIIGPP.JR.Models.RAcuerdoReparatorio;
using SIIGPP.JR.Models.RAsignacionEnvios;
using SIIGPP.JR.Models.RCitatorioRecordatorio;
using SIIGPP.JR.Models.REnvio;

namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionEnviosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public AsignacionEnviosController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/AsignacionEnvios/CrearAsingacion
        // API: SE ASIGNA EL ENVIO AL MODULO
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearAsingacion([FromBody] POST_AsignacionEnvioViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                AsignacionEnvio ae = new AsignacionEnvio
                {
                    EnvioId = model.EnvioId,
                    ModuloServicioId = model.ModuloServicioId,

                };
                _context.AsignacionEnvios.Add(ae);

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

        // POST: api/AsignacionEnvios/CrearAsingacion
        // API: SE ASIGNA EL ENVIO AL MODULO
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearAsignacion([FromBody] POST_AsignacionEnvioViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                foreach (var det in model.Asignaciones)
                {
                    AsignacionEnvio ae = new AsignacionEnvio
                    {
                        EnvioId = det.EnvioId,
                        ModuloServicioId = det.ModuloServicioId,

                    };
                    _context.AsignacionEnvios.Add(ae);
                }
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

         // POST: api/AsignacionEnvios/AsignarFacilitadorNotificador
         [HttpPost("[action]")]
         public async Task<IActionResult> AsignarFacilitadorNotificador([FromBody] AsignacionFacilitadorNotificador model)
         {
             try
             {
                 if (!ModelState.IsValid)
                 {
                     return BadRequest(ModelState);
                 }
                 foreach (var det in model.ModuloServicioIdFacilitadorNotificador)
                 {
                     AsignacionEnvio ae = new AsignacionEnvio
                     {
                         EnvioId = model.EnvioId,
                         ModuloServicioId = det.ModuloServicioId,
 
                     };
                     _context.AsignacionEnvios.Add(ae); 
                 } 
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

        //MODIFICACIONES DE ASIGNACION A JR
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearAsingacion2([FromBody] POST_AsignacionEnvioViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Agrega la asignación del facilitador
                AsignacionEnvio facilitador = new AsignacionEnvio
                {
                    EnvioId = model.EnvioId,
                    ModuloServicioId = model.ModuloServicioId,
                };

                _context.AsignacionEnvios.Add(facilitador);

                // Agrega la asignación del notificador
                foreach (var det in model.Asignaciones)
                {
                    AsignacionEnvio notificador = new AsignacionEnvio
                    {
                        EnvioId = model.EnvioId,
                        ModuloServicioId = det.ModuloServicioId,
                    };

                    _context.AsignacionEnvios.Add(notificador);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return BadRequest(ex);
            }
            return Ok();
        }

        //GET: api/AsignacionEnvios/ListarMisEnvios
        // API: LISTA LOS EXPEDIENTES  FILTRADOS POR NOTIFICADOR FACILITADOR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{moduloServicioId}")]
        public async Task<IEnumerable<GET_MisEnviosViewModel>> ListarMisEnvios([FromRoute] Guid moduloServicioId)
        {
            var Tabla = await _context.AsignacionEnvios
                        .Include(a => a.Envio.Expediente.RHecho)
                        .Include(a => a.Envio.Expediente.RHecho.NUCs)
                        .Include(a => a.ModuloServicio)
                        .Where(a => a.ModuloServicioId == moduloServicioId)
                        .OrderBy(a => a.Envio.IdEnvio) // Ordena por el Id de Envio
                        .ToListAsync();
            Tabla = Tabla
                    .GroupBy(a => a.Envio.IdEnvio) // Agrupa por el Id de Envio
                    .Select(g => g.First()) // Toma el primer elemento de cada grupo
                    .ToList();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_MisEnviosViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_MisEnviosViewModel
                {
                    IdEnvio = a.EnvioId,
                    ExpedienteId = a.Envio.ExpedienteId,
                    TipoModulo = a.ModuloServicio.Tipo,
                    AutoridadqueDeriva = a.Envio.AutoridadqueDeriva,
                    uqe_Distrito = a.Envio.uqe_Distrito,
                    uqe_DirSubProc = a.Envio.uqe_DirSubProc,
                    uqe_Agencia = a.Envio.uqe_Agencia,
                    uqe_Modulo = a.Envio.uqe_Modulo,
                    uqe_Nombre = a.Envio.uqe_Nombre,
                    uqe_Puesto = a.Envio.uqe_Puesto,
                    StatusGeneralEnvio = a.Envio.StatusGeneral,
                    EspontaneoNoEspontaneo = a.Envio.EspontaneoNoEspontaneo,
                    PrimeraVezSubsecuente = a.Envio.PrimeraVezSubsecuente,
                    ContadorNODerivacion = a.Envio.ContadorNODerivacion,
                    FechaRegistro = a.Envio.FechaRegistro,
                    NoSolicitantes = a.Envio.NoSolicitantes,
                    RHechoId = a.Envio.Expediente.RHechoId,
                    NoExpediente = a.Envio.Expediente.NoExpediente,
                    NoDerivacion = a.Envio.Expediente.NoDerivacion,
                    StatusGeneral = a.Envio.StatusGeneral,
                    FechaRegistroExpediente = a.Envio.Expediente.FechaRegistroExpediente,
                    NUC = a.Envio.Expediente.RHecho.NUCs.nucg,
                    FechaHoraSuceso = a.Envio.Expediente.RHecho.FechaHoraSuceso,
                    ReseñaBreve = a.Envio.Expediente.RHecho.RBreve,
                });
            }
        }

        //GET: api/AsignacionEnvios/ListarMisEnviosME
        // API: LISTA LOS EXPEDIENTES  FILTRADOS POR NOTIFICADOR FACILITADOR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{moduloServicioId}/{envioId}")]
        public async Task<IActionResult> ListarMisEnviosME([FromRoute] Guid moduloServicioId, Guid envioId)
        {
            var Tabla = await _context.AsignacionEnvios
                                .Include(a => a.Envio.Expediente.RHecho)
                                .Include(a => a.Envio.Expediente.RHecho.NUCs)
                                .Include(a => a.ModuloServicio)
                                .Where(a => a.ModuloServicioId == moduloServicioId)
                                .Where(a => a.EnvioId == envioId)
                                .OrderBy(a => a.Envio.IdEnvio)
                                .FirstOrDefaultAsync();
            if (Tabla == null)
            {
                return NotFound("No hay registros");
            }
            return Ok(new GET_MisEnviosViewModel
            {
                IdEnvio = Tabla.EnvioId,
                ExpedienteId = Tabla.Envio.ExpedienteId,
                TipoModulo = Tabla.ModuloServicio.Tipo,
                AutoridadqueDeriva = Tabla.Envio.AutoridadqueDeriva,
                uqe_Distrito = Tabla.Envio.uqe_Distrito,
                uqe_DirSubProc = Tabla.Envio.uqe_DirSubProc,
                uqe_Agencia = Tabla.Envio.uqe_Agencia,
                uqe_Modulo = Tabla.Envio.uqe_Modulo,
                uqe_Nombre = Tabla.Envio.uqe_Nombre,
                uqe_Puesto = Tabla.Envio.uqe_Puesto,
                StatusGeneralEnvio = Tabla.Envio.StatusGeneral,
                EspontaneoNoEspontaneo = Tabla.Envio.EspontaneoNoEspontaneo,
                PrimeraVezSubsecuente = Tabla.Envio.PrimeraVezSubsecuente,
                ContadorNODerivacion = Tabla.Envio.ContadorNODerivacion,
                FechaRegistro = Tabla.Envio.FechaRegistro,
                NoSolicitantes = Tabla.Envio.NoSolicitantes,

                RHechoId = Tabla.Envio.Expediente.RHechoId,
                NoExpediente = Tabla.Envio.Expediente.NoExpediente,
                NoDerivacion = Tabla.Envio.Expediente.NoDerivacion,
                StatusGeneral = Tabla.Envio.StatusGeneral,
                FechaRegistroExpediente = Tabla.Envio.Expediente.FechaRegistroExpediente,

                NUC = Tabla.Envio.Expediente.RHecho.NUCs.nucg,
                FechaHoraSuceso = Tabla.Envio.Expediente.RHecho.FechaHoraSuceso,
                ReseñaBreve = Tabla.Envio.Expediente.RHecho.RBreve,
            });
        }
     
        //GET: api/AsignacionEnvios/ListarIdEnvio
        // API: LISTA LOS EXPEDIENTES  FILTRADOS POR NOTIFICADOR FACILITADOR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{idEnvio}")] 
        public async Task<IActionResult> ListarIdEnvio([FromRoute] Guid IdEnvio)
        {
            var tabla = await _context.AsignacionEnvios
                                .Include(a =>a.ModuloServicio)
                                .Where(a => a.EnvioId == IdEnvio)
                                .Where(a => a.ModuloServicio.Tipo == "Notificador")
                                .FirstOrDefaultAsync();
            if (tabla == null)
            {
                return  BadRequest( );
            } 

            return Ok(new GET_EnvioNotificadorViewModel
            {
                idModulo = tabla.ModuloServicioId,
                un_Modulo = tabla.ModuloServicio.Nombre, 

            });
        }

        [HttpGet("[action]/{idEnvio}")]
        public async Task<IActionResult> ListarIdEnvio2([FromRoute] Guid IdEnvio)
        {
            var tabla = await _context.AsignacionEnvios
                                .Include(a => a.ModuloServicio)
                                .Where(a => a.EnvioId == IdEnvio)
                                .Where(a => a.ModuloServicio.Tipo != "Notificador")
                                .FirstOrDefaultAsync();
            if (tabla == null)
            {
                return Ok(new { un_Modulo = "Sin asignación"});
            }

            return Ok(new GET_EnvioNotificadorViewModel
            {
                idModulo = tabla.ModuloServicioId,
                un_Modulo = tabla.ModuloServicio.Nombre,
            });
        }

        //GET: api/AsignacionEnvios/ListaIdEnvioAll
        // API: LISTA LOS EXPEDIENTES  FILTRADOS POR NOTIFICADOR FACILITADOR
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{envioId}")]
        public async Task<IEnumerable<GET_AsignacionEnvioviewModel>> ListarIdEnvioAll([FromRoute] Guid envioId)
        {
            var Tabla = await _context.AsignacionEnvios 
                                .Where(a => a.EnvioId == envioId)
                                .OrderBy(a => a.ModuloServicio.Nombre)
                                .GroupBy(v => v.EnvioId)
                                    .Select(x =>new
                                    {
                                        envio =x.Key,
                                        modulo1 = x.Select(v=> v.ModuloServicioId), 
                                    })
                                .ToListAsync();

            return Tabla.Select(v => new GET_AsignacionEnvioviewModel
            {
                envioId = v.envio,
                modulo1 = v.modulo1.First(),
                modulo2 = v.modulo1.Last(),
            }); 
        }

        //Api para listar todos los expedientes de determinado distrito
        //GET: api/AsignacionEnvios/ListarTodoExpedienteXDistrito
        [Authorize(Roles = "Director, Administrador, Coordinador, USAR")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IActionResult> ListarTodoExpedienteXDistrito([FromRoute] Guid distritoId)
        {
            try
            {
                //Por medio de esta conexion se hace la conexion a la ip del otro servidor, comprobar que la cadena de conexiones del back de jr tenga todas las conexiones
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + distritoId.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    // Realizar la consulta deseada
                    var Tabla = await ctx.AsignacionEnvios
                        .Include(a => a.Envio.Expediente.RHecho)
                        .Include(a => a.Envio.Expediente.RHecho.NUCs)
                        .Include(a => a.ModuloServicio)
                        .Where(a => a.Envio.Expediente.DistritoIdDestino == distritoId)
                        .OrderBy(a => a.Envio.IdEnvio)
                        .ToListAsync();

                    // Devolver el resultado de la consulta
                    return Ok(Tabla.Select(a => new GET_MisEnviosViewModel
                    {
                        IdEnvio = a.EnvioId,
                        ExpedienteId = a.Envio.ExpedienteId,
                        TipoModulo = a.ModuloServicio.Tipo,
                        AutoridadqueDeriva = a.Envio.AutoridadqueDeriva,
                        uqe_Distrito = a.Envio.uqe_Distrito,
                        uqe_DirSubProc = a.Envio.uqe_DirSubProc,
                        uqe_Agencia = a.Envio.uqe_Agencia,
                        uqe_Modulo = a.Envio.uqe_Modulo,
                        uqe_Nombre = a.Envio.uqe_Nombre,
                        uqe_Puesto = a.Envio.uqe_Puesto,
                        StatusGeneralEnvio = a.Envio.StatusGeneral,
                        EspontaneoNoEspontaneo = a.Envio.EspontaneoNoEspontaneo,
                        PrimeraVezSubsecuente = a.Envio.PrimeraVezSubsecuente,
                        ContadorNODerivacion = a.Envio.ContadorNODerivacion,
                        FechaRegistro = a.Envio.FechaRegistro,
                        NoSolicitantes = a.Envio.NoSolicitantes,

                        RHechoId = a.Envio.Expediente.RHechoId,
                        NoExpediente = a.Envio.Expediente.NoExpediente,
                        NoDerivacion = a.Envio.Expediente.NoDerivacion,
                        StatusGeneral = a.Envio.StatusGeneral,
                        FechaRegistroExpediente = a.Envio.Expediente.FechaRegistroExpediente,

                        NUC = a.Envio.Expediente.RHecho.NUCs.nucg,
                        FechaHoraSuceso = a.Envio.Expediente.RHecho.FechaHoraSuceso,
                        ReseñaBreve = a.Envio.Expediente.RHecho.RBreve,
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

        //EP para listar los acuerdos reparatorios de una carpeta
        //GET: api/AsignacionEnvios/ListarARXDistritoXRhechoid
        [Authorize(Roles = "Director, Administrador, Coordinador, USAR, AMPO-AMP, AMPO-IL, AMPO-AMP Mixto")]
        [HttpGet("[action]/{distritoId}/{rhechoid}")]
        public async Task<IActionResult> ListarARXDistritoXRhechoid([FromRoute] Guid rhechoId)
        {
            try
            {
                var derivacion = await _context.Expedientes
                    .Where(x => x.RHechoId == rhechoId)                    
                    .Select(x => x.DistritoIdDestino)
                    .Take(1)
                    .ToListAsync();

                if (derivacion != null)
                {
                    //Por medio de esta conexion se hace la conexion a la ip del otro servidor, comprobar que la cadena de conexiones del back de jr tenga todas las conexiones
                    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + derivacion[0].ToString().ToUpper())).Options;
                    using (var ctx = new DbContextSIIGPP(options))
                    {
                        string busquedaAR = @"select * from JR_ACUERDOREPARATORIO ja join JR_ACUERDOS_CONJUNTOS jac  on ja.IdAcuerdoReparatorio = jac.AcuerdoReparatorioId where EnvioId in (select IdEnvio from JR_ENVIO je where ExpedienteId in (select IdExpediente from JR_EXPEDIENTE je where RHechoId = @rhId)) and ja.StatusRespuestaCoordinadorJuridico = 'Autorizado'";

                        List<SqlParameter> bar = new List<SqlParameter>();
                        bar.Add(new SqlParameter("@rhId", rhechoId));

                        var listAR = await ctx.ACARRaws.FromSqlRaw(busquedaAR, bar.ToArray()).ToListAsync();

                        foreach (var item in listAR)
                        {
                            string busquedaA = @"select * from JR_ACUERDOREPARATORIO ja where Anexo = @acuerdo ORDER BY FechaCelebracionAcuerdo ASC";

                            List<SqlParameter> a = new List<SqlParameter>();
                            a.Add(new SqlParameter("@acuerdo", item.IdAcuerdoReparatorio));
                            var listaA = await _context.AcuerdoReparatorios.FromSqlRaw(busquedaA, a.ToArray()).ToListAsync();

                            if (listaA == null || !listaA.Any())
                            {
                                item.Attached = null;
                            }
                            else
                            {
                                item.Attached = listaA;
                            }
                        }

                        return Ok(listAR.Select(a => new GET_ARConjuntoViewModel
                        {
                            IdAcuerdoReparatorio = a.IdAcuerdoReparatorio,
                            TipoDocumento = a.TipoDocumento,
                            EnvioId = a.EnvioId,
                            NombreSolicitante = a.NombreSolicitante,
                            NombreRequerdio = a.NombreRequerdio,
                            Delitos = a.Delitos,
                            NUC = a.NUC,
                            nosise = a.Sise,
                            fechasise = a.Fechasise,
                            NoExpediente = a.NoExpediente,
                            StatusConclusion = a.StatusConclusion,
                            StatusCumplimiento = a.StatusCumplimiento,
                            MetodoUtilizado = a.MetodoUtilizado,
                            TipoPago = a.TipoPago,
                            MontoTotal = a.MontoTotal,
                            ObjectoEspecie = a.ObjectoEspecie,
                            NoTotalParcialidades = a.NoTotalParcialidades,
                            Periodo = a.Periodo,
                            FechaCelebracionAcuerdo = a.FechaCelebracionAcuerdo,
                            FechaLimiteCumplimiento = a.FechaLimiteCumplimiento,
                            StatusRespuestaCoordinadorJuridico = a.StatusRespuestaCoordinadorJuridico,
                            RespuestaCoordinadorJuridico = a.RespuestaCoordinadorJuridico,
                            FechaHoraRespuestaCoordinadorJuridico = a.FechaHoraRespuestaCoordinadorJuridico,
                            StatusRespuestaAMP = a.StatusRespuestaAMP,
                            RespuestaAMP = a.RespuestaAMP,
                            FechaRespuestaAMP = a.FechaRespuestaAMP,
                            TextoAR = a.TextoAR,
                            uf_Distrito = a.uf_Distrito,
                            uf_DirSubProc = a.uf_DirSubProc,
                            uf_Agencia = a.uf_Agencia,
                            uf_Modulo = a.uf_Modulo,
                            uf_Nombre = a.uf_Nombre,
                            uf_Puesto = a.uf_Puesto,
                            IdAC = a.IdAC,
                            ConjuntoDerivacionesId = a.ConjuntoDerivacionesId,
                            Attached = a.Attached
                        }));
                    }
                }

                return Ok(new { estatus = false });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        //GET: api/AsignacionEnvios/ContadorRegistrados
        // API: LISTA LOS EXPEDIENTES  FILTRADOS POR NOTIFICADOR FACILITADOR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{ano}")]
        public async Task<IEnumerable<Models.RAsignacionEnvios.GET_ListarTodosViewModel>> ContadorRegistrados([FromRoute] int ano)
        {
            var Tabla = await _context.AsignacionEnvios
                                      .Where(v => v.Envio.FechaRegistro.Year == ano)
                                      .GroupBy(v => v.Envio.FechaRegistro.Month)  
                                      //.Select(x=> new { etiqueta=x.Key, valor=x.Count(v=> v.EnvioId != null) })
                                      .Select(x => new { etiqueta = x.Key, valor = x.Count() })
                                      .ToListAsync();
 
            return Tabla.Select(v => new Models.RAsignacionEnvios.GET_ListarTodosViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valor = v.valor
            }); 
        }

        //GET: api/AsignacionEnvios/ListarTodos
        //API: LISTA LOS EXPEDIENTES  FILTRADOS POR NOTIFICADOR FACILITADOR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GET_TodosEnviosViewModel>> ListarTodos()
        {
            var Tabla = await _context.AsignacionEnvios
                                .Include(a => a.Envio)
                                .Include(a => a.Envio.Expediente.RHecho.NUCs)
                                .Include(a => a.Envio.Expediente)
                                .Include(a => a.ModuloServicio)    
                                .ToListAsync();
             
            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_TodosEnviosViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_TodosEnviosViewModel
                {
                    IdAsingacionEnvio = a.IdAsingacionEnvio,
                    EnvioId = a.EnvioId,
                    ModuloServicioId = a.ModuloServicioId,
                    NombreModulo = a.ModuloServicio.Nombre,
                    ExpedienteId = a.Envio.ExpedienteId,
                    NoExpediente = a.Envio.Expediente.NoExpediente,
                    TipoModulo = a.ModuloServicio.Tipo, 
                    AutoridadqueDeriva = a.Envio.AutoridadqueDeriva,
                    uqe_Distrito = a.Envio.uqe_Distrito,
                    uqe_DirSubProc = a.Envio.uqe_DirSubProc,
                    uqe_Agencia = a.Envio.uqe_Agencia,
                    uqe_Modulo = a.Envio.uqe_Modulo,
                    uqe_Nombre = a.Envio.uqe_Nombre,
                    uqe_Puesto = a.Envio.uqe_Puesto,
                    StatusGeneralEnvio = a.Envio.StatusGeneral,
                    EspontaneoNoEspontaneo = a.Envio.EspontaneoNoEspontaneo,
                    PrimeraVezSubsecuente = a.Envio.PrimeraVezSubsecuente,
                    ContadorNODerivacion = a.Envio.ContadorNODerivacion,
                    FechaRegistro = a.Envio.FechaRegistro,
                    NoSolicitantes = a.Envio.NoSolicitantes,

                    NUC = a.Envio.Expediente.RHecho.NUCs.nucg,
                });
            }
        }

        //GET: api/AsignacionEnvios/ListarTodosAgrupar
        //API: LISTA LOS EXPEDIENTES  FILTRADOS POR NOTIFICADOR FACILITADOR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{idDistrito}")]
        public async Task<IEnumerable<GET_AsignacionExpedientesViewModel>> ListarTodosAgrupar([FromRoute] Guid idDistrito)
        {
            var consulta = await _context.AsignacionEnvios
                                .Include(a => a.Envio)
                                .Include(a => a.Envio.Expediente.RHecho.NUCs)
                                .Include(a => a.Envio.Expediente)
                                .Include(a => a.ModuloServicio) 
                                .Where(a => a.Envio.Expediente.RHecho.NUCs.DistritoId == idDistrito)
                                .OrderBy(a => a.ModuloServicio.Nombre)
                                .GroupBy(v => v.EnvioId)
                                
                                .Select(x => new
                                {
                                    envioId = x.Key, 
                                    expedienteid= x.Select(v => v.Envio.ExpedienteId),
                                    noExpediente = x.Select(v=> v.Envio.Expediente.NoExpediente),
                                    autoridadqueDeriva = x.Select(v => v.Envio.AutoridadqueDeriva),
                                    fechaRegistro = x.Select(v => v.Envio.FechaRegistro),
                                    moduloId = x.Select(v => v.ModuloServicioId), 
                                    asignacionId = x.Select(v => v.IdAsingacionEnvio),
                                    facilitador = x.Select(v => v.ModuloServicio.Nombre),
                                    notificador = x.Select(v=> v.ModuloServicio.Nombre), 
                                    statusGeneralEnvio =  x.Select(v => v.Envio.StatusGeneral), 
                                    contadorNODerivacion = x.Select(v => v.Envio.ContadorNODerivacion), 
                                    nuc = x.Select(v => v.Envio.Expediente.RHecho.NUCs.nucg),
                                })                               
                                .ToListAsync();

            var c = consulta.Select(a => new GET_AsignacionExpedientesViewModel
            {
                EnvioId = a.envioId,
                asignacionId1 = a.asignacionId.First(),
                asignacionId2 = a.asignacionId.Last(),
                ModuloId1 = a.moduloId.First(),
                ModuloId2 = a.moduloId.Last(),
                NoExpediente =a.noExpediente.First(),
                AutoridadqueDeriva = a.autoridadqueDeriva.First(),  
                facilitador = a.facilitador.First(),
                notificador = a.notificador.Last(),
                StatusGeneralEnvio = a.statusGeneralEnvio.First(), 
                ContadorNODerivacion = a.contadorNODerivacion.First(), 
                FechaRegistro = a.fechaRegistro.First(),
                NUC = a.nuc.First()
            });
            return c;
        }

        // PUT: api/AsignacionEnvios/Reasignacion
       // [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Reasignacion([FromBody] PUT_ReasignacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var db = await _context.AsignacionEnvios.FirstOrDefaultAsync(a => a.IdAsingacionEnvio == model.IdAsingacionEnvio);

            if (db == null)
            {
                return NotFound();
            }

            db.ModuloServicioId = model.ModuloServicioId;

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

        [Authorize(Roles = "Director, Administrador")]
        [HttpGet("[action]/{envioId}")]
        public async Task<IActionResult> ListarFacilitadorEnv([FromRoute] Guid envioId)
        {
            try
            {
                var tabla1 = await _context.AsignacionEnvios
                                   .Include(a => a.ModuloServicio)
                                   .Where(a => a.EnvioId == envioId)
                                    .ToListAsync();

                return Ok(tabla1.Select( v => new GET_EnvioxFacilitador
                {
                    idAsignacionEnvio = v.IdAsingacionEnvio,
                    envioId = v.EnvioId,
                    ModuloServicioId = v.ModuloServicioId,
                    Tipo = v.ModuloServicio.Tipo,

                }));
            }
            catch (Exception ex)
            {
                // Guardar Excepción
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }
    }
}