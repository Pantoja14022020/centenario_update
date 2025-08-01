using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_INEGI.Nacionalidad;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NacionalidadsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public NacionalidadsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Nacionalidads/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var nacionalidad = await _context.Nacionalidads.OrderBy(a => a.Nombre).ToListAsync();

                return Ok(nacionalidad.Select(a => new NacionalidadViewModel
                {
                    IdNacionalidad = a.IdNacionalidad,
                    Clave = a.Clave,
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



        // PUT: api/Nacionalidads/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var nacionalidad = await _context.Nacionalidads.FirstOrDefaultAsync(a => a.IdNacionalidad == model.IdNacionalidad);

            if (nacionalidad == null)
            {
                return NotFound();
            }
            nacionalidad.Clave = model.Clave;
            nacionalidad.Nombre = model.Nombre;


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

        // POST: api/Nacionalidads/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Nacionalidad nacionalidad = new Nacionalidad
            {
                Clave = model.Clave,
                Nombre = model.Nombre

            };

            _context.Nacionalidads.Add(nacionalidad);
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

        private bool NacionalidadExists(Guid id)
        {
            return _context.Nacionalidads.Any(e => e.IdNacionalidad == id);
        }
    }
}