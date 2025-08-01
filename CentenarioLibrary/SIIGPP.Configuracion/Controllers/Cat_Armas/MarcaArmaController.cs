using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Armas.MarcaArmas;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Armas;

namespace SIIGPP.Configuracion.Controllers.Cat_Armas
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaArmaController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public MarcaArmaController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/MarcaArma/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<MarcaArmaViewModel>> Listar()
        {
            var ca = await _context.MarcaArmas.ToListAsync();

            return ca.Select(a => new MarcaArmaViewModel
            {
                IdMarcaArma = a.IdMarcaArma,
                Nombre  = a.Nombre,
            });

        }

        // POST: api/MarcaArma/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MarcaArma ca = new MarcaArma
            {
                Nombre = model.Nombre,
            };

            _context.MarcaArmas.Add(ca);
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

        // PUT: api/MarcaArma/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.MarcaArmas.FirstOrDefaultAsync(a => a.IdMarcaArma == model.IdMarcaArma);

            if (ao == null)
            {
                return NotFound();
            }

            ao.Nombre = model.Nombre;

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
