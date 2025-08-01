 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Catalogos.DDerivacion;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependeciasDerivacionsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public DependeciasDerivacionsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/DependeciasDerivacions/Listar
        [HttpGet]
        public IEnumerable<DependeciasDerivacion> GetDependeciasDerivacions()
        {
            return _context.DependeciasDerivacions;
        }

        // GET: api/DependeciasDerivacions/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<DDerivacionViewModel>> Listar()
        {
            var dd = await _context.DependeciasDerivacions.Include(a => a.Distrito).ToListAsync();

            return dd.Select(a => new DDerivacionViewModel
            {
                IdDDerivacion = a.IdDDerivacion,
                DistritoId = a.DistritoId,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Contacto = a.Contacto,
                NombreDistrito = a.Distrito.Nombre
            });

        }
        // GET: api/DependeciasDerivacions/ListarPorDependencia
        [HttpGet("[action]/{idDependencia}")]
        public async Task<IActionResult> ListarPorDependencia([FromRoute]Guid idDependencia)
        {
            var dependencia = await _context.DependeciasDerivacions.SingleOrDefaultAsync(a => a.IdDDerivacion == idDependencia);

            if (dependencia == null)
            {
                return NotFound();
            }

            return Ok(new DDerivacionViewModel
            {
                IdDDerivacion = dependencia.IdDDerivacion,
                DistritoId = dependencia.DistritoId,
                Nombre = dependencia.Nombre,
                Direccion = dependencia.Direccion,
                Telefono = dependencia.Telefono,
                Contacto = dependencia.Contacto, 



            });
        }

        // PUT: api/DependeciasDerivacions/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var dd = await _context.DependeciasDerivacions.FirstOrDefaultAsync(a => a.IdDDerivacion == model.IdDDerivacion);

            if (dd == null)
            {
                return NotFound();
            }
            dd.DistritoId = model.DistritoId;
            dd.Nombre = model.Nombre;
            dd.Direccion = model.Direccion;
            dd.Telefono = model.Telefono;
            dd.Contacto = model.Contacto;

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

        // POST: api/DependeciasDerivacions/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DependeciasDerivacion dd = new DependeciasDerivacion
            {
                DistritoId = model.DistritoId,
                Nombre = model.Nombre,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Contacto = model.Contacto


            };

            _context.DependeciasDerivacions.Add(dd);
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

        private bool DependeciaDerivacionExists(Guid id)
        {
            return _context.DependeciasDerivacions.Any(a => a.IdDDerivacion == id);
        }

    }
}