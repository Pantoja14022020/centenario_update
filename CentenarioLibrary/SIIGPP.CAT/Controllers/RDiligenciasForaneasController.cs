using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.DiligenciasForaneas;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Diligencias;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RDiligenciasForaneasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;


        public RDiligenciasForaneasController(DbContextSIIGPP context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/RDiligenciasForaneas/Listar
        [Authorize(Roles = "Administrador,Director,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IEnumerable<DiligenciasVieModel>> Listar([FromRoute] Guid rHechoId)
        {
            var dil = await _context.RDiligenciasForaneas
                            .OrderByDescending(a => a.FechaSys)
                            .Where(a => a.rHechoId == rHechoId)
                            .ToListAsync();



            return dil.Select(a => new DiligenciasVieModel
            {
                /*********************************************/

                IdRDiligenciasForaneas = a.IdRDiligenciasForaneas,
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
                NUC = a.NUC,
                Textofinal = a.Textofinal,
                NumeroOficio = a.NumeroOficio,
                NodeSolicitud = a.NodeSolicitud,
                NumeroDistrito = a.NumeroDistrito,
                NodeSolicitudf = a.NodeSolicitud,
                FechaSys = a.FechaSys,
                Lat = a.Lat,
                Lng = a.Lng,
                Dirigido = a.Dirigido,
                RecibidoF = a.RecibidoF,
                FechaRecibidoF = a.FechaRecibidoF,
                Respuesta = a.Respuesta,
                AgenciaEnvia = a.AgenciaEnvia,
                AgenciaRecibe = a.AgenciaRecibe,
                EtapaInicial = a.EtapaInicial,
                EnvioExitosoTF = a.EnvioExitosoTF,
                


            });

        }
        [Authorize(Roles = "Coordinador,Administrador,Perito")]
        // GET: api/RDiligencias/Listarporagencia
        [HttpGet("[action]/{nuc}")]

        public async Task<IEnumerable<DiligenciasVieModel>> FiltrarStatus([FromRoute] string nuc)
        {
            var fil = await _context.RDiligenciasForaneas
                            .Where(a => a.NUC == nuc)
                            .Where(a => a.StatusRespuesta == "Finalizado")
                            .ToListAsync();
            return fil.Select(a => new DiligenciasVieModel

            {
                IdRDiligenciasForaneas = a.IdRDiligenciasForaneas,
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

            });

        }


        // GET: api/RDiligenciasForaneas/ObtenernumeroMaximoporDistrito
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{Ndistrito}/{idDistrito}")]
        public async Task<IActionResult> ObtenernumeroMaximoporDistrito([FromRoute] String Ndistrito,[FromRoute] Guid idDistrito)
        {

            try
            {

                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + idDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var da = await ctx.RDiligenciasForaneas
                    .OrderByDescending(x => Int32.Parse(x.NodeSolicitud))
                    .Where(x => x.NumeroDistrito == Ndistrito).Where(x => x.NodeSolicitud != null)
                    .FirstOrDefaultAsync();
                    if (da == null)
                    {
                        return Ok(new { NumeroMaximo = 0 });
                    }

                    return Ok(new DatosExtrasViewModel
                    {
                        NumeroMaximo = da.NodeSolicitud
                    });
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

        }

        // POST: api/RDiligenciasForaneas/CrearPI
        //[Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearPI(CrearViewModel model, Guid iddiligenciaforanea)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var EI = await _context.HistorialCarpetas
            .Where(a => a.RHechoId == model.rHechoId)
            .Where(a => a.Detalle == "VINCULACION APROCESO")
            .OrderByDescending(a => a.Fechasys)
            .FirstOrDefaultAsync();

            Boolean eic = false;

            //Boolean enviada = false;

            if (EI != null) eic = true;
            else eic = false;

            DateTime fecha = System.DateTime.Now;

            var noSolicitud = await _context.RDiligenciasForaneas
            .Where(a => a.AgenciaRecibe == model.AgenciaRecibe)
            .ToListAsync();

            int noSol = noSolicitud.Count() + 1;


            RDiligenciasForaneas InsertarRD = new RDiligenciasForaneas
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
                NUC = model.NUC,
                Textofinal = model.Textofinal,
                NumeroOficio = model.NumeroOficio,
                NodeSolicitud = (model.NumeroDistritoOrigen + "-" + model.NumeroDistrito + "/"+ noSol),
                NumeroDistrito = model.NumeroDistrito,
                Lat = model.Lat,
                Lng = model.Lng,
                Dirigido = model.Dirigido,
                RecibidoF = false,
                FechaRecibidoF = fecha,
                AgenciaEnvia = model.AgenciaEnvia,
                AgenciaRecibe = model.AgenciaRecibe,
                EtapaInicial = eic,
                EnvioExitosoTF = true,
                    

            };

            _context.RDiligenciasForaneas.Add(InsertarRD);
            // AGREGAR EN LA BD DEL DISTRITO ORIGINAL
            await _context.SaveChangesAsync();

            iddiligenciaforanea = InsertarRD.IdRDiligenciasForaneas;
            try
            {
                //AGREGAR EN LA BD DEL DISTRITO DESTINO
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.AgenciaRecibe.ToString().ToUpper())).Options;
                using (var ctxDestino = new DbContextSIIGPP(options))
                {
                    RDiligenciasForaneas InsertarRDForaneo = InsertarRD;
                    ctxDestino.RDiligenciasForaneas.Add(InsertarRDForaneo);
                    await ctxDestino.SaveChangesAsync();
                }

                return Ok(new { iddili = iddiligenciaforanea, enviada = true});
                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {

                var rdili = await _context.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == iddiligenciaforanea);

                if (rdili == null)
                {
                    return NotFound();
                }

                rdili.EnvioExitosoTF = false;

                await _context.SaveChangesAsync();

                return Ok(new { iddili = iddiligenciaforanea,  enviada = false });
            }

        }

        // POST: api/RDiligenciasForaneas/ReenviarSolicitud
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ReenviarSolicitud (DiligenciasVieModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reenvio = await _context.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == model.IdRDiligenciasForaneas);

            if (reenvio == null)
            {
                return NotFound();
            }

            reenvio.EnvioExitosoTF = true;

            await _context.SaveChangesAsync();

            try
            {
                //AGREGAR EN LA BD DEL DISTRITO DESTINO
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.AgenciaRecibe.ToString().ToUpper())).Options;
                using (var ctxDestino = new DbContextSIIGPP(options))
                {
                    var DiligenciaClonar = await _context.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == model.IdRDiligenciasForaneas);

                    RDiligenciasForaneas InsertarRDForaneo = DiligenciaClonar;
                    ctxDestino.RDiligenciasForaneas.Add(InsertarRDForaneo);
                    await ctxDestino.SaveChangesAsync();
                }

                return Ok(new {enviada = true });
                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {

                var rdili = await _context.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == model.IdRDiligenciasForaneas);

                if (rdili == null)
                {
                    return NotFound();
                }

                rdili.EnvioExitosoTF = false;

                await _context.SaveChangesAsync();

                return Ok(new { enviada = false });
            }

        }

        [Authorize(Roles = "Coordinador,Administrador")]
        // GET: api/RDiligenciasForaneas/Listarporagencia
        [HttpGet("[action]/{idagencia}/{agenciaclave}")]

        public async Task<IEnumerable<DiligenciasSPViewModel>> Listarporagencia([FromRoute]Guid idagencia, Guid agenciaclave)
        {
            var dil = await _context.RDiligenciasForaneas
                            .Where(a => a.ASP.AgenciaId == idagencia)
                            .Where(a => a.AgenciaRecibe == agenciaclave)
                            .Include(a => a.ASP)
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


            return dil.Select(a => new DiligenciasSPViewModel

            {
                /*********************************************/

                IdRDiligenciasForaneas = a.IdRDiligenciasForaneas,
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
                AgenciaEnvia = a.AgenciaEnvia,
                AgenciaRecibe = a.AgenciaRecibe,
                EtapaInicial = a.EtapaInicial
            });

        }


        // PUT: api/RDiligenciasForaneas/Actualizarstatus

        [HttpPut("[action]")]
        [Authorize(Roles = "Director,Subprocurador,Oficialia de partes,Coordinador,Perito,Administrador")]
        public async Task<IActionResult> Actualizarstatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var rdili = await _context.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == model.IdRDiligenciasForaneas);

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

        // PUT: api/RDiligenciasForaneas/ActualizarstatusForanea

        [HttpPut("[action]")]
        [Authorize(Roles = "Director,Subprocurador,Oficialia de partes,Coordinador,Perito,Administrador")]
        public async Task<IActionResult> ActualizarstatusForanea([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var rdili = await _context.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == model.IdRDiligenciasForaneas);
                if (rdili == null)
                {
                    return NotFound();
                }
                rdili.StatusRespuesta = model.StatusRespuesta;
                rdili.Respuesta = model.Respuesta;
                await _context.SaveChangesAsync();
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.AgenciaEnvia.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    rdili = await ctx.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == model.IdRDiligenciasForaneas);
                    rdili.Respuesta = model.Respuesta;
                    await _context.SaveChangesAsync();
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



        // PUT: api/RDiligenciasForaneas/Actualizarstatus2
        [Authorize(Roles = "Director-Subprocurador,Oficialia de partes,Coordinador,Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizarstatus2([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var rdili = await _context.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == model.IdRDiligenciasForaneas);

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

        // PUT: api/RDiligenciasForaneas/Actualizarstatus2Foraneas
        [Authorize(Roles = "Director-Subprocurador,Oficialia de partes,Coordinador,Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizarstatus2Foraneas([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var rdili = await _context.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == model.IdRDiligenciasForaneas);
                rdili.StatusRespuesta = model.StatusRespuesta;
                rdili.Respuestas = model.Respuestas;
                await _context.SaveChangesAsync();
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.AgenciaEnvia.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var rdilif = await ctx.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == model.IdRDiligenciasForaneas);
                    if (rdilif == null)
                    {
                        return NotFound();
                    }
                    rdilif.StatusRespuesta = model.StatusRespuesta;
                    rdilif.Respuestas = model.Respuestas;
                    await ctx.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }

            return Ok();
        }

        // PUT: api/RDiligenciasForaneas/ActualizarfechaR
        [Authorize(Roles = "Director-Subprocurador,Oficialia de partes,Coordinador,Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarfechaR([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var rdili = await _context.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == model.IdRDiligenciasForaneas);

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

        // PUT: api/RDiligenciasForaneas/ActualizarfechaRForanea
        [Authorize(Roles = "Director-Subprocurador,Oficialia de partes,Coordinador,Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarfechaRForanea([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            { 
            var rdili = await _context.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == model.IdRDiligenciasForaneas);
            rdili.RecibidoF = true;
            rdili.FechaRecibidoF = System.DateTime.Now;
            await _context.SaveChangesAsync();
            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.AgenciaEnvia.ToString().ToUpper())).Options;
            using (var ctx = new DbContextSIIGPP(options))
            {
                var rdilif = await ctx.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == model.IdRDiligenciasForaneas);
                if (rdilif == null)
                {
                    return NotFound();
                }
                rdilif.RecibidoF = true;
                rdilif.FechaRecibidoF = System.DateTime.Now;
                await ctx.SaveChangesAsync();
            }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }
            return Ok();
        }

        // GET: api/RDiligenciasForaneas/ListarporDistrito
        [Authorize(Roles = "Administrador,Director,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{clavedistrito}")]
        public async Task<IEnumerable<DiligenciasVieModel>> ListarporDistrito([FromRoute] Guid clavedistrito)
        {
            var dil = await _context.RDiligenciasForaneas
                            .OrderByDescending(a => a.FechaSys)
                            .Where(a => a.AgenciaRecibe == clavedistrito)
                            .ToListAsync();


            return dil.Select(a => new DiligenciasVieModel
            {
                /*********************************************/

                IdRDiligenciasForaneas = a.IdRDiligenciasForaneas,
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
                AgenciaEnvia = a.AgenciaEnvia,
                AgenciaRecibe = a.AgenciaRecibe,
                EtapaInicial = a.EtapaInicial


            });

        }




    }
}
