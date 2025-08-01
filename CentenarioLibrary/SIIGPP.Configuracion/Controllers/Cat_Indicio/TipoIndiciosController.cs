using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Indicios.Tipoindicios;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Indicios;

namespace SIIGPP.Configuracion.Controllers.Cat_Indicio
{


    [Route("api/[controller]")]
    [ApiController]
    public class TipoIndiciosController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public TipoIndiciosController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/TipoIndicios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TipoIndiciosViewModel>> Listar()
        {
            var TipoIndicios = await _context.TipoIndicios.ToListAsync();

            return TipoIndicios.Select(a => new TipoIndiciosViewModel
            {
                IdTipoIndicio = a.IdTipoIndicio,
                Nombre = a.Nombre
            });

        }


        // PUT: api/TipoIndicios/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var tipoindicio = await _context.TipoIndicios.FirstOrDefaultAsync(a => a.IdTipoIndicio == model.IdTipoIndicio);

            if (tipoindicio == null)
            {
                return NotFound();
            }

            tipoindicio.Nombre = model.Nombre;
            

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


        // POST: api/TipoIndicios/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TipoIndicio tipoindicio = new TipoIndicio
            {
                Nombre = model.Nombre,
               

            };

            _context.TipoIndicios.Add(tipoindicio);
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

        private bool TipoIndiciosExists(Guid id)
        {
            return _context.TipoIndicios.Any(e => e.IdTipoIndicio == id);
        }


    }
}
