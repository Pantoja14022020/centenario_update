using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.InformacionJuridico.Arrestos;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.PI.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class ArrestoController : ControllerBase
        {

            private readonly DbContextSIIGPP _context;

            public ArrestoController(DbContextSIIGPP context)
            {
                _context = context;
            }


        // GET: api/Arresto/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{idmodulo}")]
        public async Task<IEnumerable<ArrestoViewModel>> Listar([FromRoute] Guid idmodulo)
            {
                var Arres = await _context.Arrestos
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.ModuloServicioId == idmodulo)
                .ToListAsync();

                return Arres.Select(a => new ArrestoViewModel
                {
                    IdArresto = a.IdArresto,
                    ModuloServicioId = a.ModuloServicioId,
                    FechaRecibido = a.FechaRecibido,
                    Oficio = a.Oficio,
                    CausaPenal = a.CausaPenal,
                    NomArrestado = a.NomArrestado,
                    TiempoH = a.TiempoH,
                    JuezSolicita = a.JuezSolicita,
                    GpoAsignado = a.GpoAsignado,
                    Notas = a.Notas,
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
                    FechasComparescencia = a.FechasComparescencia,
                    FechaCumplimiento = a.FechaCumplimiento,
                    FechaRecepcion = a.FechaRecepcion,

                });

            }

        // POST: api/Arresto/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
            public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Arresto Arre = new Arresto
                {
                    ModuloServicioId = model.ModuloServicioId,
                    FechaRecibido= model.FechaRecibido,
                    Oficio = model.Oficio,
                    CausaPenal = model.CausaPenal,
                    NomArrestado = model.NomArrestado,
                    TiempoH = model.TiempoH,
                    JuezSolicita = model.JuezSolicita,
                    GpoAsignado = model.GpoAsignado,
                    Notas = model.Notas,
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
                    UModulo  = model.UModulo,
                    Fechasys = System.DateTime.Now,
                    FechasComparescencia = model.FechasComparescencia,
                    FechaCumplimiento = model.FechaCumplimiento,
                    FechaRecepcion = model.FechaRecepcion,
                };

                _context.Arrestos.Add(Arre);

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


        // PUT: api/Arresto/AsiganarUnidad
        [HttpPut("[action]")]
        public async Task<IActionResult> AsiganarUnidad([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var com = await _context.Arrestos.FirstOrDefaultAsync(a => a.IdArresto == model.IdArresto);

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

        // GET: api/Arresto/ListarporModulo
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{moduloid}")]
        public async Task<IEnumerable<ArrestoViewModel>> ListarporModulo([FromRoute]Guid moduloid)
        {
            var Com = await _context.Arrestos
                .Where(a => a.ModuloServicioId == moduloid)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return Com.Select(a => new ArrestoViewModel
            {
                IdArresto = a.IdArresto,
                ModuloServicioId = a.ModuloServicioId,
                FechaRecibido = a.FechaRecibido,
                Oficio = a.Oficio,
                CausaPenal = a.CausaPenal,
                NomArrestado = a.NomArrestado,
                TiempoH = a.TiempoH,
                JuezSolicita = a.JuezSolicita,
                GpoAsignado = a.GpoAsignado,
                Notas = a.Notas,
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
                Fechasys = a.Fechasys

            });

        }

        // PUT: api/Arresto/ActualizarStatus
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.Arrestos.FirstOrDefaultAsync(a => a.IdArresto == model.IdArresto);

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


        // PUT: api/Arresto/ActualizarStatusFinalizado
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatusFinalizado([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.Arrestos.FirstOrDefaultAsync(a => a.IdArresto == model.IdArresto);

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
