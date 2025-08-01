using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.Detenciones
{
    public class ActualizarStatusViewModel
    {
        public Guid IdDetencion { get; set; }
        public string Status { get; set; }
        public DateTime FechaUltimoStatus { get; set; }
    }
}
