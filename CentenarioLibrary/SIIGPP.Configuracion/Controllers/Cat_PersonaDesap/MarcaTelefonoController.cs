using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.PersonaDesap;
using SIIGPP.Configuracion.Models.Cat_Generales.Telefonia;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaTelefonoController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public MarcaTelefonoController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/RedesSocialesPersonal/BuscarRedes/{PersonaId}
        //[Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,Recepción,Administrador")]
        [HttpGet("[action]/{PersonaId}")]
        public async Task<IEnumerable<ListarMarcaTelefonoViewModel>> ListarMarcaTelefono()
        {
            var marcaTelefono = await _context.MarcaTelefonos
                          .ToListAsync();

            return marcaTelefono.Select(a => new ListarMarcaTelefonoViewModel
            {
                IdMarcaTelefono = a.IdMarcaTelefono,
                nombre = a.nombre,
                activa = a.activa
            });
        }
        // GET: api/RedesSocialesPersonal/BuscarRedes/{PersonaId}
        //[Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,Recepción,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListarMarcaTelefonoViewModel>> ListarMarcaTelefonoActiva()
        {
            var marcaTelefono = await _context.MarcaTelefonos

                          .Where(a => a.activa == true)
                          .ToListAsync();

            return marcaTelefono.Select(a => new ListarMarcaTelefonoViewModel
            {
                IdMarcaTelefono = a.IdMarcaTelefono,
                nombre = a.nombre,
                activa = a.activa,
            });
        }

        // POST: api/RedesSocialesPersonal/CrearRedSocialPersonal
        [HttpPost("[Action]")]
        public async Task<IActionResult> Crear([FromBody] CrearMarcaTelefonoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MarcaTelefono insertarMarcaTel = new MarcaTelefono
            {
                nombre = model.nombre,
                activa = true
            };
            try
            {
                _context.MarcaTelefonos.Add(insertarMarcaTel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex);
            }

            return Ok(new { idMarcaTel = insertarMarcaTel.IdMarcaTelefono});

        }


        // PUT: api/RedesSocialesPersonal/ActualizarRedSocialPersonal
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ListarMarcaTelefonoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var marcaTelefono = await _context.MarcaTelefonos.FirstOrDefaultAsync(a => a.IdMarcaTelefono == model.IdMarcaTelefono);

            if (marcaTelefono == null)
            {
                return NotFound();
            }
            marcaTelefono.nombre = model.nombre;
            marcaTelefono.activa = model.activa;
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
