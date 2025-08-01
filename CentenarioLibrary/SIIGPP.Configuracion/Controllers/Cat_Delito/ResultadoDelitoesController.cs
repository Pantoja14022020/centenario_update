using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Delito.ResultadoDelito;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;

namespace SIIGPP.Configuracion.Controllers.Cat_Delito
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadoDelitoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public ResultadoDelitoesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/ResultadoDelitoes/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ResultadoDelitoViewModel>> Listar()
        {
            var db = await _context.ResultadoDelitos.ToListAsync();

            return db.Select(a => new ResultadoDelitoViewModel
            {
                IdResultadoDelito = a.IdResultadoDelito,
                Nombre = a.Nombre
            });

        }


        // PUT: api/ResultadoDelitoes/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var db = await _context.ResultadoDelitos.FirstOrDefaultAsync(a => a.IdResultadoDelito == model.IdResultadoDelito);

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

        // POST: api/ResultadoDelitoes/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResultadoDelito dato = new ResultadoDelito
            {
                Nombre = model.Nombre
            };

            _context.ResultadoDelitos.Add(dato);
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

        private bool ResultadoDelitoExists(Guid id)
        {
            return _context.ResultadoDelitos.Any(e => e.IdResultadoDelito == id);
        }
    }
}