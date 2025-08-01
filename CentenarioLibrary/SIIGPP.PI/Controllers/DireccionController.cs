using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.Direcciones;
using SIIGPP.Entidades.M_PI.Direcciones;
using Microsoft.AspNetCore.Hosting;


namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public DireccionController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Direccion/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{PIPersonaVisitaId}")]
        public async Task<IActionResult> Listar([FromRoute] Guid PIPersonaVisitaId)
        {
            var Dir = await _context.Direccions
                .Where(a => a.PIPersonaVisitaId == PIPersonaVisitaId)
                .FirstOrDefaultAsync();

            if (Dir == null)
            {
                return BadRequest("No hay registros");

            }

            return Ok(new DireccionViewModel
            { 
                IdDireccion = Dir.IdDireccion,
                PIPersonaVisitaId = Dir.PIPersonaVisitaId,
                Calle = Dir.Calle,
                NoExterior = Dir.NoExterior,
                NoInterior = Dir.NoInterior,
                Ecalle1 = Dir.Ecalle1,
                Ecalle2  = Dir.Ecalle2,
                Referencia = Dir.Referencia,
                Pais = Dir.Pais,
                Estado = Dir.Estado,
                Municipio = Dir.Municipio,
                Localidad = Dir.Localidad,
                Cp = Dir.Cp,
                Longitud = Dir.Longitud,
                Latitud = Dir.Latitud,
                UDistrito = Dir.UDistrito,
                USubproc = Dir.USubproc,
                UAgencia = Dir.UAgencia,
                Usuario = Dir.Usuario,
                UPuesto = Dir.UPuesto,
                UModulo = Dir.UModulo,
                Fechasys = Dir.Fechasys,
            });

        }


        // POST: api/Direccion/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Direccion Dir = new Direccion
            {
                PIPersonaVisitaId = model.PIPersonaVisitaId,
                Calle = model.Calle,
                NoExterior = model.NoExterior,
                NoInterior = model.NoInterior,
                Ecalle1 = model.Ecalle1,
                Ecalle2 = model.Ecalle2,
                Referencia = model.Referencia, 
                Pais = model.Pais,
                Estado =model.Estado,
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
                Fechasys = System.DateTime.Now,
            };

            _context.Direccions.Add(Dir);

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
