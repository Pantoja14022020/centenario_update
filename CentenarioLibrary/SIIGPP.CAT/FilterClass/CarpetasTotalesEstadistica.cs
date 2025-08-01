using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.FilterClass
{
    public class CarpetasTotalesEstadistica
    {
        public DatosGeneralesEstadistica DatosGenerales { get; set; }
        public DatosDetenidoEstadistica DatosDetenidoEstadistica { get; set; }
        public CaracteristicasDelitoEstadistica CaracteristicasDelitoEstadistica { get; set; }
        public EstatusEtapaCarpeta EstatusEtapaCarpeta { get; set; }
        public string Mediollegada { get; set; }
        public string Cumplerequisitosley { get; set; }
        public string Tipoinicio { get; set; }
    }
}
