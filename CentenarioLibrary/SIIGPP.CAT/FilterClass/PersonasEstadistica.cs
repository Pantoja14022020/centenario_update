using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.FilterClass
{
    public class PersonasEstadistica
    {
        public DatosGeneralesEstadistica DatosGenerales { get; set; }
        public string Sexo { get; set; }
        public DatosDetenidoEstadistica DatosDetenidoEstadistica { get; set; }
        public CaracteristicasDelitoEstadistica CaracteristicasDelitoEstadistica { get; set; } 
        public EstatusEtapaCarpeta EstatusEtapaCarpeta { get; set; }

    }
}
