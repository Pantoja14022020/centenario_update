using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.IL.Models.Agenda;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_IL.Agendas;
using SIIGPP.IL.FilterClass;
using System.Text.Json;

namespace SIIGPP.IL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudAudienciaController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;
        public SolicitudAudienciaController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<SolicitudAudiencia> SolicitudAudiencia()
        {
            return _context.SolicitudAudiencias;
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ListarAudiencias()
        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-SOLAUD")).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var audiencias = await ctx.SolicitudAudiencias
                        .ToListAsync();

                    return Ok(audiencias.Select(a => new ListarAudienciasViewModel
                    {
                        oficio = a.NumOficio,
                        solicitud = a.AgendaId,
                        DistritoId=a.DistritoId,
                        NUC = a.NUC,
                        solicitante = a.solicitante,
                        partes =JsonSerializer.Deserialize<List<partesModel>>(a.Partes),
                        delitos = JsonSerializer.Deserialize<List<delitosModel>>(a.delitos),
                        fechaSolicitud = a.FechaSolicitud
                    }));
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ListarAudienciaPorFecha([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-SOLAUD")).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var audiencias = await ctx.SolicitudAudiencias
                        .Where(a=>(a.FechaSolicitud>=fechaInicio   && a.FechaSolicitud<= fechaFin))
                        .OrderBy(a=> a.FechaSolicitud)
                        .ToListAsync();

                    return Ok(audiencias.Select(a => new ListarAudienciasViewModel
                    {
                        oficio = a.NumOficio,
                        solicitud = a.AgendaId,
                        DistritoId = a.DistritoId,
                        NUC = a.NUC,
                        solicitante = a.solicitante,
                        partes = JsonSerializer.Deserialize<List<partesModel>>(a.Partes),
                        delitos = JsonSerializer.Deserialize<List<delitosModel>>(a.delitos),
                        fechaSolicitud = a.FechaSolicitud
                    }));
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ListarAudienciaPorNuc([FromQuery] String nuc)
        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-SOLAUD")).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var audiencias = await ctx.SolicitudAudiencias
                        .Where(a => (nuc == a.NUC))
                        .ToListAsync();

                    return Ok(audiencias.Select(a => new ListarAudienciasViewModel
                    {
                        oficio = a.NumOficio,
                        solicitud = a.AgendaId,
                        DistritoId = a.DistritoId,
                        NUC = a.NUC,
                        solicitante = a.solicitante,
                        partes = JsonSerializer.Deserialize<List<partesModel>>(a.Partes),
                        delitos = JsonSerializer.Deserialize<List<delitosModel>>(a.delitos),
                        fechaSolicitud = a.FechaSolicitud
                    }));
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ListarAudienciaPorSolicitud([FromQuery] Guid solicitud)
        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-SOLAUD")).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var audiencias = await ctx.SolicitudAudiencias
                        .Where(a => (solicitud == a.AgendaId ))
                        .ToListAsync();

                    return Ok(audiencias.Select(a => new ListarAudienciasViewModel
                    {
                        oficio = a.NumOficio,
                        solicitud = a.AgendaId,
                        DistritoId = a.DistritoId,
                        NUC = a.NUC,
                        solicitante = a.solicitante,
                        partes = JsonSerializer.Deserialize<List<partesModel>>(a.Partes),
                        delitos = JsonSerializer.Deserialize<List<delitosModel>>(a.delitos),
                        fechaSolicitud = a.FechaSolicitud
                    }));
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ListarAudienciaTest()
        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-SOLAUD")).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var audiencias = await ctx.SolicitudAudiencias
                        //.Where(a => a.Estatus == 1)
                        .ToListAsync();
                    //string Y = "[{\"nombre\":\"JUAN\",\"tipoParte\":\"victima\"},{\"nombre\":\"CHANA\",\"tipoParte\":\"imputado\"}]";
                    //List<partesModel> X = JsonSerializer.Deserialize<List<partesModel>>(Y);

                    //audiencias.ForEach(a => { a.Estatus = 2; });
                    //ctx.UpdateRange(audiencias);
                    //await ctx.SaveChangesAsync();

                    return Ok(audiencias.Select(a => new ListarAudienciasViewModel
                    {
                        oficio = a.NumOficio,
                        solicitud = a.AgendaId,
                        DistritoId = a.DistritoId,
                        NUC = a.NUC,
                        solicitante = a.solicitante,
                        partes = JsonSerializer.Deserialize<List<partesModel>>(a.Partes),
                        delitos = JsonSerializer.Deserialize<List<delitosModel>>(a.delitos),
                        fechaSolicitud = a.FechaSolicitud
                    }));
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }
        }
    }
}