 
using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.DatosProtegidos;
namespace SIIGPP.Entidades.M_Cat.Registro
{
    public class RAP
    {
        public Guid IdRAP { get; set; }
        public RAtencion RAtencion { get; set; }
        public Guid RAtencionId { get; set; }
        public Persona Persona { get; set; }
        public Guid PersonaId { get; set; }
        public string ClasificacionPersona { get; set; }
        public Boolean PInicio { get; set; }
        public DatoProtegido DatoProtegido { get; set; }



    }
}
