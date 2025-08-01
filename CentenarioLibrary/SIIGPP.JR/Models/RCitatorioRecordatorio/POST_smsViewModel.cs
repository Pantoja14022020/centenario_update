using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RCitatorioRecordatorio
{
    public class POST_smsViewModel
    {
        public string token { get; set; } 
       public string c { get; set; }
        public string dest { get; set; }
        public string texto { get; set; }
    }
}
