using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Formas_de_Menton;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Forma_de_MentonController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Forma_de_MentonController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Forma_de_Menton/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Forma_de_MentonViewModel>> Listar()
        {
            var ao = await _context.Forma_De_Mentons
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Forma_de_MentonViewModel
            {
                IdFormaMenton = a.IdFormaMenton,
                Dato = a.Dato,

            });

        }


        // POST: api/Forma_de_Menton/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Forma_de_Menton ao = new Forma_de_Menton
            {
                Dato = model.Dato,

            };

            _context.Forma_De_Mentons.Add(ao);
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

        // PUT: api/Forma_de_Menton/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          

            var ao = await _context.Forma_De_Mentons.FirstOrDefaultAsync(a => a.IdFormaMenton == model.IdFormaMenton);

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
