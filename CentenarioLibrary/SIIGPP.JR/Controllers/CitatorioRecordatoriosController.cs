using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_JR.RCitatorioRecordatorio;
using SIIGPP.Entidades.M_JR.RSesion;
using SIIGPP.JR.Models.RCitatorioRecordatorio;

namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitatorioRecordatoriosController : ControllerBase
    {
        public readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public CitatorioRecordatoriosController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
       
        // GET: api/CitatorioRecordatorios/ListarSCPorEnvio
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{envioId}")]
        public async Task<IEnumerable<GET_CitatorioRecordatorioSViewModel>> ListarSCPorEnvio([FromRoute] Guid envioId)
        {
            var Tabla = await _context.CitatorioRecordatorios
                                    .Include(a => a.Sesion)
                                    .Where(a => a.Sesion.EnvioId == envioId)
                                    .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_CitatorioRecordatorioSViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_CitatorioRecordatorioSViewModel
                {
                    SesionId = a.SesionId,
                    ModuloServicioId = a.Sesion.ModuloServicioId,
                    EnvioId = a.Sesion.EnvioId,
                    NoSesion = a.Sesion.NoSesion,
                    FechaHoraSys = a.Sesion.FechaHoraSys,
                    StatusSesion = a.Sesion.StatusSesion,
                    DescripcionSesion = a.Sesion.DescripcionSesion,
                    Asunto = a.Sesion.Asunto,
                    FechaHora = a.Sesion.FechaHora,
                    Solicitates = a.Sesion.Solicitates,
                    Reuqeridos = a.Sesion.Reuqeridos,
                    uf_Distrito = a.uf_Distrito,
                    uf_DirSubProc = a.uf_DirSubProc,
                    uf_Agencia = a.uf_Agencia,
                    uf_Modulo = a.uf_Modulo,
                    uf_Nombre = a.uf_Nombre,
                    uf_Puesto = a.uf_Puesto,
                    IdRCitatoriosRecordatorios  = a.IdCitatorioRecordatorio,
                     NoExpediente = a.NoExpediente,
                     FechaSys = a.FechaSys,
                     FechaHoraCita = a.FechaHoraCita,
                     Duracion = a.Duracion,
                     LugarCita = a.LugarCita,
                     dirigidoa_Nombre = a.dirigidoa_Nombre,
                     dirigidoa_Direccion = a.dirigidoa_Direccion,
                     dirigidoa_Telefono = a.dirigidoa_Telefono,
                     solicitadoPor = a.solicitadoPor,
                     solicitadoPor_Telefono = a.solicitadoPor_Telefono,
                     Textooficio = a.Textooficio,
                     NoCitatorio = a.NoCitatorio,
                     ContadorSMSS = a.ContadorSMSS,
                     ContadorSMSR = a.ContadorSMSR,
                    StatusAsistencia = a.StatusAsistencia,
                     StatusEntrega = a.StatusEntrega
                });
            }
        }

        // GET: api/CitatorioRecordatorios/ListarCR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{sesionId}")]
        public async Task<IActionResult> ListarCR([FromRoute] Guid sesionId)
        {
            try
            {
                var Tabla = await _context.CitatorioRecordatorios
                                        .Include(a => a.Sesion)
                                        .Where(a => a.SesionId == sesionId)
                                        .ToListAsync();

                if (Tabla.Count == 0)
                {
                    return Ok(Enumerable.Empty<GET_CitatorioRecordatorioViewModel>());
                }
                else
                {

                    return Ok(Tabla.Select(a => new GET_CitatorioRecordatorioViewModel
                    {
                        IdRCitatoriosRecordatorios = a.IdCitatorioRecordatorio,
                        SesionId = a.SesionId,
                        NoSesion = a.Sesion.NoSesion,
                        NoExpediente = a.NoExpediente,
                        FechaSys = a.FechaSys,
                        FechaHoraCita = a.FechaHoraCita,
                        Duracion = a.Duracion,
                        LugarCita = a.LugarCita,
                        dirigidoa_Nombre = a.dirigidoa_Nombre,
                        dirigidoa_Direccion = a.dirigidoa_Direccion,
                        dirigidoa_Telefono = a.dirigidoa_Telefono,
                        solicitadoPor = a.solicitadoPor,
                        solicitadoPor_Telefono = a.solicitadoPor_Telefono,
                        Textooficio = a.Textooficio,
                        uf_Distrito = a.uf_Distrito,
                        uf_DirSubProc = a.uf_DirSubProc,
                        uf_Agencia = a.uf_Agencia,
                        uf_Modulo = a.uf_Modulo,
                        uf_Nombre = a.uf_Nombre,
                        uf_Puesto = a.uf_Puesto,
                        NoCitatorio = a.NoCitatorio,
                        StatusAsistencia = a.StatusAsistencia,
                        StatusEntrega = a.StatusEntrega,
                        ContadorSMSR = a.ContadorSMSR,
                        ContadorSMSS = a.ContadorSMSS,

                    }));
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/CitatorioRecordatorios/FiltrarNoCIta
        [HttpGet("[action]/{NoExpediente}")]

        public async Task<IActionResult> FiltrarNoCIta([FromRoute] string NoExpediente )
        {
            string  stringNoExpediente = HextoString(NoExpediente);

            var consultarCita = await _context.CitatorioRecordatorios
                           .Where(a => a.NoExpediente == stringNoExpediente) 
                           .OrderByDescending(a => a.IdCitatorioRecordatorio)
                           .Take(1)
                           .FirstOrDefaultAsync();

            if (consultarCita == null)
            {
                return Ok(new { nc = 0 });

            }

            return Ok(new {nc = consultarCita.NoCitatorio });
        }

        // GET: api/CitatorioRecordatorios/FiltrarNoCItaC
        [HttpGet("[action]/{IdConjuntoDerivaciones}")]

        public async Task<IActionResult> FiltrarNoCItaC([FromRoute] Guid IdConjuntoDerivaciones)
        {

            var consultarCita = await (from sc in _context.SesionConjuntos
                                       join s in _context.Sesions on sc.SesionId equals s.IdSesion
                                       join c in _context.CitatorioRecordatorios on s.IdSesion equals c.SesionId
                                       where sc.ConjuntoDerivacionesId == IdConjuntoDerivaciones
                                       orderby c.NoCitatorio descending
                                       select c.NoCitatorio)
                                       .FirstOrDefaultAsync();

            //if (consultarCita == null)
            if (consultarCita == 0)
            {
                return Ok(new { nc = 0 });
            }
            return Ok(new { nc = consultarCita });
        }

        public static string HextoString(string InputText)
        {
            byte[] bb = Enumerable.Range(0, InputText.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(InputText.Substring(x, 2), 16))
                             .ToArray();
            return System.Text.Encoding.ASCII.GetString(bb);
            // or System.Text.Encoding.UTF7.GetString
            // or System.Text.Encoding.UTF8.GetString
            // or System.Text.Encoding.Unicode.GetString
            // or etc.
        }

        //*****************************************************************************************************************//
        //*****************************************************************************************************************//
        //ESTADISTICA

        // GET: api/CitatorioRecordatorios/ListarAsistencia
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GET_EAsistenciaViewModel>> ListarAsistencia()
        {
            var consulta = await _context.CitatorioRecordatorios
                                        .GroupBy(v => v.StatusAsistencia) 
                                        .Select(x=> new {
                                            etiqueta = x.Key,
                                            valor = x.Count(v=> v.StatusAsistencia == x.Key), 
                                         }) 
                                        .ToListAsync();

     


            return consulta.Select(v => new GET_EAsistenciaViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valorAsistencia = v.valor,  

            });
            
        }
        // GET: api/CitatorioRecordatorios/ListarEntregas
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<GET_EAEntregaViewModel>> ListarEntregas()
        {
            var consulta = await _context.CitatorioRecordatorios
                                        .GroupBy(v => v.StatusEntrega)
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            valor = x.Count(v => v.StatusEntrega == x.Key),
                                        })
                                        .ToListAsync();




            return consulta.Select(v => new GET_EAEntregaViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valorEntrega = v.valor,

            });

        }

        // GET: api/CitatorioRecordatorios/ListarAsistenciaDetalleAño
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{año}")]
        public async Task<IEnumerable<GET_EAsistenciaDetalleViewModel>> ListarAsistenciaDetalleAno([FromRoute]  int año)
        {
            var consulta = await _context.CitatorioRecordatorios 
                                        .Where(v => v.FechaSys.Year == año)
                                        .GroupBy(v => v.un_Nombre)
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            valor = x.Count(v => v.un_Nombre == x.Key),
                                            status1 = x.Count(v => v.StatusAsistencia == true),
                                            status2 = x.Count(v => v.StatusAsistencia == false),
                                        })
                                        .ToListAsync();
             

            return consulta.Select(v => new GET_EAsistenciaDetalleViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valorTotal = v.valor,
                Asiste = v.status1,
                NoAsiste = v.status2


            });

        }
        // GET: api/CitatorioRecordatorios/ListarAsistenciaDetalleMesAño
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{mes}/{año}")]
        public async Task<IEnumerable<GET_EAsistenciaDetalleViewModel>> ListarAsistenciaDetalleMesAño([FromRoute] int mes, int año)
        {
            var consulta = await _context.CitatorioRecordatorios
                                        .Where(v => v.FechaSys.Month == mes)
                                        .Where(v => v.FechaSys.Year == año)
                                        .GroupBy(v => v.un_Nombre) 
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            valor = x.Count(v => v.un_Nombre == x.Key),
                                            status1 = x.Count(v => v.StatusAsistencia == true),
                                            status2 = x.Count(v => v.StatusAsistencia == false),
                                        })
                                        .ToListAsync();


 

            return consulta.Select(v => new GET_EAsistenciaDetalleViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valorTotal = v.valor,
                Asiste = v.status1,
                NoAsiste= v.status2


            });

        }
        // GET: api/CitatorioRecordatorios/ListarAsistenciaDetalleDia
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{fi}/{ff}")]
        public async Task<IEnumerable<GET_EAsistenciaDetalleViewModel>> ListarAsistenciaDetalleDias([FromRoute] DateTime fi, DateTime ff)
        {
            var consulta = await _context.CitatorioRecordatorios
                                        .Where(v => v.FechaSys >= fi)
                                        .Where(v => v.FechaSys <= ff) 
                                        .GroupBy(v => v.un_Nombre)
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            valor = x.Count(v => v.un_Nombre == x.Key),
                                            status1 = x.Count(v => v.StatusAsistencia == true),
                                            status2 = x.Count(v => v.StatusAsistencia == false),
                                        })
                                        .ToListAsync();

             

            return consulta.Select(v => new GET_EAsistenciaDetalleViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valorTotal = v.valor,
                Asiste = v.status1,
                NoAsiste = v.status2


            });

        }

        //ESTADISITCAS DE ENTREGAS 
        // GET: api/CitatorioRecordatorios/ListarEntregasAno
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{año}")]
        public async Task<IEnumerable<GET_EEntregasViewModel>> ListarEntregasAno([FromRoute]  int año)
        {
            var consulta = await _context.CitatorioRecordatorios 
                                        .Where(v => v.FechaSys.Year == año)
                                        .GroupBy(v => v.un_Nombre)
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            valor = x.Count(v => v.un_Nombre == x.Key),
                                            statusE1 = x.Count(v => v.StatusEntrega == "Domicilio incorrecto"),
                                            statusE2 = x.Count(v => v.StatusEntrega == "Entregado a familiar o conocido"),
                                            statusE3 = x.Count(v => v.StatusEntrega == "Entregado personal"),
                                            statusE4 = x.Count(v => v.StatusEntrega == "Fijado"),
                                            statusE5 = x.Count(v => v.StatusEntrega == "No se ubica el domicilio"),
                                            statusE6 = x.Count(v => v.StatusEntrega == "Nombre incorrecto"),
                                            statusE7 = x.Count(v => v.StatusEntrega == "Ya no vive ahí"),
                                            statusE8 = x.Count(v => v.StatusEntrega == "En proceso"), 
                                        })
                                        .ToListAsync();
 

            return consulta.Select(v => new GET_EEntregasViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valorEntrega = v.valor,
                valor1 = v.statusE1,
                valor2 = v.statusE2,
                valor3 = v.statusE3,
                valor4 = v.statusE4,
                valor5 = v.statusE5,
                valor6 = v.statusE6,
                valor7 = v.statusE7,
                valor8 = v.statusE8,


            });

        }

        // GET: api/CitatorioRecordatorios/ListarEntregasMesAno
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{mes}/{año}")]
        public async Task<IEnumerable<GET_EEntregasViewModel>> ListarEntregasMesAno([FromRoute] int mes, int año)
        {
            var consulta = await _context.CitatorioRecordatorios 
                                        .Where(v => v.FechaSys.Month == mes)
                                        .Where(v => v.FechaSys.Year == año)
                                        .GroupBy(v => v.un_Nombre)
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            valor = x.Count(v => v.un_Nombre == x.Key),
                                            statusE1 = x.Count(v => v.StatusEntrega == "Domicilio incorrecto"),
                                            statusE2 = x.Count(v => v.StatusEntrega == "Entregado a familiar o conocido"),
                                            statusE3 = x.Count(v => v.StatusEntrega == "Entregado personal"),
                                            statusE4 = x.Count(v => v.StatusEntrega == "Fijado"),
                                            statusE5 = x.Count(v => v.StatusEntrega == "No se ubica el domicilio"),
                                            statusE6 = x.Count(v => v.StatusEntrega == "Nombre incorrecto"),
                                            statusE7 = x.Count(v => v.StatusEntrega == "Ya no vive ahí"),
                                            statusE8 = x.Count(v => v.StatusEntrega == "En proceso"),
                                        })
                                        .ToListAsync();


            return consulta.Select(v => new GET_EEntregasViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valorEntrega = v.valor,
                valor1 = v.statusE1,
                valor2 = v.statusE2,
                valor3 = v.statusE3,
                valor4 = v.statusE4,
                valor5 = v.statusE5,
                valor6 = v.statusE6,
                valor7 = v.statusE7,
                valor8 = v.statusE8,


            });

        }

        // GET: api/CitatorioRecordatorios/ListarEntregasDia
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{fi}/{ff}")]
        public async Task<IEnumerable<GET_EEntregasViewModel>> ListarEntregasDia([FromRoute] DateTime fi, DateTime ff)
        {
            var consulta = await _context.CitatorioRecordatorios
                                         .Where(v => v.FechaSys >= fi)
                                        .Where(v => v.FechaSys <= ff)
                                        .GroupBy(v => v.un_Nombre)
                                        .Select(x => new {
                                            etiqueta = x.Key,
                                            valor = x.Count(v => v.un_Nombre == x.Key),
                                            statusE1 = x.Count(v => v.StatusEntrega == "Domicilio incorrecto"),
                                            statusE2 = x.Count(v => v.StatusEntrega == "Entregado a familiar o conocido"),
                                            statusE3 = x.Count(v => v.StatusEntrega == "Entregado personal"),
                                            statusE4 = x.Count(v => v.StatusEntrega == "Fijado"),
                                            statusE5 = x.Count(v => v.StatusEntrega == "No se ubica el domicilio"),
                                            statusE6 = x.Count(v => v.StatusEntrega == "Nombre incorrecto"),
                                            statusE7 = x.Count(v => v.StatusEntrega == "Ya no vive ahí"),
                                            statusE8 = x.Count(v => v.StatusEntrega == "En proceso"),
                                        })
                                        .ToListAsync();


            return consulta.Select(v => new GET_EEntregasViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valorEntrega = v.valor,
                valor1 = v.statusE1,
                valor2 = v.statusE2,
                valor3 = v.statusE3,
                valor4 = v.statusE4,
                valor5 = v.statusE5,
                valor6 = v.statusE6,
                valor7 = v.statusE7,
                valor8 = v.statusE8,


            });

        }

 
        // GET: api/CitatorioRecordatorios/ListarMisCR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{moduloId}")]
        public async Task<IEnumerable<GET_DistinticViewModel>> ListarMisCR([FromRoute] Guid moduloId)
        { 
            var Tabla = await _context.CitatorioRecordatorios
                                    .Include(a => a.Sesion)
                                    .Where(a => a.Sesion.ModuloServicioId == moduloId)     
                                    .ToListAsync();
 

            return Tabla.Select(a => new GET_DistinticViewModel
            {
                IdRCitatoriosRecordatorios = a.IdCitatorioRecordatorio,
                SesionId = a.SesionId,
                NoSesion = a.Sesion.NoSesion,
                NoExpediente = a.NoExpediente,
                FechaSys = a.FechaSys,
                FechaHoraCita = a.FechaHoraCita,
                Duracion = a.Duracion,
                LugarCita = a.LugarCita,
                dirigidoa_Nombre = a.dirigidoa_Nombre,
                dirigidoa_Direccion = a.dirigidoa_Direccion,
                solicitadoPor = a.solicitadoPor, 
                Textooficio = a.Textooficio,
                uf_Distrito = a.uf_Distrito,
                uf_DirSubProc = a.uf_DirSubProc,
                uf_Agencia = a.uf_Agencia,
                uf_Modulo = a.uf_Modulo,
                uf_Nombre = a.uf_Nombre,
                uf_Puesto = a.uf_Puesto,
                NoCitatorio = a.NoCitatorio,
                StatusAsistencia = a.StatusAsistencia,
                StatusEntrega = a.StatusEntrega,
                StatusSesion = a.Sesion.StatusSesion,
            });
            //}).Distinct(new CompararExpediente());

        }


        // GET: api/CitatorioRecordatorios/ListarAgendaGlobal1
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoid}")]
        public async Task<IEnumerable<GET_DistinticViewModel>> ListarAgendaGlobal1([FromRoute] Guid distritoid)
        {
            var Tabla = await _context.CitatorioRecordatorios
                                    .Include(a => a.Sesion) 
                                    .Include(a => a.Sesion.Envios)
                                    .Include(a => a.Sesion.Envios.Expediente)
                                    .Include(a => a.Sesion.Envios.Expediente.RHecho)
                                    .Include(a => a.Sesion.Envios.Expediente.RHecho.Agencia)
                                    .Include(a => a.Sesion.Envios.Expediente.RHecho.Agencia.DSP)
                                    .Include(a => a.Sesion.Envios.Expediente.RHecho.Agencia.DSP.Distrito)
                                    .Where(a => a.Sesion.Envios.Expediente.RHecho.Agencia.DSP.Distrito.IdDistrito== distritoid)
                                    .ToListAsync();


            return Tabla.Select(a => new GET_DistinticViewModel
            {
                IdRCitatoriosRecordatorios = a.IdCitatorioRecordatorio,
                SesionId = a.SesionId,
                NoSesion = a.Sesion.NoSesion,
                NoExpediente = a.NoExpediente,
                FechaSys = a.FechaSys,
                FechaHoraCita = a.FechaHoraCita,
                Duracion = a.Duracion,
                LugarCita = a.LugarCita,
                dirigidoa_Nombre = a.dirigidoa_Nombre,
                dirigidoa_Direccion = a.dirigidoa_Direccion,
                solicitadoPor = a.solicitadoPor,
                Textooficio = a.Textooficio,
                uf_Distrito = a.uf_Distrito,
                uf_DirSubProc = a.uf_DirSubProc,
                uf_Agencia = a.uf_Agencia,
                uf_Modulo = a.uf_Modulo,
                uf_Nombre = a.uf_Nombre,
                uf_Puesto = a.uf_Puesto,
                NoCitatorio = a.NoCitatorio,
                StatusAsistencia = a.StatusAsistencia,
                StatusEntrega = a.StatusEntrega,
                StatusSesion = a.Sesion.StatusSesion,
            });
            //}).Distinct(new CompararExpediente());

        }

        // GET: api/CitatorioRecordatorios/ListarAgendaGlobal2
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoid}")]
        public async Task<IEnumerable<GET_DistinticViewModel>> ListarAgendaGlobal2([FromRoute] Guid distritoid)
        {



            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-7F662EC1-6705-406E-BCD0-F56ADE7BCAE2")).Options;
            using (var ctx = new DbContextSIIGPP(options))
            {

                var Tabla = await ctx.CitatorioRecordatorios
                                    .Include(a => a.Sesion)
                                    .Include(a => a.Sesion.Envios)
                                    .Include(a => a.Sesion.Envios.Expediente)
                                    .Include(a => a.Sesion.Envios.Expediente.RHecho)
                                    .Include(a => a.Sesion.Envios.Expediente.RHecho.Agencia)
                                    .Include(a => a.Sesion.Envios.Expediente.RHecho.Agencia.DSP)
                                    .Include(a => a.Sesion.Envios.Expediente.RHecho.Agencia.DSP.Distrito)
                                    .Where(a => a.Sesion.Envios.Expediente.RHecho.Agencia.DSP.Distrito.IdDistrito == distritoid)
                                    .ToListAsync();


                return Tabla.Select(a => new GET_DistinticViewModel
                {
                    IdRCitatoriosRecordatorios = a.IdCitatorioRecordatorio,
                    SesionId = a.SesionId,
                    NoSesion = a.Sesion.NoSesion,
                    NoExpediente = a.NoExpediente,
                    FechaSys = a.FechaSys,
                    FechaHoraCita = a.FechaHoraCita,
                    Duracion = a.Duracion,
                    LugarCita = a.LugarCita,
                    dirigidoa_Nombre = a.dirigidoa_Nombre,
                    dirigidoa_Direccion = a.dirigidoa_Direccion,
                    solicitadoPor = a.solicitadoPor,
                    Textooficio = a.Textooficio,
                    uf_Distrito = a.uf_Distrito,
                    uf_DirSubProc = a.uf_DirSubProc,
                    uf_Agencia = a.uf_Agencia,
                    uf_Modulo = a.uf_Modulo,
                    uf_Nombre = a.uf_Nombre,
                    uf_Puesto = a.uf_Puesto,
                    NoCitatorio = a.NoCitatorio,
                    StatusAsistencia = a.StatusAsistencia,
                    StatusEntrega = a.StatusEntrega,
                    StatusSesion = a.Sesion.StatusSesion,
                });
                //}).Distinct(new CompararExpediente());
            }
        }

        // GET: api/CitatorioRecordatorios/ListarMisCRStatus
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{moduloId}/{statusSesion}")]
        public async Task<IEnumerable<GET_DistinticViewModel>> ListarMisCRStatus([FromRoute] Guid moduloId, string statusSesion)
        {
            var Tabla = await _context.CitatorioRecordatorios
                                    .Include(a => a.Sesion)
                                    .Where(a => a.Sesion.ModuloServicioId == moduloId)
                                    .Where(a => a.Sesion.StatusSesion == statusSesion)
                                    .ToListAsync();


            return Tabla.Select(a => new GET_DistinticViewModel
            {
                IdRCitatoriosRecordatorios = a.IdCitatorioRecordatorio,
                SesionId = a.SesionId,
                NoSesion = a.Sesion.NoSesion,
                NoExpediente = a.NoExpediente,
                FechaSys = a.FechaSys,
                FechaHoraCita = a.FechaHoraCita,
                Duracion = a.Duracion,
                LugarCita = a.LugarCita,
                dirigidoa_Nombre = a.dirigidoa_Nombre,
                dirigidoa_Direccion = a.dirigidoa_Direccion,
                solicitadoPor = a.solicitadoPor,
                Textooficio = a.Textooficio,
                uf_Distrito = a.uf_Distrito,
                uf_DirSubProc = a.uf_DirSubProc,
                uf_Agencia = a.uf_Agencia,
                uf_Modulo = a.uf_Modulo,
                uf_Nombre = a.uf_Nombre,
                uf_Puesto = a.uf_Puesto,
                NoCitatorio = a.NoCitatorio,
                StatusAsistencia = a.StatusAsistencia,
                StatusEntrega = a.StatusEntrega,
                StatusSesion = a.Sesion.StatusSesion,
            });
            //}).Distinct(new CompararExpediente());

        }
        // GET: api/CitatorioRecordatorios/ListarCRStatusglobal
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{distritoid}/{statusSesion}")]
        public async Task<IEnumerable<GET_DistinticViewModel>> ListarCRStatusglobal([FromRoute] Guid distritoid, string statusSesion)
        {
            var Tabla = await _context.CitatorioRecordatorios
                                    .Include(a => a.Sesion)
                                    .Include(a => a.Sesion.Envios)
                                    .Include(a => a.Sesion.Envios.Expediente)
                                    .Include(a => a.Sesion.Envios.Expediente.RHecho)
                                    .Include(a => a.Sesion.Envios.Expediente.RHecho.Agencia)
                                    .Include(a => a.Sesion.Envios.Expediente.RHecho.Agencia.DSP)
                                    .Include(a => a.Sesion.Envios.Expediente.RHecho.Agencia.DSP.Distrito)
                                    .Where(a => a.Sesion.Envios.Expediente.RHecho.Agencia.DSP.Distrito.IdDistrito == distritoid) 
                                    .Where(a => a.Sesion.StatusSesion == statusSesion)
                                    .ToListAsync();


            return Tabla.Select(a => new GET_DistinticViewModel
            {
                IdRCitatoriosRecordatorios = a.IdCitatorioRecordatorio,
                SesionId = a.SesionId,
                NoSesion = a.Sesion.NoSesion,
                NoExpediente = a.NoExpediente,
                FechaSys = a.FechaSys,
                FechaHoraCita = a.FechaHoraCita,
                Duracion = a.Duracion,
                LugarCita = a.LugarCita,
                dirigidoa_Nombre = a.dirigidoa_Nombre,
                dirigidoa_Direccion = a.dirigidoa_Direccion,
                solicitadoPor = a.solicitadoPor,
                Textooficio = a.Textooficio,
                uf_Distrito = a.uf_Distrito,
                uf_DirSubProc = a.uf_DirSubProc,
                uf_Agencia = a.uf_Agencia,
                uf_Modulo = a.uf_Modulo,
                uf_Nombre = a.uf_Nombre,
                uf_Puesto = a.uf_Puesto,
                NoCitatorio = a.NoCitatorio,
                StatusAsistencia = a.StatusAsistencia,
                StatusEntrega = a.StatusEntrega,
                StatusSesion = a.Sesion.StatusSesion,
            });
            //}).Distinct(new CompararExpediente());

        }

        // GET: api/CitatorioRecordatorios/ListaFiltroCR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{moduloId}")]
        public async Task<IEnumerable<GET_FiltroAgendaViewModel>> ListaFiltroCR([FromRoute] Guid moduloId)
        {
            var Tabla = await _context.CitatorioRecordatorios
                                    .Include(a => a.Sesion)
                                    .Where(a => a.Sesion.ModuloServicioId == moduloId)
                                    .ToListAsync();


            return Tabla.Select(a => new GET_FiltroAgendaViewModel
            {
                IdRCitatoriosRecordatorios = a.IdCitatorioRecordatorio,
                SesionId = a.SesionId,
                NoSesion = a.Sesion.NoSesion,
                NoExpediente = a.NoExpediente,
                FechaSys = a.FechaSys,
                FechaHoraCita = a.FechaHoraCita,
                Duracion = a.Duracion,
                LugarCita = a.LugarCita,
                dirigidoa_Nombre = a.dirigidoa_Nombre,
                dirigidoa_Direccion = a.dirigidoa_Direccion,
                solicitadoPor = a.solicitadoPor,
                Textooficio = a.Textooficio,
                uf_Distrito = a.uf_Distrito,
                uf_DirSubProc = a.uf_DirSubProc,
                uf_Agencia = a.uf_Agencia,
                uf_Modulo = a.uf_Modulo,
                uf_Nombre = a.uf_Nombre,
                uf_Puesto = a.uf_Puesto,
                NoCitatorio = a.NoCitatorio,
                StatusAsistencia = a.StatusAsistencia,
                StatusEntrega = a.StatusEntrega,

            });

        }

        // GET: api/CitatorioRecordatorios/ListarC
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{moduloId}/{sesionId}")]
        public async Task<IEnumerable<GET_CitatorioRecordatorioViewModel>> ListarC([FromRoute] Guid moduloId, Guid sesionId)
        {



            var Tabla = await _context.CitatorioRecordatorios
                                    .Include(a => a.Sesion)
                                    .Where(a => a.Sesion.ModuloServicioId == moduloId)
                                    .Where(a=> a.SesionId == sesionId)
                                    .ToListAsync();



            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_CitatorioRecordatorioViewModel>();
            }
            return Tabla.Select(a => new GET_CitatorioRecordatorioViewModel
            {
                IdRCitatoriosRecordatorios = a.IdCitatorioRecordatorio,
                SesionId = a.SesionId,
                NoSesion = a.Sesion.NoSesion,
                NoExpediente = a.NoExpediente,
                FechaSys = a.FechaSys,
                FechaHoraCita = a.FechaHoraCita,
                Duracion = a.Duracion,
                LugarCita = a.LugarCita,
                dirigidoa_Nombre = a.dirigidoa_Nombre,
                dirigidoa_Direccion = a.dirigidoa_Direccion,
                solicitadoPor = a.solicitadoPor, 
                Textooficio = a.Textooficio,
                uf_Distrito = a.uf_Distrito,
                uf_DirSubProc = a.uf_DirSubProc,
                uf_Agencia = a.uf_Agencia,
                uf_Modulo = a.uf_Modulo,
                uf_Nombre = a.uf_Nombre,
                uf_Puesto = a.uf_Puesto,
                NoCitatorio = a.NoCitatorio,
                StatusAsistencia = a.StatusAsistencia,
                StatusEntrega = a.StatusEntrega,
                StatusSesion = a.Sesion.StatusSesion,

            });

        }
        
        // GET: api/CitatorioRecordatorios/ListarCRConjunto
        // [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{ConjuntoDerivacionId}")]
        public async Task<IEnumerable<GET_CitatorioRecordatorioViewModel>> ListarCRConjunto([FromRoute] Guid ConjuntoDerivacionId)
        {
            
            var tabla = await _context.SesionConjuntos
                                .Join(
                                        _context.CitatorioRecordatorios,
                                                sc => sc.SesionId,
                                                c => c.SesionId,
                                                (sc, c) => new { sc, c })
                                .Join(
                                        _context.Sesions,
                                                temp => temp.c.SesionId,
                                                s => s.IdSesion,
                                                (temp, s) => new { temp.sc, temp.c, s })
                                .Where(joined => joined.sc.ConjuntoDerivacionesId == ConjuntoDerivacionId)
                                .ToListAsync();

            Console.WriteLine($"encontardo el error {tabla}"); 


            if (tabla.Count == 0)
            {
                return Enumerable.Empty<GET_CitatorioRecordatorioViewModel>();
            }
            else
            {

                return tabla.Select(a => new GET_CitatorioRecordatorioViewModel
                {
                    IdRCitatoriosRecordatorios = a.c.IdCitatorioRecordatorio,
                    SesionId = a.c.SesionId,
                    NoSesion = a.s.NoSesion,
                    NoExpediente = a.c.NoExpediente,
                    FechaSys = a.c.FechaSys,
                    FechaHoraCita = a.c.FechaHoraCita,
                    Duracion = a.c.Duracion,
                    LugarCita = a.c.LugarCita,
                    dirigidoa_Nombre = a.c.dirigidoa_Nombre,
                    dirigidoa_Direccion = a.c.dirigidoa_Direccion,
                    dirigidoa_Telefono = a.c.dirigidoa_Telefono,
                    solicitadoPor = a.c.solicitadoPor,
                    solicitadoPor_Telefono = a.c.solicitadoPor_Telefono,
                    Textooficio = a.c.Textooficio,
                    uf_Distrito = a.c.uf_Distrito,
                    uf_DirSubProc = a.c.uf_DirSubProc,
                    uf_Agencia = a.c.uf_Agencia,
                    un_Nombre = a.c.un_Nombre,
                    un_Puesto = a.c.un_Puesto,
                    uf_Modulo = a.c.uf_Modulo,
                    uf_Nombre = a.c.uf_Nombre,
                    uf_Puesto = a.c.uf_Puesto,
                    NoCitatorio = a.c.NoCitatorio,
                    StatusAsistencia = a.c.StatusAsistencia,
                    StatusEntrega = a.c.StatusEntrega,
                    ContadorSMSR = a.c.ContadorSMSR,
                    ContadorSMSS = a.c.ContadorSMSS,
                });
            }
                 
        }
        
        // GET: api/CitatorioRecordatorios/ListarCREnvio
        // [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{envioId}")]
        public async Task<IEnumerable<GET_CitatorioRecordatorioViewModel>> ListarCREnvio([FromRoute] Guid envioId)
        {

            var Tabla = await _context.CitatorioRecordatorios
                                    .Include(a => a.Sesion)
                                    .Include(a=>a.Sesion.Envios.Expediente)
                                    .Where(a => a.Sesion.EnvioId == envioId)
                                    .ToListAsync();


            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_CitatorioRecordatorioViewModel>();
            }
            else
            {

                return Tabla.Select(a => new GET_CitatorioRecordatorioViewModel
                {
                    IdRCitatoriosRecordatorios = a.IdCitatorioRecordatorio,
                    SesionId = a.SesionId,
                    NoSesion = a.Sesion.NoSesion,
                    NoExpediente = a.NoExpediente,
                    FechaSys = a.FechaSys,
                    fechaExpediente=a.Sesion.Envios.Expediente.FechaRegistroExpediente,
                    fechaDerivacion=a.Sesion.Envios.FechaRegistro,
                    FechaHoraCita = a.FechaHoraCita,
                    Duracion = a.Duracion,
                    LugarCita = a.LugarCita,
                    dirigidoa_Nombre = a.dirigidoa_Nombre,
                    dirigidoa_Direccion = a.dirigidoa_Direccion,
                    dirigidoa_Telefono = a.dirigidoa_Telefono,
                    solicitadoPor = a.solicitadoPor,
                    solicitadoPor_Telefono = a.solicitadoPor_Telefono,
                    Textooficio = a.Textooficio,
                    uf_Distrito = a.uf_Distrito,
                    uf_DirSubProc = a.uf_DirSubProc,
                    uf_Agencia = a.uf_Agencia,
                    un_Nombre = a.un_Nombre,
                    un_Puesto= a.un_Puesto,
                    uf_Modulo = a.uf_Modulo,
                    uf_Nombre = a.uf_Nombre,
                    uf_Puesto = a.uf_Puesto,
                    NoCitatorio = a.NoCitatorio,
                    StatusAsistencia = a.StatusAsistencia,
                    StatusEntrega = a.StatusEntrega,
                    ContadorSMSR = a.ContadorSMSR,
                    ContadorSMSS = a.ContadorSMSS,

                });
            }
        }

        
        // GET: api/CitatorioRecordatorios/ListarCitatoriosModulo
        // [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{modulo}/{tipomodulo}/{nombrefacilitador}")]
        public async Task<IEnumerable<GET_CitatorioRecordatorioViewModel>> ListarCitatoriosModulo([FromRoute] string modulo, string tipomodulo, string nombrefacilitador)
        {
            if (tipomodulo == "Notificador")
            {
                 var Tabla = await _context.CitatorioRecordatorios
                                    .Include(a => a.Sesion)
                                    .Where(a => a.un_Modulo == modulo)
                                    .OrderByDescending(a => a.FechaSys)
                                    .ToListAsync();

                if (Tabla.Count == 0)
                {
                    return Enumerable.Empty<GET_CitatorioRecordatorioViewModel>();
                } else {
                    return Tabla.Select(a => new GET_CitatorioRecordatorioViewModel {
                        IdRCitatoriosRecordatorios = a.IdCitatorioRecordatorio,
                        SesionId = a.SesionId,
                        EnvioId = a.Sesion.EnvioId,
                        NoSesion = a.Sesion.NoSesion,
                        NoExpediente = a.NoExpediente,
                        FechaSys = a.FechaSys,
                        FechaHoraCita = a.FechaHoraCita,
                        Duracion = a.Duracion,
                        LugarCita = a.LugarCita,
                        dirigidoa_Nombre = a.dirigidoa_Nombre,
                        dirigidoa_Direccion = a.dirigidoa_Direccion,
                        dirigidoa_Telefono = a.dirigidoa_Telefono,
                        solicitadoPor = a.solicitadoPor,
                        solicitadoPor_Telefono = a.solicitadoPor_Telefono,
                        Textooficio = a.Textooficio,
                        uf_Distrito = a.uf_Distrito,
                        uf_DirSubProc = a.uf_DirSubProc,
                        uf_Agencia = a.uf_Agencia,
                        uf_Modulo = a.uf_Modulo,
                        uf_Nombre = a.uf_Nombre,
                        uf_Puesto = a.uf_Puesto,
                        NoCitatorio = a.NoCitatorio,
                        StatusAsistencia = a.StatusAsistencia,
                        StatusEntrega = a.StatusEntrega,
                        ContadorSMSR = a.ContadorSMSR,
                        ContadorSMSS = a.ContadorSMSS,
                    });  
                }

            } else {
                var Tabla = await _context.CitatorioRecordatorios
                            .Include(a => a.Sesion)
                            .Where(a => a.uf_Nombre.Contains(nombrefacilitador))
                            .OrderByDescending(a => a.FechaSys)
                            .ToListAsync();
                if (Tabla.Count == 0)
                {
                    return Enumerable.Empty<GET_CitatorioRecordatorioViewModel>();
                }
                else {
                    return Tabla.Select(a => new GET_CitatorioRecordatorioViewModel {
                        IdRCitatoriosRecordatorios = a.IdCitatorioRecordatorio,
                        SesionId = a.SesionId,
                        EnvioId = a.Sesion.EnvioId,
                        NoSesion = a.Sesion.NoSesion,
                        NoExpediente = a.NoExpediente,
                        FechaSys = a.FechaSys,
                        FechaHoraCita = a.FechaHoraCita,
                        Duracion = a.Duracion,
                        LugarCita = a.LugarCita,
                        dirigidoa_Nombre = a.dirigidoa_Nombre,
                        dirigidoa_Direccion = a.dirigidoa_Direccion,
                        dirigidoa_Telefono = a.dirigidoa_Telefono,
                        solicitadoPor = a.solicitadoPor,
                        solicitadoPor_Telefono = a.solicitadoPor_Telefono,
                        Textooficio = a.Textooficio,
                        uf_Distrito = a.uf_Distrito,
                        uf_DirSubProc = a.uf_DirSubProc,
                        uf_Agencia = a.uf_Agencia,
                        uf_Modulo = a.uf_Modulo,
                        uf_Nombre = a.uf_Nombre,
                        uf_Puesto = a.uf_Puesto,
                        NoCitatorio = a.NoCitatorio,
                        StatusAsistencia = a.StatusAsistencia,
                        StatusEntrega = a.StatusEntrega,
                        ContadorSMSR = a.ContadorSMSR,
                        ContadorSMSS = a.ContadorSMSS,
                    });
                }
            }
        }

        // GET: api/CitatorioRecordatorios/ListarCitatoriosModuloFiltros
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{modulo}/{fechai}/{fechaf}/{StatusEntrega}")]
        public async Task<IEnumerable<GET_CitatorioRecordatorioViewModel>> ListarCitatoriosModuloFiltros([FromRoute] string modulo, DateTime fechai, DateTime fechaf, string StatusEntrega)
        {
            //MODIFICAR DISTRITO
            var Tabla = await _context.CitatorioRecordatorios
                                    .Include(a => a.Sesion)
                                    .Where(a => a.un_Modulo == modulo)
                                    .Where(a => a.FechaHoraCita >= fechai)
                                    .Where(a => a.FechaHoraCita <= fechaf)
                                    .Where(a => a.StatusEntrega == StatusEntrega)
                                    .ToListAsync();


            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_CitatorioRecordatorioViewModel>();
            }
            else
            {

                return Tabla.Select(a => new GET_CitatorioRecordatorioViewModel
                {
                    IdRCitatoriosRecordatorios = a.IdCitatorioRecordatorio,
                    SesionId = a.SesionId,
                    EnvioId = a.Sesion.EnvioId,
                    NoSesion = a.Sesion.NoSesion,
                    NoExpediente = a.NoExpediente,
                    FechaSys = a.FechaSys,
                    FechaHoraCita = a.FechaHoraCita,
                    Duracion = a.Duracion,
                    LugarCita = a.LugarCita,
                    dirigidoa_Nombre = a.dirigidoa_Nombre,
                    dirigidoa_Direccion = a.dirigidoa_Direccion,
                    dirigidoa_Telefono = a.dirigidoa_Telefono,
                    solicitadoPor = a.solicitadoPor,
                    solicitadoPor_Telefono = a.solicitadoPor_Telefono,
                    Textooficio = a.Textooficio,
                    uf_Distrito = a.uf_Distrito,
                    uf_DirSubProc = a.uf_DirSubProc,
                    uf_Agencia = a.uf_Agencia,
                    uf_Modulo = a.uf_Modulo,
                    uf_Nombre = a.uf_Nombre,
                    uf_Puesto = a.uf_Puesto,
                    NoCitatorio = a.NoCitatorio,
                    StatusAsistencia = a.StatusAsistencia,
                    StatusEntrega = a.StatusEntrega,
                    ContadorSMSS = a.ContadorSMSS,
                    ContadorSMSR = a.ContadorSMSR,

                });
            }
        }
        // POST: api/CitatorioRecordatorios/CrearCR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearCR([FromBody] POST_CrearCitatorioRecordatorioViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Tabla = await _context.CitatorioRecordatorios
                            .Include(a => a.Sesion)
                            .Where(a => a.Sesion.ModuloServicioId == model.ModuloServicioId)
                            .Where(a => a.FechaHoraCita == model.FechaHoraCita)
                            .FirstOrDefaultAsync();

            if(Tabla == null)
            {

                var FechaHoraSys = DateTime.Now;

                CitatorioRecordatorio rc = new CitatorioRecordatorio
                {
                    SesionId = model.SesionId,
                    NoExpediente = model.NoExpediente,
                    FechaSys = FechaHoraSys,
                    FechaHoraCita = model.FechaHoraCita,
                    Duracion = model.Duracion,
                    LugarCita = model.LugarCita,
                    dirigidoa_Nombre = model.dirigidoa_Nombre,
                    dirigidoa_Direccion = model.dirigidoa_Direccion,
                    dirigidoa_Telefono = model.dirigidoa_Telefono,
                    solicitadoPor = model.solicitadoPor,
                    StatusEntrega = model.StatusEntrega,
                    solicitadoPor_Telefono = model.solicitadoPor_Telefono,
                    Textooficio = model.Textooficio,
                    uf_Distrito = model.uf_Distrito,
                    uf_DirSubProc = model.uf_DirSubProc,
                    uf_Agencia = model.uf_Agencia,
                    uf_Modulo = model.uf_Modulo,
                    uf_Nombre = model.uf_Nombre,
                    uf_Puesto = model.uf_Puesto,
                    un_Modulo = model.un_Modulo,
                    un_Nombre = model.un_Nombre,
                    un_Puesto = model.un_Puesto,





                    NoCitatorio = model.NoCitatorio,
                    ContadorSMSR = 0,
                    ContadorSMSS = 0,

                };

                _context.CitatorioRecordatorios.Add(rc);


                try
                {
                    await _context.SaveChangesAsync();
                    return Ok();
                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    return BadRequest();
                }

            }
            else
            {
                if(Tabla.NoExpediente == model.NoExpediente)
                {
                    var FechaHoraSys = DateTime.Now;

                    CitatorioRecordatorio rc = new CitatorioRecordatorio
                    {
                        SesionId = model.SesionId,
                        NoExpediente = model.NoExpediente,
                        FechaSys = FechaHoraSys,
                        FechaHoraCita = model.FechaHoraCita,
                        Duracion = model.Duracion,
                        LugarCita = model.LugarCita,
                        dirigidoa_Nombre = model.dirigidoa_Nombre,
                        dirigidoa_Direccion = model.dirigidoa_Direccion,
                        dirigidoa_Telefono = model.dirigidoa_Telefono,
                        solicitadoPor = model.solicitadoPor,
                        StatusEntrega = model.StatusEntrega,
                        solicitadoPor_Telefono = model.solicitadoPor_Telefono,
                        Textooficio = model.Textooficio,
                        uf_Distrito = model.uf_Distrito,
                        uf_DirSubProc = model.uf_DirSubProc,
                        uf_Agencia = model.uf_Agencia,
                        uf_Modulo = model.uf_Modulo,
                        uf_Nombre = model.uf_Nombre,
                        uf_Puesto = model.uf_Puesto,
                        un_Modulo = model.un_Modulo,
                        un_Nombre = model.un_Nombre,
                        un_Puesto = model.un_Puesto,





                        NoCitatorio = model.NoCitatorio,
                        ContadorSMSR = 0,
                        ContadorSMSS = 0,

                    };

                    _context.CitatorioRecordatorios.Add(rc);


                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                    catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                    {
                        return BadRequest();
                    }

                }
                return Ok(new { fhnd = 1 });
            }
          
        }

       
        // PUT: api/CitatorioRecordatorios/StatusAsistencia
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> StatusAsistencia([FromBody] PUT_StatusAsistenciaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var db = await _context.CitatorioRecordatorios.FirstOrDefaultAsync(a => a.IdCitatorioRecordatorio == model.IdRCitatoriosRecordatorios);

            if (db == null)
            {
                return NotFound();
            }

            db.StatusAsistencia = model.StatusAsistencia; 


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }

            return Ok();
        }
        // PUT: api/CitatorioRecordatorios/StatusEntrega
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> StatusEntrega([FromBody] PUT_StatusEntregaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var db = await _context.CitatorioRecordatorios.FirstOrDefaultAsync(a => a.IdCitatorioRecordatorio == model.IdRCitatoriosRecordatorios);

            if (db == null)
            {
                return NotFound();
            }

            db.StatusEntrega = model.StatusEntrega;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }

            return Ok();
        }


        // PUT: api/CitatorioRecordatorios/Notificador
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Notificador([FromBody] PUT_NotificadorViewModel model)
        
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var db = await _context.CitatorioRecordatorios 
                            .Where(a => a.SesionId == model.IdSesion).ToListAsync();
                
            if (db == null)
            {
                return NotFound();
            }

            foreach (var item in db)
            {
                item.un_Modulo = model.un_Modulo; 
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }

            return Ok();
        }



        // POST: api/CitatorioRecordatorios/auth
        [HttpPost("[action]")]
        /*public IActionResult auth()
        {
             
            var api_key = "acf97a1ac34f3725efbc0c6192476ad89560aa77";
            var request = (HttpWebRequest)WebRequest.Create("https://api.smsmasivos.com.mx/auth");
            var postData = $"apikey={api_key}";
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return Content(responseString);

            }
            catch (WebException ex)
            {
                var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                return Content(resp); // Ejemplo, respuesta procesada en el front-end
            }

        }*/
        public async Task<IActionResult> Auth()
        {
            var api_key = "acf97a1ac34f3725efbc0c6192476ad89560aa77";

            using (var client = new HttpClient())
            {
                var url = "https://api.smsmasivos.com.mx/auth";

                var postData = new StringContent($"apikey={api_key}", Encoding.ASCII, "application/x-www-form-urlencoded");

                try
                {
                    var response = await client.PostAsync(url, postData);

                    response.EnsureSuccessStatusCode();

                    var responseString = await response.Content.ReadAsStringAsync();

                    return Content(responseString);
                }
                catch (HttpRequestException ex)
                {
                    return Content($"Error: {ex.Message}");
                }
            }
        }


        // POST: api/CitatorioRecordatorios/send
        [HttpPost("[action]")]
        /*public IActionResult send([FromBody] POST_smsViewModel model)
        {

            if (model.token != "" && model.token != null)
            {
               
                model.c = model.c.Replace(" ", "");
                int country = int.Parse(model.c);


                var request = (HttpWebRequest)WebRequest.Create("https://api.smsmasivos.com.mx/sms/send");
                var postData = $"message={model.texto}&"; 
                postData += $"numbers={model.dest}&"; 
                postData += $"country_code={country}&";

                var data = Encoding.ASCII.GetBytes(postData);
                Console.WriteLine(postData.ToString());
                request.Method = "POST";
                request.Headers["token"] = model.token;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = null;

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    return Content(responseString);

                }
                catch (WebException ex)
                {
                    var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    return Content(resp); // Ejemplo, respuesta procesada en el front-ends
                }

            }

            return Content("Default Server Error 500");
        }*/
        public async Task<IActionResult> Send([FromBody] POST_smsViewModel model)
        {
            if (!string.IsNullOrEmpty(model.token))
            {
                model.c = model.c.Replace(" ", "");
                int country = int.Parse(model.c);

                using (var client = new HttpClient())
                {
                    var postData = new StringContent(
                        $"message={model.texto}&numbers={model.dest}&country_code={country}",
                        Encoding.ASCII,
                        "application/x-www-form-urlencoded"
                    );

                    client.DefaultRequestHeaders.Add("token", model.token);

                    try
                    {
                        var response = await client.PostAsync("https://api.smsmasivos.com.mx/sms/send", postData);

                        response.EnsureSuccessStatusCode();

                        var responseString = await response.Content.ReadAsStringAsync();

                        return Content(responseString);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Content($"Error: {ex.Message}");
                    }
                }
            }

            return Content("Default Server Error 500");
        }

        // PUT: api/CitatorioRecordatorios/ContadorSMSS
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ContadorSMSS([FromBody] PUT_ContadorSMSViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var db = await _context.CitatorioRecordatorios.FirstOrDefaultAsync(a => a.IdCitatorioRecordatorio == model.IdRCitatoriosRecordatorios);

            if (db == null)
            {
                return NotFound();
            }

            db.ContadorSMSS = db.ContadorSMSS +1 ;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }

            return Ok();
        }
        // PUT: api/CitatorioRecordatorios/ContadorSMSR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ContadorSMSR([FromBody] PUT_ContadorSMSViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var db = await _context.CitatorioRecordatorios.FirstOrDefaultAsync(a => a.IdCitatorioRecordatorio == model.IdRCitatoriosRecordatorios);

            if (db == null)
            {
                return NotFound();
            }

            db.ContadorSMSR = db.ContadorSMSR + 1;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }

            return Ok();
        }

    }
}