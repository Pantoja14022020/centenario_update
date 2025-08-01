using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.RActosInvestigacion;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.RActosInvestigacion;
using SIIGPP.Entidades.M_Administracion;

using SIIGPP.CAT.FilterClass;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RActosInvestigacionController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public RActosInvestigacionController(DbContextSIIGPP context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        // GET: api/RActosInvestigacion/Listarporid
        [Authorize(Roles = " Recepción,Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IEnumerable<RactoInvestigacionViewModel>> Listarporid([FromRoute] Guid rHechoId)
        {
            var dil = await _context.RActoInvestigacions
                            .Include(a => a.RHecho)
                            .OrderByDescending(x => x.FechaSys)
                            .Where(x => x.RHechoId == rHechoId).ToListAsync();


            return dil.Select(a => new RactoInvestigacionViewModel

            {
                /*********************************************/

                IdRActosInvestigacion = a.IdRActosInvestigacion,
                RHechoId = a.RHechoId,
                FechaSolicitud = a.FechaSolicitud,
                Status = a.Status,
                Servicios = a.Servicios,
                Especificaciones = a.Especificaciones,
                Cdetenido = a.Cdetenido,
                Respuestas = a.Respuestas, 
                NUC = a.NUC,
                Textofinal = a.Textofinal,
                FechaSys = a.FechaSys,
                UDirSubPro = a.UDirSubPro,
                UUsuario = a.UUsuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                UAgencia = a.UAgencia,
                NumeroOficio = a.NumeroOficio,
                NodeSolicitud = a.NodeSolicitud,
                NumeroDistrito = a.NumeroDistrito,
                NodeSolicitudf = a.NumeroDistrito+"/"+a.NodeSolicitud,
                EtapaInicial = a.EtapaInicial,
                DSPDEstino = a.DSPDEstino,
                DistritoId = a.DistritoId
            });

        }

        // POST: api/RActosInvestigacion/Crear
        [Authorize(Roles = " Recepción,Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear(CrearViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var EI = await _context.HistorialCarpetas
                .Where(a => a.RHechoId == model.RHechoId)
                .Where(a => a.Detalle == "VINCULACION APROCESO")
                .OrderByDescending(a => a.Fechasys)
                .FirstOrDefaultAsync();

            Boolean eic = false;

            if (EI != null) eic = true;
            else eic = false;

            DateTime fecha = System.DateTime.Now;

            RActoInvestigacion InsertarRD = new RActoInvestigacion
            {
               
                RHechoId = model.RHechoId,
                FechaSolicitud = model.FechaSolicitud,
                Status = model.Status,
                Servicios = model.Servicios,
                Especificaciones = model.Especificaciones,
                Cdetenido = model.Cdetenido,
                Respuestas = model.Respuestas,
                NUC = model.NUC,
                Textofinal = model.Textofinal,
                FechaSys = fecha,
                UDirSubPro = model.UDirSubPro,
                UUsuario = model.UUsuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                UAgencia = model.UAgencia,
                NumeroOficio = model.NumeroOficio,
                NodeSolicitud = model.NodeSolicitud,
                NumeroDistrito = model.NumeroDistrito,
                EtapaInicial = eic,
                DSPDEstino = model.DSPDEstino,
                DistritoId = model.DistritoId
            };

            _context.RActoInvestigacions.Add(InsertarRD);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { idactoinve = InsertarRD.IdRActosInvestigacion });
                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }


        }

        // POST: api/RActosInvestigacion/Creardetalle
        [Authorize(Roles = " Recepción,Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Creardetalle(DetalleCrearViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            ActosInDetalle Insertardetalle = new ActosInDetalle
            {

                RActosInvestigacionId = model.RActosInvestigacionId,
                Servicio = model.Servicio,
                ServicioNM = model.ServicioNM,
                Status = model.Status,
                TextoFinal = model.TextoFinal,
                FechaRecibido = model.FechaRecibido,
                FechaAceptado = model.FechaAceptado,
                FechaFinalizado = model.FechaFinalizado,
                FechaEntregado = model.FechaEntregado,
                UltmimoStatus = fecha,
                Respuesta = model.Respuesta,
                Conclusion = model.Conclusion,
                FechaSys = fecha,
                UDirSubPro = model.UDirSubPro,
                UUsuario = model.UUsuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                UAgencia = model.UAgencia
            };

            _context.ActosInDetalles.Add(Insertardetalle);
            try
            {
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

        // GET: api/RActosInvestigacion/ListarDetallesporActo
        [Authorize(Roles = " Agente-Policía, Recepción,Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Comandante Unidad,Oficialia de partes,Recepción")]
        [HttpGet("[action]/{ActosInvestigacionId}")]
        public async Task<IEnumerable<ActoDetalleViewModel>> ListarDetallesporActo([FromRoute] Guid ActosInvestigacionId)
        {
            var dil = await _context.ActosInDetalles
                            .Where(a => a.RActosInvestigacionId == ActosInvestigacionId)
                            .OrderBy(a => a.ServicioNM)
                            .ToListAsync();


            return dil.Select(a => new ActoDetalleViewModel

            {
                /*********************************************/

                IdActosInDetetalle = a.IdActosInDetetalle,
                RActosInvestigacionId = a.RActosInvestigacionId,
                Servicio = a.Servicio,
                ServicioNM = a.ServicioNM,
                Status = a.Status,
                TextoFinal = a.TextoFinal,
                FechaRecibido = a.FechaRecibido,
                FechaAceptado = a.FechaAceptado,
                FechaFinalizado = a.FechaFinalizado,
                FechaEntregado = a.FechaEntregado,
                UltmimoStatus = a.UltmimoStatus,
                Respuesta = a.Respuesta,
                Conclusion = a.Conclusion,
                FechaSys = a.FechaSys,
                UDirSubPro = a.UDirSubPro,
                UUsuario = a.UUsuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                UAgencia = a.UAgencia


            });

        }


        // PUT: api/RActosInvestigacion/ActualizarstatusDetalle
        [HttpPut("[action]")]
        [Authorize(Roles = "Director-Subprocurador ,Oficialia de partes,Coordinador-Jurídico,Agente-Policía,Administrador,Comandante Unidad")]
        public async Task<IActionResult> ActualizarstatusDetalle([FromBody] ActualizarDetalleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var racto = await _context.ActosInDetalles.FirstOrDefaultAsync(a => a.IdActosInDetetalle == model.IdActosInDetetalle);

            if (racto == null)
            {
                return NotFound();
            }

            racto.Status = model.Status;
            racto.Conclusion = model.Conclusion;
            racto.Respuesta = model.Respuesta;
            racto.FechaFinalizado = model.FechaFinalizado;

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



        // GET: api/RActosInvestigacion/Listar
        [Authorize(Roles = " Recepción,Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Oficialia de partes,Comandante General,Recepción")]
        [HttpGet("[action]/{IdPanel}/{Iddistrito}")]
        public async Task<IEnumerable<RactoInvestigacionViewModel>> Listar([FromRoute] Guid IdPanel, Guid Iddistrito)
        {
            var dil = await _context.RActoInvestigacions
                            .Include(a => a.RHecho)
                            .OrderByDescending(a => a.FechaSys)
                            .Where(a => a.DistritoId == Iddistrito)
                            .Where(a => a.DSPDEstino == IdPanel)
                            .ToListAsync();


            return dil.Select(a => new RactoInvestigacionViewModel

            {
                /*********************************************/

                IdRActosInvestigacion = a.IdRActosInvestigacion,
                RHechoId = a.RHechoId,
                FechaSolicitud = a.FechaSolicitud,
                Status = a.Status,
                Servicios = a.Servicios,
                Especificaciones = a.Especificaciones,
                Cdetenido = a.Cdetenido,
                Respuestas = a.Respuestas,
                NUC = a.NUC,
                Textofinal = a.Textofinal,
                FechaSys = a.FechaSys,
                UDirSubPro = a.UDirSubPro,
                UUsuario = a.UUsuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                UAgencia = a.UAgencia,
                NumeroOficio = a.NumeroOficio,
                NodeSolicitud = a.NodeSolicitud,
                NumeroDistrito = a.NumeroDistrito,
                NodeSolicitudf = a.NumeroDistrito + "/" + a.NodeSolicitud,
                EtapaInicial = a.EtapaInicial,
                DSPDEstino = a.DSPDEstino,
                DistritoId = a.DistritoId

            });

        }

        // PUT: api/RActosInvestigacion/Actualizarstatus
        [HttpPut("[action]")]
        [Authorize(Roles = "Director-Subprocurador ,Oficialia de partes,Comandante General,Coordinador-Jurídico,Agente-Policía,Administrador,Comandante Unidad")]
        public async Task<IActionResult> Actualizarstatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var racto = await _context.RActoInvestigacions.FirstOrDefaultAsync(a => a.IdRActosInvestigacion == model.IdRActosInvestigacion);

            if (racto == null)
            {
                return NotFound();
            }

            racto.Status = model.Status;


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



        // PUT: api/RActosInvestigacion/ActualizarstatusRespuesta
        [Authorize(Roles = "Director-Subprocurador ,Oficialia de partes,Comandante General,Coordinador-Jurídico,Administrador,Comandante Unidad")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarstatusRespuesta([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var racto = await _context.RActoInvestigacions.FirstOrDefaultAsync(a => a.IdRActosInvestigacion == model.IdRActosInvestigacion);

            if (racto == null)
            {
                return NotFound();
            }

            racto.Status = model.Status;
            racto.Respuestas = model.Respuestas;


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

        // PUT: api/RActosInvestigacion/ActualizarstatusDetalleEntregado
        [Authorize(Roles = "Administrador,Agente-Policía,Oficialia de partes,Comandante Unidad")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarstatusDetalleEntregado([FromBody] ActualizarDetalleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var racto = await _context.ActosInDetalles.FirstOrDefaultAsync(a => a.IdActosInDetetalle == model.IdActosInDetetalle);

            if (racto == null)
            {
                return NotFound();
            }

            racto.Status = model.Status;
            racto.FechaEntregado = model.FechaEntregado;

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

        // GET: api/RActosInvestigacion/ObtenernumeroMaximoporDistrito
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción" +
            "")]
        [HttpGet("[action]/{Ndistrito}")]
        public async Task<IActionResult> ObtenernumeroMaximoporDistrito([FromRoute] string Ndistrito)
        {
            try
            {
                var da = await _context.RActoInvestigacions
                        .Include(x => x.RHecho)
                        .Where(x => x.NumeroDistrito == Ndistrito)
                        .Select(x => new { x.NodeSolicitud })
                        .ToListAsync();
                var _da = da.OrderByDescending(x => Int32.Parse(x.NodeSolicitud)).FirstOrDefault();


                if (da == null | _da == null)
                {
                    return Ok(new { NumeroMaximo = 0 });

                }
                return Ok(new DatosExtrasViewModel
                {
                    NumeroMaximo = _da.NodeSolicitud

                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }


        // GET: api/RActosInvestigacion/SolicitudesTotales
        [HttpGet("[action]/{SolicitudesPIEstadistica}")]
        public async Task<IEnumerable<EstadisticaMCViewModel>> SolicitudesTotales([FromQuery] SolicitudesPIEstadistica SolicitudesPIEstadistica)
        {
            var pis = await _context.RActoInvestigacions
                .Where(a => a.RHecho.NucId != null)
                .Where(a => SolicitudesPIEstadistica.AutorizacionJuez == "Si" ? a.Cdetenido  : SolicitudesPIEstadistica.AutorizacionJuez == "No" ? !a.Cdetenido : 1 == 1)
                .Where(a => SolicitudesPIEstadistica.DatosGenerales.Distritoact ? a.RHecho.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == SolicitudesPIEstadistica.DatosGenerales.Distrito : 1 == 1)
                .Where(a => SolicitudesPIEstadistica.DatosGenerales.Dspact ? a.RHecho.ModuloServicio.Agencia.DSP.IdDSP == SolicitudesPIEstadistica.DatosGenerales.Dsp : 1 == 1)
                .Where(a => SolicitudesPIEstadistica.DatosGenerales.Agenciaact ? a.RHecho.ModuloServicio.Agencia.IdAgencia == SolicitudesPIEstadistica.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.RHecho.FechaElevaNuc2 >= SolicitudesPIEstadistica.DatosGenerales.Fechadesde)
                .Where(a => a.RHecho.FechaElevaNuc2 <= SolicitudesPIEstadistica.DatosGenerales.Fechahasta)
                .Where(a => SolicitudesPIEstadistica.DatosGeneralesPI.Distritoact ? new Guid(a.Servicios) == SolicitudesPIEstadistica.DatosGenerales.Distrito : 1 == 1)
                .Where(a => SolicitudesPIEstadistica.DatosGeneralesPI.Dspact ? a.DSPDEstino == SolicitudesPIEstadistica.DatosGenerales.Dsp : 1 == 1)
                .Where(a => a.FechaSys >= SolicitudesPIEstadistica.DatosGeneralesPI.Fechadesde)
                .Where(a => a.FechaSys <= SolicitudesPIEstadistica.DatosGeneralesPI.Fechahasta)
                .ToListAsync();

            IEnumerable<EstadisticaMCViewModel> items = new EstadisticaMCViewModel[] { };

            IEnumerable<EstadisticaMCViewModel> ReadLines(int cantidad, string tipo)
            {
                IEnumerable<EstadisticaMCViewModel> item2;

                item2 = (new[]{new EstadisticaMCViewModel{
                                Cantidad = cantidad,
                                Tipo = tipo
                            }});

                return item2;
            }

            items = items.Concat(ReadLines(pis.Count , "Total de solicitudes a policia investigadora"));


            return items;
        }

        // GET: api/RActosInvestigacion/Eliminar
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
                var consultaActoInv = await _context.RActoInvestigacions.Where(a => a.IdRActosInvestigacion == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaActoInv == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningún acto de investigación con la información enviada" });
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
                            MovimientoId = new Guid("0e263a0a-ae70-481b-9258-723567bb69b8") // ESTE DATO HAY QUE BUSCARLO EN LA TABLA C_TIPO_MOV DE LA BD CENTENARIO_LOG
                        };

                        ctx.Add(laRegistro);
                        LogRActoInvestigacion actoInvestigacion= new LogRActoInvestigacion
                        {
                            LogAdmonId = gLog,
                            IdRActosInvestigacion = consultaActoInv.IdRActosInvestigacion,
                            RHechoId = consultaActoInv.RHechoId,
                            FechaSolicitud = consultaActoInv.FechaSolicitud,
                            Status = consultaActoInv.Status,
                            Servicios = consultaActoInv.Servicios,
                            Especificaciones = consultaActoInv.Especificaciones,
                            Cdetenido = consultaActoInv.Cdetenido,
                            Respuestas = consultaActoInv.Respuestas,
                            NUC = consultaActoInv.NUC,
                            Textofinal = consultaActoInv.Textofinal,
                            FechaSys = consultaActoInv.FechaSys,
                            UDirSubPro = consultaActoInv.UDirSubPro,
                            UUsuario = consultaActoInv.UUsuario,
                            UPuesto = consultaActoInv.UPuesto,
                            UModulo = consultaActoInv.UModulo,
                            UAgencia = consultaActoInv.UAgencia,
                            NumeroOficio = consultaActoInv.NumeroOficio,
                            NodeSolicitud = consultaActoInv.NodeSolicitud,
                            NumeroDistrito = consultaActoInv.NumeroDistrito,
                            EtapaInicial = consultaActoInv.EtapaInicial,
                            DSPDEstino = consultaActoInv.DSPDEstino,
                            DistritoId = consultaActoInv.DistritoId

                        };
                        ctx.Add(actoInvestigacion);
                        _context.Remove(consultaActoInv);

                        //SE APLICAN TODOS LOS CAMBIOS A LA BD
                        await ctx.SaveChangesAsync();
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "Acto de Investigación eliminado Correctamente" });
        }



        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/RActosInvestigacion/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var actosInvestigacion = await _context.RActoInvestigacions.Where(x => x.RHechoId == model.IdRHecho).ToListAsync();
            if (actosInvestigacion == null)
            {
                return Ok();
            }

            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    foreach (RActoInvestigacion actoInvestigacionActual in actosInvestigacion)
                    {
                        var insertarActoInvestigacion = await ctx.RActoInvestigacions.FirstOrDefaultAsync(a => a.IdRActosInvestigacion == actoInvestigacionActual.IdRActosInvestigacion);
                        if (insertarActoInvestigacion == null)
                        {
                            insertarActoInvestigacion = new RActoInvestigacion();
                            ctx.RActoInvestigacions.Add(insertarActoInvestigacion);
                        }

                        insertarActoInvestigacion.IdRActosInvestigacion = actoInvestigacionActual.IdRActosInvestigacion;
                        insertarActoInvestigacion.RHechoId = actoInvestigacionActual.RHechoId;
                        insertarActoInvestigacion.FechaSolicitud = actoInvestigacionActual.FechaSolicitud;
                        insertarActoInvestigacion.Status = actoInvestigacionActual.Status;
                        insertarActoInvestigacion.Servicios = actoInvestigacionActual.Servicios;
                        insertarActoInvestigacion.Cdetenido = actoInvestigacionActual.Cdetenido;
                        insertarActoInvestigacion.Respuestas = actoInvestigacionActual.Respuestas;
                        insertarActoInvestigacion.NUC = actoInvestigacionActual.NUC;
                        insertarActoInvestigacion.Textofinal = actoInvestigacionActual.Textofinal;
                        insertarActoInvestigacion.FechaSys = actoInvestigacionActual.FechaSys;
                        insertarActoInvestigacion.UDirSubPro = actoInvestigacionActual.UDirSubPro;
                        insertarActoInvestigacion.UUsuario = actoInvestigacionActual.UUsuario;
                        insertarActoInvestigacion.UPuesto = actoInvestigacionActual.UPuesto;
                        insertarActoInvestigacion.UModulo = actoInvestigacionActual.UModulo;
                        insertarActoInvestigacion.UAgencia = actoInvestigacionActual.UAgencia;
                        insertarActoInvestigacion.NumeroOficio = actoInvestigacionActual.NumeroOficio;
                        insertarActoInvestigacion.NodeSolicitud = actoInvestigacionActual.NodeSolicitud;
                        insertarActoInvestigacion.NumeroDistrito = actoInvestigacionActual.NumeroDistrito;
                        insertarActoInvestigacion.EtapaInicial = actoInvestigacionActual.EtapaInicial;
                        insertarActoInvestigacion.DSPDEstino = actoInvestigacionActual.DSPDEstino;
                        insertarActoInvestigacion.DistritoId = actoInvestigacionActual.DistritoId;

                        await ctx.SaveChangesAsync();
        

                    }
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }
        }

    }
}
