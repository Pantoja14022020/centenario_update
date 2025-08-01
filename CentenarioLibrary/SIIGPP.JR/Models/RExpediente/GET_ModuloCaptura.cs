using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RExpediente
{
    public class GET_ModuloCaptura
    {
        public Guid IdExpediente { get; set; }
        public Guid RHechoId { get; set; }
        public string Prefijo { get; set; }
        public int Ano { get; set; }
        public string NoExpediente { get; set; }


        public Guid IdEnvio { get; set; }
        public string StatusGeneral { get; set; }
        public int NoSolicitantes { get; set; }


        public string Solicitantes {get; set;}
        public string Requeridos { get; set; }
        public string Delitos { get; set; }
        public string StatusConclusion { get; set; }
        public string StatusCumplimiento { get; set; }
        public DateTime? Fechacelebracion { get; set; }
        public DateTime? FechaLimitecumplimiento { get; set; }
        public int Sise { get; set; }


        public string StatusBaja { get; set; }


        public DateTime? FechaConclusion { get; set; }


        public string uqe_Distrito { get; set; }
        public string uqe_DirSubProc { get; set; }
        public string uqe_Agencia { get; set; }
        public string uqe_Modulo { get; set; }
        public string uqe_Nombre { get; set; }
        public string uqe_Puesto { get; set; }
    }
}
