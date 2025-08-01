using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.RArma;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Arma;


namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RArmaController:ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public RArmaController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/RArma/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<RArmaViewModel>> Listar([FromRoute]Guid RHechoId)
        {
            var arma = await _context.rArmas
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.FechaSys)
                .ToListAsync();

            return arma.Select(a => new RArmaViewModel
            {
                IdRarma = a.IdRarma,
                RHechoId = a.RHechoId,
                TipoAr = a.TipoAr,
                NombreAr = a.NombreAr,
                DescripcionAr = a.DescripcionAr,
                FechaSys = a.FechaSys,
                FechaRegistro = a.FechaRegistro,
                Distrito = a.Distrito,
                Subproc = a.Subproc,
                Agencia = a.Agencia,
                Usuario = a.Usuario,
                Puesto = a.Puesto,
                Modulo = a.Modulo,
                Matricula = a.Matricula,
                Marca = a.Marca,
                Modelo = a.Modelo,
                Calibre = a.Calibre,

            });

        }


        // PUT: api/RArma/Actualizar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var arma= await _context.rArmas.FirstOrDefaultAsync(a => a.IdRarma == model.IdRarma);

            if (arma == null)
            {
                return NotFound();
            }

            arma.NombreAr = model.NombreAr;
            arma.TipoAr = model.TipoAr;
            arma.DescripcionAr = model.DescripcionAr;
            arma.Matricula = model.Matricula;
            arma.Marca = model.Marca;
            arma.Modelo = model.Modelo;
            arma.Calibre = model.Calibre;

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


        // POST: api/RArma/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            RArma arma = new RArma
            {
                RHechoId = model.RHechoId,
                TipoAr = model.TipoAr,
                NombreAr = model.NombreAr,
                DescripcionAr = model.DescripcionAr,
                FechaSys = fecha,
                FechaRegistro = model.FechaRegistro,
                Distrito = model.Distrito,
                Subproc = model.Subproc,
                Agencia = model.Agencia,
                Usuario = model.Usuario,
                Puesto = model.Puesto,
                Modulo = model.Modulo,
                Matricula = model.Matricula,
                Marca = model.Marca,
                Modelo = model.Modelo,
                Calibre = model.Calibre,
            };

            _context.rArmas.Add(arma);
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


        // GET: api/RArma/ListarEstadisticas
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{iddistrito}/{iddsp}/{año}/{mes}/{indicadorano}/{indicadormes}/{armaclasificacion}/{armaobjetocat}/{calibre}/{marca}/{matricula}/{modelo}/{descripcion}/{distritoactivo}/{dspactivo}")]
        public async Task<IEnumerable<EstadisticasViewModel>> ListarEstadisticas([FromRoute]Guid iddistrito, Guid iddsp, DateTime año, DateTime mes, Boolean indicadorano, Boolean indicadormes, string armaclasificacion, string armaobjetocat, string calibre, string marca, string matricula, string modelo, string descripcion,Boolean distritoactivo,Boolean dspactivo)
        {
            var vehiculo = await _context.rArmas
                .Include(a => a.RHecho.NUCs)
                .Include(a => a.RHecho.ModuloServicio)
                .Include(a => a.RHecho.Agencia.DSP)
                .Where(a => distritoactivo ? a.RHecho.Agencia.DSP.DistritoId == iddistrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.Agencia.DSPId == iddsp : 1 == 1)
                .Where(a => !indicadorano ? a.FechaSys.Year == año.Year : 1 == 1)
                .Where(a => !indicadormes ? a.FechaSys.Month == mes.Month : 1 == 1)
                .Where(a => armaclasificacion != "ZKR" ? a.TipoAr == armaclasificacion : 1 == 1)
                .Where(a => armaobjetocat != "ZKR" ? a.NombreAr == armaobjetocat : 1 == 1)
                .Where(a => descripcion != "ZKR"? a.DescripcionAr.Contains(descripcion): 1 == 1)
                .Where(a => armaclasificacion == "Arma de Fuego" && calibre != "ZKR" ? a.Calibre == calibre : 1 == 1)
                .Where(a => armaclasificacion == "Arma de Fuego" && marca != "ZKR" ? a.Marca == marca : 1 == 1)
                .Where(a => armaclasificacion == "Arma de Fuego" && matricula != "ZKR" ? a.Matricula == matricula : 1 == 1)
                .Where(a => armaclasificacion == "Arma de Fuego" && modelo != "ZKR" ? a.Modelo == modelo : 1 == 1)
                .OrderByDescending(a => a.FechaSys)
                .ToListAsync();

            return vehiculo.Select(a => new EstadisticasViewModel
            {
                NUC = a.RHecho.NUCs.nucg,
                Agencia = a.RHecho.Agencia.Nombre,
                Modulo = a.RHecho.ModuloServicio.Nombre,
                DSP = a.RHecho.Agencia.DSP.NombreSubDir
            });

        }

        // GET: api/RArma/ListarEstadisticasModulo
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}/{armaclasificacion}/{armaobjetocat}/{calibre}/{marca}/{matricula}/{modelo}/{descripcion}")]
        public async Task<IEnumerable<EstadisticasViewModel>> ListarEstadisticasModulo([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf, string armaclasificacion, string armaobjetocat, string calibre, string marca, string matricula, string modelo, string descripcion)
        {
            var vehiculo = await _context.rArmas
                .Include(a => a.RHecho.NUCs)
                .Include(a => a.RHecho.ModuloServicio)
                .Include(a => a.RHecho.Agencia.DSP)
                .Where(a => a.RHecho.FechaHoraSuceso2 >= fechai)
                .Where(a => a.RHecho.FechaHoraSuceso2 <= fechaf)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => armaclasificacion != "ZKR" ? a.TipoAr == armaclasificacion : 1 == 1)
                .Where(a => armaobjetocat != "ZKR" ? a.NombreAr == armaobjetocat : 1 == 1)
                .Where(a => descripcion != "ZKR" ? a.DescripcionAr.Contains(descripcion) : 1 == 1)
                .Where(a => armaclasificacion == "Arma de Fuego" && calibre != "ZKR" ? a.Calibre == calibre : 1 == 1)
                .Where(a => armaclasificacion == "Arma de Fuego" && marca != "ZKR" ? a.Marca == marca : 1 == 1)
                .Where(a => armaclasificacion == "Arma de Fuego" && matricula != "ZKR" ? a.Matricula == matricula : 1 == 1)
                .Where(a => armaclasificacion == "Arma de Fuego" && modelo != "ZKR" ? a.Modelo == modelo : 1 == 1)
                .OrderByDescending(a => a.FechaSys)
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
