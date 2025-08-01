using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Color_Ojos;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Color_OjosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Color_OjosController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Color_Ojos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Color_OjoViewModel>> Listar()
        {
            var ao = await _context.Color_Ojos
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Color_OjoViewModel
            {
                IdColorOjos = a.IdColorOjos,
                Dato = a.Dato,

            });

        }


        // POST: api/Color_Ojos/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Color_Ojos ao = new Color_Ojos
            {
                Dato = model.Dato,

            };

            _context.Color_Ojos.Add(ao);
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

        // PUT: api/Color_Ojos/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          

            var ao = await _context.Color_Ojos.FirstOrDefaultAsync(a => a.IdColorOjos == model.IdColorOjos);

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
