using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using Microsoft.AspNetCore.Authorization;
using SIIGPP.Entidades.M_PI.SolicitudesInteligencia;
using SIIGPP.PI.Models.SolicitudesInteligencia;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesInteligenciaController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public SolicitudesInteligenciaController(DbContextSIIGPP context)
        {
            _context = context;
        }



        // POST: api/SolicitudesInteligencia/Crear
        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        public async Task<IActionResult> Crear([FromBody] CrearSolicitudViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            SolicitudInteligencia soli = new SolicitudInteligencia
            {

                PeritoAsignadoPIId = model.PeritoAsignadoPIId,
                Mensaje = model.Mensaje,
                Respuesta = model.Respuesta,
                StatusAutorizacion = model.StatusAutorizacion,
                StatusMensaje = model.StatusMensaje,
                Fecha = model.Fecha,
                FechaSys = fecha,
                uDistrito = model.uDistrito,
                uSubproc = model.uSubproc,
                uAgencia = model.uAgencia,
                uUsuario = model.uUsuario,
                uPuesto = model.uPuesto,
                uModulo = model.uModulo
            };

            _context.SolicitudInteligencias.Add(soli);
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

        // GET: api/SolicitudesInteligencia/ListarTodos
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SolicitudInteligenciaViewModel>> ListarTodos()
        {
            var soli = await _context.SolicitudInteligencias
                .OrderByDescending(a => a.FechaSys)
                .ToListAsync();

            return soli.Select(a => new SolicitudInteligenciaViewModel
            {

                IdSolicitudInteligencia = a.IdSolicitudInteligencia,
                PeritoAsignadoPIId = a.PeritoAsignadoPIId,
                Mensaje = a.Mensaje,
                Respuesta = a.Respuesta,
                StatusAutorizacion = a.StatusAutorizacion,
                StatusMensaje = a.StatusMensaje,
                Fecha = a.Fecha,
                FechaSys = a.FechaSys,
                uDistrito = a.uDistrito,
                uSubproc = a.uSubproc,
                uAgencia = a.uAgencia,
                uUsuario = a.uUsuario,
                uPuesto = a.uPuesto,
                uModulo = a.uModulo
            });

        }

        // GET: api/SolicitudesInteligencia/ValidarExistencia
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{idperitoasignado}")]
        public async Task<IActionResult> ValidarExistencia([FromRoute] Guid idperitoasignado)
        {
            var soli = await _context.SolicitudInteligencias
                .Where(a => a.PeritoAsignadoPIId == idperitoasignado)
                .FirstOrDefaultAsync();

            if (soli == null)
            {
                return Ok(new { Statusfound  = false});

            }

            return Ok(new SolicitudInteligenciaViewModel
            {
                IdSolicitudInteligencia = soli.IdSolicitudInteligencia,
                PeritoAsignadoPIId = soli.PeritoAsignadoPIId,
                Mensaje = soli.Mensaje,
                Respuesta = soli.Respuesta,
                StatusAutorizacion = soli.StatusAutorizacion,
                StatusMensaje = soli.StatusMensaje,
                Fecha = soli.Fecha,
                FechaSys = soli.FechaSys,
                uDistrito = soli.uDistrito,
                uSubproc = soli.uSubproc,
                uAgencia = soli.uAgencia,
                uUsuario = soli.uUsuario,
                uPuesto = soli.uPuesto,
                uModulo = soli.uModulo,
                Statusfound = true
            });

        }

        // POST: api/SolicitudesInteligencia/CrearAsignacion
        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        public async Task<IActionResult> CrearAsignacion([FromBody] CrearSolicitudAsignacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SolicitudInteligenciaAsig soli = new SolicitudInteligenciaAsig
            {
                    SolicitudInteligenciaId = model.SolicitudInteligenciaId,
                    ModuloServicioId = model.ModuloServicioId
            };

            _context.SolicitudInteligenciaAsigs.Add(soli);
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


        // PUT: api/SolicitudesInteligencia/Actualizar
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarSolicitiudViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var si = await _context.SolicitudInteligencias.FirstOrDefaultAsync(a => a.IdSolicitudInteligencia == model.IdSolicitudInteligencia);

            if (si == null)
            {
                return NotFound();
            }

                si.StatusAutorizacion = model.StatusAutorizacion;
                si.StatusMensaje = model.StatusMensaje;
                si.Respuesta = model.Respuesta;

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

        // GET: api/SolicitudesInteligencia/ListarAsignacionesAprovadas
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{idmoduloservicio}")]
        public async Task<IEnumerable<AsignacionFullViewModel>> ListarAsignacionesAprovadas([FromRoute] Guid idmoduloservicio)
        {
            var soli = await _context.SolicitudInteligenciaAsigs
                .Where(a => a.SolicitudInteligencia.StatusAutorizacion == true)
                .Where(a => a.ModuloServicioId == idmoduloservicio)
                .Include(a => a.SolicitudInteligencia)
                .OrderByDescending(a => a.SolicitudInteligencia.FechaSys)
                .ToListAsync();

            return soli.Select(a => new AsignacionFullViewModel
            {
                IdSolicitudInteligenciaAsig = a.IdSolicitudInteligenciaAsig,
                SolicitudInteligenciaId = a.SolicitudInteligenciaId,
                ModuloServicioId = a.ModuloServicioId,
                PeritoAsignadoPIId = a.SolicitudInteligencia.PeritoAsignadoPIId,
                Mensaje = a.SolicitudInteligencia.Mensaje, 
                Respuesta = a.SolicitudInteligencia.Respuesta,
                StatusAutorizacion = a.SolicitudInteligencia.StatusAutorizacion,
                StatusMensaje = a.SolicitudInteligencia.StatusMensaje,
                Fecha = a.SolicitudInteligencia.Fecha,
                FechaSys = a.SolicitudInteligencia.FechaSys,
                uDistrito = a.SolicitudInteligencia.uDistrito,
                uSubproc = a.SolicitudInteligencia.uSubproc,
                uAgencia = a.SolicitudInteligencia.uAgencia,
                uUsuario = a.SolicitudInteligencia.uUsuario,
                uPuesto = a.SolicitudInteligencia.uPuesto,
                uModulo = a.SolicitudInteligencia.uModulo
            });

        }

    }
}
