using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Tipo_De_Mentons;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tipo_De_MentonsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Tipo_De_MentonsController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Tipo_De_Mentons/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Tipo_De_MentonsViewModel>> Listar()
        {
            var ao = await _context.Tipo_De_Mentons
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Tipo_De_MentonsViewModel
            {
                IdTipo_de_Menton = a.IdTipo_de_Menton,
                Dato = a.Dato,

            });

        }


        // POST: api/Tipo_De_Mentons/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tipo_de_Menton ao = new Tipo_de_Menton
            {
                Dato = model.Dato,

            };

            _context.Tipo_De_Mentons.Add(ao);

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

        // PUT: api/Tipo_De_Mentons/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.Tipo_De_Mentons.FirstOrDefaultAsync(a => a.IdTipo_de_Menton == model.IdTipo_de_Menton);

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
