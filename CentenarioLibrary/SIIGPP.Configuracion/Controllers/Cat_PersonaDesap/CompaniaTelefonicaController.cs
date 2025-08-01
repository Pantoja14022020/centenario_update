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
    public class CompaniaTelefonicaController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public CompaniaTelefonicaController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/RedesSocialesPersonal/BuscarRedes/{PersonaId}
        //[Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,Recepción,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListarCompaniaTelefonicaViewModel>> ListarCompaniaTelefonica()
        {
            var companiaTele = await _context.CompaniaTelefonicas
                          .OrderBy(a=> a.nombre)  
                          .ToListAsync();
            
            return companiaTele.Select(a => new ListarCompaniaTelefonicaViewModel
            {
                IdCompaniaTelefonica = a.IdCompaniaTelefonica,
                nombre = a.nombre,
                activa = a.activa,
            });
        }
        // GET: api/RedesSocialesPersonal/BuscarRedes/{PersonaId}
        //[Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,Recepción,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListarCompaniaTelefonicaViewModel>> ListarCompaniaTelefonicaActiva()
        {
            var companiaTele = await _context.CompaniaTelefonicas

                          .Where(a => a.activa == true)
                          .OrderByDescending(a => a.nombre)
                          .ToListAsync();

            return companiaTele.Select(a => new ListarCompaniaTelefonicaViewModel
            {
                IdCompaniaTelefonica = a.IdCompaniaTelefonica,
                nombre = a.nombre,
                activa = a.activa,
            });
        }
        // POST: api/RedesSocialesPersonal/CrearRedSocialPersonal
        [HttpPost("[Action]")]
        public async Task<IActionResult> Crear([FromBody] CrearCompaniaTelefonicaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CompaniaTelefonica insertarCompania = new CompaniaTelefonica
            {
                nombre = model.nombre,
                activa = true
            };
            try
            {
                _context.CompaniaTelefonicas.Add(insertarCompania);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex);
            }

            return Ok(new { idCompaniatelefonica = insertarCompania.IdCompaniaTelefonica });

        }


        // PUT: api/RedesSocialesPersonal/ActualizarRedSocialPersonal
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ListarCompaniaTelefonicaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companiaTelefonica = await _context.CompaniaTelefonicas.FirstOrDefaultAsync(a => a.IdCompaniaTelefonica == model.IdCompaniaTelefonica);

            if (companiaTelefonica == null)
            {
                return NotFound();
            }
            companiaTelefonica.nombre = model.nombre;
            companiaTelefonica.activa = model.activa;
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
