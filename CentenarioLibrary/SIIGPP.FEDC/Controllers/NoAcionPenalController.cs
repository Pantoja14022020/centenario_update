using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.FEDC.Models.NoAcionPenal;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_FEDC.NoAccionPenal;

namespace SIIGPP.FEDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoAcionPenalController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public NoAcionPenalController(DbContextSIIGPP context)
        {
            _context = context;
        }



        // GET: api/NoAcionPenal/ListarporRHecho
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<NoAcionPenalViewModel>> ListarporRHecho([FromRoute]Guid RHechoId)
        {
            var NEAP = await _context.NoAcionPenals
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.FechaSys)
                .ToListAsync();

            return NEAP.Select(a => new NoAcionPenalViewModel
            {

                IdNoAcionPenal = a.IdNoAcionPenal,
                RHechoId = a.RHechoId,
                NumeroOficio = a.NumeroOficio,
                Delitos = a.Delitos,
                Victimas = a.Victimas,
                Imputados = a.Imputados,
                Cosumacion = a.Cosumacion,
                AusenciaVOluntad = a.AusenciaVOluntad,
                CausasAtipicidad = a.CausasAtipicidad,
                FalteElementos = a.FalteElementos,
                EfectosCodigo = a.EfectosCodigo,
                Sobreseimiento = a.Sobreseimiento,
                HechocncDelito = a.HechocncDelito,
                Antecedente = a.Antecedente,
                Articulo25 = a.Articulo25,
                FechaSys = a.FechaSys,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                UUsuario = a.UUsuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo

            });

        }


        // POST: api/NoAcionPenal/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            NoAcionPenal NEAP = new NoAcionPenal
            {
                RHechoId = model.RHechoId,
                NumeroOficio = model.NumeroOficio,
                Delitos = model.Delitos,
                Victimas = model.Victimas,
                Imputados = model.Imputados,
                Cosumacion = model.Cosumacion,
                AusenciaVOluntad = model.AusenciaVOluntad,
                CausasAtipicidad = model.CausasAtipicidad,
                FalteElementos = model.FalteElementos,
                EfectosCodigo = model.EfectosCodigo,
                Sobreseimiento = model.Sobreseimiento,
                HechocncDelito = model.HechocncDelito,
                Antecedente = model.Antecedente,
                Articulo25 = model.Articulo25,
                FechaSys = fecha,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                UUsuario = model.UUsuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo
            };

            _context.NoAcionPenals.Add(NEAP);
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
