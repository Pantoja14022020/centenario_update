using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_PI.PersonasVisita;

namespace SIIGPP.Entidades.M_PI.FPersonas
{
   public class FPersona
    {
        public Guid IdFPersona { get; set; }
        public Guid PIPersonaVisitaId { get; set; }
        public PIPersonaVisita PIPersonaVisita { get; set; }
        public string TipoDocumento { get; set; }
        public string DescripcionDocumento { get; set; }
        public string FechaRegistro { get; set; }
        public string Puesto { get; set; }
        public string Ruta { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
    }
}
