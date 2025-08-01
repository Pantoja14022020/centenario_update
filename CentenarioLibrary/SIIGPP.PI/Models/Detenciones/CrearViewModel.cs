using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.Detenciones
{
    public class CrearViewModel
    {
        public Guid RHechoId { get; set; }
        public Guid PersonaId { get; set; }
        public string Nuc { get; set; }
        public string MpAsignado { get; set; }
        public string Mesa { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaSalida { get; set; }
        public string FechaTraslado { get; set; }
        public string Status { get; set; }
        public string AutoridadQO { get; set; }
        public string AutoridadED { get; set; }
        public string Delito { get; set; }
        public string Tdelito { get; set; }
        public string Modalidad { get; set; }
        public string MOperandi { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
        public DateTime FechaUltimoStatus { get; set; }
        public string NumUnicoRegNacional { get; set; }
        public string TipoDetencion { get; set; }
        public string Pertenecnias { get; set; }
        public DateTime FechaHReingreso { get; set; }
    }
}
