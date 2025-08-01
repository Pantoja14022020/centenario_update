using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.CAT.Models.DocumentosPersonas;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosPesonasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public DocumentosPesonasController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/DocumentosPesonas
        [HttpGet]
        public IEnumerable<DocumentosPesona> GetDocumentosPesonas()
        {
            return _context.DocumentosPesonas;
        }
        
        // GET: api/DocumentosPesonas/Listar
 
        //[Authorize(Roles = "Administrador, Director, AMPO-AMP, Director, Coordinador, AMPO-AMP Mixto, AMPO-AMP Detenido,Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{personaId}")]
        public async Task<IActionResult> Listar([FromRoute] Guid personaId)
        {
            var dp = await _context.DocumentosPesonas 
                            .Where(x => x.PersonaId == personaId)
                            .FirstOrDefaultAsync();

            if (dp == null)
            {
                return Ok(new { Ruta = "", TipoDocumento = "No se registro documento de esta persona" });
            }
            return Ok(new DocumentosPesona
            {
                /*********************************************/
                /*CAT_RAP*/
                IdDocumentoPersona = dp.IdDocumentoPersona,
                Ruta = dp.Ruta,  
                TipoDocumento = dp.TipoDocumento
                /*********************************************/
            });
        }
        // GET: api/DocumentosPesonas/Listar2

        [Authorize(Roles = "Administrador,Director,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Coordinador, Jurídico, Recepción, Facilitador, Notificador,AMPO-IL,Recepción")]
        [HttpGet("[action]/{personaId}")]
        public async Task<IActionResult> Listar2([FromRoute] Guid personaId)
        {
            var dp = await _context.DocumentosPesonas
                            .Where(x => x.PersonaId == personaId)
                            .FirstOrDefaultAsync();

            if (dp == null)
            {
                return Ok(new { Ruta = "", TipoDocumento ="No se registro documento de esta persona" });

            }

            return Ok(new DocumentosPesona
            {
                /*********************************************/
                /*CAT_RAP*/
                Ruta = dp.Ruta,
                TipoDocumento = dp.TipoDocumento
                /*********************************************/

            });

        }

        // POST: api/DocumentosPesonas/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            DocumentosPesona docs = new DocumentosPesona
            {

                PersonaId = model.PersonaId,
                TipoDocumento = model.TipoDocumento,
                NombreDocumento = model.NombreDocumento,
                Descripcion = model.Descripcion,
                FechaRegistro = fecha,
                Ruta = model.Ruta,
                Distrito = model.Distrito,
                DirSubProc = model.DirSubProc,
                Agencia = model.Agencia,
                Usuario = model.Usuario,
                Puesto = model.Puesto


            };

            _context.DocumentosPesonas.Add(docs);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();
        }

        // PUT: api/DocumentosPesonas/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var ao = await _context.DocumentosPesonas.FirstOrDefaultAsync(a => a.IdDocumentoPersona == model.IdDocumentoPersona);

            if (ao == null)
            {
                return NotFound();
            }

            ao.TipoDocumento = model.TipoDocumento;
            ao.NombreDocumento = model.NombreDocumento;
            ao.Descripcion  = model.Descripcion;
            ao.Ruta = model.Ruta;
            ao.Distrito = model.Distrito;
            ao.DirSubProc = model.DirSubProc;
            ao.Agencia = model.Agencia;
            ao.Usuario = model.Usuario;
            ao.Puesto = model.Puesto;

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

        // PUT: api/DocumentosPesonas/ActualizarporIdpersona
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarporIdpersona([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var ao = await _context.DocumentosPesonas.FirstOrDefaultAsync(a => a.PersonaId == model.idpersona);

            if (ao == null)
            {
                return NotFound();
            }

            ao.TipoDocumento = model.TipoDocumento;
            ao.NombreDocumento = model.NombreDocumento;
            ao.Descripcion = model.Descripcion;
            ao.Ruta = model.Ruta;
            ao.Distrito = model.Distrito;
            ao.DirSubProc = model.DirSubProc;
            ao.Agencia = model.Agencia;
            ao.Usuario = model.Usuario;
            ao.Puesto = model.Puesto;

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



        private bool DocumentosPesonaExists(Guid id)
        {
            return _context.DocumentosPesonas.Any(e => e.IdDocumentoPersona == id);
        }
    }
}