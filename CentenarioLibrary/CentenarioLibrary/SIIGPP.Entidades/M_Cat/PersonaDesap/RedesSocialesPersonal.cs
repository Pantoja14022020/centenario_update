using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.Registro;
//using SIIGPP.Entidades.M_Cat.PersonaDesap;

namespace SIIGPP.Entidades.M_Cat.PersonaDesap
{
    public class RedesSocialesPersonal
    {
        public Guid IdRedesSocialesPersonal { get; set; }
        public Guid PersonaId { get; set; }
        public Persona persona { get; set; }
        public Guid RedSocialId { get; set; }
        public RedesSociales RedSocial { get; set; }
        public string Enlace { get; set; }
    }
}
