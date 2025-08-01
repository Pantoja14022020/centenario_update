using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_JR.RAsignacionEnvios;
using SIIGPP.Entidades.M_JR.RCitatorioRecordatorio;
using SIIGPP.Entidades.M_JR.RSesion;
using SIIGPP.JR.Models.RSesion;
using SIIGPP.Entidades.M_Cat.DDerivacion;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using SIIGPP.Entidades.M_JR.REnvio;

namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public SesionsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        //GET: api/Sesions/ListarPrimeraSesion
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{envioId}")]
        public async Task<IEnumerable<GET_PrimeraSesionViewModel>> ListarPrimeraSesion([FromRoute] Guid envioId)
        {
            var primeraSesion = await _context.CitatorioRecordatorios
                                              .Include(a => a.Sesion)
                                              .Where(a => a.Sesion.EnvioId == envioId)
                                              .Where(a => a.Sesion.NoSesion == 1)
                                              .OrderBy(a => a.Sesion.FechaHora)
                                              .FirstOrDefaultAsync();

            if(primeraSesion == null)
            {
                return Enumerable.Empty<GET_PrimeraSesionViewModel>();
            }
            else
            {
                return new List<GET_PrimeraSesionViewModel>
                {
                    new GET_PrimeraSesionViewModel
                    {
                        IdSesion = primeraSesion.Sesion.IdSesion,
                        ModuloServicioId = primeraSesion.Sesion.ModuloServicioId,
                        EnvioId = primeraSesion.Sesion.EnvioId,
                        NoSesion = primeraSesion.Sesion.NoSesion,
                        FechaHoraSys = primeraSesion.Sesion.FechaHoraSys,
                        StatusSesion = primeraSesion.Sesion.StatusSesion,
                        DescripcionSesion = primeraSesion.Sesion.DescripcionSesion,
                        Asunto = primeraSesion.Sesion.Asunto,
                        FechaHora = primeraSesion.Sesion.FechaHora,
                        Solicitates = primeraSesion.Sesion.Solicitates,
                        Reuqeridos = primeraSesion.Sesion.Reuqeridos,
                        uf_Distrito = primeraSesion.Sesion.uf_Distrito,
                        uf_DirSubProc = primeraSesion.Sesion.uf_DirSubProc,
                        uf_Agencia = primeraSesion.Sesion.uf_Agencia,
                        uf_Modulo = primeraSesion.Sesion.uf_Modulo,
                        uf_Nombre = primeraSesion.Sesion.uf_Nombre,
                        uf_Puesto = primeraSesion.Sesion.uf_Puesto,
                        Capturista = primeraSesion.Sesion.Capturista,
                        Notificador = primeraSesion.un_Nombre,
                    }
                };
            }
        }      

        // POST: api/Sesions/CrearSCAconC
        // SE ELIMINA LAS AUTORIZACIONES
        // [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")] 
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearSCAconC([FromBody] POST_CrearSCAcCViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //************************************************************
            // OBTENEMOS EL NO DE SESION
            var nosesion = 1;
            var consultaNosesion = await _context.SesionConjuntos
                            .Include(a => a.Sesion)
                            .Where(a => a.ConjuntoDerivacionesId == model.ConjuntoDerivacionesId)
                            .OrderByDescending(a => a.Sesion.NoSesion)
                            .Take(1)
                            .FirstOrDefaultAsync();

            if (consultaNosesion != null)
            {
                nosesion = consultaNosesion.Sesion.NoSesion + 1;
            }

            //************************************************************
            var FechaHoraSys = DateTime.Now;
            //************************************************************
            // SESIONES
            Sesion addsesion = new Sesion
            {
                ModuloServicioId = model.ModuloServicioId,
                EnvioId = model.EnvioId,
                FechaHoraSys = FechaHoraSys,
                StatusSesion = "En proceso",
                NoSesion = nosesion,
                Solicitates = model.Solicitantes,
                Reuqeridos = model.Requeridos,
                uf_Distrito = model.uf_Distrito,
                uf_DirSubProc = model.uf_DirSubProc,
                uf_Agencia = model.uf_Agencia,
                uf_Modulo = model.uf_Modulo,
                uf_Nombre = model.uf_Nombre,
                uf_Puesto = model.uf_Puesto,
                FechaHora = model.FechaHora,
                Capturista = model.Capturista,
            };

            _context.Sesions.Add(addsesion);

            //************************************************************

            foreach (var cit in model.Citatorios)
            {
                CitatorioRecordatorio rc = new CitatorioRecordatorio
                {
                    SesionId = addsesion.IdSesion,
                    NoExpediente = cit.NoExpediente,
                    FechaSys = FechaHoraSys,
                    FechaHoraCita = cit.FechaHoraCita,
                    Duracion = cit.Duracion,
                    LugarCita = cit.LugarCita,
                    dirigidoa_Nombre = cit.dirigidoa_Nombre,
                    dirigidoa_Direccion = cit.dirigidoa_Direccion,
                    dirigidoa_Telefono = cit.dirigidoa_Telefono,
                    solicitadoPor = cit.solicitadoPor,
                    solicitadoPor_Telefono = cit.solicitadoPor_Telefono,
                    Textooficio = cit.Textooficio,
                    uf_Distrito = cit.uf_Distrito,
                    uf_DirSubProc = cit.uf_DirSubProc,
                    uf_Agencia = cit.uf_Agencia,
                    uf_Modulo = cit.uf_Modulo,
                    uf_Nombre = cit.uf_Nombre,
                    uf_Puesto = cit.uf_Puesto,
                    un_Modulo = cit.un_Modulo,
                    un_Nombre = cit.un_Nombre,
                    un_Puesto = cit.un_Puesto,
                    StatusEntrega = cit.StatusEntrega,
                    NoCitatorio = cit.NoCitatorio,
                    ContadorSMSS = 0,
                    ContadorSMSR = 0,
                };
                _context.CitatorioRecordatorios.Add(rc);
            }

            //************************************************************
                
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest(ex);
            }
            return Ok(new { idSesion = addsesion.IdSesion });
        }


        // POST: api/Sesions/CrearSCAconC
         // [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")] 
         [HttpPost("[action]")]
         public async Task<IActionResult> CrearSesionesRecordatorios([FromBody] SesionRecordatorioIntermediaViewModel model)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             var nosesion = 1;
             var FechaHoraSys = DateTime.Now;
 
             //Se buscan sesiones existentes
             var consultaNosesion = await _context.SesionConjuntos
                            .Include(a => a.Sesion)
                            .Where(a => a.ConjuntoDerivacionesId == model.ConjuntoDerivacionesId)
                            .OrderByDescending(a => a.Sesion.NoSesion)
                            .Take(1)
                            .FirstOrDefaultAsync();
 
             //De no haber sesiones se determina la primera
             if (consultaNosesion != null)
             {
                 nosesion = consultaNosesion.Sesion.NoSesion + 1;
             } 
                 Sesion addsesion = new Sesion
                 {
                     EnvioId = model.EnvioId,
                     ModuloServicioId = model.ModuloServicioId,
                     Solicitates = model.Solicitantes,
                     Reuqeridos = model.Requeridos,
                     uf_Distrito = model.uf_Distrito,
                     uf_DirSubProc = model.uf_DirSubProc,
                     uf_Agencia = model.uf_Agencia,
                     uf_Modulo = model.uf_Modulo,
                     uf_Nombre = model.uf_Nombre,
                     uf_Puesto = model.uf_Puesto,
                     FechaHora = model.FechaHora,
                     Capturista = model.Capturista,
 
                     FechaHoraSys = FechaHoraSys,
                     StatusSesion = "En proceso",
                     NoSesion = nosesion, 
                 };
                 _context.Sesions.Add(addsesion);
 
                 SesionConjunto addsesionc = new SesionConjunto
                 { 
                     SesionId = addsesion.IdSesion,
                     ConjuntoDerivacionesId = model.ConjuntoDerivacionesId, 
                 };
 
                 _context.SesionConjuntos.Add(addsesionc);
 
                 CitatorioRecordatorio rc = new CitatorioRecordatorio
                 {
                     SesionId = addsesion.IdSesion,
                     NoExpediente = model.NoExpediente,
                     FechaSys = FechaHoraSys,
                     FechaHoraCita = model.FechaHoraCita,
                     Duracion = model.Duracion,
                     LugarCita = model.LugarCita,
                     dirigidoa_Nombre = model.dirigidoa_Nombre,
                     dirigidoa_Direccion = model.dirigidoa_Direccion,
                     dirigidoa_Telefono = model.dirigidoa_Telefono,
                     solicitadoPor = model.solicitadoPor,
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
                     StatusEntrega = model.StatusEntrega,
                     NoCitatorio = model.NoCitatorio,
                     ContadorSMSS = 0,
                     ContadorSMSR = 0, 
                 };

                 _context.CitatorioRecordatorios.Add(rc);
             
             try
             {
                 await _context.SaveChangesAsync();
             }
 #pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
             catch (Exception ex)
 #pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
             {
                 var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException.Message, version = "version 1.3" });
                 result.StatusCode = 402;
                 return result;
             } 
             return Ok(new { idSesion = addsesion.IdSesion }); 
        }

        //GET: api/Sesions/ListarSCpC
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{ConjuntoDerivacionId}")]
        public async Task<IEnumerable<GET_SesionConjuntoViewModel>> ListarSCpC([FromRoute] Guid ConjuntoDerivacionId)
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
            if (tabla == null)
            {
                return Enumerable.Empty<GET_SesionConjuntoViewModel>();
            }
            else
            {
                return tabla.Select(a => new GET_SesionConjuntoViewModel
                {
                    IdSC = a.sc.IdSC,
                    SesionId = a.sc.SesionId,
                    ModuloServicioId = a.s.ModuloServicioId,
                    EnvioId = a.s.EnvioId,
                    NoSesion = a.s.NoSesion,
                    FechaHoraSys = a.s.FechaHoraSys,
                    StatusSesion = a.s.StatusSesion,
                    DescripcionSesion = a.s.DescripcionSesion,
                    Asunto = a.s.Asunto,
                    FechaHora = a.s.FechaHora,
                    Solicitates = a.s.Solicitates,
                    Reuqeridos = a.s.Reuqeridos,
                    uf_Distrito = a.s.uf_Distrito,
                    uf_DirSubProc = a.s.uf_DirSubProc,
                    uf_Agencia = a.s.uf_Agencia,
                    uf_Modulo = a.s.uf_Modulo,
                    uf_Nombre = a.s.uf_Nombre,
                    uf_Puesto = a.s.uf_Puesto,
                    FechaHoraCita = a.c.FechaHoraCita,
                });
            }
        }

        // POST: api/Sesions/CrearSesionConjunto
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearSesionConjunto ([FromBody] POST_SesionConjuntoViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SesionConjunto addsesionc = new SesionConjunto
            {

                SesionId = model.SesionId,
                ConjuntoDerivacionesId = model.ConjuntoDerivacionesId,
            };

            _context.SesionConjuntos.Add(addsesionc);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { idSesion = addsesionc.IdSC });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }            
        }

        // DELETE: api/Sesions/EliminarSesion/IdSesion
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpDelete("[action]/{idSesion}")]
        public async Task<IActionResult> EliminarSesion([FromRoute] Guid idSesion)
        {
            var sesion = await _context.Sesions.FirstOrDefaultAsync(a => a.IdSesion == idSesion);

            if(sesion==null)
            {
                return NotFound();
            }
            _context.Sesions.Remove(sesion);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { respuesta = 'd' });
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return Ok(new { respuesta = 'p' });
            }            
        }

        // GET: api/Sesions/ListarConjuntoPorEnvio/
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{envioId}")]
        public async Task<IEnumerable<GET_ConjuntosViewModel1>> ListarConjuntoPorEnvio([FromRoute] Guid envioId)
        {
            var Tabla = await _context.ConjuntoDerivaciones
                                    .Where(a => a.EnvioId == envioId)
                                    .ToListAsync();

            if (Tabla.Count != 0)
            {
                Console.WriteLine("Conjunto");
                return Tabla.Select(a => new GET_ConjuntosViewModel1
                {
                    IdConjuntoDerivaciones = a.IdConjuntoDerivaciones,
                    EnvioId = a.EnvioId,
                    SolicitadosC = a.SolicitadosC,
                    RequeridosC = a.RequeridosC,
                    DelitosC = a.DelitosC,
                    NombreS = a.NombreS,
                    DireccionS = a.DireccionS,
                    TelefonoS = a.TelefonoS,
                    ClasificacionS = a.ClasificacionS,
                    NombreR = a.NombreR,
                    DireccionR = a.DireccionR,
                    TelefonoR = a.TelefonoR,
                    ClasificacionR = a.ClasificacionR,
                    NombreD = a.NombreD,
                    NoOficio = a.NoOficio,
                    ResponsableJR = a.ResponsableJR,
                    NombreSubDirigido = a.NombreSubDirigido,
                    SEC = true
                });
            }
            else
            {
                var Tabla1 = await _context.SolicitanteRequeridos
                            .Include(st => st.Persona)
                            .Where(st => st.EnvioId == envioId)

                .Select(st => new GET_ConjuntosViewModel1
                {
                    IdRSolicitanteRequerido = st.IdRSolicitanteRequerido,
                    IdPersona = st.Persona.IdPersona,
                    NombreCompleto = st.Persona.Nombre + " " + st.Persona.ApellidoMaterno + " " + st.Persona.ApellidoPaterno,
                    Tipo = st.Tipo,
                    Telefono = st.Persona.Telefono1 + ", " + st.Persona.Telefono2,
                    SEC = false
                }).ToListAsync();

                return Tabla1;
            }
        }

        // GET: api/Sesions/ListarDDPorEnvio
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{envioId}")]
        public async Task<IEnumerable<GET_ConjuntosViewModel2>> ListarDDPorEnvio([FromRoute] Guid envioId)
        {
            var Tabla = await _context.DelitosDerivados
                                    .Include(a => a.RDH)
                                    .ThenInclude(r => r.Delito)
                                    .Where(a => a.EnvioId == envioId)
                                    .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_ConjuntosViewModel2>();
            }
            else
            {
                return Tabla.Select(a => new GET_ConjuntosViewModel2
                {
                    IdDelitoDerivado = a.IdDelitoDerivado,
                    DelitoId = a.RDH.DelitoId,
                    EnvioId = a.EnvioId,
                    Nombre = a.RDH.Delito.Nombre,
                });
            }
        }
        
        // GET: api/Sesions/ListarPorEnvio
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{envioId}")]
        public async Task<IEnumerable<GET_SesionViewModel>> ListarPorEnvio([FromRoute] Guid envioId)
        {
            var Tabla = await _context.Sesions
                                    .Where(a => a.EnvioId == envioId)
                                    .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_SesionViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_SesionViewModel
                {
                    IdSesion = a.IdSesion, 
                    ModuloServicioId= a.ModuloServicioId, 
                    EnvioId = a.EnvioId,
                    NoSesion= a.NoSesion,
                    FechaHoraSys = a.FechaHoraSys,
                    StatusSesion = a.StatusSesion,
                    DescripcionSesion = a.DescripcionSesion,
                    Asunto = a.Asunto,
                    FechaHora = a.FechaHora,
                    Solicitates = a.Solicitates,
                    Reuqeridos = a.Reuqeridos,
                    uf_Distrito = a.uf_Distrito,
                    uf_DirSubProc = a.uf_DirSubProc,
                    uf_Agencia = a.uf_Agencia,
                    uf_Modulo = a.uf_Modulo,
                    uf_Nombre = a.uf_Nombre,
                    uf_Puesto = a.uf_Puesto,
                });
            }
        }

        // GET: api/Sesions/ListarPorSesion
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{IdSesion}")]
        public async Task<IEnumerable<GET_SesionViewModel>> ListarPorSesion([FromRoute] Guid IdSesion)
        {
            var Tabla = await _context.Sesions
                                    .Where(a => a.IdSesion == IdSesion)
                                    .ToListAsync();
            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_SesionViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_SesionViewModel
                {
                    IdSesion = a.IdSesion,
                    ModuloServicioId = a.ModuloServicioId,
                    EnvioId = a.EnvioId,
                    NoSesion = a.NoSesion,
                    FechaHoraSys = a.FechaHoraSys,
                    StatusSesion = a.StatusSesion,
                    DescripcionSesion = a.DescripcionSesion,
                    Asunto = a.Asunto,
                    FechaHora = a.FechaHora,
                    Solicitates = a.Solicitates,
                    Reuqeridos = a.Reuqeridos,
                    uf_Distrito = a.uf_Distrito,
                    uf_DirSubProc = a.uf_DirSubProc,
                    uf_Agencia = a.uf_Agencia,
                    uf_Modulo = a.uf_Modulo,
                    uf_Nombre = a.uf_Nombre,
                    uf_Puesto = a.uf_Puesto,
                });
            }
        }

        // GET: api/Sesions/ListarPorConjuntoUltimaSesion
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{ConjuntoDerivacionId}")]
        public async Task<IActionResult> ListarPorConjuntoUltimaSesion([FromRoute] Guid ConjuntoDerivacionId)
        {
            try
            {
                 var tabla = await _context.SesionConjuntos
                                          .Include(s => s.Sesion)
                                          .Where(s => s.ConjuntoDerivacionesId == ConjuntoDerivacionId)
                                          .OrderByDescending(a => a.Sesion.NoSesion)
                                          .FirstOrDefaultAsync();

                if(tabla == null)
                {
                    return Ok(new { noHaySesion = 1 });
                }
                return Ok(new GET_SesionConjuntoViewModel
                {
                    SesionId = tabla.SesionId,
                    ModuloServicioId = tabla.Sesion.ModuloServicioId,
                    EnvioId = tabla.Sesion.EnvioId,
                    NoSesion = tabla.Sesion.NoSesion,
                    FechaHoraSys = tabla.Sesion.FechaHoraSys,
                    StatusSesion = tabla.Sesion.StatusSesion,
                    DescripcionSesion = tabla.Sesion.DescripcionSesion,
                    Asunto = tabla.Sesion.Asunto,
                    FechaHora = tabla.Sesion.FechaHora,
                    Solicitates = tabla.Sesion.Solicitates,
                    Reuqeridos = tabla.Sesion.Reuqeridos,
                    uf_Distrito = tabla.Sesion.uf_Distrito,
                    uf_DirSubProc = tabla.Sesion.uf_DirSubProc,
                    uf_Agencia = tabla.Sesion.uf_Agencia,
                    uf_Modulo = tabla.Sesion.uf_Modulo,
                    uf_Nombre = tabla.Sesion.uf_Nombre,
                    uf_Puesto = tabla.Sesion.uf_Puesto,
                });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/Sesions/ListarPorEnvioUltimaSeseion
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{envioId}")]
        public async Task<IActionResult> ListarPorEnvioUltimaSeseion([FromRoute] Guid envioId)
        {
            var Tabla = await _context.Sesions
                                 .Where(a => a.EnvioId == envioId)
                                 .OrderByDescending(a => a.NoSesion)
                                .FirstOrDefaultAsync();

            if (Tabla == null)
            {

                return Ok(new { noHaySesion = 1 });
            }
            return Ok(new GET_SesionViewModel
            {
                IdSesion = Tabla.IdSesion,
                ModuloServicioId = Tabla.ModuloServicioId,
                EnvioId = Tabla.EnvioId,
                NoSesion = Tabla.NoSesion,
                FechaHoraSys = Tabla.FechaHoraSys,
                StatusSesion = Tabla.StatusSesion,
                DescripcionSesion = Tabla.DescripcionSesion,
                Asunto = Tabla.Asunto,
                FechaHora = Tabla.FechaHora,
                Solicitates = Tabla.Solicitates,
                Reuqeridos = Tabla.Reuqeridos,
                uf_Distrito = Tabla.uf_Distrito,
                uf_DirSubProc = Tabla.uf_DirSubProc,
                uf_Agencia = Tabla.uf_Agencia,
                uf_Modulo = Tabla.uf_Modulo,
                uf_Nombre = Tabla.uf_Nombre,
                uf_Puesto = Tabla.uf_Puesto,
            });
        }

        // GET: api/Sesions/ObtenerUltimaSeseion
        // [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{envioId}/{conjuntoid}")]
        public async Task<IActionResult> ObtenerUltimaSeseion([FromRoute] Guid envioId, Guid conjuntoid)
        {

            //Evaluamos la existencia de conjunto en la tabla intermedia de sesiones y conjuntos
            var existenciaConjunto = await _context.SesionConjuntos
                                    .Where(a => a.ConjuntoDerivacionesId == conjuntoid)
                                    .ToListAsync();

            //Evaluamos el total de sesiones que deberia tener por el hecho de ser parte de un conjunto
            if (existenciaConjunto.Count > 0)
            {
                var Tabla = await _context.SesionConjuntos
                            .Include(a => a.Sesion)
                            .Where(a => a.ConjuntoDerivacionesId == conjuntoid)
                            .OrderByDescending(a => a.Sesion.NoSesion)
                            .FirstOrDefaultAsync();

                return Ok(new GET_SesionViewModel
                {
                    IdSesion = Tabla.Sesion.IdSesion,
                    ModuloServicioId = Tabla.Sesion.ModuloServicioId,
                    EnvioId = Tabla.Sesion.EnvioId,
                    NoSesion = Tabla.Sesion.NoSesion,
                    FechaHoraSys = Tabla.Sesion.FechaHoraSys,
                    StatusSesion = Tabla.Sesion.StatusSesion,
                    DescripcionSesion = Tabla.Sesion.DescripcionSesion,
                });
            }

            //En caso de no haber simplemente devolvemos el ultimo valor con respecto del id de encio
            else
            {
                var Tabla = await _context.Sesions
                            .Where(a => a.EnvioId == envioId)
                            .OrderByDescending(a => a.NoSesion)
                            .FirstOrDefaultAsync();

                if (Tabla == null)
                {

                    return Ok(new { notSesion = 1 });
                }
                return Ok(new GET_SesionViewModel
                {
                    IdSesion = Tabla.IdSesion,
                    ModuloServicioId = Tabla.ModuloServicioId,
                    EnvioId = Tabla.EnvioId,
                    NoSesion = Tabla.NoSesion,
                    FechaHoraSys = Tabla.FechaHoraSys,
                    StatusSesion = Tabla.StatusSesion,
                    DescripcionSesion = Tabla.DescripcionSesion,
                });
            }               
        }

        // PATCH: api/Sesions/ActualizarStatusSesion 
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPatch("[action]")]
        public async Task<IActionResult> ActualizarStatusSesion([FromBody] PATCH_SesionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var db = await _context.Sesions.FirstOrDefaultAsync(a => a.IdSesion == model.IdSesion);

            if (db == null)
            {
                return NotFound();
            }
            db.StatusSesion = model.StatusSesion;
            db.DescripcionSesion = model.DescripcionSesion;
            db.Asunto = model.Asunto;
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

        // PUT: api/Sesions/ActualizarSDS 
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarSDS([FromBody] PUT_SesionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
    
            var db = await _context.Sesions.FirstOrDefaultAsync(a => a.IdSesion == model.IdSesion);

            if (db == null)
            {
                return NotFound();
            }
                var fechahora = DateTime.Now;
                db.StatusSesion = model.StatusSesion;
                db.DescripcionSesion = model.DescripcionSesion;
                db.Asunto = model.Asunto;
                db.FechaHora = fechahora; 
                db.Solicitates = model.Solicitates;
                db.Reuqeridos = model.Reuqeridos;
                db.uf_Distrito = model.uf_Distrito;
                db.uf_DirSubProc = model.uf_DirSubProc;
                db.uf_Agencia = model.uf_Agencia;
                db.uf_Modulo = model.uf_Modulo;
                db.uf_Nombre = model.uf_Nombre;
                db.uf_Puesto = model.uf_Puesto;
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

        // PUT: api/Sesions/ActualizarReasignacion
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarReasignacion([FromBody] PUT_ReasignacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var db = await _context.Sesions.FirstOrDefaultAsync(a => a.IdSesion == model.IdSesion);

            if (db == null)
            {
                return NotFound();
            }
           
            db.ModuloServicioId = model.ModuloServicioId;

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

        // POST: api/Sesions/CrearSconC
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearSconC([FromBody] POST_SesionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //************************************************************
            // OBTENEMOS EL NO DE SESION
            var nosesion = 1;
            var consultaNosesion = await _context.SesionConjuntos
                           .Include(a => a.Sesion)
                           .Where(a => a.ConjuntoDerivacionesId == model.ConjuntoDerivacionesId)
                           .OrderByDescending(a => a.Sesion.NoSesion)
                           .Take(1)
                           .FirstOrDefaultAsync();

            if (consultaNosesion != null)
            {
                nosesion = consultaNosesion.Sesion.NoSesion + 1;
            }

            //************************************************************
            var FechaHoraSys = DateTime.Now;

            Sesion addsesion = new Sesion
            {
                ModuloServicioId = model.ModuloServicioId,
                EnvioId = model.EnvioId,
                FechaHoraSys = FechaHoraSys,
                StatusSesion = "En proceso",
                NoSesion = nosesion,
                Solicitates = model.Solicitantes,
                Reuqeridos = model.Requeridos,
                uf_Distrito = model.uf_Distrito,
                uf_DirSubProc = model.uf_DirSubProc,
                uf_Agencia = model.uf_Agencia,
                uf_Modulo = model.uf_Modulo,
                uf_Nombre = model.uf_Nombre,
                uf_Puesto = model.uf_Puesto,
                Capturista = model.Capturista,
                FechaHora = model.FechaHora,
            };

            _context.Sesions.Add(addsesion);

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
            return Ok(new { idSesion = addsesion.IdSesion });
        }

        // POST: api/Sesions/Crear
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] POST_SesionViewModel model)
        {          
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //************************************************************
                // OBTENEMOS EL NO DE SESION
                var nosesion = 1;
                var consultaNosesion = await _context.Sesions
                               .Where(a => a.EnvioId == model.EnvioId) 
                               .OrderByDescending(a => a.NoSesion)
                               .Take(1)
                               .FirstOrDefaultAsync();

                if (consultaNosesion != null)
                {
                    nosesion = consultaNosesion.NoSesion + 1;
                }
               
                //************************************************************
                var FechaHoraSys = DateTime.Now;

                Sesion addsesion = new Sesion
                {                    
                    ModuloServicioId = model.ModuloServicioId,
                    EnvioId = model.EnvioId,
                    FechaHoraSys = FechaHoraSys,
                    StatusSesion ="En proceso",
                    NoSesion = nosesion,
                    Solicitates = model.Solicitantes,
                    Reuqeridos = model.Requeridos
                };

                _context.Sesions.Add(addsesion);

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
            return Ok(new {  idSesion = addsesion.IdSesion });
        }

        // POST: api/Sesions/CrearSCA
        // SE ELIMINA LAS AUTORIZACIONES
        // [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")] 
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearSCA([FromBody] POST_CrearSCAViewModel model)
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

            if (Tabla == null)
            {
                //************************************************************
                // OBTENEMOS EL NO DE SESION
                var nosesion = 1;
                var consultaNosesion = await _context.Sesions
                               .Where(a => a.EnvioId == model.EnvioId)
                               .OrderByDescending(a => a.NoSesion)
                               .Take(1)
                               .FirstOrDefaultAsync();

                if (consultaNosesion != null)
                {
                    nosesion = consultaNosesion.NoSesion + 1;
                }

                //************************************************************
                var FechaHoraSys = DateTime.Now;
                //************************************************************
                // SESIONES
                Sesion addsesion = new Sesion
                {
                    ModuloServicioId = model.ModuloServicioId,
                    EnvioId = model.EnvioId,
                    FechaHoraSys = FechaHoraSys,
                    NoSesion = nosesion,
                    StatusSesion="En proceso",
                };

                _context.Sesions.Add(addsesion);

                //************************************************************
                foreach (var cit in model.Citatorios)
                {
                    CitatorioRecordatorio rc = new CitatorioRecordatorio
                    {
                        SesionId = addsesion.IdSesion,
                        NoExpediente = cit.NoExpediente,
                        FechaSys = FechaHoraSys,
                        FechaHoraCita = cit.FechaHoraCita,
                        Duracion = cit.Duracion,
                        LugarCita = cit.LugarCita,
                        dirigidoa_Nombre = cit.dirigidoa_Nombre,
                        dirigidoa_Direccion = cit.dirigidoa_Direccion,
                        dirigidoa_Telefono = cit.dirigidoa_Telefono,
                        solicitadoPor = cit.solicitadoPor,
                        solicitadoPor_Telefono = cit.solicitadoPor_Telefono,
                        Textooficio = cit.Textooficio,
                        uf_Distrito = cit.uf_Distrito,
                        uf_DirSubProc = cit.uf_DirSubProc,
                        uf_Agencia = cit.uf_Agencia,
                        uf_Modulo = cit.uf_Modulo,
                        uf_Nombre = cit.uf_Nombre,
                        uf_Puesto = cit.uf_Puesto,
                        un_Modulo = cit.un_Modulo,
                        un_Nombre = cit.un_Nombre,
                        un_Puesto = cit.un_Puesto,
                        StatusEntrega = cit.StatusEntrega,
                        NoCitatorio = cit.NoCitatorio,
                        ContadorSMSS = 0,
                        ContadorSMSR = 0,

                    };
                    _context.CitatorioRecordatorios.Add(rc);
                }
               
                //************************************************************
                foreach (var det in model.Asignaciones)
                {
                    AsignacionEnvio ae = new AsignacionEnvio
                    {
                        EnvioId = det.EnvioId,
                        ModuloServicioId = det.ModuloServicioId,
                    };
                    _context.AsignacionEnvios.Add(ae);
                }
                //************************************************************
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
                return Ok(new { idSesion = addsesion.IdSesion, fhnd = 0 });
            }
            else
            {
                return Ok(new { fhnd = 1 });
            }
        }

        // POST: api/Sesions/CrearSC
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearSC([FromBody] POST_CrearSCViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //A travez de esta consulta comprueba la exixtencia de una sesion en la hora que lo solicito
            var Tabla = await _context.CitatorioRecordatorios
                                .Include(a => a.Sesion) 
                                .Where(a => a.Sesion.ModuloServicioId== model.ModuloServicioId)
                                .Where(a => a.FechaHoraCita == model.FechaHoraCita)
                                .FirstOrDefaultAsync();

            //En caso de no haber sesion en ese horario, inicia un proceso de guardado de sesion
            if (Tabla == null)
            {
                //Se inicializa la variable para posteriormente obtener el numero maximo de sesion
                var nosesion = 1;

                //Consulta para obtener el numero maximo de sesion                
                var consultaNosesion = await _context.SesionConjuntos
                           .Include(a => a.Sesion)
                           .Where(a => a.ConjuntoDerivacionesId == Guid.Parse(model.conjuntoId))
                           .OrderByDescending(a => a.Sesion.NoSesion)
                           .Take(1)
                           .FirstOrDefaultAsync();

                //Si el numero de sesion es diferente de nulo, es decir, existen sesiones, el valor de la variable inicializada pasara a ser la ultima sesion mas 1       
                if (consultaNosesion != null)
                {
                    nosesion = consultaNosesion.Sesion.NoSesion + 1;
                }
                //No hay else, ya que en caso de si encontrar el valor nulo en la sesion, es decir, no hay sesiones, se mantiene el valor estatico de lña sesion 1

                var FechaHoraSys = DateTime.Now;
                Guid idsesion;

                try
                {
                    // INICIA EL GUARDADOP DE SESIONES
                    Sesion addsesion = new Sesion
                    {

                        ModuloServicioId = model.ModuloServicioId,
                        EnvioId = model.EnvioId,
                        FechaHoraSys = FechaHoraSys,
                        NoSesion = nosesion,
                        StatusSesion = "En proceso",
                        Solicitates = model.solicitadoPor,
                        Reuqeridos = model.dirigidoa_Nombre,


                    };
                    _context.Sesions.Add(addsesion);

                    //Al mismo tiempo se inserta los valores de Citatorio/Recordatorio
                    CitatorioRecordatorio rc = new CitatorioRecordatorio
                    {
                        SesionId = addsesion.IdSesion,
                        NoExpediente = model.NoExpediente,
                        FechaSys = FechaHoraSys,
                        FechaHoraCita = model.FechaHoraCita,
                        Duracion = model.Duracion,
                        LugarCita = model.LugarCita,
                        dirigidoa_Nombre = model.dirigidoa_Nombre,
                        dirigidoa_Direccion = model.dirigidoa_Direccion,
                        dirigidoa_Telefono = model.dirigidoa_Telefono,
                        solicitadoPor = model.solicitadoPor,
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
                        StatusEntrega = model.StatusEntrega,
                        NoCitatorio = model.NoCitatorio,
                        ContadorSMSS = 0,
                        ContadorSMSR = 0,
                    };
                    _context.CitatorioRecordatorios.Add(rc); 

                    //Se guardan los cambios afectuados
                    await _context.SaveChangesAsync();

                    //se guarda el valor de is de sesion para su retorno y uso en la api de SesionesConjunto
                    idsesion = addsesion.IdSesion;
                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                    result.StatusCode = 402;
                    return result;
                }

                //Retornado de valor de id de sesion generada
                return Ok(new { idsesion = idsesion });

                //En caso de haber sesion se retorna una variable esdtatica que en Frontend sera evaluala para enviar un mensaje a usuario
            }
            else
            {
                return Ok(new { fhnd = 1 });
            }
        }

        //GET: api/Sesions/ValidateSessionToA
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{acuerdoRId}")]
        public async Task<IActionResult> ValidateSessionToA([FromRoute] Guid acuerdoRId)
        {
            try
            {
                string vali = @"select top 1 * from JR_SESION js where IdSesion in (select SesionId from JR_SESIONESCONJUNTOS jsc where ConjuntoDerivacionesId = (select ConjuntoDerivacionesId from JR_ACUERDOS_CONJUNTOS jac where AcuerdoReparatorioId = @acuerdo)) ORDER BY (NoSesion) DESC";

                List<SqlParameter> qw = new List<SqlParameter>();
                qw.Add(new SqlParameter("@acuerdo", acuerdoRId));
                var ses = await _context.Sesions.FromSqlRaw(vali, qw.ToArray()).ToListAsync();

                if (ses[0].NoSesion > 1 && ses[0].StatusSesion == "En proceso")
                {
                    return Ok(new { status = true });
                }else
                {
                    return Ok(new { status = false });
                }                                   
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        //GET: api/Sesions/ValidateSessionWA
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{acuerdoRId}")]
        public async Task<IActionResult> ValidateSessionWA([FromRoute] Guid acuerdoRId)
        {
            try
            {
                string vali = @"select * from JR_SESION js where IdSesion in (select SesionId from JR_SESIONESCONJUNTOS jsc where ConjuntoDerivacionesId = (select ConjuntoDerivacionesId from JR_ACUERDOS_CONJUNTOS jac where AcuerdoReparatorioId = @acuerdo)) and StatusSesion = 'Se crea anexo a acuerdo reparatorio' ORDER BY (NoSesion) DESC";

                List<SqlParameter> qw = new List<SqlParameter>();
                qw.Add(new SqlParameter("@acuerdo", acuerdoRId));
                var ses = await _context.Sesions.FromSqlRaw(vali, qw.ToArray()).ToListAsync();

                if(ses.Count() > 0 &&  ses != null)
                {
                    return Ok(new { exists = true, cantidad = ses.Count() });
                }

                return Ok(new { exists = false });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }
    }
}