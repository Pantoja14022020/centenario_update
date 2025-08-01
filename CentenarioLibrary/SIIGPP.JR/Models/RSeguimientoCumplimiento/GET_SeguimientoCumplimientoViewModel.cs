using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSeguimientoCumplimiento
{
    public class GET_SeguimientoCumplimientoViewModel
    {
        public Guid IdSeguimientoCumplimiento { get; set; }
        public Guid AcuerdoReparatorioId { get; set; } 
        public int NoParcialidad { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaProrroga { get; set; }
        public decimal CantidadAPagar { get; set; }
        public string TipoPago { get; set; }
        public string ObjectoEspecie { get; set; }
        public String StatusPago { get; set; }
        public string NoExpediente { get; set; }





    }
}
