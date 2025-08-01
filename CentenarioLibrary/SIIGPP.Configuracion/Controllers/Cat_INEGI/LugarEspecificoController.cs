using Microsoft.AspNetCore.Mvc;
using SIIGPP.Datos;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_INEGI.Vialidad;
using SIIGPP.Configuracion.Models.Cat_INEGI.LugarEspecifico;

namespace SIIGPP.Configuracion.Controllers.Cat_INEGI
{
    [Route("api/[controller]")]
    [ApiController]
    public class LugarEspecificoController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public LugarEspecificoController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Lugarespecifico/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var lugar = await _context.LugaresEspecificos.OrderBy(a => a.Nombre).ToListAsync();

                return Ok(lugar.Select(a => new LugarEspecificoViewModel
                {
                    IdLugarEspecifico = a.IdLugarEspecifico,
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
