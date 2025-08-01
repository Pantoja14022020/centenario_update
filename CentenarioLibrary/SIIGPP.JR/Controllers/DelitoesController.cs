using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using SIIGPP.Entidades.M_JR.RDelitodelitosDerivados;
using SIIGPP.JR.Models.RDelito;
using SIIGPP.JR.Models.RSolicitanteRequerido;

namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DelitoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public DelitoesController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Delitoes
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet]
        public IEnumerable<Delito> GetDelitos()
        {
            return _context.Delitos;
        }

        // GET: api/Delitoes/ListarDelitoConjunto
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{ConjuntoDerivacionesId}")]
        public async Task<IEnumerable<GET_DelitoViewModel>> ListarDelitoConjunto([FromRoute] Guid ConjuntoDerivacionesId)
        {
            var tabla = await _context.DelitosDerivados
                                .Where(a => a.ConjuntoDerivacionesId == ConjuntoDerivacionesId)
                                .Include(a => a.RDH)
                                .Include(a => a.RDH.Delito)
                                .ToListAsync();


            return tabla.Select(a => new GET_DelitoViewModel
            {
                //*********************************************************************

                IdDelitoDerivado = a.IdDelitoDerivado,
                EnvioId = a.EnvioId,
                RDHId = a.RDHId,
                DelitoId = a.RDH.DelitoId,
                nombreDelito = a.RDH.Delito.Nombre,
                RHechoId = a.RDH.RHechoId,
                OfiNoOfic = a.RDH.Delito.OfiNoOfi,
                altoImpacto = a.RDH.Delito.AltoImpacto,
                TipoFuero = a.RDH.TipoFuero,
                TipoDeclaracion = a.RDH.TipoDeclaracion,
                ResultadoDelito = a.RDH.ResultadoDelito,
                GraveNoGrave = a.RDH.GraveNoGrave,
                IntensionDelito = a.RDH.IntensionDelito,
                ViolenciaSinViolencia = a.RDH.ViolenciaSinViolencia,
                Equiparado = a.RDH.Equiparado,
                Tipo = a.RDH.Tipo,
                Concurso = a.RDH.Concurso,
                ClasificaOrdenResult = a.RDH.ClasificaOrdenResult,
                ArmaFuego = a.RDH.ArmaFuego,
                ArmaBlanca = a.RDH.ArmaBlanca,
                ConAlgunaParteCuerpo = a.RDH.ConAlgunaParteCuerpo,
                ConotroElemento = a.RDH.ConotroElemento,
                TipoRobado = a.RDH.TipoRobado,
                MontoRobado = a.RDH.MontoRobado,
                Hipotesis = a.RDH.Hipotesis,
                DelitoEspecifico = a.RDH.DelitoEspecifico,
            });

        }

        // GET: api/Delitoes/ListarDelitos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{EnvioId}")]
        public async Task<IEnumerable<GET_idDelitoViewModel>> ListarDelitos([FromRoute] Guid EnvioId)
        {
            var tabla = await _context.DelitosDerivados
                                .Where(a => a.EnvioId == EnvioId) 
                                .Include(a => a.RDH)
                                .Include(a => a.RDH.Delito) 
                                .ToListAsync();
            var delitoUnico = tabla.DistinctBy(a => a.RDH.DelitoId);


            return delitoUnico.Select(a => new GET_idDelitoViewModel
            {
                //*********************************************************************

                IdDelitoDerivado = a.IdDelitoDerivado,
                nombreDelito = a.RDH.Delito.Nombre,
                IdDelito = a.RDH.Delito.IdDelito,
            });

        }
        // GET: api/Delitoes/ListarDelitos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{IdDelitoDerivado}/{EnvioId}")]
        public async Task<IEnumerable<GET_DelitoViewModel>> InfoDelitos([FromRoute] Guid EnvioId, Guid IdDelitoDerivado)
        {
            var tabla = await _context.DelitosDerivados
                                .Where(a => a.EnvioId == EnvioId)
                                .Where(a => a.IdDelitoDerivado == IdDelitoDerivado)
                                .Include(a => a.RDH)
                                .Include(a => a.RDH.Delito)
                                .ToListAsync();


            return tabla.Select(a => new GET_DelitoViewModel
            {
                //*********************************************************************

                IdDelitoDerivado = a.IdDelitoDerivado,
                EnvioId = a.EnvioId,
                RDHId = a.RDHId,
                DelitoId = a.RDH.DelitoId,
                nombreDelito = a.RDH.Delito.Nombre,
                RHechoId = a.RDH.RHechoId,
                OfiNoOfic = a.RDH.Delito.OfiNoOfi,
                altoImpacto = a.RDH.Delito.AltoImpacto,
                TipoFuero = a.RDH.TipoFuero,
                TipoDeclaracion = a.RDH.TipoDeclaracion,
                ResultadoDelito = a.RDH.ResultadoDelito,
                GraveNoGrave = a.RDH.GraveNoGrave,
                IntensionDelito = a.RDH.IntensionDelito,
                ViolenciaSinViolencia = a.RDH.ViolenciaSinViolencia,
                Equiparado = a.RDH.Equiparado,
                Tipo = a.RDH.Tipo,
                Concurso = a.RDH.Concurso,
                ClasificaOrdenResult = a.RDH.ClasificaOrdenResult,
                ArmaFuego = a.RDH.ArmaFuego,
                ArmaBlanca = a.RDH.ArmaBlanca,
                ConAlgunaParteCuerpo = a.RDH.ConAlgunaParteCuerpo,
                ConotroElemento = a.RDH.ConotroElemento,
                TipoRobado = a.RDH.TipoRobado,
                MontoRobado = a.RDH.MontoRobado,
                Hipotesis = a.RDH.Hipotesis,
                DelitoEspecifico = a.RDH.DelitoEspecifico,
            });

        }


        // DELETE: api/Delitoes/EliminarEnvio
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpDelete("[action]/{idEnvio}")]
        public async Task<IActionResult> EliminarEnvio([FromRoute] Guid idEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var db = await _context.DelitosDerivados.Where(x => x.EnvioId == idEnvio).ToListAsync(); 

            if (db == null)
            {
                return NotFound();
            }


            _context.DelitosDerivados.RemoveRange(db);





            var db2 = await _context.SolicitanteRequeridos.Where(x => x.EnvioId == idEnvio).ToListAsync();

            if (db == null)
            {
                return NotFound();
            }


            _context.SolicitanteRequeridos.RemoveRange(db2);

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

        //Api para actualizar los registros de delitos derivados y colocarles el id de conjunto como parte de su trasformacion a conjuntos
        // PUT:  api/Delitoes/EditarConjuntoIdDelitosDerivados
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> EditarConjuntoIdDelitosDerivados([FromBody] GET_idDelitoDeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var SR = await _context.DelitosDerivados.FirstOrDefaultAsync(a => a.IdDelitoDerivado == model.IdDelitoDerivado);

            if (SR == null)
            {
                return NotFound();
            }

            SR.ConjuntoDerivacionesId = model.ConjuntoDerivacionesId;


            //Esta solo es una respuesta en caso que se haya actualizado correctamente
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new { update = true });

        }


        private bool DelitoExists(Guid id)
        {
            return _context.Delitos.Any(e => e.IdDelito == id);
        }

        
    }
}