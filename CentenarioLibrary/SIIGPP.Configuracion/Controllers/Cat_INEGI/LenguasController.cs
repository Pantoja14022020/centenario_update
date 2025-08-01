using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_INEGI.Lengua;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LenguasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public LenguasController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Lenguas/Listar 
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var lengua = await _context.Lenguas.OrderBy(a => a.Nombre).ToListAsync();

                return Ok(lengua.Select(a => new LenguaViewModel
                {
                    IdLengua = a.IdLengua,
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


        // PUT: api/Lenguas/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var lengua = await _context.Lenguas.FirstOrDefaultAsync(a => a.IdLengua == model.IdLengua);

            if (lengua == null)
            {
                return NotFound();
            }

           lengua.Nombre = model.Nombre; 

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

        // POST: api/Lenguas/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           Lengua lengua = new Lengua
            {
                Nombre = model.Nombre
            };

            _context.Lenguas.Add(lengua);
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

        private bool LenguaExists(Guid id)
        {
            return _context.Lenguas.Any(e => e.IdLengua == id);
        }
    }
}