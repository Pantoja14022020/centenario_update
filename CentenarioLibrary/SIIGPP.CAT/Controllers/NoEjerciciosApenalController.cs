using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.NoEjerciciosApenal;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.NoEjerciciosApenal;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoEjerciciosApenalController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public NoEjerciciosApenalController(DbContextSIIGPP context)
        {
            _context = context;
        } 

    // GET: api/NoEjerciciosApenal/ListarporRHecho
    [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
    [HttpGet("[action]/{RHechoId}")]
    public async Task<IEnumerable<NoEjerciciosApenalViewModel>> ListarporRHecho([FromRoute]Guid RHechoId)
    {
        var NEAP = await _context.noEjercicioApenals
            .Where(a => a.RHechoId == RHechoId)
            .OrderByDescending(a => a.FechaSys)
            .ToListAsync();

        return NEAP.Select(a => new NoEjerciciosApenalViewModel
        {
            IdNoEjercicioApenal = a.IdNoEjercicioApenal,
            RHechoId = a.RHechoId,
            Op1 = a.Op1,
            Op2 = a.Op2,
            Op3 = a.Op3,
            Op4 = a.Op4,
            FechaEntrevista = a.FechaEntrevista,
            FechaHecho = a.FechaHecho,
            Delito = a.Delito,
            Denunciante = a.Denunciante,
            ArticulosCp = a.ArticulosCp,
            OficioQuerella = a.OficioQuerella,
            ArticuloLegislador = a.ArticuloLegislador,
            TipoQuerella = a.TipoQuerella,
            TFechaQuerella = a.TFechaQuerella,
            FFechaLimiteQI = a.FFechaLimiteQI,
            FFechaInterposicionMp = a.FFechaInterposicionMp,
            PunibilidadMinimo = a.PunibilidadMinimo,
            PunibilidadMaximo = a.PunibilidadMaximo,
            TipoPrescripcion = a.TipoPrescripcion,
            Ttipodelito = a.Ttipodelito,
            TAccionPenalFecha = a.TAccionPenalFecha,
            TUltimoActoInvestigacion = a.TUltimoActoInvestigacion,
            TOperacionAritmeticaFecha = a.TOperacionAritmeticaFecha,
            FechaUltimoActo = a.FechaUltimoActo,
            FechaEjercerAcion = a.FechaEjercerAcion,
            FechaResolucionconsulta = a.FechaResolucionconsulta,
            Art123 = a.Art123,
            PerdonOfendido = a.PerdonOfendido,
            PONumeroFirmas = a.PONumeroFirmas,
            PONombreFirmas = a.PONombreFirmas,
            POFechaPerdon = a.POFechaPerdon,
            POViolenciaMuMeOG = a.POViolenciaMuMeOG,
            POVReparacionDaño = a.POVReparacionDaño,
            POVFITratamiento = a.POVFITratamiento,
            POVFFTratamiento = a.POVFFTratamiento,
            ExcluyenteDelito = a.ExcluyenteDelito,
            EDHipotesis = a.EDHipotesis,
            EDRazonar = a.EDRazonar,
            NoConstituyeDelito = a.NoConstituyeDelito,
            NCDElementospenal = a.NCDElementospenal,
            NCDRazonar = a.NCDRazonar,
            ExentoResponsabilidadPenal = a.ExentoResponsabilidadPenal,
            ERPCausaInculpabilidad = a.ERPCausaInculpabilidad,
            ERPRazonar = a.ERPRazonar,
            ImputadoFallecio = a.ImputadoFallecio,
            IFImputadoNombre = a.IFImputadoNombre,
            IFFechaFallecimiento = a.IFFechaFallecimiento,
            NumeroOficio = a.NumeroOficio,
            Tipopena = a.Tipopena,
            DiasTranscurridos = a.DiasTranscurridos,
            MesesTranscurridos = a.MesesTranscurridos,
            AnosTranscurridos = a.AnosTranscurridos,
            FechaSys =a.FechaSys,
            UDistrito = a.UDistrito,
            USubproc = a.USubproc,
            UAgencia = a.UAgencia,
            UUsuario = a.UUsuario,
            UPuesto = a.UPuesto,
            UModulo = a.UModulo

        });

    }


        // POST: api/NoEjerciciosApenal/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            NoEjercicioApenal NEAP = new NoEjercicioApenal
            {
                RHechoId = model.RHechoId,
                Op1 = model.Op1,
                Op2 = model.Op2,
                Op3 = model.Op3,
                Op4 = model.Op4,
                FechaEntrevista = model.FechaEntrevista,
                FechaHecho = model.FechaHecho,
                Delito = model.Delito,
                Denunciante = model.Denunciante,
                ArticulosCp = model.ArticulosCp,
                OficioQuerella = model.OficioQuerella,
                ArticuloLegislador = model.ArticuloLegislador,
                TipoQuerella = model.TipoQuerella,
                TFechaQuerella = model.TFechaQuerella,
                FFechaLimiteQI = model.FFechaLimiteQI,
                FFechaInterposicionMp = model.FFechaInterposicionMp, 
                TipoPrescripcion = model.TipoPrescripcion,
                PunibilidadMinimo = model.PunibilidadMinimo,
                PunibilidadMaximo = model.PunibilidadMaximo,
                Tipopena = model.Tipopena,
                Ttipodelito = model.Ttipodelito,
                TAccionPenalFecha = model.TAccionPenalFecha,
                TUltimoActoInvestigacion = model.TUltimoActoInvestigacion,
                TOperacionAritmeticaFecha = model.TOperacionAritmeticaFecha,
                DiasTranscurridos = model.DiasTranscurridos,
                MesesTranscurridos = model.MesesTranscurridos,
                AnosTranscurridos = model.AnosTranscurridos,
                FechaUltimoActo = model.FechaUltimoActo,
                FechaEjercerAcion = model.FechaEjercerAcion,
                FechaResolucionconsulta = model.FechaResolucionconsulta,
                Art123 = model.Art123,
                PerdonOfendido = model.PerdonOfendido,
                PONumeroFirmas = model.PONumeroFirmas,
                PONombreFirmas = model.PONombreFirmas,
                POFechaPerdon = model.POFechaPerdon,
                POViolenciaMuMeOG = model.POViolenciaMuMeOG,
                POVReparacionDaño = model.POVReparacionDaño,
                POVFITratamiento = model.POVFITratamiento,
                POVFFTratamiento = model.POVFFTratamiento,
                ExcluyenteDelito = model.ExcluyenteDelito,
                EDHipotesis = model.EDHipotesis,
                EDRazonar = model.EDRazonar,
                NoConstituyeDelito = model.NoConstituyeDelito,
                NCDElementospenal = model.NCDElementospenal,
                NCDRazonar = model.NCDRazonar,
                ExentoResponsabilidadPenal = model.ExentoResponsabilidadPenal,
                ERPCausaInculpabilidad = model.ERPCausaInculpabilidad,
                ERPRazonar = model.ERPRazonar,
                ImputadoFallecio = model.ImputadoFallecio,
                IFImputadoNombre = model.IFImputadoNombre,
                IFFechaFallecimiento = model.IFFechaFallecimiento,
                NumeroOficio = model.NumeroOficio,
                FechaSys = fecha,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                UUsuario = model.UUsuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo
            };

            _context.noEjercicioApenals.Add(NEAP);
            try
            {
                await _context.SaveChangesAsync();
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

    }
}
