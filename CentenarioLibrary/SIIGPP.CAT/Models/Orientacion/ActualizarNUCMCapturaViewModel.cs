using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Orientacion
{
    public class ActualizarNUCMCapturaViewModel
    {
        public Guid IdRHecho { get; set; }
        public Guid ratencionId { get; set; }
        public Guid? nucId { get; set; }
        public DateTime FechaElevacion { get; set; }
    }
}
