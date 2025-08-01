using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Armas.ClasificacionArma;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Armas;

namespace SIIGPP.Configuracion.Controllers.Cat_Armas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasificacionArmaController :ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public ClasificacionArmaController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/ClasificacionArma/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ClasificacionArmaViewModel>> Listar()
        {
            var ca = await _context.ClasificacionArmas.ToListAsync();

            return ca.Select(a => new ClasificacionArmaViewModel
            {
               IdClasificacionArma =a.IdClasificacionArma,
               NombreC =a.NombreC,
               Catalogo = a.Catalogo
            });

        }


        // GET: api/ClasificacionArma/Listarporcatalogo
      
        [HttpGet("[action]")]
        public async Task<IEnumerable<ClasificacionArmaViewModel>> Listarporcatalogo()
        {
            var ca = await _context.ClasificacionArmas
                .Where(a=> a.Catalogo == true)
                .ToListAsync();

            return ca.Select(a => new ClasificacionArmaViewModel
            {
                IdClasificacionArma = a.IdClasificacionArma,
                NombreC = a.NombreC,
                Catalogo = a.Catalogo
            });

        }

        // GET: api/ClasificacionArma/ListarNombre/marcan
       
        [HttpGet("[action]/{MarcaNombre}")]
        public async Task<IActionResult> ListarNombre([FromRoute] Guid MarcaNombre)
        {
            var clasi = await _context.ClasificacionArmas
                .Where(a => a.IdClasificacionArma == MarcaNombre)
                .FirstOrDefaultAsync();

            if (clasi == null)
            {
                return BadRequest("No hay registros");

            }

            return Ok(new ClasificacionArmaViewModel
            {
                IdClasificacionArma = clasi.IdClasificacionArma,
                NombreC = clasi.NombreC,
                Catalogo =clasi.Catalogo
            });

        }


        // PUT: api/ClasificacionArma/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 

            var ca = await _context.ClasificacionArmas.FirstOrDefaultAsync(a => a.IdClasificacionArma == model.IdClasificacionArma);

            if (ca == null)
            {
                return NotFound();
            }

            ca.NombreC = model.NombreC;
            ca.Catalogo = model.Catalogo;

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


        // POST: api/ClasificacionArma/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClasificacionArma ca = new ClasificacionArma
            {
                NombreC = model.NombreC,
                Catalogo = model.Catalogo
            };

            _context.ClasificacionArmas.Add(ca);
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
