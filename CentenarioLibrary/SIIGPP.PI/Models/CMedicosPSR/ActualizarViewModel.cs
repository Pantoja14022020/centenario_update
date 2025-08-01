using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.CMedicosPSR
{
    public class ActualizarViewModel
    {
        public Guid IdCMedicoPSR { get; set; }
        public Guid ModuloServicioId { get; set; }
        public string Status { get; set; }
        public string Respuesta { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime FechaUltimoStatus { get; set; }
        public string NumeroControl { get; set; }
    }
}
