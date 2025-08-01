using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_INEGI.Localidad;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalidadsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public LocalidadsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Localidads/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<LocalidadViewModel>> Listar()
        {
            var localidad = await _context.Localidads.Include(a => a.municipio).ToListAsync();

            return localidad.Select(a => new LocalidadViewModel
            {
                IdLocalidad = a.IdLocalidad,
                CP = a.CP,
                MunicipioId = a.MunicipioId,
                Nombre = a.Nombre,
                Zona = a.Zona,
                NombreMunicipio = a.municipio.Nombre
                
            });

        }
        // GET: api/Localidads/MostrarPorMPO/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ListarPorMunicipioViewModel>> MostrarPorMPO([FromRoute] int id)
        {
            var localidad = await _context.Localidads.Where(x => x.MunicipioId == id).Include(a => a.municipio).ToListAsync();


            return localidad.Select(a => new ListarPorMunicipioViewModel
            {
                IdLocalidad = a.IdLocalidad,
                Nombre = a.Nombre,
                CP = a.CP,
                 Zona = a.Zona,
                NombreMunicipio = a.municipio.Nombre


            });
        }
        // GET: api/Localidads/MostrarPorLocalidad/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> MostrarPorLocalidad([FromRoute]int id)
        {
            var localidad = await _context.Localidads.SingleOrDefaultAsync(a => a.IdLocalidad == id);
            
            if (localidad == null)
            {
                return NotFound();
            }

            return Ok(new ListarPorMunicipioViewModel
            {
                IdLocalidad = localidad.IdLocalidad,
                Nombre =localidad.Nombre,
                CP = localidad.CP,
                

            });
        }
        // GET: api/Localidads/MostrarPorCP/1
        [HttpGet("[action]/{cp}")]
        public async Task<IActionResult> MostrarPorCP([FromRoute] int cp)
        { 
            var localidad = await _context.Localidads.Include(a => a.municipio).FirstOrDefaultAsync(a => a.CP == cp);
            
            if (localidad == null)
            {
                return NotFound();
            }
           

            return Ok(new CodigoPostalViewModel
            { 
                CP = localidad.CP,
                idMunicipio = localidad.MunicipioId,
                idEstado = localidad.municipio.EstadoId
                
            });
        }
        // GET: api/Localidads/BuscarPorCP
        [HttpGet("[action]/{cp}")]
        public async Task<IActionResult> BuscarPorCP([FromRoute] int cp)
        {
            var localidad = await _context.Localidads.
                                    Include(a => a.municipio.estado).
                                    FirstOrDefaultAsync(a => a.CP == cp);

            if (localidad == null)
            {
                return NotFound();
            }


            return Ok(new DatosCodigoPostalViewModel
            {
                CP = localidad.CP,
                idMunicipio = localidad.MunicipioId,
                municipio = localidad.municipio.Nombre,
                idEstado = localidad.municipio.EstadoId,
                estado = localidad.municipio.estado.Nombre,
                IdLocalidad = localidad.IdLocalidad,
                Nombre = localidad.Nombre



            });
        }
        // GET: api/Localidads/MostrarPorCPMpo/1
        [HttpGet("[action]/{id},{cp}")]
        public async Task<IEnumerable<ListarPorMunicipioViewModel>> MostrarPorCPMpo([FromRoute] int id, int cp)
        {
            var localidad = await _context.Localidads.Where(x => x.MunicipioId == id).Where(x => x.CP == cp).ToListAsync();


            return localidad.Select(a => new ListarPorMunicipioViewModel
            {
                IdLocalidad = a.IdLocalidad,
                Nombre = a.Nombre,
                CP = a.CP


            });
        }
        // PUT: api/Localidads/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var localidad = await _context.Localidads.FirstOrDefaultAsync(a => a.IdLocalidad == model.IdLocalidad);

            if (localidad == null)
            {
                return NotFound();
            }

            localidad.CP = model.CP;
            localidad.MunicipioId = model.MunicipioId;
            localidad.Nombre = model.Nombre;
            localidad.Zona = model.Zona;
            

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

        // POST: api/Localidads/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Localidad localidad = new Localidad
            {
                CP = model.CP,
                MunicipioId = model.MunicipioId,
                Nombre = model.Nombre,
                Zona=model.Zona
            };

            _context.Localidads.Add(localidad);
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

        // GET: api/Localidads/MostrarPorMPONombre/
        [HttpGet("[action]/{municipio}")]
        public async Task<IEnumerable<ListarPorMunicipioViewModel>> MostrarPorMPONombre([FromRoute] string municipio)
        {
            var localidad = await _context.Localidads
                .Where(x => x.municipio.Nombre == municipio)
                .Include(a => a.municipio)
                .ToListAsync();


            return localidad.Select(a => new ListarPorMunicipioViewModel
            {
                IdLocalidad = a.IdLocalidad,
                Nombre = a.Nombre,
                CP = a.CP,
                Zona = a.Zona,
                NombreMunicipio = a.municipio.Nombre


            });
        }

        private bool LocalidadExists(int IdLocalidad)
        {
            return _context.Localidads.Any(e => e.IdLocalidad == IdLocalidad);
        }
    }
}