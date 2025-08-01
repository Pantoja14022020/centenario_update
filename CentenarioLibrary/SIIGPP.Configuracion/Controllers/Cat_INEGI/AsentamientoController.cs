using Microsoft.AspNetCore.Mvc;
using SIIGPP.Datos;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_INEGI.Asentamiento;

namespace SIIGPP.Configuracion.Controllers.Cat_INEGI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsentamientoController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public AsentamientoController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Asentamiento/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var asentamientos = await _context.Asentamientos.OrderBy(a => a.Nombre).ToListAsync();

                return Ok(asentamientos.Select(a => new AsentamientoViewModel
                {
                    IdAsentamiento = a.IdAsentamiento,
                    Nombre = a.Nombre,
                    Clave = a.Clave,
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, ExtraInfo = ex.Message });
                result.StatusCode = 402;
                return result;
            }
        }
    }
}
