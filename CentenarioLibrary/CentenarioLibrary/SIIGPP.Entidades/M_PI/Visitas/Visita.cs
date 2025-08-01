using SIIGPP.Entidades.M_PI.PersonasVisita;
using SIIGPP.Entidades.M_PI.Detenciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_PI.Visitas
{
    public class Visita
    {
        public Guid IdVisita { get; set; }
        public Guid PIPersonaVisitaId { get; set; }
        public PIPersonaVisita PIPersonaVisita { get; set; }
        public Guid DetencionId { get; set; }
        public Detencion Detencion { get; set; }
        public string FechayHora { get; set; }
        public string HILocutorio { get; set; }
        public string HSLocutorio { get; set; }
        public string QAutorizaVisita { get; set; }
        public string MotivoVisita { get; set; }
        public string RDetenido { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
    }
}
