using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.MedidasProteccion;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.MedidasProteccion;
using SIIGPP.CAT.FilterClass;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidasProteccionController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public MedidasProteccionController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // GET: api/MedidasProteccion/Listar
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<MedidasProteccionViewModel>> Listar([FromRoute]Guid RHechoId)
        {
            var mdp = await _context.Medidasproteccions
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return mdp.Select(a => new MedidasProteccionViewModel
            {
                IdMProteccion = a.IdMProteccion,
                RHechoId = a.RHechoId,
                Victima  = a.Victima,
                Imputado= a.Imputado,
                Lugar = a.Lugar,
                Fechahora = a.Fechahora,
                Agente = a.Agente,
                Nuc = a.Nuc,
                Delito = a.Delito,
                Narrativa = a.Narrativa,
                Domicilio = a.Domicilio,
                Telefono = a.Telefono,
                MedidaProtecion = a.MedidaProtecion,
                Duracion = a.Duracion,
                Institucionejec= a.Institucionejec,
                Agencia= a.Agencia,
                Nomedidas  = a.Nomedidas,
                Destinatarion  = a.Destinatarion,
                Domicilion = a.Domicilion,
                FInicio = a.FInicio,
                FVencimiento  = a.FVencimiento,
                Ampliacion = a.Ampliacion,
                FAmpliacion  = a.FAmpliacion,
                FterminoAm  = a.FterminoAm,
                Ratificacion  = a.Ratificacion,
                Distrito = a.Distrito,
                Subproc  = a.Subproc,
                UAgencia  = a.UAgencia,
                Usuario  = a.Usuario,
                Puesto = a.Puesto,
                Fechasys = a.Fechasys,
                Textofinal = a.Textofinal,
                Textofinal2 = a.Textofinal2,
                Detalleactivo = a.Detalleactivo,
                Textofinaldetalle = a.Textofinaldetalle,
                Modulo =a.Modulo,
                NumeroOficio = a.NumeroOficio,
                NumeroOficioN = a.NumeroOficioN,
                //NUEVAS COLUMNAS DE FCAT018C
                PetiOfiMPBool = (bool)(a.PetiOfiMPBool == null ? false : a.PetiOfiMPBool),
                PetiOfiMPVar = a.PetiOfiMPVar,
                MedidasExtraTF = (bool)(a.MedidasExtraTF == null ? false : a.MedidasExtraTF),
                MedidasExtra = a.MedidasExtra

            });

        }


        // PUT: api/MedidasProteccion/ActualizarAmpliacion
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarAmpliacion([FromBody] ActualizarAViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var mdp = await _context.Medidasproteccions.FirstOrDefaultAsync(a => a.IdMProteccion == model.IdMProteccion);

            if (mdp == null)
            {
                return NotFound();
            }

                mdp.Ampliacion = model.Ampliacion;
                mdp.FAmpliacion = model.FAmpliacion;
                mdp.FterminoAm = model.FterminoAm;
                mdp.Ratificacion = model.Ratificacion;


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

        // PUT: api/MedidasProteccion/ActualizarNotificacion
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarNotificacion([FromBody] ActualizarNViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

       

            var mdp = await _context.Medidasproteccions.FirstOrDefaultAsync(a => a.IdMProteccion == model.IdMProteccion);

            if (mdp == null)
            {
                return NotFound();
            }

            mdp.Destinatarion = model.Destinatarion;
            mdp.Domicilion = model.Domicilion;
            mdp.Textofinaldetalle = model.Textofinaldetalle;
            mdp.Detalleactivo = model.Detalleactivo;
            mdp.NumeroOficioN = model.NumeroOficioN;

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



        // PUT: api/MedidasProteccion/Actualizar
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var mdp = await _context.Medidasproteccions.FirstOrDefaultAsync(a => a.IdMProteccion == model.IdMProteccion);

            if (mdp == null)
            {
                return NotFound();
            }

            mdp.Victima = model.Victima;
            mdp.Imputado = model.Imputado;
            mdp.Delito = model.Delito;
            mdp.Domicilio = model.Domicilio;
            mdp.Telefono = model.Telefono;
            mdp.MedidaProtecion = model.MedidaProtecion;
            mdp.Duracion = model.Duracion;
            mdp.Institucionejec = model.Institucionejec;
            mdp.Nomedidas = model.Nomedidas;
            mdp.FInicio = model.FInicio;
            mdp.FVencimiento = model.FVencimiento;
            mdp.Textofinal = model.Textofinal;
            mdp.Textofinal2 = model.Textofinal2;
            //NUEVAS COLUMNAS DE FCAT018C
            mdp.PetiOfiMPBool = model.PetiOfiMPBool;
            mdp.PetiOfiMPVar = model.PetiOfiMPVar;
            mdp.MedidasExtraTF = model.MedidasExtraTF;
            mdp.MedidasExtra = model.MedidasExtra;

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


        // POST: api/MedidasProteccion/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            Medidasproteccion mdp = new Medidasproteccion
            {
                RHechoId = model.RHechoId,
                Victima = model.Victima,
                Imputado = model.Imputado,
                Lugar = model.Lugar,
                Fechahora = model.Fechahora,
                Agente = model.Agente,
                Nuc = model.Nuc,
                Delito = model.Delito,
                Narrativa = model.Narrativa,
                Domicilio = model.Domicilio,
                Telefono = model.Telefono,
                MedidaProtecion = model.MedidaProtecion,
                Duracion = model.Duracion,
                Institucionejec = model.Institucionejec,
                Agencia = model.Agencia,
                Nomedidas = model.Nomedidas,
                Destinatarion = model.Destinatarion,
                Domicilion = model.Domicilion,
                FInicio = model.FInicio,
                FVencimiento = model.FVencimiento,
                Ampliacion = model.Ampliacion,
                FAmpliacion = model.FAmpliacion,
                FterminoAm = model.FterminoAm,
                Ratificacion = model.Ratificacion,
                Distrito = model.Distrito,
                Subproc = model.Subproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                Puesto = model.Puesto,
                Fechasys = fecha,
                Textofinal = model.Textofinal,
                Textofinal2 = model.Textofinal2,
                Textofinaldetalle = model.Textofinaldetalle,
                Detalleactivo = model.Detalleactivo,
                Modulo = model.Modulo,
                NumeroOficio = model.NumeroOficio,
                NumeroOficioN = model.NumeroOficioN,
                //NUEVAS COLUMNAS DE FCAT018C
                PetiOfiMPBool = model.PetiOfiMPBool,
                PetiOfiMPVar = model.PetiOfiMPVar,
                MedidasExtraTF = model.MedidasExtraTF,
                MedidasExtra = model.MedidasExtra
                
            };

            _context.Medidasproteccions.Add(mdp);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.2" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new { idmedidaprotecion = mdp.IdMProteccion });
        }

        // GET: api/MedidasProteccion/ListarNoMedidas
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{MedidaprotecionId}")]
        public async Task<IEnumerable<NoMedidasProteccionViewModel>> ListarNoMedidas([FromRoute] Guid MedidaprotecionId)
        {
            var mdp = await _context.NoMedidasProteccions
                .Where(a => a.MedidasproteccionId == MedidaprotecionId)
                .ToListAsync();

            return mdp.Select(a => new NoMedidasProteccionViewModel
            {
                IdNoMedidasProteccion = a.IdNoMedidasProteccion,
                MedidasproteccionId = a.MedidasproteccionId,
                Clave = a.Clave,
                Descripcion = a.Descripcion

            });

        }

        // POST: api/MedidasProteccion/CrearNomedida
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearNomedida([FromBody] CrearNoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NoMedidasProteccion mdp = new NoMedidasProteccion
            {
                Clave = model.Clave,
                Descripcion = model.Descripcion,
                MedidasproteccionId = model.MedidasproteccionId

            };

            _context.NoMedidasProteccions.Add(mdp);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok();
        }


        // DELETE: api/MedidasProteccion/Eliminar
        [Authorize(Roles = " Administrador,AMPO-IL")]
        [HttpDelete("[action]/{medidasproteccionId}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid medidasproteccionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var rpu = await _context.NoMedidasProteccions
                           .Where(a => a.MedidasproteccionId == medidasproteccionId)
                           .ToListAsync();



            rpu.Select(u => new NoMedidasProteccionViewModel
            {
                IdNoMedidasProteccion = u.IdNoMedidasProteccion,
                Clave = u.Clave,
                Descripcion = u.Descripcion,
                MedidasproteccionId = u.MedidasproteccionId
            });

            foreach (var encuentra in rpu)
            {
                var up = await _context.NoMedidasProteccions.FindAsync(encuentra.IdNoMedidasProteccion);

                if (up == null)
                {
                    return NotFound();
                }

                _context.NoMedidasProteccions.Remove(up);

                try
                {
                    await _context.SaveChangesAsync();
                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    return BadRequest();
                }

            }

            return Ok();
        }



        // GET: api/MedidasProteccion/TotalEstadistica
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{MedidasProteccionEstadistica}")]
        public async Task<IEnumerable<TotalMedidasEstadisticaViewModel>> TotalEstadistica([FromQuery] MedidasProteccionEstadistica MedidasProteccionEstadistica)
        {
            var mc = await _context.NoMedidasProteccions
                            .Where(a => MedidasProteccionEstadistica.DatosGenerales.Distritoact ? a.Medidasproteccion.RHecho.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == MedidasProteccionEstadistica.DatosGenerales.Distrito : 1 == 1)
                            .Where(a => MedidasProteccionEstadistica.DatosGenerales.Dspact ? a.Medidasproteccion.RHecho.ModuloServicio.Agencia.DSP.IdDSP == MedidasProteccionEstadistica.DatosGenerales.Dsp : 1 == 1)
                            .Where(a => MedidasProteccionEstadistica.DatosGenerales.Agenciaact ? a.Medidasproteccion.RHecho.ModuloServicio.Agencia.IdAgencia == MedidasProteccionEstadistica.DatosGenerales.Agencia : 1 == 1)
                            .Where(a => a.Medidasproteccion.RHecho.FechaElevaNuc2 >= MedidasProteccionEstadistica.DatosGenerales.Fechadesde)
                            .Where(a => a.Medidasproteccion.RHecho.FechaElevaNuc2 <= MedidasProteccionEstadistica.DatosGenerales.Fechahasta)
                            .GroupBy(v => v.Descripcion)
                            .Select(x => new TotalMedidasEstadisticaViewModel
                            {
                                Tipo = x.Key,
                                Cantidad = x.Count()
                            })
                            .ToListAsync();

            return mc;
           
        }
        // GET: api/MedidasProteccion/Eliminar
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
                var consultaMProteccion = await _context.Medidasproteccions.Where(a => a.IdMProteccion == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaMProteccion == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningúna medida de protección con la información enviada" });
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
                            MovimientoId = new Guid("c64fd7f3-deb0-432f-ae48-71c08334e6c1") // ESTE DATO HAY QUE BUSCARLO EN LA TABLA C_TIPO_MOV DE LA BD CENTENARIO_LOG
                        };

                        ctx.Add(laRegistro);
                        LogMedidasproteccion mproteccion = new LogMedidasproteccion
                        {
                            LogAdmonId = gLog,
                            IdMProteccion = consultaMProteccion.IdMProteccion,
                            RHechoId = consultaMProteccion.RHechoId,
                            Victima = consultaMProteccion.Victima,
                            Fechahora = consultaMProteccion.Fechahora,
                            Imputado = consultaMProteccion.Imputado,
                            Agente = consultaMProteccion.Agente,
                            Nuc = consultaMProteccion.Nuc,
                            Delito = consultaMProteccion.Delito,
                            Narrativa = consultaMProteccion.Narrativa,
                            Domicilio = consultaMProteccion.Domicilio,
                            Telefono = consultaMProteccion.Telefono,
                            MedidaProtecion = consultaMProteccion.MedidaProtecion,
                            Duracion = consultaMProteccion.Duracion,
                            Institucionejec = consultaMProteccion.Institucionejec,
                            Agencia = consultaMProteccion.Agencia,
                            Nomedidas = consultaMProteccion.Nomedidas,
                            Destinatarion = consultaMProteccion.Destinatarion,
                            Domicilion = consultaMProteccion.Domicilion,
                            FInicio = consultaMProteccion.FInicio,
                            FVencimiento = consultaMProteccion.FVencimiento,
                            Ampliacion = consultaMProteccion.Ampliacion,
                            FAmpliacion = consultaMProteccion.FAmpliacion,
                            FterminoAm = consultaMProteccion.FterminoAm,
                            Ratificacion = consultaMProteccion.Ratificacion,
                            Distrito = consultaMProteccion.Distrito,
                            Subproc = consultaMProteccion.Subproc,
                            UAgencia = consultaMProteccion.UAgencia,
                            Usuario = consultaMProteccion.Usuario,
                            Puesto = consultaMProteccion.Puesto,
                            Modulo = consultaMProteccion.Modulo,
                            Fechasys = consultaMProteccion.Fechasys,
                            Textofinal = consultaMProteccion.Textofinal,
                            Textofinal2 = consultaMProteccion.Textofinal2,
                            Detalleactivo = consultaMProteccion.Detalleactivo,
                            Textofinaldetalle = consultaMProteccion.Textofinaldetalle,
                            NumeroOficio = consultaMProteccion.NumeroOficio,
                            NumeroOficioN = consultaMProteccion.NumeroOficioN
                        };
                        ctx.Add(mproteccion);
                        _context.Remove(consultaMProteccion);

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
            return Ok(new { res = "success", men = "Medida de protección eliminada Correctamente" });
        }



    }
}
