using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Largo_de_Cabellos;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Largo_de_CabelloController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Largo_de_CabelloController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Largo_de_Cabello/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Largo_de_CabelloViewModel>> Listar()
        {
            var ao = await _context.Largo_De_Cabellos
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Largo_de_CabelloViewModel
            {
                IdLargoCabello = a.IdLargoCabello,
                Dato = a.Dato,

            });

        }


        // POST: api/Largo_de_Cabello/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Largo_de_Cabello ao = new Largo_de_Cabello
            {
                Dato = model.Dato,

            };

            _context.Largo_De_Cabellos.Add(ao);
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

        // PUT: api/Largo_de_Cabello/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ao = await _context.Largo_De_Cabellos.FirstOrDefaultAsync(a => a.IdLargoCabello == model.IdLargoCabello);

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
