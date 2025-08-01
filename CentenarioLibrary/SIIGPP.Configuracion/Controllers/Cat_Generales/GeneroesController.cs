using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Generales.Genero;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Generales;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public GeneroesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Generoes/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var genero = await _context.Generos.OrderBy(a => a.Nombre).ToListAsync();

                return Ok(genero.Select(a => new GeneroViewModel
                {
                    IdGenero = a.IdGenero,
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


        // PUT: api/Generoes/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var genero = await _context.Generos.FirstOrDefaultAsync(a => a.IdGenero == model.IdGenero);

            if (genero == null)
            {
                return NotFound();
            }

            genero.Nombre = model.Nombre; 

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

        // POST: api/Estadoes/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Genero genero = new Genero
            {
                Nombre = model.Nombre 
            };

            _context.Generos.Add(genero);
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

        private bool GeneroExists(Guid id)
        {
            return _context.Generos.Any(e => e.IdGenero == id);
        }
    }
}