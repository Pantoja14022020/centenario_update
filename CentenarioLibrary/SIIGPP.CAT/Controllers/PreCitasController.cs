using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.PRegistro;
using SIIGPP.Entidades.M_Cat.PCitas;
using SIIGPP.Datos.M_Cat.PCitas;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.PRegistro;
using SIIGPP.Entidades.M_ControlAcceso.Usuarios;


namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreCitasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public PreCitasController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/PreCitas
        [HttpGet]
        public IEnumerable<PreCitas> PreCitas()
        {
            return _context.PreCitas;
        }

        //POST: api/Precitas/ListarPorAgencia
        [HttpPost("[action]/{agenciaid}")]
        public async Task<IActionResult> ListarPorAgenciayDia([FromRoute] Guid agenciaid)
        {
            if(!ModelState.IsValid)
            {
                return Ok(new { error = "Información Incompleta" });
            }
            try
            {

                var citas = await _context.PreCitas
                                .Include(registros => registros.registros)
                                .Join(_context.PreAtenciones, pCitas => pCitas.registros.IdPRegistro, pAtenciones => pAtenciones.PRegistroId, (pCitas, pAtenciones) => new { pCitas, pAtenciones })
                                .Join(_context.PreRaps, CitaAtencion => CitaAtencion.pAtenciones.IdPAtencion, pRaps => pRaps.PAtencionId, (CitaAtencion, pRaps) => new { CitaAtencion.pCitas, CitaAtencion.pAtenciones, pRaps })
                                .Join(_context.Personas, CitaAtencionRap => CitaAtencionRap.pRaps.PersonaId, cPersonas => cPersonas.IdPersona, (CitaAtencionRap, cPersonas) => new { CitaAtencionRap.pCitas, CitaAtencionRap.pAtenciones, CitaAtencionRap.pRaps, cPersonas })
                                .OrderByDescending(a => a.pCitas.fecha)
                                .OrderByDescending(a => a.pCitas.Hora)
                                .Where(a => a.pCitas.registros.Atendido == false)
                                .Where(a => a.pCitas.AgenciaId == agenciaid)

                                .ToListAsync();

                return Ok(citas.Select(a => new ListarCitasViewModel
                {

                    PreRegistroId = a.pCitas.registros.IdPRegistro,
                    IdCita = a.pCitas.IdPCita,
                    nombre = a.cPersonas.Nombre,
                    apellidoPaterno = a.cPersonas.ApellidoPaterno,
                    apellidoMaterno = a.cPersonas.ApellidoMaterno,
                    IdPersona = a.cPersonas.IdPersona,
                    fecha = a.pCitas.fecha,
                    Hora = a.pCitas.Hora,
                    telefono = a.cPersonas.Telefono1,
                    edad = a.cPersonas.Edad,
                    RBreve=a.pCitas.registros.RBreve,
                    fechaSuceso=a.pCitas.registros.fechaSuceso
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

        }


        //GET: api/Precitas/ListarPorAgencia
        [HttpGet("[action]/{registroid}")]
        public async Task<IActionResult> ListarRegistro([FromRoute] Guid registroid)
        {

            try
            {

                var cita = await _context.PreCitas
                                .Include(registros => registros.registros)
                                .Join(_context.PreAtenciones, pCitas => pCitas.registros.IdPRegistro, pAtenciones => pAtenciones.PRegistroId, (pCitas, pAtenciones) => new { pCitas, pAtenciones })
                                .Join(_context.PreRaps, CitaAtencion => CitaAtencion.pAtenciones.IdPAtencion, pRaps => pRaps.PAtencionId, (CitaAtencion, pRaps) => new { CitaAtencion.pCitas, CitaAtencion.pAtenciones, pRaps })
                                .Join(_context.Personas, CitaAtencionRap => CitaAtencionRap.pRaps.PersonaId, cPersonas => cPersonas.IdPersona, (CitaAtencionRap, cPersonas) => new { CitaAtencionRap.pCitas, CitaAtencionRap.pAtenciones, CitaAtencionRap.pRaps, cPersonas })
                                .OrderByDescending(a => a.pCitas.fecha)
                                .OrderByDescending(a => a.pCitas.Hora)
                                .Where(a => a.pCitas.registros.IdPRegistro == registroid)
                                
                                .Take(1)
                                .FirstOrDefaultAsync();

                return Ok(new ListarCitasViewModel
                {

                    PreRegistroId = cita.pCitas.registros.IdPRegistro,
                    IdCita = cita.pCitas.IdPCita,
                    nombre = cita.cPersonas.Nombre,
                    apellidoPaterno = cita.cPersonas.ApellidoPaterno,
                    apellidoMaterno = cita.cPersonas.ApellidoMaterno,
                    IdPersona = cita.cPersonas.IdPersona,
                    fecha = cita.pCitas.fecha,
                    Hora = cita.pCitas.Hora,
                    telefono = cita.cPersonas.Telefono1,
                    edad = cita.cPersonas.Edad,
                    RBreve = cita.pCitas.registros.RBreve,
                    fechaSuceso = cita.pCitas.registros.fechaSuceso
                });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }

        }




        //GET: api/Precitas/ListarPorAgencia
        [HttpGet("[action]/{agenciaid}")]
        public async Task<IActionResult> ListarPorAgencia([FromRoute] Guid agenciaid)
        {
            
            try
            {

                var citas = await _context.PreCitas
                                .Include(registros => registros.registros)
                                .Join(_context.PreAtenciones, pCitas => pCitas.registros.IdPRegistro, pAtenciones => pAtenciones.PRegistroId, (pCitas, pAtenciones) => new { pCitas, pAtenciones })
                                .Join(_context.PreRaps, CitaAtencion => CitaAtencion.pAtenciones.IdPAtencion, pRaps => pRaps.PAtencionId, (CitaAtencion, pRaps) => new { CitaAtencion.pCitas, CitaAtencion.pAtenciones, pRaps })
                                .Join(_context.Personas, CitaAtencionRap => CitaAtencionRap.pRaps.PersonaId, cPersonas => cPersonas.IdPersona, (CitaAtencionRap, cPersonas) => new { CitaAtencionRap.pCitas, CitaAtencionRap.pAtenciones, CitaAtencionRap.pRaps, cPersonas })
                                .OrderByDescending(a => a.pCitas.fecha)
                                .OrderByDescending(a => a.pCitas.Hora)
                                .Where(a => a.pCitas.registros.Atendido == false)
                                .Where(a => a.pCitas.AgenciaId == agenciaid)

                                .ToListAsync();

                return Ok(citas.Select(a => new ListarCitasViewModel {

                    PreRegistroId=a.pCitas.registros.IdPRegistro,
                    IdCita=a.pCitas.IdPCita,
                    nombre=a.cPersonas.Nombre,
                    apellidoPaterno=a.cPersonas.ApellidoPaterno,
                    apellidoMaterno=a.cPersonas.ApellidoMaterno,
                    IdPersona=a.cPersonas.IdPersona,
                    fecha=a.pCitas.fecha,
                    Hora=a.pCitas.Hora,
                    telefono=a.cPersonas.Telefono1,
                    edad=a.cPersonas.Edad,
                    RBreve = a.pCitas.registros.RBreve,
                    fechaSuceso = a.pCitas.registros.fechaSuceso
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }

        }

        // POST: api/PreCitas/Crear
        [HttpPost("[Action]")]
        public async Task<IActionResult> Crear([FromBody] CrearCitasViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PreCitas insertarCita = new PreCitas 
            { 
                PRegistroId=model.PRegistroId,
                fecha=model.fecha,
                Hora=model.Hora,
                AgenciaId=model.AgenciaId
            };

            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoId.ToString().ToUpper())).Options;
            using (var ctx = new DbContextSIIGPP(options))
            {
                try
                {
                    ctx.PreCitas.Add(insertarCita);
                    await ctx.SaveChangesAsync();
                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                    result.StatusCode = 402;
                    return result;
                }

                return Ok(new { idcita = insertarCita.IdPCita });
            }
       }
    }
}
