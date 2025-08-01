using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Oficios;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.Oficios;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OficiosController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;
        public OficiosController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // GET: api/Oficios/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<OficiosViewModel>> Listar([FromRoute]Guid RHechoId)
        {
            var per = await _context.Oficios
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.RHechoId == RHechoId)
                .ToListAsync();

            return per.Select(a => new OficiosViewModel
            {
                IdOficios = a.IdOficios,
                RHechoId = a.RHechoId,
                Texto = a.Texto,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                NumeroOficio = a.NumeroOficio,
                TipoOficio = a.TipoOficio
            });

        }

        // POST: api/Oficios/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            Guid idoficios;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Oficio Oficio = new Oficio
            {
                
                RHechoId = model.RHechoId,
                Texto = model.Texto,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
                NumeroOficio = model.NumeroOficio,
                TipoOficio = model.TipoOficio
            };

            _context.Oficios.Add(Oficio);
            
            try
            {
                await _context.SaveChangesAsync();
                idoficios = Oficio.IdOficios;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new {idoficios = idoficios});
        }


        // GET: api/Oficios/Eliminar
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
                var consultaOficio = await _context.Oficios.Where(a => a.IdOficios == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaOficio == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningun Oficio con la información enviada" });
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
                            MovimientoId = new Guid("99da0dc6-1a32-44b6-8eea-96882f55c65c") // ESTE DATO HAY QUE BUSCARLO EN LA TABLA C_TIPO_MOV DE LA BD CENTENARIO_LOG
                        };

                        ctx.Add(laRegistro);
                        LogOficio oficio = new LogOficio
                        {
                            LogAdmonId = gLog,
                            IdOficios = consultaOficio.IdOficios,
                            RHechoId = consultaOficio.RHechoId,
                            Texto = consultaOficio.Texto,
                            UDistrito = consultaOficio.UDistrito,
                            USubproc = consultaOficio.USubproc,
                            UAgencia = consultaOficio.UAgencia,
                            Usuario = consultaOficio.Usuario,
                            UPuesto = consultaOficio.UPuesto,
                            UModulo = consultaOficio.UModulo,
                            Fechasys = consultaOficio.Fechasys,
                            NumeroOficio = consultaOficio.NumeroOficio,
                            TipoOficio = consultaOficio.TipoOficio
                        };
                        ctx.Add(oficio);
                        _context.Remove(consultaOficio);

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
            return Ok(new { res = "success", men = "Oficio eliminado Correctamente" });
        }


        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/Oficios/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var oficiosBuscados = await _context.Oficios.Where(x => x.RHechoId == model.IdRHecho).ToListAsync();
            if (oficiosBuscados == null)
            {
                return Ok();
            }
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    foreach (Oficio oficioActual in oficiosBuscados)
                    {
                        var insertarOficio = await ctx.Oficios.FirstOrDefaultAsync(a => a.IdOficios == oficioActual.IdOficios);
                        if (insertarOficio == null)
                        {
                            insertarOficio = new Oficio();
                            ctx.Oficios.Add(insertarOficio);
                        }
                        insertarOficio.IdOficios = oficioActual.IdOficios;
                        insertarOficio.RHechoId = oficioActual.RHechoId;
                        insertarOficio.Texto = oficioActual.Texto;
                        insertarOficio.UDistrito = oficioActual.UDistrito;
                        insertarOficio.USubproc = oficioActual.USubproc;
                        insertarOficio.UAgencia = oficioActual.UAgencia;
                        insertarOficio.Usuario = oficioActual.Usuario;
                        insertarOficio.UPuesto = oficioActual.UPuesto;
                        insertarOficio.UModulo = oficioActual.UModulo;
                        insertarOficio.Fechasys = oficioActual.Fechasys;
                        insertarOficio.NumeroOficio = oficioActual.NumeroOficio;
                        insertarOficio.TipoOficio = oficioActual.TipoOficio;
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
