using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.Terminacion;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Terminacion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminacionController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public TerminacionController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Terminacion/Determinacioneslistarporid
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<DeterminacionViewModel>> Determinacioneslistarporid([FromRoute] Guid RHechoId)
        {
            var deter = await _context.DeterminacionArchivos
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return deter.Select(a => new DeterminacionViewModel
            {
                RHechoId = a.RHechoId,
                Municipio = a.Municipio,
                Fecha = a.Fecha,
                MunicionEstado = a.MunicionEstado,
                FechaIHecho = a.FechaIHecho,
                MedioDenuncia = a.MedioDenuncia,
                ClasificacionPersona = a.ClasificacionPersona,
                Delito = a.Delito,
                Articulos = a.Articulos,
                Aifr = a.Aifr,
                Opcion1 = a.Opcion1,
                Opcion2 = a.Opcion2,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UUAgencia = a.UUAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                NumeroOficio = a.NumeroOficio

            });

        }


        // POST: api/Terminacion/CrearDeterminacion
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearDeterminacion([FromBody] CrearDeterminacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;
            DeterminacionArchivo determinacion = new DeterminacionArchivo
            {
                RHechoId = model.RHechoId,
                Municipio = model.Municipio,
                Fecha = model.Fecha,
                MunicionEstado = model.MunicionEstado,
                FechaIHecho = model.FechaIHecho,
                MedioDenuncia = model.MedioDenuncia,
                ClasificacionPersona = model.ClasificacionPersona,
                Delito = model.Delito,
                Articulos = model.Articulos,
                Aifr = model.Aifr,
                Opcion1 = model.Opcion1,
                Opcion2 = model.Opcion2,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UUAgencia = model.UUAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = fecha,
                NumeroOficio = model.NumeroOficio
            };

            _context.DeterminacionArchivos.Add(determinacion);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok();
        }

        // GET: api/Terminacion/IncompetencialistarporId
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<RIncompetenciaViewModel>> IncompetencialistarporId([FromRoute] Guid RHechoId)
        {
            var incom = await _context.RInconpentencias
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return incom.Select(a => new RIncompetenciaViewModel
            {
                IdInconpentencia = a.IdInconpentencia,
                RHechoId = a.RHechoId,
                TextoFinal = a.TextoFinal,
                Fecha = a.Fecha,
                RBreve = a.RBreve,
                Dependencia = a.Dependencia,
                Articulos = a.Articulos,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UUAgencia = a.UUAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                NumeroOficio = a.NumeroOficio

            });

        }

        // POST: api/Terminacion/CrearIncompetencia
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearIncompetencia([FromBody] CrearRIncompetenciaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            RInconpentencia incom = new RInconpentencia
            {
                RHechoId = model.RHechoId, 
                TextoFinal = model.TextoFinal,
                Fecha = model.Fecha,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UUAgencia = model.UUAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = fecha,
                RBreve = model.RBreve,
                Dependencia = model.Dependencia,
                Articulos = model.Articulos,
                NumeroOficio = model.NumeroOficio
            };

            _context.RInconpentencias.Add(incom);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
