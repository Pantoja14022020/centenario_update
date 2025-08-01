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
    public class UnasNoSaludablesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public UnasNoSaludablesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/UnasNoSaludables/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<UnasNoSaludablesViewModel>> Listar()
        {
            var ao = await _context.UnasNoSaludable
                .ToListAsync();

            return ao.Select(a => new UnasNoSaludablesViewModel
            {
                IdUñasNoSaludables = a.IdUñasNoSaludables,
                Dato = a.Dato,

        });

        }


       
    }
}
