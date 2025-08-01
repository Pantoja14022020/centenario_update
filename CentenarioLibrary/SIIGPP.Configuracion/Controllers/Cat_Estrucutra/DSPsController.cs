using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Configuracion.Models.Cat_Estrucutra.DSP;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_Configuracion.Cat_Estrucutra;
using SIIGPP.Entidades.M_JR.RResponsable;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DSPsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IConfiguration _configuration;

        public DSPsController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/DSPs/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<DSPViewModel>> Listar()
        {
            var dsp = await _context.DSPs.Include(a => a.Distrito).ToListAsync();

            return dsp.Select(a => new DSPViewModel
            {
                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Responsable = a.Responsable,
                Telefono = a.Telefono,
                StatusInicioCarpeta = a.StatusInicioCarpeta,
                DistritoId = a.DistritoId,
                NombreDistrito = a.Distrito.Nombre,
                Clave = a.Clave,
                Tipo = a.Tipo,
                NombreSub = a.NombreSub,
                StatusDSP = a.StatusDSP
            });

        }

        // GET: api/DSPs/ListarClaveDSP
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Oficialia de partes,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido Mixto,Coordinador-Jurídico,Coordinador-Jurídico,Director-Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,Procurador")]
        [HttpGet("[action]/{clave}/{IdDistrito}")]
        public async Task<IEnumerable<DSPViewModel>> ListarClaveDSP([FromRoute] String clave, Guid IdDistrito)
        {
            var dsp = await _context.DSPs
                    .Include(a => a.Distrito)
                    .Where(a => a.Clave == clave)
                    .Where(a => a.Distrito.IdDistrito == IdDistrito)
                    .Where(a => a.StatusDSP == true)
                    .OrderBy(a => a.NombreSubDir)
                    .ToListAsync();

            return dsp.Select(a => new DSPViewModel
            {
                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Responsable = a.Responsable,
                Telefono = a.Telefono,
                StatusInicioCarpeta = a.StatusInicioCarpeta,
                DistritoId = a.DistritoId,
                NombreDistrito = a.Distrito.Nombre,
                Clave = a.Clave,
                Tipo = a.Tipo,
                NombreSub = a.NombreSub
            });

        }

        // GET: api/DSPs/ListarPorDistritoId
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Oficialia de partes,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido Mixto,Coordinador-Jurídico,Coordinador-Jurídico,Director-Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,Procurador")]
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<DSPViewModel>> ListarPorDistritoId([FromRoute] Guid id)
        {
            var dsp = await _context.DSPs
                    .Include(a => a.Distrito)
                    .Where(a => a.StatusDSP == true)
                    .Where(a => a.DistritoId == id).OrderBy(a => a.NombreSubDir).ToListAsync();

            return dsp.Select(a => new DSPViewModel
            {
                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Responsable = a.Responsable,
                Telefono = a.Telefono,
                StatusInicioCarpeta = a.StatusInicioCarpeta,
                DistritoId = a.DistritoId,
                NombreDistrito = a.Distrito.Nombre,
                Clave = a.Clave,
                Tipo = a.Tipo,
                NombreSub = a.NombreSub
            });

        }
        // GET: api/DSPs/ListarPorDistritoId/Captura
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Oficialia de partes,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido Mixto,Coordinador-Jurídico,Coordinador-Jurídico,Director-Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,Procurador")]
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<DSPViewModel>> ListarPorDistritoIdCap([FromRoute] Guid id)
        {
            var dsp = await _context.DSPs
                    .Include(a => a.Distrito)
                    .Where(a => a.DistritoId == id).OrderBy(a => a.NombreSubDir).ToListAsync();

            return dsp.Select(a => new DSPViewModel
            {
                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Responsable = a.Responsable,
                Telefono = a.Telefono,
                StatusInicioCarpeta = a.StatusInicioCarpeta,
                DistritoId = a.DistritoId,
                NombreDistrito = a.Distrito.Nombre,
                Clave = a.Clave,
                Tipo = a.Tipo,
                NombreSub = a.NombreSub
            });

        }
        // GET: api/DSPs/ListarPorDisttritoCve
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Oficialia de partes,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido Mixto,Coordinador-Jurídico,Coordinador-Jurídico,Director-Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,AMPO-IL")]
        [HttpGet("[action]/{distritoId}/{cve}")]
        public async Task<IActionResult> ListarPorDisttritoCve([FromRoute] Guid distritoId, string cve)
        {
            var a = await _context.DSPs
                    .Include(x => x.Distrito)
                    .Where(x => x.Clave == cve)
                    .Where(x => x.DistritoId == distritoId).FirstOrDefaultAsync();


            if (a == null)
            {
                return Ok(new { message = "No hay registros" });

            }
            return Ok(new DSPViewModel
            {
                /*********************************************/

                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Responsable = a.Responsable,
                Telefono = a.Telefono,
                StatusInicioCarpeta = a.StatusInicioCarpeta,
                DistritoId = a.DistritoId,
                NombreDistrito = a.Distrito.Nombre,
                Clave = a.Clave,
                Tipo = a.Tipo,
                NombreSub = a.NombreSub
            });

        }

        // GET: api/DSPs/ListarPorDistritoStatus/1
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Oficialia de partes,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido Mixto,Coordinador-Jurídico,Coordinador-Jurídico,Director-Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,")]
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ListarPorDistritoViewModel>> ListarPorDistritoStatus([FromRoute] Guid id)
        {
            var dsp = await _context.DSPs.Where(x => x.DistritoId == id)
                .Include(x => x.Distrito)
                .Where(x => x.StatusInicioCarpeta == false)
                .ToListAsync();

            return dsp.Select(a => new ListarPorDistritoViewModel
            {
                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                NombreDistrito = a.Distrito.Nombre,
                Tipo = a.Tipo,
                NombreSub = a.NombreSub
            });

        }

        // PUT: api/DSPs/Actualizar
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var a = await _context.DSPs.FirstOrDefaultAsync(x => x.IdDSP == model.IdDSP);

            if (a == null)
            {
                return NotFound();
            }

            a.NombreSubDir = model.NombreSubDir;
            a.Responsable = model.Responsable;
            a.Telefono = model.Telefono;
            a.StatusInicioCarpeta = model.StatusInicioCarpeta;
            a.DistritoId = model.DistritoId;
            a.Clave = model.Clave;
            a.Tipo = model.Tipo;
            a.NombreSub = model.NombreSub;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(new { id = model.IdDSP });
        }

        // POST: api/DSPs/Crear
        [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DSP a = new DSP
            {
                NombreSubDir = model.NombreSubDir,
                Responsable = model.Responsable,
                Telefono = model.Telefono,
                StatusInicioCarpeta = model.StatusInicioCarpeta,
                DistritoId = model.DistritoId,
                Clave = model.Clave,
                Tipo = model.Tipo,
                NombreSub = model.NombreSub,
                StatusDSP = true,



            };

            _context.DSPs.Add(a);
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

            return Ok(new { id = a.IdDSP });
        }

        // GET: api/DSPs/ListarDistritoySP
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Oficialia de partes,AMPO-AMP,Director,Coordinador,AMPO-IL,AMPO-AMP Mixto, AMPO-AMP Detenido Mixto,Coordinador-Jurídico,Director,Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IActionResult> ListarDistritoySP([FromRoute] Guid distritoId)
        {
            var a = await _context.DSPs
                    .Include(x => x.Distrito)
                    .Where(x => x.DistritoId == distritoId)
                    .Where(x => x.Clave == "DGSP")
                    .FirstOrDefaultAsync();


            if (a == null)
            {
                return Ok(new { idDSP = 0 });

            }
            return Ok(new DSPViewModel
            {
                /*********************************************/

                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Responsable = a.Responsable,
                Telefono = a.Telefono,
                StatusInicioCarpeta = a.StatusInicioCarpeta,
                DistritoId = a.DistritoId,
                NombreDistrito = a.Distrito.Nombre,
                Clave = a.Clave,
                Tipo = a.Tipo,
                NombreSub = a.NombreSub
            });

        }


        // GET: api/DSPs/ListarPorDistritoyICarpeta
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Oficialia de partes,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido Mixto,Coordinador-Jurídico,Coordinador-Jurídico,Director-Subprocurador,Notificador,Agente-Policía,Facilitador, Facilitador-Mixto,AMPO-IL")]
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ListarPorDistritoViewModel>> ListarPorDistritoyICarpeta([FromRoute] Guid id)
        {
            var dsp = await _context.DSPs
                .Where(x => x.DistritoId == id)
                .Where(x => x.StatusInicioCarpeta == true)
                .Where(x => x.StatusDSP == true)
                .ToListAsync();

            return dsp.Select(a => new ListarPorDistritoViewModel
            {
                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Tipo = a.Tipo,
                NombreSub = a.NombreSub
            });

        }

        // GET: api/DSPs/ListarPorDistritoyICarpeta
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ListarPorDistritoViewModel>> ListarDspSinCAT([FromRoute] Guid id)
        {
            var dsp = await _context.DSPs
                .Where(x => x.DistritoId == id)
                .Where(x => x.StatusInicioCarpeta == true)
                .Where(x => x.StatusDSP == true)
                .Where(x => x.Clave != "CAT")
                .ToListAsync();

            return dsp.Select(a => new ListarPorDistritoViewModel
            {
                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Tipo = a.Tipo,
                NombreSub = a.NombreSub
            });

        }

        // GET: api/DSPs/ListarPISP
        [HttpGet("[action]")]
        public async Task<IEnumerable<DSPViewModel>> ListarPISP()
        {
            var dsp = await _context.DSPs
                .Where(a => a.Tipo == "PI" || a.Tipo == "SP")
                .Include(a => a.Distrito)
                .ToListAsync();

            return dsp.Select(a => new DSPViewModel
            {
                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Responsable = a.Responsable,
                Telefono = a.Telefono,
                StatusInicioCarpeta = a.StatusInicioCarpeta,
                DistritoId = a.DistritoId,
                NombreDistrito = a.Distrito.Nombre,
                Clave = a.Clave,
                Tipo = a.Tipo,
                NombreSub = a.NombreSub
            });

        }

        // GET: api/DSPs/ListarSPporDistrito
        [HttpGet("[action]/{iddistrito}")]
        public async Task<IEnumerable<DSPViewModel>> ListarSPporDistrito([FromRoute] Guid iddistrito)
        {
            var dsp = await _context.DSPs
                .Where(a => a.Tipo == "SP")
                .Where(a => a.DistritoId == iddistrito)
                .Include(a => a.Distrito)
                .ToListAsync();

            return dsp.Select(a => new DSPViewModel
            {
                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Responsable = a.Responsable,
                Telefono = a.Telefono,
                StatusInicioCarpeta = a.StatusInicioCarpeta,
                DistritoId = a.DistritoId,
                NombreDistrito = a.Distrito.Nombre,
                Clave = a.Clave,
                Tipo = a.Tipo,
                NombreSub = a.NombreSub
            });

        }

        // GET: api/DSPs/ListarPIporDistrito
        [HttpGet("[action]/{iddistrito}")]
        public async Task<IEnumerable<DSPViewModel>> ListarPIporDistrito([FromRoute] Guid iddistrito)
        {
            var dsp = await _context.DSPs
                .Where(a => a.Tipo == "PI")
                .Where(a => a.DistritoId == iddistrito)
                .Include(a => a.Distrito)
                .ToListAsync();

            return dsp.Select(a => new DSPViewModel
            {
                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Responsable = a.Responsable,
                Telefono = a.Telefono,
                StatusInicioCarpeta = a.StatusInicioCarpeta,
                DistritoId = a.DistritoId,
                NombreDistrito = a.Distrito.Nombre,
                Clave = a.Clave,
                Tipo = a.Tipo,
                NombreSub = a.NombreSub
            });

        }

        private bool DSPExists(Guid id)
        {
            return _context.DSPs.Any(e => e.IdDSP == id);
        }

        // PUT: api/ModuloServcio/DesactivarDSP
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{idDSP}")]
        public async Task<IActionResult> DesactivarDSP([FromRoute] Guid idDSP)
        {


            var subp = await _context.DSPs.FirstOrDefaultAsync(s => s.IdDSP == idDSP);

            if (subp == null)
            {
                return NotFound();
            }

            subp.StatusDSP = false;

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

        // PUT: api/ModuloServicio/ActivarDSP
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{idDSP}")]
        public async Task<IActionResult> ActivarDSP([FromRoute] Guid idDSP)
        {

            var subp = await _context.DSPs.FirstOrDefaultAsync(s => s.IdDSP == idDSP);

            if (subp == null)
            {
                return NotFound();
            }

            subp.StatusDSP = true;

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

        // GET: api/DSPs/ListarporId
        [HttpGet("[action]/{idDSP}")]
        public async Task<IActionResult> ListarporId([FromRoute] Guid idDSP)
        {
            var dsp = await _context.DSPs
                .Where(a => a.IdDSP == idDSP)
                .Include(a => a.Distrito)
                .FirstOrDefaultAsync();

            if (dsp == null)
            {
                NotFound("No hay registros");
            }

            try
            {
                return Ok(new DSPViewModel
                {
                    IdDSP = dsp.IdDSP,
                    NombreSubDir = dsp.NombreSubDir,
                    Responsable = dsp.Responsable,
                    Telefono = dsp.Telefono,
                    StatusInicioCarpeta = dsp.StatusInicioCarpeta,
                    DistritoId = dsp.DistritoId,
                    NombreDistrito = dsp.Distrito.Nombre,
                    Clave = dsp.Clave,
                    Tipo = dsp.Tipo,
                    NombreSub = dsp.NombreSub,
                    StatusDSP = dsp.StatusDSP
                });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

        }
        // PUT: api/Distritoes/ClonarDSPs
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> ClonarDSPs([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoCnx.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var dsp = await ctx.DSPs.FirstOrDefaultAsync(a => a.IdDSP == model.IdDSP);

                    if (dsp == null)
                    {
                        dsp = new DSP();
                        ctx.DSPs.Add(dsp);
                    }

                    dsp.IdDSP = model.IdDSP;
                    dsp.DistritoId = model.DistritoId;
                    dsp.Clave = model.Clave;
                    dsp.NombreSubDir = model.NombreSubDir;
                    dsp.NombreSub = model.NombreSub;
                    dsp.Responsable = model.Responsable;
                    dsp.Telefono = model.Telefono;
                    dsp.StatusInicioCarpeta = model.StatusInicioCarpeta;
                    dsp.Tipo = model.Tipo;
                    dsp.StatusDSP = model.StatusDSP;

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
        // PUT: api/ModuloServicio/ActDesDSPxDis
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{idDSP}/{distritoCnx}")]
        public async Task<IActionResult> ActDesDSPxDis([FromRoute] Guid idDSP, Guid distritoCnx)
        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + distritoCnx.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var dsp = await ctx.DSPs.FirstOrDefaultAsync(s => s.IdDSP == idDSP);

                    if (dsp.StatusDSP)
                    {
                        dsp.StatusDSP = false;
                        await ctx.SaveChangesAsync();
                    }
                    else
                    {
                        dsp.StatusDSP = true;
                        await ctx.SaveChangesAsync();
                    }

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
    }
}