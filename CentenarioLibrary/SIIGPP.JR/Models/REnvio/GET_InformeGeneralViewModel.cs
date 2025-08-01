using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.REnvio
{
    public class GET_InformeGeneralViewModel
    {
        public int mes { get; set; }
        public int registrados { get; set; }
        public int acuerdos { get; set; } 
        public int inmediato { get; set; }
        public int diferidos { get; set; }
        public int cumplidos { get; set; }
        public int incumplidos { get; set; }
        public int encumplimiento { get; set; }
        public int baja { get; set; }
        public int tramite { get; set; }
       
        
    }
}
