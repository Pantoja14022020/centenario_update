using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_INEGI.Ocupacion;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcupacionsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public OcupacionsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Ocupacions/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var ocupacion = await _context.Ocupacions.OrderBy(a => a.Nombre).ToListAsync();

                return Ok(ocupacion.Select(a => new OcupacionViewModel
                {
                    IdOcupacion = a.IdOcupacion,
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


        // PUT: api/Ocupacions/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var ocupacion = await _context.Ocupacions.FirstOrDefaultAsync(a => a.IdOcupacion == model.IdOcupacion);

            if (ocupacion == null)
            {
                return NotFound();
            }

            ocupacion.Nombre = model.Nombre; 

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

        // POST: api/Ocupacions/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ocupacion ocupacion = new Ocupacion
            {
                Nombre = model.Nombre, 
            };

            _context.Ocupacions.Add(ocupacion);
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

        private bool OcupacionExists(Guid id)
        {
            return _context.Ocupacions.Any(e => e.IdOcupacion == id);
        }
    }
}