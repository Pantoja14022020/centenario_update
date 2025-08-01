using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.PI.Models.CMedicosPR;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_PI.CMedicosPR;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMedicoPRController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public CMedicoPRController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/CMedicoPR/Listarporid
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{PersonaID}")]
        public async Task<IEnumerable<CMedicoPRViewModel>> Listarporid([FromRoute]Guid PersonaID)
        {
            var persona = await _context.CMedicoPRs
                .Where(a => a.PersonaId == PersonaID)
                .ToListAsync();

            return persona.Select(a => new CMedicoPRViewModel
            {
                IdCMedicoPR = a.IdCMedicoPR,
                PersonaId = a.PersonaId,
                Nuc = a.Nuc,
                NOficio = a.NOficio,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                ModuloServicioId = a.ModuloServicioId,
                Status = a.Status,
                Respuesta = a.Respuesta,    
                FechaAsignacion  = a.FechaAsignacion,
                FechaUltimoStatus = a.FechaUltimoStatus,
                NumeroAgencia = a.NumeroAgencia,
                TelefonoAgencia = a.TelefonoAgencia,
                NodeSolicitud = a.NodeSolicitud,
                NumeroControl = a.NumeroControl,
                NumeroDistrito = a.NumeroDistrito,
                NumeroControlf = a.NumeroDistrito+"/"+a.NodeSolicitud


            });

        }

        // POST: api/CMedicoPR/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CMedicoPR Medico = new CMedicoPR
            {
             PersonaId = model.PersonaId,
             Nuc = model.Nuc,
             NOficio = model.NOficio,
             UDistrito = model.UDistrito,
             USubproc = model.USubproc,
             UAgencia = model.UAgencia,
             Usuario = model.Usuario,
             UPuesto = model.UPuesto,
             UModulo = model.UModulo,
             Fechasys = System.DateTime.Now,
             ModuloServicioId = model.ModuloServicioId,
             Status = model.Status,
             Respuesta = model.Respuesta,
             FechaAsignacion = model.FechaAsignacion,
             FechaUltimoStatus = System.DateTime.Now,
             NumeroAgencia = model.NumeroAgencia,
             TelefonoAgencia = model.TelefonoAgencia,
             NodeSolicitud = model.NodeSolicitud,
             NumeroControl = model.NumeroControl,
             NumeroDistrito = model.NumeroDistrito
            };

            _context.CMedicoPRs.Add(Medico);
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

        // GET: api/CMedicoPR/ListarTodos
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<CMedicoPRViewModel>> ListarTodos()
        {
            var todos = await _context.CMedicoPRs
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return todos.Select(a => new CMedicoPRViewModel
            {
                IdCMedicoPR = a.IdCMedicoPR,
                PersonaId = a.PersonaId,
                Nuc = a.Nuc,
                NOficio = a.NOficio,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                ModuloServicioId = a.ModuloServicioId,
                Status = a.Status,
                Respuesta = a.Respuesta,
                FechaAsignacion = a.FechaAsignacion,
                FechaUltimoStatus = a.FechaUltimoStatus,
                NodeSolicitud = a.NodeSolicitud,
                NumeroDistrito = a.NumeroDistrito,
                NumeroControlf = a.NumeroDistrito + "/" + a.NumeroControl


            });

        }

        // PUT: api/CMedicoPR/AsignarModuloServicio
        [HttpPut("[action]")]
        public async Task<IActionResult> AsignarModuloServicio([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var pa = await _context.CMedicoPRs.FirstOrDefaultAsync(a => a.IdCMedicoPR == model.IdCMedicoPR);

            if (pa == null)
            {
                return NotFound();
            }

            pa.ModuloServicioId = model.ModuloServicioId;
            pa.Status = "Asignado";
            pa.FechaAsignacion = System.DateTime.Now;
            pa.FechaUltimoStatus = System.DateTime.Now;
            pa.NumeroControl = model.NumeroControl;

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

        // PUT: api/CMedicoPR/RechazarSolicitudServicio
        [HttpPut("[action]")]
        public async Task<IActionResult> RechazarSolicitudServicio([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pa = await _context.CMedicoPRs.FirstOrDefaultAsync(a => a.IdCMedicoPR == model.IdCMedicoPR);

            if (pa == null)
            {
                return NotFound();
            }

            
            pa.Status = "Rechazado";
            pa.Respuesta = model.Respuesta;
            pa.FechaUltimoStatus = System.DateTime.Now;

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

        // GET: api/CMedicoPR/ListarporModulo
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{moduloid}")]
        public async Task<IEnumerable<CMedicoPRViewModel>> ListarporModulo([FromRoute]Guid moduloid)
        {
            var Com = await _context.CMedicoPRs
                .Where(a => a.ModuloServicioId == moduloid)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return Com.Select(a => new CMedicoPRViewModel
            {
                IdCMedicoPR = a.IdCMedicoPR,
                PersonaId = a.PersonaId,
                ModuloServicioId = a.ModuloServicioId,
                Nuc = a.Nuc,
                NOficio = a.NOficio,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                Status = a.Status,
                Respuesta = a.Respuesta,
                FechaAsignacion = a.FechaAsignacion,
                FechaUltimoStatus = a.FechaUltimoStatus,
                NodeSolicitud = a.NodeSolicitud,
                NumeroControl = a.NumeroControl,
                NumeroDistrito = a.NumeroDistrito,
                NumeroControlf = a.NumeroDistrito + "/" + a.NumeroControl
            });
        }

        // PUT: api/CMedicoPR/ActualizarStatus
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.CMedicoPRs.FirstOrDefaultAsync(a => a.IdCMedicoPR == model.IdCMedicoPR);

            if (com == null)
            {
                return NotFound();
            }
            com.Status = model.Status;
            com.FechaUltimoStatus = System.DateTime.Now;

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

        // PUT: api/CMedicoPR/ActualizarStatusFinalizado
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatusFinalizado([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var com = await _context.CMedicoPRs.FirstOrDefaultAsync(a => a.IdCMedicoPR == model.IdCMedicoPR);

            if (com == null)
            {
                return NotFound();
            }
            com.Status = model.Status;
            com.FechaUltimoStatus = System.DateTime.Now;

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

        // GET: api/CMedicoPR/ObtenernumeroMaximoporDistrito
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{Ndistrito}")]
        public async Task<IActionResult> ObtenernumeroMaximoporDistrito([FromRoute] string Ndistrito)
        {
            var da = await _context.CMedicoPRs
                .OrderByDescending(x => Int32.Parse(x.NodeSolicitud))
                .Where(x => x.NumeroDistrito == Ndistrito)
                .FirstOrDefaultAsync();

            if (da == null)
            {
                return NotFound("No hay registros");

            }

            return Ok(new DatosExtrasViewModel
            {
                NumeroMaximo = da.NodeSolicitud

            });

        }

    }
}
