using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_ConfGlobal;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_ConfiGlobal;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfGlobalsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public ConfGlobalsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/ConfGlobals/Listar
        [HttpGet("[action]")]
        
        public async Task<IActionResult> Listar()
        {
            var cg = await _context.ConfGlobals.FirstOrDefaultAsync();

            if (cg == null)
            {
                return NotFound();
            }
            return Ok (  new ConfGlobalViewModel
            {
               Logo1 = cg.Logo1,
               Logo2 = cg.Logo2,
               Logo3 = cg.Logo3,
               Logo4 = cg.Logo4
            });

        }


        private bool ConfGlobalExists(Guid id)
        {
            return _context.ConfGlobals.Any(e => e.IdConfGlobal == id);
        }
    }
}