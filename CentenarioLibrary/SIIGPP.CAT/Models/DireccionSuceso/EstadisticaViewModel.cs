using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DireccionSuceso
{
    public class EstadisticaViewModel
    {
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public string Colonia { get; set; }
        public Guid rHechoId { get; set; }
    }
}
