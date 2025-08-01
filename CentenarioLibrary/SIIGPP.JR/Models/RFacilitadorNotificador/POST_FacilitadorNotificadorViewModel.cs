using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RFacilitadorNotificador
{
    public class POST_FacilitadorNotificadorViewModel
    {
 
        public Guid ModuloServicioId { get; set; }
        public Boolean StatusAsignado { get; set; }
        public Boolean StatusActivo { get; set; }
    }
}
