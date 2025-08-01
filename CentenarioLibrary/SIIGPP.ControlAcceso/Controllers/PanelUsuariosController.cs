using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.ControlAcceso.Models.ControlAcceso.PanelUsuario;
using SIIGPP.ControlAcceso.Models.ControlAcceso.Usuarios;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_ControlAcceso.PanelUsuarios;
using SIIGPP.Entidades.M_Panel.M_PanelControl;

namespace SIIGPP.ControlAcceso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanelUsuariosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IConfiguration _config;

        public PanelUsuariosController(DbContextSIIGPP context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/PanelUsuarios
        [HttpGet]
        public IEnumerable<PanelUsuario> GetPanelUsuarios()
        {
            return _context.PanelUsuarios;

        }

        // POST: api/PanelUsuarios/ClonarSoloPanel
        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador,Director")]
        public async Task<IActionResult> ClonarSoloPanel([FromBody] ClonarSoloPanelUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var option = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_config.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;

                using (var ctx = new DbContextSIIGPP(option))
                {
                    var panel = await ctx.PanelUsuarios.Where(p => p.UsuarioId == model.UsuarioId).ToListAsync();

                    if (panel.Any())
                    {
                        ctx.PanelUsuarios.RemoveRange(panel);
                        await ctx.SaveChangesAsync();
                    }

                    var nuevoPanel = new PanelUsuario
                    {
                        UsuarioId = model.UsuarioId,
                        PanelControlId = model.PanelControlId
                    };

                    ctx.PanelUsuarios.Add(nuevoPanel);
                    await ctx.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception ex)
            {

                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

            

        }

        // POST: api/PanelUsuarios/ClonarPanel
        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador,Director")]
        public async Task<IActionResult> ClonarPanel([FromBody] ClonarPanelUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var consultaPanel = await _context.PanelUsuarios.Where(a => a.UsuarioId == model.UsuarioId).ToListAsync();

                if (consultaPanel == null || !consultaPanel.Any())
                {
                    return BadRequest(ModelState);
                }

                if (model.Caso == 2)
                {
                    var option = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_config.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;

                    using (var ctx = new DbContextSIIGPP(option))
                    {
                        var panel = await ctx.PanelUsuarios.Where(p => p.UsuarioId == model.UsuarioId).ToListAsync();

                        if (panel.Any())
                        {
                            ctx.PanelUsuarios.RemoveRange(panel);
                            await ctx.SaveChangesAsync();
                        }

                        foreach (var item in consultaPanel)
                        {
                            var nuevoPanel = new PanelUsuario
                            {
                                UsuarioId = item.UsuarioId,
                                PanelControlId = item.PanelControlId
                            };

                            ctx.PanelUsuarios.Add(nuevoPanel);
                        }
                        await ctx.SaveChangesAsync();
                    }
                    return Ok();
                }
                else if (model.Caso == 3)
                {
                    var option = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_config.GetConnectionString("C-" + model.IdDistritoD.ToString().ToUpper())).Options;

                    using (var ctx = new DbContextSIIGPP(option))
                    {
                        var panelDestino = await ctx.PanelUsuarios.Where(p => p.UsuarioId == model.UsuarioId).ToListAsync();

                        if (panelDestino.Any())
                        {
                            ctx.PanelUsuarios.RemoveRange(panelDestino);
                            await ctx.SaveChangesAsync();
                        }

                        foreach (var item in consultaPanel)
                        {
                            var nuevoPanel = new PanelUsuario
                            {
                                UsuarioId = item.UsuarioId,
                                PanelControlId = item.PanelControlId
                            };

                            ctx.PanelUsuarios.Add(nuevoPanel);
                        }
                        await ctx.SaveChangesAsync();
                    }
                    return Ok();
                }

                return BadRequest("No se Puede");
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/PanelUsuarios/ListarAsignados
        [Authorize(Roles = "Administrador,Director")]
        [HttpGet("[action]/{idUsuario}")]
        public async Task<IEnumerable<PanelUsuarioViewModel>> ListarAsignados([FromRoute] Guid idUsuario)
        {
            var rpu = await _context.PanelUsuarios
                            .Include(a => a.PanelControl
                            
                            )
                            .Where(a => a.UsuarioId== idUsuario) 
                            .ToListAsync();



            return rpu.Select(u => new PanelUsuarioViewModel
            {
                UsuarioId = u.UsuarioId,
                PanelControlId = u.PanelControlId,
                IdPanelUsuario = u.IdPanelUsuario,
                NombrePanel = u.PanelControl.Nombre,

            });



        }
        // POST: api/PanelUsuarios/Crear
        [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] POST_CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (PanelUsuarioExists(model.PanelControlId, model.UsuarioId) == false)
            {
                PanelUsuario pu = new PanelUsuario
                {

                    UsuarioId = model.UsuarioId,
                    PanelControlId = model.PanelControlId,
                };

                _context.PanelUsuarios.Add(pu);
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
            }
      
           

            return Ok();


        }
        // DELETE: api/PanelUsuarios/Eliminar/1
        [Authorize(Roles = " Administrador")]
        [HttpDelete("[action]/{idUsuario}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid idUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var rpu = await _context.PanelUsuarios
                           .Where(a => a.UsuarioId == idUsuario)
                           .ToListAsync();



             rpu.Select(u => new PanelUsuarioViewModel
            {
                UsuarioId = u.UsuarioId,
                PanelControlId = u.PanelControlId,
                 

            });

            foreach (var encuentra in rpu)
            {
                var up = await _context.PanelUsuarios.FindAsync(encuentra.IdPanelUsuario);
                if (up == null)
                {
                    return NotFound();
                }

                _context.PanelUsuarios.Remove(up);
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

            }


          

            return Ok();
        }

        private bool PanelUsuarioExists(Guid idPC, Guid IdU)
        {
            return _context.PanelUsuarios.Any(e => e.PanelControlId == idPC && e.UsuarioId == IdU);
        }
    }
}