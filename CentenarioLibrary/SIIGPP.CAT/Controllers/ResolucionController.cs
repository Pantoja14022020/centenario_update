using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.Resolucion;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Resoluciones;
using SIIGPP.CAT.FilterClass;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResolucionController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public ResolucionController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/Resolucion/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RhechoId}")]
        public async Task<IEnumerable<ResolucionViewModel>> Listar([FromRoute] Guid RhechoId)
        {
            var per = await _context.Resolucions
                .Where(a => a.RHechoId == RhechoId)
                .OrderBy(a => a.Fechasys)
                .ToListAsync();

            return per.Select(a => new ResolucionViewModel
            {
                IdResolucion = a.IdResolucion,
                RHechoId = a.RHechoId,
                Victimas = a.Victimas,
                Imputados = a.Imputados,
                Delitos = a.Delitos,
                CausaPenal = a.CausaPenal,
                FechaAutorizacion = a.FechaAutorizacion,
                FechaConsulta = a.FechaConsulta,
                FechaResolucion = a.FechaResolucion,
                Status = a.Status,
                Tipo = a.Tipo,
                SubTipo = a.SubTipo,
                TextoDocumento = a.TextoDocumento,
                NumeroOficio = a.NumeroOficio,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys
            });

        }

        // GET: api/Resolucion/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{RhechoId}")]
        public async Task<IActionResult> ComprobarRegistroResolucion([FromRoute] Guid rHechoId)
        {
            var a = await _context.Resolucions
                .Where(x => x.RHechoId == rHechoId)
                .OrderBy(x => x.Fechasys)
                .FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound("No hay registros");
            }

            return Ok( new ResolucionViewModel
            {
                IdResolucion = a.IdResolucion,
                RHechoId = a.RHechoId,
                Victimas = a.Victimas,
                Imputados = a.Imputados,
                Delitos = a.Delitos,
                CausaPenal = a.CausaPenal,
                FechaAutorizacion = a.FechaAutorizacion,
                FechaConsulta = a.FechaConsulta,
                FechaResolucion = a.FechaResolucion,
                Status = a.Status,
                Tipo = a.Tipo,
                SubTipo = a.SubTipo,
                TextoDocumento = a.TextoDocumento,
                NumeroOficio = a.NumeroOficio,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys
            });

        }


        // POST: api/Resolucion/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            Guid idresolucion;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Resolucion Dato = new Resolucion
            {

                RHechoId = model.RHechoId,
                Victimas = model.Victimas,
                Imputados = model.Imputados,
                Delitos = model.Delitos,
                CausaPenal = model.CausaPenal,
                FechaAutorizacion = model.FechaAutorizacion,
                FechaConsulta = model.FechaConsulta,
                FechaResolucion = model.FechaResolucion,
                Status = model.Status,
                Tipo = model.Tipo,
                SubTipo = model.SubTipo,
                TextoDocumento = model.TextoDocumento,
                NumeroOficio = model.NumeroOficio,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
                URLDocumento = model.URLDocumento

            };

            _context.Resolucions.Add(Dato);
            try
            {
                await _context.SaveChangesAsync();
                idresolucion = Dato.IdResolucion;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok(new { idresolucion = idresolucion});
        }

        // PUT:  api/Resolucion/Actualizar
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var db = await _context.Resolucions.FirstOrDefaultAsync(a => a.IdResolucion== model.IdResolucion);

            if (db == null)
            {
                return NotFound();
            }

            db.Status = model.Status;

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

        // GET: api/Resolucion/EstadisticaTotalResoluciones
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{ResolucionEstadistica}")]
        public async Task<IEnumerable<EstadisticaViewModel>> EstadisticaTotalResoluciones([FromQuery] ResolucionEstadistica ResolucionEstadistica)
        {
            var per = await _context.Resolucions
                .Where(a => ResolucionEstadistica.DatosGenerales.Distritoact ? a.RHecho.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == ResolucionEstadistica.DatosGenerales.Distrito : 1 == 1)
                .Where(a => ResolucionEstadistica.DatosGenerales.Dspact ? a.RHecho.ModuloServicio.Agencia.DSP.IdDSP == ResolucionEstadistica.DatosGenerales.Dsp : 1 == 1)
                .Where(a => ResolucionEstadistica.DatosGenerales.Agenciaact ? a.RHecho.ModuloServicio.Agencia.IdAgencia == ResolucionEstadistica.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.RHecho.FechaElevaNuc2 >= ResolucionEstadistica.DatosGenerales.Fechadesde)
                .Where(a => a.RHecho.FechaElevaNuc2 <= ResolucionEstadistica.DatosGenerales.Fechahasta)
                .Where(a => ResolucionEstadistica.EstatusEtapaCarpeta.EtapaActual != "null" ? a.RHecho.NUCs.Etapanuc == ResolucionEstadistica.EstatusEtapaCarpeta.EtapaActual : 1 == 1)
                .Where(a => ResolucionEstadistica.EstatusEtapaCarpeta.StatusActual != "null" ? a.RHecho.NUCs.StatusNUC == ResolucionEstadistica.EstatusEtapaCarpeta.StatusActual : 1 == 1)
                .Where(a => ResolucionEstadistica.Resolucion != "null" ? a.Tipo == ResolucionEstadistica.Resolucion : 1 == 1)
                .ToListAsync();

            IEnumerable<EstadisticaViewModel> items = new EstadisticaViewModel[] { };

            IEnumerable<EstadisticaViewModel> ReadLines(int cantidad, string tipo)
            {
                IEnumerable<EstadisticaViewModel> item2;

                item2 = (new[]{new EstadisticaViewModel{
                                Cantidad = cantidad,
                                Tipo = tipo
                            }});

                return item2;
            }

            items = items.Concat(ReadLines(per.Count, (ResolucionEstadistica.Resolucion != "null" ? ResolucionEstadistica.Resolucion : "Todas las resoluciones")));


            return items;

        }

    }
}
