using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using Microsoft.AspNetCore.Authorization;
using SIIGPP.Entidades.M_PI.PeritosAsignadosPI;
using SIIGPP.PI.Models.PeritoAsignadoPI;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeritoAsignadoPIController :ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public PeritoAsignadoPIController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/PeritoAsignadoPI/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PeritoAsignadoPIViewModel>> Listar()
        {
            var pa = await _context.PeritoAsignadoPIs
                .ToListAsync();

            return pa.Select(a => new PeritoAsignadoPIViewModel
            {
                IdPeritoAsignadoPI = a.IdPeritoAsignadoPI,
                ModuloServicioId = a.ModuloServicioId,
                RActosInvestigacionId = a.RActoInvestigacionId,
                Respuesta = a.Respuesta,
                NumeroInterno = a.NumeroInterno,
                Conclusion = a.Conclusion,
                FechaRecibido = a.FechaRecibido,
                FechaAceptado = a.FechaAceptado,
                FechaFinalizado = a.FechaFinalizado,
                FechaEntregado = a.FechaEntregado,
                uDistrito = a.uDistrito,
                uSubproc = a.uSubproc,
                uAgencia = a.uAgencia,
                uUsuario = a.uUsuario,
                uPuesto = a.uPuesto,
                uModulo = a.uModulo,
                Fechasysregistro = a.Fechasysregistro,
                Fechasysfinalizado = a.Fechasysfinalizado,
                UltmimoStatus = a.UltmimoStatus
            });

        }


        // PUT: api/PeritoAsignadoPI/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var pa = await _context.PeritoAsignadoPIs.FirstOrDefaultAsync(a => a.IdPeritoAsignadoPI == model.IdPeritoAsignadoPI);

            if (pa == null)
            {
                return NotFound();
            }

            pa.Respuesta = model.Respuesta;
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



        // POST: api/PeritoAsignadoPI/Crear
        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            PeritoAsignadoPI mdp = new PeritoAsignadoPI
            {

                ModuloServicioId = model.ModuloServicioId,
                RActoInvestigacionId = model.RActoInvestigacionId,
                Respuesta = model.Respuesta,
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
                uPuesto = model.uPuesto,
                uModulo = model.uModulo,
                Fechasysregistro = fecha,
                Fechasysfinalizado = model.Fechasysfinalizado,
                UltmimoStatus = fecha
            };

            _context.PeritoAsignadoPIs.Add(mdp);
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


        // GET: api/PeritoAsignadoPI/Listarporstatus
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PeritoAsignadoPIViewModel>> Listarporstatus()
        {
            var pa = await _context.PeritoAsignadoPIs
                .Where(a => a.RActoInvestigacion.Respuestas != "Finalizado")
                .Include(a => a.RActoInvestigacion)
                .Include(a => a.ModuloServicio)
                .OrderByDescending(a => a.Fechasysregistro)
                .ToListAsync();

            return pa.Select(a => new PeritoAsignadoPIViewModel
            {
                IdPeritoAsignadoPI = a.IdPeritoAsignadoPI,
                ModuloServicioId = a.ModuloServicioId,
                RActosInvestigacionId = a.RActoInvestigacionId,
                Respuesta = a.Respuesta,
                NumeroInterno = a.NumeroInterno,
                Conclusion = a.Conclusion,
                FechaRecibido = a.FechaRecibido,
                FechaAceptado = a.FechaAceptado,
                FechaFinalizado = a.FechaFinalizado,
                FechaEntregado = a.FechaEntregado,
                uDistrito = a.uDistrito,
                uSubproc = a.uSubproc,
                uAgencia = a.uAgencia,
                uUsuario = a.uUsuario,
                uPuesto = a.uPuesto,
                uModulo = a.uModulo,
                Fechasysregistro = a.Fechasysregistro,
                Fechasysfinalizado = a.Fechasysfinalizado,
                UltmimoStatus = a.UltmimoStatus,
                StatusAC = a.RActoInvestigacion.Status,
                Modulo = a.ModuloServicio.Nombre


            });

        }

        // PUT: api/PeritoAsignadoPI/Actualizarmodulo
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        public async Task<IActionResult> Actualizarmodulo([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var pa = await _context.PeritoAsignadoPIs.FirstOrDefaultAsync(a => a.IdPeritoAsignadoPI == model.IdPeritoAsignadoPI);

            if (pa == null)
            {
                return NotFound();
            }


            pa.ModuloServicioId = model.ModuloServicioId;
            pa.Motivo = model.Motivo;
            pa.FechaCambio = System.DateTime.Now;


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


        // GET: api/PeritoAsignadoPI/ListarporModulo
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{idmoduloservicio}")]
        public async Task<IEnumerable<PeritoAsignadoPIFullViewModel>> ListarporModulo([FromRoute] Guid idmoduloservicio)
        {
            var pa = await _context.PeritoAsignadoPIs
                .Include(a => a.RActoInvestigacion)
                .Include(a => a.ModuloServicio)
                .Where(a => a.ModuloServicioId == idmoduloservicio)
                .OrderByDescending(a => a.Fechasysregistro)
                .ToListAsync();

            return pa.Select(a => new PeritoAsignadoPIFullViewModel
            {
                IdPeritoAsignadoPI = a.IdPeritoAsignadoPI,
                ModuloServicioId = a.ModuloServicioId,
                RActosInvestigacionId = a.RActoInvestigacionId,
                Respuesta = a.Respuesta,
                NumeroInterno = a.NumeroInterno,
                Conclusion = a.Conclusion,
                FechaRecibido = a.FechaRecibido,
                FechaAceptado = a.FechaAceptado,
                FechaFinalizado = a.FechaFinalizado,
                FechaEntregado = a.FechaEntregado,
                uDistrito = a.uDistrito,
                uSubproc = a.uSubproc,
                uAgencia = a.uAgencia,
                uUsuario = a.uUsuario,
                uPuesto = a.uPuesto,
                uModulo = a.uModulo,
                Fechasysregistro = a.Fechasysregistro,
                Fechasysfinalizado = a.Fechasysfinalizado,
                UltmimoStatus = a.UltmimoStatus,
                StatusAC = a.RActoInvestigacion.Status,
                Modulo = a.ModuloServicio.Nombre,

                DIdRActosInvestigacion = a.RActoInvestigacion.IdRActosInvestigacion,
                DRHechoId = a.RActoInvestigacion.RHechoId,
                DFechaSolicitud = a.RActoInvestigacion.FechaSolicitud,
                DStatus = a.RActoInvestigacion.Status,
                DServicios = a.RActoInvestigacion.Servicios,
                DEspecificaciones = a.RActoInvestigacion.Especificaciones,
                DCdetenido = a.RActoInvestigacion.Cdetenido,
                DRespuestas = a.RActoInvestigacion.Respuestas,
                DNUC = a.RActoInvestigacion.NUC,
                DTextofinal = a.RActoInvestigacion.Textofinal,
                DFechaSys = a.RActoInvestigacion.FechaSys,
                DUDirSubPro = a.RActoInvestigacion.UDirSubPro,
                DUUsuario = a.RActoInvestigacion.UUsuario,
                DUPuesto = a.RActoInvestigacion.UPuesto,
                DUModulo = a.RActoInvestigacion.UModulo,
                DUAgencia = a.RActoInvestigacion.UAgencia,
                NumerOficio = a.RActoInvestigacion.NumeroOficio
            });

        }


        // PUT: api/PeritoAsignadoPI/Actualizarultimostatus
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        public async Task<IActionResult> Actualizarultimostatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

  

            var pa = await _context.PeritoAsignadoPIs.FirstOrDefaultAsync(a => a.IdPeritoAsignadoPI == model.IdPeritoAsignadoPI);

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


        // PUT: api/PeritoAsignadoPI/ActualizarConclu
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        public async Task<IActionResult> ActualizarConclu([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pa = await _context.PeritoAsignadoPIs.FirstOrDefaultAsync(a => a.IdPeritoAsignadoPI == model.IdPeritoAsignadoPI);

            if (pa == null)
            {
                return NotFound();
            }
            DateTime fecha = System.DateTime.Now;


            pa.Conclusion = model.Conclusion;
            pa.Respuesta = model.Respuesta;
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


        // PUT: api/PeritoAsignadoPI/ActualizarFechaEntregado
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        public async Task<IActionResult> ActualizarFechaEntregado([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var pa = await _context.PeritoAsignadoPIs.FirstOrDefaultAsync(a => a.IdPeritoAsignadoPI == model.IdPeritoAsignadoPI);

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

        // GET: api/PeritoAsignadoPI/Listarporidfecha/idpersona/fechai/fechaf
        [HttpGet("[action]/{fechai}/{fechaf}")]
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        public async Task<IEnumerable<PeritoAsignadoPIFullViewModel>> Listarporidfecha([FromRoute] DateTime fechai, DateTime fechaf)
        {
            var pa = await _context.PeritoAsignadoPIs
                .Where(a => a.UltmimoStatus >= fechai)
                .Where(a => a.UltmimoStatus <= fechaf)
                .Include(a => a.RActoInvestigacion)
                .Include(a => a.ModuloServicio)
                .ToListAsync();

            return pa.Select(a => new PeritoAsignadoPIFullViewModel
            {
                IdPeritoAsignadoPI = a.IdPeritoAsignadoPI,
                ModuloServicioId = a.ModuloServicioId,
                RActosInvestigacionId = a.RActoInvestigacionId,
                Respuesta = a.Respuesta,
                NumeroInterno = a.NumeroInterno,
                Conclusion = a.Conclusion,
                FechaRecibido = a.FechaRecibido,
                FechaAceptado = a.FechaAceptado,
                FechaFinalizado = a.FechaFinalizado,
                FechaEntregado = a.FechaEntregado,
                uDistrito = a.uDistrito,
                uSubproc = a.uSubproc,
                uAgencia = a.uAgencia,
                uUsuario = a.uUsuario,
                uPuesto = a.uPuesto,
                uModulo = a.uModulo,
                Fechasysregistro = a.Fechasysregistro,
                Fechasysfinalizado = a.Fechasysfinalizado,
                UltmimoStatus = a.UltmimoStatus,
                StatusAC = a.RActoInvestigacion.Status,
                Modulo = a.ModuloServicio.Nombre,

                DIdRActosInvestigacion = a.RActoInvestigacion.IdRActosInvestigacion,
                DRHechoId = a.RActoInvestigacion.RHechoId,
                DFechaSolicitud = a.RActoInvestigacion.FechaSolicitud,
                DStatus = a.RActoInvestigacion.Status,
                DServicios = a.RActoInvestigacion.Servicios,
                DEspecificaciones = a.RActoInvestigacion.Especificaciones,
                DCdetenido = a.RActoInvestigacion.Cdetenido,
                DRespuestas = a.RActoInvestigacion.Respuestas,
                DNUC = a.RActoInvestigacion.NUC,
                DTextofinal = a.RActoInvestigacion.Textofinal,
                DFechaSys = a.RActoInvestigacion.FechaSys,
                DUDirSubPro = a.RActoInvestigacion.UDirSubPro,
                DUUsuario = a.RActoInvestigacion.UUsuario,
                DUPuesto = a.RActoInvestigacion.UPuesto,
                DUModulo = a.RActoInvestigacion.UModulo,
                DUAgencia = a.RActoInvestigacion.UAgencia

            });

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
        
            

        //GET: api/PeritoAsignadoPI/ContadorRegistrados
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{ano}")]
        public async Task<IEnumerable<CC>> ContadorRegistrados([FromRoute] int ano)
        {
            var Tabla = await _context.PeritoAsignadoPIs
                                      
                                      .GroupBy(v => v.ModuloServicio.Nombre)
                                      .Select(x => new { etiqueta = x.Key
                                      ,valor1 = x.Count(v => v.RActoInvestigacion.Status == "Finalizado")
                                      ,valor2 = x.Count(v => v.RActoInvestigacion.Status == "Asignado")
                                      ,valor3 = x.Count(v => v.RActoInvestigacion.Status == "Enproceso")
                                      ,valor4 = x.Count(v => v.RActoInvestigacion.Status == "Suspendido")
                                      ,valor5 = x.Count(v => v.RActoInvestigacion.Status == "Entregado")})
                                      .ToListAsync();

            return Tabla.Select(v => new CC
            {
                mes = v.etiqueta.ToString(),
                finalizado= v.valor1,
                asignado = v.valor2,
                enproceso = v.valor3,
                suspendido = v.valor4,
                entregado = v.valor5,
                total = v.valor1 + v.valor2 + v.valor3 + v.valor4+ v.valor5,
            }
            
            );
        }
    }
}