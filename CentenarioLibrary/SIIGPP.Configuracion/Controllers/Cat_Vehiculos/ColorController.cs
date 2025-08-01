using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Vehiculos.Color;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos;

namespace SIIGPP.Configuracion.Controllers.Cat_Vehiculos
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public ColorController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Color/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ColorViewModel>> Listar()
        {
            var color = await _context.Colors.OrderBy(m => m.Dato).ToListAsync();

            return color.Select(a => new ColorViewModel
            {
                IdColor = a.IdColor,
                Dato = a.Dato
            });

        }


        // PUT: api/Color/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var color = await _context.Colors.FirstOrDefaultAsync(a => a.IdColor == model.IdColor);

            if (color == null)
            {
                return NotFound();
            }

            color.Dato = model.Dato;

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


        // POST: api/Color/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Color color = new Color
            {
                Dato = model.Dato

            };

            _context.Colors.Add(color);
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

    }
}
