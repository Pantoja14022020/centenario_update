using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.ProcedimientoAbreviado
{
    public class ProcedimientoAbreviadoViewModel
    {
        public Guid IdProcedimientoAbreviado { get; set; }
        public Guid RHechoId { get; set; }
        public string CausaPenal { get; set; }
        public string Delito { get; set; }
        public string TitularOrientePoniente { get; set; }
        public string OrientePoniente { get; set; }
        public string Imputado { get; set; }
        public string EdadImputado { get; set; }
        public string FechaNImputado { get; set; }
        public string AutoDescripcionIndignenaImputado { get; set; }
        public string AsistenciaConsularImputado { get; set; }
        public string DomicilioParticularImputado { get; set; }
        public string DomicilioEscucha { get; set; }
        public string DefensaParticularPublicaImputado { get; set; }
        public string BaseAcusacionRH { get; set; }
        public string TiempoRH { get; set; }
        public string LugarRH { get; set; }
        public string ModoRH { get; set; }
        public string DolosoCulposo { get; set; }
        public string PrisionDe { get; set; }
        public Boolean PrisionLP { get; set; }
        public Boolean MultaLP { get; set; }
        public string PrisionALP { get; set; }
        public string MultaDeLPLP { get; set; }
        public string MultaALP { get; set; }
        public Boolean PrisionMayor5LP { get; set; }
        public Boolean PrisionMenor5LP { get; set; }
        public string TipoAutoriaParticipacion { get; set; }
        public string CodigoPenalPL { get; set; }
        public string RegistroAntecedentes { get; set; }
        public string DGNumero { get; set; }
        public string DGFecha { get; set; }
        public string DGComunica { get; set; }
        public string DCDe { get; set; }
        public string DCNumero { get; set; }
        public string DCFecha { get; set; }
        public string DCComunica { get; set; }
        public Boolean PSOpcion1PS { get; set; }
        public string PrisionOp1PS { get; set; }
        public Boolean PSOpcion2PS { get; set; }
        public string MultaOp2PS { get; set; }
        public string CanditadOp2PS { get; set; }
        public string CantidadHechosOp2PS { get; set; }
        public Boolean PSOpcion3PS { get; set; }
        public Boolean PSOpcion4PS { get; set; }
        public Boolean PSOpcion5PS { get; set; }
        public string DuranteOp5PS { get; set; }
        public Boolean PSOpcion6PS { get; set; }
        public string PeriodoOp6PS { get; set; }
        public Boolean Opcion1PPA { get; set; }
        public string ExplicarOP1PPA { get; set; }
        public Boolean Opcion2PPA { get; set; }
        public string ExplicarOP2PPA { get; set; }
        public Boolean Opcion3PPA { get; set; }
        public Boolean Opcion4PPA { get; set; }
        public Boolean Opcion5PPA { get; set; }
        public Boolean Opcion6PPA { get; set; }
        public Boolean Opcion7PPA { get; set; }
        public Boolean Opcion8PPA { get; set; }
        public Boolean Opcion9PPA { get; set; }
        public Boolean Opcion10PPA { get; set; }
        public Boolean Opcion11PPA { get; set; }
        public Boolean Opcion12PPA { get; set; }
        public Boolean Opcion13PPA { get; set; }
        public string OtroOP13PPA { get; set; }
        public Boolean Opcion1RD { get; set; }
        public Boolean Opcion2RD { get; set; }
        public Boolean Opcion3RD { get; set; }
        public Boolean GastosOp1RD { get; set; }
        public string GastosErogadosOP3RD { get; set; }
        public Boolean GastosOp2RD { get; set; }
        public string GastosCuantificadosOP3RD { get; set; }
        public Boolean GastosOp3RD { get; set; }
        public string GastosAsesorJuridicoOP3RD { get; set; }
        public Boolean GastosOp4RD { get; set; }
        public Boolean Opcion4RD { get; set; }
        public Boolean Opcion5RD { get; set; }
        public DateTime ExplicacionImputadoFechaHora { get; set; }
        public DateTime PosturaVictimaFechaHora { get; set; }
        public Boolean Opcion1PV { get; set; }
        public Boolean Opcion2PV { get; set; }
        public string PorqueOp2PV { get; set; }
        public string NumeroOficio { get; set; }
        public DateTime FechaSys { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string UUsuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public string Texto1RD { get; set; }
        public string Texto2RD { get; set; }
    }
}
