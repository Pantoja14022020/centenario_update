using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_PI.PersonasVisita
{
    public class PIPersonaVisita
    {
        public Guid IdPIPersonaVisita { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public int Edad { get; set; }
        public string Ocupacion { get; set; }
        public int Telefono1 { get; set; }
        public int Telefono2 { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }

    }
}
