using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Registro
{
    public class DEPersonaViewModel
    {

        public string NombreCompleto { get; set; }
        public string ClasificacionPersona { get; set; }
        public string Curp { get; set; }
        public Guid IdDEscucha { get; set; }
        public Guid PersonaId { get; set; }
       

    }
}
