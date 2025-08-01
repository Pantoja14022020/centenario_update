using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Configuracion.MedioNotificacion;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Configuracion;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedioNotificacionsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public MedioNotificacionsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/MedioNotificacions/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var mi = await _context.MedioNotificacions
                                        .OrderBy(a => a.Nombre)
                                        .ToListAsync();

                return Ok(mi.Select(a => new MedioNotificacionViewModel
                {
                    IdMedioNotificacion = a.IdMedioNotificacion,
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


        // PUT: api/MedioNotificacions/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mi = await _context.MedioNotificacions.FirstOrDefaultAsync(a => a.IdMedioNotificacion == model.IdMedioNotificacion);

            if (mi == null)
            {
                return NotFound();
            }

           mi.Nombre = model.Nombre;

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

        // POST: api/MedioNotificacions/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MedioNotificacion mi = new MedioNotificacion
            {
                Nombre = model.Nombre
            };

            _context.MedioNotificacions.Add(mi);
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

        private bool MedioNotificacionExists(Guid id)
        {
            return _context.MedioNotificacions.Any(e => e.IdMedioNotificacion == id);
        }
    }
}