using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Armas.ArmaObjeto;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Armas;

namespace SIIGPP.Configuracion.Controllers.Cat_Armas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmaObjetoController:ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public ArmaObjetoController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/ArmaObjeto/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArmaObjetoViewModel>> Listar()
        {
            var ao = await _context.ArmaObjetos
                .Include(a=> a.ClasificacionArma)
                .ToListAsync();

            return ao.Select(a => new ArmaObjetoViewModel
            {
                IdArmaObjeto = a.IdArmaObjeto,
                nombreAO=a.nombreAO,
                ClasificacionArmaId=a.ClasificacionArmaId,
                NombreClasi = a.ClasificacionArma.NombreC,
               
    });

        }


        // PUT: api/ArmaObjeto/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var ao = await _context.ArmaObjetos.FirstOrDefaultAsync(a => a.IdArmaObjeto == model.IdArmaObjeto);

            if (ao == null)
            {
                return NotFound();
            }

            ao.nombreAO = model.nombreAO;
            ao.ClasificacionArmaId = model.ClasificacionArmaId;
            
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


        // POST: api/ArmaObjeto/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ArmaObjeto ao = new ArmaObjeto
            {
                nombreAO = model.nombreAO,
                ClasificacionArmaId = model.ClasificacionArmaId,
                
            };

            _context.ArmaObjetos.Add(ao);
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

        // GET: api/ArmaObjeto/ListarClasi
        [HttpGet("[action]/{IdClasificacionArmaId}")]
        public async Task<IEnumerable<ArmaObjetoViewModel>> ListarClasi([FromRoute] Guid IdClasificacionArmaId)
        {
            var ao = await _context.ArmaObjetos
                .Where(a=>a.ClasificacionArmaId == IdClasificacionArmaId)
                .Include(a => a.ClasificacionArma)
                .ToListAsync();

            return ao.Select(a => new ArmaObjetoViewModel
            {
                IdArmaObjeto = a.IdArmaObjeto,
                nombreAO = a.nombreAO,
                ClasificacionArmaId = a.ClasificacionArmaId,
                NombreClasi = a.ClasificacionArma.NombreC
            });

        }
    }
}
