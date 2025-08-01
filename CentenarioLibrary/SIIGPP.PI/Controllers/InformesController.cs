using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using Microsoft.AspNetCore.Authorization;
using SIIGPP.Entidades.M_PI.Informes;
using SIIGPP.PI.Models.Informes;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public InformesController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // POST: api/Informes/Crear
        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            Informe info = new Informe
            {

                PeritoAsignadoPIId = model.PeritoAsignadoPIId,
                TipoInforme = model.TipoInforme,
                TextoInforme = model.TextoInforme,
                Fecha = model.Fecha,
                FechaSys = fecha,
                uDistrito =  model.uDistrito,
                uSubproc = model.uSubproc,
                uAgencia = model.uAgencia,
                uUsuario = model.uUsuario,
                uPuesto = model.uPuesto,
                uModulo = model.uModulo,
                NoOficio = model.NoOficio
            };

            _context.Informes.Add(info);
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


        // GET: api/Informes/ListarporPeritoId
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{IdPerito}")]
        public async Task<IEnumerable<InformeViewModel>> ListarporPeritoId([FromRoute] Guid IdPerito)
        {
            var info = await _context.Informes
                .Where(a => a.PeritoAsignadoPIId == IdPerito)
                .Include(a => a.PeritoAsignadoPI.RActoInvestigacion)
                .ToListAsync();

            return info.Select(a => new InformeViewModel
            {
                IdInforme = a.IdInforme,
                PeritoAsignadoPIId = a.PeritoAsignadoPIId,
                TipoInforme = a.TipoInforme,
                TextoInforme = a.TextoInforme,
                Fecha = a.Fecha,
                FechaSys = a.FechaSys,
                uDistrito = a.uDistrito,
                uSubproc = a.uSubproc,
                uAgencia = a.uAgencia,
                uUsuario = a.uUsuario,
                uPuesto = a.uPuesto,
                uModulo = a.uModulo,
                PersonaSolicita = a.PeritoAsignadoPI.RActoInvestigacion.UUsuario,
                Nuc = a.PeritoAsignadoPI.RActoInvestigacion.NUC,
                NoOficio = a.NoOficio
            });

        }

        // GET: api/Informes/ListarporPeritoIdParciales
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{IdPerito}")]
        public async Task<IEnumerable<InformeViewModel>> ListarporPeritoIdParciales([FromRoute] Guid IdPerito)
        {
            var info = await _context.Informes
                .Where(a => a.PeritoAsignadoPIId == IdPerito)
                .Where(a => a.TipoInforme == 1)
                .Include(a => a.PeritoAsignadoPI.RActoInvestigacion)
                .ToListAsync();

            return info.Select(a => new InformeViewModel
            {
                IdInforme = a.IdInforme,
                PeritoAsignadoPIId = a.PeritoAsignadoPIId,
                TipoInforme = a.TipoInforme,
                TextoInforme = a.TextoInforme,
                Fecha = a.Fecha,
                FechaSys = a.FechaSys,
                uDistrito = a.uDistrito,
                uSubproc = a.uSubproc,
                uAgencia = a.uAgencia,
                uUsuario = a.uUsuario,
                uPuesto = a.uPuesto,
                uModulo = a.uModulo,
                PersonaSolicita = a.PeritoAsignadoPI.RActoInvestigacion.UUsuario,
                NoOficio = a.NoOficio
            });

        }


    }
}
