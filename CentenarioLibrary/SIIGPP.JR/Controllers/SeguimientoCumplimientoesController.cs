using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using SIIGPP.Datos.Migrations;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_JR.RExpediente;
using SIIGPP.Entidades.M_JR.RSeguimientoCumplimiento;
using SIIGPP.JR.Models.RAcuerdoReparatorio;
using SIIGPP.JR.Models.RAsignacionEnvios;
using SIIGPP.JR.Models.RSeguimientoCumplimiento;


namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguimientoCumplimientoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

            
        public SeguimientoCumplimientoesController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/SeguimientoCumplimientoes
        [HttpGet]
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        public IEnumerable<SeguimientoCumplimiento> GetSeguimientoCumplimientos()
        {
            return _context.SeguimientoCumplimientos;
        }

        // GET: api/SeguimientoCumplimientoes/ObtenerExistenciaConjunto
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{envioid}")]
        public async Task<IActionResult> ObtenerExistenciaConjunto([FromRoute] Guid envioid)
        {
            //Primer filtro para saber si existe por lo menos un acuerdo reparatorio, asi se de un conjunto o de uno sin conjunto
            var searcAcuerdo = await _context.AcuerdoReparatorios
                            .Where(a => a.EnvioId == envioid).
                            ToListAsync();

            //Evalua el resultado y si hay acuerdo hace el siguiente proceso, si no retorna una variable que en front sera evaluada
            if(searcAcuerdo.Count > 0)
            {

                //Esta consulta busca los conjuntos de laa derivacion y caso de no haber se da por entendido que es una derivacion anterior a la implementacion de conjuntos
                var serchConjunto = await _context.AcuerdosConjuntos
                                .Include(a => a.AcuerdoReparatorio)
                                .Include(a => a.ConjuntoDerivaciones)
                                .Where(a => a.AcuerdoReparatorio.EnvioId == envioid)
                                .ToListAsync();

                //Aqui se evalua la existencia de conjuntos, en caso de no haber retorna los valores de SolicitantesRequerids
                if (serchConjunto.Count == 0)
                {
                    //Consulta los resultados de los solicitates y requeridos del expediente
                    var tabla = await _context.SolicitanteRequeridos
                                .Where(v => v.EnvioId == envioid)
                                .Include(v => v.Persona)
                                .ToListAsync();

                    //Consulta los delitos derivados
                    var tabla2 = await _context.DelitosDerivados
                                .Where(v => v.EnvioId == envioid)
                                .Include(v => v.RDH)
                                .Include(v => v.RDH.Delito)
                                .ToListAsync();

                    //Si no hay conjunto quiere decir que solo hay un acuerdo, por tal motivo buscamos dicho acuerdo y lo guardamos en una variable estatica
                    var obtenerAcuerdo = await _context.AcuerdoReparatorios
                                        .Where(x => x.EnvioId == envioid)
                                        .FirstOrDefaultAsync();

                    //Se guarda el valor del acuerdo reparatorio  del expediente, el cual solo debe ser uno, se guarda el id
                    Guid idAcuerdoReparatorio = obtenerAcuerdo.IdAcuerdoReparatorio;

                    //Como es el caso donde no hay conjuto retorno el valor estatico y vacio de un tipo de dato Guid(C#) o UNIQUEIDENTIFIER(SQL) del conjunto
                    //este valor sera util para evaluarlo en front
                    string conjuntoderivacionesid = "00000000-0000-0000-0000-000000000000";

                    //Condicionar los solicitantes y requeridos para que se guarden en determinado valor
                    var resultadosConcatenados = tabla.Select(v => new AcuerdosConjuntosExistenciaViewModel
                    {
                        NombreS = (v.Tipo == "Solicitante" ? v.Persona.Nombre + " " + v.Persona.ApellidoPaterno + " " + v.Persona.ApellidoPaterno + ", " : ""),
                        NombreR = (v.Tipo == "Requerido" ? v.Persona.Nombre + " " + v.Persona.ApellidoPaterno + " " + v.Persona.ApellidoPaterno + ", " : ""),
                    }).ToList();

                    //Lo mismo con los delitos derivados
                    var delitosConcatenados = tabla2.Select(v => new AcuerdosConjuntosExistenciaViewModel
                    {
                        NombreD = v.RDH.Delito.Nombre,
                    }).ToList();

                    // Crear objeto anónimo con propiedades concatenadas y los valores estaticos para retornar
                    var resultadoFinal = new
                    {
                        NombreS = string.Join("", resultadosConcatenados.Select(v => v.NombreS)),
                        NombreR = string.Join("", resultadosConcatenados.Select(v => v.NombreR)),
                        NombreD = string.Join(", ", delitosConcatenados.Select(v => v.NombreD)),
                        AcuerdoReparatorioId = idAcuerdoReparatorio,
                        ConjuntoDerivacionesId = conjuntoderivacionesid,
                    };

                    return Ok(resultadoFinal);
                }
                //En caso de existir conjunto devuelve los valores del conjunto con el id del acuerdo reparatorio que es uno diferente por cada conjunto.
                else
                {

                    return Ok(serchConjunto.Select(a => new AcuerdosConjuntosExistenciaViewModel
                    {
                        //Seguimiento
                        IdAC = a.IdAC,
                        AcuerdoReparatorioId = a.AcuerdoReparatorioId,
                        ConjuntoDerivacionesId = a.ConjuntoDerivacionesId,

                        EnvioId = a.ConjuntoDerivaciones.EnvioId,
                        SolicitadosC = a.ConjuntoDerivaciones.SolicitadosC,
                        RequeridosC = a.ConjuntoDerivaciones.RequeridosC,
                        DelitosC = a.ConjuntoDerivaciones.DelitosC,
                        NombreS = a.ConjuntoDerivaciones.NombreS,
                        DireccionS = a.ConjuntoDerivaciones.DireccionS,
                        TelefonoS = a.ConjuntoDerivaciones.TelefonoS,
                        ClasificacionS = a.ConjuntoDerivaciones.ClasificacionS,
                        NombreR = a.ConjuntoDerivaciones.NombreR,
                        DireccionR = a.ConjuntoDerivaciones.DireccionR,
                        TelefonoR = a.ConjuntoDerivaciones.TelefonoR,
                        ClasificacionR = a.ConjuntoDerivaciones.ClasificacionR,
                        NombreD = a.ConjuntoDerivaciones.NombreD,
                        NoOficio = a.ConjuntoDerivaciones.NoOficio,
                        ResponsableJR = a.ConjuntoDerivaciones.ResponsableJR,
                        NombreSubDirigido = a.ConjuntoDerivaciones.NombreSubDirigido

                    }));
                }


            }
            else
            {

                return Ok(new { NoHayAR = 1 });

            }

            
        }

        // GET: api/SeguimientoCumplimientoes/ObtenerExistenciaConjuntoXDistrito
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador, USAR")]
        [HttpGet("[action]/{envioid}/{distritoId}")]
        public async Task<IActionResult> ObtenerExistenciaConjuntoXDistrito([FromRoute] Guid envioid, Guid distritoId)
        {
            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + distritoId.ToString().ToUpper())).Options;
            using (var ctx = new DbContextSIIGPP(options))
            {
                try
                {

                    //Primer filtro para saber si existe por lo menos un acuerdo reparatorio, asi se de un conjunto o de uno sin conjunto
                    var searcAcuerdo = await ctx.AcuerdoReparatorios
                                    .Where(a => a.EnvioId == envioid).
                                    ToListAsync();

                    //Evalua el resultado y si hay acuerdo hace el siguiente proceso, si no retorna una variable que en front sera evaluada
                    if (searcAcuerdo.Count > 0)
                    {

                        //Esta consulta busca los conjuntos de laa derivacion y caso de no haber se da por entendido que es una derivacion anterior a la implementacion de conjuntos
                        var serchConjunto = await ctx.AcuerdosConjuntos
                                        .Include(a => a.AcuerdoReparatorio)
                                        .Include(a => a.ConjuntoDerivaciones)
                                        .Where(a => a.AcuerdoReparatorio.EnvioId == envioid)
                                        .ToListAsync();

                        //Aqui se evalua la existencia de conjuntos, en caso de no haber retorna los valores de SolicitantesRequerids
                        if (serchConjunto.Count == 0)
                        {
                            //Consulta los resultados de los solicitates y requeridos del expediente
                            var tabla = await ctx.SolicitanteRequeridos
                                        .Where(v => v.EnvioId == envioid)
                                        .Include(v => v.Persona)
                                        .ToListAsync();

                            //Consulta los delitos derivados
                            var tabla2 = await ctx.DelitosDerivados
                                        .Where(v => v.EnvioId == envioid)
                                        .Include(v => v.RDH)
                                        .Include(v => v.RDH.Delito)
                                        .ToListAsync();

                            //Si no hay conjunto quiere decir que solo hay un acuerdo, por tal motivo buscamos dicho acuerdo y lo guardamos en una variable estatica
                            var obtenerAcuerdo = await ctx.AcuerdoReparatorios
                                                .Where(x => x.EnvioId == envioid)
                                                .FirstOrDefaultAsync();

                            //Se guarda el valor del acuerdo reparatorio  del expediente, el cual solo debe ser uno, se guarda el id
                            Guid idAcuerdoReparatorio = obtenerAcuerdo.IdAcuerdoReparatorio;

                            //Como es el caso donde no hay conjuto retorno el valor estatico y vacio de un tipo de dato Guid(C#) o UNIQUEIDENTIFIER(SQL) del conjunto
                            //este valor sera util para evaluarlo en front
                            string conjuntoderivacionesid = "00000000-0000-0000-0000-000000000000";

                            //Condicionar los solicitantes y requeridos para que se guarden en determinado valor
                            var resultadosConcatenados = tabla.Select(v => new AcuerdosConjuntosExistenciaViewModel
                            {
                                NombreS = (v.Tipo == "Solicitante" ? v.Persona.Nombre + " " + v.Persona.ApellidoPaterno + " " + v.Persona.ApellidoPaterno + ", " : ""),
                                NombreR = (v.Tipo == "Requerido" ? v.Persona.Nombre + " " + v.Persona.ApellidoPaterno + " " + v.Persona.ApellidoPaterno + ", " : ""),
                            }).ToList();

                            //Lo mismo con los delitos derivados
                            var delitosConcatenados = tabla2.Select(v => new AcuerdosConjuntosExistenciaViewModel
                            {
                                NombreD = v.RDH.Delito.Nombre,
                            }).ToList();

                            // Crear objeto anónimo con propiedades concatenadas y los valores estaticos para retornar
                            var resultadoFinal = new
                            {
                                NombreS = string.Join("", resultadosConcatenados.Select(v => v.NombreS)),
                                NombreR = string.Join("", resultadosConcatenados.Select(v => v.NombreR)),
                                NombreD = string.Join(", ", delitosConcatenados.Select(v => v.NombreD)),
                                AcuerdoReparatorioId = idAcuerdoReparatorio,
                                ConjuntoDerivacionesId = conjuntoderivacionesid,
                            };

                            return Ok(resultadoFinal);
                        }
                        //En caso de existir conjunto devuelve los valores del conjunto con el id del acuerdo reparatorio que es uno diferente por cada conjunto.
                        else
                        {

                            return Ok(serchConjunto.Select(a => new AcuerdosConjuntosExistenciaViewModel
                            {
                                //Seguimiento
                                IdAC = a.IdAC,
                                AcuerdoReparatorioId = a.AcuerdoReparatorioId,
                                ConjuntoDerivacionesId = a.ConjuntoDerivacionesId,

                                EnvioId = a.ConjuntoDerivaciones.EnvioId,
                                SolicitadosC = a.ConjuntoDerivaciones.SolicitadosC,
                                RequeridosC = a.ConjuntoDerivaciones.RequeridosC,
                                DelitosC = a.ConjuntoDerivaciones.DelitosC,
                                NombreS = a.ConjuntoDerivaciones.NombreS,
                                DireccionS = a.ConjuntoDerivaciones.DireccionS,
                                TelefonoS = a.ConjuntoDerivaciones.TelefonoS,
                                ClasificacionS = a.ConjuntoDerivaciones.ClasificacionS,
                                NombreR = a.ConjuntoDerivaciones.NombreR,
                                DireccionR = a.ConjuntoDerivaciones.DireccionR,
                                TelefonoR = a.ConjuntoDerivaciones.TelefonoR,
                                ClasificacionR = a.ConjuntoDerivaciones.ClasificacionR,
                                NombreD = a.ConjuntoDerivaciones.NombreD,
                                NoOficio = a.ConjuntoDerivaciones.NoOficio,
                                ResponsableJR = a.ConjuntoDerivaciones.ResponsableJR,
                                NombreSubDirigido = a.ConjuntoDerivaciones.NombreSubDirigido

                            }));
                        }


                    }
                    else
                    {

                        return Ok(new { NoHayAR = 1 });

                    }
                }
                catch (Exception ex)
                {
                    var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                    result.StatusCode = 402;
                    return result;
                }
            }

        }

        // GET: api/SeguimientoCumplimientoes/ListarTodosDistrito
        [HttpGet("[action]/{distrito}")]
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, USAR, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        public async Task<IEnumerable<GET_AgendaViewModel>> ListarTodosDistrito([FromRoute] string distrito )
        {
            var Tabla = await _context.SeguimientoCumplimientos
                            .Include(a => a.AcuerdoReparatorio)  
                            .Where(a=> a.AcuerdoReparatorio.uf_Distrito == distrito)
                            .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_AgendaViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_AgendaViewModel
                {
                    //Seguimiento
                    IdSeguimientoCumplimiento  = a.IdSeguimientoCumplimiento,
                    AcuerdoReparatorioId = a.AcuerdoReparatorioId,
                    NoParcialidad = a.NoParcialidad,
                    Fecha = a.Fecha,
                    FechaProrroga = a.FechaProrroga,
                    CantidadAPagar = a.CantidadAPagar,
                    TipoPago = a.TipoPago,
                    ObjectoEspecie = a.ObjectoEspecie,
                    StatusPago = a.StatusPago, 
                    Titulo = a.Titulo,
                    Dirigidoa = a.Dirigidoa,
                    Direccion = a.Direccion,
                    Solicitantes = a.Solicitantes,
                    Requeridos = a.Requeridos, 
                    FechaHoraCita = a.FechaHoraCita,
                    Texto = a.Texto,

                    uf_Distrito = a.uf_Distrito,
                    uf_DirSubProc = a.uf_DirSubProc,
                    uf_Agencia = a.uf_Agencia,
                    uf_Modulo = a.uf_Modulo,
                    uf_Nombre = a.uf_Nombre,
                    uf_Puesto = a.uf_Puesto,

                    //Acuerdos 
                    EnvioId = a.AcuerdoReparatorio.EnvioId,
                    NombreSolicitante = a.AcuerdoReparatorio.NombreSolicitante,
                    NombreRequerdio = a.AcuerdoReparatorio.NombreRequerdio,
                    Delitos = a.AcuerdoReparatorio.Delitos,
                    NUC = a.AcuerdoReparatorio.NUC,
                    NoExpediente = a.AcuerdoReparatorio.NoExpediente,
                    StatusConclusion = a.AcuerdoReparatorio.StatusConclusion,
                    StatusCumplimiento = a.AcuerdoReparatorio.StatusCumplimiento,
                    TipoPagoA = a.AcuerdoReparatorio.TipoPago,
                    MetodoUtilizado = a.AcuerdoReparatorio.MetodoUtilizado,
                    MontoTotal = a.AcuerdoReparatorio.MontoTotal,
                    ObjectoEspecieA = a.AcuerdoReparatorio.ObjectoEspecie,
                    NoTotalParcialidades = a.AcuerdoReparatorio.NoTotalParcialidades,
                    Periodo = a.AcuerdoReparatorio.Periodo,
                    FechaCelebracionAcuerdo = a.AcuerdoReparatorio.FechaCelebracionAcuerdo,
                    FechaLimiteCumplimiento = a.AcuerdoReparatorio.FechaLimiteCumplimiento,
                    StatusRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                    RespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                    FechaHoraRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.FechaHoraRespuestaCoordinadorJuridico,
                    StatusRespuestaAMP = a.AcuerdoReparatorio.StatusRespuestaAMP,
                    RespuestaAMP = a.AcuerdoReparatorio.RespuestaAMP,
                    FechaRespuestaAMP = a.AcuerdoReparatorio.FechaRespuestaAMP,


                });
            }
        }

        // GET: api/SeguimientoCumplimientoes/ListarTodosDistritoStatus
        [HttpGet("[action]/{distrito}/{status}")]
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, USAR,Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        public async Task<IEnumerable<GET_AgendaViewModel>> ListarTodosDistritoStatus([FromRoute] string distrito, string status)
        {
            var Tabla = await _context.SeguimientoCumplimientos
                            .Include(a => a.AcuerdoReparatorio)
                            .Where(a => a.AcuerdoReparatorio.uf_Distrito == distrito)
                            .Where(a => a.StatusPago == status)
                            .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_AgendaViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_AgendaViewModel
                {
                    //Seguimiento
                    IdSeguimientoCumplimiento = a.IdSeguimientoCumplimiento,
                    AcuerdoReparatorioId = a.AcuerdoReparatorioId,
                    NoParcialidad = a.NoParcialidad,
                    Fecha = a.Fecha,
                    FechaProrroga = a.FechaProrroga,
                    CantidadAPagar = a.CantidadAPagar,
                    TipoPago = a.TipoPago,
                    ObjectoEspecie = a.ObjectoEspecie,
                    StatusPago = a.StatusPago,
                    Titulo = a.Titulo,
                    Dirigidoa = a.Dirigidoa,
                    Direccion = a.Direccion,
                    Solicitantes = a.Solicitantes,
                    Requeridos = a.Requeridos,
                    FechaHoraCita = a.FechaHoraCita,
                    Texto = a.Texto,

                    uf_Distrito = a.uf_Distrito,
                    uf_DirSubProc = a.uf_DirSubProc,
                    uf_Agencia = a.uf_Agencia,
                    uf_Modulo = a.uf_Modulo,
                    uf_Nombre = a.uf_Nombre,
                    uf_Puesto = a.uf_Puesto,

                    //Acuerdos 
                    EnvioId = a.AcuerdoReparatorio.EnvioId,
                    NombreSolicitante = a.AcuerdoReparatorio.NombreSolicitante,
                    NombreRequerdio = a.AcuerdoReparatorio.NombreRequerdio,
                    Delitos = a.AcuerdoReparatorio.Delitos,
                    NUC = a.AcuerdoReparatorio.NUC,
                    NoExpediente = a.AcuerdoReparatorio.NoExpediente,
                    StatusConclusion = a.AcuerdoReparatorio.StatusConclusion,
                    StatusCumplimiento = a.AcuerdoReparatorio.StatusCumplimiento,
                    TipoPagoA = a.AcuerdoReparatorio.TipoPago,
                    MetodoUtilizado = a.AcuerdoReparatorio.MetodoUtilizado,
                    MontoTotal = a.AcuerdoReparatorio.MontoTotal,
                    ObjectoEspecieA = a.AcuerdoReparatorio.ObjectoEspecie,
                    NoTotalParcialidades = a.AcuerdoReparatorio.NoTotalParcialidades,
                    Periodo = a.AcuerdoReparatorio.Periodo,
                    FechaCelebracionAcuerdo = a.AcuerdoReparatorio.FechaCelebracionAcuerdo,
                    FechaLimiteCumplimiento = a.AcuerdoReparatorio.FechaLimiteCumplimiento,
                    StatusRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                    RespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                    FechaHoraRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.FechaHoraRespuestaCoordinadorJuridico,
                    StatusRespuestaAMP = a.AcuerdoReparatorio.StatusRespuestaAMP,
                    RespuestaAMP = a.AcuerdoReparatorio.RespuestaAMP,
                    FechaRespuestaAMP = a.AcuerdoReparatorio.FechaRespuestaAMP,


                });
            }
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



        // GET: api/SeguimientoCumplimientoes/ListarTodosDistritoExpediente
        [HttpGet("[action]/{distrito}/{expediente}")]
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico,USAR, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        public async Task<IEnumerable<GET_AgendaViewModel>> ListarTodosDistritoExpediente([FromRoute] string distrito, string expediente)
        {



            string noexpediente = HextoString(expediente);

            var Tabla = await _context.SeguimientoCumplimientos
                            .Include(a => a.AcuerdoReparatorio)
                            .Where(a => a.AcuerdoReparatorio.uf_Distrito == distrito)
                            .Where(a => a.AcuerdoReparatorio.NoExpediente == noexpediente)
                            .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_AgendaViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_AgendaViewModel
                {
                    //Seguimiento
                    IdSeguimientoCumplimiento = a.IdSeguimientoCumplimiento,
                    AcuerdoReparatorioId = a.AcuerdoReparatorioId,
                    NoParcialidad = a.NoParcialidad,
                    Fecha = a.Fecha,
                    FechaProrroga = a.FechaProrroga,
                    CantidadAPagar = a.CantidadAPagar,
                    TipoPago = a.TipoPago,
                    ObjectoEspecie = a.ObjectoEspecie,
                    StatusPago = a.StatusPago,
                    Titulo = a.Titulo,
                    Dirigidoa = a.Dirigidoa,
                    Direccion = a.Direccion,
                    Solicitantes = a.Solicitantes,
                    Requeridos = a.Requeridos,
                    FechaHoraCita = a.FechaHoraCita,
                    Texto = a.Texto,

                    uf_Distrito = a.uf_Distrito,
                    uf_DirSubProc = a.uf_DirSubProc,
                    uf_Agencia = a.uf_Agencia,
                    uf_Modulo = a.uf_Modulo,
                    uf_Nombre = a.uf_Nombre,
                    uf_Puesto = a.uf_Puesto,

                    //Acuerdos 
                    EnvioId = a.AcuerdoReparatorio.EnvioId,
                    NombreSolicitante = a.AcuerdoReparatorio.NombreSolicitante,
                    NombreRequerdio = a.AcuerdoReparatorio.NombreRequerdio,
                    Delitos = a.AcuerdoReparatorio.Delitos,
                    NUC = a.AcuerdoReparatorio.NUC,
                    NoExpediente = a.AcuerdoReparatorio.NoExpediente,
                    StatusConclusion = a.AcuerdoReparatorio.StatusConclusion,
                    StatusCumplimiento = a.AcuerdoReparatorio.StatusCumplimiento,
                    TipoPagoA = a.AcuerdoReparatorio.TipoPago,
                    MetodoUtilizado = a.AcuerdoReparatorio.MetodoUtilizado,
                    MontoTotal = a.AcuerdoReparatorio.MontoTotal,
                    ObjectoEspecieA = a.AcuerdoReparatorio.ObjectoEspecie,
                    NoTotalParcialidades = a.AcuerdoReparatorio.NoTotalParcialidades,
                    Periodo = a.AcuerdoReparatorio.Periodo,
                    FechaCelebracionAcuerdo = a.AcuerdoReparatorio.FechaCelebracionAcuerdo,
                    FechaLimiteCumplimiento = a.AcuerdoReparatorio.FechaLimiteCumplimiento,
                    StatusRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                    RespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                    FechaHoraRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.FechaHoraRespuestaCoordinadorJuridico,
                    StatusRespuestaAMP = a.AcuerdoReparatorio.StatusRespuestaAMP,
                    RespuestaAMP = a.AcuerdoReparatorio.RespuestaAMP,
                    FechaRespuestaAMP = a.AcuerdoReparatorio.FechaRespuestaAMP,


                });
            }
        }




        // GET: api/SeguimientoCumplimientoes/ListarTodosDistritoIdSeg
        [HttpGet("[action]/{distrito}/{idSeguimiento}")]
       // [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, USAR, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        public async Task<IEnumerable<GET_AgendaViewModel>> ListarTodosDistritoIdSeg([FromRoute] string distrito, Guid idSeguimiento)
        {
            var Tabla = await _context.SeguimientoCumplimientos
                            .Include(a => a.AcuerdoReparatorio)
                            .Where(a => a.AcuerdoReparatorio.uf_Distrito == distrito)
                            .Where(a => a.IdSeguimientoCumplimiento == idSeguimiento)
                            .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_AgendaViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_AgendaViewModel
                {
                    //Seguimiento
                    IdSeguimientoCumplimiento = a.IdSeguimientoCumplimiento,
                    AcuerdoReparatorioId = a.AcuerdoReparatorioId,
                    NoParcialidad = a.NoParcialidad,
                    Fecha = a.Fecha,
                    FechaProrroga = a.FechaProrroga,
                    CantidadAPagar = a.CantidadAPagar,
                    TipoPago = a.TipoPago,
                    ObjectoEspecie = a.ObjectoEspecie,
                    StatusPago = a.StatusPago,
                    Titulo = a.Titulo,
                    Dirigidoa = a.Dirigidoa,
                    Direccion = a.Direccion,
                    Solicitantes = a.Solicitantes,
                    Requeridos = a.Requeridos,
                    FechaHoraCita = a.FechaHoraCita,
                    Texto = a.Texto,

                    uf_Distrito = a.uf_Distrito,
                    uf_DirSubProc = a.uf_DirSubProc,
                    uf_Agencia = a.uf_Agencia,
                    uf_Modulo = a.uf_Modulo,
                    uf_Nombre = a.uf_Nombre,
                    uf_Puesto = a.uf_Puesto,

                    //Acuerdos 
                    EnvioId = a.AcuerdoReparatorio.EnvioId,
                    NombreSolicitante = a.AcuerdoReparatorio.NombreSolicitante,
                    NombreRequerdio = a.AcuerdoReparatorio.NombreRequerdio,
                    Delitos = a.AcuerdoReparatorio.Delitos,
                    NUC = a.AcuerdoReparatorio.NUC,
                    NoExpediente = a.AcuerdoReparatorio.NoExpediente,
                    StatusConclusion = a.AcuerdoReparatorio.StatusConclusion,
                    StatusCumplimiento = a.AcuerdoReparatorio.StatusCumplimiento,
                    TipoPagoA = a.AcuerdoReparatorio.TipoPago,
                    MetodoUtilizado = a.AcuerdoReparatorio.MetodoUtilizado,
                    MontoTotal = a.AcuerdoReparatorio.MontoTotal,
                    ObjectoEspecieA = a.AcuerdoReparatorio.ObjectoEspecie,
                    NoTotalParcialidades = a.AcuerdoReparatorio.NoTotalParcialidades,
                    Periodo = a.AcuerdoReparatorio.Periodo,
                    FechaCelebracionAcuerdo = a.AcuerdoReparatorio.FechaCelebracionAcuerdo,
                    FechaLimiteCumplimiento = a.AcuerdoReparatorio.FechaLimiteCumplimiento,
                    StatusRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                    RespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                    FechaHoraRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.FechaHoraRespuestaCoordinadorJuridico,
                    StatusRespuestaAMP = a.AcuerdoReparatorio.StatusRespuestaAMP,
                    RespuestaAMP = a.AcuerdoReparatorio.RespuestaAMP,
                    FechaRespuestaAMP = a.AcuerdoReparatorio.FechaRespuestaAMP,


                });
            }
        }

        //Api para el listado de seguimiento por conjunto
        // GET: api/SeguimientoCumplimientoes/ListarSeguimiento
        [HttpGet("[action]/{acuerdoreparatorioId}")]
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        public async Task<IEnumerable<GET_SeguimientoCumplimientoViewModel>> ListarSeguimiento([FromRoute] Guid acuerdoreparatorioId)
        {
            //En realidad es una api muy sencilla, simplemente muestra el seguimiento del acuerdo  dependiendo el conjunto, cuyo conjunto tiene su propio id de acuerdo
            var Tabla = await _context.SeguimientoCumplimientos
                            .Include(a => a.AcuerdoReparatorio)
                            .Include(a => a.AcuerdoReparatorio.Envio)
                            .Where(a => a.AcuerdoReparatorioId == acuerdoreparatorioId)
                            .ToListAsync();
            //Funciona de la misma manera cuando no hay conjunto, pues muestra el unico acuerdso permitido crear antes de esta actualizacion

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_SeguimientoCumplimientoViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_SeguimientoCumplimientoViewModel
                {
                    IdSeguimientoCumplimiento = a.IdSeguimientoCumplimiento,
                    AcuerdoReparatorioId = a.AcuerdoReparatorioId,
                    NoParcialidad = a.NoParcialidad,
                    Fecha = a.Fecha,
                    FechaProrroga = a.FechaProrroga,
                    CantidadAPagar = a.CantidadAPagar,
                    StatusPago = a.StatusPago,
                    TipoPago = a.TipoPago,
                    ObjectoEspecie = a.ObjectoEspecie,
                    NoExpediente = a.AcuerdoReparatorio.NoExpediente,
                   

                });
            }
        }


        //Api para el listado de seguimiento por conjunto pero en este caso por distrito
        // GET: api/SeguimientoCumplimientoes/ListarSeguimiento
        [HttpGet("[action]/{acuerdoreparatorioId}/{distritoId}")]
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        public async Task<IActionResult> ListarSeguimientoXDistrito([FromRoute] Guid acuerdoreparatorioId, Guid distritoId)
        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + distritoId.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    //En realidad es una api muy sencilla, simplemente muestra el seguimiento del acuerdo  dependiendo el conjunto, cuyo conjunto tiene su propio id de acuerdo
                    var Tabla = await _context.SeguimientoCumplimientos
                                    .Include(a => a.AcuerdoReparatorio)
                                    .Include(a => a.AcuerdoReparatorio.Envio)
                                    .Where(a => a.AcuerdoReparatorioId == acuerdoreparatorioId)
                                    .ToListAsync();
                    //Funciona de la misma manera cuando no hay conjunto, pues muestra el unico acuerdso permitido crear antes de esta actualizacion

                    if (Tabla.Count == 0)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(Tabla.Select(a => new GET_SeguimientoCumplimientoViewModel
                        {
                            IdSeguimientoCumplimiento = a.IdSeguimientoCumplimiento,
                            AcuerdoReparatorioId = a.AcuerdoReparatorioId,
                            NoParcialidad = a.NoParcialidad,
                            Fecha = a.Fecha,
                            FechaProrroga = a.FechaProrroga,
                            CantidadAPagar = a.CantidadAPagar,
                            StatusPago = a.StatusPago,
                            TipoPago = a.TipoPago,
                            ObjectoEspecie = a.ObjectoEspecie,
                            NoExpediente = a.AcuerdoReparatorio.NoExpediente,


                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/SeguimientoCumplimientoes/ListarSeguimientoDistrito
        [HttpGet("[action]/{distrito}/{fi}/{ff}/{statusconclusion}")]
       //[Authorize(Roles = "Director, Administrador, Coordinador, USAR,Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        public async Task<IEnumerable<GET_SeguimientoCumplimientoViewModel>> ListarSeguimientoDistrito([FromRoute] string distrito, DateTime fi, DateTime ff, string statusconclusion)
        {
            var Tabla = await _context.SeguimientoCumplimientos
                            .OrderBy(v => v.NoParcialidad)
                            .Include(a => a.AcuerdoReparatorio) 
                            .Where(a => a.AcuerdoReparatorio.uf_Distrito == distrito)
                            .Where(v => v.Fecha >= fi)
                            .Where(v => v.Fecha <= ff)
                            .Where(v => v.AcuerdoReparatorio.StatusConclusion ==statusconclusion)
                            .ToListAsync();

            if (Tabla.Count == 0)
            {
                return Enumerable.Empty<GET_SeguimientoCumplimientoViewModel>();
            }
            else
            {
                return Tabla.Select(a => new GET_SeguimientoCumplimientoViewModel
                {
                    IdSeguimientoCumplimiento = a.IdSeguimientoCumplimiento,
                    AcuerdoReparatorioId = a.AcuerdoReparatorioId,
                    NoParcialidad = a.NoParcialidad,
                    Fecha = a.Fecha,
                    FechaProrroga = a.FechaProrroga,
                    CantidadAPagar = a.CantidadAPagar,
                    StatusPago = a.StatusPago,
                    TipoPago = a.TipoPago,
                    ObjectoEspecie = a.ObjectoEspecie,
                    NoExpediente = a.AcuerdoReparatorio.NoExpediente,


                });
            }
        }


        // GET: api/SeguimientoCumplimientoes/ListarSeguimientoDistritoGrupo
        [HttpGet("[action]/{distrito}/{fi}/{ff}/{statusconclusion}")]
        //[Authorize(Roles = "Director, Administrador, Coordinador, USAR, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        public async Task<IEnumerable<GET_GrupoStatusViewModel>> ListarSeguimientoDistritoGrupo([FromRoute] string distrito, DateTime fi, DateTime ff, string statusconclusion)
        {
            var Tabla = await _context.SeguimientoCumplimientos
                            .Include(a => a.AcuerdoReparatorio)
                            .Where(a => a.AcuerdoReparatorio.uf_Distrito == distrito) 
                            .ToListAsync();


            var consulta = await _context.SeguimientoCumplimientos
                                         .OrderBy(v=> v.NoParcialidad)
                                         .Include(v => v.AcuerdoReparatorio)
                                         .Where(v => v.AcuerdoReparatorio.uf_Distrito == distrito)
                                         .Where(v => v.Fecha >= fi)
                                         .Where(v => v.Fecha <= ff)
                                         .Where(v => v.AcuerdoReparatorio.StatusConclusion == statusconclusion)
                                         
                                         .GroupBy(v => v.StatusPago) 
                                         .Select(x => new {
                                            Statuspago = x.Key, 
                                            total = x.Sum(v => v.CantidadAPagar),
                                            
                                        }) 
                                        .ToListAsync();


            return consulta.Select(v => new GET_GrupoStatusViewModel
            {
                StatusPago = v.Statuspago,
                SumaTotal = v.total, 
            });

        }


    



        // GET: api/SeguimientoCumplimientoes/Seguimiento
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{idSeguimiento}")]
        public async Task<IActionResult> Seguimiento([FromRoute] Guid idSeguimiento)
        {
            var a = await _context.SeguimientoCumplimientos
                                      .Where(x => x.IdSeguimientoCumplimiento == idSeguimiento) 
                                      .FirstOrDefaultAsync();

            if (a == null)
            {

                return Ok(new { NoHayS = 1 });
            }
            return Ok(new GET_RegistroViewModel
            { 

                StatusPago = a.StatusPago,
                Titulo = a.Titulo,
                Dirigidoa = a.Dirigidoa,
                Direccion = a.Direccion,
                Solicitantes = a.Solicitantes,
                Requeridos = a.Requeridos, 
                FechaHoraCita = a.FechaHoraCita,
                Texto  = a.Texto,
                uf_Distrito  = a.uf_Distrito,
                uf_DirSubProc = a.uf_DirSubProc,
                uf_Agencia = a.uf_Agencia,
                uf_Modulo = a.uf_Modulo,
                uf_Nombre = a.uf_Nombre,
                uf_Puesto = a.uf_Puesto, 

            });
        }






        // GET: api/SeguimientoCumplimientoes/SeguimientoStatus
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{idAcuerdo}")]
        public async Task<IActionResult> SeguimientoStatus([FromRoute] Guid idAcuerdo)
        {
            var a = await _context.SeguimientoCumplimientos
                                      .Where(x => x.AcuerdoReparatorioId == idAcuerdo)
                                      .Where(x => x.StatusPago == "Pendiente")
                                      .FirstOrDefaultAsync();

            if (a == null)
            {

                return Ok(new { NoHayPendiente = 1 });
            }
            else
            {
                return Ok(new { NoHayPendiente = 0 });
            }
           
          
        }



        // PUT: api/SeguimientoCumplimientoes/Actualizar
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] PUT_SeguimientoCumplimientoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sc = await _context.SeguimientoCumplimientos.FirstOrDefaultAsync(a => a.IdSeguimientoCumplimiento == model.IdSeguimientoCumplimiento);

            if (sc == null)
            {
                return NotFound();
            }

            sc.StatusPago = model.StatusPago;
            sc.Titulo = model.Titulo; 
            sc.Dirigidoa = model.Dirigidoa;
            sc.Direccion = model.Direccion;
            sc.Solicitantes = model.Solicitantes;
            sc.Requeridos = model.Requeridos;
            sc.Texto = model.Texto;
            sc.uf_Distrito = model.uf_Distrito;
            sc.uf_DirSubProc = model.uf_DirSubProc;
            sc.uf_Agencia = model.uf_Agencia;
            sc.uf_Modulo = model.uf_Modulo;
            sc.uf_Nombre = model.uf_Nombre;
            sc.uf_Puesto = model.uf_Puesto;
             

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
        // PUT: api/SeguimientoCumplimientoes/ActualizarSP
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarSP([FromBody] PUT_StatuPagoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var sc = await _context.SeguimientoCumplimientos.FirstOrDefaultAsync(a => a.IdSeguimientoCumplimiento == model.IdSeguimientoCumplimiento);

            if (sc == null)
            {
                return NotFound();
            }

            sc.StatusPago = model.StatusPago;
            sc.FechaProrroga = model.FechaProrroga;
            sc.Titulo = model.Titulo; 


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

        //EP para modificar el estatus de pago de los pagos restantes a MODIFICACIÓN Y REVISIÓN por creación de anexo
        //PUT: api/SeguimientoCumplimientoes/ActualizarSPMyR
        [HttpPut("[action]/{conjunto}")]
        public async Task<IActionResult> ActualizarSPMyR([FromRoute] string conjunto)
        {
            try
            {

                Guid con = Guid.Parse(conjunto);

                var a1 = await _context.AcuerdosConjuntos
                    .Where(a => a.ConjuntoDerivacionesId == con)
                    .ToArrayAsync();

                if (a1 == null || !a1.Any())
                {
                    return Ok(new { status = "No se encontro el conjunto" });
                }

                var a2 = await _context.AcuerdoReparatorios
                    .Where(b => b.Anexo == a1[0].AcuerdoReparatorioId && b.StatusAnexo == true)
                    .ToArrayAsync();

                if (a2 == null || !a2.Any())
                {
                    var a3 = await _context.SeguimientoCumplimientos
                        .Where(c => c.AcuerdoReparatorioId == a1[0].AcuerdoReparatorioId && c.StatusPago == "Pendiente")
                        .ToArrayAsync();

                    if (a3 == null || !a3.Any())
                    {
                        return Ok(new { status = "No hay seguimiento del acuerdo" });
                    }

                    foreach (var followup in a3)
                    {
                        followup.StatusPago = "Modificación y revisión";
                    }

                    _context.SaveChanges();

                    return Ok(new { status = "Seguimiento de acuerdo modificado" });
                }

                var a4 = await _context.SeguimientoCumplimientos
                    .Where(d => d.AcuerdoReparatorioId == a2[0].IdAcuerdoReparatorio && d.StatusPago == "Pendiente")
                    .ToArrayAsync();

                if (a4 == null || !a4.Any())
                {
                    return Ok(new { status = "No hay seguimiento del anexo" });
                }

                foreach (var followup in a4)
                {
                    followup.StatusPago = "Modificación y Revisión";
                }

                _context.SaveChanges();
                           
                return Ok(new { status = "Seguimiento de anexo modificado" });                

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        //Nueva api para copnocer el seguimiento que no pago y qui se requiere actualizar el valor de la prorroga cuando hay conjunto
        // GET: api/SeguimientoCumplimientoes/ObtenerIdseguimientoConjunto
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{idconjuntod}")]
        public async Task<IActionResult> ObtenerIdseguimientoConjunto([FromRoute] Guid idconjuntod)
        {
            try
            {
                //Debido a la estructura de los conjuntos y las tablas intermedias no se puede relacionar los acuerdos conjuntos conla tabla de seguimiento, po eso se opto
                //por una consulta SQL en lugar de una LINQ
                string obtenerIdSeguimiento = @"SELECT 
                                                        SC.IdSeguimientoCumplimiento,
                                                        AC.ConjuntoDerivacionesId,
                                                        AC.IdAC,
                                                        AR.IdAcuerdoReparatorio,
                                                        SC.Fecha
                                                    FROM JR_ACUERDOS_CONJUNTOS AS AC
                                                        LEFT JOIN JR_ACUERDOREPARATORIO AS AR ON AR.IdAcuerdoReparatorio = AC.AcuerdoReparatorioId
                                                        LEFT JOIN JR_SEGUIMIENTOCUMPLIMIENTO AS SC ON SC.AcuerdoReparatorioId = AR.IdAcuerdoReparatorio
                                                    WHERE AC.ConjuntoDerivacionesId = @idconjuntod AND SC.StatusPago = 'No pagado'";

                //La siguiente consulta toma ellos id mas importantes con respecto del id del conjunto

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>
                {
                    new SqlParameter("@idconjuntod", idconjuntod)
                };

                //Y ordena el primer pago y este es el seguimiento al que se le actualiza la prorroga
                var obSeg = await _context.ConsultaIdSeguimientoDesdeSesions
                                    .FromSqlRaw(obtenerIdSeguimiento, filtrosBusqueda.ToArray())
                                    .OrderBy(x => x.Fecha) // Ordena por la fecha de forma ascendente
                                    .ToArrayAsync();

                string busquedaRA = @"select * from JR_SEGUIMIENTOCUMPLIMIENTO js where AcuerdoReparatorioId = (select IdAcuerdoReparatorio from JR_ACUERDOREPARATORIO ja where ja.Anexo = @acuerdo and ja.StatusAnexo = 1) and js.StatusPago = 'No pagado'";

                List<SqlParameter> fb = new List<SqlParameter>();
                fb.Add(new SqlParameter("@acuerdo", obSeg[0].IdAcuerdoReparatorio));

                var listaC = await _context.SeguimientoCumplimientos.FromSqlRaw(busquedaRA, fb.ToArray()).ToListAsync();

                if (listaC.Any())
                {                    
                    //Retorno de los valores mas importantes
                    return Ok(new ConsultaIdSeguimientoDSesionViewModel
                    {
                        IdSeguimientoCumplimiento = listaC[0].IdSeguimientoCumplimiento,
                        ConjuntoDerivacionesId = obSeg[0].ConjuntoDerivacionesId,
                        IdAC = obSeg[0].IdAC,
                        IdAcuerdoReparatorio = listaC[0].AcuerdoReparatorioId,
                        Fecha = obSeg[0].Fecha,
                    });
                }
                else
                {                    
                    //Retorno de los valores mas importantes
                    return Ok(new ConsultaIdSeguimientoDSesionViewModel
                    {
                    IdSeguimientoCumplimiento = obSeg[0].IdSeguimientoCumplimiento,
                    ConjuntoDerivacionesId = obSeg[0].ConjuntoDerivacionesId,
                    IdAC = obSeg[0].IdAC,
                    IdAcuerdoReparatorio = obSeg[0].IdAcuerdoReparatorio,
                    Fecha = obSeg[0].Fecha,

                    });
                }
            }
            //Este catch hara que en caso de error te muestre que clase de error en el reponse de la api (Revisar consola) Es muy usado en todo el proyecto pero esa es su funcion.
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/SeguimientoCumplimientoes/SeguimientoStatus
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpGet("[action]/{envioId}")]
        public async Task<IActionResult> ObtenerIdseguimientoSinConjunto([FromRoute] Guid envioId)
        {
            try
            {


                var obSeg = await _context.SeguimientoCumplimientos
                    .Include(a => a.AcuerdoReparatorio.Envio)
                    .Where(a => a.AcuerdoReparatorio.Envio.IdEnvio == envioId)
                    .Where(a => a.StatusPago == "No pagado")
                    .OrderBy(a => a.Fecha)
                    .FirstOrDefaultAsync();
                    

                return Ok(new ConsultaIdSeguimientoDSesionViewModel
                {
                    IdSeguimientoCumplimiento = obSeg.IdSeguimientoCumplimiento,
                    Fecha = obSeg.Fecha,

                });

            }
            //Este catch hara que en caso de error te muestre que clase de error en el reponse de la api (Revisar consola) Es muy usado en todo el proyecto pero esa es su funcion.
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // POST: api/SeguimientoCumplimientoes/Crear
        // API: SE CREA EL ACUERDO REPARATORIO
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] POST_SCRegistroViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }



                SeguimientoCumplimiento SC = new  SeguimientoCumplimiento
                {

                    AcuerdoReparatorioId =model.AcuerdoReparatorioId,
                    NoParcialidad = model.NoParcialidad,
                    Fecha = model.Fecha,
                    CantidadAPagar = model.CantidadAPagar,
                    TipoPago =model.TipoPago,
                    ObjectoEspecie = model.ObjectoEspecie, 
                    StatusPago = "Pendiente",
                };

                _context.SeguimientoCumplimientos.Add(SC);

                

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

        //Api para listar el seguimiento de pagos de todo un distrito
        // GET: api/SeguimientoCumplimientoes/ListarTodosXDistrito
        [HttpGet("[action]/{distritoId}")]
        [Authorize(Roles = "Director, Administrador, Coordinador, USAR")]
        public async Task<IActionResult> ListarTodosXDistrito([FromRoute] Guid distritoId)
        {
            try
            {
                //Por medio de esta conexion se hace la conexion a la ip del otro servidor, comprobar que la cadena de conexiones del back de jr tenga todas las conexiones
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + distritoId.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var Tabla = await _context.SeguimientoCumplimientos
                                .Include(a => a.AcuerdoReparatorio)
                                .Include(a => a.AcuerdoReparatorio.Envio)
                                .Include(a => a.AcuerdoReparatorio.Envio.Expediente)
                                .Where(a => a.AcuerdoReparatorio.Envio.Expediente.DistritoIdDestino == distritoId)
                                .ToListAsync();


                    return Ok(Tabla.Select(a => new GET_AgendaViewModel
                    {
                        //Seguimiento
                        IdSeguimientoCumplimiento = a.IdSeguimientoCumplimiento,
                        AcuerdoReparatorioId = a.AcuerdoReparatorioId,
                        NoParcialidad = a.NoParcialidad,
                        Fecha = a.Fecha,
                        FechaProrroga = a.FechaProrroga,
                        CantidadAPagar = a.CantidadAPagar,
                        TipoPago = a.TipoPago,
                        ObjectoEspecie = a.ObjectoEspecie,
                        StatusPago = a.StatusPago,
                        Titulo = a.Titulo,
                        Dirigidoa = a.Dirigidoa,
                        Direccion = a.Direccion,
                        Solicitantes = a.Solicitantes,
                        Requeridos = a.Requeridos,
                        FechaHoraCita = a.FechaHoraCita,
                        Texto = a.Texto,

                        uf_Distrito = a.uf_Distrito,
                        uf_DirSubProc = a.uf_DirSubProc,
                        uf_Agencia = a.uf_Agencia,
                        uf_Modulo = a.uf_Modulo,
                        uf_Nombre = a.uf_Nombre,
                        uf_Puesto = a.uf_Puesto,

                        //Acuerdos 
                        EnvioId = a.AcuerdoReparatorio.EnvioId,
                        NombreSolicitante = a.AcuerdoReparatorio.NombreSolicitante,
                        NombreRequerdio = a.AcuerdoReparatorio.NombreRequerdio,
                        Delitos = a.AcuerdoReparatorio.Delitos,
                        NUC = a.AcuerdoReparatorio.NUC,
                        NoExpediente = a.AcuerdoReparatorio.NoExpediente,
                        StatusConclusion = a.AcuerdoReparatorio.StatusConclusion,
                        StatusCumplimiento = a.AcuerdoReparatorio.StatusCumplimiento,
                        TipoPagoA = a.AcuerdoReparatorio.TipoPago,
                        MetodoUtilizado = a.AcuerdoReparatorio.MetodoUtilizado,
                        MontoTotal = a.AcuerdoReparatorio.MontoTotal,
                        ObjectoEspecieA = a.AcuerdoReparatorio.ObjectoEspecie,
                        NoTotalParcialidades = a.AcuerdoReparatorio.NoTotalParcialidades,
                        Periodo = a.AcuerdoReparatorio.Periodo,
                        FechaCelebracionAcuerdo = a.AcuerdoReparatorio.FechaCelebracionAcuerdo,
                        FechaLimiteCumplimiento = a.AcuerdoReparatorio.FechaLimiteCumplimiento,
                        StatusRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                        RespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                        FechaHoraRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.FechaHoraRespuestaCoordinadorJuridico,
                        StatusRespuestaAMP = a.AcuerdoReparatorio.StatusRespuestaAMP,
                        RespuestaAMP = a.AcuerdoReparatorio.RespuestaAMP,
                        FechaRespuestaAMP = a.AcuerdoReparatorio.FechaRespuestaAMP,


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

        //Api para listar el seguimiento de pagos de todo un distrito
        // GET: api/SeguimientoCumplimientoes/ListarTodosXDistrito
        [HttpGet("[action]/{distritoId}/{noexpediente}")]
        [Authorize(Roles = "Director, Administrador, Coordinador, USAR")]
        public async Task<IActionResult> ListarTodosXDistritoXExpediente([FromRoute] Guid distritoId, string noexpediente)
        {
            string noExpediente = HextoString(noexpediente);
            try
            {
                //Por medio de esta conexion se hace la conexion a la ip del otro servidor, comprobar que la cadena de conexiones del back de jr tenga todas las conexiones
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + distritoId.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var Tabla = await _context.SeguimientoCumplimientos
                                .Include(a => a.AcuerdoReparatorio)
                                .Include(a => a.AcuerdoReparatorio.Envio)
                                .Include(a => a.AcuerdoReparatorio.Envio.Expediente)
                                .Where(a => a.AcuerdoReparatorio.NoExpediente == noExpediente)
                                .Where(a => a.AcuerdoReparatorio.Envio.Expediente.DistritoIdDestino == distritoId)
                                .ToListAsync();


                    return Ok(Tabla.Select(a => new GET_AgendaViewModel
                    {
                        //Seguimiento
                        IdSeguimientoCumplimiento = a.IdSeguimientoCumplimiento,
                        AcuerdoReparatorioId = a.AcuerdoReparatorioId,
                        NoParcialidad = a.NoParcialidad,
                        Fecha = a.Fecha,
                        FechaProrroga = a.FechaProrroga,
                        CantidadAPagar = a.CantidadAPagar,
                        TipoPago = a.TipoPago,
                        ObjectoEspecie = a.ObjectoEspecie,
                        StatusPago = a.StatusPago,
                        Titulo = a.Titulo,
                        Dirigidoa = a.Dirigidoa,
                        Direccion = a.Direccion,
                        Solicitantes = a.Solicitantes,
                        Requeridos = a.Requeridos,
                        FechaHoraCita = a.FechaHoraCita,
                        Texto = a.Texto,

                        uf_Distrito = a.uf_Distrito,
                        uf_DirSubProc = a.uf_DirSubProc,
                        uf_Agencia = a.uf_Agencia,
                        uf_Modulo = a.uf_Modulo,
                        uf_Nombre = a.uf_Nombre,
                        uf_Puesto = a.uf_Puesto,

                        //Acuerdos 
                        EnvioId = a.AcuerdoReparatorio.EnvioId,
                        NombreSolicitante = a.AcuerdoReparatorio.NombreSolicitante,
                        NombreRequerdio = a.AcuerdoReparatorio.NombreRequerdio,
                        Delitos = a.AcuerdoReparatorio.Delitos,
                        NUC = a.AcuerdoReparatorio.NUC,
                        NoExpediente = a.AcuerdoReparatorio.NoExpediente,
                        StatusConclusion = a.AcuerdoReparatorio.StatusConclusion,
                        StatusCumplimiento = a.AcuerdoReparatorio.StatusCumplimiento,
                        TipoPagoA = a.AcuerdoReparatorio.TipoPago,
                        MetodoUtilizado = a.AcuerdoReparatorio.MetodoUtilizado,
                        MontoTotal = a.AcuerdoReparatorio.MontoTotal,
                        ObjectoEspecieA = a.AcuerdoReparatorio.ObjectoEspecie,
                        NoTotalParcialidades = a.AcuerdoReparatorio.NoTotalParcialidades,
                        Periodo = a.AcuerdoReparatorio.Periodo,
                        FechaCelebracionAcuerdo = a.AcuerdoReparatorio.FechaCelebracionAcuerdo,
                        FechaLimiteCumplimiento = a.AcuerdoReparatorio.FechaLimiteCumplimiento,
                        StatusRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                        RespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                        FechaHoraRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.FechaHoraRespuestaCoordinadorJuridico,
                        StatusRespuestaAMP = a.AcuerdoReparatorio.StatusRespuestaAMP,
                        RespuestaAMP = a.AcuerdoReparatorio.RespuestaAMP,
                        FechaRespuestaAMP = a.AcuerdoReparatorio.FechaRespuestaAMP,


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

        // GET: api/SeguimientoCumplimientoes/ListarTodosDistritoStatus
        [HttpGet("[action]/{distritoId}/{status}")]
        [Authorize(Roles = "Director, Administrador, Coordinador, USAR")]
        public async Task<IActionResult> ListarXDistritoStatus([FromRoute] Guid distritoId, string status)
        {

            try
            {
                //Por medio de esta conexion se hace la conexion a la ip del otro servidor, comprobar que la cadena de conexiones del back de jr tenga todas las conexiones
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + distritoId.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {

                    var Tabla = await _context.SeguimientoCumplimientos
                            .Include(a => a.AcuerdoReparatorio)
                            .Where(a => a.AcuerdoReparatorio.Envio.Expediente.DistritoIdDestino == distritoId)
                            .Where(a => a.StatusPago == status)
                            .ToListAsync();

                    return Ok(Tabla.Select(a => new GET_AgendaViewModel
                    {
                        //Seguimiento
                        IdSeguimientoCumplimiento = a.IdSeguimientoCumplimiento,
                        AcuerdoReparatorioId = a.AcuerdoReparatorioId,
                        NoParcialidad = a.NoParcialidad,
                        Fecha = a.Fecha,
                        FechaProrroga = a.FechaProrroga,
                        CantidadAPagar = a.CantidadAPagar,
                        TipoPago = a.TipoPago,
                        ObjectoEspecie = a.ObjectoEspecie,
                        StatusPago = a.StatusPago,
                        Titulo = a.Titulo,
                        Dirigidoa = a.Dirigidoa,
                        Direccion = a.Direccion,
                        Solicitantes = a.Solicitantes,
                        Requeridos = a.Requeridos,
                        FechaHoraCita = a.FechaHoraCita,
                        Texto = a.Texto,

                        uf_Distrito = a.uf_Distrito,
                        uf_DirSubProc = a.uf_DirSubProc,
                        uf_Agencia = a.uf_Agencia,
                        uf_Modulo = a.uf_Modulo,
                        uf_Nombre = a.uf_Nombre,
                        uf_Puesto = a.uf_Puesto,

                        //Acuerdos 
                        EnvioId = a.AcuerdoReparatorio.EnvioId,
                        NombreSolicitante = a.AcuerdoReparatorio.NombreSolicitante,
                        NombreRequerdio = a.AcuerdoReparatorio.NombreRequerdio,
                        Delitos = a.AcuerdoReparatorio.Delitos,
                        NUC = a.AcuerdoReparatorio.NUC,
                        NoExpediente = a.AcuerdoReparatorio.NoExpediente,
                        StatusConclusion = a.AcuerdoReparatorio.StatusConclusion,
                        StatusCumplimiento = a.AcuerdoReparatorio.StatusCumplimiento,
                        TipoPagoA = a.AcuerdoReparatorio.TipoPago,
                        MetodoUtilizado = a.AcuerdoReparatorio.MetodoUtilizado,
                        MontoTotal = a.AcuerdoReparatorio.MontoTotal,
                        ObjectoEspecieA = a.AcuerdoReparatorio.ObjectoEspecie,
                        NoTotalParcialidades = a.AcuerdoReparatorio.NoTotalParcialidades,
                        Periodo = a.AcuerdoReparatorio.Periodo,
                        FechaCelebracionAcuerdo = a.AcuerdoReparatorio.FechaCelebracionAcuerdo,
                        FechaLimiteCumplimiento = a.AcuerdoReparatorio.FechaLimiteCumplimiento,
                        StatusRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                        RespuestaCoordinadorJuridico = a.AcuerdoReparatorio.StatusRespuestaCoordinadorJuridico,
                        FechaHoraRespuestaCoordinadorJuridico = a.AcuerdoReparatorio.FechaHoraRespuestaCoordinadorJuridico,
                        StatusRespuestaAMP = a.AcuerdoReparatorio.StatusRespuestaAMP,
                        RespuestaAMP = a.AcuerdoReparatorio.RespuestaAMP,
                        FechaRespuestaAMP = a.AcuerdoReparatorio.FechaRespuestaAMP,


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


        private bool SeguimientoCumplimientoExists(Guid id)
        {
            return _context.SeguimientoCumplimientos.Any(e => e.IdSeguimientoCumplimiento == id);
        }

        // GET: api/SeguimientoCumplimientoes/ListAttachedWithFollowUp
        [HttpGet("[action]/{idAgreement}")]
        public async Task<IActionResult> ListAttached([FromRoute] Guid idAgreement)
        {
            try
            {
                var Tabla = await _context.AcuerdoReparatorios
                                          .Where(x => x.Anexo == idAgreement)
                                          .OrderBy(x => x.FechaCelebracionAcuerdo)
                                          .ToListAsync(); // Usar ToListAsync en lugar de ToArray

                if (Tabla == null || !Tabla.Any()) // También revisamos si la lista está vacía
                {
                    return Ok(new { anexos = false });
                }

                return Ok(Tabla.Select(t => new GET_ARJRViewModel
                {
                    IdAcuerdoReparatorio = t.IdAcuerdoReparatorio,
                    EnvioId = t.EnvioId,
                    NombreSolicitante = t.NombreSolicitante,
                    NombreRequerdio = t.NombreRequerdio,
                    Delitos = t.Delitos,
                    NUC = t.NUC,
                    NoExpediente = t.NoExpediente,
                    StatusConclusion = t.StatusConclusion,
                    StatusCumplimiento = t.StatusCumplimiento,
                    TipoPago = t.TipoPago,
                    MetodoUtilizado = t.MetodoUtilizado,
                    MontoTotal = t.MontoTotal,
                    ObjetoEspecie = t.ObjectoEspecie,
                    NoTotalParcialidades = t.NoTotalParcialidades,
                    Periodo = t.Periodo,
                    FechaCelebracionAcuerdo = t.FechaCelebracionAcuerdo,
                    FechaLimiteCumplimiento = t.FechaLimiteCumplimiento,
                    StatusRespuestaCoordinadorJuridico = t.StatusRespuestaCoordinadorJuridico,
                    RespuestaCoordinadorJuridico = t.RespuestaCoordinadorJuridico,
                    FechaHoraRespuestaCoordinadorJuridico = t.FechaHoraRespuestaCoordinadorJuridico,
                    StatusRespuestaAMP = t.StatusRespuestaAMP,
                    RespuestaAMP = t.RespuestaAMP,
                    FechaRespuestaAMP = t.FechaRespuestaAMP,
                    TextoAR = t.TextoAR,
                    uf_Distrito = t.uf_Distrito,
                    uf_DirSubProc = t.uf_DirSubProc,
                    uf_Agencia = t.uf_Agencia,
                    uf_Modulo = t.uf_Modulo,
                    uf_Nombre = t.uf_Nombre,
                    uf_Puesto = t.uf_Puesto,
                    nosise = t.Sise,
                    fechasise = t.Fechasise,
                    TipoDocumento = t.TipoDocumento,
                    MoneyChain = t.MoneyChain,
                    SpeciesChain = t.SpeciesChain,
                    Anexo = t.Anexo,
                    StatusAnexo = t.StatusAnexo,
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }


        //GET: api/SeguimientoCumplimientoes/ValidateCJMMP
        [HttpGet("[action]/{acuerdoId}")]
        public async Task<IActionResult> ValidateCJMMP([FromRoute] Guid acuerdoId)
        {
            try
            {
                var anexo = await _context.AcuerdoReparatorios
                            .Where(x => x.Anexo == acuerdoId)
                            .Where(x => x.StatusAnexo == true)                            
                            .ToArrayAsync();

                if(anexo == null && anexo.Count() == 0)
                {
                    return Ok(new { status = false });
                }
                else
                {
                    if (anexo[0].StatusRespuestaCoordinadorJuridico != "Autorizado" && anexo[0].StatusRespuestaAMP != "Autorizado")
                    {
                        return Ok(new { status = false });
                    }else
                    {
                        return Ok(new { status = true });
                    }
                }
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