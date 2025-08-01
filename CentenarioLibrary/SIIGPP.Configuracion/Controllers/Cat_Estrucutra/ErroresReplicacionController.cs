using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Estrucutra.ErrorReplicacion;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Estrucutra;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErroresReplicacionController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public ErroresReplicacionController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/ErroresReplicacion/Listar
        [HttpGet("[action]/")]
        public async Task<ActionResult<ErrorReplicacionViewModel>> Listar()
        {
            try
            {
                var registros = await _context.Replicacion
                    .Where(a => a.Status == true)
                    .Select(a => new ErrorReplicacionViewModel
                    {
                        IdReplicacion = a.IdReplicacion,
                        RegistroErrorId = a.RegistroErrorId,
                        DistritoId = a.DistritoId,
                        NombreDistrito = a.NombreDistrito,
                        Modulo = a.Modulo,
                        Proceso = a.Proceso,
                        Status = a.Status,
                        FechaCreacion = a.FechaCreacion,
                    })
                    .OrderByDescending(a => a.FechaCreacion)
                    .ToListAsync();

                if (registros == null || !registros.Any())
                {
                    return NoContent();
                }

                return Ok(registros);
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // POST: api/ErroresReplicacion/RegistrarError
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegistrarError([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (model.ActualizaRegistro == false)
                {
                    bool registroExistente = await _context.Replicacion
                    .AnyAsync(r => r.RegistroErrorId == model.RegistroErrorId
                               && r.DistritoId == model.DistritoId
                               && r.Status == true);

                    if (registroExistente)
                    {
                        return new ObjectResult(new { mensaje = "Ya hay un registro del problema. Verifique la tabla de errores." })
                        {
                            StatusCode = 201
                        };
                    }

                    ErrorReplicacion nuevoRegistro = new ErrorReplicacion
                    {
                        RegistroErrorId = model.RegistroErrorId,
                        DistritoId = model.DistritoId,
                        NombreDistrito = model.NombreDistrito,
                        Modulo = model.Modulo,
                        Proceso = model.Proceso,
                        Status = model.Status,
                        FechaCreacion = DateTime.Now,
                        FechaActualizacion = DateTime.Now,
                    };

                    _context.Replicacion.Add(nuevoRegistro);
                    await _context.SaveChangesAsync();
                    
                    return Ok();
                }
                else
                {
                    var registrosExistentes = await _context.Replicacion
                    .Where(r => r.RegistroErrorId == model.RegistroErrorId &&
                                r.DistritoId == model.DistritoId &&
                                r.Status == true)
                    .FirstOrDefaultAsync();

                    if (registrosExistentes == null)
                    {
                        return NoContent();
                    }

                    registrosExistentes.Status = false;
                    registrosExistentes.FechaActualizacion = DateTime.Now;

                    await _context.SaveChangesAsync();

                    return Ok();
                }

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }
    }
}
