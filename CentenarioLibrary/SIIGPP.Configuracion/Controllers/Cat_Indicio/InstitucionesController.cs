using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_Indicios.Instituciones;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Indicios;

namespace SIIGPP.Configuracion.Controllers.Cat_Indicio
{

    [Route("api/[controller]")]
    [ApiController]
    public class InstitucionesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public InstitucionesController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Instituciones/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<InstitucionesViewModel>> Listar()
        {
            var Instituciones = await _context.Instituciones.ToListAsync();

            return Instituciones.Select(a => new InstitucionesViewModel
            {
                IdInstitucion = a.IdInstitucion,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Contacto = a.Contacto,
                Telefono = a.Telefono
            });

        }

        // GET: api/Instituciones/ListarInstitucionesM
        [HttpGet("[action]/{idMProteccion}")]
        public async Task<IActionResult> ListarInstitucionesM([FromRoute] Guid idMProteccion)
      //public async Task<IActionResult> Representanteslistarporid([FromRoute] Guid RHechoId)

        {
            try
            {
                string busquedaInstitucionesM = @"select 
                                                    i.IdInstitucion,
                                                    i.Nombre,
                                                    i.Direccion,
                                                    i.Contacto,
                                                    i.Telefono
                                                    from CI_Institucion as i
                                                    left join CAT_MEDIDASPROTECCION as m on
                                                    m.Institucionejec like i.Nombre+', %' 
                                                    OR m.Institucionejec like '%, %'+i.Nombre
                                                    OR m.Institucionejec like i.Nombre
                                                    OR m.Institucionejec like '%, %'+i.Nombre+'%, %'
                                                    where m.IdMProteccion = @idmproteccion";

            List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
            filtrosBusqueda.Add(new SqlParameter("@idmproteccion", idMProteccion));
            var Instm = await _context.qBusquedaInstitucionM.FromSqlRaw(busquedaInstitucionesM, filtrosBusqueda.ToArray()).ToListAsync();

            return Ok (Instm.Select(a => new InstitucionesViewModel
            {
                IdInstitucion = a.IdInstitucion,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Contacto = a.Contacto,
                Telefono = a.Telefono
            }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

        }


        // PUT: api/Instituciones/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var institucion = await _context.Instituciones.FirstOrDefaultAsync(a => a.IdInstitucion == model.IdInstitucion);

            if (institucion == null)
            {
                return NotFound();
            }

            institucion.Nombre = model.Nombre;
            institucion.Direccion = model.Direccion;
            institucion.Contacto = model.Contacto;
            institucion.Telefono = model.Telefono;

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


        // POST: api/Instituciones/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Institucion institucion = new Institucion
            {
                Nombre = model.Nombre,
                Direccion = model.Direccion,
                Contacto = model.Contacto,
                Telefono = model.Telefono
                
            };

            _context.Instituciones.Add(institucion);
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

        private bool InstitucionesExists(Guid id)
        {
            return _context.Instituciones.Any(e => e.IdInstitucion == id);
        }


    }
}
