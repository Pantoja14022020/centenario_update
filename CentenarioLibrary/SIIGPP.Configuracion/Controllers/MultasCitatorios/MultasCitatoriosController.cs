using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_MultaCitatorios;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_MultasCitatorios;

namespace SIIGPP.Configuracion.Controllers.MultasCitatorios
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultasCitatoriosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public MultasCitatoriosController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/MultasCitatorios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<MultaCitatoriosViewModel>> Listar()
        {
            var ao = await _context.MultaCitatorios
                .ToListAsync();

            return ao.Select(a => new MultaCitatoriosViewModel
            {
                  IdMultaCitatorio = a.IdMultaCitatorio,
                  Dato =a.Dato,
                  Descripcion =a.Descripcion,

    });

        }


        // POST: api/MultasCitatorios/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MultaCitatorios ag = new MultaCitatorios
            {

                Dato = model.Dato,
                Descripcion = model.Descripcion,

            };

            _context.MultaCitatorios.Add(ag);
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

        // PUT: api/MultasCitatorios/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.MultaCitatorios.FirstOrDefaultAsync(a => a.IdMultaCitatorio == model.IdMultaCitatorio);

            if (ao == null)
            {
                return NotFound();
            }

            ao.Dato = model.Dato;
            ao.Descripcion = model.Descripcion;
            

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
