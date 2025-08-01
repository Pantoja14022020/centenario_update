using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.MedidasProteccion;
using SIIGPP.CAT.Models.DocumentacionSistema.Actualizaciones;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.DocumentacionSistema;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Security.Policy;
using SIIGPP.Entidades.M_Cat.CArchivos;
using SIIGPP.CAT.Models.Vehiculos;


namespace SIIGPP.Configuracion.Controllers.DocumentacionSistema
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActualizacionesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;
        private IConfiguration _configuration;

        public ActualizacionesController(DbContextSIIGPP context, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;
        }


        // GET: api/Actualizaciones/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ActualizacionesViewModel>> Listar()
        {
            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-7F662EC1-6705-406E-BCD0-F56ADE7BCAE2".ToUpper())).Options;
            using (var ctx = new DbContextSIIGPP(options))
            {
                var ao = await ctx.Actualizaciones
                .ToListAsync();

                return ao.Select(a => new ActualizacionesViewModel
                {
                    IdActualizacion = a.IdActualizacion,
                    ClaveActualizacion = a.ClaveActualizacion,
                    FechaActualizacion = a.FechaActualizacion,
                    NombreActualizacion = a.NombreActualizacion,
                    DescripcionActualizacion = a.DescripcionActualizacion,
                    LigaServidor = a.LigaServidor,
                    RutaDocumento = a.RutaDocumento,
                    RamasRelacionadas = a.RamasRelacionadas,
                    HayQuerys = a.HayQuerys,
                    QuerysRelacionados = a.QuerysRelacionados,
                    ShaCommitRepositorioCompilado = a.ShaCommitRepositorioCompilado,
                    RealizadoPor = a.RealizadoPor,
                    MostrarUsuarios = a.MostrarUsuarios,
                    FechaSys = a.FechaSys,
                    MostrarAviso = a.MostrarAviso,
                    MensajeAviso = a.MensajeAviso,
                    MostrarPDFAviso = a.MostrarPDFAviso,
                    ModuloCentenario = a.ModuloCentenario,



                });
            }

        }

        // GET: api/Actualizaciones/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL,Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{modulo}")]
        public async Task<IEnumerable<ActualizacionesViewModel>> ListarUsuarios(string modulo)
        {
            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-7F662EC1-6705-406E-BCD0-F56ADE7BCAE2".ToUpper())).Options;
            using (var ctx = new DbContextSIIGPP(options))
            {
                var ao = await ctx.Actualizaciones.
                Where(a => a.MostrarUsuarios == true).
                Where(a => a.ModuloCentenario == modulo).
                ToListAsync();


                return ao.Select(a => new ActualizacionesViewModel
                {
                    IdActualizacion = a.IdActualizacion,
                    ClaveActualizacion = a.ClaveActualizacion,
                    FechaActualizacion = a.FechaActualizacion,
                    NombreActualizacion = a.NombreActualizacion,
                    DescripcionActualizacion = a.DescripcionActualizacion,
                    LigaServidor = a.LigaServidor,
                    RutaDocumento = a.RutaDocumento,
                    RamasRelacionadas = a.RamasRelacionadas,
                    HayQuerys = a.HayQuerys,
                    QuerysRelacionados = a.QuerysRelacionados,
                    ShaCommitRepositorioCompilado = a.ShaCommitRepositorioCompilado,
                    RealizadoPor = a.RealizadoPor,
                    MostrarUsuarios = a.MostrarUsuarios,
                    FechaSys = a.FechaSys,
                    MostrarAviso = a.MostrarAviso,
                    MensajeAviso = a.MensajeAviso,
                    MostrarPDFAviso = a.MostrarPDFAviso,
                    ModuloCentenario = a.ModuloCentenario,


                });
            }

        }

        // GET: api/Actualizaciones/comprobarAvisos
        [HttpGet("[action]")]
        public async Task<IActionResult> ComprobarAvisos()
        {
            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-7F662EC1-6705-406E-BCD0-F56ADE7BCAE2".ToUpper())).Options;
            using (var ctx = new DbContextSIIGPP(options))
            {
                var ao = await ctx.Actualizaciones
                    .Where(a => a.MostrarAviso == true)
                .FirstOrDefaultAsync();

                if (ao == null)
                {
                    return Ok(new { MostrarAviso = false});


                }
                return Ok ( new ActualizacionesViewModel
                {
                    LigaServidor = ao.LigaServidor,
                    RutaDocumento = ao.RutaDocumento,
                    MostrarAviso = ao.MostrarAviso,
                    MensajeAviso = ao.MensajeAviso,
                    MostrarPDFAviso = ao.MostrarPDFAviso,
                });
            }

        }

        // POST: api/Actualizaciones/CrearActualizacion
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearActualizacion([FromBody] ActualizacionesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var Existeclave = await _context.Actualizaciones.FirstOrDefaultAsync(a => a.ClaveActualizacion == model.ClaveActualizacion);

                if (Existeclave == null)
                {
                    Actualizaciones actualizaciones = new Actualizaciones

                    {
                        ClaveActualizacion = model.ClaveActualizacion,
                        FechaActualizacion = model.FechaActualizacion,
                        NombreActualizacion = model.NombreActualizacion,
                        DescripcionActualizacion = model.DescripcionActualizacion,
                        LigaServidor = model.LigaServidor,
                        RutaDocumento = model.RutaDocumento,
                        RamasRelacionadas = model.RamasRelacionadas,
                        HayQuerys = model.HayQuerys,
                        QuerysRelacionados = model.QuerysRelacionados,
                        ShaCommitRepositorioCompilado = model.ShaCommitRepositorioCompilado,
                        RealizadoPor = model.RealizadoPor,
                        MostrarUsuarios = model.MostrarUsuarios,
                        FechaSys = System.DateTime.Now,
                        MostrarAviso = model.MostrarAviso,
                        MensajeAviso = model.MensajeAviso,
                        MostrarPDFAviso = model.MostrarPDFAviso,
                        ModuloCentenario = model.ModuloCentenario,
                    };

                    _context.Actualizaciones.Add(actualizaciones);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Ok(new { exito = 0, aviso = "No puedes guardar esta actualizacion porque ya existe un registro con esta clave" });
                }

                
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

        // PUT: api/Actualizaciones/EditarActualizacion
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpPut("[action]")]
        public async Task<IActionResult> EditarActualizacion([FromBody] ActualizacionesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var EdicionActualizacion = await _context.Actualizaciones.FirstOrDefaultAsync(a => a.IdActualizacion == model.IdActualizacion);

            if (EdicionActualizacion == null)
            {
                return NotFound();
            }


            EdicionActualizacion.ClaveActualizacion = model.ClaveActualizacion;
            EdicionActualizacion.FechaActualizacion = model.FechaActualizacion;
            EdicionActualizacion.NombreActualizacion = model.NombreActualizacion;
            EdicionActualizacion.DescripcionActualizacion = model.DescripcionActualizacion;
            EdicionActualizacion.LigaServidor = model.LigaServidor;
            EdicionActualizacion.RutaDocumento = model.RutaDocumento;
            EdicionActualizacion.RamasRelacionadas = model.RamasRelacionadas;
            EdicionActualizacion.HayQuerys = model.HayQuerys;
            EdicionActualizacion.QuerysRelacionados = model.QuerysRelacionados;
            EdicionActualizacion.ShaCommitRepositorioCompilado = model.ShaCommitRepositorioCompilado;
            EdicionActualizacion.RealizadoPor = model.RealizadoPor;
            EdicionActualizacion.MostrarUsuarios = model.MostrarUsuarios;
            EdicionActualizacion.MostrarAviso = model.MostrarAviso;
            EdicionActualizacion.MensajeAviso = model.MensajeAviso;
            EdicionActualizacion.MostrarPDFAviso = model.MostrarPDFAviso;
            EdicionActualizacion.ModuloCentenario = model.ModuloCentenario;



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

        // PUT: api/Actualizaciones/EditarActualizacion
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpPut("[action]")]
        public async Task<IActionResult> DesactivarAviso([FromBody] ActualizacionesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var EdicionActualizacion = await _context.Actualizaciones.FirstOrDefaultAsync(a => a.IdActualizacion == model.IdActualizacion);

            if (EdicionActualizacion == null)
            {
                return NotFound();
            }

            EdicionActualizacion.MostrarAviso = model.MostrarAviso;

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

        //[HttpPost("Post/{nombre}" )]
        [HttpPost("[action]/{nombreCarpeta}/{nombreArchivo}")]
        public async Task<IActionResult> SubirPdfAct(IFormFile file, [FromRoute] string nombreCarpeta, string nombreArchivo)
        {
            try
            {
                //***********************************************************************************
                string patchp = Path.Combine(_environment.ContentRootPath, "Carpetas\\Actualizaciones\\" + nombreCarpeta);

                if (!Directory.Exists(patchp))
                    Directory.CreateDirectory(patchp);

                string extension;

                extension = Path.GetExtension(file.FileName);


                var filePath = Path.Combine(_environment.ContentRootPath, "Carpetas\\Actualizaciones\\" + nombreCarpeta, nombreArchivo + extension);

                var ligaServidor = ("https://" + HttpContext.Request.Host.Value);
                var rutaDocumento = ("/Carpetas/Actualizaciones/" + nombreCarpeta + "/" + nombreArchivo + extension);

                if (file.Length > 0)
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(stream);

                return Ok(new { count = 1, ruta = rutaDocumento, urlS = ligaServidor });


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

    }
}
