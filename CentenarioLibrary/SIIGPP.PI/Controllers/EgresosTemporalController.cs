using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.EgresosTemporales;
using SIIGPP.Entidades.M_PI.EgresosTemporales;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EgresosTemporalController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public EgresosTemporalController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/EgresosTemporal/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{DetencionId}")]
        public async Task<IEnumerable<EgresosTemporalViewModel>> Listar([FromRoute] Guid DetencionId)
        {
            var Egr = await _context.EgresoTemporals
                .Where(a => a.DetencionId == DetencionId)
                .ToListAsync();

            return Egr.Select(a => new EgresosTemporalViewModel
            {
                IdEgresoTemporal = a.IdEgresoTemporal,
                DetencionId = a.DetencionId,
                Motivo = a.Motivo,
                Horaegreso  = a.Horaegreso,
                QuienSolicita = a.QuienSolicita,
                QuienAutoriza = a.QuienAutoriza,
                Notas = a.Notas,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,


            });

        }

        // POST: api/EgresosTemporal/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EgresoTemporal Egr = new EgresoTemporal
            {

                DetencionId = model.DetencionId,
                Motivo = model.Motivo,
                Horaegreso = model.Horaegreso,
                QuienSolicita = model.QuienSolicita,
                QuienAutoriza = model.QuienAutoriza,
                Notas = model.Notas,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
            };

            _context.EgresoTemporals.Add(Egr);
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

        // GET: api/EgresosTemporal/ListarporidDetencion
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{DetencionId}")]
        public async Task<IActionResult> ListarporidDetencion([FromRoute] Guid DetencionId)
        {
            var egre = await _context.EgresoTemporals
                .Where(a => a.DetencionId == DetencionId)
                .FirstOrDefaultAsync();

            if (egre == null)
            {
                return NotFound("No hay registros");

            }

            return Ok(new EgresosTemporalViewModel
            {
                IdEgresoTemporal = egre.IdEgresoTemporal,
                DetencionId = egre.DetencionId,
                Motivo = egre.Motivo,
                Horaegreso = egre.Horaegreso,
                QuienSolicita = egre.QuienSolicita,
                QuienAutoriza = egre.QuienAutoriza,
                Notas = egre.Notas,
                UDistrito = egre.UDistrito,
                USubproc = egre.USubproc,
                UAgencia = egre.UAgencia,
                Usuario = egre.Usuario,
                UPuesto = egre.UPuesto,
                UModulo = egre.UModulo,
                Fechasys = egre.Fechasys,
            });

        }

    }
}
