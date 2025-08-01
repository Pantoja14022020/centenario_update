using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Panel.M_PanelControl;
using SIIGPP.Panel.Models;

namespace SIIGPP.PC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanelControlsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public PanelControlsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/PanelControls/Permitidos/{idUsuario}
        [HttpGet("[action]/{idUsuario}")]
        public async Task<IActionResult> Permitidos([FromRoute] Guid idUsuario)
        {
            try
            {

                //Lista de PanelControlId que tiene el usuario
                var rpu = await _context.PanelUsuarios
                            .Where(a => a.UsuarioId == idUsuario) 
                            .Select(a => a.PanelControlId.ToString())
                            .ToListAsync();
                
                // Lista de Paneles filtrada por los PanelControlId del usuario
                var panelcontrol = await _context.PanelControls
                                    .Where(a => a.Activo == true && rpu.Contains(a.Id.ToString()))
                                    .OrderBy(a => a.Nombre)
                                    .ToListAsync();
                

                return Ok(panelcontrol.Select(c => new PanelControlViewModel
                {
                    IdPC = c.Id,
                    Nombre = c.Nombre,
                    Abrev = c.Abrev,
                    Icono = c.Icono,
                    Status = c.Status,
                    Ruta = c.Ruta,
                    Clave = c.Clave,
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/PanelControls/Listar
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var panelcontrol = await _context.PanelControls
                                    .Where(a => a.Activo == true)
                                    .OrderBy(a => a.Nombre)
                                    .ToListAsync();                

                return Ok(panelcontrol.Select(c => new PanelControlViewModel
                {
                    IdPC = c.Id,
                    Nombre = c.Nombre,
                    Abrev = c.Abrev,
                    Icono = c.Icono,
                    Status = c.Status,
                    Ruta = c.Ruta,
                    Clave = c.Clave,
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/PanelControls/ObtenerDistrito 
        [HttpGet("[action]")]
        public async Task<IActionResult> obtenerDistrito()
        {
            try
            {
                var distritoServidor = await _context.ControlDistritos.ToListAsync();

                return Ok(distritoServidor.Select(c => new ControlDistritoViewModel
                {
                    IdControlDistrito = c.IdControlDistrito,
                    Direccion = c.Direccion,
                    NombreDistrito = c.NombreDistrito,
                    DisId = c.DisId,
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/PanelControls/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var lpc = await _context.PanelControls.ToListAsync();

            return lpc.Select(r => new SelectViewModel
            {
                IdPC = r.Id,
                Nombre   = r.Nombre
            });
        }

        // GET: api/PanelControls/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            var panelControl = await _context.PanelControls.FindAsync(id);

            if (panelControl == null)
            {
                return NotFound();
            }

            return Ok(new PanelControlViewModel
            {
                IdPC = panelControl.Id,
                Nombre = panelControl.Nombre,
                Abrev = panelControl.Abrev,
                Icono = panelControl.Icono,
                Status = panelControl.Status,
                Ruta = panelControl.Ruta
            });
        }

        // PUT: api/PanelControls/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var panelcontrol = await _context.PanelControls.FirstOrDefaultAsync(a => a.Id == model.IdPC);

            if (panelcontrol == null)
            {
                return NotFound();
            }

            panelcontrol.Nombre = model.Nombre;
            panelcontrol.Abrev = model.Abrev;
            panelcontrol.Icono = model.Icono;
            panelcontrol.Ruta = model.Ruta;
            panelcontrol.Status = model.Status;

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

        // POST: api/PanelControls/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PanelControl panelcontrol = new PanelControl
            {
                Nombre = model.Nombre,
                Abrev = model.Abrev,
                Icono = model.Icono,
                Ruta = model.Ruta,
                Status = model.Status,
                Clave = Guid.NewGuid(),
            };

            _context.PanelControls.Add(panelcontrol);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { id = panelcontrol.Id });
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest(new { message = ex.Message, stackTrace = ex.StackTrace });
            }
            //return Ok();
        }

        // PUT: api/PanelControls/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] Guid id)
        {
            var panelcontrol = await _context.PanelControls.FirstOrDefaultAsync(c => c.Id == id);

            if (panelcontrol == null)
            {
                return NotFound();
            }

            panelcontrol.Status = false;

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

        // PUT: api/PanelControls/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] Guid id)
        {
            var panelcontrol = await _context.PanelControls.FirstOrDefaultAsync(c => c.Id == id);

            if (panelcontrol == null)
            {
                return NotFound();
            }

            panelcontrol.Activo = true;

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

        private bool PanelControlExists(Guid id)
        {
            return _context.PanelControls.Any(e => e.Id == id);
        }
    }

}