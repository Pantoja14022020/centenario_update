using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.TamañoNariz;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;
namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tamaño_NarizController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Tamaño_NarizController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Tamaño_Nariz/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Tamaño_NarizViewModel>> Listar()
        {
            var ao = await _context.Tamaño_Narizs
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Tamaño_NarizViewModel
            {
                IdTamañoNariz = a.IdTamañoNariz,
                Dato = a.Dato,

            });

        }


        // POST: api/Tamaño_Nariz/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tamaño_Nariz ao = new Tamaño_Nariz
            {
                Dato = model.Dato,

            };

            _context.Tamaño_Narizs.Add(ao);
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

        // PUT: api/Tamaño_Nariz/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          
            var ao = await _context.Tamaño_Narizs.FirstOrDefaultAsync(a => a.IdTamañoNariz == model.IdTamañoNariz);

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
