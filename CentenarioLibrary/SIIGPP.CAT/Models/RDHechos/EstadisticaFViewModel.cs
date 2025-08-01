using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RDHechos
{
    public class EstadisticaFViewModel
    {
        public Guid RHechoId { get; set; }
        public string Delito { get; set; }
        public string Localidad { get; set; }
        public string Municipio { get; set; }
        public DateTime Fecha { get; set; }
        public int NumeroDelitos { get; set; }
    }
}
