using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Datos_Relacionados;
using SIIGPP.CAT.Models.Datos_Relacionados;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosRelacionadosController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public DatosRelacionadosController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/DatosRelacionados/ListarporIdrhecho
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IEnumerable<DatosRelacionadosViewModel>> ListarporIdrhecho([FromRoute] Guid rHechoId)
        {
            var Datos = await _context.DatosRelacionados
                .Where (a => a.RHechoId == rHechoId)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return Datos.Select(a => new DatosRelacionadosViewModel
            {
                IdDatosRelacionados = a.IdDatosRelacionados,
                RHechoId = a.RHechoId,
                Tipo = a.Tipo,
                Descripcion = a.Descripcion,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
            });

        }

        // POST: api/DatosRelacionados/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DatosRelacionados Datos = new DatosRelacionados
            {
                RHechoId = model.RHechoId,
                Tipo = model.Tipo,
                Descripcion = model.Descripcion,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
            };

            _context.DatosRelacionados.Add(Datos);
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

        // PUT: api/DatosRelacionados/Actualizar
        [Authorize(Roles = "Administrador,AMPO-IL")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var dato = await _context.DatosRelacionados.FirstOrDefaultAsync(a => a.IdDatosRelacionados == model.IdDatosRelacionados);

            if (dato == null)
            {
                return NotFound();
            }

            dato.Tipo = model.Tipo;
            dato.Descripcion = model.Descripcion;


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

        // GET: api/DatosRelacionados/ListarEstadisticas
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{iddistrito}/{iddsp}/{año}/{mes}/{indicadorano}/{indicadormes}/{tipodato}/{descripcion}/{distritoactivo}/{dspactivo}")]
        public async Task<IEnumerable<EstadisticasViewModel>> ListarEstadisticas([FromRoute]Guid iddistrito, Guid iddsp, DateTime año, DateTime mes, Boolean indicadorano, Boolean indicadormes, string tipodato, string descripcion, Boolean distritoactivo, Boolean dspactivo)
        {
            var vehiculo = await _context.DatosRelacionados
                .Include(a => a.RHecho.NUCs)
                .Include(a => a.RHecho.ModuloServicio)
                .Include(a => a.RHecho.Agencia.DSP)
                .Where(a => distritoactivo ? a.RHecho.Agencia.DSP.DistritoId == iddistrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.Agencia.DSPId == iddsp : 1 == 1)
                .Where(a => !indicadorano ? a.Fechasys.Year == año.Year : 1 == 1)
                .Where(a => !indicadormes ? a.Fechasys.Month == mes.Month : 1 == 1)
                .Where(a => tipodato != "ZKR" ? a.Tipo == tipodato : 1 == 1)
                .Where(a => descripcion != "ZKR" ? a.Descripcion.Contains(descripcion) : 1 == 1)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return vehiculo.Select(a => new EstadisticasViewModel
            {
                NUC = a.RHecho.NUCs.nucg,
                Agencia = a.RHecho.Agencia.Nombre,
                Modulo = a.RHecho.ModuloServicio.Nombre,
                DSP = a.RHecho.Agencia.DSP.NombreSubDir
            });

        }

        // GET: api/DatosRelacionados/ListarEstadisticasModulo
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}/{tipodato}/{descripcion}")]
        public async Task<IEnumerable<EstadisticasViewModel>> ListarEstadisticasModulo([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf, string tipodato, string descripcion)
        {
            var vehiculo = await _context.DatosRelacionados
                .Include(a => a.RHecho.NUCs)
                .Include(a => a.RHecho.ModuloServicio)
                .Include(a => a.RHecho.Agencia.DSP)
                .Where(a => a.RHecho.FechaHoraSuceso2 >= fechai)
                .Where(a => a.RHecho.FechaHoraSuceso2 <= fechaf)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => tipodato != "ZKR" ? a.Tipo == tipodato : 1 == 1)
                .Where(a => descripcion != "ZKR" ? a.Descripcion.Contains(descripcion) : 1 == 1)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return vehiculo.Select(a => new EstadisticasViewModel
            {
                NUC = a.RHecho.NUCs.nucg,
                Agencia = a.RHecho.Agencia.Nombre,
                Modulo = a.RHecho.ModuloServicio.Nombre,
                DSP = a.RHecho.Agencia.DSP.NombreSubDir
            });

        }

    }
}
