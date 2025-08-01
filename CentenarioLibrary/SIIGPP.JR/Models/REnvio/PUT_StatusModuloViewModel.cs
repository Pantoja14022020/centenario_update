using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.REnvio
{
    public class PUT_StatusModuloViewModel
    {
        // MODELO PARA ACTUALIZAR EL STATUS  
        public Guid IdExpediente { get; set; }
        public Guid IdEnvio { get; set; }
        public Guid Agenciaid { get; set; }
        public Guid distritoOrigen { get; set; }
        public string StatusGeneralEnvio { get; set; }
        public string RespuestaExpediente { get; set; }
        public string Sede { get; set; }
        public int NoDerivacion { get; set; }
        public int Cosecutivo { get; set; }
        public int anioEx { get; set; }
        public string NoExpediente { get; set; }
        public string Prefijo { get; set; }
        
    }
}
