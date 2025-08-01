using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Diligencias;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.Diligencias;
using SIIGPP.CAT.FilterClass;
using SIIGPP.Entidades.M_Administracion;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using SIIGPP.CAT.Models.Victimas;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RDiligenciasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public RDiligenciasController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/RDiligencias/Listar
        [Authorize(Roles = "Administrador,Director,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IEnumerable<DiligenciasVieModel>> Listar([FromRoute] Guid rHechoId)
        {
            var dil = await _context.RDiligencias                          
                            .Include(a => a.RHecho)
                            .OrderByDescending(a => a.FechaSys)
                            .Where(a => a.rHechoId == rHechoId)
                            .ToListAsync();


            return dil.Select(a => new DiligenciasVieModel
            {
                /*********************************************/

                IdRDiligencias = a.IdRDiligencias,
                FechaSolicitud = a.FechaSolicitud,
                Dirigidoa = a.Dirigidoa,
                EmitidoPor = a.EmitidoPor,
                DirSubPro = a.DirSubPro,
                uDirSubPro = a.uDirSubPro,
                UPuesto = a.UPuesto,
                StatusRespuesta = a.StatusRespuesta,
                Servicio = a.Servicio,
                Especificaciones = a.Especificaciones,
                Prioridad = a.Prioridad,
                rHechoId = a.rHechoId,
                ASPId = a.ASPId,
                Modulo = a.Modulo,
                Agencia = a.Agencia,
                Respuestas = a.Respuestas,
                ConIndicio = a.ConIndicio,
                NUC = a.NUC,
                Textofinal = a.Textofinal,
                NumeroOficio = a.NumeroOficio,
                NodeSolicitud = a.NodeSolicitud,
                NumeroDistrito = a.NumeroDistrito,
                NodeSolicitudf = a.NumeroDistrito + "/" + a.NodeSolicitud,
                FechaSys = a.FechaSys,
                Lat = a.Lat,
                Lng = a.Lng,
                Dirigido = a.Dirigido,
                RecibidoF = a.RecibidoF,
                FechaRecibidoF = a.FechaRecibidoF,
                Respuesta = a.Respuesta,
                EtapaInicial = a.EtapaInicial,
                DSPDEstino = a.DSPDEstino,
                DistritoId = a.DistritoId


            });

        }


        // GET: api/RDiligencias/ObtenernumeroMaximoporDistrito
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{Ndistrito}")]
        public async Task<IActionResult> ObtenernumeroMaximoporDistrito([FromRoute] string Ndistrito)
        {

            try
            {
                string nmpd = @"select TOP 1 MAX(FechaSys) AS FechaSys, 
                                        NumeroDistrito, 
                                        NodeSolicitud, 
                                        IdRDiligencias 
                                        from CAT_RDILIGENCIAS cr 
                                        where NumeroDistrito = @numdis 
                                        and NodeSolicitud IS NOT NULL 
                                        GROUP BY FechaSys,
                                        NumeroDistrito,
                                        NodeSolicitud,
                                        IdRDiligencias 
                                        ORDER BY CAST(NodeSolicitud AS INT) DESC";

                List<SqlParameter> filtroNum = new List<SqlParameter>();
                filtroNum.Add(new SqlParameter("@numdis", Ndistrito));
                var nn = await _context.RDNum.FromSqlRaw(nmpd, filtroNum.ToArray()).FirstOrDefaultAsync();

                return Ok(new DatosExtrasViewModel
                {
                    NumeroMaximo = nn.NodeSolicitud
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        // POST: api/RDiligencias/CrearPI
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearPI(CrearViewModel model)

        {

            var EI = await _context.HistorialCarpetas
                .Where(a => a.RHechoId == model.rHechoId)
                .Where(a => a.Detalle == "VINCULACION APROCESO")
                .OrderByDescending(a => a.Fechasys)
                .FirstOrDefaultAsync();

            Boolean eic = false;

            if (EI != null) eic = true;
            else eic = false;


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            RDiligencias InsertarRD = new RDiligencias
            {

                FechaSolicitud = model.FechaSolicitud,
                Dirigidoa = model.Dirigidoa,
                DirSubPro = model.DirSubPro,
                EmitidoPor = model.EmitidoPor,
                uDirSubPro = model.uDirSubPro,
                UPuesto = model.UPuesto,
                StatusRespuesta = model.StatusRespuesta,
                Servicio = model.Servicio,
                Especificaciones = model.Especificaciones,
                rHechoId = model.rHechoId,
                ASPId = model.ASPId,
                Prioridad = model.Prioridad,
                FechaSys = fecha,
                Modulo = model.Modulo,
                Agencia = model.Agencia,
                Respuestas = model.Respuestas,
                ConIndicio = model.ConIndicio,
                NUC = model.NUC,
                Textofinal = model.Textofinal,
                NumeroOficio = model.NumeroOficio,
                NodeSolicitud = model.NodeSolicitud,
                NumeroDistrito = model.NumeroDistrito,
                Lat = model.Lat,
                Lng = model.Lng,
                Dirigido = model.Dirigido,
                RecibidoF = false,
                FechaRecibidoF = fecha,
                EtapaInicial = eic,
                DSPDEstino = model.DSPDEstino,
                DistritoId = model.DistritoId

            };



            _context.RDiligencias.Add(InsertarRD);


            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { iddili = InsertarRD.IdRDiligencias });
                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

#pragma warning disable CS0162 // Se detectó código inaccesible
            return Ok();
#pragma warning restore CS0162 // Se detectó código inaccesible
        }

        // GET: api/RDiligencias/ListarTodos
        [HttpGet("[action]/{IdPanel}/{Iddistrito}")]
        [Authorize(Roles = "Administrador,Director,Oficialia de partes")]
        public async Task<IEnumerable<DiligenciasSPViewModel>> ListarTodos([FromRoute] Guid IdPanel, Guid Iddistrito)
        {
            var dil = await _context.RDiligencias
                            .Include(a => a.RHecho)
                            .Include(a => a.ASP)
                            .Include(a => a.ASP.Agencia)
                            .Where(a => a.DistritoId == Iddistrito)
                            .Where(a => a.DSPDEstino == IdPanel)
                            .OrderByDescending(x => x.FechaSys)
                            .ToListAsync();

            return dil.Select(a => new DiligenciasSPViewModel

            {
                /*********************************************/

                IdRDiligencias = a.IdRDiligencias,
                FechaSolicitud = a.FechaSolicitud,
                Dirigidoa = a.Dirigidoa,
                EmitidoPor = a.EmitidoPor,
                DirSubPro = a.DirSubPro,
                uDirSubPro = a.uDirSubPro,
                UPuesto = a.UPuesto,
                StatusRespuesta = a.StatusRespuesta,
                Servicio = a.Servicio,
                Especificaciones = a.Especificaciones,
                Prioridad = a.Prioridad,
                rHechoId = a.rHechoId,
                ASPId = a.ASPId,
                Modulo = a.Modulo,
                Agencia = a.Agencia,
                AgenciaPericial = a.ASP.Agencia.Nombre,
                Respuestas = a.Respuestas,
                ConIndicio = a.ConIndicio,
                NUC = a.NUC,
                Textofinal = a.Textofinal,
                idserviciopericial = a.ASP.ServicioPericialId,
                idagencia = a.ASP.AgenciaId,
                NumeroOficio = a.NumeroOficio,
                NodeSolicitud = a.NodeSolicitud,
                NumeroDistrito = a.NumeroDistrito,
                NodeSolicitudf = a.NumeroDistrito + "/" + a.NodeSolicitud,
                FechaSys = a.FechaSys,
                Lat = a.Lat,
                Lng = a.Lng,
                Dirigido = a.Dirigido,
                RecibidoF = a.RecibidoF,
                FechaRecibidoF = a.FechaRecibidoF,
                EtapaInicial = a.EtapaInicial,
                DSPDEstino = a.DSPDEstino,
                DistritoId = a.DistritoId
            });

        }
        [Authorize(Roles = "Coordinador,Administrador,Perito")]
        // GET: api/RDiligencias/Listarporagencia
        [HttpGet("[action]/{nuc}")]

        public async Task<IEnumerable<DiligenciasSPViewModel>> FiltrarStatus([FromRoute] string nuc)
        {
            var fil = await _context.RDiligencias
                            .Where(a => a.NUC == nuc)
                            .Where(a => a.StatusRespuesta == "Finalizado")
                            .ToListAsync();
            return fil.Select(a => new DiligenciasSPViewModel

            {
                IdRDiligencias = a.IdRDiligencias,
                FechaSolicitud = a.FechaSolicitud,
                Dirigidoa = a.Dirigidoa,
                EmitidoPor = a.EmitidoPor,
                DirSubPro = a.DirSubPro,
                uDirSubPro = a.uDirSubPro,
                UPuesto = a.UPuesto,
                StatusRespuesta = a.StatusRespuesta,
                Servicio = a.Servicio,
                Especificaciones = a.Especificaciones,
                Prioridad = a.Prioridad,
                rHechoId = a.rHechoId,     
                Modulo = a.Modulo,
                Agencia = a.Agencia,         
                Respuestas = a.Respuestas,
                ConIndicio = a.ConIndicio,
                NUC = a.NUC,
                Textofinal = a.Textofinal,
                NumeroOficio = a.NumeroOficio,
                NodeSolicitud = a.NodeSolicitud,
                NumeroDistrito = a.NumeroDistrito,
                FechaSys = a.FechaSys,
                Dirigido = a.Dirigido,
                RecibidoF = a.RecibidoF,
                FechaRecibidoF = a.FechaRecibidoF,
                EtapaInicial = a.EtapaInicial,
                DSPDEstino = a.DSPDEstino,
                DistritoId = a.DistritoId

            });

            }

            [Authorize(Roles = "Coordinador,Administrador")]
        // GET: api/RDiligencias/Listarporagencia
        [HttpGet("[action]/{idagencia}/{IdPanel}/{Iddistrito}")]

        public async Task<IActionResult> Listarporagencia([FromRoute]Guid idagencia, Guid IdPanel, Guid Iddistrito)
        {
            try
            {

                        var dil = await _context.RDiligencias
                       .Where(a => a.ASP.AgenciaId == idagencia && a.DistritoId == Iddistrito && a.DSPDEstino == IdPanel)
                       .Include(a => a.RHecho)
                       .Include(a => a.ASP)
                       .Include(a => a.ASP.Agencia)
                       .OrderByDescending(x => x.Prioridad)
                       .OrderByDescending(x =>
                           x.StatusRespuesta == "Solicitado" ? 7 :
                           x.StatusRespuesta == "Enproceso" ? 6 :
                           x.StatusRespuesta == "Asignado" ? 5 :
                           x.StatusRespuesta == "Entregado" ? 4 :
                           x.StatusRespuesta == "Pospuesto" ? 3 :
                           x.StatusRespuesta == "Finalizado" ? 2 :
                           x.StatusRespuesta == "Rechazado" ? 1 : 0)
                       .ToListAsync();



                return Ok(dil.Select(a => new DiligenciasSPViewModel

                 {
                /*********************************************/

                IdRDiligencias = a.IdRDiligencias,
                FechaSolicitud = a.FechaSolicitud,
                Dirigidoa = a.Dirigidoa,
                EmitidoPor = a.EmitidoPor,
                DirSubPro = a.DirSubPro,
                uDirSubPro = a.uDirSubPro,
                UPuesto = a.UPuesto,
                StatusRespuesta = a.StatusRespuesta,
                Servicio = a.Servicio,
                Especificaciones = a.Especificaciones,
                Prioridad = a.Prioridad,
                rHechoId = a.rHechoId,
                ASPId = a.ASPId,
                Modulo = a.Modulo,
                Agencia = a.Agencia,
                AgenciaPericial = a.ASP.Agencia.Nombre,
                Respuestas = a.Respuestas,
                ConIndicio = a.ConIndicio,
                NUC = a.NUC,
                Textofinal = a.Textofinal,
                idserviciopericial = a.ASP.ServicioPericialId,
                idagencia = a.ASP.AgenciaId,
                NumeroOficio = a.NumeroOficio,
                NodeSolicitud = a.NodeSolicitud,
                NumeroDistrito = a.NumeroDistrito,
                NodeSolicitudf = a.NumeroDistrito + "/" + a.NodeSolicitud,
                FechaSys = a.FechaSys,
                Lat = a.Lat,
                Lng = a.Lng,
                Dirigido = a.Dirigido,
                RecibidoF = a.RecibidoF,
                FechaRecibidoF = a.FechaRecibidoF,
                EtapaInicial = a.EtapaInicial,
                DSPDEstino = a.DSPDEstino,
                DistritoId = a.DistritoId
            }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }

        }

        // PUT: api/RDiligencias/Actualizarstatus
        
        [HttpPut("[action]")]
        [Authorize(Roles = "Director,Subprocurador,Oficialia de partes,Coordinador,Perito,Administrador")]
        public async Task<IActionResult> Actualizarstatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var rdili = await _context.RDiligencias.FirstOrDefaultAsync(a => a.IdRDiligencias == model.IdRDiligencias);

            if (rdili == null)
            {
                return NotFound();
            }

            rdili.StatusRespuesta = model.StatusRespuesta;
            rdili.Respuesta = model.Respuesta;
           
            
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



        // PUT: api/RDiligencias/Actualizarstatus2
        [Authorize(Roles = "Director-Subprocurador,Oficialia de partes,Coordinador,Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizarstatus2([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var rdili = await _context.RDiligencias.FirstOrDefaultAsync(a => a.IdRDiligencias == model.IdRDiligencias);

            if (rdili == null)
            {
                return NotFound();
            }

            rdili.StatusRespuesta = model.StatusRespuesta;
            rdili.Respuestas = model.Respuestas;


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

        // PUT: api/RDiligencias/ActualizarfechaR
        [Authorize(Roles = "Director-Subprocurador,Oficialia de partes,Coordinador,Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarfechaR([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var rdili = await _context.RDiligencias.FirstOrDefaultAsync(a => a.IdRDiligencias == model.IdRDiligencias);

            if (rdili == null)
            {
                return NotFound();
            }

            rdili.RecibidoF = true;
            rdili.FechaRecibidoF = System.DateTime.Now;


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

        // GET: api/RDiligencias/SolicitudesTotales
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{SolicitudesSPEstadistica}")]
        public async Task<IEnumerable<SolicitudestotalesViewModel>> SolicitudesTotales([FromQuery] SolicitudesSPEstadistica SolicitudesSPEstadistica)
        {
            var sps = await _context.RDiligencias
                .Where(a => a.RHecho.NucId != null)
                .Where(a => SolicitudesSPEstadistica.DatosGenerales.Distritoact ? a.RHecho.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == SolicitudesSPEstadistica.DatosGenerales.Distrito : 1 == 1)
                .Where(a => SolicitudesSPEstadistica.DatosGenerales.Dspact ? a.RHecho.ModuloServicio.Agencia.DSP.IdDSP == SolicitudesSPEstadistica.DatosGenerales.Dsp : 1 == 1)
                .Where(a => SolicitudesSPEstadistica.DatosGenerales.Agenciaact ? a.RHecho.ModuloServicio.Agencia.IdAgencia == SolicitudesSPEstadistica.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.RHecho.FechaElevaNuc2 >= SolicitudesSPEstadistica.DatosGenerales.Fechadesde)
                .Where(a => a.RHecho.FechaElevaNuc2 <= SolicitudesSPEstadistica.DatosGenerales.Fechahasta)
                .Where(a => SolicitudesSPEstadistica.DatosGeneralesSP.Distritoact ? a.ASP.Agencia.DSP.DistritoId == SolicitudesSPEstadistica.DatosGeneralesSP.Distrito : 1 == 1)
                .Where(a => SolicitudesSPEstadistica.DatosGeneralesSP.Dspact ? a.ASP.Agencia.DSPId == SolicitudesSPEstadistica.DatosGeneralesSP.Dsp : 1 == 1)
                .Where(a => SolicitudesSPEstadistica.DatosGeneralesSP.Agenciaact ? a.ASP.AgenciaId == SolicitudesSPEstadistica.DatosGeneralesSP.Agencia : 1 == 1)
                .Where(a => a.FechaSys >= SolicitudesSPEstadistica.DatosGeneralesSP.Fechadesde)
                .Where(a => a.FechaSys <= SolicitudesSPEstadistica.DatosGeneralesSP.Fechahasta)
                .ToListAsync();

            var spsforaneas = await _context.RDiligenciasForaneas
                .Where(a => SolicitudesSPEstadistica.DatosGeneralesSP.Distritoact ? a.ASP.Agencia.DSP.DistritoId == SolicitudesSPEstadistica.DatosGeneralesSP.Distrito : 1 == 1)
                .Where(a => SolicitudesSPEstadistica.DatosGeneralesSP.Dspact ? a.ASP.Agencia.DSPId == SolicitudesSPEstadistica.DatosGeneralesSP.Dsp : 1 == 1)
                .Where(a => SolicitudesSPEstadistica.DatosGeneralesSP.Agenciaact ? a.ASP.AgenciaId == SolicitudesSPEstadistica.DatosGeneralesSP.Agencia : 1 == 1)
                .Where(a => a.FechaSys >= SolicitudesSPEstadistica.DatosGeneralesSP.Fechadesde)
                .Where(a => a.FechaSys <= SolicitudesSPEstadistica.DatosGeneralesSP.Fechahasta)
                .ToListAsync();


            IEnumerable<SolicitudestotalesViewModel> items = new SolicitudestotalesViewModel[] { };

            IEnumerable<SolicitudestotalesViewModel> ReadLines(int cantidad, string tipo)
            {
                IEnumerable<SolicitudestotalesViewModel> item2;

                item2 = (new[]{new SolicitudestotalesViewModel{
                                Cantidad = cantidad,
                                Tipo = tipo
                            }});

                return item2;
            }

            items = items.Concat(ReadLines(sps.Count + spsforaneas.Count, "Total de solicitudes a servicios periciales"));


            return items;

        }


        private bool RDiligenciasExists(Guid id)
        {
            return _context.RDiligencias.Any(e => e.IdRDiligencias == id);
        }

        // GET: api/RDiligencias/Eliminar
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
                var consultaDiligencia = await _context.RDiligencias.Where(a => a.IdRDiligencias == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaDiligencia == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ninguna diligencia con la información enviada" });
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
                            MovimientoId = new Guid("deb806a6-0cfb-4500-bef8-42372c249a2b") // ESTE DATO HAY QUE BUSCARLO EN LA TABLA C_TIPO_MOV DE LA BD CENTENARIO_LOG
                        };

                        ctx.Add(laRegistro);
                        LogRDiligencias diligencia = new LogRDiligencias
                        {
                            LogAdmonId = gLog,
                            IdRDiligencias = consultaDiligencia.IdRDiligencias,
                            rHechoId = consultaDiligencia.rHechoId,
                            FechaSolicitud = consultaDiligencia.FechaSolicitud,
                            FechaSys = consultaDiligencia.FechaSys,
                            Dirigidoa = consultaDiligencia.Dirigidoa,
                            DirSubPro = consultaDiligencia.DirSubPro,
                            EmitidoPor = consultaDiligencia.EmitidoPor,
                            uDirSubPro = consultaDiligencia.uDirSubPro,
                            UPuesto = consultaDiligencia.UPuesto,
                            StatusRespuesta = consultaDiligencia.StatusRespuesta,
                            Especificaciones = consultaDiligencia.Especificaciones,
                            Servicio = consultaDiligencia.Servicio,
                            Prioridad = consultaDiligencia.Prioridad,
                            ASPId = consultaDiligencia.ASPId,
                            Modulo = consultaDiligencia.Modulo,
                            Agencia = consultaDiligencia.Agencia,
                            Respuestas = consultaDiligencia.Respuestas,
                            ConIndicio = consultaDiligencia.ConIndicio,
                            NUC = consultaDiligencia.NUC,
                            Textofinal = consultaDiligencia.Textofinal,
                            NumeroOficio = consultaDiligencia.NumeroOficio,
                            NodeSolicitud = consultaDiligencia.NodeSolicitud,
                            NumeroDistrito = consultaDiligencia.NumeroDistrito,
                            Lat = consultaDiligencia.Lat,
                            Lng = consultaDiligencia.Lng,
                            Dirigido = consultaDiligencia.Dirigido,
                            RecibidoF = consultaDiligencia.RecibidoF,
                            FechaRecibidoF = consultaDiligencia.FechaRecibidoF,
                            Respuesta = consultaDiligencia.Respuesta,
                            EtapaInicial = consultaDiligencia.EtapaInicial,
                            DSPDEstino = consultaDiligencia.DSPDEstino,
                            DistritoId = consultaDiligencia.DistritoId

                        };
                        ctx.Add(diligencia);
                        _context.Remove(consultaDiligencia);

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
            return Ok(new { res = "success", men = "Diligencia eliminada Correctamente" });
        }


        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/RDiligencias/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var diligenciaBuscadas = await _context.RDiligencias.Where(x => x.rHechoId == model.IdRHecho).ToListAsync();
            if (diligenciaBuscadas == null)
            {
                return Ok();
            }
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    foreach (RDiligencias diligenciaActual in diligenciaBuscadas)
                    {
                        var insertarDiligencia = await ctx.RDiligencias.FirstOrDefaultAsync(a=> a.IdRDiligencias == diligenciaActual.IdRDiligencias);
                        if (insertarDiligencia == null)
                        {
                            insertarDiligencia = new RDiligencias();
                            ctx.RDiligencias.Add(insertarDiligencia);
                        }

                        insertarDiligencia.IdRDiligencias = diligenciaActual.IdRDiligencias;
                        insertarDiligencia.rHechoId = diligenciaActual.rHechoId;
                        insertarDiligencia.FechaSolicitud = diligenciaActual.FechaSolicitud;
                        insertarDiligencia.FechaSys = diligenciaActual.FechaSys;
                        insertarDiligencia.Dirigidoa = diligenciaActual.Dirigidoa;
                        insertarDiligencia.DirSubPro = diligenciaActual.DirSubPro;
                        insertarDiligencia.EmitidoPor = diligenciaActual.EmitidoPor;
                        insertarDiligencia.uDirSubPro = diligenciaActual.uDirSubPro;
                        insertarDiligencia.UPuesto = diligenciaActual.UPuesto;
                        insertarDiligencia.StatusRespuesta = diligenciaActual.StatusRespuesta;
                        insertarDiligencia.Servicio = diligenciaActual.Servicio;
                        insertarDiligencia.Especificaciones = diligenciaActual.Especificaciones;
                        insertarDiligencia.Prioridad = diligenciaActual.Prioridad;
                        insertarDiligencia.ASPId = diligenciaActual.ASPId;
                        insertarDiligencia.Modulo = diligenciaActual.Modulo;
                        insertarDiligencia.Agencia = diligenciaActual.Agencia;
                        insertarDiligencia.Respuestas = diligenciaActual.Respuestas;
                        insertarDiligencia.ConIndicio = diligenciaActual.ConIndicio;
                        insertarDiligencia.NUC = diligenciaActual.NUC;
                        insertarDiligencia.Textofinal = diligenciaActual.Textofinal;
                        insertarDiligencia.NumeroOficio = diligenciaActual.NumeroOficio;
                        insertarDiligencia.NodeSolicitud = diligenciaActual.NodeSolicitud;
                        insertarDiligencia.NumeroDistrito = diligenciaActual.NumeroDistrito;
                        insertarDiligencia.Lat = diligenciaActual.Lat;
                        insertarDiligencia.Lng = diligenciaActual.Lng;
                        insertarDiligencia.Dirigido = diligenciaActual.Dirigido;
                        insertarDiligencia.RecibidoF = diligenciaActual.RecibidoF;
                        insertarDiligencia.FechaRecibidoF = diligenciaActual.FechaRecibidoF;
                        insertarDiligencia.Respuesta = diligenciaActual.Respuesta;
                        insertarDiligencia.EtapaInicial = diligenciaActual.EtapaInicial;
                        insertarDiligencia.DSPDEstino = diligenciaActual.DSPDEstino;
                        insertarDiligencia.DistritoId = diligenciaActual.DistritoId;

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