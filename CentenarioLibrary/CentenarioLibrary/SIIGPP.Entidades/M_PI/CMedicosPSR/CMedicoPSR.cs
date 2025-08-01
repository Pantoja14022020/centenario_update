using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_PI.CMedicosPSR
{
    public class CMedicoPSR
    {
        public Guid IdCMedicoPSR { get; set; }
        public ModuloServicio ModuloServicio { get; set; }
        public Guid ModuloServicioId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string NumUnicoRegistro { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }

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

    }
}
