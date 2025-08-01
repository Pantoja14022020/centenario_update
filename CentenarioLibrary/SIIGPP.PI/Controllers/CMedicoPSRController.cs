using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.CMedicosPSR;
using SIIGPP.Entidades.M_PI.CMedicosPSR;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.PI.Controllers
{
  
        [Route("api/[controller]")]
        [ApiController]
        public class CMedicoPSRController : ControllerBase
        {

            private readonly DbContextSIIGPP _context;

            public CMedicoPSRController(DbContextSIIGPP context)
            {
                _context = context;
            }


            // GET: api/CMedicoPSR/Listar
            [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
            [HttpGet("[action]")]
            public async Task<IEnumerable<CMedicoPSRViewModel>> Listar()
            {
                var per = await _context.CMedicoPSRs
                    .OrderByDescending(a => a.Fechasys)
                    .ToListAsync();

                return per.Select(a => new CMedicoPSRViewModel
                {
                IdCMedicoPSR =a.IdCMedicoPSR,
                Nombre = a.Nombre,
                ApellidoP = a.ApellidoP,
                ApellidoM = a.ApellidoM,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                NumUnicoRegistro = a.NumUnicoRegistro,
                ModuloServicioId = a.ModuloServicioId,
                Status = a.Status,
                Respuesta = a.Respuesta,
                FechaAsignacion = a.FechaAsignacion,
                FechaUltimoStatus = a.FechaUltimoStatus,
                NumeroAgencia = a.NumeroAgencia,
                TelefonoAgencia = a.TelefonoAgencia,
                NodeSolicitud = a.NodeSolicitud,
                NumeroControl = a.NumeroControl,
                NumeroDistrito = a.NumeroDistrito,
                NumeroControlf = a.NumeroDistrito+"/"+a.NumeroControl

                });

            }

            // POST: api/CMedicoPSR/Crear
            [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
            [HttpPost("[action]")]
            public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                CMedicoPSR Medico = new CMedicoPSR
                {
                     Nombre = model.Nombre,
                     ApellidoP = model.ApellidoP,
                     ApellidoM  = model.ApellidoM,
                     UDistrito = model.UDistrito,
                     USubproc = model.USubproc,
                     UAgencia = model.UAgencia,
                     Usuario = model.Usuario,
                     UPuesto = model.UPuesto,
                     UModulo = model.UModulo,
                     NumUnicoRegistro = model.NumUnicoRegistro,
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
                     NumeroDistrito = model.NumeroDistrito,
                };

                _context.CMedicoPSRs.Add(Medico);
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

        // GET: api/CMedicoPSR/ListarTodos
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<CMedicoPSRViewModel>> ListarTodos()
        {
            var todos = await _context.CMedicoPSRs
                .ToListAsync();

            return todos.Select(a => new CMedicoPSRViewModel
            {
                IdCMedicoPSR = a.IdCMedicoPSR,
                ModuloServicioId = a.ModuloServicioId,
                Nombre = a.Nombre,
                ApellidoP = a.ApellidoP,
                ApellidoM = a.ApellidoM,
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

        // PUT: api/CMedicoPSR/AsignarModuloServicio
        [HttpPut("[action]")]
        public async Task<IActionResult> AsignarModuloServicio([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
     

            var pa = await _context.CMedicoPSRs.FirstOrDefaultAsync(a => a.IdCMedicoPSR == model.IdCMedicoPSR);

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

        // PUT: api/CMedicoPSR/RechazarSolicitudServicio
        [HttpPut("[action]")]
        public async Task<IActionResult> RechazarSolicitudServicio([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

  

            var pa = await _context.CMedicoPSRs.FirstOrDefaultAsync(a => a.IdCMedicoPSR == model.IdCMedicoPSR);

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

        // GET: api/CMedicoPSR/ListarporModulo
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{moduloid}")]
        public async Task<IEnumerable<CMedicoPSRViewModel>> ListarporModulo([FromRoute]Guid moduloid)
        {
            var Com = await _context.CMedicoPSRs
                .Where(a => a.ModuloServicioId == moduloid)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return Com.Select(a => new CMedicoPSRViewModel
            {
                IdCMedicoPSR = a.IdCMedicoPSR,
                ModuloServicioId = a.ModuloServicioId,
                Nombre = a.Nombre,
                ApellidoP = a.ApellidoP,
                ApellidoM = a.ApellidoM,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                NumUnicoRegistro = a.NumUnicoRegistro,
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

        // PUT: api/CMedicoPSR/ActualizarStatus
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.CMedicoPSRs.FirstOrDefaultAsync(a => a.IdCMedicoPSR == model.IdCMedicoPSR);

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

        // PUT: api/CMedicoPSR/ActualizarStatusFinalizado
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatusFinalizado([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.CMedicoPSRs.FirstOrDefaultAsync(a => a.IdCMedicoPSR == model.IdCMedicoPSR);

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


        // GET: api/CMedicoPSR/ObtenernumeroMaximoporDistrito
        [HttpGet("[action]/{Ndistrito}")]
        public async Task<IActionResult> ObtenernumeroMaximoporDistrito([FromRoute] string Ndistrito)
        {
            var da = await _context.CMedicoPSRs
                .OrderByDescending(x => x.NumeroDistrito)
                .Where(x => x.NumeroDistrito == Ndistrito)
                .FirstOrDefaultAsync();

            if (da == null)
            {
                return Ok(new { NumeroMaximo = 0 });

            }

            return Ok(new DatosExtrasViewModel
            {
                NumeroMaximo = da.NodeSolicitud

            });

        }

        // GET: api/CMedicoPSR/ObtenernumeroMaximoporDistritoPr
        [HttpGet("[action]/{Ndistrito}")]
        public async Task<IActionResult> ObtenernumeroMaximoporDistritoPr([FromRoute] string Ndistrito)
        {
            var da = await _context.CMedicoPRs
                .OrderByDescending(x => x.NumeroDistrito)
                .Where(x => x.NumeroDistrito == Ndistrito)
                .FirstOrDefaultAsync();

            if (da == null)
            {
                return Ok(new { NumeroMaximo = 0 });

            }

            return Ok(new DatosExtrasViewModel
            {
                NumeroMaximo = da.NodeSolicitud

            });

        }

    }
    
}
