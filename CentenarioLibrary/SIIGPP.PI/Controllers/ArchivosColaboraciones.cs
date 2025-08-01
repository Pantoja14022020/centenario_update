using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.PI.Models.ArchivosColaboraciones;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_PI.ArchivosColaboraciones;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SIIGPP.PI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosColaboracionesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;

        public ArchivosColaboracionesController(DbContextSIIGPP context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        // GET: api/ArchivosColaboraciones/ListarPresentacionesComparecencia
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{presentacionescid}")]
        public async Task<IEnumerable<ComparecenciaViewModel>> ListarPresentacionesComparecencia([FromRoute] Guid presentacionescid)
        {
            var Sub = await _context.ArchivosComparecencias
                .Where(a => a.PresentacionesYCId == presentacionescid)
                .ToListAsync();

            return Sub.Select(a => new ComparecenciaViewModel
            { 
                IdrchivosComparecencia = a.IdrchivosComparecencia,
                PresentacionesYCId = a.PresentacionesYCId,
                TipoDocumento = a.TipoDocumento,
                DescripcionDocumento = a.DescripcionDocumento,
                Puesto = a.Puesto,
                Ruta = a.Ruta,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys


            });

        }

        // POST: api/ArchivosColaboraciones/CrearPresentacionesComparecencia
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearPresentacionesComparecencia([FromBody] CrearComparecenciaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ArchivosComparecencia ac = new ArchivosComparecencia
            {

                PresentacionesYCId = model.PresentacionesYCId,
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
                Fechasys = System.DateTime.Now
            };

            _context.ArchivosComparecencias.Add(ac);
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

        // GET: api/ArchivosColaboraciones/ListarOrdenesAprehension
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{ordenaprehensionid}")]
        public async Task<IEnumerable<OrdenAprehensionViewModel>> ListarOrdenesAprehension([FromRoute] Guid ordenaprehensionid)
        {
            var Sub = await _context.ArchivosOAprensions
                .Where(a => a.OrdenAprensionId == ordenaprehensionid)
                .ToListAsync();

            return Sub.Select(a => new OrdenAprehensionViewModel
            {
                IdArchivosOAprension = a.IdArchivosOAprension,
                OrdenAprensionId = a.OrdenAprensionId,
                TipoDocumento = a.TipoDocumento,
                DescripcionDocumento = a.DescripcionDocumento,
                Puesto = a.Puesto,
                Ruta = a.Ruta,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys


            });

        }

        // POST: api/ArchivosColaboraciones/CrearOrdenesAprehension
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearOrdenesAprehension([FromBody] CrearOrdenAprehensionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ArchivosOAprension oa = new ArchivosOAprension
            {

                OrdenAprensionId = model.OrdenAprensionId,
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
                Fechasys = System.DateTime.Now
            };

            _context.ArchivosOAprensions.Add(oa);
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
