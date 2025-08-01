using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RAcuerdoReparatorio
{
    public class PUT_CoordinadorJuridicoViewModel
    {
        public Guid IdAcuerdoReparatorio { get; set; }
       
        public string StatusRespuestaCoordinadorJuridico { get; set; }
        public string RespuestaCoordinadorJuridico { get; set; } 
    }
}
