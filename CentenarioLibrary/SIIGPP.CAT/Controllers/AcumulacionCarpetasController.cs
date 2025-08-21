using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.AcumulacionCarpetas;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.AcumulacionCarpetas;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcumulacionCarpetasController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public AcumulacionCarpetasController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // POST: api/AcumulacionCarpetas/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AcumulacionCarpeta AC = new AcumulacionCarpeta
            {
                RHechoId = model.RHechoId,
                NUCFusion = model.NUCFusion,
                RhechoIdFusion = model.RhechoIdFusion,               
                FechaSys = System.DateTime.Now,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                UUsuario = model.UUsuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo

            };

            _context.AcumulacionCarpetas.Add(AC);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException.Message, version = "version 1.3" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();
        }



        // GET: api/AcumulacionCarpetas/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Procurador, AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<AcumulacionCarpetasViewModel>> Listar([FromRoute] Guid RHechoId)
        {
            var AC = await _context.AcumulacionCarpetas
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.FechaSys)
                .ToListAsync();

            return AC.Select(a => new AcumulacionCarpetasViewModel
            {
                IdAcumulacionCarpeta = a.IdAcumulacionCarpeta,
                RHechoId = a.RHechoId,
                NUCFusion = a.NUCFusion,
                RhechoIdFusion = a.RhechoIdFusion,
                FechaSys = a.FechaSys,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                UUsuario = a.UUsuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo
            });

        }


    }
}
