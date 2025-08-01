using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.SP.ModelsSP.PeritoAsignado;
using SIIGPP.Datos;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SIIGPP.Entidades.M_SP.PeritosAsignados;
using Microsoft.AspNetCore.Authorization;
using RestSharp;

namespace SIIGPP.SP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeritoAsignadoController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;

        public PeritoAsignadoController(DbContextSIIGPP context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/PeritoAsignado/Listar
        [Authorize(Roles = "Administrador, Oficialia de partes, Coordinador, Director, Subprocurador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PeritoAsigandoViewModel>> Listar()
        {
            var pa = await _context.PeritoAsignados
                .Include(a => a.RDiligencias)
                .Include(a => a.RDiligencias.ASP)
                .Include(a => a.ModuloServicio)
                .ToListAsync();

            return pa.Select(a => new PeritoAsigandoViewModel
            {
                IdPeritoAsignado = a.IdPeritoAsignado,
                ModuloServicioId = a.ModuloServicioId,
                Servicio = a.RDiligencias.Servicio,
                Modulo = a.ModuloServicio.Nombre,
                RDiligenciasId = a.RDiligenciasId,              
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
                Status = a.RDiligencias.StatusRespuesta,
                UltmimoStatus= a.UltmimoStatus,
                NumeroControl = a.NumeroControl,

            });

        }

        // GET: api/PeritoAsignado/Listarporstatus
        [Authorize(Roles = "Administrador, Director ,Coordinador")]
        [HttpGet("[action]/{idagencia}")]
        public async Task<IEnumerable<PeritoAsigandoViewModel>> Listarporstatus([FromRoute]Guid idagencia)
        {
            var pa = await _context.PeritoAsignados
                .Where(a =>  a.RDiligencias.StatusRespuesta != "Finalizado" )
                .Where(a => a.RDiligencias.ASP.AgenciaId == idagencia)
                .Include(a => a.RDiligencias)
                .Include(a => a.ModuloServicio)
                .Include(a => a.RDiligencias.ASP)
                .OrderByDescending(a => a.Fechasysfinalizado)
                .ToListAsync();

            return pa.Select(a => new PeritoAsigandoViewModel
            {
                IdPeritoAsignado = a.IdPeritoAsignado,
                ModuloServicioId = a.ModuloServicioId,
                Servicio = a.RDiligencias.Servicio,
                Modulo = a.ModuloServicio.Nombre,
                RDiligenciasId = a.RDiligenciasId,
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
                Status = a.RDiligencias.StatusRespuesta,
                idagencia = a.RDiligencias.ASP.AgenciaId,
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


            var pa = await _context.PeritoAsignados.FirstOrDefaultAsync(a => a.IdPeritoAsignado == model.IdPeritoAsignado);

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

        // PUT: api/PeritoAsignado/ActualizarConclu
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador, Perito")]
        public async Task<IActionResult> ActualizarConclu([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var pa = await _context.PeritoAsignados.FirstOrDefaultAsync(a => a.IdPeritoAsignado == model.IdPeritoAsignado);

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

        // PUT: api/PeritoAsignado/Actualizarmodulo
        [HttpPut("[action]")]
        [Authorize(Roles = " Administrador, Director ,Coordinador")]
        public async Task<IActionResult> Actualizarmodulo([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var pa = await _context.PeritoAsignados.FirstOrDefaultAsync(a => a.IdPeritoAsignado == model.IdPeritoAsignado);

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

        // PUT: api/PeritoAsignado/Actualizarultimostatus
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador, Perito")]
        public async Task<IActionResult> Actualizarultimostatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var pa = await _context.PeritoAsignados.FirstOrDefaultAsync(a => a.IdPeritoAsignado == model.IdPeritoAsignado);

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

        // PUT: api/PeritoAsignado/ActualizarFechaEntregado
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador, Perito,Oficialia de partes")]
        public async Task<IActionResult> ActualizarFechaEntregado([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var pa = await _context.PeritoAsignados.FirstOrDefaultAsync(a => a.IdPeritoAsignado == model.IdPeritoAsignado);

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


        // POST: api/PeritoAsignado/Crear
        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador, Director ,Oficialia de partes,Coordinador")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            PeritoAsignado mdp = new PeritoAsignado
            {
            
                RDiligenciasId = model.RDiligenciasId,           
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

            _context.PeritoAsignados.Add(mdp);
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
        public async Task<IEnumerable<PeritoAsigandoViewModel>> Listarporid([FromRoute] Guid idmoduloservicio)
        {
            var pa = await _context.PeritoAsignados
                .Where( a => a.RDiligencias.ConIndicio == true)
                .Where( a => a.ModuloServicioId == idmoduloservicio)
                .OrderByDescending(a => a.Fechasysregistro)
                .Include(a => a.RDiligencias)
                .ToListAsync();

            return pa.Select(a => new PeritoAsigandoViewModel
            {
                IdPeritoAsignado = a.IdPeritoAsignado,
                RDiligenciasId = a.RDiligenciasId,
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
                Status = a.RDiligencias.StatusRespuesta,
                NUC = a.RDiligencias.NUC,

                DFechaSolicitud = a.RDiligencias.FechaSolicitud,
                DDirigidoa = a.RDiligencias.Dirigidoa,
                DEmitidoPor = a.RDiligencias.EmitidoPor,
                DDirSubPro = a.RDiligencias.DirSubPro,
                DuDirSubPro = a.RDiligencias.uDirSubPro,
                DUPuesto = a.RDiligencias.UPuesto,
                DStatusRespuesta = a.RDiligencias.StatusRespuesta,
                DServicio = a.RDiligencias.Servicio,
                DEspecificaciones = a.RDiligencias.Especificaciones,
                DPrioridad = a.RDiligencias.Prioridad,
                DrHechoId = a.RDiligencias.rHechoId,
                DASPId = a.RDiligencias.ASPId,
                DModulo = a.RDiligencias.Modulo,
                DAgencia = a.RDiligencias.Agencia,
                DRespuestas = a.RDiligencias.Respuestas,
                DConIndicio = a.RDiligencias.ConIndicio,
                DNUC = a.RDiligencias.NUC,
                DTextofinal = a.RDiligencias.Textofinal,
                NumeroControl = a.NumeroControl,


            });

        }
        // GET: api/PeritoAsignado/Listarporrdiligencias
        [Authorize(Roles = " Administrador, Perito, Coordinador")]
        [HttpGet("[action]/{idrdiligencias}")]
        public async Task<IEnumerable<PeritoAsigandoViewModel>> Listarporrdiligencias([FromRoute] Guid idrdiligencias)
        {
            var pa = await _context.PeritoAsignados
                .Where(a => a.RDiligenciasId == idrdiligencias)
                .ToListAsync();

            return pa.Select(a => new PeritoAsigandoViewModel
            {
                IdPeritoAsignado = a.IdPeritoAsignado,
                RDiligenciasId = a.RDiligenciasId,
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

        // GET: api/PeritoAsignado/Listarporid2
        [Authorize(Roles = "Administrador, Perito,Oficialia de partes")]
        [HttpGet("[action]/{idmoduloservicio}")]
        public async Task<IEnumerable<PeritoAsigandoViewModel>> Listarporid2([FromRoute] Guid idmoduloservicio)
        {
            var pa = await _context.PeritoAsignados
                .Include(a => a.RDiligencias)
                .Where(a => a.ModuloServicioId == idmoduloservicio)
                .OrderByDescending(a => a.Fechasysregistro)
                .ToListAsync();

            return pa.Select(a => new PeritoAsigandoViewModel
            {
                IdPeritoAsignado = a.IdPeritoAsignado,
                RDiligenciasId = a.RDiligenciasId,
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
                Status = a.RDiligencias.StatusRespuesta,
                NUC = a.RDiligencias.NUC,

                DFechaSolicitud = a.RDiligencias.FechaSolicitud,
                DDirigidoa = a.RDiligencias.Dirigidoa,
                DEmitidoPor = a.RDiligencias.EmitidoPor,
                DDirSubPro = a.RDiligencias.DirSubPro,
                DuDirSubPro = a.RDiligencias.uDirSubPro,
                DUPuesto = a.RDiligencias.UPuesto,
                DStatusRespuesta = a.RDiligencias.StatusRespuesta,
                DServicio = a.RDiligencias.Servicio,
                DEspecificaciones = a.RDiligencias.Especificaciones,
                DPrioridad = a.RDiligencias.Prioridad,
                DrHechoId = a.RDiligencias.rHechoId,
                DASPId = a.RDiligencias.ASPId,
                DModulo = a.RDiligencias.Modulo,
                DAgencia = a.RDiligencias.Agencia,
                DRespuestas = a.RDiligencias.Respuestas,
                DConIndicio = a.RDiligencias.ConIndicio,
                DNUC = a.RDiligencias.NUC,
                DTextofinal = a.RDiligencias.Textofinal,
                numerooficio = a.RDiligencias.NumeroOficio,
                NumeroControl = a.NumeroControl,



            });

        }


        // GET: api/PeritoAsignado/Listarporidfecha/fechai/fechaf
        // [Authorize(Roles = "Administrador2")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        [Authorize(Roles = "Administrador, Coordinador ,Director")]
        public async Task<IEnumerable<PeritoAsigandoViewModel>> Listarporidfecha([FromRoute]Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var pa = await _context.PeritoAsignados
                .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                .Where(a => a.UltmimoStatus >= fechai)
                .Where(a => a.UltmimoStatus <= fechaf)
                .Include(a => a.RDiligencias)
                .Include(a => a.ModuloServicio)
                .ToListAsync();

            return pa.Select(a => new PeritoAsigandoViewModel
            {
                IdPeritoAsignado = a.IdPeritoAsignado,
                RDiligenciasId = a.RDiligenciasId,
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
                Status = a.RDiligencias.StatusRespuesta,
                NUC = a.RDiligencias.NUC,
                UltmimoStatus = a.UltmimoStatus,

                DFechaSolicitud = a.RDiligencias.FechaSolicitud,
                DDirigidoa = a.RDiligencias.Dirigidoa,
                DEmitidoPor = a.RDiligencias.EmitidoPor,
                DDirSubPro = a.RDiligencias.DirSubPro,
                DuDirSubPro = a.RDiligencias.uDirSubPro,
                DUPuesto = a.RDiligencias.UPuesto,
                DStatusRespuesta = a.RDiligencias.StatusRespuesta,
                DServicio = a.RDiligencias.Servicio,
                DEspecificaciones = a.RDiligencias.Especificaciones,
                DPrioridad = a.RDiligencias.Prioridad,
                DrHechoId = a.RDiligencias.rHechoId,
                DASPId = a.RDiligencias.ASPId,
                DModulo = a.RDiligencias.Modulo,
                DAgencia = a.RDiligencias.Agencia,
                DRespuestas = a.RDiligencias.Respuestas,
                DConIndicio = a.RDiligencias.ConIndicio,
                DNUC = a.RDiligencias.NUC,
                DTextofinal = a.RDiligencias.Textofinal,
                NumeroControl = a.NumeroControl,


            });

        }

        // GET: api/PeritoAsignado/Listarporagencia
        [Authorize(Roles = "Administrador, Oficialia de partes")]
        [HttpGet("[action]/{idagencia}")]
        public async Task<IEnumerable<PeritoAsigandoViewModel>> Listarporagencia([FromRoute] Guid idagencia)
        {
            var pa = await _context.PeritoAsignados
                .Include(a => a.RDiligencias)
                .Include(a => a.ModuloServicio)
                .Where(a => a.RDiligencias.ASP.AgenciaId == idagencia)
                .ToListAsync();

            return pa.Select(a => new PeritoAsigandoViewModel
            {
                IdPeritoAsignado = a.IdPeritoAsignado,
                RDiligenciasId = a.RDiligenciasId,
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
                Status = a.RDiligencias.StatusRespuesta,
                NUC = a.RDiligencias.NUC,

                DFechaSolicitud = a.RDiligencias.FechaSolicitud,
                DDirigidoa = a.RDiligencias.Dirigidoa,
                DEmitidoPor = a.RDiligencias.EmitidoPor,
                DDirSubPro = a.RDiligencias.DirSubPro,
                DuDirSubPro = a.RDiligencias.uDirSubPro,
                DUPuesto = a.RDiligencias.UPuesto,
                DStatusRespuesta = a.RDiligencias.StatusRespuesta,
                DServicio = a.RDiligencias.Servicio,
                DEspecificaciones = a.RDiligencias.Especificaciones,
                DPrioridad = a.RDiligencias.Prioridad,
                DrHechoId = a.RDiligencias.rHechoId,
                DASPId = a.RDiligencias.ASPId,
                DModulo = a.RDiligencias.Modulo,
                DAgencia = a.RDiligencias.Agencia,
                DRespuestas = a.RDiligencias.Respuestas,
                DConIndicio = a.RDiligencias.ConIndicio,
                DNUC = a.RDiligencias.NUC,
                DTextofinal = a.RDiligencias.Textofinal,
                NumeroControl = a.NumeroControl,


            });

        }


        //GET: api/PeritoAsignado/PorModulo/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasModuloViewModel>> PorModulo([FromRoute]Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.PeritoAsignados
                                      .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                                      .Where(a => a.UltmimoStatus >= fechai)
                                      .Where(a => a.UltmimoStatus <= fechaf)
                                      .Include(a => a.ModuloServicio)
                                      .Include(a => a.RDiligencias)
                                      .GroupBy(v => v.ModuloServicio.Nombre)
                                      .Select(x => new {etiqueta = x.Key
                                      ,valor1 = x.Count(v => v.RDiligencias.StatusRespuesta == "Finalizado")
                                      ,valor2 = x.Count(v => v.RDiligencias.StatusRespuesta == "Asignado")
                                      ,valor3 = x.Count(v => v.RDiligencias.StatusRespuesta == "Enproceso")
                                      ,valor4 = x.Count(v => v.RDiligencias.StatusRespuesta == "Suspendido")
                                      ,valor5 = x.Count(v => v.RDiligencias.StatusRespuesta == "Pospuesto")
                                      ,valor6 = x.Count(v => v.RDiligencias.StatusRespuesta == "Entregado")})
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
                Total = v.valor1+ v.valor2 + v.valor3 + v.valor4 + v.valor5 + v.valor6
            }

            );
        }

        //GET: api/PeritoAsignado/PorMes/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasFechaViewModel>> PorMes([FromRoute]Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.PeritoAsignados
                                      .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                                      .Where(a => a.UltmimoStatus >= fechai)
                                      .Where(a => a.UltmimoStatus <= fechaf)
                                      .Include(a => a.ModuloServicio)
                                      .Include(a => a.RDiligencias)
                                      .GroupBy(v => v.UltmimoStatus.Day)
                                      .Select(x => new { etiqueta = x.Key
                                      , valor1 = x.Count(v => v.RDiligencias.StatusRespuesta == "Finalizado")
                                      , valor2 = x.Count(v => v.RDiligencias.StatusRespuesta == "Asignado")
                                      , valor3 = x.Count(v => v.RDiligencias.StatusRespuesta == "Enproceso")
                                      , valor4 = x.Count(v => v.RDiligencias.StatusRespuesta == "Suspendido")
                                      , valor5 = x.Count(v => v.RDiligencias.StatusRespuesta == "Pospuesto")
                                      , valor6 = x.Count(v => v.RDiligencias.StatusRespuesta == "Entregado")})
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

        //GET: api/PeritoAsignado/PorAño/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasFechaViewModel>> PorAño([FromRoute] Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.PeritoAsignados
                                      .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                                      .Where(a => a.UltmimoStatus >= fechai)
                                      .Where(a => a.UltmimoStatus <= fechaf)
                                      .Include(a => a.ModuloServicio)
                                      .Include(a => a.RDiligencias)
                                      .GroupBy(v => v.UltmimoStatus.Month)
                                      .Select(x => new
                                      {
                                          etiqueta = x.Key
                                      ,
                                          valor1 = x.Count(v => v.RDiligencias.StatusRespuesta == "Finalizado")
                                      ,
                                          valor2 = x.Count(v => v.RDiligencias.StatusRespuesta == "Asignado")
                                      ,
                                          valor3 = x.Count(v => v.RDiligencias.StatusRespuesta == "Enproceso")
                                      ,
                                          valor4 = x.Count(v => v.RDiligencias.StatusRespuesta == "Suspendido")
                                      ,
                                          valor5 = x.Count(v => v.RDiligencias.StatusRespuesta == "Pospuesto")
                                      ,
                                          valor6 = x.Count(v => v.RDiligencias.StatusRespuesta == "Entregado")
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
        
        //GET: api/PeritoAsignado/1
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id, string pwdfirma, string hashregistro, string curp, string rutarchivofirma, string rutarchivo)
        {
            var client = new RestClient("https://fea.hidalgo.gob.mx/v2/firmar");
            try
            {
                var request = new RestRequest("v2/firmar", Method.Post);
                request.AddHeader("Authorization", "Basic UEdKSC1ET0NTOipQR0pILURPQ1MgZGV2MjAyMSo=");
                request.AddParameter("password", pwdfirma);
                request.AddParameter("hash", hashregistro);
                request.AddParameter("curp", curp);
                request.AddFile("pfx", rutarchivofirma);

                // Ejecuta la solicitud asincrónica y espera la respuesta
                RestResponse response = await client.ExecuteAsync(request);

                var extension = Path.GetExtension(rutarchivofirma);
                var filePath = Path.Combine(_environment.ContentRootPath, "Carpetas\\" + rutarchivo) + extension;

                // Verifica si el archivo ya existe y lo elimina si es necesario
                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        // Si el archivo existe, lo eliminamos
                        System.IO.File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        // Si ocurre un error al eliminar, logueamos el error
                        Console.WriteLine("Error al eliminar el archivo: " + ex.Message);
                    }
                }

                // Si la respuesta de la API contiene un archivo o algún tipo de contenido que se quiera devolver
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath); // Leer el archivo en bytes
                return File(fileBytes, "application/octet-stream", rutarchivo + extension); // Devuelve el archivo al cliente

                // Alternativamente, si lo que quieres es solo el contenido de la respuesta
                // return Ok(response.Content); 
            }
            catch (Exception ex)
            {
                // Si ocurre algún error, retornamos una respuesta de error
                return BadRequest("Error en los parámetros." + ex.Message);
            }
        }



    }
}
