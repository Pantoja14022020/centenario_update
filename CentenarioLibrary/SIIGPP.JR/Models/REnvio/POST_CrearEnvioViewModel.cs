using SIIGPP.JR.Models.RDelito; 
using SIIGPP.JR.Models.RSolicitanteRequerido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.REnvio
{
    public class POST_CrearEnvioViewModel
    {
        // MODELADO PARA CREAR UN ENVIO DE UN EXPEDIENTE YA REGISTRADO
        // ENVIOS
        public Guid IdEnvio { get; set; }
        public Guid ExpedienteId { get; set; } 
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
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaCierre { get; set; }
        //DETALL DE LOS SOLICITANTES Y REQUERIDOS
        public List<POST_CrearSViewModel> Solicitantes { get; set; }
        public List<POST_CrearRViewModel> Requeridos { get; set; }

        // DETALLE DE LA ASIGNACION DE DELITOS
        public List<POST_CrearDelitoViewModel> Delitos { get; set; }
    }
}
