using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.IL.Models.SolicitudesInformacion;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_IL.SolicitudesInformacion;
namespace SIIGPP.IL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudInformacionController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public SolicitudInformacionController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/SolicitudInformacion/Listar
        [HttpGet("[action]/{Idrhecho}")]
        public async Task<IEnumerable<SolicitudInformacionViewModel>> Listar([FromRoute] Guid Idrhecho)
        {
            var ao = await _context.solicitudInformacions
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.RHechoId == Idrhecho)
                .ToListAsync();

            return ao.Select(a => new SolicitudInformacionViewModel
            {
                IdSolicitudInfo = a.IdSolicitudInfo,
                CodOficio = a.CodOficio,
                RHechoId = a.RHechoId,
                Imputado =a.Imputado,
                CURP =a.CURP,
                Fnacimiento =a.Fnacimiento,
                Correo = a.Correo,
                TipoDoc = a.TipoDoc,
                Sobr = a.Sobr,
                Unico = a.Unico,
                CausaPenal = a.CausaPenal,
                Nuc = a.Nuc,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                Dirigidoa = a.Dirigidoa,
                Puesto = a.Puesto,
            });
        }

        // POST: api/SolicitudInformacion/Crear
       // [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SolicitudInformacion ag = new SolicitudInformacion
            {

                CodOficio = model.CodOficio,
                RHechoId = model.RHechoId,
                Imputado = model.Imputado,
                CURP = model.CURP,
                Fnacimiento = model.Fnacimiento,
                Correo = model.Correo,
                TipoDoc = model.TipoDoc,
                Sobr = model.Sobr,
                Unico = model.Unico,
                CausaPenal = model.CausaPenal,
                Nuc = model.Nuc,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
                Dirigidoa = model.Dirigidoa,
                Puesto = model.Puesto,
            };

            _context.solicitudInformacions.Add(ag);
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
    }
}