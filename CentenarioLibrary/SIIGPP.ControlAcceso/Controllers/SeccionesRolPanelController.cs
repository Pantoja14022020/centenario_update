using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using SIIGPP.ControlAcceso.Models.ControlAcceso.Menus;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.ControlAcceso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeccionesRolPanelController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;
        private IConfiguration _configuration;
        public SeccionesRolPanelController(DbContextSIIGPP context,IConfiguration configuration, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;
        }
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        [HttpGet("[action]/{idpc}/{idrol}")]
        public async Task<IActionResult> GetSeccionesporPanelRol([FromRoute] Guid idpc, Guid idrol)
        {
            try
            {
                var secciones = await _context.SeccionesRolPanels
                    .Include(a=>a.Seccion)
                    .Where(a => a.Activo == true)
                    .Where(a=> a.Seccion.Activo==true)
                    .Where(a=>a.PanelControlId==idpc)
                    .Where(a=>a.RolId==idrol)
                    .OrderBy(a=>a.Orden)
                    .ToListAsync();

                var modulos = await _context.ModuloRols
                    .Include(a => a.SeccionRolPanel)
                    .Include(a=> a.Modulo)
                    .Include(a=> a.SeccionRolPanel.Seccion)
                    .Where(a => a.Activo == true)
                    .Where(a => a.SeccionRolPanel.Activo == true)
                    .Where(a => a.SeccionRolPanel.PanelControlId == idpc)
                    .Where(a => a.SeccionRolPanel.RolId == idrol)
                    .OrderBy(a => a.Orden)
                    .ToListAsync();

                var submodulos = await _context.SubModuloRols
                    .Include(a=>a.ModuloRol)
                    .Include(a=> a.ModuloRol.SeccionRolPanel)
                    .Where(a=> a.Activo)
                    .Where(a => a.ModuloRol.Activo)
                    .Where(a=> a.ModuloRol.SeccionRolPanel.Activo)
                    .Where(a => a.ModuloRol.SeccionRolPanel.PanelControlId == idpc)
                    .Where(a => a.ModuloRol.SeccionRolPanel.RolId == idrol)
                    .OrderBy(a => a.Orden)
                    .ToListAsync();

                return Ok(new
                {
                    secciones = secciones.Select(a => new SeccionRolPanelViewModel
                    {
                        IdMenuPanel = a.IdMenuPanel,
                        RolId = a.RolId,
                        SeccionId = a.SeccionId,
                        PanelControlId = a.PanelControlId,
                        Descripcion = a.Seccion.Descripcion,
                    }),
                    modulos = modulos.Select(a=> new ModuloRolViewModel 
                    {
                        IdModuloRol = a.IdModuloRol,
                        MenuPanelId = a.MenuPanelId,
                        Descripcion = a.Modulo.Descripcion,
                        Icono = a.Modulo.Icono,
                        Ruta = a.Modulo.Ruta,
                        Orden = a.Orden
                    }),
                    submodulos= submodulos.Select(a => new SubModuloRolViewModel
                    {
                        IdSubModuloRol = a.IdSubModuloRol,
                        ModuloRolId = a.ModuloRolId,
                        Icono = a.Modulo.Icono,
                        Descripcion = a.Modulo.Descripcion,
                        Ruta = a.Modulo.Ruta,
                        Orden = a.Orden
                    })

                }
); ;
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }
    }
}
