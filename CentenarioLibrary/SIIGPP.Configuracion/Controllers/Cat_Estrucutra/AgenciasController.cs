using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Configuracion.Models.Cat_Estrucutra.Agencia; 
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_IL.Agendas;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenciasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IConfiguration _configuration;

        public AgenciasController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Agencias
        [HttpGet]
        public IEnumerable<Agencia> GetAgencias()
        {
            return _context.Agencias;
        }

        // GET: api/Agencias/Listar
        //[Authorize(Roles = " Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<AgenciaViewModel>> Listar()
        {
            var agencia = await _context.Agencias.Include(a => a.DSP).Include(a => a.DSP.Distrito).OrderBy(a => a.Nombre).ToListAsync();

            return agencia.Select(a => new AgenciaViewModel
            {
                IdAgencia = a.IdAgencia,
                DSPId = a.DSPId,
                NombreDirSub = a.DSP.NombreSubDir,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Contacto = a.Contacto,
                TipoServicio = a.TipoServicio,
                DistritoId = a.DSP.Distrito.IdDistrito,
                NombreDistrito = a.DSP.Distrito.Nombre,
                Clave = a.Clave,
                Condetencion = a.Condetencion,
                Activa = a.Activa,
                Municipio = a.Municipio,
            });

        }
        // GET: api/Agencias/ListarPorDirSubStatus/1

        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ListarPorDirSubViewModel>> ListarPorDirSubStatus([FromRoute] Guid id)
        {
            var agencia = await _context.Agencias.Where(x => x.DSPId == id).Where(x => x.TipoServicio == "Servicio interno").ToListAsync();

            return agencia.Select(a => new ListarPorDirSubViewModel
            {
                IdAgencia = a.IdAgencia,
                Nombre = a.Nombre
            });

        }
        // GET: api/Agencias/ListarPorDirSub/1

        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ListarPorDirSubViewModel>> ListarPorDirSub([FromRoute] Guid id)
        {
            var agencia = await _context.Agencias.Where(x => x.DSPId == id).OrderBy(x => x.Nombre).ToListAsync();

            return agencia.Select(a => new ListarPorDirSubViewModel
            {
                IdAgencia = a.IdAgencia,
                Nombre = a.Nombre
            });

        }


        // PUT: api/Agencias/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var agencia = await _context.Agencias.FirstOrDefaultAsync(a => a.IdAgencia == model.IdAgencia);

                if (agencia == null)
                {
                    return NotFound();
                }
                agencia.DSPId = model.DSPId;
                agencia.Nombre = model.Nombre;
                agencia.Direccion = model.Direccion;
                agencia.Telefono = model.Telefono;
                agencia.Contacto = model.Contacto;
                agencia.TipoServicio = model.TipoServicio;
                agencia.Clave = model.Clave;
                agencia.Condetencion = model.Condetencion;
                agencia.Activa = model.Activa;
                agencia.Municipio = model.Municipio;
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
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // POST: api/Agencias/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {   
                Agencia agencia = new Agencia
                {
                    DSPId = model.DSPId,
                    Nombre = model.Nombre,
                    Direccion = model.Direccion,
                    Telefono = model.Telefono,
                    Contacto = model.Contacto,
                    TipoServicio = model.TipoServicio,
                    Clave = model.Clave,
                    Condetencion = model.Condetencion,
                    Activa = model.Activa,
                    Municipio = model.Municipio,
                
                
                };

                _context.Agencias.Add(agencia);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

                return Ok(new { id = agencia.IdAgencia });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // POST: api/Agencias/Replicar
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Replicar([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoId.ToString().ToUpper())).Options;

                using (var ctx = new DbContextSIIGPP(options))
                {
                    var agencia = await ctx.Agencias.FirstOrDefaultAsync(a => a.IdAgencia == model.IdAgencia);

                    if (agencia == null)
                    {
                        agencia = new Agencia();
                        ctx.Agencias.Add(agencia);
                    }

                    agencia.IdAgencia = model.IdAgencia;
                    agencia.DSPId = model.DSPId;
                    agencia.Nombre = model.Nombre;
                    agencia.Direccion = model.Direccion;
                    agencia.Telefono = model.Telefono;
                    agencia.Contacto = model.Contacto;
                    agencia.TipoServicio = model.TipoServicio;
                    agencia.Clave = model.Clave;
                    agencia.Condetencion = model.Condetencion;
                    agencia.Activa = model.Activa;
                    agencia.Municipio = model.Municipio;

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


        // GET: api/Agencias/Listarporid
        [HttpGet("[action]/{IdAgencia}")]
        public async Task<IActionResult> Listarporid([FromRoute] Guid IdAgencia)
        {
            var agencia = await _context.Agencias
                                .Where(a=>a.IdAgencia == IdAgencia)
                                .Include(a => a.DSP)
                                .Include(a => a.DSP.Distrito)
                                .FirstOrDefaultAsync();


            if (agencia == null)
            {
                return NotFound("No hay registros");

            }
            try
            {
                return Ok(new AgenciaViewModel
                {
                    /*********************************************/

                    IdAgencia = agencia.IdAgencia,
                    DSPId = agencia.DSPId,
                    NombreDirSub = agencia.DSP.NombreSubDir,
                    ContacotDirSub = agencia.DSP.Responsable,
                    cargoResponsable = agencia.DSP.cargoResponsable,
                    Nombre = agencia.Nombre,
                    Direccion = agencia.Direccion,
                    Telefono = agencia.Telefono,
                    Contacto = agencia.Contacto,
                    TipoServicio = agencia.TipoServicio,
                    NombreDistrito = agencia.DSP.Distrito.Nombre,
                    Clave = agencia.Clave,
                    Condetencion = agencia.Condetencion,
                    Activa = agencia.Activa,
                    Municipio = agencia.Municipio,
                });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

        }
        // GET: api/Agencias/Listarporid
        [HttpGet("[action]/{Agencia}")]
        public async Task<IActionResult> ListarporNombre([FromRoute] string Agencia)
        {
            var agencia = await _context.Agencias
                                .Where(a => a.Nombre== Agencia)
                                .Include(a => a.DSP)
                                .Include(a => a.DSP.Distrito)
                                .FirstOrDefaultAsync();


            if (agencia == null)
            {
                return NotFound("No hay registros");

            }
            return Ok(new AgenciaViewModel
            {
                /*********************************************/

                IdAgencia = agencia.IdAgencia,
                DSPId = agencia.DSPId,
                NombreDirSub = agencia.DSP.NombreSubDir,
                ContacotDirSub = agencia.DSP.Responsable,
                cargoResponsable = agencia.DSP.cargoResponsable,
                Nombre = agencia.Nombre,
                Direccion = agencia.Direccion,
                Telefono = agencia.Telefono,
                Contacto = agencia.Contacto,
                TipoServicio = agencia.TipoServicio,
                NombreDistrito = agencia.DSP.Distrito.Nombre,
                Clave = agencia.Clave,
                Condetencion = agencia.Condetencion,
                Activa = agencia.Activa
            });

        }

        // GET: api/Agencias/ListarporDSP
        //[Authorize(Roles = " Administrador")]
        [HttpGet("[action]/{Iddsp}")]
        public async Task<IEnumerable<AgenciaViewModel>> ListarporDSP([FromRoute] Guid Iddsp)
        {
            var agencia = await _context
                .Agencias.Include(a => a.DSP)
                .Where(a => a.DSPId == Iddsp)
                .Include(a => a.DSP.Distrito)
                .OrderBy(a => a.Nombre)
                .ToListAsync();

            return agencia.Select(a => new AgenciaViewModel
            {
                IdAgencia = a.IdAgencia,
                DSPId = a.DSPId,
                NombreDirSub = a.DSP.NombreSubDir,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Contacto = a.Contacto,
                TipoServicio = a.TipoServicio,
                DistritoId = a.DSP.Distrito.IdDistrito,
                NombreDistrito = a.DSP.Distrito.Nombre,
                Clave = a.Clave,
                Condetencion = a.Condetencion,
                Activa = a.Activa
            });

        }

        // GET: api/Agencias/ListarporDSP
        //[Authorize(Roles = " Administrador")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IEnumerable<AgenciaDSPDisViewModel>> ListarporDSPSP([FromRoute] Guid distritoId)
        {
            var agencia = await _context.Agencias
                .Include(a => a.DSP)
                .Include(a => a.DSP.Distrito)
                .Where(x => x.DSP.DistritoId == distritoId)
                .Where(x => x.DSP.Clave == "DGSP")
                .OrderBy(a => a.Nombre)
                .ToListAsync();

            return agencia.Select(a => new AgenciaDSPDisViewModel
            {
                DistritoId = a.DSP.Distrito.IdDistrito,
                NombreDistrito = a.DSP.Distrito.Nombre,

                DSPId = a.DSPId,
                NombreDirSub = a.DSP.NombreSubDir,
                Responsable = a.DSP.Responsable,
                TelefonoDSP = a.DSP.Telefono,
                StatusInicioCarpeta = a.DSP.StatusInicioCarpeta,
                ClaveDSP = a.DSP.Clave,
                Tipo = a.DSP.Tipo,
                NombreSub = a.DSP.NombreSub,

                IdAgencia = a.IdAgencia,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Contacto = a.Contacto,
                TipoServicio = a.TipoServicio,
                Clave = a.Clave,
                Condetencion = a.Condetencion,
                Activa = a.Activa,

            });

        }

        // GET: api/Agencias/ListarporDistritoyCriminalistica
        [HttpGet("[action]/{IdDistrito}")]
        public async Task<IActionResult> ListarporDistritoyCriminalistica([FromRoute] Guid IdDistrito)
        {
            var agencia = await _context.Agencias
                .Where(a => a.Nombre.Contains("Criminalistica"))
                .Where(a => a.DSP.DistritoId == IdDistrito)
                .Include(a => a.DSP)
                .Include(a => a.DSP.Distrito)
                .FirstOrDefaultAsync();


            if (agencia == null)
            {
                return NotFound("No hay registros");

            }
            return Ok(new AgenciaViewModel
            {
                /*********************************************/

                IdAgencia = agencia.IdAgencia,
                DSPId = agencia.DSPId,
                NombreDirSub = agencia.DSP.NombreSubDir,
                Nombre = agencia.Nombre,
                Direccion = agencia.Direccion,
                Telefono = agencia.Telefono,
                Contacto = agencia.Contacto,
                TipoServicio = agencia.TipoServicio,
                NombreDistrito = agencia.DSP.Distrito.Nombre,
                Clave = agencia.Clave,
                Condetencion = agencia.Condetencion,
                Activa = agencia.Activa
            });

        }


        // GET: api/Agencias/ListarcarpetasporDsP

        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ListarPorDirSubViewModel>> ListarcarpetasporDsP([FromRoute] Guid id)
        {
            var agencia = await _context.Agencias
                .Where(x => x.DSPId == id)
                .Where(x => x.TipoServicio == "Servicio externo con inicio de carpeta")
                .ToListAsync();

            return agencia.Select(a => new ListarPorDirSubViewModel
            {
                IdAgencia = a.IdAgencia,
                Nombre = a.Nombre
            });

        }

        //EP para obtener el listado de recepción de carpetas activas de las agencias
        // GET: api/Agencias/ListarcarpetasporDsP
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ListarPorDirSubViewModel>> ListarRecepcionCA([FromRoute] Guid id)
        {
            var agencia = await _context.ModuloServicios
                .Include(x => x.Agencia)
                .Where(x => x.Agencia.DSPId == id)
                .Where(x => x.Condicion == true)
                .Where(x => x.Tipo == "Recepción")
                .Where(x => x.Agencia.TipoServicio == "Servicio externo con inicio de carpeta")
                .ToListAsync();

            return agencia.Select(a => new ListarPorDirSubViewModel
            {
                IdAgencia = a.Agencia.IdAgencia,
                Nombre = a.Agencia.Nombre
            });
        }

        // GET: api/Agencias/ListarporDSPDetenciones
        //[Authorize(Roles = " Administrador")]
        [HttpGet("[action]/{Iddsp}")]
        public async Task<IEnumerable<AgenciaViewModel>> ListarporDSPDetenciones([FromRoute] Guid Iddsp)
        {
            var agencia = await _context
                .Agencias.Include(a => a.DSP)
                .Where(a => a.DSPId == Iddsp)
                .Where(a => a.Condetencion == true)
                .Include(a => a.DSP.Distrito)
                .ToListAsync();

            return agencia.Select(a => new AgenciaViewModel
            {
                IdAgencia = a.IdAgencia,
                DSPId = a.DSPId,
                NombreDirSub = a.DSP.NombreSubDir,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Contacto = a.Contacto,
                TipoServicio = a.TipoServicio,
                DistritoId = a.DSP.Distrito.IdDistrito,
                NombreDistrito = a.DSP.Distrito.Nombre,
                Clave = a.Clave,
                Condetencion = a.Condetencion,
                Activa = a.Activa
            });

        }

        // GET: api/Agencias/ListarporDSPServiciosAtencion
        //[Authorize(Roles = " Administrador")]
        [HttpGet("[action]/{Iddsp}")]
        public async Task<IEnumerable<AgenciaViewModel>> ListarporDSPServiciosAtencion([FromRoute] Guid Iddsp)
        {
            var agencia = await _context
                .Agencias.Include(a => a.DSP)
                .Where(a => a.DSPId == Iddsp)
                .Include(a => a.DSP.Distrito)
                .ToListAsync();

            var agencias = agencia.Select(a => new AgenciaViewModel
            {
                IdAgencia = a.IdAgencia,
                DSPId = a.DSPId,
                NombreDirSub = a.DSP.NombreSubDir,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Contacto = a.Contacto,
                TipoServicio = a.TipoServicio,
                DistritoId = a.DSP.Distrito.IdDistrito,
                NombreDistrito = a.DSP.Distrito.Nombre,
                Clave = a.Clave,
                Condetencion = a.Condetencion,
                Activa = a.Activa
            });

            IEnumerable<AgenciaViewModel> items2 = new AgenciaViewModel[] { };

            foreach (var agenciaf in agencias)
            {

                var servicio = await _context.ASPs.Include(a => a.Agencia.DSP)
                    .Where(a => a.AgenciaId == agenciaf.IdAgencia)
                    .Where(a => a.ServicioPericial.AtencionVictimas == true)
                    .Include(a => a.Agencia)
                    .Include(a => a.ServicioPericial)
                    .Include(a => a.Agencia.DSP.Distrito)
                    .FirstOrDefaultAsync();


                if (servicio != null) {  

                    IEnumerable<AgenciaViewModel> ReadLines()
                    {
                        IEnumerable<AgenciaViewModel> item2;


                        item2 = (new[]{new AgenciaViewModel{

                            IdAgencia = agenciaf.IdAgencia,
                            DSPId = agenciaf.DSPId,
                            NombreDirSub = agenciaf.NombreDirSub,
                            Nombre = agenciaf.Nombre,
                            Direccion = agenciaf.Direccion,
                            Telefono = agenciaf.Telefono,
                            Contacto = agenciaf.Contacto,
                            TipoServicio = agenciaf.TipoServicio,
                            DistritoId = agenciaf.DistritoId,
                            NombreDistrito = agenciaf.Nombre,
                            Clave = agenciaf.Clave,
                            Condetencion = agenciaf.Condetencion,
                            Activa = agenciaf.Activa
                            }});

                        return item2;
                    }

                        items2 = items2.Concat(ReadLines());
                }

            }

            return items2;
        }

        // GET: api/Agencias/ListarporDSPmedicos
        //[Authorize(Roles = " Administrador")]
        [HttpGet("[action]/{Iddsp}")]
        public async Task<IEnumerable<AgenciaViewModel>> ListarporDSPmedicos([FromRoute] Guid Iddsp)
        {
            var agencia = await _context
                .Agencias.Include(a => a.DSP)
                .Where(a => a.DSPId == Iddsp)
                .Where(a => a.Nombre.Contains("medico") || a.Nombre.Contains("medicos") || a.Nombre.Contains("Medico") || a.Nombre.Contains("Medicos"))
                .Include(a => a.DSP.Distrito)
                .ToListAsync();

            return agencia.Select(a => new AgenciaViewModel
            {
                IdAgencia = a.IdAgencia,
                DSPId = a.DSPId,
                NombreDirSub = a.DSP.NombreSubDir,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Contacto = a.Contacto,
                TipoServicio = a.TipoServicio,
                DistritoId = a.DSP.Distrito.IdDistrito,
                NombreDistrito = a.DSP.Distrito.Nombre,
                Clave = a.Clave,
                Condetencion = a.Condetencion,
                Activa = a.Activa
            });

        }

        private bool AgenciaExists(Guid id)
        {
            return _context.Agencias.Any(a => a.IdAgencia == id);
        }

        //EP para obtener el listado de la agencias pertenecientes a CAT del distrito
        //GET: api/Agencias/ListarAgenciasCAT
        [HttpGet("[action]/{distritoId}")]
        public async Task<IEnumerable<AgenciaViewModel>> ListarCATAgencia([FromRoute] Guid distritoId)
        {
            var agencia = await _context.Agencias
                .Include(a => a.DSP)
                .Include(a => a.DSP.Distrito)
                .Where(a => a.DSP.NombreSubDir == "Dirección General de Atención Temprana")
                .Where(a => a.DSP.DistritoId == distritoId)
                .ToListAsync();

            return agencia.Select(a => new AgenciaViewModel
            {
                IdAgencia = a.IdAgencia,
                DSPId = a.DSPId,
                NombreDirSub = a.DSP.NombreSubDir,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Contacto = a.Contacto,
                TipoServicio = a.TipoServicio,
                DistritoId = a.DSP.Distrito.IdDistrito,
                NombreDistrito = a.DSP.Distrito.Nombre,
                Clave = a.Clave,
                Condetencion = a.Condetencion,
                Activa = a.Activa
            });

        }

    }
}