using SIIGPP.JR.Models.RDelito;
using SIIGPP.JR.Models.RSolicitanteRequerido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RExpediente
{
    public class POST_CrearExpedienteViewModel
    {

        // CREA EL EXPEDIENTE INICIAL METODO POST
        // EXPEDIENTE

        public Guid RHechoId { get; set; }
        public string Sede { get; set; }
        public string StatusAcepRech { get; set; }

        // ENVIOS 
        public string AutoridadqueDeriva { get; set; }
        public string uqe_Distrito { get; set; }
        public string uqe_DirSubProc { get; set; }
        public string uqe_Agencia { get; set; }
        public string uqe_Modulo { get; set; }
        public string uqe_Nombre { get; set; }
        public string uqe_Puesto { get; set; }

        public string StatusGeneralEnvio { get; set; }
        public string RespuestaExpediente { get; set; }
        public string EspontaneoNoEspontaneo { get; set; }

        public int ContadorNODerivacion { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int NoSolicitantes { get; set; }
        public Guid DistritoIdDestino { get; set; }
        public string ArregloConjunto { get; set; }
        public string ArregloRepresentantes { get; set; }
        public bool statusEnvio { get; set; }
    }
}
