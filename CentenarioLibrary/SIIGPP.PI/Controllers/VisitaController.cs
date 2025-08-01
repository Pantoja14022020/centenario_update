using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.Visitas;
using SIIGPP.Entidades.M_PI.Visitas;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitaController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public VisitaController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Visita/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<VisitaViewModel>> Listar()
        {
            var Sdet = await _context.Visitas
                .OrderByDescending(a => a.Fechasys)
                .Include(a => a.Detencion.Persona)
                .Include(a => a.PIPersonaVisita)
                .ToListAsync();

            return Sdet.Select(a => new VisitaViewModel
            {
                IdVisita = a.IdVisita,
                PIPersonaVisitaId = a.PIPersonaVisitaId,
                DetencionId = a.DetencionId,
                FechayHora = a.FechayHora,
                HILocutorio =a.HILocutorio,
                HSLocutorio = a.HSLocutorio,
                QAutorizaVisita =a.QAutorizaVisita,
                MotivoVisita =a.MotivoVisita,
                RDetenido =a.RDetenido,
                UDistrito = a.UDistrito,
                USubproc =a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                NombreVisita = a.PIPersonaVisita.Nombre +" "+ a.PIPersonaVisita.ApellidoP +" "+ a.PIPersonaVisita.ApellidoM,
                NombreDetenido = a.Detencion.Persona.Nombre + " "+ a.Detencion.Persona.ApellidoPaterno + " "+ a.Detencion.Persona.ApellidoMaterno
            });
        }

        // POST: api/Visita/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Visita Vis = new Visita
            {
                PIPersonaVisitaId =model.PIPersonaVisitaId,
                DetencionId = model.DetencionId,
                FechayHora = model.FechayHora,
                HILocutorio = model.HILocutorio,
                HSLocutorio = model.HSLocutorio,
                QAutorizaVisita = model.QAutorizaVisita,
                MotivoVisita = model.MotivoVisita,
                RDetenido = model.RDetenido,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo  = model.UModulo,
                Fechasys = System.DateTime.Now,
            };

            _context.Visitas.Add(Vis);

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

        // PUT: api/Visita/ActualizarhrSalida
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarhrSalida([FromBody] ActualizarhrSalidaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var de = await _context.Visitas.FirstOrDefaultAsync(a => a.IdVisita == model.IdVisita);

            if (de == null)
            {
                return NotFound();
            }

            de.HSLocutorio = model.HSLocutorio;

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

        // GET: api/Visita/ListarporIdvisita
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{personavisitaId}")]
        public async Task<IEnumerable<VisitaViewModel>> ListarporIdvisita([FromRoute] Guid personavisitaId)
        {
            var Sdet = await _context.Visitas
                .OrderByDescending(a => a.Fechasys)              
                .Include(a => a.Detencion.Persona)
                .Include(a => a.PIPersonaVisita)
                .Where(a => a.PIPersonaVisitaId == personavisitaId)
                .ToListAsync();

            var Sdet2 = await _context.Visitas
                .GroupBy(v => v.DetencionId)
                .Select(x => new { etiqueta = x.Key })
                .ToListAsync();

            int numerovisitados = 0;

            int calcular(Guid a)
            {
                numerovisitados+=1;
                return 1;
            }
            
             Sdet2.Select(v => new VisitaViewModel
            {
                Detenidosvisitados = calcular(v.etiqueta),
            });
           
            return Sdet.Select(a => new VisitaViewModel
            {
                IdVisita = a.IdVisita,
                PIPersonaVisitaId = a.PIPersonaVisitaId,
                DetencionId = a.DetencionId,
                FechayHora = a.FechayHora,
                HILocutorio = a.HILocutorio,
                HSLocutorio = a.HSLocutorio,
                QAutorizaVisita = a.QAutorizaVisita,
                MotivoVisita = a.MotivoVisita,
                RDetenido = a.RDetenido,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                NombreVisita = a.PIPersonaVisita.Nombre + " " + a.PIPersonaVisita.ApellidoP + " " + a.PIPersonaVisita.ApellidoM,
                NombreDetenido = a.Detencion.Persona.Nombre + " " + a.Detencion.Persona.ApellidoPaterno + " " + a.Detencion.Persona.ApellidoMaterno,
                Detenidosvisitados = numerovisitados                
            });
        }

        // GET: api/Visita/ListarporIddetenido
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{detenidoid}")]
        public async Task<IEnumerable<VisitaViewModel>> ListarporIddetenido([FromRoute] Guid detenidoid)
        {
            var Sdet = await _context.Visitas
                .OrderByDescending(a => a.Fechasys)
                .Include(a => a.Detencion.Persona)
                .Include(a => a.PIPersonaVisita)
                .Where(a => a.DetencionId == detenidoid)
                .ToListAsync();

            return Sdet.Select(a => new VisitaViewModel
            {
                IdVisita = a.IdVisita,
                PIPersonaVisitaId = a.PIPersonaVisitaId,
                DetencionId = a.DetencionId,
                FechayHora = a.FechayHora,
                HILocutorio = a.HILocutorio,
                HSLocutorio = a.HSLocutorio,
                QAutorizaVisita = a.QAutorizaVisita,
                MotivoVisita = a.MotivoVisita,
                RDetenido = a.RDetenido,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                NombreVisita = a.PIPersonaVisita.Nombre + " " + a.PIPersonaVisita.ApellidoP + " " + a.PIPersonaVisita.ApellidoM,
                NombreDetenido = a.Detencion.Persona.Nombre + " " + a.Detencion.Persona.ApellidoPaterno + " " + a.Detencion.Persona.ApellidoMaterno
            });
        }
    }
}