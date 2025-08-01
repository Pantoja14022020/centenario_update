using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.InformacionJuridico.ComparecenciasElementoss;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComparecenciaElementoController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public ComparecenciaElementoController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/ComparecenciaElemento/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{idmodulo}")]
        public async Task<IEnumerable<ComparecenciaElementoViewModel>> Listar([FromRoute] Guid idmodulo)
        {
            var Com = await _context.ComparecenciasElementos
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.ModuloServicioId == idmodulo)
                .ToListAsync();

            return Com.Select(a => new ComparecenciaElementoViewModel
            {
                IdCompElementos = a.IdCompElementos,
                ModuloServicioId = a.ModuloServicioId,
                Comparecencia = a.Comparecencia,
                Elemento = a.Elemento,
                FComparecencia = a.FComparecencia,
                Hora = a.Hora,
                Noficio = a.Noficio,
                CausaPenal = a.CausaPenal,
                AnteAutoridad = a.AnteAutoridad,
                Notas = a.Notas,
                Recibe = a.Recibe,
                Status = a.Status,
                Estado = a.Estado,
                Respuesta = a.Respuesta,
                FAsignacion = a.FAsignacion,
                FFinalizacion = a.FFinalizacion,
                FUltmimoStatus = a.FUltmimoStatus,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                

            }); ;

        }

        // POST: api/ComparecenciaElemento/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ComparecenciasElementos Com = new ComparecenciasElementos
            {
                ModuloServicioId = model.ModuloServicioId,
                Comparecencia = model.Comparecencia,
                Elemento = model.Elemento,
                FComparecencia = model.FComparecencia,
                Hora = model.Hora,
                Noficio = model.Noficio,
                CausaPenal = model.CausaPenal,
                AnteAutoridad = model.AnteAutoridad,
                Notas = model.Notas,
                Recibe = model.Recibe,
                Status = model.Status,
                Estado = model.Estado,
                Respuesta = model.Respuesta,
                FAsignacion = model.FAsignacion,
                FFinalizacion = model.FFinalizacion,
                FUltmimoStatus = System.DateTime.Now,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
            };

            _context.ComparecenciasElementos.Add(Com);

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


        // PUT: api/ComparecenciaElemento/AsiganarUnidad
        [HttpPut("[action]")]
        public async Task<IActionResult> AsiganarUnidad([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.ComparecenciasElementos.FirstOrDefaultAsync(a => a.IdCompElementos == model.IdCompElementos);

            if (com == null)
            {
                return NotFound();
            }
            com.ModuloServicioId = model.ModuloServicioId;
            com.Status = "Asignado";
            com.FAsignacion = System.DateTime.Now;
            com.FUltmimoStatus = System.DateTime.Now;
            
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

        // GET: api/ComparecenciaElemento/ListarporModulo
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{moduloid}")]
        public async Task<IEnumerable<ComparecenciaElementoViewModel>> ListarporModulo([FromRoute]Guid moduloid)
        {
            var Com = await _context.ComparecenciasElementos
                .Where( a=> a.ModuloServicioId == moduloid)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return Com.Select(a => new ComparecenciaElementoViewModel
            {
                IdCompElementos = a.IdCompElementos,
                ModuloServicioId = a.ModuloServicioId,
                Comparecencia = a.Comparecencia,
                Elemento = a.Elemento,
                FComparecencia = a.FComparecencia,
                Hora = a.Hora,
                Noficio = a.Noficio,
                CausaPenal = a.CausaPenal,
                AnteAutoridad = a.AnteAutoridad,
                Notas = a.Notas,
                Recibe = a.Recibe,
                Status = a.Status,
                Estado = a.Estado,
                Respuesta = a.Respuesta,
                FAsignacion = a.FAsignacion,
                FFinalizacion = a.FFinalizacion,
                FUltmimoStatus = a.FUltmimoStatus,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,

            });

        }

        // PUT: api/ComparecenciaElemento/ActualizarStatus
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.ComparecenciasElementos.FirstOrDefaultAsync(a => a.IdCompElementos == model.IdCompElementos);

            if (com == null)
            {
                return NotFound();
            }
            com.Status = model.Status;
            com.FUltmimoStatus = System.DateTime.Now;

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


        // PUT: api/ComparecenciaElemento/ActualizarStatusFinalizado
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatusFinalizado([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

 

            var com = await _context.ComparecenciasElementos.FirstOrDefaultAsync(a => a.IdCompElementos == model.IdCompElementos);

            if (com == null)
            {
                return NotFound();
            }
            com.Status = model.Status;
            com.FFinalizacion = System.DateTime.Now;
            com.FUltmimoStatus = System.DateTime.Now;

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

    }
}
