using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Grosores_de_labios;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Grosor_de_labiosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Grosor_de_labiosController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Grosor_de_labios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Grosor_de_labiosViewModel>> Listar()
        {
            var ao = await _context.Grosor_De_Labios
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Grosor_de_labiosViewModel
            {
                IdGrosorLabios = a.IdGrosorLabios,
                Dato = a.Dato,

            });

        }


        // POST: api/Grosor_de_labios/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Grosor_de_labios ao = new Grosor_de_labios
            {
                Dato = model.Dato,

            };

            _context.Grosor_De_Labios.Add(ao);
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

        // PUT: api/Grosor_de_labios/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.Grosor_De_Labios.FirstOrDefaultAsync(a => a.IdGrosorLabios == model.IdGrosorLabios);

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
