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
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.CArchivos;
using SIIGPP.CAT.Models.CArchivos;
using SIIGPP.Entidades.M_Administracion;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;
        private IConfiguration _configuration;
        public ArchivosController(DbContextSIIGPP context, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;
        }

        // GET: api/Archivos/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<CArchivosViewModel>> Listar([FromRoute] Guid RHechoId)
        {
            var archivo = await _context.Archivos
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return archivo.Select(a => new CArchivosViewModel
            {
                IdArchivos = a.IdArchivos,
                RHechoId = a.RHechoId,
                TipoDocumento = a.TipoDocumento,
                NombreDocumento = a.NombreDocumento,
                DescripcionDocumento = a.DescripcionDocumento,
                Ruta = a.Ruta,
                Fecha = a.Fecha,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia =a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
             });

        }


        // POST: api/Archivos/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Archivos archivos = new Archivos 
            {
            RHechoId = model.RHechoId,
            TipoDocumento = model.TipoDocumento,
            NombreDocumento = model.NombreDocumento,
            DescripcionDocumento = model.DescripcionDocumento,
            Ruta = model.Ruta,
            Fecha = model.Fecha,
            UDistrito = model.UDistrito,
            USubproc = model.USubproc,
            UAgencia = model.UAgencia,
            Usuario = model.Usuario,
            UPuesto = model.UPuesto,
            UModulo = model.UModulo,
            Fechasys = System.DateTime.Now,
            };

            _context.Archivos.Add(archivos);
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
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/Archivos/Eliminar
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var consultaArchivo = await _context.Archivos.Where(a => a.IdArchivos == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaArchivo == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningún archivo de investigación con la información enviada" });
                }
                else
                {
                    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("LOG")).Options;
                    using (var ctx = new DbContextSIIGPP(options))
                    {
                        Guid gLog = Guid.NewGuid();
                        DateTime fecha = System.DateTime.Now;
                        LogAdmon laRegistro = new LogAdmon
                        {
                            IdLogAdmon = gLog,
                            UsuarioId = model.usuario,
                            Tabla = model.tabla,
                            FechaMov = fecha,
                            RegistroId = model.infoBorrado.registroId,
                            SolicitanteId = model.solicitante,
                            RazonMov = model.razon,
                            MovimientoId = new Guid("d0f9cafc-d7ea-4856-bb92-04f00df3a532") // ESTE DATO HAY QUE BUSCARLO EN LA TABLA C_TIPO_MOV DE LA BD CENTENARIO_LOG
                        };

                        ctx.Add(laRegistro);
                        LogArchivos archivo = new LogArchivos
                        {
                            LogAdmonId = gLog,
                            IdArchivos = consultaArchivo.IdArchivos,
                            RHechoId = consultaArchivo.RHechoId,
                            TipoDocumento = consultaArchivo.TipoDocumento,
                            NombreDocumento = consultaArchivo.NombreDocumento,
                            DescripcionDocumento = consultaArchivo.DescripcionDocumento,
                            Ruta = consultaArchivo.Ruta,
                            Fecha = consultaArchivo.Fecha,
                            UDistrito = consultaArchivo.UDistrito,
                            USubproc = consultaArchivo.USubproc,
                            UAgencia = consultaArchivo.UAgencia,
                            Usuario = consultaArchivo.Usuario,
                            UPuesto = consultaArchivo.UPuesto,
                            UModulo = consultaArchivo.UModulo,
                            Fechasys = consultaArchivo.Fechasys
                        };
                        ctx.Add(archivo);
                        _context.Remove(consultaArchivo);

                        //SE APLICAN TODOS LOS CAMBIOS A LA BD
                        await ctx.SaveChangesAsync();
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "Archivo eliminado Correctamente" });
        }

    }
}
