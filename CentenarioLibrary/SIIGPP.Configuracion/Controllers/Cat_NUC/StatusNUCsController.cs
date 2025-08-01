using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_NUC;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_StatusNUC;

namespace SIIGPP.Configuracion.Controllers.Cat_NUC
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusNUCsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public StatusNUCsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/StatusNUCs
        [HttpGet("[action]")]
        public async Task<IEnumerable<StatusNUCViewModel>> Listar()
        {
            var db = await _context.StatusNUCs.ToListAsync();

            return db.Select(a => new StatusNUCViewModel
            {
                IdStatusNuc = a.IdStatusNuc,
                NombreStatus = a.NombreStatus,
                Etapa = a.Etapa
            });

        }


        // PUT: api/TipoDeclaracions/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var db = await _context.StatusNUCs.FirstOrDefaultAsync(a => a.IdStatusNuc == model.IdStatusNuc);

            if (db == null)
            {
                return NotFound();
            }

            db.NombreStatus = model.NombreStatus;
            db.Etapa = model.Etapa;
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

        // POST: api/TipoDeclaracions/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StatusNUC dato = new StatusNUC
            {
                NombreStatus = model.NombreStatus,
                Etapa = model.Etapa
                
            };

            _context.StatusNUCs.Add(dato);
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


        // GET: api/StatusNUCs/ListarT
        [HttpGet("[action]")]
        public async Task<IEnumerable<StatusNUCViewModel>> ListarT()
        {
            var db = await _context.StatusNUCs.ToListAsync();

            return db.Select(a => new StatusNUCViewModel
            {
                IdStatusNuc = a.IdStatusNuc,
                NombreStatus = a.NombreStatus,
                Etapa = a.Etapa
            });

        }


        // GET: api/StatusNUCs/ListarTD
        [HttpGet("[action]/{etapa}")]
        public async Task<IEnumerable<StatusNUCViewModel>> ListarTD([FromRoute] string etapa)
        //     public async Task<IEnumerable<VictimasViewModel>> ListarTodos([FromRoute] Guid rAtencionId)

        {


            var db = await _context.StatusNUCs.Where(x => x.Etapa == etapa).ToListAsync();

            return db.Select(a => new StatusNUCViewModel
            {
                IdStatusNuc = a.IdStatusNuc,
                NombreStatus = a.NombreStatus,
                Etapa = a.Etapa
            });



        }


        private bool StatusNUCExists(Guid id)
        {
            return _context.StatusNUCs.Any(e => e.IdStatusNuc == id);
        }
    }
}