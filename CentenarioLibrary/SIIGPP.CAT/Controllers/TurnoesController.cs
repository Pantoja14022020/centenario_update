using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Turnador;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Turnador;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public TurnoesController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //GET: api/Turnoes/ListarPorAgencia/1 
        //Regresa el ultimo turno   creado
        [Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,Administrador,Recepción")]
        [HttpGet("[action]/{idAgencia}/{idModuloServicio}")]

        public async Task<IActionResult> ListarPorAgencia([FromRoute] Guid idAgencia, Guid idModuloServicio)
        {
            DateTime fecha = System.DateTime.Now;

            var turnore = await _context.AmpoTurnos
                             .Where(a => a.Turno.AgenciaId == idAgencia)
                             .Where(a => a.Turno.FechaHoraInicio.ToString("dd/MM/yyyy") == fecha.ToString("dd/MM/yyyy"))
                             .Where(a => a.Turno.Status == true)
                             .Where(a => a.Turno.StatusReAsignado == true)
                             .Where(a => a.ModuloServicioId == idModuloServicio)
                             .Include(a => a.Turno)
                             .OrderBy(a => a.Turno.NoTurno)
                             .Take(1)
                             .FirstOrDefaultAsync();

            if (turnore == null)
            {
                //***********************************************
                var turno = await _context.Turnos
                                 .Where(a => a.AgenciaId == idAgencia)
                                 .Where(a => a.FechaHoraInicio.ToString("dd/MM/yyyy") == fecha.ToString("dd/MM/yyyy"))
                                 .Where(a => a.Status == false)
                                 .OrderBy(a => a.NoTurno)
                                 .Take(1)
                                 .FirstOrDefaultAsync();

                if (turno == null)
                {
                    return NotFound("No hay turnos en espera");
                }
                return Ok(new TurnoViewModel
                {

                    idTurno = turno.IdTurno,
                    serie = turno.Serie,
                    noturno = turno.NoTurno,
                    fechaHoraInicio = turno.FechaHoraInicio,
                    status = turno.Status,
                    statusReAsignado = turno.StatusReAsignado,
                    AgenciaId = turno.AgenciaId,
                    rAtencionId = turno.RAtencionId

                });
                //***********************************************
            }


            return Ok(new TurnoViewModel
            {

                idTurno = turnore.Turno.IdTurno,
                serie = turnore.Turno.Serie,
                noturno = turnore.Turno.NoTurno,
                fechaHoraInicio = turnore.Turno.FechaHoraInicio,
                status = turnore.Turno.Status,
                AgenciaId = turnore.Turno.AgenciaId,
                rAtencionId = turnore.Turno.RAtencionId

            });

        }

        // GET: api/Turnoes/ListarTodos
        [Authorize(Roles = " Director,Administrador")]
        [HttpGet("[action]/{idAgencia}")]
        public async Task<IEnumerable<ListarTurnosViewModel>> ListarTodos([FromRoute] Guid idAgencia)
        {
            DateTime fecha = System.DateTime.Now;
            var turnos = await _context.Turnos
                                 .Where(a => a.AgenciaId == idAgencia)
                                 .Where(a => a.FechaHoraInicio.ToString("dd/MM/yyyy") == fecha.ToString("dd/MM/yyyy"))
                                 .Include(a => a.AmpoTurnos)
                                 .Include(a => a.Agencia)
                                 .Include(a => a.RAtencion)
                                 .ToListAsync();

            return turnos.Select(a => new ListarTurnosViewModel
            {

                TurnoId = a.IdTurno,
                serie = a.Serie,
                noturno = a.NoTurno,
                fechaHoraInicio = a.FechaHoraInicio,
                status = a.Status,
                statusReAsignado = a.StatusReAsignado,
                AgenciaId = a.AgenciaId,
                nombreAgencia = a.Agencia.Nombre,
                rAtencionId = a.RAtencionId,
                atendidopor = a.RAtencion.u_Nombre,
                modulo = a.Modulo,





            });
        }
        // PUT: api/Turnoes/Actualizar
        [Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var turno = await _context.Turnos.FirstOrDefaultAsync(a => a.IdTurno == model.idTurno);

            if (turno == null)
            {
                return NotFound();
            }
            turno.Status = model.Status;
            turno.StatusReAsignado = false;

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
        // PUT: api/Turnoes/ActualizarSRA
        [Authorize(Roles = " Director,Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarSRA([FromBody] ActualizarStatusReasignadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var turno = await _context.Turnos.FirstOrDefaultAsync(a => a.IdTurno == model.idTurno);

            if (turno == null)
            {
                return NotFound();
            }
            turno.StatusReAsignado = model.statusReAsignado;


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


        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/Turnoes/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consultaTurno = await _context.Turnos
                               .Where(a => a.RAtencionId == model.IdRAtencion)
                               .Take(1)
                               .FirstOrDefaultAsync();

            if (consultaTurno == null)
            {
                return Ok();

            }



            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
            using (var ctx = new DbContextSIIGPP(options))
            {


                var elTurno = await ctx.Turnos.FirstOrDefaultAsync(a => a.IdTurno == consultaTurno.IdTurno);

                if (elTurno == null)
                {
                    elTurno = new Turno();
                    ctx.Turnos.Add(elTurno);
                }
                elTurno.IdTurno = consultaTurno.IdTurno;
                elTurno.Serie = consultaTurno.Serie;
                elTurno.NoTurno = consultaTurno.NoTurno;
                elTurno.FechaHoraInicio = consultaTurno.FechaHoraInicio;
                elTurno.FechaHoraFin = consultaTurno.FechaHoraFin;
                elTurno.Status = consultaTurno.Status;
                elTurno.RAtencionId = consultaTurno.RAtencionId;
                elTurno.AgenciaId = consultaTurno.AgenciaId;
                elTurno.StatusReAsignado = consultaTurno.StatusReAsignado;
                elTurno.Modulo = consultaTurno.Modulo;


                try
                {
                    await ctx.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                    result.StatusCode = 402;
                    return result;
                }
                return Ok();
            }



        }
    }
    }
