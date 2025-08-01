using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.PI.Models.InformacionJuridico.OrdenesAprension;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using Microsoft.AspNetCore.Hosting;
using SIIGPP.PI.FilterClass;
using SIIGPP.PI.Models.InformacionJuridico;

namespace SIIGPP.PI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenAprensionController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public OrdenAprensionController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/OrdenAprension/Listar
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{idmodulo}")]
        public async Task<IEnumerable<OrdenAprensionViewModel>> Listar([FromRoute] Guid idmodulo)
        {
            var Orden = await _context.OrdenAprensions
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.ModuloServicioId == idmodulo)
                .ToListAsync();

            return Orden.Select(a => new OrdenAprensionViewModel
            {
                IdOrdenAprension = a.IdOrdenAprension,
                ModuloServicioId = a.ModuloServicioId,
                Juzgado = a.Juzgado,
                OficialiaDPartes = a.OficialiaDPartes,
                CausaPenal = a.CausaPenal,
                Oficio = a.Oficio,
                ARC = a.ARC,
                Imputado = a.Imputado,
                Delito = a.Delito,
                Agraviado = a.Agraviado,
                Recibida = a.Recibida,
                Observaciones = a.Observaciones,
                Status = a.Status,
                Estado = a.Estado,
                Respuesta = a.Respuesta,
                FAsignacion = a.FAsignacion,
                FFinalizacion = a.FFinalizacion,
                FUltmimoStatus = a.FUltmimoStatus,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                FechasComparescencia = a.FechasComparescencia,
                FechaCumplimiento = a.FechaCumplimiento,
                FechaRecepcion = a.FechaRecepcion,

            });

        }

        // POST: api/OrdenAprension/Crear
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrdenAprension Orden = new OrdenAprension
            {
                
                ModuloServicioId = model.ModuloServicioId,
                Juzgado = model.Juzgado,
                OficialiaDPartes = model.OficialiaDPartes,
                CausaPenal = model.CausaPenal,
                Oficio = model.Oficio,
                ARC = model.ARC,
                Imputado = model.Imputado,
                Delito = model.Delito,
                Agraviado = model.Agraviado,
                Recibida = model.Recibida,
                Observaciones = model.Observaciones,
                Status = model.Status,
                Estado = model.Estado,
                Respuesta = model.Respuesta,
                FAsignacion = model.FAsignacion,
                FFinalizacion = model.FFinalizacion,
                FUltmimoStatus = System.DateTime.Now,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
                FechasComparescencia = model.FechasComparescencia,
                FechaCumplimiento = model.FechaCumplimiento,
                FechaRecepcion = model.FechaRecepcion,
            };

            _context.OrdenAprensions.Add(Orden);

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

        // PUT: api/OrdenAprension/AsiganarUnidad
        [HttpPut("[action]")]
        public async Task<IActionResult> AsiganarUnidad([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.OrdenAprensions.FirstOrDefaultAsync(a => a.IdOrdenAprension == model.IdOrdenAprension);

            if (com == null)
            {
                return NotFound();
            }
            com.ModuloServicioId = model.ModuloServicioId;
            com.Status = "Asignado";
            com.FAsignacion = System.DateTime.Now;
            com.FUltmimoStatus = System.DateTime.Now;

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

        // GET: api/OrdenAprension/ListarporModulo
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{moduloid}")]
        public async Task<IEnumerable<OrdenAprensionViewModel>> ListarporModulo([FromRoute]Guid moduloid)
        {
            var Com = await _context.OrdenAprensions
                .Where(a => a.ModuloServicioId == moduloid)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return Com.Select(a => new OrdenAprensionViewModel
            {
                IdOrdenAprension = a.IdOrdenAprension,
                ModuloServicioId = a.ModuloServicioId,
                Juzgado = a.Juzgado,
                OficialiaDPartes = a.OficialiaDPartes,
                CausaPenal = a.CausaPenal,
                Oficio = a.Oficio,
                ARC = a.ARC,
                Imputado = a.Imputado,
                Delito = a.Delito,
                Agraviado = a.Agraviado,
                Recibida = a.Recibida,
                Observaciones = a.Observaciones,
                Status = a.Status,
                Estado = a.Estado,
                Respuesta = a.Respuesta,
                FAsignacion = a.FAsignacion,
                FFinalizacion = a.FFinalizacion,
                FUltmimoStatus = a.FUltmimoStatus,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,

            });

        }

        // PUT: api/OrdenAprension/ActualizarStatus
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.OrdenAprensions.FirstOrDefaultAsync(a => a.IdOrdenAprension == model.IdOrdenAprension);

            if (com == null)
            {
                return NotFound();
            }
            com.Status = model.Status;
            com.FUltmimoStatus = System.DateTime.Now;

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


        // PUT: api/OrdenAprension/ActualizarStatusFinalizado
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatusFinalizado([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var com = await _context.OrdenAprensions.FirstOrDefaultAsync(a => a.IdOrdenAprension == model.IdOrdenAprension);

            if (com == null)
            {
                return NotFound();
            }
            com.Status = model.Status;
            com.FFinalizacion = System.DateTime.Now;
            com.FUltmimoStatus = System.DateTime.Now;

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


        // GET: api/OrdenAprension/EstadisticaOrdenesArrestosComparecencias
        [Authorize(Roles = "Administrador,Comandante General,Agente-Policía,Oficialia de partes,Comandante Unidad,Juridico,Detenciones")]
        [HttpGet("[action]/{OrArCo}")]
        public async Task<IEnumerable<EstadisticaOrArCoViewModel>> EstadisticaOrdenesArrestosComparecencias([FromQuery] OrArCo OrArCo)
        {
            var ORAP = await _context.OrdenAprensions
                .Where(a => OrArCo.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == OrArCo.DatosGenerales.Distrito : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == OrArCo.DatosGenerales.Dsp : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == OrArCo.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.Fechasys >= OrArCo.DatosGenerales.Fechadesde)
                .Where(a => a.Fechasys <= OrArCo.DatosGenerales.Fechahasta)
                .ToListAsync();

            var ORAPCP = await _context.OrdenAprensions
                .Where(a => OrArCo.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == OrArCo.DatosGenerales.Distrito : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == OrArCo.DatosGenerales.Dsp : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == OrArCo.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.Fechasys >= OrArCo.DatosGenerales.Fechadesde)
                .Where(a => a.Fechasys <= OrArCo.DatosGenerales.Fechahasta)
                .Where(a => a.Status == "Cumplida parcial")
                .ToListAsync();

            var ORAPCT = await _context.OrdenAprensions
                .Where(a => OrArCo.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == OrArCo.DatosGenerales.Distrito : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == OrArCo.DatosGenerales.Dsp : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == OrArCo.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.Fechasys >= OrArCo.DatosGenerales.Fechadesde)
                .Where(a => a.Fechasys <= OrArCo.DatosGenerales.Fechahasta)
                .Where(a => a.Status == "Cumplida Total")
                .ToListAsync();

            var Arrestos = await _context.Arrestos
                .Where(a => OrArCo.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == OrArCo.DatosGenerales.Distrito : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == OrArCo.DatosGenerales.Dsp : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == OrArCo.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.Fechasys >= OrArCo.DatosGenerales.Fechadesde)
                .Where(a => a.Fechasys <= OrArCo.DatosGenerales.Fechahasta)
                .ToListAsync();

            var ArrestosCP = await _context.Arrestos
                .Where(a => OrArCo.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == OrArCo.DatosGenerales.Distrito : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == OrArCo.DatosGenerales.Dsp : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == OrArCo.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.Fechasys >= OrArCo.DatosGenerales.Fechadesde)
                .Where(a => a.Fechasys <= OrArCo.DatosGenerales.Fechahasta)
                .Where(a => a.Status == "Cumplida parcial")
                .ToListAsync();

            var ArrestosCT = await _context.Arrestos
                .Where(a => OrArCo.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == OrArCo.DatosGenerales.Distrito : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == OrArCo.DatosGenerales.Dsp : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == OrArCo.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.Fechasys >= OrArCo.DatosGenerales.Fechadesde)
                .Where(a => a.Fechasys <= OrArCo.DatosGenerales.Fechahasta)
                .Where(a => a.Status == "Cumplida Total")
                .ToListAsync();

            var PryCs = await _context.PresentacionesYCs
                .Where(a => OrArCo.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == OrArCo.DatosGenerales.Distrito : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == OrArCo.DatosGenerales.Dsp : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == OrArCo.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.Fechasys >= OrArCo.DatosGenerales.Fechadesde)
                .Where(a => a.Fechasys <= OrArCo.DatosGenerales.Fechahasta)
                .ToListAsync();

            var PryCsCp = await _context.PresentacionesYCs
                .Where(a => OrArCo.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == OrArCo.DatosGenerales.Distrito : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == OrArCo.DatosGenerales.Dsp : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == OrArCo.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.Fechasys >= OrArCo.DatosGenerales.Fechadesde)
                .Where(a => a.Fechasys <= OrArCo.DatosGenerales.Fechahasta)
                .Where(a => a.Status == "Cumplida parcial")
                .ToListAsync();

            var PryCsCt = await _context.PresentacionesYCs
                .Where(a => OrArCo.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == OrArCo.DatosGenerales.Distrito : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == OrArCo.DatosGenerales.Dsp : 1 == 1)
                .Where(a => OrArCo.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == OrArCo.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.Fechasys >= OrArCo.DatosGenerales.Fechadesde)
                .Where(a => a.Fechasys <= OrArCo.DatosGenerales.Fechahasta)
                .Where(a => a.Status == "Cumplida Total")
                .ToListAsync();


            IEnumerable<EstadisticaOrArCoViewModel> items = new EstadisticaOrArCoViewModel[] { };

            IEnumerable<EstadisticaOrArCoViewModel> ReadLines(string tipo, int cantidad)
            {
                IEnumerable<EstadisticaOrArCoViewModel> item2;

                item2 = (new[]{new EstadisticaOrArCoViewModel{
                                Tipo = tipo,
                                Cantidad = cantidad
                            }});

                return item2;
            }


            items = items.Concat(ReadLines("Ordenes de aprehensión registradas", ORAP.Count));
            items = items.Concat(ReadLines("Ordenes de aprehensión cumplida parcial", ORAPCP.Count));
            items = items.Concat(ReadLines("Ordenes de aprehensión cumplida total", ORAPCT.Count));

            items = items.Concat(ReadLines("Arrestos registrados", Arrestos.Count));
            items = items.Concat(ReadLines("Arrestos cumplida parcial", ArrestosCP.Count));
            items = items.Concat(ReadLines("Arrestos cumplida total", ArrestosCT.Count));

            items = items.Concat(ReadLines("Ordenes y comparecencias registradas", PryCs.Count));
            items = items.Concat(ReadLines("Ordenes y comparecencias cumplida parcial", PryCsCp.Count));
            items = items.Concat(ReadLines("Ordenes y comparecencias cumplida total", PryCsCt.Count));

            return items;


        }
    }
}
