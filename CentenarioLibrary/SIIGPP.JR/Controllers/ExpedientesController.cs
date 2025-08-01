using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.JR.Models.RExpediente;
using SIIGPP.Entidades.M_JR.RExpediente;
using SIIGPP.JR.Models.AdminInfo;
using Microsoft.Extensions.Configuration;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_JR.RSolicitanteRequerido;
using SIIGPP.Entidades.M_JR.RDelitodelitosDerivados;
using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;
using SIIGPP.Entidades.M_JR.RSesion;
using SIIGPP.Entidades.M_JR.RRegistro;
using SIIGPP.Entidades.M_Administracion;
using SIIGPP.Entidades.M_Cat.DDerivacion;
using SIIGPP.JR.Models.RSolicitanteRequerido;
using SIIGPP.JR.Models.REnvio;
using SIIGPP.Entidades.M_Cat.Orientacion;

namespace SIIGPP.JR.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpedientesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public ExpedientesController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Expedientes/ExisteExpedientes
        // API: VALIDAR SI EXISTE EL EXPEDIETE
        [Authorize(Roles = "Administrador,Director,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IActionResult> ExisteExpedientes([FromRoute] Guid rHechoId)
        {
            var Tabla = await _context.Expedientes
                          .Include(a => a.RHecho)
                          .Include(a => a.RHecho.NUCs)
                          .Where(a => a.RHechoId == rHechoId)
                          .ToListAsync();

            if (Tabla == null)
            {

                return Ok(new { ner = 1 });
            }
            return Ok(Tabla.Select(a => new GET_ExpedienteViewModel
            {
                IdExpediente = a.IdExpediente,
                RHechoId = a.RHechoId,
                Prefijo = a.Prefijo,
                Cosecutivo = a.Cosecutivo,
                Año = a.Año,
                Sede = a.Sede,
                NoExpediente = a.NoExpediente,
                NoDerivacion = a.NoDerivacion,
                //StatusGeneral = Tabla.StatusGeneral,
                FechaRegistroExpediente = a.FechaRegistroExpediente,
                NUC = a.RHecho.NUCs.nucg,
                FechaHoraSuceso = a.RHecho.FechaHoraSuceso,
                ReseñaBreve = a.RHecho.RBreve,
            }));
        }

        // GET: api/Expedientes/ExisteExpedientes
        // API: VALIDAR SI EXISTE EL EXPEDIETE
        [Authorize(Roles = "Administrador,Director,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{idexpediente}")]
        public async Task<IActionResult> ExisteExpedienteEspecifico([FromRoute] Guid idexpediente)
        {
            var Tabla = await _context.Expedientes
                          .Include(a => a.RHecho)
                          .Include(a => a.RHecho.NUCs)
                          .Where(a => a.IdExpediente == idexpediente)
                          .FirstOrDefaultAsync();

            if (Tabla == null)
            {

                return Ok(new { ExisteExpediente = 0 });
            }
            return Ok(new GET_ExpedienteViewModel
            {
                IdExpediente = Tabla.IdExpediente,
                RHechoId = Tabla.RHechoId,
                Prefijo = Tabla.Prefijo,
                Cosecutivo = Tabla.Cosecutivo,
                Año = Tabla.Año,
                Sede = Tabla.Sede,
                NoExpediente = Tabla.NoExpediente,
                NoDerivacion = Tabla.NoDerivacion,
                //StatusGeneral = Tabla.StatusGeneral,
                FechaRegistroExpediente = Tabla.FechaRegistroExpediente,
                NUC = Tabla.RHecho.NUCs.nucg,
                FechaHoraSuceso = Tabla.RHecho.FechaHoraSuceso,
                ReseñaBreve = Tabla.RHecho.RBreve,
            });
        }


        // GET: api/Expedientes/BusquedaExpediente
        // API: VALIDAR SI EXISTE EL EXPEDIETE
        //[Authorize(Roles = "Administrador,Director,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IEnumerable<GET_ExpedienteViewModel>> BusquedaExpediente([FromRoute] Guid rHechoId)
        {
            var Tabla = await _context.Expedientes
                            .Include(a => a.RHecho)
                            .Include(a => a.RHecho.NUCs)
                            .Where(a => a.RHechoId == rHechoId)
                            .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_ExpedienteViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_ExpedienteViewModel
                {
                    IdExpediente = a.IdExpediente,
                    RHechoId = a.RHechoId,
                    Prefijo = a.Prefijo,
                    Cosecutivo = a.Cosecutivo,
                    Año = a.Año,
                    Sede = a.Sede,
                    NoExpediente = a.NoExpediente,
                    NoDerivacion = a.NoDerivacion,
                    StatusAcepRech = a.StatusAcepRech,
                    InformacionStatus = a.InformacionStatus,
                    FechaRegistroExpediente = a.FechaRegistroExpediente,
                    NUC = a.RHecho.NUCs.nucg,
                    FechaHoraSuceso = a.RHecho.FechaHoraSuceso,
                    ReseñaBreve = a.RHecho.RBreve,

                });
            }
        }

        // GET: api/Expedientes/ListarPorHecho
        // API: VALIDAR SI EXISTE EL EXPEDIETE 
        [HttpGet("[action]/{idRHecho}")]
        public async Task<IActionResult> ListarPorHecho([FromRoute] Guid idRHecho)
        {
            try
            {
                var expediente = await _context.Expedientes.Where(a => a.RHechoId == idRHecho).ToListAsync();
                return Ok(expediente.Select(a => new ListaExpedienteViewModel
                {
                    IdExpediente = a.IdExpediente,
                    NoExpediente = a.NoExpediente,
                    Sede = a.Sede,
                    Año = a.Año,
                    FechaDerivacion = a.FechaDerivacion,
                    StatusAcepRech = a.StatusAcepRech,
                    RHechoId = a.RHechoId
                })
                    );
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/Expedientes/ListarStatus
        // API: VALIDAR SI EXISTE EL EXPEDIETE 
        //[Authorize(Roles = "Director, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{idDistrito}")]
        public async Task<IEnumerable<GET_ExpedienteViewModel>> ListarStatus([FromRoute] Guid idDistrito)
        {
            var Tabla = await _context.Envios
                            .Include(a => a.Expediente.RHecho)
                            .Include(a => a.Expediente.RHecho.NUCs)
                            .Include(a => a.Expediente.RHecho.Agencia.DSP.Distrito)
                            .Where(a => a.Expediente.StatusAcepRech == "Solicitado")
                            .Where(a => a.StatusGeneral == "Solicitado")                            
                            .Where(a => a.Expediente.DistritoIdDestino == idDistrito)
                            .OrderBy(a => a.FechaRegistro)
                            .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_ExpedienteViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_ExpedienteViewModel
                {
                    IdExpediente = a.Expediente.IdExpediente,
                    RHechoId = a.Expediente.RHechoId,
                    Prefijo = a.Expediente.Prefijo,
                    Cosecutivo = a.Expediente.Cosecutivo,
                    Año = a.Expediente.Año,
                    Sede = a.Expediente.Sede,
                    NoExpediente = a.Expediente.NoExpediente,
                    NoDerivacion = a.Expediente.NoDerivacion,
                    StatusAcepRech = a.Expediente.StatusAcepRech,
                    InformacionStatus = a.Expediente.InformacionStatus,
                    FechaRegistroExpediente = a.Expediente.FechaRegistroExpediente,
                    NUC = a.Expediente.RHecho.NUCs.nucg,
                    FechaHoraSuceso = a.Expediente.RHecho.FechaHoraSuceso,
                    ReseñaBreve = a.Expediente.RHecho.RBreve,
                    NarracionHechos = a.Expediente.RHecho.NarrativaHechos,

                    //INFORMACION ENVIO
                    AutoridadqueDeriva = a.AutoridadqueDeriva,
                    uqe_Distrito = a.uqe_Distrito,
                    uqe_DirSubProc = a.uqe_DirSubProc,
                    uqe_Agencia = a.uqe_Agencia,
                    uqe_Modulo = a.uqe_Modulo,
                    uqe_Nombre = a.uqe_Nombre,
                    uqe_Puesto = a.uqe_Puesto,
                    StatusGeneralEnvio = a.StatusGeneral,
                    EspontaneoNoEspontaneo = a.EspontaneoNoEspontaneo,
                    PrimeraVezSubsecuente = a.PrimeraVezSubsecuente,
                    ContadorNODerivacion = a.ContadorNODerivacion,
                    FechaRegistro = a.FechaRegistro,
                    NoSolicitantes = a.NoSolicitantes,
                    DistritoOrigenId = a.Expediente.RHecho.Agencia.DSP.Distrito.IdDistrito
                });
            }
        }
        // GET: api/Expedientes/ListarTodas
        // API: VALIDAR SI EXISTE EL EXPEDIETE 
        //[Authorize(Roles = "Director, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{idDistrito}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<GET_ExpedienteViewModel>> ListarTodas([FromRoute] Guid idDistrito, DateTime fechai, DateTime fechaf, string StatusAcepRech)
        {
            var Tabla = await _context.Envios
                            .Include(a => a.Expediente.RHecho)
                            .Include(a => a.Expediente.RHecho.NUCs)
                            //.Where(a => a.Expediente.RHecho.NUCs.DistritoId == idDistrito)
                            .Where(a => a.Expediente.RHecho.ModuloServicio.Agencia.DSP.DistritoId == idDistrito)
                            .Where(a => a.Expediente.FechaDerivacion >= fechai)
                            .Where(a => a.Expediente.FechaDerivacion <= fechaf)
                            .OrderBy(a => a.FechaRegistro)
                            .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_ExpedienteViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_ExpedienteViewModel
                {
                    IdExpediente = a.Expediente.IdExpediente,
                    RHechoId = a.Expediente.RHechoId,
                    Prefijo = a.Expediente.Prefijo,
                    Cosecutivo = a.Expediente.Cosecutivo,
                    Año = a.Expediente.Año,
                    Sede = a.Expediente.Sede,
                    NoExpediente = a.Expediente.NoExpediente,
                    NoDerivacion = a.Expediente.NoDerivacion,
                    StatusAcepRech = a.Expediente.StatusAcepRech,
                    FechaRegistroExpediente = a.FechaRegistro,
                    NUC = a.Expediente.RHecho.NUCs.nucg,
                    FechaHoraSuceso = a.Expediente.RHecho.FechaHoraSuceso,
                    ReseñaBreve = a.Expediente.RHecho.RBreve,
                    NarracionHechos = a.Expediente.RHecho.NarrativaHechos,

                    //INFORMACION ENVIO
                    AutoridadqueDeriva = a.AutoridadqueDeriva,
                    uqe_Distrito = a.uqe_Distrito,
                    uqe_DirSubProc = a.uqe_DirSubProc,
                    uqe_Agencia = a.uqe_Agencia,
                    uqe_Modulo = a.uqe_Modulo,
                    uqe_Nombre = a.uqe_Nombre,
                    uqe_Puesto = a.uqe_Puesto,
                    StatusGeneralEnvio = a.StatusGeneral,
                    EspontaneoNoEspontaneo = a.EspontaneoNoEspontaneo,
                    PrimeraVezSubsecuente = a.PrimeraVezSubsecuente,
                    ContadorNODerivacion = a.ContadorNODerivacion,
                    FechaRegistro = a.FechaRegistro,
                    NoSolicitantes = a.NoSolicitantes,

                });
            }
        }


        // GET: api/Expedientes/ListarTodasFiltro
        // API: VALIDAR SI EXISTE EL EXPEDIETE 
        //[Authorize(Roles = "Director, AMPO-AMP, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{idDistrito}/{fechai}/{fechaf}/{StatusGeneral}")]
        public async Task<IEnumerable<GET_ExpedienteViewModel>> ListarTodasFiltro([FromRoute] Guid idDistrito, DateTime fechai, DateTime fechaf, string StatusGeneral)
        {
            var Tabla = await _context.Envios
                            .Include(a => a.Expediente.RHecho)
                            .Include(a => a.Expediente.RHecho.NUCs)
                            //.Where(a => a.Expediente.RHecho.NUCs.DistritoId == idDistrito)
                            .Where(a => a.Expediente.RHecho.ModuloServicio.Agencia.DSP.DistritoId == idDistrito)
                            .Where(a => a.Expediente.FechaDerivacion >= fechai)
                            .Where(a => a.Expediente.FechaDerivacion <= fechaf)
                            .Where(a => StatusGeneral != "Todos" ? a.StatusGeneral == StatusGeneral : 1 == 1)
                            .OrderBy(a => a.FechaRegistro)
                            .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_ExpedienteViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_ExpedienteViewModel
                {
                    IdExpediente = a.Expediente.IdExpediente,
                    RHechoId = a.Expediente.RHechoId,
                    Prefijo = a.Expediente.Prefijo,
                    Cosecutivo = a.Expediente.Cosecutivo,
                    Año = a.Expediente.Año,
                    Sede = a.Expediente.Sede,
                    NoExpediente = a.Expediente.NoExpediente,
                    NoDerivacion = a.Expediente.NoDerivacion,
                    StatusAcepRech = a.Expediente.StatusAcepRech,
                    FechaRegistroExpediente = a.Expediente.FechaDerivacion,
                    NUC = a.Expediente.RHecho.NUCs.nucg,
                    FechaHoraSuceso = a.Expediente.RHecho.FechaHoraSuceso,
                    ReseñaBreve = a.Expediente.RHecho.RBreve,
                    NarracionHechos = a.Expediente.RHecho.NarrativaHechos,
                    InformacionStatus = a.Expediente.InformacionStatus,

                    //INFORMACION ENVIO
                    AutoridadqueDeriva = a.AutoridadqueDeriva,
                    uqe_Distrito = a.uqe_Distrito,
                    uqe_DirSubProc = a.uqe_DirSubProc,
                    uqe_Agencia = a.uqe_Agencia,
                    uqe_Modulo = a.uqe_Modulo,
                    uqe_Nombre = a.uqe_Nombre,
                    uqe_Puesto = a.uqe_Puesto,
                    StatusGeneralEnvio = a.StatusGeneral,
                    EspontaneoNoEspontaneo = a.EspontaneoNoEspontaneo,
                    PrimeraVezSubsecuente = a.PrimeraVezSubsecuente,
                    ContadorNODerivacion = a.ContadorNODerivacion,
                    FechaRegistro = a.FechaRegistro,
                    NoSolicitantes = a.NoSolicitantes,

                });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CrearExpedienteJRNew([FromBody] POST_CrearExpedienteViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid idenvio;
            Guid idexpediente;

            try
            {
                //************************************************************

                var nc = 0;
                //var prefijo = "CJRP";
                DateTime fechaActual = DateTime.Today;
                var año = fechaActual.Year;

                var fechaHoraDer = DateTime.Now;

                Expediente exp = new Expediente
                {
                    RHechoId = model.RHechoId,
                    Sede = model.Sede,
                    Cosecutivo = nc,
                    Año = año,
                    NoDerivacion = 1,
                    StatusAcepRech = model.StatusAcepRech,
                    FechaRegistroExpediente = fechaHoraDer,
                    FechaDerivacion = fechaHoraDer,
                    InformacionStatus = "",
                    DistritoIdDestino = model.DistritoIdDestino

                };

                _context.Expedientes.Add(exp);
                await _context.SaveChangesAsync();

                // SE AGREGA EL REGISTRO DE ENVIO
                //************************************************************
                var idExpediente = exp.IdExpediente;
                idexpediente = exp.IdExpediente;


                Envio Env = new Envio
                {
                    ExpedienteId = idExpediente,
                    AutoridadqueDeriva = model.AutoridadqueDeriva,
                    uqe_Distrito = model.uqe_Distrito,
                    uqe_DirSubProc = model.uqe_DirSubProc,
                    uqe_Agencia = model.uqe_Agencia,
                    uqe_Modulo = model.uqe_Modulo,
                    uqe_Nombre = model.uqe_Nombre,
                    uqe_Puesto = model.uqe_Puesto,
                    StatusGeneral = model.StatusGeneralEnvio,
                    RespuestaExpediente = model.RespuestaExpediente,
                    EspontaneoNoEspontaneo = model.EspontaneoNoEspontaneo,
                    PrimeraVezSubsecuente = "Primera vez",
                    ContadorNODerivacion = model.ContadorNODerivacion,
                    FechaRegistro = fechaHoraDer,
                    NoSolicitantes = model.NoSolicitantes,
                    ArregloConjunto = model.ArregloConjunto,
                    ArregloRepresentantes = model.ArregloRepresentantes,
                    statusEnvio = model.statusEnvio,
                };
                _context.Envios.Add(Env);
                await _context.SaveChangesAsync();

                idenvio = Env.IdEnvio;
                idexpediente = exp.IdExpediente;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(new { idenvio = idenvio, idexpediente = idexpediente });

        }

        //POST: api/Expedientes/CrearConjunto
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearConjunto([FromBody] POST_CrearConjuntosSRD model)

        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Variables que retornaran
            Guid idconjunto;
            string solicitados;
            string requeridos;
            string clasificacionr;
            string clasificacions;
            string delitos;

            try
            {
                //CODIGO PARA INSERTAR EN TABLA DE CONJUNTOS
                ConjuntoDerivaciones condev = new ConjuntoDerivaciones
                {
                    EnvioId = model.EnvioId,
                    //VALORES DE ID 
                    SolicitadosC = model.SolicitadosC,
                    RequeridosC = model.RequeridosC,
                    DelitosC = model.DelitosC,
                    //VALORES DE SOLICITANTES
                    NombreS = model.NombreS,
                    DireccionS = model.DireccionS,
                    TelefonoS = model.TelefonoS,
                    ClasificacionS = model.ClasificacionS,
                    //VALORES DE SOLICITANTES
                    NombreR = model.NombreR,
                    DireccionR = model.DireccionR,
                    TelefonoR = model.TelefonoR,
                    ClasificacionR = model.ClasificacionR,
                    //VALORES DE LOS DELITOS
                    NombreD = model.NombreD,
                    NoOficio = model.NoOficio,
                    ResponsableJR = model.ResponsableJR,
                    NombreSubDirigido = model.NombreSubDirigido
                };

                _context.ConjuntoDerivaciones.Add(condev);
                //GUARDAMOS  EL PRIMER DATO
                await _context.SaveChangesAsync();

                //Asignamos valores a los valores que se van a retornar
                idconjunto = condev.IdConjuntoDerivaciones;
                solicitados = condev.SolicitadosC;
                requeridos = condev.RequeridosC;
                clasificacions = condev.ClasificacionS;
                clasificacionr = condev.ClasificacionR;
                delitos = condev.DelitosC;

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
            //Retorno de las variables
            return Ok(new { idconjunto = idconjunto, solicitados = solicitados, requeridos = requeridos, clasificacions = clasificacions, clasificacionr = clasificacionr, delitos = delitos });


        }
        // GET: api/Expedientes/ListarConjuntos
        // API: VALIDAR SI EXISTE EL EXPEDIETE 
        [HttpGet("[action]/{idEnvio}")]
        public async Task<IActionResult> ListarConjuntos([FromRoute] Guid idEnvio)
        {
            try
            {
                var listaC = await _context.ConjuntoDerivaciones.
                    Include(a => a.Envio).
                    Where(a => a.EnvioId == idEnvio).ToListAsync();


                return Ok(listaC.Select(a => new POST_CrearConjuntosSRD
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
                    EspontaneoNoEspontaneo = a.Envio.EspontaneoNoEspontaneo,
                })
                    );
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        //POST: api/Expedientes/CrearSolReq
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearSolReq([FromBody] POST_CrearSViewModel model)

        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                SolicitanteRequerido SoliReq = new SolicitanteRequerido();

                //Splites para guardar registros concatenados de uno por uno

                //IDs de personas de los requeridos y solicitados
                string[] solicitant = model.SolicitantesC.Split("; ");
                string[] requerid = model.RequeridosC.Split("; ");
                //Clasificacion de los requeridos y solicitados
                string[] clasireqs = model.ClasificacionS.Split("; ");
                string[] clasireqr = model.ClasificacionR.Split("; ");
                //Delitos
                string[] delitos = model.DelitosC.Split("; ");

                //Primer for para el guardado de los solicitantes
                for (int i = 0; i < solicitant.Length; i++)
                {
                    //PersonaId = Guid.Parse(personal[i]),
                    SoliReq = new SolicitanteRequerido
                    {
                        EnvioId = model.envioId,
                        PersonaId = Guid.Parse(solicitant[i]),
                        Tipo = "Solicitante",
                        Clasificacion = clasireqs[i],
                        ConjuntoDerivacionesId = model.ConjuntoDerivacionesId,
                    };
                    _context.SolicitanteRequeridos.Add(SoliReq);

                    await _context.SaveChangesAsync();

                }

                //Segundo for para el guardado de los requeridos
                for (int i = 0; i < requerid.Length; i++)
                {
                    //PersonaId = Guid.Parse(personal[i]),
                    SoliReq = new SolicitanteRequerido
                    {
                        EnvioId = model.envioId,
                        PersonaId = Guid.Parse(requerid[i]),
                        Tipo = "Requerido",
                        Clasificacion = clasireqr[i],
                        ConjuntoDerivacionesId = model.ConjuntoDerivacionesId,
                    };
                    _context.SolicitanteRequeridos.Add(SoliReq);

                    await _context.SaveChangesAsync();

                }

                //Tercer for para el guardado de los delitos
                DelitoDerivado Delit = new DelitoDerivado();

                for (int i = 0; i < delitos.Length; i++)
                {
                    //PersonaId = Guid.Parse(personal[i]),
                    Delit = new DelitoDerivado
                    {
                        EnvioId = model.envioId,
                        RDHId = Guid.Parse(delitos[i]),
                        ConjuntoDerivacionesId = model.ConjuntoDerivacionesId,
                    };
                    _context.DelitosDerivados.Add(Delit);

                    await _context.SaveChangesAsync();

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
            return Ok();


        }


        // PUT: api/Expedientes/NoDerivacionStatus
        [HttpPut("[action]")]
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        public async Task<IActionResult> NoDerivacionStatus([FromBody] PUT_NoDerivacionStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var db = await _context.Expedientes.FirstOrDefaultAsync(a => a.IdExpediente == model.IdExpediente);

            if (db == null)
            {
                return NotFound();
            }

            db.NoDerivacion = model.NoDerivacion;
            db.StatusAcepRech = model.StatusAcepRech;
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
        // PUT: api/Expedientes/StatusAcepRech
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> StatusAcepRech([FromBody] PUT_StatusGeneralViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var db = await _context.Expedientes.FirstOrDefaultAsync(a => a.IdExpediente == model.IdExpediente);

            if (db == null)
            {
                return NotFound();
            }

            db.StatusAcepRech = model.StatusAcepRech;
            db.InformacionStatus = model.Informacionstatus;

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
        // PUT: api/Expedientes/InformacionStatus
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> InformacionStatus([FromBody] PUT_InformacionStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var db = await _context.Expedientes.FirstOrDefaultAsync(a => a.IdExpediente == model.IdExpediente);

            if (db == null)
            {
                return NotFound();
            }

            db.InformacionStatus = model.InformacionStatus;


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



        //POST: api/Expedientes/CrearRegistroModuloCaptura
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearRegistroModuloCaptura([FromBody] ALL_ModuloCaptura model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }



                //************************************************************ Expediente

                Expediente expediente = new Expediente
                {
                    RHechoId = model.RhechoId,
                    Prefijo = model.Prefijo,
                    Cosecutivo = model.Consecutivo,
                    Año = model.Ano,
                    Sede = model.EnDistrito,
                    NoExpediente = model.NumeroExpediente,
                    NoDerivacion = 1,
                    InformacionStatus = "",
                    FechaRegistroExpediente = System.DateTime.Now,
                    FechaDerivacion = System.DateTime.Now,
                    StatusAcepRech = "",

                };

                _context.Expedientes.Add(expediente);


                var idExpediente = expediente.IdExpediente;

                //************************************************************ Envio

                Envio Env = new Envio
                {
                    ExpedienteId = idExpediente,
                    AutoridadqueDeriva = "",
                    uqe_Distrito = model.EnDistrito,
                    uqe_DirSubProc = model.EnDisubproc,
                    uqe_Agencia = model.EnAgencia,
                    uqe_Modulo = model.EnModulo,
                    uqe_Nombre = model.EnNombre,
                    uqe_Puesto = model.EnPuesto,
                    StatusGeneral = "Concluido",
                    RespuestaExpediente = "",
                    EspontaneoNoEspontaneo = "",
                    PrimeraVezSubsecuente = "Primera vez",
                    ContadorNODerivacion = 1,
                    FechaRegistro = System.DateTime.Now,
                    NoSolicitantes = model.Solicitantes.Count + model.Requeridos.Count,

                };
                _context.Envios.Add(Env);


                //************************************************************ SolicitantesRequerido


                var idEnvio = Env.IdEnvio;

                foreach (var Solicitante in model.Solicitantes)
                {
                    SolicitanteRequerido S = new SolicitanteRequerido
                    {
                        EnvioId = idEnvio,
                        PersonaId = Solicitante.PersonaId,
                        Tipo = "Solicitante",
                        Clasificacion = Solicitante.Clasificacion,
                    };
                    _context.SolicitanteRequeridos.Add(S);
                }
                foreach (var Requerido in model.Requeridos)
                {
                    SolicitanteRequerido R = new SolicitanteRequerido
                    {
                        EnvioId = idEnvio,
                        PersonaId = Requerido.PersonaId,
                        Tipo = "Requerido",
                        Clasificacion = Requerido.Clasificacion,
                    };
                    _context.SolicitanteRequeridos.Add(R);
                }

                //************************************************************ DelitoDerivado



                foreach (var delitoft in model.Delitos)
                {
                    DelitoDerivado delito = new DelitoDerivado
                    {
                        EnvioId = idEnvio,
                        RDHId = delitoft.IdDelito,
                    };
                    _context.DelitosDerivados.Add(delito);
                }



                string solicitantef = "", requeridof = "", delitof = "";

                foreach (var Solicitante in model.Solicitantes)
                {
                    solicitantef += Solicitante.Nombre + ", ";

                }

                foreach (var Requerido in model.Requeridos)
                {
                    requeridof += Requerido.Nombre + ", ";
                }

                foreach (var Delito in model.Delitos)
                {
                    delitof += Delito.Nombre + ", ";
                }


                AcuerdoReparatorio ar = new AcuerdoReparatorio
                {
                    EnvioId = idEnvio,
                    NombreSolicitante = solicitantef,
                    NombreRequerdio = requeridof,
                    Delitos = delitof,
                    NUC = model.NUC,
                    NoExpediente = model.NumeroExpediente,
                    StatusConclusion = model.Status,
                    StatusCumplimiento = (model.Status == "Diferido" ? model.StatusCumplimiento : ""),
                    TipoPago = "",
                    MetodoUtilizado = "",
                    MontoTotal = 0,
                    ObjectoEspecie = "",
                    NoTotalParcialidades = 0,
                    Periodo = 0,
                    FechaCelebracionAcuerdo = (model.Status == "Inmediato" ? model.FechaCelebracionInme : model.Status == "Diferido" ? model.FechaCelebracionDife : new DateTime?()),
                    FechaLimiteCumplimiento = (model.Status == "Diferido" ? model.Fechalimitecumplimiento : new DateTime?()),
                    StatusRespuestaCoordinadorJuridico = "",
                    RespuestaCoordinadorJuridico = "",
                    FechaHoraRespuestaCoordinadorJuridico = new DateTime?(),
                    StatusRespuestaAMP = "",
                    FechaRespuestaAMP = System.DateTime.Now,
                    RespuestaAMP = "",
                    TextoAR = "",
                    uf_Distrito = "",
                    uf_DirSubProc = "",
                    uf_Agencia = "",
                    uf_Modulo = "",
                    uf_Nombre = "",
                    uf_Puesto = "",
                    Fechasise = System.DateTime.Now,
                    Sise = (model.Status == "Diferido" ? model.SISE : 0),
                    TipoDocumento = ""

                };
                _context.AcuerdoReparatorios.Add(ar);



                if (model.Status != "Tramite")
                {
                    RegistroConclusion RegistroConclusiones = new RegistroConclusion
                    {

                        Asunto = "REGISTRO DE CONCLUSIÓN",
                        Texto = "",
                        FechaHora = (model.Status == "Diferido" ? model.FechaConstanciaconclusion : System.DateTime.Now),
                        Solicitates = solicitantef,
                        Reuqeridos = requeridof,
                        StatusGeneral = (model.Status == "Baja" ? "Baja" : "Concluido"),
                        Conclusion = (model.Status == "Baja" ? model.StatusBaja : model.Status == "Inmediato" ? "Registros conclusión acuerdo inmediato" : model.Status == "Diferido" ? model.StatusCumplimiento : ""),
                        uf_Distrito = "",
                        uf_DirSubProc = "",
                        uf_Agencia = "",
                        uf_Modulo = "",
                        uf_Nombre = "",
                        uf_Puesto = "",
                        EnvioId = idEnvio

                    };
                    _context.RegistroConclusions.Add(RegistroConclusiones);
                }


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }


        // GET: api/Expedientes/ListarModuloCaptura
        //[Authorize(Roles = "Administrador,Director,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IEnumerable<GET_ModuloCaptura>> ListarModuloCaptura([FromRoute] Guid rHechoId)
        {

            var Expediente = await _context.Expedientes
                            .Where(a => a.RHechoId == rHechoId)
                            .ToListAsync();


            var Expedientet = Expediente.Select(a => new GET_ModuloCaptura
            {
                IdExpediente = a.IdExpediente,
                RHechoId = a.RHechoId,
                Prefijo = a.Prefijo,
                Ano = a.Año,
                NoExpediente = a.NoExpediente

            });

            IEnumerable<GET_ModuloCaptura> items1 = new GET_ModuloCaptura[] { };

            foreach (var Expe in Expedientet)
            {
                var Envio = await _context.Envios
                        .Where(a => a.ExpedienteId == Expe.IdExpediente)
                        .FirstOrDefaultAsync();

                IEnumerable<GET_ModuloCaptura> ReadLines()
                {
                    IEnumerable<GET_ModuloCaptura> item2;

                    item2 = (new[]{new GET_ModuloCaptura{

                        IdExpediente = Expe.IdExpediente,
                        RHechoId = Expe.RHechoId,
                        Prefijo = Expe.Prefijo,
                        Ano = Expe.Ano,
                        NoExpediente = Expe.NoExpediente,
                        IdEnvio = Envio.IdEnvio,
                        uqe_Agencia = Envio.uqe_Agencia,
                        uqe_DirSubProc = Envio.uqe_DirSubProc,
                        uqe_Distrito = Envio.uqe_Distrito,
                        uqe_Modulo = Envio.uqe_Modulo,
                        uqe_Nombre = Envio.uqe_Nombre,
                        uqe_Puesto = Envio.uqe_Puesto,
                        StatusGeneral = Envio.StatusGeneral,
                        NoSolicitantes = Envio.NoSolicitantes

                    }});

                    return item2;
                }

                items1 = items1.Concat(ReadLines());
                Expedientet = items1;

            }



            items1 = new GET_ModuloCaptura[] { };

            foreach (var Expe in Expedientet)
            {
                var AcuerdoReparatorio = await _context.AcuerdoReparatorios
                        .Where(a => a.EnvioId == Expe.IdEnvio)
                        .FirstOrDefaultAsync();

                IEnumerable<GET_ModuloCaptura> ReadLines()
                {
                    IEnumerable<GET_ModuloCaptura> item2;

                    item2 = (new[]{new GET_ModuloCaptura{

                        IdExpediente = Expe.IdExpediente,
                        RHechoId = Expe.RHechoId,
                        Prefijo = Expe.Prefijo,
                        Ano = Expe.Ano,
                        NoExpediente = Expe.NoExpediente,
                        IdEnvio = Expe.IdEnvio,
                        uqe_Agencia = Expe.uqe_Agencia,
                        uqe_DirSubProc = Expe.uqe_DirSubProc,
                        uqe_Distrito = Expe.uqe_Distrito,
                        uqe_Modulo = Expe.uqe_Modulo,
                        uqe_Nombre = Expe.uqe_Nombre,
                        uqe_Puesto = Expe.uqe_Puesto,
                        StatusGeneral = Expe.StatusGeneral,
                        NoSolicitantes = Expe.NoSolicitantes,
                        Solicitantes = AcuerdoReparatorio != null ?  AcuerdoReparatorio.NombreSolicitante : "",
                        Requeridos =  AcuerdoReparatorio != null ? AcuerdoReparatorio.NombreRequerdio : "",
                        Delitos =  AcuerdoReparatorio != null ? AcuerdoReparatorio.Delitos: "",
                        StatusConclusion = AcuerdoReparatorio != null ?  AcuerdoReparatorio.StatusConclusion : "",
                        StatusCumplimiento = AcuerdoReparatorio != null ?  AcuerdoReparatorio.StatusCumplimiento : "",
                        Fechacelebracion =  AcuerdoReparatorio != null ? AcuerdoReparatorio.FechaCelebracionAcuerdo : new DateTime(),
                        FechaLimitecumplimiento =  AcuerdoReparatorio != null ? AcuerdoReparatorio.FechaLimiteCumplimiento : new DateTime(),
                        Sise =  AcuerdoReparatorio != null ? AcuerdoReparatorio.Sise : 0
                    }});

                    return item2;
                }

                items1 = items1.Concat(ReadLines());
                Expedientet = items1;

            }


            items1 = new GET_ModuloCaptura[] { };

            foreach (var Expe in Expedientet)
            {
                var conclusiones = await _context.RegistroConclusions
                        .Where(a => a.EnvioId == Expe.IdEnvio)
                        .FirstOrDefaultAsync();

                IEnumerable<GET_ModuloCaptura> ReadLines()
                {
                    IEnumerable<GET_ModuloCaptura> item2;

                    item2 = (new[]{new GET_ModuloCaptura{

                        IdExpediente = Expe.IdExpediente,
                        RHechoId = Expe.RHechoId,
                        Prefijo = Expe.Prefijo,
                        Ano = Expe.Ano,
                        NoExpediente = Expe.NoExpediente,
                        IdEnvio = Expe.IdEnvio,
                        uqe_Agencia = Expe.uqe_Agencia,
                        uqe_DirSubProc = Expe.uqe_DirSubProc,
                        uqe_Distrito = Expe.uqe_Distrito,
                        uqe_Modulo = Expe.uqe_Modulo,
                        uqe_Nombre = Expe.uqe_Nombre,
                        uqe_Puesto = Expe.uqe_Puesto,
                        StatusGeneral = Expe.StatusGeneral,
                        NoSolicitantes = Expe.NoSolicitantes,
                        Solicitantes = Expe.Solicitantes,
                        Requeridos = Expe.Requeridos,
                        Delitos = Expe.Delitos,
                        StatusConclusion = Expe.StatusConclusion,
                        StatusCumplimiento = Expe.StatusCumplimiento,
                        Fechacelebracion = Expe.Fechacelebracion,
                        FechaLimitecumplimiento = Expe.FechaLimitecumplimiento,
                        Sise = Expe.Sise,
                        StatusBaja = conclusiones != null ? conclusiones.Conclusion : "",
                        FechaConclusion =  conclusiones != null ? conclusiones.FechaHora : new DateTime()

                    }});

                    return item2;
                }

                items1 = items1.Concat(ReadLines());
                Expedientet = items1;

            }

            return Expedientet;

        }


        private bool ExpedienteExists(Guid id)
        {
            return _context.Expedientes.Any(e => e.IdExpediente == id);
        }

        // POST: api/RemisionesUI/Eliminar
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] EliminarNuc model)
        {
            var mensajeExitoso = "Derivación a Justicia Restaurativa eliminada correctamente";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
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
                        MovimientoId = new Guid("83c7fe4c-8f1b-498b-8622-9809629b039f")
                    };

                    ctx.Add(laRegistro);

                    var consultaEnvio = await _context.Envios.Where(a => a.ExpedienteId == model.infoBorrado.registroId)
                                        .Take(1).FirstOrDefaultAsync();

                    if (consultaEnvio == null)
                    {
                        return Ok(new { res = "Error", men = "No se encontró en  registro de envio con la información enviada" });
                    }
                    else
                    {
                        // Obtencion de la fecha y hora de registro para posterior uso en historial de carpeta, sin los segundos y milisegundos
                        var fechaRegistroEnvio = new DateTime(
                            consultaEnvio.FechaRegistro.Year,
                            consultaEnvio.FechaRegistro.Month,
                            consultaEnvio.FechaRegistro.Day,
                            consultaEnvio.FechaRegistro.Hour,
                            consultaEnvio.FechaRegistro.Minute,
                            0
                        );

                        var idEnvioM = consultaEnvio.IdEnvio;

                        LogEnvio envio = new LogEnvio
                        {
                            LogAdmonId = gLog,
                            IdEnvio = consultaEnvio.IdEnvio,
                            ExpedienteId = consultaEnvio.ExpedienteId,
                            AutoridadqueDeriva = consultaEnvio.AutoridadqueDeriva,
                            uqe_Distrito = consultaEnvio.uqe_Distrito,
                            uqe_DirSubProc = consultaEnvio.uqe_DirSubProc,
                            uqe_Agencia = consultaEnvio.uqe_Agencia,
                            uqe_Modulo = consultaEnvio.uqe_Modulo,
                            uqe_Nombre = consultaEnvio.uqe_Nombre,
                            uqe_Puesto = consultaEnvio.uqe_Puesto,
                            StatusGeneral = consultaEnvio.StatusGeneral,
                            InfoConclusion = consultaEnvio.InfoConclusion,
                            StatusAMPO = consultaEnvio.StatusAMPO,
                            InformaAMPO = consultaEnvio.InformaAMPO,
                            oficioAMPO = consultaEnvio.oficioAMPO,
                            RespuestaExpediente = consultaEnvio.RespuestaExpediente,
                            EspontaneoNoEspontaneo = consultaEnvio.EspontaneoNoEspontaneo,
                            PrimeraVezSubsecuente = consultaEnvio.PrimeraVezSubsecuente,
                            ContadorNODerivacion = consultaEnvio.ContadorNODerivacion,
                            FechaRegistro = consultaEnvio.FechaRegistro,
                            FechaCierre = consultaEnvio.FechaCierre,
                            NoSolicitantes = consultaEnvio.NoSolicitantes,
                            ArregloConjunto = consultaEnvio.ArregloConjunto,
                            FirmaInfoAMPO = consultaEnvio.FirmaInfoAMPO,
                            PuestoFirmaAMPO = consultaEnvio.PuestoFirmaAMPO,
                            ArregloRepresentantes = consultaEnvio.ArregloRepresentantes,
                            statusEnvio = consultaEnvio.statusEnvio,
                        };
                        ctx.Add(envio);
                        _context.Remove(consultaEnvio);

                        /* Se hace asi la consulta ya que no existe un identificador clave para diferenciar el historial del registro que se
                            pretende elimnar, y en el caso de que no llegara a existir se hace una segunda busqueda pero con un minutos mas 
                            por la cuestion del desface de tiempo que pudiera llegar a tener */
                        var consultaHistorial = await _context.HistorialCarpetas
                                                                .Where(a => a.RHechoId == model.infoBorrado.rHechoId)
                                                                .Where(a => a.Detalle == "Derivación a Justicia Restaurativa")
                                                                .Where(a => a.Fechasys.Year == fechaRegistroEnvio.Year &&
                                                                            a.Fechasys.Month == fechaRegistroEnvio.Month &&
                                                                            a.Fechasys.Day == fechaRegistroEnvio.Day &&
                                                                            a.Fechasys.Hour == fechaRegistroEnvio.Hour &&
                                                                            a.Fechasys.Minute == fechaRegistroEnvio.Minute)
                                                                .FirstOrDefaultAsync();
                            if (consultaHistorial == null)
                            {
                                // Segunda consulta: Si no encontró exacto, busca con un minuto más
                                consultaHistorial = await _context.HistorialCarpetas
                                                                    .Where(a => a.RHechoId == model.infoBorrado.rHechoId)
                                                                    .Where(a => a.Detalle == "Derivación a Justicia Restaurativa")
                                                                    .Where(a => a.Fechasys.Year == fechaRegistroEnvio.Year &&
                                                                                a.Fechasys.Month == fechaRegistroEnvio.Month &&
                                                                                a.Fechasys.Day == fechaRegistroEnvio.Day &&
                                                                                a.Fechasys.Hour == fechaRegistroEnvio.Hour &&
                                                                                a.Fechasys.Minute == (fechaRegistroEnvio.Minute + 1)) // Un minuto más
                                                                    .FirstOrDefaultAsync();
                            }
                        if (consultaHistorial != null)
                        {
                            LogHistorialCarpeta historial = new LogHistorialCarpeta
                            {
                                LogAdmonId = gLog,
                                IdHistorialcarpetas = consultaHistorial.IdHistorialcarpetas,
                                RHechoId = consultaHistorial.RHechoId,
                                Detalle = consultaHistorial.Detalle,
                                DetalleEtapa = consultaHistorial.DetalleEtapa,
                                Modulo = consultaHistorial.Modulo,
                                Agencia = consultaHistorial.Agencia,
                                UDistrito = consultaHistorial.UDistrito,
                                USubproc = consultaHistorial.USubproc,
                                UAgencia = consultaHistorial.UAgencia,
                                Usuario = consultaHistorial.Usuario,
                                UPuesto = consultaHistorial.UPuesto,
                                UModulo = consultaHistorial.UModulo,
                                Fechasys = consultaHistorial.Fechasys

                            };
                            ctx.Add(historial);
                            _context.Remove(consultaHistorial);
                        }

                        var consultaExpediente = await _context.Expedientes.Where(a => a.IdExpediente == model.infoBorrado.registroId)
                                    .Take(1).FirstOrDefaultAsync();
                        if (consultaExpediente == null)
                        {
                            return Ok(new { res = "Error", men = "No se encontró expediente con la información enviada" });
                        }
                        else
                        {
                            LogExpediente expediente = new LogExpediente
                            {
                                LogAdmonId = gLog,
                                RHechoId = consultaExpediente.RHechoId,
                                Prefijo = consultaExpediente.Prefijo,
                                Cosecutivo = consultaExpediente.Cosecutivo,
                                Ano = consultaExpediente.Año,
                                Sede = consultaExpediente.Sede,
                                NoExpediente = consultaExpediente.NoExpediente,
                                NoDerivacion = consultaExpediente.NoDerivacion,
                                StatusAcepRech = consultaExpediente.StatusAcepRech,
                                InformacionStatus = consultaExpediente.InformacionStatus,
                                FechaRegistroExpediente = consultaExpediente.FechaRegistroExpediente,
                                FechaDerivacion = consultaExpediente.FechaDerivacion
                            };
                            ctx.Add(expediente);
                            _context.Remove(consultaExpediente);

                            // Obtén todos los registros de ConjuntoDerivacion con el mismo IdEnvio
                            var conjuntosAEliminar = await _context.ConjuntoDerivaciones
                                .Where(c => c.EnvioId == idEnvioM)
                                .ToListAsync();

                            if (conjuntosAEliminar == null)
                            {
                                return Ok(new { res = "Error", men = "No se encontró un conjunto de derivaciones" });
                            }
                            else
                            {
                                var sesionEliminar = await _context.Sesions
                                        .Where(c => c.EnvioId == idEnvioM)
                                        .ToListAsync();

                                if (sesionEliminar.Count > 0)
                                {
                                    List<SesionConjunto> sesionesConjuntoEliminar = new List<SesionConjunto>();

                                    mensajeExitoso = "Derivación a Justicia Restaurativa y sesiones eliminados correctamente";

                                    foreach (var item in sesionEliminar)
                                    {
                                        var sesionesConjunto = await _context.SesionConjuntos
                                                    .Where(c => c.SesionId == item.IdSesion)
                                                    .ToListAsync();

                                        sesionesConjuntoEliminar.AddRange(sesionesConjunto);
                                    }

                                    _context.RemoveRange(sesionEliminar);
                                    _context.RemoveRange(sesionesConjuntoEliminar);
                                }

                                _context.RemoveRange(conjuntosAEliminar);

                                // Obtén todos los registros de DelitosDerivados con el mismo IdEnvio
                                var DelitosEliminar = await _context.DelitosDerivados
                                    .Where(c => c.EnvioId == idEnvioM)
                                    .ToListAsync();

                                if (DelitosEliminar == null)
                                {
                                    return Ok(new { res = "Error", men = "No se encontró delitos" });
                                }
                                else
                                {
                                    _context.RemoveRange(DelitosEliminar);

                                    // Obtén todos los registros de SolicitantesRequeridos con el mismo IdEnvio
                                    var SolReqEliminar = await _context.SolicitanteRequeridos
                                        .Where(c => c.EnvioId == idEnvioM)
                                        .ToListAsync();

                                    if (SolReqEliminar == null)
                                    {
                                        return Ok(new { res = "Error", men = "No se encontraron solicitantes requeridos" });
                                    }
                                    else
                                    {
                                        _context.RemoveRange(SolReqEliminar);

                                        var consultaNuc = await _context.RHechoes
                                            .Include(a => a.NUCs)
                                            .Where(a => a.IdRHecho == model.infoBorrado.rHechoId)
                                            .Take(1).FirstOrDefaultAsync();
                                        if (consultaNuc == null)
                                        {
                                            return Ok(new { res = "Error", men = "No se encontró NUC con la información enviada" });
                                        }
                                        else
                                        {
                                            LogNuc nuc = new LogNuc
                                            {
                                                LogAdminId = gLog,
                                                idNuc = consultaNuc.NUCs.idNuc,
                                                Indicador = consultaNuc.NUCs.Indicador,
                                                DistritoId = consultaNuc.NUCs.DistritoId,
                                                CveDistrito = consultaNuc.NUCs.CveDistrito,
                                                DConsecutivo = consultaNuc.NUCs.DConsecutivo,
                                                AgenciaId = consultaNuc.NUCs.AgenciaId,
                                                CveAgencia = consultaNuc.NUCs.CveAgencia,
                                                AConsecutivo = consultaNuc.NUCs.AConsecutivo,
                                                Año = consultaNuc.NUCs.Año,
                                                nucg = consultaNuc.NUCs.nucg,
                                                StatusNUC = consultaNuc.NUCs.StatusNUC,
                                                Etapanuc = consultaNuc.NUCs.Etapanuc
                                            };
                                            ctx.Add(nuc);

                                            consultaNuc.NUCs.Etapanuc = "Inicial";
                                            consultaNuc.NUCs.StatusNUC = "Inicio de la investigación";

                                        }
                                    }
                                }
                            }

                        }
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
            return Ok(new { res = "success", men = mensajeExitoso });
        }
    }
}