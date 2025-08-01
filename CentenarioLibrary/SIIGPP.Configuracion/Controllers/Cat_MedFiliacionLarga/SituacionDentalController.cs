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
    public class SituacionDentalController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public SituacionDentalController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/SituacionDental/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SituacionDentalViewModel>> Listar()
        {
            var ao = await _context.SituacionDentals
                .ToListAsync();

            return ao.Select(a => new SituacionDentalViewModel
            {
                IdSituacionDental = a.IdSituacionDental,
                Dato = a.Dato,

        });

        }


       
    }
}
