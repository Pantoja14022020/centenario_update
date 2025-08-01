using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Hipotesis;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Hipotesis;

namespace SIIGPP.Configuracion.Controllers.Cat_Hipotesis
{
    [Route("api/[controller]")]
    [ApiController]
    public class HipotesisController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public HipotesisController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Hipotesis/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<HipotesisViewModel>> Listar()
        {
            var ao = await _context.Hipoteses
                .ToListAsync();

            return ao.Select(a => new HipotesisViewModel
            {
                IdHipotesis = a.IdHipotesis,
                Dato = a.Dato,

            });

        }


        // POST: api/Hipotesis/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hipotesis ao = new Hipotesis
            {
                Dato = model.Dato,

            };

            _context.Hipoteses.Add(ao);
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
