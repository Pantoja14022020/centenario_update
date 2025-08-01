using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Tamaños_de_Boca;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tamaño_de_BocaController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Tamaño_de_BocaController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Tamaño_de_Boca/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Tamaño_de_BocaViewModel>> Listar()
        {
            var ao = await _context.Tamaño_De_Bocas
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Tamaño_de_BocaViewModel
            {
                IdTamañoBoca = a.IdTamañoBoca,
                Dato = a.Dato,

            });

        }


        // POST: api/Tamaño_de_Boca/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tamaño_de_Boca ao = new Tamaño_de_Boca
            {
                Dato = model.Dato,

            };

            _context.Tamaño_De_Bocas.Add(ao);
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

        // PUT: api/Tamaño_de_Boca/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var ao = await _context.Tamaño_De_Bocas.FirstOrDefaultAsync(a => a.IdTamañoBoca == model.IdTamañoBoca);

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
