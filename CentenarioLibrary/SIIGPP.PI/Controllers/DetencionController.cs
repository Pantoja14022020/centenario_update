using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.Detenciones;
using SIIGPP.Entidades.M_PI.Detenciones;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetencionController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public DetencionController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Detencion/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<DetencionViewModel>> Listar()
        {
            var Det = await _context.Detencions
                .Include(a => a.Persona)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return Det.Select(a => new DetencionViewModel
            {
                 IdDetencion = a.IdDetencion,
                 RHechoId = a.RHechoId,
                 PersonaId = a.PersonaId,
                 Nompersona = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                 Nuc = a.Nuc,
                 MpAsignado = a.MpAsignado,
                 Mesa = a.Mesa,
                 FechaIngreso = a.FechaIngreso,
                 FechaSalida = a.FechaSalida,
                 FechaTraslado = a.FechaTraslado,
                 Status = a.Status,
                 AutoridadQO = a.Status,
                 AutoridadED =a.AutoridadED,
                 Delito = a.Delito,
                 Tdelito =a.Tdelito,
                 Modalidad = a.Modalidad,
                 MOperandi = a.MOperandi,
                 UDistrito = a.UDistrito,
                 USubproc = a.USubproc,
                 UAgencia =a.UAgencia,
                 Usuario = a.Usuario,
                 UPuesto = a.UPuesto,
                 UModulo =a.UModulo,
                 Fechasys = a.Fechasys,
                 FechaUltimoStatus = a.FechaUltimoStatus,
                 TipoDetencion = a.TipoDetencion,
                 NumUnicoRegNacional = a.NumUnicoRegNacional,
                 Pertenecnias = a.Pertenecnias

            });

        }
        public string cambiar(int a)
        {
            if (a == 1) return "Custodia";
            else return "Detenido";
                
        }

        // POST: api/Detencion/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Detencion Det = new Detencion
            {
                RHechoId = model.RHechoId,
                PersonaId = model.PersonaId,
                Nuc = model.Nuc,
                MpAsignado = model.MpAsignado,
                Mesa = model.Mesa,
                FechaIngreso = model.FechaIngreso,
                FechaSalida = "",
                FechaTraslado = "",
                Status = cambiar(Int32.Parse(model.Status)),
                AutoridadQO ="",
                AutoridadED =model.AutoridadED,
                Delito = model.Delito,
                Tdelito = model.Tdelito,
                Modalidad =model.Modalidad,
                MOperandi = model.MOperandi,
                UDistrito =model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
                FechaUltimoStatus = System.DateTime.Now,
                TipoDetencion = model.TipoDetencion,
                NumUnicoRegNacional = model.NumUnicoRegNacional,
                Pertenecnias = model.Pertenecnias
            };

            _context.Detencions.Add(Det);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { iddetencion = Det.IdDetencion,status = Det.Status });
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

        // PUT: api/Detencion/ActualizarLiberacion
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarLiberacion([FromBody] ActualizarLiberacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

       

            var de = await _context.Detencions.FirstOrDefaultAsync(a => a.IdDetencion == model.IdDetencion);

            if (de == null)
            {
                return NotFound();
            }

            de.Status = model.Status;
            de.FechaSalida = model.FechaSalida;
            de.AutoridadQO = model.AutoridadQO;
            de.FechaUltimoStatus = System.DateTime.Now;

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



        // PUT: api/Detencion/ActualizarTraslado
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarTraslado([FromBody] ActualizarTrasladoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

       

            var de = await _context.Detencions.FirstOrDefaultAsync(a => a.IdDetencion == model.IdDetencion);

            if (de == null)
            {
                return NotFound();
            }

            de.Status = model.Status;
            de.FechaTraslado = model.FechaTraslado;
            de.FechaUltimoStatus = System.DateTime.Now;

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


        // PUT: api/Detencion/ActualizarStatus
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var de = await _context.Detencions.FirstOrDefaultAsync(a => a.IdDetencion == model.IdDetencion);

            if (de == null)
            {
                return NotFound();
            }

            de.Status = model.Status;
            de.FechaUltimoStatus = System.DateTime.Now;

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

        // PUT: api/Detencion/ActualizarDetencionporegresotemporal
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarDetencionporegresotemporal([FromBody] ActualizarStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

       

            var de = await _context.Detencions.FirstOrDefaultAsync(a => a.IdDetencion == model.IdDetencion);

            if (de == null)
            {
                return NotFound();
            }

            de.Status = model.Status;
            de.FechaUltimoStatus = System.DateTime.Now;
            de.FechaHReingreso = System.DateTime.Now;

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
