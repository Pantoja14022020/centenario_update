using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Incompetencia;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Incompetencias;

namespace SIIGPP.Configuracion.Controllers.Cat_Incompetencia
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncompetenciaController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public IncompetenciaController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Incompetencia/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<IncompetenciaViewModel>> Listar()
        {
            var inco = await _context.Incompetencias.ToListAsync();

            return inco.Select(a => new IncompetenciaViewModel
            {
                IdIncompetencia = a.IdIncompetencia,
                nombre = a.nombre
            });

        }


        // PUT: api/Incompetencia/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var inco = await _context.Incompetencias.FirstOrDefaultAsync(a => a.IdIncompetencia == model.IdIncompetencia);

            if (inco == null)
            {
                return NotFound();
            }

            inco.nombre = model.nombre;

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


        // POST: api/Incompetencia/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Incompetencia inco = new Incompetencia
            {
                nombre = model.nombre

            };

            _context.Incompetencias.Add(inco);
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
