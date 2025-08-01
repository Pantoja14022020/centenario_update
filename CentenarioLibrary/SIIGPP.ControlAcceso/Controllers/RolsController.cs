using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using SIIGPP.ControlAcceso.Models.ControlAcceso.Roles;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_ControlAcceso.Roles;

namespace SIIGPP.ControlAcceso.Controllers
{
     
    [Route("api/[controller]")]
    [ApiController]
    public class RolsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public RolsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Rols/Listar
        //[Authorize(Roles =  "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolViewModel>> Listar()
        {
           
            var rol = await _context.Rols.ToListAsync();

            return rol.Select(r => new RolViewModel
            {
                idrol = r.IdRol, 
                nombre = r.Nombre,
                descripcion = r.Descripcion,
                condicion = r.Condicion
            });

        }

        // GET: api/Rols/Select
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var rol = await _context.Rols.Where(r => r.Condicion == true).ToListAsync();

            return rol.Select(r => new SelectViewModel
            {
                idrol = r.IdRol,
                nombre = r.Nombre
            });
        }

        // PUT: api/Rols/Actualizar
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

 

            var rol = await _context.Rols.FirstOrDefaultAsync(a => a.IdRol == model.idrol);

            if (rol == null)
            {
                return NotFound();
            }
            rol.IdRol = model.idrol; 
            rol.Nombre = model.nombre;
            rol.Descripcion = model.descripcion;
            rol.Condicion = model.condicion; 

             
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

        // POST: api/Rols/Crear
        [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rol rol = new Rol
            {  
                Nombre = model.nombre,
                Descripcion = model.descripcion,
                Condicion = model.condicion,
            };

            _context.Rols.Add(rol);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

            return Ok();
        }
        private bool RolExists(Guid id)
        {
            return _context.Rols.Any(e => e.IdRol == id);
        }
    }
}