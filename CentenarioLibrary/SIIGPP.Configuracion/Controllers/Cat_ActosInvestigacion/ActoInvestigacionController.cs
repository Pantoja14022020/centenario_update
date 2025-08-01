using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_ActosInvestigacion;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_ActosInvestigacion;

namespace SIIGPP.Configuracion.Controllers.Cat_ActosInvestigacion
{

    [Route("api/[controller]")]
    [ApiController]
    public class ActoInvestigacionController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public ActoInvestigacionController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/ActoInvestigacion/ListarTodos
        [HttpGet("[action]")]
        public async Task<IEnumerable<ActoInvestigacionViewModel>> ListarTodos()
        {
            var ai = await _context.ActoInvestigacions.ToListAsync();

            return ai.Select(a => new ActoInvestigacionViewModel
            {
                IdActonvestigacion = a.IdActonvestigacion,
                Nombre = a.Nombre,
                Nomenclatura = a.Nomenclatura,
                Descripcion =a.Descripcion,
                RAutorizacion = a.RAutorizacion
            });

        }

        // GET: api/ActoInvestigacion/ListarRequiere
        [HttpGet("[action]")]
        public async Task<IEnumerable<ActoInvestigacionViewModel>> ListarRequiere()
        {
            var ai = await _context.ActoInvestigacions
                .Where(a=> a.RAutorizacion == true)
                .OrderBy(a => a.Nomenclatura)
                .ToListAsync();

            return ai.Select(a => new ActoInvestigacionViewModel
            {
                IdActonvestigacion = a.IdActonvestigacion,
                Nombre = a.Nombre,
                Nomenclatura = a.Nomenclatura,
                Descripcion = a.Descripcion,
                RAutorizacion = a.RAutorizacion
            });

        }

        // GET: api/ActoInvestigacion/ListarNoRequiere
        [HttpGet("[action]")]
        public async Task<IEnumerable<ActoInvestigacionViewModel>> ListarNoRequiere()
        {
            var ai = await _context.ActoInvestigacions
                .Where(a => a.RAutorizacion == false)
                .OrderBy(a => a.Nomenclatura)
                .ToListAsync();

            return ai.Select(a => new ActoInvestigacionViewModel
            {
                IdActonvestigacion = a.IdActonvestigacion,
                Nombre = a.Nombre,
                Nomenclatura = a.Nomenclatura,
                Descripcion = a.Descripcion,
                RAutorizacion = a.RAutorizacion
            });

        }


        // POST: api/ActoInvestigacion/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActoInvestigacion ai = new ActoInvestigacion
            {
                Nombre = model.Nombre,
                Nomenclatura = model.Nomenclatura,
                Descripcion = model.Descripcion,
                RAutorizacion = model.RAutorizacion
            };

            _context.ActoInvestigacions.Add(ai);
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


        // PUT: api/ActoInvestigacion/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

         

            var ai = await _context.ActoInvestigacions.FirstOrDefaultAsync(a => a.IdActonvestigacion == model.IdActonvestigacion);

            if (ai == null)
            {
                return NotFound();
            }

            ai.Nombre = model.Nombre;
            ai.Nomenclatura = model.Nomenclatura;
            ai.Descripcion = model.Descripcion;
            ai.RAutorizacion = model.RAutorizacion;

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
