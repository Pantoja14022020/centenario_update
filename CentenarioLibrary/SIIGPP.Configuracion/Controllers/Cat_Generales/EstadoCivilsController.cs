using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Catalogos.EstadoCivil;
using SIIGPP.Datos; 
using SIIGPP.Entidades.M_Configuracion.Cat_Generales;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoCivilsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public EstadoCivilsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/EstadoCivils/Listar 
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {

            try
            {
                var estadocivil = await _context.EstadoCivils
                                                .OrderBy(a => a.Nombre)
                                                .ToListAsync();

                return Ok(estadocivil.Select(a => new EstadoCivilViewModel

                {
                    IdECivil = a.IdECivil,
                    Nombre = a.Nombre
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, ExtraInfo = ex.Message });
                result.StatusCode = 402;
                return result;
            }

        }

        // GET: api/EstadoCivils/Mostrar/1
        [HttpGet("[action]/{id}")]
    public async Task<IActionResult> Mostrar([FromRoute] int id)
    {

        var estadocivil = await _context.EstadoCivils.FindAsync(id);

        if (estadocivil == null)
        {
            return NotFound();
        }

        return Ok(new EstadoCivilViewModel
        {
            IdECivil = estadocivil.IdECivil,
            Nombre = estadocivil.Nombre
        });
    }

        // PUT: api/EstadoCivils/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
    public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

       
        var estadocivil = await _context.EstadoCivils.FirstOrDefaultAsync(a => a.IdECivil == model.IdECivil);

        if (estadocivil == null)
        {
            return NotFound();
        }

        estadocivil.Nombre = model.Nombre;

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

        // POST: api/EstadoCivils/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
    public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        EstadoCivil estadocivil = new EstadoCivil
        {
            Nombre = model.Nombre
        };

        _context.EstadoCivils.Add(estadocivil);
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

    private bool EstadoCivilExists(Guid id)
    {
        return _context.EstadoCivils.Any(a => a.IdECivil == id);
    }

}
}