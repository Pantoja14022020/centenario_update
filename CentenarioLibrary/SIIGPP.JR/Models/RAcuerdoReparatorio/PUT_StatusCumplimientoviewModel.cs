using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RAcuerdoReparatorio
{
    public class PUT_StatusCumplimientoviewModel
    {
        public Guid IdAcuerdo { get; set; }
        public string StatusCumplimiento { get; set; }
    }
}
