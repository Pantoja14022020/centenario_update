using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RAsignacionEnvios
{
    public class GET_AsignacionExpedientesViewModel
    {
        // MODELO PARA LISTAR LOS ENVIOS 
        // ENVIOS
       

        public Guid EnvioId { get; set; }
        public Guid asignacionId1 { get; set; }
        public Guid asignacionId2 { get; set; }
        public Guid ModuloId1 { get; set; }
        public Guid ModuloId2 { get; set; }
        public string NoExpediente { get; set; }
        public string AutoridadqueDeriva { get; set; }
        public int ContadorNODerivacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NUC { get; set; }

        public string StatusGeneralEnvio { get; set; }
        
        public string facilitador { get; set; }
        public string notificador { get; set; }
       
        
 





    }
}
