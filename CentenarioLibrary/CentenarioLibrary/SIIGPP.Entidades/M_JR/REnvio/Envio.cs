using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_JR.RExpediente;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_JR.REnvio
{
    public class Envio
    {
        public Guid IdEnvio { get; set; }
        public Guid ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }

        public string AutoridadqueDeriva { get; set; }
        public string uqe_Distrito { get; set; }
        public string uqe_DirSubProc { get; set; }
        public string uqe_Agencia { get; set; }
        public string uqe_Modulo { get; set; }
        public string uqe_Nombre { get; set; }
        public string uqe_Puesto { get; set; }
        public string StatusGeneral { get; set; }
        public string InfoConclusion { get; set; }
        public string StatusAMPO { get; set; }
        public string InformaAMPO { get; set; }
        public string oficioAMPO { get; set; }

        public string RespuestaExpediente { get; set; }
        public string EspontaneoNoEspontaneo { get; set; }
        public string PrimeraVezSubsecuente { get; set; }
        public int ContadorNODerivacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int NoSolicitantes { get; set; }
        public string FirmaInfoAMPO { get; set; }
        public string PuestoFirmaAMPO { get; set; }
        public string ArregloConjunto { get; set; }
        public string ArregloRepresentantes { get; set; }
        public bool statusEnvio { get; set; }
    }
}
