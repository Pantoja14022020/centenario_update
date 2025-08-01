using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_SpPi_Ligaciones;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_SpPi_Ligaciones;


namespace SIIGPP.Configuracion.Controllers.Cat_SpPi_Ligaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpPiLigacionesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public SpPiLigacionesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/SpPiLigaciones/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SpPiLigacionesViewModel>> Listar()
        {
            var sppi = await _context.SPPiligaciones
                .Include(a => a.DSP)
                .Include(a => a.PanelControl)
                .ToListAsync();

            return sppi.Select(a => new SpPiLigacionesViewModel
            {
                IdSPPiligaciones = a.IdSPPiligaciones,
                PanelControlId = a.PanelControlId,
                DSPId = a.DSPId,
                Direccion = a.Direccion,
                Dspn = a.DSP.NombreSubDir,
                Paneln = a.PanelControl.Nombre
            });

        }


        // PUT: api/SpPiLigaciones/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var sppi = await _context.SPPiligaciones.FirstOrDefaultAsync(a => a.IdSPPiligaciones == model.IdSPPiligaciones);

            if (sppi == null)
            {
                return NotFound();
            }

            sppi.PanelControlId = model.PanelControlId;
            sppi.DSPId = model.DSPId;
            sppi.Direccion = model.Direccion;

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


        // POST: api/SpPiLigaciones/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SPPiligaciones sppi = new SPPiligaciones
            {

                PanelControlId = model.PanelControlId,
                DSPId = model.DSPId,
                Direccion = model.Direccion,

            };

            _context.SPPiligaciones.Add(sppi);
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


        // GET: api/SpPiLigaciones/ListarPertenecienteyGeneralSP
        [HttpGet("[action]/{idmodulo}")]
        public async Task<IEnumerable<SpPiLigacionesViewModel>> ListarPertenecienteyGeneralSP([FromRoute]Guid idmodulo)
        {
            var sppi = await _context.SPPiligaciones
                .Include(a => a.DSP)
                .Include(a => a.PanelControl)
                .Where(a => a.Direccion == true || a.PanelControl.Clave == idmodulo )
                .Where(a => a.DSP.Tipo == "SP")
                .ToListAsync();

            return sppi.Select(a => new SpPiLigacionesViewModel
            {
                IdSPPiligaciones = a.IdSPPiligaciones,
                PanelControlId = a.PanelControlId,
                DSPId = a.DSPId,
                Direccion = a.Direccion,
                Dspn = a.DSP.NombreSubDir,
                Paneln = a.PanelControl.Nombre
            });

        }

        // GET: api/SpPiLigaciones/ListarPertenecienteyGeneralPI
        [HttpGet("[action]/{idmodulo}")]
        public async Task<IEnumerable<SpPiLigacionesViewModel>> ListarPertenecienteyGeneralPI([FromRoute]Guid idmodulo)
        {
            var sppi = await _context.SPPiligaciones
                .Include(a => a.DSP)
                .Include(a => a.PanelControl)
                .Include(a => a.DSP.Distrito)
                .Where(a => a.Direccion == true || a.PanelControl.Clave == idmodulo)
                .Where(a => a.DSP.Tipo == "PI")
                .ToListAsync();

            return sppi.Select(a => new SpPiLigacionesViewModel
            {
                IdSPPiligaciones = a.IdSPPiligaciones,
                PanelControlId = a.PanelControlId,
                DSPId = a.DSPId,
                Direccion = a.Direccion,
                Dspn = a.DSP.NombreSubDir,
                Paneln = a.PanelControl.Nombre,
                DistritoId = a.DSP.DistritoId
            });

        }


        // GET: api/SpPiLigaciones/ListarPertenecienteyGeneralSPDistrito
        [HttpGet("[action]/{idmodulo}/{iddistrito}")]
        public async Task<IEnumerable<SpPiLigacionesViewModel>> ListarPertenecienteyGeneralSPDistrito([FromRoute] Guid idmodulo,Guid iddistrito)
        {
            var sppi = await _context.SPPiligaciones
                .Include(a => a.DSP)
                .Include(a => a.PanelControl)
                .Where(a => a.Direccion == true || a.PanelControl.Clave == idmodulo)
                .Where(a => a.DSP.Tipo == "SP")
                .Where(a => a.DSP.DistritoId == iddistrito)
                .ToListAsync();

            return sppi.Select(a => new SpPiLigacionesViewModel
            {
                IdSPPiligaciones = a.IdSPPiligaciones,
                PanelControlId = a.PanelControlId,
                DSPId = a.DSPId,
                Direccion = a.Direccion,
                Dspn = a.DSP.NombreSubDir,
                Paneln = a.PanelControl.Nombre
            });

        }


        // GET: api/SpPiLigaciones/ListarPertenecienteyGeneralPIDistrito
        [HttpGet("[action]/{idmodulo}/{iddistrito}")]
        public async Task<IEnumerable<SpPiLigacionesViewModel>> ListarPertenecienteyGeneralPIDistrito([FromRoute] Guid idmodulo, Guid iddistrito)
        {
            var sppi = await _context.SPPiligaciones
                .Include(a => a.DSP)
                .Include(a => a.PanelControl)
                .Include(a => a.DSP.Distrito)
                .Where(a => a.Direccion == true || a.PanelControl.Clave == idmodulo)
                .Where(a => a.DSP.Tipo == "PI")
                .Where(a => a.DSP.DistritoId == iddistrito)
                .ToListAsync();

            return sppi.Select(a => new SpPiLigacionesViewModel
            {
                IdSPPiligaciones = a.IdSPPiligaciones,
                PanelControlId = a.PanelControlId,
                DSPId = a.DSPId,
                Direccion = a.Direccion,
                Dspn = a.DSP.NombreSubDir,
                Paneln = a.PanelControl.Nombre,
                DistritoId = a.DSP.DistritoId
            });

        }

    }
}
