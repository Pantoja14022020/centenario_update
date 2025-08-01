using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_ServicioPericiales.Servicios;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_ServiciosPericiales;
using SIIGPP.Entidades.M_ControlAcceso.Usuarios;


namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioPericialsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public ServicioPericialsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/ServicioPericials/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ServicioViewModel>> Listar()
        {
            var sp = await _context.ServicioPericiales.ToListAsync();

            return sp.Select(a => new ServicioViewModel
            {
                IdServicioPericial = a.IdServicioPericial,
                Codigo = a.Codigo,
                Servicio = a.Servicio,
                Descripcion = a.Descripcion,
                Requisitos = a.Requisitos,
                AtencionVictimas = a.AtencionVictimas,
                CancelableporJR = a.CancelableporJR,
                Materia = a.EnMateriaDe
            });

        }

        // GET: api/ServicioPericials/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var a = await _context.ServicioPericiales.FindAsync(id);

            if (a == null)
            {
                return NotFound();
            }

            return Ok(new ServicioPericial
            {
                IdServicioPericial = a.IdServicioPericial,
                Codigo = a.Codigo,
                Servicio = a.Servicio,
                Descripcion = a.Descripcion,
                Requisitos = a.Requisitos,
                AtencionVictimas = a.AtencionVictimas,
                CancelableporJR = a.CancelableporJR,
                EnMateriaDe = a.EnMateriaDe

            });
        }

        // PUT: api/ServicioPericials/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var a = await _context.ServicioPericiales.FirstOrDefaultAsync(x => x.IdServicioPericial == model.IdServicioPericial);

            if (a == null)
            {
                return NotFound();
            }

   
            a.Codigo = model.Codigo;
            a.Servicio = model.Servicio;
            a.Descripcion = model.Descripcion;
            a.Requisitos = model.Requisitos;
            a.AtencionVictimas = model.AtencionVictimas;
            a.CancelableporJR = model.CancelableporJR;
            a.EnMateriaDe = model.Materia;
 

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

        // POST: api/ServicioPericials/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServicioPericial a = new ServicioPericial
            {
            

            Codigo = model.Codigo,
            Servicio = model.Servicio,
            Descripcion = model.Descripcion,
            Requisitos = model.Requisitos,
            AtencionVictimas = model.AtencionVictimas,
            CancelableporJR = model.CancelableporJR,
            EnMateriaDe = model.Materia,
            


        };

            _context.ServicioPericiales.Add(a);
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


        // GET: api/ServicioPericials/Idpornombre
        [HttpGet("[action]{Nombrea}")]
        public async Task<IActionResult> Idpornombre([FromRoute] string Nombrea)
        {
            var sp = await _context.ServicioPericiales.Where(a => a.Servicio == Nombrea)                                                                                                          
                                                        .FirstOrDefaultAsync();

            if (sp == null)
            {
                return BadRequest("No hay registros");

            }
            return Ok(new ServicioViewModel
            {
                IdServicioPericial = sp.IdServicioPericial
            });

        }

        private bool ServicioPericialExists(Guid id)
        {
            return _context.ServicioPericiales.Any(e => e.IdServicioPericial == id);
        }
    }
}