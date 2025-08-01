using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.CMedicosPSR
{
    public class CMedicoPSRViewModel
    {
        public Guid IdCMedicoPSR { get; set; }
        public Guid ModuloServicioId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public string NumUnicoRegistro { get; set; }
        public DateTime Fechasys { get; set; }
        public string Status { get; set; }
        public string Respuesta { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime FechaUltimoStatus { get; set; }
        public string NumeroAgencia { get; set; }
        public string TelefonoAgencia { get; set; }
        public string NodeSolicitud { get; set; }
        public string NumeroControl { get; set; }
        public string NumeroDistrito { get; set; }
        public string NumeroControlf { get; set; }

    }
}
