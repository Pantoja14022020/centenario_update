using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.InformacionJuridico.Exhortos;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExhortoController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public ExhortoController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Exhorto/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{idmodulo}")]
        public async Task<IEnumerable<ExhortoViewModel>> Listar([FromRoute] Guid idmodulo)
        {
            var Ex = await _context.Exhortos
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.ModuloServicioId == idmodulo)
                .ToListAsync();

            return Ex.Select(a => new ExhortoViewModel
            {
                IdExhorto = a.IdExhorto,
                ModuloServicioId = a.ModuloServicioId,
                Recepcion = a.Recepcion,
                Oficio = a.Oficio,
                Juzgado = a.Juzgado,
                PerAPresentar = a.PerAPresentar,
                Delito = a.Delito,
                Agraviado = a.Agraviado,
                CausaPenal = a.CausaPenal,
                Asignado = a.Asignado,
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
                ActosRealizar = a.ActosRealizar,
                FechasComparescencia = a.FechasComparescencia,
                FechaCumplimiento = a.FechaCumplimiento,
                FechaRecepcion = a.FechaRecepcion,

            });

        }

        // POST: api/Exhorto/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Exhorto Ex = new Exhorto
            {
                
                ModuloServicioId = model.ModuloServicioId,
                Recepcion = model.Recepcion,
                Oficio = model.Oficio,
                Juzgado = model.Juzgado,
                PerAPresentar = model.PerAPresentar,
                Delito = model.Delito,
                Agraviado = model.Agraviado,
                CausaPenal = model.CausaPenal,
                Asignado = model.Asignado,
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
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
                ActosRealizar = model.ActosRealizar,
                FechasComparescencia = model.FechasComparescencia,
                FechaCumplimiento = model.FechaCumplimiento,
                FechaRecepcion = model.FechaRecepcion,
            };

            _context.Exhortos.Add(Ex);

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
        // PUT: api/Exhorto/AsiganarUnidad
        [HttpPut("[action]")]
        public async Task<IActionResult> AsiganarUnidad([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.Exhortos.FirstOrDefaultAsync(a => a.IdExhorto == model.IdExhorto);

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

        // GET: api/Exhorto/ListarporModulo
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{moduloid}")]
        public async Task<IEnumerable<ExhortoViewModel>> ListarporModulo([FromRoute]Guid moduloid)
        {
            var Com = await _context.Exhortos
                .Where(a => a.ModuloServicioId == moduloid)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return Com.Select(a => new ExhortoViewModel
            {
                IdExhorto = a.IdExhorto,
                ModuloServicioId = a.ModuloServicioId,
                Recepcion = a.Recepcion,
                Oficio = a.Oficio,
                Juzgado = a.Juzgado,
                PerAPresentar = a.PerAPresentar,
                Delito = a.Delito,
                Agraviado = a.Agraviado,
                CausaPenal = a.CausaPenal,
                Asignado = a.Asignado,
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

        // PUT: api/Exhorto/ActualizarStatus
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.Exhortos.FirstOrDefaultAsync(a => a.IdExhorto == model.IdExhorto);

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


        // PUT: api/Exhorto/ActualizarStatusFinalizado
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatusFinalizado([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.Exhortos.FirstOrDefaultAsync(a => a.IdExhorto == model.IdExhorto);

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
