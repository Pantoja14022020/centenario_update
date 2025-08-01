using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_INEGI.Municipio;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipiosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public MunicipiosController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Municipios/Listar
        // Muestra todos los municipios
        [HttpGet("[action]")]
        public async Task<IEnumerable<MunicipioViewModel>> Listar()
        {
            var municipio = await _context.Municipios.Include(a => a.estado).ToListAsync();

            return municipio.Select(a => new MunicipioViewModel
            {
                IdMunicipio = a.IdMunicipio,
                Numero_Mpio = a.Numero_Mpio,
                EstadoId = a.EstadoId,
                Nombre = a.Nombre,
                NombreEstado = a.estado.Nombre
            });

        }



        // GET: api/Municipios/ListarPorEstado/1 
        // Muestra todos los municipios de un Estado
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ListarPorEstadoViewModel>> ListarPorEstado([FromRoute] int id)
        {
            var municipio = await _context.Municipios.Include(a => a.estado).Where(x => x.EstadoId == id).ToListAsync();

            return municipio.Select(a => new ListarPorEstadoViewModel
            {
                IdMunicipio = a.IdMunicipio,  
                Nombre = a.Nombre 
            });

        }


        // PUT: api/Municipios/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var municipio = await _context.Municipios.FirstOrDefaultAsync(a => a.IdMunicipio == model.IdMunicipio);

            if (municipio == null)
            {
                return NotFound();
            }

            municipio.Numero_Mpio = model.Numero_Mpio;
            municipio.EstadoId = model.EstadoId;
            municipio.Nombre = model.Nombre;
            

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

        // POST: api/Municipios/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Municipio municipio = new Municipio
            {
                Numero_Mpio = model.Numero_Mpio,
                EstadoId = model.EstadoId,
                Nombre = model.Nombre
            };

            _context.Municipios.Add(municipio);
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
        private bool MunicipioExists(int IdMunicipio)
        {
            return _context.Municipios.Any(e => e.IdMunicipio == IdMunicipio);
        }
    }
}