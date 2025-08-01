using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSolicitanteRequerido
{
    public class GET_BusquedaGeneralViewModel
    {
        public Guid IdEnvio { get; set; }
        public string Clasificacion { get; set; }
        public Guid ExpedienteId { get; set; }
        public string Distrito { get; set; }
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
        public string RespuestaExpediente { get; set; }
        public string EspontaneoNoEspontaneo { get; set; }
        public string PrimeraVezSubsecuente { get; set; }
        public int ContadorNODerivacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int NoSolicitantes { get; set; }
        // EXPEDIENTE
        public string NoExpediente { get; set; }
        public string StatusAcepRech { get; set; }
        public string InformacionStatus { get; set; }
        public DateTime FechaRegistroExpediente { get; set; }
        public DateTime FechaDerivacion { get; set; }

        // INFORMACION DEL HECHO 
        public string NUC { get; set; }
        //INFORMACION PERSONA

        public string TipoPersona { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; } 
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Alias { get; set; }

    }
}
