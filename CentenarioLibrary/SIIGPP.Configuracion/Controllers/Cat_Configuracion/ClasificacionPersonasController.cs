using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Configuracion.ClasificacionPersona;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Configuracion;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasificacionPersonasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public ClasificacionPersonasController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/ClasificacionPersonas
        [HttpGet]
        public IEnumerable<ClasificacionPersona> GetClasificacionPersonas()
        {
            return _context.ClasificacionPersonas;
        }
        // Tienes que crear un componente donde generes un documento, eso te lo explico en voz, en general es crear un modulo para generar un documento y guardarlo en la bd, te toco el ese 
        //documento de uan hoja creo, yo me chute el de 4, solo es de explicarte  como va la estructura
        //va y me dices igual donde vergas va no? , se por eso lo quiero terminar todo para explicarte toda la estructura de como funciona vale we me dices we para que me conecte y me expliques todo va para empezar ese desmdre, semon esta rrapido orale a shingar a su madre :v .l.

        // GET: api/ClasificacionPersonas/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {

            try
            {
                var cp = await _context.ClasificacionPersonas
                                    .OrderBy(a => a.Nombre)
                                    .ToListAsync();

                return Ok
                    (cp.Select(a => new ClasificaionPersonaViewModel
                    {
                        IdClasificacionPersona = a.IdClasificacionPersona,
                        Nombre = a.Nombre
                    }));

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message,ExtraInfo = ex.Message });
                result.StatusCode = 402;
                return result;
            }


        }


        // PUT: api/ClasificacionPersonas/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var tp = await _context.ClasificacionPersonas.FirstOrDefaultAsync(a => a.IdClasificacionPersona == model.IdClasificacionPersona);

            if (tp == null)
            {
                return NotFound();
            }

            tp.Nombre = model.Nombre;

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

        // POST: api/TipoPersonas/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClasificacionPersona tp = new ClasificacionPersona
            {
                Nombre = model.Nombre
            };

            _context.ClasificacionPersonas.Add(tp);
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
        private bool ClasificacionPersonaExists(Guid id)
        {
            return _context.ClasificacionPersonas.Any(e => e.IdClasificacionPersona == id);
        }
    }
}