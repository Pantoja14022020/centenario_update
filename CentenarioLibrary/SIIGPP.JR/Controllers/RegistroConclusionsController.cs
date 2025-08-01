using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_JR.RRegistro;
using SIIGPP.JR.Models.RRegistro;

namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroConclusionsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public RegistroConclusionsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/RegistroConclusions
        [HttpGet]
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")] 
        public IEnumerable<RegistroConclusion> GetRegistroConclusions()
        {
            return _context.RegistroConclusions;
        }

    
        // GET: api/RegistroConclusions/ListarRegistros
        [HttpGet("[action]/{EnvioId}")]
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        public async Task<IActionResult> ListarRegistros([FromRoute] Guid EnvioId)
        {
            var Tabla = await _context.RegistroConclusions
                                .Where(a => a.EnvioId == EnvioId)
                                .Include(a => a.Envio)
                                .FirstOrDefaultAsync();

            if (Tabla == null)
            {

                return Ok(new { ner = 1 });
            }
            return Ok(new GET_RegistroConclusionViewModel
            {
                IdRegistroConclusion = Tabla.IdRegistroConclusion,
                EnvioId = Tabla.EnvioId,
                Conclusion = Tabla.Conclusion,
                Asunto = Tabla.Asunto,
                Texto = Tabla.Texto,
                FechaHora = Tabla.FechaHora,
                Solicitates = Tabla.Solicitates,
                Reuqeridos = Tabla.Reuqeridos,
                StatusGeneral = Tabla.StatusGeneral,
                uf_Distrito = Tabla.uf_Distrito,
                uf_DirSubProc = Tabla.uf_DirSubProc,
                uf_Agencia = Tabla.uf_Agencia,
                uf_Modulo = Tabla.uf_Modulo,
                uf_Nombre = Tabla.uf_Nombre,
                uf_Puesto = Tabla.uf_Puesto,
            });
        }
        // POST: api/RegistroConclusions/Crear
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] POST_RegistroConclusionViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //************************************************************

                var FechaHoraSys = DateTime.Now;

                RegistroConclusion rc = new RegistroConclusion
                {
                    EnvioId = model.EnvioId, 
                    Asunto = model.Asunto,
                    Conclusion = model.Conclusion,
                    Texto = model.Texto,
                    FechaHora = FechaHoraSys,
                    Solicitates = model.Solicitates,
                    Reuqeridos = model.Reuqeridos,
                    StatusGeneral= model.StatusGeneral,
                    uf_Distrito = model.uf_Distrito,
                    uf_DirSubProc = model.uf_DirSubProc,
                    uf_Agencia = model.uf_Agencia,
                    uf_Modulo = model.uf_Modulo,
                    uf_Nombre = model.uf_Nombre,
                    uf_Puesto = model.uf_Puesto, 

                };

                _context.RegistroConclusions.Add(rc);




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
        private bool RegistroConclusionExists(Guid id)
        {
            return _context.RegistroConclusions.Any(e => e.IdRegistroConclusion == id);
        }
    }
}