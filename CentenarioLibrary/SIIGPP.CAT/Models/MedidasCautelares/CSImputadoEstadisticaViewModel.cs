using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.MedidasCautelares
{
    public class CSImputadoEstadisticaViewModel
    {
        public Guid RhechoId { get; set; }
        public Guid Ratencion { get; set; }
        public Guid PersonaId { get; set; }
        public Boolean ConMedida { get; set; }
        public string MedidaCautelar { get; set; }
    }
}
