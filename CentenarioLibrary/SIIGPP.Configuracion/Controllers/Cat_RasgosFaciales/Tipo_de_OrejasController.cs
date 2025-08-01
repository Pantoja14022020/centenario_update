using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Tipos_de_Orejas;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tipo_de_OrejasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Tipo_de_OrejasController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Tipo_de_Orejas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Tipo_de_OrejasViewModel>> Listar()
        {
            var ao = await _context.Tipo_De_Orejas
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Tipo_de_OrejasViewModel
            {
                IdTipoOrejas = a.IdTipoOrejas,
                Dato = a.Dato,

            });

        }


        // POST: api/Tipo_de_Orejas/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tipo_de_Orejas ao = new Tipo_de_Orejas
            {
                Dato = model.Dato,

            };

            _context.Tipo_De_Orejas.Add(ao);
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

        // PUT: api/Tipo_de_Orejas/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

        

            var ao = await _context.Tipo_De_Orejas.FirstOrDefaultAsync(a => a.IdTipoOrejas == model.IdTipoOrejas);

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
