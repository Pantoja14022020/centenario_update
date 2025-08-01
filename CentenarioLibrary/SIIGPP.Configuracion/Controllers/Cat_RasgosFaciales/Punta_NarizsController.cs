using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Punta_Narizs;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Punta_NarizsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Punta_NarizsController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Punta_Narizs/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Punta_NarizsViewModel>> Listar()
        {
            var ao = await _context.Punta_Narizs
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Punta_NarizsViewModel
            {
                IdPunta_Nariz = a.IdPunta_Nariz,
                Dato = a.Dato,

            });

        }


        // POST: api/Punta_Narizs/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Punta_Nariz ao = new Punta_Nariz
            {
                Dato = model.Dato,

            };

            _context.Punta_Narizs.Add(ao);
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

        // PUT: api/Punta_Narizs/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.Punta_Narizs.FirstOrDefaultAsync(a => a.IdPunta_Nariz == model.IdPunta_Nariz);

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
