using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.SP.ModelsSP.DocsDiligenciasForaneas;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_SP.DocsDiligenciasForaneas;
using SIIGPP.Entidades.M_Cat.Representantes;
using SIIGPP.SP.ModelsSP.DocsDiligencias;

namespace SIIGPP.SP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocsDiligenciasForaneasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;

        public DocsDiligenciasForaneasController(DbContextSIIGPP context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/DocsDiligencias/Listar
        [Authorize(Roles = "Perito")]
        [HttpGet("[action]/{idrdiligencias}")]
        public async Task<IEnumerable<DocsDiligenciaForaneasViewModel>> Listar([FromRoute] Guid idrdiligencias)
        {
            var docs = await _context.DocsDiligenciaForaneas
                .Where(a => a.RDiligenciasForaneasId == idrdiligencias)
                .Include(a => a.RDiligenciasForaneas)
                .ToListAsync();




            return docs.Select(a => new DocsDiligenciaForaneasViewModel
            {
                IdDocsDiligenciaForaneas = a.IdDocsDiligenciaForaneas,
                RDiligenciasForaneasId = a.RDiligenciasForaneasId,
                TipoDocumento = a.TipoDocumento,
                DescripcionDocumento = a.DescripcionDocumento,
                FechaRegistro = a.FechaRegistro,
                Puesto = a.Puesto,
                Ruta = a.Ruta,
                Usuario = a.Usuario,
                uDistrito = a.uDistrito,
                uSubproc = a.uSubproc,
                uAgencia = a.uAgencia,
                uUsuario = a.uUsuario,
                uPuesto = a.uPuesto,
                uModulo = a.uModulo,
                fechasysregistro = a.fechasysregistro

            });

        }



        // POST: api/DocsDiligencias/Crear
        [HttpPost("[action]")]
        [Authorize(Roles = "Perito")]
        public async Task<IActionResult> Crear([FromBody] CrearForaneasViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            DateTime fecha = System.DateTime.Now;
            DocsDiligenciaForaneas docssf = new DocsDiligenciaForaneas();
            try
            {
            string[] idnuevo = model.servicios.Split("; ");

            for (int i = 0; i < idnuevo.Length; i++)
            {
                 docssf = new DocsDiligenciaForaneas
                 {


                    RDiligenciasForaneasId = Guid.Parse(idnuevo[i]),
                    TipoDocumento = model.TipoDocumento,
                    DescripcionDocumento = model.DescripcionDocumento,
                    FechaRegistro = model.FechaRegistro,
                    Puesto = model.Puesto,
                    Ruta = model.Ruta,
                    Usuario = model.Usuario,
                    uDistrito = model.uDistrito,
                    uSubproc = model.uSubproc,
                    uAgencia = model.uAgencia,
                    uUsuario = model.uUsuario,
                    uPuesto = model.uPuesto,
                    uModulo = model.uModulo,
                    fechasysregistro = fecha


                 };

                    _context.DocsDiligenciaForaneas.Add(docssf);

                    await _context.SaveChangesAsync();
                }
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
                var path = ("https://" + HttpContext.Request.Host.Value + "/Carpetas/" + nombreCarpeta + "/" + nombreArchivo + extension);
                if (file.Length > 0)
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(stream);

                return Ok(new { count = 1, ruta = path, rutafisica = filePath });

            }
            catch (Exception ex)
            {
                return BadRequest("Error al copiar: " + ex);
            }
        }


        // POST: api/DocsDiligencias/Borra
        //[HttpPost("Borra/{nombrecarpeta}/{nombreArchivo}")]
        //[HttpPost("[action]/{nombreCarpeta}/{nombreArchivo}")]
        //public async Task<ActionResult<string>> Borra(IFormFile file, [FromRoute] string nombreCarpeta, string nombreArchivo)
        //{
        //    try
        //    {
        //        //                
        //        string patchp = Path.Combine(_environment.ContentRootPath, "Carpetas\\" + nombreCarpeta);
        //        string extension;
        //        //
        //        extension = Path.GetExtension(file.FileName);
        //        var resultado = "";
        //        var filePath = Path.Combine(_environment.ContentRootPath, "Carpetas\\" + nombreCarpeta, nombreArchivo) + extension;
        //        var path = ("https://" + HttpContext.Request.Host.Value + "/Carpetas/" + nombreCarpeta + "/" + nombreArchivo + extension);
        //        if (file.Length > 0)
        //        {
        //            System.IO.File.Exists(filePath);
        //            await Task.Run(() => {
        //                GC.Collect();
        //                GC.WaitForPendingFinalizers();
        //                System.IO.File.Delete(@filePath);
        //                resultado = "borrado: " + filePath;
        //            });
        //        }
        //        else
        //        { resultado = "Nada"; }
        //        //
        //        return Ok(new { count = 1, ruta = path, rutafisica = filePath, resul = resultado });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error al borrar, código: " + ex);
        //    }
        //}
    }
}
