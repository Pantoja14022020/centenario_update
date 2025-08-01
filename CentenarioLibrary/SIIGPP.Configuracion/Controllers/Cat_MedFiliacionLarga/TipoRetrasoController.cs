using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_MedFiliacionLarga;
using SIIGPP.Configuracion.Models.Cat_MedFiliacionLarga.TipoRetraso;
using SIIGPP.Datos;

namespace SIIGPP.Configuracion.Controllers.Cat_MedFiliacionLarga
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoRetrasoController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public TipoRetrasoController(DbContextSIIGPP context)
        {
            _context = context;
        }

        //GET: api/TipoRetraso/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TipoRetrasoViewModel>> Listar()
        {
            var retraso = await _context.TipoRetrasos.ToListAsync();

            return retraso.Select(a => new TipoRetrasoViewModel
            {
                IdTipoDeRetraso = a.IdTipoDeRetraso,
                Dato = a.Dato,
            });
        }


    }
}
