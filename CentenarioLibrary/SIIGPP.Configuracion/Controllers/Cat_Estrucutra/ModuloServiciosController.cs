using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Configuracion.Models.Cat_Estrucutra.ModuloServicio;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuloServiciosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IConfiguration _configuration;

        public ModuloServiciosController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/ModuloServicios 
        [HttpGet]
        public IEnumerable<ModuloServicio> GetModuloServicio()
        {
            return _context.ModuloServicios;
        }

        // GET: api/ModuloServicios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ModuloServicioViewModel>> Listar()
        {
            var moduloservicio = await _context.ModuloServicios
                                        .Include(a => a.Agencia)
                                        .Include(a => a.Agencia.DSP)
                                        .Include(a => a.Agencia.DSP.Distrito)
                                        .ToListAsync();

            return moduloservicio.Select(a => new ModuloServicioViewModel
            {
                IdModuloServicio = a.IdModuloServicio,
                DistritoId = a.Agencia.DSP.DistritoId,
                DSPId = a.Agencia.DSPId,
                AgenciaId = a.AgenciaId,
                Agencia = a.Agencia.Nombre,
                Tipo = a.Tipo,
                Nombre = a.Nombre,
                Clave = a.Clave,
                ServicioInterno = a.ServicioInterno,
                Condicion = a.Condicion
            });

        }

        // GET: api/ModuloServicios/ListarPorAgencia
        [Authorize(Roles = "Administrador, Director ,Oficialia de partes,Coordinador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{agenciaId}")]
        public async Task<IEnumerable<ModuloServicioViewModel>> ListarPorAgencia([FromRoute] Guid agenciaId)
        {   
            var moduloservicio = await _context.ModuloServicios
                                .Where(a => a.AgenciaId == agenciaId)
                                .Where(a => a.Condicion == true)
                                .OrderBy(a => a.Nombre)
                                .Include(a => a.Agencia).ToListAsync();

            return moduloservicio.Select(a => new ModuloServicioViewModel
            {
                IdModuloServicio = a.IdModuloServicio,
                AgenciaId = a.AgenciaId,
                Agencia = a.Agencia.Nombre,
                Tipo = a.Tipo,
                Nombre = a.Nombre,
                Clave = a.Clave,
                ServicioInterno = a.ServicioInterno
            });

        }
        // GET: api/ModuloServicios/ListarPorAgencia
        [Authorize(Roles = "Administrador, Director ,Oficialia de partes,Coordinador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{agenciaId}/{claveAgencia}")]
        public async Task<IEnumerable<ModuloServicioViewModel>> ListarPorAgenciaSinCAT([FromRoute] Guid agenciaId, string claveAgencia)
        {

            if(claveAgencia == "CAT")
            {
                var moduloservicio = await _context.ModuloServicios
                                .Where(a => a.AgenciaId == agenciaId)
                                .Where(a => a.Condicion == true)
                                .Where(a => a.Tipo != "Recepción")
                                .OrderBy(a => a.Nombre)
                                .Include(a => a.Agencia).ToListAsync();

                return moduloservicio.Select(a => new ModuloServicioViewModel
                {
                    IdModuloServicio = a.IdModuloServicio,
                    AgenciaId = a.AgenciaId,
                    Agencia = a.Agencia.Nombre,
                    Tipo = a.Tipo,
                    Nombre = a.Nombre,
                    Clave = a.Clave,
                    ServicioInterno = a.ServicioInterno
                });
            }
            else
            {
                var moduloservicio = await _context.ModuloServicios
                                .Where(a => a.AgenciaId == agenciaId)
                                .Where(a => a.Condicion == true)
                                .OrderBy(a => a.Nombre)
                                .Include(a => a.Agencia).ToListAsync();

                return moduloservicio.Select(a => new ModuloServicioViewModel
                {
                    IdModuloServicio = a.IdModuloServicio,
                    AgenciaId = a.AgenciaId,
                    Agencia = a.Agencia.Nombre,
                    Tipo = a.Tipo,
                    Nombre = a.Nombre,
                    Clave = a.Clave,
                    ServicioInterno = a.ServicioInterno
                });
            }


        }


        // GET: api/ModuloServicios/ListarPorAgencia/captura
        [Authorize(Roles = "Administrador, Director ,Oficialia de partes,Coordinador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{agenciaId}")]
        public async Task<IEnumerable<ModuloServicioViewModel>> ListarPorAgenciaCap([FromRoute] Guid agenciaId)
        {
            var moduloservicio = await _context.ModuloServicios
                                .Where(a => a.AgenciaId == agenciaId)
                                .OrderBy(a => a.Nombre)
                                .Include(a => a.Agencia).ToListAsync();

            return moduloservicio.Select(a => new ModuloServicioViewModel
            {
                IdModuloServicio = a.IdModuloServicio,
                AgenciaId = a.AgenciaId,
                Agencia = a.Agencia.Nombre,
                Tipo = a.Tipo,
                Nombre = a.Nombre,
                Clave = a.Clave,
                ServicioInterno = a.ServicioInterno
            });

        }



        // PUT: api/ModuloServicios/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var moduloservicio = await _context.ModuloServicios.FirstOrDefaultAsync(a => a.IdModuloServicio == model.IdModuloServicio);

                if (moduloservicio == null)
                {
                    return NotFound();
                }
                moduloservicio.AgenciaId = model.AgenciaId;
                moduloservicio.Tipo = model.Tipo;
                moduloservicio.Nombre = model.Nombre;
                moduloservicio.Clave = model.Clave;
                moduloservicio.ServicioInterno = model.ServicioInterno;

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
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // POST: api/ModuloServicios/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ModuloServicio ms = new ModuloServicio
                {
                    AgenciaId = model.AgenciaId,
                    Tipo = model.Tipo,
                    Nombre = model.Nombre,
                    Clave = model.Clave,
                    ServicioInterno = model.ServicioInterno,
                    Condicion = true


                };

                _context.ModuloServicios.Add(ms);
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

                return Ok(new { id = ms.IdModuloServicio });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // POST: api/ModuloServicios/Replicar
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Replicar([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoId.ToString().ToUpper())).Options;

                using (var ctx = new DbContextSIIGPP(options))
                {
                    var moduloservicio = await ctx.ModuloServicios.FirstOrDefaultAsync(a => a.IdModuloServicio == model.IdModuloServicio);

                    if (moduloservicio == null)
                    {
                        moduloservicio = new ModuloServicio();
                        ctx.ModuloServicios.Add(moduloservicio);
                    }

                    moduloservicio.IdModuloServicio = model.IdModuloServicio;
                    moduloservicio.AgenciaId = model.AgenciaId;
                    moduloservicio.Tipo = model.Tipo;
                    moduloservicio.Nombre = model.Nombre;
                    moduloservicio.Clave = model.Clave;
                    moduloservicio.ServicioInterno = model.ServicioInterno;
                    moduloservicio.Condicion = model.Condicion;

                    await ctx.SaveChangesAsync();

                }
                return Ok();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/ModuloServicios/Listarporid
        [HttpGet("[action]/{IdModulo}")]
        public async Task<IActionResult> Listarporid([FromRoute] Guid IdModulo)
        {
            var moduloservicio = await _context.ModuloServicios
                                .Where(a => a.IdModuloServicio == IdModulo)
                                .Include(a => a.Agencia)
                                .Include(a => a.Agencia.DSP.Distrito)
                                .FirstOrDefaultAsync();


            if (moduloservicio == null)
            {
                return NotFound("No hay registros");

            }
            try
            {
                return Ok(new ModuloServicioErrorRepliViewModel
                {
                    IdModuloServicio = moduloservicio.IdModuloServicio,
                    AgenciaId = moduloservicio.AgenciaId,
                    Tipo = moduloservicio.Tipo,
                    Nombre = moduloservicio.Nombre,
                    Clave = moduloservicio.Clave,
                    ServicioInterno = moduloservicio.ServicioInterno,
                    Condicion = moduloservicio.Condicion,
                    Agencia = moduloservicio.Agencia.Nombre,
                    NombreDSP = moduloservicio.Agencia.DSP.NombreSubDir,
                    NombreDistrito = moduloservicio.Agencia.DSP.Distrito.Nombre,
                });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/ModuloServicios/agenciaId
        [HttpGet("[action]{agenciaId}")]
        public async Task<IActionResult> agenciaId([FromRoute] Guid agenciaId)
        {
            var ms = await _context.ModuloServicios.Where(a => a.AgenciaId == agenciaId)
                                                        .FirstOrDefaultAsync();

            if (ms == null)
            {
                return BadRequest("No hay registros");

            }
            return Ok(new ModuloServicioViewModel
            {
                IdModuloServicio = ms.IdModuloServicio
            });

        }

        // GET: api/ModuloServicios/Listarmodulosporagencia
        [HttpGet("[action]/{idagencia}")]
        [Authorize(Roles = "Administrador, Director ,Oficialia de partes,Coordinador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,AMPO-IL")]
        public async Task<IEnumerable<ModuloServicioViewModel>> Listarmodulosporagencia([FromRoute] Guid idagencia)
        {
            var modulo = await _context.ModuloServicios
                .Where(a => a.AgenciaId == idagencia)
                .ToListAsync();

            return modulo.Select(u => new ModuloServicioViewModel
            {
                IdModuloServicio = u.IdModuloServicio,
                AgenciaId = u.AgenciaId,
                Tipo = u.Tipo,
                Nombre = u.Nombre,
                Clave = u.Clave,
                ServicioInterno = u.ServicioInterno
                

            });

        }

        // GET: api/ModuloServicios/ListarPorDistrito
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,Recepción,Oficialia de partes,Comandante General,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido Mixto,Coordinador-Jurídico,Coordinador,Jurídico,Director,Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,")]
        [HttpGet("[action]/{distritoid}")]
        public async Task<IEnumerable<ModuloServicioViewModel>> ListarPorDistrito([FromRoute] Guid distritoid)
        {
            var moduloservicio = await _context.ModuloServicios
                                .Include(a => a.Agencia.DSP)
                                .Where(a => a.Agencia.DSP.DistritoId == distritoid)
                                .Where(a => a.Agencia.DSP.NombreSubDir == "Policia Investigadora")
                                .Include(a => a.Agencia).ToListAsync();

            return moduloservicio.Select(a => new ModuloServicioViewModel
            {
                IdModuloServicio = a.IdModuloServicio,
                AgenciaId = a.AgenciaId,
                Agencia = a.Agencia.Nombre,
                Tipo = a.Tipo,
                Nombre = a.Nombre,
                Clave = a.Clave,
                ServicioInterno = a.ServicioInterno
            });

        }

        // GET: api/ModuloServicios/ListarPorDistritoServicioIn
        [Authorize(Roles = " Administrador,Comandante Unidad,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,Recepción,Oficialia de partes,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido Mixto,Coordinador-Jurídico,Coordinador,Jurídico,Director,Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,Comandante Unidad")]
        [HttpGet("[action]/{iddspusuario}")]
        public async Task<IEnumerable<ModuloServicioViewModel>> ListarPorDistritoServicioIn([FromRoute] Guid iddspusuario)
        {
            var moduloservicio = await _context.ModuloServicios
                                .Include(a => a.Agencia.DSP)
                                .Where(a => a.Agencia.DSPId == iddspusuario)
                                .Where(a => a.ServicioInterno )
                                .Include(a => a.Agencia).ToListAsync();

            return moduloservicio.Select(a => new ModuloServicioViewModel
            {
                IdModuloServicio = a.IdModuloServicio,
                AgenciaId = a.AgenciaId,
                Agencia = a.Agencia.Nombre,
                Tipo = a.Tipo,
                Nombre = a.Nombre,
                Clave = a.Clave,
                ServicioInterno = a.ServicioInterno
            });

        }

        // GET: api/ModuloServicios/Listarpordspid
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,Recepción,Oficialia de partes,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido Mixto,Coordinador-Jurídico,Coordinador,Jurídico,Director,Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,")]
        [HttpGet("[action]/{dspid}")]
        public async Task<IEnumerable<ModuloServicioViewModel>> Listarpordspid([FromRoute] Guid dspid)
        {
            var moduloservicio = await _context.ModuloServicios
                                .Include(a => a.Agencia.DSP)
                                .Where(a => a.Agencia.DSPId == dspid)
                                .Include(a => a.Agencia).ToListAsync();

            return moduloservicio.Select(a => new ModuloServicioViewModel
            {
                IdModuloServicio = a.IdModuloServicio,
                AgenciaId = a.AgenciaId,
                Agencia = a.Agencia.Nombre,
                Tipo = a.Tipo,
                Nombre = a.Nombre,
                Clave = a.Clave,
                ServicioInterno = a.ServicioInterno
            });

        }

        // GET: api/ModuloServicios/ListarPorDistritoInternoSP
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,Recepción,Oficialia de partes,Comandante General,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido Mixto,Coordinador-Jurídico,Coordinador,Jurídico,Director,Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,")]
        [HttpGet("[action]/{distritoid}/{subprocid}")]
        public async Task<IEnumerable<ModuloServicioViewModel>> ListarPorDistritoInternoSP([FromRoute] Guid distritoid, Guid subprocid)
        {
            var moduloservicio = await _context.ModuloServicios
                                .Include(a => a.Agencia.DSP)
                                .Where(a => a.Agencia.DSP.DistritoId == distritoid)
                                .Where(a => a.Agencia.DSP.IdDSP == subprocid)
                                .Where(a => a.ServicioInterno == true)
                                .Include(a => a.Agencia).ToListAsync();

            return moduloservicio.Select(a => new ModuloServicioViewModel
            {
                IdModuloServicio = a.IdModuloServicio,
                AgenciaId = a.AgenciaId,
                Agencia = a.Agencia.Nombre,
                Tipo = a.Tipo,
                Nombre = a.Nombre,
                Clave = a.Clave,
                ServicioInterno = a.ServicioInterno
            });

        }


        // GET: api/ModuloServicios/RecepcionCarpeta
        [HttpGet("[action]/{agenciaId}")]
        public async Task<IActionResult> RecepcionCarpeta([FromRoute] Guid agenciaId)
        {
            var ms = await _context.ModuloServicios
                .Where(a => a.AgenciaId == agenciaId)
                .Where(a => a.Tipo == "Recepción")
                .Where(a => a.Condicion == true)
                .FirstOrDefaultAsync();

            if (ms == null)
            {
                return Ok(new { IdModuloServicio = "" });

            }
            return Ok(new ModuloServicioViewModel
            {
                IdModuloServicio = ms.IdModuloServicio,
                Nombre = ms.Nombre
            });

        }

        // GET: api/ModuloServicios/ListarPorDistritoidUsario
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,Recepción,Oficialia de partes,Comandante General,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido Mixto,Coordinador-Jurídico,Coordinador,Jurídico,Director,Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,Comandante Unidad")]
        [HttpGet("[action]/{iddspusuario}")]
        public async Task<IEnumerable<ModuloServicioViewModel>> ListarPorDistritoidUsario([FromRoute] Guid iddspusuario)
        {
            var moduloservicio = await _context.ModuloServicios
                                .Include(a => a.Agencia.DSP)
                                .Where(a => a.Agencia.DSPId == iddspusuario)
                                .Include(a => a.Agencia).ToListAsync();

            return moduloservicio.Select(a => new ModuloServicioViewModel
            {
                IdModuloServicio = a.IdModuloServicio,
                AgenciaId = a.AgenciaId,
                Agencia = a.Agencia.Nombre,
                Tipo = a.Tipo,
                Nombre = a.Nombre,
                Clave = a.Clave,
                ServicioInterno = a.ServicioInterno
            });

        }


        private bool ModuloServicioExists(Guid id)
        {
            return _context.ModuloServicios.Any(e => e.IdModuloServicio == id);
        }

        // PUT: api/ModuloServcio/Desactivar
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{idModulo}")]
        public async Task<IActionResult> DesactivarModulo([FromRoute] Guid idModulo)
        {


            var modulo = await _context.ModuloServicios.FirstOrDefaultAsync(m => m.IdModuloServicio == idModulo);

            if (modulo == null)
            {
                return NotFound();
            }

            modulo.Condicion = false;

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

        // PUT: api/ModuloServicio/Activar
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{idModulo}")]
        public async Task<IActionResult> ActivarModulo([FromRoute] Guid idModulo)
        {

            var modulo = await _context.ModuloServicios.FirstOrDefaultAsync(m => m.IdModuloServicio == idModulo);

            if (modulo == null)
            {
                return NotFound();
            }

            modulo.Condicion = true;

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