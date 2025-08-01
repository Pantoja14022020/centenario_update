using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Rac;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.GRAC;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public RacsController(DbContextSIIGPP context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Racs
        [HttpGet]
        public IEnumerable<Rac> GetRacs()
        {
            return _context.Racs;
        }
        // GET: api/Nucs/GenerarNuc
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
                //PRIMERO BUSCAR EL HECHO
                var consultaHecho = await _context.RHechoes.Where(a => a.IdRHecho == model.infoBorrado.rHechoId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaHecho == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningún Hecho con la información enviada" });
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
                            MovimientoId = new Guid("26E30B48-3774-403E-AD07-050D3F097EB6")


                        };

                        ctx.Add(laRegistro);

                        LogCat_RHecho rHecho = new LogCat_RHecho
                        {
                            LogAdmonId = gLog,
                            IdRHecho = consultaHecho.IdRHecho,
                            RAtencionId = consultaHecho.RAtencionId,
                            ModuloServicioId = consultaHecho.ModuloServicioId,
                            Agenciaid = consultaHecho.Agenciaid,
                            FechaReporte = consultaHecho.FechaReporte,
                            FechaHoraSuceso = consultaHecho.FechaHoraSuceso,
                            Status = consultaHecho.Status,
                            RBreve = consultaHecho.RBreve,
                            NarrativaHechos = consultaHecho.NarrativaHechos,
                            NucId = consultaHecho.NucId,
                            FechaElevaNuc = consultaHecho.FechaElevaNuc,
                            FechaElevaNuc2 = consultaHecho.FechaElevaNuc2,
                            Vanabim = consultaHecho.Vanabim,
                            NDenunciaOficio = consultaHecho.NDenunciaOficio,
                            Texto = consultaHecho.Texto,
                            Observaciones = consultaHecho.Observaciones,
                            FechaHoraSuceso2 = consultaHecho.FechaHoraSuceso2


                        };
                        ctx.Add(rHecho);
                        _context.Remove(consultaHecho);

                        var consultaAtencion = await _context.RAtencions.Where(a => a.IdRAtencion == model.infoBorrado.rAtencionId)
                                          .Take(1).FirstOrDefaultAsync();

                        if (consultaAtencion == null)
                        {
                            return Ok(new { res = "Error", men = "No se encontró ningún Atencion con la información enviada " });
                        }
                        else
                        {
                            LogCat_RAtencon laAtencion = new LogCat_RAtencon
                            {
                                LogAdmonId = gLog,
                                IdRAtencion = consultaAtencion.IdRAtencion,
                                FechaHoraAtencion = consultaAtencion.FechaHoraAtencion,
                                FechaHoraRegistro = consultaAtencion.FechaHoraRegistro,
                                FechaHoraCierre = consultaAtencion.FechaHoraCierre,
                                u_Nombre = consultaAtencion.u_Nombre,
                                u_Puesto = consultaAtencion.u_Puesto,
                                u_Modulo = consultaAtencion.u_Modulo,
                                DistritoInicial = consultaAtencion.DistritoInicial,
                                AgenciaInicial = consultaAtencion.AgenciaInicial,
                                DirSubProcuInicial = consultaAtencion.DirSubProcuInicial,
                                StatusAtencion = consultaAtencion.StatusAtencion,
                                StatusRegistro = consultaAtencion.StatusRegistro,
                                MedioDenuncia = consultaAtencion.MedioDenuncia,
                                ContencionVicitma = consultaAtencion.ContencionVicitma,
                                racId = consultaAtencion.racId,
                                ModuloServicio = consultaAtencion.ModuloServicio,
                                MedioLlegada = consultaAtencion.MedioLlegada
                            };
                            Guid gRacId = consultaAtencion.racId;
                            ctx.Add(laAtencion);
                            _context.Remove(consultaAtencion);
                            var consultaRac = await _context.Racs.Where(a => a.idRac == gRacId)
                            .Take(1).FirstOrDefaultAsync();

                            if (consultaRac == null)
                            {
                                return Ok(new { res = "Error", men = "No se encontró ningún RAC con la información enviada " });
                            }
                            else
                            {
                                LogRac lrRac = new LogRac
                                {
                                    idRac = consultaRac.idRac,
                                    LogAdmonId = gLog,
                                    Indicador = consultaRac.Indicador,
                                    DistritoId = consultaRac.DistritoId,
                                    CveDistrito = consultaRac.CveDistrito,
                                    DConsecutivo = consultaRac.DConsecutivo,
                                    AgenciaId = consultaRac.AgenciaId,
                                    CveAgencia = consultaRac.CveAgencia,
                                    AConsecutivo = consultaRac.AConsecutivo,
                                    Ano = consultaRac.Año,
                                    racg = consultaRac.racg,
                                    Asignado = consultaRac.Asignado,
                                    Ndenuncia = consultaRac.Ndenuncia
                                };
                                ctx.Add(lrRac);
                                _context.Remove(consultaRac);

                            }

                        }

                        await _context.SaveChangesAsync();
                        await ctx.SaveChangesAsync();

                    }

                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "RAC eliminado Correctamente" });
        }
        // POST: api/Racs/GenerarRac
        [HttpPost("[action]")]
        [Authorize(Roles= "Administrador,Recepción,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-AMP,Recepción")]
       
        public async Task<IActionResult> GenerarRac([FromBody] RacViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var result = new ObjectResult(new { statusCode = "403", mensaje = "MODELO NO VALIDO" });
                result.StatusCode = 495;
                return result;
            }
            try
            {
                  
                var ncd = 1;
                var nca = 1;
                DateTime fechaActual = DateTime.Today;
                var año = fechaActual.Year;
                var dcve = "";
                var acve = "";

                var selectClave = await _context.Agencias
                               .Where(a => a.IdAgencia == model.agenciaId)
                               .Include(a => a.DSP)
                               .Include(a => a.DSP.Distrito)
                               .FirstOrDefaultAsync();

               
                if (selectClave != null)
                {
                    dcve = selectClave.DSP.Distrito.Clave;
                    acve = selectClave.Clave;

                }
                else
                {
                    var result = new ObjectResult(new { statusCode = "403", mensaje = "CLAVE DE DISTRITO NO ENCONTRADA" });
                    result.StatusCode = 403;
                    return result;
                }
                
                var consultarac = await _context.Racs
                                   .Where(a => a.DistritoId == model.distritoId)
                                   .Where(a => a.Año == año)
                                   .OrderByDescending(a => a.AConsecutivo)
                                   .Take(1)
                                   .FirstOrDefaultAsync();

                if (consultarac!= null)
                {
                    ncd = consultarac.DConsecutivo + 1;
                    nca = consultarac.AConsecutivo + 1;
                }
                Rac rac = new Rac
                {
                    Indicador = "R",
                    DistritoId = model.distritoId,
                    CveDistrito = dcve,
                    DConsecutivo = ncd,
                    AgenciaId = model.agenciaId,
                    CveAgencia = acve,
                    AConsecutivo = nca,
                    Año = año,
                    //racg = "R" + "-" + dcve + "-" + ncd + "-" + acve + "-" + nca + "-" + año,
                    racg = "R-" + dcve + "-" + año + "-" + ncd.ToString("D5"),
                    Ndenuncia = model.Ndenuncia,
                    Asignado = model.Asignado
                };

                _context.Racs.Add(rac);
                await _context.SaveChangesAsync();
                return Ok(new RacReturnViewModel { rac = rac.racg, idrac = rac.idRac, nombreCarpeta = "C" + "-" + dcve + "-" + ncd + "-" + acve + "-" + nca + "-" + año });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

        }


        // GET: api/Racs/ListarporAgencia
        [Authorize(Roles = " Recepción,Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{agenciaid}")]
        public async Task<IEnumerable<ListarViewModel>> ListarporAgencia([FromRoute]Guid agenciaid)
        {
            var dil = await _context.Racs
                            .Where(x => x.Ndenuncia != null)
                            .Where(x => x.AgenciaId == agenciaid)
                            .ToListAsync();


            return dil.Select(a => new ListarViewModel

            {
                /*********************************************/
                Idrac = a.idRac,
                RAC = a.racg,
                Asignado = a.Asignado,
                Ndenuncia = a.Ndenuncia,
           
            });

        }

        // PUT: api/Racs/Actualizar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ListarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var actualizamosN = await _context.Racs.FirstOrDefaultAsync(a => a.idRac == model.Idrac);

            if (actualizamosN == null)
            {
                return NotFound();
            }

            actualizamosN.Asignado = true;
                

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();
        }


        // POST: api/Racs/GenerarRacModuloCaptura
        [HttpPost("[action]")]
        //[Authorize(Roles= "Administrador,Recepción,AMPO-AMP Mixto, AMPO-AMP Detenido")]

        public async Task<IActionResult> GenerarRacModuloCaptura([FromBody] RacViewModelMCaptura model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ncd = 1;
            var nca = 1;
            DateTime fechaActual = model.FechaCreacion;

            var año = fechaActual.Year;
            var dcve = "";
            var acve = "";

            var selectClave = await _context.Agencias
                           .Where(a => a.IdAgencia == model.agenciaId)
                           .Include(a => a.DSP)
                           .Include(a => a.DSP.Distrito)
                           .FirstOrDefaultAsync();

            if (selectClave != null)
            {
                dcve = selectClave.DSP.Distrito.Clave;
                acve = selectClave.Clave;

            }
            else
            {
                return BadRequest();
            }

            var consultarac = await _context.Racs
                               .Where(a => a.DistritoId == model.distritoId)
                               .Where(a => a.Año == año)
                               .OrderByDescending(a => a.AConsecutivo)
                               .Take(1)
                               .FirstOrDefaultAsync();

            if (consultarac != null)
            {
                ncd = consultarac.DConsecutivo + 1;
                nca = consultarac.AConsecutivo + 1;

            }

            Rac rac = new Rac
            {
                Indicador = "R",
                DistritoId = model.distritoId,
                CveDistrito = dcve,
                DConsecutivo = ncd,
                AgenciaId = model.agenciaId,
                CveAgencia = acve,
                AConsecutivo = nca,
                Año = año,
                //racg = "R" + "-" + dcve + "-" + ncd + "-" + acve + "-" + nca + "-" + año,
                racg = "R-" + dcve + "-" + año + "-" + ncd.ToString("D5"),
                Ndenuncia = model.Ndenuncia,
                Asignado = model.Asignado

            };


            _context.Racs.Add(rac);

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
            return Ok(new { rac = rac.racg, idrac = rac.idRac, nombreCarpeta = "C" + "-" + dcve + "-" + ncd + "-" + acve + "-" + nca + "-" + año });
        }

        // POST: api/Racs/ModNumRac
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> ModNumRac([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //PRIMERO BUSCAR EL HECHO
                var consultaRAC = await _context.Racs.Where(a => a.idRac == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaRAC == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningún RAC con la información enviada" });
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
                            MovimientoId = new Guid("5dd6daee-8027-43d5-8f5e-0c6a42509f65")
                        };

                        ctx.Add(laRegistro);

                        LogRac Rac = new LogRac
                        {
                            LogAdmonId = gLog,
                            idRac = consultaRAC.idRac,
                            Indicador = consultaRAC.Indicador,
                            DistritoId = consultaRAC.DistritoId,
                            CveDistrito = consultaRAC.CveDistrito,
                            DConsecutivo = consultaRAC.DConsecutivo,
                            AgenciaId = consultaRAC.AgenciaId,
                            CveAgencia = consultaRAC.CveAgencia,
                            AConsecutivo = consultaRAC.AConsecutivo,
                            Ano = consultaRAC.Año,
                            racg = consultaRAC.racg,
                            Asignado = consultaRAC.Asignado,
                            Ndenuncia = consultaRAC.Ndenuncia

                        };
                        ctx.Add(Rac);

                        consultaRAC.racg = model.infoBorrado.textoMod;
                        // AQUI AGREGAR EL MANEJO DE CONSECUTIVOS
                        consultaRAC.AConsecutivo = 0;
                        consultaRAC.DConsecutivo = 0;
                        //SE APLICAN TODOS LOS CAMBIOS A LA BD
                        await ctx.SaveChangesAsync();
                        await _context.SaveChangesAsync();


                    }
                }

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "Número de RAC modificado Correctamente" });
        }





        //PARA REMISIONES DE CARPETA INTRADISTRITALES

        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/Racs/Clonar
        public async Task<IActionResult> Clonar([FromBody] ClonarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var consultarac = await _context.Racs
                                   .Include(a => a.RAtencion)
                                   .Where(a => a.RAtencion.IdRAtencion == model.IdRAtencion)
                                   .Take(1)
                                   .FirstOrDefaultAsync();

                if (consultarac == null)
                {
                    return BadRequest(ModelState);

                }
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {


                    var rac = await ctx.Racs.FirstOrDefaultAsync(a => a.idRac == consultarac.idRac);

                    if (rac == null)
                    {
                        rac = new Rac();
                        ctx.Racs.Add(rac);
                    }

                    rac.idRac = consultarac.idRac;
                    rac.Indicador = consultarac.Indicador;
                    rac.DistritoId = consultarac.DistritoId;
                    rac.CveDistrito = consultarac.CveDistrito;
                    rac.DConsecutivo = consultarac.DConsecutivo;
                    rac.AgenciaId = consultarac.AgenciaId;
                    rac.CveAgencia = consultarac.CveAgencia;
                    rac.AConsecutivo = consultarac.AConsecutivo;
                    rac.Año = consultarac.Año;
                    rac.racg = consultarac.racg;
                    rac.Ndenuncia = consultarac.Ndenuncia;
                    rac.Asignado = consultarac.Asignado;

                    await ctx.SaveChangesAsync();

                    
                }
                return Ok();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

        }

    }
}