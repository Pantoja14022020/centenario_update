using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.REnvio
{
    public class PUT_StatusAMPOViewModel
    {
        public Guid IdEnvio { get; set; }
        public string StatusAMPO{ get; set; }
        public string InfoAMPO { get; set; }
        public string oficioAMPO { get; set; }
        public string StatusGeneral { get; set; }
        public string FirmaInfoAMPO { get; set; }
        public string PuestoFirmaAMPO { get; set; }
    }
}
