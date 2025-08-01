using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.PersonasVisita;
using SIIGPP.Entidades.M_PI.PersonasVisita;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaVisitaController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public PersonaVisitaController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/PersonaVisita/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaVisitaViewModel>> Listar()
        {
            var Per = await _context.PIPersonaVisitas   
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return Per.Select(a => new PersonaVisitaViewModel
            {
                IdPIPersonaVisita = a.IdPIPersonaVisita,
                Nombre = a.Nombre,
                ApellidoP = a.ApellidoP,
                ApellidoM  = a.ApellidoM,
                Edad =a.Edad,
                Ocupacion = a.Ocupacion,
                Telefono1 = a.Telefono1,
                Telefono2 = a.Telefono2,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario  = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
    });

        }

        // POST: api/PersonaVisita/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PIPersonaVisita Per = new PIPersonaVisita
            {
                Nombre = model.Nombre,
                ApellidoP = model.ApellidoP,
                ApellidoM = model.ApellidoM,
                Edad = model.Edad,
                Ocupacion = model.Ocupacion,
                Telefono1 =model.Telefono1,
                Telefono2 =model.Telefono2,
                UDistrito = model.UDistrito,
                USubproc =model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
            };

            _context.PIPersonaVisitas.Add(Per);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { idpersonavisita = Per.IdPIPersonaVisita });
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

#pragma warning disable CS0162 // Se detectó código inaccesible
            return Ok();
#pragma warning restore CS0162 // Se detectó código inaccesible
        }


    }
}
