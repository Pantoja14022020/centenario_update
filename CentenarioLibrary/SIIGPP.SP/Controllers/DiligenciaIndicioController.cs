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
using SIIGPP.SP.ModelsSP.DiligenciaIndicos;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_SP.DiligenciaIndicios;

namespace SIIGPP.SP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiligenciaIndicioController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public DiligenciaIndicioController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // POST: api/DiligenciaIndicio/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            DiligenciaIndicio di = new DiligenciaIndicio
            {
                
                RDiligenciasId = model.RDiligenciasId,
                IndiciosId = model.IndiciosId

            };

            _context.DiligenciaIndicios.Add(di);
            try
            {
                await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

            return Ok();
        }


        // GET: api/DiligenciaIndicio/Listarporiddili
        [HttpGet("[action]/{idRDiligencias}")]
        [Authorize(Roles = "Perito,Oficialia de partes")]
        public async Task<IActionResult> Listarporiddili([FromRoute] Guid idRDiligencias)
        {
            var di = await _context.DiligenciaIndicios
                .Where(a => a.RDiligenciasId == idRDiligencias)
                .FirstOrDefaultAsync();

            if (di == null)
            {
                return BadRequest("No hay registros");

            }
            return Ok(new DiligenciaIndicioDetalleinViewModel
            {
                IndiciosId = di.IndiciosId
            });

        }


        // GET: api/DiligenciaIndicio/ListarIndicios
        [Authorize(Roles = "Administrador, Oficialia de partes, Coordinador, Director, Subprocurador,Perito")]
        [HttpGet("[action]/{idRdiligencia}")]
        public async Task<IEnumerable<DiligenciaIndicioDetalleinViewModel>> ListarIndicios([FromRoute] Guid idRdiligencia)
        {
            var pa = await _context.DiligenciaIndicios
                .Where(a => a.RDiligenciasId == idRdiligencia)
                .Include(a => a.Indicios)
                .ToListAsync();

            return pa.Select(a => new DiligenciaIndicioDetalleinViewModel
            {

                IndiciosId = a.IndiciosId,
                Dato = a.Indicios.TipoIndicio + " - " + a.Indicios.Etiqueta


            });

        }


    }
}
