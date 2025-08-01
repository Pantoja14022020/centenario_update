using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DDerivacion
{
    public class CrearViewModel
    {

        public Guid rHechoId { get; set; }
        public Guid DDerivacionId { get; set; }
        public string Espesificaciones { get; set; }
        public string FechaDerivacion { get; set; }
        public DateTime? FechaSys { get; set; }
        public string uDistrito { get; set; }
        public string uDirSubPro { get; set; }
        public string uAgencia { get; set; }
        public string uNombre { get; set; }
        public string UPuesto { get; set; }
    }
}
