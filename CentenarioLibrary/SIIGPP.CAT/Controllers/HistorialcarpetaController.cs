using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Historialcarpetas;
using SIIGPP.CAT.Models.Historialcarpetas;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialcarpetaController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public HistorialcarpetaController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // GET: api/Historialcarpeta/Listar
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<HistorialCarpetaViewModel>> Listar([FromRoute] Guid RHechoId)
        {
            var hi = await _context.HistorialCarpetas
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return hi.Select(a => new HistorialCarpetaViewModel
            {
              IdHistorialcarpetas = a.IdHistorialcarpetas,
              RHechoId = a.RHechoId,
              Detalle =  a.DetalleEtapa + " - " + a.Detalle ,
              DetalleEtapa = a.DetalleEtapa,
              Modulo =a.Modulo,
              Agencia = a.Agencia,
              UDistrito = a.UDistrito,
              USubproc = a.USubproc,
              UAgencia = a.UAgencia,
              Usuario  = a.Usuario,
              UPuesto  =a.UPuesto,
              UModulo  = a.UModulo,
              Fechasys =a.Fechasys,

         });

        }


        // POST: api/Historialcarpeta/Crear
        //[Authorize(Roles = "Administrador,Director,AMPO-AMP,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto,Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            HistorialCarpeta hi = new HistorialCarpeta
            {
                RHechoId = model.RHechoId,
                Detalle = model.Detalle,
                DetalleEtapa = model.DetalleEtapa,
                Modulo = model.Modulo,
                Agencia = model.Agencia,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,

            };

            _context.HistorialCarpetas.Add(hi);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();
        }



        // POST: api/Historialcarpeta/CrearModuloCaptura
        //[Authorize(Roles = "Administrador,Director,AMPO-AMP,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto,Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearModuloCaptura([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HistorialCarpeta hi = new HistorialCarpeta
            {
                RHechoId = model.RHechoId,
                Detalle = model.Detalle,
                DetalleEtapa = model.DetalleEtapa,
                Modulo = model.Modulo,
                Agencia = model.Agencia,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = model.Fechasys

            };

            _context.HistorialCarpetas.Add(hi);
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
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/Historialcarpeta/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var historialCarpeta = await _context.HistorialCarpetas.Where(x => x.RHechoId == model.IdRHecho).ToListAsync();



            if (historialCarpeta == null)
            {
                return Ok();

            }

            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {


                    foreach (HistorialCarpeta historialActual in historialCarpeta)
                    {

                        var insertarHistorial = await ctx.HistorialCarpetas.FirstOrDefaultAsync(a => a.IdHistorialcarpetas == historialActual.IdHistorialcarpetas);

                        if (insertarHistorial == null)
                        {
                            insertarHistorial = new HistorialCarpeta();
                            ctx.HistorialCarpetas.Add(insertarHistorial);
                        }

                        insertarHistorial.IdHistorialcarpetas = historialActual.IdHistorialcarpetas;
                        insertarHistorial.RHechoId = historialActual.RHechoId;
                        insertarHistorial.Detalle = historialActual.Detalle;
                        insertarHistorial.Modulo = historialActual.Modulo;
                        insertarHistorial.UDistrito = historialActual.UDistrito;
                        insertarHistorial.USubproc = historialActual.USubproc;
                        insertarHistorial.UAgencia = historialActual.UAgencia;
                        insertarHistorial.Usuario = historialActual.Usuario;
                        insertarHistorial.UPuesto = historialActual.UPuesto;
                        insertarHistorial.UModulo = historialActual.UModulo;
                        insertarHistorial.Fechasys = historialActual.Fechasys;
                        insertarHistorial.Agencia = historialActual.Agencia;
                        insertarHistorial.DetalleEtapa = historialActual.DetalleEtapa;

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
