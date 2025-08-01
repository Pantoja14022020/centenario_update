using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.FilterClass
{
    public class DelitosTotalesEstadistica
    {
        public DatosGeneralesEstadistica DatosGenerales { get; set; }
        public EstatusEtapaCarpeta EstatusEtapaCarpeta { get; set; }
        public CaracteristicasDelitoEstadistica Delito { get; set; }
        public HoraLugarSucesoEstadistica HoraLugarSuceso { get; set; }
    }
}
