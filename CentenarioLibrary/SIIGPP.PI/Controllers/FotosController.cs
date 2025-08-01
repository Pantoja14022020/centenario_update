using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SIIGPP.Entidades.M_PI.CFotos;
using SIIGPP.PI.Models.Cfotos;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;

        public FotosController(DbContextSIIGPP context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/Fotos/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{ActosInvestigacionId}")]
        public async Task<IEnumerable<CFotosViewModel>> Listar([FromRoute] Guid ActosInvestigacionId)
        {
            var foto = await _context.Fotos
                .Where(a => a.RActoInvestigacionId == ActosInvestigacionId)
                .ToListAsync();

            return foto.Select(a => new CFotosViewModel
            {
                 IdFotos = a.IdFotos,
                 RActoInvestigacionId = a.RActoInvestigacionId, 
                 TipoDocumento = a.TipoDocumento,
                 DescripcionDocumento = a.DescripcionDocumento,
                 FechaRegistro = a.FechaRegistro,
                 Puesto  = a.Puesto,
                 Ruta = a.Ruta,
                 Usuario = a.Usuario,
                 uDistrito = a.UDistrito,
                 uSubproc = a.USubproc,
                 uAgencia = a.UAgencia,
                 uUsuario = a.Usuario,
                 uPuesto = a.UPuesto,
                 uModulo = a.UModulo,
                 fechasysregistro = a.Fechasys
         });

        }


        // POST: api/Fotos/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Fotos Foto = new Fotos
            {
            RActoInvestigacionId  = model.RActoInvestigacionId,
            TipoDocumento = model.TipoDocumento,
            DescripcionDocumento = model.DescripcionDocumento,
            FechaRegistro =model.FechaRegistro,
            Puesto = model.Puesto,
            Ruta = model.Ruta,
            UDistrito = model.uDistrito, 
            USubproc = model.uSubproc,
            UAgencia = model.uAgencia,
            Usuario = model.Usuario,
            UPuesto = model.uPuesto,
            UModulo = model.uModulo,
            Fechasys = System.DateTime.Now
        };

            _context.Fotos.Add(Foto);
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
