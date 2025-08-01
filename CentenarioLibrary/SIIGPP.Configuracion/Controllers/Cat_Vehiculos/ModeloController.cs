using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Vehiculos.Modelo;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos;

namespace SIIGPP.Configuracion.Controllers.Cat_Vehiculos
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeloController : ControllerBase
    {


        private readonly DbContextSIIGPP _context;

        public ModeloController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Modelo/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ModeloViewModel>> Listar()
        {
            var modelo = await _context.Modelos
                               .OrderBy(a => a.Dato)
                               .Include(a=> a.Marca)
                               .ToListAsync();

            return modelo.Select(a => new ModeloViewModel
            {
                IdModelo = a.IdModelo,
                Dato = a.Dato,
                MarcaId =a.MarcaId,
                NombreMarca = a.Marca.Dato
            });

        }



        // GET: api/Modelo/ListarId
        [HttpGet("[action]/{MarcaId}")]
        public async Task<IEnumerable<ModeloViewModel>> ListarId([FromRoute]Guid MarcaId)
        {
            var Modelo = await _context.Modelos
                .Where(a => a.MarcaId == MarcaId)
                .OrderBy(a => a.Dato)
                .ToListAsync();

            return Modelo.Select(a => new ModeloViewModel
            {
                IdModelo = a.IdModelo,
                Dato= a.Dato,
                MarcaId = a.MarcaId
            });

        }


        // PUT: api/Modelo/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var modelo = await _context.Modelos.FirstOrDefaultAsync(a => a.IdModelo == model.IdModelo);

            if (modelo == null)
            {
                return NotFound();
            }

                modelo.Dato = model.Dato;
                modelo.MarcaId = model.MarcaId;

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


        // POST: api/Modelo/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Modelo modelo = new Modelo
            {
                Dato = model.Dato,
                MarcaId = model.MarcaId
            };

            _context.Modelos.Add(modelo);
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
