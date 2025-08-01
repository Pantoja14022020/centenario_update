using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.FilterClass
{
    public class HoraLugarSucesoEstadistica
    {
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public int CP { get; set; }
        public string Calle { get; set; }
        public string LugarEspecifico { get; set; }
        public DateTime Horainicio {get; set;}
        public DateTime HoraFin { get; set; }
        public DateTime Fechainicio { get; set; }
        public DateTime FechaFin { get; set; }


    }
}
