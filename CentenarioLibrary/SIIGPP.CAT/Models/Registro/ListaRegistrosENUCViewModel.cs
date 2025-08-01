using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Registro
{
    public class ListaRegistrosENUCViewModel
    {
        public Guid IdRAP { get; set; }

        public Guid RAtencionId { get; set; }
        public Guid PersonaId { get; set; }
        public string ClasificacionPersona { get; set; }
    }
}
