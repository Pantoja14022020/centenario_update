using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Delito.IntensionDelito;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;

namespace SIIGPP.Configuracion.Controllers.Cat_Delito
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntensionDelitoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public IntensionDelitoesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/IntensionDelitoes/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<IntensionDelitoViewModel>> Listar()
        {
            var db = await _context.IntensionDelitos.ToListAsync();

            return db.Select(a => new IntensionDelitoViewModel
            {
                IdIntensionDelito = a.IdIntesionDelio,
                Nombre = a.Nombre
            });

        }


        // PUT: api/IntensionDelitoes/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var db = await _context.IntensionDelitos.FirstOrDefaultAsync(a => a.IdIntesionDelio == model.IdIntensionDelito);

            if (db == null)
            {
                return NotFound();
            }

            db.Nombre = model.Nombre;

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

        // POST: api/IntensionDelitoes/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IntensionDelito dato = new IntensionDelito
            {
                Nombre = model.Nombre
            };

            _context.IntensionDelitos.Add(dato);
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
        private bool IntensionDelitoExists(Guid id)
        {
            return _context.IntensionDelitos.Any(e => e.IdIntesionDelio == id);
        }
    }
}