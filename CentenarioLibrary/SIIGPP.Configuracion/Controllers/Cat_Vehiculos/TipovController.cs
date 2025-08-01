using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Vehiculos.Tipov;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos;

namespace SIIGPP.Configuracion.Controllers.Cat_Vehiculos
{

    [Route("api/[controller]")]
    [ApiController]
    public class TipovController : ControllerBase
    {
        
        private readonly DbContextSIIGPP _context;

        public TipovController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Tipov/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TipovViewModel>> Listar()
        {
            var tipov = await _context.Tipovs.OrderBy(m => m.Dato).ToListAsync();

            return tipov.Select(a => new TipovViewModel
            {
                IdTipov = a.IdTipov,
                Dato = a.Dato
            });

        }


        // PUT: api/Tipov/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipov = await _context.Tipovs.FirstOrDefaultAsync(a => a.IdTipov== model.IdTipov);

            if (tipov == null)
            {
                return NotFound();
            }

            tipov.Dato = model.Dato;

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


        // POST: api/Tipov/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tipov tipov = new Tipov
            {
                Dato = model.Dato
            };

            _context.Tipovs.Add(tipov);
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





    }
}
