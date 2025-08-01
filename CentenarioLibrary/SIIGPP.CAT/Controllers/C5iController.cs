using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.C5i;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.C5i;
using SIIGPP.CAT.FilterClass;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C5iController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;
        public C5iController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/C5i/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            Guid idc5;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            C5 ci = new C5
            {
                RHechoId = model.RHechoId,
                NumeroOficio = model.NumeroOficio,
                Status = model.Status,
                FechaStatus = model.FechaStatus,
                HoraStatus = model.HoraStatus,
                Encargadoc5 = model.Encargadoc5,
                Puestoc5 = model.Puestoc5,
                Direccion5 = model.Direccion5,
                Agentequerecibe = model.Agentequerecibe,
                NumtelefonicoS = model.NumtelefonicoS,
                CorreoElecS = model.CorreoElecS,
                Op1 = model.Op1,
                Op2 = model.Op2,
                Op3 = model.Op3,
                Op4 = model.Op4,
                Op5 = model.Op5,
                Op5Texto = model.Op5Texto,
                Op6 = model.Op6,
                Op7 = model.Op7,
                Descripcion = model.Descripcion,
                RazonDescripcion = model.RazonDescripcion,
                FechaSys = System.DateTime.Now,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                UUsuario = model.UUsuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo
            };

            _context.C5s.Add(ci);
            try
            {
                await _context.SaveChangesAsync();
                idc5 = ci.IdC5;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.2" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new {idc5 = idc5});
        }

        // GET: api/C5i/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<C5ViewModel>> Listar([FromRoute] Guid RHechoId)
        {
            var vehiculo = await _context.C5s
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.FechaSys)
                .ToListAsync();

            return vehiculo.Select(a => new C5ViewModel
            {
                IdC5 = a.IdC5,
                RHechoId = a.RHechoId,
                NumeroOficio = a.NumeroOficio,
                Status = a.Status,
                FechaStatus = a.FechaStatus,
                HoraStatus = a.HoraStatus,
                Encargadoc5 = a.Encargadoc5,
                Puestoc5 = a.Puestoc5,
                Direccion5 = a.Direccion5,
                Agentequerecibe = a.Agentequerecibe,
                NumtelefonicoS = a.NumtelefonicoS,
                CorreoElecS = a.CorreoElecS,
                Op1 = a.Op1,
                Op2 = a.Op2,
                Op3 = a.Op3,
                Op4 = a.Op4,
                Op5 = a.Op5,
                Op5Texto = a.Op5Texto,
                Op6 = a.Op6,
                Op7 = a.Op7,
                Descripcion = a.Descripcion,
                RazonDescripcion = a.RazonDescripcion,
                FechaSys = a.FechaSys,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                UUsuario = a.UUsuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Insumos = cinsumos(a.Op1, a.Op2, a.Op3, a.Op4, a.Op5, a.Op6, a.Op7)
            });
        }

        public string cinsumos(Boolean a, Boolean b, Boolean c, Boolean d, Boolean e, Boolean f, Boolean g)
        {
            string fi = "";
            if (a) fi = "I, ";
            if (b) fi += "II, ";
            if (c) fi += "III, ";
            if (d) fi += "VI, ";
            if (e) fi += "V, ";
            if (f) fi += "VI, ";
            if (g) fi += "VII";
            return fi;
        }

        // GET: api/C5i/SolicitudesTotalesEstadistica
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{SolicitudesSPEstadistica}")]
        public async Task<IEnumerable<C5EstadisticaViewModel>> SolicitudesTotalesEstadistica([FromQuery] C5SolicitudesEstadistica C5SolicitudesEstadistica)
        {
            var sps = await _context.C5s
                .Where(a => a.RHecho.NucId != null)
                .Where(a => C5SolicitudesEstadistica.DatosGenerales.Distritoact ? a.RHecho.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == C5SolicitudesEstadistica.DatosGenerales.Distrito : 1 == 1)
                .Where(a => C5SolicitudesEstadistica.DatosGenerales.Dspact ? a.RHecho.ModuloServicio.Agencia.DSP.IdDSP == C5SolicitudesEstadistica.DatosGenerales.Dsp : 1 == 1)
                .Where(a => C5SolicitudesEstadistica.DatosGenerales.Agenciaact ? a.RHecho.ModuloServicio.Agencia.IdAgencia == C5SolicitudesEstadistica.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.RHecho.FechaElevaNuc2 >= C5SolicitudesEstadistica.DatosGenerales.Fechadesde)
                .Where(a => a.RHecho.FechaElevaNuc2 <= C5SolicitudesEstadistica.DatosGenerales.Fechahasta)
                .ToListAsync();

            IEnumerable<C5EstadisticaViewModel> items = new C5EstadisticaViewModel[] { };

            IEnumerable<C5EstadisticaViewModel> ReadLines(int cantidad, string tipo)
            {
                IEnumerable<C5EstadisticaViewModel> item2;

                item2 = (new[]{new C5EstadisticaViewModel{
                                Cantidad = cantidad,
                                Tipo = tipo
                            }});

                return item2;
            }

            items = items.Concat(ReadLines(sps.Count, "Total de solicitudes a C5i"));

            return items;
        }

        // GET: api/C5i/Eliminar
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
                var consultaC5 = await _context.C5s.Where(a => a.IdC5 == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaC5 == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningun Oficio de C5i con la información enviada" });
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
                            MovimientoId = new Guid("f28e49e6-bb67-4825-889d-a564f586514c")
                        };

                        ctx.Add(laRegistro);
                        LogC5Formatos c5 = new LogC5Formatos
                        {
                            LogAdmonId = gLog,
                            IdC5 = consultaC5.IdC5,
                            RHechoId = consultaC5.RHechoId,
                            NumeroOficio = consultaC5.NumeroOficio,
                            Status = consultaC5.Status,
                            FechaStatus = consultaC5.FechaStatus,
                            HoraStatus = consultaC5.HoraStatus,
                            Encargadoc5 = consultaC5.Encargadoc5,
                            Puestoc5 = consultaC5.Puestoc5,
                            Direccion5 = consultaC5.Direccion5,
                            Agentequerecibe = consultaC5.Agentequerecibe,
                            NumtelefonicoS = consultaC5.NumtelefonicoS,
                            CorreoElecS = consultaC5.CorreoElecS,
                            Op1 = consultaC5.Op1,
                            Op2 = consultaC5.Op2,
                            Op3 = consultaC5.Op3,
                            Op4 = consultaC5.Op4,
                            Op5 = consultaC5.Op5,
                            Op5Texto = consultaC5.Op5Texto,
                            Op6 = consultaC5.Op6,
                            Op7 = consultaC5.Op7,
                            Descripcion = consultaC5.Descripcion,
                            RazonDescripcion = consultaC5.RazonDescripcion,
                            FechaSys = consultaC5.FechaSys,
                            UDistrito = consultaC5.UDistrito,
                            USubproc = consultaC5.USubproc,
                            UAgencia = consultaC5.UAgencia,
                            UUsuario = consultaC5.UUsuario,
                            UPuesto = consultaC5.UPuesto,
                            UModulo = consultaC5.UModulo
                        };
                        ctx.Add(c5);
                        _context.Remove(consultaC5);

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
            return Ok(new { res = "success", men = "Formato de C5i eliminado Correctamente" });
        }

        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/C5i/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var c5Buscados = await _context.C5s.Where(x => x.RHechoId == model.IdRHecho).ToListAsync();
            if (c5Buscados == null)
            {
                return Ok();
            }
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    foreach(C5 c5Actual in c5Buscados)
                    {
                        var insertarC5 = await ctx.C5s.FirstOrDefaultAsync(a => a.IdC5 == c5Actual.IdC5);
                        if (insertarC5 == null)
                        {
                            insertarC5 = new C5();
                            ctx.C5s.Add(insertarC5);
                        }
                        insertarC5.IdC5 = c5Actual.IdC5;
                        insertarC5.RHechoId = c5Actual.RHechoId;
                        insertarC5.NumeroOficio = c5Actual.NumeroOficio;
                        insertarC5.Status = c5Actual.Status;
                        insertarC5.FechaStatus = c5Actual.FechaStatus;
                        insertarC5.HoraStatus = c5Actual.HoraStatus;
                        insertarC5.Encargadoc5 = c5Actual.Encargadoc5;
                        insertarC5.Puestoc5 = c5Actual.Puestoc5;
                        insertarC5.Direccion5 = c5Actual.Direccion5;
                        insertarC5.Agentequerecibe = c5Actual.Agentequerecibe;
                        insertarC5.NumtelefonicoS = c5Actual.NumtelefonicoS;
                        insertarC5.CorreoElecS = c5Actual.CorreoElecS;
                        insertarC5.Op1 = c5Actual.Op1;
                        insertarC5.Op2 = c5Actual.Op2;
                        insertarC5.Op3 = c5Actual.Op3;
                        insertarC5.Op4 = c5Actual.Op4;
                        insertarC5.Op5 = c5Actual.Op5;
                        insertarC5.Op5Texto = c5Actual.Op5Texto;
                        insertarC5.Op6 = c5Actual.Op6;
                        insertarC5.Op7 = c5Actual.Op7;
                        insertarC5.Descripcion = c5Actual.Descripcion;
                        insertarC5.RazonDescripcion = c5Actual.RazonDescripcion;
                        insertarC5.FechaSys = c5Actual.FechaSys;
                        insertarC5.UDistrito = c5Actual.UDistrito;
                        insertarC5.USubproc = c5Actual.USubproc;
                        insertarC5.UAgencia = c5Actual.UAgencia;
                        insertarC5.UUsuario = c5Actual.UUsuario;
                        insertarC5.UPuesto = c5Actual.UPuesto;
                        insertarC5.UModulo = c5Actual.UModulo;
                        await ctx.SaveChangesAsync();
                    }
                    return Ok();
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