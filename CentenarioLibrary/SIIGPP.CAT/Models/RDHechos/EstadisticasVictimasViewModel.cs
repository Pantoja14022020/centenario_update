using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RDHechos
{
    public class EstadisticasVictimasViewModel
    {
        public Guid Ratencion { get; set; }
        public Guid Rhecho { get; set; }
        public string Sexo { get; set; }
        public string Edad { get; set; }
        public int CantidadV { get; set; }
    }
}
