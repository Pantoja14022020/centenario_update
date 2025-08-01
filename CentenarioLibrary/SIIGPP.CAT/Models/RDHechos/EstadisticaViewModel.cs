using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RDHechos
{
    public class EstadisticaViewModel
    {
        public Guid RHechoId { get; set; }
        public string Delito { get; set; }
        public string ModalidadesDelito { get; set; }
        public string GradoEjecucion { get; set; }
        public string TipoRobado { get; set; }
        public string ConAlgunaParteCuerpo { get; set; }
        public string ViolenciaSinViolencia { get; set; }
        public string ConotroElemento { get; set; }
        public Boolean ArmaFuego { get; set; }
        public Boolean ArmaBlanca { get; set; }
    }
}
