using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.EstatusCustodias;
using SIIGPP.Entidades.M_PI.EstatusCustodias;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatusCustodiaController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public EstatusCustodiaController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/EstatusCustodia/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{DetencionId}")]
        public async Task<IEnumerable<EstatusCustodiaViewModel>> Listar([FromRoute] Guid DetencionId)
        {
            var Est = await _context.EstatusCustodias
                .Where(a => a.DetencionId == DetencionId)
                .ToListAsync();

            return Est.Select(a => new EstatusCustodiaViewModel
            {

                IdEstatusCustodia = a.IdEstatusCustodia,
                DetencionId = a.DetencionId,
                Calle = a.Calle,
                NoExterior = a.NoExterior,
                NoInterior = a.NoInterior,
                Ecalle1 = a.Ecalle1,
                Ecalle2 = a.Ecalle2,
                Referencia = a.Referencia,
                Pais = a.Pais,
                Estado = a.Estado,
                Municipio = a.Municipio,
                Localidad = a.Localidad,
                Cp = a.Cp,
                Longitud = a.Longitud,
                Latitud = a.Localidad,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                ElementoAsignado = a.ElementoAsignado,
                FechaInicio = a.FechaInicio,
                FechaTermino = a.FechaTermino,
                Horainicio = a.Horainicio,
                HoraTermino = a.HoraTermino,
                Motivo = a.Motivo,
                Observaciones = a.Observaciones,
                Origen = a.Origen,
                Destino = a.Destino,
                HoraSalida = a.HoraSalida,
                HoraLLegada = a.HoraLLegada,
                Ruta = a.Ruta,
                Tipo = a.Tipo


            });

        }

        // POST: api/EstatusCustodia/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EstatusCustodia Est = new EstatusCustodia
            {
                DetencionId = model.DetencionId,
                Calle = model.Calle,
                NoExterior = model.NoExterior,
                NoInterior = model.NoInterior,
                Ecalle1 = model.Ecalle1,
                Ecalle2 = model.Ecalle2,
                Referencia = model.Referencia,
                Pais = model.Pais,
                Estado = model.Estado,
                Municipio = model.Municipio,
                Localidad = model.Localidad,
                Cp = model.Cp,
                Longitud = model.Longitud,
                Latitud = model.Latitud,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                ElementoAsignado = model.ElementoAsignado,
                FechaInicio = model.FechaInicio,
                FechaTermino = model.FechaTermino,
                Horainicio = model.Horainicio,
                HoraTermino = model.HoraTermino,
                Motivo = model.Motivo,
                Observaciones = model.Observaciones,
                Fechasys = System.DateTime.Now,
                Origen = model.Origen,
                Destino = model.Destino,
                HoraSalida = model.HoraSalida,
                HoraLLegada = model.HoraLLegada,
                Ruta = model.Ruta,
                Tipo = model.Tipo,
            };

            _context.EstatusCustodias.Add(Est);
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

        // PUT: api/EstatusCustodia/Actualizarhorallegada
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizarhorallegada([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

    

            var com = await _context.EstatusCustodias.FirstOrDefaultAsync(a => a.IdEstatusCustodia == model.IdEstatusCustodia);

            if (com == null)
            {
                return NotFound();
            }
            com.HoraLLegada = model.Horallegada;

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



    }
}
