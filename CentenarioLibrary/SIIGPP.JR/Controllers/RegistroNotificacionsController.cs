using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_JR.RNotificacion;
using SIIGPP.JR.Models.RNotificacion;

namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroNotificacionsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public RegistroNotificacionsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/RegistroNotificacions
        [HttpGet]
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        public IEnumerable<RegistroNotificacion> GetRegistroNotificacions()
        {
            return _context.RegistroNotificacions;
        }

        // GET: api/RegistroNotificacions/5
        [HttpGet("[action]/{ExpedienteId}")]
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        public async Task<IActionResult> ListarRegistros([FromRoute] Guid ExpedienteId)
        {
            var Tabla = await _context.RegistroNotificacions
                                .Where(a => a.ExpedienteId == ExpedienteId)
                                .Include(a => a.Expediente) 
                                .FirstOrDefaultAsync();

            if (Tabla == null)
            {

                return Ok(new { ner = 1 });
            }
            return Ok(new GET_RegistroNotificacionViewModel
            {
                IdRegistroNotificaciones = Tabla.IdRegistroNotificaciones,
                ExpedienteId = Tabla.ExpedienteId,
                Asunto = Tabla.Asunto,
                Texto = Tabla.Texto,
                FechaHora = Tabla.FechaHora,
                Solicitates = Tabla.Solicitates,
                Reuqeridos = Tabla.Reuqeridos,
                uf_Distrito = Tabla.uf_Distrito,
                uf_DirSubProc = Tabla.uf_DirSubProc,
                uf_Agencia = Tabla.uf_Agencia,
                uf_Modulo = Tabla.uf_Modulo,
                uf_Nombre = Tabla.uf_Nombre,
                uf_Puesto = Tabla.uf_Puesto,
            });
        }
        // POST: api/RegistroNotificacions/Crear
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] POST_RegistroNotificacionViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //************************************************************

                var FechaHoraSys = DateTime.Now;

                RegistroNotificacion rn = new RegistroNotificacion
                {
                    ExpedienteId = model.ExpedienteId,
                    Asunto = model.Asunto,
                    Texto = model.Texto,
                    FechaHora = FechaHoraSys,
                    Solicitates = model.Solicitates,
                    Reuqeridos = model.Reuqeridos,
                    uf_Distrito = model.uf_Distrito,
                    uf_DirSubProc = model.uf_DirSubProc,
                    uf_Agencia = model.uf_Agencia,
                    uf_Modulo = model.uf_Modulo,
                    uf_Nombre = model.uf_Nombre,
                    uf_Puesto = model.uf_Puesto,

                };

                _context.RegistroNotificacions.Add(rn);




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
        private bool RegistroNotificacionExists(Guid id)
        {
            return _context.RegistroNotificacions.Any(e => e.IdRegistroNotificaciones == id);
        }
    }
}