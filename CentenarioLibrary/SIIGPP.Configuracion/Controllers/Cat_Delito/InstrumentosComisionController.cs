using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using SIIGPP.Datos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Delito.InstrumentoComision;

namespace SIIGPP.Configuracion.Controllers.Cat_Delito
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentosComisionController : Controller
    {
        private readonly DbContextSIIGPP _context;

        public InstrumentosComisionController(DbContextSIIGPP context)
        {
            _context = context;
        }
        //GET: api/RDHs/ListarInstrumentosComision
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-IL,Recepción")]
        [HttpGet("[action]")]
        public async Task<IActionResult> ListarInstrumentosC()
        {
            try
            {
                var instrumentos = await _context.InstrumentosComision
                                   .OrderBy(a => a.NombreInstrumento)
                                   .ToListAsync();

                return Ok(instrumentos.Select(a => new InstrumentoComisionViewModel
                {
                    IdInstrumentoComision = a.IdInstrumentoComision,
                    NombreInstrumento = a.NombreInstrumento
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

    }
}
