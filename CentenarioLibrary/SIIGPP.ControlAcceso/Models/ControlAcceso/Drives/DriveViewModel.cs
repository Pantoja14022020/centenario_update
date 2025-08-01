using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Drives
{
    public class DriveViewModel
    {
        public string NombreUnidad { get; set; } 
        public decimal EspacioDsiponible { get; set; }
        public decimal EspacioTotal { get; set; }
        public decimal EspacioUtilizado { get; set; }
 
    }
}
