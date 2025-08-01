using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.InformacionJuridico.PresentacionesYC;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using Microsoft.AspNetCore.Hosting;
namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentacionYCController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public PresentacionYCController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/PresentacionYC/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{idmodulo}")]
        public async Task<IEnumerable<PresentacionYCViewModel>> Listar([FromRoute] Guid idmodulo)
        {
            var Pres = await _context.PresentacionesYCs
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.ModuloServicioId == idmodulo)
                .ToListAsync();

            return Pres.Select(a => new PresentacionYCViewModel
            {
                IdPresentacionesYC = a.IdPresentacionesYC,
                ModuloServicioId = a.ModuloServicioId,
                Recepcion = a.Recepcion,
                Oficio = a.Oficio,
                CausaPenal = a.CausaPenal,
                Dependencia = a.Dependencia,
                PerAPresentar = a.PerAPresentar,
                FdePresentacion  = a.FdePresentacion,
                Hora = a.Hora,
                Asignado = a.Asignado,
                CumplidaOInf = a.CumplidaOInf,
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

        // POST: api/PresentacionYC/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PresentacionesYC Pre = new PresentacionesYC
            {
                ModuloServicioId = model.ModuloServicioId,
                Recepcion = model.Recepcion,
                Oficio = model.Oficio,
                CausaPenal = model.CausaPenal,
                Dependencia = model.Dependencia,
                PerAPresentar = model.PerAPresentar,
                FdePresentacion = model.FdePresentacion,
                Hora = model.Hora,
                Asignado = model.Asignado,
                CumplidaOInf = model.CumplidaOInf,
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
                FechasComparescencia = model.FechasComparescencia,
                FechaCumplimiento = model.FechaCumplimiento,
                FechaRecepcion = model.FechaRecepcion,
            };

            _context.PresentacionesYCs.Add(Pre);

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
        // PUT: api/PresentacionYC/AsiganarUnidad
        [HttpPut("[action]")]
        public async Task<IActionResult> AsiganarUnidad([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.PresentacionesYCs.FirstOrDefaultAsync(a => a.IdPresentacionesYC == model.IdPresentacionesYC);

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

        // GET: api/PresentacionYC/ListarporModulo
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{moduloid}")]
        public async Task<IEnumerable<PresentacionYCViewModel>> ListarporModulo([FromRoute]Guid moduloid)
        {
            var Com = await _context.PresentacionesYCs
                .Where(a => a.ModuloServicioId == moduloid)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return Com.Select(a => new PresentacionYCViewModel
            {
                IdPresentacionesYC = a.IdPresentacionesYC,
                ModuloServicioId = a.ModuloServicioId,
                Recepcion = a.Recepcion,
                Oficio = a.Oficio,
                CausaPenal = a.CausaPenal,
                Dependencia = a.Dependencia,
                PerAPresentar = a.PerAPresentar,
                FdePresentacion = a.FdePresentacion,
                Hora = a.Hora,
                Asignado = a.Asignado,
                CumplidaOInf = a.CumplidaOInf,
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

        // PUT: api/PresentacionYC/ActualizarStatus
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.PresentacionesYCs.FirstOrDefaultAsync(a => a.IdPresentacionesYC == model.IdPresentacionesYC);

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


        // PUT: api/PresentacionYC/ActualizarStatusFinalizado
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatusFinalizado([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var com = await _context.PresentacionesYCs.FirstOrDefaultAsync(a => a.IdPresentacionesYC == model.IdPresentacionesYC);

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
