using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.ArchivoVehiculos;
using Microsoft.AspNetCore.Hosting;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.ArchivosVehiculos;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoVehiculosController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;
#pragma warning disable CS0649 // El campo 'ArchivoVehiculosController._environment' nunca se asigna y siempre tendrá el valor predeterminado null
        private readonly IWebHostEnvironment _environment;
#pragma warning restore CS0649 // El campo 'ArchivoVehiculosController._environment' nunca se asigna y siempre tendrá el valor predeterminado null

        public ArchivoVehiculosController(DbContextSIIGPP context, IWebHostEnvironment enviroment)
        {
            _context = context;
            _environment = enviroment;
        }

        // GET: api/ArchivoVehiculos/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{VehiculoId}")]
        public async Task<IEnumerable<ArchivoVehiculo>> Listar([FromRoute] Guid VehiculoId)
        {
            var vehiculo = await _context.ArchivoVehiculos
                .Where(a => a.VehiculoId == VehiculoId)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return vehiculo.Select(a => new ArchivoVehiculo
            {
                IdArchivoVehiculos = a.IdArchivoVehiculos,
                VehiculoId = a.VehiculoId,
                TipoDocumento = a.TipoDocumento,
                DescripcionDocumento  = a.DescripcionDocumento,
                Ruta = a.Ruta,
                UDistrito = a.UDistrito,
                USubproc =a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto =a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
            });
        }

        // POST: api/ArchivoVehiculos/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ArchivoVehiculo vehiculo = new ArchivoVehiculo
            {
                VehiculoId = model.VehiculoId,
                TipoDocumento = model.TipoDocumento,
                DescripcionDocumento = model.DescripcionDocumento,
                Ruta = model.Ruta,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
            };

            _context.ArchivoVehiculos.Add(vehiculo);
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
                return BadRequest();
            }
        }
        // PUT: api/ArchivoVehiculos/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ao = await _context.ArchivoVehiculos.FirstOrDefaultAsync(a => a.IdArchivoVehiculos == model.IdArchivoVehiculos);

            if (ao == null)
            {
                return NotFound();
            }

            ao.TipoDocumento = model.TipoDocumento;
            ao.DescripcionDocumento = model.DescripcionDocumento;
            ao.Ruta = model.Ruta;
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
    }
}