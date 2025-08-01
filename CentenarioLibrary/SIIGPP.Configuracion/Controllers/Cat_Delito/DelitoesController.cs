using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Delito.Delito;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DelitoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public DelitoesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Delitoes/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<DelitoViewModel>> Listar()
        {
            var db = await _context.Delitos.ToListAsync();

            return db.Select(a => new DelitoViewModel
            {
                IdDelito = a.IdDelito,
                Nombre = a.Nombre,
                SuceptibleMASC = a.SuceptibleMASC,
                TipoMontoRobo = a.TipoMontoRobo,
                AltoImpacto = a.AltoImpacto,
                OfiNoOfi = a.OfiNoOfi,
                
            });

        }
        // GET: api/Delitoes/ListarPorDelito/delitoId
        
        [HttpGet("[action]/{delitoid}")]
        public async Task<IActionResult> ListarPorDelito([FromRoute] Guid delitoid) 
        {
          
            var db = await _context.Delitos.Where(a => a.IdDelito == delitoid).FirstOrDefaultAsync();

            return Ok(new DelitoViewModel 
            {
                IdDelito = db.IdDelito,
                Nombre = db.Nombre,
                SuceptibleMASC = db.SuceptibleMASC,
                TipoMontoRobo = db.TipoMontoRobo,
                AltoImpacto = db.AltoImpacto,
                OfiNoOfi = db.OfiNoOfi,

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

          

            var db = await _context.Delitos.FirstOrDefaultAsync(a => a.IdDelito == model.IdDelito);

            if (db == null)
            {
                return NotFound();
            }

            db.Nombre = model.Nombre;
            db.SuceptibleMASC = model.SuceptibleMASC;
            db.TipoMontoRobo = model.TipoMontoRobo;
            db.AltoImpacto = model.AltoImpacto;
            db.OfiNoOfi = model.OfiNoOfi;
            

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

            Delito dato = new Delito
            {
                Nombre = model.Nombre,
                OfiNoOfi = model.OfiNoOfi,
                TipoMontoRobo = model.TipoMontoRobo,
            };

            _context.Delitos.Add(dato);
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

        private bool DelitoExists(Guid id)
        {
            return _context.Delitos.Any(a => a.IdDelito == id);
        }

    }
}