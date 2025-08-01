using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Vehiculos.Marca;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos;

namespace SIIGPP.Configuracion.Controllers.Cat_Vehiculos
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public MarcaController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Marca/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<MarcaViewModel>> Listar()
        {
            var marca = await _context.Marcas
                              .OrderBy(m => m.Dato)
                              .ToListAsync();

            return marca.Select(a => new MarcaViewModel
            {
                IdMarca = a.IdMarca,
                Dato = a.Dato
            });

        }

        // GET: api/Marca/ListarNombre/idn
        [HttpGet("[action]/{IdMarca}")]
        public async Task<IActionResult> ListarNombre([FromRoute] string IdMarca)
        {
            var marca = await _context.Marcas
                .Where(a => a.Dato == IdMarca)
                .FirstOrDefaultAsync();

            if (marca == null)
            {
                return BadRequest("No hay registros");

            }

            return Ok( new MarcaViewModel
            {              
                IdMarca = marca.IdMarca,
                Dato = marca.Dato
            });

        }


        // PUT: api/Marca/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var marca = await _context.Marcas.FirstOrDefaultAsync(a => a.IdMarca == model.IdMarca);

            if (marca == null)
            {
                return NotFound();
            }

            marca.Dato = model.Dato;

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


        // POST: api/Marca/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Marca marca = new Marca
            {
                Dato = model.Dato

            };

            _context.Marcas.Add(marca);
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
