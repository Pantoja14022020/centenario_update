using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.Registro;

namespace SIIGPP.Entidades.M_Cat.ContencionesPersonas
{
    public class ContencionesPersona
    {
        public Guid IdContencionesPersona { get; set; }
        public Guid RAtencionId { get; set; }
        public RAtencion RAtencion { get; set; }
        public string QueRequirio { get; set; }
        public string NombrePersona { get; set; }
        public DateTime FechaSys { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string UUsuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
    }
}
