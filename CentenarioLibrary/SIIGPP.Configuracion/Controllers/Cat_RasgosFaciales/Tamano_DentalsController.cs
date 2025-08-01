using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Tamano_Dentals;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tamano_DentalsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Tamano_DentalsController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Tamano_Dentals/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Tamano_DentalsViewModel>> Listar()
        {
            var ao = await _context.Tamano_Dentals
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Tamano_DentalsViewModel
            {
                IdTamano_Dental = a.IdTamano_Dental,
                Dato = a.Dato,

            });

        }


        // POST: api/Tamano_Dentals/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tamano_Dental ao = new Tamano_Dental
            {
                Dato = model.Dato,

            };

            _context.Tamano_Dentals.Add(ao);
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

        // PUT: api/Tamano_Dentals/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.Tamano_Dentals.FirstOrDefaultAsync(a => a.IdTamano_Dental == model.IdTamano_Dental);

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
