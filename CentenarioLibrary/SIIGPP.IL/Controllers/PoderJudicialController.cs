using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using SIIGPP.Datos.M_IL.PJudicial;
using SIIGPP.Entidades.M_IL.PJudicial;
using SIIGPP.IL.Models.Agenda;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SIIGPP.IL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoderJudicialController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IMailService _mailService;
        private IConfiguration _configuration;
    public PoderJudicialController(DbContextSIIGPP context, IConfiguration configuration,IMailService mailService)
        {
            _context = context;
            _configuration = configuration;
            _mailService = mailService;
        }

        //GET: api/PoderJudicial
        [HttpGet]
        public IEnumerable<PoderJudicial> PoderJudicial()
        {
            return _context.PoderJudicial;
        }

        //POST: api/PoderJudicial
        [HttpPost]
        public async Task<ActionResult<Guid>> Post(PoderJudicial poderJudicial)
        {
            _context.Add(poderJudicial);
            await _context.SaveChangesAsync();
            return poderJudicial.IdSolicitud;
        }

        //Este es solo un fake para poder hacerlo ver funconar EN JUNTAS 
        //POST: api/PoderJudicial/ListarAgenda
        [HttpGet("[action]/{NUC}")]
        public async Task<IActionResult> ListarAgenda([FromRoute]String NUC)
        {
            try
            {
                var actualizarSolilitud = await _context.Agendas
                                                            .Join(_context.Usuarios, agendas => agendas.Usuario, usuarios => usuarios.nombre, (agendas, usuarios) => new { agendas, usuarios })
                                                            .Where(a => a.agendas.Nuc == NUC)
                                                            .Take(1)
                                                            .FirstOrDefaultAsync();

                var falseDateString = "2021-10-28 11:00:00";
                var format = "yyyy-MM-dd hh:mm:ss";
                actualizarSolilitud.agendas.FechaCitacion = DateTime.ParseExact(falseDateString, format, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None); ;//soliciTudActual.FechaAudi;

                actualizarSolilitud.agendas.LugarCitacion = "JUZGADO PENAL ACUSATORIO Y ORAL DE TIZAYUCA";
                actualizarSolilitud.agendas.Tipo2 = "Audiencia intermedia";
                actualizarSolilitud.agendas.DescripcionCitacion = "SALA UNO";
                //ctx.Agendas.Add(actualizarSolilitud.agendas);
                await _context.SaveChangesAsync();

                MailRequest recordatorio = new MailRequest
                {
                    ToEmail = actualizarSolilitud.usuarios.email,
                    Subject = "FECHA PARA AUDIENCIA ACEPTADA NUC: " + actualizarSolilitud.agendas.Nuc,
                    Body = "<p> Buenas Tardes: </p>" +
                    "<p> El presente mensaje se envía para informarle que el PODER JUDICIAL DEL ESTADO DE HIDALGO ha aceptado su Solicitud de audiencia para la carpeta: <b>" + NUC + "</b>" +
                    "<p>Tipo:" + actualizarSolilitud.agendas.Tipo2 + "</p>" +
                    "<p>Lugar:" + actualizarSolilitud.agendas.DescripcionCitacion + "</p>" +
                    "<p>Juzgado:" + actualizarSolilitud.agendas.LugarCitacion + "</p>" +
                    "<p>Fecha:" + falseDateString + "</p>" +
                    "<p>Sin mas por el momento esperamos pueda atender a la cita en el lugar y horario indicado.</p>" +
                    "<p> Este correo ha sido enviado de manera auomática por el Sistema Centenario de la PGJEH, no conteste a esta dirección si necesita la corrección de una fecha, en dado caso contacte con el area correspondiente del PJH"
                };
                await _mailService.SendEmailAsync(recordatorio);

                return Ok();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }
        }

        //POST: api/PoderJudicial
        [HttpGet("[action]")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var formatoFecha = "yyyy-MM-dd HH:mm:ss";
                /* var falseDateString = "2021-10-28 11:00:00";                
                DateTime.ParseExact(falseDateString, format, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None); ;//soliciTudActual.FechaAudi;
                */
                DateTime fechaHoy = System.DateTime.Today;
                DateTime fechaLimite = fechaHoy.AddDays(2).AddHours(23).AddMinutes(59).AddSeconds(59);

                /*String periodo = "?FechaIni=" + fechaHoy.ToString(formatoFecha) + "&FechaFin=" + fechaLimite.ToString(formatoFecha);
                var request = (HttpWebRequest)WebRequest.Create("http://servernet2.pjhidalgo.gob.mx/WebserviceProcu/api/ProcuAudiencias" + periodo);

                result.Method = "GET";
                HttpWebResponse response = null;

                response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                var listadoPJudicial = JsonSerializer.Deserialize<RespuestaPJAPIViewModel>(responseString);*/

                /////

                HttpClient httpClient = new HttpClient();

                String periodo = "?FechaIni=" + fechaHoy.ToString(formatoFecha) + "&FechaFin=" + fechaLimite.ToString(formatoFecha);
                String url = "http://servernet2.pjhidalgo.gob.mx/WebserviceProcu/api/ProcuAudiencias" + periodo;

                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Lanza excepción si la respuesta no fue exitosa

                String result = await response.Content.ReadAsStringAsync();

                var listadoPJudicial = JsonSerializer.Deserialize<RespuestaPJAPIViewModel>(result);

                /////                

                if(listadoPJudicial.ListaResultados != null)
                { 
                foreach (PoderJudicialAPIViewModel soliciTudActual in listadoPJudicial.ListaResultados)
                {
                    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + soliciTudActual.ClavePGJ.ToString().ToUpper())).Options;
                    using (var ctx = new DbContextSIIGPP(options))
                    {

                        var actualizarSolilitud = await ctx.Agendas
                                                            .Join(ctx.Usuarios, agendas => agendas.Usuario, usuarios => usuarios.nombre, (agendas, usuarios) => new { agendas, usuarios })
                                                            .Where(a => a.agendas.Nuc == soliciTudActual.NUC)
                                                            .Where(a => a.agendas.NumeroOficio == soliciTudActual.Causa)
                                                            .Take(1)
                                                            .FirstOrDefaultAsync();
                        if (actualizarSolilitud == null)
                        {
                            //HACER UN CONTROLADOR NUEVO PARA INSERTAR LOS ERRORES, SE NECESITA OTRO CONTROLADOR
                        }
                        else
                        {

                            // REAL
                            actualizarSolilitud.agendas.FechaCitacion = soliciTudActual.FechaAudi;
                            actualizarSolilitud.agendas.LugarCitacion = soliciTudActual.Juzgado;
                            actualizarSolilitud.agendas.Tipo2 = soliciTudActual.TipoAudi;
                            actualizarSolilitud.agendas.DescripcionCitacion = soliciTudActual.SalaDescrip;

                            var falseDateString = "0001-01-01 00:00:00";
                            var format = "yyyy-MM-dd hh:mm:ss";

                            DateTime fecha = System.DateTime.Now;
                            TimeSpan tiempo = fecha - actualizarSolilitud.agendas.Chequeo.GetValueOrDefault(DateTime.ParseExact(falseDateString, format, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None));

                            if ((actualizarSolilitud.agendas.Chequeo == null) || (actualizarSolilitud.agendas.Chequeo != null && tiempo.TotalHours >= 24))
                            {
                                actualizarSolilitud.agendas.Chequeo = fecha;
                                await ctx.SaveChangesAsync();

                                MailRequest recordatorio = new MailRequest
                                {

                                    ToEmail = actualizarSolilitud.usuarios.email,
                                    Subject = "FECHA PARA AUDIENCIA ACEPTADA NUC: " + actualizarSolilitud.agendas.Nuc,
                                    Body = "<p> Buenas Tardes: </p>" +
                                            "<p> El presente mensaje se envía para informarle que el PODER JUDICIAL DEL ESTADO DE HIDALGO ha aceptado su Solicitud de audiencia para la carpeta: <b>" + soliciTudActual.NUC.ToString() + "</b>" +
                                            "<p>Tipo:" + soliciTudActual.TipoAudi + "</p>" +
                                            "<p>Lugar:" + soliciTudActual.SalaDescrip + "</p>" +
                                            "<p>Juzgado:" + soliciTudActual.Juzgado + "</p>" +
                                            "<p>Fecha:" + soliciTudActual.FechaAudi + "</p>" +
                                            "<p>Sin mas por el momento esperamos pueda atender a la cita en el lugar y horario indicado.</p>" +
                                            "<p> Este correo ha sido enviado de manera auomática por el Sistema Centenario de la PGJEH, no conteste a esta dirección si necesita la corrección de una fecha, en dado caso contacte con el area correspondiente del PJH"
                                };
                                await _mailService.SendEmailAsync(recordatorio);
                                //yield return new WaitForSeconds(1.4f);
                            }
                            else
                            {
                                await ctx.SaveChangesAsync();
                            }
                        }
                    }
                }

                return Ok(new {mensaje = "Solicitudes actualizadas" });
                //return Content(responseString);
            }
                else
                {
                    return Ok(new { mensaje = "Sin solicitudes " });
                }
            }
            catch (WebException ex)
            {
                var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                return Content(resp); // Ejemplo, respuesta procesada en el front-end
            }
        }
    }
}