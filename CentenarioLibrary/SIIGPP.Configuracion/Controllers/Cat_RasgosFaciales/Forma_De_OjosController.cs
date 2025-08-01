using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Forma_De_Ojos;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Forma_De_OjosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Forma_De_OjosController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Forma_De_Ojos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Forma_De_OjosViewModel>> Listar()
        {
            var ao = await _context.Forma_De_Ojos
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Forma_De_OjosViewModel
            {
                IdForma_de_ojo = a.IdForma_de_ojo,
                Dato = a.Dato,

            });

        }


        // POST: api/Forma_De_Ojos/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Forma_de_ojo ao = new Forma_de_ojo
            {
                Dato = model.Dato,

            };

            _context.Forma_De_Ojos.Add(ao);
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

        // PUT: api/Forma_De_Ojos/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.Forma_De_Ojos.FirstOrDefaultAsync(a => a.IdForma_de_ojo == model.IdForma_de_ojo);

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
