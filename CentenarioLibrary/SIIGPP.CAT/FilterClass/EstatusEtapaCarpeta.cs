using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.FilterClass
{
    public class EstatusEtapaCarpeta
    {
        public string EtapaActual { get; set; }
        public string StatusActual { get; set; }


        public string EtapaHistorico { get; set; }
        public string StatusHistorico { get; set; }
        public Boolean FiltroActivoStatusHistorico { get; set; }
    }
}
