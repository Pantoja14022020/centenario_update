using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.RDHechos;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.RDHecho;
using SIIGPP.CAT.FilterClass;
using SIIGPP.Entidades.M_Administracion;
using Microsoft.Data.SqlClient;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RDHsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public RDHsController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/RDHs/ListarPorHecho
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-IL,Recepción")]
        [HttpGet("[action]/{rhechoid}")]
        public async Task<IActionResult> ListarPorHecho([FromRoute] Guid rhechoid)
        {

            try
            {

                var delitos = await _context.RDHs
                              .Include(a => a.Delito)
                              .Include(a => a.RHecho)
                              
                              .Where(a => a.RHechoId == rhechoid)
                              .Where(a => a.Equiparado)
                              .ToListAsync();

                return Ok(delitos.Select(a => new RDHechosViewModel
                {
                    IdRDH = a.IdRDH,
                    RHechoId = a.RHechoId,

                    DelitoId = a.DelitoId,
                    nombreDelito = a.Delito.Nombre,
                    OfiNoOfic = a.Delito.OfiNoOfi,
                    altoImpacto = a.Delito.AltoImpacto,
                    suceptibleMASC = a.Delito.SuceptibleMASC,

                    TipoRobado = a.TipoRobado,
                    TipoDeclaracion = a.TipoDeclaracion,
                    ViolenciaSinViolencia = a.ViolenciaSinViolencia,
                    ArmaBlanca = a.ArmaBlanca,
                    ArmaFuego = a.ArmaFuego,
                    ClasificaOrdenResult = a.ClasificaOrdenResult,
                    ConAlgunaParteCuerpo = a.ConAlgunaParteCuerpo,
                    Concurso = a.Concurso,
                    ConotroElemento = a.ConotroElemento,
                    Equiparado = a.Equiparado,
                    GraveNoGrave = a.GraveNoGrave,
                    IntensionDelito = a.IntensionDelito,
                    MontoRobado = a.MontoRobado,
                    ResultadoDelito = a.ResultadoDelito,
                    Tipo = a.Tipo,
                    TipoFuero = a.TipoFuero,
                    Observaciones = a.Observaciones,
                    Fechasys = a.Fechasys,
                    Hipotesis = a.Hipotesis,
                    DelitoEspecifico = a.DelitoEspecifico,
                  


                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }


        }

        // GET: api/RDHs/ListarPorHechoDE
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}/{idMProteccion}")]
        public async Task<IActionResult> ListarPorHechoDE([FromRoute] Guid RHechoId, Guid idMProteccion)

        {

            try
            {

                String busquedaDelitosM = @"select 
                                                r.IdRDH,
                                                r.RHechoId,
                                                r.DelitoId,
                                                d.Nombre,
                                                d.OfiNoOfi,
                                                d.AltoImpacto,
                                                d.SuceptibleMASC,
                                                r.TipoRobado,
                                                r.TipoDeclaracion,
                                                r.ViolenciaSinViolencia,
                                                r.ArmaBlanca,
                                                r.ArmaFuego,
                                                r.ClasificaOrdenResult,
                                                r.ConAlgunaParteCuerpo,
                                                r.Concurso,
                                                r.ConotroElemento,
                                                r.Equiparado,
                                                r.GraveNoGrave,
                                                r.IntensionDelito,
                                                r.MontoRobado,
                                                r.ResultadoDelito,
                                                r.Tipo,
                                                r.TipoFuero,
                                                r.Observaciones,
                                                r.Fechasys,
                                                r.Hipotesis,
                                                r.DelitoEspecifico
                                                from CAT_RDH as r
                                                left join C_DELITO as d on d.IdDelito = r.DelitoId
                                                left join CAT_RHECHO as h on h.IdRHecho = r.RHechoId
                                                left join CAT_MEDIDASPROTECCION as m on m.RHechoId = r.RHechoId and
                                                m.Delito like d.Nombre+', %' 
                                                OR m.Delito like '%, %'+d.Nombre
                                                OR m.Delito like d.Nombre
                                                OR m.Delito like '%, %'+d.Nombre+'%, %'
                                                where h.IdRHecho= @rhechoid and m.IdMProteccion = @idmproteccion";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@rhechoid", RHechoId));
                filtrosBusqueda.Add(new SqlParameter("@idmproteccion", idMProteccion));
                var rap = await _context.qBusquedaDelitoM.FromSqlRaw(busquedaDelitosM, filtrosBusqueda.ToArray()).ToListAsync();

                return Ok(rap.Select(a => new RDHechosViewModelM
                {
                    IdRDH = a.IdRDH,
                    RHechoId = a.RHechoId,

                    DelitoId = a.DelitoId,
                    Nombre = a.Nombre,
                    OfiNoOfi = a.OfiNoOfi,
                    AltoImpacto = a.AltoImpacto,
                    SuceptibleMASC = a.SuceptibleMASC,

                    TipoRobado = a.TipoRobado,
                    TipoDeclaracion = a.TipoDeclaracion,
                    ViolenciaSinViolencia = a.ViolenciaSinViolencia,
                    ArmaBlanca = a.ArmaBlanca,
                    ArmaFuego = a.ArmaFuego,
                    ClasificaOrdenResult = a.ClasificaOrdenResult,
                    ConAlgunaParteCuerpo = a.ConAlgunaParteCuerpo,
                    Concurso = a.Concurso,
                    ConotroElemento = a.ConotroElemento,
                    Equiparado = a.Equiparado,
                    GraveNoGrave = a.GraveNoGrave,
                    IntensionDelito = a.IntensionDelito,
                    MontoRobado = a.MontoRobado,
                    ResultadoDelito = a.ResultadoDelito,
                    Tipo = a.Tipo,
                    TipoFuero = a.TipoFuero,
                    Observaciones = a.Observaciones,
                    Fechasys = a.Fechasys,
                    Hipotesis = a.Hipotesis,
                    DelitoEspecifico = a.DelitoEspecifico,



                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }


        }

        // POST: api/RDHs/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear(CrearViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            RDH InsertarRDH = new RDH
            {

                RHechoId = model.RHechoId,
                DelitoId = model.DelitoId,
                TipoFuero = model.TipoFuero,
                TipoDeclaracion = model.TipoDeclaracion,
                ResultadoDelito = model.ResultadoDelito,
                GraveNoGrave = model.GraveNoGrave,
                IntensionDelito = model.IntensionDelito,
                ViolenciaSinViolencia = model.ViolenciaSinViolencia,
                Equiparado = model.Equiparado,
                Tipo = model.Tipo,
                Concurso = model.Concurso,
                ClasificaOrdenResult = model.ClasificaOrdenResult,
                ArmaFuego = model.ArmaFuego,
                ArmaBlanca = model.ArmaBlanca,
                ConAlgunaParteCuerpo = model.ConAlgunaParteCuerpo,
                ConotroElemento = model.ConotroElemento,
                TipoRobado = model.TipoRobado,
                MontoRobado = model.MontoRobado,
                Observaciones = model.Observaciones,
                Fechasys = System.DateTime.Now,
                Hipotesis = model.Hipotesis,
                DelitoEspecifico = model.DelitoEspecifico,
                TipoViolencia = model.TipoViolencia,
                SubtipoSexual = model.SubtipoSexual,
                TipoInfoDigital = model.TipoInfoDigital,
                MedioDigital = model.MedioDigital,
                InstrumentosComision = model.InstrumentosComision,
                GradoDelito = model.GradoDelito

            };

            _context.RDHs.Add(InsertarRDH);



            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }
            return Ok();
            //return Ok(new { idRAH = InsertarRAH.IdRAH, idRH = InsertarRH.IdRHecho });
        }

        // GET: api/RDHs/ListarPorHechoMASC
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rhechoid}")]
        public async Task<IEnumerable<RDHechosViewModel>> ListarPorHechoMASC([FromRoute] Guid rhechoid)
        {
            var delitos = await _context.RDHs
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho)
                          .Where(a => a.Delito.SuceptibleMASC == true)
                          .Where(a => a.Equiparado)
                          .Where(a => a.RHechoId == rhechoid).ToListAsync();

            return delitos.Select(a => new RDHechosViewModel
            {
                IdRDH = a.IdRDH,
                RHechoId = a.RHechoId,

                DelitoId = a.DelitoId,
                nombreDelito = a.Delito.Nombre,
                OfiNoOfic = a.Delito.OfiNoOfi,
                altoImpacto = a.Delito.AltoImpacto,
                suceptibleMASC = a.Delito.SuceptibleMASC,

                TipoRobado = a.TipoRobado,
                TipoDeclaracion = a.TipoDeclaracion,
                ViolenciaSinViolencia = a.ViolenciaSinViolencia,
                ArmaBlanca = a.ArmaBlanca,
                ArmaFuego = a.ArmaFuego,
                ClasificaOrdenResult = a.ClasificaOrdenResult,
                ConAlgunaParteCuerpo = a.ConAlgunaParteCuerpo,
                Concurso = a.Concurso,
                ConotroElemento = a.ConotroElemento,
                Equiparado = a.Equiparado,
                GraveNoGrave = a.GraveNoGrave,
                IntensionDelito = a.IntensionDelito,
                MontoRobado = a.MontoRobado,
                ResultadoDelito = a.ResultadoDelito,
                Tipo = a.Tipo,
                TipoFuero = a.TipoFuero,
                Observaciones = a.Observaciones,
                Hipotesis = a.Hipotesis,
                //PersonaId = a.PersonaId,
                //nombreImputado = a.Personas.Nombre + ' ' + a.Personas.ApellidoPaterno + ' ' + a.Personas.ApellidoMaterno,


            });

        }

        // PUT: api/RDHs/ActualizarEquiparadosActivar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarEquiparadosActivar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var rd = await _context.RDHs.FirstOrDefaultAsync(a => a.IdRDH == model.IdRDH);

            if (rd == null)
            {
                return NotFound();
            }

            rd.Equiparado = model.Equiparado;

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

        // PUT: api/RDHs/ActualizarEquiparadosDesactivar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarEquiparadosDesactivar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var rd = await _context.RDHs.FirstOrDefaultAsync(a => a.IdRDH == model.IdRDH);

            if (rd == null)
            {
                return NotFound();
            }

            rd.Equiparado = model.Equiparado;

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

        // PUT: api/RDHs/Actualizar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarFulViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var rd = await _context.RDHs.FirstOrDefaultAsync(a => a.IdRDH == model.IdRDH);

            if (rd == null)
            {
                return NotFound();
            }

            rd.DelitoId = model.DelitoId;
            rd.TipoFuero = model.TipoFuero;
            rd.ResultadoDelito = model.ResultadoDelito;
            rd.TipoDeclaracion = model.TipoDeclaracion;
            rd.GraveNoGrave = model.GraveNoGrave;
            rd.IntensionDelito = model.IntensionDelito;
            rd.ViolenciaSinViolencia = model.ViolenciaSinViolencia;
            rd.Tipo = model.Tipo;
            rd.Concurso = model.Concurso;
            rd.ClasificaOrdenResult = model.ClasificaOrdenResult;
            rd.ArmaBlanca = model.ArmaBlanca;
            rd.ArmaFuego = model.ArmaFuego;
            rd.TipoRobado = model.TipoRobado;
            rd.MontoRobado = model.MontoRobado;
            rd.Observaciones = model.Observaciones;
            rd.DelitoEspecifico = model.DelitoEspecifico;
            rd.TipoViolencia = model.TipoViolencia;
            rd.SubtipoSexual = model.SubtipoSexual;
            rd.TipoInfoDigital = model.TipoInfoDigital;
            rd.MedioDigital = model.MedioDigital;
            rd.ConAlgunaParteCuerpo = model.ConAlgunaParteCuerpo;
            rd.ConotroElemento = model.ConotroElemento;
            rd.InstrumentosComision = model.InstrumentosComision;
            rd.GradoDelito = model.GradoDelito;

            //rd.PersonaId = model.PersonaId;
            


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


        // GET: api/RDHs/ListarEstadistica
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{fechai}/{fechaf}/{distrito}/{status}/{delito}/{modalidad}/{grado}/{tiporobado}/{violencia}/{concuerpo}/{conotrolemento}/{armafuego}/{armablanca}/{fechhrinicio}/{fechhrfin}")]
        public async Task<IEnumerable<EstadisticaViewModel>> ListarEstadistica([FromRoute] DateTime fechai, DateTime fechaf, string distrito, string status, string delito, string modalidad, string grado, string tiporobado, string violencia, string concuerpo, string conotrolemento, string armafuego, string armablanca, DateTime fechhrinicio, DateTime fechhrfin)
        {
            Boolean armafuego2 = false;
            Boolean armablanca2 = false;

            if (armafuego == "Si")
                armafuego2 = true;
            else
                armafuego2 = false;

            if (armablanca == "Si")
                armablanca2 = true;
            else
                armablanca2 = false;


            var delitos = await _context.RDHs
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho)
                          .Where(a => a.Equiparado == true)
                          .Where(a => a.RHecho.FechaElevaNuc2 >= fechai)
                          .Where(a => a.RHecho.FechaElevaNuc2 <= fechaf)
                          .Where(a => (distrito != "ZKR" ? a.RHecho.RAtencion.DistritoInicial == distrito : a.RHecho.RAtencion.DistritoInicial != distrito))
                          .Where(a => (status != "ZKR" ? a.RHecho.NUCs.StatusNUC == status : a.RHecho.NUCs.StatusNUC != status))
                          .Where(a => (delito != "ZKR" ? a.Delito.Nombre == delito : a.Delito.Nombre != delito))
                          .Where(a => (modalidad != "ZKR" ? a.Tipo == modalidad : a.Tipo != modalidad))
                          .Where(a => (grado != "ZKR" ? a.ResultadoDelito == grado : a.ResultadoDelito != grado))
                          .Where(a => (tiporobado != "ZKR" ? a.TipoRobado == tiporobado : a.TipoRobado != tiporobado))
                          .Where(a => (violencia != "ZKR" ? a.ViolenciaSinViolencia == violencia : a.ViolenciaSinViolencia != violencia))
                          .Where(a => (concuerpo != "ZKR" ? a.ConAlgunaParteCuerpo == concuerpo : a.ConAlgunaParteCuerpo != concuerpo))
                          .Where(a => (conotrolemento != "ZKR" ? a.ConotroElemento == conotrolemento : a.ConotroElemento != conotrolemento))
                          .Where(a => (armafuego != "ZKR" ? a.ArmaFuego == armafuego2 : (a.ArmaFuego == true || a.ArmaFuego == false)))
                          .Where(a => (armablanca != "ZKR" ? a.ArmaBlanca == armablanca2 : (a.ArmaBlanca == true || a.ArmaBlanca == false)))
                          .Where(a => a.RHecho.FechaHoraSuceso2.TimeOfDay >= fechhrinicio.TimeOfDay && a.RHecho.FechaHoraSuceso2.TimeOfDay <= fechhrfin.TimeOfDay)
                          .Where(a => a.RHecho.FechaHoraSuceso2.Date >= fechhrinicio.Date && a.RHecho.FechaHoraSuceso2.Date <= fechhrfin.Date)
                          .Where(a => a.RHecho.Status == true)
                          .ToListAsync();

            return delitos.Select(a => new EstadisticaViewModel
            {
                RHechoId = a.RHechoId,
                Delito = a.Delito.Nombre,
                TipoRobado = a.TipoRobado,
                ConAlgunaParteCuerpo = a.ConAlgunaParteCuerpo,
                ModalidadesDelito = a.Tipo,
                ViolenciaSinViolencia = a.ViolenciaSinViolencia,
                ConotroElemento = a.ConotroElemento,
                GradoEjecucion = a.ResultadoDelito,
                ArmaFuego = a.ArmaFuego,
                ArmaBlanca = a.ArmaBlanca
            });

        }


        // GET: api/RDHs/FeminicidiosVictimas
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> FeminicidiosVictimas([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("FEMINICIDIO"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas de feminicidio", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/HomicidiosDolosos
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IEnumerable<EstadisticaMesesAñoViewModel>> HomicidiosDolosos([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho)
                          .Where(a => a.Equiparado == true)
                          .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                          .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                          .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                          .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                          .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                          .Where(a => a.Delito.Nombre.Contains("HOMICIDIO") && a.IntensionDelito == "Doloso")
                          .GroupBy(v => v.RHecho.FechaHoraSuceso2.Year)
                          .Select(x => new {
                              etiqueta = "Homicidios Dolosos",
                              enero = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 01),
                              febrero = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 02),
                              marzo = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 03),
                              abril = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 04),
                              mayo = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 05),
                              junio = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 06),
                              julio = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 07),
                              agosto = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 08),
                              semptiembre = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 09),
                              octubre = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 10),
                              noviembre = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 11),
                              diciembre = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 12),
                          })
                          .ToListAsync();


            return delitos.Select(v => new EstadisticaMesesAñoViewModel
            {
                Delito = v.etiqueta,
                Enero = v.enero,
                Febrero = v.febrero,
                Marzo = v.marzo,
                Abril = v.abril,
                Mayo = v.mayo,
                Junio = v.junio,
                Julio = v.julio,
                Agosto = v.agosto,
                Septiembre = v.semptiembre,
                Octubre = v.octubre,
                Noviembre = v.noviembre,
                Diciembre = v.diciembre,
                Total = v.enero + v.febrero + v.marzo + v.abril + v.mayo + v.junio + v.julio + v.agosto + v.semptiembre + v.octubre + v.noviembre + v.diciembre

            });

        }


        // GET: api/RDHs/HomicidiosDolososMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> HomicidiosDolososMujeres([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("HOMICIDIO") && a.IntensionDelito == "Doloso")
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas homicidios dolosos(mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }

        // GET: api/RDHs/HomicidiosCulposos
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IEnumerable<EstadisticaMesesAñoViewModel>> HomicidiosCulposos([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho)
                          .Where(a => a.Equiparado == true)
                          .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                          .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                          .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                          .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                          .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                          .Where(a => a.Delito.Nombre.Contains("HOMICIDIO") && a.IntensionDelito == "Culposo")
                          .GroupBy(v => v.RHecho.FechaHoraSuceso2.Year)
                          .Select(x => new {
                              etiqueta = "Homicidios Culposos",
                              enero = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 01),
                              febrero = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 02),
                              marzo = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 03),
                              abril = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 04),
                              mayo = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 05),
                              junio = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 06),
                              julio = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 07),
                              agosto = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 08),
                              semptiembre = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 09),
                              octubre = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 10),
                              noviembre = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 11),
                              diciembre = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 12),
                          })
                          .ToListAsync();


            return delitos.Select(v => new EstadisticaMesesAñoViewModel
            {
                Delito = v.etiqueta,
                Enero = v.enero,
                Febrero = v.febrero,
                Marzo = v.marzo,
                Abril = v.abril,
                Mayo = v.mayo,
                Junio = v.junio,
                Julio = v.julio,
                Agosto = v.agosto,
                Septiembre = v.semptiembre,
                Octubre = v.octubre,
                Noviembre = v.noviembre,
                Diciembre = v.diciembre,
                Total = v.enero + v.febrero + v.marzo + v.abril + v.mayo + v.junio + v.julio + v.agosto + v.semptiembre + v.octubre + v.noviembre + v.diciembre

            });

        }

        // GET: api/RDHs/HomicidiosCulpososMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> HomicidiosCulpososMujeres([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("HOMICIDIO") && a.IntensionDelito == "Culposo")
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas homicidios culposos(mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/Secuestros
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IEnumerable<EstadisticaMesesAñoViewModel>> Secuestros([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho)
                          .Where(a => a.Equiparado == true)
                          .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                          .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                          .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                          .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                          .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                          .Where(a => a.Delito.Nombre.Contains("SECUESTRO"))
                          .GroupBy(v => v.RHecho.FechaHoraSuceso2.Year)
                          .Select(x => new {
                              etiqueta = "Secuestros",
                              enero = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 01),
                              febrero = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 02),
                              marzo = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 03),
                              abril = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 04),
                              mayo = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 05),
                              junio = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 06),
                              julio = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 07),
                              agosto = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 08),
                              semptiembre = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 09),
                              octubre = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 10),
                              noviembre = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 11),
                              diciembre = x.Count(v => v.RHecho.FechaHoraSuceso2.Month == 12),
                          })
                          .ToListAsync();


            return delitos.Select(v => new EstadisticaMesesAñoViewModel
            {
                Delito = v.etiqueta,
                Enero = v.enero,
                Febrero = v.febrero,
                Marzo = v.marzo,
                Abril = v.abril,
                Mayo = v.mayo,
                Junio = v.junio,
                Julio = v.julio,
                Agosto = v.agosto,
                Septiembre = v.semptiembre,
                Octubre = v.octubre,
                Noviembre = v.noviembre,
                Diciembre = v.diciembre,
                Total = v.enero + v.febrero + v.marzo + v.abril + v.mayo + v.junio + v.julio + v.agosto + v.semptiembre + v.octubre + v.noviembre + v.diciembre

            });

        }

        // GET: api/RDHs/SecuestrossMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> SecuestrossMujeres([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("SECUESTRO"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas Secuestros(mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }

        // GET: api/RDHs/Extorsiones
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> Extorsiones([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("EXTORSIÓN"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .Include(a => a.Persona)
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas Extorsiones", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }

        // GET: api/RDHs/ExtosionesMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> ExtosionesMujeres([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("EXTORSIÓN"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas Extorsiones(mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }

        // GET: api/RDHs/LesionesDolosos
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> LesionesDolosos([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("LESIONES") && a.IntensionDelito == "Doloso")
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .Include(a => a.Persona)
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas Lesiones dolosas", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/LesionesDolososMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> LesionesDolososMujeres([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("LESIONES") && a.IntensionDelito == "Doloso")
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas Lesiones dolosas(mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }

        // GET: api/RDHs/LesionesCulposo
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> LesionesCulposo([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("LESIONES") && a.IntensionDelito == "Culposo")
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .Include(a => a.Persona)
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas Lesiones culposas", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/LesionesCulposasMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> LesionesCulposasMujeres([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("LESIONES") && a.IntensionDelito == "Culposo")
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas Lesiones culposas(mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }

        // GET: api/RDHs/CorrupcionMenores
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> CorrupcionMenores([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("CORRUPCIÓN DE MENORES"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .Include(a => a.Persona)
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas Corrupción de menores", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/CorrupcionMenoresMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> CorrupcionMenoresMujeres([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("CORRUPCIÓN DE MENORES"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas Corrupción de menores(mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }

        // GET: api/RDHs/TrataPersonas
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> TrataPersonas([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("TRATA DE PERSONAS"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .Include(a => a.Persona)
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas Trata de personas", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/TrataPersonasMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{anoi}/{anof}/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}")]
        public async Task<IActionResult> TrataPersonasMujeres([FromRoute] DateTime anoi, DateTime anof, Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= anoi.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= anof.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("TRATA DE PERSONAS"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Victimas Trata de personsas(mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }



        // GET: api/RDHs/AbusoSexualCarpetas
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> AbusoSexualCarpetas([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("ABUSO SEXUAL"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {

                if (registro.mes == 01) enero++;
                else if (registro.mes == 02) febrero++;
                else if (registro.mes == 03) marzo++;
                else if (registro.mes == 04) abril++;
                else if (registro.mes == 05) mayo++;
                else if (registro.mes == 06) junio++;
                else if (registro.mes == 07) julio++;
                else if (registro.mes == 08) agosto++;
                else if (registro.mes == 09) septiembre++;
                else if (registro.mes == 10) octubre++;
                else if (registro.mes == 11) noviembre++;
                else if (registro.mes == 12) diciembre++;

                total++;


            }

            return Ok(new { delito = "Abuso Sexual(Carpetas Iniciadas)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/AbusoSexualMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> AbusoSexualMujeres([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("ABUSO SEXUAL"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Abuso sexual(Victimas mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/AcosoSexualCarpetas
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> AcosoSexualCarpetas([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("ACOSO SEXUAL"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {

                if (registro.mes == 01) enero++;
                else if (registro.mes == 02) febrero++;
                else if (registro.mes == 03) marzo++;
                else if (registro.mes == 04) abril++;
                else if (registro.mes == 05) mayo++;
                else if (registro.mes == 06) junio++;
                else if (registro.mes == 07) julio++;
                else if (registro.mes == 08) agosto++;
                else if (registro.mes == 09) septiembre++;
                else if (registro.mes == 10) octubre++;
                else if (registro.mes == 11) noviembre++;
                else if (registro.mes == 12) diciembre++;

                total++;


            }

            return Ok(new { delito = "Acoso Sexual(Carpetas Iniciadas)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/AcosoSexualMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> AcosoSexualMujeres([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("ACOSO SEXUAL"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Acoso sexual(Victimas mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }

        // GET: api/RDHs/HostigamientoSexualCarpetas
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> HostigamientoSexualCarpetas([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("HOSTIGAMIENTO SEXUAL"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {

                if (registro.mes == 01) enero++;
                else if (registro.mes == 02) febrero++;
                else if (registro.mes == 03) marzo++;
                else if (registro.mes == 04) abril++;
                else if (registro.mes == 05) mayo++;
                else if (registro.mes == 06) junio++;
                else if (registro.mes == 07) julio++;
                else if (registro.mes == 08) agosto++;
                else if (registro.mes == 09) septiembre++;
                else if (registro.mes == 10) octubre++;
                else if (registro.mes == 11) noviembre++;
                else if (registro.mes == 12) diciembre++;

                total++;


            }

            return Ok(new { delito = "Hostigamiento Sexual(Carpetas Iniciadas)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/HostigamientoSexualMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> HostigamientoSexualMujeres([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("HOSTIGAMIENTO SEXUAL"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Hostigamiento sexual(Victimas mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }

        // GET: api/RDHs/ViolacionSimpleCarpetas
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> ViolacionSimpleCarpetas([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("VIOLACION"))
                .Where(a => a.Tipo == "Simple")
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {

                if (registro.mes == 01) enero++;
                else if (registro.mes == 02) febrero++;
                else if (registro.mes == 03) marzo++;
                else if (registro.mes == 04) abril++;
                else if (registro.mes == 05) mayo++;
                else if (registro.mes == 06) junio++;
                else if (registro.mes == 07) julio++;
                else if (registro.mes == 08) agosto++;
                else if (registro.mes == 09) septiembre++;
                else if (registro.mes == 10) octubre++;
                else if (registro.mes == 11) noviembre++;
                else if (registro.mes == 12) diciembre++;

                total++;


            }

            return Ok(new { delito = "Violación simple(Carpetas Iniciadas)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/ViolacionSimpleMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> ViolacionSimpleMujeres([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("VIOLACION"))
                .Where(a => a.Tipo == "Simple")
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Violacion simple(Victimas mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/ViolacionEquiparadaCarpetas
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> ViolacionEquiparadaCarpetas([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("VIOLACION"))
                .Where(a => a.Tipo == "Equiparado")
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {

                if (registro.mes == 01) enero++;
                else if (registro.mes == 02) febrero++;
                else if (registro.mes == 03) marzo++;
                else if (registro.mes == 04) abril++;
                else if (registro.mes == 05) mayo++;
                else if (registro.mes == 06) junio++;
                else if (registro.mes == 07) julio++;
                else if (registro.mes == 08) agosto++;
                else if (registro.mes == 09) septiembre++;
                else if (registro.mes == 10) octubre++;
                else if (registro.mes == 11) noviembre++;
                else if (registro.mes == 12) diciembre++;

                total++;


            }

            return Ok(new { delito = "Violación equipada(Carpetas Iniciadas)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }



        // GET: api/RDHs/ViolacionEquiparadaMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> ViolacionEquiparadaMujeres([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("VIOLACION"))
                .Where(a => a.Tipo == "Equipado")
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Violacion equiparada(Victimas mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RDHs/FeminicidioCarpetasAgencia
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> FeminicidioCarpetasAgencia([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.RHecho.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.Delito.Nombre.Contains("FEMINICIDIO"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {

                if (registro.mes == 01) enero++;
                else if (registro.mes == 02) febrero++;
                else if (registro.mes == 03) marzo++;
                else if (registro.mes == 04) abril++;
                else if (registro.mes == 05) mayo++;
                else if (registro.mes == 06) junio++;
                else if (registro.mes == 07) julio++;
                else if (registro.mes == 08) agosto++;
                else if (registro.mes == 09) septiembre++;
                else if (registro.mes == 10) octubre++;
                else if (registro.mes == 11) noviembre++;
                else if (registro.mes == 12) diciembre++;

                total++;


            }

            return Ok(new { delito = "Total de carpetas de investigación iniciadas por feminicidio", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }



        // GET: api/RDHs/ListarPorDspAnoMes
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}/{delitostr}")]
        public async Task<IEnumerable<EstadisticaViewModelMicrodato>> ListarPorDspAnoMes([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf, string delitostr)
        {

            var delitos = await _context.RDHs
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho)
                          .Include(a => a.RHecho.NUCs)
                          .Include(a => a.RHecho.RAtencion)
                          .Where(a => a.Equiparado == true)
                          .Where(a => a.RHecho.FechaHoraSuceso2 >= fechai)
                          .Where(a => a.RHecho.FechaHoraSuceso2 <= fechaf)
                          .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                          .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                          .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                          .Where(a => a.RHecho.Status == true)
                          .Where(a => a.Delito.Nombre.Contains(delitostr))
                          .GroupBy(v => v.RHechoId)
                          .Select(x => new
                          {
                              rhecho = x.Key,
                              nuc = x.Select(v => v.RHecho.NUCs.nucg),
                              MunicipioAgencia = x.Select(v => v.RHecho.RAtencion.DistritoInicial),
                              FechaInicioCarpeta = x.Select(v => v.RHecho.FechaElevaNuc2),
                              FechaHechos = x.Select(v => v.RHecho.FechaHoraSuceso2),
                              StatusCarpeta = x.Select(v => v.RHecho.NUCs.StatusNUC),
                              Rbreve = x.Select(v => v.RHecho.RBreve),
                              Ratencion = x.Select(v => v.RHecho.RAtencionId),
                              Agencia = x.Select(v => v.RHecho.RAtencion.DirSubProcuInicial),
                              Armafuego = x.Select(v => v.ArmaFuego),
                              ArmaBlanca = x.Select(v => v.ArmaBlanca),
                              ParteCuerpo = x.Select(v => v.ConAlgunaParteCuerpo ),
                              OtroElemento = x.Select(v => v.ConotroElemento),
                              Violencia = x.Select(v => v.ViolenciaSinViolencia),
                          })
                          .ToListAsync();

            string convertir(IEnumerable<bool> armafuego, IEnumerable<bool> armablanca, IEnumerable<string> partecuerpo, IEnumerable<string> otroelemento, IEnumerable<string> violencia)
            {
                List<bool> armafuegols = armafuego.ToList();
                List<bool> armablancals = armablanca.ToList();
                List<string> partecuerpols = partecuerpo.ToList();
                List<string> otroelementols = otroelemento.ToList();
                List<string> violencials = violencia.ToList();
                string final = "";
                int cont = 0;

                foreach (var registro in armafuego)
                {
                    if (violencials[cont] == "Violencia física")
                    {
                        if (armafuegols[cont])
                            final += " - Uso de arma de fuego";

                        if (armablancals[cont])
                            final += " - Uso de arma blanca";

                        if (partecuerpols[cont] != "")
                            final += " - " + partecuerpols[cont];

                        if (otroelementols[cont] != "")
                            final += " - " + otroelementols[cont];

                        final += " / ";
                    }
                    cont++;

                }
                return final;
            }

            string convertirfc(IEnumerable<string> violencia)
            {
                List<string> violencials = violencia.ToList();
                string final = "";
                int cont = 0;

                foreach (var registro in violencia)
                {
                    final += " -" + violencials[cont];
                    cont++;
                }
                return final;
            }

            var delito = delitos.Select(a => new EstadisticaViewModelMicrodato
            {
                Rhechoid = a.rhecho,
                nuc = a.nuc.First(),
                MunicipioAgencia = a.MunicipioAgencia.First(),
                FechaInicioCarpeta = a.FechaInicioCarpeta.First(),
                FechaHechos = a.FechaHechos.First(),
                StatusCarpeta = a.StatusCarpeta.First(),
                Rbreve = a.Rbreve.First(),
                Ratencion = a.Ratencion.First(),
                Agencia = a.Agencia.First(),
                ElementoParaComision = convertir(a.Armafuego, a.ArmaBlanca, a.ParteCuerpo, a.OtroElemento, a.Violencia),
                FormaComision = convertirfc(a.Violencia)
            });

            //--------------------------------------------------DIRECCION HECHO
            IEnumerable<EstadisticaViewModelMicrodato> items = new EstadisticaViewModelMicrodato[] { };


            foreach (var registro in delito)
            {
                var direccion = await _context.DireccionDelitos
                        .Where(a => a.RHechoId == registro.Rhechoid)
                        .FirstOrDefaultAsync();

                IEnumerable<EstadisticaViewModelMicrodato> ReadLines()
                {
                    IEnumerable<EstadisticaViewModelMicrodato> item;

                    string entidad, municipio;

                    if (direccion == null)
                    {
                        entidad = "";
                        municipio = "";
                    }
                    else
                    {
                        entidad = direccion.Estado;
                        municipio = direccion.Municipio;
                    }

                    item = (new[]{new EstadisticaViewModelMicrodato{
                        Rhechoid = registro.Rhechoid,
                        Ratencion = registro.Ratencion,
                        nuc = registro.nuc,
                        MunicipioAgencia = registro.MunicipioAgencia,
                        FechaInicioCarpeta = registro.FechaInicioCarpeta,
                        FechaHechos = registro.FechaHechos,
                        StatusCarpeta = registro.StatusCarpeta,
                        Rbreve = registro.Rbreve,
                        Agencia = registro.Agencia,
                        ElementoParaComision = registro.ElementoParaComision,
                        FormaComision = registro.FormaComision,
                        EntidadFHechos = entidad,
                        MunicipioHechos = municipio
                    }});

                    return item;
                }

                items = items.Concat(ReadLines());
            }


            //--------------------------------------------------PERSONAS
            IEnumerable<EstadisticaViewModelMicrodato> items2 = new EstadisticaViewModelMicrodato[] { };

            foreach (var item in items)
            {

                var personas = await _context.RAPs
                    .Include(a => a.Persona)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .Where(a => a.RAtencionId == item.Ratencion)
                    .ToListAsync();

                foreach (var persona in personas)
                {
                    IEnumerable<EstadisticaViewModelMicrodato> ReadLines()
                    {
                        IEnumerable<EstadisticaViewModelMicrodato> item2;


                        item2 = (new[]{new EstadisticaViewModelMicrodato{
                                Rhechoid = item.Rhechoid,
                                Ratencion = item.Ratencion,
                                nuc = item.nuc,
                                MunicipioAgencia = item.MunicipioAgencia,
                                FechaInicioCarpeta = item.FechaInicioCarpeta,
                                FechaHechos = item.FechaHechos,
                                StatusCarpeta = item.StatusCarpeta,
                                Rbreve = item.Rbreve,
                                Agencia = item.Agencia,
                                ElementoParaComision = item.ElementoParaComision,
                                FormaComision = item.FormaComision,
                                EntidadFHechos = item.EntidadFHechos,
                                MunicipioHechos = item.MunicipioHechos,
                                NombreVictima = persona.Persona.Nombre,
                                ApellidoPaVictima = persona.Persona.ApellidoPaterno,
                                ApellidoMaVictima = persona.Persona.ApellidoMaterno,
                                FechaNacimiento = persona.Persona.FechaNacimiento,
                                EdadVictima = persona.Persona.Edad,
                                RelacionVictima = persona.Persona.Parentesco,
                                EscolaridadVictima = persona.Persona.NivelEstudio,
                                OcupacionVictima = persona.Persona.Ocupacion,
                                NacionalidadVictima = persona.Persona.Nacionalidad,
                                PersonaId = persona.PersonaId

                            }});

                        return item2;
                    }

                    items2 = items2.Concat(ReadLines());

                }

            }

            IEnumerable<EstadisticaViewModelMicrodato> items3 = new EstadisticaViewModelMicrodato[] { };


            foreach (var item in items2)
            {
                var direccionpersonas = await _context.DireccionPersonals
                        .Where(a => a.PersonaId == item.PersonaId)
                        .FirstOrDefaultAsync();

                IEnumerable<EstadisticaViewModelMicrodato> ReadLines()
                {
                    IEnumerable<EstadisticaViewModelMicrodato> item2;

                    string pais;

                    if (direccionpersonas == null)
                    {
                        pais = "";
                    }
                    else
                    {
                        pais = direccionpersonas.Pais;
                    }


                    item2 = (new[]{new EstadisticaViewModelMicrodato{
                        Rhechoid = item.Rhechoid,
                        Ratencion = item.Ratencion,
                        nuc = item.nuc,
                        MunicipioAgencia = item.MunicipioAgencia,
                        FechaInicioCarpeta = item.FechaInicioCarpeta,
                        FechaHechos = item.FechaHechos,
                        StatusCarpeta = item.StatusCarpeta,
                        Rbreve = item.Rbreve,
                        Agencia = item.Agencia,
                        ElementoParaComision = item.ElementoParaComision,
                        FormaComision = item.FormaComision,
                        EntidadFHechos = item.EntidadFHechos,
                        MunicipioHechos = item.MunicipioHechos,
                        NombreVictima = item.NombreVictima,
                        ApellidoPaVictima = item.ApellidoPaVictima,
                        ApellidoMaVictima = item.ApellidoMaVictima,
                        FechaNacimiento = item.FechaNacimiento,
                        EdadVictima = item.EdadVictima,
                        RelacionVictima = item.RelacionVictima,
                        EscolaridadVictima = item.EscolaridadVictima,
                        OcupacionVictima = item.OcupacionVictima,
                        NacionalidadVictima = item.NacionalidadVictima,
                        PersonaId = item.PersonaId,
                        PaisVictima = pais
                    }});

                    return item2;
                }

                items3 = items3.Concat(ReadLines());
            }


            IEnumerable<EstadisticaViewModelMicrodato> items4 = new EstadisticaViewModelMicrodato[] { };


            foreach (var item in items3)
            {
                var iniciodetenido = await _context.RAPs
                        .Where(a => a.RAtencionId == item.Ratencion)
                        .Where(a => a.ClasificacionPersona == "Imputado")
                        .Where(a => a.PInicio == true)
                        .FirstOrDefaultAsync();

                IEnumerable<EstadisticaViewModelMicrodato> ReadLines()
                {
                    IEnumerable<EstadisticaViewModelMicrodato> item2;

                    string iniciodet;

                    if (iniciodetenido == null)
                    {
                        iniciodet = "No";
                    }
                    else
                    {
                        iniciodet = "Si";
                    }


                    item2 = (new[]{new EstadisticaViewModelMicrodato{
                        Rhechoid = item.Rhechoid,
                        Ratencion = item.Ratencion,
                        nuc = item.nuc,
                        MunicipioAgencia = item.MunicipioAgencia,
                        FechaInicioCarpeta = item.FechaInicioCarpeta,
                        FechaHechos = item.FechaHechos,
                        StatusCarpeta = item.StatusCarpeta,
                        Rbreve = item.Rbreve,
                        Agencia = item.Agencia,
                        ElementoParaComision = item.ElementoParaComision,
                        FormaComision = item.FormaComision,
                        EntidadFHechos = item.EntidadFHechos,
                        MunicipioHechos = item.MunicipioHechos,
                        NombreVictima = item.NombreVictima,
                        ApellidoPaVictima = item.ApellidoPaVictima,
                        ApellidoMaVictima = item.ApellidoMaVictima,
                        FechaNacimiento = item.FechaNacimiento,
                        EdadVictima = item.EdadVictima,
                        RelacionVictima = item.RelacionVictima,
                        EscolaridadVictima = item.EscolaridadVictima,
                        OcupacionVictima = item.OcupacionVictima,
                        NacionalidadVictima = item.NacionalidadVictima,
                        PersonaId = item.PersonaId,
                        PaisVictima = item.PaisVictima,
                        InicioDetenidi = iniciodet
                    }});

                    return item2;
                }

                items4 = items4.Concat(ReadLines());
            }
            return items4;


        }


 
        // GET: api/RDHs/ListarPorDspAnoMesNumeroDelitos
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<NumeroDelitosViewModel>> ListarPorDspAnoMesNumeroDelitos([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho)
                          .Include(a => a.RHecho.NUCs)
                          .Include(a => a.RHecho.RAtencion)
                          .Where(a => a.Equiparado == true)
                          .Where(a => a.RHecho.FechaHoraSuceso2 >= fechai)
                          .Where(a => a.RHecho.FechaHoraSuceso2  <= fechaf)
                          .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                          .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                          .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                          .Where(a => a.RHecho.Status == true)
                          .Where(a => a.Delito.Nombre.Contains("FEMINICIDIO"))
                          .GroupBy(v => v.Delito.Nombre)
                          .Select(x => new
                          {
                              etiqueta = x.Key,
                              nombredelito = x.Count(v => v.Delito.Nombre == x.Key),
                          })
                          .ToListAsync();

            return delitos.Select(v => new NumeroDelitosViewModel
            {
                Delito = v.etiqueta,
                Numerodelitos = v.nombredelito,
            });
        }

        // GET: api/RDHs/HomicidiodolosoCulposo
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> HomicidiodolosoCulposo([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Include(a => a.RHecho.NUCs)
                .Include(a => a.RHecho.RAtencion)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2 >= fechai)
                .Where(a => a.RHecho.FechaHoraSuceso2 <= fechaf)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.RHecho.Status == true)
                .Where(a => a.Delito.Nombre.Contains("HOMICIDIO"))
                .GroupBy(v => v.Delito.Nombre)
                .Select(x => new {
                    Delito = x.Key,
                    homicidio = x.Count(v => v.Delito.Nombre.Contains("HOMICIDIO")),
                    homicidiodoloso = x.Count(v => v.Delito.Nombre.Contains("HOMICIDIO") && v.IntensionDelito == "Doloso"),
                    homicidioculposo = x.Count(v => v.Delito.Nombre.Contains("HOMICIDIO") && v.IntensionDelito == "Culposo")
                })
                .ToListAsync();

            var tx = delitos.Select(a => new HomicidiosDolososCulpososViewModel
            {
                Delito = a.Delito,
                Homicidio = a.homicidio,
                HomicidioDoloso = a.homicidiodoloso,
                HomicidioCulposo = a.homicidioculposo

            });

            int homicidio = 0, homicidiodoloso = 0, homicidioculposo = 0;

            foreach (var registro in tx)
            {
                homicidio += registro.Homicidio;
                homicidiodoloso += registro.HomicidioDoloso;
                homicidioculposo += registro.HomicidioCulposo;
            }

            //------------------------------------------------------------------------------------------------------------------

            var victimashomicidio = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Include(a => a.RHecho.NUCs)
                .Include(a => a.RHecho.RAtencion)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2 >= fechai)
                .Where(a => a.RHecho.FechaHoraSuceso2 <= fechaf)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.RHecho.Status == true)
                .Where(a => a.Delito.Nombre.Contains("HOMICIDIO"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.RHecho.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx1 = victimashomicidio.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
            });


            int victimashomicidiono = 0;

            foreach (var registro in tx1)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    victimashomicidiono++;
                }

            }

            //---------------------------------------------------------------------------------------------------------------

            var victimashomicidioculposo = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Include(a => a.RHecho.NUCs)
                .Include(a => a.RHecho.RAtencion)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2 >= fechai)
                .Where(a => a.RHecho.FechaHoraSuceso2 <= fechaf)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.RHecho.Status == true)
                .Where(a => a.Delito.Nombre.Contains("HOMICIDIO"))
                .Where(a => a.IntensionDelito == "Culposo")
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                })
                .ToListAsync();

            var tx2 = victimashomicidioculposo.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
            });


            int victimashomicidioculposono = 0;

            foreach (var registro in tx2)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    victimashomicidioculposono++;
                }

            }

            //---------------------------------------------------------------------------------------------------------------

            var victimashomicidiodoloso = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Include(a => a.RHecho.NUCs)
                .Include(a => a.RHecho.RAtencion)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2 >= fechai)
                .Where(a => a.RHecho.FechaHoraSuceso2 <= fechaf)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.RHecho.Status == true)
                .Where(a => a.Delito.Nombre.Contains("HOMICIDIO"))
                .Where(a => a.IntensionDelito == "Doloso")
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                })
                .ToListAsync();

            var tx3 = victimashomicidiodoloso.Select(a => new HomicidiosDoMujeresViewModel
            {
                ratencionid = a.ratencionid,
            });


            int victimashomicidiodolosono = 0;

            foreach (var registro in tx3)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    victimashomicidiodolosono++;
                }

            }

            return Ok(new { Homicidios = homicidio, HomicidioDoloso = homicidiodoloso, HomicidioCulposo = homicidioculposo, VictimasHomicidio = victimashomicidiono, VictimasHomicidioDoloso = victimashomicidiodolosono, VictimasHomicidioCulposo = victimashomicidioculposono });

        }


        // GET: api/RDHs/TrataPersonasSujetoActivo
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}/{delitostr}")]
        public async Task<IEnumerable<TrataPSujetoActivoViewModel>> TrataPersonasSujetoActivo([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf, string delitostr)
        {

            var delitos = await _context.RDHs
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho)
                          .Include(a => a.RHecho.NUCs)
                          .Include(a => a.RHecho.RAtencion)
                          .Where(a => a.Equiparado == true)
                          .Where(a => a.RHecho.FechaHoraSuceso2 >= fechai)
                          .Where(a => a.RHecho.FechaHoraSuceso2 <= fechaf)
                          .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                          .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                          .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                          .Where(a => a.RHecho.Status == true)
                          .Where(a => a.Delito.Nombre.Contains(delitostr))
                          .GroupBy(v => v.RHechoId)
                          .Select(x => new
                          {
                              rhecho = x.Key,
                              nuc = x.Select(v => v.RHecho.NUCs.nucg),
                              MunicipioAgencia = x.Select(v => v.RHecho.RAtencion.DistritoInicial),
                              Ratencion = x.Select(v => v.RHecho.RAtencionId),
                              Agencia = x.Select(v => v.RHecho.RAtencion.DirSubProcuInicial),
                              Mes = x.Select(v => v.RHecho.FechaReporte),
                          })
                          .ToListAsync();


            int Convertir(string fecha)
            {
                if (fecha.Contains("enero")) return 1;
                else if (fecha.Contains("febrero")) return 2;
                else if (fecha.Contains("marzo")) return 3;
                else if (fecha.Contains("abril")) return 4;
                else if (fecha.Contains("mayo")) return 5;
                else if (fecha.Contains("junio")) return 6;
                else if (fecha.Contains("julio")) return 7;
                else if (fecha.Contains("agosto")) return 8;
                else if (fecha.Contains("septiembre")) return 9;
                else if (fecha.Contains("octubre")) return 10;
                else if (fecha.Contains("noviembre")) return 11;
                else return 12;
            }

            String Convertirst(int fecha)
            {
                if (fecha == 1) return "Enero";
                else if (fecha == 2) return "Febrero";
                else if (fecha == 3) return "Marzo";
                else if (fecha == 4) return "Abril";
                else if (fecha == 5) return "Mayo";
                else if (fecha == 6) return "Junio";
                else if (fecha == 7) return "Julio";
                else if (fecha == 8) return "Agosto";
                else if (fecha == 9) return "Septiembre";
                else if (fecha == 10) return "Octubre";
                else if (fecha == 11) return "Noviembre";
                else return "Diciembre";
            }


            var delito = delitos.Select(a => new TrataPSujetoActivoViewModel
            {
                Rhechoid = a.rhecho,
                nuc = a.nuc.First(),
                MunicipioAgencia = a.MunicipioAgencia.First(),
                Ratencion = a.Ratencion.First(),
                Agencia = a.Agencia.First(),
                Mesi = Convertir(a.Mes.First()),
                Mes = Convertirst(Convertir(a.Mes.First()))
            });

            //--------------------------------------------------PERSONAS
            IEnumerable<TrataPSujetoActivoViewModel> items2 = new TrataPSujetoActivoViewModel[] { };

            foreach (var item in delito)
            {

                var personas = await _context.RAPs
                    .Include(a => a.Persona)
                    .Where(a => a.ClasificacionPersona == "Imputado")
                    .Where(a => a.RAtencionId == item.Ratencion)
                    .ToListAsync();

                foreach (var persona in personas)
                {
                    IEnumerable<TrataPSujetoActivoViewModel> ReadLines()
                    {
                        IEnumerable<TrataPSujetoActivoViewModel> item2;


                        item2 = (new[]{new TrataPSujetoActivoViewModel{
                                Rhechoid = item.Rhechoid,
                                Ratencion = item.Ratencion,
                                nuc = item.nuc,
                                MunicipioAgencia = item.MunicipioAgencia,
                                Agencia = item.Agencia,
                                Mes = item.Mes,
                                Mesi = item.Mesi,
                                NombreImputado = persona.Persona.Nombre,
                                ApellidoPaImputado = persona.Persona.ApellidoPaterno,
                                ApellidoMaImputado = persona.Persona.ApellidoMaterno,
                                Alias = persona.Persona.Alias,
                                FechaNacimiento = persona.Persona.FechaNacimiento,
                                EdadImputado = persona.Persona.Edad,
                                Sexoimputado = persona.Persona.Sexo,
                                GeneroImputado = persona.Persona.Genero,
                                EstadoCivilImputado = persona.Persona.EstadoCivil,
                                NacionalidadImputado = persona.Persona.Nacionalidad,
                                EscolaridadImputado = persona.Persona.NivelEstudio,
                                OcupacionImputado = persona.Persona.Ocupacion,
                                PersonaId = persona.PersonaId

                            }});

                        return item2;
                    }

                    items2 = items2.Concat(ReadLines());

                }

            }

            //-------------------------------------------------DIRECCION PERSONAS

            IEnumerable<TrataPSujetoActivoViewModel> items3 = new TrataPSujetoActivoViewModel[] { };


            foreach (var item in items2)
            {
                var direccionpersonas = await _context.DireccionPersonals
                        .Where(a => a.PersonaId == item.PersonaId)
                        .FirstOrDefaultAsync();

                IEnumerable<TrataPSujetoActivoViewModel> ReadLines()
                {
                    IEnumerable<TrataPSujetoActivoViewModel> item2;

                    string pais;

                    if (direccionpersonas == null)
                    {
                        pais = "";
                    }
                    else
                    {
                        pais = direccionpersonas.Pais;
                    }


                    item2 = (new[]{new TrataPSujetoActivoViewModel{
                        Rhechoid = item.Rhechoid,
                        Ratencion = item.Ratencion,
                        nuc = item.nuc,
                        MunicipioAgencia = item.MunicipioAgencia,
                        Agencia = item.Agencia,
                        Mes = item.Mes,
                        Mesi = item.Mesi,
                        NombreImputado = item.NombreImputado,
                        ApellidoPaImputado = item.ApellidoPaImputado,
                        ApellidoMaImputado = item.ApellidoMaImputado,
                        Alias = item.Alias,
                        FechaNacimiento = item.FechaNacimiento,
                        EdadImputado = item.EdadImputado,
                        Sexoimputado = item.Sexoimputado,
                        GeneroImputado = item.GeneroImputado,
                        EstadoCivilImputado = item.EstadoCivilImputado,
                        NacionalidadImputado = item.NacionalidadImputado,
                        EscolaridadImputado = item.EscolaridadImputado,
                        OcupacionImputado = item.OcupacionImputado,
                        PersonaId = item.PersonaId,
                        PaisImputado = direccionpersonas.Pais,
                        EntidadImputado = direccionpersonas.Estado,
                        MunicipioImputado = direccionpersonas.Municipio
                    }});

                    return item2;
                }

                items3 = items3.Concat(ReadLines());
            }

            return items3;


        }


        // GET: api/RDHs/TrataPersonasSujetoPasivo
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}/{delitostr}")]
        public async Task<IEnumerable<TrataPSujetoPasivoViewModel>> TrataPersonasSujetoPasivo([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf, string delitostr)
        {

            var delitos = await _context.RDHs
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho)
                          .Include(a => a.RHecho.NUCs)
                          .Include(a => a.RHecho.RAtencion)
                          .Where(a => a.Equiparado == true)
                          .Where(a => a.RHecho.FechaHoraSuceso2 >= fechai)
                          .Where(a => a.RHecho.FechaHoraSuceso2 <= fechaf)
                          .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                          .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                          .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                          .Where(a => a.RHecho.Status == true)
                          .Where(a => a.Delito.Nombre.Contains(delitostr))
                          .GroupBy(v => v.RHechoId)
                          .Select(x => new
                          {
                              rhecho = x.Key,
                              nuc = x.Select(v => v.RHecho.NUCs.nucg),
                              MunicipioAgencia = x.Select(v => v.RHecho.RAtencion.DistritoInicial),
                              Ratencion = x.Select(v => v.RHecho.RAtencionId),
                              Agencia = x.Select(v => v.RHecho.RAtencion.DirSubProcuInicial),
                              Mes = x.Select(v => v.RHecho.FechaReporte),
                              Fechainicio = x.Select(v => v.RHecho.FechaElevaNuc2),
                              Delitos = x.Select(v => v.Delito.Nombre),
                              Rbreve = x.Select(v => v.RHecho.RBreve),
                          })
                          .ToListAsync();


            int Convertir(string fecha)
            {
                if (fecha.Contains("enero")) return 1;
                else if (fecha.Contains("febrero")) return 2;
                else if (fecha.Contains("marzo")) return 3;
                else if (fecha.Contains("abril")) return 4;
                else if (fecha.Contains("mayo")) return 5;
                else if (fecha.Contains("junio")) return 6;
                else if (fecha.Contains("julio")) return 7;
                else if (fecha.Contains("agosto")) return 8;
                else if (fecha.Contains("septiembre")) return 9;
                else if (fecha.Contains("octubre")) return 10;
                else if (fecha.Contains("noviembre")) return 11;
                else return 12;
            }

            String Convertirst(int fecha)
            {
                if (fecha == 1) return "Enero";
                else if (fecha == 2) return "Febrero";
                else if (fecha == 3) return "Marzo";
                else if (fecha == 4) return "Abril";
                else if (fecha == 5) return "Mayo";
                else if (fecha == 6) return "Junio";
                else if (fecha == 7) return "Julio";
                else if (fecha == 8) return "Agosto";
                else if (fecha == 9) return "Septiembre";
                else if (fecha == 10) return "Octubre";
                else if (fecha == 11) return "Noviembre";
                else return "Diciembre";
            }



            var delito = delitos.Select(a => new TrataPSujetoPasivoViewModel
            {
                Rhechoid = a.rhecho,
                nuc = a.nuc.First(),
                MunicipioAgencia = a.MunicipioAgencia.First(),
                Ratencion = a.Ratencion.First(),
                Agencia = a.Agencia.First(),
                Mesi = Convertir(a.Mes.First()),
                Mes = Convertirst(Convertir(a.Mes.First())),
                FechaInicioCarpeta = a.Fechainicio.First(),
                Rbreve = a.Rbreve.First()
            });

            //--------------------------------------------------PERSONAS
            IEnumerable<TrataPSujetoPasivoViewModel> items2 = new TrataPSujetoPasivoViewModel[] { };

            foreach (var item in delito)
            {

                var personas = await _context.RAPs
                    .Include(a => a.Persona)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .Where(a => a.RAtencionId == item.Ratencion)
                    .ToListAsync();



                foreach (var persona in personas)
                {
                    IEnumerable<TrataPSujetoPasivoViewModel> ReadLines()
                    {
                        IEnumerable<TrataPSujetoPasivoViewModel> item2;


                        item2 = (new[]{new TrataPSujetoPasivoViewModel{
                                Rhechoid = item.Rhechoid,
                                Ratencion = item.Ratencion,
                                nuc = item.nuc,
                                MunicipioAgencia = item.MunicipioAgencia,
                                Agencia = item.Agencia,
                                Mes = item.Mes,
                                Mesi = item.Mesi,
                                FechaInicioCarpeta = item.FechaInicioCarpeta,
                                Rbreve = item.Rbreve,
                                NombreVictima = persona.Persona.Nombre,
                                ApellidoMaVictima = persona.Persona.ApellidoPaterno,
                                ApellidoPaVictima = persona.Persona.ApellidoMaterno,
                                Alias = persona.Persona.Alias,
                                FechaNacimiento = persona.Persona.FechaNacimiento,
                                EdadImputado = persona.Persona.Edad,
                                SexoVictima = persona.Persona.Sexo,
                                GeneroVictima = persona.Persona.Genero,
                                EstadoCivilVictima = persona.Persona.EstadoCivil,
                                NacionalidadVictima = persona.Persona.Nacionalidad,
                                HablaEspanol = persona.Persona.Lengua == "" ? "Si" : "No",
                                HablaIndigena = persona.Persona.Lengua != "" ? "Si" : "No",
                                LeguaIndigena = persona.Persona.Lengua,
                                Discapacidad = persona.Persona.Discapacidad ? "Si" : "No",
                                TipoDiscapadicad = persona.Persona.TipoDiscapacidad,
                                EscolaridadVictima = persona.Persona.NivelEstudio,
                                OcupacionVictima = persona.Persona.Ocupacion,
                                RelacionVictimario = persona.Persona.Parentesco,
                                PersonaId = persona.PersonaId,


                            }});

                        return item2;
                    }

                    items2 = items2.Concat(ReadLines());

                }

            }

            //-------------------------------------------------DIRECCION PERSONAS

            IEnumerable<TrataPSujetoPasivoViewModel> items3 = new TrataPSujetoPasivoViewModel[] { };


            foreach (var item in items2)
            {
                var direccionpersonas = await _context.DireccionPersonals
                        .Where(a => a.PersonaId == item.PersonaId)
                        .FirstOrDefaultAsync();

                IEnumerable<TrataPSujetoPasivoViewModel> ReadLines()
                {
                    IEnumerable<TrataPSujetoPasivoViewModel> item2;

                    string pais;

                    if (direccionpersonas == null)
                    {
                        pais = "";
                    }
                    else
                    {
                        pais = direccionpersonas.Pais;
                    }


                    item2 = (new[]{new TrataPSujetoPasivoViewModel{
                        Rhechoid = item.Rhechoid,
                        Ratencion = item.Ratencion,
                        nuc = item.nuc,
                        MunicipioAgencia = item.MunicipioAgencia,
                        Agencia = item.Agencia,
                        Mes = item.Mes,
                        Mesi = item.Mesi,
                        FechaInicioCarpeta = item.FechaInicioCarpeta,
                        Rbreve = item.Rbreve,
                        NombreVictima = item.NombreVictima,
                        ApellidoMaVictima = item.ApellidoPaVictima,
                        ApellidoPaVictima = item.ApellidoMaVictima,
                        Alias = item.Alias,
                        FechaNacimiento = item.FechaNacimiento,
                        EdadImputado = item.EdadImputado,
                        SexoVictima = item.SexoVictima,
                        GeneroVictima = item.GeneroVictima,
                        EstadoCivilVictima = item.EstadoCivilVictima,
                        NacionalidadVictima = item.NacionalidadVictima,
                        HablaEspanol = item.HablaEspanol,
                        HablaIndigena = item.HablaIndigena,
                        LeguaIndigena = item.LeguaIndigena,
                        Discapacidad = item.Discapacidad,
                        TipoDiscapadicad = item.TipoDiscapadicad,
                        EscolaridadVictima = item.EscolaridadVictima,
                        OcupacionVictima = item.OcupacionVictima,
                        RelacionVictimario = item.RelacionVictimario,
                        PersonaId = item.PersonaId,
                        PaisVictima = direccionpersonas.Pais,
                        EntidadVictima = direccionpersonas.Estado,
                        MunicipioVictima = direccionpersonas.Municipio

                    }});

                    return item2;
                }

                items3 = items3.Concat(ReadLines());
            }

            //--------------------------------------------------------------REMISION UI
            IEnumerable<TrataPSujetoPasivoViewModel> items4 = new TrataPSujetoPasivoViewModel[] { };


            foreach (var item in items3)
            {
                var remisionui = await _context.RemisionUIs
                        .Where(a => a.RHechoId == item.Rhechoid)
                        .Where(a => a.Rechazo != null)
                        .OrderBy(a => a.Fechasys)
                        .FirstOrDefaultAsync();

                IEnumerable<TrataPSujetoPasivoViewModel> ReadLines()
                {
                    IEnumerable<TrataPSujetoPasivoViewModel> item2;

                    string agenciaenvia, nucenvia;

                    if (remisionui == null)
                    {
                        agenciaenvia = "";
                        nucenvia = "";
                    }
                    else
                    {
                        agenciaenvia = remisionui.UAgencia + ", " + remisionui.UModulo;
                        nucenvia = remisionui.Nuc;
                    }


                    item2 = (new[]{new TrataPSujetoPasivoViewModel{
                        Rhechoid = item.Rhechoid,
                        Ratencion = item.Ratencion,
                        nuc = item.nuc,
                        MunicipioAgencia = item.MunicipioAgencia,
                        Agencia = item.Agencia,
                        Mes = item.Mes,
                        Mesi = item.Mesi,
                        FechaInicioCarpeta = item.FechaInicioCarpeta,
                        Rbreve = item.Rbreve,
                        NombreVictima = item.NombreVictima,
                        ApellidoMaVictima = item.ApellidoPaVictima,
                        ApellidoPaVictima = item.ApellidoMaVictima,
                        Alias = item.Alias,
                        FechaNacimiento = item.FechaNacimiento,
                        EdadImputado = item.EdadImputado,
                        SexoVictima = item.SexoVictima,
                        GeneroVictima = item.GeneroVictima,
                        EstadoCivilVictima = item.EstadoCivilVictima,
                        NacionalidadVictima = item.NacionalidadVictima,
                        HablaEspanol = item.HablaEspanol,
                        HablaIndigena = item.HablaIndigena,
                        LeguaIndigena = item.LeguaIndigena,
                        Discapacidad = item.Discapacidad,
                        TipoDiscapadicad = item.TipoDiscapadicad,
                        EscolaridadVictima = item.EscolaridadVictima,
                        OcupacionVictima = item.OcupacionVictima,
                        RelacionVictimario = item.RelacionVictimario,
                        PersonaId = item.PersonaId,
                        PaisVictima = item.PaisVictima,
                        EntidadVictima = item.EstadoCivilVictima,
                        MunicipioVictima = item.MunicipioVictima,
                        TransferidaDnuc = remisionui != null && remisionui.Nuc != item.nuc ? "Si" : "No",
                        AgenciaTransfiere = agenciaenvia,
                        Nucanterior = nucenvia,
                        Reclasificada = remisionui != null ? "Si" : "No"

                    }});

                    return item2;
                }

                items4 = items4.Concat(ReadLines());
            }

            //--------------------------------------TOTALDELITOS

            IEnumerable<TrataPSujetoPasivoViewModel> items5 = new TrataPSujetoPasivoViewModel[] { };


            foreach (var item in items4)
            {
                var delitos2 = await _context.RDHs
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho.RAtencion)
                          .Where(a => a.RHechoId == item.Rhechoid)
                          .ToListAsync();

                var g = delitos2.Select(a => new TrataPSujetoPasivoViewModel
                {
                    DelitosReclasificada = a.Delito.Nombre
                });

                string DelitosConcatenar(IEnumerable<TrataPSujetoPasivoViewModel> Delitostotal)
                {
                    string a = "";
                    int x = 0;
                    foreach (var registro in Delitostotal)
                    {
                        a += registro.DelitosReclasificada;
                        if (x != Delitostotal.Count() - 1)
                            a += ", ";
                        x++;
                    }
                    return a;
                }

                string delitostotal = DelitosConcatenar(g);

                IEnumerable<TrataPSujetoPasivoViewModel> ReadLines()
                {
                    IEnumerable<TrataPSujetoPasivoViewModel> item2;

                    item2 = (new[]{new TrataPSujetoPasivoViewModel{
                        Rhechoid = item.Rhechoid,
                        Ratencion = item.Ratencion,
                        nuc = item.nuc,
                        MunicipioAgencia = item.MunicipioAgencia,
                        Agencia = item.Agencia,
                        Mes = item.Mes,
                        Mesi = item.Mesi,
                        FechaInicioCarpeta = item.FechaInicioCarpeta,
                        Rbreve = item.Rbreve,
                        NombreVictima = item.NombreVictima,
                        ApellidoMaVictima = item.ApellidoPaVictima,
                        ApellidoPaVictima = item.ApellidoMaVictima,
                        Alias = item.Alias,
                        FechaNacimiento = item.FechaNacimiento,
                        EdadImputado = item.EdadImputado,
                        SexoVictima = item.SexoVictima,
                        GeneroVictima = item.GeneroVictima,
                        EstadoCivilVictima = item.EstadoCivilVictima,
                        NacionalidadVictima = item.NacionalidadVictima,
                        HablaEspanol = item.HablaEspanol,
                        HablaIndigena = item.HablaIndigena,
                        LeguaIndigena = item.LeguaIndigena,
                        Discapacidad = item.Discapacidad,
                        TipoDiscapadicad = item.TipoDiscapadicad,
                        EscolaridadVictima = item.EscolaridadVictima,
                        OcupacionVictima = item.OcupacionVictima,
                        RelacionVictimario = item.RelacionVictimario,
                        PersonaId = item.PersonaId,
                        PaisVictima = item.PaisVictima,
                        EntidadVictima = item.EstadoCivilVictima,
                        MunicipioVictima = item.MunicipioVictima,
                        TransferidaDnuc = item.TransferidaDnuc,
                        AgenciaTransfiere = item.AgenciaTransfiere,
                        Nucanterior = item.Nucanterior,
                        Reclasificada = item.Reclasificada,
                        DelitosReclasificada = delitostotal

                    }});

                    return item2;
                }

                items5 = items5.Concat(ReadLines());
            }

            //---------------------------------------------------------DIRECCION HECHO

            IEnumerable<TrataPSujetoPasivoViewModel> items6 = new TrataPSujetoPasivoViewModel[] { };


            foreach (var item in items5)
            {
                var direccion = await _context.DireccionDelitos
                        .Where(a => a.RHechoId == item.Rhechoid)
                        .FirstOrDefaultAsync();

                IEnumerable<TrataPSujetoPasivoViewModel> ReadLines()
                {
                    IEnumerable<TrataPSujetoPasivoViewModel> item2;

                    string entidad, municipio, lugarespecifico;

                    if (direccion == null)
                    {
                        entidad = "";
                        municipio = "";
                        lugarespecifico = "";
                    }
                    else
                    {
                        entidad = direccion.Estado;
                        municipio = direccion.Municipio;
                        lugarespecifico = direccion.LugarEspecifico;
                    }

                    item2 = (new[]{new TrataPSujetoPasivoViewModel{
                        Rhechoid = item.Rhechoid,
                        Ratencion = item.Ratencion,
                        nuc = item.nuc,
                        MunicipioAgencia = item.MunicipioAgencia,
                        Agencia = item.Agencia,
                        Mes = item.Mes,
                        Mesi = item.Mesi,
                        FechaInicioCarpeta = item.FechaInicioCarpeta,
                        Rbreve = item.Rbreve,
                        NombreVictima = item.NombreVictima,
                        ApellidoMaVictima = item.ApellidoPaVictima,
                        ApellidoPaVictima = item.ApellidoMaVictima,
                        Alias = item.Alias,
                        FechaNacimiento = item.FechaNacimiento,
                        EdadImputado = item.EdadImputado,
                        SexoVictima = item.SexoVictima,
                        GeneroVictima = item.GeneroVictima,
                        EstadoCivilVictima = item.EstadoCivilVictima,
                        NacionalidadVictima = item.NacionalidadVictima,
                        HablaEspanol = item.HablaEspanol,
                        HablaIndigena = item.HablaIndigena,
                        LeguaIndigena = item.LeguaIndigena,
                        Discapacidad = item.Discapacidad,
                        TipoDiscapadicad = item.TipoDiscapadicad,
                        EscolaridadVictima = item.EscolaridadVictima,
                        OcupacionVictima = item.OcupacionVictima,
                        RelacionVictimario = item.RelacionVictimario,
                        PersonaId = item.PersonaId,
                        PaisVictima = item.PaisVictima,
                        EntidadVictima = item.EstadoCivilVictima,
                        MunicipioVictima = item.MunicipioVictima,
                        TransferidaDnuc = item.TransferidaDnuc,
                        AgenciaTransfiere = item.AgenciaTransfiere,
                        Nucanterior = item.Nucanterior,
                        Reclasificada = item.Reclasificada,
                        DelitosReclasificada = item.DelitosReclasificada,
                        EntidadFederativaH = entidad,
                        MunicipioH = municipio,
                        LugarEspecificoH = lugarespecifico
                    }});

                    return item2;
                }

                items6 = items6.Concat(ReadLines());
            }

            return items6;
        }



        // GET: api/RDHs/TrataPersonasConteoActivosPasivos
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> TrataPersonasConteoActivosPasivos([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Include(a => a.RHecho.NUCs)
                .Include(a => a.RHecho.RAtencion)
                .Where(a => a.Equiparado == true)
                .Where(a => a.RHecho.FechaHoraSuceso2 >= fechai)
                .Where(a => a.RHecho.FechaHoraSuceso2 <= fechaf)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.RHecho.Status == true)
                .Where(a => a.Delito.Nombre.Contains("TRATA DE PERSONAS"))
                .GroupBy(v => v.RHecho.RAtencionId)
                .Select(x => new {
                    ratencion = x.Key,
                    conteo = x.Count(v => v.Delito.Nombre.Contains("TRATA DE PERSONAS")),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new TrataPersonasConteoActivosPasivosViewModel
            {
                Ratencion = a.ratencion,
                Conteo = a.conteo,
            });

            int numerodelitos = 0;

            foreach (var registro in tx)
            {
                numerodelitos += registro.Conteo;
            }

            int victimasc = 0, imputadosc = 0;

            foreach (var registro in tx)
            {
                var victimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.Ratencion)
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in victimas)
                    victimasc++;

                var imputados = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.Ratencion)
                    .Where(a => a.ClasificacionPersona == "Imputado")
                    .ToListAsync();

                foreach (var imputado in imputados)
                    imputadosc++;
            }


            return Ok(new { Numerodelitos = numerodelitos, Victimas = victimasc, Imputados = imputadosc });

        }


        // GET: api/RDHs/ListarEstadisticas
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{iddistrito}/{iddsp}/{distritoactivo}/{dspactivo}/{fechai}/{fechaf}/{municipio}/{localidad}/{agencia}/{agenciaactivo}")]
        public async Task<IEnumerable<EstadisticaFViewModel>> ListarEstadisticas([FromRoute] Guid iddistrito, Guid iddsp, Boolean distritoactivo, Boolean dspactivo, DateTime fechai, DateTime fechaf, string municipio, string localidad, Guid agencia, Boolean agenciaactivo)
        {
            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => distritoactivo ? a.RHecho.Agencia.DSP.DistritoId == iddistrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.Agencia.DSPId == iddsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.Agenciaid == agencia : 1 == 1)
                .Where(a =>  a.RHecho.FechaHoraSuceso2 >= fechai )
                .Where(a =>  a.RHecho.FechaHoraSuceso2 <= fechaf )
                .ToListAsync();

            delitos.Select(a => new EstadisticaFViewModel
            {
                RHechoId = a.RHechoId,
                Delito = a.Delito.Nombre,
                Fecha = a.RHecho.FechaHoraSuceso2
            });


            IEnumerable<EstadisticaFViewModel> items = new EstadisticaFViewModel[] { };

            foreach (var delito in delitos)
            {
                var direcciondelito = await _context.DireccionDelitos
                .Where(a => a.RHechoId == delito.RHechoId)
                .Where(a => municipio != "ZKR" ? a.Municipio == municipio : 1 == 1)
                .Where(a => localidad != "ZKR" ? a.Localidad == localidad : 1 == 1)
                .FirstOrDefaultAsync();

                if (direcciondelito != null)
                {

                    IEnumerable<EstadisticaFViewModel> ReadLines()
                    {
                        IEnumerable<EstadisticaFViewModel> item2;

                        item2 = (new[]{new EstadisticaFViewModel{
                        RHechoId = delito.RHechoId,
                        Delito = delito.Delito.Nombre,
                        Fecha = delito.RHecho.FechaHoraSuceso2,
                        Localidad = direcciondelito.Localidad,
                        Municipio = direcciondelito.Municipio
                    }});

                        return item2;
                    }

                    items = items.Concat(ReadLines());


                }

            }

            IEnumerable<EstadisticaFViewModel> items2 = new EstadisticaFViewModel[] { };

            foreach (var item in items.GroupBy(item => item.Delito))
            {

                IEnumerable<EstadisticaFViewModel> ReadLines()
                {
                    IEnumerable<EstadisticaFViewModel> item2;

                    item2 = (new[]{new EstadisticaFViewModel{
                        Delito = item.Key,
                        NumeroDelitos = item.Count()
                    }});

                    return item2;
                }

                items2 = items2.Concat(ReadLines());

            }

            return items2.OrderByDescending(a => a.NumeroDelitos);

        }


        // GET: api/RDHs/ListarEstadisticasporVictimas
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{iddistrito}/{iddsp}/{distritoactivo}/{dspactivo}/{fechai}/{fechaf}/{municipio}/{localidad}/{indicadorFI}/{indicadorFF}/{tpersona}/{delitor}/{delitoactivo}/{agencia}/{agenciaactivo}/{delitoespecifico}")]
        public async Task<IEnumerable<EstadisticasVictimasViewModel>> ListarEstadisticasporVictimas([FromRoute] Guid iddistrito, Guid iddsp, Boolean distritoactivo, Boolean dspactivo, DateTime fechai, DateTime fechaf, string municipio, string localidad, Boolean indicadorFI, Boolean indicadorFF, string tpersona, Guid delitor, Boolean delitoactivo, Guid agencia, Boolean agenciaactivo, string delitoespecifico)
        {
            var delitos = await _context.RDHs
                .Include(a => a.Delito)
                .Include(a => a.RHecho)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == iddistrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == iddsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => delitoactivo ? a.DelitoId == delitor : 1 == 1)
                .Where(a => delitoespecifico != "ZKR" ? a.DelitoEspecifico == delitoespecifico : 1 == 1)
                .Where(a => indicadorFI ? a.RHecho.FechaHoraSuceso2 > fechai : 1 == 1)
                .Where(a => indicadorFF ? a.RHecho.FechaHoraSuceso2 < fechaf : 1 == 1)
                .ToListAsync();

            delitos.Select(a => new EstadisticasVictimasViewModel
            {
                Rhecho = a.RHechoId,
                Ratencion = a.RHecho.RAtencionId
            });


            IEnumerable<EstadisticasVictimasViewModel> items = new EstadisticasVictimasViewModel[] { };

            foreach (var delito in delitos)
            {
                var direcciondelito = await _context.DireccionDelitos
                .Where(a => a.RHechoId == delito.RHechoId)
                .Where(a => municipio != "ZKR" ? a.Municipio == municipio : 1 == 1)
                .Where(a => localidad != "ZKR" ? a.Localidad == localidad : 1 == 1)
                .FirstOrDefaultAsync();

                if (direcciondelito != null)
                {

                    IEnumerable<EstadisticasVictimasViewModel> ReadLines()
                    {
                        IEnumerable<EstadisticasVictimasViewModel> item2;

                        item2 = (new[]{new EstadisticasVictimasViewModel{
                        Rhecho = delito.RHechoId,
                        Ratencion = delito.RHecho.RAtencionId,
                    }});

                        return item2;
                    }

                    items = items.Concat(ReadLines());


                }

            }




            IEnumerable<EstadisticasVictimasViewModel> items2 = new EstadisticasVictimasViewModel[] { };

            foreach (var rhechoes in items)
            {
                var personas = await _context.RAPs
                .Include(a => a.Persona)
                .Where(a => a.RAtencionId == rhechoes.Ratencion)
                .Where(a => tpersona != "ZKR" ? a.Persona.TipoPersona == tpersona : 1 == 1)
                .ToListAsync();

                foreach (var persona in personas)
                {

                    IEnumerable<EstadisticasVictimasViewModel> ReadLines()
                    {
                        IEnumerable<EstadisticasVictimasViewModel> item2;

                        item2 = (new[]{new EstadisticasVictimasViewModel{
                        Rhecho = rhechoes.Rhecho,
                        Ratencion = rhechoes.Ratencion,
                        Sexo = persona.Persona.Sexo,
                        Edad = persona.Persona.Edad.ToString()

                    }});

                        return item2;
                    }

                    items2 = items2.Concat(ReadLines());

                }


            }


            IEnumerable<EstadisticasVictimasViewModel> items3 = new EstadisticasVictimasViewModel[] { };


            foreach (var item in items2.GroupBy(c => new { c.Sexo, c.Edad }))
            {

                IEnumerable<EstadisticasVictimasViewModel> ReadLines()
                {
                    IEnumerable<EstadisticasVictimasViewModel> item2;

                    item2 = (new[]{new EstadisticasVictimasViewModel{
                        Sexo =  (item.Key.Sexo == "" ? "NE" : item.Key.Sexo) ,
                        Edad =  (item.Key.Edad == "99" ? "NE" : item.Key.Edad)  ,
                        CantidadV = item.Count(),
                    }});

                    return item2;
                }

                items3 = items3.Concat(ReadLines());

            }

            return items3.OrderByDescending(a => a.CantidadV);

        }

        // GET: api/RDHs/ListarPorHechoActivos
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rhechoid}")]
        public async Task<IEnumerable<RDHechosViewModel>> ListarPorHechoActivos([FromRoute] Guid rhechoid)
        {
            var delitos = await _context.RDHs
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho)
                          .Where(a => a.RHechoId == rhechoid)
                          .Where(a => a.Equiparado)
                          .ToListAsync();

            return delitos.Select(a => new RDHechosViewModel
            {
                IdRDH = a.IdRDH,
                RHechoId = a.RHechoId,

                DelitoId = a.DelitoId,
                nombreDelito = a.Delito.Nombre,
                OfiNoOfic = a.Delito.OfiNoOfi,
                altoImpacto = a.Delito.AltoImpacto,
                suceptibleMASC = a.Delito.SuceptibleMASC,

                TipoRobado = a.TipoRobado,
                TipoDeclaracion = a.TipoDeclaracion,
                ViolenciaSinViolencia = a.ViolenciaSinViolencia,
                ArmaBlanca = a.ArmaBlanca,
                ArmaFuego = a.ArmaFuego,
                ClasificaOrdenResult = a.ClasificaOrdenResult,
                ConAlgunaParteCuerpo = a.ConAlgunaParteCuerpo,
                Concurso = a.Concurso,
                ConotroElemento = a.ConotroElemento,
                Equiparado = a.Equiparado,
                GraveNoGrave = a.GraveNoGrave,
                IntensionDelito = a.IntensionDelito,
                MontoRobado = a.MontoRobado,
                ResultadoDelito = a.ResultadoDelito,
                Tipo = a.Tipo,
                TipoFuero = a.TipoFuero,
                Observaciones = a.Observaciones,
                Fechasys = a.Fechasys,
                Hipotesis = a.Hipotesis,
                DelitoEspecifico = a.DelitoEspecifico

            });

        }


        // GET: api/RDHs/ListarPorHechoTodos
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rhechoid}")]
        public async Task<IEnumerable<RDHechosViewModel>> ListarPorHechoTodos([FromRoute] Guid rhechoid)
        {
            var delitos = await _context.RDHs
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho)
                          .Where(a => a.RHechoId == rhechoid)
                          .ToListAsync();

            return delitos.Select(a => new RDHechosViewModel
            {
                IdRDH = a.IdRDH,
                RHechoId = a.RHechoId,

                DelitoId = a.DelitoId,
                nombreDelito = a.Delito.Nombre,
                OfiNoOfic = a.Delito.OfiNoOfi,
                altoImpacto = a.Delito.AltoImpacto,
                suceptibleMASC = a.Delito.SuceptibleMASC,

                TipoRobado = a.TipoRobado,
                TipoDeclaracion = a.TipoDeclaracion,
                ViolenciaSinViolencia = a.ViolenciaSinViolencia,
                ArmaBlanca = a.ArmaBlanca,
                ArmaFuego = a.ArmaFuego,
                ClasificaOrdenResult = a.ClasificaOrdenResult,
                ConAlgunaParteCuerpo = a.ConAlgunaParteCuerpo,
                Concurso = a.Concurso,
                ConotroElemento = a.ConotroElemento,
                Equiparado = a.Equiparado,
                GraveNoGrave = a.GraveNoGrave,
                IntensionDelito = a.IntensionDelito,
                MontoRobado = a.MontoRobado,
                ResultadoDelito = a.ResultadoDelito,
                Tipo = a.Tipo,
                TipoFuero = a.TipoFuero,
                Observaciones = a.Observaciones,
                Fechasys = a.Fechasys,
                Hipotesis = a.Hipotesis,
                //PersonaId = a.PersonaId,
                DelitoEspecifico = a.DelitoEspecifico,
                TipoViolencia = a.TipoViolencia,
                SubtipoSexual = a.SubtipoSexual,
                TipoInfoDigital = a.TipoInfoDigital,
                MedioDigital = a.MedioDigital,
                InstrumentosComision = a.InstrumentosComision,
                GradoDelito = a.GradoDelito

            });

        }


        // GET: api/RDHs/DelitosTotalesEstadistica
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{DelitosTotalesEstadistica}")]
        public async Task<IEnumerable<DelitosTotalesViewModel>> DelitosTotalesEstadistica([FromQuery] DelitosTotalesEstadistica DelitosTotalesEstadistica)
        {
            var rdhst = await _context.RDHs
                .Where(a => DelitosTotalesEstadistica.DatosGenerales.Distritoact ? a.RHecho.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == DelitosTotalesEstadistica.DatosGenerales.Distrito : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.DatosGenerales.Dspact ? a.RHecho.ModuloServicio.Agencia.DSP.IdDSP == DelitosTotalesEstadistica.DatosGenerales.Dsp : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.DatosGenerales.Agenciaact ? a.RHecho.ModuloServicio.Agencia.IdAgencia == DelitosTotalesEstadistica.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.RHecho.FechaElevaNuc2 >= DelitosTotalesEstadistica.DatosGenerales.Fechadesde)
                .Where(a => a.RHecho.FechaElevaNuc2 <= DelitosTotalesEstadistica.DatosGenerales.Fechahasta)
                .Where(a => DelitosTotalesEstadistica.EstatusEtapaCarpeta.EtapaActual != "null" ? a.RHecho.NUCs.Etapanuc == DelitosTotalesEstadistica.EstatusEtapaCarpeta.EtapaActual : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.EstatusEtapaCarpeta.StatusActual != "null" ? a.RHecho.NUCs.StatusNUC == DelitosTotalesEstadistica.EstatusEtapaCarpeta.StatusActual : 1 == 1)
                .Where(a => a.RHecho.FechaHoraSuceso2.TimeOfDay >= DelitosTotalesEstadistica.HoraLugarSuceso.Horainicio.TimeOfDay && a.RHecho.FechaHoraSuceso2.TimeOfDay <= DelitosTotalesEstadistica.HoraLugarSuceso.HoraFin.TimeOfDay)

                .Where(a => DelitosTotalesEstadistica.Delito.Delitoactivo ? a.DelitoId == DelitosTotalesEstadistica.Delito.DelitoNombre : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.Delitoactivo && DelitosTotalesEstadistica.Delito.Delitoespecifico != "null" ? a.DelitoEspecifico == DelitosTotalesEstadistica.Delito.Delitoespecifico : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.Tipofuero != "null" ? a.TipoFuero == DelitosTotalesEstadistica.Delito.Tipofuero : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.Requisitoprocedibilidad != "null" ? a.TipoDeclaracion == DelitosTotalesEstadistica.Delito.Requisitoprocedibilidad : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.Gradoejecucion != "null" ? a.ResultadoDelito == DelitosTotalesEstadistica.Delito.Gradoejecucion : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.Prisionpreventiva != "null" ? a.GraveNoGrave == DelitosTotalesEstadistica.Delito.Prisionpreventiva : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.Formacomision != "null" ? a.IntensionDelito == DelitosTotalesEstadistica.Delito.Formacomision : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.ViolenciaSinViolencia != "null" ? a.ViolenciaSinViolencia == DelitosTotalesEstadistica.Delito.ViolenciaSinViolencia : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.Modalidaddelito != "null" ? a.Tipo == DelitosTotalesEstadistica.Delito.Modalidaddelito : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.ClasificaOrdenResult != "null" ? a.ClasificaOrdenResult == DelitosTotalesEstadistica.Delito.ClasificaOrdenResult : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.Montorobado > 0 ? a.MontoRobado == DelitosTotalesEstadistica.Delito.Montorobado : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.Descripcionrobado != "null" ? a.TipoRobado == DelitosTotalesEstadistica.Delito.Descripcionrobado : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.ViolenciaSinViolencia == "Violencia física" && DelitosTotalesEstadistica.Delito.Armablanca == "Si" ? a.ArmaBlanca : DelitosTotalesEstadistica.Delito.ViolenciaSinViolencia == "Violencia física" && DelitosTotalesEstadistica.Delito.Armablanca == "No" ? !a.ArmaBlanca : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.ViolenciaSinViolencia == "Violencia física" && DelitosTotalesEstadistica.Delito.Armafuego == "Si" ? a.ArmaFuego : DelitosTotalesEstadistica.Delito.ViolenciaSinViolencia == "Violencia física" && DelitosTotalesEstadistica.Delito.Armafuego == "No" ? !a.ArmaFuego : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.ViolenciaSinViolencia == "Violencia física" && DelitosTotalesEstadistica.Delito.ConOtroElemento != "null" ? a.ConotroElemento == DelitosTotalesEstadistica.Delito.ConOtroElemento : 1 == 1)
                .Where(a => DelitosTotalesEstadistica.Delito.ViolenciaSinViolencia == "Violencia física" && DelitosTotalesEstadistica.Delito.ConAlgunaParteCuerpo != "null" ? a.ConAlgunaParteCuerpo == DelitosTotalesEstadistica.Delito.ConAlgunaParteCuerpo : 1 == 1)
                .ToListAsync();

            IEnumerable<AcDatosDelitosTotalesViewModel> items = new AcDatosDelitosTotalesViewModel[] { };

           
            foreach (var rdh in rdhst)
            {
                var direcciondelito = await _context.DireccionDelitos
                    .Where(a => a.RHechoId == rdh.RHechoId)
                    .Where(a => DelitosTotalesEstadistica.HoraLugarSuceso.Municipio != "null" ? a.Municipio == DelitosTotalesEstadistica.HoraLugarSuceso.Municipio : 1 == 1)
                    .Where(a => DelitosTotalesEstadistica.HoraLugarSuceso.Localidad != "null" ? a.Localidad == DelitosTotalesEstadistica.HoraLugarSuceso.Localidad : 1 == 1)
                    .Where(a => DelitosTotalesEstadistica.HoraLugarSuceso.CP != 0 ? a.CP == DelitosTotalesEstadistica.HoraLugarSuceso.CP : 1 == 1)
                    .Where(a => DelitosTotalesEstadistica.HoraLugarSuceso.Calle != "null" ? a.Calle == DelitosTotalesEstadistica.HoraLugarSuceso.Calle : 1 == 1)
                    .Where(a => DelitosTotalesEstadistica.HoraLugarSuceso.LugarEspecifico != "null" ? a.LugarEspecifico == DelitosTotalesEstadistica.HoraLugarSuceso.LugarEspecifico : 1 == 1)
                    .FirstOrDefaultAsync();

                if(direcciondelito != null)
                {
                    IEnumerable<AcDatosDelitosTotalesViewModel> ReadLines()
                    {
                        IEnumerable<AcDatosDelitosTotalesViewModel> item2;

                        item2 = (new[]{new AcDatosDelitosTotalesViewModel{
                                IdRDH = rdh.IdRDH,
                                IdRhecho = direcciondelito.RHechoId
                            }});

                        return item2;
                    }

                    items = items.Concat(ReadLines());

                }

                
            }

            IEnumerable<DelitosTotalesViewModel> items2 = new DelitosTotalesViewModel[] { };

            IEnumerable<DelitosTotalesViewModel> ReadLines2( int cantidad, string tipo)
            {
                IEnumerable<DelitosTotalesViewModel> item2;

                item2 = (new[]{new DelitosTotalesViewModel{
                                Tipo = tipo,
                                Cantidad = cantidad
                            }});

                return item2;
            }


            items2 = items2.Concat(ReadLines2(items.Count(), "Total de delitos"));

            return items2;

        }


        private bool RDHExists(Guid id)
        {
            return _context.RDHs.Any(e => e.IdRDH == id);
        }


        // GET: api/RDH/Eliminar
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
                var consultaDelito = await _context.RDHs.Where(a => a.IdRDH == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaDelito == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningun delito con la información enviada" });
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
                            MovimientoId = new Guid("4d2fd4ff-269b-4153-8eb1-656d42bf2c57")
                        };

                        ctx.Add(laRegistro);
                        LogRDH RDH = new LogRDH
                        {
                            LogAdmonId = gLog,
                            IdRDH = consultaDelito.IdRDH,
                            DelitoId = consultaDelito.DelitoId,
                            RHechoId = consultaDelito.RHechoId,
                            ReclasificacionDelito = consultaDelito.ReclasificacionDelito,
                            TipoFuero = consultaDelito.TipoFuero,
                            TipoDeclaracion = consultaDelito.TipoDeclaracion,
                            ResultadoDelito = consultaDelito.ResultadoDelito,
                            GraveNoGrave = consultaDelito.GraveNoGrave,
                            IntensionDelito = consultaDelito.IntensionDelito,
                            ViolenciaSinViolencia = consultaDelito.ViolenciaSinViolencia,
                            Equiparado = consultaDelito.Equiparado,
                            Tipo = consultaDelito.Tipo,
                            Concurso = consultaDelito.Concurso,
                            ClasificaOrdenResult = consultaDelito.ClasificaOrdenResult,
                            ArmaFuego = consultaDelito.ArmaFuego,
                            ArmaBlanca = consultaDelito.ArmaBlanca,
                            Observaciones = consultaDelito.Observaciones,
                            ConAlgunaParteCuerpo = consultaDelito.ConAlgunaParteCuerpo,
                            ConotroElemento = consultaDelito.ConotroElemento,
                            TipoRobado = consultaDelito.TipoRobado,
                            MontoRobado = consultaDelito.MontoRobado,
                            Fechasys = consultaDelito.Fechasys,
                            Hipotesis = consultaDelito.Hipotesis,
                            DelitoEspecifico = consultaDelito.DelitoEspecifico
                        };
                        ctx.Add(RDH);
                        _context.Remove(consultaDelito);

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
            return Ok(new { res = "success", men = "Delito eliminado Correctamente" });
        }

        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/RDHs/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var delitos = await _context.RDHs.Where(a => a.RHechoId == model.IdRHecho).ToListAsync();



                if (delitos == null)
                {
                    return Ok();

                }
                


                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {


                    foreach (RDH rdhActual in delitos)
                    {

                        var insertarRDH = await ctx.RDHs.FirstOrDefaultAsync(a => a.IdRDH == rdhActual.IdRDH);

                        if (insertarRDH == null)
                        {
                            insertarRDH = new RDH();
                            ctx.RDHs.Add(insertarRDH);
                        }

                        insertarRDH.IdRDH = rdhActual.IdRDH;
                        insertarRDH.DelitoId = rdhActual.DelitoId;
                        insertarRDH.RHechoId = rdhActual.RHechoId;
                        insertarRDH.ReclasificacionDelito = rdhActual.ReclasificacionDelito;
                        insertarRDH.TipoFuero = rdhActual.TipoFuero;
                        insertarRDH.TipoDeclaracion = rdhActual.TipoDeclaracion;
                        insertarRDH.ResultadoDelito = rdhActual.ResultadoDelito;
                        insertarRDH.GraveNoGrave = rdhActual.GraveNoGrave;
                        insertarRDH.IntensionDelito = rdhActual.IntensionDelito;
                        insertarRDH.ViolenciaSinViolencia = rdhActual.ViolenciaSinViolencia;
                        insertarRDH.Equiparado = rdhActual.Equiparado;
                        insertarRDH.Tipo = rdhActual.Tipo;
                        insertarRDH.Concurso = rdhActual.Concurso;
                        insertarRDH.ClasificaOrdenResult = rdhActual.ClasificaOrdenResult;
                        insertarRDH.ArmaFuego = rdhActual.ArmaFuego;
                        insertarRDH.ArmaBlanca = rdhActual.ArmaBlanca;
                        insertarRDH.Observaciones = rdhActual.Observaciones;
                        insertarRDH.ConAlgunaParteCuerpo = rdhActual.ConAlgunaParteCuerpo;
                        insertarRDH.ConotroElemento = rdhActual.ConotroElemento;
                        insertarRDH.TipoRobado = rdhActual.TipoRobado;
                        insertarRDH.MontoRobado = rdhActual.MontoRobado;
                        insertarRDH.Fechasys = rdhActual.Fechasys;
                        insertarRDH.Hipotesis = rdhActual.Hipotesis;
                        insertarRDH.DelitoEspecifico = rdhActual.DelitoEspecifico;
                        insertarRDH.TipoViolencia = rdhActual.TipoViolencia;
                        insertarRDH.SubtipoSexual = rdhActual.SubtipoSexual;
                        insertarRDH.TipoInfoDigital = rdhActual.TipoInfoDigital;
                        insertarRDH.MedioDigital = rdhActual.MedioDigital;
                        insertarRDH.InstrumentosComision = rdhActual.InstrumentosComision;
                        insertarRDH.GradoDelito = rdhActual.GradoDelito;


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

        // GET: api/RDHs/ListarPorHecho
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-IL,Recepción")]
        [HttpGet("[action]")]
        public async Task<IActionResult> ListarTviolencia()
        {

            try
            {

                var violencia = await _context.TipoViolencias
                              .OrderBy(a => a.Tipo)
                              .ToListAsync();
                              

                return Ok(violencia.Select(a => new TipoViolenciaViewModel
                {
                  IdTipoViolencia = a.IdTipoViolencia,
                  TipoViolencia = a.Tipo

                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }


        }

        //Api para guardar Instrumentos de Comision
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> InsertarInstrumentos([FromBody] InsertInstrumentosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instrumento = await _context.RDHs.FirstOrDefaultAsync(a => a.IdRDH == model.IdRDH);

            if(instrumento == null)
            {
                return NotFound();
            }

            instrumento.InstrumentosComision = model.InstrumentosComision;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
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