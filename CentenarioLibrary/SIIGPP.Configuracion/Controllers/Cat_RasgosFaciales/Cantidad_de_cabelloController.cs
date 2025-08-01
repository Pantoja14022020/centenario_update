using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Cantidad_de_cabellos;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cantidad_de_cabelloController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Cantidad_de_cabelloController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Cantidad_de_cabello/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Cantidad_de_cabelloViewModel>> Listar()
        {
            var ao = await _context.Cantidad_De_Cabellos 
                .ToListAsync();

            return ao.Select(a => new Cantidad_de_cabelloViewModel
            {
             IdCantidadCabello = a.IdCantidadCabello,
             Dato = a.Dato,

            });

        }


        // POST: api/Cantidad_de_cabello/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cantidad_de_cabello ao = new Cantidad_de_cabello
            {
                Dato = model.Dato,

            };

            _context.Cantidad_De_Cabellos.Add(ao);
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

        // PUT: api/Cantidad_de_cabello/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.Cantidad_De_Cabellos.FirstOrDefaultAsync(a => a.IdCantidadCabello == model.IdCantidadCabello);

            if (ao == null)
            {
                return NotFound();
            }

            ao.Dato = model.Dato;

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

    }
}
