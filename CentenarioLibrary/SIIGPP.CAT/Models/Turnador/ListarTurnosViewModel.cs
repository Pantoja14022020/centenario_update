using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Turnador
{
    public class ListarTurnosViewModel
    {
        public Guid TurnoId { get; set; }

        public string serie { get; set; }
        public int noturno { get; set; }
        public DateTime fechaHoraInicio { get; set; }
        public Boolean status { get; set; }
        public Boolean statusReAsignado { get; set; }
        public Guid AgenciaId { get; set; }
        public string nombreAgencia { get; set; }
        public Guid rAtencionId { get; set; }
        public string atendidopor { get; set; }
        public string modulo { get; set; }
    }
}
