using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.InformacionJuridico.BusquedaDomicilios;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusquedaDomicilioController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public BusquedaDomicilioController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/BusquedaDomicilio/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{idmodulo}")]
        public async Task<IEnumerable<BusquedaDomicilioViewModel>> Listar([FromRoute] Guid idmodulo)
        {
            var Bus = await _context.BusquedaDomicilios
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.ModuloServicioId == idmodulo)
                .ToListAsync();

            return Bus.Select(a => new BusquedaDomicilioViewModel
            {
                 IdBusquedaDomicilio = a.IdBusquedaDomicilio,
                 ModuloServicioId  = a.ModuloServicioId,
                 Recepcion = a.Recepcion,
                 Oficio = a.Oficio,
                 CausaPenal = a.CausaPenal,
                 Juzgado = a.Juzgado,
                 Concepto = a.Concepto,
                 Status = a.Status,
                 Estado = a.Estado,
                 Respuesta = a.Respuesta,
                 FAsignacion = a.FAsignacion,
                 FFinalizacion = a.FFinalizacion,
                 FUltmimoStatus = a.FUltmimoStatus,
                 UDistrito = a.UDistrito,
                 USubproc = a.USubproc,
                 UAgencia = a.UAgencia,
                 Usuario = a.Usuario,
                 UPuesto = a.UPuesto,
                 UModulo = a.UModulo,
                 Fechasys = a.Fechasys,
                 FechasComparescencia = a.FechasComparescencia,
                 FechaCumplimiento = a.FechaCumplimiento,
                 FechaRecepcion = a.FechaRecepcion,

            });

        }

        // POST: api/BusquedaDomicilio/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BusquedaDomicilio Bus = new BusquedaDomicilio
            {
                ModuloServicioId = model.ModuloServicioId,
                Recepcion = model.Recepcion,
                Oficio = model.Oficio,
                CausaPenal = model.CausaPenal,
                Juzgado = model.Juzgado,
                Concepto = model.Concepto,
                Status = model.Status,
                Estado = model.Estado,
                Respuesta = model.Respuesta,
                FAsignacion = model.FAsignacion,
                FFinalizacion = model.FFinalizacion,
                FUltmimoStatus = System.DateTime.Now,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo  = model.UModulo,
                Fechasys = System.DateTime.Now,
                FechasComparescencia = model.FechasComparescencia,
                FechaCumplimiento = model.FechaCumplimiento,
                FechaRecepcion = model.FechaRecepcion,

            };

            _context.BusquedaDomicilios.Add(Bus);

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
        // PUT: api/BusquedaDomicilio/AsiganarUnidad
        [HttpPut("[action]")]
        public async Task<IActionResult> AsiganarUnidad([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.BusquedaDomicilios.FirstOrDefaultAsync(a => a.IdBusquedaDomicilio == model.IdBusquedaDomicilio);

            if (com == null)
            {
                return NotFound();
            }
            com.ModuloServicioId = model.ModuloServicioId;
            com.Status = "Asignado";
            com.FAsignacion = System.DateTime.Now;
            com.FUltmimoStatus = System.DateTime.Now;

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

        // GET: api/BusquedaDomicilio/ListarporModulo
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{moduloid}")]
        public async Task<IEnumerable<BusquedaDomicilioViewModel>> ListarporModulo([FromRoute]Guid moduloid)
        {
            var Com = await _context.BusquedaDomicilios
                .Where(a => a.ModuloServicioId == moduloid)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return Com.Select(a => new BusquedaDomicilioViewModel
            {
                IdBusquedaDomicilio = a.IdBusquedaDomicilio,
                ModuloServicioId = a.ModuloServicioId,
                Recepcion = a.Recepcion,
                Oficio = a.Oficio,
                CausaPenal = a.CausaPenal,
                Juzgado = a.Juzgado,
                Concepto = a.Concepto,
                Status = a.Status,
                Estado = a.Estado,
                Respuesta = a.Respuesta,
                FAsignacion = a.FAsignacion,
                FFinalizacion = a.FFinalizacion,
                FUltmimoStatus = a.FUltmimoStatus,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
            });
        }

        // PUT: api/BusquedaDomicilio/ActualizarStatus
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var com = await _context.BusquedaDomicilios.FirstOrDefaultAsync(a => a.IdBusquedaDomicilio == model.IdBusquedaDomicilio);

            if (com == null)
            {
                return NotFound();
            }
            com.Status = model.Status;
            com.FUltmimoStatus = System.DateTime.Now;

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


        // PUT: api/BusquedaDomicilio/ActualizarStatusFinalizado
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatusFinalizado([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var com = await _context.BusquedaDomicilios.FirstOrDefaultAsync(a => a.IdBusquedaDomicilio == model.IdBusquedaDomicilio);

            if (com == null)
            {
                return NotFound();
            }
            com.Status = model.Status;
            com.FFinalizacion = System.DateTime.Now;
            com.FUltmimoStatus = System.DateTime.Now;

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

    }
}
