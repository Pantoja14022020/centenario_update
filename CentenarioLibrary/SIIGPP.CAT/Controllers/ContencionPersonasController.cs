using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.ContencionPersonas;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.ContencionesPersonas;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContencionPersonasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public ContencionPersonasController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // POST: api/ContencionPersonas/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ContencionesPersona CP = new ContencionesPersona
            {
                RAtencionId = model.RAtencionId,
                QueRequirio = model.QueRequirio,
                NombrePersona = model.NombrePersona,
                FechaSys = System.DateTime.Now,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                UUsuario = model.UUsuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo

            };

            _context.ContencionesPersonas.Add(CP);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.2" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();
        }



        // GET: api/ContencionPersonas/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Recepción")]
        [HttpGet("[action]/{modulo}")]
        public async Task<IEnumerable<ContencionPersonasViewModel>> Listar([FromRoute] string modulo)
        {
            var CP = await _context.ContencionesPersonas
                .OrderByDescending(a => a.FechaSys)
                .Where(a => a.UModulo == modulo )
                .ToListAsync();

            return CP.Select(a => new ContencionPersonasViewModel
            {
                IdContencionesPersona = a.IdContencionesPersona,
                RAtencionId = a.RAtencionId,
                QueRequirio = a.QueRequirio,
                NombrePersona = a.NombrePersona,
                FechaSys = a.FechaSys,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                UUsuario = a.UUsuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo
            });

        }

        // GET: api/ContencionPersonas/ListarporFecha
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{modulo}/{fecha}")]
        public async Task<IEnumerable<ContencionPersonasViewModel>> ListarporFecha([FromRoute] string modulo, DateTime fecha)
        {
            var CP = await _context.ContencionesPersonas
                .OrderByDescending(a => a.FechaSys)
                .Where(a => a.FechaSys.Year == fecha.Year)
                .Where(a => a.FechaSys.Month == fecha.Month)
                .Where(a => a.FechaSys.Day == fecha.Day)
                .Where(a => a.UModulo == modulo)
                .ToListAsync();

            return CP.Select(a => new ContencionPersonasViewModel
            {
                IdContencionesPersona = a.IdContencionesPersona,
                RAtencionId = a.RAtencionId,
                QueRequirio = a.QueRequirio,
                NombrePersona = a.NombrePersona,
                FechaSys = a.FechaSys,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                UUsuario = a.UUsuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo
            });

        }


    }
}
