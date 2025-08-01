using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Configuracion.DocIdentificacion;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Configuracion;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocIdentificacionsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public DocIdentificacionsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/DocIdentificacions/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var DocIdentificacion = await _context.DocIdentificacions
                                                      .OrderBy(a => a.Nombre)
                                                      .ToListAsync();

                return Ok(DocIdentificacion.Select(a => new DocIdentificacionViewModel
                {
                    IdDocIdentificacion = a.IdDocIdentificacion,
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


        // PUT: api/DocIdentificacions/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var DocIdentificacion = await _context.DocIdentificacions.FirstOrDefaultAsync(a => a.IdDocIdentificacion == model.IdDocIdentificacion);

            if (DocIdentificacion == null)
            {
                return NotFound();
            }

            DocIdentificacion.Nombre = model.Nombre;

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

        // POST: api/DocIdentificacions/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DocIdentificacion docIdentificacion = new DocIdentificacion
            {
                Nombre = model.Nombre
            };

            _context.DocIdentificacions.Add(docIdentificacion);
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


        private bool DocIdentificacionExists(Guid id)
        {
            return _context.DocIdentificacions.Any(e => e.IdDocIdentificacion == id);
        }
    }
}