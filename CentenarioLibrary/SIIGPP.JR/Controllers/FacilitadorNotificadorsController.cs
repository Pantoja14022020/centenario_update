using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_ControlAcceso.Usuarios;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_IL.Citatorios;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.JR.Models.RCitatorioRecordatorio;
using SIIGPP.JR.Models.RFacilitadorNotificador;
using SIIGPP.Entidades.M_JR.RFacilitadorNotificador;

namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitadorNotificadorsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public FacilitadorNotificadorsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/FacilitadorNotificadors
        // [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet]
        public IEnumerable<FacilitadorNotificador> GetFacilitadorNotificadors()
        {
            return _context.FacilitadorNotificadors;
        }

        // GET: api/FacilitadorNotificadors/ListarPorFecha 
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{moduloServicioId}/{fechaInicio}/{fechaFinal}")]
        public async Task<IActionResult> ListarPorFecha([FromRoute] Guid moduloServicioId, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var nombre_usuario = await _context.CitatorioRecordatorios
                                        .Include(a => a.Sesion)
                                        .Where(a => a.Sesion.ModuloServicioId == moduloServicioId)
                                        .Where(a => (a.FechaHoraCita > fechaInicio && a.FechaHoraCita < fechaFinal) ||
                                                    (a.FechaHoraCita.AddMinutes(a.Duracion) > fechaInicio && a.FechaHoraCita < fechaFinal) ||
                                                    (a.FechaHoraCita < fechaInicio && a.FechaHoraCita.AddMinutes(a.Duracion) > fechaInicio))
                                        .ToListAsync();


                var nombres = nombre_usuario.Select(c => c.uf_Nombre).ToList();

                var nombres_2 = await _context.Usuarios
                                        .Where(a => a.ModuloServicioId == moduloServicioId)
                                        .Where(a => !nombres.Contains(a.nombre))
                                        .ToListAsync();

                return Ok(nombres_2.Where(uuu => uuu.condicion == true).Select(uuu => new GET_FacilitadorNotificadorViewModel1
                {
                    NombreUsuario = uuu.nombre,
                    ModuloServicioId = uuu.ModuloServicioId,
                    IdFacilitadorNotificador = uuu.IdUsuario,
                    puesto = uuu.puesto,

                }));

            //return Ok(nombreFinalesReales);

            }
            catch (Exception ex)
            {

                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // Endpoint para obtener los facilitadores con sus sesiones agendadas en el dia por la fecha
        //GET: api/FacilitadorNotificadors/ListarSesionesFecha
        [HttpGet("[action]/{moduloServicio}/{fecha}")]
        public async Task<IActionResult> ListarSesionesFecha([FromRoute] Guid moduloServicio, DateTime fecha)
        {
            try
            {
                var facilitadores = await _context.Usuarios
                                        .Where(a => a.ModuloServicioId == moduloServicio)
                                        .Where(a => a.RolId == Guid.Parse("45A8B7C7-8368-43DD-8D8A-C4861E51B8A2"))
                                        .Where(a => a.condicion == true)
                                        .Select(a => new
                                        {
                                            a.nombre,
                                            sesiones = _context.CitatorioRecordatorios
                                                .Where(b => b.uf_Nombre == a.nombre)                                              
                                                .Where(b => b.FechaHoraCita >= fecha && b.FechaHoraCita < fecha.AddDays(1))
                                                .Select(b => new
                                                {
                                                    b.FechaHoraCita,
                                                    b.Duracion,
                                                    horaFormateada = $"{b.FechaHoraCita:HH:mm tt} - {b.FechaHoraCita.AddMinutes(b.Duracion):HH:mm tt}"
                                                })
                                                .OrderBy(b => b.FechaHoraCita)
                                                .ToList()
                                        })
                                        .ToListAsync();
                

                return Ok(facilitadores);
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/FacilitadorNotificadors/Listar 
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IEnumerable<GET_FacilitadorNotificadorViewModel>> Listar([FromRoute] Guid distritoId)
        {
            var facnot = await _context.FacilitadorNotificadors
                                .Where(a => a.ModuloServicio.Agencia.DSP.DistritoId == distritoId)
                                .Include(a => a.ModuloServicio.usuarios)
                                .Include(a => a.ModuloServicio).ToListAsync();

            return facnot.Select(a => new GET_FacilitadorNotificadorViewModel
            {
                IdFacilitadorNotificador = a.IdFacilitadorNotificador,
                ModuloServicioId = a.ModuloServicioId,
                StatusAsignado = a.StatusAsignado,
                StatusActivo = a.StatusActivo,
                NombreModulo = a.ModuloServicio.Nombre,
            });

        }
        // GET: api/FacilitadorNotificadors/ListarFacilitadores2
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}/{idAgencia}/{idModulo}")]
        public async Task<IEnumerable<GET_FacilitadorNotificadorViewModel>> ListarFacilitadores2([FromRoute] Guid distritoId, Guid idAgencia, Guid idModulo)
        {
          
            var facnot = await _context.FacilitadorNotificadors
                                .Include(a => a.ModuloServicio)
                                    .ThenInclude(m => m.usuarios)
                                .Where(a => a.ModuloServicio.Tipo == "Facilitador")
                                .Where(a => a.ModuloServicio.Agencia.DSP.DistritoId == distritoId)
                                .Where(a => a.ModuloServicio.Agencia.IdAgencia == idAgencia)
                                .Where(a => a.ModuloServicio.IdModuloServicio == idModulo)
                                .ToListAsync();

            return facnot.SelectMany(a => a.ModuloServicio.usuarios.Select(u => new GET_FacilitadorNotificadorViewModel
            {
                IdFacilitadorNotificador = a.IdFacilitadorNotificador,
                ModuloServicioId = a.ModuloServicioId,
                StatusAsignado = a.StatusAsignado,
                StatusActivo = a.StatusActivo,
                NombreModulo = a.ModuloServicio.Nombre,
                NombreUsuario = u.nombre,
                puesto = u.puesto,
            }));
        }

        // GET: api/FacilitadorNotificadors/ListarM
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}/{idAgencia}")]
        public async Task<IEnumerable<GET_FacilitadorNotificadorViewModel>> ListarM([FromRoute] Guid distritoId, Guid idAgencia)
        {
            var facnot = await _context.FacilitadorNotificadors
                                .Include(a => a.ModuloServicio)
                                .ThenInclude(m => m.usuarios)
                                .Where(a => a.ModuloServicio.Tipo == "Facilitador")
                                .Where(a => a.ModuloServicio.Agencia.DSP.DistritoId == distritoId)
                                .Where(a => a.ModuloServicio.Agencia.IdAgencia == idAgencia)
                                .Where(a => a.ModuloServicio.Condicion == true)                                
                                .OrderBy(a => a.ModuloServicio.Nombre)
                                .ToListAsync();

            return facnot.SelectMany(a => a.ModuloServicio.usuarios.Select(u => new GET_FacilitadorNotificadorViewModel
            {
                IdFacilitadorNotificador = a.IdFacilitadorNotificador,
                ModuloServicioId = a.ModuloServicioId,
                StatusAsignado = a.StatusAsignado,
                StatusActivo = a.StatusActivo,
                NombreModulo = a.ModuloServicio.Nombre,
                NombreUsuario = u.nombre
            }));
        }

        // GET: api/FacilitadorNotificadors/ListarAgenciasModulos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IActionResult> ListarAgenciasModulos([FromRoute] Guid distritoId)
        {
            try
            {
                var agencias = await _context.Agencias
                                .Include(a => a.DSP)
                                .Where(a => a.Activa == true)
                                .Where(a => a.DSP.DistritoId == distritoId)
                                .Where(a => a.DSP.Clave == "CJR")
                                .Select(a => new
                                {
                                    a.IdAgencia,
                                    a.Nombre,
                                    modulos = _context.ModuloServicios
                                        .Where(b => b.AgenciaId == a.IdAgencia && b.Condicion == true && b.Tipo == "Facilitador")
                                        .OrderBy(b => b.Nombre)
                                        .ToList()
                                })
                                .ToListAsync();

                return Ok(agencias);
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }            
        }

        // GET: api/FacilitadorNotificadors/ListarNotificadores
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IEnumerable<GET_FacilitadorNotificadorViewModel>> ListarNotificadores([FromRoute] Guid distritoId)
        {
            var facnot = await _context.FacilitadorNotificadors
                                .Include(a => a.ModuloServicio.usuarios)
                                .Include(a=> a.ModuloServicio)
                                .Where(a => a.ModuloServicio.Tipo == "Notificador")
                                .Where(a => a.ModuloServicio.Agencia.DSP.DistritoId == distritoId)
                                .OrderBy(a => a.ModuloServicio.Nombre)
                                .ToListAsync();

            return facnot.SelectMany(a => a.ModuloServicio.usuarios.Where(u => u.condicion == true).Select(u => new GET_FacilitadorNotificadorViewModel
            {
                IdFacilitadorNotificador = a.IdFacilitadorNotificador,
                ModuloServicioId = a.ModuloServicioId,
                StatusAsignado = a.StatusAsignado,
                StatusActivo = a.StatusActivo,
                NombreModulo = a.ModuloServicio.Nombre,
                NombreUsuario = u.nombre,
                IdUsuario = u.IdUsuario,
                puesto = u.puesto,
                

    }));

        }
        // GET: api/FacilitadorNotificadors/ListarModulos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IEnumerable<string>> ListarModulos([FromRoute] Guid distritoId)
        {
            var facnot = await _context.FacilitadorNotificadors
                                .Include(a => a.ModuloServicio)
                                    .ThenInclude(m => m.usuarios)
                                .Where(a => a.ModuloServicio.Tipo == "Facilitador")
                                .Where(a => a.ModuloServicio.Agencia.DSP.DistritoId == distritoId)
                                .ToListAsync();

            return facnot
                .SelectMany(a => a.ModuloServicio.usuarios.Select(u => a.ModuloServicio.Nombre))
                .Distinct(); 
        }

        [HttpGet("[action]/{distritoId}")]
        public async Task<IEnumerable<GET_FacilitadorNotificadorViewModel>> ListarFacilitadores([FromRoute] Guid distritoId)
        {
            var facnot = await _context.FacilitadorNotificadors
                                .Include(a => a.ModuloServicio.usuarios)
                                .Include(a => a.ModuloServicio)
                                .Include(a => a.ModuloServicio.usuarios)
                                .Where(a => a.ModuloServicio.Tipo == "Facilitador")
                                .Where(a => a.ModuloServicio.Agencia.DSP.DistritoId == distritoId)
                                .ToListAsync();

            return facnot.Select(a => new GET_FacilitadorNotificadorViewModel
            {
                IdFacilitadorNotificador = a.IdFacilitadorNotificador,
                ModuloServicioId = a.ModuloServicioId,
                StatusAsignado = a.StatusAsignado,
                StatusActivo = a.StatusActivo,
                NombreModulo = a.ModuloServicio.Nombre,

            });

        }

        // GET: api/FacilitadorNotificadors/ListarFacilitadoresAsignados 
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IEnumerable<GET_FacilitadorNotificadorViewModel>> ListarFacilitadoresAsignados([FromRoute] Guid distritoId)
        {
            var facnot = await _context.FacilitadorNotificadors
                                .Include(a => a.ModuloServicio.usuarios)
                                .Include(a => a.ModuloServicio)
                                .Where(a => a.ModuloServicio.Tipo == "Facilitador") 
                                .Where(a => a.ModuloServicio.Agencia.DSP.DistritoId == distritoId)
                                .Where(a => a.StatusAsignado == true)
                                .ToListAsync();

            return facnot.Select(a => new GET_FacilitadorNotificadorViewModel
            {
                IdFacilitadorNotificador = a.IdFacilitadorNotificador,
                ModuloServicioId = a.ModuloServicioId,
                StatusAsignado = a.StatusAsignado,
                StatusActivo = a.StatusActivo,
                NombreModulo = a.ModuloServicio.Nombre,
            });

        }
        // GET: api/FacilitadorNotificadors/ListarNotificadoresAsignados 
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IEnumerable<GET_FacilitadorNotificadorViewModel>> ListarNotificadoresAsignados([FromRoute] Guid distritoId)
        {
            var facnot = await _context.FacilitadorNotificadors
                                .Include(a => a.ModuloServicio.usuarios)
                                .Include(a => a.ModuloServicio)
                                .Where(a => a.ModuloServicio.Tipo == "Notificador")
                                .Where(a => a.ModuloServicio.Agencia.DSP.DistritoId == distritoId)
                                .Where(a => a.StatusAsignado == true)
                                .ToListAsync();

            return facnot.Select(a => new GET_FacilitadorNotificadorViewModel
            {
                IdFacilitadorNotificador = a.IdFacilitadorNotificador,
                ModuloServicioId = a.ModuloServicioId,
                StatusAsignado = a.StatusAsignado,
                StatusActivo = a.StatusActivo,
                NombreModulo = a.ModuloServicio.Nombre,
            });

        }

        // GET: api/FacilitadorNotificadors/BuscarFacilitador
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IActionResult> BuscarFacilitador([FromRoute] Guid distritoId)
        {
            var busqueda = await _context.FacilitadorNotificadors 
                                         .Where(a => a.StatusAsignado == false)
                                         .Where(a => a.StatusActivo == true)
                                         .Where(a => a.ModuloServicio.Tipo=="Facilitador")
                                         .Where(a => a.ModuloServicio.Agencia.DSP.DistritoId == distritoId)
                                         .OrderBy(a => a.IdFacilitadorNotificador)
                                         .FirstOrDefaultAsync();

            if (busqueda == null)
            {
                return Ok("No hay registros");
            }
            return Ok(new GET_FacilitadorNotificadorViewModel
            {
                IdFacilitadorNotificador = busqueda.IdFacilitadorNotificador,
                ModuloServicioId = busqueda.ModuloServicioId,
                StatusAsignado = busqueda.StatusAsignado,
                StatusActivo = busqueda.StatusActivo,
            });
        }
        // GET: api/FacilitadorNotificadors/BuscarNotificador
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IActionResult> BuscarNotificador([FromRoute] Guid distritoId)
        {
            var busqueda = await _context.FacilitadorNotificadors
                                         .Where(a => a.StatusAsignado == false)
                                         .Where(a => a.StatusActivo == true)
                                         .Where(a => a.ModuloServicio.Tipo == "Notificador")
                                         .Where(a => a.ModuloServicio.Agencia.DSP.DistritoId == distritoId)
                                         .OrderBy(a => a.IdFacilitadorNotificador)
                                         .FirstOrDefaultAsync();

            if (busqueda == null)
            {
                return Ok("No hay registros");
            }
            return Ok(new GET_FacilitadorNotificadorViewModel
            {
                IdFacilitadorNotificador = busqueda.IdFacilitadorNotificador,
                ModuloServicioId = busqueda.ModuloServicioId,
                StatusAsignado = busqueda.StatusAsignado,
                StatusActivo = busqueda.StatusActivo,
            });
        }
        //POST: api/FacilitadorNotificadors/CrearFacilitadorNotificador 
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearFacilitadorNotificador ([FromBody] POST_FacilitadorNotificadorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //************************************************************
              

                FacilitadorNotificador fn = new FacilitadorNotificador
                {
                    ModuloServicioId = model.ModuloServicioId,
                    StatusAsignado = model.StatusAsignado,
                    StatusActivo = model.StatusActivo, 

                }; 

                _context.FacilitadorNotificadors.Add(fn);

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

        // PUT: api/FacilitadorNotificadors/StatusReAsignacionFacilitador
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> StatusReAsignacionFacilitador([FromBody] PUT_StatusAsignadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          

            var db = await _context.FacilitadorNotificadors
                                   .Where(a => a.ModuloServicio.Agencia.DSP.DistritoId == model.distritoId)
                                   .Where(a => a.StatusAsignado == true)
                                   .Where(a => a.StatusActivo == true)
                                   .Where(a => a.ModuloServicio.Tipo == "Facilitador")
                                   .FirstOrDefaultAsync(a => a.IdFacilitadorNotificador == model.IdFacilitadorNotificador);

            if (db == null)
            {

                return NotFound();
            }

            db.StatusAsignado = model.StatusAsignado;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }

            return Ok();
        }
        // PUT: api/FacilitadorNotificadors/StatusReAsignacionNotificador
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> StatusReAsignacionNotificador([FromBody] PUT_StatusAsignadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var db = await _context.FacilitadorNotificadors
                                   .Where(a => a.ModuloServicio.Agencia.DSP.DistritoId == model.distritoId)
                                   .Where(a => a.StatusAsignado == true)
                                   .Where(a => a.StatusActivo == true)
                                   .Where(a => a.ModuloServicio.Tipo == "Notificador")
                                   .FirstOrDefaultAsync(a => a.IdFacilitadorNotificador == model.IdFacilitadorNotificador);

            if (db == null)
            {

                return NotFound();
            }

            db.StatusAsignado = model.StatusAsignado;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }

            return Ok();
        }
        // PUT: api/FacilitadorNotificadors/StatusActivarDesactivar
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> StatusActivarDesactivar([FromBody] PUT_StatusInactivoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var db = await _context.FacilitadorNotificadors.FirstOrDefaultAsync(a => a.IdFacilitadorNotificador == model.IdFacilitadorNotificador);

            if (db == null)
            {
                return NotFound();
            }

            db.StatusActivo = model.StatusActivo;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }

            return Ok();
        }



    }
}