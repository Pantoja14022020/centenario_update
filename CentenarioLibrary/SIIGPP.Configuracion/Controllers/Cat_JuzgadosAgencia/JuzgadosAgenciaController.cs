using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_JuzgadosAgencias;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_JuzgadoAgencias;

namespace SIIGPP.Configuracion.Controllers.Cat_JuzgadosAgencia
{
    [Route("api/[controller]")]
    [ApiController]
    public class JuzgadosAgenciaController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public JuzgadosAgenciaController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/JuzgadosAgencia/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<JuzgadosAgenciaViewmodel>> Listar()
        {
            var ao = await _context.JuzgadosAgencias
                .ToListAsync();

            return ao.Select(a => new JuzgadosAgenciaViewmodel
            {
                 IdJuzgadoAgencia = a.IdJuzgadoAgencia,
                 DistritoId = a.DistritoId,
                 Nombre = a.Nombre,
                 Direccion =a.Direccion,
                 Telefono = a.Telefono,
                 Encargado =a.Encargado,

        });

        }


        // POST: api/JuzgadosAgencia/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JuzgadosAgencias ao = new JuzgadosAgencias
            {
                DistritoId = model.DistritoId,
                Nombre = model.Nombre,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Encargado = model.Encargado,

            };

            _context.JuzgadosAgencias.Add(ao);
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

        // GET: api/JuzgadosAgencia/ListarPorDistrito
        [HttpGet("[action]/{iddistrito}")]
        public async Task<IEnumerable<JuzgadosAgenciaViewmodel>> ListarPorDistrito([FromRoute] Guid iddistrito)
        {
            var ao = await _context.JuzgadosAgencias
                .Where (x => x.DistritoId == iddistrito)
                .ToListAsync();

            return ao.Select(a => new JuzgadosAgenciaViewmodel
            {
                IdJuzgadoAgencia = a.IdJuzgadoAgencia,
                DistritoId = a.DistritoId,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Encargado = a.Encargado,

            });

        }
    }
}
