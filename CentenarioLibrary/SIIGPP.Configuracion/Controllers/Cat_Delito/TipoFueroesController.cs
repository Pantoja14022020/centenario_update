using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Delito.TipoFuero;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;

namespace SIIGPP.Configuracion.Controllers.Cat_Delito
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoFueroesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public TipoFueroesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/TipoFueroes/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TipoFueroViewModel>> Listar()
        {
            var db = await _context.TipoFueros.ToListAsync();

            return db.Select(a => new TipoFueroViewModel
            {
                IdTipoFuero = a.IdTipoFuero,
                Nombre = a.Nombre
            });

        }


        // PUT: api/Delitoes/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var db = await _context.TipoFueros.FirstOrDefaultAsync(a => a.IdTipoFuero == model.IdTipoFuero);

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

        // POST: api/Delitoes/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoFuero dato = new TipoFuero
            {
                Nombre = model.Nombre
            };

            _context.TipoFueros.Add(dato);
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

        private bool TipoFueroExists(Guid id)
        {
            return _context.TipoFueros.Any(e => e.IdTipoFuero == id);
        }
    }
}