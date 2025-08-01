using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Tratamiento_Dentals;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tratamiento_DentalsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Tratamiento_DentalsController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Tratamiento_Dentals/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Tratamiento_DentalsViewModel>> Listar()
        {
            var ao = await _context.Tratamiento_Dentals
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Tratamiento_DentalsViewModel
            {
                IdTratamiento_Dental = a.IdTratamiento_Dental,
                Dato = a.Dato,

            });

        }


        // POST: api/Tratamiento_Dentals/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tratamiento_Dental ao = new Tratamiento_Dental
            {
                Dato = model.Dato,

            };

            _context.Tratamiento_Dentals.Add(ao);

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

        // PUT: api/Tratamiento_Dentals/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.Tratamiento_Dentals.FirstOrDefaultAsync(a => a.IdTratamiento_Dental == model.IdTratamiento_Dental);

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
