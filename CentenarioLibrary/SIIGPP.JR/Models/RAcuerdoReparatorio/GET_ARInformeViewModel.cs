using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RAcuerdoReparatorio
{
    public class GET_ARInformeViewModel
    {
        public string etiqueta { get; set; }
        public int tramite { get; set; }
        public int baja { get; set; }
        public int inmediato { get; set; }
        public int diferido { get; set; }
        public int total { get; set; }
        public int acuerdos { get; set; }

        public int cumplido { get; set; }
        public int incumplido { get; set; } 
        public int encumplimiento { get; set; }
    }
}
