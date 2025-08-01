using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Adherencia_Orejas;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Adherencia_OrejasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Adherencia_OrejasController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Adherencia_Orejas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Adherencia_OrejasViewModel>> Listar()
        {
            var ao = await _context.Adherencia_Orejas
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Adherencia_OrejasViewModel
            {
                IdAdherencia_Oreja = a.IdAdherencia_Oreja,
                Dato = a.Dato,

            });

        }


        // POST: api/Adherencia_Orejas/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Adherencia_Oreja ao = new Adherencia_Oreja
            {
                Dato = model.Dato,

            };

            _context.Adherencia_Orejas.Add(ao);
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

        // PUT: api/Adherencia_Orejas/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.Adherencia_Orejas.FirstOrDefaultAsync(a => a.IdAdherencia_Oreja == model.IdAdherencia_Oreja);

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
