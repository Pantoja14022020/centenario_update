using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSesion
{
    public class POST_SesionConjuntoViewModel
    {
        public Guid IdSC { get; set; }
        public Guid SesionId { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }

    }
}
