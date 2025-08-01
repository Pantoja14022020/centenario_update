using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Delito.DelitoEspecifico;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;

namespace SIIGPP.Configuracion.Controllers.Cat_Delito
{
    [Route("api/[controller]")]
    [ApiController]
    public class DelitoEspecificoController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public DelitoEspecificoController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/DelitoEspecifico/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<DelitoEspecificoViewModel>> Listar()
        {
            var db = await _context.DelitoEspecificos
                .Include(a => a.Delito)
                .ToListAsync();

            return db.Select(a => new DelitoEspecificoViewModel
            {
                IdDelitoEspecifico = a.IdDelitoEspecifico,
                Nombre = a.Nombre,
                DelitoId = a.DelitoId,
                NombreDelito = a.Delito.Nombre
            });

        }


        // GET: api/DelitoEspecifico/ListarPorDelito
        [HttpGet("[action]/{delitoid}")]
        public async Task<IEnumerable<DelitoEspecificoViewModel>> ListarPorDelito([FromRoute] Guid delitoid)
        {
            var db = await _context.DelitoEspecificos
                .Where(a => a.DelitoId == delitoid)
                .Include(a => a.Delito)
                .ToListAsync();

            return db.Select(a => new DelitoEspecificoViewModel
            {
                IdDelitoEspecifico = a.IdDelitoEspecifico,
                Nombre = a.Nombre,
                DelitoId = a.DelitoId,
                NombreDelito = a.Delito.Nombre
            });

        }


        // PUT: api/DelitoEspecifico/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var db = await _context.DelitoEspecificos.FirstOrDefaultAsync(a => a.IdDelitoEspecifico == model.IdDelitoEspecifico);

            if (db == null)
            {
                return NotFound();
            }

            db.Nombre = model.Nombre;
            db.DelitoId = model.DelitoId;

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

        // POST: api/DelitoEspecifico/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DelitoEspecifico delitoEspecifico = new DelitoEspecifico
            {
                Nombre = model.Nombre,
                DelitoId = model.DelitoId
            };

            _context.DelitoEspecificos.Add(delitoEspecifico);
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
