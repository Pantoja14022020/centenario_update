using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SIIGPP.CAT.Models.MediaFiliacionDesaparecido;
using SIIGPP.Entidades.M_Cat.MedFiliacionDesaparecido;
using SIIGPP.CAT.Models.MediaAfiliacion;
using Microsoft.Win32;
using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.CAT.Models.RDHechos;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaFiliacionDesaparecidoController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public MediaFiliacionDesaparecidoController(DbContextSIIGPP context)
            {
                _context = context;
            }

        // PUT: api/MediaFiliacionDesaparecido/ListarPorRHecho/{RHechoId}
        //[Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] MediaFiliacionDesaparecidoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var MFD = await _context.MediaFiliacionDesaparecidos.FirstOrDefaultAsync(a => a.IdMediaFiliacionDesaparecido == model.IdMediaFiliacionDesaparecido);

            if (MFD == null)
            {
                return NotFound();
            }

            MFD.Manchas = model.Manchas;
            MFD.TipoManchas = model.TipoManchas;
            MFD.ManchasOtrasCyU = model.ManchasOtrasCyU;
            MFD.Lunares = model.Lunares;
            MFD.LunaresCyU = model.LunaresCyU;
            MFD.Cicatrices = model.Cicatrices;
            MFD.TipoCicatrices = model.TipoCicatrices;
            MFD.CicatricesTraumaticasCyU = model.CicatricesTraumaticasCyU;
            MFD.CicatricesQuirurgicasTipo = model.CicatricesQuirurgicasTipo;
            MFD.CicatricesQuirurgicasCesareaNumero = model.CicatricesQuirurgicasCesareaNumero;
            MFD.CicatricesQuirurgicasCesareaOrientacion = model.CicatricesQuirurgicasCesareaOrientacion;
            MFD.CicatricesQuirurgicasOperacionMyU = model.CicatricesQuirurgicasOperacionMyU;
            MFD.Tatuajes = model.Tatuajes;
            MFD.TatuajesNumero = model.TatuajesNumero;
            MFD.TatuajesDescripcion = model.TatuajesDescripcion;
            MFD.Piercing = model.Piercing;
            MFD.PiercingNumero = model.PiercingNumero;
            MFD.PiercingDescripcion = model.PiercingDescripcion;
            MFD.UñasEstado = model.UñasEstado;
            MFD.UñasNoSaludables = model.UñasNoSaludables;
            MFD.UñasPostizas = model.UñasPostizas;
            MFD.Deformidades = model.Deformidades;
            MFD.TipoDeformidades = model.TipoDeformidades;
            MFD.CongenitasUbicacion = model.CongenitasUbicacion;
            MFD.AdquiridasUbicacion = model.AdquiridasUbicacion;
            MFD.ProtesisDental = model.ProtesisDental;
            MFD.ProtesisDentalUbicacion = model.ProtesisDentalUbicacion;
            MFD.DentaduraCaracteristicas = model.DentaduraCaracteristicas;
            MFD.DentaduraDetalles = model.DentaduraDetalles;
            MFD.Traumatismos = model.Traumatismos;
            MFD.TipoTraumatismos = model.TipoTraumatismos;
            MFD.UbicacionFracturas = model.UbicacionFracturas;
            MFD.TipoLesiones = model.TipoLesiones;
            MFD.CausaMordedura = model.CausaMordedura;
            MFD.TipoLesionesOtra = model.TipoLesionesOtra;
            MFD.UbicacionLesiones = model.UbicacionLesiones;
            MFD.FacultadesMentales = model.FacultadesMentales;
            MFD.TipoRetraso = model.TipoRetraso;
            MFD.EnfermedadesCronicas = model.EnfermedadesCronicas;
            MFD.EnfermedadTipo = model.EnfermedadTipo;
            MFD.EnfermedadTiempoDiagnostico = model.EnfermedadTiempoDiagnostico;
            MFD.TratamientoEnfermedadCronica = model.TratamientoEnfermedadCronica;
            MFD.TratamientoMedicamento = model.TratamientoMedicamento;
            MFD.TratamientoPeriocidad = model.TratamientoPeriocidad;
            MFD.Alergias = model.Alergias;
            MFD.TratamientoAlergia = model.TratamientoAlergia;
            MFD.MdicamentoTratamientoAlergia = model.MdicamentoTratamientoAlergia;
            MFD.PeriocidadTratamientoAlergia = model.PeriocidadTratamientoAlergia;
            MFD.Lentes = model.Lentes;
            MFD.TipoLentes = model.TipoLentes;
            MFD.LentesGraduacion = model.LentesGraduacion;
            MFD.AparatosAuditivos = model.AparatosAuditivos;
            MFD.oidos = model.oidos;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(MFD.IdMediaFiliacionDesaparecido);
        }


        // GET: api/MediaFiliacionDesaparecido/ListarPorRHecho/{RHechoId}
        //[Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<Get_MediaFiliacionDesaparecidoViewModel>> ListarPorRhecho([FromRoute] Guid RHechoId)
        {
            var MADyMA = await _context.MediaFiliacionDesaparecidos
                                       .Include(a => a.MediaFiliacion)
                                       .Include(a => a.MediaFiliacion.Persona)
                                       .Include(a => a.MediaFiliacion.Persona.RAPs)
                                       .Where(a => a.MediaFiliacion.RHechoId == RHechoId)
                                       .Where(a => a.MediaFiliacion.Persona.Registro == true)
                                       .ToListAsync();

            foreach (var fg in MADyMA)
            {
                var RAPS = await _context.RAPs.Where(b => b.PersonaId == fg.MediaFiliacion.Persona.IdPersona).ToArrayAsync();

                fg.MediaFiliacion.Persona.RAPs[0].ClasificacionPersona = RAPS[0].ClasificacionPersona;
            }


            if(MADyMA == null)
            {
                return (IEnumerable<Get_MediaFiliacionDesaparecidoViewModel>)NotFound();
            }
            else
            {
                return MADyMA.Select((a, index) => new Get_MediaFiliacionDesaparecidoViewModel
                {
                    IdMediaFiliacionDesaparecido = a.IdMediaFiliacionDesaparecido,
                    MediaFiliacionId = a.MediaFiliacionId,
                    PersonaId = a.MediaFiliacion.PersonaId,
                    NombreImputado = a.MediaFiliacion.Persona.Nombre + " " + a.MediaFiliacion.Persona.ApellidoPaterno + " " + a.MediaFiliacion.Persona.ApellidoMaterno,
                    Registro = a.MediaFiliacion.Persona.Registro,
                    ClasificacionPersona = a.MediaFiliacion.Persona.RAPs[0].ClasificacionPersona,
                    RHechoId = a.MediaFiliacion.RHechoId,
                    Complexion = a.MediaFiliacion.Complexion,
                    Peso = a.MediaFiliacion.Peso,
                    Estatura = a.MediaFiliacion.Estatura,
                    FormaCara = a.MediaFiliacion.FormaCara,
                    ColoOjos = a.MediaFiliacion.ColoOjos,
                    Tez = a.MediaFiliacion.Tez,
                    FormaCabello = a.MediaFiliacion.FormaCabello,
                    ColorCabello = a.MediaFiliacion.ColorCabello,
                    LargoCabello = a.MediaFiliacion.LargoCabello,
                    TamañoNariz = a.MediaFiliacion.TamañoNariz,
                    TipoNariz = a.MediaFiliacion.TipoNariz,
                    GrosorLabios = a.MediaFiliacion.GrosorLabios,
                    TipoFrente = a.MediaFiliacion.TipoFrente,
                    Cejas = a.MediaFiliacion.Cejas,
                    TipoCejas = a.MediaFiliacion.TipoCejas,
                    TamañoBoca = a.MediaFiliacion.TamañoBoca,
                    TamañoOrejas = a.MediaFiliacion.TamañoOrejas,
                    FormaMenton = a.MediaFiliacion.FormaMenton,
                    TipoOjo = a.MediaFiliacion.TipoOjo,
                    TipoOjo2 = a.MediaFiliacion.Tipo2Ojos,
                    FechaSys = a.MediaFiliacion.FechaSys,
                    Distrito = a.MediaFiliacion.Distrito,
                    DirSubProc = a.MediaFiliacion.DirSubProc,
                    Agencia = a.MediaFiliacion.Agencia,
                    Usuario = a.MediaFiliacion.Usuario,
                    Puesto = a.MediaFiliacion.Puesto,
                    Numerooficio = a.MediaFiliacion.Numerooficio,
                    Gruposanguineo = a.MediaFiliacion.Gruposanguineo,
                    Calvicie = a.MediaFiliacion.Calvicie,
                    AdherenciaOreja = a.MediaFiliacion.AdherenciaOreja,
                    TratamientosQuimicosCabello = a.MediaFiliacion.TratamientosQuimicosCabello,
                    FormaOjo = a.MediaFiliacion.FormaOjo,
                    ImplantacionCeja = a.MediaFiliacion.ImplantacionCeja,
                    PuntaNariz = a.MediaFiliacion.PuntaNariz,
                    TipoMenton = a.MediaFiliacion.TipoMenton,
                    Cicatriz = a.MediaFiliacion.Cicatriz,
                    DescripcionCicatriz = a.MediaFiliacion.DescripcionCicatriz,
                    Tatuaje = a.MediaFiliacion.Tatuaje,
                    DescripcionTatuaje = a.MediaFiliacion.DescripcionTatuaje,
                    OtrasCaracteristicas = a.MediaFiliacion.OtrasCaracteristicas,
                    TamanoDental = a.MediaFiliacion.TamanoDental,
                    TratamientoDental = a.MediaFiliacion.TratamientoDental,
                    DentaduraCompleta = a.MediaFiliacion.DentaduraCompleta,
                    DientesAusentes = a.MediaFiliacion.DientesAusentes,
                    TipoDentadura = a.MediaFiliacion.TipoDentadura,
                    Pomulos = a.MediaFiliacion.Pomulos,
                    Lateralidad = a.MediaFiliacion.Lateralidad,
                    Pupilentes = a.MediaFiliacion.Pupilentes,
                    Manchas = a.Manchas,
                    TipoManchas = a.TipoManchas,
                    ManchasOtrasCyU = a.ManchasOtrasCyU,
                    Lunares = a.Lunares,
                    LunaresCyU = a.LunaresCyU,
                    Cicatrices = a.Cicatrices,
                    TipoCicatrices = a.TipoCicatrices,
                    CicatricesTraumaticasCyU = a.CicatricesTraumaticasCyU,
                    CicatricesQuirurgicasTipo = a.CicatricesQuirurgicasTipo,
                    CicatricesQuirurgicasCesareaNumero = a.CicatricesQuirurgicasCesareaNumero,
                    CicatricesQuirurgicasCesareaOrientacion = a.CicatricesQuirurgicasCesareaOrientacion,
                    CicatricesQuirurgicasOperacionMyU = a.CicatricesQuirurgicasOperacionMyU,
                    Tatuajes = a.Tatuajes,
                    TatuajesNumero = a.TatuajesNumero,
                    TatuajesDescripcion = a.TatuajesDescripcion,
                    Piercing = a.Piercing,
                    PiercingNumero = a.PiercingNumero,
                    PiercingDescripcion = a.PiercingDescripcion,
                    UñasEstado = a.UñasEstado,
                    UñasNoSaludables = a.UñasNoSaludables,
                    UñasPostizas = a.UñasPostizas,
                    Deformidades = a.Deformidades,
                    TipoDeformidades = a.TipoDeformidades,
                    CongenitasUbicacion = a.CongenitasUbicacion,
                    AdquiridasUbicacion = a.AdquiridasUbicacion,
                    ProtesisDental = a.ProtesisDental,
                    ProtesisDentalUbicacion = a.ProtesisDentalUbicacion,
                    DentaduraCaracteristicas = a.DentaduraCaracteristicas,
                    DentaduraDetalles = a.DentaduraDetalles,
                    Traumatismos = a.Traumatismos,
                    TipoTraumatismos = a.TipoTraumatismos,
                    UbicacionFracturas = a.UbicacionFracturas,
                    TipoLesiones = a.TipoLesiones,
                    CausaMordedura = a.CausaMordedura,
                    TipoLesionesOtra = a.TipoLesionesOtra,
                    UbicacionLesiones = a.UbicacionLesiones,
                    FacultadesMentales = a.FacultadesMentales,
                    TipoRetraso = a.TipoRetraso,
                    EnfermedadesCronicas = a.EnfermedadesCronicas,
                    EnfermedadTipo = a.EnfermedadTipo,
                    EnfermedadTiempoDiagnostico = a.EnfermedadTiempoDiagnostico,
                    TratamientoEnfermedadCronica = a.TratamientoEnfermedadCronica,
                    TratamientoMedicamento = a.TratamientoMedicamento,
                    TratamientoPeriocidad = a.TratamientoPeriocidad,
                    Alergias = a.Alergias,
                    TratamientoAlergia = a.TratamientoAlergia,
                    MdicamentoTratamientoAlergia = a.MdicamentoTratamientoAlergia,
                    PeriocidadTratamientoAlergia = a.PeriocidadTratamientoAlergia,
                    Lentes = a.Lentes,
                    TipoLentes = a.TipoLentes,
                    LentesGraduacion = a.LentesGraduacion,
                    AparatosAuditivos = a.AparatosAuditivos,
                    oidos = a.oidos,
                    Pupilentes2 = a.MediaFiliacion.Pupilentes2,
                    
                });
            }
        }

        //POST: api/MediaFiliacionDesaparecido/Crear
        //[Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] MediaFiliacionDesaparecidoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MediaFiliacionDesaparecido MFD = new MediaFiliacionDesaparecido
            {
                MediaFiliacionId = model.MediaFiliacionId,
                Manchas = model.Manchas,
                TipoManchas = model.TipoManchas,
                ManchasOtrasCyU = model.ManchasOtrasCyU,
                Lunares = model.Lunares,
                LunaresCyU = model.LunaresCyU,
                Cicatrices = model.Cicatrices,
                TipoCicatrices = model.TipoCicatrices,
                CicatricesTraumaticasCyU = model.CicatricesTraumaticasCyU,
                CicatricesQuirurgicasTipo = model.CicatricesQuirurgicasTipo,
                CicatricesQuirurgicasCesareaNumero = model.CicatricesQuirurgicasCesareaNumero,
                CicatricesQuirurgicasCesareaOrientacion = model.CicatricesQuirurgicasCesareaOrientacion,
                CicatricesQuirurgicasOperacionMyU = model.CicatricesQuirurgicasOperacionMyU,
                Tatuajes = model.Tatuajes,
                TatuajesNumero = model.TatuajesNumero,
                TatuajesDescripcion = model.TatuajesDescripcion,
                Piercing = model.Piercing,
                PiercingNumero = model.PiercingNumero,
                PiercingDescripcion = model.PiercingDescripcion,
                UñasEstado = model.UñasEstado,
                UñasNoSaludables = model.UñasNoSaludables,
                UñasPostizas = model.UñasPostizas,
                Deformidades = model.Deformidades,
                TipoDeformidades = model.TipoDeformidades,
                CongenitasUbicacion = model.CongenitasUbicacion,
                AdquiridasUbicacion = model.AdquiridasUbicacion,
                ProtesisDental = model.ProtesisDental,
                ProtesisDentalUbicacion = model.ProtesisDentalUbicacion,
                DentaduraCaracteristicas = model.DentaduraCaracteristicas,
                DentaduraDetalles = model.DentaduraDetalles,
                Traumatismos = model.Traumatismos,
                TipoTraumatismos = model.TipoTraumatismos,
                UbicacionFracturas = model.UbicacionFracturas,
                TipoLesiones = model.TipoLesiones,
                CausaMordedura = model.CausaMordedura,
                TipoLesionesOtra = model.TipoLesionesOtra,
                UbicacionLesiones = model.UbicacionLesiones,
                FacultadesMentales = model.FacultadesMentales,
                TipoRetraso = model.TipoRetraso,
                EnfermedadesCronicas = model.EnfermedadesCronicas,
                EnfermedadTipo = model.EnfermedadTipo,
                EnfermedadTiempoDiagnostico = model.EnfermedadTiempoDiagnostico,
                TratamientoEnfermedadCronica = model.TratamientoEnfermedadCronica,
                TratamientoMedicamento = model.TratamientoMedicamento,
                TratamientoPeriocidad =  model.TratamientoPeriocidad,
                Alergias = model.Alergias,
                TratamientoAlergia = model.TratamientoAlergia,
                MdicamentoTratamientoAlergia = model.MdicamentoTratamientoAlergia,
                PeriocidadTratamientoAlergia = model.PeriocidadTratamientoAlergia,
                Lentes = model.Lentes,  
                TipoLentes = model.TipoLentes,
                LentesGraduacion = model.LentesGraduacion,
                AparatosAuditivos = model.AparatosAuditivos,
                oidos = model.oidos,
            };

            _context.MediaFiliacionDesaparecidos.Add(MFD);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(MFD.IdMediaFiliacionDesaparecido);
        }

        // GET: api/MediaAfiliacions/ListarPorRHecho/{RHechoId}
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{idmediaafiliacion}")]
        public async Task<IActionResult> ComprobarMADesaparecido([FromRoute] Guid idmediaafiliacion)
        {
            var ma = await _context.MediaFiliacionDesaparecidos
                          .Where(a => a.MediaFiliacionId == idmediaafiliacion)
                          .FirstOrDefaultAsync();

            if (ma == null)
            {

                return Ok(new { ner = false });
            }
            else
            {
                return Ok(new { ner = true });
            }
           


        }
    }
}
