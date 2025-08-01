using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Tratamientos_Quimicos_Cabellos;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tratamientos_Quimicos_CabellosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public Tratamientos_Quimicos_CabellosController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Tratamientos_Quimicos_Cabellos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Tratamientos_Quimicos_CabellosViewModel>> Listar()
        {
            var ao = await _context.Tratamientos_Quimicos_Cabellos
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new Tratamientos_Quimicos_CabellosViewModel
            {
                IdTratamientos_Quimicos_Cabello = a.IdTratamientos_Quimicos_Cabello,
                Dato = a.Dato,

            });

        }


        // POST: api/Tratamientos_Quimicos_Cabellos/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tratamientos_Quimicos_Cabello ao = new Tratamientos_Quimicos_Cabello
            {
                Dato = model.Dato,

            };

            _context.Tratamientos_Quimicos_Cabellos.Add(ao);

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

        // PUT: api/Tratamientos_Quimicos_Cabellos/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.Tratamientos_Quimicos_Cabellos.FirstOrDefaultAsync(a => a.IdTratamientos_Quimicos_Cabello == model.IdTratamientos_Quimicos_Cabello);

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
