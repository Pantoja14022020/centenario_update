using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.Detenciones;
using SIIGPP.Entidades.M_PI.Detenciones;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialDetencionController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;

        public HistorialDetencionController(DbContextSIIGPP context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        // GET: api/HistorialDetencion/ListarporIddtencion
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{iddetenciones}")]
        public async Task<IEnumerable<HistorialDetencionesViewModel>> ListarporIddtencion([FromRoute] Guid iddetenciones)
        {
            var info = await _context.HistorialDetencions
                .Where(a => a.DetencionId == iddetenciones)
                .OrderByDescending(a => a.Fechasys)
                .Include(a => a.Detencion)
                .ToListAsync();

            return info.Select(a => new HistorialDetencionesViewModel
            {
                IdHistorialDetencion = a.IdHistorialDetencion,
                DetencionId = a.DetencionId,
                StatusPasado = a.StatusPasado,                          
                StatuusNuevo = a.StatuusNuevo,
                Fechasys = a.Fechasys,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo
            });

        }


        // POST: api/HistorialDetencion/CrearEntrada
        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        public async Task<IActionResult> CrearEntrada([FromBody] CrearHistorialDetencionesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            HistorialDetencion histo = new HistorialDetencion
            {

                DetencionId = model.DetencionId,
                StatusPasado = model.StatusPasado,
                StatuusNuevo = model.StatuusNuevo,
                Fechasys = System.DateTime.Now,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo
            };

            _context.HistorialDetencions.Add(histo);
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

        // POST: api/HistorialDetencion/NuevoStatus
        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        public async Task<IActionResult> NuevoStatus([FromBody] CrearHistorialDetencionesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            HistorialDetencion histo = new HistorialDetencion
            {

                DetencionId = model.DetencionId,
                StatusPasado = model.StatusPasado,
                StatuusNuevo = model.StatuusNuevo,
                Fechasys = System.DateTime.Now,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo
            };

            _context.HistorialDetencions.Add(histo);
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

    }
}
