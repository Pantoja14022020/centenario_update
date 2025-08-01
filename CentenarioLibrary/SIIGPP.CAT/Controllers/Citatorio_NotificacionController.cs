using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Citatorio_Notificacion;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.Citatorio_Notificacion;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Citatorio_NotificacionController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;
        public Citatorio_NotificacionController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // POST: api/Citatorio_Notificacion/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {

            Guid idcitatorio_notificacion;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            RCitatorio_Notificacion citnot = new RCitatorio_Notificacion
            {
                RHechoId = model.RHechoId,
                NombrePersona = model.NombrePersona,
                DomicilioPersona = model.DomicilioPersona,
                ReferenciaPersona = model.ReferenciaPersona,
                TelefonoPersona = model.TelefonoPersona,
                LugarCita = model.LugarCita,
                FechaCita = model.FechaCita,
                Nuc = model.Nuc,
                Delito = model.Delito,
                Hora = model.Hora,
                Descripcion = model.Descripcion,
                FechaReporte = model.FechaReporte,
                FechaSis = fecha,
                Tipo = model.Tipo,
                Distrito = model.Distrito,
                Subproc = model.Subproc,
                Agencia = model.Agencia,
                Usuario = model.Usuario,
                Puesto = model.Puesto,
                Textofinal = model.Textofinal,
                Modulo = model.Modulo,
                NumeroOficio = model.NumeroOficio
              


            };

            _context.RCitatorio_Notificacions.Add(citnot);
            try
            {
                await _context.SaveChangesAsync();
                idcitatorio_notificacion = citnot.IdCitatorio_Notificacion;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new {idcitatorio_notificacion = idcitatorio_notificacion});
        }

        // GET: api/Citatorio_Notificacion/Listar
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}/{Tipo}")]
        public async Task<IEnumerable<Citatorio_NotificacionViewModel>> Listar([FromRoute]Guid RHechoId, Boolean Tipo)
        {
            var CitNot = await _context.RCitatorio_Notificacions
                .Where (a=> a.Tipo == Tipo)
                .Where(a=> a.RHechoId == RHechoId)
                .OrderByDescending(a => a.FechaSis)
                .ToListAsync();

            return CitNot.Select(a => new Citatorio_NotificacionViewModel
            {
                IdCitatorio_Notificacion = a.IdCitatorio_Notificacion,
                RHechoId = a.RHechoId,
                NombrePersona = a.NombrePersona,
                DomicilioPersona = a.DomicilioPersona,
                ReferenciaPersona = a.ReferenciaPersona,
                TelefonoPersona = a.TelefonoPersona,
                LugarCita = a.LugarCita,
                FechaCita = a.FechaCita,
                Nuc = a.Nuc,
                Hora = a.Hora,
                Delito = a.Delito,
                Descripcion = a.Descripcion,
                FechaReporte = a.FechaReporte,
                FechaSis = a.FechaSis,
                Tipo = a.Tipo,
                Distrito = a.Distrito,
                Subproc = a.Subproc,
                Agencia = a.Agencia,
                Usuario = a.Usuario,
                Puesto = a.Puesto,
                Textofinal = a.Textofinal,
                Modulo = a.Modulo,
                NumeroOficio = a.NumeroOficio
            });

        }

        // GET: api/Citatorio_Notificacion/ListarAmbas
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<Citatorio_NotificacionViewModel>> ListarAmbas([FromRoute] Guid RHechoId)
        {
            var CitNot = await _context.RCitatorio_Notificacions
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.FechaSis)
                .ToListAsync();

            return CitNot.Select(a => new Citatorio_NotificacionViewModel
            {
                IdCitatorio_Notificacion = a.IdCitatorio_Notificacion,
                RHechoId = a.RHechoId,
                NombrePersona = a.NombrePersona,
                DomicilioPersona = a.DomicilioPersona,
                ReferenciaPersona = a.ReferenciaPersona,
                TelefonoPersona = a.TelefonoPersona,
                LugarCita = a.LugarCita,
                FechaCita = a.FechaCita,
                Nuc = a.Nuc,
                Hora = a.Hora,
                Delito = a.Delito,
                Descripcion = a.Descripcion,
                FechaReporte = a.FechaReporte,
                FechaSis = a.FechaSis,
                Tipo = a.Tipo,
                Distrito = a.Distrito,
                Subproc = a.Subproc,
                Agencia = a.Agencia,
                Usuario = a.Usuario,
                Puesto = a.Puesto,
                Textofinal = a.Textofinal,
                Modulo = a.Modulo,
                NumeroOficio = a.NumeroOficio
            });

        }

        // PUT: api/Citatorio_Notificacion/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

         

            var citnot = await _context.RCitatorio_Notificacions.FirstOrDefaultAsync(a => a.IdCitatorio_Notificacion == model.IdCitatorio_Notificacion);

            if (citnot == null)
            {
                return NotFound();
            }

            citnot.NombrePersona = model.NombrePersona;
            citnot.LugarCita = model.LugarCita;
            citnot.Delito = model.Delito;
            citnot.FechaCita = model.FechaCita;
            citnot.Hora = model.Hora;
            citnot.Descripcion = model.Descripcion;
            citnot.Textofinal = model.Textofinal;
            citnot.ReferenciaPersona = model.ReferenciaPersona;
            citnot.TelefonoPersona = model.TelefonoPersona;
            citnot.DomicilioPersona = model.DomicilioPersona;

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

        // GET: api/Citatorio_Notificacion/Eliminar
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var consultaNotificacion = await _context.RCitatorio_Notificacions.Where(a => a.IdCitatorio_Notificacion == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaNotificacion == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningún acto de investigación con la información enviada" });
                }
                else
                {
                    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("LOG")).Options;
                    using (var ctx = new DbContextSIIGPP(options))
                    {
                        Guid gLog = Guid.NewGuid();
                        DateTime fecha = System.DateTime.Now;
                        LogAdmon laRegistro = new LogAdmon
                        {
                            IdLogAdmon = gLog,
                            UsuarioId = model.usuario,
                            Tabla = model.tabla,
                            FechaMov = fecha,
                            RegistroId = model.infoBorrado.registroId,
                            SolicitanteId = model.solicitante,
                            RazonMov = model.razon,
                            MovimientoId = new Guid("ed088970-43f8-4a67-983a-a17771667b1c") // ESTE DATO HAY QUE BUSCARLO EN LA TABLA C_TIPO_MOV DE LA BD CENTENARIO_LOG
                        };

                        ctx.Add(laRegistro);
                        LogRCitatorioNotificacion notificacion = new LogRCitatorioNotificacion
                        {
                            LogAdmonId = gLog,
                            IdCitatorio_Notificacion = consultaNotificacion.IdCitatorio_Notificacion,
                            RHechoId = consultaNotificacion.RHechoId,
                            NombrePersona = consultaNotificacion.NombrePersona,
                            DomicilioPersona = consultaNotificacion.DomicilioPersona,
                            ReferenciaPersona = consultaNotificacion.ReferenciaPersona,
                            TelefonoPersona = consultaNotificacion.TelefonoPersona,
                            LugarCita = consultaNotificacion.LugarCita,
                            FechaCita = consultaNotificacion.FechaCita,
                            Nuc = consultaNotificacion.Nuc,
                            Delito = consultaNotificacion.Delito,
                            Hora = consultaNotificacion.Hora,
                            Descripcion = consultaNotificacion.Descripcion,
                            FechaReporte = consultaNotificacion.FechaReporte,
                            FechaSis = consultaNotificacion.FechaSis,
                            Tipo = consultaNotificacion.Tipo,
                            Distrito = consultaNotificacion.Distrito,
                            Subproc = consultaNotificacion.Subproc,
                            Agencia = consultaNotificacion.Agencia,
                            Usuario = consultaNotificacion.Usuario,
                            Puesto = consultaNotificacion.Puesto,
                            Textofinal = consultaNotificacion.Textofinal,
                            Modulo = consultaNotificacion.Modulo,
                            NumeroOficio = consultaNotificacion.NumeroOficio

                        };
                        ctx.Add(notificacion);
                        _context.Remove(consultaNotificacion);

                        //SE APLICAN TODOS LOS CAMBIOS A LA BD
                        await ctx.SaveChangesAsync();
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "Citatorio/Notificacion eliminado Correctamente" });
        }
    }
}
