using SIIGPP.Entidades.M_Cat.PAtencion;
using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.PRegistro
{
    public class PreRap
    {
        [Key]
        public Guid IdPRap { get; set; }
        public Guid PAtencionId { get; set; }
        public PreAtencion preAtencion { get; set; }
        public Guid PersonaId { get; set; }
        public Persona persona {get;set;}
        public string ClasificacionPersona { get; set; }
        public Boolean PInicio { get; set; }
    }
}
