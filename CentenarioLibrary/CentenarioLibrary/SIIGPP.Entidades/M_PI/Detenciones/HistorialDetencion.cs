using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_PI.Detenciones
{
    public class HistorialDetencion
    {
        public Guid IdHistorialDetencion { get; set; }
        public Guid DetencionId { get; set; }
        public Detencion Detencion { get; set; }
        public string StatusPasado { get; set; }
        public string StatuusNuevo { get; set; }
        public DateTime Fechasys { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }

    }
}
