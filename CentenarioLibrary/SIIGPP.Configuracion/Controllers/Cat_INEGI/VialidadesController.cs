using Microsoft.AspNetCore.Mvc;
using SIIGPP.Datos;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_INEGI.Vialidad;

namespace SIIGPP.Configuracion.Controllers.Cat_INEGI
{
    [Route("api/[controller]")]
    [ApiController]
    public class VialidadesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public VialidadesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Vialidades/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var vialidades = await _context.Vialidades.OrderBy(a => a.Nombre).ToListAsync();

                return Ok(vialidades.Select(a => new VialidadViewModel
                {
                    IdVialidad = a.IdVialidad,
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
