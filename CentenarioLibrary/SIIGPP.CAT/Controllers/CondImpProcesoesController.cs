using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.ImpProceso;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.ImpProceso;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondImpProcesoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public CondImpProcesoesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/CondImpProcesoes
        [HttpGet]
        public IEnumerable<CondImpProceso> GetCondImpProcesos()
        {
            return _context.CondImpProcesos;
        }

        // GET: api/CondImpProcesoes/5
        // GET: api/MediaAfiliacions/ListarPorRHecho{RHechoId}
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{PersonaId}")]
        public async Task<IEnumerable<CondImpProcesoViewModel>> ListarPorRhecho([FromRoute] Guid RHechoId)
        {
            var cip = await _context.CondImpProcesos
                          .Include(a => a.Persona)
                          .Include(a => a.Persona.RAPs)
                          .Where(a => a.RHechoId == RHechoId).ToListAsync();

            return cip.Select(a => new CondImpProcesoViewModel
            {
                idConImpProceso = a.idConImpProceso,
                PersonaId = a.PersonaId,
                NombreImputado = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                RHechoId = a.RHechoId,
                ConduccionImputadoProceso = a.ConduccionImputadoProceso,
                FechaHoraCitacion = a.FechaHoraCitacion,
                FechahoraComparecencia =a.FechahoraComparecencia,
                OrdeAprehension = a.OrdeAprehension,
                FechaHoraAudienciaOrdenAprehencion = a.FechaHoraAudienciaOrdenAprehencion,
                AutoridadEjecutora = a.AutoridadEjecutora,
                ResultadoOrdenAprehension = a.ResultadoOrdenAprehension,
                FechaHoraEjecucionOrdenAprehecion = a.FechaHoraEjecucionOrdenAprehecion,
                FechaHoraCancelacionOrdenAprehecion = a.FechaHoraCancelacionOrdenAprehecion,
                OrdenReaprehesion = a.OrdenReaprehesion,
                PlazoResolverSituacionJuridica =a.PlazoResolverSituacionJuridica,
                FechaSys = a.FechaSys,
                Distrito = a.Distrito,
                DirSubProc = a.DirSubProc,
                Agencia = a.Agencia,
                Usuario = a.Usuario,
                Puesto = a.Usuario,



            });

        }

        // PUT: api/MediaAfiliacions//Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cip = await _context.CondImpProcesos.FirstOrDefaultAsync(a => a.idConImpProceso == model.idConImpProceso);

            if (cip == null)
            {
                return NotFound();
            }


            cip.PersonaId = model.PersonaId;
            cip.RHechoId = model.RHechoId;
            cip.ConduccionImputadoProceso = model.ConduccionImputadoProceso;
            cip.FechaHoraCitacion = model.FechaHoraCitacion;
            cip.FechahoraComparecencia = model.FechahoraComparecencia;
            cip.OrdeAprehension = model.OrdeAprehension;
            cip.FechaHoraAudienciaOrdenAprehencion = model.FechaHoraAudienciaOrdenAprehencion;
            cip.AutoridadEjecutora = model.AutoridadEjecutora;
            cip.ResultadoOrdenAprehension = model.ResultadoOrdenAprehension;
            cip.FechaHoraEjecucionOrdenAprehecion = model.FechaHoraEjecucionOrdenAprehecion;
            cip.FechaHoraCancelacionOrdenAprehecion = model.FechaHoraCancelacionOrdenAprehecion;
            cip.OrdenReaprehesion = model.OrdenReaprehesion;
            cip.PlazoResolverSituacionJuridica = model.PlazoResolverSituacionJuridica;
            cip.FechaSys = model.FechaSys;
            cip.Distrito = model.Distrito;
            cip.DirSubProc = model.DirSubProc;
            cip.Agencia = model.Agencia;
            cip.Usuario = model.Usuario;
            cip.Puesto = model.Usuario;

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

        // POST: api/MediaAfiliacions/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CondImpProceso cip = new CondImpProceso
            {
                PersonaId = model.PersonaId,
                RHechoId = model.RHechoId,
                ConduccionImputadoProceso = model.ConduccionImputadoProceso,
                FechaHoraCitacion = model.FechaHoraCitacion,
                FechahoraComparecencia = model.FechahoraComparecencia,
                OrdeAprehension = model.OrdeAprehension,
                FechaHoraAudienciaOrdenAprehencion = model.FechaHoraAudienciaOrdenAprehencion,
                AutoridadEjecutora = model.AutoridadEjecutora,
                ResultadoOrdenAprehension = model.ResultadoOrdenAprehension,
                FechaHoraEjecucionOrdenAprehecion = model.FechaHoraEjecucionOrdenAprehecion,
                FechaHoraCancelacionOrdenAprehecion = model.FechaHoraCancelacionOrdenAprehecion,
                OrdenReaprehesion = model.OrdenReaprehesion,
                PlazoResolverSituacionJuridica = model.PlazoResolverSituacionJuridica,
                FechaSys = model.FechaSys,
                Distrito = model.Distrito,
                DirSubProc = model.DirSubProc,
                Agencia = model.Agencia,
                Usuario = model.Usuario,
                Puesto = model.Usuario,

            };

            _context.CondImpProcesos.Add(cip);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.2" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();
        }
        private bool CondImpProcesoExists(Guid id)
        {
            return _context.CondImpProcesos.Any(e => e.idConImpProceso == id);
        }
    }
}