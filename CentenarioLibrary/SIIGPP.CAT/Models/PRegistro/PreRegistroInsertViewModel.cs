using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.PRegistro
{
    public class PreRegistroInsertViewModel
    {
        public Guid distritoId { get; set; }
       public string Ndenuncia { get; set; }
       public Boolean Asignado { get; set; }
       public DateTime fechaSuceso { get; set; }
       public string RBreve { get; set; }


    }
}
