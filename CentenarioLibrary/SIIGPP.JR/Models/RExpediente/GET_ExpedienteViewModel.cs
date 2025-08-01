using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RExpediente
{
    public class GET_ExpedienteViewModel
    {
        //MODELOS PARA LISTAR LOS EXPEDIENTES EN LA BANDEJA DE RECEPCION DE EXPEDIENTES
        public Guid IdExpediente { get; set; }
        public Guid RHechoId { get; set; }
        public Guid DistritoOrigenId { get; set; }
        public string Prefijo { get; set; }
        public int Cosecutivo { get; set; }
        public int Año { get; set; }
        public string Sede { get; set; }
        public string NoExpediente { get; set; }
        public int NoDerivacion { get; set; }
        public string StatusAcepRech { get; set; }
        public string InformacionStatus { get; set; }
        public DateTime FechaRegistroExpediente { get; set; }

        public DateTime FechaDerivacion { get; set; }


        // INFORMACION DEL HECHO

        public string NUC { get; set; }
        public DateTime? FechaHoraSuceso { get; set; }
        public string ReseñaBreve { get; set; }
        public string NarracionHechos { get; set; }

        //INFORMACION ENVIO

        public string AutoridadqueDeriva { get; set; }
        public string uqe_Distrito { get; set; }
        public string uqe_DirSubProc { get; set; }
        public string uqe_Agencia { get; set; }
        public string uqe_Modulo { get; set; }
        public string uqe_Nombre { get; set; }
        public string uqe_Puesto { get; set; }
        public string StatusGeneralEnvio { get; set; }
        public string EspontaneoNoEspontaneo { get; set; }
        public string PrimeraVezSubsecuente { get; set; }
        public int ContadorNODerivacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int NoSolicitantes { get; set; }
    }
}
