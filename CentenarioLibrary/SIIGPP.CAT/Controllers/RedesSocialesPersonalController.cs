using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.PersonaDesap;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.PersonaDesap;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedesSocialesPersonalController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public RedesSocialesPersonalController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //GET: api/RedesSocialesPersonal
        [HttpGet]
        public IEnumerable<RedesSocialesPersonal> RedesSocialesPersonal()
        {
            return _context.RedesSocialesPersonal;
        }

        //GET: api/RedesSociales
        [HttpGet]
        public IEnumerable<RedesSociales> RedesSociales()
        {
            return _context.RedesSociales;
        }

        // GET: api/RedesSocialesPersonal/BuscarRedes/{PersonaId}
        //[Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Administrador")]
        [HttpGet("[action]/{PersonaId}")]
        public async Task<IEnumerable<ListarRedesSocialesPersonalViewModel>> BuscarRedes([FromRoute] Guid PersonaId)
        {
            var redesper = await _context.RedesSocialesPersonal
                          .Include(a => a.RedSocial)
                          .Where(a => a.PersonaId == PersonaId)
                          .ToListAsync();

            return redesper.Select(a => new ListarRedesSocialesPersonalViewModel
            {
                IdRedesSocialesPersonal = a.IdRedesSocialesPersonal,
                PersonaId = a.PersonaId,
                RedSocialId = a.RedSocialId,
                RedSocial = a.RedSocial.RedSocial,
                Enlace = a.Enlace
            });
        }

        // GET: api/RedesSocialesPersonal/ListarRedesSociales
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListarRedesSocialesViewModel>> ListarRedesSociales()
        {
            var redessociales = await _context.RedesSociales 
                                .OrderBy(a => a.RedSocial)
                                .ToListAsync();
            return redessociales.Select(a => new ListarRedesSocialesViewModel
            {
                IdRedSocial = a.IdRedSocial,
                RedSocial = a.RedSocial
            });
        }

        // POST: api/RedesSocialesPersonal/CrearRedSocialPersonal
        [HttpPost("[Action]")]
        public async Task<IActionResult> CrearRedSocialPersonal([FromBody] CrearRedSocialPersonalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RedesSocialesPersonal insertarredsocial = new RedesSocialesPersonal
            {
                PersonaId = model.PersonaId,
                RedSocialId = model.RedSocialId,
                Enlace = model.Enlace
            };
            try
            {
                _context.RedesSocialesPersonal.Add(insertarredsocial);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex);
            }

            return Ok(new { idredessocialespersonal = insertarredsocial.IdRedesSocialesPersonal });

        }


        // PUT: api/RedesSocialesPersonal/ActualizarRedSocialPersonal
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarRedSocialPersonal([FromBody] ActualizarRedSocialPersonalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vredsocialpersonal = await _context.RedesSocialesPersonal.FirstOrDefaultAsync(a => a.IdRedesSocialesPersonal == model.IdRedesSocialesPersonal);

            if (vredsocialpersonal == null)
            {
                return NotFound();
            }
            vredsocialpersonal.RedSocialId = model.RedSocialId;
            vredsocialpersonal.Enlace = model.Enlace;
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
