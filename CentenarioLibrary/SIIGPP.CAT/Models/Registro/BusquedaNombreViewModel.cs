using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Registro
{
    public class BusquedaNombreViewModel
    {
        public Guid RAtencionId { get; set; }
        public string Persona { get; set; }
        public string Nuc { get; set; }
        public string Agencia { get; set; }
        public string Modulo { get; set; }
    }
}
