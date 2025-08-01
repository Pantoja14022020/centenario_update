using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_INEGI.Discapacidad;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscapacidadsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public DiscapacidadsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Discapacidads/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var discapacidad = await _context.Discapacidads.OrderBy(a => a.Nombre).ToListAsync();

                return Ok(discapacidad.Select(a => new DiscapacidadViewModel
                {
                    IdDiscapacidad = a.IdDiscapacidad,
                    Nombre = a.Nombre
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, ExtraInfo = ex.Message });
                result.StatusCode = 402;
                return result;
            }
        }


        // PUT: api/Discapacidads/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var discapacidad = await _context.Discapacidads.FirstOrDefaultAsync(a => a.IdDiscapacidad == model.IdDiscapacidad);

            if (discapacidad == null)
            {
                return NotFound();
            }

            discapacidad.Nombre = model.Nombre;

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

        // POST: api/Discapacidads/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Discapacidad discapacidad = new Discapacidad
            {
                Nombre = model.Nombre
            };

            _context.Discapacidads.Add(discapacidad);
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
        private bool DiscapacidadExists(Guid id)
        {
            return _context.Discapacidads.Any(e => e.IdDiscapacidad == id);
        }
    }
}