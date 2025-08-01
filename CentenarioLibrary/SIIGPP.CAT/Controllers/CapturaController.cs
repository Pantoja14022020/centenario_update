using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using SIIGPP.CAT.Models.Captura;
using SIIGPP.Entidades.M_Cat.Captura;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapturaController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public CapturaController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/Captura/Crear
        [Authorize(Roles = "Administrador,Director,AMPO-AMP,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto,Notificador,Procurador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearCapturaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Captura hi = new Captura
                {
                    RHechoId = model.RHechoId,
                    RegistroTableroId = model.RegistroTableroId,
                    UsuarioId = model.UsuarioId,
                    UModuloServicioId = model.UModuloServicioId,
                    RemitioModuloServicioId = model.RemitioModuloServicioId,
                    FechaRegistro = System.DateTime.Now,
                };

                _context.Capturas.Add(hi);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
            return Ok();
        }

        // GET: api/Captura/ListarPorModulo
        [Authorize(Roles = "Administrador,Director,AMPO-AMP ,AMPO-AMP Mixto, AMPO-AMP Detenido,Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto,Notificador,Procurador")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ListarPorModulo([FromRoute] Guid id)
        {
            try
            {
                var capturas = await _context.Capturas
                                    .AsNoTracking()
                                    .Where(a => a.UModuloServicioId == id)
                                    .OrderBy(a => a.RHecho.NUCs.nucg)
                                    .Select(a => new CapturaViewModel
                                    {
                                        IdCaptura = a.IdCaptura,
                                        RHechoId = a.RHechoId,
                                        RegistroTableroId = a.RegistroTableroId,
                                        UsuarioId = a.UsuarioId,
                                        UModuloServicioId = a.UModuloServicioId,
                                        RemitioModuloServicioId = a.RemitioModuloServicioId,
                                        Nuc = a.RHecho.NUCs.nucg,
                                        UsuarioNombre = a.Usuario.nombre,
                                        LugarRemitio = $"{a.RemitioModuloServicio.Nombre} - {a.RemitioModuloServicio.Agencia.Nombre} - {a.RemitioModuloServicio.Agencia.DSP.NombreSubDir} - {a.RemitioModuloServicio.Agencia.DSP.Distrito.Nombre}",
                                        LugarInicio = $"{a.RHecho.RAtencion.AgenciaInicial} - {a.RHecho.RAtencion.DirSubProcuInicial} - {a.RHecho.RAtencion.DistritoInicial}",
                                        CreoDistrito = a.UModuloServicio.Agencia.DSP.Distrito.Nombre,
                                        CreoDSP = a.UModuloServicio.Agencia.DSP.NombreSubDir,
                                        CreoAgencia = a.UModuloServicio.Agencia.Nombre,
                                        CreoModulo = a.UModuloServicio.Nombre,
                                        U_Nombre = a.RHecho.RAtencion.u_Nombre,
                                        FechaElevaNuc = a.RHecho.FechaElevaNuc,
                                        FechaRegistro = a.FechaRegistro,
                                        Victima = _context.RAPs
                                                                .Where(rap => rap.RAtencionId == a.RHecho.RAtencionId && rap.PInicio)
                                                                .Select(rap => rap.Persona.Nombre + " " + rap.Persona.ApellidoPaterno + (rap.Persona.ApellidoMaterno != "LO DESCONOCE" ? " " + rap.Persona.ApellidoMaterno : ""))
                                                                .FirstOrDefault() ?? "Sin registrar V/I"
                                    })
                                    .ToListAsync();

                return Ok(capturas);
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
        }
    }
}
