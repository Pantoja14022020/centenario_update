using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Catalogos.Sexo
{
    public class RangoEdadViewModel
    {
        public Guid IdRangoEdad { get; set; }
        public string Rango { get; set; }
        public Boolean Activo { get; set; }
        public int OrdenEdad { get; set; }

    }
}
