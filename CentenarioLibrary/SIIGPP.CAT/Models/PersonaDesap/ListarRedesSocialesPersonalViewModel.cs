using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.PersonaDesap
{
    public class ListarRedesSocialesPersonalViewModel
    {
        public Guid IdRedesSocialesPersonal { get; set; }
        public Guid PersonaId { get; set; }
        public Guid RedSocialId { get; set; }
        public string Enlace { get; set; }
        public string RedSocial { get; set; }
    }
}
