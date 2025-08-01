using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using SIIGPP.Entidades.M_JR.RDelitodelitosDerivados;
using SIIGPP.JR.Models.RRegistro;
using SIIGPP.Entidades.M_JR.RRegistros;
using SIIGPP.JR.Models.RRegistros;
using SIIGPP.Entidades.M_JR.RSeguimientoCumplimiento;
using SIIGPP.JR.Models.RSeguimientoCumplimiento;
using SIIGPP.JR.Models.REnvio;
using SIIGPP.Entidades.M_JR.RExpediente;

namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public RegistrosController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Registros
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet]
        public IEnumerable<Registro> GetRegistros()
        {
            return _context.Registros;
        }

        // GET: api/Registros/ListarRegistros
        // API: VALIDAR SI EXISTE EL EXPEDIETE 
        [HttpGet("[action]/{idEnvio}")]
        public async Task<IActionResult> ListarRegistros([FromRoute] Guid idEnvio)
        {
            try
            {
                var listaRegistros = await _context.Registros.Where(a => a.EnvioId == idEnvio).ToListAsync();


                return Ok(listaRegistros.Select(a => new RegistrosViewModel
                {
                    IdRegistro = a.IdRegistro,
                    EnvioId = a.EnvioId,
                    Tipo = a.Tipo,
                    QuienPorRegistro = a.QuienPorRegistro,
                    Descripcion = a.Descripcion,
                    Distrito = a.Distrito,
                    Dirsubproc = a.Dirsubproc,
                    Agencia  = a.Agencia,
                    Usuario = a.Usuario,
                    Puesto = a.Puesto,
                    Fechasis = a.Fechasis,
                    Numerooficio = a.Numerooficio,
                    
                })
                    );
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/Registros/ListarRegistroEspecifico
        // API: VALIDAR SI EXISTE EL EXPEDIETE 
        [HttpGet("[action]/{idExpediente}")]
        public async Task<IActionResult> ListarRegistroEspecifico([FromRoute] Guid idExpediente)
        {
            try
            {
                var detallesR = await _context.Registros
                .Include(a => a.Envio.Expediente)
                                .Where(a => a.Envio.Expediente.IdExpediente == idExpediente)
                                .FirstOrDefaultAsync();

                if (detallesR == null)
                {

                    return Ok(new { ner = 1 });
                }

                return Ok(new RegistrosViewModel
                {
                    IdRegistro = detallesR.IdRegistro,
                    EnvioId = detallesR.EnvioId,
                    Tipo = detallesR.Tipo,
                    QuienPorRegistro = detallesR.QuienPorRegistro,
                    Descripcion = detallesR.Descripcion,
                    Distrito = detallesR.Distrito,
                    Dirsubproc = detallesR.Dirsubproc,
                    Agencia = detallesR.Agencia,
                    Usuario = detallesR.Usuario,
                    Puesto = detallesR.Puesto,
                    Fechasis = detallesR.Fechasis,
                    Numerooficio = detallesR.Numerooficio,

                });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

        }
        // POST: api/Registros/Crear
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearRegistro([FromBody] RegistrosViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }



                Registro GR = new Registro
                {
                    EnvioId = model.EnvioId,
                    Tipo = model.Tipo,
                    QuienPorRegistro = model.QuienPorRegistro,
                    Descripcion = model.Descripcion,
                    Distrito = model.Distrito,
                    Dirsubproc =model.Dirsubproc,
                    Agencia = model.Agencia,
                    Usuario = model.Usuario,
                    Puesto = model.Puesto,
                    Fechasis = System.DateTime.Now,
                    Numerooficio = model.Numerooficio,

                };

                _context.Registros.Add(GR);

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

        private bool RegistroExists(Guid id)
        {
            return _context.Registros.Any(e => e.IdRegistro == id);
        }

        
    }
}