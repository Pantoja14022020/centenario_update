using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.IL.Models.Agenda
{
    public class RespuestaPJAPIViewModel
    {
        public int status { get; set; }
        public string Mensaje { get; set; }
        public List<PoderJudicialAPIViewModel> ListaResultados { get; set; }
    }
}
