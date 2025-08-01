using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RAsignacionEnvios
{
    public class GET_TodosEnviosViewModel
    {
        // MODELO PARA LISTAR LOS ENVIOS 
        // ENVIOS
        public Guid IdAsingacionEnvio { get; set; }
    
        public Guid EnvioId { get; set; } 
        public Guid ModuloServicioId { get; set; }
        public string NombreModulo { get; set; }
        public string TipoModulo { get; set; } 
        public Guid ExpedienteId { get; set; }
        public string NoExpediente { get; set; }
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
        public string PrimeraVezSubsecuente { get; set; }
        public int ContadorNODerivacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int NoSolicitantes { get; set; }
        // INFORMACION DEL HECHO

        public string NUC { get; set; }


    }
}
