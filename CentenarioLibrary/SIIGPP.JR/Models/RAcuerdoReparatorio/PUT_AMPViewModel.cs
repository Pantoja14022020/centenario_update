using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RAcuerdoReparatorio
{
    public class PUT_AMPViewModel
    {
        public Guid IdAcuerdoReparatorio { get; set; }
        public string StatusRespuestaAMP { get; set; }
        public string RespuestaAMP { get; set; }
        public DateTime? FechaRespuestaAMP { get; set; }

    }
}
