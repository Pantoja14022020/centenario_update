using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Vehiculos.Ano;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos;

namespace SIIGPP.Configuracion.Controllers.Cat_Vehiculos
{

    [Route("api/[controller]")]
    [ApiController]
    public class AnoController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public AnoController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Ano/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<AnoViewModel>> Listar()
        {
            var ano = await _context.Anos.OrderBy(m => m.Dato).ToListAsync();

            return ano.Select(a => new AnoViewModel
            {
                IdAno = a.IdAno,
                Dato = a.Dato
        });

        }


        // PUT: api/Ano/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ano = await _context.Anos.FirstOrDefaultAsync(a => a.IdAno== model.IdAno);

            if (ano == null)
            {
                return NotFound();
            }

            ano.Dato = model.Dato;

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


        // POST: api/Ano/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ano ano = new Ano
            {
                Dato = model.Dato

            };

            _context.Anos.Add(ano);
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
