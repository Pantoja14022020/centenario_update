using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Nuc;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.GNUC;
using SIIGPP.Entidades.M_Administracion;
using System.Collections;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NucsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public NucsController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Nucs
        [HttpGet]
        public IEnumerable<Nuc> GetNucs()
        {
            return _context.Nucs;
        }

        // GET: api/Nucs/GenerarNuc
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Cancel([FromBody] EliminarNuc model)
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
                            MovimientoId = new Guid("6336D888-5289-4B57-AD20-8A25DEC9F705")
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

                        consultaHecho.NucId = null;

                        var consultaNuc = await _context.Nucs.Where(a => a.idNuc == model.infoBorrado.registroId)
                                         .Take(1).FirstOrDefaultAsync();

                        if (consultaNuc == null)
                        {
                            return Ok(new { res = "Error", men = "No se encontró ningún NUC con la información enviada " });
                        }
                        else
                        {
                            LogNuc lNuc = new LogNuc
                            {
                                idNuc = consultaNuc.idNuc,
                                LogAdminId = gLog,
                                Indicador = consultaNuc.Indicador,
                                DistritoId = consultaNuc.DistritoId,
                                CveDistrito = consultaNuc.CveDistrito,
                                DConsecutivo = consultaNuc.DConsecutivo,
                                AgenciaId = consultaNuc.AgenciaId,
                                CveAgencia = consultaNuc.CveAgencia,
                                AConsecutivo = consultaNuc.AConsecutivo,
                                Año = consultaNuc.Año,
                                nucg = consultaNuc.nucg,
                                StatusNUC = consultaNuc.StatusNUC,
                                Etapanuc = consultaNuc.Etapanuc

                            };
                            ctx.Add(lNuc);
                            _context.Remove(consultaNuc);
                        }
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
            return Ok(new { res = "success", men = "NUC eliminado Correctamente" });
        }

        // POST: api/Nucs/MostrarNUCG
        [Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador, Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> MostrarNUCG([FromBody] NucViewModel model)
        //public async Task<IActionResult> Eliminar([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ncd = 1;
            var nca = 1;
            DateTime fechaActual = DateTime.Today;
            var año = fechaActual.Year;
            var dcve = "";
            var acve = "";
            var nucg = "";

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

            var consultanuc = await _context.Nucs
                               .Where(a => a.DistritoId == model.distritoId)
                               .Where(a => a.Año == año)
                               .OrderByDescending(a => a.DConsecutivo)
                               .Take(1)
                               .FirstOrDefaultAsync();

            if (consultanuc != null)
            {
                ncd = consultanuc.DConsecutivo + 1;
                nca = consultanuc.AConsecutivo + 1;

            }
            nucg = dcve + "-" + año + "-" + ncd.ToString("D5");



            try
            {
                return Ok(nucg);

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }



        }




        // POST: api/Nucs/ModNumNuc
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> ModNumNuc([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //PRIMERO BUSCAR EL HECHO
                var consultaNUC = await _context.Nucs.Where(a => a.idNuc == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaNUC == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningúna carpeta con la información enviada" });
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
                            MovimientoId = new Guid("dccf03aa-d16c-411e-a564-2b2f9294b2e8")
                        };

                        ctx.Add(laRegistro);

                        LogNuc Nuc = new LogNuc
                        {
                            LogAdminId = gLog,
                            idNuc = consultaNUC.idNuc,
                            Indicador = consultaNUC.Indicador,
                            DistritoId = consultaNUC.DistritoId,
                            CveDistrito = consultaNUC.CveDistrito,
                            DConsecutivo = consultaNUC.DConsecutivo,
                            AgenciaId = consultaNUC.AgenciaId,
                            CveAgencia = consultaNUC.CveAgencia,
                            AConsecutivo = consultaNUC.AConsecutivo,
                            Año = consultaNUC.Año,
                            nucg = consultaNUC.nucg,
                            StatusNUC = consultaNUC.StatusNUC,
                            Etapanuc = consultaNUC.Etapanuc
                        };
                        ctx.Add(Nuc);

                        consultaNUC.nucg = model.infoBorrado.textoMod;
                        // AQUI AGREGAR EL MANEJO DE CONSECUTIVOS
                        consultaNUC.AConsecutivo = 0;
                        consultaNUC.DConsecutivo = 0;
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
            return Ok(new { res = "success", men = "Número de NUC modificado Correctamente" });
        }




        // GET: api/Nucs/GenerarNuc
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Recepción")]
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
                            MovimientoId = new Guid("4BB82D5A-B86B-4155-98B4-CD4526AC7AFD")


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

                        var consultaNuc = await _context.Nucs.Where(a => a.idNuc == model.infoBorrado.registroId)
                                         .Take(1).FirstOrDefaultAsync();

                        if (consultaNuc == null)
                        {
                            return Ok(new { res = "Error", men = "No se encontró ningún NUC con la información enviada " });
                        }
                        else
                        {
                            LogNuc lNuc = new LogNuc
                            {
                                idNuc = consultaNuc.idNuc,
                                LogAdminId = gLog,
                                Indicador = consultaNuc.Indicador,
                                DistritoId = consultaNuc.DistritoId,
                                CveDistrito = consultaNuc.CveDistrito,
                                DConsecutivo = consultaNuc.DConsecutivo,
                                AgenciaId = consultaNuc.AgenciaId,
                                CveAgencia = consultaNuc.CveAgencia,
                                AConsecutivo = consultaNuc.AConsecutivo,
                                Año = consultaNuc.Año,
                                nucg = consultaNuc.nucg,
                                StatusNUC = consultaNuc.StatusNUC,
                                Etapanuc = consultaNuc.Etapanuc

                            };
                            ctx.Add(lNuc);
                            _context.Remove(consultaNuc);

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
                        }
                        // SE APLICAN TODOS LOS CAMBIOS A LA BD
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
            return Ok(new { res = "success", men = "NUC eliminado Correctamente" });
        }
        // GET: api/Nucs/GenerarNuc
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GenerarNuc([FromBody] NucViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
                return BadRequest();
            }

            var consultanuc = await _context.Nucs
                               .Where(a => a.DistritoId == model.distritoId)
                               .Where(a => a.Año == año)
                               .OrderByDescending(a => a.DConsecutivo)
                               .Take(1)
                               .FirstOrDefaultAsync();

            if (consultanuc != null)
            {
                ncd = consultanuc.DConsecutivo + 1;
                nca = consultanuc.AConsecutivo + 1;

            }

            Nuc nuc = new Nuc
            {
                Indicador = "N",
                DistritoId = model.distritoId,
                CveDistrito = dcve,
                DConsecutivo = ncd,
                AgenciaId = model.agenciaId,
                CveAgencia = acve,
                AConsecutivo = nca,
                Año = año,
                StatusNUC = "Inicio de la investigación",
                Etapanuc = "Inicial",
                //nucg = "N" + "-" + dcve + "-" + ncd + "-" + acve + "-" + nca + "-" + año,
                nucg = dcve + "-" + año + "-" + ncd.ToString("D5"),

            };

            _context.Nucs.Add(nuc);

            try
            {
                await _context.SaveChangesAsync();

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
            return Ok(new { nuc = nuc.nucg, idnuc = nuc.idNuc });


        }

        // PUT:  api/Nucs/Actualizar
        //[Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var db = await _context.Nucs.FirstOrDefaultAsync(a => a.idNuc == model.IdNuc);

            if (db == null)
            {
                return NotFound();
            }

            db.StatusNUC = model.StatusNUC;
            db.Etapanuc = model.Etapanuc;

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


        // PUT:  api/Nucs/ActualizarNucAcum
        //[Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarNucAcum([FromBody] ActualizarNucAcumViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var db = await _context.Nucs.FirstOrDefaultAsync(a => a.idNuc == model.idNuc);

            if (db == null)
            {
                return NotFound();
            }

            db.nucg = model.NuevoNucAcum;
            //db.nucg = db.nucg + "/Acum12-2021-00300";

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


        // GET: api/Nucs/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Recepción")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListarNucViewModel>> Listar()
        {
            var nuc = await _context.Nucs
                .ToListAsync();

            return nuc.Select(a => new ListarNucViewModel
            {
                idNuc = a.idNuc,
                Indicador = a.Indicador,
                DistritoId = a.DistritoId,
                DConsecutivo = a.DConsecutivo,
                AgenciaId = a.AgenciaId,
                AConsecutivo = a.AConsecutivo,
                Año = a.Año,
                CveAgencia = a.CveAgencia,
                CveDistrito = a.CveDistrito,
                nucg = a.nucg,
                StatusNUC = a.StatusNUC,
                Etapanuc = a.Etapanuc

            });

        }

        //Si reutilizas esta api y quieres mas datos en viewmodel colocalos, donde lo uso no afecta. 
        // GET: api/Nucs/BuscarExistenciaNuc
        [HttpGet("[action]/{NUC}")]
        public async Task<IActionResult> BuscarExistenciaNuc([FromRoute] string NUC)
        {
            var nuc = await _context.Nucs
                            .Where(x => x.nucg == NUC).FirstOrDefaultAsync();

            if (nuc == null)
            {
                return NotFound("No hay registros");

            }

            return Ok(new ListarNucViewModel
            {
                idNuc = nuc.idNuc,
                Indicador = nuc.Indicador,
                DistritoId = nuc.DistritoId,
                DConsecutivo = nuc.DConsecutivo,
                AgenciaId = nuc.AgenciaId,
                AConsecutivo = nuc.AConsecutivo,
                Año = nuc.Año,
                CveAgencia = nuc.CveAgencia,
                CveDistrito = nuc.CveDistrito,
                nucg = nuc.nucg,
                StatusNUC = nuc.StatusNUC,
                Etapanuc = nuc.Etapanuc
            });

        }

        // GET: api/Nucs/BuscarNuc
        [HttpGet("[action]/{NUC}/{IdDistrito}")]
        public async Task<IActionResult> BuscarNuc([FromRoute] string NUC, Guid IdDistrito)
        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var nuc = await ctx.RHechoes.
                                    Include(x => x.NUCs).
                                    Include(x => x.RAtencion).
                                    Include(x => x.RAtencion.RACs).
                                    Include(x => x.ModuloServicio.Agencia.DSP.Distrito).
                                    Where(x => x.NUCs.nucg.Contains(NUC)).
                                    ToListAsync();


                    return Ok(nuc.Select(a => new ListarInfoNucViewModel
                    {
                        idNuc = a.NUCs.idNuc,
                        DistritoId = a.NUCs.DistritoId,
                        DConsecutivo = a.NUCs.DConsecutivo,
                        CveDistrito = a.NUCs.CveDistrito,
                        nucg = a.NUCs.nucg,
                        racg = a.RAtencion.RACs.racg,
                        DistritoActual = a.ModuloServicio.Agencia.DSP.Distrito.Nombre,
                        SubdireccionActual = (a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == IdDistrito ? a.ModuloServicio.Agencia.DSP.NombreSubDir : ""),
                        AgenciaActual = (a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == IdDistrito ? a.ModuloServicio.Agencia.Nombre : ""),
                        ModuloActual = (a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == IdDistrito ? a.ModuloServicio.Nombre : ""),
                    }));
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

        }

        // GET: api/Nucs/BuscarNucGnrl
        [HttpGet("[action]/{NUC}/{IdDistrito}")]
        public async Task<IActionResult> BuscarNucGnrl([FromRoute] string NUC, Guid IdDistrito)
        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var nuc = await ctx.RHechoes.
                                    Include(x => x.NUCs).
                                    Include(x => x.RAtencion).
                                    Include(x => x.RAtencion.RACs).
                                    Include(x => x.ModuloServicio.Agencia.DSP.Distrito).
                                    Where(x => x.NUCs.nucg.Contains(NUC)).
                                    ToListAsync();


                    var nucResult = nuc.Select(a => new ListarInfoNucViewModel
                    {
                        idNuc = a.NUCs.idNuc,
                        DistritoId = a.NUCs.DistritoId,
                        DConsecutivo = a.NUCs.DConsecutivo,
                        CveDistrito = a.NUCs.CveDistrito,
                        nucg = a.NUCs.nucg,
                        racg = a.RAtencion.RACs.racg,
                        DistritoActual = a.ModuloServicio.Agencia.DSP.Distrito.Nombre,
                        SubdireccionActual = (a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == IdDistrito ? a.ModuloServicio.Agencia.DSP.NombreSubDir : ""),
                        AgenciaActual = (a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == IdDistrito ? a.ModuloServicio.Agencia.Nombre : ""),
                        ModuloActual = (a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == IdDistrito ? a.ModuloServicio.Nombre : ""),
                        DistritoIdActual = a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito,
                    }).ToList();

                    var newIdDistrito = nucResult.FirstOrDefault()?.DistritoIdActual;

                    if(newIdDistrito.Value != IdDistrito)
                    {
                        return await BuscarNucGnrl(NUC, newIdDistrito.Value);
                    }

                    return Ok(nucResult);
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

        }


        // GET: api/Nucs/GenerarNucMCaptura
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GenerarNucMCaptura([FromBody] NucMCapturaViewModel model)
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

            var consultanuc = await _context.Nucs
                               .Where(a => a.DistritoId == model.distritoId)
                               .Where(a => a.Año == año)
                               .OrderByDescending(a => a.DConsecutivo)
                               .Take(1)
                               .FirstOrDefaultAsync();

            if (consultanuc != null)
            {
                ncd = consultanuc.DConsecutivo + 1;
                nca = consultanuc.AConsecutivo + 1;

            }

            Nuc nuc = new Nuc
            {
                Indicador = "N",
                DistritoId = model.distritoId,
                CveDistrito = dcve,
                DConsecutivo = 0,
                AgenciaId = model.agenciaId,
                CveAgencia = acve,
                AConsecutivo = 0,
                Año = año,
                StatusNUC = "Inicio de la investigación",
                Etapanuc = "Inicial",
                //nucg = "N" + "-" + dcve + "-" + ncd + "-" + acve + "-" + nca + "-" + año,
                nucg = model.NUC,

            };

            _context.Nucs.Add(nuc);

            try
            {
                await _context.SaveChangesAsync();

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
            return Ok(new { nuc = nuc.nucg, idnuc = nuc.idNuc });


        }
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Recepción, AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/Nucs/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var consultaNuc = await _context.Nucs
                                   .Where(x => x.nucg == model.IdNuc)
                                   .Take(1)
                                   .FirstOrDefaultAsync();
                if (consultaNuc == null)
                {
                    return BadRequest(ModelState);

                }
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {

                    var nuc = await ctx.Nucs.FirstOrDefaultAsync(a => a.idNuc == consultaNuc.idNuc);

                    if (nuc == null)
                    {
                        nuc = new Nuc();
                        ctx.Nucs.Add(nuc);
                    }

                    nuc.idNuc = consultaNuc.idNuc;
                    nuc.Indicador = consultaNuc.Indicador;
                    nuc.DistritoId = consultaNuc.DistritoId;
                    nuc.CveDistrito = consultaNuc.CveDistrito;
                    nuc.DConsecutivo = consultaNuc.DConsecutivo;
                    nuc.AConsecutivo = consultaNuc.AConsecutivo;
                    nuc.AgenciaId = consultaNuc.AgenciaId;
                    nuc.CveAgencia = consultaNuc.CveAgencia;
                    nuc.Año = consultaNuc.Año;
                    nuc.StatusNUC = consultaNuc.StatusNUC;
                    nuc.Etapanuc = consultaNuc.Etapanuc;
                    nuc.nucg = consultaNuc.nucg;

                    await ctx.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (Exception ex)

            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }
        // GET: api/Nucs/BuscarNucCaptura
        [HttpGet("[action]/{NUC}/{IdDistrito}")]
        public async Task<IActionResult> BuscarNucCaptura([FromRoute] string NUC, Guid IdDistrito)
        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var nucResult = await ctx.RHechoes
                                    .Where(x => x.NUCs.nucg == NUC)
                                    .Select(a => new ListarInfoNucViewModel
                                    {
                                        DistritoActual = a.ModuloServicio.Agencia.DSP.Distrito.Nombre,
                                        SubdireccionActual = (a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == IdDistrito ? a.ModuloServicio.Agencia.DSP.NombreSubDir : ""),
                                        AgenciaActual = (a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == IdDistrito ? a.ModuloServicio.Agencia.Nombre : ""),
                                        ModuloActual = (a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == IdDistrito ? a.ModuloServicio.Nombre : ""),
                                        DistritoIdActual = a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito,
                                    })
                                    .ToListAsync();

                    var newIdDistrito = nucResult.FirstOrDefault()?.DistritoIdActual;

                    return Ok(new
                    {
                        Resultado = nucResult,
                        DistritoIdActual = newIdDistrito
                    });
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException?.Message ?? ex.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
        }
    }
}