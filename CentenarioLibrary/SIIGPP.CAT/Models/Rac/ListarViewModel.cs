using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Rac
{
    public class ListarViewModel
    {
        public Guid Idrac { get; set; }
        public string RAC { get; set; }
        public string Ndenuncia { get; set; }
        public Boolean Asignado { get; set; }
    }
}
