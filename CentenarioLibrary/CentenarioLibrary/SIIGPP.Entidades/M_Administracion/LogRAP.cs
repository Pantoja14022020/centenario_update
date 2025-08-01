 
using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.DatosProtegidos;
namespace SIIGPP.Entidades.M_Administracion
{
    public class LogRAP
    {
        public Guid IdAdminRAP { get; set; }
        public Guid LogAdmonId { get; set; }
        public Guid IdRAP { get; set; }
        public Guid RAtencionId { get; set; }
        public Guid PersonaId { get; set; }
        public string ClasificacionPersona { get; set; }
        public Boolean PInicio { get; set; }
    }
}