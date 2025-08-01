using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Configuracion.Models.Catalogos.Distrito;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_IL.Agendas;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistritoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IConfiguration _configuration;

        public DistritoesController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Distritoes/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<DistritoViewModel>> Listar()
        {
            var distrito = await _context.Distritos
                .OrderBy(a=> a.Nombre)                
                .ToListAsync();

            return distrito.Select(a => new DistritoViewModel
            {
                IdDistrito= a.IdDistrito,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Contacto = a.Contacto,
                Clave = a.Clave,
                StatusAsginacion = a.StatusAsginacion,
                Nombrejr = a.Nombrejr,
                UrlDistrito =a.UrlDistrito,
                
            });

        }


        // GET: api/Distritoes/Select
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Oficialia de partes,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto Mixto,Coordinador-Jurídico,Coordinador-Jurídico,Director-Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var distrito = await _context.Distritos.ToListAsync();

            return distrito.Select(a => new SelectViewModel
            {
                IdDistrito = a.IdDistrito,
                Nombre = a.Nombre
            });


        }
        // GET: api/Distritoes/ListarSede
        //distrito asignado
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{idDistrito}")]
        public async Task<IActionResult> ListarSede([FromRoute] Guid idDistrito)
        {
            var Tabla = await _context.Distritos
                                .Where(a => a.IdDistrito == idDistrito)
                                .FirstOrDefaultAsync();

            if (Tabla == null)
            {

                return Ok(new { ner = 1 });
            }
            return Ok(new SelectSedeViewModel
            {
                IdDistrito = Tabla.IdDistrito,
                Sede = Tabla.Nombrejr

            });
        }
        // GET: api/Distritoes/StatusAsignado
        //distrito asignado
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]")]
        public async Task<IActionResult> StatusAsignado()
        {
            var Tabla = await _context.Distritos  
                                .Where(a => a.StatusAsginacion == true)
                                .FirstOrDefaultAsync();

            if (Tabla == null)
            {

                return Ok(new { ner = 1 });
            }
            return Ok(new SelectViewModel
            {
                IdDistrito = Tabla.IdDistrito,
                Nombre = Tabla.Nombre
              
            });
        }

        // PUT: api/Distritoes/ClonarDistritos
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> ClonarDistritos([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoCnx.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var distrito = await ctx.Distritos.FirstOrDefaultAsync(a => a.IdDistrito == model.IdDistrito);

                    if (distrito == null)
                    {
                        distrito = new Distrito();
                        ctx.Distritos.Add(distrito);
                    }

                        distrito.IdDistrito = model.IdDistrito;
                        distrito.Nombre = model.Nombre;
                        distrito.Clave = model.Clave;
                        distrito.Direccion = model.Direccion;
                        distrito.Telefono = model.Telefono;
                        distrito.Contacto = model.Contacto;
                        distrito.StatusAsginacion = model.StatusAsginacion;
                        distrito.Nombrejr = model.Nombrejr;
                        distrito.UrlDistrito = model.UrlDistrito;

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

        // PUT: api/Distritoes/ActualizarPachuca
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarPachuca([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                var distrito = await _context.Distritos.FirstOrDefaultAsync(a => a.IdDistrito == model.IdDistrito);

                if (distrito == null)
                {
                    return NotFound();
                }

                distrito.Nombre = model.Nombre;
                distrito.Clave = model.Clave;
                distrito.Direccion = model.Direccion;
                distrito.Telefono = model.Telefono;
                distrito.Contacto = model.Contacto;
                distrito.StatusAsginacion = model.StatusAsginacion;
                distrito.Nombrejr = model.Nombrejr;
            distrito.UrlDistrito = model.UrlDistrito;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Guardar Excepción
                    return BadRequest();
                }

                return Ok(new {id = model.IdDistrito});
        }

        // POST: api/Distritoes/CrearPachuca
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearPachuca([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                Distrito distrito = new Distrito
                {
                    Nombre = model.Nombre,
                    Direccion = model.Direccion,
                    Telefono = model.Telefono,
                    Contacto = model.Contacto,
                    StatusAsginacion = model.StatusAsginacion,
                    Clave = model.Clave,
                    Nombrejr = model.Nombrejr,
                    UrlDistrito = model.UrlDistrito,



                };

                _context.Distritos.Add(distrito);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                    result.StatusCode = 402;
                    return result;
                }

            return Ok(new { id = distrito.IdDistrito });

        }

        // GET: api/Distritoes/ListarSinPropiodistrito
        [HttpGet("[action]/{iddistrito}")]
        public async Task<IEnumerable<DistritoViewModel>> ListarSinPropiodistrito([FromRoute] Guid iddistrito)
        {
            var distrito = await _context.Distritos
                .Where(a=> a.IdDistrito != iddistrito)
                .OrderBy(a => a.Nombre)
                .ToListAsync();

            return distrito.Select(a => new DistritoViewModel
            {
                IdDistrito = a.IdDistrito,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Contacto = a.Contacto,
                Clave = a.Clave,
                StatusAsginacion = a.StatusAsginacion,
            });

        }

        private bool DistritoExists(Guid id)
        {
            return _context.Distritos.Any(a => a.IdDistrito == id);
        }

        // GET: api/Distritoes/ListarCvedistrito
        [HttpGet("[action]")]
        public async Task<IEnumerable<cveDistritoViewModel>> ListarCvedistrito()
        {
            var distrito = await _context.Distritos
                .OrderBy(a => a.Nombre)
                .ToListAsync();

            return distrito.Select(a => new cveDistritoViewModel
            {
                IdDistrito = a.IdDistrito,
                Nombre = a.Nombre,
                Clave = a.Clave,
            });

        }


        }
    }