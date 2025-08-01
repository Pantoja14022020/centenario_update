using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.OAprhensionBitacoras;
using SIIGPP.Entidades.M_PI.OAprhensionBitacoras;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAprhensionBitacoraController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public OAprhensionBitacoraController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/OAprhensionBitacora/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{OrdenAprensionId}")]
        public async Task<IEnumerable<OAprhensionBitacoraViewModel>> Listar([FromRoute] Guid OrdenAprensionId)
        {
            var Orden = await _context.OAprhensionBitacoras
                .Where(a => a.OrdenAprensionId == OrdenAprensionId)
                .ToListAsync();
             
            return Orden.Select(a => new OAprhensionBitacoraViewModel
            {
                IdOAprhensionBitacora = a.IdOAprhensionBitacora,
                OrdenAprensionId = a.OrdenAprensionId,
                Texto = a.Texto,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                FechaSys = a.FechaSys,
            });

        }

        // POST: api/OAprhensionBitacora/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             OAprhensionBitacora Apre = new OAprhensionBitacora
            {
                OrdenAprensionId = model.OrdenAprensionId,
                Texto = model.Texto,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,

                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                FechaSys = System.DateTime.Now,
            };

            _context.OAprhensionBitacoras.Add(Apre);

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
