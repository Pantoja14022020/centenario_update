using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.ProcedimientoAbreviado;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.ProcedimientoAbreviados;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedimientoAbreviadosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public ProcedimientoAbreviadosController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/ProcedimientoAbreviados/ListarporRHecho
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<ProcedimientoAbreviadoViewModel>> ListarporRHecho([FromRoute]Guid RHechoId)
        {
            var PA = await _context.ProcedimientoAbreviados
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.FechaSys)
                .ToListAsync();

            return PA.Select(a => new ProcedimientoAbreviadoViewModel
            {

                IdProcedimientoAbreviado = a.IdProcedimientoAbreviado,
                RHechoId = a.RHechoId,
                CausaPenal = a.CausaPenal,
                Delito = a.Delito,
                TitularOrientePoniente = a.TitularOrientePoniente,
                OrientePoniente = a.OrientePoniente,
                Imputado = a.Imputado,
                EdadImputado = a.EdadImputado,
                FechaNImputado = a.FechaNImputado,
                AutoDescripcionIndignenaImputado = a.AutoDescripcionIndignenaImputado,
                AsistenciaConsularImputado = a.AsistenciaConsularImputado,
                DomicilioParticularImputado = a.DomicilioParticularImputado,
                DomicilioEscucha = a.DomicilioEscucha,
                DefensaParticularPublicaImputado = a.DefensaParticularPublicaImputado,
                BaseAcusacionRH = a.BaseAcusacionRH,
                TiempoRH = a.TiempoRH,
                LugarRH = a.LugarRH,
                ModoRH = a.ModoRH,
                DolosoCulposo = a.DolosoCulposo,
                PrisionDe = a.PrisionDe,
                PrisionLP = a.PrisionLP,
                MultaLP = a.MultaLP,
                PrisionALP = a.PrisionALP,
                MultaDeLPLP = a.MultaDeLPLP,
                MultaALP = a.MultaALP,
                PrisionMayor5LP = a.PrisionMayor5LP,
                PrisionMenor5LP = a.PrisionMenor5LP,
                TipoAutoriaParticipacion = a.TipoAutoriaParticipacion,
                CodigoPenalPL = a.CodigoPenalPL,
                RegistroAntecedentes = a.RegistroAntecedentes,
                DGNumero = a.DGNumero,
                DGFecha = a.DGFecha,
                DGComunica = a.DGComunica,
                DCDe = a.DCDe,
                DCNumero = a.DCNumero,
                DCFecha = a.DCFecha,
                DCComunica = a.DCComunica,
                PSOpcion1PS = a.PSOpcion1PS,
                PrisionOp1PS = a.PrisionOp1PS,
                PSOpcion2PS = a.PSOpcion2PS,
                MultaOp2PS = a.MultaOp2PS,
                CanditadOp2PS = a.CanditadOp2PS,
                CantidadHechosOp2PS = a.CantidadHechosOp2PS,
                PSOpcion3PS = a.PSOpcion3PS,
                PSOpcion4PS = a.PSOpcion4PS,
                PSOpcion5PS = a.PSOpcion5PS,
                DuranteOp5PS = a.DuranteOp5PS,
                PSOpcion6PS = a.PSOpcion6PS,
                PeriodoOp6PS = a.PeriodoOp6PS,
                Opcion1PPA = a.Opcion1PPA,
                ExplicarOP1PPA = a.ExplicarOP1PPA,
                Opcion2PPA = a.Opcion2PPA,
                ExplicarOP2PPA = a.ExplicarOP2PPA,
                Opcion3PPA = a.Opcion3PPA,
                Opcion4PPA = a.Opcion4PPA,
                Opcion5PPA = a.Opcion5PPA,
                Opcion6PPA = a.Opcion6PPA,
                Opcion7PPA = a.Opcion7PPA,
                Opcion8PPA = a.Opcion8PPA,
                Opcion9PPA = a.Opcion9PPA,
                Opcion10PPA = a.Opcion10PPA,
                Opcion11PPA = a.Opcion11PPA,
                Opcion12PPA = a.Opcion12PPA,
                Opcion13PPA = a.Opcion13PPA,
                OtroOP13PPA = a.OtroOP13PPA,
                Opcion1RD = a.Opcion1RD,
                Opcion2RD = a.Opcion2RD,
                Opcion3RD = a.Opcion3RD,
                GastosOp1RD = a.GastosOp1RD,
                GastosErogadosOP3RD = a.GastosErogadosOP3RD,
                GastosOp2RD = a.GastosOp2RD,
                GastosCuantificadosOP3RD = a.GastosCuantificadosOP3RD,
                GastosOp3RD = a.GastosOp3RD,
                GastosAsesorJuridicoOP3RD = a.GastosAsesorJuridicoOP3RD,
                GastosOp4RD = a.GastosOp4RD,
                Opcion4RD = a.Opcion4RD,
                Opcion5RD = a.Opcion5RD,
                ExplicacionImputadoFechaHora = a.ExplicacionImputadoFechaHora,
                PosturaVictimaFechaHora = a.PosturaVictimaFechaHora,
                Opcion1PV = a.Opcion1PV,
                Opcion2PV = a.Opcion2PV,
                PorqueOp2PV = a.PorqueOp2PV,
                NumeroOficio = a.NumeroOficio,
                FechaSys = a.FechaSys,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                UUsuario = a.UUsuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Texto1RD = a.Texto1RD,
                Texto2RD = a.Texto2RD,

            });

        }


        // POST: api/ProcedimientoAbreviados/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            ProcedimientoAbreviado Pa = new ProcedimientoAbreviado
            {

                RHechoId = model.RHechoId,
                CausaPenal = model.CausaPenal,
                Delito = model.Delito,
                TitularOrientePoniente = model.TitularOrientePoniente,
                OrientePoniente = model.OrientePoniente,
                Imputado = model.Imputado,
                EdadImputado = model.EdadImputado,
                FechaNImputado = model.FechaNImputado,
                AutoDescripcionIndignenaImputado = model.AutoDescripcionIndignenaImputado,
                AsistenciaConsularImputado = model.AsistenciaConsularImputado,
                DomicilioParticularImputado = model.DomicilioParticularImputado,
                DomicilioEscucha = model.DomicilioEscucha,
                DefensaParticularPublicaImputado = model.DefensaParticularPublicaImputado,
                BaseAcusacionRH = model.BaseAcusacionRH,
                TiempoRH = model.TiempoRH,
                LugarRH = model.LugarRH,
                ModoRH = model.ModoRH,
                DolosoCulposo = model.DolosoCulposo,
                PrisionLP = model.PrisionLP,
                PrisionDe = model.PrisionDe,               
                PrisionALP = model.PrisionALP,
                MultaLP = model.MultaLP,
                MultaDeLPLP = model.MultaDeLPLP,
                MultaALP = model.MultaALP,
                PrisionMayor5LP = model.PrisionMayor5LP,
                PrisionMenor5LP = model.PrisionMenor5LP,
                TipoAutoriaParticipacion = model.TipoAutoriaParticipacion,
                CodigoPenalPL = model.CodigoPenalPL,
                RegistroAntecedentes = model.RegistroAntecedentes,
                DGNumero = model.DGNumero,
                DGFecha = model.DGFecha,
                DGComunica = model.DGComunica,
                DCDe = model.DCDe,
                DCNumero = model.DCNumero,
                DCFecha = model.DCFecha,
                DCComunica = model.DCComunica,
                PSOpcion1PS = model.PSOpcion1PS,
                PrisionOp1PS = model.PrisionOp1PS,
                PSOpcion2PS = model.PSOpcion2PS,
                MultaOp2PS = model.MultaOp2PS,
                CanditadOp2PS = model.CanditadOp2PS,
                CantidadHechosOp2PS = model.CantidadHechosOp2PS,
                PSOpcion3PS = model.PSOpcion3PS,
                PSOpcion4PS = model.PSOpcion4PS,
                PSOpcion5PS = model.PSOpcion5PS,
                DuranteOp5PS = model.DuranteOp5PS,
                PSOpcion6PS = model.PSOpcion6PS,
                PeriodoOp6PS = model.PeriodoOp6PS,
                Opcion1PPA = model.Opcion1PPA,
                ExplicarOP1PPA = model.ExplicarOP1PPA,
                Opcion2PPA = model.Opcion2PPA,
                ExplicarOP2PPA = model.ExplicarOP2PPA,
                Opcion3PPA = model.Opcion3PPA,
                Opcion4PPA = model.Opcion4PPA,
                Opcion5PPA = model.Opcion5PPA,
                Opcion6PPA = model.Opcion6PPA,
                Opcion7PPA = model.Opcion7PPA,
                Opcion8PPA = model.Opcion8PPA,
                Opcion9PPA = model.Opcion9PPA,
                Opcion10PPA = model.Opcion10PPA,
                Opcion11PPA = model.Opcion11PPA,
                Opcion12PPA = model.Opcion12PPA,
                Opcion13PPA = model.Opcion13PPA,
                OtroOP13PPA = model.OtroOP13PPA,
                Opcion1RD = model.Opcion1RD,
                Texto1RD = model.Texto1RD,
                Opcion2RD = model.Opcion2RD,
                Texto2RD = model.Texto2RD,
                Opcion3RD = model.Opcion3RD,
                GastosOp1RD = model.GastosOp1RD,
                GastosErogadosOP3RD = model.GastosErogadosOP3RD,
                GastosOp2RD = model.GastosOp2RD,
                GastosCuantificadosOP3RD = model.GastosCuantificadosOP3RD,
                GastosOp3RD = model.GastosOp3RD,
                GastosAsesorJuridicoOP3RD = model.GastosAsesorJuridicoOP3RD,
                GastosOp4RD = model.GastosOp4RD,
                Opcion4RD = model.Opcion4RD,
                Opcion5RD = model.Opcion5RD,
                ExplicacionImputadoFechaHora = model.ExplicacionImputadoFechaHora,
                PosturaVictimaFechaHora = model.PosturaVictimaFechaHora,
                Opcion1PV = model.Opcion1PV,
                Opcion2PV = model.Opcion2PV,
                PorqueOp2PV = model.PorqueOp2PV,
                NumeroOficio = model.NumeroOficio,
                FechaSys = fecha,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                UUsuario = model.UUsuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                
                
            };

            _context.ProcedimientoAbreviados.Add(Pa);

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

    }
}
