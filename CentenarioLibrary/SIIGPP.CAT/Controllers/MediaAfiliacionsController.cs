using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SIIGPP.CAT.Models.MediaAfiliacion;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.MedAfiliacion;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Configuracion.Cat_Configuracion;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaAfiliacionsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public MediaAfiliacionsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        //DELETE: api/MediaAfiliacions/eliminarMFporID
        //[Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpDelete("[action]/{idMediaFiliacion}")]
        public async Task<IActionResult> eliminarMFporID (Guid idMediaFiliacion) 
        {
            var mediaFiliacion = await _context.MediaAfiliacions
                                               .Where(a => a.idMediaAfiliacion == idMediaFiliacion)
                                               .FirstOrDefaultAsync();

            if(mediaFiliacion == null)
            {
                return NotFound();
            }

            _context.MediaAfiliacions.Remove(mediaFiliacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/MediaAfiliacions/ListarPorRHecho/{RHechoId}
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<MediaAfiliacionViewModel>> ListarPorRhecho([FromRoute] Guid RHechoId)
        {
            var ma = await _context.MediaAfiliacions
                          .Include(a => a.Persona)
                          .Include(a => a.Persona.RAPs)
                          .OrderByDescending(a => a.FechaSys)
                          .Where(a => a.RHechoId == RHechoId).ToListAsync();

            return ma.Select((a,index) => new MediaAfiliacionViewModel
            {
                idMediaAfiliacion = a.idMediaAfiliacion,
                PersonaId = a.PersonaId,
                NombreImputado = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Registro = a.Persona.Registro,
                ClasificacionPersona = a.Persona.RAPs[0].ClasificacionPersona,
                RHechoId = a.RHechoId,
                Complexion = a.Complexion,
                Peso = a.Peso,
                Estatura = a.Estatura,
                FormaCara = a.FormaCara,
                ColoOjos = a.ColoOjos,
                Tez = a.Tez,
                FormaCabello = a.FormaCabello,
                ColorCabello = a.ColorCabello,
                LargoCabello = a.LargoCabello,
                TamañoNariz = a.TamañoNariz,
                TipoNariz = a.TipoNariz,
                GrosorLabios = a.GrosorLabios,
                TipoFrente = a.TipoFrente,
                Cejas = a.Cejas,
                TipoCejas = a.TipoCejas,
                TamañoBoca = a.TamañoBoca,
                TamañoOrejas = a.TamañoOrejas,
                FormaMenton = a.FormaMenton,
                TipoOjo = a.TipoOjo,
                Tipo2Ojos = a.Tipo2Ojos,
                FechaSys = a.FechaSys,
                Distrito = a.Distrito,
                DirSubProc = a.DirSubProc,
                Agencia = a.Agencia,
                Usuario = a.Usuario,
                Puesto = a.Puesto,
                Numerooficio = a.Numerooficio,
                Gruposanguineo = a.Gruposanguineo,
                Calvicie = a.Calvicie,
                AdherenciaOreja = a.AdherenciaOreja,
                TratamientosQuimicosCabello = a.TratamientosQuimicosCabello,
                FormaOjo = a.FormaOjo,
                ImplantacionCeja = a.ImplantacionCeja,
                PuntaNariz = a.PuntaNariz,
                TipoMenton = a.TipoMenton,
                Cicatriz = a.Cicatriz,
                DescripcionCicatriz = a.DescripcionCicatriz,
                Tatuaje = a.Tatuaje,
                DescripcionTatuaje = a.DescripcionTatuaje,
                OtrasCaracteristicas = a.OtrasCaracteristicas,
                TamanoDental = a.TamanoDental,
                TratamientoDental = a.TratamientoDental,
                DentaduraCompleta = a.DentaduraCompleta,
                DientesAusentes = a.DientesAusentes,
                TipoDentadura = a.TipoDentadura,
                Pomulos = a.Pomulos,
                Lateralidad = a.Lateralidad,
                Pupilentes = a.Pupilentes,
                Pupilentes2 = a.Pupilentes2,

            });

        }

        // PUT: api/MediaAfiliacions/Actualizar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var ma = await _context.MediaAfiliacions.FirstOrDefaultAsync(a => a.idMediaAfiliacion == model.idMediaAfiliacion);

            if (ma == null)
            {
                return NotFound();
            }


            ma.idMediaAfiliacion = model.idMediaAfiliacion;
            ma.PersonaId = model.PersonaId;
            ma.RHechoId = model.RHechoId;
            ma.Complexion = model.Complexion;
            ma.Peso = model.Peso;
            ma.Estatura = model.Estatura;
            ma.FormaCara = model.FormaCara;
            ma.ColoOjos = model.ColoOjos;
            ma.Tez = model.Tez;
            ma.FormaCabello = model.FormaCabello;
            ma.ColorCabello = model.ColorCabello;
            ma.LargoCabello = model.LargoCabello;
            ma.TamañoNariz = model.TamañoNariz;
            ma.TipoNariz = model.TipoNariz;
            ma.GrosorLabios = model.GrosorLabios;
            ma.TipoFrente = model.TipoFrente;
            ma.Cejas = model.Cejas;
            ma.TipoCejas = model.TipoCejas;
            ma.TamañoBoca = model.TamañoBoca;
            ma.TamañoOrejas = model.TamañoOrejas;
            ma.FormaMenton = model.FormaMenton;
            ma.TipoOjo = model.TipoOjo;
            ma.Tipo2Ojos = model.tipo2Ojos;
            ma.Gruposanguineo = model.Gruposanguineo;
            ma.Calvicie = model.Calvicie;
            ma.AdherenciaOreja = model.AdherenciaOreja;
            ma.TratamientosQuimicosCabello = model.TratamientosQuimicosCabello;
            ma.FormaOjo = model.FormaOjo;
            ma.ImplantacionCeja = model.ImplantacionCeja;
            ma.PuntaNariz = model.PuntaNariz;
            ma.TipoMenton = model.TipoMenton;
            ma.Cicatriz = model.Cicatriz;
            ma.DescripcionCicatriz = model.DescripcionCicatriz;
            ma.Tatuaje = model.Tatuaje;
            ma.DescripcionTatuaje = model.DescripcionTatuaje;
            ma.OtrasCaracteristicas = model.OtrasCaracteristicas;
            ma.TamanoDental = model.TamanoDental;
            ma.TratamientoDental = model.TratamientoDental;
            ma.DentaduraCompleta = model.DentaduraCompleta;
            ma.DientesAusentes = model.DientesAusentes;
            ma.TipoDentadura = model.TipoDentadura;
            ma.Pomulos = model.Pomulos;
            ma.Lateralidad = model.Lateralidad;
            ma.Pupilentes = model.Pupilentes;
            ma.Pupilentes2 = model.Pupilentes2;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(ma.idMediaAfiliacion);
        }

        // POST: api/MediaAfiliacions/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {

            Guid idmediaafiliacion;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DateTime fecha = System.DateTime.Now;
            MediaAfiliacion ma = new MediaAfiliacion
            { 
                PersonaId = model.PersonaId,
                RHechoId = model.RHechoId,
                Complexion = model.Complexion,
                Peso = model.Peso,
                Estatura = model.Estatura,
                FormaCara = model.FormaCara,
                ColoOjos = model.ColoOjos,
                Tez = model.Tez,
                FormaCabello = model.FormaCabello,
                ColorCabello = model.ColorCabello,
                LargoCabello = model.LargoCabello,
                TamañoNariz = model.TamañoNariz,
                TipoNariz = model.TipoNariz,
                GrosorLabios = model.GrosorLabios,
                TipoFrente = model.TipoFrente,
                Cejas = model.Cejas,
                TipoCejas = model.TipoCejas,
                TamañoBoca = model.TamañoBoca,
                TamañoOrejas = model.TamañoOrejas,
                FormaMenton = model.FormaMenton,
                TipoOjo = model.TipoOjo,
                Tipo2Ojos = model.tipo2Ojos,
                FechaSys = fecha,
                Distrito = model.Distrito,
                DirSubProc = model.DirSubProc,
                Agencia = model.Agencia,
                Usuario = model.Usuario,
                Puesto = model.Puesto,
                Numerooficio = model.Numerooficio,
                Gruposanguineo = model.Gruposanguineo,
                Calvicie = model.Calvicie,
                AdherenciaOreja = model.AdherenciaOreja,
                TratamientosQuimicosCabello = model.TratamientosQuimicosCabello,
                FormaOjo = model.FormaOjo,
                ImplantacionCeja = model.ImplantacionCeja,
                PuntaNariz = model.PuntaNariz,
                TipoMenton = model.TipoMenton,
                Cicatriz = model.Cicatriz,
                DescripcionCicatriz = model.DescripcionCicatriz,
                Tatuaje = model.Tatuaje,
                DescripcionTatuaje = model.DescripcionTatuaje,
                OtrasCaracteristicas = model.OtrasCaracteristicas,
                TamanoDental = model.TamanoDental,
                TratamientoDental = model.TratamientoDental,
                DentaduraCompleta = model.DentaduraCompleta,
                DientesAusentes = model.DientesAusentes,
                TipoDentadura = model.TipoDentadura,
                Pomulos = model.Pomulos,
                Lateralidad = model.Lateralidad,
                Pupilentes = model.Pupilentes,
                Pupilentes2 = model.Pupilentes2,

            };

            _context.MediaAfiliacions.Add(ma);
            try
            {
                await _context.SaveChangesAsync();
                idmediaafiliacion = ma.idMediaAfiliacion;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
            return Ok(ma.idMediaAfiliacion);

        }
        private bool MediaAfiliacionExists(Guid id)
        {
            return _context.MediaAfiliacions.Any(e => e.idMediaAfiliacion == id);
        }
    }
}