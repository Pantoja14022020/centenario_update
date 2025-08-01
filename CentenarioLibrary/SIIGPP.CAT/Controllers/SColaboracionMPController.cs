using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.SColaboracionesMP;
using SIIGPP.CAT.Models.SColaboracionesMP;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SColaboracionMPController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public SColaboracionMPController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/SColaboracionMP/Listar
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<SColaboracionMPViewModel>> Listar([FromRoute] Guid RHechoId)
        {
            var hi = await _context.SColaboracionMPs
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return hi.Select(a => new SColaboracionMPViewModel
            {
                IdSColaboracionMP = a.IdSColaboracionMP,
                RHechoId = a.RHechoId,
                AgenciaId = a.AgenciaId,
                Texto = a.Texto,
                TipoColaboracion = a.TipoColaboracion,
                NUC = a.NUC,
                UsuarioSolicita = a.UsuarioSolicita,
                AgenciaOrigen = a.AgenciaOrigen,
                AgenciaDestino =a.AgenciaDestino,
                Status =a.Status,
                Respuesta =a.Respuesta,
                FechaRespuesta =a.FechaRespuesta,
                FechaRechazo =a.FechaRechazo,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,

            });

        }


        // POST: api/SColaboracionMP/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SColaboracionMP hi = new SColaboracionMP

            {
                RHechoId = model.RHechoId,
                AgenciaId = model.AgenciaId,
                Texto = model.Texto,
                TipoColaboracion = model.TipoColaboracion,
                NUC = model.NUC,
                UsuarioSolicita = model.UsuarioSolicita,
                AgenciaOrigen = model.AgenciaOrigen,
                AgenciaDestino = model.AgenciaDestino,
                Status = model.Status,
                Respuesta = model.Respuesta,
                FechaRespuesta = model.FechaRespuesta,
                FechaRechazo = model.FechaRechazo,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,


            };

            _context.SColaboracionMPs.Add(hi);
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

        // PUT: api/SColaboracionMP/ActualizarRespuesta
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarRespuesta([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.SColaboracionMPs.FirstOrDefaultAsync(a => a.IdSColaboracionMP == model.IdSColaboracionMP);

            if (ao == null)
            {
                return NotFound();
            }

            ao.Respuesta = model.Respuesta;
            ao.FechaRespuesta = System.DateTime.Now;
            ao.Status = model.Status;

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

        // GET: api/SColaboracionMP/ListarporAgencia
        [HttpGet("[action]/{Idagencia}")]
        public async Task<IEnumerable<SColaboracionMPViewModel>> ListarporAgencia([FromRoute] Guid Idagencia)
        {
            var hi = await _context.SColaboracionMPs
                .Where(a => a.AgenciaId == Idagencia)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return hi.Select(a => new SColaboracionMPViewModel
            {
                IdSColaboracionMP = a.IdSColaboracionMP,
                RHechoId = a.RHechoId,
                AgenciaId = a.AgenciaId,
                Texto = a.Texto,
                TipoColaboracion = a.TipoColaboracion,
                NUC = a.NUC,
                UsuarioSolicita = a.UsuarioSolicita,
                AgenciaOrigen = a.AgenciaOrigen,
                AgenciaDestino = a.AgenciaDestino,
                Status = a.Status,
                Respuesta = a.Respuesta,
                FechaRespuesta = a.FechaRespuesta,
                FechaRechazo = a.FechaRechazo,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,

            });

        }

        // PUT: api/SColaboracionMP/Actualizarrechazo
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizarrechazo([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.SColaboracionMPs.FirstOrDefaultAsync(a => a.IdSColaboracionMP == model.IdSColaboracionMP);

            if (ao == null)
            {
                return NotFound();
            }

            ao.FechaRechazo = System.DateTime.Now;
            ao.Respuesta = model.Respuesta;
            ao.Status = model.Status;

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

        // PUT: api/SColaboracionMP/ActualizarStatus
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.SColaboracionMPs.FirstOrDefaultAsync(a => a.IdSColaboracionMP == model.IdSColaboracionMP);

            if (ao == null)
            {
                return NotFound();
            }

            ao.Status = model.Status;

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
