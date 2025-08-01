using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Delito.TipoDeclaracion;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;

namespace SIIGPP.Configuracion.Controllers.Cat_Delito
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDeclaracionsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public TipoDeclaracionsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/TipoDeclaracions/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TipoDeclaracionViewModel>> Listar()
        {
            var db = await _context.TipoDeclaracions.ToListAsync();

            return db.Select(a => new TipoDeclaracionViewModel
            {
                IdTipoDeclaracion = a.IdTipoDeclaracion,
                Nombre = a.Nombre
            });

        }


        // PUT: api/TipoDeclaracions/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var db = await _context.TipoDeclaracions.FirstOrDefaultAsync(a => a.IdTipoDeclaracion == model.IdTipoDeclaracion);

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

        // POST: api/TipoDeclaracions/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoDeclaracion dato = new TipoDeclaracion
            {
                Nombre = model.Nombre
            };

            _context.TipoDeclaracions.Add(dato);
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

        private bool TipoDeclaracionExists(Guid id)
        {
            return _context.TipoDeclaracions.Any(e => e.IdTipoDeclaracion == id);
        }
    }
}