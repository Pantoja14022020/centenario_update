using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.Detenciones
{
    public class ActualizarLiberacionViewModel
    {
        public Guid IdDetencion { get; set; }
        public string Status { get; set; }
        public string FechaSalida { get; set; }
        public string AutoridadQO { get; set; }
        public DateTime FechaUltimoStatus { get; set; }
    }
}
