using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RFacilitadorNotificador
{
    public class PUT_StatusAsignadoViewModel
    {
        public Guid IdFacilitadorNotificador { get; set; }
        public Guid distritoId { get; set; }
        public Boolean StatusAsignado { get; set; } 
    }
}
