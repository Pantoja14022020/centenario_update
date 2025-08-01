using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.MedidasProteccion
{
    public class NoMedidasProteccionViewModel
    {
        public Guid IdNoMedidasProteccion { get; set; }
        public Guid MedidasproteccionId { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
    }
}
