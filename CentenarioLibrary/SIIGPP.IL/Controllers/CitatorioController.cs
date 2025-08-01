using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.IL.Models.Citatorios;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_IL.Citatorios;

namespace SIIGPP.IL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitatorioController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public CitatorioController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Citatorio/Listar
        [HttpGet("[action]/{Idrhecho}")]
        public async Task<IEnumerable<CitatorioViewModel>> Listar([FromRoute] Guid Idrhecho)
        {
            var ao = await _context.Citatorios
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.RHechoId == Idrhecho)
                .ToListAsync();

            return ao.Select(a => new CitatorioViewModel
            {
                IdCitatorio = a.IdCitatorio,
                RHechoId = a.RHechoId,
                NumeroOficio = a.NumeroOficio,
                CausaPenal = a.CausaPenal,
                Nuc =a.Nuc,
                Delito =a.Delito,
                Destinatario =a.Destinatario,
                Domicilio = a.Domicilio,
                FechaCitacion =a.FechaCitacion,
                JuicioOral =a.JuicioOral,
                Multa =a.Multa,
                Articulo =a.Articulo,
                LugarPresentarse =a.LugarPresentarse,
                DireccionLugar =a.DireccionLugar,
                CausaPenalJo = a.CausaPenalJo,
                Imputado =a.Imputado,
                PeritosPolicias =a.PeritosPolicias,
                HoraCitacion =a.HoraCitacion,
                DireccionAgencia =a.DireccionAgencia,
                FechaCitacion2 =a.FechaCitacion2,
                Tipo =a.Tipo,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                Dirigidoa = a.Dirigidoa,
                Puesto = a.Puesto,
            });
        }

        // POST: api/Citatorio/Crear
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Citatorio ag = new Citatorio
            {
                
                NumeroOficio = model.NumeroOficio,
                RHechoId = model.RHechoId,
                CausaPenal = model.CausaPenal,
                Nuc = model.Nuc,
                Delito = model.Delito,
                Destinatario = model.Destinatario,
                Domicilio = model.Domicilio,
                FechaCitacion = model.FechaCitacion,
                JuicioOral = model.JuicioOral,
                Multa = model.Multa,
                Articulo = model.Articulo,
                LugarPresentarse = model.LugarPresentarse,
                DireccionLugar = model.DireccionLugar,
                CausaPenalJo = model.CausaPenalJo,
                Imputado = model.Imputado,
                PeritosPolicias = model.PeritosPolicias,
                HoraCitacion = model.HoraCitacion,
                DireccionAgencia = model.DireccionAgencia,
                FechaCitacion2 = model.FechaCitacion2,
                Tipo = model.Tipo,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
                Dirigidoa = model.Dirigidoa,
                Puesto = model.Puesto,
            };

            _context.Citatorios.Add(ag);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();
        }
    }
}