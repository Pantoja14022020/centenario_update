using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.CAT.Models.PRegistro;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.PRegistro;
using SIIGPP.Entidades.M_Cat.Orientacion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreRegistrosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public PreRegistrosController(DbContextSIIGPP context, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;
        }

        // GET: api/PreRegistros
        [HttpGet]
        public IEnumerable<PreRegistro> PreRegistros()
        {
            return _context.PreRegistros;
        }

        // POST: api/PreRegistros/GenerarPreRegistro
        [HttpPost("[action]")]
        public async Task<IActionResult> GenerarPreRegistro([FromBody] PreRegistroInsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Información Incompleta" });
            }

            DateTime fechaActual = DateTime.Today;
            var año = fechaActual.Year;
            var dcve = "";
            
            var selectClave = await _context.Distritos
                           .Where(a => a.IdDistrito == model.distritoId)
                           .FirstOrDefaultAsync();

            if (selectClave != null)
            {
                dcve = selectClave.Clave;
            }
            else
            {
                return Ok(new { error = "No se encontró la clave del distrito" });
            }

            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoId.ToString().ToUpper())).Options;
            using (var ctx = new DbContextSIIGPP(options))
            {


                PreRegistro pregistro = new PreRegistro
                {
                    Indicador = "R",
                    DistritoId = model.distritoId,
                    CveDistrito = dcve,
                    Ano = año,
                    Ndenuncia = model.Ndenuncia,
                    Asignado = model.Asignado,
                    RBreve= model.RBreve,
                    fechaSuceso= model.fechaSuceso
                    

                };


                ctx.PreRegistros.Add(pregistro);

                try
                {
                    await ctx.SaveChangesAsync();
                    return Ok(new { prereg = pregistro.IdPRegistro });
                }
                catch (Exception ex)
                {
                    return Ok(new { res = ex.Message });
                }
            }
        }
        
        // POST: api/PreRegistros/GenerarPreRegistro
        [HttpPost("[action]")]
        public async Task<IActionResult> GenerarRac([FromBody] RacFromPRegistroViewModel model)
        {
            //BUSCAR LOS DATOS DE PREREGISTRO
            try
            {
                var consultarac = await _context.PreRegistros
                                   .Where(a => a.IdPRegistro == model.IdPreRegistro)
                                   .Take(1)
                                   .FirstOrDefaultAsync();

                if (consultarac == null)
                {
                    return BadRequest(ModelState);

                }
                else
                {
                    SIIGPP.CAT.Models.Rac.RacViewModel datos = new SIIGPP.CAT.Models.Rac.RacViewModel
                    {
                        distritoId=model.IdDistrito, 
                        agenciaId=model.IdAgencia,
                        Ndenuncia = null,
                        Asignado=false
                    };

                    RacsController elRAC = new RacsController(_context, _configuration);
                    IActionResult elRACInsertado = await elRAC.GenerarRac(datos);

                    var resultadoelRac = elRACInsertado as ObjectResult;

                    if (resultadoelRac.StatusCode ==200)
                    {
                        var racReturn = resultadoelRac.Value as SIIGPP.CAT.Models.Rac.RacReturnViewModel;

                        var idRAC = racReturn.idrac;
                        //
                        var consultaPAtencion = await _context.PreAtenciones
                                   .Where(a => a.PRegistroId == model.IdPreRegistro)
                                   .Take(1)
                                   .FirstOrDefaultAsync();

                        if (consultaPAtencion == null)
                        {
                            var result = new ObjectResult(new { statusCode = "402", mensaje = "No se encontró el Pre Registro de Atención" });
                            result.StatusCode = 402;
                            return result;

                        }
                        else
                        {
                            DateTime fecha = System.DateTime.Now;
                            RAtencion InsertarRA = new RAtencion
                            {
                                DistritoInicial = model.distritoinicial,
                                AgenciaInicial = model.agencia,
                                DirSubProcuInicial = model.dirSubProcuInicial,
                                FechaHoraRegistro = fecha,
                                StatusAtencion = false,
                                StatusRegistro = false,
                                racId = idRAC,
                                MedioDenuncia = "Denuncia",
                                ContencionVicitma = false,
                                ModuloServicio = "0",
                                u_Nombre = model.UNombre,
                                u_Puesto = model.UPuesto,
                                u_Modulo = model.modulo,
                                MedioLlegada = null
                            };

                            _context.RAtencions.Add(InsertarRA);

                            var idRA = InsertarRA.IdRAtencion;
                            // BUSCAR EL PRE RAP PARA LA INFORMACION
                            var consultaRAP = await _context.PreRaps
                                   .Where(a => a.PAtencionId == consultaPAtencion.IdPAtencion)
                                   .Take(1)
                                   .FirstOrDefaultAsync();

                            RAP InsertarRAP = new RAP
                            {
                                RAtencionId = idRA,
                                PersonaId = model.idPersona,
                                ClasificacionPersona = consultaRAP.ClasificacionPersona,
                                PInicio = consultaRAP.PInicio,
                            };

                            _context.RAPs.Add(InsertarRAP);

                            consultarac.Atendido = true;
                            consultarac.RacId = idRAC;
                            await _context.SaveChangesAsync();
                            return Ok(new { idRac=idRAC,idAtencion= InsertarRA.IdRAtencion,otra= idRA });
                        }
                    }
                    else
                    {
                        //NO SE GENERÓ EL RAC
                        var result = new ObjectResult(new { statusCode = "402", mensaje = "RAC no generado correctamente" });
                        result.StatusCode = 402;
                        return result;
                    }

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
