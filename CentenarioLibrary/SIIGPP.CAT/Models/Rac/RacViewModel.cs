using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Rac
{
    public class RacViewModel
    {
        public Guid distritoId { get; set; }
        public Guid agenciaId { get; set; }
        public string Ndenuncia { get; set; }
        public Boolean Asignado { get; set; }

    }
}
