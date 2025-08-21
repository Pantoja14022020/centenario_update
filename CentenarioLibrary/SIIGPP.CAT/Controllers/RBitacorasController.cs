using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Bitacora;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.Bitacora;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBitacorasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public RBitacorasController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // genera lista de los registros de bitacora
        // GET: api/RBitacoras/Listar
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<BitacoraViewModel>> Listar()
        {
            var db = await _context.Bitacoras.ToListAsync();

            return db.Select(a => new BitacoraViewModel
            {
                IdBitacora = a.IdBitacora,
                rHechoId = a.rHechoId,
                IdPersona = a.IdPersona,
                Tipo = a.Tipo,
                Descipcion = a.Descipcion,
                Distrito = a.Distrito,
                Dirsubproc = a.Dirsubproc,
                Agencia = a.Agencia,
                Usuario = a.Usuario,
                Puesto = a.Puesto,
                Fechareporte = a.Fechareporte,
                Fechasis = a.Fechasis
            });

        }

        // GET: api/RBitacoras/ListarPorHecho
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IEnumerable<BitacoraViewModel>> ListarPorHecho([FromRoute] Guid rHechoId)
        {
            var db = await _context.Bitacoras
                           .Include(a => a.Persona)
                           .Include(a => a.Persona.RAPs)
                           .OrderByDescending(a => a.Fechasis)
                           .Where(a => a.rHechoId == rHechoId).ToListAsync();

            return db.Select(a => new BitacoraViewModel
            {
                IdBitacora = a.IdBitacora,
                rHechoId = a.rHechoId,
                IdPersona = a.IdPersona,
                NombrePersona = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Tipo = a.Tipo,
                Descipcion = a.Descipcion,
                Distrito = a.Distrito,
                Dirsubproc = a.Dirsubproc,
                Agencia = a.Agencia,
                Usuario = a.Usuario,
                Puesto = a.Puesto,
                Fechareporte = a.Fechareporte,
                Fechasis = a.Fechasis,
                Numerooficio = a.Numerooficio
            });
        }

        // PUT: api/RBitacoras/Actualizar
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var db = await _context.Bitacoras.FirstOrDefaultAsync(a => a.IdBitacora == model.IdBitacora);

            if (db == null)
            {
                return NotFound();
            }

            db.rHechoId = model.rHechoId;
            db.IdPersona = model.IdPersona;
            db.Tipo = model.Tipo;
            db.Descipcion = model.Descipcion;
            db.Distrito = model.Distrito;
            db.Dirsubproc = model.Dirsubproc;
            db.Agencia = model.Agencia;
            db.Usuario = model.Usuario;
            db.Puesto = model.Puesto;
            db.Fechareporte = model.Fechareporte;

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

        // POST: api/RBitacoras/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            Guid idrbitacora;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DateTime fecha = System.DateTime.Now;
            RBitacora dato = new RBitacora
            {
                rHechoId = model.rHechoId,
                IdPersona = model.IdPersona,
                Tipo = model.Tipo,
                Descipcion = model.Descipcion,
                Distrito = model.Distrito,
                Dirsubproc = model.Dirsubproc,
                Agencia = model.Agencia,
                Usuario = model.Usuario,
                Puesto = model.Puesto,
                Fechareporte = model.Fechareporte,
                Fechasis = System.DateTime.Now,
                Numerooficio = model.Numerooficio
            };

            _context.Bitacoras.Add(dato);
            try
            {
                await _context.SaveChangesAsync();
                idrbitacora = dato.IdBitacora;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
            return Ok(new { idrbitacora = idrbitacora});
        }

        private bool RBitacoraExists(Guid id)
        {
            return _context.Bitacoras.Any(e => e.IdBitacora == id);
        }

        // GET: api/RBitacoras/Eliminar
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var consultaBitacora = await _context.Bitacoras.Where(a => a.IdBitacora == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaBitacora == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ninguna bitacora con la información enviada" });
                }
                else
                {
                    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("LOG")).Options;
                    using (var ctx = new DbContextSIIGPP(options))
                    {
                        Guid gLog = Guid.NewGuid();
                        DateTime fecha = System.DateTime.Now;
                        LogAdmon laRegistro = new LogAdmon
                        {
                            IdLogAdmon = gLog,
                            UsuarioId = model.usuario,
                            Tabla = model.tabla,
                            FechaMov = fecha,
                            RegistroId = model.infoBorrado.registroId,
                            SolicitanteId = model.solicitante,
                            RazonMov = model.razon,
                            MovimientoId = new Guid("e11cb5fb-7679-4aa2-8b52-c64e7fd57999") // ESTE DATO HAY QUE BUSCARLO EN LA TABLA C_TIPO_MOV DE LA BD CENTENARIO_LOG
                        };

                        ctx.Add(laRegistro);
                        LogBitacora bitacora = new LogBitacora
                        {
                            LogAdmonId = gLog,
                            Tipo = consultaBitacora.Tipo,
                            IdBitacora = consultaBitacora.IdBitacora,
                            Descipcion = consultaBitacora.Descipcion,
                            Distrito = consultaBitacora.Distrito,
                            Dirsubproc = consultaBitacora.Dirsubproc,
                            Agencia = consultaBitacora.Agencia,
                            Usuario = consultaBitacora.Usuario,
                            Puesto = consultaBitacora.Puesto,
                            Fechareporte = consultaBitacora.Fechareporte,
                            Fechasis = consultaBitacora.Fechasis,
                            rHechoId = consultaBitacora.rHechoId,
                            IdPersona = consultaBitacora.IdPersona,
                            Numerooficio = consultaBitacora.Numerooficio
                        };
                        ctx.Add(bitacora);
                        _context.Remove(consultaBitacora);

                        //SE APLICAN TODOS LOS CAMBIOS A LA BD
                        await ctx.SaveChangesAsync();
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "Diligencia eliminada Correctamente" });
        }

        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/RBitacoras/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bitacorasBuscadas = await _context.Bitacoras.Where(x => x.rHechoId == model.IdRHecho).ToListAsync();
            if (bitacorasBuscadas == null)
            {
                return Ok();
            }
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    foreach (RBitacora bitacoraActual in bitacorasBuscadas)
                    {
                        var insertarBitacora = await ctx.Bitacoras.FirstOrDefaultAsync(a => a.IdBitacora == bitacoraActual.IdBitacora);
                        if (insertarBitacora == null)
                        {
                            insertarBitacora = new RBitacora();
                            ctx.Bitacoras.Add(insertarBitacora);
                        }
                        insertarBitacora.Tipo = bitacoraActual.Tipo;
                        insertarBitacora.Descipcion = bitacoraActual.Descipcion;
                        insertarBitacora.Distrito = bitacoraActual.Distrito;
                        insertarBitacora.Dirsubproc = bitacoraActual.Dirsubproc;
                        insertarBitacora.Agencia = bitacoraActual.Agencia;
                        insertarBitacora.Usuario = bitacoraActual.Usuario;
                        insertarBitacora.Puesto = bitacoraActual.Puesto;
                        insertarBitacora.Fechareporte = bitacoraActual.Fechareporte;
                        insertarBitacora.Fechasis = bitacoraActual.Fechasis;
                        insertarBitacora.rHechoId = bitacoraActual.rHechoId;
                        insertarBitacora.IdPersona = bitacoraActual.IdPersona;
                        insertarBitacora.Numerooficio = bitacoraActual.Numerooficio;
                        insertarBitacora.IdBitacora = bitacoraActual.IdBitacora;
                        await ctx.SaveChangesAsync();
                    }
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }
        }
    }
}