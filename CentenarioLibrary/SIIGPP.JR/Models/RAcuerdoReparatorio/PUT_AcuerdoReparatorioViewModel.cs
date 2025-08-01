using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RAcuerdoReparatorio
{
    public class PUT_AcuerdoReparatorioViewModel
    {
        public Guid IdAcuerdoReparatorio { get; set; }
        public string TipoDocumento { get; set; }
        //public int EnvioId { get; set; }
        //public string NombreSolicitante { get; set; }
        //public string NombreRequerdio { get; set; }
        //public string Delitos { get; set; }
        //public string NUC { get; set; }
        //public string NoExpediente { get; set; }
        public string StatusConclusion { get; set; }
        //public string StatusCumplimiento { get; set; }
        public string MetodoUtilizado { get; set; }
        public string TipoPago { get; set; }
        public Decimal MontoTotal { get; set; }
        public int NoTotalParcialidades { get; set; }
        public int Periodo { get; set; }
        public DateTime? FechaCelebracionAcuerdo { get; set; }
        public DateTime? FechaLimiteCumplimiento { get; set; }
        public string objectoEspecie { get; set; }
        public string money { get; set; }
        public string species { get; set; }
        public string TextoAR { get; set; }
        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }
     

    }
}
