using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.IL.Models.Agenda
{
    public class ActualizarViewModel
    {
        public Guid IdAgenda { get; set; }
        public DateTime FechaCitacion { get; set; }
        public string Status { get; set; }
        public string HoraCitacion { get; set; }
        public string LugarCitacion { get; set; }
        public string DescripcionCitacion { get; set; }
        public Boolean Viculada { get; set; }
        public string PlazoInvestigacion { get; set; }
        public Boolean Prorroga { get; set; }
        public string TiempoProrroga { get; set; }
        public string Resumen { get; set; }
        public Boolean Status2 { get; set; }
    }
}
