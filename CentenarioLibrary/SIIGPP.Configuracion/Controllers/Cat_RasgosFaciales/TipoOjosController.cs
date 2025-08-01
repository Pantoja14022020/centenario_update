using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_RasgosFaciales.Tipo_Ojos;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Configuracion.Controllers.Cat_RasgosFaciales
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoOjosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public TipoOjosController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/TipoOjos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TipoOjosViewModel>> Listar()
        {
            var ao = await _context.TipoOjos
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new TipoOjosViewModel
            {
                IdTipoOjos = a.IdTipoOjos,
                Dato = a.Dato,

            });

        }


        // POST: api/TipoOjos/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoOjos ao = new TipoOjos
            {
                Dato = model.Dato,

            };

            _context.TipoOjos.Add(ao);
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

        // PUT: api/TipoOjos/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          

            var ao = await _context.TipoOjos.FirstOrDefaultAsync(a => a.IdTipoOjos == model.IdTipoOjos);

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

        // GET: api/TipoOjos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TipoOjos2ViewModel>> ListarTipoOjo()
        {
            var ao = await _context.Tipo2Ojos
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return ao.Select(a => new TipoOjos2ViewModel
            {
                IdTipodeOjos = a.IdTipodeOjos,
                Dato = a.Dato,

            });

        }

    }
}
