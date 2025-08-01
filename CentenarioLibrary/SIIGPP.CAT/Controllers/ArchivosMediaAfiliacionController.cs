using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.ArchivosMediaAfiliaciones;
using SIIGPP.Entidades.M_Cat.MedAfiliacion;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosMediaAfiliacionController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;

        public ArchivosMediaAfiliacionController(DbContextSIIGPP context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        // GET: api/ArchivosMediaAfiliacion/Listar
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL, Recepción")]
        [HttpGet("[action]/{MediaAfiliacionid}")]
        public async Task<IEnumerable<ArchivosMediaAfiliacionViewModel>> Listar([FromRoute]Guid MediaAfiliacionid)
        {
            var media = await _context.ArchivosMediaAfiliacions
                .Where(a => a.MediaAfiliacionid == MediaAfiliacionid)
                .ToListAsync();

            return media.Select(a => new ArchivosMediaAfiliacionViewModel
            {
                IdArchivosMediaAfiliacion = a.IdArchivosMediaAfiliacion,
                MediaAfiliacionid = a.MediaAfiliacionid,
                TipoDocumento = a.TipoDocumento,
                DescripcionDocumento = a.DescripcionDocumento,
                Puesto = a.Puesto,
                Ruta =a.Ruta,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
            });

        }

        // POST: api/ArchivosMediaAfiliacion/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ArchivosMediaAfiliacion Arch = new ArchivosMediaAfiliacion
            {
                
                MediaAfiliacionid = model.MediaAfiliacionid,
                TipoDocumento = model.TipoDocumento,
                DescripcionDocumento = model.DescripcionDocumento,
                Puesto = model.Puesto,
                Ruta = model.Ruta,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
                
            };

            _context.ArchivosMediaAfiliacions.Add(Arch);

            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.2" });
                result.StatusCode = 402;
                return result;
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
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.2" });
                result.StatusCode = 402;
                return result;
            }
        }

    }
}