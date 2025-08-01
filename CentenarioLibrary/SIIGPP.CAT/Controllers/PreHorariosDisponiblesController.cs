using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.PRegistro;
using SIIGPP.CAT.Models.RDHechos;
using SIIGPP.CAT.Models.Registro;

using SIIGPP.Datos;

using SIIGPP.Entidades.M_Cat.PRegistro;
using SIIGPP.Entidades.M_Cat.PAtencion;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using SIIGPP.Entidades.M_Cat.PCitas;


namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreHorariosDisponiblesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;
        public PreHorariosDisponiblesController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public IEnumerable<PreHorarioDisponible> PreRegistros()
        {
            return _context.PreHorariosDisponibles;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearHoarioAgenciaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PreHorarioDisponible insertarHorarioDis = new PreHorarioDisponible
            {
                horaInicio = model.horaInicio,
                horaFinal= model.horaFinal,
                AgenciaId=model.AgenciaId,
                densidadPorHora=model.densidadPorHora
            };
            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoId.ToString().ToUpper())).Options;
            using (var ctx = new DbContextSIIGPP(options))
            {
                try
                {
                    ctx.PreHorariosDisponibles.Add(insertarHorarioDis);
                    await ctx.SaveChangesAsync();
                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                    result.StatusCode = 402;
                    return result;
                }

                return Ok(new { idhorario = insertarHorarioDis.idHorarioDisponible });
            }



        }
        [HttpPost("[action]")]
        public async Task<IEnumerable<ListaCitasDiaViewModel>> ListarPorDia([FromBody] DiaAgenciaViewModel model)
        {
            //LISTA DE LAS CITAS DISPONIBLES POR DIA, INICIALIZADA COMO VACIA
            List<ListaCitasDiaViewModel> citas = new List<ListaCitasDiaViewModel>();

            try
            {
                // BUSCAR EL HORARIO DE DISPONIBILIDAD 
                var tiempo = await _context.PreHorariosDisponibles.FirstOrDefaultAsync(a => a.AgenciaId == model.IdAgencia);
                var horaCita = tiempo.horaInicio;
                TimeSpan sumar = new TimeSpan(1, 0, 0);

                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoId.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {


                    var citasPrevias = await ctx.PreCitas
                                                         .GroupBy(a => new { a.fecha, a.Hora })
                                                         .Select(a => new ListaCitasDiaViewModel { IdAgencia = model.IdAgencia, hora = a.Key.Hora, fecha = a.Key.fecha, espacios = a.Count() })
                                                         .Where(a => a.IdAgencia == model.IdAgencia).Where(a => a.fecha == model.fecha)
                                                         .ToListAsync();

                    int posicionCita = 0,espacios;
                    while (horaCita < tiempo.horaFinal)
                    {

                       
                        
                        if (citasPrevias.Count > 0 && citasPrevias[posicionCita].hora == horaCita)
                        {
                            espacios = tiempo.densidadPorHora - citasPrevias[posicionCita].espacios;
                            posicionCita++;
                        }
                        else
                        {
                            espacios = tiempo.densidadPorHora;
                        }
                        var espacio = new ListaCitasDiaViewModel { IdAgencia = model.IdAgencia, hora= horaCita, fecha =model.fecha, espacios = espacios };
                        citas.Add(espacio);

                        var newHora = horaCita.Add(sumar);
                        horaCita = newHora;
                    }
                }
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                //return BadRequest();
            }
            return citas;

        }

        }
    }
