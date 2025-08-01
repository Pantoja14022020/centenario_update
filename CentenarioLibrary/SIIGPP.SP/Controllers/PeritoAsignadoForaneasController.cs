using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.SP.ModelsSP.PeritoAsignadoForaneas;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_SP.PeritosAsignados;
using Microsoft.AspNetCore.Authorization;

namespace SIIGPP.SP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeritoAsignadoForaneasController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public PeritoAsignadoForaneasController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/PeritoAsignadoForaneas/Listar
        [Authorize(Roles = "Administrador, Oficialia de partes, Coordinador, Director, Subprocurador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PeritoAsignadoForaneaViewModel>> Listar()
        {
            var pa = await _context.PeritosAsignadoForaneas
                .Include(a => a.RDiligenciasForaneas)
                .Include(a => a.RDiligenciasForaneas.ASP)
                .Include(a => a.ModuloServicio)
                .ToListAsync();

            return pa.Select(a => new PeritoAsignadoForaneaViewModel
            {
                IdPeritosAsignadoForaneas = a.IdPeritosAsignadoForaneas,
                ModuloServicioId = a.ModuloServicioId,
                Servicio = a.RDiligenciasForaneas.Servicio,
                Modulo = a.ModuloServicio.Nombre,
                RDiligenciasForaneasId = a.RDiligenciasForaneasId,
                NumeroInterno = a.NumeroInterno,
                Conclusion = a.Conclusion,
                FechaRecibido = a.FechaRecibido,
                FechaAceptado = a.FechaAceptado,
                FechaFinalizado = a.FechaFinalizado,
                FechaEntregado = a.FechaEntregado,
                uAgencia = a.uAgencia,
                uDistrito = a.uDistrito,
                uModulo = a.uModulo,
                uPuesto = a.uPuesto,
                uSubproc = a.uSubproc,
                uUsuario = a.uUsuario,
                Fechasysregistro = a.Fechasysregistro,
                Fechasysfinalizado = a.Fechasysfinalizado,
                Status = a.RDiligenciasForaneas.StatusRespuesta,
                UltmimoStatus = a.UltmimoStatus,
                NumeroControl = a.NumeroControl,


            });

        }

        // GET: api/PeritoAsignadoForaneas/Listarporstatus
        [Authorize(Roles = "Administrador, Director ,Coordinador")]
        [HttpGet("[action]/{idagencia}")]
        public async Task<IEnumerable<PeritoAsignadoForaneaViewModel>> Listarporstatus([FromRoute]Guid idagencia)
        {
            var pa = await _context.PeritosAsignadoForaneas
                .Where(a => a.RDiligenciasForaneas.StatusRespuesta != "Finalizado")
                .Where(a => a.RDiligenciasForaneas.ASP.AgenciaId == idagencia)
                .Include(a => a.RDiligenciasForaneas)
                .Include(a => a.ModuloServicio)
                .Include(a => a.RDiligenciasForaneas.ASP)
                .OrderByDescending(a => a.Fechasysfinalizado)
                .ToListAsync();

            return pa.Select(a => new PeritoAsignadoForaneaViewModel
            {
                IdPeritosAsignadoForaneas = a.IdPeritosAsignadoForaneas,
                ModuloServicioId = a.ModuloServicioId,
                Servicio = a.RDiligenciasForaneas.Servicio,
                Modulo = a.ModuloServicio.Nombre,
                RDiligenciasForaneasId = a.RDiligenciasForaneasId,
                NumeroInterno = a.NumeroInterno,
                Conclusion = a.Conclusion,
                FechaRecibido = a.FechaRecibido,
                FechaAceptado = a.FechaAceptado,
                FechaFinalizado = a.FechaFinalizado,
                FechaEntregado = a.FechaEntregado,
                uAgencia = a.uAgencia,
                uDistrito = a.uDistrito,
                uModulo = a.uModulo,
                uPuesto = a.uPuesto,
                uSubproc = a.uSubproc,
                uUsuario = a.uUsuario,
                Fechasysregistro = a.Fechasysregistro,
                Fechasysfinalizado = a.Fechasysfinalizado,
                Status = a.RDiligenciasForaneas.StatusRespuesta,
                idagencia = a.RDiligenciasForaneas.ASP.AgenciaId,
                NumeroControl = a.NumeroControl,

            });

        }


        // PUT: api/PeritoAsignado/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var pa = await _context.PeritoAsignados.FirstOrDefaultAsync(a => a.IdPeritoAsignado == model.IdPeritosAsignadoForaneas);

            if (pa == null)
            {
                return NotFound();
            }

            pa.NumeroInterno = model.NumeroInterno;
            pa.Conclusion = model.Conclusion;
            pa.FechaRecibido = model.FechaRecibido;
            pa.FechaAceptado = model.FechaAceptado;
            pa.FechaFinalizado = model.FechaFinalizado;
            pa.FechaEntregado = model.FechaEntregado;

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

        // PUT: api/PeritoAsignadoForaneas/ActualizarConclu
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador, Perito")]
        public async Task<IActionResult> ActualizarConclu([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var pa = await _context.PeritosAsignadoForaneas.FirstOrDefaultAsync(a => a.IdPeritosAsignadoForaneas == model.IdPeritosAsignadoForaneas);

            if (pa == null)
            {
                return NotFound();
            }
            DateTime fecha = System.DateTime.Now;


            pa.Conclusion = model.Conclusion;
            pa.FechaFinalizado = model.FechaFinalizado;
            pa.Fechasysfinalizado = fecha;
            pa.UltmimoStatus = fecha;

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

        // PUT: api/PeritoAsignadoForaneas/Actualizarmodulo
        [HttpPut("[action]")]
        [Authorize(Roles = " Administrador, Director ,Coordinador")]
        public async Task<IActionResult> Actualizarmodulo([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var pa = await _context.PeritosAsignadoForaneas.FirstOrDefaultAsync(a => a.IdPeritosAsignadoForaneas == model.IdPeritosAsignadoForaneas);

            if (pa == null)
            {
                return NotFound();
            }


            pa.ModuloServicioId = model.ModuloServicioId;


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



        // PUT: api/PeritoAsignadoForaneas/Actualizarultimostatus
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador, Perito")]
        public async Task<IActionResult> Actualizarultimostatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var pa = await _context.PeritosAsignadoForaneas.FirstOrDefaultAsync(a => a.IdPeritosAsignadoForaneas == model.IdPeritosAsignadoForaneas);

            if (pa == null)
            {
                return NotFound();
            }


            pa.UltmimoStatus = System.DateTime.Now;


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

        // PUT: api/PeritoAsignadoForaneas/ActualizarFechaEntregado
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador,Perito,Oficialia de partes")]
        public async Task<IActionResult> ActualizarFechaEntregado([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var pa = await _context.PeritosAsignadoForaneas.FirstOrDefaultAsync(a => a.IdPeritosAsignadoForaneas == model.IdPeritosAsignadoForaneas);

            if (pa == null)
            {
                return NotFound();
            }


            pa.UltmimoStatus = System.DateTime.Now;
            pa.FechaEntregado = model.FechaEntregado;

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



        // POST: api/PeritoAsignadoForaneas/Crear
        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador,Director,Oficialia de partes,Coordinador")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            PeritosAsignadoForaneas mdp = new PeritosAsignadoForaneas
            {

                RDiligenciasForaneasId = model.RDiligenciasForaneasId,
                ModuloServicioId = model.ModuloServicioId,
                NumeroInterno = model.NumeroInterno,
                Conclusion = model.Conclusion,
                FechaRecibido = model.FechaRecibido,
                FechaAceptado = model.FechaAceptado,
                FechaFinalizado = model.FechaFinalizado,
                FechaEntregado = model.FechaEntregado,
                uDistrito = model.uDistrito,
                uSubproc = model.uSubproc,
                uAgencia = model.uAgencia,
                uUsuario = model.uUsuario,
                uModulo = model.uModulo,
                uPuesto = model.uPuesto,
                Fechasysregistro = fecha,
                Fechasysfinalizado = model.Fechasysfinalizado,
                UltmimoStatus = System.DateTime.Now,
                NumeroControl = model.NumeroControl,
            };

            _context.PeritosAsignadoForaneas.Add(mdp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

            return Ok();
        }

        // GET: api/PeritoAsignado/Listarporid
        [Authorize(Roles = " Administrador, Perito")]
        [HttpGet("[action]/{idmoduloservicio}")]
        public async Task<IEnumerable<PeritoAsignadoForaneaViewModel>> Listarporid([FromRoute] Guid idmoduloservicio)
        {
            var pa = await _context.PeritosAsignadoForaneas
                .Where(a => a.ModuloServicioId == idmoduloservicio)
                .OrderByDescending(a => a.Fechasysregistro)
                .Include(a => a.RDiligenciasForaneas)
                .ToListAsync();

            return pa.Select(a => new PeritoAsignadoForaneaViewModel
            {
                IdPeritosAsignadoForaneas = a.IdPeritosAsignadoForaneas,
                RDiligenciasForaneasId = a.RDiligenciasForaneasId,
                NumeroInterno = a.NumeroInterno,
                Conclusion = a.Conclusion,
                FechaRecibido = a.FechaRecibido,
                FechaAceptado = a.FechaAceptado,
                FechaFinalizado = a.FechaFinalizado,
                FechaEntregado = a.FechaEntregado,
                uAgencia = a.uAgencia,
                uDistrito = a.uDistrito,
                uModulo = a.uModulo,
                uPuesto = a.uPuesto,
                uSubproc = a.uSubproc,
                uUsuario = a.uUsuario,
                Fechasysregistro = a.Fechasysregistro,
                Fechasysfinalizado = a.Fechasysfinalizado,
                Status = a.RDiligenciasForaneas.StatusRespuesta,
                NUC = a.RDiligenciasForaneas.NUC,

                DFechaSolicitud = a.RDiligenciasForaneas.FechaSolicitud,
                DDirigidoa = a.RDiligenciasForaneas.Dirigidoa,
                DEmitidoPor = a.RDiligenciasForaneas.EmitidoPor,
                DDirSubPro = a.RDiligenciasForaneas.DirSubPro,
                DuDirSubPro = a.RDiligenciasForaneas.uDirSubPro,
                DUPuesto = a.RDiligenciasForaneas.UPuesto,
                DStatusRespuesta = a.RDiligenciasForaneas.StatusRespuesta,
                DServicio = a.RDiligenciasForaneas.Servicio,
                DEspecificaciones = a.RDiligenciasForaneas.Especificaciones,
                DPrioridad = a.RDiligenciasForaneas.Prioridad,
                DrHechoId = a.RDiligenciasForaneas.rHechoId,
                DASPId = a.RDiligenciasForaneas.ASPId,
                DModulo = a.RDiligenciasForaneas.Modulo,
                DAgencia = a.RDiligenciasForaneas.Agencia,
                DRespuestas = a.RDiligenciasForaneas.Respuestas,
                DNUC = a.RDiligenciasForaneas.NUC,
                DTextofinal = a.RDiligenciasForaneas.Textofinal,
                NumeroControl = a.NumeroControl,


            });

        }

        // GET: api/PeritoAsignadoForaneas/Listarporid2
        [Authorize(Roles = "Administrador, Perito,Oficialia de partes")]
        [HttpGet("[action]/{idmoduloservicio}")]
        public async Task<IEnumerable<PeritoAsignadoForaneaViewModel>> Listarporid2([FromRoute] Guid idmoduloservicio)
        {
            var pa = await _context.PeritosAsignadoForaneas
                .Include(a => a.RDiligenciasForaneas)
                .Where(a => a.ModuloServicioId == idmoduloservicio)
                .OrderByDescending(a => a.Fechasysregistro)
                .ToListAsync();

            return pa.Select(a => new PeritoAsignadoForaneaViewModel
            {
                IdPeritosAsignadoForaneas = a.IdPeritosAsignadoForaneas,
                RDiligenciasForaneasId = a.RDiligenciasForaneasId,
                NumeroInterno = a.NumeroInterno,
                Conclusion = a.Conclusion,
                FechaRecibido = a.FechaRecibido,
                FechaAceptado = a.FechaAceptado,
                FechaFinalizado = a.FechaFinalizado,
                FechaEntregado = a.FechaEntregado,
                uAgencia = a.uAgencia,
                uDistrito = a.uDistrito,
                uModulo = a.uModulo,
                uPuesto = a.uPuesto,
                uSubproc = a.uSubproc,
                uUsuario = a.uUsuario,
                Fechasysregistro = a.Fechasysregistro,
                Fechasysfinalizado = a.Fechasysfinalizado,
                Status = a.RDiligenciasForaneas.StatusRespuesta,
                NUC = a.RDiligenciasForaneas.NUC,

                DFechaSolicitud = a.RDiligenciasForaneas.FechaSolicitud,
                DDirigidoa = a.RDiligenciasForaneas.Dirigidoa,
                DEmitidoPor = a.RDiligenciasForaneas.EmitidoPor,
                DDirSubPro = a.RDiligenciasForaneas.DirSubPro,
                DuDirSubPro = a.RDiligenciasForaneas.uDirSubPro,
                DUPuesto = a.RDiligenciasForaneas.UPuesto,
                DStatusRespuesta = a.RDiligenciasForaneas.StatusRespuesta,
                DServicio = a.RDiligenciasForaneas.Servicio,
                DEspecificaciones = a.RDiligenciasForaneas.Especificaciones,
                DPrioridad = a.RDiligenciasForaneas.Prioridad,
                DrHechoId = a.RDiligenciasForaneas.rHechoId,
                DASPId = a.RDiligenciasForaneas.ASPId,
                DModulo = a.RDiligenciasForaneas.Modulo,
                DAgencia = a.RDiligenciasForaneas.Agencia,
                DRespuestas = a.RDiligenciasForaneas.Respuestas,
                DNUC = a.RDiligenciasForaneas.NUC,
                DTextofinal = a.RDiligenciasForaneas.Textofinal,
                numerooficio = a.RDiligenciasForaneas.NumeroOficio,
                NumeroControl = a.NumeroControl,
                AgenciaEnvia = a.RDiligenciasForaneas.AgenciaEnvia.ToString(),
                AgenciaRecibe = a.RDiligenciasForaneas.AgenciaRecibe.ToString()



            });

        }


        // GET: api/PeritoAsignadoForaneas/Listarporidfecha/fechai/fechaf
        // [Authorize(Roles = "Administrador2")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        [Authorize(Roles = "Administrador, Coordinador ,Director")]
        public async Task<IEnumerable<PeritoAsignadoForaneaViewModel>> Listarporidfecha([FromRoute]Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var pa = await _context.PeritosAsignadoForaneas
                .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                .Where(a => a.UltmimoStatus >= fechai)
                .Where(a => a.UltmimoStatus <= fechaf)
                .Include(a => a.RDiligenciasForaneas)
                .Include(a => a.ModuloServicio)
                .ToListAsync();

            return pa.Select(a => new PeritoAsignadoForaneaViewModel
            {
                IdPeritosAsignadoForaneas = a.IdPeritosAsignadoForaneas,
                RDiligenciasForaneasId = a.RDiligenciasForaneasId,
                NumeroInterno = a.NumeroInterno,
                Conclusion = a.Conclusion,
                Modulo = a.ModuloServicio.Nombre,
                FechaRecibido = a.FechaRecibido,
                FechaAceptado = a.FechaAceptado,
                FechaFinalizado = a.FechaFinalizado,
                FechaEntregado = a.FechaEntregado,
                uAgencia = a.uAgencia,
                uDistrito = a.uDistrito,
                uModulo = a.uModulo,
                uPuesto = a.uPuesto,
                uSubproc = a.uSubproc,
                uUsuario = a.uUsuario,
                Fechasysregistro = a.Fechasysregistro,
                Fechasysfinalizado = a.Fechasysfinalizado,
                Status = a.RDiligenciasForaneas.StatusRespuesta,
                NUC = a.RDiligenciasForaneas.NUC,
                UltmimoStatus = a.UltmimoStatus,

                DFechaSolicitud = a.RDiligenciasForaneas.FechaSolicitud,
                DDirigidoa = a.RDiligenciasForaneas.Dirigidoa,
                DEmitidoPor = a.RDiligenciasForaneas.EmitidoPor,
                DDirSubPro = a.RDiligenciasForaneas.DirSubPro,
                DuDirSubPro = a.RDiligenciasForaneas.uDirSubPro,
                DUPuesto = a.RDiligenciasForaneas.UPuesto,
                DStatusRespuesta = a.RDiligenciasForaneas.StatusRespuesta,
                DServicio = a.RDiligenciasForaneas.Servicio,
                DEspecificaciones = a.RDiligenciasForaneas.Especificaciones,
                DPrioridad = a.RDiligenciasForaneas.Prioridad,
                DrHechoId = a.RDiligenciasForaneas.rHechoId,
                DASPId = a.RDiligenciasForaneas.ASPId,
                DModulo = a.RDiligenciasForaneas.Modulo,
                DAgencia = a.RDiligenciasForaneas.Agencia,
                DRespuestas = a.RDiligenciasForaneas.Respuestas,
                DNUC = a.RDiligenciasForaneas.NUC,
                DTextofinal = a.RDiligenciasForaneas.Textofinal,
                NumeroControl = a.NumeroControl,


            });

        }

        // GET: api/PeritoAsignadoForaneas/Listarporagencia
        [Authorize(Roles = "Administrador, Oficialia de partes")]
        [HttpGet("[action]/{idagencia}")]
        public async Task<IEnumerable<PeritoAsignadoForaneaViewModel>> Listarporagencia([FromRoute] Guid idagencia)
        {
            var pa = await _context.PeritosAsignadoForaneas
                .Include(a => a.RDiligenciasForaneas)
                .Include(a => a.ModuloServicio)
                .Where(a => a.RDiligenciasForaneas.ASP.AgenciaId == idagencia)
                .ToListAsync();

            return pa.Select(a => new PeritoAsignadoForaneaViewModel
            {
                IdPeritosAsignadoForaneas = a.IdPeritosAsignadoForaneas,
                RDiligenciasForaneasId = a.RDiligenciasForaneasId,
                NumeroInterno = a.NumeroInterno,
                Conclusion = a.Conclusion,
                Modulo = a.ModuloServicio.Nombre,
                FechaRecibido = a.FechaRecibido,
                FechaAceptado = a.FechaAceptado,
                FechaFinalizado = a.FechaFinalizado,
                FechaEntregado = a.FechaEntregado,
                uAgencia = a.uAgencia,
                uDistrito = a.uDistrito,
                uModulo = a.uModulo,
                uPuesto = a.uPuesto,
                uSubproc = a.uSubproc,
                uUsuario = a.uUsuario,
                Fechasysregistro = a.Fechasysregistro,
                Fechasysfinalizado = a.Fechasysfinalizado,
                Status = a.RDiligenciasForaneas.StatusRespuesta,
                NUC = a.RDiligenciasForaneas.NUC,

                DFechaSolicitud = a.RDiligenciasForaneas.FechaSolicitud,
                DDirigidoa = a.RDiligenciasForaneas.Dirigidoa,
                DEmitidoPor = a.RDiligenciasForaneas.EmitidoPor,
                DDirSubPro = a.RDiligenciasForaneas.DirSubPro,
                DuDirSubPro = a.RDiligenciasForaneas.uDirSubPro,
                DUPuesto = a.RDiligenciasForaneas.UPuesto,
                DStatusRespuesta = a.RDiligenciasForaneas.StatusRespuesta,
                DServicio = a.RDiligenciasForaneas.Servicio,
                DEspecificaciones = a.RDiligenciasForaneas.Especificaciones,
                DPrioridad = a.RDiligenciasForaneas.Prioridad,
                DrHechoId = a.RDiligenciasForaneas.rHechoId,
                DASPId = a.RDiligenciasForaneas.ASPId,
                DModulo = a.RDiligenciasForaneas.Modulo,
                DAgencia = a.RDiligenciasForaneas.Agencia,
                DRespuestas = a.RDiligenciasForaneas.Respuestas,
                DNUC = a.RDiligenciasForaneas.NUC,
                DTextofinal = a.RDiligenciasForaneas.Textofinal,
                NumeroControl = a.NumeroControl,
                AgenciaEnvia = a.RDiligenciasForaneas.AgenciaEnvia.ToString(),
                AgenciaRecibe = a.RDiligenciasForaneas.AgenciaRecibe.ToString()

            });

        }

        // GET: api/PeritoAsignado/ListarporrdiligenciasForaneas
        [Authorize(Roles = " Administrador, Perito, Coordinador")]
        [HttpGet("[action]/{idrdiligenciasforaneas}")]
        public async Task<IEnumerable<PeritoAsignadoForaneaViewModel>> Listarporrdiligenciasforaneas([FromRoute] Guid idrdiligenciasforaneas)
        {
            var co = await _context.PeritosAsignadoForaneas
                .Where(a => a.RDiligenciasForaneasId == idrdiligenciasforaneas)
                .ToListAsync();

            return co.Select(a => new PeritoAsignadoForaneaViewModel
            {
                IdPeritosAsignadoForaneas = a.IdPeritosAsignadoForaneas,
                RDiligenciasForaneasId = a.IdPeritosAsignadoForaneas,
                NumeroInterno = a.NumeroInterno,
                Conclusion = a.Conclusion,
                FechaRecibido = a.FechaRecibido,
                FechaAceptado = a.FechaAceptado,
                FechaFinalizado = a.FechaFinalizado,
                FechaEntregado = a.FechaEntregado,
                uAgencia = a.uAgencia,
                uDistrito = a.uDistrito,
                uModulo = a.uModulo,
                uPuesto = a.uPuesto,
                uSubproc = a.uSubproc,
                uUsuario = a.uUsuario,
                Fechasysregistro = a.Fechasysregistro,
                Fechasysfinalizado = a.Fechasysfinalizado,



            });

        }


        //GET: api/PeritoAsignadoForaneas/PorModulo/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasModuloViewModel>> PorModulo([FromRoute]Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.PeritosAsignadoForaneas
                                      .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                                      .Where(a => a.UltmimoStatus >= fechai)
                                      .Where(a => a.UltmimoStatus <= fechaf)
                                      .Include(a => a.ModuloServicio)
                                      .Include(a => a.RDiligenciasForaneas)
                                      .GroupBy(v => v.ModuloServicio.Nombre)
                                      .Select(x => new {
                                          etiqueta = x.Key
                                      ,
                                          valor1 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Finalizado")
                                      ,
                                          valor2 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Asignado")
                                      ,
                                          valor3 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Enproceso")
                                      ,
                                          valor4 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Suspendido")
                                      ,
                                          valor5 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Pospuesto")
                                      ,
                                          valor6 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Entregado")
                                      })
                                      .ToListAsync();

            return Tabla.Select(v => new EstadisticasModuloViewModel
            {
                Modulo = v.etiqueta.ToString(),
                Finalizado = v.valor1,
                Asignado = v.valor2,
                Enproceso = v.valor3,
                Suspendido = v.valor4,
                Pospuesto = v.valor5,
                Entregado = v.valor6,
                Total = v.valor1 + v.valor2 + v.valor3 + v.valor4 + v.valor5 + v.valor6
            }

            );
        }

        //GET: api/PeritoAsignadoForaneas/PorMes/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasFechaViewModel>> PorMes([FromRoute]Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.PeritosAsignadoForaneas
                                      .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                                      .Where(a => a.UltmimoStatus >= fechai)
                                      .Where(a => a.UltmimoStatus <= fechaf)
                                      .Include(a => a.ModuloServicio)
                                      .Include(a => a.RDiligenciasForaneas)
                                      .GroupBy(v => v.UltmimoStatus.Day)
                                      .Select(x => new {
                                          etiqueta = x.Key
                                      ,
                                          valor1 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Finalizado")
                                      ,
                                          valor2 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Asignado")
                                      ,
                                          valor3 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Enproceso")
                                      ,
                                          valor4 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Suspendido")
                                      ,
                                          valor5 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Pospuesto")
                                      ,
                                          valor6 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Entregado")
                                      })
                                      .ToListAsync();

            return Tabla.Select(v => new EstadisticasFechaViewModel
            {
                Fecha = v.etiqueta,
                Finalizado = v.valor1,
                Asignado = v.valor2,
                Enproceso = v.valor3,
                Suspendido = v.valor4,
                Pospuesto = v.valor5,
                Entregado = v.valor6,
                Total = v.valor1 + v.valor2 + v.valor3 + v.valor4 + v.valor5 + v.valor6
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

        //GET: api/PeritoAsignadoForaneas/PorAño/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasFechaViewModel>> PorAño([FromRoute]Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.PeritosAsignadoForaneas
                                      .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                                      .Where(a => a.UltmimoStatus >= fechai)
                                      .Where(a => a.UltmimoStatus <= fechaf)
                                      .Include(a => a.ModuloServicio)
                                      .Include(a => a.RDiligenciasForaneas)
                                      .GroupBy(v => v.UltmimoStatus.Month)
                                      .Select(x => new {
                                          etiqueta = x.Key
                                      ,
                                          valor1 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Finalizado")
                                      ,
                                          valor2 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Asignado")
                                      ,
                                          valor3 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Enproceso")
                                      ,
                                          valor4 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Suspendido")
                                      ,
                                          valor5 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Pospuesto")
                                      ,
                                          valor6 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Entregado")
                                      })
                                      .ToListAsync();

            return Tabla.Select(v => new EstadisticasFechaViewModel
            {
                Fechas = mes(v.etiqueta),
                Finalizado = v.valor1,
                Asignado = v.valor2,
                Enproceso = v.valor3,
                Suspendido = v.valor4,
                Pospuesto = v.valor5,
                Entregado = v.valor6,
                Total = v.valor1 + v.valor2 + v.valor3 + v.valor4 + v.valor5 + v.valor6
            }

            );
        }

        //GET: api/PeritoAsignadoForaneas/PorAños/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasFechaViewModel>> PorAños([FromRoute]Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.PeritosAsignadoForaneas
                                      .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                                      .Where(a => a.UltmimoStatus >= fechai)
                                      .Where(a => a.UltmimoStatus <= fechaf)
                                      .Include(a => a.ModuloServicio)
                                      .Include(a => a.RDiligenciasForaneas)
                                      .GroupBy(v => v.UltmimoStatus.Year)
                                      .Select(x => new {
                                          etiqueta = x.Key
                                      ,
                                          valor1 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Finalizado")
                                      ,
                                          valor2 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Asignado")
                                      ,
                                          valor3 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Enproceso")
                                      ,
                                          valor4 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Suspendido")
                                      ,
                                          valor5 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Pospuesto")
                                      ,
                                          valor6 = x.Count(v => v.RDiligenciasForaneas.StatusRespuesta == "Entregado")
                                      })
                                      .ToListAsync();

            return Tabla.Select(v => new EstadisticasFechaViewModel
            {
                Fecha = v.etiqueta,
                Finalizado = v.valor1,
                Asignado = v.valor2,
                Enproceso = v.valor3,
                Suspendido = v.valor4,
                Pospuesto = v.valor5,
                Entregado = v.valor6,
                Total = v.valor1 + v.valor2 + v.valor3 + v.valor4 + v.valor5 + v.valor6
            }

            );
        }
    }
}
