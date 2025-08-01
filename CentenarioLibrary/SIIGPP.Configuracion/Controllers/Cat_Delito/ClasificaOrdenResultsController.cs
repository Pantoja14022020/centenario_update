using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Delito.ClasificaOrdenResult;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;

namespace SIIGPP.Configuracion.Controllers.Cat_Delito
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasificaOrdenResultsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public ClasificaOrdenResultsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/ClasificaOrdenResults/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ClasificaOrdenResultViewModel>> Listar()
        {
            var db = await _context.ClasificaOrdenResults.ToListAsync();

            return db.Select(a => new ClasificaOrdenResultViewModel
            {
                IdClasificaOrdenResult = a.IdClasificaOrdenResult,
                Nombre = a.Nombre
            });

        }


        // PUT: api/ClasificaOrdenResults/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var db = await _context.ClasificaOrdenResults.FirstOrDefaultAsync(a => a.IdClasificaOrdenResult == model.IdClasificaOrdenResult);

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

        // POST: api/ClasificaOrdenResults/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClasificaOrdenResult dato = new ClasificaOrdenResult
            {
                Nombre = model.Nombre
            };

            _context.ClasificaOrdenResults.Add(dato);
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

        private bool ClasificaOrdenResultExists(Guid id)
        {
            return _context.ClasificaOrdenResults.Any(e => e.IdClasificaOrdenResult == id);
        }
    }
}