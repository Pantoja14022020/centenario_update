using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_MedFiliacionLarga;
using SIIGPP.Datos;

namespace SIIGPP.Configuracion.Controllers.Cat_MedFiliacionLarga
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoLesionesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public TipoLesionesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/TipoLesiones/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TipoLesionesViewModel>> Listar()
        {
            var ao = await _context.TipoLesiones
                .ToListAsync();

            return ao.Select(a => new TipoLesionesViewModel
            {
                IdTipoDeLesiones = a.IdTipoDeLesiones,
                Dato = a.Dato,

        });

        }


       
    }
}
