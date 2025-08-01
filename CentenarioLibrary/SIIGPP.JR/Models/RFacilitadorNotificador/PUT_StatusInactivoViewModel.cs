using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RFacilitadorNotificador
{
    public class PUT_StatusInactivoViewModel
    {
        public Guid IdFacilitadorNotificador { get; set; } 
        public Boolean StatusActivo { get; set; }
    }
}
