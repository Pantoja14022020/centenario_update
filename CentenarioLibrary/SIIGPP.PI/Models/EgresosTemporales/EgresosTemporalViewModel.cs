using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.EgresosTemporales
{
    public class EgresosTemporalViewModel
    {
        public Guid IdEgresoTemporal { get; set; }
        public Guid DetencionId { get; set; }
        public string Motivo { get; set; }
        public string Horaegreso { get; set; }
        public string QuienSolicita { get; set; }
        public string QuienAutoriza { get; set; }
        public string Notas { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
    }
}
