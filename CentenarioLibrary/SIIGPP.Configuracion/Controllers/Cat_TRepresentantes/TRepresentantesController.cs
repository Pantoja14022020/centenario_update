using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_TRepresentantes;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_TRepresentantes;

namespace SIIGPP.Configuracion.Controllers.Cat_TRepresentantes
{
    [Route("api/[controller]")]
    [ApiController]
    public class TRepresentantesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public TRepresentantesController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/TRepresentantes/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TRepresentantesViewModel>> Listar()
        {
            var repre = await _context.TiposRepresentantes.ToListAsync();

            return repre.Select(a => new TRepresentantesViewModel
            {
               IdTipoRepresentantes = a.IdTipoRepresentantes,
               Tipo = a.Tipo,
               Valor = a.Valor
            });

        }

        // GET: api/TRepresentantes/listarrepresentantespersona
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{IdRepresentante}")]
        public async Task<IActionResult>listarrepresentantespersona([FromRoute] Guid idRepresentante)
        {
            try
            {
                String busquedaRepresentantesp = @"select 
                                                        t.IdTipoRepresentantes,
                                                        t.Tipo, 
                                                        t.Valor
                                                        from C_TIPOSREPRESENTANTES as t
                                                        left join CAT_REPRESENTANTES as r on t.Valor  = ABS(r.Tipo1) or t.Valor = ABS(r.Tipo2)
                                                        where 1=1
                                                        and r.IdRepresentante = @representanteId";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@representanteId", idRepresentante));
                var reprep = await _context.TiposRepresentantes.FromSqlRaw(busquedaRepresentantesp, filtrosBusqueda.ToArray()).ToListAsync();



                return Ok(reprep.Select(a => new TRepresentantesViewModel
                {

                    IdTipoRepresentantes = a.IdTipoRepresentantes,
                    Tipo = a.Tipo,
                    Valor = a.Valor

                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }


        // PUT: api/TRepresentantes/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

       

            var repre = await _context.TiposRepresentantes.FirstOrDefaultAsync(a => a.IdTipoRepresentantes == model.IdTipoRepresentantes);

            if (repre == null)
            {
                return NotFound();
            }

            repre.Tipo = model.Tipo;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }


        // POST: api/TRepresentantes/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TiposRepresentantes repre = new TiposRepresentantes
            {
                Tipo = model.Tipo,
                Valor = model.Valor

            };

            _context.TiposRepresentantes.Add(repre);
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

        // GET: api/TRepresentantes/ObtenernumeroMaximo
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenernumeroMaximo()
        {
            var da = await _context.TiposRepresentantes
                .OrderByDescending(x => x.Valor)
                .FirstOrDefaultAsync();

            if (da == null)
            {
                return Ok(new { NumeroMaximo = 0 });
            }

            return Ok(new DatosExtrasViewModel
            {
                NumeroMaximo = da.Valor

            });

        }





    }
}
