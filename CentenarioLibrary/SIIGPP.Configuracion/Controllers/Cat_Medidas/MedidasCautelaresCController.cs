using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Medidas.MedidasCautelares;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Medidas;

namespace SIIGPP.Configuracion.Controllers.Cat_Medidas
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidasCautelaresCController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public MedidasCautelaresCController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/MedidasCautelaresC/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<MedidasCautelaresViewModel>> Listar()
        {
            var db = await _context.MedidasCautelaresCs.ToListAsync();

            return db.Select(a => new MedidasCautelaresViewModel
            {
                IdMedidasCautelaresC = a.IdMedidasCautelaresC,
                Clasificacion = a.Clasificacion,
                Clave = a.Clave,
                Descripcion = a.Descripcion
            });

        }


        // PUT: api/MedidasCautelaresC/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var db = await _context.MedidasCautelaresCs.FirstOrDefaultAsync(a => a.IdMedidasCautelaresC == model.IdMedidasCautelaresC);

            if (db == null)
            {
                return NotFound();
            }

            db.Clave = model.Clave;
            db.Clasificacion = model.Clasificacion;
            db.Descripcion = model.Descripcion;

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

        // POST: api/MedidasCautelaresC/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MedidasCautelaresC dato = new MedidasCautelaresC
            {
                Clave = model.Clave,
                Clasificacion = model.Clasificacion,
                Descripcion = model.Descripcion

            };

            _context.MedidasCautelaresCs.Add(dato);
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
