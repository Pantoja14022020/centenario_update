using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Catalogos.Sexo;
using SIIGPP.Datos; 
using SIIGPP.Entidades.M_Configuracion.Cat_Generales;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RangoedadController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public RangoedadController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Sexoes/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            var rango = await _context.RangoEdads.OrderBy(a => a.OrdenEdad).ToListAsync();
            try
            {
                return Ok(rango.Select(a => new RangoEdadViewModel
                {
                    IdRangoEdad = a.IdRangoEdad,
                    Rango = a.Rango,
                    Activo = a.Activo,
                    OrdenEdad =a.OrdenEdad
                    
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