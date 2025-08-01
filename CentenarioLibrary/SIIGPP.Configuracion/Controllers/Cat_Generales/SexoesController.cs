using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Catalogos.Sexo;
using SIIGPP.Datos; 
using SIIGPP.Entidades.M_Configuracion.Cat_Generales;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SexoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public SexoesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Sexoes/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            var sexo = await _context.Sexos.OrderBy(a => a.Nombre).ToListAsync();
            try
            {
                return Ok(sexo.Select(a => new SexoViewModel
                {
                    IdSexo = a.IdSexo,
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

        // GET: api/Sexoes/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var sexo = await _context.Sexos.FindAsync(id);

            if (sexo == null)
            {
                return NotFound();
            }

            return Ok(new SexoViewModel
            {
                IdSexo = sexo.IdSexo,
                Clave = sexo.Clave,
                Nombre = sexo.Nombre
            });
        }

        // PUT: api/Sexoes/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var sexo = await _context.Sexos.FirstOrDefaultAsync(a => a.IdSexo == model.IdSexo);

            if (sexo == null)
            {
                return NotFound();
            }
            sexo.Clave = model.Clave;
            sexo.Nombre = model.Nombre;


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

        // POST: api/Sexoes/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Sexo sexo = new Sexo
            {
                Clave = model.Clave,
                Nombre = model.Nombre

            };

            _context.Sexos.Add(sexo);
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

        private bool SexoExists(Guid id)
        {
            return _context.Sexos.Any(a => a.IdSexo == id);
        }

    }
}