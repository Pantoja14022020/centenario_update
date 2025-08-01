using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.FilterClass
{
    public class ListaNucsEstadistica
    {
        public DatosGeneralesEstadistica DatosGenerales { get; set; }
        public EstatusEtapaCarpeta EstatusEtapaCarpeta { get; set; }
        public HoraLugarSucesoEstadistica HoraLugarSuceso { get; set; }
        public CaracteristicasDelitoEstadistica CaracteristicasDelito { get; set; }
        public DatosPersonasEstadistica VictimaDatos { get; set; }
        public DatosPersonasEstadistica ImputadoDatos { get; set; }
    } 
}
