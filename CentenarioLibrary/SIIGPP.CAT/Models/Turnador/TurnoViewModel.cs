using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Turnador
{
    public class TurnoViewModel
    {
        
        public Guid idTurno { get; set; }
        public string serie { get; set; }
        public int noturno { get; set; }
        public DateTime fechaHoraInicio { get; set; } 
        public Boolean status { get; set; }
        public Boolean statusReAsignado { get; set; }
        public Guid AgenciaId { get; set; }
        public Guid rAtencionId { get; set; }

     
    }
}
