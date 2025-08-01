using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSeguimientoCumplimiento
{
    public class POST_SeguimientoCumplimientoViewModel
    {
       
        public int NoParcialidad { get; set; }
        public DateTime Fecha { get; set; }

        public decimal CantidadAPagar { get; set; }
        public string TipoPago { get; set; }

        public string ObjectoEspecie { get; set; }
    }
}
