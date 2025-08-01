using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;
using SIIGPP.JR.Models.RSeguimientoCumplimiento;
using SIIGPP.Entidades.M_JR.RSeguimientoCumplimiento;
using SIIGPP.JR.Models.RAcuerdoReparatorio;
using Microsoft.Data.SqlClient;
using SIIGPP.JR.Models.RExpediente;
using SIIGPP.Entidades.M_Cat.DDerivacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIIGPP.Entidades.M_JR.RSesion;
using SIIGPP.Entidades.M_JR.REnvio;
using Microsoft.Extensions.Configuration;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.JR.Models.RSolicitanteRequerido;


namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcuerdoReparatoriosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IConfiguration _configuration;

        public AcuerdoReparatoriosController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //GET: api/AcuerdoReparatorio/AcuerdoPorConjunto
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{ConjuntoDerivacionId}")]
        public async Task<IActionResult> AcuerdoPorConjunto([FromRoute] Guid ConjuntoDerivacionId)
        {
            var acuerdo = await _context.AcuerdosConjuntos
                                        .Where(a => a.ConjuntoDerivacionesId == ConjuntoDerivacionId)
                                        .FirstOrDefaultAsync();

            if(acuerdo == null)
            {
                return Ok(new {acuerdo = false});
            }
            else
            {
                return Ok(new { acuerdo = true});
            }

        }

        // GET: api/AcuerdoReparatorios/ListaAR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{envioId}")]
        public async Task<IActionResult> ListaAR([FromRoute] Guid envioId)
        {
            var a = await _context.AcuerdoReparatorios
                                      .Where(x => x.EnvioId == envioId)
                                      .Include(x => x.Envio)
                                      .FirstOrDefaultAsync();

            if (a == null)
            {

                return Ok(new { NoHayAR = 1 });
            }
            return Ok(new GET_AcuerdoReparatorioViewModel
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
            });
        }

        // GET: api/AcuerdoReparatorios/ListaAR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{envioId}/{acuerdoreparatorioid}")]
        public async Task<IActionResult> ListaARSeguimiento([FromRoute] Guid envioId, Guid acuerdoreparatorioid)
        {
            var a = await _context.AcuerdoReparatorios
                                      .Where(x => x.EnvioId == envioId)
                                      .Where(x => x.IdAcuerdoReparatorio == acuerdoreparatorioid)
                                      .Include(x => x.Envio)
                                      .FirstOrDefaultAsync();

            if (a == null)
            {

                return Ok(new { NoHayAR = 1 });
            }
            return Ok(new GET_AcuerdoReparatorioViewModel
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
            });
        }

        //GET: api/AcuerdoReparatorios/ExistsAR
        [HttpGet("[action]/{envioId}")]
        public async Task<IActionResult> ExistsAR([FromRoute] Guid envioId)
        {
            // Usa la versión asincrónica de Where y CountAsync
            var liveCount = await _context.AcuerdoReparatorios
                                          .Where(x => x.EnvioId == envioId)
                                          .CountAsync();

            if (liveCount == 0)
                return Ok(new { status = false });

            return Ok(new { status = true, cantidad = liveCount });
        }


        //GET: api/AcuerdoReparatorios/ExistsAN
        [HttpGet("[action]/{acuerdoId}")]
        public async Task<IActionResult> ExistsAN([FromRoute] Guid acuerdoId)
        {
            // Usamos CountAsync para hacer la consulta de manera asincrónica
            var liveCount = await _context.AcuerdoReparatorios
                                          .Where(x => x.Anexo == acuerdoId)
                                          .CountAsync();

            if (liveCount == 0)
                return Ok(new { status = false });

            return Ok(new { status = true, cantidad = liveCount });
        }


        //GET: api/AcuerdoReparatorios/CheckSessions
        [HttpGet("[action]/{envioId}")]
        public async Task<IActionResult> CheckSessions([FromRoute] Guid envioId)
        {
            // Usamos CountAsync para hacer la consulta de manera asincrónica
            var sessionCount = await _context.Sesions
                                             .Where(x => x.EnvioId == envioId)
                                             .Where(x => x.StatusSesion == "Se realiza sesión con acuerdo reparatorio")
                                             .CountAsync();

            if (sessionCount == 0)
                return Ok(new { C = false });

            return Ok(new { C = true });
        }


        // GET: api/AcuerdoReparatorios/ListarValidacion 
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GET_AcuerdoReparatorioViewModel>> ListarValidacion()
        {
                    var Tabla = await _context.AcuerdoReparatorios
                            .Include(x => x.Envio)
                            .OrderByDescending(x => x.IdAcuerdoReparatorio)
                            .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_AcuerdoReparatorioViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_AcuerdoReparatorioViewModel
                {
                    IdAcuerdoReparatorio = a.IdAcuerdoReparatorio,
                    EnvioId = a.EnvioId,
                    NombreSolicitante = a.NombreSolicitante,
                    NombreRequerdio = a.NombreRequerdio,
                    Delitos = a.Delitos,
                    NUC = a.NUC,
                    NoExpediente = a.NoExpediente,
                    StatusConclusion = a.StatusConclusion,
                    StatusCumplimiento = a.StatusCumplimiento,
                    MetodoUtilizado = a.MetodoUtilizado,
                    TipoPago = a.TipoPago,
                    TipoDocumento = a.TipoDocumento,
                    MontoTotal = a.MontoTotal,
                    ObjectoEspecie = a.ObjectoEspecie,
                    NoTotalParcialidades = a.NoTotalParcialidades,
                    Periodo = a.Periodo,
                    FechaCelebracionAcuerdo = a.FechaCelebracionAcuerdo,
                    FechaLimiteCumplimiento = a.FechaLimiteCumplimiento,
                    StatusRespuestaCoordinadorJuridico = a.StatusRespuestaCoordinadorJuridico,
                    RespuestaCoordinadorJuridico = a.StatusRespuestaCoordinadorJuridico,
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
                    nosise = a.Sise,
                    fechasise = a.Fechasise,
                    AutoridadqueDeriva = a.Envio.AutoridadqueDeriva,
                    uqe_Distrito = a.Envio.uqe_Distrito,
                    uqe_DirSubProc = a.Envio.uqe_DirSubProc,
                    uqe_Agencia = a.Envio.uqe_Agencia,
                    uqe_Modulo = a.Envio.uqe_Modulo,
                    uqe_Nombre = a.Envio.uqe_Nombre,
                    uqe_Puesto = a.Envio.uqe_Puesto,
                    StatusGeneral = a.Envio.StatusGeneral,
                    RespuestaExpediente = a.Envio.RespuestaExpediente,
                    FechaRegistro = a.Envio.FechaRegistro,
                    FechaCierre = a.Envio.FechaCierre,
                    anexo = a.Anexo
                });
            }
            }

        //GET: api/AcuerdoReparatorios/ContadorRegistrados
        // API: LISTA LOS EXPEDIENTES  FILTRADOS POR NOTIFICADOR FACILITADOR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        //[HttpGet("[action]/{ano}")]
        public async Task<IEnumerable<GET_ListarTodosViewModel>> ContadorRegistrados([FromRoute] int ano)
        {
            var Tabla = await _context.AcuerdoReparatorios
                                      .Where(v => v.Envio.FechaRegistro.Year == ano)
                                      .GroupBy(v => v.Envio.FechaRegistro.Month)
                                      //.Select(x => new { etiqueta = x.Key, valor = x.Count(v => v.EnvioId != null) })
                                      .Select(x => new { etiqueta = x.Key, valor = x.Count() })
                                      .ToListAsync();

            return Tabla.Select(v => new GET_ListarTodosViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valor = v.valor

            });
        }
        //*****************************************************************************************************************
        //*****************************************************************************************************************
        // GET: api/AcuerdoReparatorios/ListarAREstadisticaAño
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{año}")]
        public async Task<IEnumerable<GET_AcuerdoReparatorioEstadisticaViewModel>> ListarAREstadisticaAño([FromRoute] int año)
        {


            var consulta1 = await _context.AcuerdoReparatorios
                                        .Include(v => v.Envio)
                                        .Where(v => v.Envio.FechaRegistro.Year == año)
                                        .GroupBy(v => v.uf_Nombre)
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            tramite = x.Count(v => v.Envio.StatusGeneral == "Tramite"),
                                            baja = x.Count(v => v.Envio.StatusGeneral == "Baja"),
                                            inmediato = x.Count(v => v.StatusConclusion == "Inmediato"),
                                            diferido = x.Count(v => v.StatusConclusion == "Diferido"),
                                            cumplido = x.Count(v => v.StatusCumplimiento == "Cumplido"),
                                            incumpliedo = x.Count(v => v.StatusCumplimiento == "Incumplido"),
                                            encumplimiento = x.Count(v => v.StatusCumplimiento == "En cumplimiento"),
                                        })
                                        .ToListAsync();


            return consulta1.Select(v => new GET_AcuerdoReparatorioEstadisticaViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                tramite = v.tramite,
                baja = v.baja,
                inmediato = v.inmediato,
                diferido = v.diferido,
                cumplido = v.cumplido,
                incumplido = v.incumpliedo,
                encumplimiento = v.encumplimiento,

            });



        }

        // GET: api/AcuerdoReparatorios/ListarAREstadisticaMes
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{año}/{mes}")]
        public async Task<IEnumerable<GET_AcuerdoReparatorioEstadisticaViewModel>> ListarAREstadisticaMes([FromRoute] int año, int mes)
        {


            var consulta1 = await _context.AcuerdoReparatorios
                                        .Include(v => v.Envio)
                                        .Where(v => v.Envio.FechaRegistro.Year == año)
                                        .Where(v => v.Envio.FechaRegistro.Month == mes)
                                        .GroupBy(v => v.uf_Nombre)
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            tramite = x.Count(v => v.Envio.StatusGeneral == "Tramite"),
                                            baja = x.Count(v => v.Envio.StatusGeneral == "Baja"),
                                            inmediato = x.Count(v => v.StatusConclusion == "Inmediato"),
                                            diferido = x.Count(v => v.StatusConclusion == "Diferido"),
                                            cumplido = x.Count(v => v.StatusCumplimiento == "Cumplido"),
                                            incumpliedo = x.Count(v => v.StatusCumplimiento == "Incumplido"),
                                            encumplimiento = x.Count(v => v.StatusCumplimiento == "En cumplimiento"),
                                        })
                                        .ToListAsync();


            return consulta1.Select(v => new GET_AcuerdoReparatorioEstadisticaViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                tramite = v.tramite,
                baja = v.baja,
                inmediato = v.inmediato,
                diferido = v.diferido,
                cumplido = v.cumplido,
                incumplido = v.incumpliedo,
                encumplimiento = v.encumplimiento,
            });
        }

        // GET: api/AcuerdoReparatorios/ListarAREstadisticaDia
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{fi}/{ff}")]
        public async Task<IEnumerable<GET_AcuerdoReparatorioEstadisticaViewModel>> ListarAREstadisticaDia([FromRoute] DateTime fi, DateTime ff)
        {
            var consulta1 = await _context.AcuerdoReparatorios
                                        .Include(v => v.Envio)
                                        .Where(v => v.Envio.FechaRegistro >= fi)
                                        .Where(v => v.Envio.FechaRegistro <= ff)
                                        .GroupBy(v => v.uf_Nombre)
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            tramite = x.Count(v => v.Envio.StatusGeneral == "Tramite"),
                                            baja = x.Count(v => v.Envio.StatusGeneral == "Baja"),
                                            inmediato = x.Count(v => v.StatusConclusion == "Inmediato"),
                                            diferido = x.Count(v => v.StatusConclusion == "Diferido"),
                                            cumplido = x.Count(v => v.StatusCumplimiento == "Cumplido"),
                                            incumpliedo = x.Count(v => v.StatusCumplimiento == "Incumplido"),
                                            encumplimiento = x.Count(v => v.StatusCumplimiento == "En cumplimiento"),
                                        })
                                        .ToListAsync();

            return consulta1.Select(v => new GET_AcuerdoReparatorioEstadisticaViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                tramite = v.tramite,
                baja = v.baja,
                inmediato = v.inmediato,
                diferido = v.diferido,
                cumplido = v.cumplido,
                incumplido = v.incumpliedo,
                encumplimiento = v.encumplimiento,
            });
        }

        //*****************************************************************************************************************
        //*****************************************************************************************************************

        // GET: api/AcuerdoReparatorios/ListarARInformeAño
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}/{año}")]
        public async Task<IEnumerable<GET_ARInformeViewModel>> ListarARInformeAño([FromRoute] Guid distritoId, int año)
        {


            var envios = await _context.AsignacionEnvios
                                        .Include(v => v.Envio)
                                        .Include(v => v.Envio.Expediente)
                                        .Include(v => v.Envio.Expediente.RHecho)
                                        .Include(v => v.Envio.Expediente.RHecho.Agencia)
                                        .Include(v => v.Envio.Expediente.RHecho.Agencia.DSP)
                                        .Where(v => v.Envio.Expediente.RHecho.Agencia.DSP.DistritoId == distritoId)
                                        .Where(v => v.Envio.FechaRegistro.Year == año)
                                        .Where(v => v.ModuloServicio.Tipo == "Facilitador")
                                        .GroupBy(v => v.ModuloServicio.Nombre)
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            tramite = x.Count(v => v.Envio.StatusGeneral == "En trámite"),
                                            baja = x.Count(v => v.Envio.StatusGeneral == "Baja"),

                                        })
                                        .ToListAsync();


            var envio = envios.Select(v => new GET_ARInformeViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                tramite = v.tramite,
                baja = v.baja,
                inmediato = 0,
                diferido = 0,
                total = v.tramite + v.baja,
                acuerdos = 0,
                cumplido = 0,
                incumplido = 0,
                encumplimiento = 0,



            });
            //***************************************************************************************
            IEnumerable<GET_ARInformeViewModel> items = new GET_ARInformeViewModel[] { };
            foreach (var env in envio)
            {
                var acuerdos = await _context.AcuerdoReparatorios
                                    .Include(v => v.Envio)
                                    .Include(v => v.Envio.Expediente)
                                    .Include(v => v.Envio.Expediente.RHecho)
                                    .Include(v => v.Envio.Expediente.RHecho.Agencia)
                                    .Include(v => v.Envio.Expediente.RHecho.Agencia.DSP)
                                    .Where(v => v.Envio.Expediente.RHecho.Agencia.DSP.DistritoId == distritoId)
                                    .Where(v => v.Envio.FechaRegistro.Year == año)
                                    .Where(a => a.uf_Modulo == env.etiqueta)
                                    .GroupBy(v => v.uf_Modulo)
                                    .Select(x => new {
                                        etiqueta = x.Key,
                                        inmediato = x.Count(v => v.StatusConclusion == "Inmediato"),
                                        diferido = x.Count(v => v.StatusConclusion == "Diferido"),
                                    })
                             .ToListAsync();

                if (acuerdos.Count == 0)
                {
                    IEnumerable<GET_ARInformeViewModel> ReadLines()
                    {
                        IEnumerable<GET_ARInformeViewModel> item;
                        item = (new[]{new GET_ARInformeViewModel{
                                etiqueta = env.etiqueta,
                                tramite = env.tramite,
                                baja = env.baja,
                                inmediato = 0,
                                diferido = 0,
                                total = env.total,
                                acuerdos = 0,
                                cumplido = 0,
                                incumplido = 0,
                                encumplimiento = 0,

                              }});

                        return item;
                    }

                    items = items.Concat(ReadLines());
                }
                else
                {
                    foreach (var ac in acuerdos)
                    {
                        IEnumerable<GET_ARInformeViewModel> ReadLines()
                        {
                            IEnumerable<GET_ARInformeViewModel> item;
                            item = (new[]{new GET_ARInformeViewModel{
                                 etiqueta = env.etiqueta,
                                tramite = env.tramite,
                                baja = env.baja,
                                inmediato = ac.inmediato,
                                diferido = ac.diferido,
                                total =  env.tramite + ac.inmediato +ac.diferido + env.baja ,
                                acuerdos = ac.inmediato + ac.diferido,
                                cumplido = 0,
                                incumplido = 0,
                                encumplimiento = 0,

                              }});

                            return item;
                        }

                        items = items.Concat(ReadLines());
                    }
                }
            }

            //***************************************************************************************
            //***************************************************************************************

            IEnumerable<GET_ARInformeViewModel> items1 = new GET_ARInformeViewModel[] { };
            foreach (var det in items)
            {
                var detalles = await _context.AcuerdoReparatorios
                                    .Include(v => v.Envio)
                                    .Include(v => v.Envio.Expediente)
                                    .Include(v => v.Envio.Expediente.RHecho)
                                    .Include(v => v.Envio.Expediente.RHecho.Agencia)
                                    .Include(v => v.Envio.Expediente.RHecho.Agencia.DSP)
                                    .Where(v => v.Envio.Expediente.RHecho.Agencia.DSP.DistritoId == distritoId)
                                    .Where(v => v.Envio.FechaRegistro.Year == año)
                                    .Where(a => a.uf_Modulo == det.etiqueta)
                                    .Where(a => a.StatusConclusion == "Diferido")
                                    .GroupBy(v => v.uf_Modulo)
                                    .Select(x => new {
                                        etiqueta = x.Key,
                                        cumplido = x.Count(v => v.StatusCumplimiento == "Cumplido"),
                                        incumplido = x.Count(v => v.StatusCumplimiento == "Incumplido"),
                                        encumplimiento = x.Count(v => v.StatusCumplimiento == "En cumplimiento"),
                                    })
                             .ToListAsync();

                if (detalles.Count == 0)
                {
                    IEnumerable<GET_ARInformeViewModel> ReadLines()
                    {
                        IEnumerable<GET_ARInformeViewModel> item;
                        item = (new[]{new GET_ARInformeViewModel{
                                etiqueta = det.etiqueta,
                                tramite = det.tramite,
                                baja = det.baja,
                                inmediato = det.inmediato,
                                diferido = det.diferido,
                                total = det.total,
                                acuerdos = det.acuerdos,
                                cumplido = 0,
                                incumplido = 0,
                                encumplimiento = 0,

                              }});

                        return item;
                    }
                    items1 = items1.Concat(ReadLines());
                }
                else
                {
                    foreach (var de in detalles)
                    {
                        IEnumerable<GET_ARInformeViewModel> ReadLines()
                        {
                            IEnumerable<GET_ARInformeViewModel> item;
                            item = (new[]{new GET_ARInformeViewModel{
                                 etiqueta = det.etiqueta,
                                tramite = det.tramite,
                                baja = det.baja,
                                inmediato = det.inmediato,
                                diferido = det.diferido,
                                total = det.total,
                                acuerdos = det.acuerdos,
                                cumplido = de.cumplido,
                                incumplido = de.incumplido,
                                encumplimiento = de.encumplimiento,

                              }});

                            return item;
                        }
                        items1 = items1.Concat(ReadLines());
                    }
                }
            }
            return items1;
        }

        // GET: api/AcuerdoReparatorios/ListarARInformeMes
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}/{año}/{mes}")]
        public async Task<IEnumerable<GET_ARInformeViewModel>> ListarARInformeMes([FromRoute] Guid distritoId, int año, int mes)
        {
            var envios = await _context.AsignacionEnvios
                                        .Include(v => v.Envio)
                                        .Include(v => v.Envio.Expediente)
                                        .Include(v => v.Envio.Expediente.RHecho)
                                        .Include(v => v.Envio.Expediente.RHecho.Agencia)
                                        .Include(v => v.Envio.Expediente.RHecho.Agencia.DSP)
                                        .Where(v => v.Envio.Expediente.RHecho.Agencia.DSP.DistritoId == distritoId)
                                        .Where(v => v.Envio.FechaRegistro.Year == año)
                                        .Where(v => v.Envio.FechaRegistro.Month == mes)
                                        .Where(v => v.ModuloServicio.Tipo == "Facilitador")
                                        .GroupBy(v => v.ModuloServicio.Nombre)
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            tramite = x.Count(v => v.Envio.StatusGeneral == "En trámite"),
                                            baja = x.Count(v => v.Envio.StatusGeneral == "Baja"),

                                        })
                                        .ToListAsync();

            var envio = envios.Select(v => new GET_ARInformeViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                tramite = v.tramite,
                baja = v.baja,
                inmediato = 0,
                diferido = 0,
                total = v.tramite + v.baja,
                acuerdos = 0,
                cumplido = 0,
                incumplido = 0,
                encumplimiento = 0,



            });
            
            //***************************************************************************************

            IEnumerable<GET_ARInformeViewModel> items = new GET_ARInformeViewModel[] { };
            foreach (var env in envio)
            {
                var acuerdos = await _context.AcuerdoReparatorios
                                    .Include(v => v.Envio)
                                    .Include(v => v.Envio.Expediente)
                                    .Include(v => v.Envio.Expediente.RHecho)
                                    .Include(v => v.Envio.Expediente.RHecho.Agencia)
                                    .Include(v => v.Envio.Expediente.RHecho.Agencia.DSP)
                                    .Where(v => v.Envio.Expediente.RHecho.Agencia.DSP.DistritoId == distritoId)
                                    .Where(v => v.Envio.FechaRegistro.Year == año)
                                    .Where(v => v.Envio.FechaRegistro.Month == mes)
                                    .Where(a => a.uf_Modulo == env.etiqueta)
                                    .GroupBy(v => v.uf_Modulo)
                                    .Select(x => new {
                                        etiqueta = x.Key,
                                        inmediato = x.Count(v => v.StatusConclusion == "Inmediato"),
                                        diferido = x.Count(v => v.StatusConclusion == "Diferido"),
                                    })
                             .ToListAsync();

                if (acuerdos.Count == 0)
                {
                    IEnumerable<GET_ARInformeViewModel> ReadLines()
                    {
                        IEnumerable<GET_ARInformeViewModel> item;
                        item = (new[]{new GET_ARInformeViewModel{
                                etiqueta = env.etiqueta,
                                tramite = env.tramite,
                                baja = env.baja,
                                inmediato = 0,
                                diferido = 0,
                                total = env.total,
                                acuerdos = 0,
                                cumplido = 0,
                                incumplido = 0,
                                encumplimiento = 0,

                              }});

                        return item;
                    }
                    items = items.Concat(ReadLines());
                }
                else
                {
                    foreach (var ac in acuerdos)
                    {
                        IEnumerable<GET_ARInformeViewModel> ReadLines()
                        {
                            IEnumerable<GET_ARInformeViewModel> item;
                            item = (new[]{new GET_ARInformeViewModel{
                                 etiqueta = env.etiqueta,
                                tramite = env.tramite,
                                baja = env.baja,
                                inmediato = ac.inmediato,
                                diferido = ac.diferido,
                                total =  env.tramite + ac.inmediato +ac.diferido + env.baja ,
                                acuerdos = ac.inmediato + ac.diferido,
                                cumplido = 0,
                                incumplido = 0,
                                encumplimiento = 0,

                              }});

                            return item;
                        }
                        items = items.Concat(ReadLines());
                    }
                }
            }
            
            //***************************************************************************************
            //***************************************************************************************

            IEnumerable<GET_ARInformeViewModel> items1 = new GET_ARInformeViewModel[] { };
            foreach (var det in items)
            {
                var detalles = await _context.AcuerdoReparatorios
                                    .Include(v => v.Envio)
                                    .Include(v => v.Envio.Expediente)
                                    .Include(v => v.Envio.Expediente.RHecho)
                                    .Include(v => v.Envio.Expediente.RHecho.Agencia)
                                    .Include(v => v.Envio.Expediente.RHecho.Agencia.DSP)
                                    .Where(v => v.Envio.Expediente.RHecho.Agencia.DSP.DistritoId == distritoId)
                                    .Where(v => v.Envio.FechaRegistro.Year == año)
                                    .Where(v => v.Envio.FechaRegistro.Month == mes)
                                    .Where(a => a.uf_Modulo == det.etiqueta)
                                    .Where(a => a.StatusConclusion == "Diferido")
                                    .GroupBy(v => v.uf_Modulo)
                                    .Select(x => new {
                                        etiqueta = x.Key,
                                        cumplido = x.Count(v => v.StatusCumplimiento == "Cumplido"),
                                        incumplido = x.Count(v => v.StatusCumplimiento == "Incumplido"),
                                        encumplimiento = x.Count(v => v.StatusCumplimiento == "En cumplimiento"),
                                    })
                             .ToListAsync();

                if (detalles.Count == 0)
                {
                    IEnumerable<GET_ARInformeViewModel> ReadLines()
                    {
                        IEnumerable<GET_ARInformeViewModel> item;
                        item = (new[]{new GET_ARInformeViewModel{
                                etiqueta = det.etiqueta,
                                tramite = det.tramite,
                                baja = det.baja,
                                inmediato = det.inmediato,
                                diferido = det.diferido,
                                total = det.total,
                                acuerdos = det.acuerdos,
                                cumplido = 0,
                                incumplido = 0,
                                encumplimiento = 0,
                              }});

                        return item;
                    }
                    items1 = items1.Concat(ReadLines());
                }
                else
                {
                    foreach (var de in detalles)
                    {
                        IEnumerable<GET_ARInformeViewModel> ReadLines()
                        {
                            IEnumerable<GET_ARInformeViewModel> item;
                            item = (new[]{new GET_ARInformeViewModel{
                                 etiqueta = det.etiqueta,
                                tramite = det.tramite,
                                baja = det.baja,
                                inmediato = det.inmediato,
                                diferido = det.diferido,
                                total = det.total,
                                acuerdos = det.acuerdos,
                                cumplido = de.cumplido,
                                incumplido = de.incumplido,
                                encumplimiento = de.encumplimiento,

                              }});

                            return item;
                        }

                        items1 = items1.Concat(ReadLines());
                    }
                }
            }
            return items1;
        }

        // GET: api/AcuerdoReparatorios/ListarARInformeDia
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}/{fi}/{ff}")]
        public async Task<IEnumerable<GET_ARInformeViewModel>> ListarARInformeDia([FromRoute] Guid distritoId, DateTime fi, DateTime ff)
        {
            var envios = await _context.AsignacionEnvios
                                        .Include(v => v.Envio)
                                        .Include(v => v.Envio.Expediente)
                                        .Include(v => v.Envio.Expediente.RHecho)
                                        .Include(v => v.Envio.Expediente.RHecho.Agencia)
                                        .Include(v => v.Envio.Expediente.RHecho.Agencia.DSP)
                                        .Where(v => v.Envio.Expediente.RHecho.Agencia.DSP.DistritoId == distritoId)
                                        .Where(v => v.Envio.FechaRegistro >= fi)
                                        .Where(v => v.Envio.FechaRegistro <= ff)
                                        .Where(v => v.ModuloServicio.Tipo == "Facilitador")
                                        .GroupBy(v => v.ModuloServicio.Nombre)
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            tramite = x.Count(v => v.Envio.StatusGeneral == "En trámite"),
                                            baja = x.Count(v => v.Envio.StatusGeneral == "Baja"),

                                        })
                                        .ToListAsync();

            var envio = envios.Select(v => new GET_ARInformeViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                tramite = v.tramite,
                baja = v.baja,
                inmediato = 0,
                diferido = 0,
                total = v.tramite + v.baja,
                acuerdos = 0,
                cumplido = 0,
                incumplido = 0,
                encumplimiento = 0,



            });

            //***************************************************************************************

            IEnumerable<GET_ARInformeViewModel> items = new GET_ARInformeViewModel[] { };
            foreach (var env in envio)
            {
                var acuerdos = await _context.AcuerdoReparatorios
                                    .Include(v => v.Envio)
                                    .Include(v => v.Envio.Expediente)
                                    .Include(v => v.Envio.Expediente.RHecho)
                                    .Include(v => v.Envio.Expediente.RHecho.Agencia)
                                    .Include(v => v.Envio.Expediente.RHecho.Agencia.DSP)
                                    .Where(v => v.Envio.Expediente.RHecho.Agencia.DSP.DistritoId == distritoId)
                                    .Where(v => v.Envio.FechaRegistro >= fi)
                                    .Where(v => v.Envio.FechaRegistro <= ff)
                                    .Where(a => a.uf_Modulo == env.etiqueta)
                                    .GroupBy(v => v.uf_Modulo)
                                    .Select(x => new {
                                        etiqueta = x.Key,
                                        inmediato = x.Count(v => v.StatusConclusion == "Inmediato"),
                                        diferido = x.Count(v => v.StatusConclusion == "Diferido"),
                                    })
                             .ToListAsync();

                if (acuerdos.Count == 0)
                {
                    IEnumerable<GET_ARInformeViewModel> ReadLines()
                    {
                        IEnumerable<GET_ARInformeViewModel> item;
                        item = (new[]{new GET_ARInformeViewModel{
                                etiqueta = env.etiqueta,
                                tramite = env.tramite,
                                baja = env.baja,
                                inmediato = 0,
                                diferido = 0,
                                total = env.total,
                                acuerdos = 0,
                                cumplido = 0,
                                incumplido = 0,
                                encumplimiento = 0,
                              }});
                        return item;
                    }
                    items = items.Concat(ReadLines());
                }
                else
                {
                    foreach (var ac in acuerdos)
                    {
                        IEnumerable<GET_ARInformeViewModel> ReadLines()
                        {
                            IEnumerable<GET_ARInformeViewModel> item;
                            item = (new[]{new GET_ARInformeViewModel{
                                 etiqueta = env.etiqueta,
                                tramite = env.tramite,
                                baja = env.baja,
                                inmediato = ac.inmediato,
                                diferido = ac.diferido,
                                total =  env.tramite + ac.inmediato +ac.diferido + env.baja ,
                                acuerdos = ac.inmediato + ac.diferido,
                                cumplido = 0,
                                incumplido = 0,
                                encumplimiento = 0,

                              }});

                            return item;
                        }
                        items = items.Concat(ReadLines());
                    }
                }
            }

            //***************************************************************************************
            //***************************************************************************************

            IEnumerable<GET_ARInformeViewModel> items1 = new GET_ARInformeViewModel[] { };
            foreach (var det in items)
            {
                var detalles = await _context.AcuerdoReparatorios
                                    .Include(v => v.Envio)
                                    .Include(v => v.Envio.Expediente)
                                    .Include(v => v.Envio.Expediente.RHecho)
                                    .Include(v => v.Envio.Expediente.RHecho.Agencia)
                                    .Include(v => v.Envio.Expediente.RHecho.Agencia.DSP)
                                    .Where(v => v.Envio.Expediente.RHecho.Agencia.DSP.DistritoId == distritoId)
                                    .Where(v => v.Envio.FechaRegistro >= fi)
                                    .Where(v => v.Envio.FechaRegistro <= ff)
                                    .Where(a => a.uf_Modulo == det.etiqueta)
                                    .Where(a => a.StatusConclusion == "Diferido")
                                    .GroupBy(v => v.uf_Modulo)
                                    .Select(x => new {
                                        etiqueta = x.Key,
                                        cumplido = x.Count(v => v.StatusCumplimiento == "Cumplido"),
                                        incumplido = x.Count(v => v.StatusCumplimiento == "Incumplido"),
                                        encumplimiento = x.Count(v => v.StatusCumplimiento == "En cumplimiento"),
                                    })
                             .ToListAsync();

                if (detalles.Count == 0)
                {
                    IEnumerable<GET_ARInformeViewModel> ReadLines()
                    {
                        IEnumerable<GET_ARInformeViewModel> item;
                        item = (new[]{new GET_ARInformeViewModel{
                                etiqueta = det.etiqueta,
                                tramite = det.tramite,
                                baja = det.baja,
                                inmediato = det.inmediato,
                                diferido = det.diferido,
                                total = det.total,
                                acuerdos = det.acuerdos,
                                cumplido = 0,
                                incumplido = 0,
                                encumplimiento = 0,

                              }});

                        return item;
                    }
                    items1 = items1.Concat(ReadLines());
                }
                else
                {
                    foreach (var de in detalles)
                    {
                        IEnumerable<GET_ARInformeViewModel> ReadLines()
                        {
                            IEnumerable<GET_ARInformeViewModel> item;
                            item = (new[]{new GET_ARInformeViewModel{
                                 etiqueta = det.etiqueta,
                                tramite = det.tramite,
                                baja = det.baja,
                                inmediato = det.inmediato,
                                diferido = det.diferido,
                                total = det.total,
                                acuerdos = det.acuerdos,
                                cumplido = de.cumplido,
                                incumplido = de.incumplido,
                                encumplimiento = de.encumplimiento,

                            }});
                            return item;
                        }
                        items1 = items1.Concat(ReadLines());
                    }
                }
            }
            return items1;
        }

        //*****************************************************************************************************************
        //*****************************************************************************************************************
        //************************************************************************************************************
        //************************************************************************************************************


        // POST: api/AcuerdoReparatorios/CrearAcuerdoReparatorio
        // API: SE CREA EL ACUERDO REPARATORIO
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearAcuerdoReparatorio([FromBody] POST_AcuerdoReparatorioViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                AcuerdoReparatorio AR = new AcuerdoReparatorio();

                if (model.StatusAnexo == null)
                {
                        AR.EnvioId = model.EnvioId;
                        AR.TipoDocumento = model.TipoDocumento;
                        AR.NombreSolicitante = model.NombreSolicitante;
                        AR.NombreRequerdio = model.NombreRequerdio;
                        AR.NUC = model.NUC;
                        AR.NoExpediente = model.NoExpediente;
                        AR.StatusConclusion = model.StatusConclusion;
                        AR.StatusCumplimiento = model.StatusCumplimiento;
                        AR.MetodoUtilizado = model.MetodoUtilizado;
                        AR.TipoPago = model.TipoPago;
                        AR.MontoTotal = model.MontoTotal;
                        AR.ObjectoEspecie = model.ObjectoEspecie;
                        AR.NoTotalParcialidades = model.NoTotalParcialidades;
                        AR.Periodo = model.Periodo;
                        AR.FechaCelebracionAcuerdo = model.FechaCelebracionAcuerdo;
                        AR.FechaLimiteCumplimiento = model.FechaLimiteCumplimiento;
                        AR.TextoAR = model.TextoAR;
                        AR.Delitos = model.Delitos;
                        AR.uf_Distrito = model.uf_Distrito;
                        AR.uf_DirSubProc = model.uf_DirSubProc;
                        AR.uf_Agencia = model.uf_Agencia;
                        AR.uf_Modulo = model.uf_Modulo;
                        AR.uf_Nombre = model.uf_Nombre;
                        AR.uf_Puesto = model.uf_Puesto;
                        AR.MoneyChain = model.money;
                        AR.SpeciesChain = model.species;                    
                }
                else
                {
                        AR.EnvioId = model.EnvioId;
                        AR.TipoDocumento = model.TipoDocumento;
                        AR.NombreSolicitante = model.NombreSolicitante;
                        AR.NombreRequerdio = model.NombreRequerdio;
                        AR.NUC = model.NUC;
                        AR.NoExpediente = model.NoExpediente;
                        AR.StatusConclusion = model.StatusConclusion;
                        AR.StatusCumplimiento = model.StatusCumplimiento;
                        AR.MetodoUtilizado = model.MetodoUtilizado;
                        AR.TipoPago = model.TipoPago;
                        AR.MontoTotal = model.MontoTotal;
                        AR.ObjectoEspecie = model.ObjectoEspecie;
                        AR.NoTotalParcialidades = model.NoTotalParcialidades;
                        AR.Periodo = model.Periodo;
                        AR.FechaCelebracionAcuerdo = model.FechaCelebracionAcuerdo;
                        AR.FechaLimiteCumplimiento = model.FechaLimiteCumplimiento;
                        AR.TextoAR = model.TextoAR;
                        AR.Delitos = model.Delitos;
                        AR.uf_Distrito = model.uf_Distrito;
                        AR.uf_DirSubProc = model.uf_DirSubProc;
                        AR.uf_Agencia = model.uf_Agencia;
                        AR.uf_Modulo = model.uf_Modulo;
                        AR.uf_Nombre = model.uf_Nombre;
                        AR.uf_Puesto = model.uf_Puesto;
                        AR.MoneyChain = model.money;
                        AR.SpeciesChain = model.species;
                        AR.Anexo = model.Anexo;
                        AR.StatusAnexo = model.StatusAnexo;

                        var AnexosT = _context.AcuerdoReparatorios
                                    .Where(x => x.StatusAnexo == true && x.Anexo == model.Anexo)                                    
                                    .ToList();
                        if(AnexosT.Any())
                        {
                            foreach (var atta in AnexosT)
                            {
                                atta.StatusAnexo = false;
                            }

                            await _context.SaveChangesAsync();
                        }
                }              

                _context.AcuerdoReparatorios.Add(AR);

                if(model.SeguimientoCumplimientos.Count != 0)
                {
                    if (model.StatusConclusion.Count() > 1)
                    {
                        foreach (var detalleSC in model.SeguimientoCumplimientos)
                        {
                            SeguimientoCumplimiento SC = new SeguimientoCumplimiento
                            {
                                AcuerdoReparatorioId = AR.IdAcuerdoReparatorio,
                                NoParcialidad = detalleSC.NoParcialidad,
                                Fecha = detalleSC.Fecha,
                                TipoPago = detalleSC.TipoPago,
                                ObjectoEspecie = detalleSC.ObjectoEspecie,
                                CantidadAPagar = detalleSC.CantidadAPagar,
                                StatusPago = "Pendiente",
                                Solicitantes = model.NombreSolicitante,
                                Requeridos = model.NombreRequerdio,
                                Texto = model.TextoAR,
                                uf_Distrito = model.uf_Distrito,
                                uf_DirSubProc = model.uf_DirSubProc,
                                uf_Agencia = model.uf_Agencia,
                                uf_Modulo = model.uf_Modulo,
                                uf_Nombre = model.uf_Nombre,
                                uf_Puesto = model.uf_Puesto,
                            };
                            _context.SeguimientoCumplimientos.Add(SC);
                        }
                    }
                    else
                    {
                        SeguimientoCumplimiento SC = new SeguimientoCumplimiento
                        {
                            AcuerdoReparatorioId = AR.IdAcuerdoReparatorio,
                            NoParcialidad = 1,
                            Fecha = model.SeguimientoCumplimientos[0].Fecha,
                            TipoPago = model.SeguimientoCumplimientos[0].TipoPago,
                            ObjectoEspecie = model.SeguimientoCumplimientos[0].ObjectoEspecie,
                            CantidadAPagar = model.SeguimientoCumplimientos[0].CantidadAPagar,
                            StatusPago = "Pendiente",
                            Solicitantes = model.NombreSolicitante,
                            Requeridos = model.NombreRequerdio,
                            Texto = model.TextoAR,
                            uf_Distrito = model.uf_Distrito,
                            uf_DirSubProc = model.uf_DirSubProc,
                            uf_Agencia = model.uf_Agencia,
                            uf_Modulo = model.uf_Modulo,
                            uf_Nombre = model.uf_Nombre,
                            uf_Puesto = model.uf_Puesto,
                        };
                        _context.SeguimientoCumplimientos.Add(SC);
                    }
                }                

                await _context.SaveChangesAsync();

                return Ok(new { idAcuerdoR = AR.IdAcuerdoReparatorio });
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }
        }
        //POST: api/AcuerdoReparatorios/CreateMonitoring
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateMonitoring([FromBody] EditAcuerdoReparatorioViewModel model)
        {
            try
            {
                if(model.SeguimientoCumplimientos.Count != 0)
                {
                    if (model.StatusConclusion.Count() > 1)
                    {
                        foreach (var detalleSC in model.SeguimientoCumplimientos)
                        {
                            SeguimientoCumplimiento SC = new SeguimientoCumplimiento
                            {
                                AcuerdoReparatorioId = model.idAcuerdoReparatorio,
                                NoParcialidad = detalleSC.NoParcialidad,
                                Fecha = detalleSC.Fecha,
                                TipoPago = detalleSC.TipoPago,
                                ObjectoEspecie = detalleSC.ObjectoEspecie,
                                CantidadAPagar = detalleSC.CantidadAPagar,
                                StatusPago = "Pendiente",
                                Solicitantes = model.NombreSolicitante,
                                Requeridos = model.NombreRequerdio,
                                Texto = model.TextoAR,
                                uf_Distrito = model.uf_Distrito,
                                uf_DirSubProc = model.uf_DirSubProc,
                                uf_Agencia = model.uf_Agencia,
                                uf_Modulo = model.uf_Modulo,
                                uf_Nombre = model.uf_Nombre,
                                uf_Puesto = model.uf_Puesto,
                            };
                            _context.SeguimientoCumplimientos.Add(SC);
                        }
                    }
                    else
                    {
                        SeguimientoCumplimiento SC = new SeguimientoCumplimiento
                        {
                            AcuerdoReparatorioId = model.idAcuerdoReparatorio,
                            NoParcialidad = 1,
                            Fecha = model.SeguimientoCumplimientos[0].Fecha,
                            TipoPago = model.SeguimientoCumplimientos[0].TipoPago,
                            ObjectoEspecie = model.SeguimientoCumplimientos[0].ObjectoEspecie,
                            CantidadAPagar = model.SeguimientoCumplimientos[0].CantidadAPagar,
                            StatusPago = "Pendiente",
                            Solicitantes = model.NombreSolicitante,
                            Requeridos = model.NombreRequerdio,
                            Texto = model.TextoAR,
                            uf_Distrito = model.uf_Distrito,
                            uf_DirSubProc = model.uf_DirSubProc,
                            uf_Agencia = model.uf_Agencia,
                            uf_Modulo = model.uf_Modulo,
                            uf_Nombre = model.uf_Nombre,
                            uf_Puesto = model.uf_Puesto,
                        };
                        _context.SeguimientoCumplimientos.Add(SC);
                    }
                }

                await _context.SaveChangesAsync();

                return Ok(new { Seguimiento = true });
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // PUT:  api/AcuerdoReparatorios/ActualizarAcuerdoReparatorio
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarAcuerdoReparatorio([FromBody] PUT_AcuerdoReparatorioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var db = await _context.AcuerdoReparatorios.FirstOrDefaultAsync(a => a.IdAcuerdoReparatorio == model.IdAcuerdoReparatorio);

            if (db == null)
            {
                return NotFound();
            }

            db.IdAcuerdoReparatorio = model.IdAcuerdoReparatorio;
            db.TipoDocumento = model.TipoDocumento;
            //db.EnvioId = model.EnvioId;
            //db.NombreSolicitante = model.NombreSolicitante;
            //db.NombreRequerdio = model.NombreRequerdio;
            //db.NUC = model.NUC;
            //db.NoExpediente = model.NoExpediente;
            db.StatusConclusion = model.StatusConclusion;
            //db.StatusCumplimiento = model.StatusCumplimiento;
            db.MetodoUtilizado = model.MetodoUtilizado;
            db.TipoPago = model.TipoPago;
            db.MontoTotal = model.MontoTotal;
            db.NoTotalParcialidades = model.NoTotalParcialidades;
            db.Periodo = model.Periodo;
            db.FechaCelebracionAcuerdo = model.FechaCelebracionAcuerdo;
            db.FechaLimiteCumplimiento = model.FechaLimiteCumplimiento;
            db.TextoAR = model.TextoAR;
            db.MoneyChain = model.money;
            db.SpeciesChain = model.species;
            db.ObjectoEspecie = model.objectoEspecie;
            db.uf_Distrito = model.uf_Distrito;
            db.uf_DirSubProc = model.uf_DirSubProc;
            db.uf_Agencia = model.uf_Agencia;
            db.uf_Modulo = model.uf_Modulo;
            db.uf_Nombre = model.uf_Nombre;
            db.uf_Puesto = model.uf_Puesto;

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
        //[Authorize(Roles = "Director, Administrador, Coordinador, Facilitador")]
        [HttpPut("[action]/{idDistrito}")]
        public async Task<IActionResult> ActualizarRespuesta([FromBody] PUT_CoordinadorJuridicoViewModel model, Guid idDistrito)
        {
            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + idDistrito.ToString().ToUpper())).Options;
            using (var ctx = new DbContextSIIGPP(options))
            { 
                if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var db = await ctx.AcuerdoReparatorios.FirstOrDefaultAsync(a => a.IdAcuerdoReparatorio == model.IdAcuerdoReparatorio);

            if (db == null)
            {
                return NotFound();
            }
            DateTime fechahora = System.DateTime.Now;
            db.StatusRespuestaCoordinadorJuridico = model.StatusRespuestaCoordinadorJuridico;
            db.RespuestaCoordinadorJuridico = model.RespuestaCoordinadorJuridico;
            db.FechaHoraRespuestaCoordinadorJuridico = fechahora;
                           
            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }

            return Ok();
            }
        }

        //[Authorize(Roles = "Director, Administrador, Coordinador, Facilitador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarRespuestaL([FromBody] PUT_CoordinadorJuridicoViewModel model)
        {           
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }



                var db = await _context.AcuerdoReparatorios.FirstOrDefaultAsync(a => a.IdAcuerdoReparatorio == model.IdAcuerdoReparatorio);

                if (db == null)
                {
                    return NotFound();
                }
                DateTime fechahora = System.DateTime.Now;
                db.StatusRespuestaCoordinadorJuridico = model.StatusRespuestaCoordinadorJuridico;
                db.RespuestaCoordinadorJuridico = model.RespuestaCoordinadorJuridico;
                db.FechaHoraRespuestaCoordinadorJuridico = fechahora;


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

        // PUT:  api/AcuerdoReparatorios/ActualizarRespuestaMP
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarRespuestaMP([FromBody] PUT_MPViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var db = await _context.AcuerdoReparatorios.FirstOrDefaultAsync(a => a.IdAcuerdoReparatorio == model.IdAcuerdoReparatorio);

            if (db == null)
            {
                return NotFound();
            }

            DateTime fechahora = System.DateTime.Now;
            db.StatusRespuestaAMP = model.statusRespuestaAMP;
            db.RespuestaAMP = model.respuestaAMP;
            db.FechaRespuestaAMP = fechahora;


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

        // PUT:  api/AcuerdoReparatorios/ActualizarSise
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizarsise([FromBody] PUT_SiseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var db = await _context.AcuerdoReparatorios.FirstOrDefaultAsync(a => a.IdAcuerdoReparatorio == model.IdAcuerdoReparatorio);

            if (db == null)
            {
                return NotFound();
            }
            DateTime fechahora = System.DateTime.Now;
            db.Fechasise = fechahora;
            db.Sise = model.Nosise;


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


        // PUT:  api/AcuerdoReparatorios/ActualizarStatuscumplimineto
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatuscumplimineto([FromBody] PUT_StatusCumplimientoviewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var db = await _context.AcuerdoReparatorios.FirstOrDefaultAsync(a => a.IdAcuerdoReparatorio == model.IdAcuerdo);

            if (db == null)
            {
                return NotFound();
            }

            db.StatusCumplimiento = model.StatusCumplimiento;


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

        //GET: api/AcuerdoReparatorios/isTypeConjunt
        [HttpGet("[action]/{idEnvio}")]
        public async Task<IActionResult> isTypeConjunt([FromRoute] Guid idEnvio)
        {
            var liveCount = await _context.ConjuntoDerivaciones
                                          .Where(x => x.EnvioId == idEnvio)
                                          .CountAsync();

            if (liveCount == 0)
                return Ok(new { type = false });

            return Ok(new { type = true });
        }

        // GET: api/AcuerdoReparatorios/ListarConjuntos
        [HttpGet("[action]/{idEnvio}")]
        public async Task<IActionResult> ListarConjuntos([FromRoute] Guid idEnvio)
        {
            try
            {

                string busquedaRA = @"SELECT * FROM JR_CONJUNTODERIVACIONES jc where jc.EnvioId = @envio AND jc.IdConjuntoDerivaciones NOT IN (SELECT ConjuntoDerivacionesId FROM JR_ACUERDOS_CONJUNTOS jac) AND jc.IdConjuntoDerivaciones IN (SELECT ConjuntoDerivacionesId FROM JR_SESIONESCONJUNTOS jsc join JR_SESION ss on jsc.SesionId = ss.IdSesion where ss.StatusSesion = 'Se realiza sesión con acuerdo reparatorio')";


                List<SqlParameter> fb = new List<SqlParameter>();
                fb.Add(new SqlParameter("@envio", idEnvio));
                var listaC = await _context.ConjuntoDerivaciones.FromSqlRaw(busquedaRA, fb.ToArray()).ToListAsync();

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
                        NombreSubDirigido = a.NombreSubDirigido
                    })
                );               
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }
        
        // GET: api/AcuerdoReparatorios/ListarConjuntosCA
        [HttpGet("[action]/{idEnvio}")]
        public async Task<IActionResult> ListarConjuntosCA([FromRoute] Guid idEnvio)
        {
            try
            {                

                var listaCAR = _context.AcuerdosConjuntos
                             .Include(a => a.AcuerdoReparatorio)
                             .Include(a => a.ConjuntoDerivaciones)
                             .Where(a => a.AcuerdoReparatorio.EnvioId == idEnvio)
                             .OrderBy(a => a.AcuerdoReparatorio.FechaCelebracionAcuerdo)
                             .ToArray();

                if (listaCAR == null)
                {
                    return Ok(new { status = false });
                }                

                foreach(var registro in listaCAR)
                {
                    // Bloque de código para consultar la cantidad de sesiones para anexo para el conjunto existen
                    string busquedaS = @"SELECT jc.IdConjuntoDerivaciones, jc.EnvioId, jc.SolicitadosC, jc.RequeridosC, jc.DelitosC, jc.NombreS, jc.DireccionS, jc.TelefonoS, jc.ClasificacionS, jc.NombreR, jc.DireccionR, jc.TelefonoR, jc.ClasificacionR, jc.NombreD, jc.NoOficio, jc.ResponsableJR, jc.NombreSubDirigido FROM JR_CONJUNTODERIVACIONES jc join JR_SESIONESCONJUNTOS jsc on jc.IdConjuntoDerivaciones = jsc.ConjuntoDerivacionesId join JR_SESION js on jsc.SesionId = js.IdSesion where jsc.ConjuntoDerivacionesId = @conjunt AND StatusSesion = 'Se crea anexo a acuerdo reparatorio'";

                    List<SqlParameter> s = new List<SqlParameter>();
                    s.Add(new SqlParameter("@conjunt", registro.ConjuntoDerivaciones.IdConjuntoDerivaciones));
                    var listaS = await _context.ConjuntoDerivaciones.FromSqlRaw(busquedaS, s.ToArray()).ToListAsync();

                    registro.CountSessionsA = listaS.Count;

                    // Bloque de código para consultar los anexos generados para el acuerdo
                    string busquedaA = @"select * from JR_ACUERDOREPARATORIO ja where Anexo = @acuerdo ORDER BY FechaCelebracionAcuerdo ASC";

                    List<SqlParameter> a = new List<SqlParameter>();
                    a.Add(new SqlParameter("@acuerdo", registro.AcuerdoReparatorio.IdAcuerdoReparatorio));
                    var listaA = await _context.AcuerdoReparatorios.FromSqlRaw(busquedaA, a.ToArray()).ToListAsync();

                    if (listaA == null || !listaA.Any())
                    {
                        registro.Attached = null;
                    }
                    else
                    {
                        registro.Attached = listaA;
                    }

                    // Bloque de código para consultar el conjunto del acuerdo
                    string busquedaC = @"select * from JR_CONJUNTODERIVACIONES ja where IdConjuntoDerivaciones = @con";

                    List<SqlParameter> c = new List<SqlParameter>();
                    c.Add(new SqlParameter("@con", registro.ConjuntoDerivaciones.IdConjuntoDerivaciones));
                    var listaC = await _context.ConjuntoDerivaciones.FromSqlRaw(busquedaC, c.ToArray()).ToListAsync();

                    if (listaC == null || !listaC.Any())
                    {
                        registro.Conjunt = null;
                    }
                    else
                    {
                        registro.Conjunt = listaC;
                    }

                };

                return Ok(listaCAR.Select(a => new ConjuntsWithAgreementViewModel
                {
                    //campos de conjuntos
                    ConjuntoDerivacionesId = a.ConjuntoDerivaciones.IdConjuntoDerivaciones,
                    EnvioId = a.ConjuntoDerivaciones.EnvioId,
                    SolicitadosC = a.ConjuntoDerivaciones.SolicitadosC,
                    RequeridosC = a.ConjuntoDerivaciones.RequeridosC,
                    DelitosC = a.ConjuntoDerivaciones.DelitosC,
                    NombreS = a.ConjuntoDerivaciones.NombreS,
                    DireccionS = a.ConjuntoDerivaciones.DireccionS,
                    TelefonoS = a.ConjuntoDerivaciones.TelefonoS,
                    ClasificacionS = a.ConjuntoDerivaciones.ClasificacionS,
                    NombreR = a.ConjuntoDerivaciones.NombreR,
                    DireccionR = a.ConjuntoDerivaciones.DireccionR,
                    TelefonoR = a.ConjuntoDerivaciones.TelefonoR,
                    ClasificacionR = a.ConjuntoDerivaciones.ClasificacionR,
                    NombreD = a.ConjuntoDerivaciones.NombreD,
                    NoOficio = a.ConjuntoDerivaciones.NoOficio,
                    ResponsableJR = a.ConjuntoDerivaciones.ResponsableJR,
                    NombreSubDirigido = a.ConjuntoDerivaciones.NombreSubDirigido,
                    //campos de acuerdo
                    IdAcuerdoReparatorio = a.AcuerdoReparatorio.IdAcuerdoReparatorio,
                    StatusConclusion = a.AcuerdoReparatorio.StatusConclusion,
                    StatusCumplimiento = a.AcuerdoReparatorio.StatusCumplimiento,
                    TipoPago = a.AcuerdoReparatorio.TipoPago,
                    MetodoUtilizado = a.AcuerdoReparatorio.MetodoUtilizado,
                    MontoTotal = a.AcuerdoReparatorio.MontoTotal,
                    ObjetoEspecie = a.AcuerdoReparatorio.ObjectoEspecie,
                    NoTotalParcialidades = a.AcuerdoReparatorio.NoTotalParcialidades,
                    Periodo = a.AcuerdoReparatorio.Periodo,
                    NUC = a.AcuerdoReparatorio.NUC,
                    money = a.AcuerdoReparatorio.MoneyChain,
                    species = a.AcuerdoReparatorio.SpeciesChain,
                    FechaCelebracionAcuerdo = a.AcuerdoReparatorio.FechaCelebracionAcuerdo,
                    FechaLimiteCumplimiento = a.AcuerdoReparatorio.FechaLimiteCumplimiento,
                    StatusRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                    RespuestaCoordinadorJuridico = a.AcuerdoReparatorio.RespuestaCoordinadorJuridico,
                    fechaHoraRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.FechaHoraRespuestaCoordinadorJuridico,                    
                    StatusRespuestaAMP = a.AcuerdoReparatorio.StatusRespuestaAMP,
                    RespuestaAMP = a.AcuerdoReparatorio.RespuestaAMP,
                    FechaRespuestaAMP = a.AcuerdoReparatorio.FechaRespuestaAMP,
                    TextoAR = a.AcuerdoReparatorio.TextoAR,
                    uf_Distrito = a.AcuerdoReparatorio.uf_Distrito,
                    uf_DirSubProc = a.AcuerdoReparatorio.uf_DirSubProc,
                    uf_Agencia = a.AcuerdoReparatorio.uf_Agencia,
                    uf_Modulo = a.AcuerdoReparatorio.uf_Modulo,
                    uf_Nombre = a.AcuerdoReparatorio.uf_Nombre,
                    uf_Puesto = a.AcuerdoReparatorio.uf_Puesto,
                    Fechasise = a.AcuerdoReparatorio.Fechasise,
                    nosise = a.AcuerdoReparatorio.Sise,
                    TipoDocumento = a.AcuerdoReparatorio.TipoDocumento,
                    CountSessionsA = a.CountSessionsA,
                    Attached = a.Attached,
                    Conjunt = a.Conjunt
                })
                );                
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/AcuerdoReparatorios/ListarConjuntoxAcuerdo
        [HttpGet("[action]/{AcuerdoId}/{enlace}")]
        public async Task<IActionResult> ListarConjuntoxAcuerdo([FromRoute] Guid AcuerdoId, bool enlace)
        {
            try
            {                
                if (enlace)
                {
                    var agreement = await _context.AcuerdoReparatorios
                            .Where(n => n.IdAcuerdoReparatorio == AcuerdoId)
                            .ToListAsync();


                    var conjuntoA = await _context.AcuerdosConjuntos
                             .Include(a => a.AcuerdoReparatorio)
                             .Include(a => a.ConjuntoDerivaciones)
                             .Where(a => a.AcuerdoReparatorioId == agreement[0].Anexo)
                             .ToListAsync();

                    if (conjuntoA == null || conjuntoA.Count == 0)
                    {
                        return Ok(new { status = false });
                    }

                    return Ok(conjuntoA.Select(a => new ConjuntsWithAgreementViewModel
                    {
                        //campos de conjuntos
                        ConjuntoDerivacionesId = a.ConjuntoDerivaciones.IdConjuntoDerivaciones,
                        EnvioId = a.ConjuntoDerivaciones.EnvioId,
                        SolicitadosC = a.ConjuntoDerivaciones.SolicitadosC,
                        RequeridosC = a.ConjuntoDerivaciones.RequeridosC,
                        DelitosC = a.ConjuntoDerivaciones.DelitosC,
                        NombreS = a.ConjuntoDerivaciones.NombreS,
                        DireccionS = a.ConjuntoDerivaciones.DireccionS,
                        TelefonoS = a.ConjuntoDerivaciones.TelefonoS,
                        ClasificacionS = a.ConjuntoDerivaciones.ClasificacionS,
                        NombreR = a.ConjuntoDerivaciones.NombreR,
                        DireccionR = a.ConjuntoDerivaciones.DireccionR,
                        TelefonoR = a.ConjuntoDerivaciones.TelefonoR,
                        ClasificacionR = a.ConjuntoDerivaciones.ClasificacionR,
                        NombreD = a.ConjuntoDerivaciones.NombreD,
                        NoOficio = a.ConjuntoDerivaciones.NoOficio,
                        ResponsableJR = a.ConjuntoDerivaciones.ResponsableJR,
                        NombreSubDirigido = a.ConjuntoDerivaciones.NombreSubDirigido,
                        //campos de acuerdo
                        IdAcuerdoReparatorio = agreement[0].IdAcuerdoReparatorio,
                        StatusConclusion = agreement[0].StatusConclusion,
                        StatusCumplimiento = agreement[0].StatusCumplimiento,
                        TipoPago = agreement[0].TipoPago,
                        MetodoUtilizado = agreement[0].MetodoUtilizado,
                        MontoTotal = agreement[0].MontoTotal,
                        ObjetoEspecie = agreement[0].ObjectoEspecie,
                        NoTotalParcialidades = agreement[0].NoTotalParcialidades,
                        Periodo = agreement[0].Periodo,
                        money = agreement[0].MoneyChain,
                        species = agreement[0].SpeciesChain,
                        FechaCelebracionAcuerdo = agreement[0].FechaCelebracionAcuerdo,
                        FechaLimiteCumplimiento = agreement[0].FechaLimiteCumplimiento,
                        StatusRespuestaCoordinadorJuridico = agreement[0].StatusRespuestaCoordinadorJuridico,
                        RespuestaCoordinadorJuridico = agreement[0].RespuestaCoordinadorJuridico,
                        fechaHoraRespuestaCoordinadorJuridico = agreement[0].FechaHoraRespuestaCoordinadorJuridico,
                        StatusRespuestaAMP = agreement[0].StatusRespuestaAMP,
                        RespuestaAMP = agreement[0].RespuestaAMP,
                        FechaRespuestaAMP = agreement[0].FechaRespuestaAMP,
                        TextoAR = agreement[0].TextoAR,
                        uf_Distrito = agreement[0].uf_Distrito,
                        uf_DirSubProc = agreement[0].uf_DirSubProc,
                        uf_Agencia = agreement[0].uf_Agencia,
                        uf_Modulo = agreement[0].uf_Modulo,
                        uf_Nombre = agreement[0].uf_Nombre,
                        uf_Puesto = agreement[0].uf_Puesto,
                        Fechasise = agreement[0].Fechasise,
                        nosise = agreement[0].Sise,
                        TipoDocumento = agreement[0].TipoDocumento,
                        CountSessionsA = a.CountSessionsA,
                        Anexo = agreement[0].Anexo,
                        StatusAnexo = agreement[0].StatusAnexo,
                        Attached = a.Attached,
                        Conjunt = a.Conjunt
                    })
                    );
                }
                else
                {
                    var listaConjunto = await _context.AcuerdosConjuntos
                          .Include(a => a.AcuerdoReparatorio)
                          .Include(a => a.ConjuntoDerivaciones)
                          .Where(a => a.AcuerdoReparatorioId == AcuerdoId)
                          .ToListAsync();

                    if (listaConjunto == null || listaConjunto.Count == 0)
                    {
                        return Ok(new { status = false });
                    }

                    return Ok(listaConjunto.Select(a => new ConjuntsWithAgreementViewModel
                    {
                        //campos de conjuntos
                        ConjuntoDerivacionesId = a.ConjuntoDerivaciones.IdConjuntoDerivaciones,
                        EnvioId = a.ConjuntoDerivaciones.EnvioId,
                        SolicitadosC = a.ConjuntoDerivaciones.SolicitadosC,
                        RequeridosC = a.ConjuntoDerivaciones.RequeridosC,
                        DelitosC = a.ConjuntoDerivaciones.DelitosC,
                        NombreS = a.ConjuntoDerivaciones.NombreS,
                        DireccionS = a.ConjuntoDerivaciones.DireccionS,
                        TelefonoS = a.ConjuntoDerivaciones.TelefonoS,
                        ClasificacionS = a.ConjuntoDerivaciones.ClasificacionS,
                        NombreR = a.ConjuntoDerivaciones.NombreR,
                        DireccionR = a.ConjuntoDerivaciones.DireccionR,
                        TelefonoR = a.ConjuntoDerivaciones.TelefonoR,
                        ClasificacionR = a.ConjuntoDerivaciones.ClasificacionR,
                        NombreD = a.ConjuntoDerivaciones.NombreD,
                        NoOficio = a.ConjuntoDerivaciones.NoOficio,
                        ResponsableJR = a.ConjuntoDerivaciones.ResponsableJR,
                        NombreSubDirigido = a.ConjuntoDerivaciones.NombreSubDirigido,
                        //campos de acuerdo
                        IdAcuerdoReparatorio = a.AcuerdoReparatorio.IdAcuerdoReparatorio,
                        StatusConclusion = a.AcuerdoReparatorio.StatusConclusion,
                        StatusCumplimiento = a.AcuerdoReparatorio.StatusCumplimiento,
                        TipoPago = a.AcuerdoReparatorio.TipoPago,
                        MetodoUtilizado = a.AcuerdoReparatorio.MetodoUtilizado,
                        MontoTotal = a.AcuerdoReparatorio.MontoTotal,
                        ObjetoEspecie = a.AcuerdoReparatorio.ObjectoEspecie,
                        NoTotalParcialidades = a.AcuerdoReparatorio.NoTotalParcialidades,
                        Periodo = a.AcuerdoReparatorio.Periodo,
                        money = a.AcuerdoReparatorio.MoneyChain,
                        species = a.AcuerdoReparatorio.SpeciesChain,
                        FechaCelebracionAcuerdo = a.AcuerdoReparatorio.FechaCelebracionAcuerdo,
                        FechaLimiteCumplimiento = a.AcuerdoReparatorio.FechaLimiteCumplimiento,
                        StatusRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                        RespuestaCoordinadorJuridico = a.AcuerdoReparatorio.RespuestaCoordinadorJuridico,
                        fechaHoraRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.FechaHoraRespuestaCoordinadorJuridico,
                        StatusRespuestaAMP = a.AcuerdoReparatorio.StatusRespuestaAMP,
                        RespuestaAMP = a.AcuerdoReparatorio.RespuestaAMP,
                        FechaRespuestaAMP = a.AcuerdoReparatorio.FechaRespuestaAMP,
                        TextoAR = a.AcuerdoReparatorio.TextoAR,
                        uf_Distrito = a.AcuerdoReparatorio.uf_Distrito,
                        uf_DirSubProc = a.AcuerdoReparatorio.uf_DirSubProc,
                        uf_Agencia = a.AcuerdoReparatorio.uf_Agencia,
                        uf_Modulo = a.AcuerdoReparatorio.uf_Modulo,
                        uf_Nombre = a.AcuerdoReparatorio.uf_Nombre,
                        uf_Puesto = a.AcuerdoReparatorio.uf_Puesto,
                        Fechasise = a.AcuerdoReparatorio.Fechasise,
                        nosise = a.AcuerdoReparatorio.Sise,
                        TipoDocumento = a.AcuerdoReparatorio.TipoDocumento,
                        CountSessionsA = a.CountSessionsA,
                        Anexo = a.AcuerdoReparatorio.Anexo,
                        StatusAnexo = a.AcuerdoReparatorio.StatusAnexo,
                        Attached = a.Attached,
                        Conjunt = a.Conjunt
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

        //GET: api/AcuerdoReparatorios/ListConjuntsWithAttached
        [HttpGet("[action]/{idConjunto}")]
        public async Task<IActionResult> ListConjuntsWithAttached([FromRoute] Guid idConjunto)
        {
            try
            {
                string busquedaRA = @"select * from JR_CONJUNTODERIVACIONES jc where jc.IdConjuntoDerivaciones in (select jsc.ConjuntoDerivacionesId from JR_SESION js join JR_SESIONES_CONJUNTOS jsc on js.IdSesion = jsc.SesionId where jsc.ConjuntoDerivacionesId = @Conjunto and StatusSesion = 'Se crea anexo a acuerdo reparatorio')";


                List<SqlParameter> fb = new List<SqlParameter>();
                fb.Add(new SqlParameter("@conjunto", idConjunto));

                var listaC = await _context.ConjuntoDerivaciones.FromSqlRaw(busquedaRA, fb.ToArray()).ToListAsync();

                if(listaC == null || !listaC.Any())
                {
                    return Ok(new { Anexo = false });
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

        [HttpPost("[action]")]
        public async Task<IActionResult> CrearAcuerdoConjunto([FromBody] AcuerdosConjunto model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                AcuerdosConjunto AC = new AcuerdosConjunto
                {
                    AcuerdoReparatorioId = model.AcuerdoReparatorioId,
                    ConjuntoDerivacionesId = model.ConjuntoDerivacionesId
                };

                _context.AcuerdosConjuntos.Add(AC);

                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return BadRequest(new { error = e });
            }

            return Ok();
        }

        // GET: api/AcuerdoReparatorios/ListaARC
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{conjuntoderivacionesId}")]
        public async Task<IActionResult> ListaARC([FromRoute] Guid conjuntoderivacionesId)
        {
            var listaAR = await _context.AcuerdosConjuntos
                                      .Include(x => x.AcuerdoReparatorio)
                                      .Where(x => x.AcuerdoReparatorio.IdAcuerdoReparatorio == x.AcuerdoReparatorioId)
                                      .Where(x => x.ConjuntoDerivacionesId == conjuntoderivacionesId)
                                      .ToListAsync();

            if (listaAR == null)
            {

                return Ok(new { NoHayAR = 1 });
            }
            return Ok(listaAR.Select(a => new GET_AcuerdoReparatorioViewModel
                {
                    IdAcuerdoReparatorio = a.AcuerdoReparatorio.IdAcuerdoReparatorio,
                    TipoDocumento = a.AcuerdoReparatorio.TipoDocumento,
                    EnvioId = a.AcuerdoReparatorio.EnvioId,
                    NombreSolicitante = a.AcuerdoReparatorio.NombreSolicitante,
                    NombreRequerdio = a.AcuerdoReparatorio.NombreRequerdio,
                    NUC = a.AcuerdoReparatorio.NUC,
                    nosise = a.AcuerdoReparatorio.Sise,
                    fechasise = a.AcuerdoReparatorio.Fechasise,
                    NoExpediente = a.AcuerdoReparatorio.NoExpediente,
                    StatusConclusion = a.AcuerdoReparatorio.StatusConclusion,
                    StatusCumplimiento = a.AcuerdoReparatorio.StatusCumplimiento,
                    MetodoUtilizado = a.AcuerdoReparatorio.MetodoUtilizado,
                    TipoPago = a.AcuerdoReparatorio.TipoPago,
                    MontoTotal = a.AcuerdoReparatorio.MontoTotal,
                    ObjectoEspecie = a.AcuerdoReparatorio.ObjectoEspecie,
                    NoTotalParcialidades = a.AcuerdoReparatorio.NoTotalParcialidades,
                    Periodo = a.AcuerdoReparatorio.Periodo,
                    FechaCelebracionAcuerdo = a.AcuerdoReparatorio.FechaCelebracionAcuerdo,
                    FechaLimiteCumplimiento = a.AcuerdoReparatorio.FechaLimiteCumplimiento,
                    StatusRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                    RespuestaCoordinadorJuridico = a.AcuerdoReparatorio.RespuestaCoordinadorJuridico,
                    FechaHoraRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.FechaHoraRespuestaCoordinadorJuridico,
                    StatusRespuestaAMP = a.AcuerdoReparatorio.StatusRespuestaAMP,
                    RespuestaAMP = a.AcuerdoReparatorio.RespuestaAMP,
                    FechaRespuestaAMP = a.AcuerdoReparatorio.FechaRespuestaAMP,
                    TextoAR = a.AcuerdoReparatorio.TextoAR,
                    uf_Distrito = a.AcuerdoReparatorio.uf_Distrito,
                    uf_DirSubProc = a.AcuerdoReparatorio.uf_DirSubProc,
                    uf_Agencia = a.AcuerdoReparatorio.uf_Agencia,
                    uf_Modulo = a.AcuerdoReparatorio.uf_Modulo,
                    uf_Nombre = a.AcuerdoReparatorio.uf_Nombre,
                    uf_Puesto = a.AcuerdoReparatorio.uf_Puesto,
                })
            );
        }

        //DELETE: api/AcuerdoReparatorios/DeletebyAR
        [HttpDelete("[action]/{idps}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid idps)
        {
            var seguimiento = await _context.SeguimientoCumplimientos.Where(a => a.AcuerdoReparatorioId == idps).ToListAsync();

            if (!seguimiento.Any())
            {
                return Ok(new { count = 0 });
            }

            _context.SeguimientoCumplimientos.RemoveRange(seguimiento);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { delete = true });
            }
            catch (Exception ex)
            {
                return Ok(new {delete = false});
            }
        }

        // API: LISTA LOS DISTRITOS POR COORDINACION
        //[Authorize(Roles = "Director, Administrador, Coordinador, Facilitador")]
        [HttpGet("[action]/{usuarioId}")]
        public async Task<IActionResult> DistritoC([FromRoute] Guid usuarioId)
        {
            try { 
            var Tabla = await _context.CoordinacionDistritos
                                      .Include(x => x.Distrito)
                                      .Where(x => x.UsuarioId == usuarioId)
                                      .ToListAsync();

            return Ok(Tabla.Select(v => new GET_CoordinacionDistritos
            {
                IdCoordinacionDistritos = v.IdCoordinacionDistritos,
                DistritoId = v.DistritoId,
                NombreDis = v.Distrito.Nombre,

            }));

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        //Api para listar acuerdos de todo un distrito
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IActionResult> Acuerdosxdistrito([FromRoute] Guid distritoId)
        {
            try
            {
                //Por medio de esta conexion se hace la conexion a la ip del otro servidor, comprobar que la cadena de conexiones del back de jr tenga todas las conexiones
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + distritoId.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var Tabla = await ctx.AcuerdoReparatorios
                                         .Include(x => x.Envio)
                                         .Include(x => x.Envio.Expediente)
                                         .Where(x => x.Envio.Expediente.DistritoIdDestino == distritoId)
                                         .ToListAsync();


                    return Ok(Tabla.Select(a => new GET_AcuerdoxDistrito
                    {
                        //Acuerdos 
                        IdAcuerdoReparatorio = a.IdAcuerdoReparatorio,
                        EnvioId = a.EnvioId,
                        NombreSolicitante = a.NombreSolicitante,
                        NombreRequerdio = a.NombreRequerdio,
                        NUC = a.NUC,
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
                        RespuestaCoordinadorJuridico = a.StatusRespuestaCoordinadorJuridico,
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
                        nosise = a.Sise,
                        fechasise = a.Fechasise,

                        AutoridadqueDeriva = a.Envio.AutoridadqueDeriva,
                        uqe_Distrito = a.Envio.uqe_Distrito,
                        uqe_DirSubProc = a.Envio.uqe_DirSubProc,
                        uqe_Agencia = a.Envio.uqe_Agencia,
                        uqe_Modulo = a.Envio.uqe_Modulo,
                        uqe_Nombre = a.Envio.uqe_Nombre,
                        uqe_Puesto = a.Envio.uqe_Puesto,
                        StatusGeneral = a.Envio.StatusGeneral,
                        RespuestaExpediente = a.Envio.RespuestaExpediente,
                        FechaRegistro = a.Envio.FechaRegistro,
                        FechaCierre = a.Envio.FechaCierre,


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

        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{idDistrito}/{EnvioId}/{SolReq}")]
        public async Task<IActionResult> ListarSolicitantesRequeridosxDis([FromRoute] Guid EnvioId, string SolReq, Guid IdDistrito)
        {

            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    SqlParameter pEnvio = new SqlParameter("@envio", EnvioId);
                    SqlParameter pSolReq = new SqlParameter("@solreq", SolReq);
                    SqlParameter pDistrito = new SqlParameter("@idDistrito", IdDistrito);
                    var consulta = await ctx.ConsultaSolicitanteRequeridos.FromSqlRaw(@"SELECT P.*,R.IdRAP,SR.EnvioId,R.ClasificacionPersona,SR.Tipo,SR.IdRSolicitanteRequerido,CONCAT (CASE WHEN (DP.Calle ='') 
                                                                                 THEN 'SIN CALLE' ELSE DP.Calle END,' ',CASE WHEN (DP.NoExt ='') THEN 'S/N' ELSE DP.NoExt END ,' ',CASE WHEN (DP.NoInt ='0' OR DP.NoInt ='')  
                                                                                  THEN '' ELSE CONCAT('No Int. ',DP.NoInt) END,'Colonia: ',  CASE WHEN (DP.Localidad ='') THEN 'N/A' ELSE DP.Localidad END ,', ',CASE WHEN (DP.Municipio ='')  
                                                                                   THEN 'SIN MUNICIPIO' ELSE DP.Municipio END,' ',CASE WHEN (DP.Estado ='') THEN 'SIN ESTADO' ELSE DP.Estado END,', ',CASE WHEN (DP.Pais ='') 
                                                                                  THEN 'SIN PAIS' ELSE DP.Pais END,' CP ',DP.CP ) as direccion FROM JR_SOLICITANTEREQUERIDO SR INNER JOIN CAT_PERSONA P ON P.IdPersona=SR.PersonaId 
                                                                                   INNER JOIN CAT_RAP R ON R.PersonaId=SR.PersonaId INNER JOIN CAT_DIRECCION_PERSONAL DP ON DP.PersonaId=P.IdPersona 
                                                                                   INNER JOIN JR_ENVIO e ON SR.EnvioId = e.IdEnvio INNER JOIN JR_EXPEDIENTE x ON e.ExpedienteId = x.IdExpediente 
                                                                                   WHERE SR.EnvioId=@envio AND SR.Tipo LIKE @solreq AND x.DistritoIdDestino = @idDistrito", pEnvio, pSolReq, pDistrito)                   
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