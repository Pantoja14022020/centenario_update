using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_FiscaliaOestados;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_FiscaliaOestados;


namespace SIIGPP.Configuracion.Controllers.Cat_FiscaliaOestados
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiscaliaOestadoController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public FiscaliaOestadoController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/FiscaliaOestado/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<FiscaliaOestadosViewModel>> Listar()
        {
            var fiscalia = await _context.FiscaliaOestados
                .Include(a => a.Estado)
                .Include(a => a.Municipio)
                .ToListAsync();

            return fiscalia.Select(a => new FiscaliaOestadosViewModel
            {
                IdFiscaliaOestado = a.IdFiscaliaOestado,
                EstadoId  = a.EstadoId,
                MunicipioId = a.MunicipioId,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Contacto = a.Contacto,
                Estado = a.Estado.Nombre,
                Municipio = a.Municipio.Nombre
             });

        }


        // PUT: api/FiscaliaOestado/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var fiscalia = await _context.FiscaliaOestados.FirstOrDefaultAsync(a => a.IdFiscaliaOestado == model.IdFiscaliaOestado);

            if (fiscalia == null)
            {
                return NotFound();
            }

            fiscalia.EstadoId = model.EstadoId;
            fiscalia.MunicipioId = model.MunicipioId;
            fiscalia.Nombre = model.Nombre;
            fiscalia.Direccion = model.Direccion;
            fiscalia.Telefono = model.Telefono;
            fiscalia.Contacto = model.Contacto;


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

        // POST: api/FiscaliaOestado/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FiscaliaOestado fiscalia = new FiscaliaOestado
            {
                EstadoId = model.EstadoId,
                MunicipioId = model.MunicipioId,
                Nombre = model.Nombre,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Contacto = model.Contacto
             };

            _context.FiscaliaOestados.Add(fiscalia);
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
