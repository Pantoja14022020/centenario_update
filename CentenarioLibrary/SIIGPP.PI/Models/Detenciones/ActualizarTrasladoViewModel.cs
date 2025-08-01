using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.Detenciones
{
    public class ActualizarTrasladoViewModel
    {
        public Guid IdDetencion { get; set; }
        public string FechaTraslado { get; set; }
        public string Status { get; set; }
        public DateTime FechaUltimoStatus { get; set; }
    }
}
