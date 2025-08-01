using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Persona
{
    public class ActualizarInfoAdicionalDetenidoViewModel
    {
        public Guid PersonaId { get; set; }
        public string CumpleRequisitoLey { get; set; }
        public string DecretoLibertad { get; set; }
        public string DispusoLibertad { get; set; }
    }
}
