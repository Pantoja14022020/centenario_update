using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Configuracion.NivelEstudios;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Configuracion;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelEstudiosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public NivelEstudiosController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/NivelEstudios/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var ne = await _context.NivelEstudios
                    .OrderBy(a => a.Nombre)
                                .ToListAsync();

                return Ok(ne.Select(a => new NivelEstudiosViewModel
                {
                    IdNivelestudios = a.IdNivelEstudios,
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


        // PUT: api/NivelEstudios/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var ne = await _context.NivelEstudios.FirstOrDefaultAsync(a => a.IdNivelEstudios == model.IdNivelestudios);

            if (ne == null)
            {
                return NotFound();
            }

            ne.Nombre = model.Nombre;

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

        // POST: api/NivelEstudios/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NivelEstudios ne = new NivelEstudios
            {
                Nombre = model.Nombre
            };

            _context.NivelEstudios.Add(ne);
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

        private bool NivelEstudiosExists(Guid id)
        {
            return _context.NivelEstudios.Any(e => e.IdNivelEstudios == id);
        }
    }
}