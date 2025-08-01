using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Vehiculos;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.VehiculoImplicito;
using SIIGPP.Entidades.M_Administracion;
using SIIGPP.Entidades.M_JR.RExpediente;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_Cat.DDerivacion;
using SIIGPP.Entidades.M_JR.RSolicitanteRequerido;
using SIIGPP.Entidades.M_JR.RDelitodelitosDerivados;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClonacionJRController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public ClonacionJRController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //AQUI VAN LAS APIS
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        //Comentado por que los roles varian demasiaod de lado de cat para derivacion a JR
        [HttpPost("[action]")]
        // POST: api/ClonacionJR/ClonarExpediente
        public async Task<IActionResult> ClonarExpediente([FromBody] Models.Rac.ClonarViewModelE model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaExpedientes = await _context.Expedientes.Where(x => x.IdExpediente == model.ExpedienteId).Take(1).FirstOrDefaultAsync();



            if (listaExpedientes == null)
            {
                return BadRequest(ModelState);

            }

            
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {

                    var insertarExpediente = await ctx.Expedientes.FirstOrDefaultAsync(a => a.IdExpediente == listaExpedientes.IdExpediente);

                    if (insertarExpediente == null)
                    {
                        insertarExpediente = new Expediente();
                        ctx.Expedientes.Add(insertarExpediente);
                    }

                    insertarExpediente.IdExpediente = listaExpedientes.IdExpediente;
                    insertarExpediente.RHechoId = listaExpedientes.RHechoId;
                    insertarExpediente.Prefijo = listaExpedientes.Prefijo;
                    insertarExpediente.Cosecutivo = listaExpedientes.Cosecutivo;
                    insertarExpediente.Año = listaExpedientes.Año;
                    insertarExpediente.Sede = listaExpedientes.Sede;
                    insertarExpediente.NoExpediente = listaExpedientes.NoExpediente;
                    insertarExpediente.NoDerivacion = listaExpedientes.NoDerivacion;
                    insertarExpediente.InformacionStatus = listaExpedientes.InformacionStatus;
                    insertarExpediente.FechaRegistroExpediente = listaExpedientes.FechaRegistroExpediente;
                    insertarExpediente.FechaDerivacion = listaExpedientes.FechaDerivacion;
                    insertarExpediente.StatusAcepRech = listaExpedientes.StatusAcepRech;
                    insertarExpediente.DistritoIdDestino = listaExpedientes.DistritoIdDestino;

                    await ctx.SaveChangesAsync();

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


        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        //Comentado por que los roles varian demasiaod de lado de cat para derivacion a JR
        [HttpPost("[action]")]
        // POST: api/ClonacionJR/ClonarEnvio
        public async Task<IActionResult> ClonarEnvio([FromBody] Models.Rac.ClonarViewModelE model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaEnvios = await _context.Envios.Where(x => x.IdEnvio == model.EnvioId).Take(1).FirstOrDefaultAsync();

                if (listaEnvios == null)
                {
                    return BadRequest(ModelState);
                }
            
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var insertarEnvio = await ctx.Envios.FirstOrDefaultAsync(a => a.IdEnvio == listaEnvios.IdEnvio);

                    if (insertarEnvio == null)
                    {
                        insertarEnvio = new Envio();
                        ctx.Envios.Add(insertarEnvio);
                    }

                    insertarEnvio.IdEnvio = listaEnvios.IdEnvio;
                    insertarEnvio.ExpedienteId = listaEnvios.ExpedienteId;
                    insertarEnvio.AutoridadqueDeriva = listaEnvios.AutoridadqueDeriva;
                    insertarEnvio.uqe_Distrito = listaEnvios.uqe_Distrito;
                    insertarEnvio.uqe_DirSubProc = listaEnvios.uqe_DirSubProc;
                    insertarEnvio.uqe_Agencia = listaEnvios.uqe_Agencia;
                    insertarEnvio.uqe_Modulo = listaEnvios.uqe_Modulo;
                    insertarEnvio.uqe_Nombre = listaEnvios.uqe_Nombre;
                    insertarEnvio.uqe_Puesto = listaEnvios.uqe_Puesto;
                    insertarEnvio.StatusGeneral = listaEnvios.StatusGeneral;
                    insertarEnvio.RespuestaExpediente = listaEnvios.RespuestaExpediente;
                    insertarEnvio.EspontaneoNoEspontaneo = listaEnvios.EspontaneoNoEspontaneo;
                    insertarEnvio.PrimeraVezSubsecuente = listaEnvios.PrimeraVezSubsecuente;
                    insertarEnvio.ContadorNODerivacion = listaEnvios.ContadorNODerivacion;
                    insertarEnvio.FechaRegistro = listaEnvios.FechaRegistro;
                    insertarEnvio.FechaCierre = listaEnvios.FechaCierre;
                    insertarEnvio.NoSolicitantes = listaEnvios.NoSolicitantes;
                    insertarEnvio.StatusAMPO = listaEnvios.StatusAMPO;
                    insertarEnvio.InformaAMPO = listaEnvios.InformaAMPO;
                    insertarEnvio.InfoConclusion = listaEnvios.InfoConclusion;
                    insertarEnvio.oficioAMPO = listaEnvios.oficioAMPO;
                    insertarEnvio.ArregloConjunto = listaEnvios.ArregloConjunto;
                    insertarEnvio.FirmaInfoAMPO = listaEnvios.FirmaInfoAMPO;
                    insertarEnvio.PuestoFirmaAMPO = listaEnvios.PuestoFirmaAMPO;

                    await ctx.SaveChangesAsync();
                    
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

        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        //Comentado por que los roles varian demasiaod de lado de cat para derivacion a JR
        [HttpPost("[action]")]
        // POST: api/ClonacionJR/ClonarConjunto
        public async Task<IActionResult> ClonarConjunto([FromBody] Models.Rac.ClonarViewModelE model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaConjuntos = await _context.ConjuntoDerivaciones.Where(x => x.EnvioId == model.EnvioId).ToListAsync();
                
                if (listaConjuntos == null)
                {
                    return Ok();
                }
            
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    foreach (ConjuntoDerivaciones conjuntoActual in listaConjuntos)
                    {
                        var insertarConjuntos = await ctx.ConjuntoDerivaciones.FirstOrDefaultAsync(a => a.IdConjuntoDerivaciones == conjuntoActual.IdConjuntoDerivaciones);

                        if (insertarConjuntos == null)
                        {
                            insertarConjuntos = new ConjuntoDerivaciones();
                            ctx.ConjuntoDerivaciones.Add(insertarConjuntos);
                        }

                        insertarConjuntos.IdConjuntoDerivaciones = conjuntoActual.IdConjuntoDerivaciones;
                        insertarConjuntos.EnvioId = conjuntoActual.EnvioId;
                        insertarConjuntos.SolicitadosC = conjuntoActual.SolicitadosC;
                        insertarConjuntos.RequeridosC = conjuntoActual.RequeridosC;
                        insertarConjuntos.DelitosC = conjuntoActual.DelitosC;
                        insertarConjuntos.NombreS = conjuntoActual.NombreS;
                        insertarConjuntos.DireccionS = conjuntoActual.DireccionS;
                        insertarConjuntos.TelefonoS = conjuntoActual.TelefonoS;
                        insertarConjuntos.ClasificacionS = conjuntoActual.ClasificacionS;
                        insertarConjuntos.NombreR = conjuntoActual.NombreR;
                        insertarConjuntos.DireccionR = conjuntoActual.DireccionR;
                        insertarConjuntos.TelefonoR = conjuntoActual.TelefonoR;
                        insertarConjuntos.ClasificacionR = conjuntoActual.ClasificacionR;
                        insertarConjuntos.NombreD = conjuntoActual.NombreD;
                        insertarConjuntos.NoOficio = conjuntoActual.NoOficio;
                        insertarConjuntos.ResponsableJR = conjuntoActual.ResponsableJR;
                        insertarConjuntos.NombreSubDirigido = conjuntoActual.NombreSubDirigido;

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
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        //Comentado por que los roles varian demasiaod de lado de cat para derivacion a JR
        [HttpPost("[action]")]
        // POST: api/ClonacionJR/ClonarSolReq
        public async Task<IActionResult> ClonarSolReq([FromBody] Models.Rac.ClonarViewModelE model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaSolReq = await _context.SolicitanteRequeridos.Where(x => x.EnvioId == model.EnvioId).ToListAsync();

                if (listaSolReq == null)
                {
                    return Ok();

                }
            
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    foreach (SolicitanteRequerido solReqActual in listaSolReq)
                    {
                        var insertarSolReq = await ctx.SolicitanteRequeridos.FirstOrDefaultAsync(a => a.IdRSolicitanteRequerido == solReqActual.IdRSolicitanteRequerido);

                        if (insertarSolReq == null)
                        {
                            insertarSolReq = new SolicitanteRequerido();
                            ctx.SolicitanteRequeridos.Add(insertarSolReq);
                        }

                        insertarSolReq.IdRSolicitanteRequerido = solReqActual.IdRSolicitanteRequerido;
                        insertarSolReq.EnvioId = solReqActual.EnvioId;
                        insertarSolReq.PersonaId = solReqActual.PersonaId;
                        insertarSolReq.Tipo = solReqActual.Tipo;
                        insertarSolReq.Clasificacion = solReqActual.Clasificacion;
                        insertarSolReq.ConjuntoDerivacionesId = solReqActual.ConjuntoDerivacionesId;

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

        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        //Comentado por que los roles varian demasiaod de lado de cat para derivacion a JR
        [HttpPost("[action]")]
        // POST: api/ClonacionJR/ClonarDelitos
        public async Task<IActionResult> ClonarDelitos([FromBody] Models.Rac.ClonarViewModelE model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaDelitosD = await _context.DelitosDerivados.Where(x => x.EnvioId == model.EnvioId).ToListAsync();

            if (listaDelitosD == null)
            {
                return Ok();
            }            
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    foreach (DelitoDerivado delitoDActual in listaDelitosD)
                    {
                        var insertarDelitoD = await ctx.DelitosDerivados.FirstOrDefaultAsync(a => a.IdDelitoDerivado == delitoDActual.IdDelitoDerivado);

                        if (insertarDelitoD == null)
                        {
                            insertarDelitoD = new DelitoDerivado();
                            ctx.DelitosDerivados.Add(insertarDelitoD);
                        }

                        insertarDelitoD.IdDelitoDerivado = delitoDActual.IdDelitoDerivado;
                        insertarDelitoD.EnvioId = delitoDActual.EnvioId;
                        insertarDelitoD.RDHId = delitoDActual.RDHId;
                        insertarDelitoD.ConjuntoDerivacionesId = delitoDActual.ConjuntoDerivacionesId;

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

        // GET: api/ClonacionJR/EliminarDerivaciones
        //[Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        [HttpPost("[action]")]
        public async Task<IActionResult> EliminarDerivaciones([FromBody] EliminarDerivacionD model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Obtén todos los registros de ConjuntoDerivacion con el mismo IdEnvio
                var conjuntosAEliminar = await _context.ConjuntoDerivaciones
                    .Where(c => c.EnvioId == model.EnvioId)
                    .ToListAsync();

                // Obtén todos los registros de DelitosDerivados con el mismo IdEnvio
                var DelitosEliminar = await _context.DelitosDerivados
                    .Where(c => c.EnvioId == model.EnvioId)
                    .ToListAsync();

                // Obtén todos los registros de DelitosDerivados con el mismo IdEnvio
                var SolReqEliminar = await _context.SolicitanteRequeridos
                    .Where(c => c.EnvioId == model.EnvioId)
                    .ToListAsync();

                // Obtén el registro de Envios con el mismo IdEnvio
                var EnvioEliminar = await _context.Envios.Where
                    (a => a.IdEnvio == model.EnvioId)
                    .Take(1).FirstOrDefaultAsync();

                // Obtén el registro de Expedientes con el mismo IdExpediente
                var ExpedienteEliminar = await _context.Expedientes
                    .Where(a => a.IdExpediente == model.ExpedienteId)
                    .Take(1).FirstOrDefaultAsync();

                if (conjuntosAEliminar == null || !conjuntosAEliminar.Any())
                {
                    return Ok(new { res = "Error", men = "No se encontro ningun expediente con ese Id" });
                }
                else
                {
                    // Elimina los registros de ConjuntoDerivacion
                    _context.ConjuntoDerivaciones.RemoveRange(conjuntosAEliminar);
                    // Aplica los cambios a la base de datos
                    await _context.SaveChangesAsync();
                    //--------------------------------------------------------------
                    // Elimina los registros de ConjuntoDerivacion
                    _context.DelitosDerivados.RemoveRange(DelitosEliminar);
                    // Aplica los cambios a la base de datos
                    await _context.SaveChangesAsync();
                    //--------------------------------------------------------------
                    // Elimina los registros de ConjuntoDerivacion
                    _context.SolicitanteRequeridos.RemoveRange(SolReqEliminar);
                    // Aplica los cambios a la base de datos
                    await _context.SaveChangesAsync();
                    //--------------------------------------------------------------
                    _context.Remove(EnvioEliminar);
                    // Aplica los cambios a la base de datos
                    await _context.SaveChangesAsync();
                    //--------------------------------------------------------------
                    _context.Remove(ExpedienteEliminar);
                    // Aplica los cambios a la base de datos
                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "El expediente fue eliminado correctamente" });
        }
    }
}
