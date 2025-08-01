using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_INEGI.Estado;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public EstadoesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Estadoes/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var estado = await _context.Estadoes.ToListAsync();

                return Ok(estado.Select(a => new EstadoViewModel
                {
                    IdEstado = a.IdEstado,
                    Nombre = a.Nombre,
                    Abreviacion = a.Abreviacion
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, ExtraInfo = ex.Message });
                result.StatusCode = 402;
                return result;
            }
        }
        // GET: api/Estadoes/Mostrar
        [HttpGet("[action]")]
        public async Task<IActionResult> Mostrar ()
        {
            try
            {
                var estado = await _context.Estadoes.ToListAsync();

                return Ok(estado.Select(a => new EstadoViewModel
                {
                    IdEstado = a.IdEstado,
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


        // PUT: api/Estadoes/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var estado = await _context.Estadoes.FirstOrDefaultAsync(a => a.IdEstado == model.IdEstado);

            if (estado == null)
            {
                return NotFound();
            }

            estado.Nombre = model.Nombre;
            estado.Abreviacion = model.Abreviacion;

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

        // POST: api/Estadoes/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Estado estado = new Estado
            {
                Nombre = model.Nombre,
                Abreviacion = model.Abreviacion
            };

            _context.Estadoes.Add(estado);
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

        private bool EstadoExists(int IdEstado)
        {
            return _context.Estadoes.Any(a => a.IdEstado == IdEstado);
        }

    }
}