using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RExpediente
{
    public class PUT_NoDerivacionStatusViewModel
    {
        public Guid IdExpediente { get; set; } 
        public int NoDerivacion { get; set; }
        public string StatusAcepRech { get; set; }
        public string InformacionStatus { get; set; }
    }
}
