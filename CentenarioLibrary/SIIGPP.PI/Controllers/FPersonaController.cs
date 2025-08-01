using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.FPersonas;
using SIIGPP.Entidades.M_PI.FPersonas;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FPersonaController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;

        public FPersonaController(DbContextSIIGPP context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        // GET: api/FPersona/Listarporid
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{PersonaVisitaId}")]
        public async Task<IActionResult> Listarporid([FromRoute] Guid PersonaVisitaId)
        {
            var Fper = await _context.FPersonas
                .Where(a => a.PIPersonaVisitaId == PersonaVisitaId)
                .FirstOrDefaultAsync();

            if (Fper == null)
            {
                return NotFound("No hay registros");

            }

            return Ok(new FPersonaViewModel
            {
                 IdFPersona = Fper.IdFPersona,
                 PIPersonaVisitaId = Fper.PIPersonaVisitaId,
                 TipoDocumento = Fper.TipoDocumento,
                 DescripcionDocumento = Fper.DescripcionDocumento,
                 FechaRegistro = Fper.FechaRegistro,
                 Puesto = Fper.Puesto,
                 Ruta = Fper.Ruta,
                 UDistrito = Fper.UDistrito,
                 USubproc= Fper.USubproc,
                 UAgencia = Fper.UAgencia,
                 Usuario = Fper.Usuario,
                 UPuesto = Fper.UPuesto,
                 UModulo = Fper.UModulo,
                 Fechasys = Fper.Fechasys, 
            });

        }

        // POST: api/FPersona/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             FPersona Fper= new FPersona
            {
                PIPersonaVisitaId = model.PIPersonaVisitaId,
                TipoDocumento = model.TipoDocumento,
                DescripcionDocumento = model.DescripcionDocumento,
                FechaRegistro = model.FechaRegistro,
                Puesto = model.Puesto,
                Ruta =model.Ruta,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
            };

            _context.FPersonas.Add(Fper);

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

            return Ok();
        }



        //[HttpPost("Post/{nombre}" )]

        [HttpPost("[action]/{nombreCarpeta}/{nombreArchivo}")]
        public async Task<IActionResult> Post(IFormFile file, [FromRoute] string nombreCarpeta, string nombreArchivo)
        {
            try
            {



                //***********************************************************************************
                string patchp = Path.Combine(_environment.ContentRootPath, "Carpetas\\" + nombreCarpeta);

                if (!Directory.Exists(patchp))
                    Directory.CreateDirectory(patchp);

                string extension;

                extension = Path.GetExtension(file.FileName);


                var filePath = Path.Combine(_environment.ContentRootPath, "Carpetas\\" + nombreCarpeta, nombreArchivo + extension);
                var path = ("https://"+ HttpContext.Request.Host.Value + "/Carpetas/" + nombreCarpeta + "/" + nombreArchivo + extension);
                if (file.Length > 0)
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(stream);


                return Ok(new { count = 1, ruta = path });


            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }
        }


    }
}
