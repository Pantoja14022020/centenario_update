using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.SP.ModelsSP.PeritoAsignado
{
    public class EstadisticasModuloViewModel
    {
        public string Modulo { get; set; }
        public int Finalizado { get; set; }
        public int Asignado { get; set; }
        public int Enproceso { get; set; }
        public int Suspendido { get; set; }
        public int Pospuesto { get; set; }
        public int Entregado { get; set; }
        public int Total { get; set; }
    }
}
