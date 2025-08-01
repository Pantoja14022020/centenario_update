using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Victimas
{
    public class ActualizarClasificacionViewModel
    {

        public string ClasificacionPersona { get; set; }
        public Guid IdRAP { get; set; }

        public Guid PersonaId { get; set; }
        public Boolean Registro { get; set; }
        public Boolean VerI { get; set; }
        public Boolean VerR { get; set; }
        public string Parentesco { get; set; }
    }

    public class ActualizarStatusDesaparecidoViewModel
    {
       
    }
}
